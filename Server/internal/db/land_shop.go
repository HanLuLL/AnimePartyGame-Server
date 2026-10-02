package db

import (
	"context"
	"database/sql"
	"encoding/json"
	"errors"
	"fmt"
	"time"
)

var (
	ErrShopNotActive        = errors.New("land shop is not active for player")
	ErrShopInsufficientGold = errors.New("not enough battle gold for shop purchase")
	ErrShopHandFull         = errors.New("battle hand limit would be exceeded by shop purchase")
	ErrShopInvalidChoice    = errors.New("land shop selection is invalid")
)

// ActiveShop returns the offer currently owned by the active shop turn.
func (s *Store) ActiveShop(ctx context.Context, roomID, playerID int64) (ShopState, error) {
	var storedPlayerID int64
	var stateJSON string
	err := s.db.QueryRowContext(ctx, `SELECT player_id,state_json FROM game_shops WHERE room_id=?`, roomID).Scan(&storedPlayerID, &stateJSON)
	if errors.Is(err, sql.ErrNoRows) || (err == nil && storedPlayerID != playerID) {
		return ShopState{}, ErrShopNotActive
	}
	if err != nil {
		return ShopState{}, err
	}
	var state ShopState
	if err := json.Unmarshal([]byte(stateJSON), &state); err != nil {
		return ShopState{}, fmt.Errorf("decode land shop for room %d: %w", roomID, err)
	}
	if state.PlayerID != playerID || len(state.Cards) == 0 || len(state.Cards) != len(state.Alreadys) {
		return ShopState{}, ErrShopNotActive
	}
	return state, nil
}

// CompleteLandShopBuy purchases selected offer indexes, or closes the shop
// when indexes is empty. Gold, cards, sold flags, action de-duplication and
// turn advancement are committed together.
func (s *Store) CompleteLandShopBuy(ctx context.Context, roomID, playerID int64, cmd uint16, upsn int64, payload []byte, indexes []int32, handLimit int32, uniqueIDs []int32) (result ShopBuyResult, created bool, err error) {
	return s.completeShopBuy(ctx, roomID, playerID, cmd, upsn, payload, indexes, handLimit, uniqueIDs, false, false)
}

// CompletePVEShopBuy commits a PVE shop purchase or an explicit close. The
// offer, sale flags, balance and hand update share one transaction.
func (s *Store) CompletePVEShopBuy(ctx context.Context, roomID, playerID int64, cmd uint16, upsn int64, payload []byte, indexes []int32, handLimit int32, uniqueIDs []int32, closeShop bool) (result ShopBuyResult, created bool, err error) {
	return s.completeShopBuy(ctx, roomID, playerID, cmd, upsn, payload, indexes, handLimit, uniqueIDs, true, closeShop)
}

