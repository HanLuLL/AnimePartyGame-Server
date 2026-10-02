package gateway

import (
	"context"
	"encoding/json"
	"errors"
	"fmt"
	"time"

	store "astralparty-server/internal/db"
	modelpb "astralparty-server/internal/protocol/gen/model"
	protocolpb "astralparty-server/internal/protocol/gen/protocol"
)

// bpConfigRow is the BattlePass_infos row (id 202609 in the 3.2.0 dump).
type bpConfigRow struct {
	ID               int64            `json:"id"`
	BeginTime        json.RawMessage  `json:"beginTime"`
	EndTime          json.RawMessage  `json:"endTime"`
	TaskGroupID      int32            `json:"taskGroupID"`
	RewardGroupID    int32            `json:"rewardGroupID"`
	PremiumOfferLv   int32            `json:"premiumOfferLevels"`
	CostPerLv        map[string]int32 `json:"costPerLv"`
	ExpPerLv         int32            `json:"expPerLv"`
	RewardPerLvAfter map[string]int32 `json:"rewardPerLvAfterMax"`
}

// bpGoodsRow is the BattlePass_goodss row; the three goods IDs map onto the
// recharge goods table that the purchase flow (ChargeC2S) already resolves.
type bpGoodsRow struct {
	ID           int32 `json:"id"`
	NormalGoods  int32 `json:"normalGoods"`
	PremiumGoods int32 `json:"premiumGoods"`
	UpgradeGoods int32 `json:"upgradeGoods"`
}

// bpTaskItem is one task inside the BattlePass_tasks group.
type bpTaskItem struct {
	ID         int32 `json:"id"`
	Exp        int32 `json:"exp"`
	Param      int32 `json:"param"`
	Condition  int32 `json:"conditionType"`
	RefreshTyp int32 `json:"taskRefreshType"`
}

type bpTaskRow struct {
	ID    int64        `json:"id"`
	Items []bpTaskItem `json:"battlePassTaskConfigureItems"`
}

// bpRewardItem is one level inside the BattlePass_rewards group.
type bpRewardItem struct {
	Level          int32            `json:"level"`
	FreeRewards    map[string]int32 `json:"freeRewards"`
	NormalRewards  map[string]int32 `json:"normalRewards"`
	PremiumRewards map[string]int32 `json:"premiumRewards"`
}

type bpRewardRow struct {
	ID    int64          `json:"id"`
	Items []bpRewardItem `json:"battlePassRewardConfigureItems"`
}

// activeBPDef resolves the battle pass whose window covers now. The dump
// timestamps come through as {_unknown:{field_1:[unix]}}; when both bounds
// are missing the first row is treated as the running season. When no row is
// active the first configured row still resolves so the client never gets an
// empty season payload (dump: UIBattlePass opens Inf regardless of window).
func (s *Server) activeBPDef(now time.Time) (bpConfigRow, bool) {
	for _, rawID := range s.resources.IDs("BattlePass_infos") {
		raw, ok := s.resources.Get("BattlePass_infos", rawID)
		if !ok {
			continue
		}
		var row bpConfigRow
		if err := json.Unmarshal(raw, &row); err != nil {
			continue
		}
		begin, beginOK := bpTimestamp(row.BeginTime)
		end, endOK := bpTimestamp(row.EndTime)
		if beginOK && endOK {
			if now.Unix() >= begin && now.Unix() < end {
				return row, true
			}
			continue
		}
		return row, true
	}
	return bpConfigRow{}, false
}

// bpTimestamp decodes the {_unknown:{field_1:[unix]}} wrapper used by the
// imported config tables.
func bpTimestamp(raw json.RawMessage) (int64, bool) {
	var wrapper struct {
		Unknown *struct {
			Field1 []int64 `json:"field_1"`
		} `json:"_unknown"`
	}
	if err := json.Unmarshal(raw, &wrapper); err != nil {
		return 0, false
	}
	if wrapper.Unknown == nil || len(wrapper.Unknown.Field1) == 0 {
		return 0, false
	}
	return wrapper.Unknown.Field1[0], true
}

func (s *Server) bpGoods() (bpGoodsRow, bool) {
	ids := s.resources.IDs("BattlePass_goodss")
	for _, id := range ids {
		raw, ok := s.resources.Get("BattlePass_goodss", id)
		if !ok {
			continue
		}
		var row bpGoodsRow
		if err := json.Unmarshal(raw, &row); err == nil {
			return row, true
		}
	}
	return bpGoodsRow{}, false
}

