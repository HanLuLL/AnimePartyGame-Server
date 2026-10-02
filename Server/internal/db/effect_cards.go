package db

import (
	"context"
	"database/sql"
	"encoding/json"
	"errors"
	"fmt"
	"reflect"
	"time"
)

type EffectCardUseResult struct {
	PlayerID      int64
	Round         int32
	OldHP         int32
	NewHP         int32
	MaxHP         int32
	Cards         []CardState
	UseCardNum    int32
	UseCardMaxNum int32
}

type EffectCardDamageResult struct {
	PlayerID      int64
	TargetID      int64
	Round         int32
	Cards         []CardState
	UseCardNum    int32
	UseCardMaxNum int32
	TargetOldHP   int32
	TargetNewHP   int32
	TargetMaxHP   int32
	RemovedBuffs  []BuffState
}

// EffectCardTurnDone reports whether the current round's optional card action
// has already been completed. A missing row means the action is still open.
func (s *Store) EffectCardTurnDone(ctx context.Context, roomID, playerID int64, round int32) (bool, error) {
	var done int
	err := s.db.QueryRowContext(ctx, `SELECT done FROM game_card_turns WHERE room_id=? AND player_id=? AND round=?`, roomID, playerID, round).Scan(&done)
	if errors.Is(err, sql.ErrNoRows) {
		return false, nil
	}
	return done != 0, err
}

