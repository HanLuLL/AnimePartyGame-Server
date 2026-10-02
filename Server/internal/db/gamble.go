package db

import (
	"context"
	"database/sql"
	"encoding/json"
	"errors"
	"fmt"
	"time"
)

// GambleState is the durable room state for the multiplayer Gamble land.
// Roles remain in room-slot order so pot remainders have a stable recipient.
type GambleState struct {
	BaseGold int32        `json:"baseGold"`
	BetGold  int32        `json:"betGold"`
	Roles    []GambleRole `json:"roles"`
	S        int32        `json:"s"`
	IsOdd    bool         `json:"isOdd"`
	LandID   int32        `json:"landId"`
}

type GambleRole struct {
	PlayerID   int64 `json:"playerId"`
	HeroID     int32 `json:"heroId"`
	IsDie      bool  `json:"isDie"`
	GoldLack   bool  `json:"goldLack"`
	BetGold    int32 `json:"betGold"`
	GuessCode  int32 `json:"guessCode"`
	Point      int32 `json:"point"`
	GoldChange int32 `json:"goldChange"`
}

type GambleGoldChange struct {
	PlayerID int64
	OldGold  int32
	NewGold  int32
}

type GambleActionResult struct {
	State      GambleState
	Phase      string
	Completed  bool
	NextPlayer int64
	Round      int32
	Gold       []GambleGoldChange
}

// GambleSnapshot restores the current or last resolved hall from SQLite.
func (s *Store) GambleSnapshot(ctx context.Context, roomID int64) (GambleState, bool, error) {
	var encoded string
	err := s.db.QueryRowContext(ctx, `SELECT state_json FROM game_gambles WHERE room_id=?`, roomID).Scan(&encoded)
	if errors.Is(err, sql.ErrNoRows) {
		return GambleState{}, false, nil
	}
	if err != nil {
		return GambleState{}, false, err
	}
	var state GambleState
	if err = json.Unmarshal([]byte(encoded), &state); err != nil {
		return GambleState{}, false, fmt.Errorf("decode Gamble state for room %d: %w", roomID, err)
	}
	return state, true, nil
}