func (s *Server) bpTaskGroup(groupID int32) (bpTaskRow, bool) {
	raw, ok := s.resources.Get("BattlePass_tasks", int64(groupID))
	if !ok {
		return bpTaskRow{}, false
	}
	var row bpTaskRow
	if err := json.Unmarshal(raw, &row); err != nil {
		return bpTaskRow{}, false
	}
	return row, true
}

func (s *Server) bpRewardGroup(groupID int32) (bpRewardRow, bool) {
	raw, ok := s.resources.Get("BattlePass_rewards", int64(groupID))
	if !ok {
		return bpRewardRow{}, false
	}
	var row bpRewardRow
	if err := json.Unmarshal(raw, &row); err != nil {
		return bpRewardRow{}, false
	}
	return row, true
}

// bpMaxLevel derives the highest configured reward level.
func (s *Server) bpMaxLevel(row bpConfigRow) int32 {
	rewards, ok := s.bpRewardGroup(row.RewardGroupID)
	if !ok || len(rewards.Items) == 0 {
		return 0
	}
	max := int32(0)
	for _, item := range rewards.Items {
		if item.Level > max {
			max = item.Level
		}
	}
	return max
}

// bpExpForTask grants pass experience for a completed task; the configured
// exp value is authoritative (dump: BattlePassTaskConfigureItem.exp).
func (s *Server) bpGrantTaskExp(ctx context.Context, playerID int64, row bpConfigRow, item bpTaskItem) (store.BPSnapshot, error) {
	expPerLv := row.ExpPerLv
	if expPerLv <= 0 {
		expPerLv = 500
	}
	snap, err := s.store.AddBPExp(ctx, playerID, row.ID, item.Exp, expPerLv, s.bpMaxLevel(row))
	if err != nil {
		return store.BPSnapshot{}, err
	}
	return snap, nil
}

// bpMessage maps the snapshot onto the wire BattlePass model.
func bpMessage(snap store.BPSnapshot) *modelpb.BattlePass {
	rewardIDs := snap.RewardIDs
	if rewardIDs == nil {
		rewardIDs = map[int32]int32{}
	}
	task := snap.Task
	if task == nil {
		task = map[int32]int32{}
	}
	claimed := make([]int32, 0, len(snap.TaskReward))
	for id, ok := range snap.TaskReward {
		if ok {
			claimed = append(claimed, id)
		}
	}
	return &modelpb.BattlePass{
		DefId: snap.DefID, Lv: snap.Lv, Exp: snap.Exp, Gear: snap.Gear,
		RewardIds: rewardIDs, Task: task, TaskRewardIs: claimed,
	}
}

// refreshBPPlayerMessage keeps the login snapshot consistent with the
// persisted pass state. messages.go hardcodes a level-1 placeholder; this
// rewrites it from the store whenever the season is configured.
func (s *Server) refreshBPPlayerMessage(ctx context.Context, playerID int64, player *modelpb.Player) {
	if player == nil || player.BattlePass == nil {
		return
	}
	row, ok := s.activeBPDef(time.Now())
	if !ok || row.ID == 0 {
		return
	}
	snap, err := s.store.LoadBattlePass(ctx, playerID, row.ID)
	if err != nil {
		return
	}
	player.BattlePass = bpMessage(snap)
}

