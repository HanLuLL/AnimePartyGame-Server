package gdconf

import (
	"context"
	"encoding/json"
	"fmt"
	"log/slog"
	"os"
	"path/filepath"
	"sync/atomic"
	"time"
)

// Gameplay holds operator-owned runtime switches that live outside the game's
// static config tables loaded from JSON dumps. The file is
// polled for mtime changes so edits apply without a restart.
type Gameplay struct {
	AutoCreateAccount  bool     `json:"autoCreateAccount"`
	StoryEnabled       bool     `json:"storyEnabled"`
	InitialGold        int      `json:"initialGold"`
	InitialEffectCards int      `json:"initialEffectCards"`
	InitialCombatCards int      `json:"initialCombatCards"`
	GmAllowedAccounts  []string `json:"gmAllowedAccounts"`
	LogFullPayload     bool     `json:"logFullPayload"`
}

func defaultGameplay() Gameplay {
	return Gameplay{AutoCreateAccount: true, StoryEnabled: true}
}

type GameplayStore struct {
	path    string
	log     *slog.Logger
	current atomic.Pointer[Gameplay]
	lastMod atomic.Int64
}

// NewGameplayStore loads path or seeds it with defaults when missing. Load
// failures are logged and leave the last good value in place.
func NewGameplayStore(path string, logger *slog.Logger) *GameplayStore {
	if logger == nil {
		logger = slog.Default()
	}
	g := &GameplayStore{path: path, log: logger}
	g.Reload()
	return g
}

func (g *GameplayStore) Path() string { return g.path }

func (g *GameplayStore) Get() *Gameplay {
	if v := g.current.Load(); v != nil {
		return v
	}
	def := defaultGameplay()
	return &def
}

func (g *GameplayStore) Reload() {
	data, err := os.ReadFile(g.path)
	if err != nil {
		if !os.IsNotExist(err) {
			g.log.Warn("gameplay config read failed", "path", g.path, "err", err)
			return
		}
		if err := os.MkdirAll(filepath.Dir(g.path), 0o750); err != nil {
			g.log.Warn("gameplay config dir create failed", "path", filepath.Dir(g.path), "err", err)
			return
		}
		out, _ := json.MarshalIndent(defaultGameplay(), "", "  ")
		if err := os.WriteFile(g.path, out, 0o640); err != nil {
			g.log.Warn("gameplay config seed failed", "path", g.path, "err", err)
			return
		}
		def := defaultGameplay()
		g.current.Store(&def)
		g.log.Info("gameplay config seeded with defaults", "path", g.path)
		g.refreshModTime()
		return
	}
	var parsed Gameplay
	if err := json.Unmarshal(data, &parsed); err != nil {
		g.log.Warn("gameplay config parse failed", "path", g.path, "err", err)
		return
	}
	g.current.Store(&parsed)
	g.refreshModTime()
}

func (g *GameplayStore) refreshModTime() {
	if st, err := os.Stat(g.path); err == nil {
		g.lastMod.Store(st.ModTime().UnixNano())
	}
}

// StartPolling watches the config file mtime until ctx is cancelled. A one
// second interval is cheap because stat() is the only syscall on the happy path.
func (g *GameplayStore) StartPolling(ctx context.Context, interval time.Duration) {
	if interval <= 0 {
		interval = time.Second
	}
	ticker := time.NewTicker(interval)
	go func() {
		defer ticker.Stop()
		for {
			select {
			case <-ctx.Done():
				return
			case <-ticker.C:
				st, err := os.Stat(g.path)
				if err != nil {
					continue
				}
				if st.ModTime().UnixNano() != g.lastMod.Load() {
					g.log.Info("gameplay config reload", "path", g.path)
					g.Reload()
				}
			}
		}
	}()
}

// GMAllowed reports whether the account login key or phone may run GM commands.
// "*" grants everyone; empty list grants nobody.
func (gp *Gameplay) GMAllowed(loginKey, phone string) bool {
	if len(gp.GmAllowedAccounts) == 0 {
		return false
	}
	for _, allowed := range gp.GmAllowedAccounts {
		if allowed == "*" || allowed == loginKey || allowed == phone {
			return true
		}
	}
	return false
}

func (gp *Gameplay) String() string {
	return fmt.Sprintf("autoCreateAccount=%v storyEnabled=%v initialGold=%d gmAccounts=%d",
		gp.AutoCreateAccount, gp.StoryEnabled, gp.InitialGold, len(gp.GmAllowedAccounts))
}
