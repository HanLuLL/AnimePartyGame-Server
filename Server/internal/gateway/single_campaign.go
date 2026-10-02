package gateway

import (
	"context"
	"encoding/json"
	"fmt"
	"net/http"
	"regexp"
	"strconv"
	"strings"

	store "astralparty-server/internal/db"
	protocolpb "astralparty-server/internal/protocol/gen/protocol"
	"google.golang.org/protobuf/proto"
)

var validReplayID = regexp.MustCompile(`^[A-Za-z0-9_-]{1,64}$`)

func protoMarshal(msg proto.Message) ([]byte, error) { return proto.Marshal(msg) }

func jsonUnmarshalStrict(raw json.RawMessage, v any) error { return json.Unmarshal(raw, v) }

// ---------------------------------------------------------------- replay HTTP serving
//
// The client's ReplayLogic downloads a replay by id from a CDN base URL
// ({base}/{replayId}) and parses the downloaded bytes with
// ReplayLoader.LoadServerPackStream: a stream of {int16 cmdId, int32 length,
// payload} frames that must contain a RunningGameS2C (cmd 1003) whose Room is
// running and a GameFinishS2C (cmd 1016). The server stores a ReplaySnapshotS2C
// protobuf at game end, so the endpoint converts it into that frame stream:
// one 1113 frame (snapshot with a running Room) followed by one 1016 frame
// (GameFinish) reconstructed from the fight records.

// replayFrameAppender serializes one wire frame in the client's replay-pack
// layout (big-endian cmdId int16 + length int32 + protobuf payload).
func replayFrameAppender(out []byte, cmdID uint16, payload []byte) []byte {
	out = append(out, byte(cmdID>>8), byte(cmdID))
	n := int32(len(payload))
	out = append(out, byte(n>>24), byte(n>>16), byte(n>>8), byte(n))
	return append(out, payload...)
}

// ReplayHTTPHandler serves recorded replays by id from the dispatch web mux.
// GET /replays/{id} returns the replay-pack byte stream the client feeds into
// ReplaySession.LoadBytes. The stream synthesizes the frames the client's
// loader requires (RunningGameS2C / GameFinishS2C) around the stored
// ReplaySnapshotS2C so playback starts from the recorded running room.
func (s *Server) ReplayHTTPHandler() http.Handler {
	return http.HandlerFunc(s.replayHTTPServe)
}

func (s *Server) replayHTTPServe(w http.ResponseWriter, r *http.Request) {
	if r.Method != http.MethodGet && r.Method != http.MethodHead {
		w.Header().Set("Allow", "GET, HEAD")
		http.Error(w, "method not allowed", http.StatusMethodNotAllowed)
		return
	}
	replayID := strings.TrimPrefix(r.URL.Path, "/replays/")
	if !validReplayID.MatchString(replayID) {
		http.NotFound(w, r)
		return
	}
	snapshot, _, ok, err := s.store.ReplaySnapshot(r.Context(), replayID)
	if err != nil {
		s.log.Error("replay load failed", "replay_id", replayID, "err", err)
		http.Error(w, "replay unavailable", http.StatusInternalServerError)
		return
	}
	if !ok || len(snapshot) == 0 {
		http.NotFound(w, r)
		return
	}
	finish, err := s.replayFinishPayload(r.Context(), replayID)
	if err != nil {
		s.log.Error("replay finish lookup failed", "replay_id", replayID, "err", err)
		http.Error(w, "replay unavailable", http.StatusInternalServerError)
		return
	}
	body := make([]byte, 0, len(snapshot)+len(finish)+16)
	body = replayFrameAppender(body, 1113, snapshot)
	if len(finish) > 0 {
		body = replayFrameAppender(body, 1016, finish)
	}
	w.Header().Set("Content-Type", "application/octet-stream")
	w.Header().Set("Content-Disposition", fmt.Sprintf("attachment; filename=%q", replayID))
	w.Header().Set("Cache-Control", "no-store")
	w.Header().Set("Content-Length", strconv.Itoa(len(body)))
	if r.Method == http.MethodHead {
		return
	}
	_, _ = w.Write(body)
}

