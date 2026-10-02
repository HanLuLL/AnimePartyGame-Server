package gateway

import (
	"runtime/debug"

	"context"
	"crypto/rand"
	"crypto/sha256"
	"database/sql"
	"encoding/binary"
	"encoding/hex"
	"encoding/json"
	"errors"
	"fmt"
	"math/big"
	"sort"
	"strings"
	"time"

	store "astralparty-server/internal/db"
	wire "astralparty-server/internal/protocol"
	modelpb "astralparty-server/internal/protocol/gen/model"
	protocolpb "astralparty-server/internal/protocol/gen/protocol"
	"google.golang.org/protobuf/proto"
)

type DispatchResult struct {
	Message proto.Message
	Err     int16
	Pushes  []Push
}

// dispatchGuarded runs dispatch with panic recovery so a faulty handler
// degrades to a generic error response instead of taking the server down.
func (s *Server) dispatchGuarded(ctx context.Context, sess *Session, in wire.Frame, route wire.Command, req proto.Message) (res DispatchResult, err error) {
	defer func() {
		if r := recover(); r != nil {
			s.log.Error("handler panicked", "cmd", in.CmdID, "type", route.MessageName, "panic", r, "stack", string(debug.Stack()))
			res, err = DispatchResult{Err: ErrNotOpen}, nil
		}
	}()
	return s.dispatch(ctx, sess, in, route, req)
}

func (s *Server) dispatch(ctx context.Context, sess *Session, in wire.Frame, route wire.Command, req proto.Message) (DispatchResult, error) {
	switch q := req.(type) {
	case *protocolpb.ConnectC2S:
		return s.handleConnect(ctx, sess, q)
	case *protocolpb.ClientHarmonyC2S:
		return s.handleClientHarmony(ctx, sess, q)
	case *protocolpb.HeartbeatC2S:
		// The client computes ping as (local tick - echoed Client). If Client
		// is not echoed verbatim as sfixed64, ping overflows past the
		// reconnect threshold (~300) and NetManager force-reconnects within
		// seconds. Never drop or rewrite this field.
		result := DispatchResult{Message: &protocolpb.HeartbeatS2C{Client: q.Client, Server: time.Now().Unix()}}
		if playerID, ok := sess.nextInitialOnlineSync(); ok {
			roomID, roomErr := s.store.CurrentRoomID(ctx, playerID)
			if roomErr != nil {
				s.log.Error("online room state lookup failed", "player_id", playerID, "err", roomErr)
			} else {
				sess.updateInitialOnlineRoomID(roomID)
				push := s.pushFor("OnlineSyncRoomIdS2C",
					&protocolpb.OnlineSyncRoomIdS2C{RoomId: roomID}, []int64{playerID}, 0)
				push.OnlySession = sess
				result.Pushes = append(result.Pushes, push)
			}
		} else if playerID, roomID, ended := sess.takeInitialOnlineSyncWindowEnd(); ended {
			fields := []any{"player_id", playerID, "room_id", roomID,
				"attempts", initialOnlineSyncAttempts, "sync_room_ack_expected", roomID != 0}
			if roomID != 0 {
				s.log.Warn("online room state retry window ended", fields...)
			} else {
				s.log.Info("online room state retry window ended", fields...)
			}
		}
		return result, nil
	case *protocolpb.CreateRoomC2S:
		return s.handleCreateRoom(ctx, sess, q)
	case *protocolpb.JoinRoomC2S:
		return s.handleJoinRoom(ctx, sess, q)
	case *protocolpb.QueryRoomC2S:
		return s.handleQueryRoom(ctx, q)
	case *protocolpb.SyncRoomC2S:
		return s.handleSyncRoom(ctx, sess, q)
	case *protocolpb.QuickJoinRoomC2S:
		return s.handleQuickJoin(ctx, sess, q)
	case *protocolpb.RoomReadyC2S:
		return s.handleRoomReady(ctx, sess, q)
	case *protocolpb.StartGameC2S:
		return s.handleStartGame(ctx, sess, q)
	case *protocolpb.ExitRoomC2S:
		return s.handleExitRoom(ctx, sess, q)
	case *protocolpb.RefreshRoomStateC2S:
		return s.handleRefreshRoom(ctx, sess, q)
	case *protocolpb.SearchRoomC2S:
		return s.handleSearchRoom(ctx, q)
	case *protocolpb.RoomKickPlayerC2S:
		return s.handleRoomKickPlayer(ctx, sess, q)
	case *protocolpb.RoomAbdicationC2S:
		return s.handleRoomAbdication(ctx, sess, q)
	case *protocolpb.ChoiceHeroC2S2:
		return s.handleChoiceHero(ctx, sess, q)
	case *protocolpb.AffirmHeroC2S:
		return s.handleAffirmHero(ctx, sess, q)
	case *protocolpb.ChooseSkinC2S:
		return s.handleChooseSkin(ctx, sess, q)
	case *protocolpb.ThrowDiceC2S:
		return s.handleThrowDice(ctx, sess, in, q)
	case *protocolpb.ThrowDiceResultC2S:
		return s.handleThrowDiceResult(ctx, sess, in, q)
	case *protocolpb.ChangeRoomC2S:
		return s.handleChangeRoom(ctx, sess, q)
	case *protocolpb.UseEffectCardC2S:
		return s.handleUseEffectCard(ctx, sess, in, q)
	case *protocolpb.AbandonCardC2S:
		return s.handleAbandonCards(ctx, sess, in, q)
	case *protocolpb.BombThrowDiceC2S:
		return s.handleBombThrowDice(ctx, sess, in, q)
	case *protocolpb.MoveAgainC2S:
		return s.handleMoveAgain(ctx, sess, in, q)
	case *protocolpb.RollGoldC2S:
		return s.handleRollGold(ctx, sess, in, q)
	case *protocolpb.LotteryChoiceC2S:
		return s.handleLotteryChoice(ctx, sess, in, q)
	case *protocolpb.PursuitC2S:
		return s.handlePursuit(ctx, sess, in, q)
	case *protocolpb.LandChoiceTargetC2S:
		return s.handleLandChoiceTarget(ctx, sess, in, q)
	case *protocolpb.AskBattleC2S:
		return s.handleAskBattle(ctx, sess, in, q)
	case *protocolpb.BattleUseCardC2S:
		return s.handleBattleUseCard(ctx, sess, in, q)
	case *protocolpb.BattleThrowDiceC2S:
		return s.handleBattleThrowDice(ctx, sess, in, q)
	case *protocolpb.BattleChoiceC2S:
		return s.handleBattleChoice(ctx, sess, in, q)
	case *protocolpb.MoveC2S:
		return s.handleMove(ctx, sess, in, q)
	case *protocolpb.ShopBuyC2S:
		return s.handleShopBuy(ctx, sess, in, q)
	case *protocolpb.PVEShopBuyC2S:
		return s.handlePVEShopBuy(ctx, sess, in, q)
	case *protocolpb.TriggerEventC2S:
		return s.handleTriggerEvent(ctx, sess, in, q)
	case *protocolpb.TriggerDestinyC2S:
		return s.handleTriggerDestiny(ctx, sess, in, q)
	case *protocolpb.TriggerHospitalC2S:
		return s.handleTriggerHospital(ctx, sess, in, q)
	case *protocolpb.TriggerDivinationC2S:
		return s.handleTriggerDivination(ctx, sess, in, q)
	case *protocolpb.SelectEventC2S:
		return s.handleSelectEvent(ctx, sess, in, q)
	case *protocolpb.PveHeroUpLvC2S:
		return s.handlePveHeroUpLevel(ctx, sess, q)
	case *protocolpb.PveHeroTalentUpC2S:
		return s.handlePveHeroTalentUp(ctx, sess, q)
	case *protocolpb.StartGambleC2S:
		return s.handleStartGamble(ctx, sess, in, q)
	case *protocolpb.GambleThrowDicC2S:
		return s.handleGambleThrow(ctx, sess, in, q)
	case *protocolpb.StopOrContinueC2S:
		return s.handleStopOrContinue(ctx, sess, in, q)
	case *protocolpb.GetPlayerSimpleC2S:
		return s.handleGetPlayerSimple(ctx, q)
	case *protocolpb.GetShowPlayerC2S:
		return s.handleGetShowPlayer(ctx, sess, q)
	case *protocolpb.GetHeroInfoC2S:
		return s.handleGetHeroInfo(ctx, sess, q)
	case *protocolpb.SetShowPlayerC2S:
		return s.handleSetShowPlayer(ctx, sess, q)
	case *protocolpb.TaskRewardC2S:
		return s.handleTaskReward(ctx, sess, q)
	case *protocolpb.GetSignInRewardC2S:
		return s.handleSignInReward(ctx, sess, q)
	case *protocolpb.TeachingC2S:
		return s.handleTeaching(ctx, sess)
	case *protocolpb.SearchPlayerC2S:
		return s.handleSearchPlayer(ctx, q)
	case *protocolpb.FriendListC2S:
		return s.handleFriendList(ctx, sess)
	case *protocolpb.FriendOpC2S:
		return s.handleFriendOp(ctx, sess, q)
	case *protocolpb.FriendInviteC2S:
		return s.handleFriendInvite(ctx, sess, q)
	case *protocolpb.FriendInviteListC2S:
		return s.handleFriendInviteList(ctx, sess)
	case *protocolpb.FriendInviteCleanC2S:
		return s.handleFriendInviteClean(ctx, sess)
	case *protocolpb.NearFightPlayerC2S:
		return s.handleNearFightPlayers(ctx, sess, q)
	case *protocolpb.FriendBlacksListC2S:
		return s.handleFriendBlacksList(ctx, sess)
	case *protocolpb.FriendApplyC2S:
		return s.handleFriendApply(ctx, sess, q)
	case *protocolpb.FriendApplyListC2S:
		return s.handleFriendApplyList(ctx, sess)
	case *protocolpb.FriendApplyOpC2S:
		return s.handleFriendApplyOp(ctx, sess, q)
	case *protocolpb.SetFriendNoteC2S:
		return s.handleSetFriendNote(ctx, sess, q)
	case *protocolpb.SetOnlineStatusC2S:
		return s.handleSetOnlineStatus(ctx, sess, q)
	case *protocolpb.SendChatC2S:
		return s.handleSendChat(ctx, sess, in, q)
	case *protocolpb.FriendSendMsgC2S:
		return s.handleFriendSendMsg(ctx, sess, q)
	case *protocolpb.GetChatMsgC2S:
		return s.handleGetChatMsg(ctx, sess, q)
	case *protocolpb.ReadChatMsgC2S:
		return DispatchResult{Message: &protocolpb.ReadChatMsgS2C{}}, nil
	case *protocolpb.DelChatMsgInfoC2S:
		return s.handleDeleteChatMsgInfo(ctx, sess, q)
	case *protocolpb.ChatMapMarkersC2S:
		return s.handleChatMapMarkers(ctx, sess, q)
	case *protocolpb.PlayerChatC2S:
		return s.handlePlayerChat(ctx, sess, q)
	case *protocolpb.MatchTeamChatC2S:
		return s.handleMatchTeamChat(ctx, sess, q)
	case *protocolpb.PlayerUseItemC2S:
		return s.handleUseItem(ctx, sess, q)
	case *protocolpb.SetFashionC2S:
		return s.handleSetFashion(ctx, sess, q)
	case *protocolpb.SelectFashionPlanC2S:
		return s.handleSelectFashionPlan(ctx, sess, q)
	case *protocolpb.GachaC2S:
		return s.handleGacha(ctx, sess, in, q)
	case *protocolpb.GachaRecordC2S:
		return s.handleGachaRecord(ctx, sess, q)
	case *protocolpb.GachaCountRewardC2S:
		return s.handleGachaCountReward(ctx, sess, q)
	case *protocolpb.RookieGachaRewardC2S:
		return s.handleRookieGachaReward(ctx, sess, q)
	case *protocolpb.PlayerShopBuyC2S:
		return s.handlePlayerShopBuy(ctx, sess, in, q)
	case *protocolpb.MailReadC2S:
		return s.handleMailRead(ctx, sess, q)
	case *protocolpb.MailStarC2S:
		return s.handleMailStar(ctx, sess, q)
	case *protocolpb.MailGetRewardC2S:
		return s.handleMailReward(ctx, sess, q)
	case *protocolpb.MailDelReadC2S:
		return s.handleMailDelete(ctx, sess, q)
	case *protocolpb.ChangeNameC2S:
		return s.handleChangeName(ctx, sess, q)
	case *protocolpb.RoomShortChatC2S:
		return s.handleRoomShortChat(ctx, sess, in, q)
	case *protocolpb.CreateMatchTeamC2S:
		return s.handleCreateMatchTeam(ctx, sess, q)
	case *protocolpb.ChangeMatchTeamC2S:
		return s.handleChangeMatchTeam(ctx, sess, q)
	case *protocolpb.JoinMatchTeamC2S:
		return s.handleJoinMatchTeam(ctx, sess, q)
	case *protocolpb.ExitMatchTeamC2S:
		return s.handleExitMatchTeam(ctx, sess, q)
	case *protocolpb.RefreshMatchTeamInfoC2S:
		return s.handleRefreshMatchTeam(ctx, sess, q)
	case *protocolpb.MatchTeamReadyC2S:
		return s.handleMatchTeamReady(ctx, sess, q)
	case *protocolpb.StartMatchC2S:
		return s.handleStartMatch(ctx, sess, q.TeamId, true)
	case *protocolpb.CancelMatchC2S:
		return s.handleStartMatch(ctx, sess, q.TeamId, false)
	case *protocolpb.MatchSuccessC2S:
		return s.handleMatchSuccessAck(ctx, sess, q)
	case *protocolpb.GetPlayerFightRecordC2S:
		return s.handleGetPlayerFightRecord(ctx, sess, q)
	case *protocolpb.BattlePassGetRewardC2S:
		return s.handleBattlePassGetReward(ctx, sess)
	case *protocolpb.EventThrowDiceC2S:
		return s.handleEventThrowDice(ctx, sess, q)
	case *protocolpb.ChoiceDirectionC2S:
		return s.handleChoiceDirection(ctx, sess, q)
	case *protocolpb.UseQuickCardC2S:
		return s.handleUseQuickCard(ctx, sess, q)
	case *protocolpb.CreateGuildC2S:
		return s.handleCreateGuild(ctx, sess, q)
	case *protocolpb.SearchGuildC2S:
		return s.handleSearchGuild(ctx, sess, q)
	case *protocolpb.ApplyToGuildC2S:
		return s.handleApplyToGuild(ctx, sess, q)
	case *protocolpb.ProcessGuildApplicationC2S:
		return s.handleProcessGuildApplication(ctx, sess, q)
	case *protocolpb.SendGuildInvitationC2S:
		return s.handleSendGuildInvitation(ctx, sess, q)
	case *protocolpb.ProcessGuildInvitationC2S:
		return s.handleProcessGuildInvitation(ctx, sess, q)
	case *protocolpb.GetGuildInfoC2S:
		return s.handleGetGuildInfo(ctx, sess, q)
	case *protocolpb.UpdateGuildSettingsC2S:
		return s.handleUpdateGuildSettings(ctx, sess, q)
	case *protocolpb.UpdateGuildInAnnouncementC2S:
		return s.handleUpdateGuildInAnnouncement(ctx, sess, q)
	case *protocolpb.TransferGuildMasterC2S:
		return s.handleTransferGuildMaster(ctx, sess, q)
	case *protocolpb.ChangeGuildMemberTitleC2S:
		return s.handleChangeGuildMemberTitle(ctx, sess, q)
	case *protocolpb.KickGuildMemberC2S:
		return s.handleKickGuildMember(ctx, sess, q)
	case *protocolpb.ImpeachGuildMasterC2S:
		return s.handleImpeachGuildMaster(ctx, sess)
	case *protocolpb.ExitGuildC2S:
		return s.handleExitGuild(ctx, sess, q)
	case *protocolpb.DisbandGuildC2S:
		return s.handleDisbandGuild(ctx, sess, q)
	case *protocolpb.GuildMissionRewardC2S:
		return s.handleGuildMissionReward(ctx, sess, q)
	case *protocolpb.SendGuildChatMsgC2S:
		return s.handleSendGuildChatMsg(ctx, sess, q)
	case *protocolpb.GetGuildChatMsgC2S:
		return s.handleGetGuildChatMsg(ctx, sess, q)
	case *protocolpb.GuildMemberC2S:
		return s.handleGuildMember(ctx, sess, q)
	case *protocolpb.GetGuildsInfoC2S:
		return s.handleGetGuildsInfo(ctx, sess, q)
	case *protocolpb.GetGuildMemberChangeMsgC2S:
		return s.handleGetGuildMemberChangeMsg(ctx, sess, q)
	case *protocolpb.GmC2S:
		return s.handleGm(ctx, sess, q)
	case *protocolpb.CheatItemC2S:
		return s.handleCheatItem(ctx, sess, q)
	case *protocolpb.GMPlayerSettingC2S:
		return s.handleGMPlayerSetting(ctx, sess, q)
	case *protocolpb.RoleCardUpLvC2S:
		return s.handleRoleCardUpLv(ctx, sess, q)
	case *protocolpb.RoleCardBreakThroughC2S:
		return s.handleRoleCardBreakThrough(ctx, sess, q)
	case *protocolpb.RoleCardChoiceResC2S:
		return s.handleRoleCardChoiceRes(ctx, sess, q)
	case *protocolpb.RoleCardCollectC2S:
		return s.handleRoleCardCollect(ctx, sess, q)
	case *protocolpb.UseTreasureC2S:
		return s.handleUseTreasure(ctx, sess, q)
	case *protocolpb.UseTreasureAutoTransformC2S:
		return s.handleUseTreasureAutoTransform(ctx, sess, q)
	case *protocolpb.ChargeCreateC2S:
		return s.handleChargeCreate(ctx, sess, q)
	case *protocolpb.ChargeC2S:
		return s.handleCharge(ctx, sess, q)
	case *protocolpb.GiftCdkC2S:
		return s.handleGiftCdk(ctx, sess, q)
	case *protocolpb.DevChargeC2S:
		return s.handleDevCharge(ctx, sess, q)
	case *protocolpb.AbroadCreateOrderC2S:
		return s.handleAbroadCreateOrder(ctx, sess, q)
	case *protocolpb.ActivityTaskRewardC2S:
		return s.handleActivityTaskReward(ctx, sess, q)
	case *protocolpb.ActivityMissionRewardC2S:
		return s.handleActivityMissionReward(ctx, sess, q)
	case *protocolpb.ScratchCardC2S:
		return s.handleScratchCard(ctx, sess, q)
	case *protocolpb.NextScratchCardPoolC2S:
		return s.handleNextScratchCardPool(ctx, sess, q)
	case *protocolpb.FlipCardC2S:
		return s.handleFlipCard(ctx, sess, q)
	case *protocolpb.FlipCardProgressRewardC2S:
		return s.handleFlipCardProgressReward(ctx, sess, q)
	case *protocolpb.LightGiftC2S:
		return s.handleLightGift(ctx, sess, q)
	case *protocolpb.BuyLightGiftC2S:
		return s.handleBuyLightGift(ctx, sess, q)
	case *protocolpb.PraisePlayerC2S:
		return s.handlePraisePlayer(ctx, sess, q)
	case *protocolpb.AccuseC2S:
		return s.handleAccuse(ctx, sess, q)
	case *protocolpb.ClientDataUploadC2S:
		return s.handleClientDataUpload(ctx, sess, q)
	case *protocolpb.ClientCheckTaskC2S:
		return s.handleClientCheckTask(ctx, sess, q)
	case *protocolpb.ClientClickConfirmTaskC2S:
		return s.handleClientClickConfirmTask(ctx, sess, q)
	case *protocolpb.AgeVerifyC2S:
		return s.handleAgeVerify(ctx, sess, q)
	case *protocolpb.TimeWastingC2S:
		return s.handleTimeWasting(ctx, sess, q)
	case *protocolpb.ActionOverTimeLogC2S:
		return s.handleActionOverTimeLog(ctx, sess, q)
	case *protocolpb.TestRpcEchoC2S:
		return s.handleTestRpcEcho(ctx, sess, q)
	case *protocolpb.VoteC2S:
		return s.handleVote(ctx, sess, q)
	case *protocolpb.VoteSelectC2S:
		return s.handleVoteSelect(ctx, sess, q)
	case *protocolpb.NotifyStoryC2S:
		return s.handleNotifyStory(ctx, sess, q)
	case *protocolpb.CampScoreC2S:
		return s.handleCampScore(ctx, sess, q)
	case *protocolpb.AskReviveTeammateC2S:
		return s.handleAskReviveTeammate(ctx, sess, q)
	case *protocolpb.VendorBuyCardC2S:
		return s.handleVendorBuyCard(ctx, sess, q)
	case *protocolpb.TransferStarDiscC2S:
		return s.handleTransferStarDisc(ctx, sess, q)
	case *protocolpb.ApplyChangeSlotC2S:
		return s.handleApplyChangeSlot(ctx, sess, q)
	case *protocolpb.OpsChangeSlotC2S:
		return s.handleOpsChangeSlot(ctx, sess, q)
	case *protocolpb.SyncSingleGameDataC2S:
		return s.handleSyncSingleGameData(ctx, sess, q)
	case *protocolpb.SingleGameDataC2S:
		return s.handleSingleGameData(ctx, sess, q)
	case *protocolpb.SingleCampaignC2S:
		return s.handleSingleCampaign(ctx, sess, q)
	case *protocolpb.MatchTeamInviteC2S:
		return s.handleMatchTeamInvite(ctx, sess, q)
	case *protocolpb.GetQuestionUrlC2S:
		return s.handleGetQuestionUrl(ctx, sess, q)
	case *protocolpb.SelectRelicC2S:
		return s.handleSelectRelic(ctx, sess, q)
	case *protocolpb.BuyRelicC2S:
		return s.handleBuyRelic(ctx, sess, q)
	case *protocolpb.SelectMechanismC2S:
		return s.handleSelectMechanism(ctx, sess, q)
	case *protocolpb.MonsterPursuitC2S:
		return s.handleMonsterPursuit(ctx, sess, q)
	case *protocolpb.LiveGiftPackageC2S:
		return s.handleLiveGiftPackage(ctx, sess, q)
	case *protocolpb.LaborActDiceC2S:
		return s.handleLaborActDice(ctx, sess, q)
	case *protocolpb.AcquisitionC2S:
		return s.handleAcquisition(ctx, sess, q)
	case *protocolpb.AcquisitionRewardC2S:
		return s.handleAcquisitionReward(ctx, sess, q)
	case *protocolpb.ReturnGiftClaimC2S:
		return s.handleReturnGiftClaim(ctx, sess, q)
	case *protocolpb.ReturnSignInClaimC2S:
		return s.handleReturnSignInClaim(ctx, sess, q)
	case *protocolpb.ReturnSurveyFinishC2S:
		return s.handleReturnSurveyFinish(ctx, sess, q)
	case *protocolpb.SetCardAltArtC2S:
		return s.handleSetCardAltArt(ctx, sess, q)
	case *protocolpb.SelectRewardCardC2S:
		return s.handleSelectRewardCard(ctx, sess, q)
	case *protocolpb.SteamSearchRoomC2S:
		return s.handleSteamSearchRoom(ctx, sess, q)
	case *protocolpb.WatchJoinRoomC2S:
		return s.handleWatchJoinRoom(ctx, sess, q)
	case *protocolpb.WatchExitRoomC2S:
		return s.handleWatchExitRoom(ctx, sess, q)
	case *protocolpb.WatchRefreshRoomStateC2S:
		return s.handleWatchRefreshRoomState(ctx, sess, q)
	case *protocolpb.GetDay7RewardC2S:
		return s.handleGetDay7Reward(ctx, sess, q)
	case *protocolpb.GetActivityPassRewardC2S:
		return s.handleGetActivityPassReward(ctx, sess, q)
	case *protocolpb.GetReturnInfoC2S:
		return s.handleGetReturnInfo(ctx, sess, q)
	case *protocolpb.ChinaCreateOrderC2S:
		return s.handleChinaCreateOrder(ctx, sess, q)
	case *protocolpb.BattlePassTaskRewardC2S:
		return s.handleBattlePassTaskReward(ctx, sess, q)
	case *protocolpb.BattlePassUpLvC2S:
		return s.handleBattlePassUpLv(ctx, sess, q)
	default:
		// For every known request pair, the outer router still returns its exact
		// protobuf response type. This keeps framing/routing functional while
		// domain rules for the remaining commands are implemented incrementally.
		//
		// The Sys*-prefix commands (SysFriendInfo, SysPlayerOnline, SysSyncPlayer,
		// SysSendMail, SysRoomFinish, SysRoomAddExp, SysPraise, SysCanPraiseInfo,
		// SysChinaPayMsg, SysAbroadPayMsg, SysQuestion, SysRecoupItem,
		// SysPlayerClean, SysMutePlayer, SysGmChangeName,
		// SysPlayerCreditScoreChange, SysPlayerPunishmentTime,
		// SysSyncPlayerMatchPunishmentTime, SysSaveSimplePlayerInfo,
		// SysGetShowFriend, SysPushReturnInfo, SysFriendAdd, SysFriendApply,
		// SysFriendDel, SysFriendInvite, SysFriendSendMsg, SysCampaignAward,
		// SysCampaignFinish, SysPlayerOnlineRoom) plus PlayerOnlineRoom,
		// AcquisitionMsg and GMChangeCreditScore are internal/ops-originated
		// messages. Verified against the decompiled client: the game client never
		// sends them, so they intentionally fall through to the not-open response.
		respRoute, ok := s.registry.ResponseFor(in.CmdID)
		if !ok {
			return DispatchResult{Err: ErrNotOpen}, nil
		}
		m, err := s.registry.NewMessage(respRoute.Message)
		if err != nil {
			return DispatchResult{}, err
		}
		if isReadOnlyCommand(route.MessageName) {
			return DispatchResult{Message: m, Err: ErrSucc}, nil
		}
		return DispatchResult{Message: m, Err: ErrNotOpen}, nil
	}
}

