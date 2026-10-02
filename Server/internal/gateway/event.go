package gateway

import (
	"context"
	"crypto/rand"
	"errors"
	"math/big"
	"sort"

	store "astralparty-server/internal/db"
	wire "astralparty-server/internal/protocol"
	modelpb "astralparty-server/internal/protocol/gen/model"
	protocolpb "astralparty-server/internal/protocol/gen/protocol"
	"google.golang.org/protobuf/proto"
)

func (s *Server) handleTriggerEvent(ctx context.Context, sess *Session, in wire.Frame, q *protocolpb.TriggerEventC2S) (DispatchResult, error) {
	_, playerID, ok := sessionPlayer(sess)
	if !ok {
		return DispatchResult{Err: ErrAuth}, nil
	}
	roomID, err := s.store.RoomForPlayer(ctx, playerID)
	if err != nil {
		return DispatchResult{Err: ErrRoomNotAction}, nil
	}
	result, created, err := s.resolveTriggerEvent(ctx, roomID, playerID, in.CmdID, in.UPSN, q)
	if errors.Is(err, store.ErrTurnPlayerMismatch) {
		return DispatchResult{Err: ErrRoomActionPlayer}, nil
	}
	if errors.Is(err, store.ErrActionNotReady) {
		return DispatchResult{Err: ErrRoomActionIncorrect}, nil
	}
	if errors.Is(err, errEventConfigMissing) {
		return DispatchResult{Err: ErrNotOpen}, nil
	}
	if err != nil {
		return DispatchResult{}, err
	}
	if !created {
		return DispatchResult{Err: ErrRoomActionAlreadyDone}, nil
	}
	message, pushes := s.eventMessages(ctx, roomID, playerID, result)
	pushes = append(pushes, s.turnPushes(ctx, roomID, result.Round, result.NextPlayer)...)
	return DispatchResult{Message: message, Pushes: pushes}, nil
}

type resolvedEvent struct {
	store.GameEventResult
}

var errEventConfigMissing = errors.New("event resource or implemented event rule is missing")

func (s *Server) resolveTriggerEvent(ctx context.Context, roomID, playerID int64, cmd uint16, upsn int64, request proto.Message) (resolvedEvent, bool, error) {
	room, err := s.store.RoomSnapshot(ctx, roomID)
	if err != nil {
		return resolvedEvent{}, false, err
	}
	var player store.Player
	for _, member := range room.Players {
		if member.ID == playerID {
			player = member
			break
		}
	}
	if player.ID == 0 {
		return resolvedEvent{}, false, store.ErrActionNotReady
	}
	phase, err := s.store.CurrentTurnPhase(ctx, roomID)
	if err != nil {
		return resolvedEvent{}, false, err
	}
	current, _, pending, err := s.store.CurrentTurn(ctx, roomID)
	if err != nil {
		return resolvedEvent{}, false, err
	}
	if current != playerID {
		return resolvedEvent{}, false, store.ErrTurnPlayerMismatch
	}
	if pending != 0 || phase != store.TurnPhaseEvent {
		return resolvedEvent{}, false, store.ErrActionNotReady
	}
	landType, exists := s.domain.LandTypeAt(room, player.NodeID)
	if !exists || landType != 8 {
		return resolvedEvent{}, false, store.ErrActionNotReady
	}
	implemented, candidateErr := s.supportedEventIDs(room)
	if candidateErr != nil {
		return resolvedEvent{}, false, candidateErr
	}
	if len(implemented) == 0 {
		return resolvedEvent{}, false, errEventConfigMissing
	}
	n, err := rand.Int(rand.Reader, big.NewInt(int64(len(implemented))))
	if err != nil {
		return resolvedEvent{}, false, err
	}
	eventID := implemented[n.Int64()]
	params, paramsOK := s.domain.EventParams(eventID)
	buffIDs, buffsOK := s.domain.EventBuffIDs(eventID)
	if !paramsOK || (len(params) == 0 && (!buffsOK || len(buffIDs) == 0) && eventID != 30001 && eventID != 30005 && eventID != 30013 && eventID != 30021) {
		return resolvedEvent{}, false, errEventConfigMissing
	}
	deltas, err := s.eventDeltas(eventID, params, room, playerID)
	if err != nil {
		return resolvedEvent{}, false, err
	}
	deltas, err = s.applyEventPassives(room, player, deltas)
	if err != nil {
		return resolvedEvent{}, false, err
	}
	var progressDelta *int32
	switch eventID {
	case 30201, 30205:
		if len(params) == 0 {
			return resolvedEvent{}, false, errEventConfigMissing
		}
		change := params[0]
		progressDelta = &change
	}
	raw, err := proto.Marshal(request)
	if err != nil {
		return resolvedEvent{}, false, err
	}
	result, created, err := s.store.CompleteGameEvent(ctx, roomID, playerID, cmd, upsn, raw, eventID, deltas, progressDelta)
	if err != nil || !created {
		return resolvedEvent{}, created, err
	}
	s.log.Info("land event resolved", "room_id", roomID, "player_id", playerID, "event_id", eventID, "targets", len(result.Changes), "game_progress", result.GameProgress, "game_max_progress", result.GameMaxProgress, "progress_changed", result.ProgressChanged)
	return resolvedEvent{GameEventResult: result}, true, nil
}

