package gateway

import (
	"context"
	"crypto/rand"
	"errors"
	"math/big"

	store "astralparty-server/internal/db"
	wire "astralparty-server/internal/protocol"
	modelpb "astralparty-server/internal/protocol/gen/model"
	protocolpb "astralparty-server/internal/protocol/gen/protocol"
	"google.golang.org/protobuf/proto"
)

var errGambleConfigMissing = errors.New("Gamble land parameters are missing")

func (s *Server) newGambleState(room store.Room, landID int32) (*store.GambleState, bool, error) {
	params, ok := s.domain.LandParams(10)
	if !ok || len(params) < 2 || params[0] < 0 || params[1] <= 0 {
		return nil, false, errGambleConfigMissing
	}
	state := &store.GambleState{BaseGold: params[0], BetGold: params[1], LandID: landID, Roles: make([]store.GambleRole, 0, len(room.Players))}
	eligible := false
	for _, player := range room.Players {
		role := store.GambleRole{PlayerID: player.ID, HeroID: player.HeroID, IsDie: player.HP <= 0, GoldLack: player.Gold < params[1]}
		eligible = eligible || !role.IsDie && !role.GoldLack
		state.Roles = append(state.Roles, role)
	}
	return state, eligible, nil
}

func gambleModel(state store.GambleState) *modelpb.Gamble {
	hall := &modelpb.Gamble{BaseGold: state.BaseGold, BetGold: state.BetGold, S: modelpb.GambleState(state.S), IsOdd: state.IsOdd}
	for _, role := range state.Roles {
		hall.Roles = append(hall.Roles, &modelpb.GambleRole{PlayerId: role.PlayerID, HeroId: role.HeroID, IsDie: role.IsDie, GoldLack: role.GoldLack,
			BetGold: role.BetGold, GuessCode: role.GuessCode, Point: role.Point, GoldChange: role.GoldChange})
	}
	return hall
}

func findRoomPlayer(room store.Room, playerID int64) (store.Player, bool) {
	for _, player := range room.Players {
		if player.ID == playerID {
			return player, true
		}
	}
	return store.Player{}, false
}

func (s *Server) gambleActionPrompts(room store.Room, round int32, state store.GambleState, phase string) []Push {
	var pushes []Push
	for _, role := range state.Roles {
		if role.IsDie || role.GoldLack {
			continue
		}
		player, found := findRoomPlayer(room, role.PlayerID)
		if !found || player.IsBot {
			continue
		}
		if phase == store.TurnPhaseGambleGuess && role.GuessCode == 0 {
			pushes = append(pushes, s.actionPush(round, 5081, role.PlayerID, &protocolpb.StartGambleC2S{Hall: gambleModel(state), IsExec: true}))
		} else if phase == store.TurnPhaseGambleThrow && role.GuessCode != 0 && role.Point == 0 {
			pushes = append(pushes, s.actionPush(round, 5083, role.PlayerID, &protocolpb.GambleThrowDicC2S{}))
		}
	}
	return pushes
}

func (s *Server) gambleStartPushes(room store.Room, round int32, state store.GambleState) []Push {
	pushes := s.gambleActionPrompts(room, round, state, store.TurnPhaseGambleGuess)
	participants := make(map[int64]struct{}, len(state.Roles))
	for _, role := range state.Roles {
		if !role.IsDie && !role.GoldLack {
			participants[role.PlayerID] = struct{}{}
		}
	}
	observers := make([]int64, 0, len(room.Players))
	for _, player := range room.Players {
		if player.IsBot {
			continue
		}
		if _, participating := participants[player.ID]; !participating {
			observers = append(observers, player.ID)
		}
	}
	if len(observers) > 0 {
		pushes = append(pushes, s.pushFor("GambleObServeS2C", &protocolpb.GambleObServeS2C{Hall: gambleModel(state)}, observers, 0))
	}
	return pushes
}

