// Package common holds shared, dependency-free helpers used by gateway and game:
// online session identity and input limits. It must not import gateway or db.
package common

type Identity struct {
	AccountID int64
	PlayerID  int64
	Nick      string
	Platform  string
	SessionID int64
}

const DefaultMaxPayload = 16 << 20

func ValidText(s string, max int) bool { return len(s) > 0 && len(s) <= max }
