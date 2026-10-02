package gateway

import (
	"context"
	"errors"
	"fmt"
	"time"
	"unicode/utf8"

	store "astralparty-server/internal/db"
	modelpb "astralparty-server/internal/protocol/gen/model"
	protocolpb "astralparty-server/internal/protocol/gen/protocol"
)

// Guild protocol error codes (errcode/code.proto GuildServerErr block).
const (
	ErrGuildServer            int16 = 17000
	ErrGuildFull              int16 = 17002
	ErrGuildNameExists        int16 = 17003
	ErrGuildAlreadyJoin       int16 = 17004
	ErrGuildSearchCdIng       int16 = 17005
	ErrGuildBanned            int16 = 17006
	ErrGuildNotExist          int16 = 17007
	ErrGuildAuthErr           int16 = 17009
	ErrGuildApplicationExpir  int16 = 17010
	ErrGuildPlayerNotExist    int16 = 17011
	ErrGuildAlreadyInvited    int16 = 17012
	ErrGuildInvitationExpired int16 = 17013
	ErrGuildUpdateLimit       int16 = 17014
	ErrGuildPlayerNotInGuild  int16 = 17015
	ErrGuildMuted             int16 = 17016
	ErrGuildCreateBanned      int16 = 17017
	ErrGuildFrozen            int16 = 17018
	ErrGuildChatCdIng         int16 = 17019
)

const (
	guildNameMaxRunes   = 12
	guildSearchCooldown = 10 * time.Second
	guildChatCooldown   = time.Second
	guildPageSize       = 10
)

// guildMemberMessage converts a stored membership row into the protocol's
// GuildMember entry. Online status is resolved from the live session hub.
func (s *Server) guildMemberMessage(m store.GuildMemberData) *modelpb.GuildMember {
	return &modelpb.GuildMember{
		PlayerId: m.PlayerID, Role: m.Role, WeeklyActivity: m.WeeklyActivity,
		LastWeeklyActivity: m.LastWeeklyActivity, LastLoginTime: m.LastLoginTime,
		WeeklySign: m.WeeklySign, PlayerName: m.Nick, Online: len(s.hub.sessions(m.PlayerID)) > 0,
	}
}

// guildMessage converts a stored guild row into the protocol's Guild message.
// WithMembers also embeds members and pending applications.
func (s *Server) guildMessage(ctx context.Context, g store.GuildData, withMembers bool) (*modelpb.Guild, error) {
	count, err := s.store.GuildMemberCount(ctx, g.ID)
	if err != nil {
		return nil, err
	}
	out := &modelpb.Guild{
		Id: g.ID, Name: g.Name, TagIds: g.TagIDs, Status: g.Status, MemberCount: count,
		CreateTime: g.CreatedAt, UpdateTime: g.UpdatedAt, MuteEndTime: 0,
		ExAnnouncement: g.ExAnnouncement, LastExternalEditTime: g.LastExternalEditTime,
		LastExternalEditorId: g.LastExternalEditorID, ExternalEditCount: g.ExternalEditCount,
		InAnnouncement: g.InAnnouncement, LastInternalEditTime: g.LastInternalEditTime,
		LastInternalEditorId: g.LastInternalEditorID, InternalEditCount: g.InternalEditCount,
	}
	if withMembers {
		members, err := s.store.ListGuildMembers(ctx, g.ID)
		if err != nil {
			return nil, err
		}
		out.Members = make(map[int64]*modelpb.GuildMember, len(members))
		for _, m := range members {
			out.Members[m.PlayerID] = s.guildMemberMessage(m)
		}
		apps, err := s.store.ListGuildApplications(ctx, g.ID)
		if err != nil {
			return nil, err
		}
		out.Applications = make(map[int64]*modelpb.GuildApplication, len(apps))
		for _, a := range apps {
			out.Applications[a.PlayerID] = &modelpb.GuildApplication{PlayerId: a.PlayerID, ApplyTime: a.ApplyTime}
		}
		if msgs, err := s.store.ListGuildChangeMsg(ctx, g.ID, 0); err == nil && len(msgs) > 0 {
			out.LastChangeMsgId = msgs[len(msgs)-1].ID
		}
		if chats, err := s.store.ListGuildChatMsg(ctx, g.ID, 0, 1); err == nil && len(chats) > 0 {
			out.LastChatTime = chats[len(chats)-1].Time
		}
	}
	return out, nil
}

// guildPlayerShow builds the FriendShowPlayerInfo entries used by the guild
// member list panel.
func (s *Server) guildPlayerShow(m store.GuildMemberData) *protocolpb.FriendShowPlayerInfo {
	return &protocolpb.FriendShowPlayerInfo{
		PlayerId: m.PlayerID, Name: m.Nick, Lv: m.Level,
		IsOnline: len(s.hub.sessions(m.PlayerID)) > 0,
	}
}

