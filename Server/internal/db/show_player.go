package db

import (
	"context"
	"database/sql"
	"encoding/json"
	"errors"
)

// ShowPlayerProfile stores the client-editable fields of a public profile.
// Battle statistics and match records remain server-owned data.
type ShowPlayerProfile struct {
	StandingPainting int32   `json:"standingPainting"`
	AchieveIDs       []int32 `json:"achieveId"`
	IsShowData       bool    `json:"isShowData"`
	IsShowFight      bool    `json:"isShowFight"`
}

func (s *Store) GetShowPlayerProfile(ctx context.Context, playerID int64) (ShowPlayerProfile, error) {
	var encoded string
	if err := s.db.QueryRowContext(ctx, `SELECT show_player_json FROM players WHERE id=?`, playerID).Scan(&encoded); err != nil {
		if errors.Is(err, sql.ErrNoRows) {
			return ShowPlayerProfile{}, ErrPlayerNotFound
		}
		return ShowPlayerProfile{}, err
	}
	var profile ShowPlayerProfile
	if err := json.Unmarshal([]byte(encoded), &profile); err != nil {
		return ShowPlayerProfile{}, err
	}
	if profile.AchieveIDs == nil {
		profile.AchieveIDs = []int32{}
	}
	return profile, nil
}

func (s *Store) SetShowPlayerProfile(ctx context.Context, playerID int64, profile ShowPlayerProfile) error {
	if profile.AchieveIDs == nil {
		profile.AchieveIDs = []int32{}
	}
	encoded, err := json.Marshal(profile)
	if err != nil {
		return err
	}
	result, err := s.db.ExecContext(ctx, `UPDATE players SET show_player_json=? WHERE id=?`, string(encoded), playerID)
	if err != nil {
		return err
	}
	updated, err := result.RowsAffected()
	if err != nil {
		return err
	}
	if updated == 0 {
		return ErrPlayerNotFound
	}
	return nil
}
