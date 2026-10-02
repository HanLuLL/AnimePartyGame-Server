package gateway

import (
	"context"
	"errors"

	store "astralparty-server/internal/db"
	wire "astralparty-server/internal/protocol"
	protocolpb "astralparty-server/internal/protocol/gen/protocol"
	"google.golang.org/protobuf/proto"
)

func (s *Server) handlePursuit(ctx context.Context, sess *Session, in wire.Frame, q *protocolpb.PursuitC2S) (DispatchResult, error) {
	_, playerID, ok := sessionPlayer(sess)
	if !ok {
		return DispatchResult{Err: ErrAuth}, nil
	}
	roomID, err := s.store.RoomForPlayer(ctx, playerID)
	if err != nil {
		return DispatchResult{Err: ErrRoomNotAction}, nil
	}
	current, _, pending, err := s.store.CurrentTurn(ctx, roomID)
	if err != nil {
		return DispatchResult{Err: ErrRoomNotAction}, nil
	}
	if current != playerID {
		return DispatchResult{Err: ErrRoomActionPlayer}, nil
	}
	phase, err := s.store.CurrentTurnPhase(ctx, roomID)
	if err != nil {
		return DispatchResult{}, err
	}
	if pending != 0 || phase != store.TurnPhasePursuit {
		return DispatchResult{Err: ErrRoomActionIncorrect}, nil
	}
	room, err := s.store.RoomSnapshot(ctx, roomID)
	if err != nil {
		return DispatchResult{}, err
	}
	var actor, target *store.Player
	for i := range room.Players {
		member := &room.Players[i]
		if member.ID == playerID {
			actor = member
		}
		if member.ID == q.SelectPlayerId {
			target = member
		}
	}
	if actor == nil {
		return DispatchResult{Err: ErrRoomNotAction}, nil
	}
	landType, exists := s.domain.LandTypeAt(room, actor.NodeID)
	if !exists || landType != 4 {
		return DispatchResult{Err: ErrRoomActionIncorrect}, nil
	}
	var frontNodeIDs []int32
	if q.SelectPlayerId != 0 {
		if q.SelectPlayerId < 0 || target == nil || target.HP <= 0 || target.ID == actor.ID {
			return DispatchResult{Err: ErrRoomActionIncorrect}, nil
		}
		if roomTeamID(room.Mode, actor.Slot) == roomTeamID(room.Mode, target.Slot) {
			return DispatchResult{Err: ErrRoomActionIncorrect}, nil
		}
		targetLandType, targetLandExists := s.domain.LandTypeAt(room, target.NodeID)
		if targetLandExists && targetLandType == 13 {
			return DispatchResult{Err: ErrRoomActionIncorrect}, nil
		}
		frontNodeIDs, err = s.domain.ActiveNeighborLandIDs(room, target.NodeID)
		if err != nil {
			s.log.Error("pursuit target has no active exits", "room_id", roomID, "player_id", playerID, "target_player_id", target.ID, "err", err)
			return DispatchResult{Err: ErrNotOpen}, nil
		}
	}
	raw, err := proto.Marshal(q)
	if err != nil {
		return DispatchResult{}, err
	}
	result, created, err := s.store.CompletePursuit(ctx, roomID, playerID, in.CmdID, in.UPSN, raw, q.SelectPlayerId, frontNodeIDs)
	if errors.Is(err, store.ErrTurnPlayerMismatch) {
		return DispatchResult{Err: ErrRoomActionPlayer}, nil
	}
	if errors.Is(err, store.ErrActionNotReady) || errors.Is(err, store.ErrInvalidPursuitTarget) {
		return DispatchResult{Err: ErrRoomActionIncorrect}, nil
	}
	if err != nil {
		return DispatchResult{}, err
	}
	if !created {
		return DispatchResult{Err: ErrRoomActionAlreadyDone}, nil
	}
	message := &protocolpb.PursuitS2C{
		PlayerId: playerID, NodeId: result.NodeID, FrontIds: result.FrontNodeIDs,
		BackId: result.BackNodeID, Exit: result.Exit,
	}
	s.log.Info("pursuit land resolved", "room_id", roomID, "player_id", playerID, "target_player_id", q.SelectPlayerId, "node_id", result.NodeID, "exit", result.Exit)
	ids := mustMemberIDs(ctx, s.store, roomID)
	push := s.pushFor("PursuitS2C", message, ids, playerID)
	pushes := []Push{push}
	pushes = append(pushes, s.turnPushes(ctx, roomID, result.Round, result.NextPlayer)...)
	return DispatchResult{Message: message, Pushes: pushes}, nil
}