// guildContext resolves the caller's identity and membership. ok=false means
// the response should carry an auth error; guildID 0 with ok=true means the
// player has no guild.
func (s *Server) guildContext(ctx context.Context, sess *Session) (playerID, guildID int64, errCode int16, ok bool) {
	_, _, playerID, _, logged := sess.identity()
	if !logged {
		return 0, 0, ErrAuth, false
	}
	id, err := s.store.PlayerGuildID(ctx, playerID)
	if err != nil {
		s.log.Error("guild membership lookup failed", "player_id", playerID, "err", err)
		return 0, 0, ErrGuildServer, false
	}
	return playerID, id, ErrSucc, true
}

// requireGuildRole checks membership plus a minimum role (1=master, 2=officer).
func (s *Server) requireGuildRole(ctx context.Context, guildID, playerID int64, minRole int32) (store.GuildMemberData, int16, bool) {
	member, err := s.store.GuildMemberRow(ctx, guildID, playerID)
	if err != nil {
		return store.GuildMemberData{}, ErrGuildPlayerNotInGuild, false
	}
	if member.Role > minRole {
		return store.GuildMemberData{}, ErrGuildAuthErr, false
	}
	return member, ErrSucc, true
}

// insertGuildChange records a member-change notification and returns the push
// to broadcast to guild members.
func (s *Server) insertGuildChange(ctx context.Context, guildID int64, notificationID int32, params []string) (Push, error) {
	msg, err := s.store.InsertGuildChangeMsg(ctx, guildID, notificationID, params)
	if err != nil {
		return Push{}, err
	}
	ids, err := s.store.GuildMemberIDs(ctx, guildID)
	if err != nil {
		return Push{}, err
	}
	return s.pushFor("GetGuildMemberChangeMsgS2C", &protocolpb.GetGuildMemberChangeMsgS2C{
		Messages: []*modelpb.GuildMemberChangeMsg{{
			Id: msg.ID, NotificationId: msg.NotificationID, Param: msg.Params, Time: msg.Time,
		}},
	}, ids, 0), nil
}

func (s *Server) handleCreateGuild(ctx context.Context, sess *Session, q *protocolpb.CreateGuildC2S) (DispatchResult, error) {
	playerID, guildID, errCode, ok := s.guildContext(ctx, sess)
	if !ok {
		return DispatchResult{Err: errCode}, nil
	}
	if guildID != 0 {
		return DispatchResult{Err: ErrGuildAlreadyJoin}, nil
	}
	name := q.GetName()
	if name == "" || utf8.RuneCountInString(name) > guildNameMaxRunes {
		return DispatchResult{Err: ErrInvalidParam}, nil
	}
	g, err := s.store.CreateGuild(ctx, name, q.GetTagIds(), q.GetExAnnouncement(), playerID)
	switch {
	case errors.Is(err, store.ErrGuildNameTaken):
		return DispatchResult{Err: ErrGuildNameExists}, nil
	case err != nil:
		return DispatchResult{}, fmt.Errorf("create guild player=%d name=%q: %w", playerID, name, err)
	}
	message, err := s.guildMessage(ctx, g, true)
	if err != nil {
		return DispatchResult{}, fmt.Errorf("create guild message player=%d: %w", playerID, err)
	}
	playerGuild := &modelpb.PlayerGuildInfo{GuildId: g.ID, LastJoinTime: time.Now().Unix()}
	s.log.Info("guild created", "guild_id", g.ID, "master_id", playerID)
	return DispatchResult{Message: &protocolpb.CreateGuildS2C{Guild: message, PlayerGuild: playerGuild}}, nil
}

func (s *Server) handleSearchGuild(ctx context.Context, sess *Session, q *protocolpb.SearchGuildC2S) (DispatchResult, error) {
	_, _, errCode, ok := s.guildContext(ctx, sess)
	if !ok {
		return DispatchResult{Err: errCode}, nil
	}
	guilds, isEnd, err := s.store.SearchGuilds(ctx, q.GetId(), q.GetName(), q.GetTagIds(), q.GetPage(), guildPageSize)
	if err != nil {
		return DispatchResult{}, fmt.Errorf("search guilds: %w", err)
	}
	out := &protocolpb.SearchGuildS2C{IsEnd: isEnd, Guilds: []*modelpb.Guild{}}
	for _, g := range guilds {
		message, err := s.guildMessage(ctx, g, false)
		if err != nil {
			return DispatchResult{}, fmt.Errorf("search guilds message guild=%d: %w", g.ID, err)
		}
		out.Guilds = append(out.Guilds, message)
	}
	return DispatchResult{Message: out}, nil
}

