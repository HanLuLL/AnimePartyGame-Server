package gateway

import (
	"context"
	"crypto/rand"
	"errors"
	"fmt"
	"math/big"

	store "astralparty-server/internal/db"
	wire "astralparty-server/internal/protocol"
	modelpb "astralparty-server/internal/protocol/gen/model"
	protocolpb "astralparty-server/internal/protocol/gen/protocol"
	"google.golang.org/protobuf/proto"
)

func (s *Server) prepareLandShop(room store.Room, player store.Player) (store.ShopState, error) {
	params, ok := s.domain.LandParams(7)
	if !ok || len(params) < 3 || params[0] < 0 || params[2] <= 0 || params[2] > 12 {
		return store.ShopState{}, errors.New("Shop land price or offer count is missing or invalid")
	}
	offerCount := params[2]
	merchant := s.resources.HeroHasPassiveSkill(player.HeroID, 10111, usesPVEPassiveSkills(room.Mode))
	if merchant {
		offerCount++
	}
	for _, buff := range player.Buffs {
		if buff.BuffID != 3001201 || buff.KeepRound <= 0 {
			continue
		}
		eventParams, exists := s.domain.EventParams(30012)
		if !exists || len(eventParams) < 2 || eventParams[1] <= 0 || offerCount > 12-eventParams[1] {
			return store.ShopState{}, errors.New("shop event offer count is missing or invalid")
		}
		offerCount += eventParams[1]
		break
	}
	if offerCount > 12 {
		return store.ShopState{}, errors.New("Shop offer count exceeds the supported limit")
	}
	_, poolID, ok := s.domain.CardPoolsForMapMode(int64(room.MapID), room.Mode)
	if !ok {
		return store.ShopState{}, fmt.Errorf("Shop card pool is unavailable for map %d mode %d", room.MapID, room.Mode)
	}
	pool, ok := s.domain.BattleCardIDs(poolID)
	if !ok || len(pool) < int(offerCount) {
		return store.ShopState{}, fmt.Errorf("Shop card pool %d has fewer cards than the configured offer count", poolID)
	}
	cards, err := randomDistinctCardIDs(pool, int(offerCount))
	if err != nil {
		return store.ShopState{}, err
	}
	freeCardNum, ok := s.resources.GlobalInt("GAME_LAND_SHOP_FREECARDNUMB")
	if !ok || freeCardNum < 0 {
		return store.ShopState{}, errors.New("Shop free-card count is missing or invalid")
	}
	state := store.ShopState{
		PlayerID:    player.ID,
		Cards:       cards,
		Gold:        params[0],
		FreeCard:    params[1],
		FreeCardNum: freeCardNum,
		Alreadys:    make([]bool, len(cards)),
	}
	if merchant {
		state.EntryGold = 1
	}
	return state, nil
}

func usesPVEPassiveSkills(mode int32) bool {
	switch mode {
	case 4, 6, 9, 10, 12:
		return true
	default:
		return false
	}
}

func randomDistinctCardIDs(pool []int32, count int) ([]int32, error) {
	if count <= 0 || count > len(pool) {
		return nil, errors.New("invalid distinct card selection size")
	}
	remaining := append([]int32(nil), pool...)
	out := make([]int32, 0, count)
	for i := 0; i < count; i++ {
		n, err := rand.Int(rand.Reader, big.NewInt(int64(len(remaining))))
		if err != nil {
			return nil, fmt.Errorf("select a shop card: %w", err)
		}
		index := int(n.Int64())
		out = append(out, remaining[index])
		remaining[index] = remaining[len(remaining)-1]
		remaining = remaining[:len(remaining)-1]
	}
	return out, nil
}

func shopActionData(state store.ShopState) *protocolpb.ShopBuyC2S {
	return &protocolpb.ShopBuyC2S{
		Cards:       append([]int32(nil), state.Cards...),
		FreeCard:    state.FreeCard,
		Gold:        state.Gold,
		FreeCardNum: state.FreeCardNum,
		Alreadys:    append([]bool(nil), state.Alreadys...),
	}
}

func shopBuyMessage(playerID int64, result store.ShopBuyResult) *protocolpb.ShopBuyS2C {
	return &protocolpb.ShopBuyS2C{
		PlayerId: playerID,
		BuyCards: append([]int32(nil), result.Bought...),
		Cards:    append([]int32(nil), result.State.Cards...),
		Alreadys: append([]bool(nil), result.State.Alreadys...),
	}
}

