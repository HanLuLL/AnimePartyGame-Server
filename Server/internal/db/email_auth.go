package db

import (
	"context"
	"crypto/rand"
	"crypto/sha256"
	"crypto/subtle"
	"database/sql"
	"encoding/hex"
	"encoding/json"
	"errors"
	"fmt"
	"math/big"
	"time"
)

var (
	ErrEmailRateLimited        = errors.New("email verification rate limit exceeded")
	ErrEmailCodeInvalid        = errors.New("email verification code is invalid or expired")
	ErrEmailSession            = errors.New("email session is invalid or expired")
	ErrEmailHandoff            = errors.New("email handoff is invalid or expired")
	ErrHandoffRateLimit        = errors.New("email handoff rate limit exceeded")
	ErrAccountDisabled         = errors.New("account is disabled")
	ErrGamePlayerNotRegistered = errors.New("game player is not registered")
)

type EmailAccount struct {
	ID        int64
	Number    string
	Email     string
	Phone     string
	Nick      string
	PlayerID  int64
	ExpiresAt int64
	Disabled  bool
}

// IssueEmailCode persists only keyed digests of the one-time code and optional
// SDK phone token. Only the newest challenge for an email remains usable. The
// caller sends the message after this succeeds and invalidates the code if SMTP
// delivery fails.
func (s *Store) IssueEmailCode(ctx context.Context, email, codeHash, sdkTokenHash, ipHash string, now, expiresAt int64) error {
	tx, err := s.db.BeginTx(ctx, nil)
	if err != nil {
		return err
	}
	defer tx.Rollback()

	var last int64
	if err := tx.QueryRowContext(ctx, `SELECT COALESCE(MAX(created_at),0) FROM auth_email_codes WHERE email=?`, email).Scan(&last); err != nil {
		return err
	}
	if last > 0 && now-last < 60 {
		return ErrEmailRateLimited
	}
	var emailCount int
	if err := tx.QueryRowContext(ctx, `SELECT COUNT(*) FROM auth_email_codes WHERE email=? AND created_at>?`, email, now-3600).Scan(&emailCount); err != nil {
		return err
	}
	if emailCount >= 5 {
		return ErrEmailRateLimited
	}
	if ipHash != "" {
		var ipCount int
		if err := tx.QueryRowContext(ctx, `SELECT COUNT(*) FROM auth_email_codes WHERE ip_hash=? AND created_at>?`, ipHash, now-3600).Scan(&ipCount); err != nil {
			return err
		}
		if ipCount >= 20 {
			return ErrEmailRateLimited
		}
	}
	if _, err := tx.ExecContext(ctx, `UPDATE auth_email_codes SET consumed_at=? WHERE email=? AND consumed_at=0`, now, email); err != nil {
		return err
	}
	if _, err := tx.ExecContext(ctx, `INSERT INTO auth_email_codes(email,code_hash,sdk_token_hash,ip_hash,expires_at,created_at) VALUES(?,?,?,?,?,?)`, email, codeHash, sdkTokenHash, ipHash, expiresAt, now); err != nil {
		return err
	}
	if _, err := tx.ExecContext(ctx, `DELETE FROM auth_email_codes WHERE created_at<?`, now-7*24*3600); err != nil {
		return err
	}
	return tx.Commit()
}

func (s *Store) InvalidateEmailCode(ctx context.Context, email, codeHash string, now int64) error {
	_, err := s.db.ExecContext(ctx, `UPDATE auth_email_codes SET consumed_at=? WHERE email=? AND code_hash=? AND consumed_at=0`, now, email, codeHash)
	return err
}

