package db

import (
	"context"
	"database/sql"
	"encoding/json"
	"errors"
	"sort"
	"time"
)

var (
	ErrGachaActionConflict = errors.New("gacha action sequence was already used")
	ErrGachaRewardLocked   = errors.New("gacha progress reward is not eligible")
)

type GachaDrop struct {
	ItemID    int32       `json:"itemId"`
	Count     int32       `json:"count"`
	Transform []GachaDrop `json:"transform,omitempty"`
	Converted bool        `json:"converted,omitempty"`
}

type GachaHistoryRecord struct {
	ItemID    int32
	ItemCount int32
	IsConvert bool
	Time      int64
}

type GachaDraw struct {
	Progress  GachaProgress
	Rewards   []GachaDrop
	Bonus     []GachaDrop
	Inventory map[int32]int32
}

type GachaMilestone struct {
	Count   int32
	Rewards []GachaDrop
}

func (s *Store) CommitGachaDraw(ctx context.Context, playerID int64, cmd uint16, upsn int64,
	payload []byte, gachaID, poolID, pullCount int32, costs map[int32]int32,
	rewards, bonus []GachaDrop) (GachaDraw, error) {
	if playerID <= 0 || gachaID <= 0 || poolID <= 0 || pullCount <= 0 || len(costs) == 0 || len(rewards) == 0 {
		return GachaDraw{}, ErrPveProgressInvalid
	}
	for itemID, count := range costs {
		if itemID <= 0 || count <= 0 {
			return GachaDraw{}, ErrPveProgressInvalid
		}
	}
	for _, reward := range rewards {
		if reward.ItemID <= 0 || reward.Count <= 0 {
			return GachaDraw{}, ErrPveProgressInvalid
		}
		for _, transform := range reward.Transform {
			if transform.ItemID <= 0 || transform.Count <= 0 {
				return GachaDraw{}, ErrPveProgressInvalid
			}
		}
	}
	for _, reward := range bonus {
		if reward.ItemID <= 0 || reward.Count <= 0 {
			return GachaDraw{}, ErrPveProgressInvalid
		}
	}

	tx, err := s.db.BeginTx(ctx, nil)
	if err != nil {
		return GachaDraw{}, err
	}
	defer tx.Rollback()

	if upsn > 0 {
		var prior GachaDraw
		var priorGachaID, priorPoolID, priorCount, priorProgressCount int32
		var priorCmd int
		var costsJSON, rewardsJSON, bonusJSON string
		err = tx.QueryRowContext(ctx, `SELECT cmd_id,gacha_id,pool_id,count,progress_count,costs_json,rewards_json,bonus_json FROM gacha_draws WHERE player_id=? AND upsn=?`, playerID, upsn).
			Scan(&priorCmd, &priorGachaID, &priorPoolID, &priorCount, &priorProgressCount, &costsJSON, &rewardsJSON, &bonusJSON)
		if err == nil {
			if priorCmd != int(cmd) || priorGachaID != gachaID || priorPoolID != poolID || priorCount != pullCount {
				return GachaDraw{}, ErrGachaActionConflict
			}
			if err = json.Unmarshal([]byte(rewardsJSON), &prior.Rewards); err != nil {
				return GachaDraw{}, err
			}
			if err = json.Unmarshal([]byte(bonusJSON), &prior.Bonus); err != nil {
				return GachaDraw{}, err
			}
			var priorCosts map[int32]int32
			if err = json.Unmarshal([]byte(costsJSON), &priorCosts); err != nil {
				return GachaDraw{}, err
			}
			prior.Progress, err = readGachaProgress(ctx, tx, playerID, priorPoolID)
			if err != nil {
				return GachaDraw{}, err
			}
			prior.Progress.Count = priorProgressCount
			if err = tx.Commit(); err != nil {
				return GachaDraw{}, err
			}
			prior.Inventory, err = readGachaInventory(ctx, s.db, playerID, priorCosts, append(prior.Rewards, prior.Bonus...))
			if err != nil {
				return GachaDraw{}, err
			}
			return prior, nil
		}
		if !errors.Is(err, sql.ErrNoRows) {
			return GachaDraw{}, err
		}

		result, insertErr := tx.ExecContext(ctx, `INSERT OR IGNORE INTO game_actions(room_id,player_id,cmd_id,upsn,payload,created_at) VALUES(0,?,?,?,?,?)`, playerID, cmd, upsn, payload, time.Now().Unix())
		if insertErr != nil {
			return GachaDraw{}, insertErr
		}
		inserted, rowsErr := result.RowsAffected()
		if rowsErr != nil {
			return GachaDraw{}, rowsErr
		}
		if inserted == 0 {
			return GachaDraw{}, ErrGachaActionConflict
		}
	}

	if err = consumeInventoryItems(ctx, tx, playerID, costs); err != nil {
		return GachaDraw{}, err
	}

	now := time.Now().Unix()
	const maxGachaTransformCount = int64(1<<31 - 1)
	for i := range rewards {
		reward := &rewards[i]
		var owned int32
		err = tx.QueryRowContext(ctx, `SELECT count FROM inventory WHERE player_id=? AND item_id=?`, playerID, reward.ItemID).Scan(&owned)
		if errors.Is(err, sql.ErrNoRows) {
			owned = 0
		} else if err != nil {
			return GachaDraw{}, err
		}
		isConvert := owned > 0
		reward.Converted = isConvert && len(reward.Transform) > 0
		if reward.Converted {
			for _, transform := range reward.Transform {
				count := int64(transform.Count) * int64(reward.Count)
				if count > maxGachaTransformCount {
					return GachaDraw{}, ErrPveProgressInvalid
				}
				if _, err = tx.ExecContext(ctx, `INSERT INTO inventory(player_id,item_id,count,updated_at) VALUES(?,?,?,?) ON CONFLICT(player_id,item_id) DO UPDATE SET count=count+excluded.count,updated_at=excluded.updated_at`, playerID, transform.ItemID, count, now); err != nil {
					return GachaDraw{}, err
				}
			}
		} else {
			if _, err = tx.ExecContext(ctx, `INSERT INTO inventory(player_id,item_id,count,updated_at) VALUES(?,?,?,?) ON CONFLICT(player_id,item_id) DO UPDATE SET count=count+excluded.count,updated_at=excluded.updated_at`, playerID, reward.ItemID, reward.Count, now); err != nil {
				return GachaDraw{}, err
			}
		}
		converted := 0
		if isConvert {
			converted = 1
		}
		if _, err = tx.ExecContext(ctx, `INSERT INTO gacha_records(player_id,gacha_id,pool_id,item_id,item_count,is_convert,created_at) VALUES(?,?,?,?,?,?,?)`, playerID, gachaID, poolID, reward.ItemID, reward.Count, converted, now); err != nil {
			return GachaDraw{}, err
		}
	}
	for _, reward := range bonus {
		if _, err = tx.ExecContext(ctx, `INSERT INTO inventory(player_id,item_id,count,updated_at) VALUES(?,?,?,?) ON CONFLICT(player_id,item_id) DO UPDATE SET count=count+excluded.count,updated_at=excluded.updated_at`, playerID, reward.ItemID, reward.Count, now); err != nil {
			return GachaDraw{}, err
		}
	}

	if _, err = tx.ExecContext(ctx, `INSERT INTO gacha_progress(player_id,gacha_id,pool_id,count,reward_count,updated_at) VALUES(?,?,?,?,-1,?) ON CONFLICT(player_id,pool_id) DO UPDATE SET gacha_id=excluded.gacha_id,count=gacha_progress.count+excluded.count,updated_at=excluded.updated_at`, playerID, gachaID, poolID, pullCount, now); err != nil {
		return GachaDraw{}, err
	}

	progress, err := readGachaProgress(ctx, tx, playerID, poolID)
	if err != nil {
		return GachaDraw{}, err
	}
	if upsn > 0 {
		costsJSON, marshalErr := json.Marshal(costs)
		if marshalErr != nil {
			return GachaDraw{}, marshalErr
		}
		rewardsJSON, marshalErr := json.Marshal(rewards)
		if marshalErr != nil {
			return GachaDraw{}, marshalErr
		}
		bonusJSON, marshalErr := json.Marshal(bonus)
		if marshalErr != nil {
			return GachaDraw{}, marshalErr
		}
		if _, err = tx.ExecContext(ctx, `INSERT INTO gacha_draws(player_id,upsn,cmd_id,gacha_id,pool_id,count,progress_count,costs_json,rewards_json,bonus_json,created_at) VALUES(?,?,?,?,?,?,?,?,?,?,?)`, playerID, upsn, cmd, gachaID, poolID, pullCount, progress.Count, string(costsJSON), string(rewardsJSON), string(bonusJSON), now); err != nil {
			return GachaDraw{}, err
		}
	}
	inventory := make(map[int32]int32, len(rewards)+1)
	changedItems := make(map[int32]struct{}, len(rewards)+1)
	for itemID := range costs {
		changedItems[itemID] = struct{}{}
	}
	for _, reward := range rewards {
		changedItems[reward.ItemID] = struct{}{}
		if reward.Converted {
			for _, transform := range reward.Transform {
				changedItems[transform.ItemID] = struct{}{}
			}
		}
	}
	for _, reward := range bonus {
		changedItems[reward.ItemID] = struct{}{}
	}
	for itemID := range changedItems {
		var current int32
		err = tx.QueryRowContext(ctx, `SELECT count FROM inventory WHERE player_id=? AND item_id=?`, playerID, itemID).Scan(&current)
		if err != nil && !errors.Is(err, sql.ErrNoRows) {
			return GachaDraw{}, err
		}
		inventory[itemID] = current
	}
	if err = tx.Commit(); err != nil {
		return GachaDraw{}, err
	}
	return GachaDraw{Progress: progress, Rewards: rewards, Bonus: bonus, Inventory: inventory}, nil
}

