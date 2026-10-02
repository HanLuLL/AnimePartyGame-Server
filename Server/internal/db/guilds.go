package db

import (
	"context"
	"database/sql"
	"encoding/json"
	"errors"
	"time"
)

var (
	ErrGuildNameTaken     = errors.New("guild name already exists")
	ErrGuildNotFound      = errors.New("guild not found")
	ErrGuildAlreadyMember = errors.New("player already in a guild")
	ErrGuildNotMember     = errors.New("player is not a guild member")
	ErrGuildFull          = errors.New("guild is full")
	ErrGuildAlreadyApply  = errors.New("application already pending")
	ErrGuildAlreadyInvite = errors.New("invitation already pending")
	ErrGuildPlayerMissing = errors.New("target player does not exist")
)

// Guild role values stored in guild_members.role. The official protocol does
// not publish the enum; 1=master, 2=officer, 3=member follows the client's
// member-list rendering (master listed first, then officers, then members).
const (
	GuildRoleMaster  int32 = 1
	GuildRoleOfficer int32 = 2
	GuildRoleMember  int32 = 3
)

// GuildMemberCap is the maximum number of members per guild.
const GuildMemberCap = 30

type GuildData struct {
	ID                   int64
	Name                 string
	TagIDs               []int32
	Status               int32
	ExAnnouncement       string
	InAnnouncement       string
	LastExternalEditTime int64
	LastExternalEditorID int64
	ExternalEditCount    int32
	LastInternalEditTime int64
	LastInternalEditorID int64
	InternalEditCount    int32
	MasterID             int64
	MemberCount          int32
	CreatedAt            int64
	UpdatedAt            int64
}

type GuildMemberData struct {
	GuildID            int64
	PlayerID           int64
	Role               int32
	WeeklyActivity     int32
	LastWeeklyActivity int32
	LastLoginTime      int64
	WeeklySign         bool
	JoinAt             int64
	// Joined player attributes resolved via the players table.
	Nick  string
	Level int32
}

type GuildApplicationData struct {
	GuildID   int64
	PlayerID  int64
	ApplyTime int64
	Nick      string
	Level     int32
}

type GuildInvitationData struct {
	GuildID        int64
	PlayerID       int64
	InviterID      int64
	InvitationTime int64
}

type GuildChatMsgData struct {
	ID       int64
	GuildID  int64
	SenderID int64
	Msg      string
	Time     int64
}

type GuildChangeMsgData struct {
	ID             int64
	GuildID        int64
	NotificationID int32
	Params         []string
	Time           int64
}

// guildRow scans a guilds row (without the derived member count).
func (s *Store) guildRow(scan func(dest ...any) error) (GuildData, error) {
	var g GuildData
	var tagsJSON string
	var masterID sql.NullInt64
	if err := scan(&g.ID, &g.Name, &tagsJSON, &g.Status, &g.ExAnnouncement, &g.InAnnouncement,
		&g.LastExternalEditTime, &g.LastExternalEditorID, &g.ExternalEditCount,
		&g.LastInternalEditTime, &g.LastInternalEditorID, &g.InternalEditCount,
		&masterID, &g.CreatedAt, &g.UpdatedAt); err != nil {
		return GuildData{}, err
	}
	g.MasterID = masterID.Int64
	g.TagIDs = []int32{}
	if err := json.Unmarshal([]byte(tagsJSON), &g.TagIDs); err != nil {
		g.TagIDs = []int32{}
	}
	return g, nil
}

const guildColumns = `id,name,tag_ids_json,status,ex_announcement,in_announcement,
 last_external_edit_time,last_external_editor_id,external_edit_count,
 last_internal_edit_time,last_internal_editor_id,internal_edit_count,
 master_id,created_at,updated_at`

func (s *Store) GuildByID(ctx context.Context, guildID int64) (GuildData, error) {
	row := s.db.QueryRowContext(ctx, `SELECT `+guildColumns+` FROM guilds WHERE id=?`, guildID)
	g, err := s.guildRow(row.Scan)
	if errors.Is(err, sql.ErrNoRows) {
		return GuildData{}, ErrGuildNotFound
	}
	return g, err
}

func (s *Store) GuildByName(ctx context.Context, name string) (GuildData, error) {
	row := s.db.QueryRowContext(ctx, `SELECT `+guildColumns+` FROM guilds WHERE name=?`, name)
	g, err := s.guildRow(row.Scan)
	if errors.Is(err, sql.ErrNoRows) {
		return GuildData{}, ErrGuildNotFound
	}
	return g, err
}