func (s *Server) supportedEventIDs(room store.Room) ([]int32, error) {
	poolID, ok := s.domain.EventPoolForMapMode(int64(room.MapID), room.Mode)
	if !ok {
		return nil, errEventConfigMissing
	}
	candidates, ok := s.domain.EventIDsForPool(poolID)
	if !ok {
		return nil, errEventConfigMissing
	}
	implemented := make([]int32, 0, len(candidates))
	for _, id := range candidates {
		if id == 30201 || id == 30205 {
			if _, configured := s.domain.MapProgressLimit(int64(room.MapID), room.Difficulty); !configured {
				continue
			}
		}
		if s.domain.EventEffectSupported(id) && s.domain.EventAvailableInRoom(id, room) && s.domain.EventInfoExists(id) {
			implemented = append(implemented, id)
		}
	}
	if len(implemented) == 0 {
		return nil, errEventConfigMissing
	}
	return implemented, nil
}

func (s *Server) applyEventPassives(room store.Room, player store.Player, deltas []store.EventAttrDelta) ([]store.EventAttrDelta, error) {
	if s.resources.HeroHasPassiveSkill(player.HeroID, 10211, usesPVEPassiveSkills(room.Mode)) {
		deltas = addEventGoldReward(deltas, player.ID, 3)
	}
	eventCardID := int32(0)
	if s.resources.HeroHasPassiveSkill(player.HeroID, 12111, usesPVEPassiveSkills(room.Mode)) {
		eventCardID = 20030
	} else if s.resources.HeroHasPassiveSkill(player.HeroID, 12112, usesPVEPassiveSkills(room.Mode)) {
		eventCardID = 20031
	}
	if eventCardID > 0 {
		uniqueID, err := newCardUniqueID()
		if err != nil {
			return nil, err
		}
		deltas = addEventCardReward(deltas, player.ID, store.CardState{UniqueID: uniqueID, CardID: eventCardID, BattleCost: -1})
	}
	return s.applyHeroHPChangePassives(room, deltas), nil
}

func addEventGoldReward(deltas []store.EventAttrDelta, playerID int64, amount int32) []store.EventAttrDelta {
	for index := range deltas {
		if deltas[index].PlayerID == playerID {
			deltas[index].GoldChange += amount
			return deltas
		}
	}
	return append(deltas, store.EventAttrDelta{PlayerID: playerID, GoldChange: amount})
}

func addEventCardReward(deltas []store.EventAttrDelta, playerID int64, card store.CardState) []store.EventAttrDelta {
	for index := range deltas {
		if deltas[index].PlayerID == playerID {
			deltas[index].Cards = append(deltas[index].Cards, card)
			return deltas
		}
	}
	return append(deltas, store.EventAttrDelta{PlayerID: playerID, Cards: []store.CardState{card}})
}