// ConsumeEmailCode atomically validates an OTP and optional SDK phone token,
// creates an email account on first successful verification, and optionally
// issues a short-lived internal handoff for session creation.
func (s *Store) ConsumeEmailCode(ctx context.Context, email, codeHash, handoffHash, deviceID, sdkTokenHash string, now, handoffExpiresAt int64) (EmailAccount, error) {
	tx, err := s.db.BeginTx(ctx, nil)
	if err != nil {
		return EmailAccount{}, err
	}
	defer tx.Rollback()

	var codeID, codeExpires int64
	var storedHash, storedSDKTokenHash string
	var attempts int
	err = tx.QueryRowContext(ctx, `SELECT id,code_hash,sdk_token_hash,expires_at,attempts FROM auth_email_codes WHERE email=? AND consumed_at=0 ORDER BY id DESC LIMIT 1`, email).Scan(&codeID, &storedHash, &storedSDKTokenHash, &codeExpires, &attempts)
	if errors.Is(err, sql.ErrNoRows) {
		return EmailAccount{}, ErrEmailCodeInvalid
	}
	if err != nil {
		return EmailAccount{}, err
	}
	if now >= codeExpires || attempts >= 5 {
		_, _ = tx.ExecContext(ctx, `UPDATE auth_email_codes SET consumed_at=? WHERE id=?`, now, codeID)
		if err := tx.Commit(); err != nil {
			return EmailAccount{}, err
		}
		return EmailAccount{}, ErrEmailCodeInvalid
	}
	codeMatches := subtle.ConstantTimeCompare([]byte(storedHash), []byte(codeHash)) == 1
	tokenMatches := sdkTokenHash == "" || (storedSDKTokenHash != "" && subtle.ConstantTimeCompare([]byte(storedSDKTokenHash), []byte(sdkTokenHash)) == 1)
	if !codeMatches || !tokenMatches {
		attempts++
		consumedAt := int64(0)
		if attempts >= 5 {
			consumedAt = now
		}
		if _, err := tx.ExecContext(ctx, `UPDATE auth_email_codes SET attempts=?,consumed_at=? WHERE id=?`, attempts, consumedAt, codeID); err != nil {
			return EmailAccount{}, err
		}
		if err := tx.Commit(); err != nil {
			return EmailAccount{}, err
		}
		return EmailAccount{}, ErrEmailCodeInvalid
	}
	if _, err := tx.ExecContext(ctx, `UPDATE auth_email_codes SET consumed_at=? WHERE id=? AND consumed_at=0`, now, codeID); err != nil {
		return EmailAccount{}, err
	}

	var account EmailAccount
	var disabled int
	err = tx.QueryRowContext(ctx, `SELECT id,account_no,email,phone,nick,disabled FROM accounts WHERE email=?`, email).Scan(&account.ID, &account.Number, &account.Email, &account.Phone, &account.Nick, &disabled)
	account.Disabled = disabled != 0
	if errors.Is(err, sql.ErrNoRows) {
		for attempt := 0; attempt < 8; attempt++ {
			accountNo, err := randomAccountNumber()
			if err != nil {
				return EmailAccount{}, err
			}
			phone, err := randomVirtualPhone()
			if err != nil {
				return EmailAccount{}, err
			}
			nick := "Astral" + accountNo[len(accountNo)-5:]
			res, insertErr := tx.ExecContext(ctx, `INSERT OR IGNORE INTO accounts(login_key,platform,nick,device_id,token,email,phone,account_no,created_at,last_login_at) VALUES(?,?,?,?,?,?,?,?,?,?)`, "email:"+email, "self", nick, "", "", email, phone, accountNo, now, now)
			if insertErr != nil {
				return EmailAccount{}, insertErr
			}
			n, _ := res.RowsAffected()
			if n == 1 {
				err = tx.QueryRowContext(ctx, `SELECT id,account_no,email,phone,nick,disabled FROM accounts WHERE email=?`, email).Scan(&account.ID, &account.Number, &account.Email, &account.Phone, &account.Nick, &disabled)
				account.Disabled = disabled != 0
				break
			}
		}
	}
	if err != nil {
		return EmailAccount{}, err
	}
	if account.ID == 0 {
		return EmailAccount{}, errors.New("could not allocate account number")
	}
	if account.Disabled {
		return EmailAccount{}, ErrAccountDisabled
	}
	if account.Phone == "" {
		for attempt := 0; attempt < 8; attempt++ {
			phone, err := randomVirtualPhone()
			if err != nil {
				return EmailAccount{}, err
			}
			if _, err = tx.ExecContext(ctx, `UPDATE OR IGNORE accounts SET phone=? WHERE id=? AND phone=''`, phone, account.ID); err != nil {
				return EmailAccount{}, err
			}
			if err = tx.QueryRowContext(ctx, `SELECT phone FROM accounts WHERE id=?`, account.ID).Scan(&account.Phone); err != nil {
				return EmailAccount{}, err
			}
			if account.Phone != "" {
				break
			}
		}
		if account.Phone == "" {
			return EmailAccount{}, errors.New("could not allocate virtual phone number")
		}
	}
	if handoffHash != "" {
		if _, err := tx.ExecContext(ctx, `INSERT INTO auth_handoffs(code_hash,account_id,device_id,expires_at,created_at) VALUES(?,?,?,?,?)`, handoffHash, account.ID, deviceID, handoffExpiresAt, now); err != nil {
			return EmailAccount{}, err
		}
		_, _ = tx.ExecContext(ctx, `DELETE FROM auth_handoffs WHERE expires_at<? OR consumed_at>0 AND created_at<?`, now, now-7*24*3600)
	}
	if err := tx.Commit(); err != nil {
		return EmailAccount{}, err
	}
	account.ExpiresAt = handoffExpiresAt
	return account, nil
}

