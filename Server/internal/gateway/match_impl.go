package gateway

import (
	"context"
	"sync"
	"time"

	store "astralparty-server/internal/db"
	modelpb "astralparty-server/internal/protocol/gen/model"
	protocolpb "astralparty-server/internal/protocol/gen/protocol"
)

// This file implements the matchmaking loop. StartMatchC2S flips a ready team
// into the matching state; a background goroutine (hub-style, started with the
// server) periodically pairs matching teams, fills remaining seats with bots,
// creates the hosting room, and pushes MatchSuccessS2C to every participant.

const (
	matchLoopInterval   = 3 * time.Second
	matchMinTeamMembers = 1
)

type matchCoordinator struct {
	mu      sync.Mutex
	running bool
	stop    chan struct{}
}

var matchLoop = &matchCoordinator{stop: make(chan struct{})}

// startMatchLoop launches the background matchmaking goroutine once. Safe to
// call repeatedly.
func (s *Server) startMatchLoop() {
	matchLoop.mu.Lock()
	defer matchLoop.mu.Unlock()
	if matchLoop.running {
		return
	}
	matchLoop.running = true
	go func() {
		ticker := time.NewTicker(matchLoopInterval)
		defer ticker.Stop()
		for {
			select {
			case <-matchLoop.stop:
				return
			case <-ticker.C:
				s.runMatchIteration(context.Background())
			}
		}
	}()
}

// stopMatchLoop terminates the background matchmaking goroutine. Intended for
// tests; the production server keeps the loop running for its lifetime.
func stopMatchLoop() {
	select {
	case <-matchLoop.stop:
	default:
		close(matchLoop.stop)
	}
}

// runMatchIteration pairs every ready matching team with another matching team
// of the same mode, then fills unmatched leftovers with bots so a queue never
// stalls.
func (s *Server) runMatchIteration(ctx context.Context) {
	teams, err := s.store.ListMatchableTeams(ctx)
	if err != nil {
		s.log.Error("match loop could not list teams", "err", err)
		return
	}
	if len(teams) == 0 {
		return
	}
	// Pair teams of the same mode/map/difficulty first; anything left over
	// gets bot-filled below.
	used := make([]bool, len(teams))
	for i := range teams {
		if used[i] || teams[i].Count < matchMinTeamMembers {
			continue
		}
		group := []store.MatchCandidate{teams[i]}
		used[i] = true
		for j := i + 1; j < len(teams); j++ {
			if used[j] {
				continue
			}
			if teams[j].Mode == teams[i].Mode && teams[j].MapID == teams[i].MapID && teams[j].Difficulty == teams[i].Difficulty {
				group = append(group, teams[j])
				used[j] = true
			}
			if len(group) >= 4 {
				break
			}
		}
		s.matchGroups(ctx, group)
	}
}

// matchGroups creates one room for a group of teams and pushes MatchSuccessS2C
// to all of their members.
func (s *Server) matchGroups(ctx context.Context, group []store.MatchCandidate) {
	first := group[0]
	totalMembers := 0
	for _, t := range group {
		totalMembers += t.Count
	}
	roomID, err := s.store.CreateMatchRoom(ctx, first.Mode, first.MapID, first.Difficulty, first.LeaderID)
	if err != nil {
		s.log.Error("match loop could not create room", "leader", first.LeaderID, "err", err)
		return
	}
	teamIDs := make([]int64, 0, len(group))
	for _, t := range group {
		teamIDs = append(teamIDs, t.ID)
	}
	claimed, err := s.store.ClaimMatchTeams(ctx, roomID, teamIDs)
	if err != nil {
		s.log.Error("match loop could not claim teams", "room_id", roomID, "err", err)
		return
	}
	if len(claimed) == 0 {
		return
	}
	botsAdded := 0
	if s.BotAutoFill() && totalMembers < 4 {
		if added, e := s.store.FillRoomWithBots(ctx, roomID); e != nil {
			s.log.Error("match loop could not fill room with bots", "room_id", roomID, "err", e)
		} else {
			botsAdded = added
		}
		heroIDs := s.resources.DefaultHeroIDs()
		heroMaxHP := make(map[int32]int32, len(heroIDs))
		for _, heroID := range heroIDs {
			heroMaxHP[heroID] = s.resources.HeroMaxHP(heroID)
		}
		if len(heroIDs) > 0 {
			if e := s.store.ConfigureRoomBots(ctx, roomID, heroIDs, heroMaxHP); e != nil {
				s.log.Error("match loop could not prepare bots", "room_id", roomID, "err", e)
			}
		}
	}
	room, err := s.store.RoomSnapshot(ctx, roomID)
	if err != nil {
		s.log.Error("match loop could not snapshot room", "room_id", roomID, "err", err)
		return
	}
	msg := &protocolpb.MatchSuccessS2C{Code: 0, Room: s.roomMessage(room)}
	targets := make([]int64, 0, len(room.Players))
	for _, p := range room.Players {
		if !p.IsBot {
			targets = append(targets, p.ID)
		}
	}
	push := s.pushFor("MatchSuccessS2C", msg, targets, 0)
	s.broadcastPush(push, [3]byte{1, 0, 0})
	s.log.Info("match succeeded", "room_id", roomID, "teams", len(claimed), "players", totalMembers, "bots_added", botsAdded)
}

// handleMatchSuccessAck answers the client's MatchSuccessC2S acknowledgement.
// The client only uses the response to confirm it entered the room.
func (s *Server) handleMatchSuccessAck(ctx context.Context, sess *Session, q *protocolpb.MatchSuccessC2S) (DispatchResult, error) {
	_, pid, ok := sessionPlayer(sess)
	if !ok {
		return DispatchResult{Err: ErrAuth}, nil
	}
	roomID := q.GetId()
	if roomID == 0 {
		var err error
		if roomID, err = s.store.RoomForPlayer(ctx, pid); err != nil {
			return DispatchResult{Err: ErrRoomNotExist}, nil
		}
	}
	room, err := s.store.RoomSnapshot(ctx, roomID)
	if err != nil {
		return DispatchResult{Err: ErrRoomNotExist}, nil
	}
	return DispatchResult{Message: &protocolpb.MatchSuccessS2C{Code: 0, Room: s.roomMessage(room)}}, nil
}

// matchTeamState pushes the current team state to all members.
func (s *Server) matchTeamStatePush(t store.MatchTeam) Push {
	state := modelpb.MatchTeamInfo_waiting
	switch t.State {
	case 2:
		state = modelpb.MatchTeamInfo_matching
	case 3:
		state = modelpb.MatchTeamInfo_playing
	}
	msg := &protocolpb.RefreshMatchTeamStateNotify{TeamId: t.ID, State: state}
	return s.pushFor("RefreshMatchTeamStateNotify", msg, matchTeamPlayerIDs(t), 0)
}
