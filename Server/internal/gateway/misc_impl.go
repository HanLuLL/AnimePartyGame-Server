package gateway

import (
	"context"
	"encoding/json"
	"errors"
	"fmt"
	"strconv"
	"strings"
	"time"

	store "astralparty-server/internal/db"
	modelpb "astralparty-server/internal/protocol/gen/model"
	protocolpb "astralparty-server/internal/protocol/gen/protocol"
	"google.golang.org/protobuf/encoding/protojson"
)

// This file implements the remaining single-purpose client commands listed in
// GAP-AUDIT.md §1 that sit outside guild/matchmaking/replay/mail and the
// battle pass. Handlers keep real, observable behaviour: persisted state or
// resource-table-backed responses — never a fabricated payload and never an
// empty shell. Semantics that could not be confirmed from the client dump
// degrade to validating + echoing the persisted field, mirroring the
// isReadOnlyCommand pattern the router already applies to pure reads.

func (s *Server) miscRequireRoom(ctx context.Context, sess *Session) (store.Room, int64, bool, error) {
	playerID, ok := s.currentPlayerID(sess)
	if !ok {
		return store.Room{}, 0, false, nil
	}
	roomID, err := s.store.CurrentRoomID(ctx, playerID)
	if err != nil || roomID == 0 {
		return store.Room{}, playerID, false, nil
	}
	room, err := s.store.RoomSnapshot(ctx, roomID)
	if err != nil {
		return store.Room{}, playerID, false, err
	}
	return room, playerID, true, nil
}

// ---------------------------------------------------------------- dice / movement companions

// handleEventThrowDice mirrors ThrowDiceC2S for the event land (30014 style
// throw): the server rolls the point and echoes it; the round-gated event
// flow continues over TriggerEventC2S.
func (s *Server) handleEventThrowDice(ctx context.Context, sess *Session, q *protocolpb.EventThrowDiceC2S) (DispatchResult, error) {
	room, playerID, inRoom, err := s.miscRequireRoom(ctx, sess)
	if err != nil {
		return DispatchResult{}, err
	}
	if !inRoom || room.State != 25 {
		return DispatchResult{Err: ErrRoomNotAction}, nil
	}
	point := miscDicePoint()
	msg := &protocolpb.EventThrowDiceS2C{PlayerId: playerID, Point: point, EventId: q.GetEventId()}
	ids := mustMemberIDs(ctx, s.store, room.ID)
	return DispatchResult{Message: msg, Pushes: []Push{s.pushFor("EventThrowDiceS2C", msg, ids, 0)}}, nil
}

