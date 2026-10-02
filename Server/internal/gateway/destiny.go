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

var errDestinyConfigMissing = errors.New("Destiny resource or supported outcome is missing")

type destinyInfo struct {
	ID       int32   `json:"id"`
	Params   []int32 `json:"params"`
	BuffIDs  []int32 `json:"buffIds"`
	SummonID int32   `json:"summonId"`
}

type destinySummonInfo struct {
	ID         int32 `json:"id"`
	SummonType struct {
		Value int32 `json:"value"`
	} `json:"summonType"`
	BuffIDs []int32 `json:"buffIds"`
}

type destinySummonBuffInfo struct {
	ID             int32 `json:"id"`
	IsTriggerClear bool  `json:"isTriggerClear"`
}

func roomPlayerNodeID(room store.Room, playerID int64) int32 {
	for _, player := range room.Players {
		if player.ID == playerID {
			return player.NodeID
		}
	}
	return -1
}

func (s *Server) handleTriggerDestiny(ctx context.Context, sess *Session, in wire.Frame, q *protocolpb.TriggerDestinyC2S) (DispatchResult, error) {
	_, playerID, ok := sessionPlayer(sess)
	if !ok {
		return DispatchResult{Err: ErrAuth}, nil
	}
	roomID, err := s.store.RoomForPlayer(ctx, playerID)
	if err != nil {
		return DispatchResult{Err: ErrRoomNotAction}, nil
	}
	result, created, err := s.resolveTriggerDestiny(ctx, roomID, playerID, in.CmdID, in.UPSN, q)
	if errors.Is(err, store.ErrTurnPlayerMismatch) {
		return DispatchResult{Err: ErrRoomActionPlayer}, nil
	}
	if errors.Is(err, store.ErrActionNotReady) {
		return DispatchResult{Err: ErrRoomActionIncorrect}, nil
	}
	if errors.Is(err, errDestinyConfigMissing) {
		return DispatchResult{Err: ErrNotOpen}, nil
	}
	if err != nil {
		return DispatchResult{}, err
	}
	if !created {
		return DispatchResult{Err: ErrRoomActionAlreadyDone}, nil
	}
	message, pushes := s.destinyMessages(ctx, roomID, playerID, result)
	if result.DiscardRequired {
		pushes = append(pushes, s.actionPush(result.Round, 5075, playerID, &protocolpb.AbandonCardC2S{}))
	} else {
		pushes = append(pushes, s.turnPushes(ctx, roomID, result.Round, result.NextPlayer)...)
	}
	s.log.Info("land Destiny resolved", "room_id", roomID, "player_id", playerID, "destiny_id", result.EventID, "discard_required", result.DiscardRequired)
	return DispatchResult{Message: message, Pushes: pushes}, nil
}

func (s *Server) resolveTriggerDestiny(ctx context.Context, roomID, playerID int64, cmd uint16, upsn int64, request proto.Message) (store.GameActionResult, bool, error) {
	room, err := s.store.RoomSnapshot(ctx, roomID)
	if err != nil {
		return store.GameActionResult{}, false, err
	}
	var player store.Player
	for _, member := range room.Players {
		if member.ID == playerID {
			player = member
			break
		}
	}
	if player.ID == 0 {
		return store.GameActionResult{}, false, store.ErrActionNotReady
	}
	current, _, pending, err := s.store.CurrentTurn(ctx, roomID)
	if err != nil {
		return store.GameActionResult{}, false, err
	}
	if current != playerID {
		return store.GameActionResult{}, false, store.ErrTurnPlayerMismatch
	}
	phase, err := s.store.CurrentTurnPhase(ctx, roomID)
	if err != nil {
		return store.GameActionResult{}, false, err
	}
	if pending != 0 || phase != store.TurnPhaseDestiny {
		return store.GameActionResult{}, false, store.ErrActionNotReady
	}
	if landType, exists := s.domain.LandTypeAt(room, player.NodeID); !exists || landType != 16 {
		return store.GameActionResult{}, false, store.ErrActionNotReady
	}
	ids := s.destinyCandidates(room)
	if len(ids) == 0 {
		return store.GameActionResult{}, false, errDestinyConfigMissing
	}
	choice, err := rand.Int(rand.Reader, big.NewInt(int64(len(ids))))
	if err != nil {
		return store.GameActionResult{}, false, err
	}
	destinyID := ids[choice.Int64()]
	delta, err := s.destinyDelta(room, player, destinyID)
	if err != nil {
		return store.GameActionResult{}, false, err
	}
	if !delta.SetHP {
		delta.HPChange = s.applyHeroHPChangePassive(room, playerID, delta.HPChange)
	}
	raw, err := proto.Marshal(request)
	if err != nil {
		return store.GameActionResult{}, false, err
	}
	return s.store.CompleteGameAction(ctx, roomID, playerID, cmd, upsn, raw, destinyID, store.TurnPhaseDestiny, []store.EventAttrDelta{delta}, nil)
}

