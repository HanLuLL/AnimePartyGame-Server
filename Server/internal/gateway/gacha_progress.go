package gateway

import (
	"context"
	"encoding/json"
	"sort"

	store "astralparty-server/internal/db"
	protocolpb "astralparty-server/internal/protocol/gen/protocol"
)

func (s *Server) handleGachaCountReward(ctx context.Context, sess *Session, q *protocolpb.GachaCountRewardC2S) (DispatchResult, error) {
	pid, ok := s.currentPlayerID(sess)
	if !ok {
		return DispatchResult{Err: ErrAuth}, nil
	}
	if q.PoolId <= 0 {
		return DispatchResult{Err: ErrInvalidParam}, nil
	}
	gachaID, milestones, found := s.gachaPoolMilestones(q.PoolId)
	if !found {
		return DispatchResult{Err: ErrNotOpen}, nil
	}
	progress, inventory, _, err := s.store.ClaimGachaProgress(ctx, pid, gachaID, q.PoolId, milestones, false)
	if err != nil {
		return DispatchResult{}, err
	}
	pushes := gachaInventoryPush(s, pid, inventory, false)
	return DispatchResult{Message: &protocolpb.GachaCountRewardS2C{PoolId: q.PoolId, RewardCount: progress.RewardCount}, Pushes: pushes}, nil
}

func (s *Server) handleRookieGachaReward(ctx context.Context, sess *Session, q *protocolpb.RookieGachaRewardC2S) (DispatchResult, error) {
	pid, ok := s.currentPlayerID(sess)
	if !ok {
		return DispatchResult{Err: ErrAuth}, nil
	}
	if q.PoolId <= 0 || q.DefId != 9 || q.ItemId <= 0 {
		return DispatchResult{Err: ErrInvalidParam}, nil
	}
	gachaID, milestones, found := s.gachaPoolMilestones(q.PoolId)
	if !found || gachaID != q.DefId || len(milestones) == 0 {
		return DispatchResult{Err: ErrNotOpen}, nil
	}
	milestone := milestones[0]
	var selected store.GachaDrop
	for _, reward := range milestone.Rewards {
		if reward.ItemID == q.ItemId {
			selected = reward
			break
		}
	}
	if selected.ItemID == 0 {
		return DispatchResult{Err: ErrInvalidParam}, nil
	}
	progress, inventory, _, err := s.store.ClaimGachaProgress(ctx, pid, gachaID, q.PoolId,
		[]store.GachaMilestone{{Count: milestone.Count, Rewards: []store.GachaDrop{selected}}}, true)
	if err == store.ErrGachaRewardLocked {
		return DispatchResult{Err: ErrInvalidParam}, nil
	}
	if err != nil {
		return DispatchResult{}, err
	}
	pushes := gachaInventoryPush(s, pid, inventory, false)
	return DispatchResult{Message: &protocolpb.RookieGachaRewardS2C{PoolId: q.PoolId, DefId: q.DefId, RewardCount: progress.RewardCount}, Pushes: pushes}, nil
}

func (s *Server) gachaPoolMilestones(poolID int32) (int32, []store.GachaMilestone, bool) {
	var gachaID int32
	for _, raw := range s.resources.Rows("Gacha_backstages") {
		var backstage map[string]json.RawMessage
		if json.Unmarshal(raw, &backstage) != nil || jsonInt(backstage, "poolID", "poolId") != poolID {
			continue
		}
		gachaID = jsonInt(backstage, "gachaType")
		break
	}
	if gachaID <= 0 {
		return 0, nil, false
	}
	raw, found := s.resources.Get("Gacha_progresss", int64(poolID))
	if !found {
		return 0, nil, false
	}
	var row map[string]json.RawMessage
	if json.Unmarshal(raw, &row) != nil || jsonInt(row, "poolID", "poolId") != poolID {
		return 0, nil, false
	}
	items := jsonItems(row["gachaProgressConfigureItems"])
	milestones := make([]store.GachaMilestone, 0, len(items))
	for _, item := range items {
		var rewardsByID map[int32]int32
		if json.Unmarshal(item["reward"], &rewardsByID) != nil || len(rewardsByID) == 0 {
			continue
		}
		itemIDs := make([]int, 0, len(rewardsByID))
		for itemID := range rewardsByID {
			itemIDs = append(itemIDs, int(itemID))
		}
		sort.Ints(itemIDs)
		rewards := make([]store.GachaDrop, 0, len(itemIDs))
		for _, rawID := range itemIDs {
			itemID := int32(rawID)
			rewards = append(rewards, store.GachaDrop{ItemID: itemID, Count: rewardsByID[itemID]})
		}
		milestones = append(milestones, store.GachaMilestone{Count: jsonInt(item, "count"), Rewards: rewards})
	}
	if len(milestones) == 0 {
		return 0, nil, false
	}
	sort.SliceStable(milestones, func(i, j int) bool { return milestones[i].Count < milestones[j].Count })
	return gachaID, milestones, true
}

func gachaInventoryPush(s *Server, playerID int64, inventory map[int32]int32, isNotShow bool) []Push {
	if len(inventory) == 0 {
		return nil
	}
	return []Push{s.pushFor("BagItemChangeS2C", &protocolpb.BagItemChangeS2C{
		Item: pveInventoryMessages(inventory), IsNotShow: isNotShow,
	}, []int64{playerID}, 0)}
}
