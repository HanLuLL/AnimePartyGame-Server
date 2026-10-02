package db

import (
	"context"
	"encoding/json"
	"errors"
	"fmt"
	"time"
)

var ErrInvalidAbandonCards = errors.New("discarded cards do not satisfy the hand limit")

type AbandonCardsResult struct {
	Cards      []CardState
	RemovedIDs []int32
	NextPlayer int64
	Round      int32
}

// CompleteAbandonCards removes exactly the cards needed to restore the
// configured hand limit and releases the active turn in the same transaction.
func (s *Store) CompleteAbandonCards(ctx context.Context, roomID, playerID int64, cmd uint16, upsn int64, payload []byte, selectedIDs []int32, handLimit int32) (result AbandonCardsResult, created bool, err error) {
	if handLimit <= 0 || len(selectedIDs) == 0 || len(selectedIDs) > 128 {
		return result, false, ErrInvalidAbandonCards
	}
	selected := make(map[int32]struct{}, len(selectedIDs))
	for _, id := range selectedIDs {
		if id <= 0 {
			return result, false, ErrInvalidAbandonCards
		}
		if _, duplicate := selected[id]; duplicate {
			return result, false, ErrInvalidAbandonCards
		}
		selected[id] = struct{}{}
	}

	tx, err := s.db.BeginTx(ctx, nil)
	if err != nil {
		return result, false, err
	}
	defer tx.Rollback()
	if upsn > 0 {
		var existing int
		if err = tx.QueryRowContext(ctx, `SELECT COUNT(*) FROM game_actions WHERE room_id=? AND player_id=? AND upsn=?`, roomID, playerID, upsn).Scan(&existing); err != nil {
			return result, false, err
		}
		if existing > 0 {
			return result, false, nil
		}
	}
	var currentPlayer int64
	var pending int32
	var phase string
	if err = tx.QueryRowContext(ctx, `SELECT current_player_id,round,pending_move,turn_phase FROM game_sessions WHERE room_id=? AND status='running'`, roomID).Scan(&currentPlayer, &result.Round, &pending, &phase); err != nil {
		return result, false, err
	}
	if currentPlayer != playerID {
		return result, false, ErrTurnPlayerMismatch
	}
	if pending != 0 || phase != TurnPhaseAbandonCards {
		return result, false, ErrActionNotReady
	}
	var cardsJSON string
	if err = tx.QueryRowContext(ctx, `SELECT battle_cards_json FROM players WHERE id=? AND room_id=?`, playerID, roomID).Scan(&cardsJSON); err != nil {
		return result, false, err
	}
	var cards []CardState
	if cardsJSON != "" {
		if err = json.Unmarshal([]byte(cardsJSON), &cards); err != nil {
			return result, false, fmt.Errorf("decode player battle cards for discard: %w", err)
		}
	}
	if len(cards) <= int(handLimit) || len(selectedIDs) != len(cards)-int(handLimit) {
		return result, false, ErrInvalidAbandonCards
	}
	remaining := make([]CardState, 0, len(cards)-len(selectedIDs))
	for _, card := range cards {
		if _, remove := selected[card.UniqueID]; remove {
			delete(selected, card.UniqueID)
			result.RemovedIDs = append(result.RemovedIDs, card.UniqueID)
			continue
		}
		remaining = append(remaining, card)
	}
	if len(selected) != 0 || len(remaining) != int(handLimit) {
		return result, false, ErrInvalidAbandonCards
	}
	if upsn > 0 {
		res, insertErr := tx.ExecContext(ctx, `INSERT OR IGNORE INTO game_actions(room_id,player_id,cmd_id,upsn,payload,created_at) VALUES(?,?,?,?,?,?)`, roomID, playerID, cmd, upsn, payload, time.Now().Unix())
		if insertErr != nil {
			return result, false, insertErr
		}
		inserted, rowsErr := res.RowsAffected()
		if rowsErr != nil {
			return result, false, rowsErr
		}
		if inserted == 0 {
			return result, false, nil
		}
	}
	encoded, err := json.Marshal(remaining)
	if err != nil {
		return result, false, err
	}
	if _, err = tx.ExecContext(ctx, `UPDATE players SET battle_cards_json=? WHERE id=? AND room_id=?`, string(encoded), playerID, roomID); err != nil {
		return result, false, err
	}
	result.NextPlayer, result.Round, err = nextRoomTurnInTx(ctx, tx, roomID, playerID, result.Round)
	if err != nil {
		return result, false, err
	}
	res, err := tx.ExecContext(ctx, `UPDATE game_sessions SET current_player_id=?,round=?,pending_move=0,turn_phase=? WHERE room_id=? AND status='running' AND current_player_id=? AND pending_move=0 AND turn_phase=?`, result.NextPlayer, result.Round, TurnPhaseThrowDice, roomID, playerID, TurnPhaseAbandonCards)
	if err != nil {
		return result, false, err
	}
	updated, err := res.RowsAffected()
	if err != nil {
		return result, false, err
	}
	if updated != 1 {
		return result, false, ErrActionNotReady
	}
	if err = tx.Commit(); err != nil {
		return result, false, err
	}
	result.Cards = remaining
	return result, true, nil
}