func (s *Server) eventDeltas(eventID int32, params []int32, room store.Room, ownerID int64) ([]store.EventAttrDelta, error) {
	players := room.Players
	if len(players) == 0 {
		return nil, errEventConfigMissing
	}
	deltas := make([]store.EventAttrDelta, 0, len(players))
	maxHP := func(player store.Player) (int32, error) {
		value := s.domain.HeroMaxHP(player.HeroID)
		if value <= 0 {
			return 0, errEventConfigMissing
		}
		return value, nil
	}
	switch eventID {
	case 30201:
		if len(params) < 2 || params[0] <= 0 || params[1] <= 0 || params[1] > 10 {
			return nil, errEventConfigMissing
		}
		_, poolID, ok := s.domain.CardPoolsForMapMode(int64(room.MapID), room.Mode)
		if !ok {
			return nil, errEventConfigMissing
		}
		cardIDs, ok := s.domain.BattleCardIDs(poolID)
		if !ok || len(cardIDs) == 0 {
			return nil, errEventConfigMissing
		}
		for _, player := range players {
			delta := store.EventAttrDelta{PlayerID: player.ID}
			for i := int32(0); i < params[1]; i++ {
				cardID := cardIDs[0]
				if len(cardIDs) > 1 {
					n, err := rand.Int(rand.Reader, big.NewInt(int64(len(cardIDs))))
					if err != nil {
						return nil, err
					}
					cardID = cardIDs[n.Int64()]
				}
				uniqueID, err := newCardUniqueID()
				if err != nil {
					return nil, err
				}
				delta.Cards = append(delta.Cards, store.CardState{UniqueID: uniqueID, CardID: cardID, BattleCost: -1})
			}
			deltas = append(deltas, delta)
		}
		return deltas, nil
	case 30205:
		if len(params) < 1 || params[0] >= 0 {
			return nil, errEventConfigMissing
		}
		return nil, nil
	case 30001:
		if len(players) == 0 {
			return nil, errEventConfigMissing
		}
		richest, poorest := players[0], players[0]
		for _, player := range players[1:] {
			if player.Gold > richest.Gold || player.Gold == richest.Gold && player.Slot < richest.Slot {
				richest = player
			}
			if player.Gold < poorest.Gold || player.Gold == poorest.Gold && player.Slot > poorest.Slot {
				poorest = player
			}
		}
		if richest.ID == poorest.ID {
			return []store.EventAttrDelta{{PlayerID: richest.ID}}, nil
		}
		total := int64(richest.Gold) + int64(poorest.Gold)
		richShare, poorShare := total/2, total/2
		if total%2 != 0 {
			// Preserve the last indivisible coin and use the earlier room slot as
			// the deterministic tie-breaker.
			if richest.Slot < poorest.Slot {
				richShare++
			} else {
				poorShare++
			}
		}
		return []store.EventAttrDelta{
			{PlayerID: richest.ID, GoldChange: int32(richShare) - richest.Gold},
			{PlayerID: poorest.ID, GoldChange: int32(poorShare) - poorest.Gold},
		}, nil
	case 30015:
		hospitalID, ok := s.domain.HospitalNodeID(room)
		if !ok {
			return nil, errEventConfigMissing
		}
		hospitalFront, err := s.domain.ActiveNeighborLandIDs(room, hospitalID)
		if err != nil {
			return nil, err
		}
		for _, player := range players {
			deltas = append(deltas, store.EventAttrDelta{
				PlayerID: player.ID, PlaceChanged: true, NodeID: hospitalID,
				BackNodeID: 0, FrontNodeIDs: hospitalFront,
			})
		}
		return deltas, nil
	case 30013:
		destinations, ok, err := s.domain.RandomConnectedLandSet(room, 4)
		if err != nil {
			return nil, err
		}
		if !ok || len(destinations) < len(players) {
			return nil, errEventConfigMissing
		}
		orderedPlayers := append([]store.Player(nil), players...)
		sort.Slice(orderedPlayers, func(i, j int) bool { return orderedPlayers[i].Slot < orderedPlayers[j].Slot })
		for index, player := range orderedPlayers {
			front, frontErr := s.domain.ActiveNeighborLandIDs(room, destinations[index])
			if frontErr != nil {
				return nil, frontErr
			}
			deltas = append(deltas, store.EventAttrDelta{
				PlayerID: player.ID, PlaceChanged: true, NodeID: destinations[index],
				BackNodeID: 0, FrontNodeIDs: front,
			})
		}
		return deltas, nil
	case 30005:
		positions := make([]int32, len(players))
		for i, player := range players {
			if player.NodeID < 0 {
				return nil, errEventConfigMissing
			}
			positions[i] = player.NodeID
		}
		for i := len(positions) - 1; i > 0; i-- {
			j, err := rand.Int(rand.Reader, big.NewInt(int64(i+1)))
			if err != nil {
				return nil, err
			}
			positions[i], positions[j.Int64()] = positions[j.Int64()], positions[i]
		}
		for i, player := range players {
			front, frontErr := s.domain.ActiveNeighborLandIDs(room, positions[i])
			if frontErr != nil {
				return nil, frontErr
			}
			deltas = append(deltas, store.EventAttrDelta{
				PlayerID: player.ID, PlaceChanged: true, NodeID: positions[i],
				// A shuffle is a teleport: old path history cannot be reused at
				// the new tile, so the server clears the prior movement pointers.
				BackNodeID: 0, FrontNodeIDs: front,
			})
		}
		return deltas, nil
	case 30020:
		if len(params) < 2 || params[0] <= 0 || params[1] >= 0 {
			return nil, errEventConfigMissing
		}
		buffIDs, ok := s.domain.EventBuffIDs(eventID)
		if !ok || len(buffIDs) == 0 {
			return nil, errEventConfigMissing
		}
		for _, player := range players {
			cap, err := maxHP(player)
			if err != nil {
				return nil, err
			}
			delta := store.EventAttrDelta{PlayerID: player.ID, SetHP: true, HPValue: params[0], MaxHP: cap}
			for _, buffID := range buffIDs {
				keepRound, delayRound, exists := s.domain.BuffTiming(buffID)
				if !exists {
					return nil, errEventConfigMissing
				}
				uniqueID, err := newBuffUniqueID()
				if err != nil {
					return nil, err
				}
				delta.Buffs = append(delta.Buffs, store.BuffState{
					UniqueID: uniqueID, BuffID: buffID, KeepRound: keepRound, DelayRound: delayRound,
					Source: &store.BuffSourceState{S: 3, ID: eventID},
				})
			}
			deltas = append(deltas, delta)
		}
		return deltas, nil
	case 30012:
		if len(params) < 2 || params[0] <= 0 || params[1] <= 0 || params[1] > 12 {
			return nil, errEventConfigMissing
		}
		buffIDs, ok := s.domain.EventBuffIDs(eventID)
		if !ok || len(buffIDs) == 0 {
			return nil, errEventConfigMissing
		}
		for _, player := range players {
			delta := store.EventAttrDelta{PlayerID: player.ID}
			for _, buffID := range buffIDs {
				keepRound, delayRound, exists := s.domain.BuffTiming(buffID)
				if !exists || keepRound != params[0] {
					return nil, errEventConfigMissing
				}
				uniqueID, err := newBuffUniqueID()
				if err != nil {
					return nil, err
				}
				delta.Buffs = append(delta.Buffs, store.BuffState{
					UniqueID: uniqueID, BuffID: buffID, KeepRound: keepRound, DelayRound: delayRound,
					Source: &store.BuffSourceState{S: 3, ID: eventID},
				})
			}
			deltas = append(deltas, delta)
		}
		return deltas, nil
	case 30021:
		if len(players) == 0 {
			return nil, errEventConfigMissing
		}
		var owner store.Player
		ownerFound := false
		for _, player := range players {
			if player.ID == ownerID {
				owner = player
				ownerFound = true
				break
			}
		}
		if !ownerFound {
			return nil, errEventConfigMissing
		}
		previous := owner
		previousFound := false
		for _, player := range players {
			if player.ID != owner.ID && player.Slot < owner.Slot && (!previousFound || player.Slot > previous.Slot) {
				previous = player
				previousFound = true
			}
		}
		if !previousFound {
			for _, player := range players {
				if player.ID != owner.ID && (!previousFound || player.Slot > previous.Slot) {
					previous = player
					previousFound = true
				}
			}
		}
		if previous.ID == owner.ID {
			return []store.EventAttrDelta{{PlayerID: owner.ID, Cards: owner.Cards, ReplaceCards: true}}, nil
		}
		transferred := make([]store.CardState, 0, len(owner.Cards)+len(previous.Cards))
		transferred = append(transferred, owner.Cards...)
		transferred = append(transferred, previous.Cards...)
		return []store.EventAttrDelta{
			{PlayerID: owner.ID, Cards: transferred, ReplaceCards: true},
			{PlayerID: previous.ID, Cards: []store.CardState{}, ReplaceCards: true},
		}, nil
	case 30003:
		if len(params) < 1 || params[0] > 0 {
			return nil, errEventConfigMissing
		}
		richest := players[0]
		for _, player := range players[1:] {
			if player.Gold > richest.Gold || player.Gold == richest.Gold && player.Slot < richest.Slot {
				richest = player
			}
		}
		return []store.EventAttrDelta{{PlayerID: richest.ID, GoldChange: params[0]}}, nil
	case 30006:
		if len(params) < 1 || params[0] >= 0 {
			return nil, errEventConfigMissing
		}
		buffIDs, ok := s.domain.EventBuffIDs(eventID)
		if !ok || len(buffIDs) == 0 {
			return nil, errEventConfigMissing
		}
		for _, player := range players {
			cap, err := maxHP(player)
			if err != nil {
				return nil, err
			}
			delta := store.EventAttrDelta{PlayerID: player.ID, HPChange: params[0], MaxHP: cap}
			for _, buffID := range buffIDs {
				keepRound, delayRound, exists := s.domain.BuffTiming(buffID)
				if !exists {
					return nil, errEventConfigMissing
				}
				uniqueID, err := newBuffUniqueID()
				if err != nil {
					return nil, err
				}
				delta.Buffs = append(delta.Buffs, store.BuffState{
					UniqueID: uniqueID, BuffID: buffID, KeepRound: keepRound, DelayRound: delayRound,
					Source: &store.BuffSourceState{S: 3, ID: eventID},
				})
			}
			deltas = append(deltas, delta)
		}
		return deltas, nil
	case 30014, 30019:
		var cardIDs []int32
		goldChange := int32(0)
		cardCount := 1
		if eventID == 30014 {
			if len(params) < 2 || params[0] < 0 || params[1] < 0 || params[1] > 10 {
				return nil, errEventConfigMissing
			}
			goldChange, cardCount = params[0], int(params[1])
			cardIDs, _ = s.domain.BattleCardIDs(1001)
			if cardCount > 0 && len(cardIDs) == 0 {
				return nil, errEventConfigMissing
			}
		} else {
			if len(params) < 1 || params[0] <= 0 {
				return nil, errEventConfigMissing
			}
			cardIDs = []int32{params[0]}
		}
		for _, player := range players {
			delta := store.EventAttrDelta{PlayerID: player.ID, GoldChange: goldChange}
			for i := 0; i < cardCount; i++ {
				cardID := cardIDs[0]
				if len(cardIDs) > 1 {
					n, err := rand.Int(rand.Reader, big.NewInt(int64(len(cardIDs))))
					if err != nil {
						return nil, err
					}
					cardID = cardIDs[n.Int64()]
				}
				uniqueID, err := newCardUniqueID()
				if err != nil {
					return nil, err
				}
				delta.Cards = append(delta.Cards, store.CardState{UniqueID: uniqueID, CardID: cardID, BattleCost: -1})
			}
			deltas = append(deltas, delta)
		}
		return deltas, nil
	case 30010:
		if len(params) < 1 || params[0] >= 0 {
			return nil, errEventConfigMissing
		}
		removeCount := int(-params[0])
		for _, player := range players {
			remaining := append([]store.CardState(nil), player.Cards...)
			discarded := make([]store.CardState, 0, removeCount)
			for i := 0; i < removeCount && len(remaining) > 0; i++ {
				idx, err := rand.Int(rand.Reader, big.NewInt(int64(len(remaining))))
				if err != nil {
					return nil, err
				}
				index := int(idx.Int64())
				discarded = append(discarded, remaining[index])
				remaining = append(remaining[:index], remaining[index+1:]...)
			}
			goldChange := int32(0)
			if skillParams := s.resources.HeroPassiveSkillParams(player.HeroID, 10811, usesPVEPassiveSkills(room.Mode)); len(skillParams) > 0 && skillParams[0] > 0 {
				for _, card := range discarded {
					if _, isBattleCard := s.resources.BattleCardInfo(card.CardID); isBattleCard {
						goldChange += skillParams[0]
					}
				}
			}
			deltas = append(deltas, store.EventAttrDelta{PlayerID: player.ID, GoldChange: goldChange, Cards: remaining, ReplaceCards: true})
		}
		return deltas, nil
	case 30002, 30202:
		buffIDs, ok := s.domain.EventBuffIDs(eventID)
		if !ok || len(buffIDs) == 0 {
			return nil, errEventConfigMissing
		}
		source := int32(3) // event
		if eventID == 30202 {
			source = 2 // client tutorial creates this buff from a card source
		}
		for _, player := range players {
			delta := store.EventAttrDelta{PlayerID: player.ID}
			for _, buffID := range buffIDs {
				keepRound, delayRound, exists := s.domain.BuffTiming(buffID)
				if !exists {
					return nil, errEventConfigMissing
				}
				uniqueID, err := newBuffUniqueID()
				if err != nil {
					return nil, err
				}
				delta.Buffs = append(delta.Buffs, store.BuffState{
					UniqueID: uniqueID, BuffID: buffID, KeepRound: keepRound, DelayRound: delayRound,
					Source: &store.BuffSourceState{S: source, ID: eventID},
				})
			}
			deltas = append(deltas, delta)
		}
		return deltas, nil
	case 30018:
		buffIDs, ok := s.domain.EventBuffIDs(eventID)
		if !ok || len(buffIDs) == 0 {
			return nil, errEventConfigMissing
		}
		for _, player := range players {
			delta := store.EventAttrDelta{PlayerID: player.ID}
			for _, buffID := range buffIDs {
				keepRound, delayRound, exists := s.domain.BuffTiming(buffID)
				if !exists {
					return nil, errEventConfigMissing
				}
				uniqueID, err := newBuffUniqueID()
				if err != nil {
					return nil, err
				}
				delta.Buffs = append(delta.Buffs, store.BuffState{
					UniqueID: uniqueID, BuffID: buffID, KeepRound: keepRound, DelayRound: delayRound,
					Source: &store.BuffSourceState{S: 3, ID: eventID},
				})
			}
			deltas = append(deltas, delta)
		}
		return deltas, nil
	case 30008, 30009:
		if len(params) < 1 || (eventID == 30008 && params[0] < 0) || (eventID == 30009 && params[0] > 0) {
			return nil, errEventConfigMissing
		}
		for _, player := range players {
			cap, err := maxHP(player)
			if err != nil {
				return nil, err
			}
			deltas = append(deltas, store.EventAttrDelta{PlayerID: player.ID, HPChange: params[0], MaxHP: cap})
		}
		return deltas, nil
	case 30206:
		if len(params) < 2 || params[0] < 0 || params[1] > 0 {
			return nil, errEventConfigMissing
		}
		lowest, highest := players[0], players[0]
		for _, player := range players[1:] {
			if player.HP < lowest.HP || player.HP == lowest.HP && player.Slot < lowest.Slot {
				lowest = player
			}
			if player.HP > highest.HP || player.HP == highest.HP && player.Slot < highest.Slot {
				highest = player
			}
		}
		lowCap, err := maxHP(lowest)
		if err != nil {
			return nil, err
		}
		deltas = append(deltas, store.EventAttrDelta{PlayerID: lowest.ID, HPChange: params[0], MaxHP: lowCap})
		if highest.ID != lowest.ID {
			highCap, err := maxHP(highest)
			if err != nil {
				return nil, err
			}
			deltas = append(deltas, store.EventAttrDelta{PlayerID: highest.ID, HPChange: params[1], MaxHP: highCap})
		}
		return deltas, nil
	default:
		return nil, errEventConfigMissing
	}
}

