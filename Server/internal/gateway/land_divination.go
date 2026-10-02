package gateway

import (
	"context"
	"crypto/rand"
	"encoding/json"
	"errors"
	"fmt"
	"math/big"

	store "astralparty-server/internal/db"
	wire "astralparty-server/internal/protocol"
	modelpb "astralparty-server/internal/protocol/gen/model"
	protocolpb "astralparty-server/internal/protocol/gen/protocol"
	"google.golang.org/protobuf/proto"
)

var errDivinationConfigMissing = errors.New("Divination resources or outcome are unavailable")

type divinationInfo struct {
	ID     int32   `json:"id"`
	Params []int32 `json:"params"`
}

func (s *Server) divinationSettings() (int32, error) {
	params, ok := s.domain.LandParams(6)
	if !ok || len(params) == 0 || params[0] != 2 {
		return 0, errDivinationConfigMissing
	}
	return params[0], nil
}

func (s *Server) divinationInfo(id int32) (divinationInfo, bool) {
	raw, ok := s.resources.Get("Divination_infos", int64(id))
	if !ok {
		return divinationInfo{}, false
	}
	var info divinationInfo
	if json.Unmarshal(raw, &info) != nil || info.ID != id || len(info.Params) == 0 {
		return divinationInfo{}, false
	}
	return info, true
}

func (s *Server) divinationIDs() []int32 {
	ids := make([]int32, 0, 8)
	for _, id := range s.resources.IDs("Divination_targets") {
		if id > 0 && id <= 8 {
			ids = append(ids, int32(id))
		}
	}
	return ids
}

func (s *Server) divinationCandidates(room store.Room) []int32 {
	ids := make([]int32, 0, 6)
	for _, rawID := range s.resources.IDs("Divination_infos") {
		if rawID <= 0 || rawID > int64(1<<31-1) {
			continue
		}
		id := int32(rawID)
		if _, ok := s.divinationInfo(id); !ok {
			continue
		}
		if id == 50005 {
			if room.Mode != 10 || room.MapID != 1001 && room.MapID != 1002 {
				poolID, _, ok := s.domain.CardPoolsForMapMode(int64(room.MapID), room.Mode)
				if !ok {
					continue
				}
				pool, ok := s.domain.EffectCardIDs(poolID)
				if !ok || len(pool) == 0 {
					continue
				}
			}
		}
		ids = append(ids, id)
	}
	return ids
}

// newDivinationChoices draws the two cards configured for a Divination land.
// The client dump proves the count and submission shape; the uniform draw is
// an explicit inference until an original live-session trace is available.
func (s *Server) newDivinationChoices(room store.Room) ([]int32, error) {
	count, err := s.divinationSettings()
	if err != nil {
		return nil, err
	}
	candidates := s.divinationCandidates(room)
	if len(candidates) < int(count) || len(s.divinationIDs()) == 0 {
		return nil, errDivinationConfigMissing
	}
	for i := 0; i < int(count); i++ {
		remaining := len(candidates) - i
		n, randomErr := rand.Int(rand.Reader, big.NewInt(int64(remaining)))
		if randomErr != nil {
			return nil, randomErr
		}
		j := i + int(n.Int64())
		candidates[i], candidates[j] = candidates[j], candidates[i]
	}
	return append([]int32(nil), candidates[:count]...), nil
}

func (s *Server) handleTriggerDivination(ctx context.Context, sess *Session, in wire.Frame, q *protocolpb.TriggerDivinationC2S) (DispatchResult, error) {
	_, playerID, ok := sessionPlayer(sess)
	if !ok {
		return DispatchResult{Err: ErrAuth}, nil
	}
	roomID, err := s.store.RoomForPlayer(ctx, playerID)
	if err != nil {
		return DispatchResult{Err: ErrRoomNotAction}, nil
	}
	result, targetType, targetIDs, created, err := s.resolveTriggerDivination(ctx, roomID, playerID, in.CmdID, in.UPSN, q)
	if errors.Is(err, store.ErrTurnPlayerMismatch) {
		return DispatchResult{Err: ErrRoomActionPlayer}, nil
	}
	if errors.Is(err, store.ErrActionNotReady) || errors.Is(err, store.ErrInvalidDivinationChoice) {
		return DispatchResult{Err: ErrRoomActionIncorrect}, nil
	}
	if errors.Is(err, errDivinationConfigMissing) {
		return DispatchResult{Err: ErrNotOpen}, nil
	}
	if err != nil {
		return DispatchResult{}, err
	}
	if !created {
		return DispatchResult{Err: ErrRoomActionAlreadyDone}, nil
	}
	message, pushes := s.divinationMessages(ctx, roomID, playerID, result, targetType, targetIDs)
	pushes = append(pushes, s.turnPushes(ctx, roomID, result.Round, result.NextPlayer)...)
	s.log.Info("Divination resolved", "room_id", roomID, "player_id", playerID, "divination_id", result.EventID, "target_type", targetType, "target_ids", targetIDs)
	return DispatchResult{Message: message, Pushes: pushes}, nil
}