func gambleGoldPushes(ctx context.Context, s *Server, roomID int64, landID int32, result store.GambleActionResult) []Push {
	ids := mustMemberIDs(ctx, s.store, roomID)
	var pushes []Push
	for _, change := range result.Gold {
		message := &protocolpb.UpdateHeroAttrS2C{
			PlayerId: change.PlayerID,
			Cause:    &protocolpb.CauseOrigin{S: protocolpb.CauseOrigin_land, Id: int64(landID)},
			EffectDatas: []*protocolpb.HeroAttrEffect{{PlayerId: change.PlayerID, Data: &protocolpb.HeroAttrEffect_Gold{Gold: &protocolpb.HeroGoldChangeS2C{
				PlayerId: change.PlayerID, ChangeGold: change.NewGold - change.OldGold, OriGold: change.OldGold, CurrGold: change.NewGold,
			}}}},
		}
		pushes = append(pushes, s.pushFor("UpdateHeroAttrS2C", message, ids, 0))
	}
	return pushes
}

func (s *Server) handleStartGamble(ctx context.Context, sess *Session, in wire.Frame, q *protocolpb.StartGambleC2S) (DispatchResult, error) {
	_, playerID, ok := sessionPlayer(sess)
	if !ok {
		return DispatchResult{Err: ErrAuth}, nil
	}
	if !q.IsExec || q.GuessCode < 1 || q.GuessCode > 2 {
		return DispatchResult{Err: ErrRoomActionIncorrect}, nil
	}
	roomID, err := s.store.RoomForPlayer(ctx, playerID)
	if err != nil {
		return DispatchResult{Err: ErrRoomNotAction}, nil
	}
	state, exists, err := s.store.GambleSnapshot(ctx, roomID)
	if err != nil {
		return DispatchResult{}, err
	}
	if !exists {
		return DispatchResult{Err: ErrRoomActionIncorrect}, nil
	}
	room, err := s.store.RoomSnapshot(ctx, roomID)
	if err != nil {
		return DispatchResult{}, err
	}
	landType, found := s.domain.LandTypeAt(room, state.LandID)
	if !found || landType != 10 {
		return DispatchResult{Err: ErrRoomActionIncorrect}, nil
	}
	raw, err := proto.Marshal(q)
	if err != nil {
		return DispatchResult{}, err
	}
	result, created, err := s.store.ApplyGambleAction(ctx, roomID, playerID, in.CmdID, in.UPSN, raw, 1, q.GuessCode)
	if errors.Is(err, store.ErrActionNotReady) {
		return DispatchResult{Err: ErrRoomActionIncorrect}, nil
	}
	if err != nil {
		return DispatchResult{}, err
	}
	if !created {
		return DispatchResult{Err: ErrRoomActionAlreadyDone}, nil
	}
	s.log.Info("Gamble guess accepted", "room_id", roomID, "player_id", playerID, "guess", q.GuessCode, "bet_gold", state.BetGold)
	ids := mustMemberIDs(ctx, s.store, roomID)
	pushes := []Push{s.pushFor("GambleChangeS2C", &protocolpb.GambleChangeS2C{Hall: gambleModel(result.State)}, ids, 0)}
	pushes = append(pushes, gambleGoldPushes(ctx, s, roomID, state.LandID, result)...)
	if result.Phase == store.TurnPhaseGambleThrow {
		room, roomErr := s.store.RoomSnapshot(ctx, roomID)
		if roomErr != nil {
			return DispatchResult{}, roomErr
		}
		pushes = append(pushes, s.gambleActionPrompts(room, result.Round, result.State, result.Phase)...)
	}
	botPushes, completed, next, round, botErr := s.resolveGambleBots(ctx, roomID)
	if botErr != nil {
		return DispatchResult{}, botErr
	}
	pushes = append(pushes, botPushes...)
	if completed {
		pushes = append(pushes, s.turnPushes(ctx, roomID, round, next)...)
	}
	return DispatchResult{Message: &protocolpb.StartGambleS2C{}, Pushes: pushes}, nil
}