func (s *Server) eventMessages(ctx context.Context, roomID, playerID int64, result resolvedEvent) (*protocolpb.TriggerEventS2C, []Push) {
	ids := mustMemberIDs(ctx, s.store, roomID)
	targets := make([]int64, 0, len(result.Changes))
	effects := make([]*protocolpb.HeroAttrEffect, 0, len(result.Changes))
	for _, change := range result.Changes {
		targets = append(targets, change.PlayerID)
		if change.NewHP != change.OldHP {
			effects = append(effects, &protocolpb.HeroAttrEffect{PlayerId: change.PlayerID, Data: &protocolpb.HeroAttrEffect_Hp{Hp: &protocolpb.HeroHpChangeS2C{
				PlayerId: change.PlayerID, ChangeHp: change.NewHP - change.OldHP,
				OriHp: change.OldHP, CurrHp: change.NewHP,
				RealChangeHp: change.NewHP - change.OldHP, MaxHp: change.MaxHP,
			}}})
		}
		if change.NewGold != change.OldGold {
			effects = append(effects, &protocolpb.HeroAttrEffect{PlayerId: change.PlayerID, Data: &protocolpb.HeroAttrEffect_Gold{Gold: &protocolpb.HeroGoldChangeS2C{
				PlayerId: change.PlayerID, ChangeGold: change.NewGold - change.OldGold,
				OriGold: change.OldGold, CurrGold: change.NewGold,
			}}})
		}
		if change.PlaceChanged {
			effects = append(effects, &protocolpb.HeroAttrEffect{
				PlayerId: change.PlayerID,
				Data: &protocolpb.HeroAttrEffect_Place{Place: &protocolpb.HeroPlaceChangeS2C{
					PlayerId: change.PlayerID,
					Place: &modelpb.HeroPlace{
						NodeId: change.NodeID, FrontNodeIds: change.FrontNodeIDs, BackNodeId: change.BackNodeID,
					},
				}},
			})
		}
		if change.CardsChanged {
			cards := make([]*modelpb.CardInfo, 0, len(change.Cards))
			for _, card := range change.Cards {
				cards = append(cards, &modelpb.CardInfo{UniqueId: card.UniqueID, CardId: card.CardID, PurifyNum: card.PurifyNum, IsTemp: card.IsTemp, BattleCost: card.BattleCost})
			}
			effects = append(effects, &protocolpb.HeroAttrEffect{PlayerId: change.PlayerID, Data: &protocolpb.HeroAttrEffect_Card{Card: &protocolpb.HeroCardChangeS2C{PlayerId: change.PlayerID, Cards: cards}}})
		}
		for _, buff := range change.NewBuffs {
			effects = append(effects, &protocolpb.HeroAttrEffect{PlayerId: change.PlayerID, Data: &protocolpb.HeroAttrEffect_Buff{Buff: &protocolpb.HeroBuffChangeS2C{PlayerId: change.PlayerID, Buff: buffMessage(buff), Op: protocolpb.HeroBuffChangeS2C_Insert}}})
		}
		for _, buff := range change.RemovedBuffs {
			effects = append(effects, &protocolpb.HeroAttrEffect{PlayerId: change.PlayerID, Data: &protocolpb.HeroAttrEffect_Buff{Buff: &protocolpb.HeroBuffChangeS2C{PlayerId: change.PlayerID, Buff: buffMessage(buff), Op: protocolpb.HeroBuffChangeS2C_Delete}}})
		}
	}
	message := &protocolpb.TriggerEventS2C{PlayerId: playerID, EventId: result.EventID, TargetIds: targets}
	pushes := []Push{s.pushFor("TriggerEventS2C", message, ids, playerID)}
	if len(effects) > 0 {
		update := &protocolpb.UpdateHeroAttrS2C{
			PlayerId:    playerID,
			Cause:       &protocolpb.CauseOrigin{S: protocolpb.CauseOrigin_event, Id: int64(result.EventID)},
			EffectDatas: effects,
		}
		pushes = append(pushes, s.pushFor("UpdateHeroAttrS2C", update, ids, 0))
	}
	if result.ProgressChanged {
		pushes = append(pushes, s.pushFor("GameProgressChangeS2C", &protocolpb.GameProgressChangeS2C{
			Progress: result.GameProgress, MaxProgress: result.GameMaxProgress,
		}, ids, 0))
	}
	return message, pushes
}

