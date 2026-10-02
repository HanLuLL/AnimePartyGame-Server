package gateway

import (
	"context"
	"time"

	store "astralparty-server/internal/db"
	protocolpb "astralparty-server/internal/protocol/gen/protocol"
)

func (s *Server) DisconnectPlayer(playerID int64) {
	for _, sess := range s.hub.sessions(playerID) {
		sess.shutdown()
	}
}

func (s *Server) finishRoomPush(ctx context.Context, room store.Room, winnerTeamID int64) (Push, error) {
	statsByPlayer, err := s.store.FinishGameWithStats(ctx, room.ID, winnerTeamID)
	if err != nil {
		return Push{}, err
	}
	newAchieve := make(map[int64]*protocolpb.PlayerFinishAchieve, len(room.Players))
	for _, player := range room.Players {
		stats := statsByPlayer[player.ID]
		newAchieve[player.ID] = &protocolpb.PlayerFinishAchieve{
			KillCount:      stats.KillCount,
			TotalDamage:    stats.TotalDamage,
			TotalDie:       stats.TotalDie,
			TotalInjured:   stats.TotalInjured,
			TreatmentScore: stats.TreatmentScore,
		}
	}
	// Ranks let every participant's fight record carry a final placement; the
	// winner team's members are flagged as replays.
	ranks := make(map[int64]int32, len(room.Players))
	for i, player := range room.Players {
		if winnerTeamID != 0 && player.Slot/2 == int32(winnerTeamID/10-1) {
			ranks[player.ID] = 1
			continue
		}
		ranks[player.ID] = int32(i + 1)
	}
	replayID := s.persistFightRecords(ctx, room, ranks)
	msg := &protocolpb.GameFinishS2C{
		RoomId:     room.ID,
		Winer:      winnerTeamID,
		FinishTime: time.Now().Unix(),
		NewAchieve: newAchieve,
		MapType:    room.Mode,
		Awards:     map[int32]int32{},
		Rank:       ranks,
		ReplayId:   replayID,
	}
	return s.pushFor("GameFinishS2C", msg, memberIDs(room), 0), nil
}

// persistFightRecords stores the replay snapshot and one fight_records row per
// human participant, returning the shared replay id.
func (s *Server) persistFightRecords(ctx context.Context, room store.Room, ranks map[int64]int32) string {
	replayID := newReplayID()
	now := time.Now().Unix()
	snapshot, err := s.replaySnapshotMessage(ctx, room, 0)
	if err != nil {
		s.log.Error("replay snapshot could not be built", "room_id", room.ID, "err", err)
	}
	if len(snapshot) > 0 {
		if err = s.store.SaveReplaySnapshot(ctx, replayID, room.ID, snapshot); err != nil {
			s.log.Error("replay snapshot could not be saved", "room_id", room.ID, "err", err)
		}
	}
	for _, player := range room.Players {
		if player.IsBot {
			continue
		}
		rank := ranks[player.ID]
		if err = s.store.SaveFightRecord(ctx, store.FightRecordRow{
			ReplayID: replayID, RoomID: room.ID, PlayerID: player.ID, Rank: rank,
			HeroID: player.HeroID, MapType: room.Mode, IsReplay: rank == 1,
			Time: now, CreatedAt: now,
		}); err != nil {
			s.log.Error("fight record could not be saved", "room_id", room.ID, "player_id", player.ID, "err", err)
		}
	}
	return replayID
}
