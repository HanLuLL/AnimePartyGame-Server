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

func (s *Server) handleSignInReward(ctx context.Context, sess *Session, request *protocolpb.GetSignInRewardC2S) (DispatchResult, error) {
	if request == nil || request.ActivityId <= 0 {
		return DispatchResult{Message: &protocolpb.GetSignInRewardS2C{}, Err: ErrInvalidParam}, nil
	}
	playerID, ok := s.currentPlayerID(sess)
	if !ok {
		return DispatchResult{Message: &protocolpb.GetSignInRewardS2C{}, Err: ErrAuth}, nil
	}
	now := time.Now()
	activity, found := s.resources.SignInActivity(request.ActivityId, now)
	if !found {
		return DispatchResult{Message: &protocolpb.GetSignInRewardS2C{}, Err: ErrNotOpen}, nil
	}
	result, err := s.store.ClaimSignInReward(ctx, playerID, activity.ID, activity.Days, now)
	if errors.Is(err, store.ErrSignInNotReady) || errors.Is(err, store.ErrSignInComplete) {
		return DispatchResult{Message: &protocolpb.GetSignInRewardS2C{}, Err: ErrRepeatedReward}, nil
	}
	if err != nil {
		return DispatchResult{}, fmt.Errorf("claim sign-in reward player=%d activity=%d: %w", playerID, activity.ID, err)
	}
	pushes := []Push{}
	if len(result.Inventory) > 0 {
		pushes = append(pushes, s.pushFor("BagItemChangeS2C", &protocolpb.BagItemChangeS2C{
			Item: pveInventoryMessages(result.Inventory),
		}, []int64{playerID}, 0))
	}
	s.log.Info("sign-in reward claimed", "player_id", playerID, "activity_id", activity.ID,
		"sign_in_count", result.State.SignInCount)
	return DispatchResult{Message: &protocolpb.GetSignInRewardS2C{SignInReward: &modelpb.SignInReward{
		ActivityId: result.State.ActivityID, SignInCount: result.State.SignInCount, UpdateTime: result.State.UpdateTime,
	}}, Pushes: pushes}, nil
}