func (s *Server) handleApplyToGuild(ctx context.Context, sess *Session, q *protocolpb.ApplyToGuildC2S) (DispatchResult, error) {
	playerID, guildID, errCode, ok := s.guildContext(ctx, sess)
	if !ok {
		return DispatchResult{Err: errCode}, nil
	}
	if guildID != 0 {
		return DispatchResult{Err: ErrGuildAlreadyJoin}, nil
	}
	target := q.GetGuildId()
	if _, err := s.store.GuildByID(ctx, target); err != nil {
		return DispatchResult{Err: ErrGuildNotExist}, nil
	}
	count, err := s.store.GuildMemberCount(ctx, target)
	if err != nil {
		return DispatchResult{}, fmt.Errorf("apply guild count guild=%d: %w", target, err)
	}
	if count >= store.GuildMemberCap {
		return DispatchResult{Err: ErrGuildFull}, nil
	}
	if err = s.store.AddGuildApplication(ctx, target, playerID); err != nil {
		if errors.Is(err, store.ErrGuildAlreadyApply) {
			return DispatchResult{Err: ErrGuildApplicationExpir}, nil
		}
		return DispatchResult{}, fmt.Errorf("apply to guild player=%d guild=%d: %w", playerID, target, err)
	}
	// Notify online officers/master so they can process the application.
	apps := &modelpb.PlayerGuildInfo{Applications: map[int64]int64{target: time.Now().Unix()}, ApplyCount: 1}
	return DispatchResult{Message: &protocolpb.ApplyToGuildS2C{PlayerGuild: apps}}, nil
}

func (s *Server) handleProcessGuildApplication(ctx context.Context, sess *Session, q *protocolpb.ProcessGuildApplicationC2S) (DispatchResult, error) {
	playerID, guildID, errCode, ok := s.guildContext(ctx, sess)
	if !ok {
		return DispatchResult{Err: errCode}, nil
	}
	if guildID == 0 {
		return DispatchResult{Err: ErrGuildPlayerNotInGuild}, nil
	}
	if _, errCode, ok := s.requireGuildRole(ctx, guildID, playerID, store.GuildRoleOfficer); !ok {
		return DispatchResult{Err: errCode}, nil
	}
	accept := q.GetAction() == protocolpb.ProcessGuildApplicationC2S_Accept
	for _, applicantID := range q.GetPlayerIds() {
		if err := s.store.DeleteGuildApplication(ctx, guildID, applicantID); err != nil {
			return DispatchResult{}, fmt.Errorf("process application guild=%d applicant=%d: %w", guildID, applicantID, err)
		}
		if !accept {
			continue
		}
		if err := s.store.JoinGuild(ctx, guildID, applicantID); err != nil {
			if errors.Is(err, store.ErrGuildFull) {
				return DispatchResult{Err: ErrGuildFull}, nil
			}
			return DispatchResult{}, fmt.Errorf("accept application guild=%d applicant=%d: %w", guildID, applicantID, err)
		}
		// Push the new member list to everyone including the joiner, plus a
		// join sync to the joiner and a change notification to the guild.
		gName, err := s.store.GuildNameByID(ctx, guildID)
		if err != nil {
			return DispatchResult{}, err
		}
		ids, _ := s.store.GuildMemberIDs(ctx, guildID)
		pushes := []Push{
			s.pushFor("SyncGuildS2C", &protocolpb.SyncGuildS2C{}, ids, 0),
			s.pushFor("SyncPlayerJoinGuildS2C", &protocolpb.SyncPlayerJoinGuildS2C{
				PlayerGuild: &modelpb.PlayerGuildInfo{GuildId: guildID, LastJoinTime: time.Now().Unix()},
				GuildName:   gName,
			}, []int64{applicantID}, 0),
		}
		if push, err := s.insertGuildChange(ctx, guildID, 1, []string{q.GetPlayerName()}); err == nil {
			pushes = append(pushes, push)
		}
		return DispatchResult{Message: &protocolpb.ProcessGuildApplicationS2C{}, Pushes: pushes}, nil
	}
	return DispatchResult{Message: &protocolpb.ProcessGuildApplicationS2C{}}, nil
}