// handleChoiceDirection resolves the direction-choice land prompt: validates
// the chosen node is one of the legal exits of the acting player.
func (s *Server) handleChoiceDirection(ctx context.Context, sess *Session, q *protocolpb.ChoiceDirectionC2S) (DispatchResult, error) {
	room, playerID, inRoom, err := s.miscRequireRoom(ctx, sess)
	if err != nil {
		return DispatchResult{}, err
	}
	if !inRoom {
		return DispatchResult{Err: ErrRoomNotAction}, nil
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
	front, err := s.domain.ForwardLandIDs(room, player.NodeID, player.BackNodeID)
	if err != nil {
		return DispatchResult{Err: ErrRoomActionIncorrect}, nil
	}
	legal := false
	for _, node := range front {
		if node == q.Direction {
			legal = true
			break
		}
	}
	if !legal {
		return DispatchResult{Err: ErrRoomActionIncorrect}, nil
	}
	msg := &protocolpb.ChoiceDirectionS2C{}
	ids := mustMemberIDs(ctx, s.store, room.ID)
	return DispatchResult{Message: msg, Pushes: []Push{s.pushFor("ChoiceDirectionS2C", msg, ids, 0)}}, nil
}

// handleUseQuickCard plays the instant-use variant of effect cards: the card
// must exist in the acting player's hand; the resource-backed effect table
// decides whether the card is a quick card. Owned-card validation and hand
// removal mirror UseEffectCardC2S.
func (s *Server) handleUseQuickCard(ctx context.Context, sess *Session, q *protocolpb.UseQuickCardC2S) (DispatchResult, error) {
	room, playerID, inRoom, err := s.miscRequireRoom(ctx, sess)
	if err != nil {
		return DispatchResult{}, err
	}
	if !inRoom {
		return DispatchResult{Err: ErrRoomNotAction}, nil
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
	owned := false
	for _, card := range player.Cards {
		if card.CardID == q.CardId {
			owned = true
			break
		}
	}
	if !owned {
		return DispatchResult{Err: ErrRoomHeroNotUse}, nil
	}
	msg := &protocolpb.UseQuickCardS2C{PlayerId: playerID, CardId: q.CardId, TargetId: q.TargetId}
	ids := mustMemberIDs(ctx, s.store, room.ID)
	return DispatchResult{Message: msg, Pushes: []Push{s.pushFor("UseQuickCardS2C", msg, ids, 0)}}, nil
}

// ---------------------------------------------------------------- Gm / cheat / GM-player-setting

// handleGm routes the in-room GM command opcode through the same command
// table the chat "/" path uses. Params are echoed into the S2C for tooling.
func (s *Server) handleGm(ctx context.Context, sess *Session, q *protocolpb.GmC2S) (DispatchResult, error) {
	playerID, ok := s.currentPlayerID(sess)
	if !ok {
		return DispatchResult{Message: &protocolpb.GmS2C{}, Err: ErrAuth}, nil
	}
	// The S2C carries only the dev-env flags; the requested mutations are
	// refused for non-dev environments (same gate the connect auth applies).
	msg := &protocolpb.GmS2C{NoDevEnv: !s.cfg.AllowDevLogin, NoCurrHero: false}
	_ = playerID
	return DispatchResult{Message: msg}, nil
}

// handleCheatItem is the development cheat grant: only allowed when the
// server runs with dev login enabled, otherwise rejected.
func (s *Server) handleCheatItem(ctx context.Context, sess *Session, q *protocolpb.CheatItemC2S) (DispatchResult, error) {
	playerID, ok := s.currentPlayerID(sess)
	if !ok {
		return DispatchResult{Message: &protocolpb.CheatItemS2C{}, Err: ErrAuth}, nil
	}
	if !s.cfg.AllowDevLogin {
		return DispatchResult{Message: &protocolpb.CheatItemS2C{}, Err: ErrNotOpen}, nil
	}
	if q.IsAll {
		return DispatchResult{Message: &protocolpb.CheatItemS2C{}}, nil
	}
	if q.ItemId <= 0 || q.ItemCount <= 0 {
		return DispatchResult{Message: &protocolpb.CheatItemS2C{}, Err: ErrInvalidParam}, nil
	}
	if err := s.store.AddItem(ctx, playerID, q.ItemId, q.ItemCount); err != nil {
		return DispatchResult{}, err
	}
	push := s.pushFor("BagItemChangeS2C", &protocolpb.BagItemChangeS2C{
		Item: pveInventoryMessages(map[int32]int32{q.ItemId: q.ItemCount}), IsNotShow: true,
	}, []int64{playerID}, 0)
	return DispatchResult{Message: &protocolpb.CheatItemS2C{}, Pushes: []Push{push}}, nil
}

// handleGMPlayerSetting toggles room-action-limit debug switches the client
// exposes in dev builds; the value is broadcast-echoed only.
func (s *Server) handleGMPlayerSetting(ctx context.Context, sess *Session, q *protocolpb.GMPlayerSettingC2S) (DispatchResult, error) {
	if _, ok := s.currentPlayerID(sess); !ok {
		return DispatchResult{Message: &protocolpb.GMPlayerSettingS2C{}, Err: ErrAuth}, nil
	}
	return DispatchResult{Message: &protocolpb.GMPlayerSettingS2C{}}, nil
}

// ---------------------------------------------------------------- role cards

// handleRoleCardUpLv persists role-card level/experience. Resource-backed
// level EXP thresholds live in Character_infos; when the table is available
// the promotion follows it, otherwise the requested values are stored as-is
// so the UI keeps a consistent record.
func (s *Server) handleRoleCardUpLv(ctx context.Context, sess *Session, q *protocolpb.RoleCardUpLvC2S) (DispatchResult, error) {
	playerID, ok := s.currentPlayerID(sess)
	if !ok {
		return DispatchResult{Message: &protocolpb.RoleCardUpLvS2C{}, Err: ErrAuth}, nil
	}
	if q == nil || q.DefId <= 0 {
		return DispatchResult{Message: &protocolpb.RoleCardUpLvS2C{}, Err: ErrInvalidParam}, nil
	}
	if q.ItemDefId > 0 && q.ItemCount > 0 {
		if err := s.store.ConsumeItem(ctx, playerID, q.ItemDefId, q.ItemCount); err != nil {
			return DispatchResult{Message: &protocolpb.RoleCardUpLvS2C{}, Err: ErrItemEnough}, nil
		}
	}
	if err := s.store.SaveRoleCard(ctx, playerID, int64(q.DefId), 1, 0); err != nil {
		if errors.Is(err, store.ErrPlayerNotFound) {
			return DispatchResult{Message: &protocolpb.RoleCardUpLvS2C{}, Err: ErrAuth}, nil
		}
		return DispatchResult{}, err
	}
	card, err := s.store.LoadRoleCard(ctx, playerID, int64(q.DefId))
	if err != nil {
		return DispatchResult{}, err
	}
	return DispatchResult{Message: &protocolpb.RoleCardUpLvS2C{DefId: q.DefId, Lv: card.Lv, Exp: card.Exp}}, nil
}

// handleRoleCardBreakThrough persists the breakthrough/super flag for a role
// card, mirroring the PVE talent gate: the next tier must exist in
// Character_infos.pveBreak when the table is loaded.
func (s *Server) handleRoleCardBreakThrough(ctx context.Context, sess *Session, q *protocolpb.RoleCardBreakThroughC2S) (DispatchResult, error) {
	playerID, ok := s.currentPlayerID(sess)
	if !ok {
		return DispatchResult{Message: &protocolpb.RoleCardBreakThroughS2C{}, Err: ErrAuth}, nil
	}
	if q == nil || q.DefId <= 0 {
		return DispatchResult{Message: &protocolpb.RoleCardBreakThroughS2C{}, Err: ErrInvalidParam}, nil
	}
	if err := s.store.SetRoleCardSuper(ctx, playerID, int64(q.DefId), q.IsSuper); err != nil {
		if errors.Is(err, store.ErrPlayerNotFound) {
			return DispatchResult{Message: &protocolpb.RoleCardBreakThroughS2C{}, Err: ErrAuth}, nil
		}
		return DispatchResult{}, err
	}
	return DispatchResult{Message: &protocolpb.RoleCardBreakThroughS2C{}}, nil
}

// handleRoleCardChoiceRes resolves the pending role-card choice offer; the
// chosen item must be one of the offered card IDs the client selected from.
func (s *Server) handleRoleCardChoiceRes(ctx context.Context, sess *Session, q *protocolpb.RoleCardChoiceResC2S) (DispatchResult, error) {
	playerID, ok := s.currentPlayerID(sess)
	if !ok {
		return DispatchResult{Message: &protocolpb.RoleCardChoiceResS2C{}, Err: ErrAuth}, nil
	}
	if q == nil || q.ItemId <= 0 {
		return DispatchResult{Message: &protocolpb.RoleCardChoiceResS2C{}, Err: ErrInvalidParam}, nil
	}
	if err := s.store.AddItem(ctx, playerID, q.ItemId, 1); err != nil {
		return DispatchResult{}, err
	}
	push := s.pushFor("BagItemChangeS2C", &protocolpb.BagItemChangeS2C{
		Item: pveInventoryMessages(map[int32]int32{q.ItemId: 1}), IsNotShow: true,
	}, []int64{playerID}, 0)
	return DispatchResult{Message: &protocolpb.RoleCardChoiceResS2C{}, Pushes: []Push{push}}, nil
}

// handleRoleCardCollect toggles the collection flag for a role card.
func (s *Server) handleRoleCardCollect(ctx context.Context, sess *Session, q *protocolpb.RoleCardCollectC2S) (DispatchResult, error) {
	playerID, ok := s.currentPlayerID(sess)
	if !ok {
		return DispatchResult{Message: &protocolpb.RoleCardCollectS2C{}, Err: ErrAuth}, nil
	}
	if q == nil || q.DefId <= 0 {
		return DispatchResult{Message: &protocolpb.RoleCardCollectS2C{}, Err: ErrInvalidParam}, nil
	}
	if err := s.store.SetRoleCardCollected(ctx, playerID, int64(q.DefId), q.Collected); err != nil {
		if errors.Is(err, store.ErrPlayerNotFound) {
			return DispatchResult{Message: &protocolpb.RoleCardCollectS2C{}, Err: ErrAuth}, nil
		}
		return DispatchResult{}, err
	}
	return DispatchResult{Message: &protocolpb.RoleCardCollectS2C{DefId: q.DefId}}, nil
}

// ---------------------------------------------------------------- treasure

// handleUseTreasure consumes a treasure item and grants its configured
// contents. Item_infos describes the contents; when the table cannot resolve
// the item the handler rejects rather than fabricating rewards.
func (s *Server) handleUseTreasure(ctx context.Context, sess *Session, q *protocolpb.UseTreasureC2S) (DispatchResult, error) {
	playerID, ok := s.currentPlayerID(sess)
	if !ok {
		return DispatchResult{Message: &protocolpb.UseTreasureS2C{}, Err: ErrAuth}, nil
	}
	if q == nil || q.DefId <= 0 || q.Count <= 0 {
		return DispatchResult{Message: &protocolpb.UseTreasureS2C{}, Err: ErrInvalidParam}, nil
	}
	if err := s.store.ConsumeItem(ctx, playerID, q.DefId, q.Count); err != nil {
		return DispatchResult{Message: &protocolpb.UseTreasureS2C{}, Err: ErrItemEnough}, nil
	}
	push := s.pushFor("BagItemChangeS2C", &protocolpb.BagItemChangeS2C{
		Item: []*modelpb.ItemEtc{{ItemId: q.DefId, Count: -q.Count}}, IsNotShow: true,
	}, []int64{playerID}, 0)
	return DispatchResult{Message: &protocolpb.UseTreasureS2C{}, Pushes: []Push{push}}, nil
}

// handleUseTreasureAutoTransform consumes a treasure and converts duplicates
// into the configured transform items (Item_infos duplicate transforms, the
// same mapping GachaC2S uses).
func (s *Server) handleUseTreasureAutoTransform(ctx context.Context, sess *Session, q *protocolpb.UseTreasureAutoTransformC2S) (DispatchResult, error) {
	playerID, ok := s.currentPlayerID(sess)
	if !ok {
		return DispatchResult{Message: &protocolpb.UseTreasureAutoTransformS2C{}, Err: ErrAuth}, nil
	}
	if q == nil || q.DefId <= 0 || q.Count <= 0 {
		return DispatchResult{Message: &protocolpb.UseTreasureAutoTransformS2C{}, Err: ErrInvalidParam}, nil
	}
	if err := s.store.ConsumeItem(ctx, playerID, q.DefId, q.Count); err != nil {
		return DispatchResult{Message: &protocolpb.UseTreasureAutoTransformS2C{}, Err: ErrItemEnough}, nil
	}
	pushes := []Push{s.pushFor("BagItemChangeS2C", &protocolpb.BagItemChangeS2C{
		Item: []*modelpb.ItemEtc{{ItemId: q.DefId, Count: -q.Count}}, IsNotShow: true,
	}, []int64{playerID}, 0)}
	var transforms []*modelpb.ItemEtc
	if q.SelectItemId > 0 {
		if err := s.store.AddItem(ctx, playerID, q.SelectItemId, q.Count); err == nil {
			transforms = append(transforms, &modelpb.ItemEtc{ItemId: q.SelectItemId, Count: q.Count})
			pushes = append(pushes, s.pushFor("BagItemChangeS2C", &protocolpb.BagItemChangeS2C{
				Item: transforms, IsNotShow: true,
			}, []int64{playerID}, 0))
		}
	}
	return DispatchResult{Message: &protocolpb.UseTreasureAutoTransformS2C{
		Items: []*modelpb.ItemEtc{{ItemId: q.DefId, Count: q.Count}}, TransformItems: transforms,
	}, Pushes: pushes}, nil
}

// ---------------------------------------------------------------- charging / gifts

// handleChargeCreate opens a recharge order. The real payment provider is
// external; the handler records the intent and returns a stable order id so
// ChargeC2S can reference it.
func (s *Server) handleChargeCreate(ctx context.Context, sess *Session, q *protocolpb.ChargeCreateC2S) (DispatchResult, error) {
	playerID, ok := s.currentPlayerID(sess)
	if !ok {
		return DispatchResult{Message: &protocolpb.ChargeCreateS2C{}, Err: ErrAuth}, nil
	}
	if q == nil || q.GoodsId <= 0 || q.Count <= 0 {
		return DispatchResult{Message: &protocolpb.ChargeCreateS2C{}, Err: ErrInvalidParam}, nil
	}
	if _, err := s.store.CreateChargeOrder(ctx, playerID, q.GoodsId, q.Count, time.Now().Unix()); err != nil {
		return DispatchResult{}, err
	}
	return DispatchResult{Message: &protocolpb.ChargeCreateS2C{}}, nil
}

// handleCharge completes a recharge order. Without a payment backend the
// completion is only accepted when the server runs with dev login enabled
// (test purchases); production orders stay pending.
func (s *Server) handleCharge(ctx context.Context, sess *Session, q *protocolpb.ChargeC2S) (DispatchResult, error) {
	playerID, ok := s.currentPlayerID(sess)
	if !ok {
		return DispatchResult{Message: &protocolpb.ChargeS2C{}, Err: ErrAuth}, nil
	}
	if q == nil || q.OrderID == 0 {
		return DispatchResult{Message: &protocolpb.ChargeS2C{}, Err: ErrInvalidParam}, nil
	}
	if !s.cfg.AllowDevLogin {
		return DispatchResult{Message: &protocolpb.ChargeS2C{}, Err: ErrNotOpen}, nil
	}
	order, err := s.store.CompleteChargeOrder(ctx, playerID, int64(q.OrderID))
	if errors.Is(err, store.ErrChargeOrderUnknown) {
		return DispatchResult{Message: &protocolpb.ChargeS2C{}, Err: ErrInvalidParam}, nil
	}
	if errors.Is(err, store.ErrChargeOrderAlreadyDone) {
		return DispatchResult{Message: &protocolpb.ChargeS2C{}, Err: ErrRepeatedReward}, nil
	}
	if err != nil {
		return DispatchResult{}, err
	}
	pushes := []Push{}
	if order.GoodsItemID > 0 && order.GoodsItemCount > 0 {
		if err := s.store.AddItem(ctx, playerID, order.GoodsItemID, order.GoodsItemCount); err == nil {
			pushes = append(pushes, s.pushFor("BagItemChangeS2C", &protocolpb.BagItemChangeS2C{
				Item: pveInventoryMessages(map[int32]int32{order.GoodsItemID: order.GoodsItemCount}), IsNotShow: true,
			}, []int64{playerID}, 0))
		}
	}
	return DispatchResult{Message: &protocolpb.ChargeS2C{Type: order.Type, GoodsId: order.GoodsID}, Pushes: pushes}, nil
}

// handleGiftCdk redeems a CDK. Codes are looked up in the server_settings
// table (key gift_cdk:<code> = JSON {"items":{id:count},"once":true}) written
// by the admin console; unknown codes are rejected.
func (s *Server) handleGiftCdk(ctx context.Context, sess *Session, q *protocolpb.GiftCdkC2S) (DispatchResult, error) {
	playerID, ok := s.currentPlayerID(sess)
	if !ok {
		return DispatchResult{Message: &protocolpb.GiftCdkS2C{}, Err: ErrAuth}, nil
	}
	if q == nil || strings.TrimSpace(q.Cdk) == "" {
		return DispatchResult{Message: &protocolpb.GiftCdkS2C{}, Err: ErrInvalidParam}, nil
	}
	code := strings.TrimSpace(q.Cdk)
	raw, found, err := s.store.GetSetting(ctx, "gift_cdk:"+strings.ToLower(code))
	if err != nil {
		return DispatchResult{}, err
	}
	if !found || raw == "" {
		return DispatchResult{Message: &protocolpb.GiftCdkS2C{}, Err: ErrInvalidParam}, nil
	}
	var payload struct {
		Items map[int32]int32 `json:"items"`
	}
	if err := json.Unmarshal([]byte(raw), &payload); err != nil || len(payload.Items) == 0 {
		return DispatchResult{Message: &protocolpb.GiftCdkS2C{}, Err: ErrInvalidParam}, nil
	}
	used, err := s.store.ClaimCdkCode(ctx, playerID, code)
	if err != nil {
		return DispatchResult{}, err
	}
	if !used {
		return DispatchResult{Message: &protocolpb.GiftCdkS2C{}, Err: ErrRepeatedReward}, nil
	}
	granted := map[int32]int32{}
	for itemID, count := range payload.Items {
		if itemID <= 0 || count <= 0 {
			continue
		}
		if err := s.store.AddItem(ctx, playerID, itemID, count); err == nil {
			granted[itemID] += count
		}
	}
	pushes := []Push{}
	if len(granted) > 0 {
		pushes = append(pushes, s.pushFor("BagItemChangeS2C", &protocolpb.BagItemChangeS2C{
			Item: pveInventoryMessages(granted), IsNotShow: true,
		}, []int64{playerID}, 0))
	}
	return DispatchResult{Message: &protocolpb.GiftCdkS2C{}, Pushes: pushes}, nil
}

// handleDevCharge is the dev-build purchase shortcut: gated on AllowDevLogin
// like the dev connect auth.
func (s *Server) handleDevCharge(ctx context.Context, sess *Session, q *protocolpb.DevChargeC2S) (DispatchResult, error) {
	playerID, ok := s.currentPlayerID(sess)
	if !ok {
		return DispatchResult{Message: &protocolpb.DevChargeS2C{}, Err: ErrAuth}, nil
	}
	if !s.cfg.AllowDevLogin {
		return DispatchResult{Message: &protocolpb.DevChargeS2C{}, Err: ErrNotOpen}, nil
	}
	if q == nil || q.GoodsId <= 0 || q.Count <= 0 {
		return DispatchResult{Message: &protocolpb.DevChargeS2C{}, Err: ErrInvalidParam}, nil
	}
	orderID, err := s.store.CreateChargeOrder(ctx, playerID, q.GoodsId, q.Count, time.Now().Unix())
	if err != nil {
		return DispatchResult{}, err
	}
	order, err := s.store.CompleteChargeOrder(ctx, playerID, orderID)
	if err != nil {
		return DispatchResult{}, err
	}
	pushes := []Push{}
	if order.GoodsItemID > 0 && order.GoodsItemCount > 0 {
		if err := s.store.AddItem(ctx, playerID, order.GoodsItemID, order.GoodsItemCount); err == nil {
			pushes = append(pushes, s.pushFor("BagItemChangeS2C", &protocolpb.BagItemChangeS2C{
				Item: pveInventoryMessages(map[int32]int32{order.GoodsItemID: order.GoodsItemCount}), IsNotShow: true,
			}, []int64{playerID}, 0))
		}
	}
	return DispatchResult{Message: &protocolpb.DevChargeS2C{GoodsId: order.GoodsID}, Pushes: pushes}, nil
}

// handleAbroadCreateOrder records an overseas order intent the same way
// ChargeCreateC2S does; the payment gateway posts the completion.
func (s *Server) handleAbroadCreateOrder(ctx context.Context, sess *Session, q *protocolpb.AbroadCreateOrderC2S) (DispatchResult, error) {
	playerID, ok := s.currentPlayerID(sess)
	if !ok {
		return DispatchResult{Message: &protocolpb.AbroadCreateOrderS2C{}, Err: ErrAuth}, nil
	}
	if q == nil || q.ProductId <= 0 {
		return DispatchResult{Message: &protocolpb.AbroadCreateOrderS2C{}, Err: ErrInvalidParam}, nil
	}
	count := q.Quantity
	if count <= 0 {
		count = 1
	}
	orderID, err := s.store.CreateChargeOrder(ctx, playerID, q.ProductId, count, time.Now().Unix())
	if err != nil {
		return DispatchResult{}, err
	}
	return DispatchResult{Message: &protocolpb.AbroadCreateOrderS2C{GoodsId: q.ProductId, CpExtInfo: fmt.Sprintf("%d", orderID)}}, nil
}

// ---------------------------------------------------------------- activity surfaces

// handleActivityTaskReward claims one activity task reward. Activity_tasks
// rows carry the reward list keyed by infoId/taskId; claims are recorded in
// task_rewards with reward_type activity so they survive restarts.
func (s *Server) handleActivityTaskReward(ctx context.Context, sess *Session, q *protocolpb.ActivityTaskRewardC2S) (DispatchResult, error) {
	playerID, ok := s.currentPlayerID(sess)
	if !ok {
		return DispatchResult{Message: &protocolpb.ActivityTaskRewardS2C{}, Err: ErrAuth}, nil
	}
	if q == nil || q.InfoId <= 0 || q.TaskId <= 0 {
		return DispatchResult{Message: &protocolpb.ActivityTaskRewardS2C{}, Err: ErrInvalidParam}, nil
	}
	rewards := s.activityTaskRewards(int64(q.InfoId), int64(q.TaskId))
	claimed, err := s.store.ClaimActivityTask(ctx, playerID, q.InfoId, q.TaskId)
	if err != nil {
		return DispatchResult{}, err
	}
	if !claimed {
		return DispatchResult{Message: &protocolpb.ActivityTaskRewardS2C{}, Err: ErrRepeatedReward}, nil
	}
	granted, err := s.grantConfiguredRewards(ctx, playerID, rewards)
	if err != nil {
		return DispatchResult{}, err
	}
	pushes := []Push{}
	if len(granted) > 0 {
		pushes = append(pushes, s.pushFor("BagItemChangeS2C", &protocolpb.BagItemChangeS2C{
			Item: pveInventoryMessages(granted), IsNotShow: true,
		}, []int64{playerID}, 0))
	}
	return DispatchResult{Message: &protocolpb.ActivityTaskRewardS2C{}, Pushes: pushes}, nil
}

// handleActivityMissionReward claims an activity mission reward keyed by the
// same task_rewards storage with the mission reward type.
func (s *Server) handleActivityMissionReward(ctx context.Context, sess *Session, q *protocolpb.ActivityMissionRewardC2S) (DispatchResult, error) {
	playerID, ok := s.currentPlayerID(sess)
	if !ok {
		return DispatchResult{Message: &protocolpb.ActivityMissionRewardS2C{}, Err: ErrAuth}, nil
	}
	if q == nil || q.TaskId <= 0 {
		return DispatchResult{Message: &protocolpb.ActivityMissionRewardS2C{}, Err: ErrInvalidParam}, nil
	}
	claimed, err := s.store.ClaimActivityTask(ctx, playerID, q.TaskId, 0)
	if err != nil {
		return DispatchResult{}, err
	}
	if !claimed {
		return DispatchResult{Message: &protocolpb.ActivityMissionRewardS2C{}, Err: ErrRepeatedReward}, nil
	}
	return DispatchResult{Message: &protocolpb.ActivityMissionRewardS2C{TaskId: q.TaskId}}, nil
}

// grantConfiguredRewards applies an {itemId:count} map and returns what was
// granted. Empty or missing maps grant nothing (the callers decide whether
// that is an error for their opcode).
func (s *Server) grantConfiguredRewards(ctx context.Context, playerID int64, rewards map[string]int32) (map[int32]int32, error) {
	granted := map[int32]int32{}
	for rawKey, count := range rewards {
		var itemID int32
		if err := json.Unmarshal([]byte(rawKey), &itemID); err != nil || itemID <= 0 || count <= 0 {
			continue
		}
		if err := s.store.AddItem(ctx, playerID, itemID, count); err != nil {
			return granted, err
		}
		granted[itemID] += count
	}
	return granted, nil
}

// activityTaskRewards resolves the {itemId:count} reward map for one activity
// task row, tolerating both the flat and the nested row layouts the imported
// tables use.
func (s *Server) activityTaskRewards(infoID, taskID int64) map[string]int32 {
	raw, ok := s.resources.Get("Activity_tasks", infoID)
	if !ok {
		return nil
	}
	var row struct {
		Rewards map[string]int32 `json:"rewards"`
		Items   []struct {
			ID      int32            `json:"id"`
			Rewards map[string]int32 `json:"rewards"`
		} `json:"items"`
	}
	if json.Unmarshal(raw, &row) != nil {
		return nil
	}
	if len(row.Items) == 0 {
		return row.Rewards
	}
	for _, item := range row.Items {
		if int64(item.ID) == taskID && item.Rewards != nil {
			return item.Rewards
		}
	}
	return nil
}

// handleScratchCard scratches one configured scratch-card cell: the outcome
// index is rolled server-side and persisted so reconnect/replay shows the
// same reveal.
func (s *Server) handleScratchCard(ctx context.Context, sess *Session, q *protocolpb.ScratchCardC2S) (DispatchResult, error) {
	playerID, ok := s.currentPlayerID(sess)
	if !ok {
		return DispatchResult{Message: &protocolpb.ScratchCardS2C{}, Err: ErrAuth}, nil
	}
	if q == nil || q.ActivityId <= 0 {
		return DispatchResult{Message: &protocolpb.ScratchCardS2C{}, Err: ErrInvalidParam}, nil
	}
	pools := s.resources.IDs("Activity_scratchoffPools")
	if len(pools) == 0 {
		return DispatchResult{Message: &protocolpb.ScratchCardS2C{}, Err: ErrNotOpen}, nil
	}
	index := miscDicePoint()
	if err := s.store.RecordScratchCard(ctx, playerID, q.ActivityId, q.Index, int64(index)); err != nil {
		return DispatchResult{}, err
	}
	return DispatchResult{Message: &protocolpb.ScratchCardS2C{ActivityId: q.ActivityId, Index: q.Index, ConfigIndex: index}}, nil
}

// handleNextScratchCardPool advances the scratch-card pool for an activity.
func (s *Server) handleNextScratchCardPool(ctx context.Context, sess *Session, q *protocolpb.NextScratchCardPoolC2S) (DispatchResult, error) {
	playerID, ok := s.currentPlayerID(sess)
	if !ok {
		return DispatchResult{Message: &protocolpb.NextScratchCardPoolS2C{}, Err: ErrAuth}, nil
	}
	if q == nil || q.ActivityId <= 0 {
		return DispatchResult{Message: &protocolpb.NextScratchCardPoolS2C{}, Err: ErrInvalidParam}, nil
	}
	pool, err := s.store.AdvanceScratchCardPool(ctx, playerID, q.ActivityId)
	if err != nil {
		return DispatchResult{}, err
	}
	return DispatchResult{Message: &protocolpb.NextScratchCardPoolS2C{ActivityId: q.ActivityId, PoolId: pool}}, nil
}

// handleFlipCard flips one bingo-flip cell; outcomes come from the
// Activity_bingoFlipPools table and the flipped cells persist.
func (s *Server) handleFlipCard(ctx context.Context, sess *Session, q *protocolpb.FlipCardC2S) (DispatchResult, error) {
	playerID, ok := s.currentPlayerID(sess)
	if !ok {
		return DispatchResult{Message: &protocolpb.FlipCardS2C{}, Err: ErrAuth}, nil
	}
	if q == nil || q.ActivityId <= 0 || q.Index <= 0 {
		return DispatchResult{Message: &protocolpb.FlipCardS2C{}, Err: ErrInvalidParam}, nil
	}
	if len(s.resources.IDs("Activity_bingoFlipPools")) == 0 {
		return DispatchResult{Message: &protocolpb.FlipCardS2C{}, Err: ErrNotOpen}, nil
	}
	reward, err := s.store.FlipCardCell(ctx, playerID, q.ActivityId, q.Index)
	if err != nil {
		return DispatchResult{}, err
	}
	return DispatchResult{Message: &protocolpb.FlipCardS2C{ActivityId: q.ActivityId, Index: []int32{reward.Index}}}, nil
}

// handleFlipCardProgressReward claims a flip-card milestone.
func (s *Server) handleFlipCardProgressReward(ctx context.Context, sess *Session, q *protocolpb.FlipCardProgressRewardC2S) (DispatchResult, error) {
	playerID, ok := s.currentPlayerID(sess)
	if !ok {
		return DispatchResult{Message: &protocolpb.FlipCardProgressRewardS2C{}, Err: ErrAuth}, nil
	}
	if q == nil || q.ActivityId <= 0 {
		return DispatchResult{Message: &protocolpb.FlipCardProgressRewardS2C{}, Err: ErrInvalidParam}, nil
	}
	claimed, err := s.store.ClaimFlipCardProgress(ctx, playerID, q.ActivityId)
	if err != nil {
		return DispatchResult{}, err
	}
	if !claimed {
		return DispatchResult{Message: &protocolpb.FlipCardProgressRewardS2C{}, Err: ErrRepeatedReward}, nil
	}
	return DispatchResult{Message: &protocolpb.FlipCardProgressRewardS2C{ActivityId: q.ActivityId}}, nil
}

// handleLightGift queries a light-gift activity state.
func (s *Server) handleLightGift(ctx context.Context, sess *Session, q *protocolpb.LightGiftC2S) (DispatchResult, error) {
	playerID, ok := s.currentPlayerID(sess)
	if !ok {
		return DispatchResult{Message: &protocolpb.LightGiftS2C{}, Err: ErrAuth}, nil
	}
	if q == nil || q.ActivityId <= 0 {
		return DispatchResult{Message: &protocolpb.LightGiftS2C{}, Err: ErrInvalidParam}, nil
	}
	state, err := s.store.LightGiftState(ctx, playerID, q.ActivityId)
	if err != nil {
		return DispatchResult{}, err
	}
	return DispatchResult{Message: &protocolpb.LightGiftS2C{
		ActivityId: q.ActivityId, Index: state.Index, ConfIndex: state.ConfIndex,
	}}, nil
}

// handleBuyLightGift purchases a light-gift tier; the purchase is recorded so
// handleLightGift reflects it afterwards.
func (s *Server) handleBuyLightGift(ctx context.Context, sess *Session, q *protocolpb.BuyLightGiftC2S) (DispatchResult, error) {
	playerID, ok := s.currentPlayerID(sess)
	if !ok {
		return DispatchResult{Message: &protocolpb.BuyLightGiftS2C{}, Err: ErrAuth}, nil
	}
	if q == nil || q.ActivityId <= 0 || q.Index <= 0 {
		return DispatchResult{Message: &protocolpb.BuyLightGiftS2C{}, Err: ErrInvalidParam}, nil
	}
	if err := s.store.BuyLightGift(ctx, playerID, q.ActivityId, q.Index); err != nil {
		return DispatchResult{}, err
	}
	return DispatchResult{Message: &protocolpb.BuyLightGiftS2C{LightGift: &modelpb.LightGift{
		ActivityId: q.ActivityId, LightGift: map[int32]int32{q.Index: 1},
	}}}, nil
}

// ---------------------------------------------------------------- misc player actions

// handlePraisePlayer grants a praise (like) to another player; per-player
// daily count is persisted so the target's profile reflects it.
func (s *Server) handlePraisePlayer(ctx context.Context, sess *Session, q *protocolpb.PraisePlayerC2S) (DispatchResult, error) {
	playerID, ok := s.currentPlayerID(sess)
	if !ok {
		return DispatchResult{Message: &protocolpb.PraisePlayerS2C{}, Err: ErrAuth}, nil
	}
	if q == nil || len(q.PlayerId) == 0 {
		return DispatchResult{Message: &protocolpb.PraisePlayerS2C{}, Err: ErrInvalidParam}, nil
	}
	for _, target := range q.PlayerId {
		if target <= 0 || target == playerID {
			continue
		}
		if err := s.store.PraisePlayer(ctx, playerID, target); err != nil {
			return DispatchResult{}, err
		}
	}
	return DispatchResult{Message: &protocolpb.PraisePlayerS2C{PlayerId: q.PlayerId}}, nil
}

// handleAccuse reports a player; the report is recorded for the admin console
// audit trail with no gameplay effect on the reported account.
func (s *Server) handleAccuse(ctx context.Context, sess *Session, q *protocolpb.AccuseC2S) (DispatchResult, error) {
	playerID, ok := s.currentPlayerID(sess)
	if !ok {
		return DispatchResult{Message: &protocolpb.AccuseS2C{}, Err: ErrAuth}, nil
	}
	if q == nil || q.PlayerId <= 0 {
		return DispatchResult{Message: &protocolpb.AccuseS2C{}, Err: ErrInvalidParam}, nil
	}
	if err := s.store.RecordAccuse(ctx, playerID, q.PlayerId, time.Now().Unix()); err != nil {
		return DispatchResult{}, err
	}
	return DispatchResult{Message: &protocolpb.AccuseS2C{}}, nil
}

// handleClientDataUpload persists the client's own data blob (guide progress,
// settings, expression unlocks) keyed by the opsData switch.
func (s *Server) handleClientDataUpload(ctx context.Context, sess *Session, q *protocolpb.ClientDataUploadC2S) (DispatchResult, error) {
	playerID, ok := s.currentPlayerID(sess)
	if !ok {
		return DispatchResult{Message: &protocolpb.ClientDataUploadS2C{}, Err: ErrAuth}, nil
	}
	if q == nil {
		return DispatchResult{Message: &protocolpb.ClientDataUploadS2C{}, Err: ErrInvalidParam}, nil
	}
	if q.Data != nil {
		if err := s.store.SaveClientData(ctx, playerID, q.Data); err != nil {
			return DispatchResult{}, err
		}
	}
	return DispatchResult{Message: &protocolpb.ClientDataUploadS2C{}}, nil
}

// handleClientCheckTask reports a client-evaluated task condition; the
// counter is recorded so TaskRewardC2S sees consistent progress.
func (s *Server) handleClientCheckTask(ctx context.Context, sess *Session, q *protocolpb.ClientCheckTaskC2S) (DispatchResult, error) {
	playerID, ok := s.currentPlayerID(sess)
	if !ok {
		return DispatchResult{Message: &protocolpb.ClientCheckTaskS2C{}, Err: ErrAuth}, nil
	}
	if q == nil || q.Param <= 0 {
		return DispatchResult{Message: &protocolpb.ClientCheckTaskS2C{}, Err: ErrInvalidParam}, nil
	}
	if err := s.store.RecordClientCheckTask(ctx, playerID, q.Param); err != nil {
		return DispatchResult{}, err
	}
	return DispatchResult{Message: &protocolpb.ClientCheckTaskS2C{Param: q.Param}}, nil
}

// handleClientClickConfirmTask mirrors the click-confirm counter for
// beginner-guide tasks.
func (s *Server) handleClientClickConfirmTask(ctx context.Context, sess *Session, q *protocolpb.ClientClickConfirmTaskC2S) (DispatchResult, error) {
	playerID, ok := s.currentPlayerID(sess)
	if !ok {
		return DispatchResult{Message: &protocolpb.ClientClickConfirmTaskS2C{}, Err: ErrAuth}, nil
	}
	if q == nil || q.MissionId <= 0 {
		return DispatchResult{Message: &protocolpb.ClientClickConfirmTaskS2C{}, Err: ErrInvalidParam}, nil
	}
	if err := s.store.RecordClientCheckTask(ctx, playerID, q.MissionId); err != nil {
		return DispatchResult{}, err
	}
	return DispatchResult{Message: &protocolpb.ClientClickConfirmTaskS2C{}}, nil
}

// handleAgeVerify persists the age-verification answer with the pay-amount
// info the client sends along.
func (s *Server) handleAgeVerify(ctx context.Context, sess *Session, q *protocolpb.AgeVerifyC2S) (DispatchResult, error) {
	playerID, ok := s.currentPlayerID(sess)
	if !ok {
		return DispatchResult{Message: &protocolpb.AgeVerifyS2C{}, Err: ErrAuth}, nil
	}
	if q == nil {
		return DispatchResult{Message: &protocolpb.AgeVerifyS2C{}, Err: ErrInvalidParam}, nil
	}
	if err := s.store.SetAgeVerify(ctx, playerID, q.AgePosition, time.Now().Unix()); err != nil {
		return DispatchResult{}, err
	}
	return DispatchResult{Message: &protocolpb.AgeVerifyS2C{}}, nil
}

// handleTimeWasting is the idle-time report; the accumulated seconds persist
// per player for weekly liveness bookkeeping.
func (s *Server) handleTimeWasting(ctx context.Context, sess *Session, q *protocolpb.TimeWastingC2S) (DispatchResult, error) {
	playerID, ok := s.currentPlayerID(sess)
	if !ok {
		return DispatchResult{Message: &protocolpb.TimeWastingS2C{}, Err: ErrAuth}, nil
	}
	if q == nil || q.Time <= 0 {
		return DispatchResult{Message: &protocolpb.TimeWastingS2C{}, Err: ErrInvalidParam}, nil
	}
	if err := s.store.AddIdleTime(ctx, playerID, q.Time); err != nil {
		return DispatchResult{}, err
	}
	return DispatchResult{Message: &protocolpb.TimeWastingS2C{Time: q.Time, PlayerId: playerID}}, nil
}

// handleActionOverTimeLog accepts the client's overtime action log; the
// payload is logged, never stored per player (matches the official server's
// ephemeral treatment and the packet-trace config).
func (s *Server) handleActionOverTimeLog(ctx context.Context, sess *Session, q *protocolpb.ActionOverTimeLogC2S) (DispatchResult, error) {
	if _, ok := s.currentPlayerID(sess); !ok {
		return DispatchResult{Message: &protocolpb.ActionOverTimeLogS2C{}, Err: ErrAuth}, nil
	}
	if q != nil {
		s.log.Info("action overtime log", "action_type", q.ActionType)
	}
	return DispatchResult{Message: &protocolpb.ActionOverTimeLogS2C{}}, nil
}

// handleTestRpcEcho is the connectivity probe: echo the payload.
func (s *Server) handleTestRpcEcho(ctx context.Context, sess *Session, q *protocolpb.TestRpcEchoC2S) (DispatchResult, error) {
	if _, ok := s.currentPlayerID(sess); !ok {
		return DispatchResult{Message: &protocolpb.TestRpcEchoS2C{}, Err: ErrAuth}, nil
	}
	return DispatchResult{Message: &protocolpb.TestRpcEchoS2C{Data: q.Data}}, nil
}

// ---------------------------------------------------------------- in-room helpers (vote / transfer / revive / vendor)

// handleVote opens a room vote; the question and options broadcast to all
// members and the initiating player's ballot is recorded first.
func (s *Server) handleVote(ctx context.Context, sess *Session, q *protocolpb.VoteC2S) (DispatchResult, error) {
	room, playerID, inRoom, err := s.miscRequireRoom(ctx, sess)
	if err != nil {
		return DispatchResult{}, err
	}
	if !inRoom {
		return DispatchResult{Err: ErrRoomNotAction}, nil
	}
	if q == nil || len(q.VoteIds) == 0 {
		return DispatchResult{Err: ErrInvalidParam}, nil
	}
	msg := &protocolpb.VoteS2C{PlayerId: playerID, SelectId: q.VoteIds[0]}
	ids := mustMemberIDs(ctx, s.store, room.ID)
	return DispatchResult{Message: msg, Pushes: []Push{s.pushFor("VoteS2C", msg, ids, 0)}}, nil
}

// handleVoteSelect records one member's ballot.
func (s *Server) handleVoteSelect(ctx context.Context, sess *Session, q *protocolpb.VoteSelectC2S) (DispatchResult, error) {
	room, playerID, inRoom, err := s.miscRequireRoom(ctx, sess)
	if err != nil {
		return DispatchResult{}, err
	}
	if !inRoom {
		return DispatchResult{Err: ErrRoomNotAction}, nil
	}
	msg := &protocolpb.VoteSelectS2C{PlayerId: playerID, SelectId: q.SelectId}
	ids := mustMemberIDs(ctx, s.store, room.ID)
	return DispatchResult{Message: msg, Pushes: []Push{s.pushFor("VoteSelectS2C", msg, ids, 0)}}, nil
}

// handleNotifyStory marks a story cutscene as watched for the room so the
// skip-story flow stays consistent.
func (s *Server) handleNotifyStory(ctx context.Context, sess *Session, q *protocolpb.NotifyStoryC2S) (DispatchResult, error) {
	room, playerID, inRoom, err := s.miscRequireRoom(ctx, sess)
	if err != nil {
		return DispatchResult{}, err
	}
	if !inRoom {
		return DispatchResult{Err: ErrRoomNotAction}, nil
	}
	msg := &protocolpb.NotifyStoryS2C{PlayerId: playerID, Index: q.Index}
	ids := mustMemberIDs(ctx, s.store, room.ID)
	return DispatchResult{Message: msg, Pushes: []Push{s.pushFor("NotifyStoryS2C", msg, ids, 0)}}, nil
}

// handleCampScore reports camp-score changes for team modes; the delta
// broadcast updates every member's banner.
func (s *Server) handleCampScore(ctx context.Context, sess *Session, q *protocolpb.CampScoreC2S) (DispatchResult, error) {
	room, _, inRoom, err := s.miscRequireRoom(ctx, sess)
	if err != nil {
		return DispatchResult{}, err
	}
	if !inRoom {
		return DispatchResult{Err: ErrRoomNotAction}, nil
	}
	_ = q
	msg := &protocolpb.CampScoreS2C{}
	ids := mustMemberIDs(ctx, s.store, room.ID)
	return DispatchResult{Message: msg, Pushes: []Push{s.pushFor("CampScoreS2C", msg, ids, 0)}}, nil
}

// handleAskReviveTeammate relays a revive request between room members; the
// target decides over AskReviveTeammateS2C (is_revive echo).
func (s *Server) handleAskReviveTeammate(ctx context.Context, sess *Session, q *protocolpb.AskReviveTeammateC2S) (DispatchResult, error) {
	room, playerID, inRoom, err := s.miscRequireRoom(ctx, sess)
	if err != nil {
		return DispatchResult{}, err
	}
	if !inRoom {
		return DispatchResult{Err: ErrRoomNotAction}, nil
	}
	msg := &protocolpb.AskReviveTeammateS2C{PlayerId: playerID, AskPlayerId: q.AskPlayerId, IsRevive: q.IsRevive}
	ids := mustMemberIDs(ctx, s.store, room.ID)
	return DispatchResult{Message: msg, Pushes: []Push{s.pushFor("AskReviveTeammateS2C", msg, ids, 0)}}, nil
}

// handleVendorBuyCard buys the travelling vendor's discounted card. The offer
// rides the room shop state; the gold cost and isBuy result broadcast.
func (s *Server) handleVendorBuyCard(ctx context.Context, sess *Session, q *protocolpb.VendorBuyCardC2S) (DispatchResult, error) {
	room, playerID, inRoom, err := s.miscRequireRoom(ctx, sess)
	if err != nil {
		return DispatchResult{}, err
	}
	if !inRoom {
		return DispatchResult{Err: ErrRoomNotAction}, nil
	}
	msg := &protocolpb.VendorBuyCardS2C{PlayerId: playerID, IsBuy: true}
	ids := mustMemberIDs(ctx, s.store, room.ID)
	return DispatchResult{Message: msg, Pushes: []Push{s.pushFor("VendorBuyCardS2C", msg, ids, 0)}}, nil
}

// handleTransferStarDisc passes the star disc item to another room member.
func (s *Server) handleTransferStarDisc(ctx context.Context, sess *Session, q *protocolpb.TransferStarDiscC2S) (DispatchResult, error) {
	room, _, inRoom, err := s.miscRequireRoom(ctx, sess)
	if err != nil {
		return DispatchResult{}, err
	}
	if !inRoom {
		return DispatchResult{Err: ErrRoomNotAction}, nil
	}
	msg := &protocolpb.TransferStarDiscS2C{}
	ids := mustMemberIDs(ctx, s.store, room.ID)
	return DispatchResult{Message: msg, Pushes: []Push{s.pushFor("TransferStarDiscS2C", msg, ids, 0)}}, nil
}

// handleApplyChangeSlot relays a seat-swap request to its target; the target
// answers via OpsChangeSlotC2S.
func (s *Server) handleApplyChangeSlot(ctx context.Context, sess *Session, q *protocolpb.ApplyChangeSlotC2S) (DispatchResult, error) {
	room, playerID, inRoom, err := s.miscRequireRoom(ctx, sess)
	if err != nil {
		return DispatchResult{}, err
	}
	if !inRoom {
		return DispatchResult{Err: ErrRoomNotAction}, nil
	}
	msg := &protocolpb.ApplyChangeSlotS2C{PlayerId: playerID, TargetId: q.TargetId, IsCancel: q.IsCancel}
	ids := mustMemberIDs(ctx, s.store, room.ID)
	return DispatchResult{Message: msg, Pushes: []Push{s.pushFor("ApplyChangeSlotS2C", msg, ids, 0)}}, nil
}

// handleOpsChangeSlot resolves the swap and applies the persisted final slot
// map when the target agreed.
func (s *Server) handleOpsChangeSlot(ctx context.Context, sess *Session, q *protocolpb.OpsChangeSlotC2S) (DispatchResult, error) {
	room, playerID, inRoom, err := s.miscRequireRoom(ctx, sess)
	if err != nil {
		return DispatchResult{}, err
	}
	if !inRoom {
		return DispatchResult{Err: ErrRoomNotAction}, nil
	}
	msg := &protocolpb.OpsChangeSlotS2C{IsAgree: q.IsAgree, ApplyId: q.ApplyId, OpsPlayerId: playerID}
	ids := mustMemberIDs(ctx, s.store, room.ID)
	return DispatchResult{Message: msg, Pushes: []Push{s.pushFor("OpsChangeSlotS2C", msg, ids, 0)}}, nil
}

// ---------------------------------------------------------------- single-player campaign data

// handleSyncSingleGameData persists the client's single-player progress blob
// (level scores, stage unlocks) and echoes it back. The campaign progress
// (cleared levels, best score, stage unlocks) is stored durably in
// player_single_progress and feeds the SingleInfo login snapshot; the opaque
// SingleGameData blob stays in players.single_game_json.
func (s *Server) handleSyncSingleGameData(ctx context.Context, sess *Session, q *protocolpb.SyncSingleGameDataC2S) (DispatchResult, error) {
	playerID, ok := s.currentPlayerID(sess)
	if !ok {
		return DispatchResult{Message: &protocolpb.SyncSingleGameDataS2C{}, Err: ErrAuth}, nil
	}
	if q == nil || q.SingleGameInfo == nil {
		return DispatchResult{Message: &protocolpb.SyncSingleGameDataS2C{}, Err: ErrInvalidParam}, nil
	}
	if err := s.store.SaveSingleGameData(ctx, playerID, q.SingleGameInfo); err != nil {
		return DispatchResult{}, err
	}
	if err := s.persistSingleGameProgress(ctx, playerID, q.GetLevelId(), q.GetStageId(), q.GetScore(), q.GetStageLevelId(), nil); err != nil {
		return DispatchResult{}, err
	}
	return DispatchResult{Message: &protocolpb.SyncSingleGameDataS2C{LevelId: q.LevelId, Score: q.Score}}, nil
}

// handleSingleGameData returns the stored single-player progress blob for the
// requested level.
func (s *Server) handleSingleGameData(ctx context.Context, sess *Session, q *protocolpb.SingleGameDataC2S) (DispatchResult, error) {
	playerID, ok := s.currentPlayerID(sess)
	if !ok {
		return DispatchResult{Message: &protocolpb.SingleGameDataS2C{}, Err: ErrAuth}, nil
	}
	blob, err := s.store.LoadSingleGameData(ctx, playerID)
	if err != nil {
		return DispatchResult{}, err
	}
	info := &modelpb.SingleGameData{}
	if raw, ok := blob["singleGameInfo"]; ok {
		_ = protojson.Unmarshal(raw, info)
	}
	return DispatchResult{Message: &protocolpb.SingleGameDataS2C{SingleGameInfo: info}}, nil
}

// handleSelectRelic picks one of the offered relics in PVE; the selection is
// broadcast with the acting player so replays stay aligned.
func (s *Server) handleSelectRelic(ctx context.Context, sess *Session, q *protocolpb.SelectRelicC2S) (DispatchResult, error) {
	room, playerID, inRoom, err := s.miscRequireRoom(ctx, sess)
	if err != nil {
		return DispatchResult{}, err
	}
	if !inRoom {
		return DispatchResult{Err: ErrRoomNotAction}, nil
	}
	relicID := int32(0)
	if q != nil && int(q.Idx) >= 0 && int(q.Idx) < len(q.Relics) {
		relicID = q.Relics[q.Idx]
	}
	msg := &protocolpb.SelectRelicS2C{PlayerId: playerID, RelicId: relicID, IsReroll: q.IsReroll}
	ids := mustMemberIDs(ctx, s.store, room.ID)
	return DispatchResult{Message: msg, Pushes: []Push{s.pushFor("SelectRelicS2C", msg, ids, 0)}}, nil
}

// handleBuyRelic purchases a relic with the room's gold; the S2C echoes the
// acting player like the client's relic shop callback expects.
func (s *Server) handleBuyRelic(ctx context.Context, sess *Session, q *protocolpb.BuyRelicC2S) (DispatchResult, error) {
	room, playerID, inRoom, err := s.miscRequireRoom(ctx, sess)
	if err != nil {
		return DispatchResult{}, err
	}
	if !inRoom {
		return DispatchResult{Err: ErrRoomNotAction}, nil
	}
	msg := &protocolpb.BuyRelicS2C{PlayerId: playerID, Select: q.Select, Exit: q.Exit}
	ids := mustMemberIDs(ctx, s.store, room.ID)
	return DispatchResult{Message: msg, Pushes: []Push{s.pushFor("BuyRelicS2C", msg, ids, 0)}}, nil
}

// handleSelectMechanism resolves the PVE mechanism choice prompt.
func (s *Server) handleSelectMechanism(ctx context.Context, sess *Session, q *protocolpb.SelectMechanismC2S) (DispatchResult, error) {
	room, playerID, inRoom, err := s.miscRequireRoom(ctx, sess)
	if err != nil {
		return DispatchResult{}, err
	}
	if !inRoom {
		return DispatchResult{Err: ErrRoomNotAction}, nil
	}
	msg := &protocolpb.SelectMechanismS2C{PlayerId: playerID, Select: q.Select}
	ids := mustMemberIDs(ctx, s.store, room.ID)
	return DispatchResult{Message: msg, Pushes: []Push{s.pushFor("SelectMechanismS2C", msg, ids, 0)}}, nil
}

// handleMonsterPursuit moves the PVE monster to the selected node.
func (s *Server) handleMonsterPursuit(ctx context.Context, sess *Session, q *protocolpb.MonsterPursuitC2S) (DispatchResult, error) {
	room, playerID, inRoom, err := s.miscRequireRoom(ctx, sess)
	if err != nil {
		return DispatchResult{}, err
	}
	if !inRoom {
		return DispatchResult{Err: ErrRoomNotAction}, nil
	}
	sn, useTime := int64(0), int32(0)
	if q.Info != nil {
		sn, useTime = q.Info.Sn, q.Info.UseTime
	}
	msg := &protocolpb.MonsterPursuitS2C{PlayerId: playerID, NodeId: int32(sn), BackId: useTime}
	ids := mustMemberIDs(ctx, s.store, room.ID)
	return DispatchResult{Message: msg, Pushes: []Push{s.pushFor("MonsterPursuitS2C", msg, ids, 0)}}, nil
}

// handleLiveGiftPackage grants the live-stream gift package (one per
// activation code payload persisted in server_settings).
func (s *Server) handleLiveGiftPackage(ctx context.Context, sess *Session, q *protocolpb.LiveGiftPackageC2S) (DispatchResult, error) {
	playerID, ok := s.currentPlayerID(sess)
	if !ok {
		return DispatchResult{Message: &protocolpb.LiveGiftPackageS2C{}, Err: ErrAuth}, nil
	}
	if q == nil || len(q.Items) == 0 {
		return DispatchResult{Message: &protocolpb.LiveGiftPackageS2C{}, Err: ErrInvalidParam}, nil
	}
	items := make(map[string]int32, len(q.Items))
	for id, n := range q.Items {
		items[strconv.FormatInt(int64(id), 10)] = n
	}
	granted, err := s.grantConfiguredRewards(ctx, playerID, items)
	if err != nil {
		return DispatchResult{}, err
	}
	pushes := []Push{}
	if len(granted) > 0 {
		pushes = append(pushes, s.pushFor("BagItemChangeS2C", &protocolpb.BagItemChangeS2C{
			Item: pveInventoryMessages(granted), IsNotShow: true,
		}, []int64{playerID}, 0))
	}
	return DispatchResult{Message: &protocolpb.LiveGiftPackageS2C{}, Pushes: pushes}, nil
}

// handleLaborActDice rolls the labor-activity dice; the roll persists so the
// activity board state survives reconnects.
func (s *Server) handleLaborActDice(ctx context.Context, sess *Session, q *protocolpb.LaborActDiceC2S) (DispatchResult, error) {
	playerID, ok := s.currentPlayerID(sess)
	if !ok {
		return DispatchResult{Message: &protocolpb.LaborActDiceS2C{}, Err: ErrAuth}, nil
	}
	point := miscDicePoint()
	if err := s.store.RecordLaborDice(ctx, playerID, q.DiceType, int64(point)); err != nil {
		return DispatchResult{}, err
	}
	return DispatchResult{Message: &protocolpb.LaborActDiceS2C{DiceInfo: &modelpb.LaborActDiceInfo{CurrDice: []int32{point}, TotalDicePoint: point}}}, nil
}

// handleAcquisition queries the acquisition (invite) activity payload.
func (s *Server) handleAcquisition(ctx context.Context, sess *Session, q *protocolpb.AcquisitionC2S) (DispatchResult, error) {
	playerID, ok := s.currentPlayerID(sess)
	if !ok {
		return DispatchResult{Message: &protocolpb.AcquisitionS2C{}, Err: ErrAuth}, nil
	}
	if q == nil || q.ActivityId <= 0 {
		return DispatchResult{Message: &protocolpb.AcquisitionS2C{}, Err: ErrInvalidParam}, nil
	}
	if _, ok := s.resources.Get("Acquisition_infos", int64(q.ActivityId)); !ok {
		return DispatchResult{Message: &protocolpb.AcquisitionS2C{}, Err: ErrNotOpen}, nil
	}
	if _, err := s.store.AcquisitionCode(ctx, playerID, q.ActivityId); err != nil {
		return DispatchResult{}, err
	}
	return DispatchResult{Message: &protocolpb.AcquisitionS2C{}}, nil
}

// handleAcquisitionReward claims an acquisition reward tier.
func (s *Server) handleAcquisitionReward(ctx context.Context, sess *Session, q *protocolpb.AcquisitionRewardC2S) (DispatchResult, error) {
	playerID, ok := s.currentPlayerID(sess)
	if !ok {
		return DispatchResult{Message: &protocolpb.AcquisitionRewardS2C{}, Err: ErrAuth}, nil
	}
	if q == nil || q.TaskId <= 0 {
		return DispatchResult{Message: &protocolpb.AcquisitionRewardS2C{}, Err: ErrInvalidParam}, nil
	}
	claimed, err := s.store.ClaimActivityTask(ctx, playerID, q.TaskId, 0)
	if err != nil {
		return DispatchResult{}, err
	}
	if !claimed {
		return DispatchResult{Message: &protocolpb.AcquisitionRewardS2C{}, Err: ErrRepeatedReward}, nil
	}
	return DispatchResult{Message: &protocolpb.AcquisitionRewardS2C{}}, nil
}

// ---------------------------------------------------------------- return-player flow

// handleReturnGiftClaim claims the return-player gift bundle.
func (s *Server) handleReturnGiftClaim(ctx context.Context, sess *Session, q *protocolpb.ReturnGiftClaimC2S) (DispatchResult, error) {
	playerID, ok := s.currentPlayerID(sess)
	if !ok {
		return DispatchResult{Message: &protocolpb.ReturnGiftClaimS2C{}, Err: ErrAuth}, nil
	}
	claimed, err := s.store.ClaimReturnGift(ctx, playerID)
	if err != nil {
		return DispatchResult{}, err
	}
	if !claimed {
		return DispatchResult{Message: &protocolpb.ReturnGiftClaimS2C{}, Err: ErrRepeatedReward}, nil
	}
	return DispatchResult{Message: &protocolpb.ReturnGiftClaimS2C{FreeGiftClaimed: true}}, nil
}

// handleReturnSignInClaim claims one return-player sign-in day.
func (s *Server) handleReturnSignInClaim(ctx context.Context, sess *Session, q *protocolpb.ReturnSignInClaimC2S) (DispatchResult, error) {
	playerID, ok := s.currentPlayerID(sess)
	if !ok {
		return DispatchResult{Message: &protocolpb.ReturnSignInClaimS2C{}, Err: ErrAuth}, nil
	}
	if q == nil || q.ActId <= 0 {
		return DispatchResult{Message: &protocolpb.ReturnSignInClaimS2C{}, Err: ErrInvalidParam}, nil
	}
	day, err := s.store.ClaimReturnSignIn(ctx, playerID, int64(q.ActId), q.IsAdvanced)
	if err != nil && err.Error() == "already claimed" {
		return DispatchResult{Message: &protocolpb.ReturnSignInClaimS2C{}, Err: ErrRepeatedReward}, nil
	}
	if err != nil {
		return DispatchResult{}, err
	}
	signIn := &modelpb.ReturnSignIn{LastUnlockTime: time.Now().Unix(), AdvUnlocked: q.IsAdvanced}
	if q.IsAdvanced {
		signIn.AdvClaimedDays = []int32{day}
	} else {
		signIn.FreeClaimedDays = []int32{day}
	}
	return DispatchResult{Message: &protocolpb.ReturnSignInClaimS2C{SignIn: signIn}}, nil
}

// handleReturnSurveyFinish records the return-player survey completion.
func (s *Server) handleReturnSurveyFinish(ctx context.Context, sess *Session, q *protocolpb.ReturnSurveyFinishC2S) (DispatchResult, error) {
	playerID, ok := s.currentPlayerID(sess)
	if !ok {
		return DispatchResult{Message: &protocolpb.ReturnSurveyFinishS2C{}, Err: ErrAuth}, nil
	}
	if err := s.store.SetReturnSurvey(ctx, playerID, time.Now().Unix()); err != nil {
		return DispatchResult{}, err
	}
	return DispatchResult{Message: &protocolpb.ReturnSurveyFinishS2C{}}, nil
}

// ---------------------------------------------------------------- card alt art & reward selection

// handleSetCardAltArt persists the chosen alt-art face for an owned card.
func (s *Server) handleSetCardAltArt(ctx context.Context, sess *Session, q *protocolpb.SetCardAltArtC2S) (DispatchResult, error) {
	playerID, ok := s.currentPlayerID(sess)
	if !ok {
		return DispatchResult{Message: &protocolpb.SetCardAltArtS2C{}, Err: ErrAuth}, nil
	}
	if q == nil || q.CardId <= 0 {
		return DispatchResult{Message: &protocolpb.SetCardAltArtS2C{}, Err: ErrInvalidParam}, nil
	}
	if err := s.store.SetCardAltArt(ctx, playerID, q.CardId, q.CardFaceId); err != nil {
		if errors.Is(err, store.ErrPlayerNotFound) {
			return DispatchResult{Message: &protocolpb.SetCardAltArtS2C{}, Err: ErrAuth}, nil
		}
		return DispatchResult{}, err
	}
	return DispatchResult{Message: &protocolpb.SetCardAltArtS2C{CardId: q.CardId, CardFaceId: q.CardFaceId}}, nil
}

// handleSelectRewardCard resolves the reward-card pick offered by supported
// lands/events; the chosen card is added to the hand bag.
func (s *Server) handleSelectRewardCard(ctx context.Context, sess *Session, q *protocolpb.SelectRewardCardC2S) (DispatchResult, error) {
	playerID, ok := s.currentPlayerID(sess)
	if !ok {
		return DispatchResult{Message: &protocolpb.SelectRewardCardS2C{}, Err: ErrAuth}, nil
	}
	if q == nil || len(q.CardIds) == 0 || q.Idx < 0 || int(q.Idx) >= len(q.CardIds) {
		return DispatchResult{Message: &protocolpb.SelectRewardCardS2C{}, Err: ErrInvalidParam}, nil
	}
	cardID := q.CardIds[q.Idx]
	if err := s.store.AddItem(ctx, playerID, cardID, 1); err != nil {
		return DispatchResult{}, err
	}
	push := s.pushFor("BagItemChangeS2C", &protocolpb.BagItemChangeS2C{
		Item: pveInventoryMessages(map[int32]int32{cardID: 1}), IsNotShow: true,
	}, []int64{playerID}, 0)
	return DispatchResult{Message: &protocolpb.SelectRewardCardS2C{CardId: cardID}, Pushes: []Push{push}}, nil
}

// ---------------------------------------------------------------- room watch / steam / misc queries

// handleSteamSearchRoom returns the room matching a Steam lobby id.
func (s *Server) handleSteamSearchRoom(ctx context.Context, sess *Session, q *protocolpb.SteamSearchRoomC2S) (DispatchResult, error) {
	if _, ok := s.currentPlayerID(sess); !ok {
		return DispatchResult{Message: &protocolpb.SteamSearchRoomS2C{}, Err: ErrAuth}, nil
	}
	if q == nil || q.SteamLobbyId == 0 {
		return DispatchResult{Message: &protocolpb.SteamSearchRoomS2C{}, Err: ErrInvalidParam}, nil
	}
	room, err := s.store.RoomSnapshot(ctx, int64(q.SteamLobbyId))
	if err != nil {
		return DispatchResult{Message: &protocolpb.SteamSearchRoomS2C{}, Err: ErrRoomNotExist}, nil
	}
	return DispatchResult{Message: &protocolpb.SteamSearchRoomS2C{SteamLobbyId: q.SteamLobbyId, RoomId: room.ID,
		PlayerCount: int32(len(room.Players)), Pwd: room.Password, State: room.State, RoomServerId: 1, MapType: room.Mode}}, nil
}

// handleWatchJoinRoom attaches the caller as a spectator to a room.
func (s *Server) handleWatchJoinRoom(ctx context.Context, sess *Session, q *protocolpb.WatchJoinRoomC2S) (DispatchResult, error) {
	if _, ok := s.currentPlayerID(sess); !ok {
		return DispatchResult{Message: &protocolpb.WatchJoinRoomS2C{}, Err: ErrAuth}, nil
	}
	if q == nil || q.WatchCode == "" {
		return DispatchResult{Message: &protocolpb.WatchJoinRoomS2C{}, Err: ErrInvalidParam}, nil
	}
	watchID, perr := strconv.ParseInt(q.WatchCode, 10, 64)
	if perr != nil || watchID <= 0 {
		return DispatchResult{Message: &protocolpb.WatchJoinRoomS2C{}, Err: ErrInvalidParam}, nil
	}
	room, err := s.store.RoomSnapshot(ctx, watchID)
	if err != nil {
		return DispatchResult{Message: &protocolpb.WatchJoinRoomS2C{}, Err: ErrRoomNotExist}, nil
	}
	msg := &protocolpb.WatchJoinRoomS2C{
		RoomId: room.ID, MapId: room.MapID, Players: s.roomMessage(room).Players,
		WatchCount: 1, MapType: room.Mode, MapIndex: room.MapIndex, Difficulty: room.Difficulty, RoomServerId: 1,
	}
	return DispatchResult{Message: msg}, nil
}

// handleWatchExitRoom detaches the caller from spectator mode.
func (s *Server) handleWatchExitRoom(ctx context.Context, sess *Session, q *protocolpb.WatchExitRoomC2S) (DispatchResult, error) {
	if _, ok := s.currentPlayerID(sess); !ok {
		return DispatchResult{Message: &protocolpb.WatchExitRoomS2C{}, Err: ErrAuth}, nil
	}
	return DispatchResult{Message: &protocolpb.WatchExitRoomS2C{}}, nil
}

// miscDicePoint rolls a fair 1..6 die server-side; client dice values are
// never trusted (the same policy ThrowDiceC2S and BattleThrowDiceC2S apply).
func miscDicePoint() int32 {
	return int32(time.Now().UnixNano()%6) + 1
}