// RedeemEmailHandoff consumes a short-lived, server-internal grant created only
// after a valid email OTP and creates the game session atomically. The native
// registration endpoint may also create the player's initial profile; login
// requires that profile to already exist. The grant's keyed digest is never
// sent to the browser or game client.
func (s *Store) RedeemEmailHandoff(ctx context.Context, codeHash, deviceID, tokenHash, ipHash string, now, expiresAt int64, allowGameRegistration bool) (EmailAccount, error) {
	tx, err := s.db.BeginTx(ctx, nil)
	if err != nil {
		return EmailAccount{}, err
	}
	defer tx.Rollback()

	if ipHash != "" {
		_, err = tx.ExecContext(ctx, `INSERT INTO auth_handoff_limits(ip_hash,window_started_at,attempts) VALUES(?,?,1)
			ON CONFLICT(ip_hash) DO UPDATE SET
			attempts=CASE WHEN auth_handoff_limits.window_started_at<=? THEN 1 ELSE auth_handoff_limits.attempts+1 END,
			window_started_at=CASE WHEN auth_handoff_limits.window_started_at<=? THEN excluded.window_started_at ELSE auth_handoff_limits.window_started_at END`,
			ipHash, now, now-3600, now-3600)
		if err != nil {
			return EmailAccount{}, err
		}
		var attempts int
		if err = tx.QueryRowContext(ctx, `SELECT attempts FROM auth_handoff_limits WHERE ip_hash=?`, ipHash).Scan(&attempts); err != nil {
			return EmailAccount{}, err
		}
		if attempts > 30 {
			if err = tx.Commit(); err != nil {
				return EmailAccount{}, err
			}
			return EmailAccount{}, ErrHandoffRateLimit
		}
	}

	var accountID, handoffExpiresAt, consumedAt int64
	var boundDeviceID string
	err = tx.QueryRowContext(ctx, `SELECT account_id,device_id,expires_at,consumed_at FROM auth_handoffs WHERE code_hash=?`, codeHash).
		Scan(&accountID, &boundDeviceID, &handoffExpiresAt, &consumedAt)
	if errors.Is(err, sql.ErrNoRows) || (err == nil && (consumedAt != 0 || now >= handoffExpiresAt || (boundDeviceID != "" && boundDeviceID != deviceID))) {
		if err = tx.Commit(); err != nil {
			return EmailAccount{}, err
		}
		return EmailAccount{}, ErrEmailHandoff
	}
	if err != nil {
		return EmailAccount{}, err
	}

	var disabled int
	var account EmailAccount
	err = tx.QueryRowContext(ctx, `SELECT id,account_no,email,phone,nick,disabled FROM accounts WHERE id=?`, accountID).
		Scan(&account.ID, &account.Number, &account.Email, &account.Phone, &account.Nick, &disabled)
	if err != nil {
		return EmailAccount{}, err
	}
	account.Disabled = disabled != 0
	if account.Disabled {
		if err = tx.Commit(); err != nil {
			return EmailAccount{}, err
		}
		return EmailAccount{}, ErrAccountDisabled
	}
	var playerID int64
	playerErr := tx.QueryRowContext(ctx, `SELECT id FROM players WHERE account_id=?`, account.ID).Scan(&playerID)
	playerExists := playerErr == nil
	if playerErr != nil && !errors.Is(playerErr, sql.ErrNoRows) {
		return EmailAccount{}, playerErr
	}
	if !playerExists && !allowGameRegistration {
		return EmailAccount{}, ErrGamePlayerNotRegistered
	}
	result, err := tx.ExecContext(ctx, `UPDATE auth_handoffs SET consumed_at=? WHERE code_hash=? AND consumed_at=0 AND expires_at>?`, now, codeHash, now)
	if err != nil {
		return EmailAccount{}, err
	}
	updated, err := result.RowsAffected()
	if err != nil {
		return EmailAccount{}, err
	}
	if updated == 0 {
		if err = tx.Commit(); err != nil {
			return EmailAccount{}, err
		}
		return EmailAccount{}, ErrEmailHandoff
	}
	if _, err = tx.ExecContext(ctx, `INSERT INTO auth_sessions(token_hash,account_id,expires_at,created_at) VALUES(?,?,?,?)`, tokenHash, account.ID, expiresAt, now); err != nil {
		return EmailAccount{}, err
	}
	createdPlayer := false
	if !playerExists {
		playerResult, insertErr := tx.ExecContext(ctx, `INSERT OR IGNORE INTO players(account_id,nick,created_at) VALUES(?,?,?)`, account.ID, account.Nick, now)
		if insertErr != nil {
			return EmailAccount{}, insertErr
		}
		createdPlayers, rowsErr := playerResult.RowsAffected()
		if rowsErr != nil {
			return EmailAccount{}, rowsErr
		}
		createdPlayer = createdPlayers > 0
		if err = tx.QueryRowContext(ctx, `SELECT id FROM players WHERE account_id=?`, account.ID).Scan(&playerID); err != nil {
			return EmailAccount{}, err
		}
	}
	if createdPlayer {
		// Fresh private-server accounts start at Home. The client otherwise
		// launches its first-run tutorial whenever task condition 1 is absent.
		if _, err = tx.ExecContext(ctx, `INSERT OR IGNORE INTO task_counters(player_id,condition_type,period_key,progress,updated_at) VALUES(?,1,'all',1,?)`, playerID, now); err != nil {
			return EmailAccount{}, err
		}
	}
	if _, err = tx.ExecContext(ctx, `UPDATE accounts SET device_id=?,last_login_at=? WHERE id=?`, deviceID, now, account.ID); err != nil {
		return EmailAccount{}, err
	}
	account.PlayerID = playerID
	if _, err = tx.ExecContext(ctx, `DELETE FROM auth_handoff_limits WHERE window_started_at<?`, now-7*24*3600); err != nil {
		return EmailAccount{}, err
	}
	if err = tx.Commit(); err != nil {
		return EmailAccount{}, err
	}
	account.ExpiresAt = expiresAt
	return account, nil
}

