package gateway

import (
	"context"
	"crypto/rand"
	"encoding/json"
	"errors"
	"math/big"

	store "astralparty-server/internal/db"
	wire "astralparty-server/internal/protocol"
	protocolpb "astralparty-server/internal/protocol/gen/protocol"
	"google.golang.org/protobuf/proto"
)

const skill10202ID int32 = 10202

type skillEventConfig struct {
	Round  int32   `json:"round"`
	Params []int32 `json:"params"`
}

func (s *Server) activeSkillID(room store.Room, player store.Player) int32 {
	raw, ok := s.resources.Get("Character_infos", int64(player.HeroID))
	if !ok {
		return 0
	}
	var character struct {
		ActiveSkill    int32 `json:"activeSkill"`
		PVEActiveSkill int32 `json:"pveActiveSkill"`
	}
	if json.Unmarshal(raw, &character) != nil {
		return 0
	}
	if !usesPVEPassiveSkills(room.Mode) {
		return character.ActiveSkill
	}
	activeSkill := character.PVEActiveSkill
	if activeSkill == 0 {
		activeSkill = character.ActiveSkill
	}
	if player.PveTalentID <= 0 {
		return activeSkill
	}
	raw, ok = s.resources.Get("PVENurturance_breaks", int64(player.PveTalentID))
	if !ok {
		return activeSkill
	}
	var talent struct {
		ReplaceActiveSkill int32 `json:"replaceActiveSkill"`
	}
	if json.Unmarshal(raw, &talent) != nil || talent.ReplaceActiveSkill <= 0 {
		return activeSkill
	}
	return talent.ReplaceActiveSkill
}

func (s *Server) skillEventConfig(skillID int32) (skillEventConfig, bool) {
	raw, ok := s.resources.Get("Skill_infos", int64(skillID))
	if !ok {
		return skillEventConfig{}, false
	}
	var config skillEventConfig
	if json.Unmarshal(raw, &config) != nil || config.Round <= 0 || len(config.Params) == 0 || config.Params[0] != 2 {
		return skillEventConfig{}, false
	}
	return config, true
}

func (s *Server) skillEventChoices(room store.Room, count int) ([]int32, error) {
	candidates, err := s.supportedEventIDs(room)
	if err != nil {
		return nil, err
	}
	if count <= 0 || count > len(candidates) {
		return nil, errEventConfigMissing
	}
	for i := 0; i < count; i++ {
		n, randomErr := rand.Int(rand.Reader, big.NewInt(int64(len(candidates)-i)))
		if randomErr != nil {
			return nil, randomErr
		}
		j := i + int(n.Int64())
		candidates[i], candidates[j] = candidates[j], candidates[i]
	}
	return append([]int32(nil), candidates[:count]...), nil
}

func (s *Server) handleUseSkillEvent(ctx context.Context, sess *Session, in wire.Frame, q *protocolpb.UseEffectCardC2S, roomID, playerID int64, round int32) (DispatchResult, error) {
	room, err := s.store.RoomSnapshot(ctx, roomID)
	if err != nil {
		return DispatchResult{}, err
	}
	var player store.Player
	for _, member := range room.Players {
		if member.ID == playerID {
			player = member
			break
		}
	}
	if player.ID == 0 {
		return DispatchResult{Err: ErrRoomNotAction}, nil
	}
	skillID := q.GetSkillId()
	if !usesPVEPassiveSkills(room.Mode) || skillID != skill10202ID || s.activeSkillID(room, player) != skillID {
		return DispatchResult{Err: ErrNotOpen}, nil
	}
	if q.GetCardId() != 0 || q.GetUseSelectCardIndex() != 0 || q.GetDicePoint() != 0 || q.GetCounterPlayer() != 0 || q.GetNotUseSkill() || len(q.GetTargetIds()) != 0 || len(q.GetTargetNodeIds()) != 0 || len(q.GetCardUniqueIds()) != 0 || len(q.GetLandBuffUniqueIds()) != 0 {
		return DispatchResult{Err: ErrInvalidParam}, nil
	}
	config, ok := s.skillEventConfig(skillID)
	if !ok {
		return DispatchResult{Err: ErrNotOpen}, nil
	}
	choices, err := s.skillEventChoices(room, int(config.Params[0]))
	if errors.Is(err, errEventConfigMissing) {
		return DispatchResult{Err: ErrNotOpen}, nil
	}
	if err != nil {
		return DispatchResult{}, err
	}
	raw, err := proto.Marshal(q)
	if err != nil {
		return DispatchResult{}, err
	}
	actionSN := s.nextActionSN()
	created, err := s.store.BeginSkillEventSelection(ctx, roomID, playerID, round, in.CmdID, in.UPSN, raw, actionSN, skillID, config.Round, choices)
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
	message := &protocolpb.UseEffectCardS2C{PlayerId: playerID, UseSkill: true, SkillId: skillID, SkillCds: map[int32]int32{skillID: config.Round}}
	pushes := []Push{
		s.pushFor("UseEffectCardS2C", message, memberIDs(room), playerID),
		s.actionPushWithSN(round, 5317, playerID, &protocolpb.SelectEventC2S{Events: choices, Idx: 0}, actionSN),
	}
	s.log.Info("PVE skill opened event selection", "room_id", roomID, "player_id", playerID, "skill_id", skillID, "event_ids", choices, "round", round)
	return DispatchResult{Message: message, Pushes: pushes}, nil
}