func randomGamblePoint() (int32, error) {
	n, err := rand.Int(rand.Reader, big.NewInt(6))
	if err != nil {
		return 0, err
	}
	return int32(n.Int64() + 1), nil
}

func (s *Server) handleGambleThrow(ctx context.Context, sess *Session, in wire.Frame, q *protocolpb.GambleThrowDicC2S) (DispatchResult, error) {
	_, playerID, ok := sessionPlayer(sess)
	if !ok {
		return DispatchResult{Err: ErrAuth}, nil
	}
	roomID, err := s.store.RoomForPlayer(ctx, playerID)
	if err != nil {
		return DispatchResult{Err: ErrRoomNotAction}, nil
	}
	state, exists, err := s.store.GambleSnapshot(ctx, roomID)
	if err != nil {
		return DispatchResult{}, err
	}
	if !exists {
		return DispatchResult{Err: ErrRoomActionIncorrect}, nil
	}
	point, err := randomGamblePoint()
	if err != nil {
		return DispatchResult{}, err
	}
	raw, err := proto.Marshal(q)
	if err != nil {
		return DispatchResult{}, err
	}
	result, created, err := s.store.ApplyGambleAction(ctx, roomID, playerID, in.CmdID, in.UPSN, raw, 2, point)
	if errors.Is(err, store.ErrActionNotReady) {
		return DispatchResult{Err: ErrRoomActionIncorrect}, nil
	}
	if err != nil {
		return DispatchResult{}, err
	}
	if !created {
		return DispatchResult{Err: ErrRoomActionAlreadyDone}, nil
	}
	s.log.Info("Gamble die rolled", "room_id", roomID, "player_id", playerID, "point", point, "completed", result.Completed)
	ids := mustMemberIDs(ctx, s.store, roomID)
	message := &protocolpb.GambleThrowDicS2C{PlayerId: playerID, Point: point}
	pushes := []Push{s.pushFor("GambleThrowDicS2C", message, ids, playerID), s.pushFor("GambleChangeS2C", &protocolpb.GambleChangeS2C{Hall: gambleModel(result.State)}, ids, 0)}
	pushes = append(pushes, gambleGoldPushes(ctx, s, roomID, state.LandID, result)...)
	botPushes, completed, next, round, botErr := s.resolveGambleBots(ctx, roomID)
	if botErr != nil {
		return DispatchResult{}, botErr
	}
	pushes = append(pushes, botPushes...)
	if completed {
		pushes = append(pushes, s.turnPushes(ctx, roomID, round, next)...)
	}
	return DispatchResult{Message: message, Pushes: pushes}, nil
}

