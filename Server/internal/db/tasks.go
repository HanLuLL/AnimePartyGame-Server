package db

import (
	"context"
	"database/sql"
	"errors"
	"fmt"
	"sort"
	"strings"
	"time"
)

var (
	ErrTaskNotReady       = errors.New("task conditions are not met")
	ErrTaskAlreadyClaimed = errors.New("task reward was already claimed")
)

type TaskSnapshot struct {
	ProgressReward    int32
	Condition         map[int32]int32
	WeekCondition     map[int32]int32
	TaskRewardIDs     []int32
	AchieveRewardIDs  []int32
	WeekTaskRewardIDs []int32
}

type TaskRewardClaim struct {
	ID            int32
	Type          int32
	ConditionType int32
	Param         int32
	Day           int32
	Progress      int32
	Reward        map[int32]int32
}

type TaskRewardResult struct {
	ProgressReward int32
	Inventory      map[int32]int32
}

var taskLocation = time.FixedZone("UTC+8", 8*60*60)

func taskLocalTime(now time.Time) time.Time { return now.In(taskLocation) }

func TaskWeekPrefix(now time.Time) string {
	year, week := taskLocalTime(now).ISOWeek()
	return fmt.Sprintf("week:%04d-W%02d", year, week)
}

// RecordTaskLoginProgress counts distinct China-local login days and login
// days within the current ISO week. Reconnects on the same day do not inflate
// either task. The weekly liveness item is reset when a new week starts.
func (s *Store) RecordTaskLoginProgress(ctx context.Context, playerID int64, now time.Time) error {
	local := taskLocalTime(now)
	day := local.Format("20060102")
	weekPrefix := TaskWeekPrefix(local)
	tx, err := s.db.BeginTx(ctx, nil)
	if err != nil {
		return err
	}
	defer tx.Rollback()
	var weeklyRecords int
	if err = tx.QueryRowContext(ctx, `SELECT COUNT(*) FROM task_counters WHERE player_id=? AND condition_type=200 AND period_key LIKE ?`, playerID, weekPrefix+":day:%").Scan(&weeklyRecords); err != nil {
		return err
	}
	if weeklyRecords == 0 {
		if _, err = tx.ExecContext(ctx, `UPDATE inventory SET count=0,updated_at=? WHERE player_id=? AND item_id=52`, now.Unix(), playerID); err != nil {
			return err
		}
	}
	if _, err = tx.ExecContext(ctx, `INSERT OR IGNORE INTO task_counters(player_id,condition_type,period_key,progress,updated_at) VALUES(?,?,?,?,?)`,
		playerID, 15, "day:"+day, 1, now.Unix()); err != nil {
		return err
	}
	if _, err = tx.ExecContext(ctx, `INSERT OR IGNORE INTO task_counters(player_id,condition_type,period_key,progress,updated_at) VALUES(?,?,?,?,?)`,
		playerID, 200, weekPrefix+":day:"+day, 1, now.Unix()); err != nil {
		return err
	}
	return tx.Commit()
}

// IncrementTaskCounter records an authoritative server-observed gameplay
// event for lifetime and weekly task progress.
func (s *Store) IncrementTaskCounter(ctx context.Context, playerID int64, conditionType, amount int32, now time.Time) error {
	if playerID <= 0 || conditionType <= 0 || amount <= 0 {
		return errors.New("invalid task counter update")
	}
	tx, err := s.db.BeginTx(ctx, nil)
	if err != nil {
		return err
	}
	defer tx.Rollback()
	if err = incrementTaskCounterTx(ctx, tx, playerID, conditionType, amount, now); err != nil {
		return err
	}
	return tx.Commit()
}

func incrementTaskCounterTx(ctx context.Context, tx *sql.Tx, playerID int64, conditionType, amount int32, now time.Time) error {
	if playerID <= 0 || conditionType <= 0 || amount <= 0 {
		return errors.New("invalid task counter update")
	}
	weekPrefix := TaskWeekPrefix(now)
	for _, period := range []string{"all", weekPrefix + ":event"} {
		if _, err := tx.ExecContext(ctx, `INSERT INTO task_counters(player_id,condition_type,period_key,progress,updated_at) VALUES(?,?,?,?,?) ON CONFLICT(player_id,condition_type,period_key) DO UPDATE SET progress=progress+excluded.progress,updated_at=excluded.updated_at`,
			playerID, conditionType, period, amount, now.Unix()); err != nil {
			return err
		}
	}
	return nil
}