func (s *Server) destinyCandidates(room store.Room) []int32 {
	periods := s.resources.IDs("Destiny_periods")
	ids := make([]int32, 0, len(periods))
	for _, id := range periods {
		if id <= 0 || id > int64(1<<31-1) || !destinyOutcomeSupported(int32(id)) {
			continue
		}
		info, ok := s.destinyInfo(int32(id))
		if !ok || !s.destinyOutcomeConfigured(room, info) {
			continue
		}
		ids = append(ids, int32(id))
	}
	return ids
}

func destinyOutcomeSupported(id int32) bool {
	switch id {
	case 40001, 40002, 40003, 40004, 40005, 40006, 40007, 40008, 40009, 40010, 40011, 40012:
		return true
	default:
		return false
	}
}

func (s *Server) destinyInfo(id int32) (destinyInfo, bool) {
	raw, ok := s.resources.Get("Destiny_infos", int64(id))
	if !ok {
		return destinyInfo{}, false
	}
	var info destinyInfo
	if json.Unmarshal(raw, &info) != nil || info.ID != id {
		return destinyInfo{}, false
	}
	return info, true
}

func (s *Server) destinyOutcomeConfigured(room store.Room, info destinyInfo) bool {
	switch info.ID {
	case 40001, 40002, 40005, 40006, 40009, 40010, 40011:
		return len(info.Params) > 0
	case 40007, 40008:
		if len(info.Params) == 0 || info.Params[0] < 0 {
			return false
		}
		handLimit, ok := s.domain.CardInHandLimit(room.Mode)
		if !ok {
			handLimit, ok = s.resources.GlobalInt("GAME_CARDINHAND_LIMIT")
		}
		if !ok || handLimit <= 0 {
			return false
		}
		if room.Mode == 10 && (room.MapID == 1001 || room.MapID == 1002) {
			return true
		}
		poolID, _, ok := s.domain.CardPoolsForMapMode(int64(room.MapID), room.Mode)
		if !ok {
			return false
		}
		pool, ok := s.domain.EffectCardIDs(poolID)
		return ok && len(pool) > 0
	case 40003, 40004:
		if len(info.Params) == 0 || len(info.BuffIDs) == 0 {
			return false
		}
		for _, buffID := range info.BuffIDs {
			if _, _, ok := s.domain.BuffTiming(buffID); !ok {
				return false
			}
		}
		return true
	case 40012:
		if len(info.Params) != 1 || info.Params[0] >= 0 || info.SummonID <= 0 {
			return false
		}
		summonRaw, ok := s.resources.Get("Summon_infos", int64(info.SummonID))
		if !ok {
			return false
		}
		var summon destinySummonInfo
		if json.Unmarshal(summonRaw, &summon) != nil || summon.ID != info.SummonID || summon.SummonType.Value != 3 || len(summon.BuffIDs) == 0 {
			return false
		}
		for _, buffID := range summon.BuffIDs {
			buffRaw, exists := s.resources.Get("Buff_infos", int64(buffID))
			if !exists {
				return false
			}
			var buff destinySummonBuffInfo
			if json.Unmarshal(buffRaw, &buff) != nil || buff.ID != buffID || !buff.IsTriggerClear {
				return false
			}
		}
		return true
	default:
		return false
	}
}

