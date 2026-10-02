package db

import (
	"context"
	"database/sql"
	"errors"
	"fmt"
	"time"
)

// This file implements the matchmaking and replay persistence layer used by
// the gateway matching loop (internal/gateway/match_impl.go) and replay
// handlers (internal/gateway/replay_impl.go).

// MatchCandidate is a team currently queued for matchmaking.
type MatchCandidate struct {
	ID                int64
	Mode, MapID       int32
	Difficulty        int32
	LeaderID          int64
	CreatedAt         int64
	Count, ReadyCount int
	AddedBots         int
}

// FightRecordRow is one participant entry of a finished fight.
type FightRecordRow struct {
	ID        int64
	ReplayID  string
	RoomID    int64
	PlayerID  int64
	Rank      int32
	HeroID    int32
	MapType   int32
	IsGiveUp  bool
	IsReplay  bool
	Time      int64
	CreatedAt int64
}

// StartMatchForTeam flips a waiting team into matching state (state=2). Only
// the leader may start, and every member must be ready.
func (s *Store) StartMatchForTeam(ctx context.Context, teamID, playerID int64, start bool) error {
	tx, err := s.db.BeginTx(ctx, nil)
	if err != nil {
		return err
	}
	defer tx.Rollback()
	var leader int64
	var teamState int32
	if err = tx.QueryRowContext(ctx, `SELECT leader_id,state FROM match_teams WHERE id=?`, teamID).Scan(&leader, &teamState); err != nil {
		if errors.Is(err, sql.ErrNoRows) {
			return errors.New("team not exist")
		}
		return err
	}
	if leader != playerID {
		return errors.New("only team leader can start")
	}
	newState := int32(1)
	if start {
		newState = 2
	}
	if _, err = tx.ExecContext(ctx, `UPDATE match_teams SET state=? WHERE id=?`, newState, teamID); err != nil {
		return err
	}
	if err = tx.Commit(); err != nil {
		return err
	}
	return nil
}

// ListMatchableTeams returns teams whose state is matching (2) and whose
// members are all ready, oldest first.
func (s *Store) ListMatchableTeams(ctx context.Context) ([]MatchCandidate, error) {
	rows, err := s.db.QueryContext(ctx, `
		SELECT t.id, t.mode, t.map_id, t.difficulty, t.leader_id, t.created_at,
		       COUNT(m.player_id), SUM(CASE WHEN m.ready=0 THEN 1 ELSE 0 END)
		FROM match_teams t JOIN match_team_members m ON m.team_id=t.id
		WHERE t.state=2
		GROUP BY t.id
		ORDER BY t.created_at`)
	if err != nil {
		return nil, err
	}
	defer rows.Close()
	var out []MatchCandidate
	for rows.Next() {
		var c MatchCandidate
		var notReady sql.NullInt64
		if err = rows.Scan(&c.ID, &c.Mode, &c.MapID, &c.Difficulty, &c.LeaderID, &c.CreatedAt, &c.Count, &notReady); err != nil {
			return nil, err
		}
		if notReady.Int64 != 0 {
			continue
		}
		out = append(out, c)
	}
	return out, rows.Err()
}

// ClaimMatchTeams atomically moves every member of the supplied teams into the
// newly created room and marks the teams as playing (state=3). Teams that left
// matching state in the meantime are skipped; the returned slice contains only
// the teams actually claimed.
func (s *Store) ClaimMatchTeams(ctx context.Context, roomID int64, teamIDs []int64) ([]int64, error) {
	if len(teamIDs) == 0 {
		return nil, nil
	}
	tx, err := s.db.BeginTx(ctx, nil)
	if err != nil {
		return nil, err
	}
	defer tx.Rollback()
	now := time.Now().Unix()
	var claimed []int64
	nextSlot := int32(0)
	for _, teamID := range teamIDs {
		res, err := tx.ExecContext(ctx, `UPDATE match_teams SET state=3 WHERE id=? AND state=2`, teamID)
		if err != nil {
			return nil, err
		}
		if changed, rowsErr := res.RowsAffected(); rowsErr != nil {
			return nil, rowsErr
		} else if changed != 1 {
			continue
		}
		rows, err := tx.QueryContext(ctx, `SELECT player_id, ready FROM match_team_members WHERE team_id=?`, teamID)
		if err != nil {
			return nil, err
		}
		var members []int64
		for rows.Next() {
			var pid int64
			var ready int
			if err = rows.Scan(&pid, &ready); err != nil {
				rows.Close()
				return nil, err
			}
			members = append(members, pid)
		}
		if err = rows.Err(); err != nil {
			rows.Close()
			return nil, err
		}
		rows.Close()
		for _, pid := range members {
			if _, err = tx.ExecContext(ctx, `DELETE FROM room_members WHERE room_id=? AND slot=?`, roomID, nextSlot); err != nil {
				return nil, err
			}
			if _, err = tx.ExecContext(ctx, `INSERT OR IGNORE INTO room_members(room_id,player_id,slot,ready,online) VALUES(?,?,?,?,1)`, roomID, pid, nextSlot, 1); err != nil {
				return nil, err
			}
			if _, err = tx.ExecContext(ctx, `UPDATE players SET room_id=?,slot=? WHERE id=?`, roomID, nextSlot, pid); err != nil {
				return nil, err
			}
			nextSlot++
		}
		claimed = append(claimed, teamID)
	}
	if len(claimed) == 0 {
		if _, err = tx.ExecContext(ctx, `DELETE FROM rooms WHERE id=?`, roomID); err != nil {
			return nil, err
		}
		if err = tx.Commit(); err != nil {
			return nil, err
		}
		return nil, nil
	}
	if _, err = tx.ExecContext(ctx, `UPDATE rooms SET updated_at=? WHERE id=?`, now, roomID); err != nil {
		return nil, err
	}
	if err = tx.Commit(); err != nil {
		return nil, err
	}
	return claimed, nil
}

