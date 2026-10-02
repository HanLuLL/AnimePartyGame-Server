package gateway

import (
	"context"

	store "astralparty-server/internal/db"
	protocolpb "astralparty-server/internal/protocol/gen/protocol"
)

// skipDeadPlayerAction mirrors the decompiled tutorial turn loop: notify the
// room that the player's action starts, restore a dead hero, and advance past
// that action without rolling or moving.
func (s *Server) skipDeadPlayerAction(ctx context.Context, roomID int64, player store.Player, round, pending int32, phase string, ids []int64) (bool, int64, int32, []Push, error) {
	if player.HP > 0 || pending != 0 || phase != store.TurnPhaseThrowDice {
		return false, 0, round, nil, nil
	}
	maxHP := s.domain.HeroMaxHP(player.HeroID)
	clearBuffIDs := make([]int32, 0, len(player.Buffs))
	for _, buff := range player.Buffs {
		if s.domain.BuffClearsOnDeath(buff.BuffID) {
			clearBuffIDs = append(clearBuffIDs, buff.BuffID)
		}
	}
	oldHP, newHP, nextPlayer, nextRound, removedBuffs, created, err := s.store.ReviveAndSkipDeadTurn(ctx, roomID, player.ID, maxHP, clearBuffIDs)
	if err != nil {
		s.log.Error("dead player's turn recovery failed", "room_id", roomID, "player_id", player.ID, "err", err)
		return false, 0, round, nil, err
	}
	if !created {
		return false, nextPlayer, nextRound, nil, nil
	}
	s.log.Info("dead player action skipped and HP restored", "room_id", roomID, "player_id", player.ID, "old_hp", oldHP, "hp", newHP, "buffs_cleared", len(removedBuffs), "next_player_id", nextPlayer, "round", nextRound)
	attrEffects := []*protocolpb.HeroAttrEffect{{
		PlayerId: player.ID,
		Data: &protocolpb.HeroAttrEffect_Hp{Hp: &protocolpb.HeroHpChangeS2C{
			PlayerId: player.ID, ChangeHp: newHP - oldHP, OriHp: oldHP,
			CurrHp: newHP, RealChangeHp: newHP - oldHP, MaxHp: maxHP,
		}},
	}}
	for _, buff := range removedBuffs {
		attrEffects = append(attrEffects, &protocolpb.HeroAttrEffect{
			PlayerId: player.ID,
			Data: &protocolpb.HeroAttrEffect_Buff{Buff: &protocolpb.HeroBuffChangeS2C{
				PlayerId: player.ID, Buff: buffMessage(buff), Op: protocolpb.HeroBuffChangeS2C_Delete,
			}},
		})
	}
	if len(player.Bombs) > 0 {
		attrEffects = append(attrEffects, bombAttrEffect(player.ID, nil))
	}
	pushes := []Push{
		s.pushFor("GameRoundChangeS2C", &protocolpb.GameRoundChangeS2C{Round: round}, ids, 0),
		s.pushFor("ActionStartNotifyS2C", &protocolpb.ActionStartNotifyS2C{PlayerId: player.ID, IsDie: true}, ids, 0),
		s.pushFor("UpdateHeroAttrS2C", &protocolpb.UpdateHeroAttrS2C{
			PlayerId:    player.ID,
			Cause:       &protocolpb.CauseOrigin{},
			EffectDatas: attrEffects,
		}, ids, 0),
	}
	return true, nextPlayer, nextRound, pushes, nil
}