// CompleteEffectCardUse consumes a supported card or records the client's
// explicit skip. The current turn, card hand, HP change, action replay key and
// per-round card limit are committed together.
func (s *Store) CompleteEffectCardUse(ctx context.Context, roomID, playerID int64, round int32, cmd uint16, upsn int64, payload []byte, cardID, hpChange, maxHP int32) (EffectCardUseResult, bool, error) {
	if roomID <= 0 || playerID <= 0 || round <= 0 || cardID < 0 || hpChange < 0 || (cardID > 0 && (hpChange <= 0 || maxHP <= 0)) || (cardID == 0 && hpChange != 0) {
		return EffectCardUseResult{}, false, errors.New("effect card action has invalid parameters")
	}
	tx, err := s.db.BeginTx(ctx, nil)
	if err != nil {
		return EffectCardUseResult{}, false, err
	}
	defer tx.Rollback()
	if upsn > 0 {
		var existing int
		if err = tx.QueryRowContext(ctx, `SELECT COUNT(*) FROM game_actions WHERE room_id=? AND player_id=? AND upsn=?`, roomID, playerID, upsn).Scan(&existing); err != nil {
			return EffectCardUseResult{}, false, err
		}
		if existing > 0 {
			return EffectCardUseResult{}, false, nil
		}
	}
	var currentPlayer int64
	var currentRound, pending int32
	var phase string
	if err = tx.QueryRowContext(ctx, `SELECT current_player_id,round,pending_move,turn_phase FROM game_sessions WHERE room_id=? AND status='running'`, roomID).Scan(&currentPlayer, &currentRound, &pending, &phase); err != nil {
		return EffectCardUseResult{}, false, err
	}
	if currentPlayer != playerID {
		return EffectCardUseResult{}, false, ErrTurnPlayerMismatch
	}
	if currentRound != round || pending != 0 || phase != TurnPhaseThrowDice {
		return EffectCardUseResult{}, false, ErrActionNotReady
	}
	if _, err = tx.ExecContext(ctx, `INSERT OR IGNORE INTO game_card_turns(room_id,player_id,round,done,created_at) VALUES(?,?,?,0,?)`, roomID, playerID, round, time.Now().Unix()); err != nil {
		return EffectCardUseResult{}, false, err
	}
	var done, used, max int32
	if err = tx.QueryRowContext(ctx, `SELECT done,use_card_num,use_card_max_num FROM game_card_turns WHERE room_id=? AND player_id=? AND round=?`, roomID, playerID, round).Scan(&done, &used, &max); err != nil {
		return EffectCardUseResult{}, false, err
	}
	if done != 0 || used >= max {
		return EffectCardUseResult{}, false, ErrActionNotReady
	}
	result := EffectCardUseResult{PlayerID: playerID, Round: round, MaxHP: maxHP, UseCardNum: used, UseCardMaxNum: max}
	if cardID > 0 {
		var cardsJSON string
		if err = tx.QueryRowContext(ctx, `SELECT hp,battle_cards_json FROM players WHERE id=? AND room_id=?`, playerID, roomID).Scan(&result.OldHP, &cardsJSON); err != nil {
			return EffectCardUseResult{}, false, err
		}
		if cardsJSON != "" {
			if err = json.Unmarshal([]byte(cardsJSON), &result.Cards); err != nil {
				return EffectCardUseResult{}, false, fmt.Errorf("decode effect card hand: %w", err)
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
			return EffectCardUseResult{}, false, ErrActionNotReady
		}
		result.Cards = append(result.Cards[:cardIndex], result.Cards[cardIndex+1:]...)
		result.NewHP = result.OldHP + hpChange
		if result.NewHP > maxHP {
			result.NewHP = maxHP
		}
		if result.OldHP > maxHP {
			result.NewHP = maxHP
		}
		encodedCards, marshalErr := json.Marshal(result.Cards)
		if marshalErr != nil {
			return EffectCardUseResult{}, false, marshalErr
		}
		res, updateErr := tx.ExecContext(ctx, `UPDATE players SET hp=?,battle_cards_json=? WHERE id=? AND room_id=?`, result.NewHP, string(encodedCards), playerID, roomID)
		if updateErr != nil {
			return EffectCardUseResult{}, false, updateErr
		}
		if changed, changeErr := res.RowsAffected(); changeErr != nil || changed != 1 {
			if changeErr != nil {
				return EffectCardUseResult{}, false, changeErr
			}
			return EffectCardUseResult{}, false, ErrPlayerNotFound
		}
	}
	if upsn > 0 {
		res, insertErr := tx.ExecContext(ctx, `INSERT OR IGNORE INTO game_actions(room_id,player_id,cmd_id,upsn,payload,created_at) VALUES(?,?,?,?,?,?)`, roomID, playerID, cmd, upsn, payload, time.Now().Unix())
		if insertErr != nil {
			return EffectCardUseResult{}, false, insertErr
		}
		if inserted, rowsErr := res.RowsAffected(); rowsErr != nil || inserted == 0 {
			if rowsErr != nil {
				return EffectCardUseResult{}, false, rowsErr
			}
			return EffectCardUseResult{}, false, nil
		}
	}
	if cardID > 0 {
		result.UseCardNum++
	}
	finished := cardID == 0 || result.UseCardNum >= result.UseCardMaxNum
	doneValue := 0
	if finished {
		doneValue = 1
	}
	doneResult, err := tx.ExecContext(ctx, `UPDATE game_card_turns SET done=?,use_card_num=?,use_card_max_num=? WHERE room_id=? AND player_id=? AND round=? AND done=0`, doneValue, result.UseCardNum, result.UseCardMaxNum, roomID, playerID, round)
	if err != nil {
		return EffectCardUseResult{}, false, err
	}
	if changed, changeErr := doneResult.RowsAffected(); changeErr != nil || changed != 1 {
		if changeErr != nil {
			return EffectCardUseResult{}, false, changeErr
		}
		return EffectCardUseResult{}, false, ErrActionNotReady
	}
	if err = tx.Commit(); err != nil {
		return EffectCardUseResult{}, false, err
	}
	return result, true, nil
}

// CompleteDirectDamageEffectCardUse consumes one target effect card, updates
// the target's HP and one-shot defensive buffs, records match statistics, and
// advances the per-turn card-use count in one transaction.
func (s *Store) CompleteDirectDamageEffectCardUse(ctx context.Context, roomID, playerID, targetID int64, round int32, cmd uint16, upsn int64, payload []byte, cardID, expectedTargetHP, damage, targetMaxHP int32, expectedTargetBuffs, targetBuffs, removedBuffs []BuffState) (EffectCardDamageResult, bool, error) {
	if roomID <= 0 || playerID <= 0 || targetID <= 0 || playerID == targetID || round <= 0 || cardID <= 0 || expectedTargetHP <= 0 || damage < 0 || damage > expectedTargetHP || targetMaxHP <= 0 {
		return EffectCardDamageResult{}, false, errors.New("direct damage card action has invalid parameters")
	}
	tx, err := s.db.BeginTx(ctx, nil)
	if err != nil {
		return EffectCardDamageResult{}, false, err
	}
	defer tx.Rollback()
	if upsn > 0 {
		var existing int
		if err = tx.QueryRowContext(ctx, `SELECT COUNT(*) FROM game_actions WHERE room_id=? AND player_id=? AND upsn=?`, roomID, playerID, upsn).Scan(&existing); err != nil {
			return EffectCardDamageResult{}, false, err
		}
		if existing > 0 {
			return EffectCardDamageResult{}, false, nil
		}
	}
	var currentPlayer int64
	var currentRound, pending int32
	var phase string
	if err = tx.QueryRowContext(ctx, `SELECT current_player_id,round,pending_move,turn_phase FROM game_sessions WHERE room_id=? AND status='running'`, roomID).Scan(&currentPlayer, &currentRound, &pending, &phase); err != nil {
		return EffectCardDamageResult{}, false, err
	}
	if currentPlayer != playerID {
		return EffectCardDamageResult{}, false, ErrTurnPlayerMismatch
	}
	if currentRound != round || pending != 0 || phase != TurnPhaseThrowDice {
		return EffectCardDamageResult{}, false, ErrActionNotReady
	}
	if _, err = tx.ExecContext(ctx, `INSERT OR IGNORE INTO game_card_turns(room_id,player_id,round,done,created_at) VALUES(?,?,?,0,?)`, roomID, playerID, round, time.Now().Unix()); err != nil {
		return EffectCardDamageResult{}, false, err
	}
	result := EffectCardDamageResult{PlayerID: playerID, TargetID: targetID, Round: round, TargetMaxHP: targetMaxHP, RemovedBuffs: append([]BuffState(nil), removedBuffs...)}
	var done, used, max int32
	if err = tx.QueryRowContext(ctx, `SELECT done,use_card_num,use_card_max_num FROM game_card_turns WHERE room_id=? AND player_id=? AND round=?`, roomID, playerID, round).Scan(&done, &used, &max); err != nil {
		return EffectCardDamageResult{}, false, err
	}
	if done != 0 || used >= max || max <= 0 {
		return EffectCardDamageResult{}, false, ErrActionNotReady
	}
	var cardsJSON string
	if err = tx.QueryRowContext(ctx, `SELECT battle_cards_json FROM players WHERE id=? AND room_id=?`, playerID, roomID).Scan(&cardsJSON); err != nil {
		return EffectCardDamageResult{}, false, err
	}
	if cardsJSON != "" {
		if err = json.Unmarshal([]byte(cardsJSON), &result.Cards); err != nil {
			return EffectCardDamageResult{}, false, fmt.Errorf("decode direct damage card hand: %w", err)
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
		return EffectCardDamageResult{}, false, ErrActionNotReady
	}
	result.Cards = append(result.Cards[:cardIndex], result.Cards[cardIndex+1:]...)
	var targetBuffsJSON string
	if err = tx.QueryRowContext(ctx, `SELECT hp,battle_buffs_json FROM players WHERE id=? AND room_id=?`, targetID, roomID).Scan(&result.TargetOldHP, &targetBuffsJSON); err != nil {
		return EffectCardDamageResult{}, false, err
	}
	var currentBuffs []BuffState
	if targetBuffsJSON != "" {
		if err = json.Unmarshal([]byte(targetBuffsJSON), &currentBuffs); err != nil {
			return EffectCardDamageResult{}, false, fmt.Errorf("decode target battle buffs: %w", err)
		}
	}
	if len(currentBuffs) != len(expectedTargetBuffs) || (len(currentBuffs) > 0 && !reflect.DeepEqual(currentBuffs, expectedTargetBuffs)) || result.TargetOldHP != expectedTargetHP {
		return EffectCardDamageResult{}, false, ErrActionNotReady
	}
	result.TargetNewHP = result.TargetOldHP - damage
	updatedCards, marshalErr := json.Marshal(result.Cards)
	if marshalErr != nil {
		return EffectCardDamageResult{}, false, marshalErr
	}
	updatedBuffs, marshalErr := json.Marshal(targetBuffs)
	if marshalErr != nil {
		return EffectCardDamageResult{}, false, marshalErr
	}
	res, err := tx.ExecContext(ctx, `UPDATE players SET battle_cards_json=? WHERE id=? AND room_id=?`, string(updatedCards), playerID, roomID)
	if err != nil {
		return EffectCardDamageResult{}, false, err
	}
	if changed, changeErr := res.RowsAffected(); changeErr != nil || changed != 1 {
		if changeErr != nil {
			return EffectCardDamageResult{}, false, changeErr
		}
		return EffectCardDamageResult{}, false, ErrPlayerNotFound
	}
	res, err = tx.ExecContext(ctx, `UPDATE players SET hp=?,battle_buffs_json=? WHERE id=? AND room_id=? AND hp=? AND battle_buffs_json=?`, result.TargetNewHP, string(updatedBuffs), targetID, roomID, result.TargetOldHP, targetBuffsJSON)
	if err != nil {
		return EffectCardDamageResult{}, false, err
	}
	if changed, changeErr := res.RowsAffected(); changeErr != nil || changed != 1 {
		if changeErr != nil {
			return EffectCardDamageResult{}, false, changeErr
		}
		return EffectCardDamageResult{}, false, ErrActionNotReady
	}
	if upsn > 0 {
		res, err = tx.ExecContext(ctx, `INSERT OR IGNORE INTO game_actions(room_id,player_id,cmd_id,upsn,payload,created_at) VALUES(?,?,?,?,?,?)`, roomID, playerID, cmd, upsn, payload, time.Now().Unix())
		if err != nil {
			return EffectCardDamageResult{}, false, err
		}
		if inserted, rowsErr := res.RowsAffected(); rowsErr != nil || inserted == 0 {
			if rowsErr != nil {
				return EffectCardDamageResult{}, false, rowsErr
			}
			return EffectCardDamageResult{}, false, nil
		}
	}
	result.UseCardNum = used + 1
	result.UseCardMaxNum = max
	doneValue := 0
	if result.UseCardNum >= max {
		doneValue = 1
	}
	res, err = tx.ExecContext(ctx, `UPDATE game_card_turns SET done=?,use_card_num=?,use_card_max_num=? WHERE room_id=? AND player_id=? AND round=? AND done=0`, doneValue, result.UseCardNum, max, roomID, playerID, round)
	if err != nil {
		return EffectCardDamageResult{}, false, err
	}
	if changed, changeErr := res.RowsAffected(); changeErr != nil || changed != 1 {
		if changeErr != nil {
			return EffectCardDamageResult{}, false, changeErr
		}
		return EffectCardDamageResult{}, false, ErrActionNotReady
	}
	if damage > 0 {
		stats := []BattleStatsDelta{{PlayerID: playerID, TotalDamage: damage}, {PlayerID: targetID, TotalInjured: damage}}
		if result.TargetNewHP == 0 {
			stats[0].KillCount = 1
			stats[1].TotalDie = 1
		}
		taskProgressAt := time.Now()
		for _, delta := range stats {
			if err = incrementBattleStatsTx(ctx, tx, roomID, delta); err != nil {
				return EffectCardDamageResult{}, false, err
			}
			progress := []struct{ condition, amount int32 }{{2, delta.KillCount}, {3, delta.TotalDamage}, {4, delta.TotalDie}, {5, delta.TotalInjured}}
			for _, item := range progress {
				if item.amount > 0 {
					if err = incrementTaskCounterTx(ctx, tx, delta.PlayerID, item.condition, item.amount, taskProgressAt); err != nil {
						return EffectCardDamageResult{}, false, fmt.Errorf("increment direct damage card task progress: %w", err)
					}
				}
			}
		}
	}
	if err = tx.Commit(); err != nil {
		return EffectCardDamageResult{}, false, err
	}
	return result, true, nil
}