// handleBattlePassGetReward claims every unclaimed reward bucket the player's
// current level and gear currently unlock. Granting walks the configured
// reward group; items land in the inventory and a BagItemChangeS2C push is
// returned (the client refreshes its bag on this push).
func (s *Server) handleBattlePassGetReward(ctx context.Context, sess *Session) (DispatchResult, error) {
	playerID, ok := s.currentPlayerID(sess)
	if !ok {
		return DispatchResult{Message: &protocolpb.BattlePassGetRewardS2C{}, Err: ErrAuth}, nil
	}
	row, ok := s.activeBPDef(time.Now())
	if !ok {
		return DispatchResult{Message: &protocolpb.BattlePassGetRewardS2C{}, Err: ErrNotOpen}, nil
	}
	snap, err := s.store.LoadBattlePass(ctx, playerID, row.ID)
	if err != nil {
		return DispatchResult{}, fmt.Errorf("load battle pass for reward claim player=%d: %w", playerID, err)
	}
	rewards, ok := s.bpRewardGroup(row.RewardGroupID)
	if !ok {
		return DispatchResult{Message: &protocolpb.BattlePassGetRewardS2C{}, Err: ErrNotOpen}, nil
	}
	granted := map[int32]int32{}
	for _, item := range rewards.Items {
		if item.Level > snap.Lv || item.Level <= 0 {
			continue
		}
		masks := []int32{}
		// bit 0 free, bit 1 normal gear, bit 2 premium gear
		masks = append(masks, 0)
		if snap.Gear >= 1 {
			masks = append(masks, 1)
		}
		if snap.Gear >= 2 {
			masks = append(masks, 2)
		}
		for _, mask := range masks {
			claimed, err := s.store.ClaimBPLevelReward(ctx, playerID, row.ID, int64(item.Level), int64(mask))
			if err != nil {
				return DispatchResult{}, fmt.Errorf("claim battle pass level reward player=%d level=%d: %w", playerID, item.Level, err)
			}
			if !claimed {
				continue
			}
			bucket := item.FreeRewards
			if mask == 1 {
				bucket = item.NormalRewards
			} else if mask == 2 {
				bucket = item.PremiumRewards
			}
			for rawItem, rawCount := range bucket {
				var itemID, count int32
				if err := json.Unmarshal([]byte(rawItem), &itemID); err != nil {
					continue
				}
				count = rawCount
				if count <= 0 {
					continue
				}
				if err := s.store.AddItem(ctx, playerID, itemID, count); err != nil {
					return DispatchResult{}, fmt.Errorf("grant battle pass reward item=%d player=%d: %w", itemID, playerID, err)
				}
				granted[itemID] += count
			}
		}
	}
	pushes := []Push{}
	if len(granted) > 0 {
		pushes = append(pushes, s.pushFor("BagItemChangeS2C", &protocolpb.BagItemChangeS2C{
			Item: pveInventoryMessages(granted), IsNotShow: true,
		}, []int64{playerID}, 0))
	}
	updated, err := s.store.LoadBattlePass(ctx, playerID, row.ID)
	if err != nil {
		return DispatchResult{}, err
	}
	pushes = append(pushes, s.bpSnapshotPushes(playerID, updated)...)
	return DispatchResult{Message: &protocolpb.BattlePassGetRewardS2C{}, Pushes: pushes}, nil
}

// handleBattlePassTaskReward claims the reward attached to one task ID. The
// client only offers the button once the task progress reached its configured
// param, but the server validates progress against the group config before
// granting the task experience and marking the claim.
func (s *Server) handleBattlePassTaskReward(ctx context.Context, sess *Session, q *protocolpb.BattlePassTaskRewardC2S) (DispatchResult, error) {
	playerID, ok := s.currentPlayerID(sess)
	if !ok {
		return DispatchResult{Message: &protocolpb.BattlePassTaskRewardS2C{}, Err: ErrAuth}, nil
	}
	if q == nil || q.DefId <= 0 {
		return DispatchResult{Message: &protocolpb.BattlePassTaskRewardS2C{}, Err: ErrInvalidParam}, nil
	}
	row, ok := s.activeBPDef(time.Now())
	if !ok {
		return DispatchResult{Message: &protocolpb.BattlePassTaskRewardS2C{}, Err: ErrNotOpen}, nil
	}
	group, ok := s.bpTaskGroup(row.TaskGroupID)
	if !ok {
		return DispatchResult{Message: &protocolpb.BattlePassTaskRewardS2C{}, Err: ErrNotOpen}, nil
	}
	var item *bpTaskItem
	for i := range group.Items {
		if group.Items[i].ID == q.DefId {
			item = &group.Items[i]
			break
		}
	}
	if item == nil {
		return DispatchResult{Message: &protocolpb.BattlePassTaskRewardS2C{}, Err: ErrInvalidParam}, nil
	}
	snap, err := s.store.LoadBattlePass(ctx, playerID, row.ID)
	if err != nil {
		return DispatchResult{}, err
	}
	if progress := snap.Task[q.DefId]; progress < item.Param {
		return DispatchResult{Message: &protocolpb.BattlePassTaskRewardS2C{}, Err: ErrRoomActionIncorrect}, nil
	}
	claimed, err := s.store.ClaimBPTaskReward(ctx, playerID, row.ID, int64(q.DefId))
	if err != nil {
		return DispatchResult{}, err
	}
	if !claimed {
		return DispatchResult{Message: &protocolpb.BattlePassTaskRewardS2C{}, Err: ErrRepeatedReward}, nil
	}
	updated, err := s.bpGrantTaskExp(ctx, playerID, row, *item)
	if err != nil {
		return DispatchResult{}, err
	}
	return DispatchResult{Message: &protocolpb.BattlePassTaskRewardS2C{},
		Pushes: s.bpSnapshotPushes(playerID, updated)}, nil
}