func (s *Server) handleClientHarmony(ctx context.Context, sess *Session, q *protocolpb.ClientHarmonyC2S) (DispatchResult, error) {
	playerID, ok := s.currentPlayerID(sess)
	if !ok {
		return DispatchResult{Err: ErrAuth}, nil
	}
	if err := s.store.SetPlayerHarmony(ctx, playerID, q.GetIsHarmony(), q.GetHarmonyType()); err != nil {
		s.log.Warn("persist harmony state failed", "player_id", playerID, "err", err)
	}
	return DispatchResult{Message: &protocolpb.ClientHarmonyS2C{IsHarmony: q.GetIsHarmony()}}, nil
}

func (s *Server) handleConnect(ctx context.Context, sess *Session, q *protocolpb.ConnectC2S) (DispatchResult, error) {
	platform, loginKey, nick, device, token := "", "", "", "", ""
	reject := func(reason string, code int16) (DispatchResult, error) {
		s.log.Warn("connect rejected", "auth_type", int32(q.GetAuth()), "auth_method", platform, "reason", reason)
		return DispatchResult{Message: &protocolpb.ConnectS2C{}, Err: code}, nil
	}
	var selfAccount *store.EmailAccount
	switch q.Auth {
	case protocolpb.AuthType_Dev:
		platform = "dev"
		if !s.cfg.AllowDevLogin {
			return reject("dev_login_disabled", ErrAuth)
		}
		if d := q.GetDev(); d != nil {
			nick = strings.TrimSpace(d.Nick)
		}
		if nick == "" {
			nick = "Guest"
		}
		loginKey = "dev:" + nick
	case protocolpb.AuthType_China:
		platform = "self"
		c := q.GetChina()
		if c == nil || strings.TrimSpace(c.Sid) == "" {
			return reject("sid_missing", ErrAuth)
		}
		if c.GetExtra() != "bn" {
			return reject("channel_extra_invalid", ErrAuth)
		}
		device = c.DeviceId
		token = c.Sid
		account, err := s.store.ResolveGameSelfSession(ctx, store.SessionTokenHash(c.Sid), time.Now().Unix())
		switch {
		case err == nil:
			// A session issued by this server's account portal: bind to it.
			selfAccount = &account
		case errors.Is(err, store.ErrPlayerWebPasswordChangeRequired):
			return reject("player_web_password_change_required", ErrAuth)
		case errors.Is(err, store.ErrEmailSession):
			// Not a portal session: this is a native SDK login ticket (the
			// stock client always sends one on the bn channel). Accept it as
			// an opaque credential keyed by device so the client can enter
			// the game; a stable per-device login key keeps the same save.
			if strings.TrimSpace(device) == "" {
				return reject("device_id_missing", ErrAuth)
			}
			loginKey = "sdk:bn:" + device
			nick = "Traveler" + shortKey(loginKey)
		default:
			s.log.Error("connect session lookup failed", "err", err)
			return reject("session_store_unavailable", ErrAuth)
		}
	default:
		// This reconstruction accepts self-hosted account sessions only. Provider
		// tickets are not identity proofs unless the provider's signature is
		// actually verified by a trusted server-side SDK.
		return reject("unsupported_auth_type", ErrAuth)
	}
	if selfAccount != nil {
		nick = selfAccount.Nick
	}
	if len([]rune(nick)) > 32 {
		return reject("nick_too_long", ErrInvalidParam)
	}
	for _, r := range nick {
		if r < 0x20 || r == 0x7f {
			return reject("nick_contains_control_character", ErrInvalidParam)
		}
	}
	if token == "" {
		var err error
		token, err = randomToken()
		if err != nil {
			return DispatchResult{}, err
		}
	}
	var p store.Player
	var accountID int64
	var storedToken string
	var err error
	if selfAccount != nil {
		p, err = s.store.LoadSelfPlayer(ctx, selfAccount.ID, device, time.Now().Unix())
		accountID = selfAccount.ID
	} else {
		p, accountID, storedToken, err = s.store.GetOrCreatePlayer(ctx, loginKey, platform, nick, device, token)
	}
	if err != nil {
		return DispatchResult{}, err
	}
	if accountID > 0 {
		disabled, checkErr := s.store.AccountDisabled(ctx, accountID)
		if checkErr != nil {
			s.log.Error("connect account status lookup failed", "account_id", accountID, "err", checkErr)
			return reject("account_status_unavailable", ErrAuth)
		}
		if disabled {
			s.log.Warn("connect rejected", "auth_type", int32(q.GetAuth()), "auth_method", platform,
				"account_id", accountID, "reason", "account_disabled")
			return DispatchResult{Message: &protocolpb.ConnectS2C{}, Err: ErrAuth}, nil
		}
	}
	nick = p.Nick
	sid, err := randomSessionID()
	if err != nil {
		return DispatchResult{}, err
	}
	if storedToken != "" {
		token = storedToken
	}
	if err = s.store.EnsureStarterItem(ctx, p.ID, 90001, 100); err != nil {
		return DispatchResult{}, err
	}
	now := time.Now()
	// The novice-guide state is only ever recorded when the client reports it
	// finished (TeachingC2S). Pre-marking it at login made the client skip the
	// official new-account guide flow and jump straight into the Home scene,
	// a state a real account only reaches after the guide completes.
	if err = s.store.RecordTaskLoginProgress(ctx, p.ID, now); err != nil {
		return DispatchResult{}, err
	}
	for _, itemID := range s.resources.DefaultFashionPlan() {
		if itemID > 0 {
			if err = s.store.EnsureStarterItem(ctx, p.ID, itemID, 1); err != nil {
				return DispatchResult{}, err
			}
		}
	}
	p.PveHeroes, p.Inventory, err = s.store.LoadPlayerGameplayData(ctx, p.ID)
	if err != nil {
		return DispatchResult{}, err
	}
	p.FashionPlans, p.UsePlan, err = s.store.LoadFashionState(ctx, p.ID)
	if err != nil {
		return DispatchResult{}, err
	}
	if len(p.FashionPlans) == 0 {
		p.FashionPlans = map[int32]map[int32]int32{1: s.resources.DefaultFashionPlan()}
		p.UsePlan = 1
		if err = s.store.SaveFashionState(ctx, p.ID, p.FashionPlans, p.UsePlan); err != nil {
			return DispatchResult{}, err
		}
	}
	if _, exists := p.FashionPlans[p.UsePlan]; !exists {
		p.UsePlan = 1
		if _, exists = p.FashionPlans[p.UsePlan]; !exists {
			p.FashionPlans[p.UsePlan] = s.resources.DefaultFashionPlan()
		}
		if err = s.store.SaveFashionState(ctx, p.ID, p.FashionPlans, p.UsePlan); err != nil {
			return DispatchResult{}, err
		}
	}
	p.GachaProgress, err = s.store.GachaProgressForPlayer(ctx, p.ID)
	if err != nil {
		return DispatchResult{}, err
	}
	p.SignInRewards, err = s.store.SignInRewardsForPlayer(ctx, p.ID, s.resources.SignInActivityIDs())
	if err != nil {
		return DispatchResult{}, err
	}
	account := &modelpb.AccountInfo{AccountId: uint64(accountID), Nick: nick, PlayerId: p.ID, Token: token, Plat: platform, DeviceId: device}
	var roomMode int32
	if p.RoomID > 0 {
		roomID := p.RoomID
		room, roomErr := s.store.RoomSnapshot(ctx, roomID)
		if roomErr != nil && !errors.Is(roomErr, sql.ErrNoRows) {
			s.log.Error("connect room restore lookup failed", "account_id", accountID, "player_id", p.ID, "room_id", roomID, "err", roomErr)
			return DispatchResult{}, fmt.Errorf("load reconnect room %d: %w", roomID, roomErr)
		}
		member := false
		if roomErr == nil {
			for _, roomPlayer := range room.Players {
				if roomPlayer.ID == p.ID {
					member = true
					break
				}
			}
		}
		if roomErr == nil && member {
			roomMode = room.Mode
		} else {
			cleared, clearErr := s.store.ClearStaleRoomReference(ctx, p.ID, roomID)
			if clearErr != nil {
				s.log.Error("connect stale room reference cleanup failed", "account_id", accountID, "player_id", p.ID, "room_id", roomID, "err", clearErr)
				return DispatchResult{}, fmt.Errorf("clear stale reconnect room %d: %w", roomID, clearErr)
			}
			if !cleared {
				s.log.Warn("connect room reference changed during restore", "account_id", accountID, "player_id", p.ID, "room_id", roomID)
				return DispatchResult{}, fmt.Errorf("reconnect room %d changed during session initialization", roomID)
			}
			s.log.Warn("connect stale room reference cleared", "account_id", accountID, "player_id", p.ID, "room_id", roomID,
				"room_missing", errors.Is(roomErr, sql.ErrNoRows), "membership_missing", !member)
			p.RoomID = 0
		}
	}
	player := s.playerMessage(p, p.RoomID, roomMode)
	taskState, err := s.store.TaskSnapshot(ctx, p.ID, now)
	if err != nil {
		return DispatchResult{}, err
	}
	player.Task = taskInfoMessage(taskState)
	userID := fmt.Sprint(accountID)
	if selfAccount != nil {
		userID = selfAccount.Number
	}
	loginData, err := json.Marshal(map[string]any{
		"content": map[string]any{"data": map[string]string{"userId": userID}},
	})
	if err != nil {
		return DispatchResult{}, fmt.Errorf("encode SDK login data: %w", err)
	}
	resp := &protocolpb.ConnectS2C{
		SessionId: sid, Account: account, Player: player, NowTime: time.Now().Unix(), Data: string(loginData),
	}
	if missing := missingConnectHomeFields(resp, selfAccount != nil); len(missing) > 0 {
		err := fmt.Errorf("incomplete ConnectS2C Home bootstrap: %s", strings.Join(missing, ", "))
		s.log.Error("connect home snapshot rejected", "auth_method", platform, "account_id", accountID, "player_id", p.ID, "missing_fields", missing, "err", err)
		return DispatchResult{}, err
	}
	// Do not publish the session as online until the complete client bootstrap
	// has been assembled. processFrame registers it only after it successfully
	// queues the ConnectS2C frame, so preparation failures cannot leave a ghost session.
	tutorialProgress := player.Task.Condition[1]
	_, hasTutorialProgress := player.Task.Condition[1]
	s.log.Info("connect home snapshot ready", "auth_method", platform, "account_id", accountID, "player_id", p.ID,
		"bag_items", len(player.BagItems), "role_cards", len(player.RoleCard), "fashion_plans", len(player.FashionPlan),
		"weekly_limit_entries", len(player.WeeklyLimits), "activity_pass_entries", len(player.ActivityPass), "alt_art_cards", len(player.AltArtCards),
		"task_condition_1_present", hasTutorialProgress, "task_condition_1", tutorialProgress, "tutorial_skipped", tutorialProgress > 0)
	pushes := []Push(nil)
	return DispatchResult{Message: resp, Err: ErrSucc, Pushes: pushes}, nil
}

