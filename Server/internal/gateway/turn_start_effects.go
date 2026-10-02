package gateway

import (
	"context"

	store "astralparty-server/internal/db"
	protocolpb "astralparty-server/internal/protocol/gen/protocol"
)

const buffRoundCountTypeRound int32 = 1

func (s *Server) resolveTurnStartEffects(ctx context.Context, roomID int64, round int32, room store.Room, player store.Player, recipients []int64) ([]Push, error) {
	if player.HospitalRounds > 0 {
		return nil, nil
	}
	if landType, exists := s.domain.LandTypeAt(room, player.NodeID); exists && landType == 13 {
		return nil, nil
	}
	maxHP := s.domain.HeroMaxHP(player.HeroID)
	roundBuffIDs := make([]int32, 0, len(player.Buffs))
	for _, buff := range player.Buffs {
		countType, configured := s.domain.BuffRoundCountType(buff.BuffID)
		if configured && countType == buffRoundCountTypeRound {
			roundBuffIDs = append(roundBuffIDs, buff.BuffID)
		}
	}
	poisonDamage := int32(1)
	if params := s.resources.HeroPassiveSkillParams(player.HeroID, hero11011HealingDamageSkillID, usesPVEPassiveSkills(room.Mode)); len(params) >= 2 && params[1] > 0 {
		poisonDamage += params[1]
	}
	result, applied, err := s.store.ProcessRoundBuffsTurnStart(ctx, roomID, player.ID, round, maxHP, poisonDamage, roundBuffIDs)
	if err != nil {
		return nil, err
	}
	if !applied || result.OldHP == result.NewHP && len(result.UpdatedBuffs) == 0 && len(result.RemovedBuffs) == 0 {
		return nil, nil
	}
	effects := make([]*protocolpb.HeroAttrEffect, 0, 1+len(result.UpdatedBuffs)+len(result.RemovedBuffs))
	if result.OldHP != result.NewHP {
		change := result.NewHP - result.OldHP
		effects = append(effects, &protocolpb.HeroAttrEffect{
			PlayerId: player.ID,
			Data: &protocolpb.HeroAttrEffect_Hp{Hp: &protocolpb.HeroHpChangeS2C{
				PlayerId: player.ID, ChangeHp: change, OriHp: result.OldHP,
				CurrHp: result.NewHP, RealChangeHp: change, MaxHp: maxHP,
			}},
		})
	}
	for _, buff := range result.UpdatedBuffs {
		effects = append(effects, &protocolpb.HeroAttrEffect{
			PlayerId: player.ID,
			Data: &protocolpb.HeroAttrEffect_Buff{Buff: &protocolpb.HeroBuffChangeS2C{
				PlayerId: player.ID, Buff: buffMessage(buff), Op: protocolpb.HeroBuffChangeS2C_Update,
			}},
		})
	}
	for _, buff := range result.RemovedBuffs {
		effects = append(effects, &protocolpb.HeroAttrEffect{
			PlayerId: player.ID,
			Data: &protocolpb.HeroAttrEffect_Buff{Buff: &protocolpb.HeroBuffChangeS2C{
				PlayerId: player.ID, Buff: buffMessage(buff), Op: protocolpb.HeroBuffChangeS2C_Delete,
			}},
		})
	}
	s.log.Info("round-start effects resolved", "room_id", roomID, "player_id", player.ID, "round", round, "hp_change", result.NewHP-result.OldHP, "buffs_updated", len(result.UpdatedBuffs), "buffs_removed", len(result.RemovedBuffs))
	return []Push{s.pushFor("UpdateHeroAttrS2C", &protocolpb.UpdateHeroAttrS2C{
		PlayerId: player.ID, Cause: &protocolpb.CauseOrigin{}, EffectDatas: effects,
	}, recipients, 0)}, nil
}