// CreateGuild inserts a new guild together with its master membership row.
func (s *Store) CreateGuild(ctx context.Context, name string, tagIDs []int32, exAnnouncement string, masterID int64) (GuildData, error) {
	now := time.Now().Unix()
	if tagIDs == nil {
		tagIDs = []int32{}
	}
	tagsJSON, err := json.Marshal(tagIDs)
	if err != nil {
		return GuildData{}, err
	}
	tx, err := s.db.BeginTx(ctx, nil)
	if err != nil {
		return GuildData{}, err
	}
	defer tx.Rollback()
	var exists int
	if err = tx.QueryRowContext(ctx, `SELECT COUNT(*) FROM guilds WHERE name=?`, name).Scan(&exists); err != nil {
		return GuildData{}, err
	}
	if exists > 0 {
		return GuildData{}, ErrGuildNameTaken
	}
	res, err := tx.ExecContext(ctx, `INSERT INTO guilds(name,tag_ids_json,status,ex_announcement,in_announcement,master_id,created_at,updated_at)
	 VALUES(?,?,1,?,?,?,?,?)`, name, string(tagsJSON), exAnnouncement, "", masterID, now, now)
	if err != nil {
		return GuildData{}, err
	}
	guildID, err := res.LastInsertId()
	if err != nil {
		return GuildData{}, err
	}
	if _, err = tx.ExecContext(ctx, `INSERT INTO guild_members(guild_id,player_id,role,join_at,last_login_time) VALUES(?,?,?,?,?)`,
		guildID, masterID, GuildRoleMaster, now, now); err != nil {
		return GuildData{}, err
	}
	if err = tx.Commit(); err != nil {
		return GuildData{}, err
	}
	return s.GuildByID(ctx, guildID)
}

// SearchGuilds lists guilds ordered by member count, filtered by an exact id,
// a name substring or tag overlap. page is 0-based with pageSize entries per page.
func (s *Store) SearchGuilds(ctx context.Context, id int64, name string, tagIDs []int32, page int32, pageSize int32) ([]GuildData, bool, error) {
	query := `SELECT ` + guildColumns + ` FROM guilds WHERE 1=1`
	args := []any{}
	if id > 0 {
		query += ` AND id=?`
		args = append(args, id)
	}
	if name != "" {
		query += ` AND name LIKE '%'||?||'%'`
		args = append(args, name)
	}
	for _, tag := range tagIDs {
		query += ` AND tag_ids_json LIKE '%'||?||'%'`
		args = append(args, tag)
	}
	query += ` ORDER BY id DESC LIMIT ? OFFSET ?`
	args = append(args, pageSize+1, int(page)*int(pageSize))
	rows, err := s.db.QueryContext(ctx, query, args...)
	if err != nil {
		return nil, false, err
	}
	defer rows.Close()
	out := []GuildData{}
	for rows.Next() {
		g, err := s.guildRow(rows.Scan)
		if err != nil {
			return nil, false, err
		}
		out = append(out, g)
	}
	isEnd := int32(len(out)) <= pageSize
	if !isEnd {
		out = out[:pageSize]
	}
	return out, isEnd, rows.Err()
}

// GuildNameByID returns the guild's name, used by join-sync pushes.
func (s *Store) GuildNameByID(ctx context.Context, guildID int64) (string, error) {
	var name string
	err := s.db.QueryRowContext(ctx, `SELECT name FROM guilds WHERE id=?`, guildID).Scan(&name)
	if errors.Is(err, sql.ErrNoRows) {
		return "", ErrGuildNotFound
	}
	return name, err
}

func (s *Store) GuildMemberCount(ctx context.Context, guildID int64) (int32, error) {
	var count int32
	err := s.db.QueryRowContext(ctx, `SELECT COUNT(*) FROM guild_members WHERE guild_id=?`, guildID).Scan(&count)
	return count, err
}

// PlayerGuildID returns the guild the player currently belongs to, or 0.
func (s *Store) PlayerGuildID(ctx context.Context, playerID int64) (int64, error) {
	var guildID int64
	err := s.db.QueryRowContext(ctx, `SELECT guild_id FROM guild_members WHERE player_id=?`, playerID).Scan(&guildID)
	if errors.Is(err, sql.ErrNoRows) {
		return 0, nil
	}
	return guildID, err
}

