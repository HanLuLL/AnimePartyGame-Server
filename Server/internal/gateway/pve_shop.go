package gateway

import (
	"context"
	"errors"
	"fmt"

	store "astralparty-server/internal/db"
	wire "astralparty-server/internal/protocol"
	modelpb "astralparty-server/internal/protocol/gen/model"
	protocolpb "astralparty-server/internal/protocol/gen/protocol"
	"google.golang.org/protobuf/proto"
)

func (s *Server) preparePVEShop(room store.Room, player store.Player) (store.ShopState, error) {
	params, ok := s.domain.LandParams(22)
	if !ok || len(params) < 4 || params[0] < 0 || params[2] <= 0 || params[2] > 12 || params[3] < 0 {
		return store.ShopState{}, errors.New("PVE shop offer parameters are missing or invalid")
	}
	count := int(params[2])
	cards := make([]int32, 0, count)
	if room.Mode == 10 && (room.MapID == 1001 || room.MapID == 1002) {
		for i := 0; i < count; i++ {
			cardID, err := randomWeightedCardID(noviceTutorialCardPool)
			if err != nil {
				return store.ShopState{}, err
			}
			cards = append(cards, cardID)
		}
	} else {
		effectPoolID, _, configured := s.domain.CardPoolsForMapMode(int64(room.MapID), room.Mode)
		if !configured {
			return store.ShopState{}, fmt.Errorf("PVE shop card pool is unavailable for map %d mode %d", room.MapID, room.Mode)
		}
		pool, found := s.domain.EffectCardIDs(effectPoolID)
		if !found || len(pool) == 0 {
			return store.ShopState{}, fmt.Errorf("PVE shop effect-card pool %d is empty", effectPoolID)
		}
		for i := 0; i < count; i++ {
			cardID, err := randomCardID(pool)
			if err != nil {
				return store.ShopState{}, fmt.Errorf("draw PVE shop offer for player %d: %w", player.ID, err)
			}
			cards = append(cards, cardID)
		}
	}
	flags := make([]bool, len(cards))
	return store.ShopState{
		PlayerID: player.ID, Cards: cards, Gold: params[0], FreeCard: params[1],
		FreeCardNum: params[3], Alreadys: make([]bool, len(cards)), PVE: true,
		TalentSkillFreeCard: flags,
	}, nil
}

func pveShopActionData(state store.ShopState) *protocolpb.PVEShopBuyC2S {
	return &protocolpb.PVEShopBuyC2S{
		Cards: append([]int32(nil), state.Cards...), FreeCard: state.FreeCard, Gold: state.Gold,
		AssistGold: state.AssistGold, FreeCardNum: state.FreeCardNum,
		Alreadys: append([]bool(nil), state.Alreadys...), TalentSkillFreeCard: append([]bool(nil), state.TalentSkillFreeCard...),
		DisCountGold: state.DiscountGold,
	}
}

func pveShopMessage(playerID, assistPlayer int64, result store.ShopBuyResult) *protocolpb.PVEShopBuyS2C {
	return &protocolpb.PVEShopBuyS2C{
		PlayerId: playerID, BuyCards: append([]int32(nil), result.Bought...), AssistPlayer: assistPlayer,
		Cards: append([]int32(nil), result.State.Cards...), Alreadys: append([]bool(nil), result.State.Alreadys...),
		IsClose: result.Closed,
	}
}

func pveShopOfferPrice(state store.ShopState, index int32) int32 {
	price := state.Gold
	if index >= 0 && int(index) < len(state.TalentSkillFreeCard) && state.TalentSkillFreeCard[index] {
		price = state.FreeCard
	}
	price -= state.DiscountGold
	if price < 0 {
		return 0
	}
	return price
}

func pveShopAttrUpdate(playerID int64, landID int32, result store.ShopBuyResult) *protocolpb.UpdateHeroAttrS2C {
	cards := make([]*modelpb.CardInfo, 0, len(result.Cards))
	for _, card := range result.Cards {
		cards = append(cards, &modelpb.CardInfo{
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
				PlayerId: playerID, Cards: cards,
			}}},
		},
	}
}

