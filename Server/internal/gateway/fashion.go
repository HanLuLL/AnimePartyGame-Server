package gateway

import (
	"context"

	protocolpb "astralparty-server/internal/protocol/gen/protocol"
)

const maxFashionPlans = 10

func (s *Server) handleSetFashion(ctx context.Context, sess *Session, request *protocolpb.SetFashionC2S) (DispatchResult, error) {
	_, playerID, ok := sessionPlayer(sess)
	if !ok {
		return DispatchResult{Err: ErrAuth}, nil
	}
	if request.Plan < 1 || request.Plan > maxFashionPlans || len(request.Fashion) != 6 {
		return DispatchResult{Err: ErrInvalidParam}, nil
	}

	defaults := s.resources.DefaultFashionPlan()
	itemTableAvailable := len(s.resources.IDs("Item_infos")) > 0
	for slot := int32(1); slot <= 6; slot++ {
		itemID, exists := request.Fashion[slot]
		if !exists || itemID < 0 || (slot != 6 && itemID == 0) {
			return DispatchResult{Err: ErrInvalidParam}, nil
		}
		if slot == 6 && itemID == 0 {
			continue
		}
		if itemTableAvailable {
			itemSlot, found := s.resources.FashionSlotForItem(itemID)
			if !found || itemSlot != slot {
				return DispatchResult{Err: ErrInvalidParam}, nil
			}
		}
		if defaults[slot] == itemID {
			continue
		}
		owned, err := s.store.HasInventoryItem(ctx, playerID, itemID)
		if err != nil {
			return DispatchResult{}, err
		}
		if !owned {
			return DispatchResult{Err: ErrItemEnough}, nil
		}
	}

	plans, usePlan, err := s.store.LoadFashionState(ctx, playerID)
	if err != nil {
		return DispatchResult{}, err
	}
	if len(plans) == 0 {
		plans = map[int32]map[int32]int32{1: defaults}
		usePlan = 1
	}
	if _, exists := plans[request.Plan]; !exists && len(plans) >= maxFashionPlans {
		return DispatchResult{Err: ErrInvalidParam}, nil
	}
	fashion := make(map[int32]int32, 6)
	for slot, itemID := range request.Fashion {
		fashion[slot] = itemID
	}
	plans[request.Plan] = fashion
	if usePlan < 1 || usePlan > maxFashionPlans {
		usePlan = 1
	}
	if _, exists := plans[usePlan]; !exists {
		usePlan = 1
	}
	if err = s.store.SaveFashionState(ctx, playerID, plans, usePlan); err != nil {
		return DispatchResult{}, err
	}
	return DispatchResult{Message: &protocolpb.SetFashionS2C{}}, nil
}

func (s *Server) handleSelectFashionPlan(ctx context.Context, sess *Session, request *protocolpb.SelectFashionPlanC2S) (DispatchResult, error) {
	_, playerID, ok := sessionPlayer(sess)
	if !ok {
		return DispatchResult{Err: ErrAuth}, nil
	}
	if request.Plan < 1 || request.Plan > maxFashionPlans {
		return DispatchResult{Err: ErrInvalidParam}, nil
	}
	plans, _, err := s.store.LoadFashionState(ctx, playerID)
	if err != nil {
		return DispatchResult{}, err
	}
	if len(plans) == 0 {
		if request.Plan != 1 {
			return DispatchResult{Err: ErrInvalidParam}, nil
		}
		plans = map[int32]map[int32]int32{1: s.resources.DefaultFashionPlan()}
	}
	if _, exists := plans[request.Plan]; !exists {
		return DispatchResult{Err: ErrInvalidParam}, nil
	}
	if err = s.store.SaveFashionState(ctx, playerID, plans, request.Plan); err != nil {
		return DispatchResult{}, err
	}
	return DispatchResult{Message: &protocolpb.SelectFashionPlanS2C{}}, nil
}