func (s *Store) GuildMemberRow(ctx context.Context, guildID, playerID int64) (GuildMemberData, error) {
	row := s.db.QueryRowContext(ctx, `SELECT m.guild_id,m.player_id,m.role,m.weekly_activity,m.last_weekly_activity,
	 m.last_login_time,m.weekly_sign,m.join_at,p.nick,p.level
	 FROM guild_members m JOIN players p ON p.id=m.player_id WHERE m.guild_id=? AND m.player_id=?`, guildID, playerID)
	m, err := scanGuildMember(row.Scan)
	if errors.Is(err, sql.ErrNoRows) {
		return GuildMemberData{}, ErrGuildNotMember
	}
	return m, err
}

func scanGuildMember(scan func(dest ...any) error) (GuildMemberData, error) {
	var m GuildMemberData
	var sign int
	if err := scan(&m.GuildID, &m.PlayerID, &m.Role, &m.WeeklyActivity, &m.LastWeeklyActivity,
		&m.LastLoginTime, &sign, &m.JoinAt, &m.Nick, &m.Level); err != nil {
		return GuildMemberData{}, err
	}
	m.WeeklySign = sign != 0
	return m, nil
}

func (s *Store) ListGuildMembers(ctx context.Context, guildID int64) ([]GuildMemberData, error) {
	rows, err := s.db.QueryContext(ctx, `SELECT m.guild_id,m.player_id,m.role,m.weekly_activity,m.last_weekly_activity,
	 m.last_login_time,m.weekly_sign,m.join_at,p.nick,p.level
	 FROM guild_members m JOIN players p ON p.id=m.player_id WHERE m.guild_id=? ORDER BY m.role ASC, m.join_at ASC`, guildID)
	if err != nil {
		return nil, err
	}
	defer rows.Close()
	out := []GuildMemberData{}
	for rows.Next() {
		m, err := scanGuildMember(rows.Scan)
		if err != nil {
			return nil, err
		}
		out = append(out, m)
	}
	return out, rows.Err()
}

func (s *Store) GuildMemberIDs(ctx context.Context, guildID int64) ([]int64, error) {
	rows, err := s.db.QueryContext(ctx, `SELECT player_id FROM guild_members WHERE guild_id=?`, guildID)
	if err != nil {
		return nil, err
	}
	defer rows.Close()
	out := []int64{}
	for rows.Next() {
		var id int64
		if err := rows.Scan(&id); err != nil {
			return nil, err
		}
		out = append(out, id)
	}
	return out, rows.Err()
}

// JoinGuild inserts a membership row and removes the player's pending
// applications and invitations in one transaction.
func (s *Store) JoinGuild(ctx context.Context, guildID, playerID int64) error {
	now := time.Now().Unix()
	tx, err := s.db.BeginTx(ctx, nil)
	if err != nil {
		return err
	}
	defer tx.Rollback()
	var count int32
	if err = tx.QueryRowContext(ctx, `SELECT COUNT(*) FROM guild_members WHERE guild_id=?`, guildID).Scan(&count); err != nil {
		return err
	}
	if count >= GuildMemberCap {
		return ErrGuildFull
	}
	if _, err = tx.ExecContext(ctx, `INSERT INTO guild_members(guild_id,player_id,role,join_at,last_login_time) VALUES(?,?,?, ?,?)`,
		guildID, playerID, GuildRoleMember, now, now); err != nil {
		return err
	}
	if _, err = tx.ExecContext(ctx, `DELETE FROM guild_applications WHERE player_id=?`, playerID); err != nil {
		return err
	}
	if _, err = tx.ExecContext(ctx, `DELETE FROM guild_invitations WHERE player_id=?`, playerID); err != nil {
		return err
	}
	return tx.Commit()
}

func (s *Store) LeaveGuild(ctx context.Context, guildID, playerID int64) error {
	res, err := s.db.ExecContext(ctx, `DELETE FROM guild_members WHERE guild_id=? AND player_id=?`, guildID, playerID)
	if err != nil {
		return err
	}
	if n, _ := res.RowsAffected(); n == 0 {
		return ErrGuildNotMember
	}
	return nil
}

