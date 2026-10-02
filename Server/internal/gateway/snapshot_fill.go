package gateway

import (
	"context"
	"encoding/json"
	"errors"
	"fmt"
	"sort"
	"strings"
	"time"

	store "astralparty-server/internal/db"
	modelpb "astralparty-server/internal/protocol/gen/model"
)

// snapImpl*: ConnectS2C Bootstrap snapshot backfill. The client's Home scene
// reads every one of these sub-messages on the first ConnectS2C frame; leaving
// them nil/empty desyncs local UI state (shop tabs, gacha pity, day-7 gift,
// credit score, ...) until the matching sync opcode happens to fire.

// snapImplTimeValue extracts a unix-seconds timestamp from a config cell that
// may be encoded as a number, a {value: N} enum wrapper, or the dump's
// {_unknown: {field_1: [N]}} shape.
func snapImplTimeValue(raw []byte) int64 {
	if len(raw) == 0 {
		return 0
	}
	var value int64
	if _, err := fmt.Sscan(strings.TrimSpace(string(raw)), &value); err == nil && value > 0 {
		return value
	}
	var typed struct {
		Value   int64   `json:"value"`
		Seconds int64   `json:"seconds"`
		Field1  []int64 `json:"field_1"`
	}
	if decodeJSONRaw(raw, &typed) != nil {
		return 0
	}
	if typed.Value > 0 {
		return typed.Value
	}
	if typed.Seconds > 0 {
		return typed.Seconds
	}
	if len(typed.Field1) > 0 {
		return typed.Field1[0]
	}
	return 0
}

// snapImplFirstTime returns the earliest non-zero timestamp among cells.
func snapImplFirstTime(cells ...[]byte) int64 {
	best := int64(0)
	for _, raw := range cells {
		if value := snapImplTimeValue(raw); value > 0 && (best == 0 || value < best) {
			best = value
		}
	}
	return best
}

// snapImplLastTime returns the latest non-zero timestamp among cells.
func snapImplLastTime(cells ...[]byte) int64 {
	best := int64(0)
	for _, raw := range cells {
		if value := snapImplTimeValue(raw); value > best {
			best = value
		}
	}
	return best
}

// snapImplIDList parses an id column that may hold a plain number, an enum
// wrapper or an array of either.
func snapImplIDList(raw []byte) []int32 {
	if len(raw) == 0 {
		return nil
	}
	var single struct {
		Value int32 `json:"value"`
	}
	if decodeJSONRaw(raw, &single) == nil && single.Value > 0 {
		return []int32{single.Value}
	}
	var plain int32
	if decodeJSONRaw(raw, &plain) == nil && plain > 0 {
		return []int32{plain}
	}
	var list []int32
	if decodeJSONRaw(raw, &list) == nil {
		return list
	}
	var wrapped []struct {
		Value int32 `json:"value"`
	}
	if decodeJSONRaw(raw, &wrapped) == nil {
		out := make([]int32, 0, len(wrapped))
		for _, item := range wrapped {
			if item.Value > 0 {
				out = append(out, item.Value)
			}
		}
		return out
	}
	return nil
}

// snapImplStringMap parses a {itemID: count, ...} reward map whose keys are
// JSON strings.
func snapImplStringMap(raw []byte) map[int32]int32 {
	out := map[int32]int32{}
	if len(raw) == 0 || decodeJSONRaw(raw, &out) == nil {
		return out
	}
	var decoded map[string]int32
	if decodeJSONRaw(raw, &decoded) == nil {
		for key, value := range decoded {
			var id int32
			if _, err := fmt.Sscan(key, &id); err == nil {
				out[id] = value
			}
		}
	}
	return out
}

func decodeJSONRaw(raw []byte, target any) error {
	return json.Unmarshal(raw, target)
}

func unmarshalImpl(raw []byte, target any) error {
	if len(raw) == 0 {
		return errImplEmpty
	}
	return json.Unmarshal(raw, target)
}

