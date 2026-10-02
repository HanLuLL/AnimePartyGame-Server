package gateway

import (
	"context"
	"errors"

	store "astralparty-server/internal/db"
	wire "astralparty-server/internal/protocol"
	modelpb "astralparty-server/internal/protocol/gen/model"
	protocolpb "astralparty-server/internal/protocol/gen/protocol"

	"google.golang.org/protobuf/proto"
)

func (s *Server) handleAbandonCards(ctx context.Context, sess *Session, in wire.Frame, q *protocolpb.AbandonCardC2S) (DispatchResult, error) {
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
	if pending != 0 {
		return DispatchResult{Err: ErrRoomActionIncorrect}, nil
	}
	room, err := s.store.RoomSnapshot(ctx, roomID)
	if err != nil {
		return DispatchResult{}, err
	}
	handLimit, found := s.domain.CardInHandLimit(room.Mode)
	if !found {
		handLimit, found = s.resources.GlobalInt("GAME_CARDINHAND_LIMIT")
	}
	if !found || handLimit <= 0 {
		return DispatchResult{Err: ErrNotOpen}, nil
	}
	raw, err := proto.Marshal(q)
	if err != nil {
		return DispatchResult{}, err
	}
	resolved, created, err := s.store.CompleteAbandonCards(ctx, roomID, playerID, in.CmdID, in.UPSN, raw, q.CardUniqueIds, handLimit)
	switch {
	case errors.Is(err, store.ErrTurnPlayerMismatch):
		return DispatchResult{Err: ErrRoomActionPlayer}, nil
	case errors.Is(err, store.ErrActionNotReady), errors.Is(err, store.ErrInvalidAbandonCards):
		return DispatchResult{Err: ErrRoomActionIncorrect}, nil
	case err != nil:
		return DispatchResult{}, err
	case !created:
		return DispatchResult{Err: ErrRoomActionAlreadyDone}, nil
	}
	message := &protocolpb.AbandonCardS2C{PlayerId: playerID, CardUniqueIds: append([]int32(nil), resolved.RemovedIDs...)}
	ids := mustMemberIDs(ctx, s.store, roomID)
	pushes := []Push{
		s.pushFor("AbandonCardS2C", message, ids, playerID),
		s.pushFor("UpdateHeroAttrS2C", handCardSnapshotUpdate(playerID, resolved.Cards), ids, 0),
	}
	pushes = append(pushes, s.turnPushes(ctx, roomID, resolved.Round, resolved.NextPlayer)...)
	s.log.Info("player discarded excess battle cards", "room_id", roomID, "player_id", playerID, "discard_count", len(resolved.RemovedIDs), "hand_count", len(resolved.Cards))
	return DispatchResult{Message: message, Pushes: pushes}, nil
}

func handCardSnapshotUpdate(playerID int64, cards []store.CardState) *protocolpb.UpdateHeroAttrS2C {
	messages := make([]*modelpb.CardInfo, 0, len(cards))
	for _, card := range cards {
		messages = append(messages, &modelpb.CardInfo{
			UniqueId:   card.UniqueID,
			CardId:     card.CardID,
			PurifyNum:  card.PurifyNum,
			IsTemp:     card.IsTemp,
			BattleCost: card.BattleCost,
		})
	}
	return &protocolpb.UpdateHeroAttrS2C{
		PlayerId: playerID,
		Cause:    &protocolpb.CauseOrigin{S: protocolpb.CauseOrigin_unknown},
		EffectDatas: []*protocolpb.HeroAttrEffect{{
			PlayerId: playerID,
			Data: &protocolpb.HeroAttrEffect_Card{Card: &protocolpb.HeroCardChangeS2C{
				PlayerId: playerID,
				Cards:    messages,
			}},
		}},
	}
}