func (s *Store) UpdateGuildMemberRole(ctx context.Context, guildID, playerID int64, role int32) error {
	res, err := s.db.ExecContext(ctx, `UPDATE guild_members SET role=? WHERE guild_id=? AND player_id=?`, role, guildID, playerID)
	if err != nil {
		return err
	}
	if n, _ := res.RowsAffected(); n == 0 {
		return ErrGuildNotMember
	}
	return nil
}

// DisbandGuild deletes the guild and all dependent rows in one transaction.
func (s *Store) DisbandGuild(ctx context.Context, guildID int64) error {
	tx, err := s.db.BeginTx(ctx, nil)
	if err != nil {
		return err
	}
	defer tx.Rollback()
	for _, table := range []string{"guild_members", "guild_applications", "guild_invitations", "guild_chat_messages", "guild_change_messages", "guild_mission_progress"} {
		if _, err = tx.ExecContext(ctx, `DELETE FROM `+table+` WHERE guild_id=?`, guildID); err != nil {
			return err
		}
	}
	if _, err = tx.ExecContext(ctx, `DELETE FROM guilds WHERE id=?`, guildID); err != nil {
		return err
	}
	return tx.Commit()
}

func (s *Store) UpdateGuildExternal(ctx context.Context, guildID int64, tagIDs []int32, exAnnouncement string, editorID int64) error {
	now := time.Now().Unix()
	tags, err := json.Marshal(tagIDs)
	if err != nil {
		return err
	}
	res, err := s.db.ExecContext(ctx, `UPDATE guilds SET tag_ids_json=?,ex_announcement=?,last_external_edit_time=?,
	 last_external_editor_id=?,external_edit_count=external_edit_count+1,updated_at=? WHERE id=?`,
		string(tags), exAnnouncement, now, editorID, now, guildID)
	if err != nil {
		return err
	}
	if n, _ := res.RowsAffected(); n == 0 {
		return ErrGuildNotFound
	}
	return nil
}

func (s *Store) UpdateGuildInternal(ctx context.Context, guildID int64, inAnnouncement string, editorID int64) error {
	now := time.Now().Unix()
	res, err := s.db.ExecContext(ctx, `UPDATE guilds SET in_announcement=?,last_internal_edit_time=?,
	 last_internal_editor_id=?,internal_edit_count=internal_edit_count+1,updated_at=? WHERE id=?`,
		inAnnouncement, now, editorID, now, guildID)
	if err != nil {
		return err
	}
	if n, _ := res.RowsAffected(); n == 0 {
		return ErrGuildNotFound
	}
	return nil
}

func (s *Store) AddGuildApplication(ctx context.Context, guildID, playerID int64) error {
	var exists int
	if err := s.db.QueryRowContext(ctx, `SELECT COUNT(*) FROM guild_applications WHERE guild_id=? AND player_id=?`, guildID, playerID).Scan(&exists); err != nil {
		return err
	}
	if exists > 0 {
		return ErrGuildAlreadyApply
	}
	_, err := s.db.ExecContext(ctx, `INSERT INTO guild_applications(guild_id,player_id,apply_time) VALUES(?,?,?)`, guildID, playerID, time.Now().Unix())
	return err
}

func (s *Store) DeleteGuildApplication(ctx context.Context, guildID, playerID int64) error {
	_, err := s.db.ExecContext(ctx, `DELETE FROM guild_applications WHERE guild_id=? AND player_id=?`, guildID, playerID)
	return err
}

func (s *Store) DeletePlayerApplications(ctx context.Context, playerID int64) error {
	_, err := s.db.ExecContext(ctx, `DELETE FROM guild_applications WHERE player_id=?`, playerID)
	return err
}

func (s *Store) ListGuildApplications(ctx context.Context, guildID int64) ([]GuildApplicationData, error) {
	rows, err := s.db.QueryContext(ctx, `SELECT a.guild_id,a.player_id,a.apply_time,p.nick,p.level
	 FROM guild_applications a JOIN players p ON p.id=a.player_id WHERE a.guild_id=? ORDER BY a.apply_time ASC`, guildID)
	if err != nil {
		return nil, err
	}
	defer rows.Close()
	out := []GuildApplicationData{}
	for rows.Next() {
		var a GuildApplicationData
		if err := rows.Scan(&a.GuildID, &a.PlayerID, &a.ApplyTime, &a.Nick, &a.Level); err != nil {
			return nil, err
		}
		out = append(out, a)
	}
	return out, rows.Err()
}

