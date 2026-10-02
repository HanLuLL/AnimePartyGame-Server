package db

import (
	"context"
	"database/sql"
	"encoding/json"
	"errors"
	"fmt"
	"time"
)

const EffectCardTransferBomb int32 = 20021

type BombState struct {
	OwnerPlayerID int64
	PlayerID      int64
	CardID        int32
	IsOpen        bool
}

type BombCardUseResult struct {
	PlayerID      int64
	TargetID      int64
	Cards         []CardState
	Bombs         []BombState
	UseCardNum    int32
	UseCardMaxNum int32
}

type BombThrowResult struct {
	PlayerID       int64
	TargetPlayerID int64
	Round          int32
	OldHP          int32
	NewHP          int32
	MaxHP          int32
	PlayerBombs    []BombState
	TargetBombs    []BombState
	RemovedBuffs   []BuffState
	More           bool
	Passed         bool
}

// BeginBombThrow persists the turn-start prompt so SyncRoom can restore it.
func (s *Store) BeginBombThrow(ctx context.Context, roomID, playerID int64, round int32) (bool, error) {
	tx, err := s.db.BeginTx(ctx, nil)
	if err != nil {
		return false, err
	}
	defer tx.Rollback()
	var current int64
	var currentRound, pending int32
	var phase string
	if err = tx.QueryRowContext(ctx, `SELECT current_player_id,round,pending_move,turn_phase FROM game_sessions WHERE room_id=? AND status='running'`, roomID).Scan(&current, &currentRound, &pending, &phase); err != nil {
		return false, err
	}
	if current != playerID {
		return false, ErrTurnPlayerMismatch
	}
	if currentRound != round || pending != 0 {
		return false, nil
	}
	if phase != TurnPhaseThrowDice && phase != TurnPhaseBombThrow {
		return false, nil
	}
	var count int
	if err = tx.QueryRowContext(ctx, `SELECT COUNT(*) FROM game_bombs WHERE room_id=? AND holder_player_id=?`, roomID, playerID).Scan(&count); err != nil {
		return false, err
	}
	if count == 0 {
		if phase == TurnPhaseBombThrow {
			if _, err = tx.ExecContext(ctx, `UPDATE game_sessions SET turn_phase=? WHERE room_id=? AND status='running' AND current_player_id=? AND round=? AND pending_move=0 AND turn_phase=?`, TurnPhaseThrowDice, roomID, playerID, round, TurnPhaseBombThrow); err != nil {
				return false, err
			}
		}
		return false, tx.Commit()
	}
	if phase == TurnPhaseThrowDice {
		res, updateErr := tx.ExecContext(ctx, `UPDATE game_sessions SET turn_phase=? WHERE room_id=? AND status='running' AND current_player_id=? AND round=? AND pending_move=0 AND turn_phase=?`, TurnPhaseBombThrow, roomID, playerID, round, TurnPhaseThrowDice)
		if updateErr != nil {
			return false, updateErr
		}
		if changed, changeErr := res.RowsAffected(); changeErr != nil || changed != 1 {
			if changeErr != nil {
				return false, changeErr
			}
			return false, ErrActionNotReady
		}
	}
	if _, err = tx.ExecContext(ctx, `UPDATE game_bombs SET is_open=1 WHERE room_id=? AND holder_player_id=?`, roomID, playerID); err != nil {
		return false, err
	}
	if err = tx.Commit(); err != nil {
		return false, err
	}
	return true, nil
}