func (s *Store) completeShopBuy(ctx context.Context, roomID, playerID int64, cmd uint16, upsn int64, payload []byte, indexes []int32, handLimit int32, uniqueIDs []int32, pve, closeShop bool) (result ShopBuyResult, created bool, err error) {
	tx, err := s.db.BeginTx(ctx, nil)
	if err != nil {
		return ShopBuyResult{}, false, err
	}
	defer tx.Rollback()
	if upsn > 0 {
		var existing int
		if err = tx.QueryRowContext(ctx, `SELECT COUNT(*) FROM game_actions WHERE room_id=? AND player_id=? AND upsn=?`, roomID, playerID, upsn).Scan(&existing); err != nil {
			return ShopBuyResult{}, false, err
		}
		if existing > 0 {
			return ShopBuyResult{}, false, nil
		}
	}
	var currentPlayer int64
	var pending int32
	var phase string
	if err = tx.QueryRowContext(ctx, `SELECT current_player_id,round,pending_move,turn_phase FROM game_sessions WHERE room_id=? AND status='running'`, roomID).Scan(&currentPlayer, &result.Round, &pending, &phase); err != nil {
		return ShopBuyResult{}, false, err
	}
	expectedPhase := TurnPhaseShop
	if pve {
		expectedPhase = TurnPhasePVEShop
	}
	if currentPlayer != playerID || pending != 0 || phase != expectedPhase {
		return ShopBuyResult{}, false, ErrActionNotReady
	}
	var shopPlayerID int64
	var stateJSON string
	if err = tx.QueryRowContext(ctx, `SELECT player_id,state_json FROM game_shops WHERE room_id=?`, roomID).Scan(&shopPlayerID, &stateJSON); err != nil {
		if errors.Is(err, sql.ErrNoRows) {
			return ShopBuyResult{}, false, ErrShopNotActive
		}
		return ShopBuyResult{}, false, err
	}
	if shopPlayerID != playerID {
		return ShopBuyResult{}, false, ErrShopNotActive
	}
	if err = json.Unmarshal([]byte(stateJSON), &result.State); err != nil {
		return ShopBuyResult{}, false, fmt.Errorf("decode land shop state: %w", err)
	}
	state := &result.State
	if state.PlayerID != playerID || state.PVE != pve || len(state.Cards) == 0 || len(state.Cards) != len(state.Alreadys) || state.Gold < 0 || state.DiscountGold < 0 || (pve && len(state.TalentSkillFreeCard) != len(state.Cards)) {
		return ShopBuyResult{}, false, ErrShopNotActive
	}
	if pve && (closeShop == (len(indexes) > 0)) {
		return ShopBuyResult{}, false, ErrShopInvalidChoice
	}
	var oldGold int32
	var cardsJSON string
	if err = tx.QueryRowContext(ctx, `SELECT gold,battle_cards_json FROM players WHERE id=? AND room_id=?`, playerID, roomID).Scan(&oldGold, &cardsJSON); err != nil {
		return ShopBuyResult{}, false, err
	}
	var cards []CardState
	if cardsJSON != "" {
		if err = json.Unmarshal([]byte(cardsJSON), &cards); err != nil {
			return ShopBuyResult{}, false, fmt.Errorf("decode battle cards for shop player %d: %w", playerID, err)
		}
	}
	if cards == nil {
		cards = []CardState{}
	}
	result.OldGold = oldGold
	result.NewGold = oldGold
	if len(indexes) == 0 {
		if len(uniqueIDs) != 0 {
			return ShopBuyResult{}, false, ErrShopInvalidChoice
		}
		members, e := tx.QueryContext(ctx, `SELECT player_id FROM room_members WHERE room_id=? ORDER BY slot`, roomID)
		if e != nil {
			return ShopBuyResult{}, false, e
		}
		var memberIDs []int64
		for members.Next() {
			var id int64
			if e = members.Scan(&id); e != nil {
				members.Close()
				return ShopBuyResult{}, false, e
			}
			memberIDs = append(memberIDs, id)
		}
		if e = members.Err(); e != nil {
			members.Close()
			return ShopBuyResult{}, false, e
		}
		members.Close()
		index := -1
		for i, id := range memberIDs {
			if id == playerID {
				index = i
				break
			}
		}
		if index < 0 || len(memberIDs) == 0 {
			return ShopBuyResult{}, false, errors.New("shop player is not in room")
		}
		wrap := index == len(memberIDs)-1
		result.NextPlayer = memberIDs[(index+1)%len(memberIDs)]
		if wrap {
			result.Round++
		}
		if upsn > 0 {
			if _, err = tx.ExecContext(ctx, `INSERT OR IGNORE INTO game_actions(room_id,player_id,cmd_id,upsn,payload,created_at) VALUES(?,?,?,?,?,?)`, roomID, playerID, cmd, upsn, payload, time.Now().Unix()); err != nil {
				return ShopBuyResult{}, false, err
			}
		}
		res, e := tx.ExecContext(ctx, `UPDATE game_sessions SET current_player_id=?,round=?,pending_move=0,turn_phase=? WHERE room_id=? AND status='running' AND current_player_id=? AND turn_phase=? AND pending_move=0`, result.NextPlayer, result.Round, TurnPhaseThrowDice, roomID, playerID, expectedPhase)
		if e != nil {
			return ShopBuyResult{}, false, e
		}
		updated, e := res.RowsAffected()
		if e != nil {
			return ShopBuyResult{}, false, e
		}
		if updated == 0 {
			return ShopBuyResult{}, false, ErrActionNotReady
		}
		if _, err = tx.ExecContext(ctx, `DELETE FROM game_shops WHERE room_id=? AND player_id=?`, roomID, playerID); err != nil {
			return ShopBuyResult{}, false, err
		}
		result.Closed = true
		if err = tx.Commit(); err != nil {
			return ShopBuyResult{}, false, err
		}
		return result, true, nil
	}
	if len(indexes) != len(uniqueIDs) || handLimit <= 0 || int64(len(cards))+int64(len(indexes)) > int64(handLimit) {
		if len(indexes) == len(uniqueIDs) && handLimit > 0 {
			return ShopBuyResult{}, false, ErrShopHandFull
		}
		return ShopBuyResult{}, false, ErrShopInvalidChoice
	}
	seenIndexes := make(map[int32]struct{}, len(indexes))
	seenIDs := make(map[int32]struct{}, len(uniqueIDs))
	for _, card := range cards {
		seenIDs[card.UniqueID] = struct{}{}
	}
	var totalCost int64
	newCards := make([]CardState, 0, len(indexes))
	for i, index := range indexes {
		if index < 0 || int(index) >= len(state.Cards) || state.Alreadys[index] {
			return ShopBuyResult{}, false, ErrShopInvalidChoice
		}
		if _, exists := seenIndexes[index]; exists {
			return ShopBuyResult{}, false, ErrShopInvalidChoice
		}
		seenIndexes[index] = struct{}{}
		uniqueID := uniqueIDs[i]
		if uniqueID <= 0 {
			return ShopBuyResult{}, false, ErrShopInvalidChoice
		}
		if _, exists := seenIDs[uniqueID]; exists {
			return ShopBuyResult{}, false, ErrShopInvalidChoice
		}
		seenIDs[uniqueID] = struct{}{}
		price := state.Gold
		if pve {
			if state.TalentSkillFreeCard[index] {
				price = state.FreeCard
			}
			price -= state.DiscountGold
			if price < 0 {
				price = 0
			}
		}
		totalCost += int64(price)
		if pve {
			result.Bought = append(result.Bought, state.Cards[index])
		} else {
			result.Bought = append(result.Bought, index)
		}
		newCards = append(newCards, CardState{UniqueID: uniqueID, CardID: state.Cards[index], BattleCost: -1})
	}
	if totalCost > int64(oldGold) {
		return ShopBuyResult{}, false, ErrShopInsufficientGold
	}
	for _, index := range indexes {
		state.Alreadys[index] = true
	}
	result.NewGold = oldGold - int32(totalCost)
	allCards := append(append([]CardState(nil), cards...), newCards...)
	result.Cards = allCards
	encodedCards, err := json.Marshal(allCards)
	if err != nil {
		return ShopBuyResult{}, false, err
	}
	encodedState, err := json.Marshal(state)
	if err != nil {
		return ShopBuyResult{}, false, err
	}
	if upsn > 0 {
		res, e := tx.ExecContext(ctx, `INSERT OR IGNORE INTO game_actions(room_id,player_id,cmd_id,upsn,payload,created_at) VALUES(?,?,?,?,?,?)`, roomID, playerID, cmd, upsn, payload, time.Now().Unix())
		if e != nil {
			return ShopBuyResult{}, false, e
		}
		inserted, e := res.RowsAffected()
		if e != nil {
			return ShopBuyResult{}, false, e
		}
		if inserted == 0 {
			return ShopBuyResult{}, false, nil
		}
	}
	if _, err = tx.ExecContext(ctx, `UPDATE players SET gold=?,battle_cards_json=? WHERE id=? AND room_id=?`, result.NewGold, string(encodedCards), playerID, roomID); err != nil {
		return ShopBuyResult{}, false, err
	}
	if _, err = tx.ExecContext(ctx, `UPDATE game_shops SET state_json=? WHERE room_id=? AND player_id=?`, string(encodedState), roomID, playerID); err != nil {
		return ShopBuyResult{}, false, err
	}
	if err = tx.Commit(); err != nil {
		return ShopBuyResult{}, false, err
	}
	return result, true, nil
}