func (s *Server) handleSendGuildInvitation(ctx context.Context, sess *Session, q *protocolpb.SendGuildInvitationC2S) (DispatchResult, error) {
	playerID, guildID, errCode, ok := s.guildContext(ctx, sess)
	if !ok {
		return DispatchResult{Err: errCode}, nil
	}
	if guildID == 0 {
		return DispatchResult{Err: ErrGuildPlayerNotInGuild}, nil
	}
	if _, errCode, ok := s.requireGuildRole(ctx, guildID, playerID, store.GuildRoleOfficer); !ok {
		return DispatchResult{Err: errCode}, nil
	}
	target := q.GetPlayerId()
	if _, err := s.store.LookupPlayer(ctx, target); err != nil {
		return DispatchResult{Err: ErrGuildPlayerNotExist}, nil
	}
	if targetGuild, err := s.store.PlayerGuildID(ctx, target); err == nil && targetGuild != 0 {
		return DispatchResult{Err: ErrGuildAlreadyJoin}, nil
	}
	if err := s.store.AddGuildInvitation(ctx, guildID, target, playerID); err != nil {
		if errors.Is(err, store.ErrGuildAlreadyInvite) {
			return DispatchResult{Err: ErrGuildAlreadyInvited}, nil
		}
		return DispatchResult{}, fmt.Errorf("invite player=%d to guild=%d: %w", target, guildID, err)
	}
	push := s.pushFor("SyncPlayerGuildS2C", &protocolpb.SyncPlayerGuildS2C{
		PlayerGuild: &modelpb.PlayerGuildInfo{
			Invitations:         map[int64]int64{guildID: time.Now().Unix()},
			ReceivedInvitations: map[int64]*modelpb.GuildInvitation{guildID: {GuildId: guildID, InviterId: playerID, InvitationTime: time.Now().Unix()}},
		},
	}, []int64{target}, 0)
	return DispatchResult{Message: &protocolpb.SendGuildInvitationS2C{}, Pushes: []Push{push}}, nil
}

func (s *Server) handleProcessGuildInvitation(ctx context.Context, sess *Session, q *protocolpb.ProcessGuildInvitationC2S) (DispatchResult, error) {
	playerID, guildID, errCode, ok := s.guildContext(ctx, sess)
	if !ok {
		return DispatchResult{Err: errCode}, nil
	}
	if guildID != 0 {
		return DispatchResult{Err: ErrGuildAlreadyJoin}, nil
	}
	accept := q.GetAction() == protocolpb.ProcessGuildInvitationC2S_Accept
	for _, invited := range q.GetGuildIds() {
		if err := s.store.DeleteGuildInvitation(ctx, invited, playerID); err != nil {
			return DispatchResult{}, fmt.Errorf("process invitation player=%d guild=%d: %w", playerID, invited, err)
		}
		if !accept {
			continue
		}
		if _, err := s.store.GuildByID(ctx, invited); err != nil {
			return DispatchResult{Err: ErrGuildNotExist}, nil
		}
		if err := s.store.JoinGuild(ctx, invited, playerID); err != nil {
			if errors.Is(err, store.ErrGuildFull) {
				return DispatchResult{Err: ErrGuildFull}, nil
			}
			return DispatchResult{}, fmt.Errorf("accept invitation player=%d guild=%d: %w", playerID, invited, err)
		}
		g, err := s.store.GuildByID(ctx, invited)
		if err != nil {
			return DispatchResult{}, err
		}
		message, err := s.guildMessage(ctx, g, true)
		if err != nil {
			return DispatchResult{}, err
		}
		playerGuild := &modelpb.PlayerGuildInfo{GuildId: invited, LastJoinTime: time.Now().Unix()}
		ids, _ := s.store.GuildMemberIDs(ctx, invited)
		pushes := []Push{
			s.pushFor("SyncPlayerJoinGuildS2C", &protocolpb.SyncPlayerJoinGuildS2C{PlayerGuild: playerGuild, GuildName: g.Name}, []int64{playerID}, 0),
			s.pushFor("SyncGuildS2C", &protocolpb.SyncGuildS2C{ExAnnouncement: message.ExAnnouncement, InAnnouncement: message.InAnnouncement}, ids, 0),
		}
		if push, err := s.insertGuildChange(ctx, invited, 1, []string{fmt.Sprintf("%d", playerID)}); err == nil {
			pushes = append(pushes, push)
		}
		return DispatchResult{Message: &protocolpb.ProcessGuildInvitationS2C{Guild: message, PlayerGuild: playerGuild}, Pushes: pushes}, nil
	}
	return DispatchResult{Message: &protocolpb.ProcessGuildInvitationS2C{}}, nil
}

func (s *Server) handleGetGuildInfo(ctx context.Context, sess *Session, q *protocolpb.GetGuildInfoC2S) (DispatchResult, error) {
	_, guildID, errCode, ok := s.guildContext(ctx, sess)
	if !ok {
		return DispatchResult{Err: errCode}, nil
	}
	if guildID == 0 {
		return DispatchResult{Err: ErrGuildPlayerNotInGuild}, nil
	}
	g, err := s.store.GuildByID(ctx, guildID)
	if err != nil {
		return DispatchResult{Err: ErrGuildNotExist}, nil
	}
	message, err := s.guildMessage(ctx, g, true)
	if err != nil {
		return DispatchResult{}, fmt.Errorf("guild info message guild=%d: %w", guildID, err)
	}
	return DispatchResult{Message: &protocolpb.GetGuildInfoS2C{Guild: message}}, nil
}