var ErrPlayerWebPasswordChangeRequired = errors.New("player must change web password before game login")

func (s *Store) ResolveSelfSession(ctx context.Context, tokenHash string, now int64) (EmailAccount, error) {
	var a EmailAccount
	err := s.db.QueryRowContext(ctx, `SELECT a.id,a.account_no,a.email,a.phone,a.nick,s.expires_at,a.disabled FROM auth_sessions s JOIN accounts a ON a.id=s.account_id WHERE s.token_hash=? AND s.revoked_at=0 AND s.expires_at>?`, tokenHash, now).Scan(&a.ID, &a.Number, &a.Email, &a.Phone, &a.Nick, &a.ExpiresAt, &a.Disabled)
	if errors.Is(err, sql.ErrNoRows) {
		return EmailAccount{}, ErrEmailSession
	}
	return a, err
}

func (s *Store) ResolveGameSelfSession(ctx context.Context, tokenHash string, now int64) (EmailAccount, error) {
	account, err := s.ResolveSelfSession(ctx, tokenHash, now)
	if err != nil {
		return EmailAccount{}, err
	}
	var mustChange int
	if err = s.db.QueryRowContext(ctx, `SELECT EXISTS(SELECT 1 FROM self_web_first_change WHERE account_id=?)`, account.ID).Scan(&mustChange); err != nil {
		return EmailAccount{}, err
	}
	if mustChange != 0 {
		return EmailAccount{}, ErrPlayerWebPasswordChangeRequired
	}
	return account, nil
}

