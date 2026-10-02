package db

import (
	"context"
	"database/sql"
	"encoding/json"
	"errors"
	"time"
)

// SingleProgressRow is the persisted single-player campaign progress for one
// player: cleared campaign levels (level -> pass flag/star count), stage
// unlock map, best score, and the client's opaque SingleGameData blob.
type SingleProgressRow struct {
	LevelPass  map[int32]int32
	StageLevel map[int32]int32
	MaxScore   int32
	Data       map[string]json.RawMessage
}

func decodeSingleInt32Map(raw string) map[int32]int32 {
	out := map[int32]int32{}
	if raw == "" || raw == "{}" {
		return out
	}
	_ = json.Unmarshal([]byte(raw), &out)
	return out
}

// LoadSingleProgress reads the player's single-player campaign progress. A
// missing row is an empty (fresh) progress, not an error.
func (s *Store) LoadSingleProgress(ctx context.Context, playerID int64) (SingleProgressRow, error) {
	out := SingleProgressRow{LevelPass: map[int32]int32{}, StageLevel: map[int32]int32{}, Data: map[string]json.RawMessage{}}
	if playerID <= 0 {
		return out, ErrPlayerNotFound
	}
	var levelPass, stageLevel, data string
	var maxScore int32
	err := s.db.QueryRowContext(ctx, `SELECT level_pass_json,stage_level_json,max_score,data_json FROM player_single_progress WHERE player_id=?`, playerID).
		Scan(&levelPass, &stageLevel, &maxScore, &data)
	if errors.Is(err, sql.ErrNoRows) {
		return out, nil
	}
	if err != nil {
		return out, err
	}
	out.LevelPass = decodeSingleInt32Map(levelPass)
	out.StageLevel = decodeSingleInt32Map(stageLevel)
	out.MaxScore = maxScore
	if data != "" && data != "{}" {
		_ = json.Unmarshal([]byte(data), &out.Data)
	}
	return out, nil
}

// SaveSingleProgress atomically upserts the single-player campaign progress.
// nil maps keep the stored values.
func (s *Store) SaveSingleProgress(ctx context.Context, playerID int64, levelPass, stageLevel map[int32]int32, maxScore int32, data map[string]json.RawMessage) error {
	if playerID <= 0 {
		return ErrPlayerNotFound
	}
	current, err := s.LoadSingleProgress(ctx, playerID)
	if err != nil {
		return err
	}
	if levelPass != nil {
		current.LevelPass = levelPass
	}
	if stageLevel != nil {
		current.StageLevel = stageLevel
	}
	if maxScore > current.MaxScore {
		current.MaxScore = maxScore
	}
	if data != nil {
		current.Data = data
	}
	encodedPass, err := json.Marshal(current.LevelPass)
	if err != nil {
		return err
	}
	encodedStage, err := json.Marshal(current.StageLevel)
	if err != nil {
		return err
	}
	encodedData, err := json.Marshal(current.Data)
	if err != nil {
		return err
	}
	_, err = s.db.ExecContext(ctx, `INSERT INTO player_single_progress(player_id,level_pass_json,stage_level_json,max_score,data_json,updated_at)
		VALUES(?,?,?,?,?,?) ON CONFLICT(player_id) DO UPDATE SET
		level_pass_json=excluded.level_pass_json, stage_level_json=excluded.stage_level_json,
		max_score=excluded.max_score, data_json=excluded.data_json, updated_at=excluded.updated_at`,
		playerID, string(encodedPass), string(encodedStage), current.MaxScore, string(encodedData), time.Now().Unix())
	return err
}