func (s *Server) handleUpdateGuildSettings(ctx context.Context, sess *Session, q *protocolpb.UpdateGuildSettingsC2S) (DispatchResult, error) {
	playerID, guildID, errCode, ok := s.guildContext(ctx, sess)
	if !ok {
		return DispatchResult{Err: errCode}, nil
	}
	if guildID == 0 {
		return DispatchResult{Err: ErrGuildPlayerNotInGuild}, nil
	}
	if _, errCode, ok := s.requireGuildRole(ctx, guildID, playerID, store.GuildRoleMaster); !ok {
		return DispatchResult{Err: errCode}, nil
	}
	if err := s.store.UpdateGuildExternal(ctx, guildID, q.GetTagIds(), q.GetExAnnouncement(), playerID); err != nil {
		return DispatchResult{}, fmt.Errorf("update guild settings guild=%d: %w", guildID, err)
	}
	ids, _ := s.store.GuildMemberIDs(ctx, guildID)
	push := s.pushFor("SyncGuildS2C", &protocolpb.SyncGuildS2C{
		TagIds: q.GetTagIds(), ExAnnouncement: q.GetExAnnouncement(),
		LastExternalEditTime: time.Now().Unix(), LastExternalEditorId: playerID,
	}, ids, 0)
	return DispatchResult{Message: &protocolpb.UpdateGuildSettingsS2C{}, Pushes: []Push{push}}, nil
}

func (s *Server) handleUpdateGuildInAnnouncement(ctx context.Context, sess *Session, q *protocolpb.UpdateGuildInAnnouncementC2S) (DispatchResult, error) {
	playerID, guildID, errCode, ok := s.guildContext(ctx, sess)
	if !ok {
		return DispatchResult{Err: errCode}, nil
	}
	if guildID == 0 {
		return DispatchResult{Err: ErrGuildPlayerNotInGuild}, nil
	}
	if _, errCode, ok := s.requireGuildRole(ctx, guildID, playerID, store.GuildRoleOfficer); !ok {
		return DispatchResult{Err: errCode}, nil
	}
	if err := s.store.UpdateGuildInternal(ctx, guildID, q.GetInAnnouncement(), playerID); err != nil {
		return DispatchResult{}, fmt.Errorf("update guild announcement guild=%d: %w", guildID, err)
	}
	ids, _ := s.store.GuildMemberIDs(ctx, guildID)
	push := s.pushFor("SyncGuildS2C", &protocolpb.SyncGuildS2C{
		InAnnouncement: q.GetInAnnouncement(), LastInternalEditTime: time.Now().Unix(), LastInternalEditorId: playerID,
	}, ids, 0)
	return DispatchResult{Message: &protocolpb.UpdateGuildInAnnouncementS2C{}, Pushes: []Push{push}}, nil
}

func (s *Server) handleTransferGuildMaster(ctx context.Context, sess *Session, q *protocolpb.TransferGuildMasterC2S) (DispatchResult, error) {
	playerID, guildID, errCode, ok := s.guildContext(ctx, sess)
	if !ok {
		return DispatchResult{Err: errCode}, nil
	}
	if guildID == 0 {
		return DispatchResult{Err: ErrGuildPlayerNotInGuild}, nil
	}
	if _, errCode, ok := s.requireGuildRole(ctx, guildID, playerID, store.GuildRoleMaster); !ok {
		return DispatchResult{Err: errCode}, nil
	}
	target := q.GetTargetPlayerId()
	if target == playerID {
		return DispatchResult{Err: ErrInvalidParam}, nil
	}
	if err := s.store.UpdateGuildMemberRole(ctx, guildID, playerID, store.GuildRoleMember); err != nil {
		return DispatchResult{}, fmt.Errorf("transfer master demote guild=%d: %w", guildID, err)
	}
	if err := s.store.UpdateGuildMemberRole(ctx, guildID, target, store.GuildRoleMaster); err != nil {
		// Roll the demotion back so the guild is not left masterless.
		_ = s.store.UpdateGuildMemberRole(ctx, guildID, playerID, store.GuildRoleMaster)
		return DispatchResult{Err: ErrGuildPlayerNotExist}, nil
	}
	if _, err := s.store.DB().ExecContext(ctx, `UPDATE guilds SET master_id=?,updated_at=? WHERE id=?`, target, time.Now().Unix(), guildID); err != nil {
		return DispatchResult{}, fmt.Errorf("transfer master guild=%d: %w", guildID, err)
	}
	members, _ := s.store.ListGuildMembers(ctx, guildID)
	memberMap := make(map[int64]*modelpb.GuildMember, len(members))
	for _, m := range members {
		memberMap[m.PlayerID] = s.guildMemberMessage(m)
	}
	ids, _ := s.store.GuildMemberIDs(ctx, guildID)
	push := s.pushFor("SyncGuildMemberS2C", &protocolpb.SyncGuildMemberS2C{Members: memberMap}, ids, 0)
	return DispatchResult{Message: &protocolpb.TransferGuildMasterS2C{}, Pushes: []Push{push}}, nil
}

