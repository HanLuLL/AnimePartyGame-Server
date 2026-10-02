package db

import (
	"context"
	"errors"
)

var ErrPrivateChatHistoryInvalid = errors.New("invalid private chat history target")

// ClearPrivateChatHistory hides the current conversation for the requesting
// player without deleting the other participant's copy or future messages.
func (s *Store) ClearPrivateChatHistory(ctx context.Context, ownerID, targetID int64) error {
	if ownerID <= 0 || targetID <= 0 || ownerID == targetID {
		return ErrPrivateChatHistoryInvalid
	}
	tx, err := s.db.BeginTx(ctx, nil)
	if err != nil {
		return err
	}
	defer tx.Rollback()
	var players int
	if err = tx.QueryRowContext(ctx, `SELECT COUNT(*) FROM players WHERE id IN (?,?)`, ownerID, targetID).Scan(&players); err != nil {
		return err
	}
	if players != 2 {
		return ErrPlayerNotFound
	}
	var throughID int64
	if err = tx.QueryRowContext(ctx, `SELECT COALESCE(MAX(id),0) FROM chat_messages
		WHERE room_id=0 AND ((from_player=? AND to_player=?) OR (from_player=? AND to_player=?))`, ownerID, targetID, targetID, ownerID).Scan(&throughID); err != nil {
		return err
	}
	if _, err = tx.ExecContext(ctx, `INSERT INTO chat_history_cursors(owner_id,target_player_id,deleted_through_id)
		VALUES(?,?,?) ON CONFLICT(owner_id,target_player_id) DO UPDATE
		SET deleted_through_id=MAX(chat_history_cursors.deleted_through_id,excluded.deleted_through_id)`, ownerID, targetID, throughID); err != nil {
		return err
	}
	return tx.Commit()
}
