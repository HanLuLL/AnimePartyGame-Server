package db

import (
	"context"
	"database/sql"
	"errors"
	"time"
)

var (
	ErrRoomInviteInvalid         = errors.New("invalid room invite")
	ErrRoomInviteRoomUnavailable = errors.New("room is not accepting invites")
	ErrRoomInvitePassword        = errors.New("room invite password does not match")
	ErrRoomInviteFull            = errors.New("room is full")
	ErrRoomInviteTargetBusy      = errors.New("invite target is unavailable")
	ErrRoomInviteTargetUnrelated = errors.New("invite target is not a friend or recent co-player")
)

type RoomInvite struct {
	InviterID    int64
	InviterName  string
	InviterLevel int32
	CreatedAt    int64
	RoomID       int64
	RoomName     string
	Password     string
	MapID        int32
	PlayerCount  int32
}

type RecentPlayer struct {
	ID           int64
	Name         string
	Level        int32
	LastPlayedAt int64
	RoomID       int64
}

// CreateRoomInvites stores an entire client selection atomically. Targets must
// be friends or have played in a match with the inviter, must not be blocked,
// and cannot already occupy a room.
func (s *Store) CreateRoomInvites(ctx context.Context, roomID, inviterID int64, password string, targetIDs []int64) ([]int64, error) {
	if roomID <= 0 || inviterID <= 0 || len(targetIDs) == 0 || len(targetIDs) > 64 {
		return nil, ErrRoomInviteInvalid
	}
	tx, err := s.db.BeginTx(ctx, nil)
	if err != nil {
		return nil, err
	}
	defer tx.Rollback()
	var state int32
	var storedPassword string
	var count int32
	if err = tx.QueryRowContext(ctx, `SELECT r.state,r.pwd,(SELECT COUNT(*) FROM room_members rm WHERE rm.room_id=r.id) FROM rooms r WHERE r.id=?`, roomID).Scan(&state, &storedPassword, &count); err != nil {
		if errors.Is(err, sql.ErrNoRows) {
			return nil, ErrRoomInviteRoomUnavailable
		}
		return nil, err
	}
	if state != 1 {
		return nil, ErrRoomInviteRoomUnavailable
	}
	if storedPassword != password {
		return nil, ErrRoomInvitePassword
	}
	var isMember int
	if err = tx.QueryRowContext(ctx, `SELECT COUNT(*) FROM room_members WHERE room_id=? AND player_id=?`, roomID, inviterID).Scan(&isMember); err != nil {
		return nil, err
	}
	if isMember == 0 {
		return nil, ErrRoomInviteRoomUnavailable
	}
	if count >= 4 {
		return nil, ErrRoomInviteFull
	}
	unique := make([]int64, 0, len(targetIDs))
	seen := make(map[int64]struct{}, len(targetIDs))
	for _, targetID := range targetIDs {
		if targetID <= 0 || targetID == inviterID {
			return nil, ErrRoomInviteInvalid
		}
		if _, exists := seen[targetID]; exists {
			continue
		}
		seen[targetID] = struct{}{}
		unique = append(unique, targetID)
	}
	if len(unique) == 0 {
		return nil, ErrRoomInviteInvalid
	}
	now := time.Now().Unix()
	for _, targetID := range unique {
		var currentRoom int64
		var platform string
		if err = tx.QueryRowContext(ctx, `SELECT COALESCE(p.room_id,0),a.platform FROM players p JOIN accounts a ON a.id=p.account_id WHERE p.id=?`, targetID).Scan(&currentRoom, &platform); err != nil {
			if errors.Is(err, sql.ErrNoRows) {
				return nil, ErrPlayerNotFound
			}
			return nil, err
		}
		if currentRoom != 0 || platform == "bot" {
			return nil, ErrRoomInviteTargetBusy
		}
		var blocked int
		if err = tx.QueryRowContext(ctx, `SELECT COUNT(*) FROM friend_blocks WHERE (player_id=? AND blocked_id=?) OR (player_id=? AND blocked_id=?)`, inviterID, targetID, targetID, inviterID).Scan(&blocked); err != nil {
			return nil, err
		}
		if blocked != 0 {
			return nil, ErrRoomInviteTargetUnrelated
		}
		var related int
		if err = tx.QueryRowContext(ctx, `SELECT CASE WHEN
			EXISTS(SELECT 1 FROM friendships WHERE state=1 AND ((player_id=? AND friend_id=?) OR (player_id=? AND friend_id=?))) OR
			EXISTS(SELECT 1 FROM recent_players WHERE player_id=? AND target_player_id=?)
			THEN 1 ELSE 0 END`, inviterID, targetID, targetID, inviterID, inviterID, targetID).Scan(&related); err != nil {
			return nil, err
		}
		if related == 0 {
			return nil, ErrRoomInviteTargetUnrelated
		}
		if _, err = tx.ExecContext(ctx, `INSERT INTO room_invites(room_id,inviter_id,invitee_id,created_at) VALUES(?,?,?,?)
			ON CONFLICT(room_id,invitee_id) DO UPDATE SET inviter_id=excluded.inviter_id,created_at=excluded.created_at`, roomID, inviterID, targetID, now); err != nil {
			return nil, err
		}
	}
	if err = tx.Commit(); err != nil {
		return nil, err
	}
	return unique, nil
}