func timeImplUnix() int64 { return time.Now().Unix() }

var errImplEmpty = errors.New("empty config cell")

// snapImplSortKeys returns map keys in ascending order for stable output.
func snapImplSortKeys[V any](m map[int32]V) []int32 {
	keys := make([]int32, 0, len(m))
	for key := range m {
		keys = append(keys, key)
	}
	sort.Slice(keys, func(i, j int) bool { return keys[i] < keys[j] })
	return keys
}

// ---------------------------------------------------------------------------
// Shop
// ---------------------------------------------------------------------------

// snapImplShopInfo builds the PlayerShopInfo snapshot. Random-slot generation
// and purchase records live server-side per session; the bootstrap only needs
// a message with the current reset window stamped so the client does not
// force an immediate refresh.
func (s *Server) snapImplShopInfo(now int64) *modelpb.PlayerShopInfo {
	return &modelpb.PlayerShopInfo{
		ShopRandomItem: map[int32]*modelpb.ShopRandomItem{},
		Record:         map[int32]*modelpb.ShopBuyRecord{},
		RechargeRecord: map[int32]*modelpb.ShopBuyRecord{},
		FirstBuy:       map[int32]*modelpb.RechargeBuyFirst{},
		LastResetTime:  now - now%86400,
	}
}

// ---------------------------------------------------------------------------
// Gacha
// ---------------------------------------------------------------------------

// snapImplGachaCounts converts the persisted gacha_progress rows (count and
// claimed-reward state per pool) into the client's pity/count map.
func (s *Server) snapImplGachaCounts(counts map[int32]*modelpb.GachaCount) map[int32]*modelpb.GachaCount {
	if counts == nil {
		return map[int32]*modelpb.GachaCount{}
	}
	return counts
}

// ---------------------------------------------------------------------------
// Day-7 gift package
// ---------------------------------------------------------------------------

// snapImplDay7 builds the day-7 gift map from Day7GiftPackage_goodss. Each
// goods row becomes one entry keyed by goods id with the configured reward-day
// list; a fresh account has claimed nothing yet.
func (s *Server) snapImplDay7(now int64) map[int32]*modelpb.Day7Reward {
	out := map[int32]*modelpb.Day7Reward{}
	for _, id := range s.resources.IDs("Day7GiftPackage_goodss") {
		row, err := s.resources.ReadRaw("Day7GiftPackage_goodss", id)
		if err != nil {
			continue
		}
		var goods struct {
			Items []struct {
				DayNumb int32 `json:"dayNumb"`
			} `json:"day7GiftPackageGoodsConfigureItems"`
		}
		if unmarshalImpl(row["day7GiftPackageGoodsConfigureItems"], &goods.Items) != nil {
			continue
		}
		days := make([]int32, 0, len(goods.Items))
		maxDay := int32(0)
		for _, item := range goods.Items {
			if item.DayNumb > 0 {
				days = append(days, item.DayNumb)
				if item.DayNumb > maxDay {
					maxDay = item.DayNumb
				}
			}
		}
		if len(days) == 0 {
			continue
		}
		out[int32(id)] = &modelpb.Day7Reward{GoodsId: int32(id), CreateTime: now, MaxRewardDay: maxDay, RewardDay: days}
	}
	return out
}

// ---------------------------------------------------------------------------
// Activity pass, scratch card, light gift, flip card
// ---------------------------------------------------------------------------

// snapImplActivityPasses maps currently-open activities (Activity_infos within
// their window) to a pass entry. Gear 0 = free track, which is the only state
// a server can vouch for without purchase records.
func (s *Server) snapImplActivityPasses(now int64) map[int32]*modelpb.ActivityPass {
	out := map[int32]*modelpb.ActivityPass{}
	for _, id := range s.resources.IDs("Activity_infos") {
		row, err := s.resources.ReadRaw("Activity_infos", id)
		if err != nil {
			continue
		}
		begin := snapImplFirstTime(row["beginTime"])
		end := snapImplLastTime(row["endTime"])
		if begin > 0 && now < begin {
			continue
		}
		if end > 0 && now > end {
			continue
		}
		out[int32(id)] = &modelpb.ActivityPass{DefId: int32(id)}
	}
	return out
}

