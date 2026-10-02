using Google.Protobuf.Reflection;

namespace party.code;

public enum Code
{
	[OriginalName("Succ")]
	Succ = 0,
	[OriginalName("Succ_Sys")]
	SuccSys = 1,
	[OriginalName("ServerClosed")]
	ServerClosed = 1001,
	[OriginalName("ServerMaxPlayer")]
	ServerMaxPlayer = 1002,
	[OriginalName("WaitingInQueue")]
	WaitingInQueue = 1003,
	[OriginalName("BUFFUSE_NOCOUNT")]
	BuffuseNocount = 5000,
	[OriginalName("AuthErr")]
	AuthErr = 10000,
	[OriginalName("AuthTypeNotExist")]
	AuthTypeNotExist = 10001,
	[OriginalName("AuthEnterGameErr")]
	AuthEnterGameErr = 10002,
	[OriginalName("AccountBanned")]
	AccountBanned = 10003,
	[OriginalName("BlackPayBanned")]
	BlackPayBanned = 10004,
	[OriginalName("ConfigErr")]
	ConfigErr = 10010,
	[OriginalName("ItemEnoughErr")]
	ItemEnoughErr = 10011,
	[OriginalName("ClientVerErr")]
	ClientVerErr = 10012,
	[OriginalName("InvalidParamErr")]
	InvalidParamErr = 10013,
	[OriginalName("NotOpen")]
	NotOpen = 10015,
	[OriginalName("ConditionErr")]
	ConditionErr = 10016,
	[OriginalName("RepeatedReward")]
	RepeatedReward = 10017,
	[OriginalName("BuyErr")]
	BuyErr = 10018,
	[OriginalName("InviteCodeErr")]
	InviteCodeErr = 10019,
	[OriginalName("RepeatedLogin")]
	RepeatedLogin = 10020,
	[OriginalName("SkinSellNoNeedToBuy")]
	SkinSellNoNeedToBuy = 10021,
	[OriginalName("NowMutePlayer")]
	NowMutePlayer = 10022,
	[OriginalName("RoomErr")]
	RoomErr = 11000,
	[OriginalName("RoomNotExist")]
	RoomNotExist = 11001,
	[OriginalName("RoomPlayerAlreadyJoin")]
	RoomPlayerAlreadyJoin = 11002,
	[OriginalName("RoomSlotErr")]
	RoomSlotErr = 11003,
	[OriginalName("RoomSlotAlreadyOccupied")]
	RoomSlotAlreadyOccupied = 11004,
	[OriginalName("RoomPlayerNotExit")]
	RoomPlayerNotExit = 11005,
	[OriginalName("RoomStateAlreadyOK")]
	RoomStateAlreadyOk = 11006,
	[OriginalName("RoomFull")]
	RoomFull = 11007,
	[OriginalName("RoomNotWait")]
	RoomNotWait = 11008,
	[OriginalName("RoomPwdErr")]
	RoomPwdErr = 11009,
	[OriginalName("RoomPlayerTooLittle")]
	RoomPlayerTooLittle = 11010,
	[OriginalName("RoomHeroNotExit")]
	RoomHeroNotExit = 11011,
	[OriginalName("RoomHeroAlreadyChoice")]
	RoomHeroAlreadyChoice = 11012,
	[OriginalName("RoomHeroChoiceNotWheel")]
	RoomHeroChoiceNotWheel = 11013,
	[OriginalName("RoomHeroNotUse")]
	RoomHeroNotUse = 11014,
	[OriginalName("RoomNotReady1")]
	RoomNotReady1 = 11015,
	[OriginalName("RoomMapNotExist")]
	RoomMapNotExist = 11016,
	[OriginalName("RoomMapNotData")]
	RoomMapNotData = 11017,
	[OriginalName("RoomActionNotExistPre")]
	RoomActionNotExistPre = 11018,
	[OriginalName("RoomActionIncorrect")]
	RoomActionIncorrect = 11019,
	[OriginalName("RoomFrontNodeIdsZero")]
	RoomFrontNodeIdsZero = 11020,
	[OriginalName("RoomNotAction")]
	RoomNotAction = 11021,
	[OriginalName("RoomActionAlreadyDone")]
	RoomActionAlreadyDone = 11022,
	[OriginalName("RoomMoveDirErr")]
	RoomMoveDirErr = 11023,
	[OriginalName("RoomActionPlayerErr")]
	RoomActionPlayerErr = 11024,
	[OriginalName("RoomLandDataNotExist")]
	RoomLandDataNotExist = 11025,
	[OriginalName("RoomLandHandleNotExist")]
	RoomLandHandleNotExist = 11026,
	[OriginalName("RoomLandTypeNotExist")]
	RoomLandTypeNotExist = 11027,
	[OriginalName("RoomLandParamErr")]
	RoomLandParamErr = 11028,
	[OriginalName("RoomShopBuyCardNotHit")]
	RoomShopBuyCardNotHit = 11029,
	[OriginalName("RoomShopBuyGoldLack")]
	RoomShopBuyGoldLack = 11030,
	[OriginalName("RoomShopBuyHandCardLimit")]
	RoomShopBuyHandCardLimit = 11031,
	[OriginalName("RoomNotSpecifyLandType")]
	RoomNotSpecifyLandType = 11032,
	[OriginalName("RoomCalDirErr")]
	RoomCalDirErr = 11033,
	[OriginalName("RoomNotSelectSelf")]
	RoomNotSelectSelf = 11034,
	[OriginalName("RoomPursuitGoldLack")]
	RoomPursuitGoldLack = 11035,
	[OriginalName("RoomGoldLack")]
	RoomGoldLack = 11036,
	[OriginalName("RoomPlayerInHospital")]
	RoomPlayerInHospital = 11037,
	[OriginalName("RoomBattleNotExist")]
	RoomBattleNotExist = 11038,
	[OriginalName("RoomCardNotExist")]
	RoomCardNotExist = 11039,
	[OriginalName("RoomCardEffTypeNoMatch")]
	RoomCardEffTypeNoMatch = 11040,
	[OriginalName("RoomBattleNotAttacker")]
	RoomBattleNotAttacker = 11041,
	[OriginalName("RoomBattleNotDefender")]
	RoomBattleNotDefender = 11042,
	[OriginalName("RoomBattleNoPlayer")]
	RoomBattleNoPlayer = 11043,
	[OriginalName("RoomUseCardCostOverflow")]
	RoomUseCardCostOverflow = 11044,
	[OriginalName("RoomBuffNotDefine")]
	RoomBuffNotDefine = 11045,
	[OriginalName("RoomBuffNotRegHandle")]
	RoomBuffNotRegHandle = 11046,
	[OriginalName("RoomEventNotDefine")]
	RoomEventNotDefine = 11047,
	[OriginalName("RoomEventNotRegHandle")]
	RoomEventNotRegHandle = 11048,
	[OriginalName("RoomEventDefParamErr")]
	RoomEventDefParamErr = 11049,
	[OriginalName("RoomLotteryNumErr")]
	RoomLotteryNumErr = 11050,
	[OriginalName("RoomTragetChoiceErr")]
	RoomTragetChoiceErr = 11051,
	[OriginalName("RoomCardCantNotUse")]
	RoomCardCantNotUse = 11052,
	[OriginalName("RoomChoiceDirErr")]
	RoomChoiceDirErr = 11053,
	[OriginalName("RoomSummonNotExist")]
	RoomSummonNotExist = 11054,
	[OriginalName("RoomNoChoiceLand")]
	RoomNoChoiceLand = 11055,
	[OriginalName("RoomChoiceLandErr")]
	RoomChoiceLandErr = 11056,
	[OriginalName("RoomBuffOutRefNoExist")]
	RoomBuffOutRefNoExist = 11057,
	[OriginalName("RoomCardNoCanUse")]
	RoomCardNoCanUse = 11058,
	[OriginalName("RoomDicePointOverflow")]
	RoomDicePointOverflow = 11059,
	[OriginalName("RoomDivinationNotExist")]
	RoomDivinationNotExist = 11060,
	[OriginalName("RoomNotChoiceDivination")]
	RoomNotChoiceDivination = 11061,
	[OriginalName("RoomCardHandleNoReg")]
	RoomCardHandleNoReg = 11062,
	[OriginalName("RoomCardLimitErr")]
	RoomCardLimitErr = 11063,
	[OriginalName("RoomStopOrContinueErr")]
	RoomStopOrContinueErr = 11064,
	[OriginalName("RoomSkillUseNumErr")]
	RoomSkillUseNumErr = 11065,
	[OriginalName("RoomSkillItemNotExist")]
	RoomSkillItemNotExist = 11066,
	[OriginalName("RoomSkillHandleNoReg")]
	RoomSkillHandleNoReg = 11067,
	[OriginalName("RoomSkillCdIng")]
	RoomSkillCdIng = 11068,
	[OriginalName("RoomHeroAlreadyAffirm")]
	RoomHeroAlreadyAffirm = 11069,
	[OriginalName("RoomHeroNotChoice")]
	RoomHeroNotChoice = 11070,
	[OriginalName("RoomAddRoomNoSucc")]
	RoomAddRoomNoSucc = 11071,
	[OriginalName("RoomAllotIdFail")]
	RoomAllotIdFail = 11072,
	[OriginalName("RoomShopBuyAlreadyHit")]
	RoomShopBuyAlreadyHit = 11073,
	[OriginalName("RoomHeroNotHospital")]
	RoomHeroNotHospital = 11074,
	[OriginalName("RoomChatCdIng")]
	RoomChatCdIng = 11075,
	[OriginalName("RoomChatNoExpression")]
	RoomChatNoExpression = 11076,
	[OriginalName("RoomReadyNoExpression")]
	RoomReadyNoExpression = 11077,
	[OriginalName("RoomPlayerNumErr")]
	RoomPlayerNumErr = 11078,
	[OriginalName("RoomNotRunning")]
	RoomNotRunning = 11079,
	[OriginalName("RoomOver")]
	RoomOver = 11080,
	[OriginalName("RoomWatchCode")]
	RoomWatchCode = 11081,
	[OriginalName("RoomWatchPlayerLimit")]
	RoomWatchPlayerLimit = 11082,
	[OriginalName("RoomMatchPlayerOffline")]
	RoomMatchPlayerOffline = 11083,
	[OriginalName("RoomMatchInPunishment")]
	RoomMatchInPunishment = 11084,
	[OriginalName("RoomSurrenderPunishment")]
	RoomSurrenderPunishment = 11085,
	[OriginalName("RoomChooseSkinNotOwn")]
	RoomChooseSkinNotOwn = 11086,
	[OriginalName("RoomChooseSkinMismatched")]
	RoomChooseSkinMismatched = 11087,
	[OriginalName("RoomChooseSkinNotExist")]
	RoomChooseSkinNotExist = 11088,
	[OriginalName("RoomChooseSkinShouldAffirmHeroFirst")]
	RoomChooseSkinShouldAffirmHeroFirst = 11089,
	[OriginalName("RoomChooseSkinShouldInBox")]
	RoomChooseSkinShouldInBox = 11090,
	[OriginalName("RoomNeedPwd")]
	RoomNeedPwd = 11091,
	[OriginalName("RoomChangeSlotErr")]
	RoomChangeSlotErr = 11092,
	[OriginalName("PlayerNotFind")]
	PlayerNotFind = 12001,
	[OriginalName("Friend")]
	Friend = 12002,
	[OriginalName("FriendApply")]
	FriendApply = 12003,
	[OriginalName("FriendLimit")]
	FriendLimit = 12004,
	[OriginalName("TargetFriendLimit")]
	TargetFriendLimit = 12005,
	[OriginalName("FriendApplyNotFind")]
	FriendApplyNotFind = 12006,
	[OriginalName("FriendApplyHandler")]
	FriendApplyHandler = 12007,
	[OriginalName("FriendBlacksLimit")]
	FriendBlacksLimit = 12008,
	[OriginalName("PlayerNotFriend")]
	PlayerNotFriend = 12009,
	[OriginalName("PlayerOffline")]
	PlayerOffline = 12010,
	[OriginalName("FriendNotInvite")]
	FriendNotInvite = 12011,
	[OriginalName("FriendChatMsgErr")]
	FriendChatMsgErr = 12012,
	[OriginalName("FriendNoPlayerSimpleInfo")]
	FriendNoPlayerSimpleInfo = 12013,
	[OriginalName("GetFriendListTooFrequency")]
	GetFriendListTooFrequency = 12014,
	[OriginalName("FriendApplyLimit")]
	FriendApplyLimit = 12015,
	[OriginalName("FriendInBlackList")]
	FriendInBlackList = 12016,
	[OriginalName("ShopNotOpen")]
	ShopNotOpen = 15001,
	[OriginalName("ShopNotFindGoods")]
	ShopNotFindGoods = 15002,
	[OriginalName("ShopBuyLimit")]
	ShopBuyLimit = 15003,
	[OriginalName("ShopBuyFrontLimit")]
	ShopBuyFrontLimit = 15004,
	[OriginalName("ShopBuyAlready")]
	ShopBuyAlready = 15005,
	[OriginalName("CdkInvalid")]
	CdkInvalid = 15011,
	[OriginalName("CdkRepeat")]
	CdkRepeat = 15012,
	[OriginalName("CdkExists")]
	CdkExists = 15013,
	[OriginalName("CdkNotInTime")]
	CdkNotInTime = 15014,
	[OriginalName("CampaignNotExist")]
	CampaignNotExist = 15015,
	[OriginalName("FormerCampaignNotPass")]
	FormerCampaignNotPass = 15016,
	[OriginalName("CdkException")]
	CdkException = 15017,
	[OriginalName("CdkOutLimit")]
	CdkOutLimit = 15018,
	[OriginalName("BattlePassNotExist")]
	BattlePassNotExist = 15100,
	[OriginalName("MatchTeamErr")]
	MatchTeamErr = 16000,
	[OriginalName("MatchTeamNotExist")]
	MatchTeamNotExist = 16001,
	[OriginalName("MatchTeamPlayerAlreadyJoin")]
	MatchTeamPlayerAlreadyJoin = 16002,
	[OriginalName("MatchTeamPlayerNotExit")]
	MatchTeamPlayerNotExit = 16003,
	[OriginalName("MatchTeamFull")]
	MatchTeamFull = 16004,
	[OriginalName("MatchTeamNotWaiting")]
	MatchTeamNotWaiting = 16005,
	[OriginalName("MatchTeamPlayerIsInTeam")]
	MatchTeamPlayerIsInTeam = 16006,
	[OriginalName("MatchParamErr")]
	MatchParamErr = 16007,
	[OriginalName("MatchTeamNotLeader")]
	MatchTeamNotLeader = 16008,
	[OriginalName("MatchTeamPlayerNotReady")]
	MatchTeamPlayerNotReady = 16009,
	[OriginalName("GuildServerErr")]
	GuildServerErr = 17000,
	[OriginalName("GuildFull")]
	GuildFull = 17002,
	[OriginalName("GuildNameExists")]
	GuildNameExists = 17003,
	[OriginalName("GuildAlreayJoin")]
	GuildAlreayJoin = 17004,
	[OriginalName("GuildSearchCdIng")]
	GuildSearchCdIng = 17005,
	[OriginalName("GuildBanned")]
	GuildBanned = 17006,
	[OriginalName("GuildNotExist")]
	GuildNotExist = 17007,
	[OriginalName("GuildAuthErr")]
	GuildAuthErr = 17009,
	[OriginalName("GuildApplicationExpired")]
	GuildApplicationExpired = 17010,
	[OriginalName("GuildPlayerNotExist")]
	GuildPlayerNotExist = 17011,
	[OriginalName("GuildAlreayInvited")]
	GuildAlreayInvited = 17012,
	[OriginalName("GuildInvitationExpired")]
	GuildInvitationExpired = 17013,
	[OriginalName("GuildUpdateLimit")]
	GuildUpdateLimit = 17014,
	[OriginalName("GuildPlayerNotInGuild")]
	GuildPlayerNotInGuild = 17015,
	[OriginalName("GuildMuted")]
	GuildMuted = 17016,
	[OriginalName("GuildCreateBanned")]
	GuildCreateBanned = 17017,
	[OriginalName("GuildFrozen")]
	GuildFrozen = 17018,
	[OriginalName("GuildChatCdIng")]
	GuildChatCdIng = 17019
}
