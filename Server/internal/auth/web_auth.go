package auth

import (
	"crypto/rand"
	"database/sql"
	"encoding/base64"
	"errors"
	"net/http"
	"strings"
	"time"

	"astralparty-server/internal/db"
	"golang.org/x/crypto/bcrypt"
)

const maxConsolePasswordBytes = 72

func hashPassword(password string) (string, error) {
	if err := validateConsolePassword(password); err != nil {
		return "", err
	}
	hash, err := bcrypt.GenerateFromPassword([]byte(password), bcrypt.DefaultCost)
	return string(hash), err
}

func verifyPassword(hash, password string) bool {
	return bcrypt.CompareHashAndPassword([]byte(hash), []byte(password)) == nil
}

func validateConsolePassword(password string) error {
	if len(password) < 8 || len(password) > maxConsolePasswordBytes {
		return errors.New("password must be 8..72 bytes")
	}
	return nil
}

func (h *HTTPHandler) playerWebLogin(w http.ResponseWriter, r *http.Request) {
	var q struct {
		Phone    string `json:"phone"`
		Password string `json:"password"`
	}
	if err := decodeJSON(w, r, &q); err != nil {
		writeJSON(w, http.StatusBadRequest, map[string]any{"error": "invalid request"})
		return
	}
	phone, ok := normalizeVirtualPhone(q.Phone)
	if !ok || len(q.Password) < 8 || len(q.Password) > maxConsolePasswordBytes {
		writeJSON(w, http.StatusUnauthorized, map[string]any{"error": "invalid phone or password"})
		return
	}
	account, passwordHash, err := h.store.PlayerWebCredentialByPhone(r.Context(), phone)
	if err != nil || account.Disabled {
		writeJSON(w, http.StatusUnauthorized, map[string]any{"error": "invalid phone or password"})
		return
	}
	if passwordHash == "" {
		writeJSON(w, http.StatusUnauthorized, map[string]any{"error": "invalid phone or password"})
		return
	}
	if !verifyPassword(passwordHash, q.Password) {
		writeJSON(w, http.StatusUnauthorized, map[string]any{"error": "invalid phone or password"})
		return
	}
	mustChange, err := h.store.PlayerWebMustChangePassword(r.Context(), account.ID)
	if err != nil {
		writeJSON(w, http.StatusInternalServerError, map[string]any{"error": "password state unavailable"})
		return
	}
	token, err := randomWebSessionToken()
	if err != nil {
		writeJSON(w, http.StatusInternalServerError, map[string]any{"error": "session unavailable"})
		return
	}
	now := time.Now()
	if err := h.store.IssueSelfSession(r.Context(), account.ID, db.SessionTokenHash(token), now.Unix(), now.Add(h.cfg.SessionTTL).Unix()); err != nil {
		writeJSON(w, http.StatusInternalServerError, map[string]any{"error": "session unavailable"})
		return
	}
	writeJSON(w, http.StatusOK, map[string]any{"token": token, "mustChangePassword": mustChange, "accountNo": account.Number, "nick": account.Nick})
}

