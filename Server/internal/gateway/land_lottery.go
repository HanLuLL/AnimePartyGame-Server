package gateway

import (
	"context"
	"crypto/rand"
	"errors"
	"fmt"
	"math/big"

	store "astralparty-server/internal/db"
	wire "astralparty-server/internal/protocol"
	protocolpb "astralparty-server/internal/protocol/gen/protocol"
	"google.golang.org/protobuf/proto"
)

var errLotteryConfigMissing = errors.New("lottery land configuration is missing")

func (s *Server) lotterySettings() (int32, int32, error) {
	params, ok := s.domain.LandParams(9)
	if !ok || len(params) == 0 || params[0] <= 0 {
		return 0, 0, errLotteryConfigMissing
	}
	limit, ok := s.resources.GlobalInt("GAME_LAND_LOTTERY_NUMB_LIMIT")
	if !ok || limit <= 0 || params[0] > limit {
		return 0, 0, errLotteryConfigMissing
	}
	return params[0], limit, nil
}

func lotteryHasCapacity(player store.Player, chooseCount, numberLimit int32) bool {
	available := int32(0)
	for value := int32(1); value <= numberLimit; value++ {
		if !player.Lotterys[value] {
			available++
		}
	}
	return available >= chooseCount
}

func chooseLotteryValues(player store.Player, chooseCount, numberLimit int32) ([]int32, error) {
	values := make([]int32, 0, chooseCount)
	for value := int32(1); value <= numberLimit && int32(len(values)) < chooseCount; value++ {
		if !player.Lotterys[value] {
			values = append(values, value)
		}
	}
	if int32(len(values)) != chooseCount {
		return nil, store.ErrInvalidLotteryChoice
	}
	return values, nil
}

func lotteryAttrUpdate(playerID int64, landID int32, lotterys map[int32]bool) *protocolpb.UpdateHeroAttrS2C {
	return &protocolpb.UpdateHeroAttrS2C{
		PlayerId: playerID,
		Cause:    &protocolpb.CauseOrigin{S: protocolpb.CauseOrigin_land, Id: int64(landID)},
		EffectDatas: []*protocolpb.HeroAttrEffect{{
			PlayerId: playerID,
			Data: &protocolpb.HeroAttrEffect_Lottery{Lottery: &protocolpb.HeroLotteryChangeS2C{
				PlayerId: playerID, Lotterys: lotterys,
			}},
		}},
	}
}

func (s *Server) handleLotteryChoice(ctx context.Context, sess *Session, in wire.Frame, q *protocolpb.LotteryChoiceC2S) (DispatchResult, error) {
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
	if pending != 0 || phase != store.TurnPhaseLottery {
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
		return DispatchResult{Err: ErrRoomNotAction}, nil
	}
	landType, exists := s.domain.LandTypeAt(room, player.NodeID)
	if !exists || landType != 9 {
		return DispatchResult{Err: ErrRoomActionIncorrect}, nil
	}
	chooseCount, numberLimit, err := s.lotterySettings()
	if errors.Is(err, errLotteryConfigMissing) {
		return DispatchResult{Err: ErrNotOpen}, nil
	}
	if err != nil {
		return DispatchResult{}, err
	}
	if !lotteryHasCapacity(player, chooseCount, numberLimit) {
		return DispatchResult{Err: ErrRoomActionIncorrect}, nil
	}
	raw, err := proto.Marshal(q)
	if err != nil {
		return DispatchResult{}, err
	}
	result, created, err := s.store.CompleteLotteryChoice(ctx, roomID, playerID, in.CmdID, in.UPSN, raw, q.Vals, chooseCount, numberLimit)
	if errors.Is(err, store.ErrTurnPlayerMismatch) {
		return DispatchResult{Err: ErrRoomActionPlayer}, nil
	}
	if errors.Is(err, store.ErrActionNotReady) || errors.Is(err, store.ErrInvalidLotteryChoice) {
		return DispatchResult{Err: ErrRoomActionIncorrect}, nil
	}
	if err != nil {
		return DispatchResult{}, err
	}
	if !created {
		return DispatchResult{Err: ErrRoomActionAlreadyDone}, nil
	}
	s.log.Info("lottery numbers selected", "room_id", roomID, "player_id", playerID, "land_id", player.NodeID, "values", q.Vals)
	message := &protocolpb.LotteryChoiceS2C{PlayerId: playerID, Vals: q.Vals}
	ids := mustMemberIDs(ctx, s.store, roomID)
	pushes := []Push{
		s.pushFor("LotteryChoiceS2C", message, ids, playerID),
		s.pushFor("UpdateHeroAttrS2C", lotteryAttrUpdate(playerID, player.NodeID, result.Lotterys), ids, 0),
	}
	// After the last player has chosen, run the scheduled draw: pick the
	// winning number, pay every player who holds it, and broadcast
	// LotteryDrawS2C with the awarded gold.
	drawPushes, drawErr := s.settleLotteryDraw(ctx, roomID, numberLimit)
	if drawErr != nil {
		s.log.Error("lottery draw settlement failed", "room_id", roomID, "err", drawErr)
	}
	pushes = append(pushes, drawPushes...)
	pushes = append(pushes, s.turnPushes(ctx, roomID, result.Round, result.NextPlayer)...)
	return DispatchResult{Message: message, Pushes: pushes}, nil
}

