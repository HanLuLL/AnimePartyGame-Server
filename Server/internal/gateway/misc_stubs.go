package gateway

import (
	"context"
	"fmt"
	"strconv"

	protocolpb "astralparty-server/internal/protocol/gen/protocol"
)

// defaultSurveyURL is returned by GetQuestionUrlC2S when the operator has not
// configured a survey endpoint. The client shows a "no questionnaire" tip on
// an empty URL, so the default points at the local notice feed host instead.
const defaultSurveyURL = ""

// handleMatchTeamInvite invites friends into the sender's current match team.
// It follows the room-invite flow (FriendInviteC2S): validate friendship,
// resolve the sender's team, and push MatchTeamInviteNotify to every invited
// player who is online; the recipient joins through JoinMatchTeamC2S.
func (s *Server) handleMatchTeamInvite(ctx context.Context, sess *Session, q *protocolpb.MatchTeamInviteC2S) (DispatchResult, error) {
	_, _, pid, _, ok := sess.identity()
	if !ok {
		return DispatchResult{Err: ErrAuth}, nil
	}
	if q == nil || len(q.PlayerIds) == 0 || len(q.PlayerIds) > 64 {
		return DispatchResult{Err: ErrInvalidParam}, nil
	}
	teamID, err := s.store.MatchTeamForPlayer(ctx, pid)
	if err != nil {
		return DispatchResult{}, err
	}
	if teamID <= 0 {
		return DispatchResult{Err: ErrInvalidParam}, nil
	}
	team, err := s.store.MatchTeamSnapshot(ctx, teamID)
	if err != nil {
		return DispatchResult{Err: ErrInvalidParam}, nil
	}
	if len(team.Players) >= 4 {
		return DispatchResult{Err: ErrRoomFull}, nil
	}
	inviter, err := s.store.LookupPlayer(ctx, pid)
	if err != nil {
		return DispatchResult{}, err
	}
	pushes := make([]Push, 0, len(q.PlayerIds))
	invited := 0
	for _, targetID := range q.PlayerIds {
		if targetID == pid {
			continue
		}
		friends, err := s.store.AreFriends(ctx, pid, targetID)
		if err != nil {
			return DispatchResult{}, err
		}
		if !friends {
			return DispatchResult{Err: ErrPlayerNotFriend}, nil
		}
		notify := &protocolpb.MatchTeamInviteNotify{
			PlayerId: pid, TeamId: teamID, Name: inviter.Nick,
			HeadIcon: inviter.Slot, Lv: inviter.Level,
		}
		pushes = append(pushes, s.pushFor("MatchTeamInviteNotify", notify, []int64{targetID}, 0))
		invited++
	}
	s.log.Info("match team invites sent", "team_id", teamID, "player_id", pid, "invited", invited)
	return DispatchResult{Message: &protocolpb.MatchTeamInviteS2C{}, Pushes: pushes}, nil
}

// handleGetQuestionUrl answers the survey-center URL query. The operator can
// configure per-activity survey URLs in server_settings with the key
// "survey_url_<activityId>"; an unconfigured id answers with an empty URL,
// which the client treats as "questionnaire unavailable".
func (s *Server) handleGetQuestionUrl(ctx context.Context, sess *Session, q *protocolpb.GetQuestionUrlC2S) (DispatchResult, error) {
	if _, ok := s.currentPlayerID(sess); !ok {
		return DispatchResult{Err: ErrAuth}, nil
	}
	if q == nil || q.ActivityId <= 0 {
		return DispatchResult{Err: ErrInvalidParam}, nil
	}
	url := defaultSurveyURL
	if value, ok, err := s.store.GetSetting(ctx, "survey_url_"+strconv.FormatInt(int64(q.ActivityId), 10)); err != nil {
		return DispatchResult{}, fmt.Errorf("load survey url activity=%d: %w", q.ActivityId, err)
	} else if ok {
		url = value
	}
	return DispatchResult{Message: &protocolpb.GetQuestionUrlS2C{Id: q.ActivityId, Url: url}}, nil
}
