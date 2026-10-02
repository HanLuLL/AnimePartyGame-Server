package db

import (
	"context"
	"errors"
)

func (s *Store) PlayerWebMustChangePassword(ctx context.Context, accountID int64) (bool, error) {
	var exists int
	err := s.db.QueryRowContext(ctx, `SELECT EXISTS(SELECT 1 FROM self_web_first_change WHERE account_id=?)`, accountID).Scan(&exists)
	return exists != 0, err
}

func (s *Store) PlayerWebMustChangePasswordBySession(ctx context.Context, tokenHash string, now int64) (bool, error) {
	var mustChange int
	err := s.db.QueryRowContext(ctx, `SELECT EXISTS(SELECT 1 FROM auth_sessions s JOIN self_web_first_change g ON g.account_id=s.account_id WHERE s.token_hash=? AND s.revoked_at=0 AND s.expires_at>? )`, tokenHash, now).Scan(&mustChange)
	return mustChange != 0, err
}

func (s *Store) PlayerWebCredentialByPhone(ctx context.Context, phone string) (EmailAccount, string, error) {
	var account EmailAccount
	var disabled int
	var passwordHash string
	err := s.db.QueryRowContext(ctx, `SELECT id,account_no,email,phone,nick,disabled,COALESCE(web_password_hash,'') FROM accounts WHERE phone=? AND platform='self'`, phone).Scan(&account.ID, &account.Number, &account.Email, &account.Phone, &account.Nick, &disabled, &passwordHash)
	if err != nil {
		return EmailAccount{}, "", err
	}
	account.Disabled = disabled != 0
	return account, passwordHash, nil
}

func (s *Store) UpdatePlayerWebPassword(ctx context.Context, accountID int64, passwordHash string, now int64) error {
	tx, err := s.db.BeginTx(ctx, nil)
	if err != nil {
		return err
	}
	defer tx.Rollback()
	result, err := tx.ExecContext(ctx, `UPDATE accounts SET web_password_hash=?,last_login_at=? WHERE id=? AND platform='self' AND phone<>''`, passwordHash, now, accountID)
	if err != nil {
		return err
	}
	count, err := result.RowsAffected()
	if err != nil {
		return err
	}
	if count != 1 {
		return errors.New("player web account not found")
	}
	if _, err = tx.ExecContext(ctx, `DELETE FROM self_web_first_change WHERE account_id=?`, accountID); err != nil {
		return err
	}
	if _, err = tx.ExecContext(ctx, `UPDATE auth_sessions SET revoked_at=? WHERE account_id=? AND revoked_at=0`, now, accountID); err != nil {
		return err
	}
	return tx.Commit()
}
