package gateway

import (
	"context"

	store "astralparty-server/internal/db"
	protocolpb "astralparty-server/internal/protocol/gen/protocol"
	"google.golang.org/protobuf/proto"
)

func (s *Server) resolveBotPursuit(ctx context.Context, room store.Room, actor store.Player, recipients []int64) (int64, int32, []Push, error) {
	var target *store.Player
	for i := range room.Players {
		candidate := &room.Players[i]
		if candidate.ID == actor.ID || candidate.HP <= 0 || roomTeamID(room.Mode, candidate.Slot) == roomTeamID(room.Mode, actor.Slot) {
			continue
		}
		landType, exists := s.domain.LandTypeAt(room, candidate.NodeID)
		if exists && landType == 13 {
			continue
		}
		if target == nil || candidate.HP < target.HP || (candidate.HP == target.HP && candidate.ID < target.ID) {
			target = candidate
		}
	}
	request := &protocolpb.PursuitC2S{}
	var targetID int64
	var frontNodeIDs []int32
	if target != nil {
		targetID = target.ID
		var err error
		frontNodeIDs, err = s.domain.ActiveNeighborLandIDs(room, target.NodeID)
		if err != nil {
			return 0, 0, nil, err
		}
		request.SelectPlayerId = target.ID
	}
	raw, err := proto.Marshal(request)
	if err != nil {
		return 0, 0, nil, err
	}
	result, created, err := s.store.CompletePursuit(ctx, room.ID, actor.ID, 5033, s.nextActionSN(), raw, targetID, frontNodeIDs)
	if err != nil {
		return 0, 0, nil, err
	}
	if !created {
		return 0, 0, nil, store.ErrActionNotReady
	}
	message := &protocolpb.PursuitS2C{
		PlayerId: actor.ID, NodeId: result.NodeID, FrontIds: result.FrontNodeIDs,
		BackId: result.BackNodeID, Exit: result.Exit,
	}
	s.log.Info("bot pursuit land resolved", "room_id", room.ID, "player_id", actor.ID, "target_player_id", targetID, "node_id", result.NodeID, "exit", result.Exit)
	return result.NextPlayer, result.Round, []Push{s.pushFor("PursuitS2C", message, recipients, 0)}, nil
}
