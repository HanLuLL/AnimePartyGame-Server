package db

import (
	"context"
	"database/sql"
	"encoding/json"
	"errors"
	"fmt"
	"time"
)

// BPSnapshot is the persisted battle-pass state for one player and one
// activity season. Level, experience, premium gear, claimed level rewards,
// per-task progress and claimed task rewards round-trip through the
// player_battle_pass table.
type BPSnapshot struct {
	DefID      int32
	Lv         int32
	Exp        int32
	Gear       int32
	RewardIDs  map[int32]int32
	Task       map[int32]int32
	TaskReward map[int32]bool
	UpdatedAt  int64
}

// ErrBPMissing marks a player without a stored battle-pass row.
var ErrBPMissing = errors.New("battle pass state not found")

// LoadBattlePass returns the persisted state, or a fresh level-1 snapshot for
// defID when the player has never touched the pass.
func (s *Store) LoadBattlePass(ctx context.Context, playerID, defID int64) (BPSnapshot, error) {
	if playerID <= 0 || defID <= 0 {
		return BPSnapshot{}, ErrBPMissing
	}
	var def, lv, exp, gear int64
	var rewardIDs, task, claimed string
	var updatedAt int64
	err := s.db.QueryRowContext(ctx,
		`SELECT def_id, lv, exp, gear, reward_ids_json, task_json, task_reward_json, updated_at
		 FROM player_battle_pass WHERE player_id=? AND def_id=?`, playerID, defID).
		Scan(&def, &lv, &exp, &gear, &rewardIDs, &task, &claimed, &updatedAt)
	if errors.Is(err, sql.ErrNoRows) {
		return BPSnapshot{
			DefID: int32(defID), Lv: 1,
			RewardIDs: map[int32]int32{}, Task: map[int32]int32{},
			TaskReward: map[int32]bool{}, UpdatedAt: time.Now().Unix(),
		}, nil
	}
	if err != nil {
		return BPSnapshot{}, err
	}
	snap := BPSnapshot{DefID: int32(def), Lv: int32(lv), Exp: int32(exp), Gear: int32(gear), UpdatedAt: updatedAt}
	snap.RewardIDs = map[int32]int32{}
	snap.Task = map[int32]int32{}
	snap.TaskReward = map[int32]bool{}
	decodeInt32Map(rewardIDs, snap.RewardIDs)
	decodeInt32Map(task, snap.Task)
	if claimed != "" && claimed != "[]" {
		_ = json.Unmarshal([]byte(claimed), &snap.TaskReward)
	}
	if snap.TaskReward == nil {
		snap.TaskReward = map[int32]bool{}
	}
	if snap.Lv < 1 {
		snap.Lv = 1
	}
	return snap, nil
}

// SaveBattlePass upserts the snapshot.
func (s *Store) SaveBattlePass(ctx context.Context, playerID int64, snap BPSnapshot) error {
	if playerID <= 0 || snap.DefID <= 0 {
		return ErrBPMissing
	}
	rewardIDs, err := json.Marshal(snap.RewardIDs)
	if err != nil {
		return err
	}
	task, err := json.Marshal(snap.Task)
	if err != nil {
		return err
	}
	claimed, err := json.Marshal(snap.TaskReward)
	if err != nil {
		return err
	}
	now := time.Now().Unix()
	_, err = s.db.ExecContext(ctx, `INSERT INTO player_battle_pass
		(player_id, def_id, lv, exp, gear, reward_ids_json, task_json, task_reward_json, updated_at)
		VALUES(?,?,?,?,?,?,?,?,?)
		ON CONFLICT(player_id,def_id) DO UPDATE SET
		lv=excluded.lv, exp=excluded.exp, gear=excluded.gear,
		reward_ids_json=excluded.reward_ids_json, task_json=excluded.task_json,
		task_reward_json=excluded.task_reward_json, updated_at=excluded.updated_at`,
		playerID, snap.DefID, snap.Lv, snap.Exp, snap.Gear,
		string(rewardIDs), string(task), string(claimed), now)
	return err
}

// SetBPGear flips the premium gear flag (0 = none, 1 = normal, 2 = premium).
func (s *Store) SetBPGear(ctx context.Context, playerID, defID, gear int64) error {
	if _, err := s.LoadBattlePass(ctx, playerID, int64(defID)); err != nil {
		return err
	}
	_, err := s.db.ExecContext(ctx,
		`UPDATE player_battle_pass SET gear=?, updated_at=? WHERE player_id=? AND def_id=?`,
		gear, time.Now().Unix(), playerID, defID)
	return err
}

