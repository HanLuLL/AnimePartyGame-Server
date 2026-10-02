package db

import (
	"context"
	"database/sql"
	"encoding/json"
	"errors"
	"time"
)

// BeginSkillEventSelection consumes the per-turn effect-card/skill slot and
// persists the exact choice shown to the client before asking it to select.
func (s *Store) BeginSkillEventSelection(ctx context.Context, roomID, playerID int64, round int32, cmd uint16, upsn int64, payload []byte, actionSN int64, skillID, cooldownRounds int32, choices []int32) (bool, error) {
	if roomID <= 0 || playerID <= 0 || round <= 0 || actionSN <= 0 || skillID <= 0 || cooldownRounds <= 0 || len(choices) != 2 || choices[0] <= 0 || choices[1] <= 0 || choices[0] == choices[1] {
		return false, errors.New("skill event selection has invalid parameters")
	}
	tx, err := s.db.BeginTx(ctx, nil)
	if err != nil {
		return false, err
	}
	defer tx.Rollback()
	if upsn > 0 {
		var existing int
		if err = tx.QueryRowContext(ctx, `SELECT COUNT(*) FROM game_actions WHERE room_id=? AND player_id=? AND upsn=?`, roomID, playerID, upsn).Scan(&existing); err != nil {
			return false, err
		}
		if existing > 0 {
			return false, nil
		}
	}
	var currentPlayer int64
	var currentRound, pending int32
	var phase string
	if err = tx.QueryRowContext(ctx, `SELECT current_player_id,round,pending_move,turn_phase FROM game_sessions WHERE room_id=? AND status='running'`, roomID).Scan(&currentPlayer, &currentRound, &pending, &phase); err != nil {
		return false, err
	}
	if currentPlayer != playerID {
		return false, ErrTurnPlayerMismatch
	}
	if currentRound != round || pending != 0 || phase != TurnPhaseThrowDice {
		return false, ErrActionNotReady
	}
	if _, err = tx.ExecContext(ctx, `INSERT OR IGNORE INTO game_card_turns(room_id,player_id,round,done,created_at) VALUES(?,?,?,0,?)`, roomID, playerID, round, time.Now().Unix()); err != nil {
		return false, err
	}
	var done int
	if err = tx.QueryRowContext(ctx, `SELECT done FROM game_card_turns WHERE room_id=? AND player_id=? AND round=?`, roomID, playerID, round).Scan(&done); err != nil {
		return false, err
	}
	if done != 0 {
		return false, ErrActionNotReady
	}
	var readyRound int32
	err = tx.QueryRowContext(ctx, `SELECT ready_round FROM game_skill_cooldowns WHERE room_id=? AND player_id=? AND skill_id=?`, roomID, playerID, skillID).Scan(&readyRound)
	if err != nil && !errors.Is(err, sql.ErrNoRows) {
		return false, err
	}
	if err == nil && round < readyRound {
		return false, ErrActionNotReady
	}
	if upsn > 0 {
		res, insertErr := tx.ExecContext(ctx, `INSERT OR IGNORE INTO game_actions(room_id,player_id,cmd_id,upsn,payload,created_at) VALUES(?,?,?,?,?,?)`, roomID, playerID, cmd, upsn, payload, time.Now().Unix())
		if insertErr != nil {
			return false, insertErr
		}
		inserted, rowsErr := res.RowsAffected()
		if rowsErr != nil {
			return false, rowsErr
		}
		if inserted == 0 {
			return false, nil
		}
	}
	choicesJSON, err := json.Marshal(choices)
	if err != nil {
		return false, err
	}
	res, err := tx.ExecContext(ctx, `UPDATE game_card_turns SET done=1 WHERE room_id=? AND player_id=? AND round=? AND done=0`, roomID, playerID, round)
	if err != nil {
		return false, err
	}
	updated, err := res.RowsAffected()
	if err != nil {
		return false, err
	}
	if updated != 1 {
		return false, ErrActionNotReady
	}
	now := time.Now().Unix()
	if _, err = tx.ExecContext(ctx, `INSERT INTO game_skill_event_offers(room_id,player_id,round,action_sn,choices_json,created_at) VALUES(?,?,?,?,?,?) ON CONFLICT(room_id) DO UPDATE SET player_id=excluded.player_id,round=excluded.round,action_sn=excluded.action_sn,choices_json=excluded.choices_json,created_at=excluded.created_at`, roomID, playerID, round, actionSN, string(choicesJSON), now); err != nil {
		return false, err
	}
	if _, err = tx.ExecContext(ctx, `INSERT INTO game_skill_cooldowns(room_id,player_id,skill_id,ready_round) VALUES(?,?,?,?) ON CONFLICT(room_id,player_id,skill_id) DO UPDATE SET ready_round=excluded.ready_round`, roomID, playerID, skillID, round+cooldownRounds); err != nil {
		return false, err
	}
	res, err = tx.ExecContext(ctx, `UPDATE game_sessions SET turn_phase=? WHERE room_id=? AND status='running' AND current_player_id=? AND round=? AND pending_move=0 AND turn_phase=?`, TurnPhaseSelectEvent, roomID, playerID, round, TurnPhaseThrowDice)
	if err != nil {
		return false, err
	}
	updated, err = res.RowsAffected()
	if err != nil {
		return false, err
	}
	if updated != 1 {
		return false, ErrActionNotReady
	}
	if err = tx.Commit(); err != nil {
		return false, err
	}
	return true, nil
}

// SkillEventOffer restores the same candidate order and action SN after a
// reconnect; the client must submit that offer unchanged.
func (s *Store) SkillEventOffer(ctx context.Context, roomID, playerID int64) ([]int32, int64, bool, error) {
	var encoded string
	var actionSN int64
	err := s.db.QueryRowContext(ctx, `SELECT choices_json,action_sn FROM game_skill_event_offers WHERE room_id=? AND player_id=?`, roomID, playerID).Scan(&encoded, &actionSN)
	if errors.Is(err, sql.ErrNoRows) {
		return nil, 0, false, nil
	}
	if err != nil {
		return nil, 0, false, err
	}
	var choices []int32
	if err = json.Unmarshal([]byte(encoded), &choices); err != nil {
		return nil, 0, false, err
	}
	if len(choices) != 2 || choices[0] <= 0 || choices[1] <= 0 || choices[0] == choices[1] || actionSN <= 0 {
		return nil, 0, false, errors.New("stored skill event offer is invalid")
	}
	return choices, actionSN, true, nil
}
