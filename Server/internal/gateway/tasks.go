package gateway

import (
	"context"
	"errors"
	"fmt"
	"time"

	store "astralparty-server/internal/db"
	modelpb "astralparty-server/internal/protocol/gen/model"
	protocolpb "astralparty-server/internal/protocol/gen/protocol"
)

func taskInfoMessage(state store.TaskSnapshot) *modelpb.TaskInfo {
	return &modelpb.TaskInfo{
		ProgressReward:   state.ProgressReward,
		IsTeaching:       state.Condition[1] > 0,
		WeekCondition:    state.WeekCondition,
		Condition:        state.Condition,
		TaskRewardIs:     state.TaskRewardIDs,
		AchieveRewardIs:  state.AchieveRewardIDs,
		WeekTaskRewardIs: state.WeekTaskRewardIDs,
		Condition1:       map[int32]*modelpb.ConditionData{},
	}
}

func (s *Server) taskConditionPushes(ctx context.Context, playerIDs []int64) []Push {
	seen := make(map[int64]struct{}, len(playerIDs))
	pushes := make([]Push, 0, len(playerIDs)*2)
	now := time.Now()
	for _, playerID := range playerIDs {
		if playerID <= 0 {
			continue
		}
		if _, exists := seen[playerID]; exists {
			continue
		}
		seen[playerID] = struct{}{}
		state, err := s.store.TaskSnapshot(ctx, playerID, now)
		if err != nil {
			s.log.Warn("cannot load task progress for push", "player_id", playerID, "err", err)
			continue
		}
		pushes = append(pushes,
			s.pushFor("TaskConditionS2C", &protocolpb.TaskConditionS2C{Type: 1, Cond: state.Condition}, []int64{playerID}, 0),
			s.pushFor("TaskConditionS2C", &protocolpb.TaskConditionS2C{Type: 2, Cond: state.WeekCondition}, []int64{playerID}, 0),
		)
	}
	return pushes
}

func (s *Server) handleTeaching(ctx context.Context, sess *Session) (DispatchResult, error) {
	playerID, ok := s.currentPlayerID(sess)
	if !ok {
		return DispatchResult{Message: &protocolpb.TeachingS2C{}, Err: ErrAuth}, nil
	}
	if err := s.store.MarkTutorialComplete(ctx, playerID, time.Now()); err != nil {
		return DispatchResult{}, fmt.Errorf("mark tutorial complete player=%d: %w", playerID, err)
	}
	push := s.pushFor("TaskConditionS2C", &protocolpb.TaskConditionS2C{
		Type: 1, Cond: map[int32]int32{1: 1},
	}, []int64{playerID}, 0)
	s.log.Info("tutorial completed", "player_id", playerID)
	return DispatchResult{Message: &protocolpb.TeachingS2C{}, Pushes: []Push{push}}, nil
}

func (s *Server) handleTaskReward(ctx context.Context, sess *Session, request *protocolpb.TaskRewardC2S) (DispatchResult, error) {
	if request == nil || request.DefId <= 0 || request.Type < 1 || request.Type > 5 {
		return DispatchResult{Message: &protocolpb.TaskRewardS2C{}, Err: ErrInvalidParam}, nil
	}
	playerID, ok := s.currentPlayerID(sess)
	if !ok {
		return DispatchResult{Message: &protocolpb.TaskRewardS2C{}, Err: ErrAuth}, nil
	}
	definition, found := s.resources.TaskReward(request.Type, request.DefId)
	if !found {
		return DispatchResult{Message: &protocolpb.TaskRewardS2C{}, Err: ErrNotOpen}, nil
	}
	now := time.Now()
	result, err := s.store.ClaimTaskReward(ctx, playerID, store.TaskRewardClaim{
		ID: definition.ID, Type: definition.Type, ConditionType: definition.ConditionType,
		Param: definition.Param, Day: definition.Day, Progress: definition.Progress,
		Reward: definition.Reward,
	}, store.TaskWeekPrefix(now), now)
	if errors.Is(err, store.ErrTaskAlreadyClaimed) {
		return DispatchResult{Message: &protocolpb.TaskRewardS2C{}, Err: ErrRepeatedReward}, nil
	}
	if errors.Is(err, store.ErrTaskNotReady) {
		return DispatchResult{Message: &protocolpb.TaskRewardS2C{}, Err: ErrNotOpen}, nil
	}
	if err != nil {
		return DispatchResult{}, fmt.Errorf("claim task reward player=%d type=%d id=%d: %w", playerID, request.Type, request.DefId, err)
	}
	pushes := []Push{}
	if len(result.Inventory) > 0 {
		pushes = append(pushes, s.pushFor("BagItemChangeS2C", &protocolpb.BagItemChangeS2C{
			Item: pveInventoryMessages(result.Inventory),
		}, []int64{playerID}, 0))
	}
	s.log.Info("task reward claimed", "player_id", playerID, "task_type", request.Type, "task_id", request.DefId)
	return DispatchResult{Message: &protocolpb.TaskRewardS2C{ProgressReward: result.ProgressReward}, Pushes: pushes}, nil
}