func (s *Store) ResolveSelfAccountByPhone(ctx context.Context, phone string) (EmailAccount, error) {
	var account EmailAccount
	var disabled int
	err := s.db.QueryRowContext(ctx, `SELECT id,account_no,email,phone,nick,disabled FROM accounts WHERE phone=? AND platform='self'`, phone).
		Scan(&account.ID, &account.Number, &account.Email, &account.Phone, &account.Nick, &disabled)
	if errors.Is(err, sql.ErrNoRows) {
		return EmailAccount{}, ErrEmailSession
	}
	account.Disabled = disabled != 0
	return account, err
}

func (s *Store) HasSelfGamePlayer(ctx context.Context, accountID int64) (bool, error) {
	var exists int
	err := s.db.QueryRowContext(ctx, `SELECT EXISTS(SELECT 1 FROM players WHERE account_id=?)`, accountID).Scan(&exists)
	return exists != 0, err
}

func (s *Store) LoadSelfPlayer(ctx context.Context, accountID int64, deviceID string, now int64) (Player, error) {
	tx, err := s.db.BeginTx(ctx, nil)
	if err != nil {
		return Player{}, err
	}
	defer tx.Rollback()
	var nick string
	if err := tx.QueryRowContext(ctx, `SELECT nick FROM accounts WHERE id=? AND platform='self' AND (email<>'' OR phone<>'')`, accountID).Scan(&nick); err != nil {
		return Player{}, err
	}
	if _, err := tx.ExecContext(ctx, `INSERT OR IGNORE INTO players(account_id,nick,created_at) VALUES(?,?,?)`, accountID, nick, now); err != nil {
		return Player{}, err
	}
	if _, err := tx.ExecContext(ctx, `UPDATE accounts SET device_id=?,last_login_at=? WHERE id=?`, deviceID, now, accountID); err != nil {
		return Player{}, err
	}
	var p Player
	var lotteriesJSON, frontJSON string
	err = tx.QueryRowContext(ctx, `SELECT id,account_id,nick,level,exp,COALESCE(slot,0),COALESCE(node_id,0),COALESCE(back_node_id,0),front_node_ids_json,gold,hp,COALESCE(room_id,0),lotterys_json FROM players WHERE account_id=?`, accountID).Scan(&p.ID, &p.AccountID, &p.Nick, &p.Level, &p.Exp, &p.Slot, &p.NodeID, &p.BackNodeID, &frontJSON, &p.Gold, &p.HP, &p.RoomID, &lotteriesJSON)
	if err != nil {
		return Player{}, err
	}
	if err = json.Unmarshal([]byte(frontJSON), &p.FrontNodeIDs); err != nil {
		return Player{}, fmt.Errorf("decode front land IDs for player %d: %w", p.ID, err)
	}
	if err = json.Unmarshal([]byte(lotteriesJSON), &p.Lotterys); err != nil {
		return Player{}, fmt.Errorf("decode lottery choices for player %d: %w", p.ID, err)
	}
	if p.Lotterys == nil {
		p.Lotterys = map[int32]bool{}
	}
	if p.RoomID != 0 {
		_ = tx.QueryRowContext(ctx, `SELECT ready FROM room_members WHERE room_id=? AND player_id=?`, p.RoomID, p.ID).Scan(&p.Ready)
	}
	if err := tx.Commit(); err != nil {
		return Player{}, err
	}
	return p, nil
}

