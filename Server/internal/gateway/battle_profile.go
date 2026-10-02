package gateway

import (
	"context"

	store "astralparty-server/internal/db"
	protocolpb "astralparty-server/internal/protocol/gen/protocol"
)

func (s *Server) handleGetHeroInfo(ctx context.Context, sess *Session, q *protocolpb.GetHeroInfoC2S) (DispatchResult, error) {
	_, playerID, ok := sessionPlayer(sess)
	if !ok {
		return DispatchResult{Err: ErrAuth}, nil
	}
	if q.GetPlayerId() <= 0 {
		return DispatchResult{Err: ErrInvalidParam}, nil
	}
	roomID, err := s.store.RoomForPlayer(ctx, playerID)
	if err != nil || roomID <= 0 {
		return DispatchResult{Err: ErrRoomActionIncorrect}, nil
	}
	room, err := s.store.RoomSnapshot(ctx, roomID)
	if err != nil {
		return DispatchResult{}, err
	}
	if room.State != 25 {
		return DispatchResult{Err: ErrRoomActionIncorrect}, nil
	}
	var target *store.Player
	for i := range room.Players {
		if room.Players[i].ID == q.GetPlayerId() {
			target = &room.Players[i]
			break
		}
	}
	if target == nil {
		return DispatchResult{Err: ErrRoomPlayerNotExist}, nil
	}
	stats, err := s.store.GetBattlePlayerStats(ctx, room.ID, target.ID)
	if err != nil {
		return DispatchResult{}, err
	}
	cooldown := target.SkillCooldowns[s.activeSkillID(room, *target)]
	if cooldown < 0 {
		cooldown = 0
	}
	return DispatchResult{Message: &protocolpb.GetHeroInfoS2C{
		PlayerId: target.ID, KillCount: stats.KillCount, TotalDie: stats.TotalDie,
		TotalDamage: stats.TotalDamage, TotalInjured: stats.TotalInjured,
		TreatmentScore: stats.TreatmentScore, Cd: cooldown,
	}}, nil
}