// handleBattlePassUpLv buys levels with the configured per-level cost item
// (BattlePass_infos.costPerLv, e.g. {2:60} = 60 of item 2 per level). The
// request count is the number of levels to buy (dump: UIBattlePass
// UpLvConfirm sends count). Consumes inventory atomically and grants the
// experience-equivalent so AddBPExp performs the promotion.
func (s *Server) handleBattlePassUpLv(ctx context.Context, sess *Session, q *protocolpb.BattlePassUpLvC2S) (DispatchResult, error) {
	playerID, ok := s.currentPlayerID(sess)
	if !ok {
		return DispatchResult{Message: &protocolpb.BattlePassUpLvS2C{}, Err: ErrAuth}, nil
	}
	if q == nil || q.Count <= 0 {
		return DispatchResult{Message: &protocolpb.BattlePassUpLvS2C{}, Err: ErrInvalidParam}, nil
	}
	row, ok := s.activeBPDef(time.Now())
	if !ok {
		return DispatchResult{Message: &protocolpb.BattlePassUpLvS2C{}, Err: ErrNotOpen}, nil
	}
	costItem, costPerLv, ok := bpCostPerLv(row)
	if !ok {
		return DispatchResult{Message: &protocolpb.BattlePassUpLvS2C{}, Err: ErrNotOpen}, nil
	}
	maxLv := s.bpMaxLevel(row)
	if maxLv <= 0 {
		return DispatchResult{Message: &protocolpb.BattlePassUpLvS2C{}, Err: ErrNotOpen}, nil
	}
	snap, err := s.store.LoadBattlePass(ctx, playerID, row.ID)
	if err != nil {
		return DispatchResult{}, err
	}
	buyable := maxLv - snap.Lv
	if q.Count > buyable {
		return DispatchResult{Message: &protocolpb.BattlePassUpLvS2C{}, Err: ErrInvalidParam}, nil
	}
	total := costPerLv * q.Count
	if total <= 0 || total < costPerLv { // overflow guard
		return DispatchResult{Message: &protocolpb.BattlePassUpLvS2C{}, Err: ErrInvalidParam}, nil
	}
	if err := s.store.ConsumeItem(ctx, playerID, costItem, total); err != nil {
		if errors.Is(err, store.ErrPlayerNotFound) {
			return DispatchResult{Message: &protocolpb.BattlePassUpLvS2C{}, Err: ErrAuth}, nil
		}
		return DispatchResult{Message: &protocolpb.BattlePassUpLvS2C{}, Err: ErrItemEnough}, nil
	}
	updated, err := s.store.AddBPExp(ctx, playerID, row.ID, row.ExpPerLv*q.Count, row.ExpPerLv, maxLv)
	if err != nil {
		return DispatchResult{}, err
	}
	pushes := []Push{s.pushFor("BagItemChangeS2C", &protocolpb.BagItemChangeS2C{
		Item: []*modelpb.ItemEtc{{ItemId: costItem, Count: -total}}, IsNotShow: true,
	}, []int64{playerID}, 0)}
	pushes = append(pushes, s.bpSnapshotPushes(playerID, updated)...)
	return DispatchResult{Message: &protocolpb.BattlePassUpLvS2C{}, Pushes: pushes}, nil
}

func bpCostPerLv(row bpConfigRow) (int32, int32, bool) {
	for rawKey, rawValue := range row.CostPerLv {
		var itemID int32
		if err := json.Unmarshal([]byte(rawKey), &itemID); err == nil && itemID > 0 && rawValue > 0 {
			return itemID, rawValue, true
		}
	}
	return 0, 0, false
}

// bpSnapshotPushes pushes the BattlePassInfoS2C snapshot plus the level and
// task deltas the client listens for (dump: 1065-1069 handlers).
func (s *Server) bpSnapshotPushes(playerID int64, snap store.BPSnapshot) []Push {
	targets := []int64{playerID}
	pushes := []Push{s.pushFor("BattlePassInfoS2C", &protocolpb.BattlePassInfoS2C{Inf: bpMessage(snap)}, targets, 0)}
	pushes = append(pushes, s.pushFor("BattlePassLvS2C", &protocolpb.BattlePassLvS2C{Lv: snap.Lv, Exp: snap.Exp}, targets, 0))
	pushes = append(pushes, s.pushFor("BattlePassTaskInfoS2C", &protocolpb.BattlePassTaskInfoS2C{
		Task: snap.Task, TaskRewardIs: taskRewardIDs(snap),
	}, targets, 0))
	pushes = append(pushes, s.pushFor("BattlePassBuyS2C", &protocolpb.BattlePassBuyS2C{Gear: snap.Gear}, targets, 0))
	return pushes
}

func taskRewardIDs(snap store.BPSnapshot) []int32 {
	ids := make([]int32, 0, len(snap.TaskReward))
	for id, ok := range snap.TaskReward {
		if ok {
			ids = append(ids, id)
		}
	}
	return ids
}