// snapImplScratchCards exposes one ScratchCardRecord per configured scratch-off
// activity, with the first pool as current pool and an empty scratch record.
func (s *Server) snapImplScratchCards() map[int32]*modelpb.ScratchCardRecord {
	out := map[int32]*modelpb.ScratchCardRecord{}
	for _, id := range s.resources.IDs("Activity_scratchoffs") {
		row, err := s.resources.ReadRaw("Activity_scratchoffs", id)
		if err != nil {
			continue
		}
		var activity struct {
			PoolIDs []int32 `json:"scratchoffPoolIds"`
		}
		if unmarshalImpl(row["scratchoffPoolIds"], &activity.PoolIDs) != nil || len(activity.PoolIDs) == 0 {
			continue
		}
		record := &modelpb.ScratchCardRecord{Pool: map[int32]*modelpb.ScratchCardPool{}, CurrentPoolId: activity.PoolIDs[0]}
		for _, poolID := range activity.PoolIDs {
			record.Pool[poolID] = &modelpb.ScratchCardPool{Record: map[int32]int32{}}
		}
		out[int32(id)] = record
	}
	return out
}

// snapImplLightGifts builds light-gift entries from Activity_lightings: one
// entry per activity with its spend requirement mirrored into the Rewards map.
func (s *Server) snapImplLightGifts() map[int32]*modelpb.LightGift {
	out := map[int32]*modelpb.LightGift{}
	for _, id := range s.resources.IDs("Activity_lightings") {
		row, err := s.resources.ReadRaw("Activity_lightings", id)
		if err != nil {
			continue
		}
		entry := &modelpb.LightGift{ActivityId: int32(id), LightGift: map[int32]int32{}, Rewards: map[int32]int32{}}
		entry.Rewards = snapImplStringMap(row["spends"])
		if target := snapImplIDList(row["targetItemID"]); len(target) > 0 {
			entry.LightGift[target[0]] = 0
		}
		out[int32(id)] = entry
	}
	return out
}

// snapImplFlipCards builds flip-card activity entries from Activity_bingoFlips
// with all grid cells un-flipped.
func (s *Server) snapImplFlipCards() map[int32]*modelpb.FlipCardActivity {
	out := map[int32]*modelpb.FlipCardActivity{}
	for _, id := range s.resources.IDs("Activity_bingoFlips") {
		row, err := s.resources.ReadRaw("Activity_bingoFlips", id)
		if err != nil {
			continue
		}
		var activity struct {
			RoundCount int32 `json:"roundCount"`
		}
		if unmarshalImpl(row["roundCount"], &activity.RoundCount) != nil || activity.RoundCount <= 0 {
			continue
		}
		out[int32(id)] = &modelpb.FlipCardActivity{Records: map[int32]bool{}, ProgressReward: map[int32]bool{}}
	}
	return out
}

// ---------------------------------------------------------------------------
// Profile: show player, credit, return, single player
// ---------------------------------------------------------------------------

// snapImplShowPlayer merges the persisted public-profile flags with an empty
// record list so the profile card renders instead of defaulting.
func (s *Server) snapImplShowPlayer(standingPainting int32, achieveIDs []int32, isShowData, isShowFight bool) *modelpb.ShowPlayerInfo {
	info := &modelpb.ShowPlayerInfo{
		StandingPainting: standingPainting,
		AchieveId:        append([]int32(nil), achieveIDs...),
		IsShowData:       isShowData,
		IsShowFight:      isShowFight,
		Record:           []*modelpb.PlayerFightRecord{},
		ReplayRecord:     []*modelpb.PlayerFightRecord{},
	}
	if info.AchieveId == nil {
		info.AchieveId = []int32{}
	}
	return info
}