func (s *Server) destinyDelta(room store.Room, player store.Player, id int32) (store.EventAttrDelta, error) {
	info, ok := s.destinyInfo(id)
	if !ok || !s.destinyOutcomeConfigured(room, info) {
		return store.EventAttrDelta{}, errDestinyConfigMissing
	}
	delta := store.EventAttrDelta{PlayerID: player.ID}
	switch id {
	case 40001, 40002:
		maxHP := s.domain.HeroMaxHP(player.HeroID)
		if maxHP <= 0 {
			return store.EventAttrDelta{}, errDestinyConfigMissing
		}
		delta.HPChange, delta.MaxHP = info.Params[0], maxHP
	case 40003, 40004:
		for _, buffID := range info.BuffIDs {
			keepRound, delayRound, exists := s.domain.BuffTiming(buffID)
			if !exists {
				return store.EventAttrDelta{}, errDestinyConfigMissing
			}
			uniqueID, err := newBuffUniqueID()
			if err != nil {
				return store.EventAttrDelta{}, err
			}
			delta.Buffs = append(delta.Buffs, store.BuffState{
				UniqueID: uniqueID, BuffID: buffID, KeepRound: keepRound, DelayRound: delayRound,
				DamageAdjustment: info.Params[0],
				TargetIDs:        []int64{player.ID}, Source: &store.BuffSourceState{S: int32(modelpb.BuffSource_destiny), ID: id},
			})
		}
	case 40005:
		// The localized card states the multiplier as a doubling. The resource
		// parameter stores 200 percent, so convert the amount to an additive delta.
		if player.Gold > int32(1<<31-1)/2 {
			delta.GoldChange = int32(1<<31-1) - player.Gold
		} else {
			delta.GoldChange = player.Gold
		}
	case 40006:
		percent := info.Params[0]
		if percent < 0 || percent > 100 {
			return store.EventAttrDelta{}, errDestinyConfigMissing
		}
		delta.GoldChange = -int32(int64(player.Gold) * int64(percent) / 100)
	case 40007, 40008:
		cards, handLimit, err := s.drawDestinyCards(room, player, info.Params[0])
		if err != nil {
			return store.EventAttrDelta{}, err
		}
		delta.Cards = cards
		delta.DiscardLimit = handLimit
		if len(cards) > 0 && handLimit <= 0 {
			return store.EventAttrDelta{}, fmt.Errorf("invalid hand limit for Destiny %d", id)
		}
	case 40009, 40010:
		count := -info.Params[0]
		if count < 0 {
			return store.EventAttrDelta{}, errDestinyConfigMissing
		}
		remaining, err := discardRandomCards(player.Cards, int(count))
		if err != nil {
			return store.EventAttrDelta{}, err
		}
		if len(remaining) != len(player.Cards) {
			delta.Cards, delta.ReplaceCards = remaining, true
		}
	case 40011:
		delta.GoldChange = info.Params[0]
	case 40012:
		summonRaw, ok := s.resources.Get("Summon_infos", int64(info.SummonID))
		if !ok {
			return store.EventAttrDelta{}, errDestinyConfigMissing
		}
		var summon destinySummonInfo
		if json.Unmarshal(summonRaw, &summon) != nil || summon.ID != info.SummonID || summon.SummonType.Value != 3 || len(summon.BuffIDs) == 0 {
			return store.EventAttrDelta{}, errDestinyConfigMissing
		}
		uniqueID, err := newBuffUniqueID()
		if err != nil {
			return store.EventAttrDelta{}, err
		}
		value := -info.Params[0]
		if value <= 0 {
			return store.EventAttrDelta{}, errDestinyConfigMissing
		}
		delta.LandBuffs = append(delta.LandBuffs, store.LandBuffState{
			NodeID: player.NodeID, Value: value,
			Buff: store.BuffState{
				UniqueID: uniqueID, BuffID: summon.BuffIDs[0], NodeID: player.NodeID,
				Source: &store.BuffSourceState{S: int32(modelpb.BuffSource_summon), ID: info.SummonID},
			},
		})
	default:
		return store.EventAttrDelta{}, errDestinyConfigMissing
	}
	return delta, nil
}

