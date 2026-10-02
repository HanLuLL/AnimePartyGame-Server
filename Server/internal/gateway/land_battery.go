package gateway

import (
	"context"
	"errors"
	"fmt"

	store "astralparty-server/internal/db"
	wire "astralparty-server/internal/protocol"
	protocolpb "astralparty-server/internal/protocol/gen/protocol"
	"google.golang.org/protobuf/proto"
)

const (
	batteryLandType    int32 = 11
	batteryTargetLimit int32 = 1
)

var errBatteryConfigMissing = errors.New("Battery land damage is missing or invalid")

type batteryResolution struct {
	Action  store.GameActionResult
	Message *protocolpb.LandChoiceTargetS2C
	LandID  int32
}

func (s *Server) batteryDamage() (int32, error) {
	params, ok := s.domain.LandParams(batteryLandType)
	if !ok || len(params) == 0 || params[0] >= 0 || params[0] == -1<<31 {
		return 0, errBatteryConfigMissing
	}
	return -params[0], nil
}

func (s *Server) batteryTargets(room store.Room, actor store.Player) []store.Player {
	var targets []store.Player
	actorTeam := roomTeamID(room.Mode, actor.Slot)
	for _, candidate := range room.Players {
		if candidate.ID == actor.ID || candidate.HP <= 0 || roomTeamID(room.Mode, candidate.Slot) == actorTeam {
			continue
		}
		targets = append(targets, candidate)
	}
	return targets
}

func (s *Server) batteryActionData(room store.Room, actor store.Player) (*protocolpb.LandChoiceTargetC2S, bool) {
	targets := s.batteryTargets(room, actor)
	if len(targets) == 0 {
		return nil, false
	}
	canTarget := make(map[int64]bool, len(targets))
	for _, target := range targets {
		canTarget[target.ID] = true
	}
	return &protocolpb.LandChoiceTargetC2S{
		LandType:     batteryLandType,
		TargetNum:    batteryTargetLimit,
		CanTargetIds: canTarget,
	}, true
}

func (s *Server) completeBatteryChoice(ctx context.Context, roomID, playerID int64, in wire.Frame, q *protocolpb.LandChoiceTargetC2S) (batteryResolution, bool, error) {
	if q == nil {
		return batteryResolution{}, false, errors.New("Battery request is empty")
	}
	damage, err := s.batteryDamage()
	if err != nil {
		return batteryResolution{}, false, err
	}
	current, _, pending, err := s.store.CurrentTurn(ctx, roomID)
	if err != nil {
		return batteryResolution{}, false, err
	}
	phase, err := s.store.CurrentTurnPhase(ctx, roomID)
	if err != nil {
		return batteryResolution{}, false, err
	}
	if current != playerID || pending != 0 || phase != store.TurnPhaseBattery {
		return batteryResolution{}, false, store.ErrActionNotReady
	}
	room, err := s.store.RoomSnapshot(ctx, roomID)
	if err != nil {
		return batteryResolution{}, false, err
	}
	var actor store.Player
	for _, player := range room.Players {
		if player.ID == playerID {
			actor = player
			break
		}
	}
	if actor.ID == 0 {
		return batteryResolution{}, false, store.ErrPlayerNotFound
	}
	landType, exists := s.domain.LandTypeAt(room, actor.NodeID)
	if !exists || landType != batteryLandType {
		return batteryResolution{}, false, store.ErrActionNotReady
	}
	eligible := make(map[int64]store.Player)
	for _, target := range s.batteryTargets(room, actor) {
		eligible[target.ID] = target
	}
	if q.Exit {
		if len(q.TargetIds) != 0 {
			return batteryResolution{}, false, store.ErrActionNotReady
		}
	} else {
		if len(q.TargetIds) == 0 || len(q.TargetIds) > int(batteryTargetLimit) {
			return batteryResolution{}, false, store.ErrActionNotReady
		}
		seen := make(map[int64]struct{}, len(q.TargetIds))
		for _, targetID := range q.TargetIds {
			if _, duplicate := seen[targetID]; duplicate {
				return batteryResolution{}, false, store.ErrActionNotReady
			}
			if _, valid := eligible[targetID]; !valid {
				return batteryResolution{}, false, store.ErrActionNotReady
			}
			seen[targetID] = struct{}{}
		}
	}
	raw, err := proto.Marshal(q)
	if err != nil {
		return batteryResolution{}, false, err
	}
	deltas := make([]store.EventAttrDelta, 0, len(q.TargetIds)+1)
	if q.Exit {
		// The shared transactional action path requires at least one affected
		// player. A zero-delta actor row makes an explicit leave idempotent while
		// leaving all gameplay attributes unchanged.
		deltas = append(deltas, store.EventAttrDelta{PlayerID: playerID, MaxHP: s.domain.HeroMaxHP(actor.HeroID)})
	} else {
		for _, targetID := range q.TargetIds {
			target := eligible[targetID]
			deltas = append(deltas, store.EventAttrDelta{PlayerID: targetID, HPChange: -damage, MaxHP: s.domain.HeroMaxHP(target.HeroID)})
		}
	}
	result, created, err := s.store.CompleteGameAction(ctx, roomID, playerID, in.CmdID, in.UPSN, raw, batteryLandType, store.TurnPhaseBattery, deltas, nil)
	if err != nil {
		return batteryResolution{}, false, err
	}
	if !created {
		return batteryResolution{}, false, nil
	}
	message := &protocolpb.LandChoiceTargetS2C{PlayerId: playerID, TargetIds: append([]int64(nil), q.TargetIds...), Exit: q.Exit}
	s.log.Info("Battery land resolved", "room_id", roomID, "player_id", playerID, "land_id", actor.NodeID, "target_ids", q.TargetIds, "exit", q.Exit, "damage", damage)
	return batteryResolution{Action: result, Message: message, LandID: actor.NodeID}, true, nil
}

