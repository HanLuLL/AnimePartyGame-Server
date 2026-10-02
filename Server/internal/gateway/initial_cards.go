package gateway

import (
	"crypto/rand"
	"fmt"
	"math/big"

	store "astralparty-server/internal/db"
)

func (s *Server) initialGameState(room store.Room) (map[int64][]store.CardState, map[int64]int32, error) {
	effectPoolID, battlePoolID, poolsConfigured := s.domain.CardPoolsForMapMode(int64(room.MapID), room.Mode)
	if !poolsConfigured {
		if room.Mode != 10 || (room.MapID != 1001 && room.MapID != 1002) {
			return nil, nil, fmt.Errorf("card pools not configured for map %d mode %d", room.MapID, room.Mode)
		}
	}
	var effectIDs, battleIDs []int32
	if poolsConfigured {
		var ok bool
		effectIDs, ok = s.domain.EffectCardIDs(effectPoolID)
		if !ok {
			return nil, nil, fmt.Errorf("effect card pool %d is empty", effectPoolID)
		}
		battleIDs, ok = s.domain.BattleCardIDs(battlePoolID)
		if !ok {
			return nil, nil, fmt.Errorf("battle card pool %d is empty", battlePoolID)
		}
	}
	globalGold, ok := s.resources.GlobalInt("GAME_INITIAL_GOLD")
	if !ok || globalGold < 0 {
		return nil, nil, fmt.Errorf("initial gold is missing or invalid")
	}
	effectCount, ok := s.resources.GlobalInt("GAME_INITIAL_EFFECTCARD_COUNT")
	if !ok {
		return nil, nil, fmt.Errorf("initial effect card count is missing")
	}
	battleCount, ok := s.resources.GlobalInt("GAME_INITIAL_COMBATCARD_COUNT")
	if !ok {
		return nil, nil, fmt.Errorf("initial combat card count is missing")
	}
	if g := s.gameplay.Get(); g != nil {
		if g.InitialGold > 0 {
			globalGold = int32(g.InitialGold)
		}
		if g.InitialEffectCards > 0 {
			effectCount = int32(g.InitialEffectCards)
		}
		if g.InitialCombatCards > 0 {
			battleCount = int32(g.InitialCombatCards)
		}
	}
	if effectCount < 0 || battleCount < 0 || int64(effectCount)+int64(battleCount) > 64 {
		return nil, nil, fmt.Errorf("invalid initial card counts: effect=%d combat=%d", effectCount, battleCount)
	}

	initial := make(map[int64][]store.CardState, len(room.Players))
	initialGold := make(map[int64]int32, len(room.Players))
	if !poolsConfigured {
		s.log.Warn("campaign novice map has no configured initial card pools; starting with an empty hand", "room_id", room.ID, "map_id", room.MapID, "mode", room.Mode)
	}
	for _, player := range room.Players {
		gold := globalGold
		if configuredGold, found := s.resources.CampaignInitialGold(int64(room.MapID), room.Mode, player.Slot); found {
			gold = configuredGold
		}
		initialGold[player.ID] = gold

		if campaignIDs, found := s.resources.CampaignInitialCardIDs(int64(room.MapID), room.Mode, player.HeroID, player.Slot); found {
			cards, err := makeCardStates(campaignIDs, player.ID)
			if err != nil {
				return nil, nil, err
			}
			initial[player.ID] = cards
			continue
		}
		if !poolsConfigured {
			initial[player.ID] = []store.CardState{}
			continue
		}
		cards := make([]store.CardState, 0, effectCount+battleCount)
		for i := int32(0); i < effectCount; i++ {
			cardID, err := randomCardID(effectIDs)
			if err != nil {
				return nil, nil, fmt.Errorf("draw initial effect card for player %d: %w", player.ID, err)
			}
			uniqueID, err := newCardUniqueID()
			if err != nil {
				return nil, nil, fmt.Errorf("create initial card ID for player %d: %w", player.ID, err)
			}
			cards = append(cards, store.CardState{UniqueID: uniqueID, CardID: cardID, BattleCost: -1})
		}
		for i := int32(0); i < battleCount; i++ {
			cardID, err := randomCardID(battleIDs)
			if err != nil {
				return nil, nil, fmt.Errorf("draw initial combat card for player %d: %w", player.ID, err)
			}
			uniqueID, err := newCardUniqueID()
			if err != nil {
				return nil, nil, fmt.Errorf("create initial card ID for player %d: %w", player.ID, err)
			}
			cards = append(cards, store.CardState{UniqueID: uniqueID, CardID: cardID, BattleCost: -1})
		}
		initial[player.ID] = cards
	}
	return initial, initialGold, nil
}

func makeCardStates(cardIDs []int32, playerID int64) ([]store.CardState, error) {
	cards := make([]store.CardState, 0, len(cardIDs))
	for _, cardID := range cardIDs {
		if cardID <= 0 {
			return nil, fmt.Errorf("invalid campaign card %d for player %d", cardID, playerID)
		}
		uniqueID, err := newCardUniqueID()
		if err != nil {
			return nil, fmt.Errorf("create initial card ID for player %d: %w", playerID, err)
		}
		cards = append(cards, store.CardState{UniqueID: uniqueID, CardID: cardID, BattleCost: -1})
	}
	return cards, nil
}

func randomCardID(pool []int32) (int32, error) {
	if len(pool) == 0 {
		return 0, fmt.Errorf("cannot draw from an empty card pool")
	}
	index, err := rand.Int(rand.Reader, big.NewInt(int64(len(pool))))
	if err != nil {
		return 0, err
	}
	return pool[index.Int64()], nil
}