// CreateMatchRoom creates a room that hosts a matched game, already in the
// hero-selection state (10), with the first team's leader as master.
func (s *Store) CreateMatchRoom(ctx context.Context, mode, mapID, difficulty int32, masterID int64) (int64, error) {
	tx, err := s.db.BeginTx(ctx, nil)
	if err != nil {
		return 0, err
	}
	defer tx.Rollback()
	now := time.Now().Unix()
	res, err := tx.ExecContext(ctx, `INSERT INTO rooms(name,pwd,map_id,max_time,upgrade_plan,time_plan,mode,lobby_id,speed_type,difficulty,skip_story,room_label,master_id,state,created_at,updated_at) VALUES(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)`,
		"Match room", "", mapID, 0, 0, 0, mode, 0, 0, difficulty, 0, 0, masterID, 10, now, now)
	if err != nil {
		return 0, err
	}
	roomID, err := res.LastInsertId()
	if err != nil {
		return 0, err
	}
	if _, err = tx.ExecContext(ctx, `INSERT OR IGNORE INTO room_members(room_id,player_id,slot,ready,online) VALUES(?,?,0,1,1)`, roomID, masterID); err != nil {
		return 0, err
	}
	if _, err = tx.ExecContext(ctx, `UPDATE players SET room_id=?,slot=0 WHERE id=?`, roomID, masterID); err != nil {
		return 0, err
	}
	if err = tx.Commit(); err != nil {
		return 0, err
	}
	return roomID, nil
}

// FillRoomWithBots creates persistent bot identities for every empty slot of a
// room, mirroring the bot seats created by StartRoom.
func (s *Store) FillRoomWithBots(ctx context.Context, roomID int64) (int, error) {
	tx, err := s.db.BeginTx(ctx, nil)
	if err != nil {
		return 0, err
	}
	defer tx.Rollback()
	added := 0
	for slot := int32(0); slot < 4; slot++ {
		var occupied int
		if err = tx.QueryRowContext(ctx, `SELECT COUNT(*) FROM room_members WHERE room_id=? AND slot=?`, roomID, slot).Scan(&occupied); err != nil {
			return 0, err
		}
		if occupied != 0 {
			continue
		}
		loginKey := fmt.Sprintf("bot:room:%d:slot:%d", roomID, slot)
		nick := fmt.Sprintf("Starling %02d", slot+1)
		now := time.Now().Unix()
		if _, err = tx.ExecContext(ctx, `INSERT INTO accounts(login_key,platform,nick,device_id,created_at,last_login_at) VALUES(?,?,?,?,?,?) ON CONFLICT(login_key) DO UPDATE SET platform='bot',nick=excluded.nick`, loginKey, "bot", nick, "server-bot", now, now); err != nil {
			return 0, err
		}
		var accountID, botID int64
		if err = tx.QueryRowContext(ctx, `SELECT id FROM accounts WHERE login_key=?`, loginKey).Scan(&accountID); err != nil {
			return 0, err
		}
		if _, err = tx.ExecContext(ctx, `INSERT OR IGNORE INTO players(account_id,nick,created_at) VALUES(?,?,?)`, accountID, nick, now); err != nil {
			return 0, err
		}
		if err = tx.QueryRowContext(ctx, `SELECT id FROM players WHERE account_id=?`, accountID).Scan(&botID); err != nil {
			return 0, err
		}
		if _, err = tx.ExecContext(ctx, `INSERT OR IGNORE INTO room_members(room_id,player_id,slot,ready,online) VALUES(?,?,?,?,1)`, roomID, botID, slot, 1); err != nil {
			return 0, err
		}
		if _, err = tx.ExecContext(ctx, `UPDATE players SET room_id=?,slot=?,node_id=0,back_node_id=0,gold=0,hp=10 WHERE id=?`, roomID, slot, botID); err != nil {
			return 0, err
		}
		added++
	}
	if err = tx.Commit(); err != nil {
		return 0, err
	}
	return added, nil
}