func (s *Store) RevokeEmailSession(ctx context.Context, tokenHash string, now int64) error {
	_, err := s.db.ExecContext(ctx, `UPDATE auth_sessions SET revoked_at=? WHERE token_hash=? AND revoked_at=0`, now, tokenHash)
	return err
}

func randomAccountNumber() (string, error) {
	n, err := rand.Int(rand.Reader, big.NewInt(9_000_000_000))
	if err != nil {
		return "", err
	}
	return fmt.Sprintf("%010d", n.Int64()+1_000_000_000), nil
}

func randomVirtualPhone() (string, error) {
	n, err := rand.Int(rand.Reader, big.NewInt(100_000_000))
	if err != nil {
		return "", err
	}
	return fmt.Sprintf("199%08d", n.Int64()), nil
}

func SessionTokenHash(token string) string {
	// The token has 256 bits of entropy, so an unkeyed SHA-256 digest is
	// sufficient for at-rest lookup while keeping the bearer secret out of SQL.
	h := sha256.Sum256([]byte(token))
	return hex.EncodeToString(h[:])
}

// EnsurePhoneAccount resolves or creates a phone-first account. The phone
// number itself is the credential: no email and no OTP are involved. New
// accounts receive a player and the tutorial-complete counter so the client
// enters Home directly.
func (s *Store) EnsurePhoneAccount(ctx context.Context, phone, deviceID string, now int64) (EmailAccount, error) {
	tx, err := s.db.BeginTx(ctx, nil)
	if err != nil {
		return EmailAccount{}, err
	}
	defer tx.Rollback()
	var a EmailAccount
	var disabled int
	err = tx.QueryRowContext(ctx,
		`SELECT id,nick,phone,account_no,disabled FROM accounts WHERE phone=? OR login_key=?`,
		phone, "phone:"+phone).Scan(&a.ID, &a.Nick, &a.Phone, &a.Number, &disabled)
	if errors.Is(err, sql.ErrNoRows) {
		nick := "Astral" + phone[len(phone)-5:]
		accountNo, numErr := randomAccountNumber()
		if numErr != nil {
			return EmailAccount{}, numErr
		}
		res, insertErr := tx.ExecContext(ctx,
			`INSERT INTO accounts(login_key,platform,nick,device_id,phone,account_no,created_at,last_login_at) VALUES(?,?,?,?,?,?,?,?)`,
			"phone:"+phone, "self", nick, deviceID, phone, accountNo, now, now)
		if insertErr != nil {
			return EmailAccount{}, insertErr
		}
		newID, idErr := res.LastInsertId()
		if idErr != nil {
			return EmailAccount{}, idErr
		}
		a = EmailAccount{ID: newID, Number: accountNo, Phone: phone, Nick: nick}
	} else if err != nil {
		return EmailAccount{}, err
	} else {
		if disabled != 0 {
			return EmailAccount{}, ErrAccountDisabled
		}
		if _, err = tx.ExecContext(ctx, `UPDATE accounts SET device_id=?,last_login_at=? WHERE id=?`, deviceID, now, a.ID); err != nil {
			return EmailAccount{}, err
		}
	}
	var playerID int64
	playerErr := tx.QueryRowContext(ctx, `SELECT id FROM players WHERE account_id=?`, a.ID).Scan(&playerID)
	if errors.Is(playerErr, sql.ErrNoRows) {
		if _, err = tx.ExecContext(ctx, `INSERT OR IGNORE INTO players(account_id,nick,created_at) VALUES(?,?,?)`, a.ID, a.Nick, now); err != nil {
			return EmailAccount{}, err
		}
		if err = tx.QueryRowContext(ctx, `SELECT id FROM players WHERE account_id=?`, a.ID).Scan(&playerID); err != nil {
			return EmailAccount{}, err
		}
		if _, err = tx.ExecContext(ctx, `INSERT OR IGNORE INTO task_counters(player_id,condition_type,period_key,progress,updated_at) VALUES(?,1,'all',1,?)`, playerID, now); err != nil {
			return EmailAccount{}, err
		}
	} else if playerErr != nil {
		return EmailAccount{}, playerErr
	}
	a.PlayerID = playerID
	if err = tx.Commit(); err != nil {
		return EmailAccount{}, err
	}
	return a, nil
}