func (s *Store) MarkTutorialComplete(ctx context.Context, playerID int64, now time.Time) error {
	if playerID <= 0 {
		return errors.New("invalid player for tutorial completion")
	}
	_, err := s.db.ExecContext(ctx, `INSERT INTO task_counters(player_id,condition_type,period_key,progress,updated_at) VALUES(?,1,'all',1,?) ON CONFLICT(player_id,condition_type,period_key) DO UPDATE SET progress=1,updated_at=excluded.updated_at`, playerID, now.Unix())
	return err
}

func (s *Store) TaskSnapshot(ctx context.Context, playerID int64, now time.Time) (TaskSnapshot, error) {
	state := TaskSnapshot{
		Condition:     make(map[int32]int32),
		WeekCondition: make(map[int32]int32),
	}
	weekPrefix := TaskWeekPrefix(now)
	rows, err := s.db.QueryContext(ctx, `SELECT condition_type,period_key,progress FROM task_counters WHERE player_id=?`, playerID)
	if err != nil {
		return TaskSnapshot{}, err
	}
	for rows.Next() {
		var condition int32
		var period string
		var progress int32
		if err = rows.Scan(&condition, &period, &progress); err != nil {
			rows.Close()
			return TaskSnapshot{}, err
		}
		if condition == 15 && strings.HasPrefix(period, "day:") {
			state.Condition[condition] += progress
		} else if period == "all" {
			state.Condition[condition] += progress
		}
		if strings.HasPrefix(period, weekPrefix+":") {
			state.WeekCondition[condition] += progress
		}
	}
	if err = rows.Err(); err != nil {
		rows.Close()
		return TaskSnapshot{}, err
	}
	if err = rows.Close(); err != nil {
		return TaskSnapshot{}, err
	}
	rows, err = s.db.QueryContext(ctx, `SELECT def_id,reward_type FROM task_rewards WHERE player_id=? AND (period_key='all' OR period_key=?)`, playerID, weekPrefix)
	if err != nil {
		return TaskSnapshot{}, err
	}
	for rows.Next() {
		var id, rewardType int32
		if err = rows.Scan(&id, &rewardType); err != nil {
			rows.Close()
			return TaskSnapshot{}, err
		}
		switch rewardType {
		case 1, 5:
			state.TaskRewardIDs = append(state.TaskRewardIDs, id)
		case 2:
			state.WeekTaskRewardIDs = append(state.WeekTaskRewardIDs, id)
		case 3:
			state.AchieveRewardIDs = append(state.AchieveRewardIDs, id)
		case 4:
			state.ProgressReward |= id
		}
	}
	if err = rows.Err(); err != nil {
		rows.Close()
		return TaskSnapshot{}, err
	}
	if err = rows.Close(); err != nil {
		return TaskSnapshot{}, err
	}
	sort.Slice(state.TaskRewardIDs, func(i, j int) bool { return state.TaskRewardIDs[i] < state.TaskRewardIDs[j] })
	sort.Slice(state.WeekTaskRewardIDs, func(i, j int) bool { return state.WeekTaskRewardIDs[i] < state.WeekTaskRewardIDs[j] })
	sort.Slice(state.AchieveRewardIDs, func(i, j int) bool { return state.AchieveRewardIDs[i] < state.AchieveRewardIDs[j] })
	return state, nil
}