func missingConnectHomeFields(response *protocolpb.ConnectS2C, requireHome bool) []string {
	missing := make([]string, 0, 10)
	if response == nil {
		return []string{"response"}
	}
	if response.GetSessionId() <= 0 {
		missing = append(missing, "session_id")
	}
	account := response.GetAccount()
	if account == nil {
		missing = append(missing, "account")
	}
	player := response.GetPlayer()
	if player == nil {
		return append(missing, "player")
	}
	if player.GetId() <= 0 {
		missing = append(missing, "player.id")
	}
	if account != nil && (account.GetPlayerId() <= 0 || account.GetPlayerId() != player.GetId()) {
		missing = append(missing, "account.player_id")
	}
	if strings.TrimSpace(player.GetNick()) == "" {
		missing = append(missing, "player.nick")
	}
	// Client account and Home initialization consume this snapshot immediately
	// after ConnectS2C. Keep even empty profile messages present so client code
	// sees a complete, stable first-login shape.
	if player.GetHero() == nil {
		missing = append(missing, "player.hero")
	}
	if len(player.GetRoleCard()) == 0 {
		missing = append(missing, "player.role_card")
	}
	if player.GetShopInfo() == nil {
		missing = append(missing, "player.shop_info")
	}
	if player.GetTask() == nil {
		missing = append(missing, "player.task")
	}
	// NOTE: ConditionType_NoviceLevelDone (1) is deliberately NOT required.
	// TutorialLogic.InitAccount starts the novice guide when GetAchieveInfo_1(1)
	// returns zero, which is the official new-account state; the flag is only
	// set once the client reports the guide finished (TeachingC2S). Requiring
	// it here forced a fake "tutorial done" marker into every first snapshot.
	if player.GetMissionMod() == nil {
		missing = append(missing, "player.mission_mod")
	}
	if player.GetFriends() == nil {
		missing = append(missing, "player.friends")
	}
	if player.GetBattlePass() == nil {
		missing = append(missing, "player.battle_pass")
	}
	if player.GetShowPlayer() == nil {
		missing = append(missing, "player.show_player")
	}
	if player.GetClientData() == nil {
		missing = append(missing, "player.client_data")
	}
	if player.GetSportsMeetInfo() == nil {
		missing = append(missing, "player.sports_meet_info")
	}
	if player.GetInviteInfo() == nil {
		missing = append(missing, "player.invite_info")
	}
	if player.GetSingleInfo() == nil {
		missing = append(missing, "player.single_info")
	}
	if player.GetGuildInfo() == nil {
		missing = append(missing, "player.guild_info")
	}
	if player.GetCreditInfo() == nil {
		missing = append(missing, "player.credit_info")
	}
	if player.GetReturnInfo() == nil {
		missing = append(missing, "player.return_info")
	}
	if player.GetPayAmountInfo() == nil {
		missing = append(missing, "player.pay_amount_info")
	}
	// These collections are read synchronously by GameLogicManager.InitAccount.
	// An empty collection is a valid first-login state; leaving one nil in the
	// server projection is a construction error even though proto3 omits it on
	// the wire and the generated Unity client normally initializes it empty.
	if player.GetWeeklyLimits() == nil {
		missing = append(missing, "player.weekly_limits")
	}
	if player.GetDay7() == nil {
		missing = append(missing, "player.day7")
	}
	if player.GetActivityPass() == nil {
		missing = append(missing, "player.activity_pass")
	}
	if player.GetAltArtCards() == nil {
		missing = append(missing, "player.alt_art_cards")
	}
	if player.GetScratchCard() == nil {
		missing = append(missing, "player.scratch_card")
	}
	if player.GetLightGift() == nil {
		missing = append(missing, "player.light_gift")
	}
	if player.GetFlipCard() == nil {
		missing = append(missing, "player.flip_card")
	}
	if len(player.GetFashionPlan()) == 0 {
		missing = append(missing, "player.fashion_plan")
	}
	var loginData struct {
		Content struct {
			Data struct {
				UserID string `json:"userId"`
			} `json:"data"`
		} `json:"content"`
	}
	if err := json.Unmarshal([]byte(response.GetData()), &loginData); err != nil || strings.TrimSpace(loginData.Content.Data.UserID) == "" {
		missing = append(missing, "data.content.data.userId")
	}
	return missing
}

func shortHash(v string) string { h := sha256.Sum256([]byte(v)); return hex.EncodeToString(h[:])[:8] }

// shortKey derives a short stable numeric suffix from a login key so
// auto-created guest accounts get a readable, persistent nickname.
func shortKey(key string) string {
	sum := sha256.Sum256([]byte(key))
	return fmt.Sprintf("%04d", binary.BigEndian.Uint32(sum[:4])%10000)
}

func randomToken() (string, error) {
	var b [16]byte
	if _, e := rand.Read(b[:]); e != nil {
		return "", e
	}
	return hex.EncodeToString(b[:]), nil
}
func randomSessionID() (int64, error) {
	for {
		n, e := rand.Int(rand.Reader, new(big.Int).SetUint64(1<<63-1))
		if e != nil {
			return 0, e
		}
		if n.Int64() > 0 {
			return n.Int64(), nil
		}
	}
}
func sessionPlayer(sess *Session) (int64, int64, bool) {
	_, account, pid, _, ok := sess.identity()
	return account, pid, ok
}

func (s *Server) handleCreateRoom(ctx context.Context, sess *Session, q *protocolpb.CreateRoomC2S) (DispatchResult, error) {
	_, pid, ok := sessionPlayer(sess)
	if !ok {
		return DispatchResult{Err: ErrAuth}, nil
	}
	mapID := q.MapId
	if mapID == 0 {
		if first, found := s.resources.FirstID("Map_infos"); found {
			mapID = int32(first)
		}
	}
	if known, loaded := s.resources.ValidateMap(int64(mapID)); loaded && !known {
		return DispatchResult{Err: ErrRoomMapNotExist}, nil
	}
	if !s.resources.Available() {
		s.log.Debug("resource rows not installed; skipping map data validation", "map_id", mapID)
	}
	settings := store.RoomCreate{Name: q.Name, Password: q.Pwd, MapID: mapID, MaxTime: q.MaxTime, UpgradePlan: q.UpgradePlan, TimePlan: q.TimePlan, Mode: q.Mode, LobbyID: q.LobbyId, SpeedType: q.SpeedType, Difficulty: q.Difficulty, SkipStory: q.SkipStory, RoomLabel: q.RoomLabel}
	p, err := s.store.LookupPlayer(ctx, pid)
	if err != nil {
		return DispatchResult{}, err
	}
	room, err := s.store.CreateRoom(ctx, p, settings)
	if err != nil {
		return DispatchResult{Err: mapRoomError(err)}, nil
	}
	return DispatchResult{Message: &protocolpb.CreateRoomS2C{Room: s.roomMessage(room)}}, nil
}

type roomSettingsMode struct {
	Mode struct {
		Value int32 `json:"value"`
	} `json:"mapModeType"`
	HasMap          bool    `json:"hasMap"`
	HasChoosingTime bool    `json:"hasChoosingTime"`
	HasUpgrade      bool    `json:"hasUpgrade"`
	HasDifficulty   bool    `json:"hasDifficulty"`
	HasLabel        []int32 `json:"hasLabel"`
}

func (s *Server) handleChangeRoom(ctx context.Context, sess *Session, q *protocolpb.ChangeRoomC2S) (DispatchResult, error) {
	_, playerID, ok := sessionPlayer(sess)
	if !ok {
		return DispatchResult{Err: ErrAuth}, nil
	}
	roomID, err := s.store.RoomForPlayer(ctx, playerID)
	if err != nil {
		return DispatchResult{Err: ErrRoomNotExist}, nil
	}
	room, err := s.store.RoomSnapshot(ctx, roomID)
	if err != nil {
		return DispatchResult{Err: ErrRoomNotExist}, nil
	}
	if room.MasterID != playerID {
		return DispatchResult{Err: ErrRoomActionPlayer}, nil
	}
	if room.State != 1 {
		return DispatchResult{Err: ErrRoomNotWait}, nil
	}
	if !s.validRoomSettings(room, q) {
		return DispatchResult{Err: ErrInvalidParam}, nil
	}
	updated, err := s.store.UpdateRoomSettings(ctx, roomID, playerID, store.RoomSettingsUpdate{
		Password: q.Pwd, MapID: q.MapId, UpgradePlan: q.UpgradePlan, TimePlan: q.TimePlan,
		SpeedType: q.SpeedType, Difficulty: q.Difficulty, SkipStory: q.SkipStory, RoomLabel: q.RoomLabel,
	})
	if err != nil {
		return DispatchResult{Err: mapRoomError(err)}, nil
	}
	message := &protocolpb.ChangeRoomS2C{Room: s.roomMessage(updated)}
	s.log.Info("room settings changed", "room_id", roomID, "player_id", playerID,
		"map_id", updated.MapID, "difficulty", updated.Difficulty, "room_label", updated.RoomLabel)
	push := s.pushFor("ChangeRoomS2C", message, mustMemberIDs(ctx, s.store, roomID), playerID)
	return DispatchResult{Message: message, Pushes: []Push{push}}, nil
}

func (s *Server) validRoomSettings(room store.Room, q *protocolpb.ChangeRoomC2S) bool {
	if q.GetMapId() <= 0 {
		return false
	}
	if !s.resources.Available() {
		return true
	}
	var mode roomSettingsMode
	raw, ok := s.resources.Get("ChoosingTimeLimit_roomsettings", int64(room.Mode))
	if !ok || json.Unmarshal(raw, &mode) != nil || mode.Mode.Value != room.Mode || !mode.HasMap {
		return false
	}
	mapRaw, ok := s.resources.Get("Map_infos", int64(q.GetMapId()))
	if !ok {
		return false
	}
	var mapInfo struct {
		Modes []int32 `json:"mids"`
	}
	if json.Unmarshal(mapRaw, &mapInfo) != nil || !containsInt32(mapInfo.Modes, room.Mode) {
		return false
	}
	if mode.HasChoosingTime {
		if _, ok = s.resources.Get("ChoosingTimeLimit_infos", int64(q.GetTimePlan())); !ok {
			return false
		}
		if _, ok = s.resources.Get("ChoosingTimeLimit_gamespeeds", int64(q.GetSpeedType())); !ok {
			return false
		}
	}
	if _, ok = s.resources.Get("Upgrade_datas", int64(q.GetUpgradePlan())); !ok {
		return false
	}
	if !mode.HasUpgrade && (room.Mode == 4 || room.Mode == 12) {
		pveRaw, found := s.resources.Get("GameMode_difficultyDatas", 4)
		if !found {
			return false
		}
		var pveDifficulty struct {
			Items []struct {
				Index     int32 `json:"index"`
				UpgradeID int32 `json:"UpgradeId"`
			} `json:"gameModeDifficultyDataConfigureItems"`
		}
		if json.Unmarshal(pveRaw, &pveDifficulty) != nil {
			return false
		}
		expectedUpgrade := int32(0)
		for _, item := range pveDifficulty.Items {
			if item.Index == q.GetDifficulty() {
				expectedUpgrade = item.UpgradeID
				break
			}
		}
		if expectedUpgrade == 0 || q.GetUpgradePlan() != expectedUpgrade {
			return false
		}
	}
	if mode.HasDifficulty {
		difficultyRaw, found := s.resources.Get("Map_gameDifficultys", int64(q.GetMapId()))
		if !found {
			return false
		}
		var difficultyData struct {
			Items []struct {
				Index int32 `json:"index"`
			} `json:"mapGameDifficultyConfigureItems"`
		}
		if json.Unmarshal(difficultyRaw, &difficultyData) != nil {
			return false
		}
		valid := false
		for _, item := range difficultyData.Items {
			if item.Index == q.GetDifficulty() {
				valid = true
				break
			}
		}
		if !valid {
			return false
		}
	} else if q.GetDifficulty() != 0 {
		return false
	}
	if q.GetSkipStory() && q.GetMapId() != 82013 && q.GetMapId() != 82015 {
		return false
	}
	if len(mode.HasLabel) == 0 {
		return q.GetRoomLabel() == 0
	}
	return q.GetRoomLabel() >= 0 && int(q.GetRoomLabel()) < len(mode.HasLabel)
}

func containsInt32(values []int32, target int32) bool {
	for _, value := range values {
		if value == target {
			return true
		}
	}
	return false
}