func (s *Server) handleSelectEvent(ctx context.Context, sess *Session, in wire.Frame, q *protocolpb.SelectEventC2S) (DispatchResult, error) {
	_, playerID, ok := sessionPlayer(sess)
	if !ok {
		return DispatchResult{Err: ErrAuth}, nil
	}
	roomID, err := s.store.RoomForPlayer(ctx, playerID)
	if err != nil {
		return DispatchResult{Err: ErrRoomNotAction}, nil
	}
	current, round, pending, err := s.store.CurrentTurn(ctx, roomID)
	if err != nil {
		return DispatchResult{Err: ErrRoomNotAction}, nil
	}
	if current != playerID {
		return DispatchResult{Err: ErrRoomActionPlayer}, nil
	}
	phase, err := s.store.CurrentTurnPhase(ctx, roomID)
	if err != nil || pending != 0 || phase != store.TurnPhaseSelectEvent {
		return DispatchResult{Err: ErrRoomActionIncorrect}, nil
	}
	choices, actionSN, exists, err := s.store.SkillEventOffer(ctx, roomID, playerID)
	if err != nil {
		return DispatchResult{}, err
	}
	if !exists || q.GetInfo() == nil || q.GetInfo().GetSn() != actionSN || len(q.GetEvents()) != len(choices) || q.GetIdx() < 0 || int(q.GetIdx()) >= len(choices) {
		return DispatchResult{Err: ErrRoomActionIncorrect}, nil
	}
	for i, eventID := range choices {
		if q.GetEvents()[i] != eventID {
			return DispatchResult{Err: ErrRoomActionIncorrect}, nil
		}
	}
	eventID := choices[q.GetIdx()]
	room, err := s.store.RoomSnapshot(ctx, roomID)
	if err != nil {
		return DispatchResult{}, err
	}
	var player store.Player
	for _, member := range room.Players {
		if member.ID == playerID {
			player = member
			break
		}
	}
	if player.ID == 0 || !s.domain.EventEffectSupported(eventID) || !s.domain.EventAvailableInRoom(eventID, room) || !s.domain.EventInfoExists(eventID) {
		return DispatchResult{Err: ErrRoomActionIncorrect}, nil
	}
	params, paramsOK := s.domain.EventParams(eventID)
	buffIDs, buffsOK := s.domain.EventBuffIDs(eventID)
	if !paramsOK || (len(params) == 0 && (!buffsOK || len(buffIDs) == 0) && eventID != 30001 && eventID != 30005 && eventID != 30013 && eventID != 30021) {
		return DispatchResult{Err: ErrNotOpen}, nil
	}
	deltas, err := s.eventDeltas(eventID, params, room, playerID)
	if err != nil {
		if errors.Is(err, errEventConfigMissing) {
			return DispatchResult{Err: ErrNotOpen}, nil
		}
		return DispatchResult{}, err
	}
	deltas, err = s.applyEventPassives(room, player, deltas)
	if err != nil {
		return DispatchResult{}, err
	}
	var progressDelta *int32
	if eventID == 30201 || eventID == 30205 {
		if len(params) == 0 {
			return DispatchResult{Err: ErrNotOpen}, nil
		}
		change := params[0]
		progressDelta = &change
	}
	raw, err := proto.Marshal(q)
	if err != nil {
		return DispatchResult{}, err
	}
	result, created, err := s.store.CompleteGameAction(ctx, roomID, playerID, in.CmdID, in.UPSN, raw, eventID, store.TurnPhaseSelectEvent, deltas, progressDelta)
	if errors.Is(err, store.ErrTurnPlayerMismatch) {
		return DispatchResult{Err: ErrRoomActionPlayer}, nil
	}
	if errors.Is(err, store.ErrActionNotReady) || errors.Is(err, store.ErrInvalidSkillEventChoice) {
		return DispatchResult{Err: ErrRoomActionIncorrect}, nil
	}
	if err != nil {
		return DispatchResult{}, err
	}
	if !created {
		return DispatchResult{Err: ErrRoomActionAlreadyDone}, nil
	}
	message := &protocolpb.SelectEventS2C{PlayerId: playerID, EventId: eventID}
	_, pushes := s.eventMessages(ctx, roomID, playerID, resolvedEvent{GameEventResult: result})
	if len(pushes) > 0 {
		pushes[0].Exclude = 0 // SelectEventS2C is the request response, not TriggerEventS2C.
	}
	pushes = append(pushes, s.actionPush(result.Round, 5021, playerID, &protocolpb.ThrowDiceC2S{}))
	s.log.Info("PVE skill event resolved", "room_id", roomID, "player_id", playerID, "skill_id", skill10202ID, "event_id", eventID, "round", round)
	return DispatchResult{Message: message, Pushes: pushes}, nil
}
