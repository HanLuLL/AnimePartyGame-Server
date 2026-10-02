package gateway

import (
	"crypto/rand"
	"fmt"
	"math/big"

	store "astralparty-server/internal/db"
	modelpb "astralparty-server/internal/protocol/gen/model"
	protocolpb "astralparty-server/internal/protocol/gen/protocol"
)

type weightedCard struct {
	cardID int32
	weight int32
}

var noviceTutorialCardPool = []weightedCard{
	{10001, 2000}, {10003, 3000}, {10005, 5000}, {10004, 2000}, {10006, 3000},
	{21002, 2000}, {20002, 1000}, {20008, 1000}, {21001, 5000}, {21006, 1000},
}

func (s *Server) drawCardLandReward(room store.Room, player store.Player, round int32, landID int32) ([]store.CardState, int32, error) {
	params, ok := s.domain.LandParams(14)
	if !ok || len(params) == 0 || params[0] < 0 {
		return nil, 0, fmt.Errorf("draw-card land count is missing or invalid")
	}
	handLimit, ok := s.domain.CardInHandLimit(room.Mode)
	if !ok {
		handLimit, ok = s.resources.GlobalInt("GAME_CARDINHAND_LIMIT")
	}
	if !ok || handLimit <= 0 {
		return nil, 0, fmt.Errorf("card hand limit is missing or invalid")
	}
	tutorialNovice := room.Mode == 10 && (room.MapID == 1001 || room.MapID == 1002)
	if tutorialNovice {
		// TutorialLandDrawCard appends its fixed-size draw without checking
		// GameMode_infos.cardInHandLimit.
		handLimit = int32(1<<31 - 1)
	}
	count := int(params[0])
	if count <= 0 {
		return nil, handLimit, nil
	}
	var cardIDs []int32
	if tutorialNovice {
		if room.MapID == 1001 && round == 2 {
			// TutorialLandDrawCard overrides its normal random pool on the
			// second round of Tutorial1001.
			cardIDs = []int32{21004, 20014}
		} else {
			cardIDs = make([]int32, 0, count)
			for i := 0; i < count; i++ {
				cardID, err := randomWeightedCardID(noviceTutorialCardPool)
				if err != nil {
					return nil, 0, err
				}
				cardIDs = append(cardIDs, cardID)
			}
		}
	} else {
		effectPoolID, _, ok := s.domain.CardPoolsForMapMode(int64(room.MapID), room.Mode)
		if !ok {
			return nil, 0, fmt.Errorf("draw-card pool is not configured for map %d mode %d", room.MapID, room.Mode)
		}
		cardIDs, ok = s.domain.EffectCardIDs(effectPoolID)
		if !ok {
			return nil, 0, fmt.Errorf("draw-card effect pool %d is empty", effectPoolID)
		}
	}
	if !tutorialNovice {
		selected := make([]int32, 0, count)
		for i := 0; i < count; i++ {
			cardID, err := randomCardID(cardIDs)
			if err != nil {
				return nil, 0, fmt.Errorf("draw card for player %d: %w", player.ID, err)
			}
			selected = append(selected, cardID)
		}
		cardIDs = selected
	}
	cards := make([]store.CardState, 0, len(cardIDs))
	for _, cardID := range cardIDs {
		uniqueID, err := newCardUniqueID()
		if err != nil {
			return nil, 0, fmt.Errorf("create drawn card ID for player %d: %w", player.ID, err)
		}
		cards = append(cards, store.CardState{UniqueID: uniqueID, CardID: cardID, BattleCost: -1})
	}
	return cards, handLimit, nil
}

func (s *Server) drawPVEShopVisitReward(room store.Room, player store.Player) ([]store.CardState, int32, error) {
	handLimit, ok := s.domain.CardInHandLimit(room.Mode)
	if !ok {
		handLimit, ok = s.resources.GlobalInt("GAME_CARDINHAND_LIMIT")
	}
	if !ok || handLimit <= 0 {
		return nil, 0, fmt.Errorf("PVE shop card hand limit is missing or invalid")
	}
	tutorialNovice := room.Mode == 10 && (room.MapID == 1001 || room.MapID == 1002)
	if tutorialNovice {
		// TutorialLandPveshop.Stay grants one card without checking the hand limit.
		handLimit = int32(1<<31 - 1)
	} else if len(player.Cards) >= int(handLimit) {
		return nil, handLimit, nil
	}
	var cardID int32
	if tutorialNovice {
		var err error
		cardID, err = randomWeightedCardID(noviceTutorialCardPool)
		if err != nil {
			return nil, 0, err
		}
	} else {
		effectPoolID, _, configured := s.domain.CardPoolsForMapMode(int64(room.MapID), room.Mode)
		if !configured {
			return nil, 0, fmt.Errorf("PVE shop reward pool is not configured for map %d mode %d", room.MapID, room.Mode)
		}
		pool, found := s.domain.EffectCardIDs(effectPoolID)
		if !found || len(pool) == 0 {
			return nil, 0, fmt.Errorf("PVE shop reward effect-card pool %d is empty", effectPoolID)
		}
		var err error
		cardID, err = randomCardID(pool)
		if err != nil {
			return nil, 0, fmt.Errorf("draw PVE shop visit card for player %d: %w", player.ID, err)
		}
	}
	uniqueID, err := newCardUniqueID()
	if err != nil {
		return nil, 0, fmt.Errorf("create PVE shop visit card ID for player %d: %w", player.ID, err)
	}
	return []store.CardState{{UniqueID: uniqueID, CardID: cardID, BattleCost: -1}}, handLimit, nil
}

func randomWeightedCardID(pool []weightedCard) (int32, error) {
	var total int64
	for _, card := range pool {
		if card.cardID <= 0 || card.weight <= 0 {
			return 0, fmt.Errorf("tutorial card pool contains an invalid entry")
		}
		total += int64(card.weight)
	}
	if total <= 0 {
		return 0, fmt.Errorf("tutorial card pool is empty")
	}
	roll, err := rand.Int(rand.Reader, big.NewInt(total))
	if err != nil {
		return 0, fmt.Errorf("draw from tutorial card pool: %w", err)
	}
	threshold := roll.Int64()
	var cumulative int64
	for _, card := range pool {
		cumulative += int64(card.weight)
		if threshold < cumulative {
			return card.cardID, nil
		}
	}
	return 0, fmt.Errorf("tutorial card pool selection exceeded its total weight")
}

func landCardRewardAttrUpdate(playerID int64, landID int32, cards []store.CardState, shopOpen bool) *protocolpb.UpdateHeroAttrS2C {
	cardMessages := make([]*modelpb.CardInfo, 0, len(cards))
	for _, card := range cards {
		cardMessages = append(cardMessages, &modelpb.CardInfo{
			UniqueId: card.UniqueID, CardId: card.CardID, PurifyNum: card.PurifyNum,
			IsTemp: card.IsTemp, BattleCost: card.BattleCost,
		})
	}
	source := protocolpb.CauseOrigin_land
	if shopOpen {
		// Type 22's tutorial implementation attributes the entry card to ShopOpen.
		source = protocolpb.CauseOrigin_shop_open
	}
	return &protocolpb.UpdateHeroAttrS2C{
		PlayerId: playerID,
		Cause:    &protocolpb.CauseOrigin{S: source, Id: int64(landID)},
		EffectDatas: []*protocolpb.HeroAttrEffect{{
			PlayerId: playerID,
			Data: &protocolpb.HeroAttrEffect_Card{Card: &protocolpb.HeroCardChangeS2C{
				PlayerId: playerID, Cards: cardMessages,
			}},
		}},
	}
}
