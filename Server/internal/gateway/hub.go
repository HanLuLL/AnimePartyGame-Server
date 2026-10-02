package gateway

import (
	"sort"
	"sync"
)

type sessionHub struct {
	mu       sync.RWMutex
	byPlayer map[int64]map[*Session]struct{}
}

func newSessionHub() *sessionHub { return &sessionHub{byPlayer: make(map[int64]map[*Session]struct{})} }
func (h *sessionHub) register(playerID int64, s *Session) {
	if playerID <= 0 {
		return
	}
	h.mu.Lock()
	defer h.mu.Unlock()
	if h.byPlayer[playerID] == nil {
		h.byPlayer[playerID] = make(map[*Session]struct{})
	}
	h.byPlayer[playerID][s] = struct{}{}
}

// onlinePlayerIDs returns the sorted distinct player ids with live sessions.
func (h *sessionHub) onlinePlayerIDs() []int64 {
	h.mu.RLock()
	defer h.mu.RUnlock()
	ids := make([]int64, 0, len(h.byPlayer))
	for id := range h.byPlayer {
		ids = append(ids, id)
	}
	sort.Slice(ids, func(i, j int) bool { return ids[i] < ids[j] })
	return ids
}

func (h *sessionHub) unregister(playerID int64, s *Session) {
	if playerID <= 0 {
		return
	}
	h.mu.Lock()
	defer h.mu.Unlock()
	m := h.byPlayer[playerID]
	delete(m, s)
	if len(m) == 0 {
		delete(h.byPlayer, playerID)
	}
}
func (h *sessionHub) unregisterConn(s *Session) {
	_, _, pid, _, ok := s.identity()
	if ok {
		h.unregister(pid, s)
	}
}
func (h *sessionHub) sessions(playerID int64) []*Session {
	h.mu.RLock()
	defer h.mu.RUnlock()
	m := h.byPlayer[playerID]
	out := make([]*Session, 0, len(m))
	for s := range m {
		out = append(out, s)
	}
	return out
}
func (h *sessionHub) registerIdentity(s *Session, playerID int64) { h.register(playerID, s) }
