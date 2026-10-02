package gateway

import (
	"context"
	"errors"

	store "astralparty-server/internal/db"
	wire "astralparty-server/internal/protocol"
	protocolpb "astralparty-server/internal/protocol/gen/protocol"
	"google.golang.org/protobuf/proto"
)

var errHospitalConfigMissing = errors.New("Hospital land parameters are missing")

func (s *Server) handleTriggerHospital(ctx context.Context, sess *Session, in wire.Frame, q *protocolpb.TriggerHospitalC2S) (DispatchResult, error) {
	_, playerID, ok := sessionPlayer(sess)
	if !ok {
		return DispatchResult{Err: ErrAuth}, nil
	}
	roomID, err := s.store.RoomForPlayer(ctx, playerID)
	if err != nil {
		return DispatchResult{Err: ErrRoomNotAction}, nil
	}
	result, created, err := s.resolveTriggerHospital(ctx, roomID, playerID, in.CmdID, in.UPSN, q)
	if errors.Is(err, store.ErrTurnPlayerMismatch) {
		return DispatchResult{Err: ErrRoomActionPlayer}, nil
	}
	if errors.Is(err, store.ErrActionNotReady) {
		return DispatchResult{Err: ErrRoomActionIncorrect}, nil
	}
	if errors.Is(err, errHospitalConfigMissing) {
		return DispatchResult{Err: ErrNotOpen}, nil
	}
	if err != nil {
		return DispatchResult{}, err
	}
	if !created {
		return DispatchResult{Err: ErrRoomActionAlreadyDone}, nil
	}
	message, pushes := hospitalMessages(ctx, s, roomID, playerID, result)
	pushes = append(pushes, s.turnPushes(ctx, roomID, result.Round, result.NextPlayer)...)
	s.log.Info("Hospital examination resolved", "room_id", roomID, "player_id", playerID, "hospitalized", result.InHospital, "old_hp", result.OldHP, "hp", result.NewHP)
	return DispatchResult{Message: message, Pushes: pushes}, nil
}

func (s *Server) resolveTriggerHospital(ctx context.Context, roomID, playerID int64, cmd uint16, upsn int64, request proto.Message) (store.HospitalResult, bool, error) {
	room, err := s.store.RoomSnapshot(ctx, roomID)
	if err != nil {
		return store.HospitalResult{}, false, err
	}
	var player store.Player
	for _, member := range room.Players {
		if member.ID == playerID {
			player = member
			break
		}
	}
	if player.ID == 0 {
		return store.HospitalResult{}, false, store.ErrActionNotReady
	}
	landType, exists := s.domain.LandTypeAt(room, player.NodeID)
	if !exists || landType != 13 {
		return store.HospitalResult{}, false, store.ErrActionNotReady
	}
	current, _, pending, err := s.store.CurrentTurn(ctx, roomID)
	if err != nil {
		return store.HospitalResult{}, false, err
	}
	if current != playerID {
		return store.HospitalResult{}, false, store.ErrTurnPlayerMismatch
	}
	phase, err := s.store.CurrentTurnPhase(ctx, roomID)
	if err != nil {
		return store.HospitalResult{}, false, err
	}
	if pending != 0 || phase != store.TurnPhaseHospital {
		return store.HospitalResult{}, false, store.ErrActionNotReady
	}
	params, ok := s.domain.LandParams(13)
	if !ok || len(params) < 2 || params[0] < 0 || params[1] < 0 {
		return store.HospitalResult{}, false, errHospitalConfigMissing
	}
	maxHP := s.domain.HeroMaxHP(player.HeroID)
	if maxHP <= 0 {
		return store.HospitalResult{}, false, errHospitalConfigMissing
	}
	healAmount := s.applyHeroHPChangePassive(room, playerID, params[1])
	raw, err := proto.Marshal(request)
	if err != nil {
		return store.HospitalResult{}, false, err
	}
	// Land_infos[13].params[0] is interpreted as the sick-HP threshold and
	// params[1] as recovery. The client text confirms +2 HP and one hospital
	// round; it does not expose the threshold comparison itself.
	return s.store.CompleteHospitalExam(ctx, roomID, playerID, cmd, upsn, raw, params[0], healAmount, maxHP)
}

func hospitalMessages(ctx context.Context, s *Server, roomID, playerID int64, result store.HospitalResult) (*protocolpb.TriggerHospitalS2C, []Push) {
	ids := mustMemberIDs(ctx, s.store, roomID)
	message := &protocolpb.TriggerHospitalS2C{PlayerId: playerID, InHospital: result.InHospital}
	pushes := []Push{s.pushFor("TriggerHospitalS2C", message, ids, playerID)}
	if result.NewHP != result.OldHP {
		change := result.NewHP - result.OldHP
		update := &protocolpb.UpdateHeroAttrS2C{
			PlayerId: playerID,
			Cause:    &protocolpb.CauseOrigin{S: protocolpb.CauseOrigin_land, Id: int64(result.NodeID)},
			EffectDatas: []*protocolpb.HeroAttrEffect{{
				PlayerId: playerID,
				Data: &protocolpb.HeroAttrEffect_Hp{Hp: &protocolpb.HeroHpChangeS2C{
					PlayerId: playerID, ChangeHp: change, OriHp: result.OldHP,
					CurrHp: result.NewHP, RealChangeHp: change, MaxHp: result.MaxHP,
				}},
			}},
		}
		pushes = append(pushes, s.pushFor("UpdateHeroAttrS2C", update, ids, 0))
	}
	return message, pushes
}

func (s *Server) skipHospitalizedTurn(ctx context.Context, roomID int64, player store.Player, round, pending int32, phase string, ids []int64) (bool, int64, int32, []Push, error) {
	if player.HospitalRounds <= 0 || pending != 0 || phase != store.TurnPhaseThrowDice {
		return false, 0, round, nil, nil
	}
	nextPlayer, nextRound, skipped, err := s.store.CompleteHospitalizedTurn(ctx, roomID, player.ID)
	if err != nil {
		s.log.Error("hospitalized player turn could not be skipped", "room_id", roomID, "player_id", player.ID, "err", err)
		return false, 0, round, nil, err
	}
	if !skipped {
		return false, nextPlayer, nextRound, nil, nil
	}
	s.log.Info("hospitalized player skipped one round", "room_id", roomID, "player_id", player.ID, "next_player_id", nextPlayer, "round", nextRound)
	pushes := []Push{
		s.pushFor("GameRoundChangeS2C", &protocolpb.GameRoundChangeS2C{Round: round}, ids, 0),
		s.pushFor("ActionStartNotifyS2C", &protocolpb.ActionStartNotifyS2C{PlayerId: player.ID, IsHospital: true}, ids, 0),
	}
	return true, nextPlayer, nextRound, pushes, nil
}
