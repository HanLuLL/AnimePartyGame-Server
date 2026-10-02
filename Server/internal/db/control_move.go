package db

import (
	"context"
	"database/sql"
	"encoding/json"
	"errors"
	"fmt"
	"time"
)

type ControlMoveCardResult struct {
	PlayerID      int64
	Round         int32
	CardID        int32
	MaxPoint      int32
	ActionSN      int64
	Cards         []CardState
	UseCardNum    int32
	UseCardMaxNum int32
	SkillID       int32
	SkillCooldown int32
}

type ControlMovePrompt struct {
	CardID   int32
	MaxPoint int32
	ActionSN int64
}

// BeginControlMoveCardUse consumes the card and persists its 5067 prompt before
// any packet is sent. The card turn, optional cooldown change and replay key
// commit with the prompt in one SQLite transaction.
func (s *Store) BeginControlMoveCardUse(ctx context.Context, roomID, playerID int64, round int32, cmd uint16, upsn int64, payload []byte, cardID, maxPoint int32, actionSN int64, skillID, cooldownDelta, cardLimitDelta int32) (ControlMoveCardResult, bool, error) {
	if roomID <= 0 || playerID <= 0 || round <= 0 || actionSN <= 0 || cardID <= 0 || maxPoint < 1 || maxPoint > 6 || skillID < 0 || cooldownDelta > 0 || cardLimitDelta < 0 {
		return ControlMoveCardResult{}, false, errors.New("control-move card action has invalid parameters")
	}
	tx, err := s.db.BeginTx(ctx, nil)
	if err != nil {
		return ControlMoveCardResult{}, false, err
	}
	defer tx.Rollback()
	if upsn > 0 {
		var existing int
		if err = tx.QueryRowContext(ctx, `SELECT COUNT(*) FROM game_actions WHERE room_id=? AND player_id=? AND upsn=?`, roomID, playerID, upsn).Scan(&existing); err != nil {
			return ControlMoveCardResult{}, false, err
		}
		if existing > 0 {
			return ControlMoveCardResult{}, false, nil
		}
	}
	var currentPlayer int64
	var currentRound, pending int32
	var phase string
	if err = tx.QueryRowContext(ctx, `SELECT current_player_id,round,pending_move,turn_phase FROM game_sessions WHERE room_id=? AND status='running'`, roomID).Scan(&currentPlayer, &currentRound, &pending, &phase); err != nil {
		return ControlMoveCardResult{}, false, err
	}
	if currentPlayer != playerID {
		return ControlMoveCardResult{}, false, ErrTurnPlayerMismatch
	}
	if currentRound != round || pending != 0 || phase != TurnPhaseThrowDice {
		return ControlMoveCardResult{}, false, ErrActionNotReady
	}
	var promptCount int
	if err = tx.QueryRowContext(ctx, `SELECT COUNT(*) FROM game_control_moves WHERE room_id=?`, roomID).Scan(&promptCount); err != nil {
		return ControlMoveCardResult{}, false, err
	}
	if promptCount != 0 {
		return ControlMoveCardResult{}, false, ErrActionNotReady
	}
	if _, err = tx.ExecContext(ctx, `INSERT OR IGNORE INTO game_card_turns(room_id,player_id,round,done,created_at) VALUES(?,?,?,0,?)`, roomID, playerID, round, time.Now().Unix()); err != nil {
		return ControlMoveCardResult{}, false, err
	}
	var done, usedCardNum, maxCardNum int32
	if err = tx.QueryRowContext(ctx, `SELECT done,use_card_num,use_card_max_num FROM game_card_turns WHERE room_id=? AND player_id=? AND round=?`, roomID, playerID, round).Scan(&done, &usedCardNum, &maxCardNum); err != nil {
		return ControlMoveCardResult{}, false, err
	}
	if done != 0 || usedCardNum >= maxCardNum+cardLimitDelta {
		return ControlMoveCardResult{}, false, ErrActionNotReady
	}
	result := ControlMoveCardResult{PlayerID: playerID, Round: round, CardID: cardID, MaxPoint: maxPoint, ActionSN: actionSN, SkillID: skillID, UseCardNum: usedCardNum + 1, UseCardMaxNum: maxCardNum + cardLimitDelta}
	if result.UseCardMaxNum < result.UseCardNum {
		result.UseCardMaxNum = result.UseCardNum
	}
	var cardsJSON string
	if err = tx.QueryRowContext(ctx, `SELECT battle_cards_json FROM players WHERE id=? AND room_id=?`, playerID, roomID).Scan(&cardsJSON); err != nil {
		return ControlMoveCardResult{}, false, err
	}
	if cardsJSON != "" {
		if err = json.Unmarshal([]byte(cardsJSON), &result.Cards); err != nil {
			return ControlMoveCardResult{}, false, fmt.Errorf("decode control-move card hand: %w", err)
		}
	}
	cardIndex := -1
	for index, card := range result.Cards {
		if card.CardID == cardID {
			cardIndex = index
			break
		}
	}
	if cardIndex < 0 {
		return ControlMoveCardResult{}, false, ErrActionNotReady
	}
	result.Cards = append(result.Cards[:cardIndex], result.Cards[cardIndex+1:]...)
	encodedCards, err := json.Marshal(result.Cards)
	if err != nil {
		return ControlMoveCardResult{}, false, err
	}
	if _, err = tx.ExecContext(ctx, `UPDATE players SET battle_cards_json=? WHERE id=? AND room_id=?`, string(encodedCards), playerID, roomID); err != nil {
		return ControlMoveCardResult{}, false, err
	}
	if skillID > 0 && cooldownDelta < 0 {
		var readyRound int32
		queryErr := tx.QueryRowContext(ctx, `SELECT ready_round FROM game_skill_cooldowns WHERE room_id=? AND player_id=? AND skill_id=?`, roomID, playerID, skillID).Scan(&readyRound)
		if queryErr != nil && !errors.Is(queryErr, sql.ErrNoRows) {
			return ControlMoveCardResult{}, false, queryErr
		}
		if queryErr == nil && readyRound > round {
			readyRound += cooldownDelta
			if readyRound < round {
				readyRound = round
			}
			if readyRound <= round {
				if _, err = tx.ExecContext(ctx, `DELETE FROM game_skill_cooldowns WHERE room_id=? AND player_id=? AND skill_id=?`, roomID, playerID, skillID); err != nil {
					return ControlMoveCardResult{}, false, err
				}
			} else if _, err = tx.ExecContext(ctx, `UPDATE game_skill_cooldowns SET ready_round=? WHERE room_id=? AND player_id=? AND skill_id=?`, readyRound, roomID, playerID, skillID); err != nil {
				return ControlMoveCardResult{}, false, err
			}
			result.SkillCooldown = readyRound - round
		}
	}
	if upsn > 0 {
		res, insertErr := tx.ExecContext(ctx, `INSERT OR IGNORE INTO game_actions(room_id,player_id,cmd_id,upsn,payload,created_at) VALUES(?,?,?,?,?,?)`, roomID, playerID, cmd, upsn, payload, time.Now().Unix())
		if insertErr != nil {
			return ControlMoveCardResult{}, false, insertErr
		}
		if inserted, rowsErr := res.RowsAffected(); rowsErr != nil || inserted == 0 {
			if rowsErr != nil {
				return ControlMoveCardResult{}, false, rowsErr
			}
			return ControlMoveCardResult{}, false, nil
		}
	}
	doneValue := 0
	if result.UseCardNum >= result.UseCardMaxNum {
		doneValue = 1
	}
	res, err := tx.ExecContext(ctx, `UPDATE game_card_turns SET done=?,use_card_num=?,use_card_max_num=? WHERE room_id=? AND player_id=? AND round=? AND done=0`, doneValue, result.UseCardNum, result.UseCardMaxNum, roomID, playerID, round)
	if err != nil {
		return ControlMoveCardResult{}, false, err
	}
	if changed, changeErr := res.RowsAffected(); changeErr != nil || changed != 1 {
		if changeErr != nil {
			return ControlMoveCardResult{}, false, changeErr
		}
		return ControlMoveCardResult{}, false, ErrActionNotReady
	}
	if _, err = tx.ExecContext(ctx, `INSERT INTO game_control_moves(room_id,player_id,round,card_id,max_point,action_sn,created_at) VALUES(?,?,?,?,?,?,?)`, roomID, playerID, round, cardID, maxPoint, actionSN, time.Now().Unix()); err != nil {
		return ControlMoveCardResult{}, false, err
	}
	res, err = tx.ExecContext(ctx, `UPDATE game_sessions SET turn_phase=? WHERE room_id=? AND status='running' AND current_player_id=? AND round=? AND pending_move=0 AND turn_phase=?`, TurnPhaseControlMove, roomID, playerID, round, TurnPhaseThrowDice)
	if err != nil {
		return ControlMoveCardResult{}, false, err
	}
	if changed, changeErr := res.RowsAffected(); changeErr != nil || changed != 1 {
		if changeErr != nil {
			return ControlMoveCardResult{}, false, changeErr
		}
		return ControlMoveCardResult{}, false, ErrActionNotReady
	}
	if err = tx.Commit(); err != nil {
		return ControlMoveCardResult{}, false, err
	}
	return result, true, nil
}