// snapImplCreditInfo reports a clean credit record: no deductions, no
// punishment timers. Every account starts at the top credit tier.
func (s *Server) snapImplCreditInfo() *modelpb.CreditInfo {
	return &modelpb.CreditInfo{}
}

// snapImplReturnInfo reports "no comeback campaign active": trigger time zero
// means the returning-player flow never fired for this account.
func (s *Server) snapImplReturnInfo() *modelpb.ReturnInfo {
	return &modelpb.ReturnInfo{}
}

// snapImplSingleInfo builds the single-player progress snapshot from the
// persisted stage progress plus the configured level table so the client can
// resolve the next playable stage.
func (s *Server) snapImplSingleInfo(levelPassIDs map[int32]int32, maxScore int32) *modelpb.SingleInfo {
	pass := map[int32]int32{}
	for stage, level := range levelPassIDs {
		if stage > 0 {
			pass[stage] = level
		}
	}
	return &modelpb.SingleInfo{LevelPassIds: pass, MaxScore: maxScore, StageLevelId: map[int32]int32{}}
}

// ---------------------------------------------------------------------------
// Aggregate
// ---------------------------------------------------------------------------

// snapImplBackfill fills every hardcoded-empty bootstrap field on the Player
// message assembled in messages.go. Called once per ConnectS2C; every step is
// best-effort so a missing config table never breaks login.
func (s *Server) snapImplBackfill(player *modelpb.Player, playerID int64, levelPassIDs map[int32]int32, maxScore int32) {
	if player == nil {
		return
	}
	now := timeImplUnix()
	if player.ShopInfo == nil {
		player.ShopInfo = s.snapImplShopInfo(now)
	}
	if player.GachaCount == nil {
		player.GachaCount = map[int32]*modelpb.GachaCount{}
	}
	if player.Day7 == nil {
		player.Day7 = s.snapImplDay7(now)
	}
	if player.ActivityPass == nil {
		player.ActivityPass = s.snapImplActivityPasses(now)
	}
	if player.ScratchCard == nil {
		player.ScratchCard = s.snapImplScratchCards()
	}
	if player.LightGift == nil {
		player.LightGift = s.snapImplLightGifts()
	}
	if player.FlipCard == nil {
		player.FlipCard = s.snapImplFlipCards()
	}
	if player.ShowPlayer == nil {
		player.ShowPlayer = s.snapImplShowPlayer(0, nil, false, false)
	}
	if player.CreditInfo == nil {
		player.CreditInfo = s.snapImplCreditInfo()
	}
	if player.ReturnInfo == nil {
		player.ReturnInfo = s.snapImplReturnInfo()
	}
	if player.SingleInfo == nil {
		player.SingleInfo = s.snapImplSingleInfo(levelPassIDs, maxScore)
	}
	if player.ClientData == nil {
		player.ClientData = &modelpb.ClientData{
			GuideData: map[int32]int32{}, DateChangeData: map[int32]int32{},
			SongData: map[int32]*modelpb.SongData{}, SettingData: map[int32]int32{},
			NewItemData: []int32{}, CampaignTutorialData: []int32{},
			StarExpression: []int32{}, TopExpression: []int32{},
		}
	}
	if player.InviteInfo == nil {
		player.InviteInfo = &modelpb.InviteInfo{TaskFinishIds: []int32{}}
	}
	if player.PayAmountInfo == nil {
		player.PayAmountInfo = &modelpb.PayAmountInfo{}
	}
	if player.Mails == nil {
		player.Mails = map[int32]*modelpb.MailData{}
	}
	s.mailImplBackfillPlayer(player, playerID)
	s.snapImplFriends(player, playerID)
	s.snapImplGuild(player, playerID)
}