// SaveFightRecord stores one participant entry for a finished fight. The
// replayID groups the entries of one fight together.
func (s *Store) SaveFightRecord(ctx context.Context, row FightRecordRow) error {
	_, err := s.db.ExecContext(ctx, `INSERT INTO fight_records(replay_id,room_id,player_id,rank,hero_id,map_type,is_give_up,is_replay,time,created_at) VALUES(?,?,?,?,?,?,?,?,?,?)`,
		row.ReplayID, row.RoomID, row.PlayerID, row.Rank, row.HeroID, row.MapType, boolInt(row.IsGiveUp), boolInt(row.IsReplay), row.Time, row.CreatedAt)
	return err
}

// FightRecordsForPlayer lists a player's recorded fights, newest first. When
// isReplay is true only replay-flagged entries are returned; index selects a
// page of up to 20 entries.
func (s *Store) FightRecordsForPlayer(ctx context.Context, playerID int64, isReplay bool, index int32) ([]FightRecordRow, error) {
	offset := int(index) * 20
	if offset < 0 {
		offset = 0
	}
	rows, err := s.db.QueryContext(ctx, `SELECT id,replay_id,room_id,player_id,rank,hero_id,map_type,is_give_up,is_replay,time,created_at FROM fight_records WHERE player_id=? AND is_replay=? ORDER BY time DESC, id DESC LIMIT 20 OFFSET ?`,
		playerID, boolInt(isReplay), offset)
	if err != nil {
		return nil, err
	}
	defer rows.Close()
	var out []FightRecordRow
	for rows.Next() {
		var r FightRecordRow
		var giveUp, replay int
		if err = rows.Scan(&r.ID, &r.ReplayID, &r.RoomID, &r.PlayerID, &r.Rank, &r.HeroID, &r.MapType, &giveUp, &replay, &r.Time, &r.CreatedAt); err != nil {
			return nil, err
		}
		r.IsGiveUp = giveUp != 0
		r.IsReplay = replay != 0
		out = append(out, r)
	}
	return out, rows.Err()
}

// SaveReplaySnapshot persists the serialized battle snapshot of a replay.
func (s *Store) SaveReplaySnapshot(ctx context.Context, replayID string, roomID int64, data []byte) error {
	_, err := s.db.ExecContext(ctx, `INSERT INTO replay_snapshots(replay_id,room_id,data,created_at) VALUES(?,?,?,?) ON CONFLICT(replay_id) DO UPDATE SET data=excluded.data`,
		replayID, roomID, data, time.Now().Unix())
	return err
}

// FightRecordsForReplay returns every fight_records row saved with one
// replay id (used to rebuild the GameFinish payload for replay downloads).
func (s *Store) FightRecordsForReplay(ctx context.Context, replayID string) ([]FightRecordRow, error) {
	if replayID == "" {
		return nil, nil
	}
	rows, err := s.db.QueryContext(ctx, `SELECT replay_id,room_id,player_id,rank,hero_id,map_type,is_give_up,is_replay,time,created_at FROM fight_records WHERE replay_id=? ORDER BY rank,id`, replayID)
	if err != nil {
		return nil, err
	}
	defer rows.Close()
	var out []FightRecordRow
	for rows.Next() {
		var row FightRecordRow
		if err := rows.Scan(&row.ReplayID, &row.RoomID, &row.PlayerID, &row.Rank, &row.HeroID, &row.MapType, &row.IsGiveUp, &row.IsReplay, &row.Time, &row.CreatedAt); err != nil {
			return nil, err
		}
		out = append(out, row)
	}
	return out, rows.Err()
}

// ReplaySnapshot fetches a stored replay snapshot. ok is false when the replay
// id is unknown.
func (s *Store) ReplaySnapshot(ctx context.Context, replayID string) (data []byte, roomID int64, ok bool, err error) {
	err = s.db.QueryRowContext(ctx, `SELECT data,room_id FROM replay_snapshots WHERE replay_id=?`, replayID).Scan(&data, &roomID)
	if errors.Is(err, sql.ErrNoRows) {
		return nil, 0, false, nil
	}
	if err != nil {
		return nil, 0, false, err
	}
	return data, roomID, true, nil
}