func (s *Store) ClaimTaskReward(ctx context.Context, playerID int64, claim TaskRewardClaim, weekPrefix string, now time.Time) (TaskRewardResult, error) {
	if playerID <= 0 || claim.ID <= 0 || claim.Type < 1 || claim.Type > 5 || weekPrefix == "" {
		return TaskRewardResult{}, errors.New("invalid task claim")
	}
	periodKey := "all"
	if claim.Type == 2 || claim.Type == 4 {
		periodKey = weekPrefix
	}
	tx, err := s.db.BeginTx(ctx, nil)
	if err != nil {
		return TaskRewardResult{}, err
	}
	defer tx.Rollback()
	var claimed int
	err = tx.QueryRowContext(ctx, `SELECT 1 FROM task_rewards WHERE player_id=? AND reward_type=? AND def_id=? AND period_key=?`, playerID, claim.Type, claim.ID, periodKey).Scan(&claimed)
	if err == nil {
		return TaskRewardResult{}, ErrTaskAlreadyClaimed
	}
	if !errors.Is(err, sql.ErrNoRows) {
		return TaskRewardResult{}, err
	}
	if claim.Type == 4 {
		var progress int32
		if err = tx.QueryRowContext(ctx, `SELECT COALESCE((SELECT count FROM inventory WHERE player_id=? AND item_id=52),0)`, playerID).Scan(&progress); err != nil {
			return TaskRewardResult{}, err
		}
		if progress < claim.Progress {
			return TaskRewardResult{}, ErrTaskNotReady
		}
	} else {
		progress, progressErr := taskProgressTx(ctx, tx, playerID, claim.ConditionType, claim.Type, weekPrefix)
		if progressErr != nil {
			return TaskRewardResult{}, progressErr
		}
		if progress < claim.Param {
			return TaskRewardResult{}, ErrTaskNotReady
		}
		if claim.Type == 5 {
			loginDays, loginErr := taskProgressTx(ctx, tx, playerID, 15, 3, weekPrefix)
			if loginErr != nil {
				return TaskRewardResult{}, loginErr
			}
			if loginDays < claim.Day {
				return TaskRewardResult{}, ErrTaskNotReady
			}
		}
	}
	if _, err = tx.ExecContext(ctx, `INSERT INTO task_rewards(player_id,reward_type,def_id,period_key,claimed_at) VALUES(?,?,?,?,?)`, playerID, claim.Type, claim.ID, periodKey, now.Unix()); err != nil {
		return TaskRewardResult{}, err
	}
	for itemID, count := range claim.Reward {
		if itemID <= 0 || count <= 0 {
			return TaskRewardResult{}, errors.New("invalid configured task reward")
		}
		if _, err = tx.ExecContext(ctx, `INSERT INTO inventory(player_id,item_id,count,updated_at) VALUES(?,?,?,?) ON CONFLICT(player_id,item_id) DO UPDATE SET count=count+excluded.count,updated_at=excluded.updated_at`, playerID, itemID, count, now.Unix()); err != nil {
			return TaskRewardResult{}, err
		}
	}
	result := TaskRewardResult{Inventory: make(map[int32]int32, len(claim.Reward))}
	for itemID := range claim.Reward {
		var count int32
		if err = tx.QueryRowContext(ctx, `SELECT count FROM inventory WHERE player_id=? AND item_id=?`, playerID, itemID).Scan(&count); err != nil {
			return TaskRewardResult{}, err
		}
		result.Inventory[itemID] = count
	}
	if claim.Type == 4 {
		rows, queryErr := tx.QueryContext(ctx, `SELECT def_id FROM task_rewards WHERE player_id=? AND reward_type=4 AND period_key=?`, playerID, weekPrefix)
		if queryErr != nil {
			return TaskRewardResult{}, queryErr
		}
		for rows.Next() {
			var id int32
			if err = rows.Scan(&id); err != nil {
				rows.Close()
				return TaskRewardResult{}, err
			}
			result.ProgressReward |= id
		}
		if err = rows.Err(); err != nil {
			rows.Close()
			return TaskRewardResult{}, err
		}
		if err = rows.Close(); err != nil {
			return TaskRewardResult{}, err
		}
	}
	if err = tx.Commit(); err != nil {
		return TaskRewardResult{}, err
	}
	return result, nil
}

func taskProgressTx(ctx context.Context, tx *sql.Tx, playerID int64, conditionType int32, taskType int32, weekPrefix string) (int32, error) {
	if taskType == 2 {
		var progress int32
		err := tx.QueryRowContext(ctx, `SELECT COALESCE(SUM(progress),0) FROM task_counters WHERE player_id=? AND condition_type=? AND period_key LIKE ?`, playerID, conditionType, weekPrefix+":%").Scan(&progress)
		return progress, err
	}
	if conditionType == 15 {
		var progress int32
		err := tx.QueryRowContext(ctx, `SELECT COALESCE(SUM(progress),0) FROM task_counters WHERE player_id=? AND condition_type=15 AND period_key LIKE 'day:%'`, playerID).Scan(&progress)
		return progress, err
	}
	var progress int32
	err := tx.QueryRowContext(ctx, `SELECT COALESCE(progress,0) FROM task_counters WHERE player_id=? AND condition_type=? AND period_key='all'`, playerID, conditionType).Scan(&progress)
	if errors.Is(err, sql.ErrNoRows) {
		return 0, nil
	}
	return progress, err
}