func (s *Store) ControlMovePrompt(ctx context.Context, roomID, playerID int64, round int32) (ControlMovePrompt, bool, error) {
	var prompt ControlMovePrompt
	err := s.db.QueryRowContext(ctx, `SELECT card_id,max_point,action_sn FROM game_control_moves WHERE room_id=? AND player_id=? AND round=? AND selected_point=0`, roomID, playerID, round).Scan(&prompt.CardID, &prompt.MaxPoint, &prompt.ActionSN)
	if errors.Is(err, sql.ErrNoRows) {
		return ControlMovePrompt{}, false, nil
	}
	if err != nil {
		return ControlMovePrompt{}, false, err
	}
	if prompt.CardID <= 0 || prompt.MaxPoint < 1 || prompt.MaxPoint > 6 || prompt.ActionSN <= 0 {
		return ControlMovePrompt{}, false, errors.New("stored control-move prompt is invalid")
	}
	return prompt, true, nil
}

func (s *Store) CompleteControlMoveSelection(ctx context.Context, roomID, playerID int64, round int32, actionSN int64, point int32, cmd uint16, upsn int64, payload []byte) (ControlMovePrompt, bool, error) {
	if roomID <= 0 || playerID <= 0 || round <= 0 || actionSN <= 0 || point < 1 || point > 6 {
		return ControlMovePrompt{}, false, errors.New("control-move selection has invalid parameters")
	}
	tx, err := s.db.BeginTx(ctx, nil)
	if err != nil {
		return ControlMovePrompt{}, false, err
	}
	defer tx.Rollback()
	if upsn > 0 {
		var existing int
		if err = tx.QueryRowContext(ctx, `SELECT COUNT(*) FROM game_actions WHERE room_id=? AND player_id=? AND upsn=?`, roomID, playerID, upsn).Scan(&existing); err != nil {
			return ControlMovePrompt{}, false, err
		}
		if existing > 0 {
			return ControlMovePrompt{}, false, nil
		}
	}
	var currentPlayer int64
	var currentRound, pending int32
	var phase string
	if err = tx.QueryRowContext(ctx, `SELECT current_player_id,round,pending_move,turn_phase FROM game_sessions WHERE room_id=? AND status='running'`, roomID).Scan(&currentPlayer, &currentRound, &pending, &phase); err != nil {
		return ControlMovePrompt{}, false, err
	}
	if currentPlayer != playerID {
		return ControlMovePrompt{}, false, ErrTurnPlayerMismatch
	}
	if currentRound != round || pending != 0 || phase != TurnPhaseControlMove {
		return ControlMovePrompt{}, false, ErrActionNotReady
	}
	var prompt ControlMovePrompt
	if err = tx.QueryRowContext(ctx, `SELECT card_id,max_point,action_sn FROM game_control_moves WHERE room_id=? AND player_id=? AND round=? AND selected_point=0`, roomID, playerID, round).Scan(&prompt.CardID, &prompt.MaxPoint, &prompt.ActionSN); err != nil {
		if errors.Is(err, sql.ErrNoRows) {
			return ControlMovePrompt{}, false, ErrActionNotReady
		}
		return ControlMovePrompt{}, false, err
	}
	if prompt.ActionSN != actionSN || point > prompt.MaxPoint {
		return ControlMovePrompt{}, false, ErrActionNotReady
	}
	if upsn > 0 {
		res, insertErr := tx.ExecContext(ctx, `INSERT OR IGNORE INTO game_actions(room_id,player_id,cmd_id,upsn,payload,created_at) VALUES(?,?,?,?,?,?)`, roomID, playerID, cmd, upsn, payload, time.Now().Unix())
		if insertErr != nil {
			return ControlMovePrompt{}, false, insertErr
		}
		if inserted, rowsErr := res.RowsAffected(); rowsErr != nil || inserted == 0 {
			if rowsErr != nil {
				return ControlMovePrompt{}, false, rowsErr
			}
			return ControlMovePrompt{}, false, nil
		}
	}
	res, err := tx.ExecContext(ctx, `UPDATE game_control_moves SET selected_point=? WHERE room_id=? AND player_id=? AND round=? AND action_sn=? AND selected_point=0`, point, roomID, playerID, round, actionSN)
	if err != nil {
		return ControlMovePrompt{}, false, err
	}
	if changed, changeErr := res.RowsAffected(); changeErr != nil || changed != 1 {
		if changeErr != nil {
			return ControlMovePrompt{}, false, changeErr
		}
		return ControlMovePrompt{}, false, ErrActionNotReady
	}
	res, err = tx.ExecContext(ctx, `UPDATE game_sessions SET turn_phase=? WHERE room_id=? AND status='running' AND current_player_id=? AND round=? AND pending_move=0 AND turn_phase=?`, TurnPhaseThrowDice, roomID, playerID, round, TurnPhaseControlMove)
	if err != nil {
		return ControlMovePrompt{}, false, err
	}
	if changed, changeErr := res.RowsAffected(); changeErr != nil || changed != 1 {
		if changeErr != nil {
			return ControlMovePrompt{}, false, changeErr
		}
		return ControlMovePrompt{}, false, ErrActionNotReady
	}
	if err = tx.Commit(); err != nil {
		return ControlMovePrompt{}, false, err
	}
	return ControlMovePrompt{CardID: prompt.CardID, MaxPoint: prompt.MaxPoint, ActionSN: prompt.ActionSN}, true, nil
}

func (s *Store) ControlMoveChoice(ctx context.Context, roomID, playerID int64, round int32) (int32, bool, error) {
	var point int32
	err := s.db.QueryRowContext(ctx, `SELECT selected_point FROM game_control_moves WHERE room_id=? AND player_id=? AND round=?`, roomID, playerID, round).Scan(&point)
	if errors.Is(err, sql.ErrNoRows) {
		return 0, false, nil
	}
	if err != nil {
		return 0, false, err
	}
	if point == 0 {
		return 0, false, nil
	}
	if point < 1 || point > 6 {
		return 0, false, errors.New("stored control-move point is invalid")
	}
	return point, true, nil
}

func (s *Store) CardUseLimit(ctx context.Context, roomID, playerID int64, round int32) (int32, int32, error) {
	var used, max int32
	err := s.db.QueryRowContext(ctx, `SELECT use_card_num,use_card_max_num FROM game_card_turns WHERE room_id=? AND player_id=? AND round=?`, roomID, playerID, round).Scan(&used, &max)
	if errors.Is(err, sql.ErrNoRows) {
		return 0, 1, nil
	}
	return used, max, err
}
