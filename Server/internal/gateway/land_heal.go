package gateway

import (
	"context"
	"errors"

	store "astralparty-server/internal/db"
	protocolpb "astralparty-server/internal/protocol/gen/protocol"
)

var errLandHealConfigMissing = errors.New("Heal land parameters are missing")

type landHealResult struct {
	Amount     int32
	OldHP      int32
	NewHP      int32
	MaxHP      int32
	NextPlayer int64
	Round      int32
}

func (s *Server) resolveLandHeal(ctx context.Context, roomID, playerID int64) (landHealResult, error) {
	params, ok := s.domain.LandParams(20)
	if !ok {
		return landHealResult{}, errLandHealConfigMissing
	}
	amount := params[0]
	if amount < 0 {
		return landHealResult{}, errors.New("Heal resource contains a negative amount")
	}
	player, err := s.store.LookupPlayer(ctx, playerID)
	if err != nil {
		return landHealResult{}, err
	}
	room, err := s.store.RoomSnapshot(ctx, roomID)
	if err != nil {
		return landHealResult{}, err
	}
	amount = s.applyHeroHPChangePassive(room, playerID, amount)
	maxHP := s.domain.HeroMaxHP(player.HeroID)
	oldHP, newHP, nextPlayer, round, created, err := s.store.CompleteLandHeal(ctx, roomID, playerID, amount, maxHP)
	if err != nil {
		return landHealResult{}, err
	}
	if !created {
		return landHealResult{}, store.ErrActionNotReady
	}
	s.log.Info("Heal land resolved", "room_id", roomID, "player_id", playerID, "hp_change", amount, "old_hp", oldHP, "hp", newHP)
	return landHealResult{
		Amount:     amount,
		OldHP:      oldHP,
		NewHP:      newHP,
		MaxHP:      maxHP,
		NextPlayer: nextPlayer,
		Round:      round,
	}, nil
}

func landHealAttrUpdate(playerID int64, landID int32, result landHealResult) *protocolpb.UpdateHeroAttrS2C {
	return &protocolpb.UpdateHeroAttrS2C{
		PlayerId: playerID,
		Cause:    &protocolpb.CauseOrigin{S: protocolpb.CauseOrigin_land, Id: int64(landID)},
		EffectDatas: []*protocolpb.HeroAttrEffect{{
			PlayerId: playerID,
			Data: &protocolpb.HeroAttrEffect_Hp{Hp: &protocolpb.HeroHpChangeS2C{
				PlayerId:     playerID,
				ChangeHp:     result.Amount,
				OriHp:        result.OldHP,
				CurrHp:       result.NewHP,
				RealChangeHp: result.NewHP - result.OldHP,
				MaxHp:        result.MaxHP,
			}},
		}},
	}
}

func (s *Server) resolveBotLandHeal(ctx context.Context, roomID, playerID int64, landID int32, ids []int64) (int64, int32, []Push, error) {
	result, err := s.resolveLandHeal(ctx, roomID, playerID)
	if err != nil {
		return 0, 0, nil, err
	}
	pushes := []Push{s.pushFor("UpdateHeroAttrS2C", landHealAttrUpdate(playerID, landID, result), ids, 0)}
	return result.NextPlayer, result.Round, pushes, nil
}
