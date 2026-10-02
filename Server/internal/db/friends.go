package db

import (
	"context"
	"errors"
	"time"
)

var (
	ErrFriendTargetNotFound   = errors.New("friend target not found")
	ErrFriendRelationMissing  = errors.New("friend relation not found")
	ErrFriendOperationInvalid = errors.New("invalid friend operation")
)

type BlockedPlayer struct {
	Player    Player
	CreatedAt int64
}

// FriendOperation implements the client's FriendOp values: 1 removes a friend,
// 2 blocks a player, and 3 removes a block. These operations are backed by the
// relationships shown by FriendLogic.OnFriendOpS2CServerCallBack.
func (s *Store) FriendOperation(ctx context.Context, ownerID, targetID int64, operation int32) error {
	if ownerID <= 0 || targetID <= 0 || ownerID == targetID {
		return ErrFriendOperationInvalid
	}
	tx, err := s.db.BeginTx(ctx, nil)
	if err != nil {
		return err
	}
	defer tx.Rollback()
	var exists int
	if err = tx.QueryRowContext(ctx, `SELECT COUNT(*) FROM players WHERE id=?`, targetID).Scan(&exists); err != nil {
		return err
	}
	if exists == 0 {
		return ErrFriendTargetNotFound
	}
	now := time.Now().Unix()
	switch operation {
	case 1:
		if err = tx.QueryRowContext(ctx, `SELECT COUNT(*) FROM friendships WHERE player_id=? AND friend_id=? AND state=1`, ownerID, targetID).Scan(&exists); err != nil {
			return err
		}
		if exists == 0 {
			return ErrFriendRelationMissing
		}
		if _, err = tx.ExecContext(ctx, `DELETE FROM friendships WHERE (player_id=? AND friend_id=?) OR (player_id=? AND friend_id=?)`, ownerID, targetID, targetID, ownerID); err != nil {
			return err
		}
		if _, err = tx.ExecContext(ctx, `DELETE FROM friend_requests WHERE (requester_id=? AND target_id=?) OR (requester_id=? AND target_id=?)`, ownerID, targetID, targetID, ownerID); err != nil {
			return err
		}
	case 2:
		if _, err = tx.ExecContext(ctx, `INSERT OR IGNORE INTO friend_blocks(player_id,blocked_id,created_at) VALUES(?,?,?)`, ownerID, targetID, now); err != nil {
			return err
		}
		if _, err = tx.ExecContext(ctx, `DELETE FROM friend_requests WHERE (requester_id=? AND target_id=?) OR (requester_id=? AND target_id=?)`, ownerID, targetID, targetID, ownerID); err != nil {
			return err
		}
	case 3:
		if _, err = tx.ExecContext(ctx, `DELETE FROM friend_blocks WHERE player_id=? AND blocked_id=?`, ownerID, targetID); err != nil {
			return err
		}
	default:
		return ErrFriendOperationInvalid
	}
	return tx.Commit()
}

func (s *Store) IsFriendBlocked(ctx context.Context, a, b int64) (bool, error) {
	var count int
	err := s.db.QueryRowContext(ctx, `SELECT COUNT(*) FROM friend_blocks WHERE (player_id=? AND blocked_id=?) OR (player_id=? AND blocked_id=?)`, a, b, b, a).Scan(&count)
	return count > 0, err
}

func (s *Store) ListBlockedPlayers(ctx context.Context, ownerID int64) ([]BlockedPlayer, error) {
	rows, err := s.db.QueryContext(ctx, `SELECT p.id,p.account_id,p.nick,p.level,p.exp,COALESCE(p.slot,0),COALESCE(p.node_id,0),p.gold,p.hp,COALESCE(p.room_id,0),b.created_at FROM friend_blocks b JOIN players p ON p.id=b.blocked_id WHERE b.player_id=? ORDER BY b.created_at DESC,b.blocked_id`, ownerID)
	if err != nil {
		return nil, err
	}
	defer rows.Close()
	var result []BlockedPlayer
	for rows.Next() {
		var entry BlockedPlayer
		if err = rows.Scan(&entry.Player.ID, &entry.Player.AccountID, &entry.Player.Nick, &entry.Player.Level, &entry.Player.Exp,
			&entry.Player.Slot, &entry.Player.NodeID, &entry.Player.Gold, &entry.Player.HP, &entry.Player.RoomID, &entry.CreatedAt); err != nil {
			return nil, err
		}
		result = append(result, entry)
	}
	return result, rows.Err()
}