// AddBPExp atomically adds experience and promotes the level while the
// configured experience-per-level applies. Past maxLv (0 = unbounded) the
// level stops growing but experience keeps accumulating, matching the client
// which derives the after-max reward count from exp/expPerLv.
func (s *Store) AddBPExp(ctx context.Context, playerID, defID int64, add, expPerLv, maxLv int32) (BPSnapshot, error) {
	if add < 0 || expPerLv <= 0 {
		return BPSnapshot{}, errors.New("invalid battle pass experience delta")
	}
	snap, err := s.LoadBattlePass(ctx, playerID, defID)
	if err != nil {
		return BPSnapshot{}, err
	}
	snap.Exp += add
	if snap.Exp < 0 {
		snap.Exp = 0
	}
	for snap.Exp >= expPerLv && (maxLv <= 0 || snap.Lv < maxLv) {
		snap.Exp -= expPerLv
		snap.Lv++
	}
	if snap.Lv < 1 {
		snap.Lv = 1
	}
	snap.UpdatedAt = time.Now().Unix()
	if err = s.SaveBattlePass(ctx, playerID, snap); err != nil {
		return BPSnapshot{}, err
	}
	return snap, nil
}

// ClaimBPLevelReward records that a level reward was taken for gear bucket
// gear (0 free, 1 normal, 2 premium). Already-claimed pairs are idempotent.
func (s *Store) ClaimBPLevelReward(ctx context.Context, playerID, defID, level, gear int64) (bool, error) {
	if level <= 0 {
		return false, errors.New("invalid battle pass reward level")
	}
	snap, err := s.LoadBattlePass(ctx, playerID, defID)
	if err != nil {
		return false, err
	}
	lvKey := int32(level)
	gearMask := int32(gear)
	if previous, ok := snap.RewardIDs[lvKey]; ok && previous&gearMask == gearMask {
		return false, nil
	}
	snap.RewardIDs[lvKey] |= gearMask
	snap.UpdatedAt = time.Now().Unix()
	return true, s.SaveBattlePass(ctx, playerID, snap)
}

// ClaimBPTaskReward records a claimed task reward; it returns false when the
// task was already claimed.
func (s *Store) ClaimBPTaskReward(ctx context.Context, playerID, defID, taskID int64) (bool, error) {
	if taskID <= 0 {
		return false, errors.New("invalid battle pass task id")
	}
	snap, err := s.LoadBattlePass(ctx, playerID, defID)
	if err != nil {
		return false, err
	}
	taskKey := int32(taskID)
	if snap.TaskReward[taskKey] {
		return false, nil
	}
	snap.TaskReward[taskKey] = true
	snap.UpdatedAt = time.Now().Unix()
	return true, s.SaveBattlePass(ctx, playerID, snap)
}

// UpdateBPTaskProgress atomically bumps one task counter by delta.
func (s *Store) UpdateBPTaskProgress(ctx context.Context, playerID, defID, taskID int64, delta int32) (int32, error) {
	if delta == 0 || taskID <= 0 {
		return 0, errors.New("invalid battle pass task delta")
	}
	snap, err := s.LoadBattlePass(ctx, playerID, defID)
	if err != nil {
		return 0, err
	}
	snap.Task[int32(taskID)] += delta
	if snap.Task[int32(taskID)] < 0 {
		snap.Task[int32(taskID)] = 0
	}
	snap.UpdatedAt = time.Now().Unix()
	if err = s.SaveBattlePass(ctx, playerID, snap); err != nil {
		return 0, err
	}
	return snap.Task[int32(taskID)], nil
}

// SetClientData merges a client data blob onto the player's persisted client
// state. Only keys present in the incoming payload are replaced.
func (s *Store) SetClientData(ctx context.Context, playerID int64, payload string) error {
	if playerID <= 0 {
		return ErrPlayerNotFound
	}
	merged, err := s.ClientData(ctx, playerID)
	if err != nil && !errors.Is(err, ErrPlayerNotFound) {
		return err
	}
	fields := map[string]json.RawMessage{}
	if merged != "" {
		_ = json.Unmarshal([]byte(merged), &fields)
	}
	incoming := map[string]json.RawMessage{}
	if err = json.Unmarshal([]byte(payload), &incoming); err != nil {
		return fmt.Errorf("decode client data for player %d: %w", playerID, err)
	}
	for key, value := range incoming {
		fields[key] = value
	}
	encoded, err := json.Marshal(fields)
	if err != nil {
		return err
	}
	_, err = s.db.ExecContext(ctx, `UPDATE players SET client_data_json=? WHERE id=?`, string(encoded), playerID)
	return err
}

// ClientData returns the raw persisted client data blob ("" when absent).
func (s *Store) ClientData(ctx context.Context, playerID int64) (string, error) {
	var blob sql.NullString
	err := s.db.QueryRowContext(ctx, `SELECT client_data_json FROM players WHERE id=?`, playerID).Scan(&blob)
	if errors.Is(err, sql.ErrNoRows) {
		return "", ErrPlayerNotFound
	}
	if err != nil {
		return "", err
	}
	return blob.String, nil
}

func decodeInt32Map(raw string, into map[int32]int32) {
	if raw == "" || raw == "{}" || into == nil {
		return
	}
	_ = json.Unmarshal([]byte(raw), &into)
}