func (s *Server) handleChangeGuildMemberTitle(ctx context.Context, sess *Session, q *protocolpb.ChangeGuildMemberTitleC2S) (DispatchResult, error) {
	playerID, guildID, errCode, ok := s.guildContext(ctx, sess)
	if !ok {
		return DispatchResult{Err: errCode}, nil
	}
	if guildID == 0 {
		return DispatchResult{Err: ErrGuildPlayerNotInGuild}, nil
	}
	if _, errCode, ok := s.requireGuildRole(ctx, guildID, playerID, store.GuildRoleMaster); !ok {
		return DispatchResult{Err: errCode}, nil
	}
	role := q.GetRole()
	if role != store.GuildRoleOfficer && role != store.GuildRoleMember {
		return DispatchResult{Err: ErrInvalidParam}, nil
	}
	target := q.GetTargetPlayerId()
	if target == playerID {
		return DispatchResult{Err: ErrInvalidParam}, nil
	}
	if err := s.store.UpdateGuildMemberRole(ctx, guildID, target, role); err != nil {
		return DispatchResult{Err: ErrGuildPlayerNotExist}, nil
	}
	members, _ := s.store.ListGuildMembers(ctx, guildID)
	memberMap := make(map[int64]*modelpb.GuildMember, len(members))
	for _, m := range members {
		memberMap[m.PlayerID] = s.guildMemberMessage(m)
	}
	ids, _ := s.store.GuildMemberIDs(ctx, guildID)
	push := s.pushFor("SyncGuildMemberS2C", &protocolpb.SyncGuildMemberS2C{Members: memberMap}, ids, 0)
	return DispatchResult{Message: &protocolpb.ChangeGuildMemberTitleS2C{}, Pushes: []Push{push}}, nil
}

func (s *Server) handleKickGuildMember(ctx context.Context, sess *Session, q *protocolpb.KickGuildMemberC2S) (DispatchResult, error) {
	playerID, guildID, errCode, ok := s.guildContext(ctx, sess)
	if !ok {
		return DispatchResult{Err: errCode}, nil
	}
	if guildID == 0 {
		return DispatchResult{Err: ErrGuildPlayerNotInGuild}, nil
	}
	actor, errCode, ok := s.requireGuildRole(ctx, guildID, playerID, store.GuildRoleOfficer)
	if !ok {
		return DispatchResult{Err: errCode}, nil
	}
	target := q.GetTargetPlayerId()
	if target == playerID {
		return DispatchResult{Err: ErrInvalidParam}, nil
	}
	victim, err := s.store.GuildMemberRow(ctx, guildID, target)
	if err != nil {
		return DispatchResult{Err: ErrGuildPlayerNotExist}, nil
	}
	// Officers may only kick plain members; only the master may kick officers.
	if victim.Role <= actor.Role {
		return DispatchResult{Err: ErrGuildAuthErr}, nil
	}
	if err := s.store.LeaveGuild(ctx, guildID, target); err != nil {
		return DispatchResult{Err: ErrGuildPlayerNotExist}, nil
	}
	ids, _ := s.store.GuildMemberIDs(ctx, guildID)
	pushes := []Push{
		s.pushFor("SyncGuildMemberExitS2C", &protocolpb.SyncGuildMemberExitS2C{PlayerIds: []int64{target}}, ids, 0),
		s.pushFor("SyncPlayerGuildS2C", &protocolpb.SyncPlayerGuildS2C{PlayerGuild: &modelpb.PlayerGuildInfo{}}, []int64{target}, 0),
	}
	if push, err := s.insertGuildChange(ctx, guildID, 3, []string{victim.Nick}); err == nil {
		pushes = append(pushes, push)
	}
	return DispatchResult{Message: &protocolpb.KickGuildMemberS2C{}, Pushes: pushes}, nil
}

func (s *Server) handleImpeachGuildMaster(ctx context.Context, sess *Session) (DispatchResult, error) {
	_, guildID, errCode, ok := s.guildContext(ctx, sess)
	if !ok {
		return DispatchResult{Err: errCode}, nil
	}
	if guildID == 0 {
		return DispatchResult{Err: ErrGuildPlayerNotInGuild}, nil
	}
	// Impeachment requires master inactivity tracking that the server does not
	// model yet; reject with the dedicated auth error instead of guessing.
	return DispatchResult{Err: ErrGuildAuthErr}, nil
}

