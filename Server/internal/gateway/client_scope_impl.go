package gateway

import (
	"context"
	"encoding/json"
	"fmt"
	"time"

	modelpb "astralparty-server/internal/protocol/gen/model"
	protocolpb "astralparty-server/internal/protocol/gen/protocol"
)

// This file implements the remaining client-scope commands that previously
// fell through to the generic not-open response: the day-7 gift claim, the
// activity-pass mission reward, the return-campaign info query, the CN order
// creation used by the SDK pay flow, and the spectator room-state refresh.

// handleGetDay7Reward grants the next unclaimed day of a day-7 gift package.
// Claims are recorded through the shared activity-claims table with the goods
// ID as the info key and the day number as the task key, so a restart cannot
// hand out the same day twice. The refreshed Day7Reward state is echoed back.
func (s *Server) handleGetDay7Reward(ctx context.Context, sess *Session, q *protocolpb.GetDay7RewardC2S) (DispatchResult, error) {
	playerID, ok := s.currentPlayerID(sess)
	if !ok {
		return DispatchResult{Message: &protocolpb.GetDay7RewardS2C{}, Err: ErrAuth}, nil
	}
	if q == nil || q.GoodsId <= 0 {
		return DispatchResult{Message: &protocolpb.GetDay7RewardS2C{}, Err: ErrInvalidParam}, nil
	}
	raw, ok := s.resources.Get("Day7GiftPackage_goodss", int64(q.GoodsId))
	if !ok {
		return DispatchResult{Message: &protocolpb.GetDay7RewardS2C{}, Err: ErrInvalidParam}, nil
	}
	var row struct {
		Items []struct {
			DayNumb int32            `json:"dayNumb"`
			Reward  map[string]int32 `json:"reward"`
		} `json:"day7GiftPackageGoodsConfigureItems"`
	}
	if err := json.Unmarshal(raw, &row); err != nil || len(row.Items) == 0 {
		return DispatchResult{Message: &protocolpb.GetDay7RewardS2C{}, Err: ErrInvalidParam}, nil
	}
	now := time.Now().Unix()
	claimedAny := false
	granted := map[int32]int32{}
	pushes := []Push{}
	for _, item := range row.Items {
		if item.DayNumb <= 0 {
			continue
		}
		claimed, err := s.store.ClaimActivityTask(ctx, playerID, q.GoodsId, item.DayNumb)
		if err != nil {
			return DispatchResult{}, err
		}
		if !claimed {
			continue
		}
		claimedAny = true
		dayGranted, err := s.grantConfiguredRewards(ctx, playerID, item.Reward)
		if err != nil {
			return DispatchResult{}, err
		}
		for id, count := range dayGranted {
			granted[id] += count
		}
	}
	if len(granted) > 0 {
		pushes = append(pushes, s.pushFor("BagItemChangeS2C", &protocolpb.BagItemChangeS2C{
			Item: pveInventoryMessages(granted), IsNotShow: true,
		}, []int64{playerID}, 0))
	}
	_ = claimedAny
	day7 := s.snapImplDay7(now)
	state := day7[q.GoodsId]
	if state == nil {
		state = &modelpb.Day7Reward{GoodsId: q.GoodsId, CreateTime: now}
	}
	return DispatchResult{Message: &protocolpb.GetDay7RewardS2C{Day7: state}, Pushes: pushes}, nil
}

// handleGetActivityPassReward claims the free-track rewards for the requested
// missions of one activity pass. Claims reuse the activity-claims table with
// the pass definition ID as the info key and each mission ID as the task key.
func (s *Server) handleGetActivityPassReward(ctx context.Context, sess *Session, q *protocolpb.GetActivityPassRewardC2S) (DispatchResult, error) {
	playerID, ok := s.currentPlayerID(sess)
	if !ok {
		return DispatchResult{Message: &protocolpb.GetActivityPassRewardS2C{}, Err: ErrAuth}, nil
	}
	if q == nil || q.DefId <= 0 || len(q.MissionIds) == 0 {
		return DispatchResult{Message: &protocolpb.GetActivityPassRewardS2C{}, Err: ErrInvalidParam}, nil
	}
	if _, ok := s.resources.Get("Activity_infos", int64(q.DefId)); !ok {
		return DispatchResult{Message: &protocolpb.GetActivityPassRewardS2C{}, Err: ErrInvalidParam}, nil
	}
	claimed := make([]int32, 0, len(q.MissionIds))
	pushes := []Push{}
	for _, missionID := range q.MissionIds {
		if missionID <= 0 {
			continue
		}
		ok, err := s.store.ClaimActivityTask(ctx, playerID, q.DefId, missionID)
		if err != nil {
			return DispatchResult{}, err
		}
		if ok {
			claimed = append(claimed, missionID)
		}
	}
	if len(claimed) == 0 {
		return DispatchResult{Message: &protocolpb.GetActivityPassRewardS2C{}, Err: ErrRepeatedReward}, nil
	}
	return DispatchResult{Message: &protocolpb.GetActivityPassRewardS2C{
		MissionIds: claimed, DefId: q.DefId, Gear: 0,
	}, Pushes: pushes}, nil
}