func (s *Server) handlePVEShopBuy(ctx context.Context, sess *Session, in wire.Frame, q *protocolpb.PVEShopBuyC2S) (DispatchResult, error) {
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
	if err != nil || pending != 0 || phase != store.TurnPhasePVEShop {
		return DispatchResult{Err: ErrRoomActionIncorrect}, nil
	}
	if q.AssistPlayer != 0 || q.AssistGold != 0 || (q.IsClose && len(q.BuyCards) != 0) {
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
	if landType, exists := s.domain.LandTypeAt(room, player.NodeID); !exists || landType != 22 {
		return DispatchResult{Err: ErrRoomActionIncorrect}, nil
	}
	shop, err := s.store.ActiveShop(ctx, roomID, playerID)
	if err != nil || !shop.PVE {
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
		var found bool
		handLimit, found = s.domain.CardInHandLimit(room.Mode)
		if !found {
			handLimit, found = s.resources.GlobalInt("GAME_CARDINHAND_LIMIT")
		}
		if !found || handLimit <= 0 {
			return DispatchResult{Err: ErrNotOpen}, nil
		}
	}
	raw, err := proto.Marshal(q)
	if err != nil {
		return DispatchResult{}, err
	}
	result, created, err := s.store.CompletePVEShopBuy(ctx, roomID, playerID, in.CmdID, in.UPSN, raw, indexes, handLimit, uniqueIDs, q.IsClose)
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
	message := pveShopMessage(playerID, 0, result)
	ids := mustMemberIDs(ctx, s.store, roomID)
	pushes := []Push{s.pushFor("PVEShopBuyS2C", message, ids, playerID)}
	if result.Closed {
		s.log.Info("PVE land shop closed", "room_id", roomID, "player_id", playerID)
		pushes = append(pushes, s.turnPushes(ctx, roomID, result.Round, result.NextPlayer)...)
	} else {
		pushes = append(pushes,
			s.pushFor("UpdateHeroAttrS2C", pveShopAttrUpdate(playerID, player.NodeID, result), ids, 0),
			s.actionPush(round, 5215, playerID, pveShopActionData(result.State)),
		)
		s.log.Info("PVE land shop cards purchased", "room_id", roomID, "player_id", playerID, "card_ids", result.Bought, "gold_before", result.OldGold, "gold_after", result.NewGold)
	}
	return DispatchResult{Message: message, Pushes: pushes}, nil
}

func (s *Server) resolveBotPVEShop(ctx context.Context, room store.Room, bot store.Player, ids []int64) (int64, int32, []Push, error) {
	landType, exists := s.domain.LandTypeAt(room, bot.NodeID)
	if !exists || landType != 22 {
		return 0, 0, nil, fmt.Errorf("bot PVE shop phase is not on a PVE shop land %d", bot.NodeID)
	}
	shop, err := s.store.ActiveShop(ctx, room.ID, bot.ID)
	if err != nil || !shop.PVE {
		return 0, 0, nil, fmt.Errorf("bot PVE shop offer is unavailable: %w", err)
	}
	handLimit, ok := s.domain.CardInHandLimit(room.Mode)
	if !ok {
		handLimit, ok = s.resources.GlobalInt("GAME_CARDINHAND_LIMIT")
	}
	if !ok || handLimit <= 0 {
		return 0, 0, nil, errors.New("PVE shop hand limit is unavailable")
	}
	capacity := int(handLimit) - len(bot.Cards)
	if capacity < 0 {
		capacity = 0
	}
	indexes := make([]int32, 0, capacity)
	var total int32
	for index, sold := range shop.Alreadys {
		if sold || len(indexes) >= capacity {
			continue
		}
		price := pveShopOfferPrice(shop, int32(index))
		if price <= bot.Gold-total {
			indexes = append(indexes, int32(index))
			total += price
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
		request := &protocolpb.PVEShopBuyC2S{BuyCards: indexes}
		raw, marshalErr := proto.Marshal(request)
		if marshalErr != nil {
			return 0, 0, pushes, marshalErr
		}
		result, created, buyErr := s.store.CompletePVEShopBuy(ctx, room.ID, bot.ID, 5215, s.nextActionSN(), raw, indexes, handLimit, uniqueIDs, false)
		if buyErr != nil {
			return 0, 0, pushes, fmt.Errorf("bot PVE shop purchase failed: %w", buyErr)
		}
		if !created {
			return 0, 0, pushes, errors.New("bot PVE shop purchase was already processed")
		}
		pushes = append(pushes,
			s.pushFor("PVEShopBuyS2C", pveShopMessage(bot.ID, 0, result), ids, 0),
			s.pushFor("UpdateHeroAttrS2C", pveShopAttrUpdate(bot.ID, bot.NodeID, result), ids, 0),
		)
	}
	closeRequest := &protocolpb.PVEShopBuyC2S{IsClose: true}
	closeRaw, err := proto.Marshal(closeRequest)
	if err != nil {
		return 0, 0, pushes, err
	}
	result, created, err := s.store.CompletePVEShopBuy(ctx, room.ID, bot.ID, 5215, s.nextActionSN(), closeRaw, nil, handLimit, nil, true)
	if err != nil {
		return 0, 0, pushes, fmt.Errorf("bot could not close PVE shop: %w", err)
	}
	if !created || !result.Closed {
		return 0, 0, pushes, errors.New("bot PVE shop close was not committed")
	}
	pushes = append(pushes, s.pushFor("PVEShopBuyS2C", pveShopMessage(bot.ID, 0, result), ids, 0))
	return result.NextPlayer, result.Round, pushes, nil
}
