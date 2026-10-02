package db

import (
	"context"
	"database/sql"
	"encoding/json"
	"errors"
	"fmt"
	"time"
)

// BattleRoleState stores the server-authoritative values used by the client's
// Battle snapshot. Base attributes come from character resources; dice and
// card bonuses are added by the battle action handlers.
type BattleRoleState struct {
	PlayerID          int64             `json:"playerId"`
	HeroID            int32             `json:"heroId"`
	Atk               int32             `json:"atk"`
	Def               int32             `json:"def"`
	Cost              int32             `json:"cost"`
	MaxCost           int32             `json:"maxCost"`
	UseCards          []int32           `json:"useCards,omitempty"`
	Point             int32             `json:"point"`
	Dodge             bool              `json:"dodge"`
	IncHP             int32             `json:"incHp"`
	DropGold          int32             `json:"dropGold"`
	CanNotFightBack   bool              `json:"canNotFightBack"`
	AttackBonus       int32             `json:"attackBonus"`
	ChainAttacker     int64             `json:"chainAttacker"`
	ChainAttackDamage int32             `json:"chainAttackDamage"`
	MaxAtk            int32             `json:"maxAtk"`
	MaxDef            int32             `json:"maxDef"`
	MinAtk            int32             `json:"minAtk"`
	MinDef            int32             `json:"minDef"`
	InitAtk           int32             `json:"initAtk"`
	InitDef           int32             `json:"initDef"`
	CardCombatBonus   []BattleCardBonus `json:"cardCombatBonus,omitempty"`
}

type BattleCardBonus struct {
	CardID int32 `json:"cardId"`
	Bonus  int32 `json:"bonus"`
}

type BattleState struct {
	BattleID     int64           `json:"battleId"`
	Attacker     BattleRoleState `json:"attacker"`
	Defender     BattleRoleState `json:"defender"`
	CardUseState map[int64]bool  `json:"cardUseState"`
	Stage        string          `json:"stage"`
	IsEnd        bool            `json:"isEnd"`
	FightBack    bool            `json:"fightBack"`
	IsPursuit    bool            `json:"isPursuit"`
}

type BattleCardsUpdate struct {
	PlayerID int64
	Cards    []CardState
}

type BattleBuffUpdate struct {
	PlayerID int64
	Buffs    []BuffState
}

type BattleHPUpdate struct {
	PlayerID int64
	NewHP    int32
}

type BattleActionResult struct {
	NextPlayer int64
	Round      int32
	Completed  bool
}

func incrementBattleStatsTx(ctx context.Context, tx *sql.Tx, roomID int64, delta BattleStatsDelta) error {
	if delta.PlayerID <= 0 || delta.KillCount < 0 || delta.TotalDie < 0 || delta.TotalDamage < 0 || delta.TotalInjured < 0 || delta.TreatmentScore < 0 {
		return errors.New("invalid match stats delta")
	}
	_, err := tx.ExecContext(ctx, `INSERT INTO game_player_stats(room_id,player_id,kill_count,total_die,total_damage,total_injured,treatment_score) VALUES(?,?,?,?,?,?,?) ON CONFLICT(room_id,player_id) DO UPDATE SET kill_count=kill_count+excluded.kill_count,total_die=total_die+excluded.total_die,total_damage=total_damage+excluded.total_damage,total_injured=total_injured+excluded.total_injured,treatment_score=treatment_score+excluded.treatment_score`, roomID, delta.PlayerID, delta.KillCount, delta.TotalDie, delta.TotalDamage, delta.TotalInjured, delta.TreatmentScore)
	return err
}

func recordMatchHPChangeTx(ctx context.Context, tx *sql.Tx, roomID, playerID int64, oldHP, newHP int32) error {
	delta := BattleStatsDelta{PlayerID: playerID}
	if newHP < oldHP {
		delta.TotalInjured = oldHP - newHP
		if oldHP > 0 && newHP == 0 {
			delta.TotalDie = 1
		}
	} else if newHP > oldHP {
		delta.TreatmentScore = newHP - oldHP
	}
	if delta.TotalInjured == 0 && delta.TotalDie == 0 && delta.TreatmentScore == 0 {
		return nil
	}
	return incrementBattleStatsTx(ctx, tx, roomID, delta)
}