func (s *Server) drawDestinyCards(room store.Room, player store.Player, requested int32) ([]store.CardState, int32, error) {
	if requested < 0 || requested > 128 {
		return nil, 0, errDestinyConfigMissing
	}
	handLimit, ok := s.domain.CardInHandLimit(room.Mode)
	if !ok {
		handLimit, ok = s.resources.GlobalInt("GAME_CARDINHAND_LIMIT")
	}
	if !ok || handLimit <= 0 {
		return nil, 0, errDestinyConfigMissing
	}
	count := int(requested)
	if count <= 0 {
		return nil, handLimit, nil
	}
	var cards []store.CardState
	tutorialNovice := room.Mode == 10 && (room.MapID == 1001 || room.MapID == 1002)
	if !tutorialNovice {
		poolID, _, found := s.domain.CardPoolsForMapMode(int64(room.MapID), room.Mode)
		if !found {
			return nil, 0, errDestinyConfigMissing
		}
		pool, found := s.domain.EffectCardIDs(poolID)
		if !found || len(pool) == 0 {
			return nil, 0, errDestinyConfigMissing
		}
		for i := 0; i < count; i++ {
			cardID, err := randomCardID(pool)
			if err != nil {
				return nil, 0, err
			}
			uniqueID, err := uniqueCardIDForHand(player.Cards, cards)
			if err != nil {
				return nil, 0, err
			}
			cards = append(cards, store.CardState{UniqueID: uniqueID, CardID: cardID, BattleCost: -1})
		}
		return cards, handLimit, nil
	}
	for i := 0; i < count; i++ {
		cardID, err := randomWeightedCardID(noviceTutorialCardPool)
		if err != nil {
			return nil, 0, err
		}
		uniqueID, err := uniqueCardIDForHand(player.Cards, cards)
		if err != nil {
			return nil, 0, err
		}
		cards = append(cards, store.CardState{UniqueID: uniqueID, CardID: cardID, BattleCost: -1})
	}
	return cards, handLimit, nil
}

func uniqueCardIDForHand(existing, newlyDrawn []store.CardState) (int32, error) {
	for attempt := 0; attempt < 8; attempt++ {
		id, err := newCardUniqueID()
		if err != nil {
			return 0, err
		}
		duplicate := false
		for _, card := range existing {
			if card.UniqueID == id {
				duplicate = true
				break
			}
		}
		for _, card := range newlyDrawn {
			if card.UniqueID == id {
				duplicate = true
				break
			}
		}
		if !duplicate {
			return id, nil
		}
	}
	return 0, errors.New("could not create a unique card identity")
}

func discardRandomCards(cards []store.CardState, count int) ([]store.CardState, error) {
	if count <= 0 || len(cards) == 0 {
		return append([]store.CardState(nil), cards...), nil
	}
	if count > len(cards) {
		count = len(cards)
	}
	removed := make(map[int]struct{}, count)
	for len(removed) < count {
		index, err := rand.Int(rand.Reader, big.NewInt(int64(len(cards))))
		if err != nil {
			return nil, err
		}
		removed[int(index.Int64())] = struct{}{}
	}
	remaining := make([]store.CardState, 0, len(cards)-count)
	for index, card := range cards {
		if _, found := removed[index]; !found {
			remaining = append(remaining, card)
		}
	}
	return remaining, nil
}