func (s *Server) handleJoinRoom(ctx context.Context, sess *Session, q *protocolpb.JoinRoomC2S) (DispatchResult, error) {
	_, pid, ok := sessionPlayer(sess)
	if !ok {
		return DispatchResult{Err: ErrAuth}, nil
	}
	p, err := s.store.LookupPlayer(ctx, pid)
	if err != nil {
		return DispatchResult{}, err
	}
	room, err := s.store.JoinRoom(ctx, q.RoomId, p, q.Slot, q.Pwd)
	if err != nil {
		return DispatchResult{Err: mapRoomError(err)}, nil
	}
	push := s.pushFor("RoomNotifyS2C", &protocolpb.RoomNotifyS2C{Room: s.roomMessage(room)}, mustMemberIDs(ctx, s.store, room.ID), pid)
	return DispatchResult{Message: &protocolpb.JoinRoomS2C{Room: s.roomMessage(room)}, Pushes: []Push{push}}, nil
}
func (s *Server) handleQueryRoom(ctx context.Context, q *protocolpb.QueryRoomC2S) (DispatchResult, error) {
	rooms, err := s.store.ListRoomsByMode(ctx, q.GetMapMod(), 50)
	if err != nil {
		return DispatchResult{}, err
	}
	out := &protocolpb.QueryRoomS2C{}
	for _, r := range rooms {
		out.Items = append(out.Items, shortRoomMessage(r))
	}
	return DispatchResult{Message: out}, nil
}
func (s *Server) handleSyncRoom(ctx context.Context, sess *Session, q *protocolpb.SyncRoomC2S) (out DispatchResult, retErr error) {
	_, pid, ok := sessionPlayer(sess)
	if !ok {
		return DispatchResult{Err: ErrAuth}, nil
	}
	id := q.RoomId
	if id == 0 {
		var e error
		id, e = s.store.RoomForPlayer(ctx, pid)
		if e != nil {
			// Not in any room yet: reply with an empty room instead of an
			// error. The client closes the connection on a 11001-class error
			// here (ReconnectNet handling in NetManager), which would drop
			// players that simply have not joined a room after login.
			return DispatchResult{Message: &protocolpb.SyncRoomS2C{}}, nil
		}
	}
	r, e := s.store.RoomSnapshot(ctx, id)
	if e != nil {
		// Unknown/stale room id: same reasoning as above, prefer an empty
		// success over a disconnect-triggering error.
		return DispatchResult{Message: &protocolpb.SyncRoomS2C{}}, nil
	}
	inRoom, e := s.store.PlayerInRoom(ctx, id, pid)
	if e != nil {
		return DispatchResult{}, e
	}
	if !inRoom {
		return DispatchResult{Err: ErrRoomPlayerNotExist}, nil
	}
	defer func() {
		if retErr == nil && out.Err == 0 && out.Message != nil && sess.acknowledgeInitialOnlineSync(id) {
			s.log.Info("online room state confirmed", "player_id", pid, "room_id", id, "via", "valid_sync_room_request")
		}
	}()
	result := DispatchResult{Message: &protocolpb.SyncRoomS2C{Room: s.roomMessage(r)}}
	if r.State == 25 {
		current, round, pending, turnErr := s.store.CurrentTurn(ctx, id)
		if turnErr != nil {
			s.log.Error("running room sync has no active turn", "room_id", id, "player_id", pid, "err", turnErr)
			return DispatchResult{Err: ErrRoomNotAction}, nil
		}
		phase, phaseErr := s.store.CurrentTurnPhase(ctx, id)
		if phaseErr != nil {
			s.log.Error("running room sync has no active turn phase", "room_id", id, "player_id", pid, "err", phaseErr)
			return DispatchResult{Err: ErrRoomNotAction}, nil
		}
		if phase == store.TurnPhaseSelectEvent {
			if current == pid {
				choices, actionSN, exists, offerErr := s.store.SkillEventOffer(ctx, id, pid)
				if offerErr != nil || !exists {
					s.log.Error("pending skill event offer could not be restored", "room_id", id, "player_id", pid, "err", offerErr)
					return DispatchResult{Err: ErrRoomNotAction}, nil
				}
				result.Pushes = append(result.Pushes, s.actionPushWithSN(round, 5317, pid, &protocolpb.SelectEventC2S{Events: choices, Idx: 0}, actionSN))
			}
			return result, nil
		}
		if phase == store.TurnPhaseControlMove {
			if current != pid {
				return result, nil
			}
			prompt, exists, promptErr := s.store.ControlMovePrompt(ctx, id, pid, round)
			if promptErr != nil || !exists {
				s.log.Error("pending controlled-movement prompt could not be restored", "room_id", id, "player_id", pid, "err", promptErr)
				return DispatchResult{Err: ErrRoomNotAction}, nil
			}
			result.Pushes = append(result.Pushes, s.actionPushWithSN(round, 5067, pid, &protocolpb.ThrowDiceResultC2S{MaxPoint: prompt.MaxPoint}, prompt.ActionSN))
			return result, nil
		}
		if phase == store.TurnPhaseBattleChallenge || phase == store.TurnPhaseBattleCards || phase == store.TurnPhaseBattleAttack || phase == store.TurnPhaseBattleDefense {
			if r.Battle == nil || r.Battle.Stage != phase {
				s.log.Error("running room sync has no battle for active phase", "room_id", id, "player_id", pid, "phase", phase)
				return DispatchResult{Err: ErrRoomNotAction}, nil
			}
			result.Pushes = append(result.Pushes, s.resumeBattleForPlayer(r, round, pid)...)
			return result, nil
		}
		if phase == store.TurnPhaseGambleGuess || phase == store.TurnPhaseGambleThrow {
			botPushes, completed, nextPlayer, nextRound, botErr := s.resolveGambleBots(ctx, id)
			if botErr != nil {
				return DispatchResult{}, botErr
			}
			result.Pushes = append(result.Pushes, botPushes...)
			if completed {
				result.Pushes = append(result.Pushes, s.turnPushes(ctx, id, nextRound, nextPlayer)...)
			} else {
				resumePushes, resumeErr := s.resumeGambleForPlayer(ctx, id, pid, round)
				if resumeErr != nil {
					return DispatchResult{}, resumeErr
				}
				result.Pushes = append(result.Pushes, resumePushes...)
			}
			r, e = s.store.RoomSnapshot(ctx, id)
			if e != nil {
				return DispatchResult{}, e
			}
			result.Message = &protocolpb.SyncRoomS2C{Room: s.roomMessage(r)}
			return result, nil
		}
		if phase == store.TurnPhaseThrowDice && pending == 0 {
			var currentPlayer store.Player
			for _, member := range r.Players {
				if member.ID == current {
					currentPlayer = member
					break
				}
			}
			if currentPlayer.ID != 0 {
				startPushes, startErr := s.resolveTurnStartEffects(ctx, id, round, r, currentPlayer, mustMemberIDs(ctx, s.store, id))
				if startErr != nil {
					return DispatchResult{}, startErr
				}
				result.Pushes = append(result.Pushes, startPushes...)
				if len(startPushes) > 0 {
					r, e = s.store.RoomSnapshot(ctx, id)
					if e != nil {
						return DispatchResult{}, e
					}
					result.Message = &protocolpb.SyncRoomS2C{Room: s.roomMessage(r)}
				}
			}
		}
		if phase == store.TurnPhaseLandHeal {
			var landID int32
			for _, member := range r.Players {
				if member.ID == current {
					landID = member.NodeID
					break
				}
			}
			landType, exists := s.domain.LandTypeAt(r, landID)
			if landID < 0 || !exists || landType != 20 {
				s.log.Error("pending Heal phase is not on a Heal land", "room_id", id, "player_id", current, "land_id", landID)
				return DispatchResult{Err: ErrRoomNotAction}, nil
			}
			healed, healErr := s.resolveLandHeal(ctx, id, current)
			if healErr != nil {
				if errors.Is(healErr, errLandHealConfigMissing) {
					return DispatchResult{Err: ErrNotOpen}, nil
				}
				return DispatchResult{}, healErr
			}
			ids := mustMemberIDs(ctx, s.store, id)
			result.Pushes = append(result.Pushes, s.pushFor("UpdateHeroAttrS2C", landHealAttrUpdate(current, landID, healed), ids, 0))
			result.Pushes = append(result.Pushes, s.turnPushes(ctx, id, healed.Round, healed.NextPlayer)...)
			r, e = s.store.RoomSnapshot(ctx, id)
			if e != nil {
				return DispatchResult{}, e
			}
			result.Message = &protocolpb.SyncRoomS2C{Room: s.roomMessage(r)}
			return result, nil
		}
		if phase == store.TurnPhaseLandBloodLoss {
			var landID int32
			for _, member := range r.Players {
				if member.ID == current {
					landID = member.NodeID
					break
				}
			}
			landType, exists := s.domain.LandTypeAt(r, landID)
			if landID < 0 || !exists || landType != 17 {
				s.log.Error("pending BloodLoss phase is not on a BloodLoss land", "room_id", id, "player_id", current, "land_id", landID)
				return DispatchResult{Err: ErrRoomNotAction}, nil
			}
			damaged, damageErr := s.resolveLandBloodLoss(ctx, id, current)
			if damageErr != nil {
				if errors.Is(damageErr, errLandBloodLossConfigMissing) {
					return DispatchResult{Err: ErrNotOpen}, nil
				}
				return DispatchResult{}, damageErr
			}
			ids := mustMemberIDs(ctx, s.store, id)
			result.Pushes = append(result.Pushes, s.pushFor("UpdateHeroAttrS2C", landBloodLossAttrUpdate(current, landID, damaged), ids, 0))
			result.Pushes = append(result.Pushes, s.turnPushes(ctx, id, damaged.Round, damaged.NextPlayer)...)
			r, e = s.store.RoomSnapshot(ctx, id)
			if e != nil {
				return DispatchResult{}, e
			}
			result.Message = &protocolpb.SyncRoomS2C{Room: s.roomMessage(r)}
			return result, nil
		}
		currentIsBot := false
		currentIsDead := false
		currentIsHospitalized := false
		for _, member := range r.Players {
			if member.ID == current {
				currentIsBot = member.IsBot
				currentIsDead = member.HP <= 0
				currentIsHospitalized = member.HospitalRounds > 0
				break
			}
		}
		if currentIsBot || phase == store.TurnPhaseHospital || phase == store.TurnPhaseDivination || (currentIsHospitalized && pending == 0 && phase == store.TurnPhaseThrowDice) || (currentIsDead && pending == 0 && phase == store.TurnPhaseThrowDice) {
			result.Pushes = append(result.Pushes, s.turnPushes(ctx, id, round, current)...)
			if currentIsDead || currentIsHospitalized || phase == store.TurnPhaseHospital || phase == store.TurnPhaseDivination {
				r, e = s.store.RoomSnapshot(ctx, id)
				if e != nil {
					return DispatchResult{}, e
				}
				result.Message = &protocolpb.SyncRoomS2C{Room: s.roomMessage(r)}
				return result, nil
			}
		} else {
			result.Pushes = append(result.Pushes, s.pushFor("GameRoundChangeS2C", &protocolpb.GameRoundChangeS2C{Round: round}, []int64{pid}, 0))
			result.Pushes = append(result.Pushes, s.pushFor("ActionStartNotifyS2C", &protocolpb.ActionStartNotifyS2C{PlayerId: current}, []int64{pid}, 0))
			if current == pid {
				result.Pushes = append(result.Pushes, s.pushFor("RoundStartS2C", s.roundStartMessage(ctx, id, round, pid), []int64{pid}, 0))
				if phase == store.TurnPhaseAbandonCards {
					var active store.Player
					for _, member := range r.Players {
						if member.ID == current {
							active = member
							break
						}
					}
					handLimit, found := s.domain.CardInHandLimit(r.Mode)
					if !found {
						handLimit, found = s.resources.GlobalInt("GAME_CARDINHAND_LIMIT")
					}
					if active.ID == 0 || !found || handLimit <= 0 || len(active.Cards) <= int(handLimit) {
						s.log.Error("pending card discard phase has no valid excess hand", "room_id", id, "player_id", current, "hand_count", len(active.Cards), "hand_limit", handLimit)
						return DispatchResult{Err: ErrRoomNotAction}, nil
					}
					result.Pushes = append(result.Pushes, s.actionPush(round, 5075, pid, &protocolpb.AbandonCardC2S{}))
				} else if phase == store.TurnPhaseFillingStation {
					result.Pushes = append(result.Pushes, s.actionPush(round, 5077, pid, &protocolpb.StopOrContinueC2S{}))
				} else if phase == store.TurnPhaseEvent {
					result.Pushes = append(result.Pushes, s.actionPush(round, 5053, pid, &protocolpb.TriggerEventC2S{}))
				} else if phase == store.TurnPhaseDestiny {
					landID := roomPlayerNodeID(r, current)
					landType, exists := s.domain.LandTypeAt(r, landID)
					if !exists || landType != 16 {
						s.log.Error("pending Destiny phase is not on a Destiny land", "room_id", id, "player_id", current, "land_id", landID)
						return DispatchResult{Err: ErrRoomNotAction}, nil
					}
					result.Pushes = append(result.Pushes, s.actionPush(round, 5071, pid, &protocolpb.TriggerDestinyC2S{}))
				} else if phase == store.TurnPhaseLottery {
					var currentLandID int32
					for _, member := range r.Players {
						if member.ID == current {
							currentLandID = member.NodeID
							break
						}
					}
					landType, exists := s.domain.LandTypeAt(r, currentLandID)
					if !exists || landType != 9 {
						s.log.Error("pending lottery phase is not on a Lottery land", "room_id", id, "player_id", current, "land_id", currentLandID)
						return DispatchResult{Err: ErrRoomNotAction}, nil
					}
					chooseCount, numberLimit, settingsErr := s.lotterySettings()
					if errors.Is(settingsErr, errLotteryConfigMissing) {
						return DispatchResult{Err: ErrNotOpen}, nil
					}
					if settingsErr != nil {
						return DispatchResult{}, settingsErr
					}
					for _, member := range r.Players {
						if member.ID == current && lotteryHasCapacity(member, chooseCount, numberLimit) {
							result.Pushes = append(result.Pushes, s.actionPush(round, 5041, pid, &protocolpb.LotteryChoiceC2S{Num: chooseCount}))
							break
						}
					}
				} else if phase == store.TurnPhasePursuit {
					var currentLandID int32
					for _, member := range r.Players {
						if member.ID == current {
							currentLandID = member.NodeID
							break
						}
					}
					landType, exists := s.domain.LandTypeAt(r, currentLandID)
					if !exists || landType != 4 {
						s.log.Error("pending pursuit phase is not on a Pursuit land", "room_id", id, "player_id", current, "land_id", currentLandID)
						return DispatchResult{Err: ErrRoomNotAction}, nil
					}
					result.Pushes = append(result.Pushes, s.actionPush(round, 5033, pid, &protocolpb.PursuitC2S{}))
				} else if phase == store.TurnPhaseBattery {
					var actor store.Player
					for _, member := range r.Players {
						if member.ID == current {
							actor = member
							break
						}
					}
					landType, exists := s.domain.LandTypeAt(r, actor.NodeID)
					if actor.ID == 0 || !exists || landType != batteryLandType {
						s.log.Error("pending Battery phase is not on a Battery land", "room_id", id, "player_id", current, "land_id", actor.NodeID)
						return DispatchResult{Err: ErrRoomNotAction}, nil
					}
					data, available := s.batteryActionData(r, actor)
					if !available {
						s.log.Error("pending Battery phase has no eligible targets", "room_id", id, "player_id", current, "land_id", actor.NodeID)
						return DispatchResult{Err: ErrRoomNotAction}, nil
					}
					result.Pushes = append(result.Pushes, s.actionPush(round, 5063, pid, data))
				} else if phase == store.TurnPhaseShop {
					var currentLandID int32
					for _, member := range r.Players {
						if member.ID == current {
							currentLandID = member.NodeID
							break
						}
					}
					landType, exists := s.domain.LandTypeAt(r, currentLandID)
					if !exists || landType != 7 {
						s.log.Error("pending shop phase is not on a Shop land", "room_id", id, "player_id", current, "land_id", currentLandID)
						return DispatchResult{Err: ErrRoomNotAction}, nil
					}
					shop, shopErr := s.store.ActiveShop(ctx, id, current)
					if shopErr != nil {
						s.log.Error("pending shop offer could not be restored", "room_id", id, "player_id", current, "err", shopErr)
						return DispatchResult{Err: ErrRoomNotAction}, nil
					}
					result.Pushes = append(result.Pushes, s.actionPush(round, 5029, pid, shopActionData(shop)))
				} else if phase == store.TurnPhasePVEShop {
					var currentLandID int32
					for _, member := range r.Players {
						if member.ID == current {
							currentLandID = member.NodeID
							break
						}
					}
					landType, exists := s.domain.LandTypeAt(r, currentLandID)
					if !exists || landType != 22 {
						s.log.Error("pending PVE shop phase is not on a PVE shop land", "room_id", id, "player_id", current, "land_id", currentLandID)
						return DispatchResult{Err: ErrRoomNotAction}, nil
					}
					shop, shopErr := s.store.ActiveShop(ctx, id, current)
					if shopErr != nil || !shop.PVE {
						s.log.Error("pending PVE shop offer could not be restored", "room_id", id, "player_id", current, "err", shopErr)
						return DispatchResult{Err: ErrRoomNotAction}, nil
					}
					result.Pushes = append(result.Pushes, s.actionPush(round, 5215, pid, pveShopActionData(shop)))
				} else if pending > 0 {
					result.Pushes = append(result.Pushes, s.actionPush(round, 5027, pid, &protocolpb.MoveC2S{}))
				} else if phase == store.TurnPhaseMoveAgain {
					result.Pushes = append(result.Pushes, s.actionPush(round, 5043, pid, &protocolpb.MoveAgainC2S{}))
				} else if phase == store.TurnPhaseRollGold {
					result.Pushes = append(result.Pushes, s.actionPush(round, 5049, pid, &protocolpb.RollGoldC2S{}))
				} else {
					result.Pushes = append(result.Pushes, s.movementActionPush(ctx, id, round, pid))
				}
			}
		}
	}
	return result, nil
}
func (s *Server) handleQuickJoin(ctx context.Context, sess *Session, q *protocolpb.QuickJoinRoomC2S) (DispatchResult, error) {
	_, pid, ok := sessionPlayer(sess)
	if !ok {
		return DispatchResult{Err: ErrAuth}, nil
	}
	p, err := s.store.LookupPlayer(ctx, pid)
	if err != nil {
		return DispatchResult{}, err
	}
	rooms, err := s.store.ListRooms(ctx, 50)
	if err != nil {
		return DispatchResult{}, err
	}
	for _, r := range rooms {
		if r.Password != "" || r.Mode != 0 && r.Mode != q.MapMod {
			continue
		}
		room, e := s.store.JoinRoom(ctx, r.ID, p, 0, "")
		if e == nil {
			return DispatchResult{Message: &protocolpb.QuickJoinRoomS2C{Room: s.roomMessage(room)}}, nil
		}
	}
	settings := store.RoomCreate{Name: nickRoomName(p.Nick), MapID: 0, Mode: q.MapMod, MaxTime: 0}
	room, err := s.store.CreateRoom(ctx, p, settings)
	if err != nil {
		return DispatchResult{Err: mapRoomError(err)}, nil
	}
	return DispatchResult{Message: &protocolpb.QuickJoinRoomS2C{Room: s.roomMessage(room)}}, nil
}
func nickRoomName(n string) string {
	if n == "" {
		return "Quick Room"
	}
	return n + "'s Room"
}
func (s *Server) handleRoomReady(ctx context.Context, sess *Session, q *protocolpb.RoomReadyC2S) (DispatchResult, error) {
	_, pid, ok := sessionPlayer(sess)
	if !ok {
		return DispatchResult{Err: ErrAuth}, nil
	}
	roomID, err := s.store.RoomForPlayer(ctx, pid)
	if err != nil {
		return DispatchResult{Err: ErrRoomNotExist}, nil
	}
	room, err := s.store.SetReady(ctx, roomID, pid, q.IsReady)
	if err != nil {
		return DispatchResult{Err: ErrRoomPlayerNotExist}, nil
	}
	msg := &protocolpb.RoomReadyS2C{PlayerId: pid, IsReady: q.IsReady}
	push := s.pushFor("RoomReadyS2C", msg, mustMemberIDs(ctx, s.store, room.ID), pid)
	return DispatchResult{Message: msg, Pushes: []Push{push}}, nil
}
func (s *Server) handleStartGame(ctx context.Context, sess *Session, q *protocolpb.StartGameC2S) (DispatchResult, error) {
	_, pid, ok := sessionPlayer(sess)
	if !ok {
		return DispatchResult{Err: ErrAuth}, nil
	}
	roomID, err := s.store.RoomForPlayer(ctx, pid)
	if err != nil {
		return DispatchResult{Err: ErrRoomNotExist}, nil
	}
	before, err := s.store.RoomSnapshot(ctx, roomID)
	if err != nil {
		return DispatchResult{Err: ErrRoomNotExist}, nil
	}
	fillBots := q.IsAddBot || (s.BotAutoFill() && len(before.Players) == 1)
	room, err := s.store.StartRoom(ctx, roomID, pid, fillBots)
	if err != nil {
		return DispatchResult{Err: mapRoomError(err)}, nil
	}
	if fillBots {
		heroIDs := s.resources.DefaultHeroIDs()
		heroMaxHP := make(map[int32]int32, len(heroIDs))
		for _, heroID := range heroIDs {
			heroMaxHP[heroID] = s.resources.HeroMaxHP(heroID)
		}
		if err = s.store.ConfigureRoomBots(ctx, roomID, heroIDs, heroMaxHP); err != nil {
			s.log.Error("room bots could not be prepared", "room_id", roomID, "err", err)
			return DispatchResult{Err: ErrRoomHeroNotChoice}, nil
		}
		room, err = s.store.RoomSnapshot(ctx, roomID)
		if err != nil {
			return DispatchResult{}, err
		}
	}
	s.log.Info("room entered hero selection", "room_id", room.ID, "player_count", len(room.Players), "bots_added", fillBots)
	msg := &protocolpb.StartGameS2C{Room: s.roomMessage(room)}
	return DispatchResult{Message: msg, Pushes: []Push{s.pushFor("StartGameS2C", msg, memberIDs(room), pid)}}, nil
}
func (s *Server) handleExitRoom(ctx context.Context, sess *Session, q *protocolpb.ExitRoomC2S) (DispatchResult, error) {
	_, pid, ok := sessionPlayer(sess)
	if !ok {
		return DispatchResult{Err: ErrAuth}, nil
	}
	roomID, err := s.store.RoomForPlayer(ctx, pid)
	if err != nil {
		return DispatchResult{Message: &protocolpb.ExitRoomS2C{}, Err: ErrSucc}, nil
	}
	master, dissolve, err := s.store.ExitRoom(ctx, roomID, pid)
	if err != nil {
		return DispatchResult{}, err
	}
	msg := &protocolpb.ExitRoomS2C{RoomId: roomID, MasterId: master, PlayerId: pid, Dissolve: dissolve}
	var pushes []Push
	if !dissolve {
		ids := mustMemberIDs(ctx, s.store, roomID)
		pushes = append(pushes, s.pushFor("ExitRoomS2C", msg, ids, 0))
	}
	return DispatchResult{Message: msg, Pushes: pushes}, nil
}
func (s *Server) handleRefreshRoom(ctx context.Context, sess *Session, q *protocolpb.RefreshRoomStateC2S) (DispatchResult, error) {
	_, pid, ok := sessionPlayer(sess)
	if !ok {
		return DispatchResult{Err: ErrAuth}, nil
	}
	roomID, err := s.store.RoomForPlayer(ctx, pid)
	if err != nil {
		return DispatchResult{Err: ErrRoomNotExist}, nil
	}
	room, err := s.store.RoomSnapshot(ctx, roomID)
	if err != nil {
		return DispatchResult{Err: ErrRoomNotExist}, nil
	}
	positions, err := s.domain.InitialPositions(room)
	if err != nil {
		s.log.Error("could not resolve authoritative board spawn points", "room_id", roomID, "map_id", room.MapID, "err", err)
		return DispatchResult{Err: ErrRoomMapNotExist}, nil
	}
	maxHPs := make(map[int64]int32, len(room.Players))
	for _, player := range room.Players {
		maxHPs[player.ID] = s.domain.HeroMaxHP(player.HeroID)
	}
	var initialCards map[int64][]store.CardState
	var initialGold map[int64]int32
	if q.Progress >= 100 && room.State < 25 {
		initialCards, initialGold, err = s.initialGameState(room)
		if err != nil {
			s.log.Error("could not resolve initial game state", "room_id", roomID, "map_id", room.MapID, "mode", room.Mode, "err", err)
			return DispatchResult{Err: ErrNotOpen}, nil
		}
	}
	gameMaxProgress, _ := s.domain.MapProgressLimit(int64(room.MapID), room.Difficulty)
	r, started, err := s.store.SetAssetProgress(ctx, roomID, pid, q.Progress, gameMaxProgress, positions, maxHPs, initialGold, initialCards)
	if err != nil {
		if strings.Contains(strings.ToLower(err.Error()), "not exist") {
			return DispatchResult{Err: ErrRoomNotExist}, nil
		}
		return DispatchResult{Err: ErrRoomNotReady}, nil
	}
	m := &protocolpb.RefreshRoomStateS2C{RoomId: roomID, PlayerId: pid, Progress: q.Progress, State: modelpb.Room_State(r.State), PlayerIds: make([]int64, 0, len(r.Players))}
	for _, p := range r.Players {
		m.PlayerIds = append(m.PlayerIds, p.ID)
	}
	result := DispatchResult{Message: m}
	if started {
		ids := mustMemberIDs(ctx, s.store, roomID)
		result.Pushes = append(result.Pushes, s.pushFor("RunningGameS2C", &protocolpb.RunningGameS2C{Room: s.roomMessage(r)}, ids, 0))
		current, round, _, e := s.store.CurrentTurn(ctx, roomID)
		if e != nil {
			return DispatchResult{}, e
		}
		result.Pushes = append(result.Pushes, s.turnPushes(ctx, roomID, round, current)...)
		s.log.Info("room battle started after asset synchronization", "room_id", roomID, "players", len(ids))
	}
	return result, nil
}
func (s *Server) handleThrowDice(ctx context.Context, sess *Session, in wire.Frame, q *protocolpb.ThrowDiceC2S) (DispatchResult, error) {
	_, pid, ok := sessionPlayer(sess)
	if !ok {
		return DispatchResult{Err: ErrAuth}, nil
	}
	roomID, err := s.store.RoomForPlayer(ctx, pid)
	if err != nil {
		return DispatchResult{Err: ErrRoomNotAction}, nil
	}
	current, round, pending, err := s.store.CurrentTurn(ctx, roomID)
	if err != nil {
		return DispatchResult{Err: ErrRoomNotAction}, nil
	}
	if current != pid {
		return DispatchResult{Err: ErrRoomActionPlayer}, nil
	}
	if pending > 0 {
		return DispatchResult{Err: ErrRoomActionAlreadyDone}, nil
	}
	cardTurnDone, err := s.store.EffectCardTurnDone(ctx, roomID, pid, round)
	if err != nil {
		return DispatchResult{}, err
	}
	if !cardTurnDone {
		return DispatchResult{Err: ErrRoomActionIncorrect}, nil
	}
	room, err := s.store.RoomSnapshot(ctx, roomID)
	if err != nil {
		return DispatchResult{Err: ErrRoomNotAction}, nil
	}
	diceCount := 1
	var heroID int32
	for _, player := range room.Players {
		if player.ID != pid {
			continue
		}
		heroID = player.HeroID
		for _, buff := range player.Buffs {
			if buff.BuffID == 3000201 {
				diceCount = 2
				break
			}
		}
		break
	}
	vals, point, err := randomMovementDice(diceCount)
	if err != nil {
		return DispatchResult{}, err
	}
	controlPoint, controlled, err := s.store.ControlMoveChoice(ctx, roomID, pid, round)
	if err != nil {
		return DispatchResult{}, err
	}
	if controlled {
		point = controlPoint
	}
	goldBonus := int32(0)
	var rewardSkillID int32
	var rewardCards []store.CardState
	if !controlled {
		rewardSkillID, rewardCards, err = s.movementPassiveCardReward(heroID, room.Mode, point)
		if err != nil {
			return DispatchResult{}, err
		}
		if point == 6 && s.resources.HeroHasPassiveSkill(heroID, 11411, usesPVEPassiveSkills(room.Mode)) {
			goldBonus = 6
		}
	}
	raw, err := proto.Marshal(q)
	if err != nil {
		return DispatchResult{}, err
	}
	var goldResult store.MoveGoldResult
	var cardResult store.MoveCardsResult
	var created bool
	var consumedBuff *store.BuffState
	if controlled {
		created, consumedBuff, err = s.store.BeginMoveRollWithPoint(ctx, roomID, pid, in.CmdID, in.UPSN, raw, vals, point, store.TurnPhaseThrowDice, goldBonus, rewardCards, &goldResult, &cardResult)
	} else {
		created, consumedBuff, err = s.store.BeginMoveRoll(ctx, roomID, pid, in.CmdID, in.UPSN, raw, vals, store.TurnPhaseThrowDice, goldBonus, rewardCards, &goldResult, &cardResult)
	}
	if errors.Is(err, store.ErrTurnPlayerMismatch) {
		return DispatchResult{Err: ErrRoomActionPlayer}, nil
	}
	if errors.Is(err, store.ErrMoveAlreadyRolled) {
		return DispatchResult{Err: ErrRoomActionAlreadyDone}, nil
	}
	if errors.Is(err, store.ErrActionNotReady) {
		return DispatchResult{Err: ErrRoomActionIncorrect}, nil
	}
	if err != nil {
		return DispatchResult{}, err
	}
	if !created {
		return DispatchResult{Err: ErrRoomActionAlreadyDone}, nil
	}
	msg := &protocolpb.ThrowDiceS2C{Vals: vals, MovePoint: point, PlayerId: pid, IsControlMovePoint: controlled}
	ids := mustMemberIDs(ctx, s.store, roomID)
	pushes := make([]Push, 0, 3)
	if consumedBuff != nil {
		pushes = append(pushes, s.consumedDiceBuffPush(pid, *consumedBuff, ids))
	}
	pushes = append(pushes, s.pushFor("ThrowDiceS2C", msg, ids, pid))
	if goldResult.Changed {
		pushes = append(pushes, s.passiveGoldPush(pid, 11411, goldResult.OldGold, goldResult.NewGold, ids))
		s.log.Info("movement passive reward", "room_id", roomID, "player_id", pid, "skill_id", 11411, "gold", goldResult.NewGold-goldResult.OldGold)
	}
	if cardResult.Changed {
		pushes = append(pushes, s.pushFor("UpdateHeroAttrS2C", movementSkillCardRewardAttrUpdate(pid, rewardSkillID, cardResult.Cards), ids, 0))
		s.log.Info("movement passive card reward", "room_id", roomID, "player_id", pid, "skill_id", rewardSkillID, "card_id", cardResult.Cards[0].CardID)
	}
	pushes = append(pushes, s.actionPush(round, 5027, pid, &protocolpb.MoveC2S{}))
	return DispatchResult{Message: msg, Pushes: pushes}, nil
}
func (s *Server) handleMoveAgain(ctx context.Context, sess *Session, in wire.Frame, q *protocolpb.MoveAgainC2S) (DispatchResult, error) {
	_, pid, ok := sessionPlayer(sess)
	if !ok {
		return DispatchResult{Err: ErrAuth}, nil
	}
	roomID, err := s.store.RoomForPlayer(ctx, pid)
	if err != nil {
		return DispatchResult{Err: ErrRoomNotAction}, nil
	}
	current, _, _, err := s.store.CurrentTurn(ctx, roomID)
	if err != nil {
		return DispatchResult{Err: ErrRoomNotAction}, nil
	}
	if current != pid {
		return DispatchResult{Err: ErrRoomActionPlayer}, nil
	}
	room, err := s.store.RoomSnapshot(ctx, roomID)
	if err != nil {
		return DispatchResult{}, err
	}
	heroID := int32(0)
	for _, player := range room.Players {
		if player.ID == pid {
			heroID = player.HeroID
			break
		}
	}
	vals, point, err := randomMovementDice(1)
	if err != nil {
		return DispatchResult{}, err
	}
	raw, err := proto.Marshal(q)
	if err != nil {
		return DispatchResult{}, err
	}
	goldBonus := int32(0)
	if point == 6 && s.resources.HeroHasPassiveSkill(heroID, 11411, usesPVEPassiveSkills(room.Mode)) {
		goldBonus = 6
	}
	rewardSkillID, rewardCards, err := s.movementPassiveCardReward(heroID, room.Mode, point)
	if err != nil {
		return DispatchResult{}, err
	}
	var goldResult store.MoveGoldResult
	var cardResult store.MoveCardsResult
	created, _, err := s.store.BeginMoveRoll(ctx, roomID, pid, in.CmdID, in.UPSN, raw, vals, store.TurnPhaseMoveAgain, goldBonus, rewardCards, &goldResult, &cardResult)
	if errors.Is(err, store.ErrTurnPlayerMismatch) {
		return DispatchResult{Err: ErrRoomActionPlayer}, nil
	}
	if errors.Is(err, store.ErrMoveAlreadyRolled) {
		return DispatchResult{Err: ErrRoomActionAlreadyDone}, nil
	}
	if errors.Is(err, store.ErrActionNotReady) {
		return DispatchResult{Err: ErrRoomActionIncorrect}, nil
	}
	if err != nil {
		return DispatchResult{}, err
	}
	if !created {
		return DispatchResult{Err: ErrRoomActionAlreadyDone}, nil
	}
	msg := &protocolpb.MoveAgainS2C{PlayerId: pid, MovePoint: point}
	_, round, _, err := s.store.CurrentTurn(ctx, roomID)
	if err != nil {
		return DispatchResult{}, err
	}
	ids := mustMemberIDs(ctx, s.store, roomID)
	pushes := []Push{s.pushFor("MoveAgainS2C", msg, ids, pid)}
	if goldResult.Changed {
		pushes = append(pushes, s.passiveGoldPush(pid, 11411, goldResult.OldGold, goldResult.NewGold, ids))
		s.log.Info("movement passive reward", "room_id", roomID, "player_id", pid, "skill_id", 11411, "gold", goldResult.NewGold-goldResult.OldGold)
	}
	if cardResult.Changed {
		pushes = append(pushes, s.pushFor("UpdateHeroAttrS2C", movementSkillCardRewardAttrUpdate(pid, rewardSkillID, cardResult.Cards), ids, 0))
		s.log.Info("movement passive card reward", "room_id", roomID, "player_id", pid, "skill_id", rewardSkillID, "card_id", cardResult.Cards[0].CardID)
	}
	pushes = append(pushes, s.actionPush(round, 5027, pid, &protocolpb.MoveC2S{}))
	return DispatchResult{Message: msg, Pushes: pushes}, nil
}