// snapImplGuild fills the PlayerGuildInfo snapshot from storage: current guild
// id, pending applications/invitations and chat metadata. Best-effort — a
// storage failure degrades to the empty state.
func (s *Server) snapImplGuild(player *modelpb.Player, playerID int64) {
	if player == nil || player.GuildInfo == nil || playerID <= 0 {
		return
	}
	ctx := context.Background()
	guildInfo := &modelpb.PlayerGuildInfo{
		Applications:        map[int64]int64{},
		Invitations:         map[int64]int64{},
		ReceivedInvitations: map[int64]*modelpb.GuildInvitation{},
		GuildTasks:          map[int32]*modelpb.TaskDSO{},
		WeeklySign:          map[int64]bool{},
	}
	guildID, err := s.store.PlayerGuildID(ctx, playerID)
	if err != nil {
		s.log.Error("guild snapshot load failed", "player_id", playerID, "err", err)
		return
	}
	guildInfo.GuildId = guildID
	apps, err := s.store.ListPlayerApplications(ctx, playerID)
	if err != nil {
		s.log.Error("guild applications snapshot failed", "player_id", playerID, "err", err)
	} else {
		for _, app := range apps {
			guildInfo.Applications[app.GuildID] = app.ApplyTime
			guildInfo.ApplyCount++
		}
	}
	invites, err := s.store.ListPlayerInvitations(ctx, playerID)
	if err != nil {
		s.log.Error("guild invitations snapshot failed", "player_id", playerID, "err", err)
	} else {
		for _, invite := range invites {
			guildInfo.Invitations[invite.GuildID] = invite.InvitationTime
			guildInfo.ReceivedInvitations[invite.GuildID] = &modelpb.GuildInvitation{
				GuildId: invite.GuildID, InviterId: invite.InviterID, InvitationTime: invite.InvitationTime,
			}
		}
	}
	if guildID > 0 {
		if member, err := s.store.GuildMemberRow(ctx, guildID, playerID); err == nil {
			guildInfo.LastJoinTime = member.JoinAt
		}
		if progress, err := s.store.GuildMissionProgress(ctx, guildID, playerID); err == nil {
			for taskID, value := range progress {
				guildInfo.GuildTasks[taskID] = &modelpb.TaskDSO{Id: taskID, Progress: value}
			}
		}
	}
	player.GuildInfo = guildInfo
}

// snapImplFriends fills the FriendList snapshot from storage: accepted friends,
// per-friend notes and blocked ids. Empty-but-present slices keep the client's
// first-login shape stable.
func (s *Server) snapImplFriends(player *modelpb.Player, playerID int64) {
	if player == nil || player.Friends == nil || playerID <= 0 {
		return
	}
	ctx := context.Background()
	ids, notes, blacks, err := s.store.FriendSnapshot(ctx, playerID)
	if err != nil {
		s.log.Error("friend snapshot load failed", "player_id", playerID, "err", err)
		return
	}
	if ids == nil {
		ids = []int64{}
	}
	if blacks == nil {
		blacks = []int64{}
	}
	player.Friends.FriendIds = ids
	player.Friends.Blacks = blacks
	player.Friends.FriendNotes = notes
	if player.Friends.Apply == nil {
		player.Friends.Apply = []*modelpb.FriendApply{}
	}
	if player.Friends.SelfApply == nil {
		player.Friends.SelfApply = []*modelpb.FriendSelfApply{}
	}
}

// gachaCountSnapshot converts persisted gacha_progress rows into the client's
// GachaCount map (pity counters per pool). Guaranteed/role pity values are not
// tracked server-side yet, so only the total draw count per pool is reported.
func gachaCountSnapshot(progress []store.GachaProgress) map[int32]*modelpb.GachaCount {
	out := make(map[int32]*modelpb.GachaCount, len(progress))
	for _, row := range progress {
		if row.PoolID <= 0 {
			continue
		}
		out[row.PoolID] = &modelpb.GachaCount{Count: row.Count, PoolId: row.PoolID}
	}
	return out
}