func shopBuyAttrUpdate(playerID int64, landID int32, result store.ShopBuyResult) *protocolpb.UpdateHeroAttrS2C {
	cardMessages := make([]*modelpb.CardInfo, 0, len(result.Cards))
	for _, card := range result.Cards {
		cardMessages = append(cardMessages, &modelpb.CardInfo{
			UniqueId: card.UniqueID, CardId: card.CardID, PurifyNum: card.PurifyNum,
			IsTemp: card.IsTemp, BattleCost: card.BattleCost,
		})
	}
	return &protocolpb.UpdateHeroAttrS2C{
		PlayerId: playerID,
		Cause:    &protocolpb.CauseOrigin{S: protocolpb.CauseOrigin_shop_buy, Id: int64(landID)},
		EffectDatas: []*protocolpb.HeroAttrEffect{
			{PlayerId: playerID, Data: &protocolpb.HeroAttrEffect_Gold{Gold: &protocolpb.HeroGoldChangeS2C{
				PlayerId: playerID, ChangeGold: result.NewGold - result.OldGold,
				OriGold: result.OldGold, CurrGold: result.NewGold,
			}}},
			{PlayerId: playerID, Data: &protocolpb.HeroAttrEffect_Card{Card: &protocolpb.HeroCardChangeS2C{
				PlayerId: playerID, Cards: cardMessages,
			}}},
		},
	}
}

func (s *Server) shopEntryGoldPush(playerID int64, landID int32, oldGold, newGold int32, recipients []int64) Push {
	return s.pushFor("UpdateHeroAttrS2C", &protocolpb.UpdateHeroAttrS2C{
		PlayerId: playerID,
		Cause:    &protocolpb.CauseOrigin{S: protocolpb.CauseOrigin_shop_open, Id: int64(landID)},
		EffectDatas: []*protocolpb.HeroAttrEffect{{
			PlayerId: playerID,
			Data: &protocolpb.HeroAttrEffect_Gold{Gold: &protocolpb.HeroGoldChangeS2C{
				PlayerId: playerID, ChangeGold: newGold - oldGold, OriGold: oldGold, CurrGold: newGold,
			}},
		}},
	}, recipients, 0)
}

func (s *Server) passiveGoldPush(playerID int64, skillID int32, oldGold, newGold int32, recipients []int64) Push {
	return s.pushFor("UpdateHeroAttrS2C", &protocolpb.UpdateHeroAttrS2C{
		PlayerId: playerID,
		Cause:    &protocolpb.CauseOrigin{S: protocolpb.CauseOrigin_skill, Id: int64(skillID)},
		EffectDatas: []*protocolpb.HeroAttrEffect{{
			PlayerId: playerID,
			Data: &protocolpb.HeroAttrEffect_Gold{Gold: &protocolpb.HeroGoldChangeS2C{
				PlayerId: playerID, ChangeGold: newGold - oldGold, OriGold: oldGold, CurrGold: newGold,
			}},
		}},
	}, recipients, 0)
}

func (s *Server) handleShopBuy(ctx context.Context, sess *Session, in wire.Frame, q *protocolpb.ShopBuyC2S) (DispatchResult, error) {
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
	if pending != 0 {
		return DispatchResult{Err: ErrRoomActionIncorrect}, nil
	}
	phase, err := s.store.CurrentTurnPhase(ctx, roomID)
	if err != nil || phase != store.TurnPhaseShop {
		return DispatchResult{Err: ErrRoomActionIncorrect}, nil
	}
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
		return DispatchResult{Err: ErrRoomPlayerNotExist}, nil
	}
	if landType, exists := s.domain.LandTypeAt(room, player.NodeID); !exists || landType != 7 {
		s.log.Error("pending shop phase is not on a Shop land", "room_id", roomID, "player_id", playerID, "land_id", player.NodeID)
		return DispatchResult{Err: ErrRoomActionIncorrect}, nil
	}
	shop, err := s.store.ActiveShop(ctx, roomID, playerID)
	if err != nil {
		return DispatchResult{Err: ErrRoomActionIncorrect}, nil
	}
	indexes := append([]int32(nil), q.BuyCards...)
	if len(indexes) > len(shop.Cards) {
		return DispatchResult{Err: ErrRoomActionIncorrect}, nil
	}
	seen := make(map[int32]struct{}, len(indexes))
	for _, index := range indexes {
		if index < 0 || int(index) >= len(shop.Cards) || shop.Alreadys[index] {
			return DispatchResult{Err: ErrRoomActionIncorrect}, nil
		}
		if _, duplicate := seen[index]; duplicate {
			return DispatchResult{Err: ErrRoomActionIncorrect}, nil
		}
		seen[index] = struct{}{}
	}
	uniqueIDs := make([]int32, 0, len(indexes))
	for range indexes {
		uniqueID, idErr := newCardUniqueID()
		if idErr != nil {
			return DispatchResult{}, idErr
		}
		uniqueIDs = append(uniqueIDs, uniqueID)
	}
	var handLimit int32
	if len(indexes) > 0 {
		var hasLimit bool
		handLimit, hasLimit = s.domain.CardInHandLimit(room.Mode)
		if !hasLimit {
			handLimit, hasLimit = s.resources.GlobalInt("GAME_CARDINHAND_LIMIT")
		}
		if !hasLimit || handLimit <= 0 {
			return DispatchResult{Err: ErrNotOpen}, nil
		}
	}
	raw, err := proto.Marshal(q)
	if err != nil {
		return DispatchResult{}, err
	}
	result, created, err := s.store.CompleteLandShopBuy(ctx, roomID, playerID, in.CmdID, in.UPSN, raw, indexes, handLimit, uniqueIDs)
	switch {
	case errors.Is(err, store.ErrActionNotReady), errors.Is(err, store.ErrShopNotActive), errors.Is(err, store.ErrShopInvalidChoice), errors.Is(err, store.ErrShopHandFull):
		return DispatchResult{Err: ErrRoomActionIncorrect}, nil
	case errors.Is(err, store.ErrShopInsufficientGold):
		return DispatchResult{Err: ErrItemEnough}, nil
	case err != nil:
		return DispatchResult{}, err
	case !created:
		return DispatchResult{Err: ErrRoomActionAlreadyDone}, nil
	}
	msg := shopBuyMessage(playerID, result)
	ids := mustMemberIDs(ctx, s.store, roomID)
	pushes := make([]Push, 0, 3)
	if len(result.Bought) > 0 {
		pushes = append(pushes, s.pushFor("UpdateHeroAttrS2C", shopBuyAttrUpdate(playerID, player.NodeID, result), ids, 0))
		pushes = append(pushes, s.actionPush(round, 5029, playerID, shopActionData(result.State)))
		s.log.Info("land shop cards purchased", "room_id", roomID, "player_id", playerID, "card_indexes", result.Bought, "gold_before", result.OldGold, "gold_after", result.NewGold)
	} else if result.Closed {
		s.log.Info("land shop closed", "room_id", roomID, "player_id", playerID)
		pushes = append(pushes, s.turnPushes(ctx, roomID, result.Round, result.NextPlayer)...)
	}
	return DispatchResult{Message: msg, Pushes: pushes}, nil
}

