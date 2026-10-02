package gateway

import (
	"errors"

	store "astralparty-server/internal/db"
	modelpb "astralparty-server/internal/protocol/gen/model"
	protocolpb "astralparty-server/internal/protocol/gen/protocol"
)

const hero11011HealingDamageSkillID int32 = 11011

// applyHeroHPChangePassive applies character 110's configured healing and
// damage modifiers to an HP delta before it is committed by the store.
func (s *Server) applyHeroHPChangePassive(room store.Room, playerID int64, change int32) int32 {
	if change == 0 {
		return change
	}
	player, found := findRoomPlayer(room, playerID)
	if !found {
		return change
	}
	params := s.resources.HeroPassiveSkillParams(player.HeroID, hero11011HealingDamageSkillID, usesPVEPassiveSkills(room.Mode))
	if len(params) < 2 {
		return change
	}
	if change > 0 && params[0] > 0 {
		return change + params[0]
	}
	if change < 0 && params[1] > 0 {
		return change - params[1]
	}
	return change
}

// applyHeroHPChangePassives adjusts additive HP outcomes. SetHP outcomes such
// as event 30020 are direct assignments and are not healing or damage ticks.
func (s *Server) applyHeroHPChangePassives(room store.Room, deltas []store.EventAttrDelta) []store.EventAttrDelta {
	for i := range deltas {
		if deltas[i].SetHP {
			continue
		}
		deltas[i].HPChange = s.applyHeroHPChangePassive(room, deltas[i].PlayerID, deltas[i].HPChange)
	}
	return deltas
}

func (s *Server) movementPassiveCardReward(heroID, mode, points int32) (int32, []store.CardState, error) {
	skillID := int32(12311)
	if usesPVEPassiveSkills(mode) {
		skillID = 12312
	}
	params := s.resources.HeroPassiveSkillParams(heroID, skillID, usesPVEPassiveSkills(mode))
	if len(params) == 0 {
		return 0, nil, nil
	}
	if len(params) < 4 {
		return 0, nil, errors.New("movement card passive resource is incomplete")
	}
	var cardID int32
	switch points {
	case params[0]:
		cardID = params[1]
	case params[2]:
		cardID = params[3]
	default:
		return 0, nil, nil
	}
	if cardID <= 0 {
		return 0, nil, errors.New("movement card passive resource contains an invalid card ID")
	}
	uniqueID, err := newCardUniqueID()
	if err != nil {
		return 0, nil, err
	}
	return skillID, []store.CardState{{UniqueID: uniqueID, CardID: cardID, BattleCost: -1}}, nil
}

func movementSkillCardRewardAttrUpdate(playerID int64, skillID int32, cards []store.CardState) *protocolpb.UpdateHeroAttrS2C {
	cardMessages := make([]*modelpb.CardInfo, 0, len(cards))
	for _, card := range cards {
		cardMessages = append(cardMessages, &modelpb.CardInfo{
			UniqueId: card.UniqueID, CardId: card.CardID, PurifyNum: card.PurifyNum,
			IsTemp: card.IsTemp, BattleCost: card.BattleCost,
		})
	}
	return &protocolpb.UpdateHeroAttrS2C{
		PlayerId: playerID,
		Cause:    &protocolpb.CauseOrigin{S: protocolpb.CauseOrigin_skill, Id: int64(skillID)},
		EffectDatas: []*protocolpb.HeroAttrEffect{{
			PlayerId: playerID,
			Data: &protocolpb.HeroAttrEffect_Card{Card: &protocolpb.HeroCardChangeS2C{
				PlayerId: playerID, Cards: cardMessages,
			}},
		}},
	}
}