func (s *Store) ClaimGachaProgress(ctx context.Context, playerID int64, gachaID, poolID int32,
	milestones []GachaMilestone, requireEligible bool) (GachaProgress, map[int32]int32, int, error) {
	if playerID <= 0 || gachaID <= 0 || poolID <= 0 || len(milestones) == 0 {
		return GachaProgress{}, nil, 0, ErrPveProgressInvalid
	}
	ordered := append([]GachaMilestone(nil), milestones...)
	sort.SliceStable(ordered, func(i, j int) bool { return ordered[i].Count < ordered[j].Count })

	tx, err := s.db.BeginTx(ctx, nil)
	if err != nil {
		return GachaProgress{}, nil, 0, err
	}
	defer tx.Rollback()

	progress := GachaProgress{GachaID: gachaID, PoolID: poolID, RewardCount: -1}
	err = tx.QueryRowContext(ctx, `SELECT gacha_id,pool_id,count,reward_count FROM gacha_progress WHERE player_id=? AND pool_id=?`, playerID, poolID).
		Scan(&progress.GachaID, &progress.PoolID, &progress.Count, &progress.RewardCount)
	if err != nil && !errors.Is(err, sql.ErrNoRows) {
		return GachaProgress{}, nil, 0, err
	}
	if errors.Is(err, sql.ErrNoRows) {
		progress = GachaProgress{GachaID: gachaID, PoolID: poolID, RewardCount: -1}
	} else if progress.GachaID != gachaID {
		return GachaProgress{}, nil, 0, ErrGachaRewardLocked
	}

	grant := make(map[int32]int32)
	claimed := 0
	for _, milestone := range ordered {
		if milestone.Count < 0 {
			return GachaProgress{}, nil, 0, ErrPveProgressInvalid
		}
		if milestone.Count > progress.Count || milestone.Count <= progress.RewardCount {
			continue
		}
		if len(milestone.Rewards) == 0 {
			return GachaProgress{}, nil, 0, ErrPveProgressInvalid
		}
		for _, reward := range milestone.Rewards {
			if reward.ItemID <= 0 || reward.Count <= 0 {
				return GachaProgress{}, nil, 0, ErrPveProgressInvalid
			}
			grant[reward.ItemID] += reward.Count
		}
		progress.RewardCount = milestone.Count
		claimed++
	}
	if requireEligible && claimed == 0 {
		return GachaProgress{}, nil, 0, ErrGachaRewardLocked
	}

	if claimed > 0 {
		now := time.Now().Unix()
		for itemID, count := range grant {
			if _, err = tx.ExecContext(ctx, `INSERT INTO inventory(player_id,item_id,count,updated_at) VALUES(?,?,?,?) ON CONFLICT(player_id,item_id) DO UPDATE SET count=count+excluded.count,updated_at=excluded.updated_at`, playerID, itemID, count, now); err != nil {
				return GachaProgress{}, nil, 0, err
			}
		}
		if _, err = tx.ExecContext(ctx, `INSERT INTO gacha_progress(player_id,gacha_id,pool_id,count,reward_count,updated_at) VALUES(?,?,?,?,?,?) ON CONFLICT(player_id,pool_id) DO UPDATE SET gacha_id=excluded.gacha_id,reward_count=MAX(gacha_progress.reward_count,excluded.reward_count),updated_at=excluded.updated_at`, playerID, gachaID, poolID, progress.Count, progress.RewardCount, now); err != nil {
			return GachaProgress{}, nil, 0, err
		}
	}

	inventory := make(map[int32]int32, len(grant))
	for itemID := range grant {
		var count int32
		err = tx.QueryRowContext(ctx, `SELECT count FROM inventory WHERE player_id=? AND item_id=?`, playerID, itemID).Scan(&count)
		if err != nil && !errors.Is(err, sql.ErrNoRows) {
			return GachaProgress{}, nil, 0, err
		}
		inventory[itemID] = count
	}
	if err = tx.Commit(); err != nil {
		return GachaProgress{}, nil, 0, err
	}
	return progress, inventory, claimed, nil
}