// IssueSelfSession stores a bearer session for a signed-in account.
func (s *Store) IssueSelfSession(ctx context.Context, accountID int64, tokenHash string, now, expiresAt int64) error {
	_, err := s.db.ExecContext(ctx, `INSERT INTO auth_sessions(token_hash,account_id,expires_at,created_at) VALUES(?,?,?,?)`, tokenHash, accountID, expiresAt, now)
	return err
}

func (s *Store) SetPlayerDisabled(ctx context.Context, playerID int64, disabled bool) error {
	value := 0
	if disabled {
		value = 1
	}
	res, err := s.db.ExecContext(ctx, `UPDATE accounts SET disabled=? WHERE id=(SELECT account_id FROM players WHERE id=?)`, value, playerID)
	if err != nil {
		return err
	}
	if n, _ := res.RowsAffected(); n == 0 {
		return ErrPlayerNotFound
	}
	if disabled {
		_, err = s.db.ExecContext(ctx, `UPDATE auth_sessions SET revoked_at=? WHERE account_id=(SELECT account_id FROM players WHERE id=?) AND revoked_at=0`, time.Now().Unix(), playerID)
	}
	return err
}

func (s *Store) AccountDisabled(ctx context.Context, accountID int64) (bool, error) {
	var disabled int
	err := s.db.QueryRowContext(ctx, `SELECT disabled FROM accounts WHERE id=?`, accountID).Scan(&disabled)
	return disabled != 0, err
}

// AllPlayerIDs returns every player id, ordered ascending. Used by the
// server console's /giveall command.
func (s *Store) AllPlayerIDs(ctx context.Context) ([]int64, error) {
	rows, err := s.db.QueryContext(ctx, `SELECT id FROM players ORDER BY id`)
	if err != nil {
		return nil, err
	}
	defer rows.Close()
	var ids []int64
	for rows.Next() {
		var id int64
		if err := rows.Scan(&id); err != nil {
			return nil, err
		}
		ids = append(ids, id)
	}
	return ids, rows.Err()
}

// CountAccounts returns the number of registered accounts.
func (s *Store) CountAccounts(ctx context.Context) (int64, error) {
	var count int64
	err := s.db.QueryRowContext(ctx, `SELECT COUNT(*) FROM accounts`).Scan(&count)
	return count, err
}

func EmailSessionExpiry(now time.Time, ttl time.Duration) int64 {
	return now.Add(ttl).Unix()
}