// CompleteBombCardUse consumes card 20021 and places its bomb on the next
// room seat in the same transaction as the optional-card limit and UPSN key.
func (s *Store) CompleteBombCardUse(ctx context.Context, roomID, playerID int64, round int32, cmd uint16, upsn int64, payload []byte) (BombCardUseResult, bool, error) {
	if roomID <= 0 || playerID <= 0 || round <= 0 {
		return BombCardUseResult{}, false, errors.New("bomb card action has invalid parameters")
	}
	tx, err := s.db.BeginTx(ctx, nil)
	if err != nil {
		return BombCardUseResult{}, false, err
	}
	defer tx.Rollback()
	if upsn > 0 {
		var existing int
		if err = tx.QueryRowContext(ctx, `SELECT COUNT(*) FROM game_actions WHERE room_id=? AND player_id=? AND upsn=?`, roomID, playerID, upsn).Scan(&existing); err != nil {
			return BombCardUseResult{}, false, err
		}
		if existing > 0 {
			return BombCardUseResult{}, false, nil
		}
	}
	var current int64
	var currentRound, pending int32
	var phase string
	if err = tx.QueryRowContext(ctx, `SELECT current_player_id,round,pending_move,turn_phase FROM game_sessions WHERE room_id=? AND status='running'`, roomID).Scan(&current, &currentRound, &pending, &phase); err != nil {
		return BombCardUseResult{}, false, err
	}
	if current != playerID {
		return BombCardUseResult{}, false, ErrTurnPlayerMismatch
	}
	if currentRound != round || pending != 0 || phase != TurnPhaseThrowDice {
		return BombCardUseResult{}, false, ErrActionNotReady
	}
	if _, err = tx.ExecContext(ctx, `INSERT OR IGNORE INTO game_card_turns(room_id,player_id,round,done,created_at) VALUES(?,?,?,0,?)`, roomID, playerID, round, time.Now().Unix()); err != nil {
		return BombCardUseResult{}, false, err
	}
	var done, used, max int32
	if err = tx.QueryRowContext(ctx, `SELECT done,use_card_num,use_card_max_num FROM game_card_turns WHERE room_id=? AND player_id=? AND round=?`, roomID, playerID, round).Scan(&done, &used, &max); err != nil {
		return BombCardUseResult{}, false, err
	}
	if done != 0 || used >= max {
		return BombCardUseResult{}, false, ErrActionNotReady
	}
	seats, err := roomSeatsTx(ctx, tx, roomID)
	if err != nil {
		return BombCardUseResult{}, false, err
	}
	playerIndex := -1
	for index, seat := range seats {
		if seat.PlayerID == playerID {
			playerIndex = index
			break
		}
	}
	if playerIndex < 0 || len(seats) < 2 {
		return BombCardUseResult{}, false, ErrActionNotReady
	}
	targetID := seats[(playerIndex+1)%len(seats)].PlayerID
	var cardsJSON string
	if err = tx.QueryRowContext(ctx, `SELECT battle_cards_json FROM players WHERE id=? AND room_id=?`, playerID, roomID).Scan(&cardsJSON); err != nil {
		return BombCardUseResult{}, false, err
	}
	var result BombCardUseResult
	result.PlayerID = playerID
	result.TargetID = targetID
	result.UseCardNum = used + 1
	result.UseCardMaxNum = max
	if cardsJSON != "" {
		if err = json.Unmarshal([]byte(cardsJSON), &result.Cards); err != nil {
			return BombCardUseResult{}, false, fmt.Errorf("decode bomb card hand: %w", err)
		}
	}
	cardIndex := -1
	for index, card := range result.Cards {
		if card.CardID == EffectCardTransferBomb {
			cardIndex = index
			break
		}
	}
	if cardIndex < 0 {
		return BombCardUseResult{}, false, ErrActionNotReady
	}
	result.Cards = append(result.Cards[:cardIndex], result.Cards[cardIndex+1:]...)
	encodedCards, err := json.Marshal(result.Cards)
	if err != nil {
		return BombCardUseResult{}, false, err
	}
	if _, err = tx.ExecContext(ctx, `UPDATE players SET battle_cards_json=? WHERE id=? AND room_id=?`, string(encodedCards), playerID, roomID); err != nil {
		return BombCardUseResult{}, false, err
	}
	if _, err = tx.ExecContext(ctx, `INSERT INTO game_bombs(room_id,owner_player_id,holder_player_id,card_id,is_open,created_at) VALUES(?,?,?,?,0,?)`, roomID, playerID, targetID, EffectCardTransferBomb, time.Now().Unix()); err != nil {
		return BombCardUseResult{}, false, err
	}
	if upsn > 0 {
		res, insertErr := tx.ExecContext(ctx, `INSERT OR IGNORE INTO game_actions(room_id,player_id,cmd_id,upsn,payload,created_at) VALUES(?,?,?,?,?,?)`, roomID, playerID, cmd, upsn, payload, time.Now().Unix())
		if insertErr != nil {
			return BombCardUseResult{}, false, insertErr
		}
		if inserted, rowsErr := res.RowsAffected(); rowsErr != nil || inserted == 0 {
			if rowsErr != nil {
				return BombCardUseResult{}, false, rowsErr
			}
			return BombCardUseResult{}, false, nil
		}
	}
	doneValue := 0
	if result.UseCardNum >= result.UseCardMaxNum {
		doneValue = 1
	}
	if _, err = tx.ExecContext(ctx, `UPDATE game_card_turns SET done=?,use_card_num=?,use_card_max_num=? WHERE room_id=? AND player_id=? AND round=? AND done=0`, doneValue, result.UseCardNum, result.UseCardMaxNum, roomID, playerID, round); err != nil {
		return BombCardUseResult{}, false, err
	}
	result.Bombs, err = bombsForPlayerTx(ctx, tx, roomID, targetID)
	if err != nil {
		return BombCardUseResult{}, false, err
	}
	if err = tx.Commit(); err != nil {
		return BombCardUseResult{}, false, err
	}
	return result, true, nil
}

