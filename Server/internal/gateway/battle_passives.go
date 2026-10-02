package gateway

import (
	"errors"

	store "astralparty-server/internal/db"
	modelpb "astralparty-server/internal/protocol/gen/model"
	protocolpb "astralparty-server/internal/protocol/gen/protocol"
)

const (
	hero11511SkillID int32 = 11511
	hero11511BuffID  int32 = 1150101
)

type battleBuffChange struct {
	playerID int64
	buff     store.BuffState
	op       protocolpb.HeroBuffChangeS2C_Oper
}

func (s *Server) gainHero115BattleStack(player store.Player, mode int32, buffs []store.BuffState) ([]store.BuffState, *battleBuffChange, error) {
	params := s.resources.HeroPassiveSkillParams(player.HeroID, hero11511SkillID, usesPVEPassiveSkills(mode))
	if len(params) == 0 {
		return buffs, nil, nil
	}
	if len(params) < 2 || params[0] <= 0 || params[1] <= 0 {
		return nil, nil, errors.New("character 115 battle passive resource is incomplete")
	}
	nextBuffs := append([]store.BuffState(nil), buffs...)
	for i := range nextBuffs {
		if nextBuffs[i].BuffID != hero11511BuffID {
			continue
		}
		if nextBuffs[i].Progress >= params[0] {
			return nextBuffs, nil, nil
		}
		nextBuffs[i].Progress += params[1]
		if nextBuffs[i].Progress > params[0] {
			nextBuffs[i].Progress = params[0]
		}
		changed := nextBuffs[i]
		return nextBuffs, &battleBuffChange{playerID: player.ID, buff: changed, op: protocolpb.HeroBuffChangeS2C_Update}, nil
	}
	uniqueID, err := newBuffUniqueID()
	if err != nil {
		return nil, nil, err
	}
	progress := params[1]
	if progress > params[0] {
		progress = params[0]
	}
	created := store.BuffState{
		UniqueID: uniqueID, BuffID: hero11511BuffID, Progress: progress,
		Source: &store.BuffSourceState{S: int32(modelpb.BuffSource_skill), ID: hero11511SkillID},
	}
	nextBuffs = append(nextBuffs, created)
	return nextBuffs, &battleBuffChange{playerID: player.ID, buff: created, op: protocolpb.HeroBuffChangeS2C_Insert}, nil
}

func hero115BattleAttackBonus(buffs []store.BuffState) int32 {
	for _, buff := range buffs {
		if buff.BuffID != hero11511BuffID || buff.Progress <= 0 {
			continue
		}
		bonus := buff.Progress
		if buff.Progress >= 3 {
			bonus += 2
		}
		return bonus
	}
	return 0
}

func removeAllBattleBuffs(buffs []store.BuffState, buffID int32) ([]store.BuffState, []store.BuffState) {
	remaining := make([]store.BuffState, 0, len(buffs))
	removed := make([]store.BuffState, 0, 1)
	for _, buff := range buffs {
		if buff.BuffID == buffID {
			removed = append(removed, buff)
			continue
		}
		remaining = append(remaining, buff)
	}
	return remaining, removed
}

func (s *Server) battleBuffChangePush(playerID, battleID int64, buff store.BuffState, op protocolpb.HeroBuffChangeS2C_Oper, recipients []int64) Push {
	return s.pushFor("UpdateHeroAttrS2C", &protocolpb.UpdateHeroAttrS2C{
		PlayerId: playerID,
		Cause:    &protocolpb.CauseOrigin{S: protocolpb.CauseOrigin_battle, Id: battleID},
		EffectDatas: []*protocolpb.HeroAttrEffect{{PlayerId: playerID, Data: &protocolpb.HeroAttrEffect_Buff{Buff: &protocolpb.HeroBuffChangeS2C{
			PlayerId: playerID, Buff: buffMessage(buff), Op: op,
		}}}},
	}, recipients, 0)
}
