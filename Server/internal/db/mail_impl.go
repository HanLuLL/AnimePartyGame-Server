package db

import (
	"context"
	"database/sql"
	"encoding/json"
	"errors"
	"fmt"
	"time"
)

// MailRecord is the storage view of one in-game mail row.
type MailRecord struct {
	ID         int32
	Title      string
	Context    string
	SendName   string
	IsRead     bool
	IsStar     bool
	IsRewarded bool
	Rewards    map[int32]int32
	CreatedAt  int64
	ExpireAt   int64
	StartTime  int64
}

// ErrMailNotFound is returned when a mail id does not exist for the player.
var ErrMailNotFound = errors.New("mail not found")

const mailDefaultExpireDays = 30

// NextMailID returns the next globally unique mail id. The mail table uses
// (player_id, mail_id) as its primary key, but the client renders the id and
// matches MailAddS2C pushes against the snapshot, so ids must not collide
// across players.
func (s *Store) NextMailID(ctx context.Context) (int32, error) {
	var maxID sql.NullInt64
	if err := s.db.QueryRowContext(ctx, `SELECT MAX(mail_id) FROM mail`).Scan(&maxID); err != nil {
		return 0, err
	}
	if maxID.Int64 >= int64(^uint32(0)>>1)-1 {
		return 0, errors.New("mail id space exhausted")
	}
	return int32(maxID.Int64) + 1, nil
}

// ListMails returns every non-expired mail for a player, newest first.
func (s *Store) ListMails(ctx context.Context, playerID int64) ([]MailRecord, error) {
	rows, err := s.db.QueryContext(ctx, `SELECT mail_id,title,context,is_read,is_star,is_rewarded,rewards_json,created_at,expire_at,start_time FROM mail WHERE player_id=? ORDER BY mail_id DESC`, playerID)
	if err != nil {
		return nil, err
	}
	defer rows.Close()
	out := make([]MailRecord, 0, 8)
	for rows.Next() {
		var rec MailRecord
		var read, star, rewarded int
		var expire, start sql.NullInt64
		var rewards string
		if err = rows.Scan(&rec.ID, &rec.Title, &rec.Context, &read, &star, &rewarded, &rewards, &rec.CreatedAt, &expire, &start); err != nil {
			return nil, err
		}
		rec.IsRead, rec.IsStar, rec.IsRewarded = read != 0, star != 0, rewarded != 0
		rec.Rewards = decodeMailRewards(rewards)
		rec.ExpireAt, rec.StartTime = expire.Int64, start.Int64
		out = append(out, rec)
	}
	return out, rows.Err()
}

func decodeMailRewards(raw string) map[int32]int32 {
	rewards := map[int32]int32{}
	if raw == "" {
		return rewards
	}
	var decoded map[string]int32
	if err := json.Unmarshal([]byte(raw), &decoded); err != nil {
		return rewards
	}
	for key, count := range decoded {
		var id int32
		if _, err := fmt.Sscan(key, &id); err == nil {
			rewards[id] = count
		}
	}
	return rewards
}

func encodeMailRewards(rewards map[int32]int32) string {
	if len(rewards) == 0 {
		return "{}"
	}
	b, err := json.Marshal(rewards)
	if err != nil {
		return "{}"
	}
	return string(b)
}

// InsertMail stores one mail for a player and returns the assigned mail id.
// When rec.ID is zero a new global id is allocated.
func (s *Store) InsertMail(ctx context.Context, playerID int64, rec MailRecord) (int32, error) {
	if rec.CreatedAt == 0 {
		rec.CreatedAt = time.Now().Unix()
	}
	if rec.ExpireAt == 0 {
		rec.ExpireAt = rec.CreatedAt + mailDefaultExpireDays*86400
	}
	id := rec.ID
	if id <= 0 {
		var err error
		if id, err = s.NextMailID(ctx); err != nil {
			return 0, err
		}
	}
	_, err := s.db.ExecContext(ctx,
		`INSERT INTO mail(player_id,mail_id,title,context,is_read,is_star,is_rewarded,rewards_json,created_at,expire_at,start_time)
		 VALUES(?,?,?,?,0,0,0,?,?,?,?)`,
		playerID, id, rec.Title, rec.Context, encodeMailRewards(rec.Rewards), rec.CreatedAt, rec.ExpireAt, rec.StartTime)
	if err != nil {
		return 0, err
	}
	return id, nil
}