func readGachaInventory(ctx context.Context, db *sql.DB, playerID int64, costs map[int32]int32, rewards []GachaDrop) (map[int32]int32, error) {
	changedItems := make(map[int32]struct{}, len(costs)+len(rewards))
	for itemID := range costs {
		changedItems[itemID] = struct{}{}
	}
	for _, reward := range rewards {
		changedItems[reward.ItemID] = struct{}{}
		if reward.Converted {
			for _, transform := range reward.Transform {
				changedItems[transform.ItemID] = struct{}{}
			}
		}
	}
	counts := make(map[int32]int32, len(changedItems))
	for itemID := range changedItems {
		var count int32
		err := db.QueryRowContext(ctx, `SELECT count FROM inventory WHERE player_id=? AND item_id=?`, playerID, itemID).Scan(&count)
		if err != nil && !errors.Is(err, sql.ErrNoRows) {
			return nil, err
		}
		counts[itemID] = count
	}
	return counts, nil
}

func (s *Store) InventoryItemCount(ctx context.Context, playerID int64, itemID int32) (int32, error) {
	var count int32
	err := s.db.QueryRowContext(ctx, `SELECT count FROM inventory WHERE player_id=? AND item_id=?`, playerID, itemID).Scan(&count)
	if errors.Is(err, sql.ErrNoRows) {
		return 0, nil
	}
	return count, err
}