func (s *Store) RoomInvitesForPlayer(ctx context.Context, playerID int64) ([]RoomInvite, error) {
	rows, err := s.db.QueryContext(ctx, `SELECT i.inviter_id,p.nick,p.level,i.created_at,r.id,r.name,r.pwd,r.map_id,
		(SELECT COUNT(*) FROM room_members rm WHERE rm.room_id=r.id)
		FROM room_invites i
		JOIN rooms r ON r.id=i.room_id AND r.state=1
		JOIN players p ON p.id=i.inviter_id
		JOIN players recipient ON recipient.id=i.invitee_id AND COALESCE(recipient.room_id,0)=0
		WHERE i.invitee_id=? AND (SELECT COUNT(*) FROM room_members rm WHERE rm.room_id=r.id)<4
		AND NOT EXISTS(SELECT 1 FROM friend_blocks b WHERE (b.player_id=i.inviter_id AND b.blocked_id=i.invitee_id) OR (b.player_id=i.invitee_id AND b.blocked_id=i.inviter_id))
		ORDER BY i.created_at DESC,i.room_id DESC`, playerID)
	if err != nil {
		return nil, err
	}
	defer rows.Close()
	var invites []RoomInvite
	for rows.Next() {
		var invite RoomInvite
		if err = rows.Scan(&invite.InviterID, &invite.InviterName, &invite.InviterLevel, &invite.CreatedAt,
			&invite.RoomID, &invite.RoomName, &invite.Password, &invite.MapID, &invite.PlayerCount); err != nil {
			return nil, err
		}
		invites = append(invites, invite)
	}
	return invites, rows.Err()
}

func (s *Store) ClearRoomInvites(ctx context.Context, playerID int64) error {
	_, err := s.db.ExecContext(ctx, `DELETE FROM room_invites WHERE invitee_id=?`, playerID)
	return err
}

func (s *Store) RecentPlayersForPlayer(ctx context.Context, playerID int64, limit int) ([]RecentPlayer, error) {
	if limit <= 0 || limit > 100 {
		limit = 50
	}
	rows, err := s.db.QueryContext(ctx, `SELECT p.id,p.nick,p.level,r.last_played_at,COALESCE(p.room_id,0)
		FROM recent_players r JOIN players p ON p.id=r.target_player_id JOIN accounts a ON a.id=p.account_id
		WHERE r.player_id=? AND a.platform<>'bot'
		AND NOT EXISTS(SELECT 1 FROM friend_blocks b WHERE (b.player_id=r.player_id AND b.blocked_id=r.target_player_id) OR (b.player_id=r.target_player_id AND b.blocked_id=r.player_id))
		ORDER BY r.last_played_at DESC,r.target_player_id LIMIT ?`, playerID, limit)
	if err != nil {
		return nil, err
	}
	defer rows.Close()
	var recent []RecentPlayer
	for rows.Next() {
		var player RecentPlayer
		if err = rows.Scan(&player.ID, &player.Name, &player.Level, &player.LastPlayedAt, &player.RoomID); err != nil {
			return nil, err
		}
		recent = append(recent, player)
	}
	return recent, rows.Err()
}

func recordRecentPlayers(ctx context.Context, tx *sql.Tx, humanIDs []int64, at int64) error {
	for _, ownerID := range humanIDs {
		for _, targetID := range humanIDs {
			if ownerID == targetID {
				continue
			}
			if _, err := tx.ExecContext(ctx, `INSERT INTO recent_players(player_id,target_player_id,last_played_at) VALUES(?,?,?)
				ON CONFLICT(player_id,target_player_id) DO UPDATE SET last_played_at=excluded.last_played_at`, ownerID, targetID, at); err != nil {
				return err
			}
		}
		if _, err := tx.ExecContext(ctx, `DELETE FROM recent_players WHERE player_id=? AND target_player_id NOT IN (
			SELECT target_player_id FROM recent_players WHERE player_id=?
			ORDER BY last_played_at DESC,target_player_id LIMIT 50)`, ownerID, ownerID); err != nil {
			return err
		}
	}
	return nil
}