// ClaimMailRewards marks a mail rewarded, credits its rewards into the
// inventory and returns what was granted, all inside one transaction.
func (s *Store) ClaimMailRewards(ctx context.Context, playerID int64, id int32) (map[int32]int32, error) {
	tx, err := s.db.BeginTx(ctx, nil)
	if err != nil {
		return nil, err
	}
	defer tx.Rollback()
	var rewarded int
	var rewards string
	if err = tx.QueryRowContext(ctx, `SELECT is_rewarded,rewards_json FROM mail WHERE player_id=? AND mail_id=?`, playerID, id).Scan(&rewarded, &rewards); err != nil {
		if errors.Is(err, sql.ErrNoRows) {
			return nil, ErrMailNotFound
		}
		return nil, err
	}
	if rewarded != 0 {
		return nil, errors.New("mail already rewarded")
	}
	granted := decodeMailRewards(rewards)
	now := time.Now().Unix()
	for itemID, count := range granted {
		if count <= 0 {
			continue
		}
		if _, err = tx.ExecContext(ctx, `INSERT INTO inventory(player_id,item_id,count,updated_at) VALUES(?,?,?,?)
			ON CONFLICT(player_id,item_id) DO UPDATE SET count=count+excluded.count,updated_at=excluded.updated_at`,
			playerID, itemID, count, now); err != nil {
			return nil, err
		}
	}
	if _, err = tx.ExecContext(ctx, `UPDATE mail SET is_rewarded=1,is_read=1 WHERE player_id=? AND mail_id=?`, playerID, id); err != nil {
		return nil, err
	}
	return granted, tx.Commit()
}

// PlayerIDs lists every player id, used by the admin broadcast-mail sender.
func (s *Store) PlayerIDs(ctx context.Context) ([]int64, error) {
	rows, err := s.db.QueryContext(ctx, `SELECT id FROM players ORDER BY id`)
	if err != nil {
		return nil, err
	}
	defer rows.Close()
	var out []int64
	for rows.Next() {
		var id int64
		if err = rows.Scan(&id); err != nil {
			return nil, err
		}
		out = append(out, id)
	}
	return out, rows.Err()
}

// FriendSnapshot returns friend ids, note text per friend and blocked ids for
// the ConnectS2C FriendList snapshot.
func (s *Store) FriendSnapshot(ctx context.Context, playerID int64) (ids []int64, notes map[int64]string, blacks []int64, err error) {
	ids, err = s.ListFriends(ctx, playerID)
	if err != nil {
		return nil, nil, nil, err
	}
	rows, err := s.db.QueryContext(ctx, `SELECT friend_id,note FROM friendships WHERE player_id=? AND state=1 AND note<>''`, playerID)
	if err != nil {
		return nil, nil, nil, err
	}
	defer rows.Close()
	notes = map[int64]string{}
	for rows.Next() {
		var friend int64
		var note string
		if err = rows.Scan(&friend, &note); err != nil {
			return nil, nil, nil, err
		}
		notes[friend] = note
	}
	if err = rows.Err(); err != nil {
		return nil, nil, nil, err
	}
	blocked, err := s.ListBlockedPlayers(ctx, playerID)
	if err != nil {
		return nil, nil, nil, err
	}
	blacks = make([]int64, 0, len(blocked))
	for _, entry := range blocked {
		blacks = append(blacks, entry.Player.ID)
	}
	return ids, notes, blacks, nil
}