// CompleteBombThrow trusts only the server's roll and atomically passes or
// explodes one bomb, updates HP, records the action, and restores the next phase.
func (s *Store) CompleteBombThrow(ctx context.Context, roomID, playerID int64, round int32, point int32, maxHP int32, cmd uint16, upsn int64, payload []byte) (BombThrowResult, bool, error) {
	if roomID <= 0 || playerID <= 0 || round <= 0 || point < 1 || point > 6 || maxHP <= 0 {
		return BombThrowResult{}, false, errors.New("bomb throw action has invalid parameters")
	}
	tx, err := s.db.BeginTx(ctx, nil)
	if err != nil {
		return BombThrowResult{}, false, err
	}
	defer tx.Rollback()
	if upsn > 0 {
		var existing int
		if err = tx.QueryRowContext(ctx, `SELECT COUNT(*) FROM game_actions WHERE room_id=? AND player_id=? AND upsn=?`, roomID, playerID, upsn).Scan(&existing); err != nil {
			return BombThrowResult{}, false, err
		}
		if existing > 0 {
			return BombThrowResult{}, false, nil
		}
	}
	var current int64
	var currentRound, pending int32
	var phase string
	if err = tx.QueryRowContext(ctx, `SELECT current_player_id,round,pending_move,turn_phase FROM game_sessions WHERE room_id=? AND status='running'`, roomID).Scan(&current, &currentRound, &pending, &phase); err != nil {
		return BombThrowResult{}, false, err
	}
	if current != playerID {
		return BombThrowResult{}, false, ErrTurnPlayerMismatch
	}
	if currentRound != round || pending != 0 || phase != TurnPhaseBombThrow {
		return BombThrowResult{}, false, ErrActionNotReady
	}
	var bombID int64
	if err = tx.QueryRowContext(ctx, `SELECT id FROM game_bombs WHERE room_id=? AND holder_player_id=? ORDER BY id LIMIT 1`, roomID, playerID).Scan(&bombID); err != nil {
		if errors.Is(err, sql.ErrNoRows) {
			return BombThrowResult{}, false, ErrActionNotReady
		}
		return BombThrowResult{}, false, err
	}
	result := BombThrowResult{PlayerID: playerID, Round: round, MaxHP: maxHP, Passed: point > 1}
	var buffsJSON string
	if err = tx.QueryRowContext(ctx, `SELECT hp,battle_buffs_json FROM players WHERE id=? AND room_id=?`, playerID, roomID).Scan(&result.OldHP, &buffsJSON); err != nil {
		return BombThrowResult{}, false, err
	}
	result.NewHP = result.OldHP
	if point == 1 {
		if _, err = tx.ExecContext(ctx, `DELETE FROM game_bombs WHERE id=? AND room_id=?`, bombID, roomID); err != nil {
			return BombThrowResult{}, false, err
		}
		damage := int32(99)
		if result.OldHP > 0 {
			buffs := []BuffState{}
			if buffsJSON != "" {
				if err = json.Unmarshal([]byte(buffsJSON), &buffs); err != nil {
					return BombThrowResult{}, false, fmt.Errorf("decode battle buffs for player %d: %w", playerID, err)
				}
			}
			var remaining []BuffState
			damage, remaining, result.RemovedBuffs = ApplyDestinyDamageBuffs(damage, buffs)
			if len(result.RemovedBuffs) > 0 {
				encoded, marshalErr := json.Marshal(remaining)
				if marshalErr != nil {
					return BombThrowResult{}, false, marshalErr
				}
				buffsJSON = string(encoded)
			}
		}
		result.NewHP -= damage
		if result.NewHP < 0 {
			result.NewHP = 0
		}
		if _, err = tx.ExecContext(ctx, `UPDATE players SET hp=?,battle_buffs_json=? WHERE id=? AND room_id=?`, result.NewHP, buffsJSON, playerID, roomID); err != nil {
			return BombThrowResult{}, false, err
		}
		if result.NewHP == 0 {
			// Buff 200601 is configured to clear on death; bombs follow that lifetime.
			if _, err = tx.ExecContext(ctx, `DELETE FROM game_bombs WHERE room_id=? AND holder_player_id=?`, roomID, playerID); err != nil {
				return BombThrowResult{}, false, err
			}
		}
	} else {
		seats, seatErr := roomSeatsTx(ctx, tx, roomID)
		if seatErr != nil {
			return BombThrowResult{}, false, seatErr
		}
		index := -1
		for i, seat := range seats {
			if seat.PlayerID == playerID {
				index = i
				break
			}
		}
		if index < 0 || len(seats) < 2 {
			return BombThrowResult{}, false, ErrActionNotReady
		}
		result.TargetPlayerID = seats[(index+1)%len(seats)].PlayerID
		if _, err = tx.ExecContext(ctx, `UPDATE game_bombs SET holder_player_id=?,is_open=0 WHERE id=? AND room_id=?`, result.TargetPlayerID, bombID, roomID); err != nil {
			return BombThrowResult{}, false, err
		}
	}
	result.PlayerBombs, err = bombsForPlayerTx(ctx, tx, roomID, playerID)
	if err != nil {
		return BombThrowResult{}, false, err
	}
	if result.TargetPlayerID != 0 {
		result.TargetBombs, err = bombsForPlayerTx(ctx, tx, roomID, result.TargetPlayerID)
		if err != nil {
			return BombThrowResult{}, false, err
		}
	}
	result.More = len(result.PlayerBombs) > 0 && result.NewHP > 0
	nextPhase := TurnPhaseThrowDice
	if result.More {
		nextPhase = TurnPhaseBombThrow
	}
	res, err := tx.ExecContext(ctx, `UPDATE game_sessions SET turn_phase=? WHERE room_id=? AND status='running' AND current_player_id=? AND round=? AND pending_move=0 AND turn_phase=?`, nextPhase, roomID, playerID, round, TurnPhaseBombThrow)
	if err != nil {
		return BombThrowResult{}, false, err
	}
	if changed, changeErr := res.RowsAffected(); changeErr != nil || changed != 1 {
		if changeErr != nil {
			return BombThrowResult{}, false, changeErr
		}
		return BombThrowResult{}, false, ErrActionNotReady
	}
	if upsn > 0 {
		res, insertErr := tx.ExecContext(ctx, `INSERT OR IGNORE INTO game_actions(room_id,player_id,cmd_id,upsn,payload,created_at) VALUES(?,?,?,?,?,?)`, roomID, playerID, cmd, upsn, payload, time.Now().Unix())
		if insertErr != nil {
			return BombThrowResult{}, false, insertErr
		}
		if inserted, rowsErr := res.RowsAffected(); rowsErr != nil || inserted == 0 {
			if rowsErr != nil {
				return BombThrowResult{}, false, rowsErr
			}
			return BombThrowResult{}, false, nil
		}
	}
	if err = tx.Commit(); err != nil {
		return BombThrowResult{}, false, err
	}
	return result, true, nil
}

