package db

import (
	"context"
	"database/sql"
	"errors"
	"time"
)

var (
	ErrSignInNotReady = errors.New("sign-in is not ready")
	ErrSignInComplete = errors.New("sign-in schedule is complete")
)

type SignInRewardState struct {
	ActivityID  int32
	SignInCount int32
	UpdateTime  int64
}

type SignInClaimResult struct {
	State     SignInRewardState
	Inventory map[int32]int32
}

func (s *Store) SignInRewardsForPlayer(ctx context.Context, playerID int64, activeIDs []int32) (map[int32]SignInRewardState, error) {
	if playerID <= 0 {
		return nil, errors.New("invalid sign-in player")
	}
	tx, err := s.db.BeginTx(ctx, nil)
	if err != nil {
		return nil, err
	}
	defer tx.Rollback()
	for _, activityID := range activeIDs {
		if activityID <= 0 {
			continue
		}
		if _, err = tx.ExecContext(ctx, `INSERT OR IGNORE INTO player_sign_in(player_id,activity_id,sign_in_count,update_time) VALUES(?,?,0,0)`, playerID, activityID); err != nil {
			return nil, err
		}
	}
	rows, err := tx.QueryContext(ctx, `SELECT activity_id,sign_in_count,update_time FROM player_sign_in WHERE player_id=? ORDER BY activity_id`, playerID)
	if err != nil {
		return nil, err
	}
	result := make(map[int32]SignInRewardState)
	for rows.Next() {
		var state SignInRewardState
		if err = rows.Scan(&state.ActivityID, &state.SignInCount, &state.UpdateTime); err != nil {
			rows.Close()
			return nil, err
		}
		result[state.ActivityID] = state
	}
	if err = rows.Err(); err != nil {
		rows.Close()
		return nil, err
	}
	if err = rows.Close(); err != nil {
		return nil, err
	}
	if err = tx.Commit(); err != nil {
		return nil, err
	}
	return result, nil
}

func (s *Store) ClaimSignInReward(ctx context.Context, playerID int64, activityID int32, days map[int32]map[int32]int32, now time.Time) (SignInClaimResult, error) {
	if playerID <= 0 || activityID <= 0 || len(days) < 7 {
		return SignInClaimResult{}, errors.New("invalid sign-in claim")
	}
	for day := int32(1); day <= 7; day++ {
		if len(days[day]) == 0 {
			return SignInClaimResult{}, errors.New("incomplete sign-in schedule")
		}
		for itemID, count := range days[day] {
			if itemID <= 0 || count <= 0 {
				return SignInClaimResult{}, errors.New("invalid configured sign-in reward")
			}
		}
	}
	tx, err := s.db.BeginTx(ctx, nil)
	if err != nil {
		return SignInClaimResult{}, err
	}
	defer tx.Rollback()
	var count int32
	var updatedAt int64
	err = tx.QueryRowContext(ctx, `SELECT sign_in_count,update_time FROM player_sign_in WHERE player_id=? AND activity_id=?`, playerID, activityID).Scan(&count, &updatedAt)
	if errors.Is(err, sql.ErrNoRows) {
		if _, err = tx.ExecContext(ctx, `INSERT INTO player_sign_in(player_id,activity_id,sign_in_count,update_time) VALUES(?,?,0,0)`, playerID, activityID); err != nil {
			return SignInClaimResult{}, err
		}
		count, updatedAt = 0, 0
	} else if err != nil {
		return SignInClaimResult{}, err
	}
	if count >= 7 {
		return SignInClaimResult{}, ErrSignInComplete
	}
	if count < 0 {
		return SignInClaimResult{}, errors.New("invalid persisted sign-in count")
	}
	if count > 0 && !signInResetPassed(updatedAt, now) {
		return SignInClaimResult{}, ErrSignInNotReady
	}
	day := count + 1
	rewards := days[day]
	updatedAt = now.Unix()
	count++
	updated, err := tx.ExecContext(ctx, `UPDATE player_sign_in SET sign_in_count=?,update_time=? WHERE player_id=? AND activity_id=? AND sign_in_count=?`, count, updatedAt, playerID, activityID, count-1)
	if err != nil {
		return SignInClaimResult{}, err
	}
	if affected, rowsErr := updated.RowsAffected(); rowsErr != nil {
		return SignInClaimResult{}, rowsErr
	} else if affected != 1 {
		return SignInClaimResult{}, ErrSignInNotReady
	}
	for itemID, itemCount := range rewards {
		if _, err = tx.ExecContext(ctx, `INSERT INTO inventory(player_id,item_id,count,updated_at) VALUES(?,?,?,?) ON CONFLICT(player_id,item_id) DO UPDATE SET count=count+excluded.count,updated_at=excluded.updated_at`, playerID, itemID, itemCount, updatedAt); err != nil {
			return SignInClaimResult{}, err
		}
	}
	inventory := make(map[int32]int32, len(rewards))
	for itemID := range rewards {
		var current int32
		if err = tx.QueryRowContext(ctx, `SELECT count FROM inventory WHERE player_id=? AND item_id=?`, playerID, itemID).Scan(&current); err != nil {
			return SignInClaimResult{}, err
		}
		inventory[itemID] = current
	}
	if err = tx.Commit(); err != nil {
		return SignInClaimResult{}, err
	}
	return SignInClaimResult{
		State:     SignInRewardState{ActivityID: activityID, SignInCount: count, UpdateTime: updatedAt},
		Inventory: inventory,
	}, nil
}

func signInResetPassed(lastSignIn int64, now time.Time) bool {
	zone := time.FixedZone("Asia/Shanghai", 8*60*60)
	last := time.Unix(lastSignIn, 0).In(zone)
	next := time.Date(last.Year(), last.Month(), last.Day(), 4, 0, 0, 0, zone)
	if !last.Before(next) {
		next = next.AddDate(0, 0, 1)
	}
	return !now.In(zone).Before(next)
}