func randomMovementDice(count int) ([]int32, int32, error) {
	if count < 1 || count > 2 {
		return nil, 0, errors.New("movement dice count must be one or two")
	}
	vals := make([]int32, 0, count)
	var total int32
	for i := 0; i < count; i++ {
		n, err := rand.Int(rand.Reader, big.NewInt(6))
		if err != nil {
			return nil, 0, err
		}
		value := int32(n.Int64() + 1)
		vals = append(vals, value)
		total += value
	}
	return vals, total, nil
}

func (s *Server) consumedDiceBuffPush(playerID int64, buff store.BuffState, recipients []int64) Push {
	update := &protocolpb.UpdateHeroAttrS2C{
		PlayerId: playerID,
		Cause:    &protocolpb.CauseOrigin{S: protocolpb.CauseOrigin_event, Id: 30002},
		EffectDatas: []*protocolpb.HeroAttrEffect{{
			PlayerId: playerID,
			Data: &protocolpb.HeroAttrEffect_Buff{Buff: &protocolpb.HeroBuffChangeS2C{
				PlayerId: playerID, Buff: buffMessage(buff), Op: protocolpb.HeroBuffChangeS2C_Delete,
			}},
		}},
	}
	return s.pushFor("UpdateHeroAttrS2C", update, recipients, 0)
}

func (s *Server) handleMove(ctx context.Context, sess *Session, in wire.Frame, q *protocolpb.MoveC2S) (DispatchResult, error) {
	_, pid, ok := sessionPlayer(sess)
	if !ok {
		return DispatchResult{Err: ErrAuth}, nil
	}
	roomID, err := s.store.RoomForPlayer(ctx, pid)
	if err != nil {
		return DispatchResult{Err: ErrRoomNotAction}, nil
	}
	current, round, pending, err := s.store.CurrentTurn(ctx, roomID)
	if err != nil {
		return DispatchResult{Err: ErrRoomNotAction}, nil
	}
	if current != pid {
		return DispatchResult{Err: ErrRoomActionPlayer}, nil
	}
	if pending <= 0 || q.Direction < 0 {
		return DispatchResult{Err: ErrRoomActionIncorrect}, nil
	}
	room, err := s.store.RoomSnapshot(ctx, roomID)
	if err != nil {
		return DispatchResult{}, err
	}
	var player store.Player
	for _, member := range room.Players {
		if member.ID == pid {
			player = member
			break
		}
	}
	if player.ID == 0 {
		return DispatchResult{Err: ErrRoomNotAction}, nil
	}
	end, err := s.domain.ValidateMoveStep(room, player, q.Direction, pending, q.ForceDir)
	if err != nil {
		return DispatchResult{Err: ErrRoomActionIncorrect}, nil
	}
	jumpTarget, jumpFront, isJump, err := s.domain.JumpTargetAt(room, q.Direction)
	if err != nil {
		s.log.Error("jump land could not be resolved", "room_id", roomID, "player_id", pid, "land_id", q.Direction, "err", err)
		return DispatchResult{Err: ErrNotOpen}, nil
	}
	var teleport *store.MoveTeleport
	if isJump {
		teleport = &store.MoveTeleport{NodeID: jumpTarget, FrontNodeIDs: jumpFront}
	}
	landType, landExists := s.domain.LandTypeAt(room, q.Direction)
	moveAgain := landExists && landType == 12
	rollGold := end && landExists && landType == 15
	landHeal := end && landExists && landType == 20
	landBloodLoss := end && landExists && landType == 17
	landHospital := end && landExists && landType == 13
	landDivination := end && landExists && landType == 6
	landGamble := end && landExists && landType == 10
	noGamblePlayers := false
	drawCard := end && landExists && landType == 14
	fillingStation := end && landExists && (landType == 1 || landType == 2 || (room.Mode == 7 && landType == 23))
	landEvent := end && landExists && landType == 8 && s.domain.HasPlayableEvent(room)
	landDestiny := end && landExists && landType == 16
	if landDestiny && len(s.destinyCandidates(room)) == 0 {
		s.log.Error("Destiny resource pool has no supported outcome", "room_id", roomID, "player_id", pid, "land_id", q.Direction)
		return DispatchResult{Err: ErrNotOpen}, nil
	}
	landPursuit := end && landExists && landType == 4
	var batteryData *protocolpb.LandChoiceTargetC2S
	landBattery := end && landExists && landType == batteryLandType
	if landBattery {
		if _, configErr := s.batteryDamage(); configErr != nil {
			s.log.Error("Battery land configuration is unavailable", "room_id", roomID, "player_id", pid, "land_id", q.Direction, "err", configErr)
			return DispatchResult{Err: ErrNotOpen}, nil
		}
		batteryData, landBattery = s.batteryActionData(room, player)
	}
	landShop := end && landExists && landType == 7
	landPveShop := end && landExists && landType == 22
	landLottery := false
	var lotteryChooseCount int32
	if end && landExists && landType == 9 {
		var lotteryLimit int32
		lotteryChooseCount, lotteryLimit, err = s.lotterySettings()
		if errors.Is(err, errLotteryConfigMissing) {
			s.log.Error("lottery land configuration is unavailable", "room_id", roomID, "player_id", pid, "land_id", q.Direction)
			return DispatchResult{Err: ErrNotOpen}, nil
		}
		if err != nil {
			return DispatchResult{}, err
		}
		landLottery = lotteryHasCapacity(player, lotteryChooseCount, lotteryLimit)
	}
	var cardsToDraw []store.CardState
	var handLimit int32
	if drawCard {
		cardsToDraw, handLimit, err = s.drawCardLandReward(room, player, round, q.Direction)
		if err != nil {
			s.log.Error("draw-card land reward could not be prepared", "room_id", roomID, "player_id", pid, "land_id", q.Direction, "err", err)
			return DispatchResult{Err: ErrNotOpen}, nil
		}
	} else if landPveShop {
		cardsToDraw, handLimit, err = s.drawPVEShopVisitReward(room, player)
		if err != nil {
			s.log.Error("PVE shop visit card could not be prepared", "room_id", roomID, "player_id", pid, "land_id", q.Direction, "err", err)
			return DispatchResult{Err: ErrNotOpen}, nil
		}
	}
	if landHeal {
		params, ok := s.domain.LandParams(20)
		if !ok || len(params) == 0 || params[0] < 0 {
			return DispatchResult{Err: ErrNotOpen}, nil
		}
	}
	if landBloodLoss {
		params, ok := s.domain.LandParams(17)
		if !ok || len(params) == 0 || params[0] > 0 {
			return DispatchResult{Err: ErrNotOpen}, nil
		}
	}
	if landHospital {
		params, ok := s.domain.LandParams(13)
		if !ok || len(params) < 2 || params[0] < 0 || params[1] < 0 {
			return DispatchResult{Err: ErrNotOpen}, nil
		}
	}
	var divinationChoices []int32
	if landDivination {
		divinationChoices, err = s.newDivinationChoices(room)
		if errors.Is(err, errDivinationConfigMissing) {
			return DispatchResult{Err: ErrNotOpen}, nil
		}
		if err != nil {
			return DispatchResult{}, err
		}
	}
	var gambleState *store.GambleState
	if landGamble {
		gambleState, landGamble, err = s.newGambleState(room, q.Direction)
		if errors.Is(err, errGambleConfigMissing) {
			return DispatchResult{Err: ErrNotOpen}, nil
		}
		if err != nil {
			return DispatchResult{}, err
		}
		noGamblePlayers = !landGamble
	}
	var shopState *store.ShopState
	if landShop || landPveShop {
		var prepared store.ShopState
		var prepareErr error
		if landPveShop {
			prepared, prepareErr = s.preparePVEShop(room, player)
		} else {
			prepared, prepareErr = s.prepareLandShop(room, player)
		}
		if prepareErr != nil {
			s.log.Error("land shop offer could not be prepared", "room_id", roomID, "player_id", pid, "land_id", q.Direction, "err", prepareErr)
			return DispatchResult{Err: ErrNotOpen}, nil
		}
		shopState = &prepared
	}
	var battleState *store.BattleState
	landAction := moveAgain || rollGold || landHeal || landBloodLoss || landHospital || landDivination || landGamble || fillingStation || landEvent || landDestiny || landLottery || landPursuit || landBattery || landShop || landPveShop || drawCard
	if end && !landAction && player.HP > 0 {
		for _, other := range room.Players {
			if other.ID == pid || other.HP <= 0 || other.NodeID != q.Direction || roomTeamID(room.Mode, other.Slot) == roomTeamID(room.Mode, player.Slot) {
				continue
			}
			candidate, stateErr := s.newBattleState(player, other, false)
			if stateErr != nil {
				return DispatchResult{}, stateErr
			}
			battleState = candidate
			break
		}
	}
	frontNodeIDs, err := s.domain.ForwardLandIDs(room, q.Direction, player.NodeID)
	if err != nil {
		s.log.Error("move exits could not be prepared", "room_id", roomID, "player_id", pid, "land_id", q.Direction, "err", err)
		return DispatchResult{Err: ErrNotOpen}, nil
	}
	goldDropBuff, hasGoldDropBuff := findGoldDropBuff(player)
	raw, err := proto.Marshal(q)
	if err != nil {
		return DispatchResult{}, err
	}
	var cardResult store.MoveCardsResult
	var placeResult store.MovePlaceResult
	var goldResult store.MoveGoldResult
	var landBuffPickup store.LandBuffPickupResult
	next, round, created, err := s.store.CommitMoveStep(ctx, roomID, pid, in.CmdID, in.UPSN, raw, player.NodeID, q.Direction, end, moveAgain, rollGold, landHeal, landBloodLoss, landHospital, landDivination, landGamble, fillingStation, landEvent, landDestiny, landLottery, landPursuit, landBattery, landShop, landPveShop, shopState, divinationChoices, gambleState, battleState, frontNodeIDs, teleport, cardsToDraw, handLimit, &cardResult, &placeResult, &goldResult, &landBuffPickup)
	if err != nil {
		return DispatchResult{Err: ErrRoomActionIncorrect}, nil
	}
	if !created {
		return DispatchResult{Err: ErrRoomActionAlreadyDone}, nil
	}
	msg := &protocolpb.MoveS2C{PlayerId: pid, NodeIds: []int32{q.Direction}, End: end}
	ids := mustMemberIDs(ctx, s.store, roomID)
	pushes := []Push{s.pushFor("MoveS2C", msg, ids, pid)}
	if placeResult.Changed {
		pushes = append(pushes, s.jumpPlacePush(pid, q.Direction, placeResult, ids))
	}
	if cardResult.Changed {
		pushes = append(pushes, s.pushFor("UpdateHeroAttrS2C", landCardRewardAttrUpdate(pid, q.Direction, cardResult.Cards, landPveShop), ids, 0))
	}
	if goldResult.Changed && !hasGoldDropBuff {
		pushes = append(pushes, s.shopEntryGoldPush(pid, q.Direction, goldResult.OldGold, goldResult.NewGold, ids))
		s.log.Info("legendary merchant entry reward", "room_id", roomID, "player_id", pid, "gold", goldResult.NewGold-goldResult.OldGold)
	}
	if landBuffPickup.LandBuff.Buff.UniqueID > 0 {
		pushes = append(pushes, s.pushFor("UpdateHeroAttrS2C", landBuffPickupAttrUpdate(pid, landBuffPickup, !(end && hasGoldDropBuff)), ids, 0))
		pushes = append(pushes, s.landBuffsPush(store.LandBuffUpdate{NodeID: landBuffPickup.LandBuff.NodeID, Buffs: landBuffPickup.Remaining}, ids))
		s.log.Info("land summon collected", "room_id", roomID, "player_id", pid, "node_id", landBuffPickup.LandBuff.NodeID, "buff_id", landBuffPickup.LandBuff.Buff.BuffID, "summon_id", landBuffPickup.LandBuff.Buff.Source.ID, "gold", landBuffPickup.LandBuff.Value)
	}
	if end && hasGoldDropBuff {
		updatedRoom, refreshErr := s.store.RoomSnapshot(ctx, roomID)
		if refreshErr != nil {
			return DispatchResult{}, refreshErr
		}
		newGold := player.Gold
		found := false
		for _, member := range updatedRoom.Players {
			if member.ID == pid {
				newGold = member.Gold
				found = true
				break
			}
		}
		if !found {
			return DispatchResult{Err: ErrRoomPlayerNotExist}, nil
		}
		pushes = append(pushes, s.goldDropBuffPush(pid, player.Gold, newGold, goldDropBuff, ids))
	}
	if end {
		if cardResult.DiscardRequired {
			pushes = append(pushes, s.actionPush(round, 5075, pid, &protocolpb.AbandonCardC2S{}))
		} else if battleState != nil {
			pushes = append(pushes, s.actionPush(round, 5047, pid, &protocolpb.AskBattleC2S{AskPlayerId: battleState.Defender.PlayerID}))
		} else if landPveShop {
			shop, shopErr := s.store.ActiveShop(ctx, roomID, pid)
			if shopErr != nil {
				return DispatchResult{}, shopErr
			}
			pushes = append(pushes, s.actionPush(round, 5215, pid, pveShopActionData(shop)))
		} else if landShop {
			shop, shopErr := s.store.ActiveShop(ctx, roomID, pid)
			if shopErr != nil {
				return DispatchResult{}, shopErr
			}
			pushes = append(pushes, s.actionPush(round, 5029, pid, shopActionData(shop)))
		} else if moveAgain {
			pushes = append(pushes, s.actionPush(round, 5043, pid, &protocolpb.MoveAgainC2S{}))
		} else if fillingStation {
			pushes = append(pushes, s.actionPush(round, 5077, pid, &protocolpb.StopOrContinueC2S{}))
		} else if landEvent {
			pushes = append(pushes, s.actionPush(round, 5053, pid, &protocolpb.TriggerEventC2S{}))
		} else if landDestiny {
			pushes = append(pushes, s.actionPush(round, 5071, pid, &protocolpb.TriggerDestinyC2S{}))
		} else if landDivination {
			pushes = append(pushes, s.actionPush(round, 5069, pid, &protocolpb.TriggerDivinationC2S{CanChoiceIds: divinationChoices}))
		} else if landGamble {
			gambleRoom, roomErr := s.store.RoomSnapshot(ctx, roomID)
			if roomErr != nil {
				return DispatchResult{}, roomErr
			}
			pushes = append(pushes, s.gambleStartPushes(gambleRoom, round, *gambleState)...)
			botPushes, completed, nextPlayer, nextRound, botErr := s.resolveGambleBots(ctx, roomID)
			if botErr != nil {
				return DispatchResult{}, botErr
			}
			pushes = append(pushes, botPushes...)
			if completed {
				pushes = append(pushes, s.turnPushes(ctx, roomID, nextRound, nextPlayer)...)
			}
		} else if noGamblePlayers {
			pushes = append(pushes, s.handleGambleLandNoPlayers(roomID, pid)...)
			pushes = append(pushes, s.turnPushes(ctx, roomID, round, next)...)
		} else if landHospital {
			pushes = append(pushes, s.actionPush(round, 5093, pid, &protocolpb.TriggerHospitalC2S{}))
		} else if landLottery {
			pushes = append(pushes, s.actionPush(round, 5041, pid, &protocolpb.LotteryChoiceC2S{Num: lotteryChooseCount}))
		} else if landPursuit {
			pushes = append(pushes, s.actionPush(round, 5033, pid, &protocolpb.PursuitC2S{}))
		} else if landBattery {
			pushes = append(pushes, s.actionPush(round, 5063, pid, batteryData))
		} else if rollGold {
			pushes = append(pushes, s.actionPush(round, 5049, pid, &protocolpb.RollGoldC2S{}))
		} else if landHeal {
			healed, e := s.resolveLandHeal(ctx, roomID, pid)
			if e != nil {
				return DispatchResult{}, e
			}
			pushes = append(pushes, s.pushFor("UpdateHeroAttrS2C", landHealAttrUpdate(pid, q.Direction, healed), ids, 0))
			pushes = append(pushes, s.turnPushes(ctx, roomID, healed.Round, healed.NextPlayer)...)
		} else if landBloodLoss {
			damaged, e := s.resolveLandBloodLoss(ctx, roomID, pid)
			if e != nil {
				return DispatchResult{}, e
			}
			pushes = append(pushes, s.pushFor("UpdateHeroAttrS2C", landBloodLossAttrUpdate(pid, q.Direction, damaged), ids, 0))
			pushes = append(pushes, s.turnPushes(ctx, roomID, damaged.Round, damaged.NextPlayer)...)
		} else {
			pushes = append(pushes, s.turnPushes(ctx, roomID, round, next)...)
		}
	} else {
		pushes = append(pushes, s.actionPush(round, 5027, pid, &protocolpb.MoveC2S{}))
	}
	return DispatchResult{Message: msg, Pushes: pushes}, nil
}