// ApplyGambleAction records a guess or a server-rolled die. The shared state,
// wager, payout, action UPSN, and turn transition commit in one transaction.
func (s *Store) ApplyGambleAction(ctx context.Context, roomID, playerID int64, cmd uint16, upsn int64, payload []byte, action, value int32) (GambleActionResult, bool, error) {
	tx, err := s.db.BeginTx(ctx, nil)
	if err != nil {
		return GambleActionResult{}, false, err
	}
	defer tx.Rollback()
	if upsn > 0 {
		var existing int
		if err = tx.QueryRowContext(ctx, `SELECT COUNT(*) FROM game_actions WHERE room_id=? AND player_id=? AND upsn=?`, roomID, playerID, upsn).Scan(&existing); err != nil {
			return GambleActionResult{}, false, err
		}
		if existing > 0 {
			return GambleActionResult{}, false, nil
		}
	}
	var currentPlayer int64
	var round, pending int32
	var phase string
	if err = tx.QueryRowContext(ctx, `SELECT current_player_id,round,pending_move,turn_phase FROM game_sessions WHERE room_id=? AND status='running'`, roomID).Scan(&currentPlayer, &round, &pending, &phase); err != nil {
		return GambleActionResult{}, false, err
	}
	if pending != 0 || (phase != TurnPhaseGambleGuess && phase != TurnPhaseGambleThrow) {
		return GambleActionResult{}, false, ErrActionNotReady
	}
	if action == 1 && phase != TurnPhaseGambleGuess || action == 2 && phase != TurnPhaseGambleThrow || action < 1 || action > 2 {
		return GambleActionResult{}, false, ErrActionNotReady
	}
	var encoded string
	if err = tx.QueryRowContext(ctx, `SELECT state_json FROM game_gambles WHERE room_id=?`, roomID).Scan(&encoded); err != nil {
		return GambleActionResult{}, false, ErrActionNotReady
	}
	var state GambleState
	if err = json.Unmarshal([]byte(encoded), &state); err != nil {
		return GambleActionResult{}, false, fmt.Errorf("decode Gamble state: %w", err)
	}
	roleIndex := -1
	for index := range state.Roles {
		if state.Roles[index].PlayerID == playerID {
			roleIndex = index
			break
		}
	}
	if roleIndex < 0 {
		return GambleActionResult{}, false, ErrActionNotReady
	}
	role := &state.Roles[roleIndex]
	if role.IsDie || role.GoldLack {
		return GambleActionResult{}, false, ErrActionNotReady
	}
	result := GambleActionResult{State: state, Phase: phase, Round: round}
	if action == 1 {
		if value != 1 && value != 2 || role.GuessCode != 0 {
			return GambleActionResult{}, false, ErrActionNotReady
		}
		var oldGold int32
		if err = tx.QueryRowContext(ctx, `SELECT gold FROM players WHERE id=? AND room_id=?`, playerID, roomID).Scan(&oldGold); err != nil {
			return GambleActionResult{}, false, err
		}
		if oldGold < state.BetGold {
			return GambleActionResult{}, false, ErrActionNotReady
		}
		if _, err = tx.ExecContext(ctx, `UPDATE players SET gold=gold-? WHERE id=? AND room_id=? AND gold>=?`, state.BetGold, playerID, roomID, state.BetGold); err != nil {
			return GambleActionResult{}, false, err
		}
		role.GuessCode, role.BetGold = value, state.BetGold
		result.Gold = append(result.Gold, GambleGoldChange{PlayerID: playerID, OldGold: oldGold, NewGold: oldGold - state.BetGold})
		allGuessed := true
		for _, candidate := range state.Roles {
			if !candidate.IsDie && !candidate.GoldLack && candidate.GuessCode == 0 {
				allGuessed = false
				break
			}
		}
		if allGuessed {
			state.S = 1
			result.Phase = TurnPhaseGambleThrow
		}
	} else {
		if value < 1 || value > 6 || role.GuessCode == 0 || role.Point != 0 {
			return GambleActionResult{}, false, ErrActionNotReady
		}
		role.Point = value
		allThrown := true
		for _, candidate := range state.Roles {
			if candidate.GuessCode != 0 && candidate.Point == 0 {
				allThrown = false
				break
			}
		}
		if allThrown {
			var totalPoint, pot int32
			pot = state.BaseGold
			for _, candidate := range state.Roles {
				totalPoint += candidate.Point
				pot += candidate.BetGold
			}
			state.IsOdd = totalPoint%2 != 0
			state.S = 2
			winners := make([]int, 0, len(state.Roles))
			for index := range state.Roles {
				winner := state.Roles[index].GuessCode == 1 && state.IsOdd || state.Roles[index].GuessCode == 2 && !state.IsOdd
				if winner {
					winners = append(winners, index)
				}
			}
			if len(winners) > 0 {
				share, remainder := pot/int32(len(winners)), pot%int32(len(winners))
				for order, index := range winners {
					payout := share
					if int32(order) < remainder {
						payout++
					}
					winner := &state.Roles[index]
					var oldGold int32
					if err = tx.QueryRowContext(ctx, `SELECT gold FROM players WHERE id=? AND room_id=?`, winner.PlayerID, roomID).Scan(&oldGold); err != nil {
						return GambleActionResult{}, false, err
					}
					newGold := int64(oldGold) + int64(payout)
					if newGold > int64(1<<31-1) {
						newGold = int64(1<<31 - 1)
					}
					if _, err = tx.ExecContext(ctx, `UPDATE players SET gold=? WHERE id=? AND room_id=?`, newGold, winner.PlayerID, roomID); err != nil {
						return GambleActionResult{}, false, err
					}
					winner.GoldChange = payout
					result.Gold = append(result.Gold, GambleGoldChange{PlayerID: winner.PlayerID, OldGold: oldGold, NewGold: int32(newGold)})
				}
			}
			members, queryErr := tx.QueryContext(ctx, `SELECT player_id FROM room_members WHERE room_id=? ORDER BY slot`, roomID)
			if queryErr != nil {
				return GambleActionResult{}, false, queryErr
			}
			var ids []int64
			for members.Next() {
				var id int64
				if err = members.Scan(&id); err != nil {
					members.Close()
					return GambleActionResult{}, false, err
				}
				ids = append(ids, id)
			}
			if err = members.Err(); err != nil {
				members.Close()
				return GambleActionResult{}, false, err
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
				return GambleActionResult{}, false, errors.New("Gamble owner is not in the room")
			}
			result.NextPlayer = ids[(index+1)%len(ids)]
			if index == len(ids)-1 {
				result.Round++
			}
			result.Completed = true
		}
	}
	if upsn > 0 {
		res, insertErr := tx.ExecContext(ctx, `INSERT OR IGNORE INTO game_actions(room_id,player_id,cmd_id,upsn,payload,created_at) VALUES(?,?,?,?,?,?)`, roomID, playerID, cmd, upsn, payload, time.Now().Unix())
		if insertErr != nil {
			return GambleActionResult{}, false, insertErr
		}
		inserted, insertErr := res.RowsAffected()
		if insertErr != nil {
			return GambleActionResult{}, false, insertErr
		}
		if inserted == 0 {
			return GambleActionResult{}, false, nil
		}
	}
	stateJSON, err := json.Marshal(state)
	if err != nil {
		return GambleActionResult{}, false, err
	}
	if _, err = tx.ExecContext(ctx, `UPDATE game_gambles SET state_json=?,updated_at=? WHERE room_id=?`, string(stateJSON), time.Now().Unix(), roomID); err != nil {
		return GambleActionResult{}, false, err
	}
	if result.Completed {
		_, err = tx.ExecContext(ctx, `UPDATE game_sessions SET current_player_id=?,round=?,pending_move=0,turn_phase=? WHERE room_id=? AND status='running' AND current_player_id=? AND pending_move=0 AND turn_phase=?`, result.NextPlayer, result.Round, TurnPhaseThrowDice, roomID, currentPlayer, TurnPhaseGambleThrow)
	} else if result.Phase != phase {
		_, err = tx.ExecContext(ctx, `UPDATE game_sessions SET turn_phase=? WHERE room_id=? AND status='running' AND current_player_id=? AND pending_move=0 AND turn_phase=?`, result.Phase, roomID, currentPlayer, phase)
	}
	if err != nil {
		return GambleActionResult{}, false, err
	}
	if err = tx.Commit(); err != nil {
		return GambleActionResult{}, false, err
	}
	result.State = state
	return result, true, nil
}