func (s *Server) resolveGambleBots(ctx context.Context, roomID int64) ([]Push, bool, int64, int32, error) {
	var pushes []Push
	for step := 0; step < 12; step++ {
		phase, err := s.store.CurrentTurnPhase(ctx, roomID)
		if err != nil {
			return pushes, false, 0, 0, err
		}
		if phase != store.TurnPhaseGambleGuess && phase != store.TurnPhaseGambleThrow {
			return pushes, false, 0, 0, nil
		}
		state, exists, err := s.store.GambleSnapshot(ctx, roomID)
		if err != nil || !exists {
			if err == nil {
				err = store.ErrActionNotReady
			}
			return pushes, false, 0, 0, err
		}
		room, err := s.store.RoomSnapshot(ctx, roomID)
		if err != nil {
			return pushes, false, 0, 0, err
		}
		var actor store.GambleRole
		found := false
		for _, role := range state.Roles {
			if role.IsDie || role.GoldLack {
				continue
			}
			player, member := findRoomPlayer(room, role.PlayerID)
			if !member || !player.IsBot {
				continue
			}
			if phase == store.TurnPhaseGambleGuess && role.GuessCode == 0 || phase == store.TurnPhaseGambleThrow && role.GuessCode != 0 && role.Point == 0 {
				actor, found = role, true
				break
			}
		}
		if !found {
			return pushes, false, 0, 0, nil
		}
		var request proto.Message
		var value, action int32
		var cmd uint16
		if phase == store.TurnPhaseGambleGuess {
			value = 1
			if room.Difficulty > 0 {
				n, randomErr := rand.Int(rand.Reader, big.NewInt(2))
				if randomErr != nil {
					return pushes, false, 0, 0, randomErr
				}
				value = int32(n.Int64() + 1)
			}
			request, action, cmd = &protocolpb.StartGambleC2S{IsExec: true, GuessCode: value}, 1, 5081
		} else {
			value, err = randomGamblePoint()
			if err != nil {
				return pushes, false, 0, 0, err
			}
			request, action, cmd = &protocolpb.GambleThrowDicC2S{}, 2, 5083
		}
		raw, err := proto.Marshal(request)
		if err != nil {
			return pushes, false, 0, 0, err
		}
		result, created, err := s.store.ApplyGambleAction(ctx, roomID, actor.PlayerID, cmd, s.nextActionSN(), raw, action, value)
		if err != nil || !created {
			if err == nil {
				err = store.ErrActionNotReady
			}
			return pushes, false, 0, 0, err
		}
		pushes = append(pushes,
			s.pushFor("GambleChangeS2C", &protocolpb.GambleChangeS2C{Hall: gambleModel(result.State)}, mustMemberIDs(ctx, s.store, roomID), 0),
		)
		pushes = append(pushes, gambleGoldPushes(ctx, s, roomID, state.LandID, result)...)
		if result.Phase == store.TurnPhaseGambleThrow {
			pushes = append(pushes, s.gambleActionPrompts(room, result.Round, result.State, result.Phase)...)
		}
		if result.Completed {
			s.log.Info("Gamble completed", "room_id", roomID, "player_id", result.NextPlayer, "round", result.Round, "odd", result.State.IsOdd)
			return pushes, true, result.NextPlayer, result.Round, nil
		}
	}
	return pushes, false, 0, 0, errors.New("Gamble bot action limit exceeded")
}

func (s *Server) resumeGambleForPlayer(ctx context.Context, roomID, playerID int64, round int32) ([]Push, error) {
	state, exists, err := s.store.GambleSnapshot(ctx, roomID)
	if err != nil || !exists {
		return nil, err
	}
	phase, err := s.store.CurrentTurnPhase(ctx, roomID)
	if err != nil {
		return nil, err
	}
	room, err := s.store.RoomSnapshot(ctx, roomID)
	if err != nil {
		return nil, err
	}
	player, found := findRoomPlayer(room, playerID)
	if !found || player.IsBot {
		return nil, nil
	}
	for _, role := range state.Roles {
		if role.PlayerID != playerID {
			continue
		}
		if !role.IsDie && !role.GoldLack && phase == store.TurnPhaseGambleGuess && role.GuessCode == 0 {
			return []Push{s.actionPush(round, 5081, playerID, &protocolpb.StartGambleC2S{Hall: gambleModel(state), IsExec: true})}, nil
		}
		if !role.IsDie && !role.GoldLack && phase == store.TurnPhaseGambleThrow && role.GuessCode != 0 && role.Point == 0 {
			return []Push{s.actionPush(round, 5083, playerID, &protocolpb.GambleThrowDicC2S{})}, nil
		}
		return []Push{s.pushFor("GambleObServeS2C", &protocolpb.GambleObServeS2C{Hall: gambleModel(state)}, []int64{playerID}, 0)}, nil
	}
	return nil, nil
}

func (s *Server) handleGambleLandNoPlayers(roomID int64, playerID int64) []Push {
	message := &protocolpb.NoGambleNotifyS2C{PlayerId: playerID}
	return []Push{s.pushFor("NoGambleNotifyS2C", message, []int64{playerID}, 0)}
}