func (s *Server) actionPush(round int32, actionID int32, playerID int64, data proto.Message) Push {
	return s.actionPushWithSN(round, actionID, playerID, data, s.nextActionSN())
}

func (s *Server) actionPushWithSN(round int32, actionID int32, playerID int64, data proto.Message, actionSN int64) Push {
	payload, err := proto.Marshal(data)
	if err != nil {
		s.log.Error("could not encode client action", "action_id", actionID, "err", err)
		return Push{}
	}
	modelAction := &modelpb.Action{Id: actionID, PlayerId: playerID, Data: payload, Sn: actionSN}
	msg := &protocolpb.PredictActionS2C{RoomRound: round, Actions: []*modelpb.Action{modelAction}}
	return s.pushFor("PredictActionS2C", msg, []int64{playerID}, 0)
}
func (s *Server) requireTurn(ctx context.Context, sess *Session) (int64, int64, error) {
	_, _, pid, _, ok := sess.identity()
	if !ok {
		return 0, 0, errors.New("not authenticated")
	}
	roomID, err := s.store.RoomForPlayer(ctx, pid)
	if err != nil {
		return 0, 0, err
	}
	current, _, pending, err := s.store.CurrentTurn(ctx, roomID)
	if err != nil {
		return 0, 0, err
	}
	if current != pid {
		return 0, 0, fmt.Errorf("not current player")
	}
	if pending <= 0 {
		return 0, 0, fmt.Errorf("no pending action")
	}
	return roomID, pid, nil
}
func mapRoomError(err error) int16 {
	if err == nil {
		return ErrSucc
	}
	m := strings.ToLower(err.Error())
	switch {
	case strings.Contains(m, "not exist"):
		return ErrRoomNotExist
	case strings.Contains(m, "full"):
		return ErrRoomFull
	case strings.Contains(m, "password"):
		return ErrRoomPwd
	case strings.Contains(m, "player too little"):
		return ErrRoomPlayerTooLittle
	case strings.Contains(m, "player not ready"):
		return ErrRoomNotReady
	case strings.Contains(m, "hero not selected"):
		return ErrRoomHeroNotChoice
	case strings.Contains(m, "hero already confirmed"):
		return ErrRoomHeroNotAffirmed
	case strings.Contains(m, "hero not choosable"):
		return ErrRoomHeroNotUse
	case strings.Contains(m, "hero not confirmed"):
		return ErrRoomChooseSkinNeedHero
	case strings.Contains(m, "skin hero mismatch"):
		return ErrRoomChooseSkinMismatch
	case strings.Contains(m, "skin already confirmed"):
		return ErrRoomChooseSkinNotOwn
	case strings.Contains(m, "not choosing hero"):
		return ErrRoomHeroNotChoice
	case strings.Contains(m, "not choosing skin"):
		return ErrRoomChooseSkinNeedBox
	case strings.Contains(m, "slot"):
		return ErrInvalidParam
	case strings.Contains(m, "not waiting"):
		return ErrRoomNotWait
	case strings.Contains(m, "master"):
		return ErrRoomActionPlayer
	default:
		return ErrInvalidParam
	}
}
func mustMemberIDs(ctx context.Context, st *store.Store, roomID int64) []int64 {
	ids, err := st.MemberIDs(ctx, roomID)
	if err != nil {
		return nil
	}
	return ids
}
func (s *Server) pushFor(name string, msg proto.Message, targets []int64, exclude int64) Push {
	r, ok := s.registry.ByMessage(name)
	if !ok {
		return Push{Message: msg, Targets: targets, Exclude: exclude}
	}
	return Push{CmdID: uint16(r.CmdID), Message: msg, Targets: targets, Exclude: exclude}
}

func isReadOnlyCommand(name string) bool {
	for _, prefix := range []string{"Get", "Query", "Search", "Refresh", "FriendList", "FriendApplyList", "FriendInviteList", "FriendBlacksList", "NearFightPlayer", "GachaRecord", "GetChatMsg", "GetPlayerFightRecord", "GetShowPlayer", "WatchRefreshRoomState"} {
		if strings.HasPrefix(name, prefix) {
			return true
		}
	}
	return false
}
func (s *Server) friendInfo(ctx context.Context, p store.Player) (*protocolpb.FriendInfo, error) {
	info := &protocolpb.FriendInfo{PlayerId: p.ID, Name: p.Nick, Lv: p.Level, IsOnline: len(s.hub.sessions(p.ID)) > 0, IsBusy: p.RoomID != 0}
	if p.RoomID != 0 {
		if room, e := s.store.RoomSnapshot(ctx, p.RoomID); e == nil {
			info.RoomId = room.ID
			info.PlayerCount = int32(len(room.Players))
			if room.State == 25 {
				info.State = modelpb.Room_running
			} else {
				info.State = modelpb.Room_wait
			}
		}
	}
	return info, nil
}
func (s *Server) handleGetPlayerSimple(ctx context.Context, q *protocolpb.GetPlayerSimpleC2S) (DispatchResult, error) {
	p, err := s.store.LookupPlayer(ctx, q.PlayerId)
	if err != nil {
		return DispatchResult{Err: 12001}, nil
	}
	info, _ := s.friendInfo(ctx, p)
	return DispatchResult{Message: &protocolpb.GetPlayerSimpleS2C{PlayerInfo: info}}, nil
}
func (s *Server) handleSearchPlayer(ctx context.Context, q *protocolpb.SearchPlayerC2S) (DispatchResult, error) {
	p, err := s.store.LookupPlayer(ctx, q.PlayerId)
	if err != nil {
		return DispatchResult{Err: 12001}, nil
	}
	info := &protocolpb.FriendShowPlayerInfo{PlayerId: p.ID, Name: p.Nick, Lv: p.Level, IsOnline: len(s.hub.sessions(p.ID)) > 0, IsBusy: p.RoomID != 0, Time: time.Now().Unix()}
	return DispatchResult{Message: &protocolpb.SearchPlayerS2C{Info: info}}, nil
}
func (s *Server) handleFriendList(ctx context.Context, sess *Session) (DispatchResult, error) {
	_, _, pid, _, ok := sess.identity()
	if !ok {
		return DispatchResult{Err: ErrAuth}, nil
	}
	ids, err := s.store.ListFriends(ctx, pid)
	if err != nil {
		return DispatchResult{}, err
	}
	out := &protocolpb.FriendListS2C{IsEnd: true}
	for _, id := range ids {
		p, e := s.store.LookupPlayer(ctx, id)
		if e != nil {
			continue
		}
		info, _ := s.friendInfo(ctx, p)
		out.Friends = append(out.Friends, info)
	}
	return DispatchResult{Message: out}, nil
}

func (s *Server) handleFriendOp(ctx context.Context, sess *Session, request *protocolpb.FriendOpC2S) (DispatchResult, error) {
	playerID, ok := s.currentPlayerID(sess)
	if !ok {
		return DispatchResult{Message: &protocolpb.FriendOpS2C{}, Err: ErrAuth}, nil
	}
	if request == nil || request.PlayerId <= 0 {
		return DispatchResult{Message: &protocolpb.FriendOpS2C{}, Err: ErrInvalidParam}, nil
	}
	if err := s.store.FriendOperation(ctx, playerID, request.PlayerId, request.OpType); err != nil {
		switch {
		case errors.Is(err, store.ErrFriendOperationInvalid):
			return DispatchResult{Message: &protocolpb.FriendOpS2C{}, Err: ErrInvalidParam}, nil
		case errors.Is(err, store.ErrFriendTargetNotFound):
			return DispatchResult{Message: &protocolpb.FriendOpS2C{}, Err: 12001}, nil
		case errors.Is(err, store.ErrFriendRelationMissing):
			return DispatchResult{Message: &protocolpb.FriendOpS2C{}, Err: ErrPlayerNotFriend}, nil
		default:
			return DispatchResult{}, fmt.Errorf("friend operation player=%d target=%d op=%d: %w", playerID, request.PlayerId, request.OpType, err)
		}
	}
	response := &protocolpb.FriendOpS2C{PlayerId: request.PlayerId, OpType: request.OpType}
	if request.OpType == 1 {
		push := s.pushFor("FriendDelNotifyS2C", &protocolpb.FriendDelNotifyS2C{PlayerId: playerID}, []int64{request.PlayerId}, 0)
		return DispatchResult{Message: response, Pushes: []Push{push}}, nil
	}
	return DispatchResult{Message: response}, nil
}

func (s *Server) handleFriendInvite(ctx context.Context, sess *Session, request *protocolpb.FriendInviteC2S) (DispatchResult, error) {
	_, playerID, ok := sessionPlayer(sess)
	if !ok {
		return DispatchResult{Message: &protocolpb.FriendInviteS2C{}, Err: ErrAuth}, nil
	}
	if request == nil || len(request.PlayerId) == 0 || len(request.PlayerId) > 64 {
		return DispatchResult{Message: &protocolpb.FriendInviteS2C{}, Err: ErrInvalidParam}, nil
	}
	roomID, err := s.store.CurrentRoomID(ctx, playerID)
	if err != nil {
		return DispatchResult{}, fmt.Errorf("resolve invite room player=%d: %w", playerID, err)
	}
	if roomID <= 0 {
		return DispatchResult{Message: &protocolpb.FriendInviteS2C{}, Err: ErrRoomNotWait}, nil
	}
	targetIDs, err := s.store.CreateRoomInvites(ctx, roomID, playerID, request.Pwd, request.PlayerId)
	if err != nil {
		switch {
		case errors.Is(err, store.ErrRoomInvitePassword):
			return DispatchResult{Message: &protocolpb.FriendInviteS2C{}, Err: ErrRoomPwd}, nil
		case errors.Is(err, store.ErrRoomInviteFull):
			return DispatchResult{Message: &protocolpb.FriendInviteS2C{}, Err: ErrRoomFull}, nil
		case errors.Is(err, store.ErrRoomInviteRoomUnavailable):
			return DispatchResult{Message: &protocolpb.FriendInviteS2C{}, Err: ErrRoomNotWait}, nil
		case errors.Is(err, store.ErrRoomInviteTargetBusy):
			return DispatchResult{Message: &protocolpb.FriendInviteS2C{}, Err: ErrRoomPlayerAlreadyJoin}, nil
		case errors.Is(err, store.ErrRoomInviteTargetUnrelated):
			return DispatchResult{Message: &protocolpb.FriendInviteS2C{}, Err: ErrPlayerNotFriend}, nil
		case errors.Is(err, store.ErrRoomInviteInvalid):
			return DispatchResult{Message: &protocolpb.FriendInviteS2C{}, Err: ErrInvalidParam}, nil
		case errors.Is(err, store.ErrPlayerNotFound):
			return DispatchResult{Message: &protocolpb.FriendInviteS2C{}, Err: ErrPlayerNotFind}, nil
		default:
			return DispatchResult{}, fmt.Errorf("create room invites room=%d player=%d: %w", roomID, playerID, err)
		}
	}
	pushes := make([]Push, 0, 1)
	if len(targetIDs) > 0 {
		pushes = append(pushes, s.pushFor("FriendInviteNotifyS2C", &protocolpb.FriendInviteNotifyS2C{}, targetIDs, 0))
	}
	s.log.Info("room invites created", "room_id", roomID, "player_id", playerID, "recipient_count", len(targetIDs))
	return DispatchResult{Message: &protocolpb.FriendInviteS2C{}, Pushes: pushes}, nil
}

func (s *Server) handleFriendInviteList(ctx context.Context, sess *Session) (DispatchResult, error) {
	playerID, ok := s.currentPlayerID(sess)
	if !ok {
		return DispatchResult{Message: &protocolpb.FriendInviteListS2C{IsEnd: true}, Err: ErrAuth}, nil
	}
	invites, err := s.store.RoomInvitesForPlayer(ctx, playerID)
	if err != nil {
		return DispatchResult{}, fmt.Errorf("load room invites player=%d: %w", playerID, err)
	}
	response := &protocolpb.FriendInviteListS2C{IsEnd: true}
	for _, invite := range invites {
		response.Inf = append(response.Inf, &protocolpb.FriendInviteInfo{
			PlayerId: invite.InviterID, Name: invite.InviterName, Lv: invite.InviterLevel,
			Time: invite.CreatedAt, Valid: true, RoomId: invite.RoomID,
			RoomName: invite.RoomName, Pwd: invite.Password, MapId: invite.MapID,
			PlayerCount: invite.PlayerCount, RoomServerId: 0,
		})
	}
	return DispatchResult{Message: response}, nil
}

func (s *Server) handleFriendInviteClean(ctx context.Context, sess *Session) (DispatchResult, error) {
	playerID, ok := s.currentPlayerID(sess)
	if !ok {
		return DispatchResult{Message: &protocolpb.FriendInviteCleanS2C{}, Err: ErrAuth}, nil
	}
	if err := s.store.ClearRoomInvites(ctx, playerID); err != nil {
		return DispatchResult{}, fmt.Errorf("clear room invites player=%d: %w", playerID, err)
	}
	return DispatchResult{Message: &protocolpb.FriendInviteCleanS2C{}}, nil
}

func (s *Server) handleNearFightPlayers(ctx context.Context, sess *Session, request *protocolpb.NearFightPlayerC2S) (DispatchResult, error) {
	playerID, ok := s.currentPlayerID(sess)
	if !ok {
		return DispatchResult{Message: &protocolpb.NearFightPlayerS2C{IsEnd: true}, Err: ErrAuth}, nil
	}
	recent, err := s.store.RecentPlayersForPlayer(ctx, playerID, 50)
	if err != nil {
		return DispatchResult{}, fmt.Errorf("load recent players player=%d: %w", playerID, err)
	}
	response := &protocolpb.NearFightPlayerS2C{IsEnd: true}
	for _, player := range recent {
		isOnline := len(s.hub.sessions(player.ID)) > 0
		if request != nil && request.OnlyOnline && !isOnline {
			continue
		}
		response.Infos = append(response.Infos, &protocolpb.FriendShowPlayerInfo{
			PlayerId: player.ID, Name: player.Name, Lv: player.Level, Time: player.LastPlayedAt,
			IsOnline: isOnline, IsBusy: player.RoomID != 0,
		})
	}
	return DispatchResult{Message: response}, nil
}

func (s *Server) handleFriendBlacksList(ctx context.Context, sess *Session) (DispatchResult, error) {
	playerID, ok := s.currentPlayerID(sess)
	if !ok {
		return DispatchResult{Message: &protocolpb.FriendBlacksListS2C{}, Err: ErrAuth}, nil
	}
	players, err := s.store.ListBlockedPlayers(ctx, playerID)
	if err != nil {
		return DispatchResult{}, fmt.Errorf("list blocked players player=%d: %w", playerID, err)
	}
	response := &protocolpb.FriendBlacksListS2C{IsEnd: true}
	for _, entry := range players {
		player := entry.Player
		response.Infos = append(response.Infos, &protocolpb.FriendShowPlayerInfo{
			PlayerId: player.ID, Name: player.Nick, Lv: player.Level,
			IsOnline: len(s.hub.sessions(player.ID)) > 0, IsBusy: player.RoomID != 0,
			Time: entry.CreatedAt,
		})
	}
	return DispatchResult{Message: response}, nil
}