func (s *Server) destinyMessages(ctx context.Context, roomID, playerID int64, result store.GameActionResult) (*protocolpb.TriggerDestinyS2C, []Push) {
	ids := mustMemberIDs(ctx, s.store, roomID)
	message := &protocolpb.TriggerDestinyS2C{PlayerId: playerID, Id: result.EventID}
	pushes := []Push{s.pushFor("TriggerDestinyS2C", message, ids, playerID)}
	effects := make([]*protocolpb.HeroAttrEffect, 0, len(result.Changes)*2)
	for _, change := range result.Changes {
		if change.NewHP != change.OldHP {
			delta := change.NewHP - change.OldHP
			effects = append(effects, &protocolpb.HeroAttrEffect{
				PlayerId: change.PlayerID,
				Data: &protocolpb.HeroAttrEffect_Hp{Hp: &protocolpb.HeroHpChangeS2C{
					PlayerId: change.PlayerID, ChangeHp: delta, OriHp: change.OldHP,
					CurrHp: change.NewHP, RealChangeHp: delta, MaxHp: change.MaxHP,
				}},
			})
		}
		if change.NewGold != change.OldGold {
			effects = append(effects, &protocolpb.HeroAttrEffect{
				PlayerId: change.PlayerID,
				Data: &protocolpb.HeroAttrEffect_Gold{Gold: &protocolpb.HeroGoldChangeS2C{
					PlayerId: change.PlayerID, ChangeGold: change.NewGold - change.OldGold,
					OriGold: change.OldGold, CurrGold: change.NewGold,
				}},
			})
		}
		if change.CardsChanged {
			cards := make([]*modelpb.CardInfo, 0, len(change.Cards))
			for _, card := range change.Cards {
				cards = append(cards, &modelpb.CardInfo{
					UniqueId: card.UniqueID, CardId: card.CardID, PurifyNum: card.PurifyNum,
					IsTemp: card.IsTemp, BattleCost: card.BattleCost,
				})
			}
			effects = append(effects, &protocolpb.HeroAttrEffect{
				PlayerId: change.PlayerID,
				Data:     &protocolpb.HeroAttrEffect_Card{Card: &protocolpb.HeroCardChangeS2C{PlayerId: change.PlayerID, Cards: cards}},
			})
		}
		for _, buff := range change.NewBuffs {
			effects = append(effects, &protocolpb.HeroAttrEffect{
				PlayerId: change.PlayerID,
				Data: &protocolpb.HeroAttrEffect_Buff{Buff: &protocolpb.HeroBuffChangeS2C{
					PlayerId: change.PlayerID, Buff: buffMessage(buff), Op: protocolpb.HeroBuffChangeS2C_Insert,
				}},
			})
		}
		for _, buff := range change.RemovedBuffs {
			effects = append(effects, &protocolpb.HeroAttrEffect{
				PlayerId: change.PlayerID,
				Data: &protocolpb.HeroAttrEffect_Buff{Buff: &protocolpb.HeroBuffChangeS2C{
					PlayerId: change.PlayerID, Buff: buffMessage(buff), Op: protocolpb.HeroBuffChangeS2C_Delete,
				}},
			})
		}
	}
	if len(effects) > 0 {
		update := &protocolpb.UpdateHeroAttrS2C{
			PlayerId:    playerID,
			Cause:       &protocolpb.CauseOrigin{S: protocolpb.CauseOrigin_destiny, Id: int64(result.EventID)},
			EffectDatas: effects,
		}
		pushes = append(pushes, s.pushFor("UpdateHeroAttrS2C", update, ids, 0))
	}
	for _, update := range result.LandBuffChanges {
		pushes = append(pushes, s.landBuffsPush(update, ids))
	}
	return message, pushes
}