// handleGetReturnInfo reports the comeback-campaign state. The private server
// runs no comeback campaign, so the same zeroed ReturnInfo used in the login
// snapshot is returned and the client treats the feature as inactive.
func (s *Server) handleGetReturnInfo(ctx context.Context, sess *Session, q *protocolpb.GetReturnInfoC2S) (DispatchResult, error) {
	if _, ok := s.currentPlayerID(sess); !ok {
		return DispatchResult{Message: &protocolpb.GetReturnInfoS2C{}, Err: ErrAuth}, nil
	}
	return DispatchResult{Message: &protocolpb.GetReturnInfoS2C{ReturnInfo: s.snapImplReturnInfo()}}, nil
}

// handleWatchRefreshRoomState returns the current room snapshot for a
// spectator. The client polls this from the watch panel to keep the view in
// sync with the running game.
func (s *Server) handleWatchRefreshRoomState(ctx context.Context, sess *Session, q *protocolpb.WatchRefreshRoomStateC2S) (DispatchResult, error) {
	if _, ok := s.currentPlayerID(sess); !ok {
		return DispatchResult{Message: &protocolpb.WatchRefreshRoomStateS2C{}, Err: ErrAuth}, nil
	}
	if q == nil || q.RoomId <= 0 {
		return DispatchResult{Message: &protocolpb.WatchRefreshRoomStateS2C{}, Err: ErrInvalidParam}, nil
	}
	room, err := s.store.RoomSnapshot(ctx, q.RoomId)
	if err != nil {
		return DispatchResult{Message: &protocolpb.WatchRefreshRoomStateS2C{}, Err: ErrRoomNotExist}, nil
	}
	return DispatchResult{Message: &protocolpb.WatchRefreshRoomStateS2C{
		RoomId: room.ID, Room: s.roomMessage(room),
	}}, nil
}

// handleChinaCreateOrder records a CN storefront order intent and answers with
// the fields the client's BnSdkLogic pay callback expects. The payment itself
// is settled by the external SDK; on this server the order stays pending and
// the client shows its normal error path when the SDK cannot complete it.
func (s *Server) handleChinaCreateOrder(ctx context.Context, sess *Session, q *protocolpb.ChinaCreateOrderC2S) (DispatchResult, error) {
	playerID, ok := s.currentPlayerID(sess)
	if !ok {
		return DispatchResult{Message: &protocolpb.ChinaCreateOrderS2C{}, Err: ErrAuth}, nil
	}
	if q == nil || q.ProductId <= 0 {
		return DispatchResult{Message: &protocolpb.ChinaCreateOrderS2C{}, Err: ErrInvalidParam}, nil
	}
	count := q.Quantity
	if count <= 0 {
		count = 1
	}
	orderID, err := s.store.CreateChargeOrder(ctx, playerID, q.ProductId, count, time.Now().Unix())
	if err != nil {
		return DispatchResult{}, err
	}
	amount := int32(0)
	if raw, ok := s.resources.Get("RechargeStore_goodss", int64(q.ProductId)); ok {
		var goods struct {
			Price int32 `json:"price"`
		}
		if json.Unmarshal(raw, &goods) == nil {
			amount = goods.Price * count
		}
	}
	return DispatchResult{Message: &protocolpb.ChinaCreateOrderS2C{
		GoodsId: q.ProductId, OrderId: fmt.Sprintf("%d", orderID), Amount: amount,
	}}, nil
}
