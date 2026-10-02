package gateway

import (
	"context"
	"crypto/rand"
	"errors"
	"math/big"

	store "astralparty-server/internal/db"
	wire "astralparty-server/internal/protocol"
	protocolpb "astralparty-server/internal/protocol/gen/protocol"
	"google.golang.org/protobuf/proto"
)

var errRollGoldConfigMissing = errors.New("RollGold land parameters are missing")

type rollGoldResult struct {
	Point      int32
	Amount     int32
	OldGold    int32
	NewGold    int32
	NextPlayer int64
	Round      int32
}

func (s *Server) resolveRollGold(ctx context.Context, roomID, playerID int64, cmd uint16, upsn int64, request proto.Message) (rollGoldResult, bool, error) {
	params, ok := s.domain.LandParams(15)
	if !ok {
		return rollGoldResult{}, false, errRollGoldConfigMissing
	}
	n, err := rand.Int(rand.Reader, big.NewInt(int64(len(params))))
	if err != nil {
		return rollGoldResult{}, false, err
	}
	point := int32(n.Int64())
	amount := params[point]
	if amount < 0 {
		return rollGoldResult{}, false, errors.New("RollGold resource contains a negative reward")
	}
	raw, err := proto.Marshal(request)
	if err != nil {
		return rollGoldResult{}, false, err
	}
	oldGold, newGold, nextPlayer, round, created, err := s.store.CompleteRollGold(ctx, roomID, playerID, cmd, upsn, raw, amount)
	if err != nil {
		return rollGoldResult{}, false, err
	}
	return rollGoldResult{
		Point:      point,
		Amount:     amount,
		OldGold:    oldGold,
		NewGold:    newGold,
		NextPlayer: nextPlayer,
		Round:      round,
	}, created, nil
}

func rollGoldAttrUpdate(playerID int64, landID int32, result rollGoldResult) *protocolpb.UpdateHeroAttrS2C {
	return &protocolpb.UpdateHeroAttrS2C{
		PlayerId: playerID,
		Cause:    &protocolpb.CauseOrigin{S: protocolpb.CauseOrigin_land, Id: int64(landID)},
		EffectDatas: []*protocolpb.HeroAttrEffect{{
			PlayerId: playerID,
			Data: &protocolpb.HeroAttrEffect_Gold{Gold: &protocolpb.HeroGoldChangeS2C{
				PlayerId:   playerID,
				ChangeGold: result.Amount,
				OriGold:    result.OldGold,
				CurrGold:   result.NewGold,
			}},
		}},
	}
}

func (s *Server) handleRollGold(ctx context.Context, sess *Session, in wire.Frame, q *protocolpb.RollGoldC2S) (DispatchResult, error) {
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
	if pending != 0 || phase != store.TurnPhaseRollGold {
		return DispatchResult{Err: ErrRoomActionIncorrect}, nil
	}
	room, err := s.store.RoomSnapshot(ctx, roomID)
	if err != nil {
		return DispatchResult{}, err
	}
	var landID int32
	for _, player := range room.Players {
		if player.ID == playerID {
			landID = player.NodeID
			break
		}
	}
	landType, exists := s.domain.LandTypeAt(room, landID)
	if landID < 0 || !exists || landType != 15 {
		return DispatchResult{Err: ErrRoomActionIncorrect}, nil
	}
	result, created, err := s.resolveRollGold(ctx, roomID, playerID, in.CmdID, in.UPSN, q)
	if errors.Is(err, errRollGoldConfigMissing) {
		return DispatchResult{Err: ErrNotOpen}, nil
	}
	if errors.Is(err, store.ErrTurnPlayerMismatch) {
		return DispatchResult{Err: ErrRoomActionPlayer}, nil
	}
	if errors.Is(err, store.ErrActionNotReady) {
		return DispatchResult{Err: ErrRoomActionIncorrect}, nil
	}
	if err != nil {
		return DispatchResult{}, err
	}
	if !created {
		return DispatchResult{Err: ErrRoomActionAlreadyDone}, nil
	}
	s.log.Info("RollGold land resolved", "room_id", roomID, "player_id", playerID, "land_id", landID, "point", result.Point, "gold_change", result.Amount, "gold", result.NewGold)
	ids := mustMemberIDs(ctx, s.store, roomID)
	pushes := []Push{s.pushFor("UpdateHeroAttrS2C", rollGoldAttrUpdate(playerID, landID, result), ids, 0)}
	pushes = append(pushes, s.turnPushes(ctx, roomID, result.Round, result.NextPlayer)...)
	return DispatchResult{Message: &protocolpb.RollGoldS2C{Point: result.Point}, Pushes: pushes}, nil
}

func (s *Server) resolveBotRollGold(ctx context.Context, roomID, playerID int64, landID int32, ids []int64) (int64, int32, []Push, error) {
	q := &protocolpb.RollGoldC2S{}
	result, created, err := s.resolveRollGold(ctx, roomID, playerID, 5049, s.nextActionSN(), q)
	if err != nil {
		return 0, 0, nil, err
	}
	if !created {
		return 0, 0, nil, store.ErrActionNotReady
	}
	s.log.Info("bot RollGold land resolved", "room_id", roomID, "player_id", playerID, "land_id", landID, "point", result.Point, "gold_change", result.Amount, "gold", result.NewGold)
	pushes := []Push{
		s.pushFor("RollGoldS2C", &protocolpb.RollGoldS2C{Point: result.Point}, ids, 0),
		s.pushFor("UpdateHeroAttrS2C", rollGoldAttrUpdate(playerID, landID, result), ids, 0),
	}
	return result.NextPlayer, result.Round, pushes, nil
}