func (s *Server) handleExitGuild(ctx context.Context, sess *Session, q *protocolpb.ExitGuildC2S) (DispatchResult, error) {
	playerID, guildID, errCode, ok := s.guildContext(ctx, sess)
	if !ok {
		return DispatchResult{Err: errCode}, nil
	}
	if guildID == 0 {
		return DispatchResult{Err: ErrGuildPlayerNotInGuild}, nil
	}
	member, err := s.store.GuildMemberRow(ctx, guildID, playerID)
	if err != nil {
		return DispatchResult{Err: ErrGuildPlayerNotInGuild}, nil
	}
	if member.Role == store.GuildRoleMaster {
		// The master must transfer leadership or disband first.
		return DispatchResult{Err: ErrGuildAuthErr}, nil
	}
	if err := s.store.LeaveGuild(ctx, guildID, playerID); err != nil {
		return DispatchResult{Err: ErrGuildPlayerNotInGuild}, nil
	}
	ids, _ := s.store.GuildMemberIDs(ctx, guildID)
	pushes := []Push{
		s.pushFor("SyncGuildMemberExitS2C", &protocolpb.SyncGuildMemberExitS2C{PlayerIds: []int64{playerID}}, ids, 0),
		s.pushFor("SyncPlayerGuildS2C", &protocolpb.SyncPlayerGuildS2C{PlayerGuild: &modelpb.PlayerGuildInfo{}}, []int64{playerID}, 0),
	}
	if push, err := s.insertGuildChange(ctx, guildID, 2, []string{member.Nick}); err == nil {
		pushes = append(pushes, push)
	}
	return DispatchResult{Message: &protocolpb.ExitGuildS2C{}, Pushes: pushes}, nil
}

func (s *Server) handleDisbandGuild(ctx context.Context, sess *Session, q *protocolpb.DisbandGuildC2S) (DispatchResult, error) {
	playerID, guildID, errCode, ok := s.guildContext(ctx, sess)
	if !ok {
		return DispatchResult{Err: errCode}, nil
	}
	if guildID == 0 {
		return DispatchResult{Err: ErrGuildPlayerNotInGuild}, nil
	}
	if _, errCode, ok := s.requireGuildRole(ctx, guildID, playerID, store.GuildRoleMaster); !ok {
		return DispatchResult{Err: errCode}, nil
	}
	ids, _ := s.store.GuildMemberIDs(ctx, guildID)
	if err := s.store.DisbandGuild(ctx, guildID); err != nil {
		return DispatchResult{}, fmt.Errorf("disband guild=%d: %w", guildID, err)
	}
	push := s.pushFor("SyncPlayerGuildS2C", &protocolpb.SyncPlayerGuildS2C{PlayerGuild: &modelpb.PlayerGuildInfo{}}, ids, 0)
	s.log.Info("guild disbanded", "guild_id", guildID, "master_id", playerID)
	return DispatchResult{Message: &protocolpb.DisbandGuildS2C{}, Pushes: []Push{push}}, nil
}

func (s *Server) handleGuildMissionReward(ctx context.Context, sess *Session, q *protocolpb.GuildMissionRewardC2S) (DispatchResult, error) {
	playerID, guildID, errCode, ok := s.guildContext(ctx, sess)
	if !ok {
		return DispatchResult{Err: errCode}, nil
	}
	if guildID == 0 {
		return DispatchResult{Err: ErrGuildPlayerNotInGuild}, nil
	}
	// Guild mission progress is not driven by any gameplay hook yet, so no
	// reward can legitimately be claimable; the claimed table still records
	// the request durably for future migration.
	taskID := q.GetTaskId()
	if taskID <= 0 {
		return DispatchResult{Err: ErrInvalidParam}, nil
	}
	if _, claimed, err := s.store.GuildMissionClaimState(ctx, guildID, playerID, taskID); err != nil {
		return DispatchResult{}, fmt.Errorf("guild mission state guild=%d task=%d: %w", guildID, taskID, err)
	} else if claimed {
		return DispatchResult{Err: ErrRepeatedReward}, nil
	}
	if err := s.store.GuildMissionClaim(ctx, guildID, playerID, taskID, 0); err != nil {
		return DispatchResult{}, fmt.Errorf("guild mission claim guild=%d task=%d: %w", guildID, taskID, err)
	}
	return DispatchResult{Message: &protocolpb.GuildMissionRewardS2C{TaskId: taskID}}, nil
}