func (h *HTTPHandler) playerWebPassword(w http.ResponseWriter, r *http.Request) {
	account, token, ok := h.playerWebAccount(w, r)
	if !ok {
		return
	}
	var q struct {
		CurrentPassword string `json:"currentPassword"`
		NewPassword     string `json:"newPassword"`
	}
	if err := decodeJSON(w, r, &q); err != nil || len(q.CurrentPassword) < 8 || len(q.CurrentPassword) > maxConsolePasswordBytes || validateConsolePassword(q.NewPassword) != nil {
		writeJSON(w, http.StatusBadRequest, map[string]any{"error": "invalid password change"})
		return
	}
	var currentHash string
	mustChange, err := h.store.PlayerWebMustChangePassword(r.Context(), account.ID)
	if err != nil {
		writeJSON(w, http.StatusInternalServerError, map[string]any{"error": "password state unavailable"})
		return
	}
	if err := h.store.DB().QueryRowContext(r.Context(), `SELECT COALESCE(web_password_hash,'') FROM accounts WHERE id=?`, account.ID).Scan(&currentHash); err != nil {
		writeJSON(w, http.StatusInternalServerError, map[string]any{"error": "password state unavailable"})
		return
	}
	if !verifyPassword(currentHash, q.CurrentPassword) {
		writeJSON(w, http.StatusUnauthorized, map[string]any{"error": "current password is incorrect"})
		return
	}
	newHash, err := hashPassword(q.NewPassword)
	if err != nil {
		writeJSON(w, http.StatusBadRequest, map[string]any{"error": "invalid password change"})
		return
	}
	if mustChange && verifyPassword(currentHash, q.NewPassword) {
		writeJSON(w, http.StatusBadRequest, map[string]any{"error": "new password must differ from initial password"})
		return
	}
	now := time.Now().Unix()
	if err = h.store.UpdatePlayerWebPassword(r.Context(), account.ID, newHash, now); err != nil {
		writeJSON(w, http.StatusInternalServerError, map[string]any{"error": "password update failed"})
		return
	}
	_ = h.store.RevokeEmailSession(r.Context(), db.SessionTokenHash(token), now)
	writeJSON(w, http.StatusOK, map[string]any{"ok": true})
}

func (h *HTTPHandler) playerWebLogout(w http.ResponseWriter, r *http.Request) {
	_, token, ok := h.playerWebAccount(w, r)
	if !ok {
		return
	}
	if err := h.store.RevokeEmailSession(r.Context(), db.SessionTokenHash(token), time.Now().Unix()); err != nil {
		writeJSON(w, http.StatusInternalServerError, map[string]any{"error": "logout failed"})
		return
	}
	writeJSON(w, http.StatusOK, map[string]bool{"ok": true})
}

func (h *HTTPHandler) playerWebSelf(w http.ResponseWriter, r *http.Request) {
	account, _, ok := h.playerWebAccount(w, r)
	if !ok {
		return
	}
	mustChange, err := h.store.PlayerWebMustChangePassword(r.Context(), account.ID)
	if err != nil {
		writeJSON(w, http.StatusInternalServerError, map[string]any{"error": "player account unavailable"})
		return
	}
	if mustChange {
		writeJSON(w, http.StatusForbidden, map[string]any{"error": "player_password_change_required"})
		return
	}
	player, err := h.store.PlayerForAccount(r.Context(), account.ID)
	if errors.Is(err, sql.ErrNoRows) || errors.Is(err, db.ErrPlayerNotFound) {
		writeJSON(w, http.StatusNotFound, map[string]any{"error": "player not found"})
		return
	}
	if err != nil {
		writeJSON(w, http.StatusInternalServerError, map[string]any{"error": "player unavailable"})
		return
	}
	playerData := map[string]any{
		"id": player.ID, "nick": player.Nick, "level": player.Level, "exp": player.Exp,
		"gold": player.Gold, "hp": player.HP, "heroLevel": player.HeroLevel,
		"inventory": player.Inventory, "pveHeroes": player.PveHeroes,
		"gachaProgress": player.GachaProgress, "fashionPlans": player.FashionPlans,
		"usePlan": player.UsePlan, "inRoom": player.RoomID != 0,
	}
	writeJSON(w, http.StatusOK, playerData)
}

func (h *HTTPHandler) playerWebAccount(w http.ResponseWriter, r *http.Request) (db.EmailAccount, string, bool) {
	parts := strings.Fields(r.Header.Get("Authorization"))
	if len(parts) != 2 || !strings.EqualFold(parts[0], "Bearer") {
		writeJSON(w, http.StatusUnauthorized, map[string]any{"error": "authentication required"})
		return db.EmailAccount{}, "", false
	}
	account, err := h.store.ResolveSelfSession(r.Context(), db.SessionTokenHash(parts[1]), time.Now().Unix())
	if err != nil || account.Disabled {
		writeJSON(w, http.StatusUnauthorized, map[string]any{"error": "authentication required"})
		return db.EmailAccount{}, "", false
	}
	return account, parts[1], true
}

func randomWebSessionToken() (string, error) {
	b := make([]byte, 32)
	if _, err := rand.Read(b); err != nil {
		return "", err
	}
	return base64.RawURLEncoding.EncodeToString(b), nil
}