// ListPlayerApplications returns the guild ids the player has pending
// applications for, used by the PlayerGuildInfo snapshot.
func (s *Store) ListPlayerApplications(ctx context.Context, playerID int64) ([]GuildApplicationData, error) {
	rows, err := s.db.QueryContext(ctx, `SELECT guild_id,player_id,apply_time,'' ,0 FROM guild_applications WHERE player_id=? ORDER BY apply_time ASC`, playerID)
	if err != nil {
		return nil, err
	}
	defer rows.Close()
	out := []GuildApplicationData{}
	for rows.Next() {
		var a GuildApplicationData
		if err := rows.Scan(&a.GuildID, &a.PlayerID, &a.ApplyTime, &a.Nick, &a.Level); err != nil {
			return nil, err
		}
		out = append(out, a)
	}
	return out, rows.Err()
}

func (s *Store) AddGuildInvitation(ctx context.Context, guildID, playerID, inviterID int64) error {
	var exists int
	if err := s.db.QueryRowContext(ctx, `SELECT COUNT(*) FROM guild_invitations WHERE guild_id=? AND player_id=?`, guildID, playerID).Scan(&exists); err != nil {
		return err
	}
	if exists > 0 {
		return ErrGuildAlreadyInvite
	}
	if _, err := s.db.ExecContext(ctx, `INSERT INTO guild_invitations(guild_id,player_id,inviter_id,invitation_time) VALUES(?,?,?,?)`,
		guildID, playerID, inviterID, time.Now().Unix()); err != nil {
		return err
	}
	return nil
}

func (s *Store) DeleteGuildInvitation(ctx context.Context, guildID, playerID int64) error {
	_, err := s.db.ExecContext(ctx, `DELETE FROM guild_invitations WHERE guild_id=? AND player_id=?`, guildID, playerID)
	return err
}

func (s *Store) DeletePlayerInvitations(ctx context.Context, playerID int64) error {
	_, err := s.db.ExecContext(ctx, `DELETE FROM guild_invitations WHERE player_id=?`, playerID)
	return err
}

func (s *Store) ListPlayerInvitations(ctx context.Context, playerID int64) ([]GuildInvitationData, error) {
	rows, err := s.db.QueryContext(ctx, `SELECT guild_id,player_id,inviter_id,invitation_time FROM guild_invitations WHERE player_id=? ORDER BY invitation_time ASC`, playerID)
	if err != nil {
		return nil, err
	}
	defer rows.Close()
	out := []GuildInvitationData{}
	for rows.Next() {
		var v GuildInvitationData
		if err := rows.Scan(&v.GuildID, &v.PlayerID, &v.InviterID, &v.InvitationTime); err != nil {
			return nil, err
		}
		out = append(out, v)
	}
	return out, rows.Err()
}

func (s *Store) ListGuildInvitations(ctx context.Context, guildID int64) ([]GuildInvitationData, error) {
	rows, err := s.db.QueryContext(ctx, `SELECT guild_id,player_id,inviter_id,invitation_time FROM guild_invitations WHERE guild_id=? ORDER BY invitation_time ASC`, guildID)
	if err != nil {
		return nil, err
	}
	defer rows.Close()
	out := []GuildInvitationData{}
	for rows.Next() {
		var v GuildInvitationData
		if err := rows.Scan(&v.GuildID, &v.PlayerID, &v.InviterID, &v.InvitationTime); err != nil {
			return nil, err
		}
		out = append(out, v)
	}
	return out, rows.Err()
}

func (s *Store) InsertGuildChatMsg(ctx context.Context, guildID, senderID int64, msg string) (GuildChatMsgData, error) {
	now := time.Now().Unix()
	res, err := s.db.ExecContext(ctx, `INSERT INTO guild_chat_messages(guild_id,sender_id,msg,time) VALUES(?,?,?,?)`, guildID, senderID, msg, now)
	if err != nil {
		return GuildChatMsgData{}, err
	}
	id, err := res.LastInsertId()
	if err != nil {
		return GuildChatMsgData{}, err
	}
	return GuildChatMsgData{ID: id, GuildID: guildID, SenderID: senderID, Msg: msg, Time: now}, nil
}

