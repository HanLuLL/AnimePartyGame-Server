package gateway

import (
	"context"

	store "astralparty-server/internal/db"
	protocolpb "astralparty-server/internal/protocol/gen/protocol"
)

func (s *Server) handleGetShowPlayer(ctx context.Context, sess *Session, q *protocolpb.GetShowPlayerC2S) (DispatchResult, error) {
	_, _, selfID, _, loggedIn := sess.identity()
	if !loggedIn {
		return DispatchResult{Err: ErrAuth}, nil
	}
	targetID := q.GetPlayerId()
	if targetID == 0 {
		targetID = selfID
	}
	if targetID < 1 {
		return DispatchResult{Err: ErrInvalidParam}, nil
	}
	if _, err := s.store.LookupPlayer(ctx, targetID); err != nil {
		return DispatchResult{Err: 12001}, nil
	}
	profile, err := s.store.GetShowPlayerProfile(ctx, targetID)
	if err != nil {
		return DispatchResult{}, err
	}
	show := &protocolpb.ShowPlayer{
		PlayerId:         targetID,
		StandingPainting: profile.StandingPainting,
		AchieveId:        append([]int32(nil), profile.AchieveIDs...),
		IsShowData:       profile.IsShowData,
		IsShowFight:      profile.IsShowFight,
		Statistics:       &protocolpb.ShowPlayerStatistics{},
		Record:           []*protocolpb.ShowPlayerShortFight{},
		ReplayRecord:     []*protocolpb.ShowPlayerShortFight{},
	}
	return DispatchResult{Message: &protocolpb.GetShowPlayerS2C{ShowData: show}}, nil
}

func (s *Server) handleSetShowPlayer(ctx context.Context, sess *Session, q *protocolpb.SetShowPlayerC2S) (DispatchResult, error) {
	_, _, playerID, _, loggedIn := sess.identity()
	if !loggedIn {
		return DispatchResult{Err: ErrAuth}, nil
	}
	if q.GetShowData() == nil || q.GetShowData().GetStandingPainting() < 0 || len(q.GetShowData().GetAchieveId()) > 6 {
		return DispatchResult{Err: ErrInvalidParam}, nil
	}
	achievements := append([]int32(nil), q.GetShowData().GetAchieveId()...)
	for _, id := range achievements {
		if id < 0 {
			return DispatchResult{Err: ErrInvalidParam}, nil
		}
	}
	profile := store.ShowPlayerProfile{
		StandingPainting: q.GetShowData().GetStandingPainting(),
		AchieveIDs:       achievements,
		IsShowData:       q.GetShowData().GetIsShowData(),
		IsShowFight:      q.GetShowData().GetIsShowFight(),
	}
	if err := s.store.SetShowPlayerProfile(ctx, playerID, profile); err != nil {
		return DispatchResult{}, err
	}
	return DispatchResult{Message: &protocolpb.SetShowPlayerS2C{}}, nil
}