func (s *Server) resolveTriggerDivination(ctx context.Context, roomID, playerID int64, cmd uint16, upsn int64, q *protocolpb.TriggerDivinationC2S) (store.GameActionResult, int32, []int64, bool, error) {
	room, err := s.store.RoomSnapshot(ctx, roomID)
	if err != nil {
		return store.GameActionResult{}, 0, nil, false, err
	}
	var player store.Player
	for _, member := range room.Players {
		if member.ID == playerID {
			player = member
			break
		}
	}
	if player.ID == 0 {
		return store.GameActionResult{}, 0, nil, false, store.ErrActionNotReady
	}
	current, _, pending, err := s.store.CurrentTurn(ctx, roomID)
	if err != nil {
		return store.GameActionResult{}, 0, nil, false, err
	}
	if current != playerID {
		return store.GameActionResult{}, 0, nil, false, store.ErrTurnPlayerMismatch
	}
	phase, err := s.store.CurrentTurnPhase(ctx, roomID)
	if err != nil {
		return store.GameActionResult{}, 0, nil, false, err
	}
	if pending != 0 || phase != store.TurnPhaseDivination {
		return store.GameActionResult{}, 0, nil, false, store.ErrActionNotReady
	}
	if landType, exists := s.domain.LandTypeAt(room, player.NodeID); !exists || landType != 6 {
		return store.GameActionResult{}, 0, nil, false, store.ErrActionNotReady
	}
	choices, exists, err := s.store.DivinationChoices(ctx, roomID, playerID)
	if err != nil {
		return store.GameActionResult{}, 0, nil, false, err
	}
	if !exists {
		return store.GameActionResult{}, 0, nil, false, store.ErrActionNotReady
	}
	allowed := false
	for _, id := range choices {
		if q.Id == id {
			allowed = true
			break
		}
	}
	if !allowed {
		return store.GameActionResult{}, 0, nil, false, store.ErrInvalidDivinationChoice
	}
	info, ok := s.divinationInfo(q.Id)
	if !ok || len(info.Params) == 0 {
		return store.GameActionResult{}, 0, nil, false, errDivinationConfigMissing
	}
	targetTypes := s.divinationIDs()
	if len(targetTypes) == 0 {
		return store.GameActionResult{}, 0, nil, false, errDivinationConfigMissing
	}
	targetRoll, err := rand.Int(rand.Reader, big.NewInt(int64(len(targetTypes))))
	if err != nil {
		return store.GameActionResult{}, 0, nil, false, err
	}
	targetType := targetTypes[targetRoll.Int64()]
	targets, err := divinationTargets(room.Players, targetType)
	if err != nil || len(targets) == 0 {
		return store.GameActionResult{}, 0, nil, false, errDivinationConfigMissing
	}
	deltas := make([]store.EventAttrDelta, 0, len(targets))
	for _, target := range targets {
		delta, deltaErr := s.divinationDelta(room, target, info)
		if deltaErr != nil {
			return store.GameActionResult{}, 0, nil, false, deltaErr
		}
		deltas = append(deltas, delta)
	}
	deltas = s.applyHeroHPChangePassives(room, deltas)
	raw, err := proto.Marshal(q)
	if err != nil {
		return store.GameActionResult{}, 0, nil, false, err
	}
	result, created, err := s.store.CompleteGameAction(ctx, roomID, playerID, cmd, upsn, raw, q.Id, store.TurnPhaseDivination, deltas, nil)
	if err != nil {
		return store.GameActionResult{}, 0, nil, false, err
	}
	targetIDs := make([]int64, 0, len(targets))
	for _, target := range targets {
		targetIDs = append(targetIDs, target.ID)
	}
	return result, targetType, targetIDs, created, nil
}

func divinationTargets(players []store.Player, targetType int32) ([]store.Player, error) {
	if len(players) == 0 || targetType < 1 || targetType > 8 {
		return nil, errDivinationConfigMissing
	}
	value := func(player store.Player) int64 {
		switch targetType {
		case 1, 2:
			return int64(player.HeroLevel)
		case 3, 4:
			return int64(player.HP)
		case 5, 6:
			return int64(player.Gold)
		default:
			return int64(len(player.Cards))
		}
	}
	selected := value(players[0])
	isMaximum := targetType%2 == 1
	for _, player := range players[1:] {
		candidate := value(player)
		if isMaximum && candidate > selected || !isMaximum && candidate < selected {
			selected = candidate
		}
	}
	result := make([]store.Player, 0, len(players))
	for _, player := range players {
		if value(player) == selected {
			result = append(result, player)
		}
	}
	return result, nil
}