func (s *Server) handleSendGuildChatMsg(ctx context.Context, sess *Session, q *protocolpb.SendGuildChatMsgC2S) (DispatchResult, error) {
	playerID, guildID, errCode, ok := s.guildContext(ctx, sess)
	if !ok {
		return DispatchResult{Err: errCode}, nil
	}
	if guildID == 0 {
		return DispatchResult{Err: ErrGuildPlayerNotInGuild}, nil
	}
	message, valid := normalizeRoomChat(q.GetMsg())
	if !valid {
		return DispatchResult{Err: ErrInvalidParam}, nil
	}
	if !s.allowChat(playerID) {
		return DispatchResult{Err: ErrGuildChatCdIng}, nil
	}
	msg, err := s.store.InsertGuildChatMsg(ctx, guildID, playerID, message)
	if err != nil {
		return DispatchResult{}, fmt.Errorf("guild chat insert guild=%d sender=%d: %w", guildID, playerID, err)
	}
	ids, _ := s.store.GuildMemberIDs(ctx, guildID)
	push := s.pushFor("GuildChatMsgS2C", &protocolpb.GuildChatMsgS2C{Msg: &modelpb.GuildChatMsg{
		Id: msg.ID, SenderId: msg.SenderID, Msg: msg.Msg, Time: msg.Time,
	}}, ids, 0)
	return DispatchResult{Message: &protocolpb.SendGuildChatMsgS2C{}, Pushes: []Push{push}}, nil
}

func (s *Server) handleGetGuildChatMsg(ctx context.Context, sess *Session, q *protocolpb.GetGuildChatMsgC2S) (DispatchResult, error) {
	_, guildID, errCode, ok := s.guildContext(ctx, sess)
	if !ok {
		return DispatchResult{Err: errCode}, nil
	}
	if guildID == 0 {
		return DispatchResult{Err: ErrGuildPlayerNotInGuild}, nil
	}
	msgs, err := s.store.ListGuildChatMsg(ctx, guildID, int64(q.GetMsgId()), 200)
	if err != nil {
		return DispatchResult{}, fmt.Errorf("guild chat list guild=%d: %w", guildID, err)
	}
	out := &protocolpb.GetGuildChatMsgS2C{Messages: []*modelpb.GuildChatMsg{}}
	for _, m := range msgs {
		out.Messages = append(out.Messages, &modelpb.GuildChatMsg{Id: m.ID, SenderId: m.SenderID, Msg: m.Msg, Time: m.Time})
	}
	return DispatchResult{Message: out}, nil
}

func (s *Server) handleGuildMember(ctx context.Context, sess *Session, q *protocolpb.GuildMemberC2S) (DispatchResult, error) {
	_, guildID, errCode, ok := s.guildContext(ctx, sess)
	if !ok {
		return DispatchResult{Err: errCode}, nil
	}
	if guildID == 0 {
		return DispatchResult{Err: ErrGuildPlayerNotInGuild}, nil
	}
	members, err := s.store.ListGuildMembers(ctx, guildID)
	if err != nil {
		return DispatchResult{}, fmt.Errorf("guild member list guild=%d: %w", guildID, err)
	}
	out := &protocolpb.GuildMemberS2C{Infos: []*protocolpb.FriendShowPlayerInfo{}}
	for _, m := range members {
		out.Infos = append(out.Infos, s.guildPlayerShow(m))
	}
	return DispatchResult{Message: out}, nil
}

func (s *Server) handleGetGuildsInfo(ctx context.Context, sess *Session, q *protocolpb.GetGuildsInfoC2S) (DispatchResult, error) {
	_, _, errCode, ok := s.guildContext(ctx, sess)
	if !ok {
		return DispatchResult{Err: errCode}, nil
	}
	out := &protocolpb.GetGuildsInfoS2C{Guilds: map[int64]*modelpb.Guild{}}
	for _, id := range q.GetGuildIds() {
		g, err := s.store.GuildByID(ctx, id)
		if err != nil {
			continue
		}
		message, err := s.guildMessage(ctx, g, false)
		if err != nil {
			return DispatchResult{}, fmt.Errorf("guilds info message guild=%d: %w", id, err)
		}
		out.Guilds[id] = message
	}
	return DispatchResult{Message: out}, nil
}

func (s *Server) handleGetGuildMemberChangeMsg(ctx context.Context, sess *Session, q *protocolpb.GetGuildMemberChangeMsgC2S) (DispatchResult, error) {
	_, guildID, errCode, ok := s.guildContext(ctx, sess)
	if !ok {
		return DispatchResult{Err: errCode}, nil
	}
	if guildID == 0 {
		return DispatchResult{Err: ErrGuildPlayerNotInGuild}, nil
	}
	msgs, err := s.store.ListGuildChangeMsg(ctx, guildID, int64(q.GetMsgId()))
	if err != nil {
		return DispatchResult{}, fmt.Errorf("guild change list guild=%d: %w", guildID, err)
	}
	out := &protocolpb.GetGuildMemberChangeMsgS2C{Messages: []*modelpb.GuildMemberChangeMsg{}}
	for _, m := range msgs {
		out.Messages = append(out.Messages, &modelpb.GuildMemberChangeMsg{
			Id: m.ID, NotificationId: m.NotificationID, Param: m.Params, Time: m.Time,
		})
	}
	return DispatchResult{Message: out}, nil
}