// ApplyBattleAction commits a stage transition, card hand change, optional HP
// updates and UPSN record in one SQLite transaction. The room turn advances
// only after the battle state is marked complete.
func (s *Store) ApplyBattleAction(ctx context.Context, roomID, playerID int64, cmd uint16, upsn int64, payload []byte, expectedPhase string, state *BattleState, cards *BattleCardsUpdate, hp []BattleHPUpdate, completed bool, stats []BattleStatsDelta, buffs ...BattleBuffUpdate) (BattleActionResult, bool, error) {
	tx, err := s.db.BeginTx(ctx, nil)
	if err != nil {
		return BattleActionResult{}, false, err
	}
	defer tx.Rollback()
	if upsn > 0 {
		var existing int
		if err = tx.QueryRowContext(ctx, `SELECT COUNT(*) FROM game_actions WHERE room_id=? AND player_id=? AND upsn=?`, roomID, playerID, upsn).Scan(&existing); err != nil {
			return BattleActionResult{}, false, err
		}
		if existing > 0 {
			return BattleActionResult{}, false, nil
		}
	}
	var currentPlayer int64
	var result BattleActionResult
	var pending int32
	var phase, battleJSON string
	if err = tx.QueryRowContext(ctx, `SELECT current_player_id,round,pending_move,turn_phase FROM game_sessions WHERE room_id=? AND status='running'`, roomID).Scan(&currentPlayer, &result.Round, &pending, &phase); err != nil {
		return BattleActionResult{}, false, err
	}
	if pending != 0 || phase != expectedPhase {
		return BattleActionResult{}, false, ErrActionNotReady
	}
	if err = tx.QueryRowContext(ctx, `SELECT state_json FROM game_battles WHERE room_id=?`, roomID).Scan(&battleJSON); err != nil {
		if errors.Is(err, sql.ErrNoRows) {
			return BattleActionResult{}, false, ErrActionNotReady
		}
		return BattleActionResult{}, false, err
	}
	var oldState BattleState
	if err = json.Unmarshal([]byte(battleJSON), &oldState); err != nil {
		return BattleActionResult{}, false, fmt.Errorf("decode battle state: %w", err)
	}
	if oldState.Stage != expectedPhase {
		return BattleActionResult{}, false, ErrActionNotReady
	}
	if currentPlayer != oldState.Attacker.PlayerID {
		return BattleActionResult{}, false, ErrActionNotReady
	}
	if playerID != oldState.Attacker.PlayerID && playerID != oldState.Defender.PlayerID {
		return BattleActionResult{}, false, ErrActionNotReady
	}
	if cards != nil && cards.PlayerID != playerID {
		return BattleActionResult{}, false, errors.New("battle hand update does not belong to the acting player")
	}
	if state != nil && (state.BattleID != oldState.BattleID || state.Attacker.PlayerID != oldState.Attacker.PlayerID || state.Defender.PlayerID != oldState.Defender.PlayerID) {
		return BattleActionResult{}, false, errors.New("invalid battle state update")
	}
	if completed && expectedPhase != TurnPhaseBattleChallenge && expectedPhase != TurnPhaseBattleDefense {
		return BattleActionResult{}, false, errors.New("battle cannot complete from the current phase")
	}
	if !completed {
		validStage := false
		switch expectedPhase {
		case TurnPhaseBattleChallenge:
			validStage = state != nil && state.Stage == TurnPhaseBattleCards
		case TurnPhaseBattleCards:
			validStage = state != nil && (state.Stage == TurnPhaseBattleCards || state.Stage == TurnPhaseBattleAttack)
		case TurnPhaseBattleAttack:
			validStage = state != nil && state.Stage == TurnPhaseBattleDefense
		}
		if !validStage {
			return BattleActionResult{}, false, errors.New("invalid battle phase transition")
		}
	}
	if upsn > 0 {
		res, insertErr := tx.ExecContext(ctx, `INSERT OR IGNORE INTO game_actions(room_id,player_id,cmd_id,upsn,payload,created_at) VALUES(?,?,?,?,?,?)`, roomID, playerID, cmd, upsn, payload, time.Now().Unix())
		if insertErr != nil {
			return BattleActionResult{}, false, insertErr
		}
		inserted, insertErr := res.RowsAffected()
		if insertErr != nil {
			return BattleActionResult{}, false, insertErr
		}
		if inserted == 0 {
			return BattleActionResult{}, false, nil
		}
	}
	if cards != nil {
		encoded, marshalErr := json.Marshal(cards.Cards)
		if marshalErr != nil {
			return BattleActionResult{}, false, marshalErr
		}
		res, updateErr := tx.ExecContext(ctx, `UPDATE players SET battle_cards_json=? WHERE id=? AND room_id=?`, string(encoded), cards.PlayerID, roomID)
		if updateErr != nil {
			return BattleActionResult{}, false, updateErr
		}
		if changed, changeErr := res.RowsAffected(); changeErr != nil || changed != 1 {
			if changeErr != nil {
				return BattleActionResult{}, false, changeErr
			}
			return BattleActionResult{}, false, ErrPlayerNotFound
		}
	}
	if len(buffs) > 2 {
		return BattleActionResult{}, false, errors.New("battle action contains too many buff updates")
	}
	updatedBuffPlayers := make(map[int64]struct{}, len(buffs))
	for _, update := range buffs {
		if update.PlayerID != oldState.Attacker.PlayerID && update.PlayerID != oldState.Defender.PlayerID {
			return BattleActionResult{}, false, errors.New("battle buff update does not belong to a battle participant")
		}
		if _, exists := updatedBuffPlayers[update.PlayerID]; exists {
			return BattleActionResult{}, false, errors.New("battle action contains duplicate buff updates")
		}
		updatedBuffPlayers[update.PlayerID] = struct{}{}
		encoded, marshalErr := json.Marshal(update.Buffs)
		if marshalErr != nil {
			return BattleActionResult{}, false, marshalErr
		}
		res, updateErr := tx.ExecContext(ctx, `UPDATE players SET battle_buffs_json=? WHERE id=? AND room_id=?`, string(encoded), update.PlayerID, roomID)
		if updateErr != nil {
			return BattleActionResult{}, false, updateErr
		}
		if changed, changeErr := res.RowsAffected(); changeErr != nil || changed != 1 {
			if changeErr != nil {
				return BattleActionResult{}, false, changeErr
			}
			return BattleActionResult{}, false, ErrPlayerNotFound
		}
	}
	for _, update := range hp {
		if update.PlayerID != oldState.Attacker.PlayerID && update.PlayerID != oldState.Defender.PlayerID {
			return BattleActionResult{}, false, errors.New("battle HP update is outside the battle")
		}
		res, updateErr := tx.ExecContext(ctx, `UPDATE players SET hp=? WHERE id=? AND room_id=?`, update.NewHP, update.PlayerID, roomID)
		if updateErr != nil {
			return BattleActionResult{}, false, updateErr
		}
		if changed, changeErr := res.RowsAffected(); changeErr != nil || changed != 1 {
			if changeErr != nil {
				return BattleActionResult{}, false, changeErr
			}
			return BattleActionResult{}, false, ErrPlayerNotFound
		}
	}
	taskProgressAt := time.Now()
	for _, delta := range stats {
		if delta.PlayerID != oldState.Attacker.PlayerID && delta.PlayerID != oldState.Defender.PlayerID {
			return BattleActionResult{}, false, errors.New("battle stats update is outside the battle")
		}
		if err = incrementBattleStatsTx(ctx, tx, roomID, delta); err != nil {
			return BattleActionResult{}, false, err
		}
		taskProgress := []struct {
			condition int32
			amount    int32
		}{
			{condition: 2, amount: delta.KillCount},
			{condition: 3, amount: delta.TotalDamage},
			{condition: 4, amount: delta.TotalDie},
			{condition: 5, amount: delta.TotalInjured},
		}
		for _, progress := range taskProgress {
			if progress.amount == 0 {
				continue
			}
			if err = incrementTaskCounterTx(ctx, tx, delta.PlayerID, progress.condition, progress.amount, taskProgressAt); err != nil {
				return BattleActionResult{}, false, fmt.Errorf("increment battle task progress player=%d condition=%d: %w", delta.PlayerID, progress.condition, err)
			}
		}
	}
	if completed {
		members, queryErr := tx.QueryContext(ctx, `SELECT player_id FROM room_members WHERE room_id=? ORDER BY slot`, roomID)
		if queryErr != nil {
			return BattleActionResult{}, false, queryErr
		}
		var ids []int64
		for members.Next() {
			var id int64
			if err = members.Scan(&id); err != nil {
				members.Close()
				return BattleActionResult{}, false, err
			}
			ids = append(ids, id)
		}
		if err = members.Err(); err != nil {
			members.Close()
			return BattleActionResult{}, false, err
		}
		members.Close()
		index := -1
		for i, id := range ids {
			if id == currentPlayer {
				index = i
				break
			}
		}
		if index < 0 || len(ids) == 0 {
			return BattleActionResult{}, false, errors.New("battle owner is not in the room")
		}
		result.NextPlayer = ids[(index+1)%len(ids)]
		if index == len(ids)-1 {
			result.Round++
		}
		res, updateErr := tx.ExecContext(ctx, `UPDATE game_sessions SET current_player_id=?,round=?,pending_move=0,turn_phase=? WHERE room_id=? AND status='running' AND current_player_id=? AND pending_move=0 AND turn_phase=?`, result.NextPlayer, result.Round, TurnPhaseThrowDice, roomID, currentPlayer, expectedPhase)
		if updateErr != nil {
			return BattleActionResult{}, false, updateErr
		}
		if changed, changeErr := res.RowsAffected(); changeErr != nil || changed != 1 {
			if changeErr != nil {
				return BattleActionResult{}, false, changeErr
			}
			return BattleActionResult{}, false, ErrActionNotReady
		}
		if _, err = tx.ExecContext(ctx, `DELETE FROM game_battles WHERE room_id=?`, roomID); err != nil {
			return BattleActionResult{}, false, err
		}
		result.Completed = true
	} else {
		if state == nil || state.Stage == "" {
			return BattleActionResult{}, false, errors.New("battle state update is missing a stage")
		}
		encoded, marshalErr := json.Marshal(state)
		if marshalErr != nil {
			return BattleActionResult{}, false, marshalErr
		}
		if _, err = tx.ExecContext(ctx, `UPDATE game_battles SET state_json=?,updated_at=? WHERE room_id=?`, string(encoded), time.Now().Unix(), roomID); err != nil {
			return BattleActionResult{}, false, err
		}
		res, updateErr := tx.ExecContext(ctx, `UPDATE game_sessions SET turn_phase=? WHERE room_id=? AND status='running' AND current_player_id=? AND pending_move=0 AND turn_phase=?`, state.Stage, roomID, currentPlayer, expectedPhase)
		if updateErr != nil {
			return BattleActionResult{}, false, updateErr
		}
		if changed, changeErr := res.RowsAffected(); changeErr != nil || changed != 1 {
			if changeErr != nil {
				return BattleActionResult{}, false, changeErr
			}
			return BattleActionResult{}, false, ErrActionNotReady
		}
	}
	if err = tx.Commit(); err != nil {
		return BattleActionResult{}, false, err
	}
	return result, true, nil
}

func (s *Store) GetBattlePlayerStats(ctx context.Context, roomID, playerID int64) (BattlePlayerStats, error) {
	var stats BattlePlayerStats
	err := s.db.QueryRowContext(ctx, `SELECT kill_count,total_die,total_damage,total_injured,treatment_score FROM game_player_stats WHERE room_id=? AND player_id=?`, roomID, playerID).Scan(&stats.KillCount, &stats.TotalDie, &stats.TotalDamage, &stats.TotalInjured, &stats.TreatmentScore)
	if errors.Is(err, sql.ErrNoRows) {
		return BattlePlayerStats{}, nil
	}
	return stats, err
}
