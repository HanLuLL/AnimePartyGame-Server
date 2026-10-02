package db

import (
	"context"
	"encoding/json"
	"errors"
	"fmt"
	"strings"
	"time"
)

// LocalizedText carries one text per client language. Empty fields fall back
// to Chinese and then English when the feed is served, so operators only need
// to fill the languages they actually support.
type LocalizedText struct {
	Cn  string `json:"cn"`
	En  string `json:"en"`
	Jp  string `json:"jp"`
	Cht string `json:"cht"`
	Ko  string `json:"ko"`
}

// Normalize returns a copy where every empty language falls back to the first
// non-empty value, so the client always renders readable text.
func (t LocalizedText) Normalize() LocalizedText {
	fallback := t.Cn
	if strings.TrimSpace(fallback) == "" {
		fallback = t.En
	}
	if strings.TrimSpace(fallback) == "" {
		fallback = t.Jp
	}
	if strings.TrimSpace(fallback) == "" {
		fallback = t.Cht
	}
	if strings.TrimSpace(fallback) == "" {
		fallback = t.Ko
	}
	out := t
	if strings.TrimSpace(out.Cn) == "" {
		out.Cn = fallback
	}
	if strings.TrimSpace(out.En) == "" {
		out.En = fallback
	}
	if strings.TrimSpace(out.Jp) == "" {
		out.Jp = fallback
	}
	if strings.TrimSpace(out.Cht) == "" {
		out.Cht = fallback
	}
	if strings.TrimSpace(out.Ko) == "" {
		out.Ko = fallback
	}
	return out
}

// Notice is one operator-managed announcement entry. Each notice renders as a
// tab in the in-game notice window with a single text item.
type Notice struct {
	ID        int64
	Enabled   bool
	SortOrder int
	Title     LocalizedText
	Content   LocalizedText
	CreatedAt int64
	UpdatedAt int64
}

// ErrNoticeNotFound is returned when a notice id does not exist.
var ErrNoticeNotFound = errors.New("notice not found")

func encodeLocalizedText(text LocalizedText) (string, error) {
	b, err := json.Marshal(text)
	if err != nil {
		return "", err
	}
	return string(b), nil
}

func decodeLocalizedText(raw string) (LocalizedText, error) {
	var text LocalizedText
	if strings.TrimSpace(raw) == "" {
		return text, nil
	}
	if err := json.Unmarshal([]byte(raw), &text); err != nil {
		return text, fmt.Errorf("decode localized text: %w", err)
	}
	return text, nil
}

type rowScanner interface{ Scan(...any) error }

func scanNotice(row rowScanner) (Notice, error) {
	var notice Notice
	var enabled int
	var titleJSON, contentJSON string
	if err := row.Scan(&notice.ID, &enabled, &notice.SortOrder, &titleJSON, &contentJSON, &notice.CreatedAt, &notice.UpdatedAt); err != nil {
		return Notice{}, err
	}
	notice.Enabled = enabled != 0
	title, err := decodeLocalizedText(titleJSON)
	if err != nil {
		return Notice{}, err
	}
	content, err := decodeLocalizedText(contentJSON)
	if err != nil {
		return Notice{}, err
	}
	notice.Title = title
	notice.Content = content
	return notice, nil
}

func (s *Store) queryNotices(ctx context.Context, where string) ([]Notice, error) {
	rows, err := s.db.QueryContext(ctx, `SELECT id,enabled,sort_order,title_json,content_json,created_at,updated_at FROM notices`+where+` ORDER BY sort_order ASC, id ASC`)
	if err != nil {
		return nil, err
	}
	defer rows.Close()
	notices := make([]Notice, 0, 8)
	for rows.Next() {
		notice, err := scanNotice(rows)
		if err != nil {
			return nil, err
		}
		notices = append(notices, notice)
	}
	return notices, rows.Err()
}

// ListNotices returns every notice ordered for the console table.
func (s *Store) ListNotices(ctx context.Context) ([]Notice, error) {
	return s.queryNotices(ctx, "")
}

// EnabledNotices returns only the notices the game client should receive.
func (s *Store) EnabledNotices(ctx context.Context) ([]Notice, error) {
	return s.queryNotices(ctx, " WHERE enabled<>0")
}

// CreateNotice inserts a notice and returns its id.
func (s *Store) CreateNotice(ctx context.Context, notice Notice) (int64, error) {
	titleJSON, err := encodeLocalizedText(notice.Title)
	if err != nil {
		return 0, err
	}
	contentJSON, err := encodeLocalizedText(notice.Content)
	if err != nil {
		return 0, err
	}
	now := time.Now().Unix()
	enabled := 0
	if notice.Enabled {
		enabled = 1
	}
	res, err := s.db.ExecContext(ctx, `INSERT INTO notices(enabled,sort_order,title_json,content_json,created_at,updated_at) VALUES(?,?,?,?,?,?)`,
		enabled, notice.SortOrder, titleJSON, contentJSON, now, now)
	if err != nil {
		return 0, err
	}
	return res.LastInsertId()
}

// UpdateNotice replaces the editable fields of an existing notice.
func (s *Store) UpdateNotice(ctx context.Context, notice Notice) error {
	titleJSON, err := encodeLocalizedText(notice.Title)
	if err != nil {
		return err
	}
	contentJSON, err := encodeLocalizedText(notice.Content)
	if err != nil {
		return err
	}
	enabled := 0
	if notice.Enabled {
		enabled = 1
	}
	res, err := s.db.ExecContext(ctx, `UPDATE notices SET enabled=?,sort_order=?,title_json=?,content_json=?,updated_at=? WHERE id=?`,
		enabled, notice.SortOrder, titleJSON, contentJSON, time.Now().Unix(), notice.ID)
	if err != nil {
		return err
	}
	affected, err := res.RowsAffected()
	if err != nil {
		return err
	}
	if affected == 0 {
		return fmt.Errorf("notice %d: %w", notice.ID, ErrNoticeNotFound)
	}
	return nil
}

// DeleteNotice removes a notice by id.
func (s *Store) DeleteNotice(ctx context.Context, id int64) error {
	res, err := s.db.ExecContext(ctx, `DELETE FROM notices WHERE id=?`, id)
	if err != nil {
		return err
	}
	affected, err := res.RowsAffected()
	if err != nil {
		return err
	}
	if affected == 0 {
		return fmt.Errorf("notice %d: %w", id, ErrNoticeNotFound)
	}
	return nil
}

// ValidateLocalizedText enforces the console input limits: every language is
// capped at 4000 bytes, titles may not contain control characters, and content
// may use newlines but no other control characters.
func ValidateLocalizedText(title, content LocalizedText) error {
	texts := []struct {
		kind  string
		text  LocalizedText
		multi bool
	}{
		{"title", title, false},
		{"content", content, true},
	}
	for _, entry := range texts {
		fields := []struct {
			name  string
			value string
		}{
			{"cn", entry.text.Cn}, {"en", entry.text.En}, {"jp", entry.text.Jp}, {"cht", entry.text.Cht}, {"ko", entry.text.Ko},
		}
		for _, field := range fields {
			if len(field.value) > 4000 {
				return fmt.Errorf("%s %s exceeds 4000 bytes", entry.kind, field.name)
			}
			for _, r := range field.value {
				if r == '\n' || r == '\t' {
					if !entry.multi {
						return fmt.Errorf("%s %s must not contain line breaks", entry.kind, field.name)
					}
					continue
				}
				if r < 0x20 || r == 0x7f {
					return fmt.Errorf("%s %s contains control characters", entry.kind, field.name)
				}
			}
		}
	}
	return nil
}