// ListGuildChatMsg returns chat messages newer than afterID, oldest first.
func (s *Store) ListGuildChatMsg(ctx context.Context, guildID, afterID int64, limit int32) ([]GuildChatMsgData, error) {
	rows, err := s.db.QueryContext(ctx, `SELECT id,guild_id,sender_id,msg,time FROM guild_chat_messages WHERE guild_id=? AND id>? ORDER BY id ASC LIMIT ?`, guildID, afterID, limit)
	if err != nil {
		return nil, err
	}
	defer rows.Close()
	out := []GuildChatMsgData{}
	for rows.Next() {
		var m GuildChatMsgData
		if err := rows.Scan(&m.ID, &m.GuildID, &m.SenderID, &m.Msg, &m.Time); err != nil {
			return nil, err
		}
		out = append(out, m)
	}
	return out, rows.Err()
}

func (s *Store) InsertGuildChangeMsg(ctx context.Context, guildID int64, notificationID int32, params []string) (GuildChangeMsgData, error) {
	now := time.Now().Unix()
	paramsJSON, err := json.Marshal(params)
	if err != nil {
		return GuildChangeMsgData{}, err
	}
	res, err := s.db.ExecContext(ctx, `INSERT INTO guild_change_messages(guild_id,notification_id,params_json,time) VALUES(?,?,?,?)`,
		guildID, notificationID, string(paramsJSON), now)
	if err != nil {
		return GuildChangeMsgData{}, err
	}
	id, err := res.LastInsertId()
	if err != nil {
		return GuildChangeMsgData{}, err
	}
	return GuildChangeMsgData{ID: id, GuildID: guildID, NotificationID: notificationID, Params: params, Time: now}, nil
}

// ListGuildChangeMsg returns member-change notifications newer than afterID.
func (s *Store) ListGuildChangeMsg(ctx context.Context, guildID, afterID int64) ([]GuildChangeMsgData, error) {
	rows, err := s.db.QueryContext(ctx, `SELECT id,guild_id,notification_id,params_json,time FROM guild_change_messages WHERE guild_id=? AND id>? ORDER BY id ASC`, guildID, afterID)
	if err != nil {
		return nil, err
	}
	defer rows.Close()
	out := []GuildChangeMsgData{}
	for rows.Next() {
		var m GuildChangeMsgData
		var paramsJSON string
		if err := rows.Scan(&m.ID, &m.GuildID, &m.NotificationID, &paramsJSON, &m.Time); err != nil {
			return nil, err
		}
		m.Params = []string{}
		if err := json.Unmarshal([]byte(paramsJSON), &m.Params); err != nil {
			m.Params = []string{}
		}
		out = append(out, m)
	}
	return out, rows.Err()
}

// GuildMissionClaimState reports whether a guild task reward was already
// claimed by the player.
func (s *Store) GuildMissionClaimState(ctx context.Context, guildID, playerID int64, taskID int32) (progress int32, claimed bool, err error) {
	var claimedInt int
	err = s.db.QueryRowContext(ctx, `SELECT progress,claimed FROM guild_mission_progress WHERE guild_id=? AND player_id=? AND task_id=?`,
		guildID, playerID, taskID).Scan(&progress, &claimedInt)
	if errors.Is(err, sql.ErrNoRows) {
		return 0, false, nil
	}
	if err != nil {
		return 0, false, err
	}
	return progress, claimedInt != 0, nil
}

// GuildMissionClaim marks a guild task reward as claimed and stores the
// tracked progress value.
func (s *Store) GuildMissionClaim(ctx context.Context, guildID, playerID int64, taskID, progress int32) error {
	_, err := s.db.ExecContext(ctx, `INSERT INTO guild_mission_progress(guild_id,player_id,task_id,progress,claimed,updated_at) VALUES(?,?,?,?,1,?)
	 ON CONFLICT(guild_id,player_id,task_id) DO UPDATE SET claimed=1,progress=excluded.progress,updated_at=excluded.updated_at`,
		guildID, playerID, taskID, progress, time.Now().Unix())
	return err
}

// GuildMissionProgress maps task ids to their progress for a guild member.
func (s *Store) GuildMissionProgress(ctx context.Context, guildID, playerID int64) (map[int32]int32, error) {
	rows, err := s.db.QueryContext(ctx, `SELECT task_id,progress FROM guild_mission_progress WHERE guild_id=? AND player_id=?`, guildID, playerID)
	if err != nil {
		return nil, err
	}
	defer rows.Close()
	out := map[int32]int32{}
	for rows.Next() {
		var taskID, progress int32
		if err := rows.Scan(&taskID, &progress); err != nil {
			return nil, err
		}
		out[taskID] = progress
	}
	return out, rows.Err()
}
