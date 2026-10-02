// Package main bootstrap.
package main

import (
	"context"
	"fmt"
	"io"
	"log/slog"
	"os"
	"path/filepath"
	"strings"
	"sync"
	"time"
)

// ANSI colors matching the Logback highlight defaults this project follows:
// timestamp cyan, INFO green, WARN red, ERROR bold red, DEBUG blue.
const (
	ansiReset   = "\033[0m"
	ansiCyan    = "\033[36m"
	ansiGreen   = "\033[32m"
	ansiRed     = "\033[31m"
	ansiBoldRed = "\033[1;31m"
	ansiBlue    = "\033[34m"
)

// consoleHandler renders single-line colored logs:
//
//	[19:25:57] [INFO] Starting Astral Party Server
//	[19:25:58] [WARN] handler failed err=...
//
// The timestamp is cyan and the level token is colorized per Logback's
// highlight defaults. Message text stays readable while key=value pairs
// follow on the same line. Every line is mirrored to logs/latest.log
// without ANSI escapes.
type consoleHandler struct {
	mu    *sync.Mutex
	out   io.Writer
	file  *os.File
	color bool
}

func newConsoleHandler() slog.Handler {
	h := &consoleHandler{mu: &sync.Mutex{}, out: os.Stdout, color: isTerminal(os.Stdout)}
	_ = h.openFile()
	return h
}

func (h *consoleHandler) openFile() error {
	if err := os.MkdirAll("logs", 0o755); err != nil {
		return err
	}
	f, err := os.OpenFile(filepath.Join("logs", "latest.log"), os.O_CREATE|os.O_WRONLY|os.O_APPEND, 0o644)
	if err != nil {
		return err
	}
	h.file = f
	return nil
}

func isTerminal(f *os.File) bool {
	fi, err := f.Stat()
	if err != nil {
		return false
	}
	return fi.Mode()&os.ModeCharDevice != 0
}

func (h *consoleHandler) Enabled(_ context.Context, level slog.Level) bool {
	return level >= slog.LevelInfo
}

func levelText(level slog.Level) string {
	switch {
	case level >= slog.LevelError:
		return "ERROR"
	case level >= slog.LevelWarn:
		return "WARN "
	case level >= slog.LevelInfo:
		return "INFO "
	default:
		return "DEBUG"
	}
}

func levelColor(level slog.Level) string {
	switch {
	case level >= slog.LevelError:
		return ansiBoldRed
	case level >= slog.LevelWarn:
		return ansiRed
	case level >= slog.LevelInfo:
		return ansiGreen
	default:
		return ansiBlue
	}
}

func (h *consoleHandler) Handle(_ context.Context, r slog.Record) error {
	var plain strings.Builder
	fmt.Fprintf(&plain, "[%s] [%s] %s", time.Now().Format("15:04:05"), levelText(r.Level), r.Message)
	r.Attrs(func(a slog.Attr) bool {
		fmt.Fprintf(&plain, " %s=%v", a.Key, a.Value)
		return true
	})
	plain.WriteByte('\n')

	line := plain.String()
	if h.color {
		var b strings.Builder
		ts := time.Now().Format("15:04:05")
		lv := strings.TrimRight(levelText(r.Level), " ")
		fmt.Fprintf(&b, "%s[%s]%s [%s%s%s] %s",
			ansiCyan, ts, ansiReset,
			levelColor(r.Level), lv, ansiReset,
			r.Message)
		r.Attrs(func(a slog.Attr) bool {
			fmt.Fprintf(&b, " %s=%v", a.Key, a.Value)
			return true
		})
		b.WriteByte('\n')
		line = b.String()
	}

	h.mu.Lock()
	defer h.mu.Unlock()
	_, _ = io.WriteString(h.out, line)
	if h.file != nil {
		_, _ = io.WriteString(h.file, plain.String())
	}
	return nil
}

func (h *consoleHandler) WithAttrs(attrs []slog.Attr) slog.Handler { return h }
func (h *consoleHandler) WithGroup(name string) slog.Handler       { return h }