type roomSeat struct {
	PlayerID int64
	Slot     int32
}

func roomSeatsTx(ctx context.Context, tx *sql.Tx, roomID int64) ([]roomSeat, error) {
	rows, err := tx.QueryContext(ctx, `SELECT player_id,slot FROM room_members WHERE room_id=? ORDER BY slot`, roomID)
	if err != nil {
		return nil, err
	}
	defer rows.Close()
	var seats []roomSeat
	for rows.Next() {
		var seat roomSeat
		if err = rows.Scan(&seat.PlayerID, &seat.Slot); err != nil {
			return nil, err
		}
		seats = append(seats, seat)
	}
	return seats, rows.Err()
}

func bombsForPlayerTx(ctx context.Context, tx *sql.Tx, roomID, playerID int64) ([]BombState, error) {
	rows, err := tx.QueryContext(ctx, `SELECT owner_player_id,holder_player_id,card_id,is_open FROM game_bombs WHERE room_id=? AND holder_player_id=? ORDER BY id`, roomID, playerID)
	if err != nil {
		return nil, err
	}
	defer rows.Close()
	var bombs []BombState
	for rows.Next() {
		var bomb BombState
		var isOpen int
		if err = rows.Scan(&bomb.OwnerPlayerID, &bomb.PlayerID, &bomb.CardID, &isOpen); err != nil {
			return nil, err
		}
		bomb.IsOpen = isOpen != 0
		bombs = append(bombs, bomb)
	}
	return bombs, rows.Err()
}