func (s *Server) jumpPlacePush(playerID int64, landID int32, place store.MovePlaceResult, recipients []int64) Push {
	update := &protocolpb.UpdateHeroAttrS2C{
		PlayerId: playerID,
		Cause:    &protocolpb.CauseOrigin{S: protocolpb.CauseOrigin_land, Id: int64(landID)},
		EffectDatas: []*protocolpb.HeroAttrEffect{{
			PlayerId: playerID,
			Data: &protocolpb.HeroAttrEffect_Place{Place: &protocolpb.HeroPlaceChangeS2C{
				PlayerId: playerID,
				Place: &modelpb.HeroPlace{
					NodeId: place.NodeID, BackNodeId: place.BackNodeID, FrontNodeIds: place.FrontNodeIDs,
				},
			}},
		}},
	}
	return s.pushFor("UpdateHeroAttrS2C", update, recipients, 0)
}

func newCardUniqueID() (int32, error) {
	n, err := rand.Int(rand.Reader, big.NewInt(1<<31-1))
	if err != nil {
		return 0, err
	}
	return int32(n.Int64() + 1), nil
}

func newBuffUniqueID() (int64, error) {
	n, err := rand.Int(rand.Reader, big.NewInt(1<<62-1))
	if err != nil {
		return 0, err
	}
	return n.Int64() + 1, nil
}