func (s *Server) divinationDelta(room store.Room, player store.Player, info divinationInfo) (store.EventAttrDelta, error) {
	if len(info.Params) == 0 {
		return store.EventAttrDelta{}, errDivinationConfigMissing
	}
	delta := store.EventAttrDelta{PlayerID: player.ID}
	switch info.ID {
	case 50001, 50002:
		delta.GoldChange = info.Params[0]
	case 50003, 50004:
		maxHP := s.domain.HeroMaxHP(player.HeroID)
		if maxHP <= 0 {
			return store.EventAttrDelta{}, errDivinationConfigMissing
		}
		delta.HPChange, delta.MaxHP = info.Params[0], maxHP
	case 50005:
		cards, _, err := s.drawDestinyCards(room, player, info.Params[0])
		if err != nil {
			return store.EventAttrDelta{}, errDivinationConfigMissing
		}
		delta.Cards = cards
	case 50006:
		remaining, err := discardRandomCards(player.Cards, int(-info.Params[0]))
		if err != nil {
			return store.EventAttrDelta{}, err
		}
		if len(remaining) != len(player.Cards) {
			delta.Cards, delta.ReplaceCards = remaining, true
		}
	default:
		return store.EventAttrDelta{}, fmt.Errorf("unsupported Divination outcome %d", info.ID)
	}
	return delta, nil
}

func (s *Server) divinationMessages(ctx context.Context, roomID, playerID int64, result store.GameActionResult, targetType int32, targetIDs []int64) (*protocolpb.TriggerDivinationS2C, []Push) {
	ids := mustMemberIDs(ctx, s.store, roomID)
	message := &protocolpb.TriggerDivinationS2C{PlayerId: playerID, Id: result.EventID, TargetType: targetType, TargetIds: targetIDs}
	pushes := []Push{s.pushFor("TriggerDivinationS2C", message, ids, playerID)}
	effects := make([]*protocolpb.HeroAttrEffect, 0, len(result.Changes)*3)
	for _, change := range result.Changes {
		if change.NewHP != change.OldHP {
			delta := change.NewHP - change.OldHP
			effects = append(effects, &protocolpb.HeroAttrEffect{PlayerId: change.PlayerID, Data: &protocolpb.HeroAttrEffect_Hp{Hp: &protocolpb.HeroHpChangeS2C{
				PlayerId: change.PlayerID, ChangeHp: delta, OriHp: change.OldHP, CurrHp: change.NewHP,
				RealChangeHp: delta, MaxHp: change.MaxHP,
			}}})
		}
		if change.NewGold != change.OldGold {
			effects = append(effects, &protocolpb.HeroAttrEffect{PlayerId: change.PlayerID, Data: &protocolpb.HeroAttrEffect_Gold{Gold: &protocolpb.HeroGoldChangeS2C{
				PlayerId: change.PlayerID, ChangeGold: change.NewGold - change.OldGold, OriGold: change.OldGold, CurrGold: change.NewGold,
			}}})
		}
		if change.CardsChanged {
			cards := make([]*modelpb.CardInfo, 0, len(change.Cards))
			for _, card := range change.Cards {
				cards = append(cards, &modelpb.CardInfo{UniqueId: card.UniqueID, CardId: card.CardID, PurifyNum: card.PurifyNum, IsTemp: card.IsTemp, BattleCost: card.BattleCost})
			}
			effects = append(effects, &protocolpb.HeroAttrEffect{PlayerId: change.PlayerID, Data: &protocolpb.HeroAttrEffect_Card{Card: &protocolpb.HeroCardChangeS2C{PlayerId: change.PlayerID, Cards: cards}}})
		}
	}
	if len(effects) > 0 {
		pushes = append(pushes, s.pushFor("UpdateHeroAttrS2C", &protocolpb.UpdateHeroAttrS2C{
			PlayerId:    playerID,
			Cause:       &protocolpb.CauseOrigin{S: protocolpb.CauseOrigin_divination, Id: int64(result.EventID)},
			EffectDatas: effects,
		}, ids, 0))
	}
	return message, pushes
}

func (s *Server) botDivination(ctx context.Context, room store.Room, player store.Player) (int64, int32, []Push, error) {
	choices, exists, err := s.store.DivinationChoices(ctx, room.ID, player.ID)
	if err != nil || !exists {
		if err == nil {
			err = store.ErrActionNotReady
		}
		return 0, 0, nil, err
	}
	request := &protocolpb.TriggerDivinationC2S{Id: choices[0]}
	result, targetType, targets, created, err := s.resolveTriggerDivination(ctx, room.ID, player.ID, 5069, s.nextActionSN(), request)
	if err != nil {
		return 0, 0, nil, err
	}
	if !created {
		return 0, 0, nil, store.ErrActionNotReady
	}
	_, pushes := s.divinationMessages(ctx, room.ID, player.ID, result, targetType, targets)
	return result.NextPlayer, result.Round, pushes, nil
}
