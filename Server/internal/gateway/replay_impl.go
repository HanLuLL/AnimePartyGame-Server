package gateway

import (
	"context"
	"crypto/rand"
	"encoding/hex"

	store "astralparty-server/internal/db"
	modelpb "astralparty-server/internal/protocol/gen/model"
	protocolpb "astralparty-server/internal/protocol/gen/protocol"
	"google.golang.org/protobuf/proto"
)

// This file implements the replay / fight-record system. When a game finishes
// the server generates a replayID, records one fight_records row per participant
// (the records drive the profile's record/replay lists and the
// GetPlayerFightRecordC2S query), and stores the room snapshot so the client
// can rebuild the battle from ReplaySnapshotS2C.

// newReplayID returns a random hex identifier used to group one fight's
// records and snapshot.
func newReplayID() string {
	b := make([]byte, 8)
	_, _ = rand.Read(b)
	return hex.EncodeToString(b)
}

// fightDataMessage converts one stored fight-record row into the client's
// PlayerFightData, enriched with the player's current profile fields.
func (s *Server) fightDataMessage(ctx context.Context, row store.FightRecordRow) *modelpb.PlayerFightData {
	data := &modelpb.PlayerFightData{PlayerId: row.PlayerID, Rank: row.Rank, HeroId: row.HeroID, MapType: row.MapType, IsGiveUp: row.IsGiveUp}
	if p, err := s.store.LookupPlayer(ctx, row.PlayerID); err == nil {
		data.Name = p.Nick
		data.Lv = p.Level
		data.Gold = p.Gold
		data.Slot = p.Slot
		data.PlayerLevel = p.Level
	}
	return data
}

// shortFightMessage converts stored rows into the ShowPlayerShortFight entries
// shown on the profile card.
func (s *Server) shortFightMessage(rows []store.FightRecordRow) []*protocolpb.ShowPlayerShortFight {
	out := make([]*protocolpb.ShowPlayerShortFight, 0, len(rows))
	for i, row := range rows {
		out = append(out, &protocolpb.ShowPlayerShortFight{
			Time: row.Time, Rank: row.Rank, HeroId: row.HeroID,
			Index: int32(i), MapType: row.MapType, ReplayId: row.ReplayID,
		})
	}
	return out
}

// replaySnapshotMessage serializes the ReplaySnapshotS2C payload stored with a
// replay. viewerID identifies the requesting player (0 while recording).
func (s *Server) replaySnapshotMessage(ctx context.Context, room store.Room, viewerID int64) ([]byte, error) {
	msg := &protocolpb.ReplaySnapshotS2C{PlayerId: viewerID, Room: s.roomMessage(room)}
	return proto.Marshal(msg)
}

// handleGetPlayerFightRecord answers GetPlayerFightRecordC2S: one page of the
// player's recorded fights. isReplay selects the replay-saving list.
func (s *Server) handleGetPlayerFightRecord(ctx context.Context, sess *Session, q *protocolpb.GetPlayerFightRecordC2S) (DispatchResult, error) {
	_, _, selfID, _, loggedIn := sess.identity()
	if !loggedIn {
		return DispatchResult{Err: ErrAuth}, nil
	}
	targetID := q.GetPlayerId()
	if targetID == 0 {
		targetID = selfID
	}
	if targetID < 1 {
		return DispatchResult{Err: ErrInvalidParam}, nil
	}
	rows, err := s.store.FightRecordsForPlayer(ctx, targetID, q.GetIsReplay(), q.GetIndex())
	if err != nil {
		return DispatchResult{}, err
	}
	out := make([]*modelpb.PlayerFightData, 0, len(rows))
	for _, row := range rows {
		out = append(out, s.fightDataMessage(ctx, row))
	}
	return DispatchResult{Message: &protocolpb.GetPlayerFightRecordS2C{RecordData: out}}, nil
}

// replayRecordForPlayer returns the ShowPlayerShortFight lists for a profile
// card: regular fight history and the replay-saving list.
func (s *Server) replayRecordForPlayer(ctx context.Context, playerID int64) ([]*protocolpb.ShowPlayerShortFight, []*protocolpb.ShowPlayerShortFight, error) {
	recordRows, err := s.store.FightRecordsForPlayer(ctx, playerID, false, 0)
	if err != nil {
		return nil, nil, err
	}
	replayRows, err := s.store.FightRecordsForPlayer(ctx, playerID, true, 0)
	if err != nil {
		return nil, nil, err
	}
	return s.shortFightMessage(recordRows), s.shortFightMessage(replayRows), nil
}
