package gateway

import (
	"context"
	"errors"

	store "astralparty-server/internal/db"
	protocolpb "astralparty-server/internal/protocol/gen/protocol"
)

var errLandBloodLossConfigMissing = errors.New("BloodLoss land parameters are missing")

type landBloodLossResult struct {
	Amount       int32
	OldHP        int32
	NewHP        int32
	MaxHP        int32
	RemovedBuffs []store.BuffState
	NextPlayer   int64
	Round        int32
}

func (s *Server) resolveLandBloodLoss(ctx context.Context, roomID, playerID int64) (landBloodLossResult, error) {
	params, ok := s.domain.LandParams(17)
	if !ok || len(params) == 0 {
		return landBloodLossResult{}, errLandBloodLossConfigMissing
	}
	amount := params[0]
	if amount > 0 {
		return landBloodLossResult{}, errors.New("BloodLoss resource contains a positive amount")
	}
	player, err := s.store.LookupPlayer(ctx, playerID)
	if err != nil {
		return landBloodLossResult{}, err
	}
	room, err := s.store.RoomSnapshot(ctx, roomID)
	if err != nil {
		return landBloodLossResult{}, err
	}
	amount = s.applyHeroHPChangePassive(room, playerID, amount)
	maxHP := s.domain.HeroMaxHP(player.HeroID)
	oldHP, newHP, nextPlayer, round, created, err := s.store.CompleteLandBloodLoss(ctx, roomID, playerID, amount, maxHP)
	if err != nil {
		return landBloodLossResult{}, err
	}
	if !created {
		return landBloodLossResult{}, store.ErrActionNotReady
	}
	var removedBuffs []store.BuffState
	if amount < 0 && oldHP > 0 {
		_, _, removedBuffs = store.ApplyDestinyDamageBuffs(-amount, player.Buffs)
	}
	actualChange := newHP - oldHP
	s.log.Info("BloodLoss land resolved", "room_id", roomID, "player_id", playerID, "hp_change", actualChange, "old_hp", oldHP, "hp", newHP, "buffs_removed", len(removedBuffs))
	return landBloodLossResult{
		Amount: actualChange, OldHP: oldHP, NewHP: newHP, MaxHP: maxHP, RemovedBuffs: removedBuffs, NextPlayer: nextPlayer, Round: round,
	}, nil
}

func landBloodLossAttrUpdate(playerID int64, landID int32, result landBloodLossResult) *protocolpb.UpdateHeroAttrS2C {
	effects := []*protocolpb.HeroAttrEffect{{
		PlayerId: playerID,
		Data: &protocolpb.HeroAttrEffect_Hp{Hp: &protocolpb.HeroHpChangeS2C{
			PlayerId: playerID, ChangeHp: result.Amount, OriHp: result.OldHP,
			CurrHp: result.NewHP, RealChangeHp: result.NewHP - result.OldHP, MaxHp: result.MaxHP,
		}},
	}}
	for _, buff := range result.RemovedBuffs {
		effects = append(effects, &protocolpb.HeroAttrEffect{
			PlayerId: playerID,
			Data: &protocolpb.HeroAttrEffect_Buff{Buff: &protocolpb.HeroBuffChangeS2C{
				PlayerId: playerID, Buff: buffMessage(buff), Op: protocolpb.HeroBuffChangeS2C_Delete,
			}},
		})
	}
	return &protocolpb.UpdateHeroAttrS2C{
		PlayerId:    playerID,
		Cause:       &protocolpb.CauseOrigin{S: protocolpb.CauseOrigin_land, Id: int64(landID)},
		EffectDatas: effects,
	}
}

func (s *Server) resolveBotLandBloodLoss(ctx context.Context, roomID, playerID int64, landID int32, ids []int64) (int64, int32, []Push, error) {
	result, err := s.resolveLandBloodLoss(ctx, roomID, playerID)
	if err != nil {
		return 0, 0, nil, err
	}
	pushes := []Push{s.pushFor("UpdateHeroAttrS2C", landBloodLossAttrUpdate(playerID, landID, result), ids, 0)}
	return result.NextPlayer, result.Round, pushes, nil
}