func readGachaProgress(ctx context.Context, tx *sql.Tx, playerID int64, poolID int32) (GachaProgress, error) {
	var progress GachaProgress
	err := tx.QueryRowContext(ctx, `SELECT gacha_id,pool_id,count,reward_count FROM gacha_progress WHERE player_id=? AND pool_id=?`, playerID, poolID).
		Scan(&progress.GachaID, &progress.PoolID, &progress.Count, &progress.RewardCount)
	if errors.Is(err, sql.ErrNoRows) {
		return GachaProgress{PoolID: poolID, RewardCount: -1}, nil
	}
	return progress, err
}

func (s *Store) GachaProgressForPlayer(ctx context.Context, playerID int64) ([]GachaProgress, error) {
	rows, err := s.db.QueryContext(ctx, `SELECT gacha_id,pool_id,count,reward_count FROM gacha_progress WHERE player_id=? ORDER BY gacha_id`, playerID)
	if err != nil {
		return nil, err
	}
	defer rows.Close()
	progress := make([]GachaProgress, 0)
	for rows.Next() {
		var entry GachaProgress
		if err = rows.Scan(&entry.GachaID, &entry.PoolID, &entry.Count, &entry.RewardCount); err != nil {
			return nil, err
		}
		progress = append(progress, entry)
	}
	return progress, rows.Err()
}

// GachaPityHistory returns the latest pulls for a pool in chronological order.
// The draw record table is authoritative for the guarantee counters so they
// survive reconnects and server restarts without a second counter ledger.
func (s *Store) GachaPityHistory(ctx context.Context, playerID int64, poolID int32, limit int) ([]int32, error) {
	if playerID <= 0 || poolID <= 0 || limit <= 0 || limit > 1000 {
		return nil, ErrPveProgressInvalid
	}
	rows, err := s.db.QueryContext(ctx, `SELECT item_id FROM (
		SELECT id,item_id FROM gacha_records WHERE player_id=? AND pool_id=? ORDER BY id DESC LIMIT ?
	) ORDER BY id`, playerID, poolID, limit)
	if err != nil {
		return nil, err
	}
	defer rows.Close()
	items := make([]int32, 0, limit)
	for rows.Next() {
		var itemID int32
		if err = rows.Scan(&itemID); err != nil {
			return nil, err
		}
		items = append(items, itemID)
	}
	return items, rows.Err()
}

func (s *Store) GachaHistory(ctx context.Context, playerID int64, gachaID int32) ([]GachaHistoryRecord, error) {
	query := `SELECT item_id,item_count,is_convert,created_at FROM (
		SELECT id,item_id,item_count,is_convert,created_at FROM gacha_records WHERE player_id=? ORDER BY id DESC LIMIT 40
	) ORDER BY id ASC`
	args := []any{playerID}
	if gachaID > 0 {
		query = `SELECT item_id,item_count,is_convert,created_at FROM (
			SELECT id,item_id,item_count,is_convert,created_at FROM gacha_records WHERE player_id=? AND gacha_id=? ORDER BY id DESC LIMIT 40
		) ORDER BY id ASC`
		args = append(args, gachaID)
	}
	rows, err := s.db.QueryContext(ctx, query, args...)
	if err != nil {
		return nil, err
	}
	defer rows.Close()
	records := make([]GachaHistoryRecord, 0)
	for rows.Next() {
		var record GachaHistoryRecord
		var converted int
		if err = rows.Scan(&record.ItemID, &record.ItemCount, &converted, &record.Time); err != nil {
			return nil, err
		}
		record.IsConvert = converted != 0
		records = append(records, record)
	}
	return records, rows.Err()
}