func (s *Server) handleSendChat(ctx context.Context, sess *Session, in wire.Frame, q *protocolpb.SendChatC2S) (DispatchResult, error) {
	_, _, pid, _, ok := sess.identity()
	if !ok {
		return DispatchResult{Err: ErrAuth}, nil
	}
	if s.resources.Available() {
		if _, found := s.resources.Get("Chat_infos", int64(q.ExpressionId)); !found {
			return DispatchResult{Err: ErrInvalidParam}, nil
		}
	}
	if in.UPSN > 0 {
		raw, _ := proto.Marshal(q)
		created, e := s.store.AddAction(ctx, 0, pid, in.CmdID, in.UPSN, raw)
		if e != nil {
			return DispatchResult{}, e
		}
		if !created {
			return DispatchResult{Err: ErrRoomActionAlreadyDone}, nil
		}
	}
	resp := &protocolpb.SendChatS2C{PlayerId: pid, ExpressionId: q.ExpressionId}
	for _, to := range q.PlayerIds {
		if to == pid {
			continue
		}
		if err := s.store.InsertChat(ctx, 0, pid, to, q.ExpressionId, 0); err != nil {
			return DispatchResult{}, err
		}
	}
	pushes := []Push{}
	if route, exists := s.registry.ByMessage("SendChatS2C"); exists {
		for _, to := range q.PlayerIds {
			if to == pid {
				continue
			}
			pushes = append(pushes, Push{CmdID: uint16(route.CmdID), Message: resp, Targets: []int64{to}})
		}
	}
	return DispatchResult{Message: resp, Pushes: pushes}, nil
}
func (s *Server) handleRoomShortChat(ctx context.Context, sess *Session, in wire.Frame, q *protocolpb.RoomShortChatC2S) (DispatchResult, error) {
	_, _, pid, _, ok := sess.identity()
	if !ok {
		return DispatchResult{Err: ErrAuth}, nil
	}
	roomID, err := s.store.RoomForPlayer(ctx, pid)
	if err != nil {
		return DispatchResult{Err: ErrRoomNotAction}, nil
	}
	room, err := s.store.RoomSnapshot(ctx, roomID)
	if err != nil {
		return DispatchResult{Err: ErrRoomNotExist}, nil
	}
	if !hasRoomPlayer(room, pid) {
		return DispatchResult{Err: ErrRoomActionPlayer}, nil
	}
	if !s.validQuickChatIndex(q.Index) {
		return DispatchResult{Err: ErrInvalidParam}, nil
	}
	targets, valid := roomChatRecipients(room, pid, q.PlayerIds)
	if !valid {
		return DispatchResult{Err: ErrInvalidParam}, nil
	}
	if !s.allowChat(pid) {
		return DispatchResult{Err: ErrRoomChatCd}, nil
	}
	if in.UPSN > 0 {
		raw, _ := proto.Marshal(q)
		created, e := s.store.AddAction(ctx, roomID, pid, in.CmdID, in.UPSN, raw)
		if e != nil {
			return DispatchResult{}, e
		}
		if !created {
			return DispatchResult{Err: ErrRoomActionAlreadyDone}, nil
		}
	}
	if err = s.store.InsertChat(ctx, roomID, pid, 0, 0, q.Index); err != nil {
		return DispatchResult{}, err
	}
	resp := &protocolpb.RoomShortChatS2C{PlayerId: pid, Index: q.Index}
	route, exists := s.registry.ByMessage("RoomShortChatS2C")
	var pushes []Push
	if exists {
		pushes = append(pushes, Push{CmdID: uint16(route.CmdID), Message: resp, Targets: targets, Exclude: pid})
	}
	return DispatchResult{Message: resp, Pushes: pushes}, nil
}

func (s *Server) teamMessage(t store.MatchTeam) *modelpb.MatchTeamInfo {
	state := modelpb.MatchTeamInfo_waiting
	if t.State == 2 {
		state = modelpb.MatchTeamInfo_matching
	} else if t.State == 3 {
		state = modelpb.MatchTeamInfo_playing
	}
	out := &modelpb.MatchTeamInfo{Id: t.ID, Mode: modelpb.MatchMode(t.Mode), State: state, MapId: t.MapID, Difficulty: t.Difficulty, LeaderId: t.LeaderID, MatchTime: t.CreatedAt, PlayerReady: map[int64]bool{}}
	for _, p := range t.Players {
		out.Players = append(out.Players, s.playerMessage(p, 0, 0))
		out.PlayerReady[p.ID] = t.Ready[p.ID]
	}
	return out
}
func (s *Server) handleCreateMatchTeam(ctx context.Context, sess *Session, q *protocolpb.CreateMatchTeamC2S) (DispatchResult, error) {
	_, _, pid, _, ok := sess.identity()
	if !ok {
		return DispatchResult{Err: ErrAuth}, nil
	}
	if known, loaded := s.resources.ValidateMap(int64(q.MapId)); loaded && !known {
		return DispatchResult{Err: ErrRoomMapNotExist}, nil
	}
	p, err := s.store.LookupPlayer(ctx, pid)
	if err != nil {
		return DispatchResult{}, err
	}
	t, err := s.store.CreateMatchTeam(ctx, p, int32(q.Mode), q.MapId, q.Difficulty)
	if err != nil {
		return DispatchResult{Err: ErrInvalidParam}, nil
	}
	return DispatchResult{Message: &protocolpb.CreateMatchTeamS2C{Team: s.teamMessage(t)}}, nil
}
func (s *Server) handleChangeMatchTeam(ctx context.Context, sess *Session, q *protocolpb.ChangeMatchTeamC2S) (DispatchResult, error) {
	_, _, pid, _, ok := sess.identity()
	if !ok {
		return DispatchResult{Err: ErrAuth}, nil
	}
	if err := s.store.UpdateMatchTeam(ctx, q.TeamId, pid, int32(q.Mode), q.MapId, q.Difficulty); err != nil {
		return DispatchResult{Err: ErrInvalidParam}, nil
	}
	t, err := s.store.MatchTeamSnapshot(ctx, q.TeamId)
	if err != nil {
		return DispatchResult{}, err
	}
	return DispatchResult{Message: &protocolpb.ChangeMatchTeamS2C{Team: s.teamMessage(t)}}, nil
}
func (s *Server) handleJoinMatchTeam(ctx context.Context, sess *Session, q *protocolpb.JoinMatchTeamC2S) (DispatchResult, error) {
	_, _, pid, _, ok := sess.identity()
	if !ok {
		return DispatchResult{Err: ErrAuth}, nil
	}
	p, err := s.store.LookupPlayer(ctx, pid)
	if err != nil {
		return DispatchResult{}, err
	}
	t, err := s.store.JoinMatchTeam(ctx, q.TeamId, p)
	if err != nil {
		return DispatchResult{Err: ErrInvalidParam}, nil
	}
	push := s.pushFor("RefreshMatchTeamStateNotify", &protocolpb.RefreshMatchTeamStateNotify{TeamId: t.ID, State: modelpb.MatchTeamInfo_waiting}, matchTeamPlayerIDs(t), pid)
	return DispatchResult{Message: &protocolpb.JoinMatchTeamS2C{Team: s.teamMessage(t)}, Pushes: []Push{push}}, nil
}
func (s *Server) handleExitMatchTeam(ctx context.Context, sess *Session, q *protocolpb.ExitMatchTeamC2S) (DispatchResult, error) {
	_, _, pid, _, ok := sess.identity()
	if !ok {
		return DispatchResult{Err: ErrAuth}, nil
	}
	if q.PlayerId != 0 && q.PlayerId != pid {
		return DispatchResult{Err: ErrInvalidParam}, nil
	}
	if err := s.store.ExitMatchTeam(ctx, q.TeamId, pid); err != nil {
		return DispatchResult{Err: ErrInvalidParam}, nil
	}
	return DispatchResult{Message: &protocolpb.ExitMatchTeamS2C{ExitPlayerId: pid}}, nil
}

func (s *Server) handleRefreshMatchTeam(ctx context.Context, sess *Session, q *protocolpb.RefreshMatchTeamInfoC2S) (DispatchResult, error) {
	_, _, playerID, _, loggedIn := sess.identity()
	if !loggedIn {
		return DispatchResult{Err: ErrAuth}, nil
	}
	if q.TeamId <= 0 || playerID <= 0 {
		return DispatchResult{Err: ErrInvalidParam}, nil
	}
	member, err := s.store.IsMatchTeamMember(ctx, q.TeamId, playerID)
	if err != nil {
		s.log.Error("match team refresh membership check failed", "team_id", q.TeamId, "player_id", playerID, "err", err)
		return DispatchResult{}, err
	}
	if !member {
		return DispatchResult{Err: ErrInvalidParam}, nil
	}
	t, err := s.store.MatchTeamSnapshot(ctx, q.TeamId)
	if err != nil {
		return DispatchResult{Err: ErrInvalidParam}, nil
	}
	return DispatchResult{Message: &protocolpb.RefreshMatchTeamInfoS2C{Team: s.teamMessage(t)}}, nil
}
func (s *Server) handleMatchTeamReady(ctx context.Context, sess *Session, q *protocolpb.MatchTeamReadyC2S) (DispatchResult, error) {
	_, _, pid, _, ok := sess.identity()
	if !ok {
		return DispatchResult{Err: ErrAuth}, nil
	}
	if q.PlayerId != 0 && q.PlayerId != pid {
		return DispatchResult{Err: ErrInvalidParam}, nil
	}
	t, err := s.store.SetMatchTeamReady(ctx, q.TeamId, pid, q.IsReady)
	if err != nil {
		return DispatchResult{Err: ErrInvalidParam}, nil
	}
	msg := &protocolpb.MatchTeamReadyS2C{PlayerId: pid, TeamId: q.TeamId, IsReady: q.IsReady}
	push := s.pushFor("MatchTeamReadyS2C", msg, matchTeamPlayerIDs(t), pid)
	return DispatchResult{Message: msg, Pushes: []Push{push}}, nil
}
func (s *Server) handleStartMatch(ctx context.Context, sess *Session, teamID int64, start bool) (DispatchResult, error) {
	_, _, pid, _, ok := sess.identity()
	if !ok {
		return DispatchResult{Err: ErrAuth}, nil
	}
	state := int32(1)
	responseName := "CancelMatchS2C"
	if start {
		state = 2
		responseName = "StartMatchS2C"
	}
	if err := s.store.StartMatchForTeam(ctx, teamID, pid, start); err != nil {
		return DispatchResult{Err: ErrInvalidParam}, nil
	}
	if start {
		// The background matchmaking loop (match_impl.go) pairs queued teams,
		// fills empty seats with bots, and pushes MatchSuccessS2C.
		s.startMatchLoop()
	}
	t, err := s.store.MatchTeamSnapshot(ctx, teamID)
	if err != nil {
		return DispatchResult{}, err
	}
	push := s.pushFor("RefreshMatchTeamStateNotify", &protocolpb.RefreshMatchTeamStateNotify{TeamId: teamID, State: modelpb.MatchTeamInfo_State(state)}, matchTeamPlayerIDs(t), pid)
	var m proto.Message
	if responseName == "StartMatchS2C" {
		m = &protocolpb.StartMatchS2C{}
	} else {
		m = &protocolpb.CancelMatchS2C{}
	}
	return DispatchResult{Message: m, Pushes: []Push{push}}, nil
}
func matchTeamPlayerIDs(t store.MatchTeam) []int64 {
	ids := make([]int64, 0, len(t.Players))
	for _, p := range t.Players {
		ids = append(ids, p.ID)
	}
	return ids
}

func (s *Server) handleFriendApply(ctx context.Context, sess *Session, q *protocolpb.FriendApplyC2S) (DispatchResult, error) {
	_, _, pid, _, ok := sess.identity()
	if !ok {
		return DispatchResult{Err: ErrAuth}, nil
	}
	if err := s.store.ApplyFriend(ctx, pid, q.PlayerId); err != nil {
		return DispatchResult{Err: 12006}, nil
	}
	return DispatchResult{Message: &protocolpb.FriendApplyS2C{PlayerId: q.PlayerId}}, nil
}
func (s *Server) handleFriendApplyList(ctx context.Context, sess *Session) (DispatchResult, error) {
	_, _, pid, _, ok := sess.identity()
	if !ok {
		return DispatchResult{Err: ErrAuth}, nil
	}
	players, err := s.store.ListFriendRequests(ctx, pid)
	if err != nil {
		return DispatchResult{}, err
	}
	out := &protocolpb.FriendApplyListS2C{IsEnd: true}
	for _, p := range players {
		out.Apply = append(out.Apply, &protocolpb.FriendShowPlayerInfo{PlayerId: p.ID, Name: p.Nick, Lv: p.Level, IsOnline: len(s.hub.sessions(p.ID)) > 0, IsBusy: p.RoomID != 0})
	}
	return DispatchResult{Message: out}, nil
}
func (s *Server) handleSetOnlineStatus(ctx context.Context, sess *Session, q *protocolpb.SetOnlineStatusC2S) (DispatchResult, error) {
	_, _, pid, _, ok := sess.identity()
	if !ok {
		return DispatchResult{Err: ErrAuth}, nil
	}
	if q.OnlineStatus < 0 || q.OnlineStatus > 3 {
		return DispatchResult{Err: ErrInvalidParam}, nil
	}
	if err := s.store.SetOnlineStatus(ctx, pid, q.OnlineStatus); err != nil {
		return DispatchResult{}, err
	}
	return DispatchResult{Message: &protocolpb.SetOnlineStatusS2C{OnlineStatus: q.OnlineStatus}}, nil
}

func (s *Server) handleFriendSendMsg(ctx context.Context, sess *Session, q *protocolpb.FriendSendMsgC2S) (DispatchResult, error) {
	_, _, pid, _, ok := sess.identity()
	if !ok {
		return DispatchResult{Err: ErrAuth}, nil
	}
	if q.PlayerId == pid || len([]rune(q.Msg)) == 0 || len([]rune(q.Msg)) > 500 {
		return DispatchResult{Err: ErrInvalidParam}, nil
	}
	friends, err := s.store.AreFriends(ctx, pid, q.PlayerId)
	if err != nil {
		return DispatchResult{}, err
	}
	if !friends {
		return DispatchResult{Err: ErrPlayerNotFriend}, nil
	}
	blocked, err := s.store.IsFriendBlocked(ctx, pid, q.PlayerId)
	if err != nil {
		return DispatchResult{}, err
	}
	if blocked {
		return DispatchResult{Err: ErrPlayerNotFriend}, nil
	}
	if reply, pushes, handled := s.gmExec(ctx, pid, q.Msg); handled {
		when := time.Now().Unix()
		push := s.pushFor("FriendsChatMsgS2C", &protocolpb.FriendsChatMsgS2C{PlayerId: q.PlayerId, LastTime: when, Message: reply}, []int64{pid}, 0)
		pushes = append(pushes, push)
		return DispatchResult{Message: &protocolpb.FriendSendMsgS2C{Time: when}, Pushes: pushes}, nil
	}
	when := time.Now().Unix()
	if _, err = s.store.InsertPrivateChat(ctx, pid, q.PlayerId, q.Msg); err != nil {
		return DispatchResult{}, err
	}
	resp := &protocolpb.FriendSendMsgS2C{Time: when}
	push := s.pushFor("FriendsChatMsgS2C", &protocolpb.FriendsChatMsgS2C{PlayerId: pid, LastTime: when, Message: q.Msg}, []int64{q.PlayerId}, 0)
	return DispatchResult{Message: resp, Pushes: []Push{push}}, nil
}
func (s *Server) handleGetChatMsg(ctx context.Context, sess *Session, q *protocolpb.GetChatMsgC2S) (DispatchResult, error) {
	_, _, pid, _, ok := sess.identity()
	if !ok {
		return DispatchResult{Err: ErrAuth}, nil
	}
	friends, err := s.store.AreFriends(ctx, pid, q.TargetId)
	if err != nil {
		return DispatchResult{}, err
	}
	if !friends {
		return DispatchResult{Err: ErrPlayerNotFriend}, nil
	}
	blocked, err := s.store.IsFriendBlocked(ctx, pid, q.TargetId)
	if err != nil {
		return DispatchResult{}, err
	}
	if blocked {
		return DispatchResult{Err: ErrPlayerNotFriend}, nil
	}
	items, err := s.store.ListPrivateChat(ctx, pid, q.TargetId)
	if err != nil {
		return DispatchResult{}, err
	}
	out := &protocolpb.GetChatMsgS2C{TargetId: q.TargetId}
	for _, v := range items {
		out.Message = append(out.Message, &modelpb.ChatMessage{SenderId: v.SenderID, ReceiverId: v.ReceiverID, Message: v.Message, Time: v.Time})
		if v.Time > out.LastTime {
			out.LastTime = v.Time
		}
	}
	return DispatchResult{Message: out}, nil
}

func (s *Server) handleDeleteChatMsgInfo(ctx context.Context, sess *Session, q *protocolpb.DelChatMsgInfoC2S) (DispatchResult, error) {
	_, _, playerID, _, ok := sess.identity()
	if !ok {
		return DispatchResult{Message: &protocolpb.DelChatMsgInfoS2C{}, Err: ErrAuth}, nil
	}
	if q.TargetId <= 0 || q.TargetId == playerID {
		return DispatchResult{Message: &protocolpb.DelChatMsgInfoS2C{}, Err: ErrInvalidParam}, nil
	}
	if err := s.store.ClearPrivateChatHistory(ctx, playerID, q.TargetId); err != nil {
		switch {
		case errors.Is(err, store.ErrPrivateChatHistoryInvalid):
			return DispatchResult{Message: &protocolpb.DelChatMsgInfoS2C{}, Err: ErrInvalidParam}, nil
		case errors.Is(err, store.ErrPlayerNotFound):
			return DispatchResult{Message: &protocolpb.DelChatMsgInfoS2C{}, Err: ErrPlayerNotFind}, nil
		default:
			return DispatchResult{}, fmt.Errorf("clear private chat history player=%d target=%d: %w", playerID, q.TargetId, err)
		}
	}
	s.log.Info("private chat history cleared", "player_id", playerID, "target_player_id", q.TargetId)
	return DispatchResult{Message: &protocolpb.DelChatMsgInfoS2C{}}, nil
}

func (s *Server) handleFriendApplyOp(ctx context.Context, sess *Session, q *protocolpb.FriendApplyOpC2S) (DispatchResult, error) {
	_, _, pid, _, ok := sess.identity()
	if !ok {
		return DispatchResult{Err: ErrAuth}, nil
	}
	// The schema exposes opType as int32 without an enum. Prototype convention is 0=accept, 1=reject;
	// confirm against an observed client action before production use.
	if q.AllRefuse {
		if err := s.store.ClearIncomingFriendRequests(ctx, pid); err != nil {
			return DispatchResult{}, err
		}
		return DispatchResult{Message: &protocolpb.FriendApplyOpS2C{PlayerId: q.PlayerId, OpType: q.OpType, AllRefuse: true}}, nil
	}
	var err error
	switch q.OpType {
	case 0:
		err = s.store.AcceptFriend(ctx, q.PlayerId, pid)
	case 1:
		err = s.store.RejectFriend(ctx, q.PlayerId, pid)
	default:
		return DispatchResult{Err: ErrInvalidParam}, nil
	}
	if err != nil {
		return DispatchResult{Err: 12006}, nil
	}
	resp := &protocolpb.FriendApplyOpS2C{PlayerId: q.PlayerId, OpType: q.OpType}
	if q.OpType == 0 {
		push := s.pushFor("FriendNotifyS2C", &protocolpb.FriendNotifyS2C{PlayerId: pid}, []int64{q.PlayerId}, 0)
		return DispatchResult{Message: resp, Pushes: []Push{push}}, nil
	}
	return DispatchResult{Message: resp}, nil
}
func (s *Server) handleSetFriendNote(ctx context.Context, sess *Session, q *protocolpb.SetFriendNoteC2S) (DispatchResult, error) {
	_, _, pid, _, ok := sess.identity()
	if !ok {
		return DispatchResult{Err: ErrAuth}, nil
	}
	if len([]rune(q.Note)) > 64 {
		return DispatchResult{Err: ErrInvalidParam}, nil
	}
	if err := s.store.SetFriendNote(ctx, pid, q.PlayerId, q.Note); err != nil {
		return DispatchResult{Err: ErrPlayerNotFriend}, nil
	}
	return DispatchResult{Message: &protocolpb.SetFriendNoteS2C{PlayerId: q.PlayerId, Note: q.Note}}, nil
}