// settleLotteryDraw resolves the lottery round once every member has locked
// their numbers. The winning number is drawn uniformly from 1..numberLimit;
// each holder earns GAME_LAND_LOTTERY_GOLD_BASE + (holders-1) *
// GAME_LAND_LOTTERY_GOLD_ADD (a shared pot scaled by the config globals). The
// gold change is pushed via UpdateHeroAttrS2C so all clients stay in sync.
func (s *Server) settleLotteryDraw(ctx context.Context, roomID int64, numberLimit int32) ([]Push, error) {
	room, err := s.store.RoomSnapshot(ctx, roomID)
	if err != nil {
		return nil, err
	}
	// The draw only fires once every member has picked their full set.
	chooseCount, _, err := s.lotterySettings()
	if err != nil {
		return nil, err
	}
	if len(room.Players) == 0 {
		return nil, nil
	}
	for _, member := range room.Players {
		count := 0
		for _, picked := range member.Lotterys {
			if picked {
				count++
			}
		}
		if count < int(chooseCount) {
			return nil, nil
		}
	}
	draw, err := rand.Int(rand.Reader, big.NewInt(int64(numberLimit)))
	if err != nil {
		return nil, err
	}
	winning := int32(draw.Int64()) + 1
	base, baseOK := s.resources.GlobalInt("GAME_LAND_LOTTERY_GOLD_BASE")
	if !baseOK || base <= 0 {
		base = 10
	}
	add, addOK := s.resources.GlobalInt("GAME_LAND_LOTTERY_GOLD_ADD")
	if !addOK || add < 0 {
		add = 10
	}
	winners := []int64{}
	for _, member := range room.Players {
		if member.Lotterys[winning] {
			winners = append(winners, member.ID)
		}
	}
	award := int32(0)
	pushes := []Push{}
	if len(winners) > 0 {
		award = base + add*int32(len(winners)-1)
	}
	for _, winnerID := range winners {
		oldGold, newGold, err := s.store.AddRoomGold(ctx, roomID, winnerID, award)
		if err != nil {
			return pushes, err
		}
		pushes = append(pushes, s.pushFor("UpdateHeroAttrS2C", lotteryGoldUpdate(winnerID, award, oldGold, newGold), []int64{winnerID}, 0))
	}
	pushes = append(pushes, s.pushFor("LotteryDrawS2C", &protocolpb.LotteryDrawS2C{PlayerIds: winners, Val: winning, AwardGold: award}, mustMemberIDs(ctx, s.store, roomID), 0))
	s.log.Info("lottery draw settled", "room_id", roomID, "winning", winning, "winners", winners, "award_gold", award)
	return pushes, nil
}

// lotteryGoldUpdate reports one winner's gold change through the land-cause
// hero-attr channel the client already handles for RollGold payouts.
func lotteryGoldUpdate(playerID int64, change, oldGold, newGold int32) *protocolpb.UpdateHeroAttrS2C {
	return &protocolpb.UpdateHeroAttrS2C{
		PlayerId: playerID,
		Cause:    &protocolpb.CauseOrigin{S: protocolpb.CauseOrigin_land, Id: int64(9)},
		EffectDatas: []*protocolpb.HeroAttrEffect{{
			PlayerId: playerID,
			Data: &protocolpb.HeroAttrEffect_Gold{Gold: &protocolpb.HeroGoldChangeS2C{
				PlayerId: playerID, ChangeGold: change, OriGold: oldGold, CurrGold: newGold,
			}},
		}},
	}
}

func (s *Server) resolveBotLotteryChoice(ctx context.Context, roomID int64, player store.Player, landID int32, ids []int64) (int64, int32, []Push, error) {
	chooseCount, numberLimit, err := s.lotterySettings()
	if err != nil {
		return 0, 0, nil, fmt.Errorf("load lottery settings: %w", err)
	}
	values, err := chooseLotteryValues(player, chooseCount, numberLimit)
	if err != nil {
		return 0, 0, nil, err
	}
	request := &protocolpb.LotteryChoiceC2S{Vals: values}
	raw, err := proto.Marshal(request)
	if err != nil {
		return 0, 0, nil, err
	}
	result, created, err := s.store.CompleteLotteryChoice(ctx, roomID, player.ID, 5041, s.nextActionSN(), raw, values, chooseCount, numberLimit)
	if err != nil {
		return 0, 0, nil, err
	}
	if !created {
		return 0, 0, nil, store.ErrActionNotReady
	}
	s.log.Info("bot lottery numbers selected", "room_id", roomID, "player_id", player.ID, "land_id", landID, "values", values)
	message := &protocolpb.LotteryChoiceS2C{PlayerId: player.ID, Vals: values}
	pushes := []Push{
		s.pushFor("LotteryChoiceS2C", message, ids, 0),
		s.pushFor("UpdateHeroAttrS2C", lotteryAttrUpdate(player.ID, landID, result.Lotterys), ids, 0),
	}
	return result.NextPlayer, result.Round, pushes, nil
}