func batteryAttrUpdate(playerID int64, landID int32, changes []store.EventAttrResult) *protocolpb.UpdateHeroAttrS2C {
	update := &protocolpb.UpdateHeroAttrS2C{PlayerId: playerID, Cause: &protocolpb.CauseOrigin{S: protocolpb.CauseOrigin_land, Id: int64(landID)}}
	for _, change := range changes {
		if change.OldHP != change.NewHP {
			update.EffectDatas = append(update.EffectDatas, &protocolpb.HeroAttrEffect{
				PlayerId: change.PlayerID,
				Data: &protocolpb.HeroAttrEffect_Hp{Hp: &protocolpb.HeroHpChangeS2C{
					PlayerId: change.PlayerID, ChangeHp: change.NewHP - change.OldHP,
					OriHp: change.OldHP, CurrHp: change.NewHP, RealChangeHp: change.NewHP - change.OldHP,
					MaxHp: change.MaxHP,
				}},
			})
		}
		for _, buff := range change.RemovedBuffs {
			update.EffectDatas = append(update.EffectDatas, &protocolpb.HeroAttrEffect{
				PlayerId: change.PlayerID,
				Data: &protocolpb.HeroAttrEffect_Buff{Buff: &protocolpb.HeroBuffChangeS2C{
					PlayerId: change.PlayerID, Buff: buffMessage(buff), Op: protocolpb.HeroBuffChangeS2C_Delete,
				}},
			})
		}
	}
	return update
}

func (s *Server) handleLandChoiceTarget(ctx context.Context, sess *Session, in wire.Frame, q *protocolpb.LandChoiceTargetC2S) (DispatchResult, error) {
	_, playerID, ok := sessionPlayer(sess)
	if !ok {
		return DispatchResult{Err: ErrAuth}, nil
	}
	roomID, err := s.store.RoomForPlayer(ctx, playerID)
	if err != nil {
		return DispatchResult{Err: ErrRoomNotAction}, nil
	}
	resolved, created, err := s.completeBatteryChoice(ctx, roomID, playerID, in, q)
	if errors.Is(err, store.ErrTurnPlayerMismatch) {
		return DispatchResult{Err: ErrRoomActionPlayer}, nil
	}
	if errors.Is(err, store.ErrActionNotReady) {
		return DispatchResult{Err: ErrRoomActionIncorrect}, nil
	}
	if errors.Is(err, errBatteryConfigMissing) {
		return DispatchResult{Err: ErrNotOpen}, nil
	}
	if err != nil {
		return DispatchResult{}, err
	}
	if !created {
		return DispatchResult{Err: ErrRoomActionAlreadyDone}, nil
	}
	ids := mustMemberIDs(ctx, s.store, roomID)
	pushes := []Push{s.pushFor("LandChoiceTargetS2C", resolved.Message, ids, playerID)}
	if len(resolved.Action.Changes) > 0 {
		pushes = append(pushes, s.pushFor("UpdateHeroAttrS2C", batteryAttrUpdate(playerID, resolved.LandID, resolved.Action.Changes), ids, 0))
	}
	pushes = append(pushes, s.turnPushes(ctx, roomID, resolved.Action.Round, resolved.Action.NextPlayer)...)
	return DispatchResult{Message: resolved.Message, Pushes: pushes}, nil
}

func (s *Server) resolveBotBattery(ctx context.Context, room store.Room, actor store.Player, ids []int64) (int64, int32, []Push, error) {
	targets := s.batteryTargets(room, actor)
	if len(targets) == 0 {
		return 0, 0, nil, fmt.Errorf("Battery phase has no eligible target")
	}
	// Deterministic bot targeting keeps replay and server logs understandable;
	// bots prefer the most vulnerable eligible opponent, then room slot order.
	selected := targets[0]
	for _, candidate := range targets[1:] {
		if candidate.HP < selected.HP {
			selected = candidate
		}
	}
	request := &protocolpb.LandChoiceTargetC2S{TargetIds: []int64{selected.ID}}
	in := wire.Frame{CmdID: 5063, UPSN: s.nextActionSN()}
	result, created, err := s.completeBatteryChoice(ctx, room.ID, actor.ID, in, request)
	if err != nil {
		return 0, 0, nil, err
	}
	if !created {
		return 0, 0, nil, store.ErrActionNotReady
	}
	pushes := []Push{s.pushFor("LandChoiceTargetS2C", result.Message, ids, 0)}
	pushes = append(pushes, s.pushFor("UpdateHeroAttrS2C", batteryAttrUpdate(actor.ID, result.LandID, result.Action.Changes), ids, 0))
	return result.Action.NextPlayer, result.Action.Round, pushes, nil
}
