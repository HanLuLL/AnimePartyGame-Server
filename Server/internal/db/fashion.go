package db

import (
	"context"
	"database/sql"
	"encoding/json"
	"errors"
	"fmt"
)

func (s *Store) LoadFashionState(ctx context.Context, playerID int64) (map[int32]map[int32]int32, int32, error) {
	var encoded string
	var usePlan int32
	err := s.db.QueryRowContext(ctx, `SELECT fashion_plans_json,use_plan FROM players WHERE id=?`, playerID).Scan(&encoded, &usePlan)
	if errors.Is(err, sql.ErrNoRows) {
		return nil, 0, ErrPlayerNotFound
	}
	if err != nil {
		return nil, 0, err
	}
	plans := make(map[int32]map[int32]int32)
	if encoded != "" {
		if err = json.Unmarshal([]byte(encoded), &plans); err != nil {
			return nil, 0, fmt.Errorf("decode fashion plans for player %d: %w", playerID, err)
		}
	}
	if plans == nil {
		plans = make(map[int32]map[int32]int32)
	}
	if usePlan < 1 || usePlan > 10 {
		usePlan = 1
	}
	if _, ok := plans[usePlan]; !ok && len(plans) > 0 {
		usePlan = 1
		if _, ok = plans[usePlan]; !ok {
			for plan := range plans {
				if plan >= 1 && plan <= 10 {
					usePlan = plan
					break
				}
			}
		}
	}
	return plans, usePlan, nil
}

func (s *Store) SaveFashionState(ctx context.Context, playerID int64, plans map[int32]map[int32]int32, usePlan int32) error {
	if playerID <= 0 {
		return ErrPlayerNotFound
	}
	if usePlan < 1 || usePlan > 10 {
		return errors.New("invalid fashion plan")
	}
	if plans == nil {
		plans = make(map[int32]map[int32]int32)
	}
	encoded, err := json.Marshal(plans)
	if err != nil {
		return fmt.Errorf("encode fashion plans for player %d: %w", playerID, err)
	}
	result, err := s.db.ExecContext(ctx, `UPDATE players SET fashion_plans_json=?,use_plan=? WHERE id=?`, string(encoded), usePlan, playerID)
	if err != nil {
		return err
	}
	if changed, _ := result.RowsAffected(); changed == 0 {
		return ErrPlayerNotFound
	}
	return nil
}