func (s *Server) currentPlayerID(sess *Session) (int64, bool) {
	_, _, pid, _, ok := sess.identity()
	return pid, ok
}
func (s *Server) handleUseItem(ctx context.Context, sess *Session, q *protocolpb.PlayerUseItemC2S) (DispatchResult, error) {
	pid, ok := s.currentPlayerID(sess)
	if !ok {
		return DispatchResult{Err: ErrAuth}, nil
	}
	if q.ItemCount <= 0 {
		return DispatchResult{Err: ErrInvalidParam}, nil
	}
	if s.resources.Available() {
		if _, found := s.resources.Get("Item_infos", int64(q.ItemId)); !found {
			return DispatchResult{Err: ErrInvalidParam}, nil
		}
	}
	if err := s.store.ConsumeItem(ctx, pid, q.ItemId, q.ItemCount); err != nil {
		return DispatchResult{Err: ErrItemEnough}, nil
	}
	return DispatchResult{Message: &protocolpb.PlayerUseItemS2C{ItemId: q.ItemId, ItemCount: q.ItemCount}}, nil
}
func jsonInt(row map[string]json.RawMessage, keys ...string) int32 {
	for _, k := range keys {
		if raw, ok := row[k]; ok {
			var n int64
			if json.Unmarshal(raw, &n) == nil {
				return int32(n)
			}
			var wrapped map[string]json.RawMessage
			if json.Unmarshal(raw, &wrapped) == nil {
				if value, ok := wrapped["value"]; ok && json.Unmarshal(value, &n) == nil {
					return int32(n)
				}
			}
		}
	}
	return 0
}
func jsonItems(raw json.RawMessage) []map[string]json.RawMessage {
	var out []map[string]json.RawMessage
	if json.Unmarshal(raw, &out) != nil {
		return nil
	}
	return out
}
func weightedPick(items []map[string]json.RawMessage, weightKey string) (map[string]json.RawMessage, bool) {
	total := int64(0)
	for _, it := range items {
		w := jsonInt(it, weightKey)
		if w > 0 {
			total += int64(w)
		}
	}
	if total <= 0 {
		return nil, false
	}
	n, e := rand.Int(rand.Reader, big.NewInt(total))
	if e != nil {
		return nil, false
	}
	cursor := int64(0)
	for _, it := range items {
		w := int64(jsonInt(it, weightKey))
		if w <= 0 {
			continue
		}
		cursor += w
		if n.Int64() < cursor {
			return it, true
		}
	}
	return nil, false
}

func (s *Server) rollGachaReward(combID int32) (store.GachaDrop, bool) {
	combRaw, found := s.resources.Get("Gacha_combs", int64(combID))
	if !found {
		return store.GachaDrop{}, false
	}
	var comb map[string]json.RawMessage
	if json.Unmarshal(combRaw, &comb) != nil {
		return store.GachaDrop{}, false
	}
	picked, ok := weightedPick(jsonItems(comb["gachaCombConfigureItems"]), "groupWeight")
	if !ok {
		return store.GachaDrop{}, false
	}
	groupID := jsonInt(picked, "groupID", "groupId")
	groupRaw, found := s.resources.Get("Gacha_groups", int64(groupID))
	if !found {
		return store.GachaDrop{}, false
	}
	var group map[string]json.RawMessage
	if json.Unmarshal(groupRaw, &group) != nil {
		return store.GachaDrop{}, false
	}
	item, ok := weightedPick(jsonItems(group["gachaGroupConfigureItems"]), "itemWeight")
	if !ok {
		return store.GachaDrop{}, false
	}
	itemID := jsonInt(item, "itemId", "itemID", "item_id")
	count := jsonInt(item, "numberMin", "number_min")
	if itemID <= 0 || count <= 0 {
		return store.GachaDrop{}, false
	}
	return store.GachaDrop{ItemID: itemID, Count: count}, true
}

func (s *Server) gachaItemClass(itemID int32) (purple, hero, ok bool) {
	itemRaw, found := s.resources.Get("Item_infos", int64(itemID))
	if !found {
		return false, false, false
	}
	var item map[string]json.RawMessage
	if json.Unmarshal(itemRaw, &item) != nil {
		return false, false, false
	}
	return jsonInt(item, "qualityType") >= 4, jsonInt(item, "itemType") == 2, true
}

func (s *Server) gachaItemTransform(itemID int32) ([]store.GachaDrop, bool) {
	itemRaw, found := s.resources.Get("Item_infos", int64(itemID))
	if !found {
		return nil, false
	}
	var item map[string]json.RawMessage
	if json.Unmarshal(itemRaw, &item) != nil {
		return nil, false
	}
	var autoTransform bool
	if raw, exists := item["isAutoTransform"]; exists && len(raw) > 0 {
		if json.Unmarshal(raw, &autoTransform) != nil {
			return nil, false
		}
	}
	if !autoTransform {
		return nil, true
	}
	raw, exists := item["transform"]
	if !exists || string(raw) == "null" {
		return nil, true
	}
	configured := make(map[int32]int32)
	if json.Unmarshal(raw, &configured) != nil {
		return nil, false
	}
	itemIDs := make([]int, 0, len(configured))
	for transformItemID := range configured {
		itemIDs = append(itemIDs, int(transformItemID))
	}
	sort.Ints(itemIDs)
	transforms := make([]store.GachaDrop, 0, len(itemIDs))
	for _, rawID := range itemIDs {
		transformItemID := int32(rawID)
		if transformItemID <= 0 || configured[transformItemID] <= 0 {
			return nil, false
		}
		if _, found = s.resources.Get("Item_infos", int64(transformItemID)); !found {
			return nil, false
		}
		transforms = append(transforms, store.GachaDrop{ItemID: transformItemID, Count: configured[transformItemID]})
	}
	return transforms, true
}

func gachaBonus(row map[string]json.RawMessage, pulls int32) ([]store.GachaDrop, bool) {
	const maxGachaBonusCount = int64(1<<31 - 1)
	extraReward, found := row["extraReward"]
	if !found {
		return nil, true
	}
	var configured map[int32]int32
	if json.Unmarshal(extraReward, &configured) != nil {
		return nil, false
	}
	itemIDs := make([]int, 0, len(configured))
	for itemID := range configured {
		itemIDs = append(itemIDs, int(itemID))
	}
	sort.Ints(itemIDs)
	bonus := make([]store.GachaDrop, 0, len(itemIDs))
	for _, rawID := range itemIDs {
		itemID := int32(rawID)
		perPull := configured[itemID]
		count := int64(perPull) * int64(pulls)
		if itemID <= 0 || perPull <= 0 || count > maxGachaBonusCount {
			return nil, false
		}
		bonus = append(bonus, store.GachaDrop{ItemID: itemID, Count: int32(count)})
	}
	return bonus, true
}

func (s *Server) handleGacha(ctx context.Context, sess *Session, in wire.Frame, q *protocolpb.GachaC2S) (DispatchResult, error) {
	pid, ok := s.currentPlayerID(sess)
	if !ok {
		return DispatchResult{Err: ErrAuth}, nil
	}
	// Pity history and draw commit must be serialized for this server process.
	s.gachaMu.Lock()
	defer s.gachaMu.Unlock()
	count := q.Count
	if count <= 0 {
		count = 1
	}
	if count > 10 {
		return DispatchResult{Err: ErrInvalidParam}, nil
	}
	back, found := s.resources.Get("Gacha_backstages", int64(q.DefId))
	if !found {
		return DispatchResult{Err: ErrNotOpen}, nil
	}
	var b map[string]json.RawMessage
	if json.Unmarshal(back, &b) != nil {
		return DispatchResult{Err: ErrNotOpen}, nil
	}
	poolID := jsonInt(b, "poolID", "poolId")
	costItem := jsonInt(b, "costItem", "cost_item")
	costOnce := jsonInt(b, "costOnce", "cost_once")
	if costOnce <= 0 {
		costOnce = 1
	}
	costCount := costOnce * count
	if jsonInt(b, "gachaType") == 9 { // Rookie pools charge the fixed tutorial price.
		costCount = 8
	}
	if poolID <= 0 || costItem <= 0 || costCount <= 0 {
		return DispatchResult{Err: ErrNotOpen}, nil
	}
	costs := make(map[int32]int32, 2)
	remainingCost := costCount
	if timeLimitItem := jsonInt(b, "timeLimit", "TimeLimit"); timeLimitItem > 0 {
		voucherCount, countErr := s.store.InventoryItemCount(ctx, pid, timeLimitItem)
		if countErr != nil {
			return DispatchResult{}, countErr
		}
		voucherUse := voucherCount
		if voucherUse > remainingCost {
			voucherUse = remainingCost
		}
		if voucherUse > 0 {
			costs[timeLimitItem] += voucherUse
			remainingCost -= voucherUse
		}
	}
	if remainingCost > 0 {
		costs[costItem] += remainingCost
	}
	pool, found := s.resources.Get("Gacha_pools", int64(poolID))
	if !found {
		return DispatchResult{Err: ErrNotOpen}, nil
	}
	var p map[string]json.RawMessage
	if json.Unmarshal(pool, &p) != nil {
		return DispatchResult{Err: ErrNotOpen}, nil
	}
	defaultComb := jsonInt(p, "combDefault", "comb_default")
	guaranteeComb := jsonInt(p, "combGuarantee", "comb_guarantee")
	roleComb := jsonInt(p, "combRole", "comb_role")
	if defaultComb <= 0 {
		return DispatchResult{Err: ErrNotOpen}, nil
	}
	history, err := s.store.GachaPityHistory(ctx, pid, poolID, 40)
	if err != nil {
		return DispatchResult{}, err
	}
	noPurple, noHero := int32(0), int32(0)
	for _, itemID := range history {
		purple, hero, known := s.gachaItemClass(itemID)
		if !known {
			return DispatchResult{Err: ErrNotOpen}, nil
		}
		if purple {
			noPurple = 0
		} else {
			noPurple++
		}
		if hero {
			noHero = 0
		} else {
			noHero++
		}
	}
	rewards := make([]store.GachaDrop, 0, count)
	for i := int32(0); i < count; i++ {
		combID := defaultComb
		forceHero := noHero >= 39 && roleComb > 0
		forcePurple := !forceHero && noPurple >= 9 && guaranteeComb > 0
		if forceHero {
			combID = roleComb
		} else if forcePurple {
			combID = guaranteeComb
		}
		reward, yes := s.rollGachaReward(combID)
		if !yes {
			return DispatchResult{Err: ErrNotOpen}, nil
		}
		purple, hero, known := s.gachaItemClass(reward.ItemID)
		if !known || (forceHero && (!hero || !purple)) || (forcePurple && !purple) {
			return DispatchResult{Err: ErrNotOpen}, nil
		}
		reward.Transform, yes = s.gachaItemTransform(reward.ItemID)
		if !yes {
			return DispatchResult{Err: ErrNotOpen}, nil
		}
		if purple {
			noPurple = 0
		} else {
			noPurple++
		}
		if hero {
			noHero = 0
		} else {
			noHero++
		}
		rewards = append(rewards, reward)
	}
	bonus, ok := gachaBonus(p, count)
	if !ok {
		return DispatchResult{Err: ErrNotOpen}, nil
	}
	raw, err := proto.Marshal(q)
	if err != nil {
		return DispatchResult{}, err
	}
	draw, err := s.store.CommitGachaDraw(ctx, pid, in.CmdID, in.UPSN, raw, q.DefId, poolID, count, costs, rewards, bonus)
	if errors.Is(err, store.ErrInventoryInsufficient) {
		return DispatchResult{Err: ErrItemEnough}, nil
	}
	if errors.Is(err, store.ErrGachaActionConflict) {
		return DispatchResult{Err: ErrInvalidParam}, nil
	}
	if err != nil {
		return DispatchResult{}, err
	}
	responseRewards := make([]*protocolpb.GachaReward, 0, len(draw.Rewards))
	for _, reward := range draw.Rewards {
		responseRewards = append(responseRewards, &protocolpb.GachaReward{ItemId: reward.ItemID, Count: reward.Count})
	}
	push := s.pushFor("BagItemChangeS2C", &protocolpb.BagItemChangeS2C{Item: pveInventoryMessages(draw.Inventory), IsNotShow: true}, []int64{pid}, 0)
	return DispatchResult{Message: &protocolpb.GachaS2C{Reward: responseRewards, Count: draw.Progress.Count, PoolId: poolID}, Pushes: []Push{push}}, nil
}

func (s *Server) handleGachaRecord(ctx context.Context, sess *Session, q *protocolpb.GachaRecordC2S) (DispatchResult, error) {
	pid, ok := s.currentPlayerID(sess)
	if !ok {
		return DispatchResult{Err: ErrAuth}, nil
	}
	if q.Id < 0 {
		return DispatchResult{Err: ErrInvalidParam}, nil
	}
	records, err := s.store.GachaHistory(ctx, pid, q.Id)
	if err != nil {
		return DispatchResult{}, err
	}
	response := make([]*modelpb.GachaRecord, 0, len(records))
	for _, record := range records {
		response = append(response, &modelpb.GachaRecord{ItemId: record.ItemID, ItemCount: record.ItemCount, IsConvert: record.IsConvert, Time: record.Time})
	}
	return DispatchResult{Message: &protocolpb.GachaRecordS2C{Records: response}}, nil
}

func (s *Server) handlePlayerShopBuy(ctx context.Context, sess *Session, in wire.Frame, q *protocolpb.PlayerShopBuyC2S) (DispatchResult, error) {
	pid, ok := s.currentPlayerID(sess)
	if !ok {
		return DispatchResult{Err: ErrAuth}, nil
	}
	count := q.BuyCount
	if count <= 0 {
		count = 1
	}
	if count > 99 {
		return DispatchResult{Err: ErrInvalidParam}, nil
	}
	var itemID, currencyID, price, itemNum int32
	found := false
	for _, raw := range s.resources.Rows("ExchangeStore_goodss") {
		var row map[string]json.RawMessage
		if json.Unmarshal(raw, &row) != nil {
			continue
		}
		for _, child := range jsonItems(row["exchangeStoreGoodsConfigureItems"]) {
			if jsonInt(child, "goodsID", "goodsId") == q.GoodsId {
				itemID = jsonInt(child, "itemID", "itemId")
				currencyID = jsonInt(child, "currencyID", "currencyId")
				price = jsonInt(child, "discountPrice", "discount_price", "originalPrice")
				itemNum = jsonInt(child, "itemNum", "item_num")
				found = true
				break
			}
		}
		if found {
			break
		}
	}
	if !found {
		return DispatchResult{Err: ErrNotOpen}, nil
	}
	if itemNum <= 0 || price < 0 {
		return DispatchResult{Err: ErrNotOpen}, nil
	}
	if err := s.store.ConsumeItem(ctx, pid, currencyID, price*count); err != nil {
		return DispatchResult{Err: ErrItemEnough}, nil
	}
	if err := s.store.AddItem(ctx, pid, itemID, itemNum*count); err != nil {
		return DispatchResult{}, err
	}
	if in.UPSN > 0 {
		raw, _ := proto.Marshal(q)
		_, _ = s.store.AddAction(ctx, 0, pid, in.CmdID, in.UPSN, raw)
	}
	msg := &protocolpb.PlayerShopBuyS2C{Type: q.Type, GoodsId: q.GoodsId, BuyCount: count, ChainId: q.ChainId, DiscountCardId: q.DiscountCardId}
	return DispatchResult{Message: msg}, nil
}

func (s *Server) handleMailRead(ctx context.Context, sess *Session, q *protocolpb.MailReadC2S) (DispatchResult, error) {
	pid, ok := s.currentPlayerID(sess)
	if !ok {
		return DispatchResult{Err: ErrAuth}, nil
	}
	if err := s.store.MarkMailRead(ctx, pid, q.Id); err != nil {
		return DispatchResult{Err: ErrInvalidParam}, nil
	}
	return DispatchResult{Message: &protocolpb.MailReadS2C{}}, nil
}
func (s *Server) handleMailStar(ctx context.Context, sess *Session, q *protocolpb.MailStarC2S) (DispatchResult, error) {
	pid, ok := s.currentPlayerID(sess)
	if !ok {
		return DispatchResult{Err: ErrAuth}, nil
	}
	if err := s.store.StarMail(ctx, pid, q.Id, q.IsStarMail); err != nil {
		return DispatchResult{Err: ErrInvalidParam}, nil
	}
	return DispatchResult{Message: &protocolpb.MailStarS2C{Id: q.Id, IsStarMail: q.IsStarMail}}, nil
}
func (s *Server) handleMailReward(ctx context.Context, sess *Session, q *protocolpb.MailGetRewardC2S) (DispatchResult, error) {
	pid, ok := s.currentPlayerID(sess)
	if !ok {
		return DispatchResult{Err: ErrAuth}, nil
	}
	granted, err := s.MailImplClaim(ctx, pid, q.Id)
	if err != nil {
		return DispatchResult{Err: ErrRepeatedReward}, nil
	}
	s.MailImplInventoryPush(pid, granted)
	return DispatchResult{Message: &protocolpb.MailGetRewardS2C{Ids: []int32{q.Id}}}, nil
}
func (s *Server) handleMailDelete(ctx context.Context, sess *Session, q *protocolpb.MailDelReadC2S) (DispatchResult, error) {
	pid, ok := s.currentPlayerID(sess)
	if !ok {
		return DispatchResult{Err: ErrAuth}, nil
	}
	if err := s.store.DeleteReadMail(ctx, pid, q.Id); err != nil {
		return DispatchResult{Err: ErrInvalidParam}, nil
	}
	return DispatchResult{Message: &protocolpb.MailDelReadS2C{Ids: []int32{q.Id}}}, nil
}
func (s *Server) handleChangeName(ctx context.Context, sess *Session, q *protocolpb.ChangeNameC2S) (DispatchResult, error) {
	pid, ok := s.currentPlayerID(sess)
	if !ok {
		return DispatchResult{Err: ErrAuth}, nil
	}
	nick := strings.TrimSpace(q.Nick)
	if len([]rune(nick)) < 1 || len([]rune(nick)) > 16 {
		return DispatchResult{Err: ErrInvalidParam}, nil
	}
	next, err := s.store.ChangeName(ctx, pid, nick)
	if err != nil {
		return DispatchResult{Err: ErrInvalidParam}, nil
	}
	return DispatchResult{Message: &protocolpb.ChangeNameS2C{PlayerName: nick, NextChangeNameTime: next}}, nil
}