// replayFinishPayload rebuilds the GameFinishS2C protobuf the client's replay
// loader expects after the snapshot frames, from the fight records saved with
// this replay id.
func (s *Server) replayFinishPayload(ctx context.Context, replayID string) ([]byte, error) {
	rows, err := s.store.FightRecordsForReplay(ctx, replayID)
	if err != nil {
		return nil, err
	}
	if len(rows) == 0 {
		return nil, nil
	}
	rank := map[int64]int32{}
	winer := int64(0)
	for _, row := range rows {
		rank[row.PlayerID] = row.Rank
		if row.Rank == 1 {
			winer = row.PlayerID
		}
	}
	msg := &protocolpb.GameFinishS2C{
		ReplayId:   replayID,
		RoomId:     rows[0].RoomID,
		Winer:      winer,
		FinishTime: rows[0].Time,
		MapType:    rows[0].MapType,
		Rank:       rank,
		Awards:     map[int32]int32{},
	}
	return protoMarshal(msg)
}

// ---------------------------------------------------------------- single-player campaign

// handleSingleCampaign starts a single-player campaign run: the server
// validates the configured campaign level, creates a persistent room in the
// hero-choice state for the requesting player, and returns it like the room
// the client's OnSingleCampaignS2CServerCallBack expects.
func (s *Server) handleSingleCampaign(ctx context.Context, sess *Session, q *protocolpb.SingleCampaignC2S) (DispatchResult, error) {
	_, pid, ok := sessionPlayer(sess)
	if !ok {
		return DispatchResult{Err: ErrAuth}, nil
	}
	levelID := int64(0)
	skipStory := false
	if q != nil {
		levelID = q.GetId()
		if levelID == 0 {
			levelID = int64(q.GetLevelId())
		}
		skipStory = q.GetSkipStory()
	}
	if levelID == 0 {
		if first, found := s.resources.FirstID("Campaign_levels"); found {
			levelID = first
		}
	}
	level, ok := s.campaignLevel(levelID)
	if !ok {
		return DispatchResult{Err: ErrInvalidParam}, nil
	}
	p, err := s.store.LookupPlayer(ctx, pid)
	if err != nil {
		return DispatchResult{}, err
	}
	if roomID, err := s.store.RoomForPlayer(ctx, pid); err == nil && roomID > 0 {
		return DispatchResult{Err: ErrRoomPlayerAlreadyJoin}, nil
	}
	settings := store.RoomCreate{MapID: level.MapID, Mode: level.MapMode, SkipStory: skipStory}
	room, err := s.store.CreateRoom(ctx, p, settings)
	if err != nil {
		return DispatchResult{Err: mapRoomError(err)}, nil
	}
	return DispatchResult{Message: &protocolpb.SingleCampaignS2C{Room: s.roomMessage(room)}}, nil
}

// campaignLevelRow is the parsed Campaign_levels row the campaign handler uses.
type campaignLevelRow struct {
	MapID   int32
	MapMode int32
}

// campaignLevel resolves a configured campaign level. Only the fields the
// server needs to create the room are read; everything else stays client-side.
func (s *Server) campaignLevel(levelID int64) (campaignLevelRow, bool) {
	raw, ok := s.resources.Get("Campaign_levels", levelID)
	if !ok {
		return campaignLevelRow{}, false
	}
	var row struct {
		ID          int64 `json:"id"`
		MapID       int32 `json:"mapID"`
		MapModeType struct {
			Value int32 `json:"value"`
		} `json:"mapModeType"`
	}
	if err := jsonUnmarshalStrict(raw, &row); err != nil || row.ID == 0 {
		return campaignLevelRow{}, false
	}
	return campaignLevelRow{MapID: row.MapID, MapMode: row.MapModeType.Value}, true
}

// handleSyncSingleGameData is implemented in misc_impl.go; the campaign
// progress helpers below extend it with durable per-player persistence.

// persistSingleGameProgress stores a completed/updated campaign run reported
// through SyncSingleGameDataC2S: the level is marked passed, the best score is
// kept, and the client's opaque blob is saved for the next session.
func (s *Server) persistSingleGameProgress(ctx context.Context, playerID int64, levelID, stageID, score int32, stageLevel map[int32]int32, data map[string]json.RawMessage) error {
	progress, err := s.store.LoadSingleProgress(ctx, playerID)
	if err != nil {
		return err
	}
	if levelID > 0 {
		progress.LevelPass[levelID] = 1
	}
	if stageID > 0 {
		progress.StageLevel[stageID] = levelID
	}
	for stage, level := range stageLevel {
		if stage > 0 && level > 0 {
			progress.StageLevel[stage] = level
		}
	}
	if score > progress.MaxScore {
		progress.MaxScore = score
	}
	if data == nil {
		data = progress.Data
	}
	return s.store.SaveSingleProgress(ctx, playerID, progress.LevelPass, progress.StageLevel, progress.MaxScore, data)
}