func (s *Server) resolveBotShop(ctx context.Context, room store.Room, bot store.Player, ids []int64) (int64, int32, []Push, error) {
	landType, exists := s.domain.LandTypeAt(room, bot.NodeID)
	if !exists || landType != 7 {
		return 0, 0, nil, fmt.Errorf("bot shop phase is not on a Shop land %d", bot.NodeID)
	}
	shop, err := s.store.ActiveShop(ctx, room.ID, bot.ID)
	if err != nil {
		return 0, 0, nil, err
	}
	handLimit, ok := s.domain.CardInHandLimit(room.Mode)
	if !ok {
		handLimit, ok = s.resources.GlobalInt("GAME_CARDINHAND_LIMIT")
	}
	if !ok || handLimit <= 0 {
		return 0, 0, nil, errors.New("shop hand limit is unavailable")
	}
	capacity := int(handLimit) - len(bot.Cards)
	if capacity < 0 {
		capacity = 0
	}
	countByGold := len(shop.Cards)
	if shop.Gold > 0 {
		countByGold = int(bot.Gold / shop.Gold)
	}
	if countByGold < 0 {
		countByGold = 0
	}
	if countByGold > capacity {
		countByGold = capacity
	}
	indexes := make([]int32, 0, countByGold)
	for i, sold := range shop.Alreadys {
		if !sold && len(indexes) < countByGold {
			indexes = append(indexes, int32(i))
		}
	}
	pushes := make([]Push, 0, 4)
	if len(indexes) > 0 {
		uniqueIDs := make([]int32, 0, len(indexes))
		for range indexes {
			id, idErr := newCardUniqueID()
			if idErr != nil {
				return 0, 0, pushes, idErr
			}
			uniqueIDs = append(uniqueIDs, id)
		}
		buy := &protocolpb.ShopBuyC2S{BuyCards: indexes}
		raw, marshalErr := proto.Marshal(buy)
		if marshalErr != nil {
			return 0, 0, pushes, marshalErr
		}
		result, created, buyErr := s.store.CompleteLandShopBuy(ctx, room.ID, bot.ID, 5029, s.nextActionSN(), raw, indexes, handLimit, uniqueIDs)
		if buyErr != nil {
			return 0, 0, pushes, fmt.Errorf("bot land shop purchase failed: %w", buyErr)
		}
		if !created {
			return 0, 0, pushes, errors.New("bot land shop purchase was already processed")
		}
		pushes = append(pushes,
			s.pushFor("ShopBuyS2C", shopBuyMessage(bot.ID, result), ids, 0),
			s.pushFor("UpdateHeroAttrS2C", shopBuyAttrUpdate(bot.ID, bot.NodeID, result), ids, 0),
		)
		s.log.Info("bot land shop cards purchased", "room_id", room.ID, "player_id", bot.ID, "card_indexes", result.Bought, "gold_before", result.OldGold, "gold_after", result.NewGold)
		shop = result.State
	}
	closeReq := &protocolpb.ShopBuyC2S{}
	closeRaw, err := proto.Marshal(closeReq)
	if err != nil {
		return 0, 0, pushes, err
	}
	result, created, err := s.store.CompleteLandShopBuy(ctx, room.ID, bot.ID, 5029, s.nextActionSN(), closeRaw, nil, handLimit, nil)
	if err != nil {
		return 0, 0, pushes, fmt.Errorf("bot could not close land shop: %w", err)
	}
	if !created || !result.Closed {
		return 0, 0, pushes, errors.New("bot land shop close was not committed")
	}
	pushes = append(pushes, s.pushFor("ShopBuyS2C", shopBuyMessage(bot.ID, result), ids, 0))
	return result.NextPlayer, result.Round, pushes, nil
}
