using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace party.protocol;

public sealed class protocol : IMessage<protocol>, IMessage, IEquatable<protocol>, IDeepCloneable<protocol>, IBufferMessage
{
	public enum MsgOneofCase
	{
		None = 0,
		SysSendMailC2S = 101,
		SysPlayerOnlineC2S = 102,
		PlayerOnlineRoomC2S = 103,
		SysCanPraiseInfoC2S = 104,
		SysPraiseC2S = 105,
		SysRoomFinishC2S = 106,
		SysRoomAddExpC2S = 107,
		SysGetShowFriendC2S = 108,
		SysFriendInviteC2S = 109,
		SysFriendDelC2S = 110,
		SysFriendInfoC2S = 111,
		SysFriendApplyC2S = 112,
		SysFriendAddC2S = 113,
		SysFriendSendMsgC2S = 114,
		SysCampaignFinishC2S = 115,
		SysAbroadPayMsgC2S = 116,
		AcquisitionMsgC2S = 117,
		SysFightRecord = 118,
		SysCampaignAwardC2S = 119,
		SysRecoupItemC2S = 120,
		SysPlayerOnlineRoomC2S = 121,
		SysPlayerCleanC2S = 122,
		SysPlayerPunishmentTimeC2S = 123,
		SysMatchSuccess = 124,
		SysChinaPayMsgC2S = 125,
		SysChangeMatchTeamState = 126,
		SysSaveSimplePlayerInfoC2S = 127,
		SysGmChangeNameC2S = 128,
		SysSyncPlayerC2S = 129,
		SysPlayerCreditScoreChangeC2S = 130,
		SysSyncPlayerMatchPunishmentTimeC2S = 131,
		GMChangeCreditScoreC2S = 132,
		SysPushReturnInfoC2S = 133,
		SysMutePlayerC2S = 134,
		SysQuestionC2S = 135,
		KickS2C = 1001,
		PredictActionS2C = 1002,
		RunningGameS2C = 1003,
		BattleS2C = 1007,
		LotteryDrawS2C = 1011,
		LandBuffsS2C = 1013,
		RoundStartS2C = 1015,
		GameFinishS2C = 1016,
		MonsterRefreshS2C = 1018,
		MovePointBuffS2C = 1019,
		ChangeDirS2C = 1020,
		GambleChangeS2C = 1022,
		HeroBarBoxChangeS2C = 1023,
		RoomNotifyS2C = 1024,
		ActionStartNotifyS2C = 1026,
		NoGambleNotifyS2C = 1031,
		GambleObServeS2C = 1034,
		UpdateHeroAttrS2C = 1040,
		ChangePlayerSlotS2C = 1042,
		BossSleepS2C = 1043,
		RefMallS2C = 1044,
		BagItemChangeS2C = 1045,
		RoleCardChangeS2C = 1046,
		TaskConditionS2C = 1047,
		TaskInfoS2C = 1048,
		PlayerOnlineS2C = 1049,
		ChangeExpS2C = 1050,
		MailAddS2C = 1051,
		OnlineSyncRoomIdS2C = 1052,
		NoticeS2C = 1053,
		ActivityTaskConditionS2C = 1054,
		MapEventS2C = 1055,
		Day7RewardS2C = 1056,
		MapEventTrainS2C = 1057,
		ChangePraiseNumS2C = 1058,
		MonthlyCardS2C = 1059,
		MailDelS2C = 1060,
		FriendNotifyS2C = 1061,
		FriendListChangeS2C = 1062,
		FriendInviteNotifyS2C = 1063,
		LoopNoticeS2C = 1064,
		BattlePassLvS2C = 1065,
		BattlePassTaskInfoS2C = 1066,
		BattlePassUpdateTaskS2C = 1067,
		BattlePassBuyS2C = 1068,
		BattlePassInfoS2C = 1069,
		FriendsChatMsgS2C = 1070,
		GameProgressChangeS2C = 1071,
		MapMissionNotifyS2C = 1072,
		ChangeItemLimitS2C = 1073,
		CleanItemLimitS2C = 1074,
		CampaignPassS2C = 1075,
		CampaignNotifyS2C = 1076,
		KillMessageS2C = 1077,
		GameScoreChangeS2C = 1078,
		SignInRewardS2C = 1079,
		PayResultS2C = 1080,
		PayInfoChangeS2C = 1081,
		InviteSuccessS2C = 1082,
		InviteInfoNotifyS2C = 1083,
		MapStatusChangeS2C = 1084,
		GachaCountS2C = 1085,
		MapIndexChangeS2C = 1086,
		SurrenderPunishS2C = 1087,
		GamePassMapSuccessS2C = 1088,
		FriendDelNotifyS2C = 1090,
		BagExpiredTransformNotify = 1091,
		MapEventCrabS2C = 1092,
		PkAfterVoteS2C = 1093,
		PlayerTaskNotifyS2C = 1094,
		UnLockDifficultyS2C = 1095,
		HeroSkillMoveEffectS2C = 1096,
		TimeOutKickPlayerS2C = 1097,
		SayPhraseNotifyS2C = 1098,
		MatchTeamInviteNotify = 1099,
		RefreshMatchTeamStateNotify = 1100,
		ActivityPassGearChangeS2C = 1101,
		SingleGameScoreChange = 1102,
		DelayProgressMapEventS2C = 1103,
		LuckyStarMissionChangeS2C = 1104,
		MatchPunishmentS2C = 1105,
		ChallengeDataChangeS2C = 1106,
		GmUnlockRoleInfoS2C = 1107,
		SyncPlayerCreditInfoS2C = 1108,
		RoomHeroCardChangeS2C = 1109,
		RoomRoundAddTermS2C = 1110,
		ReturnInfoS2C = 1111,
		SyncRelicsS2C = 1112,
		ReplaySnapshotS2C = 1113,
		ClueNotifyS2C = 1114,
		ReplayDieS2C = 1115,
		GuildTaskNotifyS2C = 1116,
		GameRoundChangeS2C = 1117,
		NotifyQuestionS2C = 1118,
		ConnectHandle = 5001,
		Connect = 5002,
		HeartbeatHandle = 5003,
		Heartbeat = 5004,
		CreateRoomHandle = 5005,
		CreateRoom = 5006,
		SyncRoomHandle = 5007,
		SyncRoom = 5008,
		JoinRoomHandle = 5009,
		JoinRoom = 5010,
		ExitRoomHandle = 5011,
		ExitRoom = 5012,
		QueryRoomHandle = 5013,
		QueryRoom = 5014,
		RefreshRoomStateHandle = 5015,
		RefreshRoomState = 5016,
		StartGameHandle = 5019,
		StartGame = 5020,
		ThrowDiceHandle = 5021,
		ThrowDice = 5022,
		ChangeRoomHandle = 5023,
		ChangeRoom = 5024,
		MoveHandle = 5027,
		Move = 5028,
		ShopBuyHandle = 5029,
		ShopBuy = 5030,
		PursuitHandle = 5033,
		Pursuit = 5034,
		BattleUseCardHandle = 5035,
		BattleUseCard = 5036,
		BattleThrowDiceHandle = 5037,
		BattleThrowDice = 5038,
		BattleChoiceHandle = 5039,
		BattleChoice = 5040,
		LotteryChoiceHandle = 5041,
		LotteryChoice = 5042,
		MoveAgainHandle = 5043,
		MoveAgain = 5044,
		AskBattleHandle = 5047,
		AskBattle = 5048,
		RollGoldHandle = 5049,
		RollGold = 5050,
		EventThrowDiceHandle = 5051,
		EventThrowDice = 5052,
		TriggerEventHandle = 5053,
		TriggerEvent = 5054,
		UseEffectCardHandle = 5055,
		UseEffectCard = 5056,
		BombThrowDiceHandle = 5059,
		BombThrowDice = 5060,
		ChoiceDirectionHandle = 5061,
		ChoiceDirection = 5062,
		LandChoiceTargetHandle = 5063,
		LandChoiceTarget = 5064,
		ThrowDiceResultHandle = 5067,
		ThrowDiceResult = 5068,
		TriggerDivinationHandle = 5069,
		TriggerDivination = 5070,
		TriggerDestinyHandle = 5071,
		TriggerDestiny = 5072,
		UseQuickCardHandle = 5073,
		UseQuickCard = 5074,
		AbandonCardHandle = 5075,
		AbandonCard = 5076,
		StopOrContinueHandle = 5077,
		StopOrContinue = 5078,
		StartGambleHandle = 5081,
		StartGamble = 5082,
		GambleThrowDicHandle = 5083,
		GambleThrowDic = 5084,
		ChoiceHeroC2S2Handle = 5085,
		ChoiceHero2 = 5086,
		AffirmHeroHandle = 5087,
		AffirmHero = 5088,
		SearchRoomHandle = 5089,
		SearchRoom = 5090,
		GmHandle = 5091,
		Gm = 5092,
		TriggerHospitalHandle = 5093,
		TriggerHospital = 5094,
		SendChatHandle = 5095,
		SendChat = 5096,
		PlayerShopBuyC2S = 5097,
		PlayerShopBuyS2C = 5098,
		PlayerUseItemHandle = 5099,
		PlayerUseItem = 5100,
		UseTreasureC2S = 5101,
		UseTreasureS2C = 5102,
		UseTreasureAutoTransformC2S = 5123,
		UseTreasureAutoTransformS2C = 5124,
		SetFashionC2S = 5103,
		SetFashionS2C = 5104,
		SelectFashionPlanC2S = 5105,
		SelectFashionPlanS2C = 5106,
		GachaC2S = 5107,
		GachaS2C = 5108,
		SteamSearchRoomC2S = 5109,
		SteamSearchRoomS2C = 5110,
		CheatItemHandle = 5111,
		CheatItem = 5112,
		RoleCardUpLvC2S = 5113,
		RoleCardUpLvS2C = 5114,
		RoleCardBreakThroughC2S = 5115,
		RoleCardBreakThroughS2C = 5116,
		RoleCardChoiceResC2S = 5117,
		RoleCardChoiceResS2C = 5118,
		TaskRewardC2S = 5119,
		TaskRewardS2C = 5120,
		TeachingC2S = 5121,
		TeachingS2C = 5122,
		QuickJoinRoomC2S = 5125,
		QuickJoinRoomS2C = 5126,
		RoomKickPlayerC2S = 5127,
		RoomKickPlayerS2C = 5128,
		RoomAbdicationC2S = 5129,
		RoomAbdicationS2C = 5130,
		RoomReadyC2S = 5131,
		RoomReadyS2C = 5132,
		ChargeCreateC2S = 5133,
		ChargeCreateS2C = 5134,
		ChargeC2S = 5135,
		ChargeS2C = 5136,
		GiftCdkC2S = 5137,
		GiftCdkS2C = 5138,
		MailReadC2S = 5139,
		MailReadS2C = 5140,
		MailGetRewardC2S = 5141,
		MailGetRewardS2C = 5142,
		MailDelReadC2S = 5143,
		MailDelReadS2C = 5144,
		GachaRecordC2S = 5145,
		GachaRecordS2C = 5146,
		ActivityTaskRewardC2S = 5147,
		ActivityTaskRewardS2C = 5148,
		RoomShortChatC2S = 5149,
		RoomShortChatS2C = 5150,
		SetShowPlayerC2S = 5151,
		SetShowPlayerS2C = 5152,
		GetShowPlayerC2S = 5153,
		GetShowPlayerS2C = 5154,
		GetPlayerFightRecordC2S = 5155,
		GetPlayerFightRecordS2C = 5156,
		GetDay7RewardC2S = 5157,
		GetDay7RewardS2C = 5158,
		PraisePlayerC2S = 5159,
		PraisePlayerS2C = 5160,
		ClientDataUploadC2S = 5161,
		ClientDataUploadS2C = 5162,
		FriendListC2S = 5163,
		FriendListS2C = 5164,
		FriendApplyC2S = 5165,
		FriendApplyS2C = 5166,
		FriendApplyListC2S = 5167,
		FriendApplyListS2C = 5168,
		FriendApplyOpC2S = 5169,
		FriendApplyOpS2C = 5170,
		FriendOpC2S = 5171,
		FriendOpS2C = 5172,
		FriendInviteC2S = 5173,
		FriendInviteS2C = 5174,
		FriendInviteListC2S = 5175,
		FriendInviteListS2C = 5176,
		FriendInviteCleanC2S = 5179,
		FriendInviteCleanS2C = 5180,
		FriendBlacksListC2S = 5181,
		FriendBlacksListS2C = 5182,
		NearFightPlayerC2S = 5183,
		NearFightPlayerS2C = 5184,
		SearchPlayerC2S = 5185,
		SearchPlayerS2C = 5186,
		ScratchCardC2S = 5187,
		ScratchCardS2C = 5188,
		NextScratchCardPoolC2S = 5189,
		NextScratchCardPoolS2C = 5190,
		WatchJoinRoomC2S = 5191,
		WatchJoinRoomS2C = 5192,
		WatchRefreshRoomStateC2S = 5193,
		WatchRefreshRoomStateS2C = 5194,
		WatchExitRoomC2S = 5195,
		WatchExitRoomS2C = 5196,
		BattlePassGetRewardC2S = 5197,
		BattlePassGetRewardS2C = 5198,
		BattlePassTaskRewardC2S = 5199,
		BattlePassTaskRewardS2C = 5200,
		BattlePassUpLvC2S = 5201,
		BattlePassUpLvS2C = 5202,
		FriendSendMsgC2S = 5203,
		FriendSendMsgS2C = 5204,
		GetChatMsgC2S = 5205,
		GetChatMsgS2C = 5206,
		ReadChatMsgC2S = 5207,
		ReadChatMsgS2C = 5208,
		DelChatMsgInfoC2S = 5209,
		DelChatMsgInfoS2C = 5210,
		SelectRelicC2S = 5211,
		SelectRelicS2C = 5212,
		MonsterPursuitC2S = 5213,
		MonsterPursuitS2C = 5214,
		PVEShopBuyC2S = 5215,
		PVEShopBuyS2C = 5216,
		ClientCheckTaskC2S = 5217,
		ClientCheckTaskS2C = 5218,
		PveHeroUpLvC2S = 5219,
		PveHeroUpLvS2C = 5220,
		StartMatchC2S = 5221,
		StartMatchS2C = 5222,
		CancelMatchC2S = 5223,
		CancelMatchS2C = 5224,
		MatchSuccessC2S = 5225,
		MatchSuccessS2C = 5226,
		AccuseC2S = 5227,
		AccuseS2C = 5228,
		SingleCampaignC2S = 5229,
		SingleCampaignS2C = 5230,
		DevChargeC2S = 5231,
		DevChargeS2C = 5232,
		AskReviveTeammateC2S = 5233,
		AskReviveTeammateS2C = 5234,
		GetSignInRewardC2S = 5235,
		GetSignInRewardS2C = 5236,
		ChatMapMarkersC2S = 5237,
		ChatMapMarkersS2C = 5238,
		AbroadCreateOrderC2S = 5239,
		AbroadCreateOrderS2C = 5240,
		AgeVerifyC2S = 5243,
		AgeVerifyS2C = 5244,
		ChangeNameC2S = 5245,
		ChangeNameS2C = 5246,
		ClientClickConfirmTaskC2S = 5247,
		ClientClickConfirmTaskS2C = 5248,
		BuyRelicC2S = 5249,
		BuyRelicS2C = 5250,
		BuyLightGiftC2S = 5251,
		BuyLightGiftS2C = 5252,
		LightGiftC2S = 5253,
		LightGiftS2C = 5254,
		AcquisitionC2S = 5255,
		AcquisitionS2C = 5256,
		AcquisitionRewardC2S = 5257,
		AcquisitionRewardS2C = 5258,
		SelectMechanismC2S = 5259,
		SelectMechanismS2C = 5260,
		GachaCountRewardC2S = 5261,
		GachaCountRewardS2C = 5262,
		GetPlayerSimpleC2S = 5263,
		GetPlayerSimpleS2C = 5264,
		RoleCardCollectC2S = 5265,
		RoleCardCollectS2C = 5266,
		GMPlayerSettingC2S = 5267,
		GMPlayerSettingS2C = 5268,
		SetFriendNoteC2S = 5269,
		SetFriendNoteS2C = 5270,
		SetOnlineStatusC2S = 5271,
		SetOnlineStatusS2C = 5272,
		ChooseSkinC2S = 5305,
		ChooseSkinS2C = 5306,
		TimeWastingC2S = 5307,
		TimeWastingS2C = 5308,
		VoteC2S = 5309,
		VoteS2C = 5310,
		VoteSelectC2S = 5311,
		VoteSelectS2C = 5312,
		NotifyStoryC2S = 5313,
		NotifyStoryS2C = 5314,
		PveHeroTalentUpC2S = 5315,
		PveHeroTalentUpS2C = 5316,
		SelectEventC2S = 5317,
		SelectEventS2C = 5318,
		CampScoreC2S = 5319,
		CampScoreS2C = 5320,
		ActivityMissionRewardC2S = 5321,
		ActivityMissionRewardS2C = 5322,
		VendorBuyCardC2S = 5323,
		VendorBuyCardS2C = 5324,
		TransferStarDiscC2S = 5325,
		TransferStarDiscS2C = 5326,
		GetHeroInfoC2S = 5327,
		GetHeroInfoS2C = 5328,
		CreateMatchTeamC2S = 5329,
		CreateMatchTeamS2C = 5330,
		ChangeMatchTeamC2S = 5331,
		ChangeMatchTeamS2C = 5332,
		JoinMatchTeamC2S = 5333,
		JoinMatchTeamS2C = 5334,
		ExitMatchTeamC2S = 5335,
		ExitMatchTeamS2C = 5336,
		RefreshMatchTeamInfoC2S = 5337,
		RefreshMatchTeamInfoS2C = 5338,
		ChinaCreateOrderC2S = 5339,
		ChinaCreateOrderS2C = 5340,
		MatchTeamInviteC2S = 5341,
		MatchTeamInviteS2C = 5342,
		MatchTeamChatC2S = 5343,
		MatchTeamChatS2C = 5344,
		MatchTeamReadyC2S = 5345,
		MatchTeamReadyS2C = 5346,
		PlayerChatC2S = 5347,
		PlayerChatS2C = 5348,
		SyncSingleGameDataC2S = 5349,
		SyncSingleGameDataS2C = 5350,
		SingleGameDataC2S = 5351,
		SingleGameDataS2C = 5352,
		GetActivityPassRewardC2S = 5353,
		GetActivityPassRewardS2C = 5354,
		ApplyChangeSlotC2S = 5355,
		ApplyChangeSlotS2C = 5356,
		OpsChangeSlotC2S = 5357,
		OpsChangeSlotS2C = 5358,
		RookieGachaRewardC2S = 5359,
		RookieGachaRewardS2C = 5360,
		LiveGiftPackageC2S = 5361,
		LiveGiftPackageS2C = 5362,
		LaborActDiceC2S = 5363,
		LaborActDiceS2C = 5364,
		ActionOverTimeLogC2S = 5365,
		ActionOverTimeLogS2C = 5366,
		ClientHarmonyC2S = 5369,
		ClientHarmonyS2C = 5370,
		MailStarC2S = 5371,
		MailStarS2C = 5372,
		SetCardAltArtC2S = 5373,
		SetCardAltArtS2C = 5374,
		SelectRewardCardC2S = 5377,
		SelectRewardCardS2C = 5378,
		GetQuestionUrlC2S = 5395,
		GetQuestionUrlS2C = 5396,
		GetReturnInfoC2S = 5379,
		GetReturnInfoS2C = 5380,
		ReturnGiftClaimC2S = 5381,
		ReturnGiftClaimS2C = 5382,
		ReturnSignInClaimC2S = 5383,
		ReturnSignInClaimS2C = 5384,
		ReturnSurveyFinishC2S = 5385,
		ReturnSurveyFinishS2C = 5386,
		FlipCardC2S = 5387,
		FlipCardS2C = 5388,
		FlipCardProgressRewardC2S = 5389,
		FlipCardProgressRewardS2C = 5390,
		SyncPlayerGuildS2C = 2001,
		SyncPlayerJoinGuildS2C = 2002,
		GuildChatMsgS2C = 2003,
		SyncGuildS2C = 2004,
		SyncGuildMemberS2C = 2005,
		SyncGuildMemberExitS2C = 2006,
		CreateGuildC2S = 9001,
		CreateGuildS2C = 9002,
		SearchGuildC2S = 9003,
		SearchGuildS2C = 9004,
		ApplyToGuildC2S = 9005,
		ApplyToGuildS2C = 9006,
		ProcessGuildApplicationC2S = 9007,
		ProcessGuildApplicationS2C = 9008,
		SendGuildInvitationC2S = 9009,
		SendGuildInvitationS2C = 9010,
		ProcessGuildInvitationC2S = 9011,
		ProcessGuildInvitationS2C = 9012,
		GetGuildInfoC2S = 9013,
		GetGuildInfoS2C = 9014,
		UpdateGuildSettingsC2S = 9015,
		UpdateGuildSettingsS2C = 9016,
		UpdateGuildInAnnouncementC2S = 9017,
		UpdateGuildInAnnouncementS2C = 9018,
		TransferGuildMasterC2S = 9019,
		TransferGuildMasterS2C = 9020,
		ChangeGuildMemberTitleC2S = 9021,
		ChangeGuildMemberTitleS2C = 9022,
		KickGuildMemberC2S = 9023,
		KickGuildMemberS2C = 9024,
		ImpeachGuildMasterC2S = 9025,
		ImpeachGuildMasterS2C = 9026,
		ExitGuildC2S = 9027,
		ExitGuildS2C = 9028,
		DisbandGuildC2S = 9029,
		DisbandGuildS2C = 9030,
		GuildMissionRewardC2S = 9031,
		GuildMissionRewardS2C = 9032,
		GetGuildMemberChangeMsgC2S = 9033,
		GetGuildMemberChangeMsgS2C = 9034,
		SendGuildChatMsgC2S = 9035,
		SendGuildChatMsgS2C = 9036,
		GetGuildChatMsgC2S = 9037,
		GetGuildChatMsgS2C = 9038,
		GuildMemberC2S = 9039,
		GuildMemberS2C = 9040,
		GetGuildsInfoC2S = 9041,
		GetGuildsInfoS2C = 9042,
		TestRpcEchoC2S = 50000,
		TestRpcEchoS2C = 50001
	}

	private static readonly MessageParser<protocol> _parser = new MessageParser<protocol>(() => new protocol());

	private UnknownFieldSet _unknownFields;

	public const int SysSendMailC2SFieldNumber = 101;

	public const int SysPlayerOnlineC2SFieldNumber = 102;

	public const int PlayerOnlineRoomC2SFieldNumber = 103;

	public const int SysCanPraiseInfoC2SFieldNumber = 104;

	public const int SysPraiseC2SFieldNumber = 105;

	public const int SysRoomFinishC2SFieldNumber = 106;

	public const int SysRoomAddExpC2SFieldNumber = 107;

	public const int SysGetShowFriendC2SFieldNumber = 108;

	public const int SysFriendInviteC2SFieldNumber = 109;

	public const int SysFriendDelC2SFieldNumber = 110;

	public const int SysFriendInfoC2SFieldNumber = 111;

	public const int SysFriendApplyC2SFieldNumber = 112;

	public const int SysFriendAddC2SFieldNumber = 113;

	public const int SysFriendSendMsgC2SFieldNumber = 114;

	public const int SysCampaignFinishC2SFieldNumber = 115;

	public const int SysAbroadPayMsgC2SFieldNumber = 116;

	public const int AcquisitionMsgC2SFieldNumber = 117;

	public const int SysFightRecordFieldNumber = 118;

	public const int SysCampaignAwardC2SFieldNumber = 119;

	public const int SysRecoupItemC2SFieldNumber = 120;

	public const int SysPlayerOnlineRoomC2SFieldNumber = 121;

	public const int SysPlayerCleanC2SFieldNumber = 122;

	public const int SysPlayerPunishmentTimeC2SFieldNumber = 123;

	public const int SysMatchSuccessFieldNumber = 124;

	public const int SysChinaPayMsgC2SFieldNumber = 125;

	public const int SysChangeMatchTeamStateFieldNumber = 126;

	public const int SysSaveSimplePlayerInfoC2SFieldNumber = 127;

	public const int SysGmChangeNameC2SFieldNumber = 128;

	public const int SysSyncPlayerC2SFieldNumber = 129;

	public const int SysPlayerCreditScoreChangeC2SFieldNumber = 130;

	public const int SysSyncPlayerMatchPunishmentTimeC2SFieldNumber = 131;

	public const int GMChangeCreditScoreC2SFieldNumber = 132;

	public const int SysPushReturnInfoC2SFieldNumber = 133;

	public const int SysMutePlayerC2SFieldNumber = 134;

	public const int SysQuestionC2SFieldNumber = 135;

	public const int KickS2CFieldNumber = 1001;

	public const int PredictActionS2CFieldNumber = 1002;

	public const int RunningGameS2CFieldNumber = 1003;

	public const int BattleS2CFieldNumber = 1007;

	public const int LotteryDrawS2CFieldNumber = 1011;

	public const int LandBuffsS2CFieldNumber = 1013;

	public const int RoundStartS2CFieldNumber = 1015;

	public const int GameFinishS2CFieldNumber = 1016;

	public const int MonsterRefreshS2CFieldNumber = 1018;

	public const int MovePointBuffS2CFieldNumber = 1019;

	public const int ChangeDirS2CFieldNumber = 1020;

	public const int GambleChangeS2CFieldNumber = 1022;

	public const int HeroBarBoxChangeS2CFieldNumber = 1023;

	public const int RoomNotifyS2CFieldNumber = 1024;

	public const int ActionStartNotifyS2CFieldNumber = 1026;

	public const int NoGambleNotifyS2CFieldNumber = 1031;

	public const int GambleObServeS2CFieldNumber = 1034;

	public const int UpdateHeroAttrS2CFieldNumber = 1040;

	public const int ChangePlayerSlotS2CFieldNumber = 1042;

	public const int BossSleepS2CFieldNumber = 1043;

	public const int RefMallS2CFieldNumber = 1044;

	public const int BagItemChangeS2CFieldNumber = 1045;

	public const int RoleCardChangeS2CFieldNumber = 1046;

	public const int TaskConditionS2CFieldNumber = 1047;

	public const int TaskInfoS2CFieldNumber = 1048;

	public const int PlayerOnlineS2CFieldNumber = 1049;

	public const int ChangeExpS2CFieldNumber = 1050;

	public const int MailAddS2CFieldNumber = 1051;

	public const int OnlineSyncRoomIdS2CFieldNumber = 1052;

	public const int NoticeS2CFieldNumber = 1053;

	public const int ActivityTaskConditionS2CFieldNumber = 1054;

	public const int MapEventS2CFieldNumber = 1055;

	public const int Day7RewardS2CFieldNumber = 1056;

	public const int MapEventTrainS2CFieldNumber = 1057;

	public const int ChangePraiseNumS2CFieldNumber = 1058;

	public const int MonthlyCardS2CFieldNumber = 1059;

	public const int MailDelS2CFieldNumber = 1060;

	public const int FriendNotifyS2CFieldNumber = 1061;

	public const int FriendListChangeS2CFieldNumber = 1062;

	public const int FriendInviteNotifyS2CFieldNumber = 1063;

	public const int LoopNoticeS2CFieldNumber = 1064;

	public const int BattlePassLvS2CFieldNumber = 1065;

	public const int BattlePassTaskInfoS2CFieldNumber = 1066;

	public const int BattlePassUpdateTaskS2CFieldNumber = 1067;

	public const int BattlePassBuyS2CFieldNumber = 1068;

	public const int BattlePassInfoS2CFieldNumber = 1069;

	public const int FriendsChatMsgS2CFieldNumber = 1070;

	public const int GameProgressChangeS2CFieldNumber = 1071;

	public const int MapMissionNotifyS2CFieldNumber = 1072;

	public const int ChangeItemLimitS2CFieldNumber = 1073;

	public const int CleanItemLimitS2CFieldNumber = 1074;

	public const int CampaignPassS2CFieldNumber = 1075;

	public const int CampaignNotifyS2CFieldNumber = 1076;

	public const int KillMessageS2CFieldNumber = 1077;

	public const int GameScoreChangeS2CFieldNumber = 1078;

	public const int SignInRewardS2CFieldNumber = 1079;

	public const int PayResultS2CFieldNumber = 1080;

	public const int PayInfoChangeS2CFieldNumber = 1081;

	public const int InviteSuccessS2CFieldNumber = 1082;

	public const int InviteInfoNotifyS2CFieldNumber = 1083;

	public const int MapStatusChangeS2CFieldNumber = 1084;

	public const int GachaCountS2CFieldNumber = 1085;

	public const int MapIndexChangeS2CFieldNumber = 1086;

	public const int SurrenderPunishS2CFieldNumber = 1087;

	public const int GamePassMapSuccessS2CFieldNumber = 1088;

	public const int FriendDelNotifyS2CFieldNumber = 1090;

	public const int BagExpiredTransformNotifyFieldNumber = 1091;

	public const int MapEventCrabS2CFieldNumber = 1092;

	public const int PkAfterVoteS2CFieldNumber = 1093;

	public const int PlayerTaskNotifyS2CFieldNumber = 1094;

	public const int UnLockDifficultyS2CFieldNumber = 1095;

	public const int HeroSkillMoveEffectS2CFieldNumber = 1096;

	public const int TimeOutKickPlayerS2CFieldNumber = 1097;

	public const int SayPhraseNotifyS2CFieldNumber = 1098;

	public const int MatchTeamInviteNotifyFieldNumber = 1099;

	public const int RefreshMatchTeamStateNotifyFieldNumber = 1100;

	public const int ActivityPassGearChangeS2CFieldNumber = 1101;

	public const int SingleGameScoreChangeFieldNumber = 1102;

	public const int DelayProgressMapEventS2CFieldNumber = 1103;

	public const int LuckyStarMissionChangeS2CFieldNumber = 1104;

	public const int MatchPunishmentS2CFieldNumber = 1105;

	public const int ChallengeDataChangeS2CFieldNumber = 1106;

	public const int GmUnlockRoleInfoS2CFieldNumber = 1107;

	public const int SyncPlayerCreditInfoS2CFieldNumber = 1108;

	public const int RoomHeroCardChangeS2CFieldNumber = 1109;

	public const int RoomRoundAddTermS2CFieldNumber = 1110;

	public const int ReturnInfoS2CFieldNumber = 1111;

	public const int SyncRelicsS2CFieldNumber = 1112;

	public const int ReplaySnapshotS2CFieldNumber = 1113;

	public const int ClueNotifyS2CFieldNumber = 1114;

	public const int ReplayDieS2CFieldNumber = 1115;

	public const int GuildTaskNotifyS2CFieldNumber = 1116;

	public const int GameRoundChangeS2CFieldNumber = 1117;

	public const int NotifyQuestionS2CFieldNumber = 1118;

	public const int ConnectHandleFieldNumber = 5001;

	public const int ConnectFieldNumber = 5002;

	public const int HeartbeatHandleFieldNumber = 5003;

	public const int HeartbeatFieldNumber = 5004;

	public const int CreateRoomHandleFieldNumber = 5005;

	public const int CreateRoomFieldNumber = 5006;

	public const int SyncRoomHandleFieldNumber = 5007;

	public const int SyncRoomFieldNumber = 5008;

	public const int JoinRoomHandleFieldNumber = 5009;

	public const int JoinRoomFieldNumber = 5010;

	public const int ExitRoomHandleFieldNumber = 5011;

	public const int ExitRoomFieldNumber = 5012;

	public const int QueryRoomHandleFieldNumber = 5013;

	public const int QueryRoomFieldNumber = 5014;

	public const int RefreshRoomStateHandleFieldNumber = 5015;

	public const int RefreshRoomStateFieldNumber = 5016;

	public const int StartGameHandleFieldNumber = 5019;

	public const int StartGameFieldNumber = 5020;

	public const int ThrowDiceHandleFieldNumber = 5021;

	public const int ThrowDiceFieldNumber = 5022;

	public const int ChangeRoomHandleFieldNumber = 5023;

	public const int ChangeRoomFieldNumber = 5024;

	public const int MoveHandleFieldNumber = 5027;

	public const int MoveFieldNumber = 5028;

	public const int ShopBuyHandleFieldNumber = 5029;

	public const int ShopBuyFieldNumber = 5030;

	public const int PursuitHandleFieldNumber = 5033;

	public const int PursuitFieldNumber = 5034;

	public const int BattleUseCardHandleFieldNumber = 5035;

	public const int BattleUseCardFieldNumber = 5036;

	public const int BattleThrowDiceHandleFieldNumber = 5037;

	public const int BattleThrowDiceFieldNumber = 5038;

	public const int BattleChoiceHandleFieldNumber = 5039;

	public const int BattleChoiceFieldNumber = 5040;

	public const int LotteryChoiceHandleFieldNumber = 5041;

	public const int LotteryChoiceFieldNumber = 5042;

	public const int MoveAgainHandleFieldNumber = 5043;

	public const int MoveAgainFieldNumber = 5044;

	public const int AskBattleHandleFieldNumber = 5047;

	public const int AskBattleFieldNumber = 5048;

	public const int RollGoldHandleFieldNumber = 5049;

	public const int RollGoldFieldNumber = 5050;

	public const int EventThrowDiceHandleFieldNumber = 5051;

	public const int EventThrowDiceFieldNumber = 5052;

	public const int TriggerEventHandleFieldNumber = 5053;

	public const int TriggerEventFieldNumber = 5054;

	public const int UseEffectCardHandleFieldNumber = 5055;

	public const int UseEffectCardFieldNumber = 5056;

	public const int BombThrowDiceHandleFieldNumber = 5059;

	public const int BombThrowDiceFieldNumber = 5060;

	public const int ChoiceDirectionHandleFieldNumber = 5061;

	public const int ChoiceDirectionFieldNumber = 5062;

	public const int LandChoiceTargetHandleFieldNumber = 5063;

	public const int LandChoiceTargetFieldNumber = 5064;

	public const int ThrowDiceResultHandleFieldNumber = 5067;

	public const int ThrowDiceResultFieldNumber = 5068;

	public const int TriggerDivinationHandleFieldNumber = 5069;

	public const int TriggerDivinationFieldNumber = 5070;

	public const int TriggerDestinyHandleFieldNumber = 5071;

	public const int TriggerDestinyFieldNumber = 5072;

	public const int UseQuickCardHandleFieldNumber = 5073;

	public const int UseQuickCardFieldNumber = 5074;

	public const int AbandonCardHandleFieldNumber = 5075;

	public const int AbandonCardFieldNumber = 5076;

	public const int StopOrContinueHandleFieldNumber = 5077;

	public const int StopOrContinueFieldNumber = 5078;

	public const int StartGambleHandleFieldNumber = 5081;

	public const int StartGambleFieldNumber = 5082;

	public const int GambleThrowDicHandleFieldNumber = 5083;

	public const int GambleThrowDicFieldNumber = 5084;

	public const int ChoiceHeroC2S2HandleFieldNumber = 5085;

	public const int ChoiceHero2FieldNumber = 5086;

	public const int AffirmHeroHandleFieldNumber = 5087;

	public const int AffirmHeroFieldNumber = 5088;

	public const int SearchRoomHandleFieldNumber = 5089;

	public const int SearchRoomFieldNumber = 5090;

	public const int GmHandleFieldNumber = 5091;

	public const int GmFieldNumber = 5092;

	public const int TriggerHospitalHandleFieldNumber = 5093;

	public const int TriggerHospitalFieldNumber = 5094;

	public const int SendChatHandleFieldNumber = 5095;

	public const int SendChatFieldNumber = 5096;

	public const int PlayerShopBuyC2SFieldNumber = 5097;

	public const int PlayerShopBuyS2CFieldNumber = 5098;

	public const int PlayerUseItemHandleFieldNumber = 5099;

	public const int PlayerUseItemFieldNumber = 5100;

	public const int UseTreasureC2SFieldNumber = 5101;

	public const int UseTreasureS2CFieldNumber = 5102;

	public const int UseTreasureAutoTransformC2SFieldNumber = 5123;

	public const int UseTreasureAutoTransformS2CFieldNumber = 5124;

	public const int SetFashionC2SFieldNumber = 5103;

	public const int SetFashionS2CFieldNumber = 5104;

	public const int SelectFashionPlanC2SFieldNumber = 5105;

	public const int SelectFashionPlanS2CFieldNumber = 5106;

	public const int GachaC2SFieldNumber = 5107;

	public const int GachaS2CFieldNumber = 5108;

	public const int SteamSearchRoomC2SFieldNumber = 5109;

	public const int SteamSearchRoomS2CFieldNumber = 5110;

	public const int CheatItemHandleFieldNumber = 5111;

	public const int CheatItemFieldNumber = 5112;

	public const int RoleCardUpLvC2SFieldNumber = 5113;

	public const int RoleCardUpLvS2CFieldNumber = 5114;

	public const int RoleCardBreakThroughC2SFieldNumber = 5115;

	public const int RoleCardBreakThroughS2CFieldNumber = 5116;

	public const int RoleCardChoiceResC2SFieldNumber = 5117;

	public const int RoleCardChoiceResS2CFieldNumber = 5118;

	public const int TaskRewardC2SFieldNumber = 5119;

	public const int TaskRewardS2CFieldNumber = 5120;

	public const int TeachingC2SFieldNumber = 5121;

	public const int TeachingS2CFieldNumber = 5122;

	public const int QuickJoinRoomC2SFieldNumber = 5125;

	public const int QuickJoinRoomS2CFieldNumber = 5126;

	public const int RoomKickPlayerC2SFieldNumber = 5127;

	public const int RoomKickPlayerS2CFieldNumber = 5128;

	public const int RoomAbdicationC2SFieldNumber = 5129;

	public const int RoomAbdicationS2CFieldNumber = 5130;

	public const int RoomReadyC2SFieldNumber = 5131;

	public const int RoomReadyS2CFieldNumber = 5132;

	public const int ChargeCreateC2SFieldNumber = 5133;

	public const int ChargeCreateS2CFieldNumber = 5134;

	public const int ChargeC2SFieldNumber = 5135;

	public const int ChargeS2CFieldNumber = 5136;

	public const int GiftCdkC2SFieldNumber = 5137;

	public const int GiftCdkS2CFieldNumber = 5138;

	public const int MailReadC2SFieldNumber = 5139;

	public const int MailReadS2CFieldNumber = 5140;

	public const int MailGetRewardC2SFieldNumber = 5141;

	public const int MailGetRewardS2CFieldNumber = 5142;

	public const int MailDelReadC2SFieldNumber = 5143;

	public const int MailDelReadS2CFieldNumber = 5144;

	public const int GachaRecordC2SFieldNumber = 5145;

	public const int GachaRecordS2CFieldNumber = 5146;

	public const int ActivityTaskRewardC2SFieldNumber = 5147;

	public const int ActivityTaskRewardS2CFieldNumber = 5148;

	public const int RoomShortChatC2SFieldNumber = 5149;

	public const int RoomShortChatS2CFieldNumber = 5150;

	public const int SetShowPlayerC2SFieldNumber = 5151;

	public const int SetShowPlayerS2CFieldNumber = 5152;

	public const int GetShowPlayerC2SFieldNumber = 5153;

	public const int GetShowPlayerS2CFieldNumber = 5154;

	public const int GetPlayerFightRecordC2SFieldNumber = 5155;

	public const int GetPlayerFightRecordS2CFieldNumber = 5156;

	public const int GetDay7RewardC2SFieldNumber = 5157;

	public const int GetDay7RewardS2CFieldNumber = 5158;

	public const int PraisePlayerC2SFieldNumber = 5159;

	public const int PraisePlayerS2CFieldNumber = 5160;

	public const int ClientDataUploadC2SFieldNumber = 5161;

	public const int ClientDataUploadS2CFieldNumber = 5162;

	public const int FriendListC2SFieldNumber = 5163;

	public const int FriendListS2CFieldNumber = 5164;

	public const int FriendApplyC2SFieldNumber = 5165;

	public const int FriendApplyS2CFieldNumber = 5166;

	public const int FriendApplyListC2SFieldNumber = 5167;

	public const int FriendApplyListS2CFieldNumber = 5168;

	public const int FriendApplyOpC2SFieldNumber = 5169;

	public const int FriendApplyOpS2CFieldNumber = 5170;

	public const int FriendOpC2SFieldNumber = 5171;

	public const int FriendOpS2CFieldNumber = 5172;

	public const int FriendInviteC2SFieldNumber = 5173;

	public const int FriendInviteS2CFieldNumber = 5174;

	public const int FriendInviteListC2SFieldNumber = 5175;

	public const int FriendInviteListS2CFieldNumber = 5176;

	public const int FriendInviteCleanC2SFieldNumber = 5179;

	public const int FriendInviteCleanS2CFieldNumber = 5180;

	public const int FriendBlacksListC2SFieldNumber = 5181;

	public const int FriendBlacksListS2CFieldNumber = 5182;

	public const int NearFightPlayerC2SFieldNumber = 5183;

	public const int NearFightPlayerS2CFieldNumber = 5184;

	public const int SearchPlayerC2SFieldNumber = 5185;

	public const int SearchPlayerS2CFieldNumber = 5186;

	public const int ScratchCardC2SFieldNumber = 5187;

	public const int ScratchCardS2CFieldNumber = 5188;

	public const int NextScratchCardPoolC2SFieldNumber = 5189;

	public const int NextScratchCardPoolS2CFieldNumber = 5190;

	public const int WatchJoinRoomC2SFieldNumber = 5191;

	public const int WatchJoinRoomS2CFieldNumber = 5192;

	public const int WatchRefreshRoomStateC2SFieldNumber = 5193;

	public const int WatchRefreshRoomStateS2CFieldNumber = 5194;

	public const int WatchExitRoomC2SFieldNumber = 5195;

	public const int WatchExitRoomS2CFieldNumber = 5196;

	public const int BattlePassGetRewardC2SFieldNumber = 5197;

	public const int BattlePassGetRewardS2CFieldNumber = 5198;

	public const int BattlePassTaskRewardC2SFieldNumber = 5199;

	public const int BattlePassTaskRewardS2CFieldNumber = 5200;

	public const int BattlePassUpLvC2SFieldNumber = 5201;

	public const int BattlePassUpLvS2CFieldNumber = 5202;

	public const int FriendSendMsgC2SFieldNumber = 5203;

	public const int FriendSendMsgS2CFieldNumber = 5204;

	public const int GetChatMsgC2SFieldNumber = 5205;

	public const int GetChatMsgS2CFieldNumber = 5206;

	public const int ReadChatMsgC2SFieldNumber = 5207;

	public const int ReadChatMsgS2CFieldNumber = 5208;

	public const int DelChatMsgInfoC2SFieldNumber = 5209;

	public const int DelChatMsgInfoS2CFieldNumber = 5210;

	public const int SelectRelicC2SFieldNumber = 5211;

	public const int SelectRelicS2CFieldNumber = 5212;

	public const int MonsterPursuitC2SFieldNumber = 5213;

	public const int MonsterPursuitS2CFieldNumber = 5214;

	public const int PVEShopBuyC2SFieldNumber = 5215;

	public const int PVEShopBuyS2CFieldNumber = 5216;

	public const int ClientCheckTaskC2SFieldNumber = 5217;

	public const int ClientCheckTaskS2CFieldNumber = 5218;

	public const int PveHeroUpLvC2SFieldNumber = 5219;

	public const int PveHeroUpLvS2CFieldNumber = 5220;

	public const int StartMatchC2SFieldNumber = 5221;

	public const int StartMatchS2CFieldNumber = 5222;

	public const int CancelMatchC2SFieldNumber = 5223;

	public const int CancelMatchS2CFieldNumber = 5224;

	public const int MatchSuccessC2SFieldNumber = 5225;

	public const int MatchSuccessS2CFieldNumber = 5226;

	public const int AccuseC2SFieldNumber = 5227;

	public const int AccuseS2CFieldNumber = 5228;

	public const int SingleCampaignC2SFieldNumber = 5229;

	public const int SingleCampaignS2CFieldNumber = 5230;

	public const int DevChargeC2SFieldNumber = 5231;

	public const int DevChargeS2CFieldNumber = 5232;

	public const int AskReviveTeammateC2SFieldNumber = 5233;

	public const int AskReviveTeammateS2CFieldNumber = 5234;

	public const int GetSignInRewardC2SFieldNumber = 5235;

	public const int GetSignInRewardS2CFieldNumber = 5236;

	public const int ChatMapMarkersC2SFieldNumber = 5237;

	public const int ChatMapMarkersS2CFieldNumber = 5238;

	public const int AbroadCreateOrderC2SFieldNumber = 5239;

	public const int AbroadCreateOrderS2CFieldNumber = 5240;

	public const int AgeVerifyC2SFieldNumber = 5243;

	public const int AgeVerifyS2CFieldNumber = 5244;

	public const int ChangeNameC2SFieldNumber = 5245;

	public const int ChangeNameS2CFieldNumber = 5246;

	public const int ClientClickConfirmTaskC2SFieldNumber = 5247;

	public const int ClientClickConfirmTaskS2CFieldNumber = 5248;

	public const int BuyRelicC2SFieldNumber = 5249;

	public const int BuyRelicS2CFieldNumber = 5250;

	public const int BuyLightGiftC2SFieldNumber = 5251;

	public const int BuyLightGiftS2CFieldNumber = 5252;

	public const int LightGiftC2SFieldNumber = 5253;

	public const int LightGiftS2CFieldNumber = 5254;

	public const int AcquisitionC2SFieldNumber = 5255;

	public const int AcquisitionS2CFieldNumber = 5256;

	public const int AcquisitionRewardC2SFieldNumber = 5257;

	public const int AcquisitionRewardS2CFieldNumber = 5258;

	public const int SelectMechanismC2SFieldNumber = 5259;

	public const int SelectMechanismS2CFieldNumber = 5260;

	public const int GachaCountRewardC2SFieldNumber = 5261;

	public const int GachaCountRewardS2CFieldNumber = 5262;

	public const int GetPlayerSimpleC2SFieldNumber = 5263;

	public const int GetPlayerSimpleS2CFieldNumber = 5264;

	public const int RoleCardCollectC2SFieldNumber = 5265;

	public const int RoleCardCollectS2CFieldNumber = 5266;

	public const int GMPlayerSettingC2SFieldNumber = 5267;

	public const int GMPlayerSettingS2CFieldNumber = 5268;

	public const int SetFriendNoteC2SFieldNumber = 5269;

	public const int SetFriendNoteS2CFieldNumber = 5270;

	public const int SetOnlineStatusC2SFieldNumber = 5271;

	public const int SetOnlineStatusS2CFieldNumber = 5272;

	public const int ChooseSkinC2SFieldNumber = 5305;

	public const int ChooseSkinS2CFieldNumber = 5306;

	public const int TimeWastingC2SFieldNumber = 5307;

	public const int TimeWastingS2CFieldNumber = 5308;

	public const int VoteC2SFieldNumber = 5309;

	public const int VoteS2CFieldNumber = 5310;

	public const int VoteSelectC2SFieldNumber = 5311;

	public const int VoteSelectS2CFieldNumber = 5312;

	public const int NotifyStoryC2SFieldNumber = 5313;

	public const int NotifyStoryS2CFieldNumber = 5314;

	public const int PveHeroTalentUpC2SFieldNumber = 5315;

	public const int PveHeroTalentUpS2CFieldNumber = 5316;

	public const int SelectEventC2SFieldNumber = 5317;

	public const int SelectEventS2CFieldNumber = 5318;

	public const int CampScoreC2SFieldNumber = 5319;

	public const int CampScoreS2CFieldNumber = 5320;

	public const int ActivityMissionRewardC2SFieldNumber = 5321;

	public const int ActivityMissionRewardS2CFieldNumber = 5322;

	public const int VendorBuyCardC2SFieldNumber = 5323;

	public const int VendorBuyCardS2CFieldNumber = 5324;

	public const int TransferStarDiscC2SFieldNumber = 5325;

	public const int TransferStarDiscS2CFieldNumber = 5326;

	public const int GetHeroInfoC2SFieldNumber = 5327;

	public const int GetHeroInfoS2CFieldNumber = 5328;

	public const int CreateMatchTeamC2SFieldNumber = 5329;

	public const int CreateMatchTeamS2CFieldNumber = 5330;

	public const int ChangeMatchTeamC2SFieldNumber = 5331;

	public const int ChangeMatchTeamS2CFieldNumber = 5332;

	public const int JoinMatchTeamC2SFieldNumber = 5333;

	public const int JoinMatchTeamS2CFieldNumber = 5334;

	public const int ExitMatchTeamC2SFieldNumber = 5335;

	public const int ExitMatchTeamS2CFieldNumber = 5336;

	public const int RefreshMatchTeamInfoC2SFieldNumber = 5337;

	public const int RefreshMatchTeamInfoS2CFieldNumber = 5338;

	public const int ChinaCreateOrderC2SFieldNumber = 5339;

	public const int ChinaCreateOrderS2CFieldNumber = 5340;

	public const int MatchTeamInviteC2SFieldNumber = 5341;

	public const int MatchTeamInviteS2CFieldNumber = 5342;

	public const int MatchTeamChatC2SFieldNumber = 5343;

	public const int MatchTeamChatS2CFieldNumber = 5344;

	public const int MatchTeamReadyC2SFieldNumber = 5345;

	public const int MatchTeamReadyS2CFieldNumber = 5346;

	public const int PlayerChatC2SFieldNumber = 5347;

	public const int PlayerChatS2CFieldNumber = 5348;

	public const int SyncSingleGameDataC2SFieldNumber = 5349;

	public const int SyncSingleGameDataS2CFieldNumber = 5350;

	public const int SingleGameDataC2SFieldNumber = 5351;

	public const int SingleGameDataS2CFieldNumber = 5352;

	public const int GetActivityPassRewardC2SFieldNumber = 5353;

	public const int GetActivityPassRewardS2CFieldNumber = 5354;

	public const int ApplyChangeSlotC2SFieldNumber = 5355;

	public const int ApplyChangeSlotS2CFieldNumber = 5356;

	public const int OpsChangeSlotC2SFieldNumber = 5357;

	public const int OpsChangeSlotS2CFieldNumber = 5358;

	public const int RookieGachaRewardC2SFieldNumber = 5359;

	public const int RookieGachaRewardS2CFieldNumber = 5360;

	public const int LiveGiftPackageC2SFieldNumber = 5361;

	public const int LiveGiftPackageS2CFieldNumber = 5362;

	public const int LaborActDiceC2SFieldNumber = 5363;

	public const int LaborActDiceS2CFieldNumber = 5364;

	public const int ActionOverTimeLogC2SFieldNumber = 5365;

	public const int ActionOverTimeLogS2CFieldNumber = 5366;

	public const int ClientHarmonyC2SFieldNumber = 5369;

	public const int ClientHarmonyS2CFieldNumber = 5370;

	public const int MailStarC2SFieldNumber = 5371;

	public const int MailStarS2CFieldNumber = 5372;

	public const int SetCardAltArtC2SFieldNumber = 5373;

	public const int SetCardAltArtS2CFieldNumber = 5374;

	public const int SelectRewardCardC2SFieldNumber = 5377;

	public const int SelectRewardCardS2CFieldNumber = 5378;

	public const int GetQuestionUrlC2SFieldNumber = 5395;

	public const int GetQuestionUrlS2CFieldNumber = 5396;

	public const int GetReturnInfoC2SFieldNumber = 5379;

	public const int GetReturnInfoS2CFieldNumber = 5380;

	public const int ReturnGiftClaimC2SFieldNumber = 5381;

	public const int ReturnGiftClaimS2CFieldNumber = 5382;

	public const int ReturnSignInClaimC2SFieldNumber = 5383;

	public const int ReturnSignInClaimS2CFieldNumber = 5384;

	public const int ReturnSurveyFinishC2SFieldNumber = 5385;

	public const int ReturnSurveyFinishS2CFieldNumber = 5386;

	public const int FlipCardC2SFieldNumber = 5387;

	public const int FlipCardS2CFieldNumber = 5388;

	public const int FlipCardProgressRewardC2SFieldNumber = 5389;

	public const int FlipCardProgressRewardS2CFieldNumber = 5390;

	public const int SyncPlayerGuildS2CFieldNumber = 2001;

	public const int SyncPlayerJoinGuildS2CFieldNumber = 2002;

	public const int GuildChatMsgS2CFieldNumber = 2003;

	public const int SyncGuildS2CFieldNumber = 2004;

	public const int SyncGuildMemberS2CFieldNumber = 2005;

	public const int SyncGuildMemberExitS2CFieldNumber = 2006;

	public const int CreateGuildC2SFieldNumber = 9001;

	public const int CreateGuildS2CFieldNumber = 9002;

	public const int SearchGuildC2SFieldNumber = 9003;

	public const int SearchGuildS2CFieldNumber = 9004;

	public const int ApplyToGuildC2SFieldNumber = 9005;

	public const int ApplyToGuildS2CFieldNumber = 9006;

	public const int ProcessGuildApplicationC2SFieldNumber = 9007;

	public const int ProcessGuildApplicationS2CFieldNumber = 9008;

	public const int SendGuildInvitationC2SFieldNumber = 9009;

	public const int SendGuildInvitationS2CFieldNumber = 9010;

	public const int ProcessGuildInvitationC2SFieldNumber = 9011;

	public const int ProcessGuildInvitationS2CFieldNumber = 9012;

	public const int GetGuildInfoC2SFieldNumber = 9013;

	public const int GetGuildInfoS2CFieldNumber = 9014;

	public const int UpdateGuildSettingsC2SFieldNumber = 9015;

	public const int UpdateGuildSettingsS2CFieldNumber = 9016;

	public const int UpdateGuildInAnnouncementC2SFieldNumber = 9017;

	public const int UpdateGuildInAnnouncementS2CFieldNumber = 9018;

	public const int TransferGuildMasterC2SFieldNumber = 9019;

	public const int TransferGuildMasterS2CFieldNumber = 9020;

	public const int ChangeGuildMemberTitleC2SFieldNumber = 9021;

	public const int ChangeGuildMemberTitleS2CFieldNumber = 9022;

	public const int KickGuildMemberC2SFieldNumber = 9023;

	public const int KickGuildMemberS2CFieldNumber = 9024;

	public const int ImpeachGuildMasterC2SFieldNumber = 9025;

	public const int ImpeachGuildMasterS2CFieldNumber = 9026;

	public const int ExitGuildC2SFieldNumber = 9027;

	public const int ExitGuildS2CFieldNumber = 9028;

	public const int DisbandGuildC2SFieldNumber = 9029;

	public const int DisbandGuildS2CFieldNumber = 9030;

	public const int GuildMissionRewardC2SFieldNumber = 9031;

	public const int GuildMissionRewardS2CFieldNumber = 9032;

	public const int GetGuildMemberChangeMsgC2SFieldNumber = 9033;

	public const int GetGuildMemberChangeMsgS2CFieldNumber = 9034;

	public const int SendGuildChatMsgC2SFieldNumber = 9035;

	public const int SendGuildChatMsgS2CFieldNumber = 9036;

	public const int GetGuildChatMsgC2SFieldNumber = 9037;

	public const int GetGuildChatMsgS2CFieldNumber = 9038;

	public const int GuildMemberC2SFieldNumber = 9039;

	public const int GuildMemberS2CFieldNumber = 9040;

	public const int GetGuildsInfoC2SFieldNumber = 9041;

	public const int GetGuildsInfoS2CFieldNumber = 9042;

	public const int TestRpcEchoC2SFieldNumber = 50000;

	public const int TestRpcEchoS2CFieldNumber = 50001;

	private object msg_;

	private MsgOneofCase msgCase_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageParser<protocol> Parser => _parser;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public static MessageDescriptor Descriptor => ProtocolReflection.Descriptor.MessageTypes[554];

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	MessageDescriptor IMessage.Descriptor => Descriptor;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SysSendMailC2S SysSendMailC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.SysSendMailC2S)
			{
				return null;
			}
			return (SysSendMailC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.SysSendMailC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SysPlayerOnlineC2S SysPlayerOnlineC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.SysPlayerOnlineC2S)
			{
				return null;
			}
			return (SysPlayerOnlineC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.SysPlayerOnlineC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public PlayerOnlineRoomC2S PlayerOnlineRoomC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.PlayerOnlineRoomC2S)
			{
				return null;
			}
			return (PlayerOnlineRoomC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.PlayerOnlineRoomC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SysCanPraiseInfoC2S SysCanPraiseInfoC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.SysCanPraiseInfoC2S)
			{
				return null;
			}
			return (SysCanPraiseInfoC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.SysCanPraiseInfoC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SysPraiseC2S SysPraiseC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.SysPraiseC2S)
			{
				return null;
			}
			return (SysPraiseC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.SysPraiseC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SysRoomFinishC2S SysRoomFinishC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.SysRoomFinishC2S)
			{
				return null;
			}
			return (SysRoomFinishC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.SysRoomFinishC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SysRoomAddExpC2S SysRoomAddExpC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.SysRoomAddExpC2S)
			{
				return null;
			}
			return (SysRoomAddExpC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.SysRoomAddExpC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SysGetShowFriendC2S SysGetShowFriendC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.SysGetShowFriendC2S)
			{
				return null;
			}
			return (SysGetShowFriendC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.SysGetShowFriendC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SysFriendInviteC2S SysFriendInviteC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.SysFriendInviteC2S)
			{
				return null;
			}
			return (SysFriendInviteC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.SysFriendInviteC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SysFriendDelC2S SysFriendDelC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.SysFriendDelC2S)
			{
				return null;
			}
			return (SysFriendDelC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.SysFriendDelC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SysFriendInfoC2S SysFriendInfoC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.SysFriendInfoC2S)
			{
				return null;
			}
			return (SysFriendInfoC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.SysFriendInfoC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SysFriendApplyC2S SysFriendApplyC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.SysFriendApplyC2S)
			{
				return null;
			}
			return (SysFriendApplyC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.SysFriendApplyC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SysFriendAddC2S SysFriendAddC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.SysFriendAddC2S)
			{
				return null;
			}
			return (SysFriendAddC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.SysFriendAddC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SysFriendSendMsgC2S SysFriendSendMsgC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.SysFriendSendMsgC2S)
			{
				return null;
			}
			return (SysFriendSendMsgC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.SysFriendSendMsgC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SysCampaignFinishC2S SysCampaignFinishC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.SysCampaignFinishC2S)
			{
				return null;
			}
			return (SysCampaignFinishC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.SysCampaignFinishC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SysAbroadPayMsgC2S SysAbroadPayMsgC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.SysAbroadPayMsgC2S)
			{
				return null;
			}
			return (SysAbroadPayMsgC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.SysAbroadPayMsgC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public AcquisitionMsgC2S AcquisitionMsgC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.AcquisitionMsgC2S)
			{
				return null;
			}
			return (AcquisitionMsgC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.AcquisitionMsgC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SysFightRecord SysFightRecord
	{
		get
		{
			if (msgCase_ != MsgOneofCase.SysFightRecord)
			{
				return null;
			}
			return (SysFightRecord)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.SysFightRecord : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SysCampaignAwardC2S SysCampaignAwardC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.SysCampaignAwardC2S)
			{
				return null;
			}
			return (SysCampaignAwardC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.SysCampaignAwardC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SysRecoupItemC2S SysRecoupItemC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.SysRecoupItemC2S)
			{
				return null;
			}
			return (SysRecoupItemC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.SysRecoupItemC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SysPlayerOnlineRoomC2S SysPlayerOnlineRoomC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.SysPlayerOnlineRoomC2S)
			{
				return null;
			}
			return (SysPlayerOnlineRoomC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.SysPlayerOnlineRoomC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SysPlayerCleanC2S SysPlayerCleanC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.SysPlayerCleanC2S)
			{
				return null;
			}
			return (SysPlayerCleanC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.SysPlayerCleanC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SysPlayerPunishmentTimeC2S SysPlayerPunishmentTimeC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.SysPlayerPunishmentTimeC2S)
			{
				return null;
			}
			return (SysPlayerPunishmentTimeC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.SysPlayerPunishmentTimeC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SysMatchSuccess SysMatchSuccess
	{
		get
		{
			if (msgCase_ != MsgOneofCase.SysMatchSuccess)
			{
				return null;
			}
			return (SysMatchSuccess)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.SysMatchSuccess : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SysChinaPayMsgC2S SysChinaPayMsgC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.SysChinaPayMsgC2S)
			{
				return null;
			}
			return (SysChinaPayMsgC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.SysChinaPayMsgC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SysChangeMatchTeamState SysChangeMatchTeamState
	{
		get
		{
			if (msgCase_ != MsgOneofCase.SysChangeMatchTeamState)
			{
				return null;
			}
			return (SysChangeMatchTeamState)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.SysChangeMatchTeamState : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SysSaveSimplePlayerInfoC2S SysSaveSimplePlayerInfoC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.SysSaveSimplePlayerInfoC2S)
			{
				return null;
			}
			return (SysSaveSimplePlayerInfoC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.SysSaveSimplePlayerInfoC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SysGmChangeNameC2S SysGmChangeNameC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.SysGmChangeNameC2S)
			{
				return null;
			}
			return (SysGmChangeNameC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.SysGmChangeNameC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SysSyncPlayerC2S SysSyncPlayerC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.SysSyncPlayerC2S)
			{
				return null;
			}
			return (SysSyncPlayerC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.SysSyncPlayerC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SysPlayerCreditScoreChangeC2S SysPlayerCreditScoreChangeC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.SysPlayerCreditScoreChangeC2S)
			{
				return null;
			}
			return (SysPlayerCreditScoreChangeC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.SysPlayerCreditScoreChangeC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SysSyncPlayerMatchPunishmentTimeC2S SysSyncPlayerMatchPunishmentTimeC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.SysSyncPlayerMatchPunishmentTimeC2S)
			{
				return null;
			}
			return (SysSyncPlayerMatchPunishmentTimeC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.SysSyncPlayerMatchPunishmentTimeC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GMChangeCreditScoreC2S GMChangeCreditScoreC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.GMChangeCreditScoreC2S)
			{
				return null;
			}
			return (GMChangeCreditScoreC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.GMChangeCreditScoreC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SysPushReturnInfoC2S SysPushReturnInfoC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.SysPushReturnInfoC2S)
			{
				return null;
			}
			return (SysPushReturnInfoC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.SysPushReturnInfoC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SysMutePlayerC2S SysMutePlayerC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.SysMutePlayerC2S)
			{
				return null;
			}
			return (SysMutePlayerC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.SysMutePlayerC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SysQuestionC2S SysQuestionC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.SysQuestionC2S)
			{
				return null;
			}
			return (SysQuestionC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.SysQuestionC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public KickS2C KickS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.KickS2C)
			{
				return null;
			}
			return (KickS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.KickS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public PredictActionS2C PredictActionS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.PredictActionS2C)
			{
				return null;
			}
			return (PredictActionS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.PredictActionS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RunningGameS2C RunningGameS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.RunningGameS2C)
			{
				return null;
			}
			return (RunningGameS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.RunningGameS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public BattleS2C BattleS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.BattleS2C)
			{
				return null;
			}
			return (BattleS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.BattleS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public LotteryDrawS2C LotteryDrawS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.LotteryDrawS2C)
			{
				return null;
			}
			return (LotteryDrawS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.LotteryDrawS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public LandBuffsS2C LandBuffsS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.LandBuffsS2C)
			{
				return null;
			}
			return (LandBuffsS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.LandBuffsS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RoundStartS2C RoundStartS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.RoundStartS2C)
			{
				return null;
			}
			return (RoundStartS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.RoundStartS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GameFinishS2C GameFinishS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.GameFinishS2C)
			{
				return null;
			}
			return (GameFinishS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.GameFinishS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MonsterRefreshS2C MonsterRefreshS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.MonsterRefreshS2C)
			{
				return null;
			}
			return (MonsterRefreshS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.MonsterRefreshS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MovePointBuffS2C MovePointBuffS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.MovePointBuffS2C)
			{
				return null;
			}
			return (MovePointBuffS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.MovePointBuffS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ChangeDirS2C ChangeDirS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.ChangeDirS2C)
			{
				return null;
			}
			return (ChangeDirS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.ChangeDirS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GambleChangeS2C GambleChangeS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.GambleChangeS2C)
			{
				return null;
			}
			return (GambleChangeS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.GambleChangeS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public HeroBarBoxChangeS2C HeroBarBoxChangeS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.HeroBarBoxChangeS2C)
			{
				return null;
			}
			return (HeroBarBoxChangeS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.HeroBarBoxChangeS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RoomNotifyS2C RoomNotifyS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.RoomNotifyS2C)
			{
				return null;
			}
			return (RoomNotifyS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.RoomNotifyS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ActionStartNotifyS2C ActionStartNotifyS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.ActionStartNotifyS2C)
			{
				return null;
			}
			return (ActionStartNotifyS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.ActionStartNotifyS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public NoGambleNotifyS2C NoGambleNotifyS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.NoGambleNotifyS2C)
			{
				return null;
			}
			return (NoGambleNotifyS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.NoGambleNotifyS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GambleObServeS2C GambleObServeS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.GambleObServeS2C)
			{
				return null;
			}
			return (GambleObServeS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.GambleObServeS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public UpdateHeroAttrS2C UpdateHeroAttrS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.UpdateHeroAttrS2C)
			{
				return null;
			}
			return (UpdateHeroAttrS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.UpdateHeroAttrS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ChangePlayerSlotS2C ChangePlayerSlotS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.ChangePlayerSlotS2C)
			{
				return null;
			}
			return (ChangePlayerSlotS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.ChangePlayerSlotS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public BossSleepS2C BossSleepS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.BossSleepS2C)
			{
				return null;
			}
			return (BossSleepS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.BossSleepS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RefMallS2C RefMallS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.RefMallS2C)
			{
				return null;
			}
			return (RefMallS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.RefMallS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public BagItemChangeS2C BagItemChangeS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.BagItemChangeS2C)
			{
				return null;
			}
			return (BagItemChangeS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.BagItemChangeS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RoleCardChangeS2C RoleCardChangeS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.RoleCardChangeS2C)
			{
				return null;
			}
			return (RoleCardChangeS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.RoleCardChangeS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public TaskConditionS2C TaskConditionS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.TaskConditionS2C)
			{
				return null;
			}
			return (TaskConditionS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.TaskConditionS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public TaskInfoS2C TaskInfoS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.TaskInfoS2C)
			{
				return null;
			}
			return (TaskInfoS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.TaskInfoS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public PlayerOnlineS2C PlayerOnlineS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.PlayerOnlineS2C)
			{
				return null;
			}
			return (PlayerOnlineS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.PlayerOnlineS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ChangeExpS2C ChangeExpS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.ChangeExpS2C)
			{
				return null;
			}
			return (ChangeExpS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.ChangeExpS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MailAddS2C MailAddS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.MailAddS2C)
			{
				return null;
			}
			return (MailAddS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.MailAddS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public OnlineSyncRoomIdS2C OnlineSyncRoomIdS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.OnlineSyncRoomIdS2C)
			{
				return null;
			}
			return (OnlineSyncRoomIdS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.OnlineSyncRoomIdS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public NoticeS2C NoticeS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.NoticeS2C)
			{
				return null;
			}
			return (NoticeS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.NoticeS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ActivityTaskConditionS2C ActivityTaskConditionS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.ActivityTaskConditionS2C)
			{
				return null;
			}
			return (ActivityTaskConditionS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.ActivityTaskConditionS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapEventS2C MapEventS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.MapEventS2C)
			{
				return null;
			}
			return (MapEventS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.MapEventS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public Day7RewardS2C Day7RewardS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.Day7RewardS2C)
			{
				return null;
			}
			return (Day7RewardS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.Day7RewardS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapEventTrainS2C MapEventTrainS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.MapEventTrainS2C)
			{
				return null;
			}
			return (MapEventTrainS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.MapEventTrainS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ChangePraiseNumS2C ChangePraiseNumS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.ChangePraiseNumS2C)
			{
				return null;
			}
			return (ChangePraiseNumS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.ChangePraiseNumS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MonthlyCardS2C MonthlyCardS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.MonthlyCardS2C)
			{
				return null;
			}
			return (MonthlyCardS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.MonthlyCardS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MailDelS2C MailDelS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.MailDelS2C)
			{
				return null;
			}
			return (MailDelS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.MailDelS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FriendNotifyS2C FriendNotifyS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.FriendNotifyS2C)
			{
				return null;
			}
			return (FriendNotifyS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.FriendNotifyS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FriendListChangeS2C FriendListChangeS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.FriendListChangeS2C)
			{
				return null;
			}
			return (FriendListChangeS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.FriendListChangeS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FriendInviteNotifyS2C FriendInviteNotifyS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.FriendInviteNotifyS2C)
			{
				return null;
			}
			return (FriendInviteNotifyS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.FriendInviteNotifyS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public LoopNoticeS2C LoopNoticeS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.LoopNoticeS2C)
			{
				return null;
			}
			return (LoopNoticeS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.LoopNoticeS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public BattlePassLvS2C BattlePassLvS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.BattlePassLvS2C)
			{
				return null;
			}
			return (BattlePassLvS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.BattlePassLvS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public BattlePassTaskInfoS2C BattlePassTaskInfoS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.BattlePassTaskInfoS2C)
			{
				return null;
			}
			return (BattlePassTaskInfoS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.BattlePassTaskInfoS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public BattlePassUpdateTaskS2C BattlePassUpdateTaskS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.BattlePassUpdateTaskS2C)
			{
				return null;
			}
			return (BattlePassUpdateTaskS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.BattlePassUpdateTaskS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public BattlePassBuyS2C BattlePassBuyS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.BattlePassBuyS2C)
			{
				return null;
			}
			return (BattlePassBuyS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.BattlePassBuyS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public BattlePassInfoS2C BattlePassInfoS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.BattlePassInfoS2C)
			{
				return null;
			}
			return (BattlePassInfoS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.BattlePassInfoS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FriendsChatMsgS2C FriendsChatMsgS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.FriendsChatMsgS2C)
			{
				return null;
			}
			return (FriendsChatMsgS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.FriendsChatMsgS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GameProgressChangeS2C GameProgressChangeS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.GameProgressChangeS2C)
			{
				return null;
			}
			return (GameProgressChangeS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.GameProgressChangeS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapMissionNotifyS2C MapMissionNotifyS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.MapMissionNotifyS2C)
			{
				return null;
			}
			return (MapMissionNotifyS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.MapMissionNotifyS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ChangeItemLimitS2C ChangeItemLimitS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.ChangeItemLimitS2C)
			{
				return null;
			}
			return (ChangeItemLimitS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.ChangeItemLimitS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public CleanItemLimitS2C CleanItemLimitS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.CleanItemLimitS2C)
			{
				return null;
			}
			return (CleanItemLimitS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.CleanItemLimitS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public CampaignPassS2C CampaignPassS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.CampaignPassS2C)
			{
				return null;
			}
			return (CampaignPassS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.CampaignPassS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public CampaignNotifyS2C CampaignNotifyS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.CampaignNotifyS2C)
			{
				return null;
			}
			return (CampaignNotifyS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.CampaignNotifyS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public KillMessageS2C KillMessageS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.KillMessageS2C)
			{
				return null;
			}
			return (KillMessageS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.KillMessageS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GameScoreChangeS2C GameScoreChangeS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.GameScoreChangeS2C)
			{
				return null;
			}
			return (GameScoreChangeS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.GameScoreChangeS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SignInRewardS2C SignInRewardS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.SignInRewardS2C)
			{
				return null;
			}
			return (SignInRewardS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.SignInRewardS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public PayResultS2C PayResultS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.PayResultS2C)
			{
				return null;
			}
			return (PayResultS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.PayResultS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public PayInfoChangeS2C PayInfoChangeS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.PayInfoChangeS2C)
			{
				return null;
			}
			return (PayInfoChangeS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.PayInfoChangeS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public InviteSuccessS2C InviteSuccessS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.InviteSuccessS2C)
			{
				return null;
			}
			return (InviteSuccessS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.InviteSuccessS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public InviteInfoNotifyS2C InviteInfoNotifyS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.InviteInfoNotifyS2C)
			{
				return null;
			}
			return (InviteInfoNotifyS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.InviteInfoNotifyS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapStatusChangeS2C MapStatusChangeS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.MapStatusChangeS2C)
			{
				return null;
			}
			return (MapStatusChangeS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.MapStatusChangeS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GachaCountS2C GachaCountS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.GachaCountS2C)
			{
				return null;
			}
			return (GachaCountS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.GachaCountS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapIndexChangeS2C MapIndexChangeS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.MapIndexChangeS2C)
			{
				return null;
			}
			return (MapIndexChangeS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.MapIndexChangeS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SurrenderPunishS2C SurrenderPunishS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.SurrenderPunishS2C)
			{
				return null;
			}
			return (SurrenderPunishS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.SurrenderPunishS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GamePassMapSuccessS2C GamePassMapSuccessS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.GamePassMapSuccessS2C)
			{
				return null;
			}
			return (GamePassMapSuccessS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.GamePassMapSuccessS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FriendDelNotifyS2C FriendDelNotifyS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.FriendDelNotifyS2C)
			{
				return null;
			}
			return (FriendDelNotifyS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.FriendDelNotifyS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public BagExpiredTransformNotify BagExpiredTransformNotify
	{
		get
		{
			if (msgCase_ != MsgOneofCase.BagExpiredTransformNotify)
			{
				return null;
			}
			return (BagExpiredTransformNotify)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.BagExpiredTransformNotify : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MapEventCrabS2C MapEventCrabS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.MapEventCrabS2C)
			{
				return null;
			}
			return (MapEventCrabS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.MapEventCrabS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public PkAfterVoteS2C PkAfterVoteS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.PkAfterVoteS2C)
			{
				return null;
			}
			return (PkAfterVoteS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.PkAfterVoteS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public PlayerTaskNotifyS2C PlayerTaskNotifyS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.PlayerTaskNotifyS2C)
			{
				return null;
			}
			return (PlayerTaskNotifyS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.PlayerTaskNotifyS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public UnLockDifficultyS2C UnLockDifficultyS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.UnLockDifficultyS2C)
			{
				return null;
			}
			return (UnLockDifficultyS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.UnLockDifficultyS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public HeroSkillMoveEffectS2C HeroSkillMoveEffectS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.HeroSkillMoveEffectS2C)
			{
				return null;
			}
			return (HeroSkillMoveEffectS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.HeroSkillMoveEffectS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public TimeOutKickPlayerS2C TimeOutKickPlayerS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.TimeOutKickPlayerS2C)
			{
				return null;
			}
			return (TimeOutKickPlayerS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.TimeOutKickPlayerS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SayPhraseNotifyS2C SayPhraseNotifyS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.SayPhraseNotifyS2C)
			{
				return null;
			}
			return (SayPhraseNotifyS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.SayPhraseNotifyS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MatchTeamInviteNotify MatchTeamInviteNotify
	{
		get
		{
			if (msgCase_ != MsgOneofCase.MatchTeamInviteNotify)
			{
				return null;
			}
			return (MatchTeamInviteNotify)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.MatchTeamInviteNotify : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RefreshMatchTeamStateNotify RefreshMatchTeamStateNotify
	{
		get
		{
			if (msgCase_ != MsgOneofCase.RefreshMatchTeamStateNotify)
			{
				return null;
			}
			return (RefreshMatchTeamStateNotify)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.RefreshMatchTeamStateNotify : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ActivityPassGearChangeS2C ActivityPassGearChangeS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.ActivityPassGearChangeS2C)
			{
				return null;
			}
			return (ActivityPassGearChangeS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.ActivityPassGearChangeS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SingleGameScoreChangeS2C SingleGameScoreChange
	{
		get
		{
			if (msgCase_ != MsgOneofCase.SingleGameScoreChange)
			{
				return null;
			}
			return (SingleGameScoreChangeS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.SingleGameScoreChange : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public DelayProgressMapEventS2C DelayProgressMapEventS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.DelayProgressMapEventS2C)
			{
				return null;
			}
			return (DelayProgressMapEventS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.DelayProgressMapEventS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public LuckyStarMissionChangeS2C LuckyStarMissionChangeS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.LuckyStarMissionChangeS2C)
			{
				return null;
			}
			return (LuckyStarMissionChangeS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.LuckyStarMissionChangeS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MatchPunishmentS2C MatchPunishmentS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.MatchPunishmentS2C)
			{
				return null;
			}
			return (MatchPunishmentS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.MatchPunishmentS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ChallengeDataChangeS2C ChallengeDataChangeS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.ChallengeDataChangeS2C)
			{
				return null;
			}
			return (ChallengeDataChangeS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.ChallengeDataChangeS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GmUnlockRoleInfoS2C GmUnlockRoleInfoS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.GmUnlockRoleInfoS2C)
			{
				return null;
			}
			return (GmUnlockRoleInfoS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.GmUnlockRoleInfoS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SyncPlayerCreditInfoS2C SyncPlayerCreditInfoS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.SyncPlayerCreditInfoS2C)
			{
				return null;
			}
			return (SyncPlayerCreditInfoS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.SyncPlayerCreditInfoS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RoomHeroCardChangeS2C RoomHeroCardChangeS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.RoomHeroCardChangeS2C)
			{
				return null;
			}
			return (RoomHeroCardChangeS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.RoomHeroCardChangeS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RoomRoundAddTermS2C RoomRoundAddTermS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.RoomRoundAddTermS2C)
			{
				return null;
			}
			return (RoomRoundAddTermS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.RoomRoundAddTermS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ReturnInfoS2C ReturnInfoS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.ReturnInfoS2C)
			{
				return null;
			}
			return (ReturnInfoS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.ReturnInfoS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SyncRelicsS2C SyncRelicsS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.SyncRelicsS2C)
			{
				return null;
			}
			return (SyncRelicsS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.SyncRelicsS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ReplaySnapshotS2C ReplaySnapshotS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.ReplaySnapshotS2C)
			{
				return null;
			}
			return (ReplaySnapshotS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.ReplaySnapshotS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ClueNotifyS2C ClueNotifyS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.ClueNotifyS2C)
			{
				return null;
			}
			return (ClueNotifyS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.ClueNotifyS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ReplayDieS2C ReplayDieS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.ReplayDieS2C)
			{
				return null;
			}
			return (ReplayDieS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.ReplayDieS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GuildTaskNotifyS2C GuildTaskNotifyS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.GuildTaskNotifyS2C)
			{
				return null;
			}
			return (GuildTaskNotifyS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.GuildTaskNotifyS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GameRoundChangeS2C GameRoundChangeS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.GameRoundChangeS2C)
			{
				return null;
			}
			return (GameRoundChangeS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.GameRoundChangeS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public NotifyQuestionS2C NotifyQuestionS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.NotifyQuestionS2C)
			{
				return null;
			}
			return (NotifyQuestionS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.NotifyQuestionS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ConnectC2S ConnectHandle
	{
		get
		{
			if (msgCase_ != MsgOneofCase.ConnectHandle)
			{
				return null;
			}
			return (ConnectC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.ConnectHandle : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ConnectS2C Connect
	{
		get
		{
			if (msgCase_ != MsgOneofCase.Connect)
			{
				return null;
			}
			return (ConnectS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.Connect : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public HeartbeatC2S HeartbeatHandle
	{
		get
		{
			if (msgCase_ != MsgOneofCase.HeartbeatHandle)
			{
				return null;
			}
			return (HeartbeatC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.HeartbeatHandle : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public HeartbeatS2C Heartbeat
	{
		get
		{
			if (msgCase_ != MsgOneofCase.Heartbeat)
			{
				return null;
			}
			return (HeartbeatS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.Heartbeat : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public CreateRoomC2S CreateRoomHandle
	{
		get
		{
			if (msgCase_ != MsgOneofCase.CreateRoomHandle)
			{
				return null;
			}
			return (CreateRoomC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.CreateRoomHandle : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public CreateRoomS2C CreateRoom
	{
		get
		{
			if (msgCase_ != MsgOneofCase.CreateRoom)
			{
				return null;
			}
			return (CreateRoomS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.CreateRoom : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SyncRoomC2S SyncRoomHandle
	{
		get
		{
			if (msgCase_ != MsgOneofCase.SyncRoomHandle)
			{
				return null;
			}
			return (SyncRoomC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.SyncRoomHandle : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SyncRoomS2C SyncRoom
	{
		get
		{
			if (msgCase_ != MsgOneofCase.SyncRoom)
			{
				return null;
			}
			return (SyncRoomS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.SyncRoom : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public JoinRoomC2S JoinRoomHandle
	{
		get
		{
			if (msgCase_ != MsgOneofCase.JoinRoomHandle)
			{
				return null;
			}
			return (JoinRoomC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.JoinRoomHandle : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public JoinRoomS2C JoinRoom
	{
		get
		{
			if (msgCase_ != MsgOneofCase.JoinRoom)
			{
				return null;
			}
			return (JoinRoomS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.JoinRoom : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ExitRoomC2S ExitRoomHandle
	{
		get
		{
			if (msgCase_ != MsgOneofCase.ExitRoomHandle)
			{
				return null;
			}
			return (ExitRoomC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.ExitRoomHandle : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ExitRoomS2C ExitRoom
	{
		get
		{
			if (msgCase_ != MsgOneofCase.ExitRoom)
			{
				return null;
			}
			return (ExitRoomS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.ExitRoom : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public QueryRoomC2S QueryRoomHandle
	{
		get
		{
			if (msgCase_ != MsgOneofCase.QueryRoomHandle)
			{
				return null;
			}
			return (QueryRoomC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.QueryRoomHandle : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public QueryRoomS2C QueryRoom
	{
		get
		{
			if (msgCase_ != MsgOneofCase.QueryRoom)
			{
				return null;
			}
			return (QueryRoomS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.QueryRoom : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RefreshRoomStateC2S RefreshRoomStateHandle
	{
		get
		{
			if (msgCase_ != MsgOneofCase.RefreshRoomStateHandle)
			{
				return null;
			}
			return (RefreshRoomStateC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.RefreshRoomStateHandle : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RefreshRoomStateS2C RefreshRoomState
	{
		get
		{
			if (msgCase_ != MsgOneofCase.RefreshRoomState)
			{
				return null;
			}
			return (RefreshRoomStateS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.RefreshRoomState : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public StartGameC2S StartGameHandle
	{
		get
		{
			if (msgCase_ != MsgOneofCase.StartGameHandle)
			{
				return null;
			}
			return (StartGameC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.StartGameHandle : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public StartGameS2C StartGame
	{
		get
		{
			if (msgCase_ != MsgOneofCase.StartGame)
			{
				return null;
			}
			return (StartGameS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.StartGame : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ThrowDiceC2S ThrowDiceHandle
	{
		get
		{
			if (msgCase_ != MsgOneofCase.ThrowDiceHandle)
			{
				return null;
			}
			return (ThrowDiceC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.ThrowDiceHandle : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ThrowDiceS2C ThrowDice
	{
		get
		{
			if (msgCase_ != MsgOneofCase.ThrowDice)
			{
				return null;
			}
			return (ThrowDiceS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.ThrowDice : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ChangeRoomC2S ChangeRoomHandle
	{
		get
		{
			if (msgCase_ != MsgOneofCase.ChangeRoomHandle)
			{
				return null;
			}
			return (ChangeRoomC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.ChangeRoomHandle : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ChangeRoomS2C ChangeRoom
	{
		get
		{
			if (msgCase_ != MsgOneofCase.ChangeRoom)
			{
				return null;
			}
			return (ChangeRoomS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.ChangeRoom : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MoveC2S MoveHandle
	{
		get
		{
			if (msgCase_ != MsgOneofCase.MoveHandle)
			{
				return null;
			}
			return (MoveC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.MoveHandle : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MoveS2C Move
	{
		get
		{
			if (msgCase_ != MsgOneofCase.Move)
			{
				return null;
			}
			return (MoveS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.Move : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ShopBuyC2S ShopBuyHandle
	{
		get
		{
			if (msgCase_ != MsgOneofCase.ShopBuyHandle)
			{
				return null;
			}
			return (ShopBuyC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.ShopBuyHandle : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ShopBuyS2C ShopBuy
	{
		get
		{
			if (msgCase_ != MsgOneofCase.ShopBuy)
			{
				return null;
			}
			return (ShopBuyS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.ShopBuy : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public PursuitC2S PursuitHandle
	{
		get
		{
			if (msgCase_ != MsgOneofCase.PursuitHandle)
			{
				return null;
			}
			return (PursuitC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.PursuitHandle : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public PursuitS2C Pursuit
	{
		get
		{
			if (msgCase_ != MsgOneofCase.Pursuit)
			{
				return null;
			}
			return (PursuitS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.Pursuit : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public BattleUseCardC2S BattleUseCardHandle
	{
		get
		{
			if (msgCase_ != MsgOneofCase.BattleUseCardHandle)
			{
				return null;
			}
			return (BattleUseCardC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.BattleUseCardHandle : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public BattleUseCardS2C BattleUseCard
	{
		get
		{
			if (msgCase_ != MsgOneofCase.BattleUseCard)
			{
				return null;
			}
			return (BattleUseCardS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.BattleUseCard : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public BattleThrowDiceC2S BattleThrowDiceHandle
	{
		get
		{
			if (msgCase_ != MsgOneofCase.BattleThrowDiceHandle)
			{
				return null;
			}
			return (BattleThrowDiceC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.BattleThrowDiceHandle : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public BattleThrowDiceS2C BattleThrowDice
	{
		get
		{
			if (msgCase_ != MsgOneofCase.BattleThrowDice)
			{
				return null;
			}
			return (BattleThrowDiceS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.BattleThrowDice : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public BattleChoiceC2S BattleChoiceHandle
	{
		get
		{
			if (msgCase_ != MsgOneofCase.BattleChoiceHandle)
			{
				return null;
			}
			return (BattleChoiceC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.BattleChoiceHandle : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public BattleChoiceS2C BattleChoice
	{
		get
		{
			if (msgCase_ != MsgOneofCase.BattleChoice)
			{
				return null;
			}
			return (BattleChoiceS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.BattleChoice : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public LotteryChoiceC2S LotteryChoiceHandle
	{
		get
		{
			if (msgCase_ != MsgOneofCase.LotteryChoiceHandle)
			{
				return null;
			}
			return (LotteryChoiceC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.LotteryChoiceHandle : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public LotteryChoiceS2C LotteryChoice
	{
		get
		{
			if (msgCase_ != MsgOneofCase.LotteryChoice)
			{
				return null;
			}
			return (LotteryChoiceS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.LotteryChoice : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MoveAgainC2S MoveAgainHandle
	{
		get
		{
			if (msgCase_ != MsgOneofCase.MoveAgainHandle)
			{
				return null;
			}
			return (MoveAgainC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.MoveAgainHandle : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MoveAgainS2C MoveAgain
	{
		get
		{
			if (msgCase_ != MsgOneofCase.MoveAgain)
			{
				return null;
			}
			return (MoveAgainS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.MoveAgain : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public AskBattleC2S AskBattleHandle
	{
		get
		{
			if (msgCase_ != MsgOneofCase.AskBattleHandle)
			{
				return null;
			}
			return (AskBattleC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.AskBattleHandle : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public AskBattleS2C AskBattle
	{
		get
		{
			if (msgCase_ != MsgOneofCase.AskBattle)
			{
				return null;
			}
			return (AskBattleS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.AskBattle : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RollGoldC2S RollGoldHandle
	{
		get
		{
			if (msgCase_ != MsgOneofCase.RollGoldHandle)
			{
				return null;
			}
			return (RollGoldC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.RollGoldHandle : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RollGoldS2C RollGold
	{
		get
		{
			if (msgCase_ != MsgOneofCase.RollGold)
			{
				return null;
			}
			return (RollGoldS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.RollGold : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public EventThrowDiceC2S EventThrowDiceHandle
	{
		get
		{
			if (msgCase_ != MsgOneofCase.EventThrowDiceHandle)
			{
				return null;
			}
			return (EventThrowDiceC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.EventThrowDiceHandle : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public EventThrowDiceS2C EventThrowDice
	{
		get
		{
			if (msgCase_ != MsgOneofCase.EventThrowDice)
			{
				return null;
			}
			return (EventThrowDiceS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.EventThrowDice : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public TriggerEventC2S TriggerEventHandle
	{
		get
		{
			if (msgCase_ != MsgOneofCase.TriggerEventHandle)
			{
				return null;
			}
			return (TriggerEventC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.TriggerEventHandle : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public TriggerEventS2C TriggerEvent
	{
		get
		{
			if (msgCase_ != MsgOneofCase.TriggerEvent)
			{
				return null;
			}
			return (TriggerEventS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.TriggerEvent : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public UseEffectCardC2S UseEffectCardHandle
	{
		get
		{
			if (msgCase_ != MsgOneofCase.UseEffectCardHandle)
			{
				return null;
			}
			return (UseEffectCardC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.UseEffectCardHandle : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public UseEffectCardS2C UseEffectCard
	{
		get
		{
			if (msgCase_ != MsgOneofCase.UseEffectCard)
			{
				return null;
			}
			return (UseEffectCardS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.UseEffectCard : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public BombThrowDiceC2S BombThrowDiceHandle
	{
		get
		{
			if (msgCase_ != MsgOneofCase.BombThrowDiceHandle)
			{
				return null;
			}
			return (BombThrowDiceC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.BombThrowDiceHandle : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public BombThrowDiceS2C BombThrowDice
	{
		get
		{
			if (msgCase_ != MsgOneofCase.BombThrowDice)
			{
				return null;
			}
			return (BombThrowDiceS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.BombThrowDice : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ChoiceDirectionC2S ChoiceDirectionHandle
	{
		get
		{
			if (msgCase_ != MsgOneofCase.ChoiceDirectionHandle)
			{
				return null;
			}
			return (ChoiceDirectionC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.ChoiceDirectionHandle : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ChoiceDirectionS2C ChoiceDirection
	{
		get
		{
			if (msgCase_ != MsgOneofCase.ChoiceDirection)
			{
				return null;
			}
			return (ChoiceDirectionS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.ChoiceDirection : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public LandChoiceTargetC2S LandChoiceTargetHandle
	{
		get
		{
			if (msgCase_ != MsgOneofCase.LandChoiceTargetHandle)
			{
				return null;
			}
			return (LandChoiceTargetC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.LandChoiceTargetHandle : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public LandChoiceTargetS2C LandChoiceTarget
	{
		get
		{
			if (msgCase_ != MsgOneofCase.LandChoiceTarget)
			{
				return null;
			}
			return (LandChoiceTargetS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.LandChoiceTarget : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ThrowDiceResultC2S ThrowDiceResultHandle
	{
		get
		{
			if (msgCase_ != MsgOneofCase.ThrowDiceResultHandle)
			{
				return null;
			}
			return (ThrowDiceResultC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.ThrowDiceResultHandle : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ThrowDiceResultS2C ThrowDiceResult
	{
		get
		{
			if (msgCase_ != MsgOneofCase.ThrowDiceResult)
			{
				return null;
			}
			return (ThrowDiceResultS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.ThrowDiceResult : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public TriggerDivinationC2S TriggerDivinationHandle
	{
		get
		{
			if (msgCase_ != MsgOneofCase.TriggerDivinationHandle)
			{
				return null;
			}
			return (TriggerDivinationC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.TriggerDivinationHandle : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public TriggerDivinationS2C TriggerDivination
	{
		get
		{
			if (msgCase_ != MsgOneofCase.TriggerDivination)
			{
				return null;
			}
			return (TriggerDivinationS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.TriggerDivination : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public TriggerDestinyC2S TriggerDestinyHandle
	{
		get
		{
			if (msgCase_ != MsgOneofCase.TriggerDestinyHandle)
			{
				return null;
			}
			return (TriggerDestinyC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.TriggerDestinyHandle : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public TriggerDestinyS2C TriggerDestiny
	{
		get
		{
			if (msgCase_ != MsgOneofCase.TriggerDestiny)
			{
				return null;
			}
			return (TriggerDestinyS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.TriggerDestiny : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public UseQuickCardC2S UseQuickCardHandle
	{
		get
		{
			if (msgCase_ != MsgOneofCase.UseQuickCardHandle)
			{
				return null;
			}
			return (UseQuickCardC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.UseQuickCardHandle : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public UseQuickCardS2C UseQuickCard
	{
		get
		{
			if (msgCase_ != MsgOneofCase.UseQuickCard)
			{
				return null;
			}
			return (UseQuickCardS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.UseQuickCard : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public AbandonCardC2S AbandonCardHandle
	{
		get
		{
			if (msgCase_ != MsgOneofCase.AbandonCardHandle)
			{
				return null;
			}
			return (AbandonCardC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.AbandonCardHandle : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public AbandonCardS2C AbandonCard
	{
		get
		{
			if (msgCase_ != MsgOneofCase.AbandonCard)
			{
				return null;
			}
			return (AbandonCardS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.AbandonCard : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public StopOrContinueC2S StopOrContinueHandle
	{
		get
		{
			if (msgCase_ != MsgOneofCase.StopOrContinueHandle)
			{
				return null;
			}
			return (StopOrContinueC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.StopOrContinueHandle : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public StopOrContinueS2C StopOrContinue
	{
		get
		{
			if (msgCase_ != MsgOneofCase.StopOrContinue)
			{
				return null;
			}
			return (StopOrContinueS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.StopOrContinue : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public StartGambleC2S StartGambleHandle
	{
		get
		{
			if (msgCase_ != MsgOneofCase.StartGambleHandle)
			{
				return null;
			}
			return (StartGambleC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.StartGambleHandle : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public StartGambleS2C StartGamble
	{
		get
		{
			if (msgCase_ != MsgOneofCase.StartGamble)
			{
				return null;
			}
			return (StartGambleS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.StartGamble : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GambleThrowDicC2S GambleThrowDicHandle
	{
		get
		{
			if (msgCase_ != MsgOneofCase.GambleThrowDicHandle)
			{
				return null;
			}
			return (GambleThrowDicC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.GambleThrowDicHandle : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GambleThrowDicS2C GambleThrowDic
	{
		get
		{
			if (msgCase_ != MsgOneofCase.GambleThrowDic)
			{
				return null;
			}
			return (GambleThrowDicS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.GambleThrowDic : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ChoiceHeroC2S2 ChoiceHeroC2S2Handle
	{
		get
		{
			if (msgCase_ != MsgOneofCase.ChoiceHeroC2S2Handle)
			{
				return null;
			}
			return (ChoiceHeroC2S2)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.ChoiceHeroC2S2Handle : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ChoiceHeroS2C2 ChoiceHero2
	{
		get
		{
			if (msgCase_ != MsgOneofCase.ChoiceHero2)
			{
				return null;
			}
			return (ChoiceHeroS2C2)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.ChoiceHero2 : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public AffirmHeroC2S AffirmHeroHandle
	{
		get
		{
			if (msgCase_ != MsgOneofCase.AffirmHeroHandle)
			{
				return null;
			}
			return (AffirmHeroC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.AffirmHeroHandle : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public AffirmHeroS2C AffirmHero
	{
		get
		{
			if (msgCase_ != MsgOneofCase.AffirmHero)
			{
				return null;
			}
			return (AffirmHeroS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.AffirmHero : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SearchRoomC2S SearchRoomHandle
	{
		get
		{
			if (msgCase_ != MsgOneofCase.SearchRoomHandle)
			{
				return null;
			}
			return (SearchRoomC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.SearchRoomHandle : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SearchRoomS2C SearchRoom
	{
		get
		{
			if (msgCase_ != MsgOneofCase.SearchRoom)
			{
				return null;
			}
			return (SearchRoomS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.SearchRoom : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GmC2S GmHandle
	{
		get
		{
			if (msgCase_ != MsgOneofCase.GmHandle)
			{
				return null;
			}
			return (GmC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.GmHandle : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GmS2C Gm
	{
		get
		{
			if (msgCase_ != MsgOneofCase.Gm)
			{
				return null;
			}
			return (GmS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.Gm : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public TriggerHospitalC2S TriggerHospitalHandle
	{
		get
		{
			if (msgCase_ != MsgOneofCase.TriggerHospitalHandle)
			{
				return null;
			}
			return (TriggerHospitalC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.TriggerHospitalHandle : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public TriggerHospitalS2C TriggerHospital
	{
		get
		{
			if (msgCase_ != MsgOneofCase.TriggerHospital)
			{
				return null;
			}
			return (TriggerHospitalS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.TriggerHospital : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SendChatC2S SendChatHandle
	{
		get
		{
			if (msgCase_ != MsgOneofCase.SendChatHandle)
			{
				return null;
			}
			return (SendChatC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.SendChatHandle : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SendChatS2C SendChat
	{
		get
		{
			if (msgCase_ != MsgOneofCase.SendChat)
			{
				return null;
			}
			return (SendChatS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.SendChat : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public PlayerShopBuyC2S PlayerShopBuyC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.PlayerShopBuyC2S)
			{
				return null;
			}
			return (PlayerShopBuyC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.PlayerShopBuyC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public PlayerShopBuyS2C PlayerShopBuyS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.PlayerShopBuyS2C)
			{
				return null;
			}
			return (PlayerShopBuyS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.PlayerShopBuyS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public PlayerUseItemC2S PlayerUseItemHandle
	{
		get
		{
			if (msgCase_ != MsgOneofCase.PlayerUseItemHandle)
			{
				return null;
			}
			return (PlayerUseItemC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.PlayerUseItemHandle : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public PlayerUseItemS2C PlayerUseItem
	{
		get
		{
			if (msgCase_ != MsgOneofCase.PlayerUseItem)
			{
				return null;
			}
			return (PlayerUseItemS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.PlayerUseItem : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public UseTreasureC2S UseTreasureC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.UseTreasureC2S)
			{
				return null;
			}
			return (UseTreasureC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.UseTreasureC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public UseTreasureS2C UseTreasureS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.UseTreasureS2C)
			{
				return null;
			}
			return (UseTreasureS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.UseTreasureS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public UseTreasureAutoTransformC2S UseTreasureAutoTransformC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.UseTreasureAutoTransformC2S)
			{
				return null;
			}
			return (UseTreasureAutoTransformC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.UseTreasureAutoTransformC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public UseTreasureAutoTransformS2C UseTreasureAutoTransformS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.UseTreasureAutoTransformS2C)
			{
				return null;
			}
			return (UseTreasureAutoTransformS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.UseTreasureAutoTransformS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SetFashionC2S SetFashionC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.SetFashionC2S)
			{
				return null;
			}
			return (SetFashionC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.SetFashionC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SetFashionS2C SetFashionS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.SetFashionS2C)
			{
				return null;
			}
			return (SetFashionS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.SetFashionS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SelectFashionPlanC2S SelectFashionPlanC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.SelectFashionPlanC2S)
			{
				return null;
			}
			return (SelectFashionPlanC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.SelectFashionPlanC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SelectFashionPlanS2C SelectFashionPlanS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.SelectFashionPlanS2C)
			{
				return null;
			}
			return (SelectFashionPlanS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.SelectFashionPlanS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GachaC2S GachaC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.GachaC2S)
			{
				return null;
			}
			return (GachaC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.GachaC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GachaS2C GachaS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.GachaS2C)
			{
				return null;
			}
			return (GachaS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.GachaS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SteamSearchRoomC2S SteamSearchRoomC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.SteamSearchRoomC2S)
			{
				return null;
			}
			return (SteamSearchRoomC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.SteamSearchRoomC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SteamSearchRoomS2C SteamSearchRoomS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.SteamSearchRoomS2C)
			{
				return null;
			}
			return (SteamSearchRoomS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.SteamSearchRoomS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public CheatItemC2S CheatItemHandle
	{
		get
		{
			if (msgCase_ != MsgOneofCase.CheatItemHandle)
			{
				return null;
			}
			return (CheatItemC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.CheatItemHandle : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public CheatItemS2C CheatItem
	{
		get
		{
			if (msgCase_ != MsgOneofCase.CheatItem)
			{
				return null;
			}
			return (CheatItemS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.CheatItem : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RoleCardUpLvC2S RoleCardUpLvC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.RoleCardUpLvC2S)
			{
				return null;
			}
			return (RoleCardUpLvC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.RoleCardUpLvC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RoleCardUpLvS2C RoleCardUpLvS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.RoleCardUpLvS2C)
			{
				return null;
			}
			return (RoleCardUpLvS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.RoleCardUpLvS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RoleCardBreakThroughC2S RoleCardBreakThroughC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.RoleCardBreakThroughC2S)
			{
				return null;
			}
			return (RoleCardBreakThroughC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.RoleCardBreakThroughC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RoleCardBreakThroughS2C RoleCardBreakThroughS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.RoleCardBreakThroughS2C)
			{
				return null;
			}
			return (RoleCardBreakThroughS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.RoleCardBreakThroughS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RoleCardChoiceResC2S RoleCardChoiceResC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.RoleCardChoiceResC2S)
			{
				return null;
			}
			return (RoleCardChoiceResC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.RoleCardChoiceResC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RoleCardChoiceResS2C RoleCardChoiceResS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.RoleCardChoiceResS2C)
			{
				return null;
			}
			return (RoleCardChoiceResS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.RoleCardChoiceResS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public TaskRewardC2S TaskRewardC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.TaskRewardC2S)
			{
				return null;
			}
			return (TaskRewardC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.TaskRewardC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public TaskRewardS2C TaskRewardS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.TaskRewardS2C)
			{
				return null;
			}
			return (TaskRewardS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.TaskRewardS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public TeachingC2S TeachingC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.TeachingC2S)
			{
				return null;
			}
			return (TeachingC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.TeachingC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public TeachingS2C TeachingS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.TeachingS2C)
			{
				return null;
			}
			return (TeachingS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.TeachingS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public QuickJoinRoomC2S QuickJoinRoomC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.QuickJoinRoomC2S)
			{
				return null;
			}
			return (QuickJoinRoomC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.QuickJoinRoomC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public QuickJoinRoomS2C QuickJoinRoomS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.QuickJoinRoomS2C)
			{
				return null;
			}
			return (QuickJoinRoomS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.QuickJoinRoomS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RoomKickPlayerC2S RoomKickPlayerC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.RoomKickPlayerC2S)
			{
				return null;
			}
			return (RoomKickPlayerC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.RoomKickPlayerC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RoomKickPlayerS2C RoomKickPlayerS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.RoomKickPlayerS2C)
			{
				return null;
			}
			return (RoomKickPlayerS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.RoomKickPlayerS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RoomAbdicationC2S RoomAbdicationC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.RoomAbdicationC2S)
			{
				return null;
			}
			return (RoomAbdicationC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.RoomAbdicationC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RoomAbdicationS2C RoomAbdicationS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.RoomAbdicationS2C)
			{
				return null;
			}
			return (RoomAbdicationS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.RoomAbdicationS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RoomReadyC2S RoomReadyC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.RoomReadyC2S)
			{
				return null;
			}
			return (RoomReadyC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.RoomReadyC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RoomReadyS2C RoomReadyS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.RoomReadyS2C)
			{
				return null;
			}
			return (RoomReadyS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.RoomReadyS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ChargeCreateC2S ChargeCreateC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.ChargeCreateC2S)
			{
				return null;
			}
			return (ChargeCreateC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.ChargeCreateC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ChargeCreateS2C ChargeCreateS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.ChargeCreateS2C)
			{
				return null;
			}
			return (ChargeCreateS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.ChargeCreateS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ChargeC2S ChargeC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.ChargeC2S)
			{
				return null;
			}
			return (ChargeC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.ChargeC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ChargeS2C ChargeS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.ChargeS2C)
			{
				return null;
			}
			return (ChargeS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.ChargeS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GiftCdkC2S GiftCdkC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.GiftCdkC2S)
			{
				return null;
			}
			return (GiftCdkC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.GiftCdkC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GiftCdkS2C GiftCdkS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.GiftCdkS2C)
			{
				return null;
			}
			return (GiftCdkS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.GiftCdkS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MailReadC2S MailReadC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.MailReadC2S)
			{
				return null;
			}
			return (MailReadC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.MailReadC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MailReadS2C MailReadS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.MailReadS2C)
			{
				return null;
			}
			return (MailReadS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.MailReadS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MailGetRewardC2S MailGetRewardC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.MailGetRewardC2S)
			{
				return null;
			}
			return (MailGetRewardC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.MailGetRewardC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MailGetRewardS2C MailGetRewardS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.MailGetRewardS2C)
			{
				return null;
			}
			return (MailGetRewardS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.MailGetRewardS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MailDelReadC2S MailDelReadC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.MailDelReadC2S)
			{
				return null;
			}
			return (MailDelReadC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.MailDelReadC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MailDelReadS2C MailDelReadS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.MailDelReadS2C)
			{
				return null;
			}
			return (MailDelReadS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.MailDelReadS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GachaRecordC2S GachaRecordC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.GachaRecordC2S)
			{
				return null;
			}
			return (GachaRecordC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.GachaRecordC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GachaRecordS2C GachaRecordS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.GachaRecordS2C)
			{
				return null;
			}
			return (GachaRecordS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.GachaRecordS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ActivityTaskRewardC2S ActivityTaskRewardC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.ActivityTaskRewardC2S)
			{
				return null;
			}
			return (ActivityTaskRewardC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.ActivityTaskRewardC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ActivityTaskRewardS2C ActivityTaskRewardS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.ActivityTaskRewardS2C)
			{
				return null;
			}
			return (ActivityTaskRewardS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.ActivityTaskRewardS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RoomShortChatC2S RoomShortChatC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.RoomShortChatC2S)
			{
				return null;
			}
			return (RoomShortChatC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.RoomShortChatC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RoomShortChatS2C RoomShortChatS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.RoomShortChatS2C)
			{
				return null;
			}
			return (RoomShortChatS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.RoomShortChatS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SetShowPlayerC2S SetShowPlayerC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.SetShowPlayerC2S)
			{
				return null;
			}
			return (SetShowPlayerC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.SetShowPlayerC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SetShowPlayerS2C SetShowPlayerS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.SetShowPlayerS2C)
			{
				return null;
			}
			return (SetShowPlayerS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.SetShowPlayerS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GetShowPlayerC2S GetShowPlayerC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.GetShowPlayerC2S)
			{
				return null;
			}
			return (GetShowPlayerC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.GetShowPlayerC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GetShowPlayerS2C GetShowPlayerS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.GetShowPlayerS2C)
			{
				return null;
			}
			return (GetShowPlayerS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.GetShowPlayerS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GetPlayerFightRecordC2S GetPlayerFightRecordC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.GetPlayerFightRecordC2S)
			{
				return null;
			}
			return (GetPlayerFightRecordC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.GetPlayerFightRecordC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GetPlayerFightRecordS2C GetPlayerFightRecordS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.GetPlayerFightRecordS2C)
			{
				return null;
			}
			return (GetPlayerFightRecordS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.GetPlayerFightRecordS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GetDay7RewardC2S GetDay7RewardC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.GetDay7RewardC2S)
			{
				return null;
			}
			return (GetDay7RewardC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.GetDay7RewardC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GetDay7RewardS2C GetDay7RewardS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.GetDay7RewardS2C)
			{
				return null;
			}
			return (GetDay7RewardS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.GetDay7RewardS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public PraisePlayerC2S PraisePlayerC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.PraisePlayerC2S)
			{
				return null;
			}
			return (PraisePlayerC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.PraisePlayerC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public PraisePlayerS2C PraisePlayerS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.PraisePlayerS2C)
			{
				return null;
			}
			return (PraisePlayerS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.PraisePlayerS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ClientDataUploadC2S ClientDataUploadC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.ClientDataUploadC2S)
			{
				return null;
			}
			return (ClientDataUploadC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.ClientDataUploadC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ClientDataUploadS2C ClientDataUploadS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.ClientDataUploadS2C)
			{
				return null;
			}
			return (ClientDataUploadS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.ClientDataUploadS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FriendListC2S FriendListC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.FriendListC2S)
			{
				return null;
			}
			return (FriendListC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.FriendListC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FriendListS2C FriendListS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.FriendListS2C)
			{
				return null;
			}
			return (FriendListS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.FriendListS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FriendApplyC2S FriendApplyC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.FriendApplyC2S)
			{
				return null;
			}
			return (FriendApplyC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.FriendApplyC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FriendApplyS2C FriendApplyS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.FriendApplyS2C)
			{
				return null;
			}
			return (FriendApplyS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.FriendApplyS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FriendApplyListC2S FriendApplyListC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.FriendApplyListC2S)
			{
				return null;
			}
			return (FriendApplyListC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.FriendApplyListC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FriendApplyListS2C FriendApplyListS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.FriendApplyListS2C)
			{
				return null;
			}
			return (FriendApplyListS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.FriendApplyListS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FriendApplyOpC2S FriendApplyOpC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.FriendApplyOpC2S)
			{
				return null;
			}
			return (FriendApplyOpC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.FriendApplyOpC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FriendApplyOpS2C FriendApplyOpS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.FriendApplyOpS2C)
			{
				return null;
			}
			return (FriendApplyOpS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.FriendApplyOpS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FriendOpC2S FriendOpC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.FriendOpC2S)
			{
				return null;
			}
			return (FriendOpC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.FriendOpC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FriendOpS2C FriendOpS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.FriendOpS2C)
			{
				return null;
			}
			return (FriendOpS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.FriendOpS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FriendInviteC2S FriendInviteC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.FriendInviteC2S)
			{
				return null;
			}
			return (FriendInviteC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.FriendInviteC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FriendInviteS2C FriendInviteS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.FriendInviteS2C)
			{
				return null;
			}
			return (FriendInviteS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.FriendInviteS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FriendInviteListC2S FriendInviteListC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.FriendInviteListC2S)
			{
				return null;
			}
			return (FriendInviteListC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.FriendInviteListC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FriendInviteListS2C FriendInviteListS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.FriendInviteListS2C)
			{
				return null;
			}
			return (FriendInviteListS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.FriendInviteListS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FriendInviteCleanC2S FriendInviteCleanC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.FriendInviteCleanC2S)
			{
				return null;
			}
			return (FriendInviteCleanC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.FriendInviteCleanC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FriendInviteCleanS2C FriendInviteCleanS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.FriendInviteCleanS2C)
			{
				return null;
			}
			return (FriendInviteCleanS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.FriendInviteCleanS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FriendBlacksListC2S FriendBlacksListC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.FriendBlacksListC2S)
			{
				return null;
			}
			return (FriendBlacksListC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.FriendBlacksListC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FriendBlacksListS2C FriendBlacksListS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.FriendBlacksListS2C)
			{
				return null;
			}
			return (FriendBlacksListS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.FriendBlacksListS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public NearFightPlayerC2S NearFightPlayerC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.NearFightPlayerC2S)
			{
				return null;
			}
			return (NearFightPlayerC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.NearFightPlayerC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public NearFightPlayerS2C NearFightPlayerS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.NearFightPlayerS2C)
			{
				return null;
			}
			return (NearFightPlayerS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.NearFightPlayerS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SearchPlayerC2S SearchPlayerC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.SearchPlayerC2S)
			{
				return null;
			}
			return (SearchPlayerC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.SearchPlayerC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SearchPlayerS2C SearchPlayerS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.SearchPlayerS2C)
			{
				return null;
			}
			return (SearchPlayerS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.SearchPlayerS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ScratchCardC2S ScratchCardC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.ScratchCardC2S)
			{
				return null;
			}
			return (ScratchCardC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.ScratchCardC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ScratchCardS2C ScratchCardS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.ScratchCardS2C)
			{
				return null;
			}
			return (ScratchCardS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.ScratchCardS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public NextScratchCardPoolC2S NextScratchCardPoolC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.NextScratchCardPoolC2S)
			{
				return null;
			}
			return (NextScratchCardPoolC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.NextScratchCardPoolC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public NextScratchCardPoolS2C NextScratchCardPoolS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.NextScratchCardPoolS2C)
			{
				return null;
			}
			return (NextScratchCardPoolS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.NextScratchCardPoolS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public WatchJoinRoomC2S WatchJoinRoomC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.WatchJoinRoomC2S)
			{
				return null;
			}
			return (WatchJoinRoomC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.WatchJoinRoomC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public WatchJoinRoomS2C WatchJoinRoomS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.WatchJoinRoomS2C)
			{
				return null;
			}
			return (WatchJoinRoomS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.WatchJoinRoomS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public WatchRefreshRoomStateC2S WatchRefreshRoomStateC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.WatchRefreshRoomStateC2S)
			{
				return null;
			}
			return (WatchRefreshRoomStateC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.WatchRefreshRoomStateC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public WatchRefreshRoomStateS2C WatchRefreshRoomStateS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.WatchRefreshRoomStateS2C)
			{
				return null;
			}
			return (WatchRefreshRoomStateS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.WatchRefreshRoomStateS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public WatchExitRoomC2S WatchExitRoomC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.WatchExitRoomC2S)
			{
				return null;
			}
			return (WatchExitRoomC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.WatchExitRoomC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public WatchExitRoomS2C WatchExitRoomS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.WatchExitRoomS2C)
			{
				return null;
			}
			return (WatchExitRoomS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.WatchExitRoomS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public BattlePassGetRewardC2S BattlePassGetRewardC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.BattlePassGetRewardC2S)
			{
				return null;
			}
			return (BattlePassGetRewardC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.BattlePassGetRewardC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public BattlePassGetRewardS2C BattlePassGetRewardS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.BattlePassGetRewardS2C)
			{
				return null;
			}
			return (BattlePassGetRewardS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.BattlePassGetRewardS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public BattlePassTaskRewardC2S BattlePassTaskRewardC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.BattlePassTaskRewardC2S)
			{
				return null;
			}
			return (BattlePassTaskRewardC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.BattlePassTaskRewardC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public BattlePassTaskRewardS2C BattlePassTaskRewardS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.BattlePassTaskRewardS2C)
			{
				return null;
			}
			return (BattlePassTaskRewardS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.BattlePassTaskRewardS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public BattlePassUpLvC2S BattlePassUpLvC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.BattlePassUpLvC2S)
			{
				return null;
			}
			return (BattlePassUpLvC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.BattlePassUpLvC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public BattlePassUpLvS2C BattlePassUpLvS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.BattlePassUpLvS2C)
			{
				return null;
			}
			return (BattlePassUpLvS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.BattlePassUpLvS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FriendSendMsgC2S FriendSendMsgC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.FriendSendMsgC2S)
			{
				return null;
			}
			return (FriendSendMsgC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.FriendSendMsgC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FriendSendMsgS2C FriendSendMsgS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.FriendSendMsgS2C)
			{
				return null;
			}
			return (FriendSendMsgS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.FriendSendMsgS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GetChatMsgC2S GetChatMsgC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.GetChatMsgC2S)
			{
				return null;
			}
			return (GetChatMsgC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.GetChatMsgC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GetChatMsgS2C GetChatMsgS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.GetChatMsgS2C)
			{
				return null;
			}
			return (GetChatMsgS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.GetChatMsgS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ReadChatMsgC2S ReadChatMsgC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.ReadChatMsgC2S)
			{
				return null;
			}
			return (ReadChatMsgC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.ReadChatMsgC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ReadChatMsgS2C ReadChatMsgS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.ReadChatMsgS2C)
			{
				return null;
			}
			return (ReadChatMsgS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.ReadChatMsgS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public DelChatMsgInfoC2S DelChatMsgInfoC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.DelChatMsgInfoC2S)
			{
				return null;
			}
			return (DelChatMsgInfoC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.DelChatMsgInfoC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public DelChatMsgInfoS2C DelChatMsgInfoS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.DelChatMsgInfoS2C)
			{
				return null;
			}
			return (DelChatMsgInfoS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.DelChatMsgInfoS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SelectRelicC2S SelectRelicC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.SelectRelicC2S)
			{
				return null;
			}
			return (SelectRelicC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.SelectRelicC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SelectRelicS2C SelectRelicS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.SelectRelicS2C)
			{
				return null;
			}
			return (SelectRelicS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.SelectRelicS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MonsterPursuitC2S MonsterPursuitC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.MonsterPursuitC2S)
			{
				return null;
			}
			return (MonsterPursuitC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.MonsterPursuitC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MonsterPursuitS2C MonsterPursuitS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.MonsterPursuitS2C)
			{
				return null;
			}
			return (MonsterPursuitS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.MonsterPursuitS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public PVEShopBuyC2S PVEShopBuyC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.PVEShopBuyC2S)
			{
				return null;
			}
			return (PVEShopBuyC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.PVEShopBuyC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public PVEShopBuyS2C PVEShopBuyS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.PVEShopBuyS2C)
			{
				return null;
			}
			return (PVEShopBuyS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.PVEShopBuyS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ClientCheckTaskC2S ClientCheckTaskC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.ClientCheckTaskC2S)
			{
				return null;
			}
			return (ClientCheckTaskC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.ClientCheckTaskC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ClientCheckTaskS2C ClientCheckTaskS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.ClientCheckTaskS2C)
			{
				return null;
			}
			return (ClientCheckTaskS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.ClientCheckTaskS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public PveHeroUpLvC2S PveHeroUpLvC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.PveHeroUpLvC2S)
			{
				return null;
			}
			return (PveHeroUpLvC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.PveHeroUpLvC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public PveHeroUpLvS2C PveHeroUpLvS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.PveHeroUpLvS2C)
			{
				return null;
			}
			return (PveHeroUpLvS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.PveHeroUpLvS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public StartMatchC2S StartMatchC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.StartMatchC2S)
			{
				return null;
			}
			return (StartMatchC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.StartMatchC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public StartMatchS2C StartMatchS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.StartMatchS2C)
			{
				return null;
			}
			return (StartMatchS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.StartMatchS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public CancelMatchC2S CancelMatchC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.CancelMatchC2S)
			{
				return null;
			}
			return (CancelMatchC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.CancelMatchC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public CancelMatchS2C CancelMatchS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.CancelMatchS2C)
			{
				return null;
			}
			return (CancelMatchS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.CancelMatchS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MatchSuccessC2S MatchSuccessC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.MatchSuccessC2S)
			{
				return null;
			}
			return (MatchSuccessC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.MatchSuccessC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MatchSuccessS2C MatchSuccessS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.MatchSuccessS2C)
			{
				return null;
			}
			return (MatchSuccessS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.MatchSuccessS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public AccuseC2S AccuseC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.AccuseC2S)
			{
				return null;
			}
			return (AccuseC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.AccuseC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public AccuseS2C AccuseS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.AccuseS2C)
			{
				return null;
			}
			return (AccuseS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.AccuseS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SingleCampaignC2S SingleCampaignC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.SingleCampaignC2S)
			{
				return null;
			}
			return (SingleCampaignC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.SingleCampaignC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SingleCampaignS2C SingleCampaignS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.SingleCampaignS2C)
			{
				return null;
			}
			return (SingleCampaignS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.SingleCampaignS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public DevChargeC2S DevChargeC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.DevChargeC2S)
			{
				return null;
			}
			return (DevChargeC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.DevChargeC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public DevChargeS2C DevChargeS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.DevChargeS2C)
			{
				return null;
			}
			return (DevChargeS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.DevChargeS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public AskReviveTeammateC2S AskReviveTeammateC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.AskReviveTeammateC2S)
			{
				return null;
			}
			return (AskReviveTeammateC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.AskReviveTeammateC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public AskReviveTeammateS2C AskReviveTeammateS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.AskReviveTeammateS2C)
			{
				return null;
			}
			return (AskReviveTeammateS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.AskReviveTeammateS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GetSignInRewardC2S GetSignInRewardC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.GetSignInRewardC2S)
			{
				return null;
			}
			return (GetSignInRewardC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.GetSignInRewardC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GetSignInRewardS2C GetSignInRewardS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.GetSignInRewardS2C)
			{
				return null;
			}
			return (GetSignInRewardS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.GetSignInRewardS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ChatMapMarkersC2S ChatMapMarkersC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.ChatMapMarkersC2S)
			{
				return null;
			}
			return (ChatMapMarkersC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.ChatMapMarkersC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ChatMapMarkersS2C ChatMapMarkersS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.ChatMapMarkersS2C)
			{
				return null;
			}
			return (ChatMapMarkersS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.ChatMapMarkersS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public AbroadCreateOrderC2S AbroadCreateOrderC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.AbroadCreateOrderC2S)
			{
				return null;
			}
			return (AbroadCreateOrderC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.AbroadCreateOrderC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public AbroadCreateOrderS2C AbroadCreateOrderS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.AbroadCreateOrderS2C)
			{
				return null;
			}
			return (AbroadCreateOrderS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.AbroadCreateOrderS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public AgeVerifyC2S AgeVerifyC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.AgeVerifyC2S)
			{
				return null;
			}
			return (AgeVerifyC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.AgeVerifyC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public AgeVerifyS2C AgeVerifyS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.AgeVerifyS2C)
			{
				return null;
			}
			return (AgeVerifyS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.AgeVerifyS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ChangeNameC2S ChangeNameC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.ChangeNameC2S)
			{
				return null;
			}
			return (ChangeNameC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.ChangeNameC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ChangeNameS2C ChangeNameS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.ChangeNameS2C)
			{
				return null;
			}
			return (ChangeNameS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.ChangeNameS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ClientClickConfirmTaskC2S ClientClickConfirmTaskC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.ClientClickConfirmTaskC2S)
			{
				return null;
			}
			return (ClientClickConfirmTaskC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.ClientClickConfirmTaskC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ClientClickConfirmTaskS2C ClientClickConfirmTaskS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.ClientClickConfirmTaskS2C)
			{
				return null;
			}
			return (ClientClickConfirmTaskS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.ClientClickConfirmTaskS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public BuyRelicC2S BuyRelicC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.BuyRelicC2S)
			{
				return null;
			}
			return (BuyRelicC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.BuyRelicC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public BuyRelicS2C BuyRelicS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.BuyRelicS2C)
			{
				return null;
			}
			return (BuyRelicS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.BuyRelicS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public BuyLightGiftC2S BuyLightGiftC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.BuyLightGiftC2S)
			{
				return null;
			}
			return (BuyLightGiftC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.BuyLightGiftC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public BuyLightGiftS2C BuyLightGiftS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.BuyLightGiftS2C)
			{
				return null;
			}
			return (BuyLightGiftS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.BuyLightGiftS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public LightGiftC2S LightGiftC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.LightGiftC2S)
			{
				return null;
			}
			return (LightGiftC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.LightGiftC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public LightGiftS2C LightGiftS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.LightGiftS2C)
			{
				return null;
			}
			return (LightGiftS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.LightGiftS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public AcquisitionC2S AcquisitionC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.AcquisitionC2S)
			{
				return null;
			}
			return (AcquisitionC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.AcquisitionC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public AcquisitionS2C AcquisitionS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.AcquisitionS2C)
			{
				return null;
			}
			return (AcquisitionS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.AcquisitionS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public AcquisitionRewardC2S AcquisitionRewardC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.AcquisitionRewardC2S)
			{
				return null;
			}
			return (AcquisitionRewardC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.AcquisitionRewardC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public AcquisitionRewardS2C AcquisitionRewardS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.AcquisitionRewardS2C)
			{
				return null;
			}
			return (AcquisitionRewardS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.AcquisitionRewardS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SelectMechanismC2S SelectMechanismC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.SelectMechanismC2S)
			{
				return null;
			}
			return (SelectMechanismC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.SelectMechanismC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SelectMechanismS2C SelectMechanismS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.SelectMechanismS2C)
			{
				return null;
			}
			return (SelectMechanismS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.SelectMechanismS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GachaCountRewardC2S GachaCountRewardC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.GachaCountRewardC2S)
			{
				return null;
			}
			return (GachaCountRewardC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.GachaCountRewardC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GachaCountRewardS2C GachaCountRewardS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.GachaCountRewardS2C)
			{
				return null;
			}
			return (GachaCountRewardS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.GachaCountRewardS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GetPlayerSimpleC2S GetPlayerSimpleC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.GetPlayerSimpleC2S)
			{
				return null;
			}
			return (GetPlayerSimpleC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.GetPlayerSimpleC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GetPlayerSimpleS2C GetPlayerSimpleS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.GetPlayerSimpleS2C)
			{
				return null;
			}
			return (GetPlayerSimpleS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.GetPlayerSimpleS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RoleCardCollectC2S RoleCardCollectC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.RoleCardCollectC2S)
			{
				return null;
			}
			return (RoleCardCollectC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.RoleCardCollectC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RoleCardCollectS2C RoleCardCollectS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.RoleCardCollectS2C)
			{
				return null;
			}
			return (RoleCardCollectS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.RoleCardCollectS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GMPlayerSettingC2S GMPlayerSettingC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.GMPlayerSettingC2S)
			{
				return null;
			}
			return (GMPlayerSettingC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.GMPlayerSettingC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GMPlayerSettingS2C GMPlayerSettingS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.GMPlayerSettingS2C)
			{
				return null;
			}
			return (GMPlayerSettingS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.GMPlayerSettingS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SetFriendNoteC2S SetFriendNoteC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.SetFriendNoteC2S)
			{
				return null;
			}
			return (SetFriendNoteC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.SetFriendNoteC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SetFriendNoteS2C SetFriendNoteS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.SetFriendNoteS2C)
			{
				return null;
			}
			return (SetFriendNoteS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.SetFriendNoteS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SetOnlineStatusC2S SetOnlineStatusC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.SetOnlineStatusC2S)
			{
				return null;
			}
			return (SetOnlineStatusC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.SetOnlineStatusC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SetOnlineStatusS2C SetOnlineStatusS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.SetOnlineStatusS2C)
			{
				return null;
			}
			return (SetOnlineStatusS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.SetOnlineStatusS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ChooseSkinC2S ChooseSkinC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.ChooseSkinC2S)
			{
				return null;
			}
			return (ChooseSkinC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.ChooseSkinC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ChooseSkinS2C ChooseSkinS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.ChooseSkinS2C)
			{
				return null;
			}
			return (ChooseSkinS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.ChooseSkinS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public TimeWastingC2S TimeWastingC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.TimeWastingC2S)
			{
				return null;
			}
			return (TimeWastingC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.TimeWastingC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public TimeWastingS2C TimeWastingS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.TimeWastingS2C)
			{
				return null;
			}
			return (TimeWastingS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.TimeWastingS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public VoteC2S VoteC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.VoteC2S)
			{
				return null;
			}
			return (VoteC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.VoteC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public VoteS2C VoteS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.VoteS2C)
			{
				return null;
			}
			return (VoteS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.VoteS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public VoteSelectC2S VoteSelectC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.VoteSelectC2S)
			{
				return null;
			}
			return (VoteSelectC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.VoteSelectC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public VoteSelectS2C VoteSelectS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.VoteSelectS2C)
			{
				return null;
			}
			return (VoteSelectS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.VoteSelectS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public NotifyStoryC2S NotifyStoryC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.NotifyStoryC2S)
			{
				return null;
			}
			return (NotifyStoryC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.NotifyStoryC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public NotifyStoryS2C NotifyStoryS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.NotifyStoryS2C)
			{
				return null;
			}
			return (NotifyStoryS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.NotifyStoryS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public PveHeroTalentUpC2S PveHeroTalentUpC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.PveHeroTalentUpC2S)
			{
				return null;
			}
			return (PveHeroTalentUpC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.PveHeroTalentUpC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public PveHeroTalentUpS2C PveHeroTalentUpS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.PveHeroTalentUpS2C)
			{
				return null;
			}
			return (PveHeroTalentUpS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.PveHeroTalentUpS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SelectEventC2S SelectEventC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.SelectEventC2S)
			{
				return null;
			}
			return (SelectEventC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.SelectEventC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SelectEventS2C SelectEventS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.SelectEventS2C)
			{
				return null;
			}
			return (SelectEventS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.SelectEventS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public CampScoreC2S CampScoreC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.CampScoreC2S)
			{
				return null;
			}
			return (CampScoreC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.CampScoreC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public CampScoreS2C CampScoreS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.CampScoreS2C)
			{
				return null;
			}
			return (CampScoreS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.CampScoreS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ActivityMissionRewardC2S ActivityMissionRewardC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.ActivityMissionRewardC2S)
			{
				return null;
			}
			return (ActivityMissionRewardC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.ActivityMissionRewardC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ActivityMissionRewardS2C ActivityMissionRewardS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.ActivityMissionRewardS2C)
			{
				return null;
			}
			return (ActivityMissionRewardS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.ActivityMissionRewardS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public VendorBuyCardC2S VendorBuyCardC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.VendorBuyCardC2S)
			{
				return null;
			}
			return (VendorBuyCardC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.VendorBuyCardC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public VendorBuyCardS2C VendorBuyCardS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.VendorBuyCardS2C)
			{
				return null;
			}
			return (VendorBuyCardS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.VendorBuyCardS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public TransferStarDiscC2S TransferStarDiscC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.TransferStarDiscC2S)
			{
				return null;
			}
			return (TransferStarDiscC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.TransferStarDiscC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public TransferStarDiscS2C TransferStarDiscS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.TransferStarDiscS2C)
			{
				return null;
			}
			return (TransferStarDiscS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.TransferStarDiscS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GetHeroInfoC2S GetHeroInfoC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.GetHeroInfoC2S)
			{
				return null;
			}
			return (GetHeroInfoC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.GetHeroInfoC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GetHeroInfoS2C GetHeroInfoS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.GetHeroInfoS2C)
			{
				return null;
			}
			return (GetHeroInfoS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.GetHeroInfoS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public CreateMatchTeamC2S CreateMatchTeamC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.CreateMatchTeamC2S)
			{
				return null;
			}
			return (CreateMatchTeamC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.CreateMatchTeamC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public CreateMatchTeamS2C CreateMatchTeamS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.CreateMatchTeamS2C)
			{
				return null;
			}
			return (CreateMatchTeamS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.CreateMatchTeamS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ChangeMatchTeamC2S ChangeMatchTeamC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.ChangeMatchTeamC2S)
			{
				return null;
			}
			return (ChangeMatchTeamC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.ChangeMatchTeamC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ChangeMatchTeamS2C ChangeMatchTeamS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.ChangeMatchTeamS2C)
			{
				return null;
			}
			return (ChangeMatchTeamS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.ChangeMatchTeamS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public JoinMatchTeamC2S JoinMatchTeamC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.JoinMatchTeamC2S)
			{
				return null;
			}
			return (JoinMatchTeamC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.JoinMatchTeamC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public JoinMatchTeamS2C JoinMatchTeamS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.JoinMatchTeamS2C)
			{
				return null;
			}
			return (JoinMatchTeamS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.JoinMatchTeamS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ExitMatchTeamC2S ExitMatchTeamC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.ExitMatchTeamC2S)
			{
				return null;
			}
			return (ExitMatchTeamC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.ExitMatchTeamC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ExitMatchTeamS2C ExitMatchTeamS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.ExitMatchTeamS2C)
			{
				return null;
			}
			return (ExitMatchTeamS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.ExitMatchTeamS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RefreshMatchTeamInfoC2S RefreshMatchTeamInfoC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.RefreshMatchTeamInfoC2S)
			{
				return null;
			}
			return (RefreshMatchTeamInfoC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.RefreshMatchTeamInfoC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RefreshMatchTeamInfoS2C RefreshMatchTeamInfoS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.RefreshMatchTeamInfoS2C)
			{
				return null;
			}
			return (RefreshMatchTeamInfoS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.RefreshMatchTeamInfoS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ChinaCreateOrderC2S ChinaCreateOrderC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.ChinaCreateOrderC2S)
			{
				return null;
			}
			return (ChinaCreateOrderC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.ChinaCreateOrderC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ChinaCreateOrderS2C ChinaCreateOrderS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.ChinaCreateOrderS2C)
			{
				return null;
			}
			return (ChinaCreateOrderS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.ChinaCreateOrderS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MatchTeamInviteC2S MatchTeamInviteC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.MatchTeamInviteC2S)
			{
				return null;
			}
			return (MatchTeamInviteC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.MatchTeamInviteC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MatchTeamInviteS2C MatchTeamInviteS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.MatchTeamInviteS2C)
			{
				return null;
			}
			return (MatchTeamInviteS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.MatchTeamInviteS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MatchTeamChatC2S MatchTeamChatC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.MatchTeamChatC2S)
			{
				return null;
			}
			return (MatchTeamChatC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.MatchTeamChatC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MatchTeamChatS2C MatchTeamChatS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.MatchTeamChatS2C)
			{
				return null;
			}
			return (MatchTeamChatS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.MatchTeamChatS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MatchTeamReadyC2S MatchTeamReadyC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.MatchTeamReadyC2S)
			{
				return null;
			}
			return (MatchTeamReadyC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.MatchTeamReadyC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MatchTeamReadyS2C MatchTeamReadyS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.MatchTeamReadyS2C)
			{
				return null;
			}
			return (MatchTeamReadyS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.MatchTeamReadyS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public PlayerChatC2S PlayerChatC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.PlayerChatC2S)
			{
				return null;
			}
			return (PlayerChatC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.PlayerChatC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public PlayerChatS2C PlayerChatS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.PlayerChatS2C)
			{
				return null;
			}
			return (PlayerChatS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.PlayerChatS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SyncSingleGameDataC2S SyncSingleGameDataC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.SyncSingleGameDataC2S)
			{
				return null;
			}
			return (SyncSingleGameDataC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.SyncSingleGameDataC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SyncSingleGameDataS2C SyncSingleGameDataS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.SyncSingleGameDataS2C)
			{
				return null;
			}
			return (SyncSingleGameDataS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.SyncSingleGameDataS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SingleGameDataC2S SingleGameDataC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.SingleGameDataC2S)
			{
				return null;
			}
			return (SingleGameDataC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.SingleGameDataC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SingleGameDataS2C SingleGameDataS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.SingleGameDataS2C)
			{
				return null;
			}
			return (SingleGameDataS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.SingleGameDataS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GetActivityPassRewardC2S GetActivityPassRewardC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.GetActivityPassRewardC2S)
			{
				return null;
			}
			return (GetActivityPassRewardC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.GetActivityPassRewardC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GetActivityPassRewardS2C GetActivityPassRewardS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.GetActivityPassRewardS2C)
			{
				return null;
			}
			return (GetActivityPassRewardS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.GetActivityPassRewardS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ApplyChangeSlotC2S ApplyChangeSlotC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.ApplyChangeSlotC2S)
			{
				return null;
			}
			return (ApplyChangeSlotC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.ApplyChangeSlotC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ApplyChangeSlotS2C ApplyChangeSlotS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.ApplyChangeSlotS2C)
			{
				return null;
			}
			return (ApplyChangeSlotS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.ApplyChangeSlotS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public OpsChangeSlotC2S OpsChangeSlotC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.OpsChangeSlotC2S)
			{
				return null;
			}
			return (OpsChangeSlotC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.OpsChangeSlotC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public OpsChangeSlotS2C OpsChangeSlotS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.OpsChangeSlotS2C)
			{
				return null;
			}
			return (OpsChangeSlotS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.OpsChangeSlotS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RookieGachaRewardC2S RookieGachaRewardC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.RookieGachaRewardC2S)
			{
				return null;
			}
			return (RookieGachaRewardC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.RookieGachaRewardC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public RookieGachaRewardS2C RookieGachaRewardS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.RookieGachaRewardS2C)
			{
				return null;
			}
			return (RookieGachaRewardS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.RookieGachaRewardS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public LiveGiftPackageC2S LiveGiftPackageC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.LiveGiftPackageC2S)
			{
				return null;
			}
			return (LiveGiftPackageC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.LiveGiftPackageC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public LiveGiftPackageS2C LiveGiftPackageS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.LiveGiftPackageS2C)
			{
				return null;
			}
			return (LiveGiftPackageS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.LiveGiftPackageS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public LaborActDiceC2S LaborActDiceC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.LaborActDiceC2S)
			{
				return null;
			}
			return (LaborActDiceC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.LaborActDiceC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public LaborActDiceS2C LaborActDiceS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.LaborActDiceS2C)
			{
				return null;
			}
			return (LaborActDiceS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.LaborActDiceS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ActionOverTimeLogC2S ActionOverTimeLogC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.ActionOverTimeLogC2S)
			{
				return null;
			}
			return (ActionOverTimeLogC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.ActionOverTimeLogC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ActionOverTimeLogS2C ActionOverTimeLogS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.ActionOverTimeLogS2C)
			{
				return null;
			}
			return (ActionOverTimeLogS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.ActionOverTimeLogS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ClientHarmonyC2S ClientHarmonyC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.ClientHarmonyC2S)
			{
				return null;
			}
			return (ClientHarmonyC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.ClientHarmonyC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ClientHarmonyS2C ClientHarmonyS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.ClientHarmonyS2C)
			{
				return null;
			}
			return (ClientHarmonyS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.ClientHarmonyS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MailStarC2S MailStarC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.MailStarC2S)
			{
				return null;
			}
			return (MailStarC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.MailStarC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MailStarS2C MailStarS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.MailStarS2C)
			{
				return null;
			}
			return (MailStarS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.MailStarS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SetCardAltArtC2S SetCardAltArtC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.SetCardAltArtC2S)
			{
				return null;
			}
			return (SetCardAltArtC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.SetCardAltArtC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SetCardAltArtS2C SetCardAltArtS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.SetCardAltArtS2C)
			{
				return null;
			}
			return (SetCardAltArtS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.SetCardAltArtS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SelectRewardCardC2S SelectRewardCardC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.SelectRewardCardC2S)
			{
				return null;
			}
			return (SelectRewardCardC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.SelectRewardCardC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SelectRewardCardS2C SelectRewardCardS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.SelectRewardCardS2C)
			{
				return null;
			}
			return (SelectRewardCardS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.SelectRewardCardS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GetQuestionUrlC2S GetQuestionUrlC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.GetQuestionUrlC2S)
			{
				return null;
			}
			return (GetQuestionUrlC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.GetQuestionUrlC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GetQuestionUrlS2C GetQuestionUrlS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.GetQuestionUrlS2C)
			{
				return null;
			}
			return (GetQuestionUrlS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.GetQuestionUrlS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GetReturnInfoC2S GetReturnInfoC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.GetReturnInfoC2S)
			{
				return null;
			}
			return (GetReturnInfoC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.GetReturnInfoC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GetReturnInfoS2C GetReturnInfoS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.GetReturnInfoS2C)
			{
				return null;
			}
			return (GetReturnInfoS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.GetReturnInfoS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ReturnGiftClaimC2S ReturnGiftClaimC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.ReturnGiftClaimC2S)
			{
				return null;
			}
			return (ReturnGiftClaimC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.ReturnGiftClaimC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ReturnGiftClaimS2C ReturnGiftClaimS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.ReturnGiftClaimS2C)
			{
				return null;
			}
			return (ReturnGiftClaimS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.ReturnGiftClaimS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ReturnSignInClaimC2S ReturnSignInClaimC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.ReturnSignInClaimC2S)
			{
				return null;
			}
			return (ReturnSignInClaimC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.ReturnSignInClaimC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ReturnSignInClaimS2C ReturnSignInClaimS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.ReturnSignInClaimS2C)
			{
				return null;
			}
			return (ReturnSignInClaimS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.ReturnSignInClaimS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ReturnSurveyFinishC2S ReturnSurveyFinishC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.ReturnSurveyFinishC2S)
			{
				return null;
			}
			return (ReturnSurveyFinishC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.ReturnSurveyFinishC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ReturnSurveyFinishS2C ReturnSurveyFinishS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.ReturnSurveyFinishS2C)
			{
				return null;
			}
			return (ReturnSurveyFinishS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.ReturnSurveyFinishS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FlipCardC2S FlipCardC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.FlipCardC2S)
			{
				return null;
			}
			return (FlipCardC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.FlipCardC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FlipCardS2C FlipCardS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.FlipCardS2C)
			{
				return null;
			}
			return (FlipCardS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.FlipCardS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FlipCardProgressRewardC2S FlipCardProgressRewardC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.FlipCardProgressRewardC2S)
			{
				return null;
			}
			return (FlipCardProgressRewardC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.FlipCardProgressRewardC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public FlipCardProgressRewardS2C FlipCardProgressRewardS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.FlipCardProgressRewardS2C)
			{
				return null;
			}
			return (FlipCardProgressRewardS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.FlipCardProgressRewardS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SyncPlayerGuildS2C SyncPlayerGuildS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.SyncPlayerGuildS2C)
			{
				return null;
			}
			return (SyncPlayerGuildS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.SyncPlayerGuildS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SyncPlayerJoinGuildS2C SyncPlayerJoinGuildS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.SyncPlayerJoinGuildS2C)
			{
				return null;
			}
			return (SyncPlayerJoinGuildS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.SyncPlayerJoinGuildS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GuildChatMsgS2C GuildChatMsgS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.GuildChatMsgS2C)
			{
				return null;
			}
			return (GuildChatMsgS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.GuildChatMsgS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SyncGuildS2C SyncGuildS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.SyncGuildS2C)
			{
				return null;
			}
			return (SyncGuildS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.SyncGuildS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SyncGuildMemberS2C SyncGuildMemberS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.SyncGuildMemberS2C)
			{
				return null;
			}
			return (SyncGuildMemberS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.SyncGuildMemberS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SyncGuildMemberExitS2C SyncGuildMemberExitS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.SyncGuildMemberExitS2C)
			{
				return null;
			}
			return (SyncGuildMemberExitS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.SyncGuildMemberExitS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public CreateGuildC2S CreateGuildC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.CreateGuildC2S)
			{
				return null;
			}
			return (CreateGuildC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.CreateGuildC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public CreateGuildS2C CreateGuildS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.CreateGuildS2C)
			{
				return null;
			}
			return (CreateGuildS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.CreateGuildS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SearchGuildC2S SearchGuildC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.SearchGuildC2S)
			{
				return null;
			}
			return (SearchGuildC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.SearchGuildC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SearchGuildS2C SearchGuildS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.SearchGuildS2C)
			{
				return null;
			}
			return (SearchGuildS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.SearchGuildS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ApplyToGuildC2S ApplyToGuildC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.ApplyToGuildC2S)
			{
				return null;
			}
			return (ApplyToGuildC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.ApplyToGuildC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ApplyToGuildS2C ApplyToGuildS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.ApplyToGuildS2C)
			{
				return null;
			}
			return (ApplyToGuildS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.ApplyToGuildS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ProcessGuildApplicationC2S ProcessGuildApplicationC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.ProcessGuildApplicationC2S)
			{
				return null;
			}
			return (ProcessGuildApplicationC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.ProcessGuildApplicationC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ProcessGuildApplicationS2C ProcessGuildApplicationS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.ProcessGuildApplicationS2C)
			{
				return null;
			}
			return (ProcessGuildApplicationS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.ProcessGuildApplicationS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SendGuildInvitationC2S SendGuildInvitationC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.SendGuildInvitationC2S)
			{
				return null;
			}
			return (SendGuildInvitationC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.SendGuildInvitationC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SendGuildInvitationS2C SendGuildInvitationS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.SendGuildInvitationS2C)
			{
				return null;
			}
			return (SendGuildInvitationS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.SendGuildInvitationS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ProcessGuildInvitationC2S ProcessGuildInvitationC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.ProcessGuildInvitationC2S)
			{
				return null;
			}
			return (ProcessGuildInvitationC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.ProcessGuildInvitationC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ProcessGuildInvitationS2C ProcessGuildInvitationS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.ProcessGuildInvitationS2C)
			{
				return null;
			}
			return (ProcessGuildInvitationS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.ProcessGuildInvitationS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GetGuildInfoC2S GetGuildInfoC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.GetGuildInfoC2S)
			{
				return null;
			}
			return (GetGuildInfoC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.GetGuildInfoC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GetGuildInfoS2C GetGuildInfoS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.GetGuildInfoS2C)
			{
				return null;
			}
			return (GetGuildInfoS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.GetGuildInfoS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public UpdateGuildSettingsC2S UpdateGuildSettingsC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.UpdateGuildSettingsC2S)
			{
				return null;
			}
			return (UpdateGuildSettingsC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.UpdateGuildSettingsC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public UpdateGuildSettingsS2C UpdateGuildSettingsS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.UpdateGuildSettingsS2C)
			{
				return null;
			}
			return (UpdateGuildSettingsS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.UpdateGuildSettingsS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public UpdateGuildInAnnouncementC2S UpdateGuildInAnnouncementC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.UpdateGuildInAnnouncementC2S)
			{
				return null;
			}
			return (UpdateGuildInAnnouncementC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.UpdateGuildInAnnouncementC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public UpdateGuildInAnnouncementS2C UpdateGuildInAnnouncementS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.UpdateGuildInAnnouncementS2C)
			{
				return null;
			}
			return (UpdateGuildInAnnouncementS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.UpdateGuildInAnnouncementS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public TransferGuildMasterC2S TransferGuildMasterC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.TransferGuildMasterC2S)
			{
				return null;
			}
			return (TransferGuildMasterC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.TransferGuildMasterC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public TransferGuildMasterS2C TransferGuildMasterS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.TransferGuildMasterS2C)
			{
				return null;
			}
			return (TransferGuildMasterS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.TransferGuildMasterS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ChangeGuildMemberTitleC2S ChangeGuildMemberTitleC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.ChangeGuildMemberTitleC2S)
			{
				return null;
			}
			return (ChangeGuildMemberTitleC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.ChangeGuildMemberTitleC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ChangeGuildMemberTitleS2C ChangeGuildMemberTitleS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.ChangeGuildMemberTitleS2C)
			{
				return null;
			}
			return (ChangeGuildMemberTitleS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.ChangeGuildMemberTitleS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public KickGuildMemberC2S KickGuildMemberC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.KickGuildMemberC2S)
			{
				return null;
			}
			return (KickGuildMemberC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.KickGuildMemberC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public KickGuildMemberS2C KickGuildMemberS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.KickGuildMemberS2C)
			{
				return null;
			}
			return (KickGuildMemberS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.KickGuildMemberS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ImpeachGuildMasterC2S ImpeachGuildMasterC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.ImpeachGuildMasterC2S)
			{
				return null;
			}
			return (ImpeachGuildMasterC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.ImpeachGuildMasterC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ImpeachGuildMasterS2C ImpeachGuildMasterS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.ImpeachGuildMasterS2C)
			{
				return null;
			}
			return (ImpeachGuildMasterS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.ImpeachGuildMasterS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ExitGuildC2S ExitGuildC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.ExitGuildC2S)
			{
				return null;
			}
			return (ExitGuildC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.ExitGuildC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public ExitGuildS2C ExitGuildS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.ExitGuildS2C)
			{
				return null;
			}
			return (ExitGuildS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.ExitGuildS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public DisbandGuildC2S DisbandGuildC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.DisbandGuildC2S)
			{
				return null;
			}
			return (DisbandGuildC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.DisbandGuildC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public DisbandGuildS2C DisbandGuildS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.DisbandGuildS2C)
			{
				return null;
			}
			return (DisbandGuildS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.DisbandGuildS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GuildMissionRewardC2S GuildMissionRewardC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.GuildMissionRewardC2S)
			{
				return null;
			}
			return (GuildMissionRewardC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.GuildMissionRewardC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GuildMissionRewardS2C GuildMissionRewardS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.GuildMissionRewardS2C)
			{
				return null;
			}
			return (GuildMissionRewardS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.GuildMissionRewardS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GetGuildMemberChangeMsgC2S GetGuildMemberChangeMsgC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.GetGuildMemberChangeMsgC2S)
			{
				return null;
			}
			return (GetGuildMemberChangeMsgC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.GetGuildMemberChangeMsgC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GetGuildMemberChangeMsgS2C GetGuildMemberChangeMsgS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.GetGuildMemberChangeMsgS2C)
			{
				return null;
			}
			return (GetGuildMemberChangeMsgS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.GetGuildMemberChangeMsgS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SendGuildChatMsgC2S SendGuildChatMsgC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.SendGuildChatMsgC2S)
			{
				return null;
			}
			return (SendGuildChatMsgC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.SendGuildChatMsgC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public SendGuildChatMsgS2C SendGuildChatMsgS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.SendGuildChatMsgS2C)
			{
				return null;
			}
			return (SendGuildChatMsgS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.SendGuildChatMsgS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GetGuildChatMsgC2S GetGuildChatMsgC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.GetGuildChatMsgC2S)
			{
				return null;
			}
			return (GetGuildChatMsgC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.GetGuildChatMsgC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GetGuildChatMsgS2C GetGuildChatMsgS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.GetGuildChatMsgS2C)
			{
				return null;
			}
			return (GetGuildChatMsgS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.GetGuildChatMsgS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GuildMemberC2S GuildMemberC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.GuildMemberC2S)
			{
				return null;
			}
			return (GuildMemberC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.GuildMemberC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GuildMemberS2C GuildMemberS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.GuildMemberS2C)
			{
				return null;
			}
			return (GuildMemberS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.GuildMemberS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GetGuildsInfoC2S GetGuildsInfoC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.GetGuildsInfoC2S)
			{
				return null;
			}
			return (GetGuildsInfoC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.GetGuildsInfoC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public GetGuildsInfoS2C GetGuildsInfoS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.GetGuildsInfoS2C)
			{
				return null;
			}
			return (GetGuildsInfoS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.GetGuildsInfoS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public TestRpcEchoC2S TestRpcEchoC2S
	{
		get
		{
			if (msgCase_ != MsgOneofCase.TestRpcEchoC2S)
			{
				return null;
			}
			return (TestRpcEchoC2S)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.TestRpcEchoC2S : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public TestRpcEchoS2C TestRpcEchoS2C
	{
		get
		{
			if (msgCase_ != MsgOneofCase.TestRpcEchoS2C)
			{
				return null;
			}
			return (TestRpcEchoS2C)msg_;
		}
		set
		{
			msg_ = value;
			msgCase_ = ((value != null) ? MsgOneofCase.TestRpcEchoS2C : MsgOneofCase.None);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public MsgOneofCase MsgCase => msgCase_;

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public protocol()
	{
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public protocol(protocol other)
		: this()
	{
		switch (other.MsgCase)
		{
		case MsgOneofCase.SysSendMailC2S:
			SysSendMailC2S = other.SysSendMailC2S.Clone();
			break;
		case MsgOneofCase.SysPlayerOnlineC2S:
			SysPlayerOnlineC2S = other.SysPlayerOnlineC2S.Clone();
			break;
		case MsgOneofCase.PlayerOnlineRoomC2S:
			PlayerOnlineRoomC2S = other.PlayerOnlineRoomC2S.Clone();
			break;
		case MsgOneofCase.SysCanPraiseInfoC2S:
			SysCanPraiseInfoC2S = other.SysCanPraiseInfoC2S.Clone();
			break;
		case MsgOneofCase.SysPraiseC2S:
			SysPraiseC2S = other.SysPraiseC2S.Clone();
			break;
		case MsgOneofCase.SysRoomFinishC2S:
			SysRoomFinishC2S = other.SysRoomFinishC2S.Clone();
			break;
		case MsgOneofCase.SysRoomAddExpC2S:
			SysRoomAddExpC2S = other.SysRoomAddExpC2S.Clone();
			break;
		case MsgOneofCase.SysGetShowFriendC2S:
			SysGetShowFriendC2S = other.SysGetShowFriendC2S.Clone();
			break;
		case MsgOneofCase.SysFriendInviteC2S:
			SysFriendInviteC2S = other.SysFriendInviteC2S.Clone();
			break;
		case MsgOneofCase.SysFriendDelC2S:
			SysFriendDelC2S = other.SysFriendDelC2S.Clone();
			break;
		case MsgOneofCase.SysFriendInfoC2S:
			SysFriendInfoC2S = other.SysFriendInfoC2S.Clone();
			break;
		case MsgOneofCase.SysFriendApplyC2S:
			SysFriendApplyC2S = other.SysFriendApplyC2S.Clone();
			break;
		case MsgOneofCase.SysFriendAddC2S:
			SysFriendAddC2S = other.SysFriendAddC2S.Clone();
			break;
		case MsgOneofCase.SysFriendSendMsgC2S:
			SysFriendSendMsgC2S = other.SysFriendSendMsgC2S.Clone();
			break;
		case MsgOneofCase.SysCampaignFinishC2S:
			SysCampaignFinishC2S = other.SysCampaignFinishC2S.Clone();
			break;
		case MsgOneofCase.SysAbroadPayMsgC2S:
			SysAbroadPayMsgC2S = other.SysAbroadPayMsgC2S.Clone();
			break;
		case MsgOneofCase.AcquisitionMsgC2S:
			AcquisitionMsgC2S = other.AcquisitionMsgC2S.Clone();
			break;
		case MsgOneofCase.SysFightRecord:
			SysFightRecord = other.SysFightRecord.Clone();
			break;
		case MsgOneofCase.SysCampaignAwardC2S:
			SysCampaignAwardC2S = other.SysCampaignAwardC2S.Clone();
			break;
		case MsgOneofCase.SysRecoupItemC2S:
			SysRecoupItemC2S = other.SysRecoupItemC2S.Clone();
			break;
		case MsgOneofCase.SysPlayerOnlineRoomC2S:
			SysPlayerOnlineRoomC2S = other.SysPlayerOnlineRoomC2S.Clone();
			break;
		case MsgOneofCase.SysPlayerCleanC2S:
			SysPlayerCleanC2S = other.SysPlayerCleanC2S.Clone();
			break;
		case MsgOneofCase.SysPlayerPunishmentTimeC2S:
			SysPlayerPunishmentTimeC2S = other.SysPlayerPunishmentTimeC2S.Clone();
			break;
		case MsgOneofCase.SysMatchSuccess:
			SysMatchSuccess = other.SysMatchSuccess.Clone();
			break;
		case MsgOneofCase.SysChinaPayMsgC2S:
			SysChinaPayMsgC2S = other.SysChinaPayMsgC2S.Clone();
			break;
		case MsgOneofCase.SysChangeMatchTeamState:
			SysChangeMatchTeamState = other.SysChangeMatchTeamState.Clone();
			break;
		case MsgOneofCase.SysSaveSimplePlayerInfoC2S:
			SysSaveSimplePlayerInfoC2S = other.SysSaveSimplePlayerInfoC2S.Clone();
			break;
		case MsgOneofCase.SysGmChangeNameC2S:
			SysGmChangeNameC2S = other.SysGmChangeNameC2S.Clone();
			break;
		case MsgOneofCase.SysSyncPlayerC2S:
			SysSyncPlayerC2S = other.SysSyncPlayerC2S.Clone();
			break;
		case MsgOneofCase.SysPlayerCreditScoreChangeC2S:
			SysPlayerCreditScoreChangeC2S = other.SysPlayerCreditScoreChangeC2S.Clone();
			break;
		case MsgOneofCase.SysSyncPlayerMatchPunishmentTimeC2S:
			SysSyncPlayerMatchPunishmentTimeC2S = other.SysSyncPlayerMatchPunishmentTimeC2S.Clone();
			break;
		case MsgOneofCase.GMChangeCreditScoreC2S:
			GMChangeCreditScoreC2S = other.GMChangeCreditScoreC2S.Clone();
			break;
		case MsgOneofCase.SysPushReturnInfoC2S:
			SysPushReturnInfoC2S = other.SysPushReturnInfoC2S.Clone();
			break;
		case MsgOneofCase.SysMutePlayerC2S:
			SysMutePlayerC2S = other.SysMutePlayerC2S.Clone();
			break;
		case MsgOneofCase.SysQuestionC2S:
			SysQuestionC2S = other.SysQuestionC2S.Clone();
			break;
		case MsgOneofCase.KickS2C:
			KickS2C = other.KickS2C.Clone();
			break;
		case MsgOneofCase.PredictActionS2C:
			PredictActionS2C = other.PredictActionS2C.Clone();
			break;
		case MsgOneofCase.RunningGameS2C:
			RunningGameS2C = other.RunningGameS2C.Clone();
			break;
		case MsgOneofCase.BattleS2C:
			BattleS2C = other.BattleS2C.Clone();
			break;
		case MsgOneofCase.LotteryDrawS2C:
			LotteryDrawS2C = other.LotteryDrawS2C.Clone();
			break;
		case MsgOneofCase.LandBuffsS2C:
			LandBuffsS2C = other.LandBuffsS2C.Clone();
			break;
		case MsgOneofCase.RoundStartS2C:
			RoundStartS2C = other.RoundStartS2C.Clone();
			break;
		case MsgOneofCase.GameFinishS2C:
			GameFinishS2C = other.GameFinishS2C.Clone();
			break;
		case MsgOneofCase.MonsterRefreshS2C:
			MonsterRefreshS2C = other.MonsterRefreshS2C.Clone();
			break;
		case MsgOneofCase.MovePointBuffS2C:
			MovePointBuffS2C = other.MovePointBuffS2C.Clone();
			break;
		case MsgOneofCase.ChangeDirS2C:
			ChangeDirS2C = other.ChangeDirS2C.Clone();
			break;
		case MsgOneofCase.GambleChangeS2C:
			GambleChangeS2C = other.GambleChangeS2C.Clone();
			break;
		case MsgOneofCase.HeroBarBoxChangeS2C:
			HeroBarBoxChangeS2C = other.HeroBarBoxChangeS2C.Clone();
			break;
		case MsgOneofCase.RoomNotifyS2C:
			RoomNotifyS2C = other.RoomNotifyS2C.Clone();
			break;
		case MsgOneofCase.ActionStartNotifyS2C:
			ActionStartNotifyS2C = other.ActionStartNotifyS2C.Clone();
			break;
		case MsgOneofCase.NoGambleNotifyS2C:
			NoGambleNotifyS2C = other.NoGambleNotifyS2C.Clone();
			break;
		case MsgOneofCase.GambleObServeS2C:
			GambleObServeS2C = other.GambleObServeS2C.Clone();
			break;
		case MsgOneofCase.UpdateHeroAttrS2C:
			UpdateHeroAttrS2C = other.UpdateHeroAttrS2C.Clone();
			break;
		case MsgOneofCase.ChangePlayerSlotS2C:
			ChangePlayerSlotS2C = other.ChangePlayerSlotS2C.Clone();
			break;
		case MsgOneofCase.BossSleepS2C:
			BossSleepS2C = other.BossSleepS2C.Clone();
			break;
		case MsgOneofCase.RefMallS2C:
			RefMallS2C = other.RefMallS2C.Clone();
			break;
		case MsgOneofCase.BagItemChangeS2C:
			BagItemChangeS2C = other.BagItemChangeS2C.Clone();
			break;
		case MsgOneofCase.RoleCardChangeS2C:
			RoleCardChangeS2C = other.RoleCardChangeS2C.Clone();
			break;
		case MsgOneofCase.TaskConditionS2C:
			TaskConditionS2C = other.TaskConditionS2C.Clone();
			break;
		case MsgOneofCase.TaskInfoS2C:
			TaskInfoS2C = other.TaskInfoS2C.Clone();
			break;
		case MsgOneofCase.PlayerOnlineS2C:
			PlayerOnlineS2C = other.PlayerOnlineS2C.Clone();
			break;
		case MsgOneofCase.ChangeExpS2C:
			ChangeExpS2C = other.ChangeExpS2C.Clone();
			break;
		case MsgOneofCase.MailAddS2C:
			MailAddS2C = other.MailAddS2C.Clone();
			break;
		case MsgOneofCase.OnlineSyncRoomIdS2C:
			OnlineSyncRoomIdS2C = other.OnlineSyncRoomIdS2C.Clone();
			break;
		case MsgOneofCase.NoticeS2C:
			NoticeS2C = other.NoticeS2C.Clone();
			break;
		case MsgOneofCase.ActivityTaskConditionS2C:
			ActivityTaskConditionS2C = other.ActivityTaskConditionS2C.Clone();
			break;
		case MsgOneofCase.MapEventS2C:
			MapEventS2C = other.MapEventS2C.Clone();
			break;
		case MsgOneofCase.Day7RewardS2C:
			Day7RewardS2C = other.Day7RewardS2C.Clone();
			break;
		case MsgOneofCase.MapEventTrainS2C:
			MapEventTrainS2C = other.MapEventTrainS2C.Clone();
			break;
		case MsgOneofCase.ChangePraiseNumS2C:
			ChangePraiseNumS2C = other.ChangePraiseNumS2C.Clone();
			break;
		case MsgOneofCase.MonthlyCardS2C:
			MonthlyCardS2C = other.MonthlyCardS2C.Clone();
			break;
		case MsgOneofCase.MailDelS2C:
			MailDelS2C = other.MailDelS2C.Clone();
			break;
		case MsgOneofCase.FriendNotifyS2C:
			FriendNotifyS2C = other.FriendNotifyS2C.Clone();
			break;
		case MsgOneofCase.FriendListChangeS2C:
			FriendListChangeS2C = other.FriendListChangeS2C.Clone();
			break;
		case MsgOneofCase.FriendInviteNotifyS2C:
			FriendInviteNotifyS2C = other.FriendInviteNotifyS2C.Clone();
			break;
		case MsgOneofCase.LoopNoticeS2C:
			LoopNoticeS2C = other.LoopNoticeS2C.Clone();
			break;
		case MsgOneofCase.BattlePassLvS2C:
			BattlePassLvS2C = other.BattlePassLvS2C.Clone();
			break;
		case MsgOneofCase.BattlePassTaskInfoS2C:
			BattlePassTaskInfoS2C = other.BattlePassTaskInfoS2C.Clone();
			break;
		case MsgOneofCase.BattlePassUpdateTaskS2C:
			BattlePassUpdateTaskS2C = other.BattlePassUpdateTaskS2C.Clone();
			break;
		case MsgOneofCase.BattlePassBuyS2C:
			BattlePassBuyS2C = other.BattlePassBuyS2C.Clone();
			break;
		case MsgOneofCase.BattlePassInfoS2C:
			BattlePassInfoS2C = other.BattlePassInfoS2C.Clone();
			break;
		case MsgOneofCase.FriendsChatMsgS2C:
			FriendsChatMsgS2C = other.FriendsChatMsgS2C.Clone();
			break;
		case MsgOneofCase.GameProgressChangeS2C:
			GameProgressChangeS2C = other.GameProgressChangeS2C.Clone();
			break;
		case MsgOneofCase.MapMissionNotifyS2C:
			MapMissionNotifyS2C = other.MapMissionNotifyS2C.Clone();
			break;
		case MsgOneofCase.ChangeItemLimitS2C:
			ChangeItemLimitS2C = other.ChangeItemLimitS2C.Clone();
			break;
		case MsgOneofCase.CleanItemLimitS2C:
			CleanItemLimitS2C = other.CleanItemLimitS2C.Clone();
			break;
		case MsgOneofCase.CampaignPassS2C:
			CampaignPassS2C = other.CampaignPassS2C.Clone();
			break;
		case MsgOneofCase.CampaignNotifyS2C:
			CampaignNotifyS2C = other.CampaignNotifyS2C.Clone();
			break;
		case MsgOneofCase.KillMessageS2C:
			KillMessageS2C = other.KillMessageS2C.Clone();
			break;
		case MsgOneofCase.GameScoreChangeS2C:
			GameScoreChangeS2C = other.GameScoreChangeS2C.Clone();
			break;
		case MsgOneofCase.SignInRewardS2C:
			SignInRewardS2C = other.SignInRewardS2C.Clone();
			break;
		case MsgOneofCase.PayResultS2C:
			PayResultS2C = other.PayResultS2C.Clone();
			break;
		case MsgOneofCase.PayInfoChangeS2C:
			PayInfoChangeS2C = other.PayInfoChangeS2C.Clone();
			break;
		case MsgOneofCase.InviteSuccessS2C:
			InviteSuccessS2C = other.InviteSuccessS2C.Clone();
			break;
		case MsgOneofCase.InviteInfoNotifyS2C:
			InviteInfoNotifyS2C = other.InviteInfoNotifyS2C.Clone();
			break;
		case MsgOneofCase.MapStatusChangeS2C:
			MapStatusChangeS2C = other.MapStatusChangeS2C.Clone();
			break;
		case MsgOneofCase.GachaCountS2C:
			GachaCountS2C = other.GachaCountS2C.Clone();
			break;
		case MsgOneofCase.MapIndexChangeS2C:
			MapIndexChangeS2C = other.MapIndexChangeS2C.Clone();
			break;
		case MsgOneofCase.SurrenderPunishS2C:
			SurrenderPunishS2C = other.SurrenderPunishS2C.Clone();
			break;
		case MsgOneofCase.GamePassMapSuccessS2C:
			GamePassMapSuccessS2C = other.GamePassMapSuccessS2C.Clone();
			break;
		case MsgOneofCase.FriendDelNotifyS2C:
			FriendDelNotifyS2C = other.FriendDelNotifyS2C.Clone();
			break;
		case MsgOneofCase.BagExpiredTransformNotify:
			BagExpiredTransformNotify = other.BagExpiredTransformNotify.Clone();
			break;
		case MsgOneofCase.MapEventCrabS2C:
			MapEventCrabS2C = other.MapEventCrabS2C.Clone();
			break;
		case MsgOneofCase.PkAfterVoteS2C:
			PkAfterVoteS2C = other.PkAfterVoteS2C.Clone();
			break;
		case MsgOneofCase.PlayerTaskNotifyS2C:
			PlayerTaskNotifyS2C = other.PlayerTaskNotifyS2C.Clone();
			break;
		case MsgOneofCase.UnLockDifficultyS2C:
			UnLockDifficultyS2C = other.UnLockDifficultyS2C.Clone();
			break;
		case MsgOneofCase.HeroSkillMoveEffectS2C:
			HeroSkillMoveEffectS2C = other.HeroSkillMoveEffectS2C.Clone();
			break;
		case MsgOneofCase.TimeOutKickPlayerS2C:
			TimeOutKickPlayerS2C = other.TimeOutKickPlayerS2C.Clone();
			break;
		case MsgOneofCase.SayPhraseNotifyS2C:
			SayPhraseNotifyS2C = other.SayPhraseNotifyS2C.Clone();
			break;
		case MsgOneofCase.MatchTeamInviteNotify:
			MatchTeamInviteNotify = other.MatchTeamInviteNotify.Clone();
			break;
		case MsgOneofCase.RefreshMatchTeamStateNotify:
			RefreshMatchTeamStateNotify = other.RefreshMatchTeamStateNotify.Clone();
			break;
		case MsgOneofCase.ActivityPassGearChangeS2C:
			ActivityPassGearChangeS2C = other.ActivityPassGearChangeS2C.Clone();
			break;
		case MsgOneofCase.SingleGameScoreChange:
			SingleGameScoreChange = other.SingleGameScoreChange.Clone();
			break;
		case MsgOneofCase.DelayProgressMapEventS2C:
			DelayProgressMapEventS2C = other.DelayProgressMapEventS2C.Clone();
			break;
		case MsgOneofCase.LuckyStarMissionChangeS2C:
			LuckyStarMissionChangeS2C = other.LuckyStarMissionChangeS2C.Clone();
			break;
		case MsgOneofCase.MatchPunishmentS2C:
			MatchPunishmentS2C = other.MatchPunishmentS2C.Clone();
			break;
		case MsgOneofCase.ChallengeDataChangeS2C:
			ChallengeDataChangeS2C = other.ChallengeDataChangeS2C.Clone();
			break;
		case MsgOneofCase.GmUnlockRoleInfoS2C:
			GmUnlockRoleInfoS2C = other.GmUnlockRoleInfoS2C.Clone();
			break;
		case MsgOneofCase.SyncPlayerCreditInfoS2C:
			SyncPlayerCreditInfoS2C = other.SyncPlayerCreditInfoS2C.Clone();
			break;
		case MsgOneofCase.RoomHeroCardChangeS2C:
			RoomHeroCardChangeS2C = other.RoomHeroCardChangeS2C.Clone();
			break;
		case MsgOneofCase.RoomRoundAddTermS2C:
			RoomRoundAddTermS2C = other.RoomRoundAddTermS2C.Clone();
			break;
		case MsgOneofCase.ReturnInfoS2C:
			ReturnInfoS2C = other.ReturnInfoS2C.Clone();
			break;
		case MsgOneofCase.SyncRelicsS2C:
			SyncRelicsS2C = other.SyncRelicsS2C.Clone();
			break;
		case MsgOneofCase.ReplaySnapshotS2C:
			ReplaySnapshotS2C = other.ReplaySnapshotS2C.Clone();
			break;
		case MsgOneofCase.ClueNotifyS2C:
			ClueNotifyS2C = other.ClueNotifyS2C.Clone();
			break;
		case MsgOneofCase.ReplayDieS2C:
			ReplayDieS2C = other.ReplayDieS2C.Clone();
			break;
		case MsgOneofCase.GuildTaskNotifyS2C:
			GuildTaskNotifyS2C = other.GuildTaskNotifyS2C.Clone();
			break;
		case MsgOneofCase.GameRoundChangeS2C:
			GameRoundChangeS2C = other.GameRoundChangeS2C.Clone();
			break;
		case MsgOneofCase.NotifyQuestionS2C:
			NotifyQuestionS2C = other.NotifyQuestionS2C.Clone();
			break;
		case MsgOneofCase.ConnectHandle:
			ConnectHandle = other.ConnectHandle.Clone();
			break;
		case MsgOneofCase.Connect:
			Connect = other.Connect.Clone();
			break;
		case MsgOneofCase.HeartbeatHandle:
			HeartbeatHandle = other.HeartbeatHandle.Clone();
			break;
		case MsgOneofCase.Heartbeat:
			Heartbeat = other.Heartbeat.Clone();
			break;
		case MsgOneofCase.CreateRoomHandle:
			CreateRoomHandle = other.CreateRoomHandle.Clone();
			break;
		case MsgOneofCase.CreateRoom:
			CreateRoom = other.CreateRoom.Clone();
			break;
		case MsgOneofCase.SyncRoomHandle:
			SyncRoomHandle = other.SyncRoomHandle.Clone();
			break;
		case MsgOneofCase.SyncRoom:
			SyncRoom = other.SyncRoom.Clone();
			break;
		case MsgOneofCase.JoinRoomHandle:
			JoinRoomHandle = other.JoinRoomHandle.Clone();
			break;
		case MsgOneofCase.JoinRoom:
			JoinRoom = other.JoinRoom.Clone();
			break;
		case MsgOneofCase.ExitRoomHandle:
			ExitRoomHandle = other.ExitRoomHandle.Clone();
			break;
		case MsgOneofCase.ExitRoom:
			ExitRoom = other.ExitRoom.Clone();
			break;
		case MsgOneofCase.QueryRoomHandle:
			QueryRoomHandle = other.QueryRoomHandle.Clone();
			break;
		case MsgOneofCase.QueryRoom:
			QueryRoom = other.QueryRoom.Clone();
			break;
		case MsgOneofCase.RefreshRoomStateHandle:
			RefreshRoomStateHandle = other.RefreshRoomStateHandle.Clone();
			break;
		case MsgOneofCase.RefreshRoomState:
			RefreshRoomState = other.RefreshRoomState.Clone();
			break;
		case MsgOneofCase.StartGameHandle:
			StartGameHandle = other.StartGameHandle.Clone();
			break;
		case MsgOneofCase.StartGame:
			StartGame = other.StartGame.Clone();
			break;
		case MsgOneofCase.ThrowDiceHandle:
			ThrowDiceHandle = other.ThrowDiceHandle.Clone();
			break;
		case MsgOneofCase.ThrowDice:
			ThrowDice = other.ThrowDice.Clone();
			break;
		case MsgOneofCase.ChangeRoomHandle:
			ChangeRoomHandle = other.ChangeRoomHandle.Clone();
			break;
		case MsgOneofCase.ChangeRoom:
			ChangeRoom = other.ChangeRoom.Clone();
			break;
		case MsgOneofCase.MoveHandle:
			MoveHandle = other.MoveHandle.Clone();
			break;
		case MsgOneofCase.Move:
			Move = other.Move.Clone();
			break;
		case MsgOneofCase.ShopBuyHandle:
			ShopBuyHandle = other.ShopBuyHandle.Clone();
			break;
		case MsgOneofCase.ShopBuy:
			ShopBuy = other.ShopBuy.Clone();
			break;
		case MsgOneofCase.PursuitHandle:
			PursuitHandle = other.PursuitHandle.Clone();
			break;
		case MsgOneofCase.Pursuit:
			Pursuit = other.Pursuit.Clone();
			break;
		case MsgOneofCase.BattleUseCardHandle:
			BattleUseCardHandle = other.BattleUseCardHandle.Clone();
			break;
		case MsgOneofCase.BattleUseCard:
			BattleUseCard = other.BattleUseCard.Clone();
			break;
		case MsgOneofCase.BattleThrowDiceHandle:
			BattleThrowDiceHandle = other.BattleThrowDiceHandle.Clone();
			break;
		case MsgOneofCase.BattleThrowDice:
			BattleThrowDice = other.BattleThrowDice.Clone();
			break;
		case MsgOneofCase.BattleChoiceHandle:
			BattleChoiceHandle = other.BattleChoiceHandle.Clone();
			break;
		case MsgOneofCase.BattleChoice:
			BattleChoice = other.BattleChoice.Clone();
			break;
		case MsgOneofCase.LotteryChoiceHandle:
			LotteryChoiceHandle = other.LotteryChoiceHandle.Clone();
			break;
		case MsgOneofCase.LotteryChoice:
			LotteryChoice = other.LotteryChoice.Clone();
			break;
		case MsgOneofCase.MoveAgainHandle:
			MoveAgainHandle = other.MoveAgainHandle.Clone();
			break;
		case MsgOneofCase.MoveAgain:
			MoveAgain = other.MoveAgain.Clone();
			break;
		case MsgOneofCase.AskBattleHandle:
			AskBattleHandle = other.AskBattleHandle.Clone();
			break;
		case MsgOneofCase.AskBattle:
			AskBattle = other.AskBattle.Clone();
			break;
		case MsgOneofCase.RollGoldHandle:
			RollGoldHandle = other.RollGoldHandle.Clone();
			break;
		case MsgOneofCase.RollGold:
			RollGold = other.RollGold.Clone();
			break;
		case MsgOneofCase.EventThrowDiceHandle:
			EventThrowDiceHandle = other.EventThrowDiceHandle.Clone();
			break;
		case MsgOneofCase.EventThrowDice:
			EventThrowDice = other.EventThrowDice.Clone();
			break;
		case MsgOneofCase.TriggerEventHandle:
			TriggerEventHandle = other.TriggerEventHandle.Clone();
			break;
		case MsgOneofCase.TriggerEvent:
			TriggerEvent = other.TriggerEvent.Clone();
			break;
		case MsgOneofCase.UseEffectCardHandle:
			UseEffectCardHandle = other.UseEffectCardHandle.Clone();
			break;
		case MsgOneofCase.UseEffectCard:
			UseEffectCard = other.UseEffectCard.Clone();
			break;
		case MsgOneofCase.BombThrowDiceHandle:
			BombThrowDiceHandle = other.BombThrowDiceHandle.Clone();
			break;
		case MsgOneofCase.BombThrowDice:
			BombThrowDice = other.BombThrowDice.Clone();
			break;
		case MsgOneofCase.ChoiceDirectionHandle:
			ChoiceDirectionHandle = other.ChoiceDirectionHandle.Clone();
			break;
		case MsgOneofCase.ChoiceDirection:
			ChoiceDirection = other.ChoiceDirection.Clone();
			break;
		case MsgOneofCase.LandChoiceTargetHandle:
			LandChoiceTargetHandle = other.LandChoiceTargetHandle.Clone();
			break;
		case MsgOneofCase.LandChoiceTarget:
			LandChoiceTarget = other.LandChoiceTarget.Clone();
			break;
		case MsgOneofCase.ThrowDiceResultHandle:
			ThrowDiceResultHandle = other.ThrowDiceResultHandle.Clone();
			break;
		case MsgOneofCase.ThrowDiceResult:
			ThrowDiceResult = other.ThrowDiceResult.Clone();
			break;
		case MsgOneofCase.TriggerDivinationHandle:
			TriggerDivinationHandle = other.TriggerDivinationHandle.Clone();
			break;
		case MsgOneofCase.TriggerDivination:
			TriggerDivination = other.TriggerDivination.Clone();
			break;
		case MsgOneofCase.TriggerDestinyHandle:
			TriggerDestinyHandle = other.TriggerDestinyHandle.Clone();
			break;
		case MsgOneofCase.TriggerDestiny:
			TriggerDestiny = other.TriggerDestiny.Clone();
			break;
		case MsgOneofCase.UseQuickCardHandle:
			UseQuickCardHandle = other.UseQuickCardHandle.Clone();
			break;
		case MsgOneofCase.UseQuickCard:
			UseQuickCard = other.UseQuickCard.Clone();
			break;
		case MsgOneofCase.AbandonCardHandle:
			AbandonCardHandle = other.AbandonCardHandle.Clone();
			break;
		case MsgOneofCase.AbandonCard:
			AbandonCard = other.AbandonCard.Clone();
			break;
		case MsgOneofCase.StopOrContinueHandle:
			StopOrContinueHandle = other.StopOrContinueHandle.Clone();
			break;
		case MsgOneofCase.StopOrContinue:
			StopOrContinue = other.StopOrContinue.Clone();
			break;
		case MsgOneofCase.StartGambleHandle:
			StartGambleHandle = other.StartGambleHandle.Clone();
			break;
		case MsgOneofCase.StartGamble:
			StartGamble = other.StartGamble.Clone();
			break;
		case MsgOneofCase.GambleThrowDicHandle:
			GambleThrowDicHandle = other.GambleThrowDicHandle.Clone();
			break;
		case MsgOneofCase.GambleThrowDic:
			GambleThrowDic = other.GambleThrowDic.Clone();
			break;
		case MsgOneofCase.ChoiceHeroC2S2Handle:
			ChoiceHeroC2S2Handle = other.ChoiceHeroC2S2Handle.Clone();
			break;
		case MsgOneofCase.ChoiceHero2:
			ChoiceHero2 = other.ChoiceHero2.Clone();
			break;
		case MsgOneofCase.AffirmHeroHandle:
			AffirmHeroHandle = other.AffirmHeroHandle.Clone();
			break;
		case MsgOneofCase.AffirmHero:
			AffirmHero = other.AffirmHero.Clone();
			break;
		case MsgOneofCase.SearchRoomHandle:
			SearchRoomHandle = other.SearchRoomHandle.Clone();
			break;
		case MsgOneofCase.SearchRoom:
			SearchRoom = other.SearchRoom.Clone();
			break;
		case MsgOneofCase.GmHandle:
			GmHandle = other.GmHandle.Clone();
			break;
		case MsgOneofCase.Gm:
			Gm = other.Gm.Clone();
			break;
		case MsgOneofCase.TriggerHospitalHandle:
			TriggerHospitalHandle = other.TriggerHospitalHandle.Clone();
			break;
		case MsgOneofCase.TriggerHospital:
			TriggerHospital = other.TriggerHospital.Clone();
			break;
		case MsgOneofCase.SendChatHandle:
			SendChatHandle = other.SendChatHandle.Clone();
			break;
		case MsgOneofCase.SendChat:
			SendChat = other.SendChat.Clone();
			break;
		case MsgOneofCase.PlayerShopBuyC2S:
			PlayerShopBuyC2S = other.PlayerShopBuyC2S.Clone();
			break;
		case MsgOneofCase.PlayerShopBuyS2C:
			PlayerShopBuyS2C = other.PlayerShopBuyS2C.Clone();
			break;
		case MsgOneofCase.PlayerUseItemHandle:
			PlayerUseItemHandle = other.PlayerUseItemHandle.Clone();
			break;
		case MsgOneofCase.PlayerUseItem:
			PlayerUseItem = other.PlayerUseItem.Clone();
			break;
		case MsgOneofCase.UseTreasureC2S:
			UseTreasureC2S = other.UseTreasureC2S.Clone();
			break;
		case MsgOneofCase.UseTreasureS2C:
			UseTreasureS2C = other.UseTreasureS2C.Clone();
			break;
		case MsgOneofCase.UseTreasureAutoTransformC2S:
			UseTreasureAutoTransformC2S = other.UseTreasureAutoTransformC2S.Clone();
			break;
		case MsgOneofCase.UseTreasureAutoTransformS2C:
			UseTreasureAutoTransformS2C = other.UseTreasureAutoTransformS2C.Clone();
			break;
		case MsgOneofCase.SetFashionC2S:
			SetFashionC2S = other.SetFashionC2S.Clone();
			break;
		case MsgOneofCase.SetFashionS2C:
			SetFashionS2C = other.SetFashionS2C.Clone();
			break;
		case MsgOneofCase.SelectFashionPlanC2S:
			SelectFashionPlanC2S = other.SelectFashionPlanC2S.Clone();
			break;
		case MsgOneofCase.SelectFashionPlanS2C:
			SelectFashionPlanS2C = other.SelectFashionPlanS2C.Clone();
			break;
		case MsgOneofCase.GachaC2S:
			GachaC2S = other.GachaC2S.Clone();
			break;
		case MsgOneofCase.GachaS2C:
			GachaS2C = other.GachaS2C.Clone();
			break;
		case MsgOneofCase.SteamSearchRoomC2S:
			SteamSearchRoomC2S = other.SteamSearchRoomC2S.Clone();
			break;
		case MsgOneofCase.SteamSearchRoomS2C:
			SteamSearchRoomS2C = other.SteamSearchRoomS2C.Clone();
			break;
		case MsgOneofCase.CheatItemHandle:
			CheatItemHandle = other.CheatItemHandle.Clone();
			break;
		case MsgOneofCase.CheatItem:
			CheatItem = other.CheatItem.Clone();
			break;
		case MsgOneofCase.RoleCardUpLvC2S:
			RoleCardUpLvC2S = other.RoleCardUpLvC2S.Clone();
			break;
		case MsgOneofCase.RoleCardUpLvS2C:
			RoleCardUpLvS2C = other.RoleCardUpLvS2C.Clone();
			break;
		case MsgOneofCase.RoleCardBreakThroughC2S:
			RoleCardBreakThroughC2S = other.RoleCardBreakThroughC2S.Clone();
			break;
		case MsgOneofCase.RoleCardBreakThroughS2C:
			RoleCardBreakThroughS2C = other.RoleCardBreakThroughS2C.Clone();
			break;
		case MsgOneofCase.RoleCardChoiceResC2S:
			RoleCardChoiceResC2S = other.RoleCardChoiceResC2S.Clone();
			break;
		case MsgOneofCase.RoleCardChoiceResS2C:
			RoleCardChoiceResS2C = other.RoleCardChoiceResS2C.Clone();
			break;
		case MsgOneofCase.TaskRewardC2S:
			TaskRewardC2S = other.TaskRewardC2S.Clone();
			break;
		case MsgOneofCase.TaskRewardS2C:
			TaskRewardS2C = other.TaskRewardS2C.Clone();
			break;
		case MsgOneofCase.TeachingC2S:
			TeachingC2S = other.TeachingC2S.Clone();
			break;
		case MsgOneofCase.TeachingS2C:
			TeachingS2C = other.TeachingS2C.Clone();
			break;
		case MsgOneofCase.QuickJoinRoomC2S:
			QuickJoinRoomC2S = other.QuickJoinRoomC2S.Clone();
			break;
		case MsgOneofCase.QuickJoinRoomS2C:
			QuickJoinRoomS2C = other.QuickJoinRoomS2C.Clone();
			break;
		case MsgOneofCase.RoomKickPlayerC2S:
			RoomKickPlayerC2S = other.RoomKickPlayerC2S.Clone();
			break;
		case MsgOneofCase.RoomKickPlayerS2C:
			RoomKickPlayerS2C = other.RoomKickPlayerS2C.Clone();
			break;
		case MsgOneofCase.RoomAbdicationC2S:
			RoomAbdicationC2S = other.RoomAbdicationC2S.Clone();
			break;
		case MsgOneofCase.RoomAbdicationS2C:
			RoomAbdicationS2C = other.RoomAbdicationS2C.Clone();
			break;
		case MsgOneofCase.RoomReadyC2S:
			RoomReadyC2S = other.RoomReadyC2S.Clone();
			break;
		case MsgOneofCase.RoomReadyS2C:
			RoomReadyS2C = other.RoomReadyS2C.Clone();
			break;
		case MsgOneofCase.ChargeCreateC2S:
			ChargeCreateC2S = other.ChargeCreateC2S.Clone();
			break;
		case MsgOneofCase.ChargeCreateS2C:
			ChargeCreateS2C = other.ChargeCreateS2C.Clone();
			break;
		case MsgOneofCase.ChargeC2S:
			ChargeC2S = other.ChargeC2S.Clone();
			break;
		case MsgOneofCase.ChargeS2C:
			ChargeS2C = other.ChargeS2C.Clone();
			break;
		case MsgOneofCase.GiftCdkC2S:
			GiftCdkC2S = other.GiftCdkC2S.Clone();
			break;
		case MsgOneofCase.GiftCdkS2C:
			GiftCdkS2C = other.GiftCdkS2C.Clone();
			break;
		case MsgOneofCase.MailReadC2S:
			MailReadC2S = other.MailReadC2S.Clone();
			break;
		case MsgOneofCase.MailReadS2C:
			MailReadS2C = other.MailReadS2C.Clone();
			break;
		case MsgOneofCase.MailGetRewardC2S:
			MailGetRewardC2S = other.MailGetRewardC2S.Clone();
			break;
		case MsgOneofCase.MailGetRewardS2C:
			MailGetRewardS2C = other.MailGetRewardS2C.Clone();
			break;
		case MsgOneofCase.MailDelReadC2S:
			MailDelReadC2S = other.MailDelReadC2S.Clone();
			break;
		case MsgOneofCase.MailDelReadS2C:
			MailDelReadS2C = other.MailDelReadS2C.Clone();
			break;
		case MsgOneofCase.GachaRecordC2S:
			GachaRecordC2S = other.GachaRecordC2S.Clone();
			break;
		case MsgOneofCase.GachaRecordS2C:
			GachaRecordS2C = other.GachaRecordS2C.Clone();
			break;
		case MsgOneofCase.ActivityTaskRewardC2S:
			ActivityTaskRewardC2S = other.ActivityTaskRewardC2S.Clone();
			break;
		case MsgOneofCase.ActivityTaskRewardS2C:
			ActivityTaskRewardS2C = other.ActivityTaskRewardS2C.Clone();
			break;
		case MsgOneofCase.RoomShortChatC2S:
			RoomShortChatC2S = other.RoomShortChatC2S.Clone();
			break;
		case MsgOneofCase.RoomShortChatS2C:
			RoomShortChatS2C = other.RoomShortChatS2C.Clone();
			break;
		case MsgOneofCase.SetShowPlayerC2S:
			SetShowPlayerC2S = other.SetShowPlayerC2S.Clone();
			break;
		case MsgOneofCase.SetShowPlayerS2C:
			SetShowPlayerS2C = other.SetShowPlayerS2C.Clone();
			break;
		case MsgOneofCase.GetShowPlayerC2S:
			GetShowPlayerC2S = other.GetShowPlayerC2S.Clone();
			break;
		case MsgOneofCase.GetShowPlayerS2C:
			GetShowPlayerS2C = other.GetShowPlayerS2C.Clone();
			break;
		case MsgOneofCase.GetPlayerFightRecordC2S:
			GetPlayerFightRecordC2S = other.GetPlayerFightRecordC2S.Clone();
			break;
		case MsgOneofCase.GetPlayerFightRecordS2C:
			GetPlayerFightRecordS2C = other.GetPlayerFightRecordS2C.Clone();
			break;
		case MsgOneofCase.GetDay7RewardC2S:
			GetDay7RewardC2S = other.GetDay7RewardC2S.Clone();
			break;
		case MsgOneofCase.GetDay7RewardS2C:
			GetDay7RewardS2C = other.GetDay7RewardS2C.Clone();
			break;
		case MsgOneofCase.PraisePlayerC2S:
			PraisePlayerC2S = other.PraisePlayerC2S.Clone();
			break;
		case MsgOneofCase.PraisePlayerS2C:
			PraisePlayerS2C = other.PraisePlayerS2C.Clone();
			break;
		case MsgOneofCase.ClientDataUploadC2S:
			ClientDataUploadC2S = other.ClientDataUploadC2S.Clone();
			break;
		case MsgOneofCase.ClientDataUploadS2C:
			ClientDataUploadS2C = other.ClientDataUploadS2C.Clone();
			break;
		case MsgOneofCase.FriendListC2S:
			FriendListC2S = other.FriendListC2S.Clone();
			break;
		case MsgOneofCase.FriendListS2C:
			FriendListS2C = other.FriendListS2C.Clone();
			break;
		case MsgOneofCase.FriendApplyC2S:
			FriendApplyC2S = other.FriendApplyC2S.Clone();
			break;
		case MsgOneofCase.FriendApplyS2C:
			FriendApplyS2C = other.FriendApplyS2C.Clone();
			break;
		case MsgOneofCase.FriendApplyListC2S:
			FriendApplyListC2S = other.FriendApplyListC2S.Clone();
			break;
		case MsgOneofCase.FriendApplyListS2C:
			FriendApplyListS2C = other.FriendApplyListS2C.Clone();
			break;
		case MsgOneofCase.FriendApplyOpC2S:
			FriendApplyOpC2S = other.FriendApplyOpC2S.Clone();
			break;
		case MsgOneofCase.FriendApplyOpS2C:
			FriendApplyOpS2C = other.FriendApplyOpS2C.Clone();
			break;
		case MsgOneofCase.FriendOpC2S:
			FriendOpC2S = other.FriendOpC2S.Clone();
			break;
		case MsgOneofCase.FriendOpS2C:
			FriendOpS2C = other.FriendOpS2C.Clone();
			break;
		case MsgOneofCase.FriendInviteC2S:
			FriendInviteC2S = other.FriendInviteC2S.Clone();
			break;
		case MsgOneofCase.FriendInviteS2C:
			FriendInviteS2C = other.FriendInviteS2C.Clone();
			break;
		case MsgOneofCase.FriendInviteListC2S:
			FriendInviteListC2S = other.FriendInviteListC2S.Clone();
			break;
		case MsgOneofCase.FriendInviteListS2C:
			FriendInviteListS2C = other.FriendInviteListS2C.Clone();
			break;
		case MsgOneofCase.FriendInviteCleanC2S:
			FriendInviteCleanC2S = other.FriendInviteCleanC2S.Clone();
			break;
		case MsgOneofCase.FriendInviteCleanS2C:
			FriendInviteCleanS2C = other.FriendInviteCleanS2C.Clone();
			break;
		case MsgOneofCase.FriendBlacksListC2S:
			FriendBlacksListC2S = other.FriendBlacksListC2S.Clone();
			break;
		case MsgOneofCase.FriendBlacksListS2C:
			FriendBlacksListS2C = other.FriendBlacksListS2C.Clone();
			break;
		case MsgOneofCase.NearFightPlayerC2S:
			NearFightPlayerC2S = other.NearFightPlayerC2S.Clone();
			break;
		case MsgOneofCase.NearFightPlayerS2C:
			NearFightPlayerS2C = other.NearFightPlayerS2C.Clone();
			break;
		case MsgOneofCase.SearchPlayerC2S:
			SearchPlayerC2S = other.SearchPlayerC2S.Clone();
			break;
		case MsgOneofCase.SearchPlayerS2C:
			SearchPlayerS2C = other.SearchPlayerS2C.Clone();
			break;
		case MsgOneofCase.ScratchCardC2S:
			ScratchCardC2S = other.ScratchCardC2S.Clone();
			break;
		case MsgOneofCase.ScratchCardS2C:
			ScratchCardS2C = other.ScratchCardS2C.Clone();
			break;
		case MsgOneofCase.NextScratchCardPoolC2S:
			NextScratchCardPoolC2S = other.NextScratchCardPoolC2S.Clone();
			break;
		case MsgOneofCase.NextScratchCardPoolS2C:
			NextScratchCardPoolS2C = other.NextScratchCardPoolS2C.Clone();
			break;
		case MsgOneofCase.WatchJoinRoomC2S:
			WatchJoinRoomC2S = other.WatchJoinRoomC2S.Clone();
			break;
		case MsgOneofCase.WatchJoinRoomS2C:
			WatchJoinRoomS2C = other.WatchJoinRoomS2C.Clone();
			break;
		case MsgOneofCase.WatchRefreshRoomStateC2S:
			WatchRefreshRoomStateC2S = other.WatchRefreshRoomStateC2S.Clone();
			break;
		case MsgOneofCase.WatchRefreshRoomStateS2C:
			WatchRefreshRoomStateS2C = other.WatchRefreshRoomStateS2C.Clone();
			break;
		case MsgOneofCase.WatchExitRoomC2S:
			WatchExitRoomC2S = other.WatchExitRoomC2S.Clone();
			break;
		case MsgOneofCase.WatchExitRoomS2C:
			WatchExitRoomS2C = other.WatchExitRoomS2C.Clone();
			break;
		case MsgOneofCase.BattlePassGetRewardC2S:
			BattlePassGetRewardC2S = other.BattlePassGetRewardC2S.Clone();
			break;
		case MsgOneofCase.BattlePassGetRewardS2C:
			BattlePassGetRewardS2C = other.BattlePassGetRewardS2C.Clone();
			break;
		case MsgOneofCase.BattlePassTaskRewardC2S:
			BattlePassTaskRewardC2S = other.BattlePassTaskRewardC2S.Clone();
			break;
		case MsgOneofCase.BattlePassTaskRewardS2C:
			BattlePassTaskRewardS2C = other.BattlePassTaskRewardS2C.Clone();
			break;
		case MsgOneofCase.BattlePassUpLvC2S:
			BattlePassUpLvC2S = other.BattlePassUpLvC2S.Clone();
			break;
		case MsgOneofCase.BattlePassUpLvS2C:
			BattlePassUpLvS2C = other.BattlePassUpLvS2C.Clone();
			break;
		case MsgOneofCase.FriendSendMsgC2S:
			FriendSendMsgC2S = other.FriendSendMsgC2S.Clone();
			break;
		case MsgOneofCase.FriendSendMsgS2C:
			FriendSendMsgS2C = other.FriendSendMsgS2C.Clone();
			break;
		case MsgOneofCase.GetChatMsgC2S:
			GetChatMsgC2S = other.GetChatMsgC2S.Clone();
			break;
		case MsgOneofCase.GetChatMsgS2C:
			GetChatMsgS2C = other.GetChatMsgS2C.Clone();
			break;
		case MsgOneofCase.ReadChatMsgC2S:
			ReadChatMsgC2S = other.ReadChatMsgC2S.Clone();
			break;
		case MsgOneofCase.ReadChatMsgS2C:
			ReadChatMsgS2C = other.ReadChatMsgS2C.Clone();
			break;
		case MsgOneofCase.DelChatMsgInfoC2S:
			DelChatMsgInfoC2S = other.DelChatMsgInfoC2S.Clone();
			break;
		case MsgOneofCase.DelChatMsgInfoS2C:
			DelChatMsgInfoS2C = other.DelChatMsgInfoS2C.Clone();
			break;
		case MsgOneofCase.SelectRelicC2S:
			SelectRelicC2S = other.SelectRelicC2S.Clone();
			break;
		case MsgOneofCase.SelectRelicS2C:
			SelectRelicS2C = other.SelectRelicS2C.Clone();
			break;
		case MsgOneofCase.MonsterPursuitC2S:
			MonsterPursuitC2S = other.MonsterPursuitC2S.Clone();
			break;
		case MsgOneofCase.MonsterPursuitS2C:
			MonsterPursuitS2C = other.MonsterPursuitS2C.Clone();
			break;
		case MsgOneofCase.PVEShopBuyC2S:
			PVEShopBuyC2S = other.PVEShopBuyC2S.Clone();
			break;
		case MsgOneofCase.PVEShopBuyS2C:
			PVEShopBuyS2C = other.PVEShopBuyS2C.Clone();
			break;
		case MsgOneofCase.ClientCheckTaskC2S:
			ClientCheckTaskC2S = other.ClientCheckTaskC2S.Clone();
			break;
		case MsgOneofCase.ClientCheckTaskS2C:
			ClientCheckTaskS2C = other.ClientCheckTaskS2C.Clone();
			break;
		case MsgOneofCase.PveHeroUpLvC2S:
			PveHeroUpLvC2S = other.PveHeroUpLvC2S.Clone();
			break;
		case MsgOneofCase.PveHeroUpLvS2C:
			PveHeroUpLvS2C = other.PveHeroUpLvS2C.Clone();
			break;
		case MsgOneofCase.StartMatchC2S:
			StartMatchC2S = other.StartMatchC2S.Clone();
			break;
		case MsgOneofCase.StartMatchS2C:
			StartMatchS2C = other.StartMatchS2C.Clone();
			break;
		case MsgOneofCase.CancelMatchC2S:
			CancelMatchC2S = other.CancelMatchC2S.Clone();
			break;
		case MsgOneofCase.CancelMatchS2C:
			CancelMatchS2C = other.CancelMatchS2C.Clone();
			break;
		case MsgOneofCase.MatchSuccessC2S:
			MatchSuccessC2S = other.MatchSuccessC2S.Clone();
			break;
		case MsgOneofCase.MatchSuccessS2C:
			MatchSuccessS2C = other.MatchSuccessS2C.Clone();
			break;
		case MsgOneofCase.AccuseC2S:
			AccuseC2S = other.AccuseC2S.Clone();
			break;
		case MsgOneofCase.AccuseS2C:
			AccuseS2C = other.AccuseS2C.Clone();
			break;
		case MsgOneofCase.SingleCampaignC2S:
			SingleCampaignC2S = other.SingleCampaignC2S.Clone();
			break;
		case MsgOneofCase.SingleCampaignS2C:
			SingleCampaignS2C = other.SingleCampaignS2C.Clone();
			break;
		case MsgOneofCase.DevChargeC2S:
			DevChargeC2S = other.DevChargeC2S.Clone();
			break;
		case MsgOneofCase.DevChargeS2C:
			DevChargeS2C = other.DevChargeS2C.Clone();
			break;
		case MsgOneofCase.AskReviveTeammateC2S:
			AskReviveTeammateC2S = other.AskReviveTeammateC2S.Clone();
			break;
		case MsgOneofCase.AskReviveTeammateS2C:
			AskReviveTeammateS2C = other.AskReviveTeammateS2C.Clone();
			break;
		case MsgOneofCase.GetSignInRewardC2S:
			GetSignInRewardC2S = other.GetSignInRewardC2S.Clone();
			break;
		case MsgOneofCase.GetSignInRewardS2C:
			GetSignInRewardS2C = other.GetSignInRewardS2C.Clone();
			break;
		case MsgOneofCase.ChatMapMarkersC2S:
			ChatMapMarkersC2S = other.ChatMapMarkersC2S.Clone();
			break;
		case MsgOneofCase.ChatMapMarkersS2C:
			ChatMapMarkersS2C = other.ChatMapMarkersS2C.Clone();
			break;
		case MsgOneofCase.AbroadCreateOrderC2S:
			AbroadCreateOrderC2S = other.AbroadCreateOrderC2S.Clone();
			break;
		case MsgOneofCase.AbroadCreateOrderS2C:
			AbroadCreateOrderS2C = other.AbroadCreateOrderS2C.Clone();
			break;
		case MsgOneofCase.AgeVerifyC2S:
			AgeVerifyC2S = other.AgeVerifyC2S.Clone();
			break;
		case MsgOneofCase.AgeVerifyS2C:
			AgeVerifyS2C = other.AgeVerifyS2C.Clone();
			break;
		case MsgOneofCase.ChangeNameC2S:
			ChangeNameC2S = other.ChangeNameC2S.Clone();
			break;
		case MsgOneofCase.ChangeNameS2C:
			ChangeNameS2C = other.ChangeNameS2C.Clone();
			break;
		case MsgOneofCase.ClientClickConfirmTaskC2S:
			ClientClickConfirmTaskC2S = other.ClientClickConfirmTaskC2S.Clone();
			break;
		case MsgOneofCase.ClientClickConfirmTaskS2C:
			ClientClickConfirmTaskS2C = other.ClientClickConfirmTaskS2C.Clone();
			break;
		case MsgOneofCase.BuyRelicC2S:
			BuyRelicC2S = other.BuyRelicC2S.Clone();
			break;
		case MsgOneofCase.BuyRelicS2C:
			BuyRelicS2C = other.BuyRelicS2C.Clone();
			break;
		case MsgOneofCase.BuyLightGiftC2S:
			BuyLightGiftC2S = other.BuyLightGiftC2S.Clone();
			break;
		case MsgOneofCase.BuyLightGiftS2C:
			BuyLightGiftS2C = other.BuyLightGiftS2C.Clone();
			break;
		case MsgOneofCase.LightGiftC2S:
			LightGiftC2S = other.LightGiftC2S.Clone();
			break;
		case MsgOneofCase.LightGiftS2C:
			LightGiftS2C = other.LightGiftS2C.Clone();
			break;
		case MsgOneofCase.AcquisitionC2S:
			AcquisitionC2S = other.AcquisitionC2S.Clone();
			break;
		case MsgOneofCase.AcquisitionS2C:
			AcquisitionS2C = other.AcquisitionS2C.Clone();
			break;
		case MsgOneofCase.AcquisitionRewardC2S:
			AcquisitionRewardC2S = other.AcquisitionRewardC2S.Clone();
			break;
		case MsgOneofCase.AcquisitionRewardS2C:
			AcquisitionRewardS2C = other.AcquisitionRewardS2C.Clone();
			break;
		case MsgOneofCase.SelectMechanismC2S:
			SelectMechanismC2S = other.SelectMechanismC2S.Clone();
			break;
		case MsgOneofCase.SelectMechanismS2C:
			SelectMechanismS2C = other.SelectMechanismS2C.Clone();
			break;
		case MsgOneofCase.GachaCountRewardC2S:
			GachaCountRewardC2S = other.GachaCountRewardC2S.Clone();
			break;
		case MsgOneofCase.GachaCountRewardS2C:
			GachaCountRewardS2C = other.GachaCountRewardS2C.Clone();
			break;
		case MsgOneofCase.GetPlayerSimpleC2S:
			GetPlayerSimpleC2S = other.GetPlayerSimpleC2S.Clone();
			break;
		case MsgOneofCase.GetPlayerSimpleS2C:
			GetPlayerSimpleS2C = other.GetPlayerSimpleS2C.Clone();
			break;
		case MsgOneofCase.RoleCardCollectC2S:
			RoleCardCollectC2S = other.RoleCardCollectC2S.Clone();
			break;
		case MsgOneofCase.RoleCardCollectS2C:
			RoleCardCollectS2C = other.RoleCardCollectS2C.Clone();
			break;
		case MsgOneofCase.GMPlayerSettingC2S:
			GMPlayerSettingC2S = other.GMPlayerSettingC2S.Clone();
			break;
		case MsgOneofCase.GMPlayerSettingS2C:
			GMPlayerSettingS2C = other.GMPlayerSettingS2C.Clone();
			break;
		case MsgOneofCase.SetFriendNoteC2S:
			SetFriendNoteC2S = other.SetFriendNoteC2S.Clone();
			break;
		case MsgOneofCase.SetFriendNoteS2C:
			SetFriendNoteS2C = other.SetFriendNoteS2C.Clone();
			break;
		case MsgOneofCase.SetOnlineStatusC2S:
			SetOnlineStatusC2S = other.SetOnlineStatusC2S.Clone();
			break;
		case MsgOneofCase.SetOnlineStatusS2C:
			SetOnlineStatusS2C = other.SetOnlineStatusS2C.Clone();
			break;
		case MsgOneofCase.ChooseSkinC2S:
			ChooseSkinC2S = other.ChooseSkinC2S.Clone();
			break;
		case MsgOneofCase.ChooseSkinS2C:
			ChooseSkinS2C = other.ChooseSkinS2C.Clone();
			break;
		case MsgOneofCase.TimeWastingC2S:
			TimeWastingC2S = other.TimeWastingC2S.Clone();
			break;
		case MsgOneofCase.TimeWastingS2C:
			TimeWastingS2C = other.TimeWastingS2C.Clone();
			break;
		case MsgOneofCase.VoteC2S:
			VoteC2S = other.VoteC2S.Clone();
			break;
		case MsgOneofCase.VoteS2C:
			VoteS2C = other.VoteS2C.Clone();
			break;
		case MsgOneofCase.VoteSelectC2S:
			VoteSelectC2S = other.VoteSelectC2S.Clone();
			break;
		case MsgOneofCase.VoteSelectS2C:
			VoteSelectS2C = other.VoteSelectS2C.Clone();
			break;
		case MsgOneofCase.NotifyStoryC2S:
			NotifyStoryC2S = other.NotifyStoryC2S.Clone();
			break;
		case MsgOneofCase.NotifyStoryS2C:
			NotifyStoryS2C = other.NotifyStoryS2C.Clone();
			break;
		case MsgOneofCase.PveHeroTalentUpC2S:
			PveHeroTalentUpC2S = other.PveHeroTalentUpC2S.Clone();
			break;
		case MsgOneofCase.PveHeroTalentUpS2C:
			PveHeroTalentUpS2C = other.PveHeroTalentUpS2C.Clone();
			break;
		case MsgOneofCase.SelectEventC2S:
			SelectEventC2S = other.SelectEventC2S.Clone();
			break;
		case MsgOneofCase.SelectEventS2C:
			SelectEventS2C = other.SelectEventS2C.Clone();
			break;
		case MsgOneofCase.CampScoreC2S:
			CampScoreC2S = other.CampScoreC2S.Clone();
			break;
		case MsgOneofCase.CampScoreS2C:
			CampScoreS2C = other.CampScoreS2C.Clone();
			break;
		case MsgOneofCase.ActivityMissionRewardC2S:
			ActivityMissionRewardC2S = other.ActivityMissionRewardC2S.Clone();
			break;
		case MsgOneofCase.ActivityMissionRewardS2C:
			ActivityMissionRewardS2C = other.ActivityMissionRewardS2C.Clone();
			break;
		case MsgOneofCase.VendorBuyCardC2S:
			VendorBuyCardC2S = other.VendorBuyCardC2S.Clone();
			break;
		case MsgOneofCase.VendorBuyCardS2C:
			VendorBuyCardS2C = other.VendorBuyCardS2C.Clone();
			break;
		case MsgOneofCase.TransferStarDiscC2S:
			TransferStarDiscC2S = other.TransferStarDiscC2S.Clone();
			break;
		case MsgOneofCase.TransferStarDiscS2C:
			TransferStarDiscS2C = other.TransferStarDiscS2C.Clone();
			break;
		case MsgOneofCase.GetHeroInfoC2S:
			GetHeroInfoC2S = other.GetHeroInfoC2S.Clone();
			break;
		case MsgOneofCase.GetHeroInfoS2C:
			GetHeroInfoS2C = other.GetHeroInfoS2C.Clone();
			break;
		case MsgOneofCase.CreateMatchTeamC2S:
			CreateMatchTeamC2S = other.CreateMatchTeamC2S.Clone();
			break;
		case MsgOneofCase.CreateMatchTeamS2C:
			CreateMatchTeamS2C = other.CreateMatchTeamS2C.Clone();
			break;
		case MsgOneofCase.ChangeMatchTeamC2S:
			ChangeMatchTeamC2S = other.ChangeMatchTeamC2S.Clone();
			break;
		case MsgOneofCase.ChangeMatchTeamS2C:
			ChangeMatchTeamS2C = other.ChangeMatchTeamS2C.Clone();
			break;
		case MsgOneofCase.JoinMatchTeamC2S:
			JoinMatchTeamC2S = other.JoinMatchTeamC2S.Clone();
			break;
		case MsgOneofCase.JoinMatchTeamS2C:
			JoinMatchTeamS2C = other.JoinMatchTeamS2C.Clone();
			break;
		case MsgOneofCase.ExitMatchTeamC2S:
			ExitMatchTeamC2S = other.ExitMatchTeamC2S.Clone();
			break;
		case MsgOneofCase.ExitMatchTeamS2C:
			ExitMatchTeamS2C = other.ExitMatchTeamS2C.Clone();
			break;
		case MsgOneofCase.RefreshMatchTeamInfoC2S:
			RefreshMatchTeamInfoC2S = other.RefreshMatchTeamInfoC2S.Clone();
			break;
		case MsgOneofCase.RefreshMatchTeamInfoS2C:
			RefreshMatchTeamInfoS2C = other.RefreshMatchTeamInfoS2C.Clone();
			break;
		case MsgOneofCase.ChinaCreateOrderC2S:
			ChinaCreateOrderC2S = other.ChinaCreateOrderC2S.Clone();
			break;
		case MsgOneofCase.ChinaCreateOrderS2C:
			ChinaCreateOrderS2C = other.ChinaCreateOrderS2C.Clone();
			break;
		case MsgOneofCase.MatchTeamInviteC2S:
			MatchTeamInviteC2S = other.MatchTeamInviteC2S.Clone();
			break;
		case MsgOneofCase.MatchTeamInviteS2C:
			MatchTeamInviteS2C = other.MatchTeamInviteS2C.Clone();
			break;
		case MsgOneofCase.MatchTeamChatC2S:
			MatchTeamChatC2S = other.MatchTeamChatC2S.Clone();
			break;
		case MsgOneofCase.MatchTeamChatS2C:
			MatchTeamChatS2C = other.MatchTeamChatS2C.Clone();
			break;
		case MsgOneofCase.MatchTeamReadyC2S:
			MatchTeamReadyC2S = other.MatchTeamReadyC2S.Clone();
			break;
		case MsgOneofCase.MatchTeamReadyS2C:
			MatchTeamReadyS2C = other.MatchTeamReadyS2C.Clone();
			break;
		case MsgOneofCase.PlayerChatC2S:
			PlayerChatC2S = other.PlayerChatC2S.Clone();
			break;
		case MsgOneofCase.PlayerChatS2C:
			PlayerChatS2C = other.PlayerChatS2C.Clone();
			break;
		case MsgOneofCase.SyncSingleGameDataC2S:
			SyncSingleGameDataC2S = other.SyncSingleGameDataC2S.Clone();
			break;
		case MsgOneofCase.SyncSingleGameDataS2C:
			SyncSingleGameDataS2C = other.SyncSingleGameDataS2C.Clone();
			break;
		case MsgOneofCase.SingleGameDataC2S:
			SingleGameDataC2S = other.SingleGameDataC2S.Clone();
			break;
		case MsgOneofCase.SingleGameDataS2C:
			SingleGameDataS2C = other.SingleGameDataS2C.Clone();
			break;
		case MsgOneofCase.GetActivityPassRewardC2S:
			GetActivityPassRewardC2S = other.GetActivityPassRewardC2S.Clone();
			break;
		case MsgOneofCase.GetActivityPassRewardS2C:
			GetActivityPassRewardS2C = other.GetActivityPassRewardS2C.Clone();
			break;
		case MsgOneofCase.ApplyChangeSlotC2S:
			ApplyChangeSlotC2S = other.ApplyChangeSlotC2S.Clone();
			break;
		case MsgOneofCase.ApplyChangeSlotS2C:
			ApplyChangeSlotS2C = other.ApplyChangeSlotS2C.Clone();
			break;
		case MsgOneofCase.OpsChangeSlotC2S:
			OpsChangeSlotC2S = other.OpsChangeSlotC2S.Clone();
			break;
		case MsgOneofCase.OpsChangeSlotS2C:
			OpsChangeSlotS2C = other.OpsChangeSlotS2C.Clone();
			break;
		case MsgOneofCase.RookieGachaRewardC2S:
			RookieGachaRewardC2S = other.RookieGachaRewardC2S.Clone();
			break;
		case MsgOneofCase.RookieGachaRewardS2C:
			RookieGachaRewardS2C = other.RookieGachaRewardS2C.Clone();
			break;
		case MsgOneofCase.LiveGiftPackageC2S:
			LiveGiftPackageC2S = other.LiveGiftPackageC2S.Clone();
			break;
		case MsgOneofCase.LiveGiftPackageS2C:
			LiveGiftPackageS2C = other.LiveGiftPackageS2C.Clone();
			break;
		case MsgOneofCase.LaborActDiceC2S:
			LaborActDiceC2S = other.LaborActDiceC2S.Clone();
			break;
		case MsgOneofCase.LaborActDiceS2C:
			LaborActDiceS2C = other.LaborActDiceS2C.Clone();
			break;
		case MsgOneofCase.ActionOverTimeLogC2S:
			ActionOverTimeLogC2S = other.ActionOverTimeLogC2S.Clone();
			break;
		case MsgOneofCase.ActionOverTimeLogS2C:
			ActionOverTimeLogS2C = other.ActionOverTimeLogS2C.Clone();
			break;
		case MsgOneofCase.ClientHarmonyC2S:
			ClientHarmonyC2S = other.ClientHarmonyC2S.Clone();
			break;
		case MsgOneofCase.ClientHarmonyS2C:
			ClientHarmonyS2C = other.ClientHarmonyS2C.Clone();
			break;
		case MsgOneofCase.MailStarC2S:
			MailStarC2S = other.MailStarC2S.Clone();
			break;
		case MsgOneofCase.MailStarS2C:
			MailStarS2C = other.MailStarS2C.Clone();
			break;
		case MsgOneofCase.SetCardAltArtC2S:
			SetCardAltArtC2S = other.SetCardAltArtC2S.Clone();
			break;
		case MsgOneofCase.SetCardAltArtS2C:
			SetCardAltArtS2C = other.SetCardAltArtS2C.Clone();
			break;
		case MsgOneofCase.SelectRewardCardC2S:
			SelectRewardCardC2S = other.SelectRewardCardC2S.Clone();
			break;
		case MsgOneofCase.SelectRewardCardS2C:
			SelectRewardCardS2C = other.SelectRewardCardS2C.Clone();
			break;
		case MsgOneofCase.GetQuestionUrlC2S:
			GetQuestionUrlC2S = other.GetQuestionUrlC2S.Clone();
			break;
		case MsgOneofCase.GetQuestionUrlS2C:
			GetQuestionUrlS2C = other.GetQuestionUrlS2C.Clone();
			break;
		case MsgOneofCase.GetReturnInfoC2S:
			GetReturnInfoC2S = other.GetReturnInfoC2S.Clone();
			break;
		case MsgOneofCase.GetReturnInfoS2C:
			GetReturnInfoS2C = other.GetReturnInfoS2C.Clone();
			break;
		case MsgOneofCase.ReturnGiftClaimC2S:
			ReturnGiftClaimC2S = other.ReturnGiftClaimC2S.Clone();
			break;
		case MsgOneofCase.ReturnGiftClaimS2C:
			ReturnGiftClaimS2C = other.ReturnGiftClaimS2C.Clone();
			break;
		case MsgOneofCase.ReturnSignInClaimC2S:
			ReturnSignInClaimC2S = other.ReturnSignInClaimC2S.Clone();
			break;
		case MsgOneofCase.ReturnSignInClaimS2C:
			ReturnSignInClaimS2C = other.ReturnSignInClaimS2C.Clone();
			break;
		case MsgOneofCase.ReturnSurveyFinishC2S:
			ReturnSurveyFinishC2S = other.ReturnSurveyFinishC2S.Clone();
			break;
		case MsgOneofCase.ReturnSurveyFinishS2C:
			ReturnSurveyFinishS2C = other.ReturnSurveyFinishS2C.Clone();
			break;
		case MsgOneofCase.FlipCardC2S:
			FlipCardC2S = other.FlipCardC2S.Clone();
			break;
		case MsgOneofCase.FlipCardS2C:
			FlipCardS2C = other.FlipCardS2C.Clone();
			break;
		case MsgOneofCase.FlipCardProgressRewardC2S:
			FlipCardProgressRewardC2S = other.FlipCardProgressRewardC2S.Clone();
			break;
		case MsgOneofCase.FlipCardProgressRewardS2C:
			FlipCardProgressRewardS2C = other.FlipCardProgressRewardS2C.Clone();
			break;
		case MsgOneofCase.SyncPlayerGuildS2C:
			SyncPlayerGuildS2C = other.SyncPlayerGuildS2C.Clone();
			break;
		case MsgOneofCase.SyncPlayerJoinGuildS2C:
			SyncPlayerJoinGuildS2C = other.SyncPlayerJoinGuildS2C.Clone();
			break;
		case MsgOneofCase.GuildChatMsgS2C:
			GuildChatMsgS2C = other.GuildChatMsgS2C.Clone();
			break;
		case MsgOneofCase.SyncGuildS2C:
			SyncGuildS2C = other.SyncGuildS2C.Clone();
			break;
		case MsgOneofCase.SyncGuildMemberS2C:
			SyncGuildMemberS2C = other.SyncGuildMemberS2C.Clone();
			break;
		case MsgOneofCase.SyncGuildMemberExitS2C:
			SyncGuildMemberExitS2C = other.SyncGuildMemberExitS2C.Clone();
			break;
		case MsgOneofCase.CreateGuildC2S:
			CreateGuildC2S = other.CreateGuildC2S.Clone();
			break;
		case MsgOneofCase.CreateGuildS2C:
			CreateGuildS2C = other.CreateGuildS2C.Clone();
			break;
		case MsgOneofCase.SearchGuildC2S:
			SearchGuildC2S = other.SearchGuildC2S.Clone();
			break;
		case MsgOneofCase.SearchGuildS2C:
			SearchGuildS2C = other.SearchGuildS2C.Clone();
			break;
		case MsgOneofCase.ApplyToGuildC2S:
			ApplyToGuildC2S = other.ApplyToGuildC2S.Clone();
			break;
		case MsgOneofCase.ApplyToGuildS2C:
			ApplyToGuildS2C = other.ApplyToGuildS2C.Clone();
			break;
		case MsgOneofCase.ProcessGuildApplicationC2S:
			ProcessGuildApplicationC2S = other.ProcessGuildApplicationC2S.Clone();
			break;
		case MsgOneofCase.ProcessGuildApplicationS2C:
			ProcessGuildApplicationS2C = other.ProcessGuildApplicationS2C.Clone();
			break;
		case MsgOneofCase.SendGuildInvitationC2S:
			SendGuildInvitationC2S = other.SendGuildInvitationC2S.Clone();
			break;
		case MsgOneofCase.SendGuildInvitationS2C:
			SendGuildInvitationS2C = other.SendGuildInvitationS2C.Clone();
			break;
		case MsgOneofCase.ProcessGuildInvitationC2S:
			ProcessGuildInvitationC2S = other.ProcessGuildInvitationC2S.Clone();
			break;
		case MsgOneofCase.ProcessGuildInvitationS2C:
			ProcessGuildInvitationS2C = other.ProcessGuildInvitationS2C.Clone();
			break;
		case MsgOneofCase.GetGuildInfoC2S:
			GetGuildInfoC2S = other.GetGuildInfoC2S.Clone();
			break;
		case MsgOneofCase.GetGuildInfoS2C:
			GetGuildInfoS2C = other.GetGuildInfoS2C.Clone();
			break;
		case MsgOneofCase.UpdateGuildSettingsC2S:
			UpdateGuildSettingsC2S = other.UpdateGuildSettingsC2S.Clone();
			break;
		case MsgOneofCase.UpdateGuildSettingsS2C:
			UpdateGuildSettingsS2C = other.UpdateGuildSettingsS2C.Clone();
			break;
		case MsgOneofCase.UpdateGuildInAnnouncementC2S:
			UpdateGuildInAnnouncementC2S = other.UpdateGuildInAnnouncementC2S.Clone();
			break;
		case MsgOneofCase.UpdateGuildInAnnouncementS2C:
			UpdateGuildInAnnouncementS2C = other.UpdateGuildInAnnouncementS2C.Clone();
			break;
		case MsgOneofCase.TransferGuildMasterC2S:
			TransferGuildMasterC2S = other.TransferGuildMasterC2S.Clone();
			break;
		case MsgOneofCase.TransferGuildMasterS2C:
			TransferGuildMasterS2C = other.TransferGuildMasterS2C.Clone();
			break;
		case MsgOneofCase.ChangeGuildMemberTitleC2S:
			ChangeGuildMemberTitleC2S = other.ChangeGuildMemberTitleC2S.Clone();
			break;
		case MsgOneofCase.ChangeGuildMemberTitleS2C:
			ChangeGuildMemberTitleS2C = other.ChangeGuildMemberTitleS2C.Clone();
			break;
		case MsgOneofCase.KickGuildMemberC2S:
			KickGuildMemberC2S = other.KickGuildMemberC2S.Clone();
			break;
		case MsgOneofCase.KickGuildMemberS2C:
			KickGuildMemberS2C = other.KickGuildMemberS2C.Clone();
			break;
		case MsgOneofCase.ImpeachGuildMasterC2S:
			ImpeachGuildMasterC2S = other.ImpeachGuildMasterC2S.Clone();
			break;
		case MsgOneofCase.ImpeachGuildMasterS2C:
			ImpeachGuildMasterS2C = other.ImpeachGuildMasterS2C.Clone();
			break;
		case MsgOneofCase.ExitGuildC2S:
			ExitGuildC2S = other.ExitGuildC2S.Clone();
			break;
		case MsgOneofCase.ExitGuildS2C:
			ExitGuildS2C = other.ExitGuildS2C.Clone();
			break;
		case MsgOneofCase.DisbandGuildC2S:
			DisbandGuildC2S = other.DisbandGuildC2S.Clone();
			break;
		case MsgOneofCase.DisbandGuildS2C:
			DisbandGuildS2C = other.DisbandGuildS2C.Clone();
			break;
		case MsgOneofCase.GuildMissionRewardC2S:
			GuildMissionRewardC2S = other.GuildMissionRewardC2S.Clone();
			break;
		case MsgOneofCase.GuildMissionRewardS2C:
			GuildMissionRewardS2C = other.GuildMissionRewardS2C.Clone();
			break;
		case MsgOneofCase.GetGuildMemberChangeMsgC2S:
			GetGuildMemberChangeMsgC2S = other.GetGuildMemberChangeMsgC2S.Clone();
			break;
		case MsgOneofCase.GetGuildMemberChangeMsgS2C:
			GetGuildMemberChangeMsgS2C = other.GetGuildMemberChangeMsgS2C.Clone();
			break;
		case MsgOneofCase.SendGuildChatMsgC2S:
			SendGuildChatMsgC2S = other.SendGuildChatMsgC2S.Clone();
			break;
		case MsgOneofCase.SendGuildChatMsgS2C:
			SendGuildChatMsgS2C = other.SendGuildChatMsgS2C.Clone();
			break;
		case MsgOneofCase.GetGuildChatMsgC2S:
			GetGuildChatMsgC2S = other.GetGuildChatMsgC2S.Clone();
			break;
		case MsgOneofCase.GetGuildChatMsgS2C:
			GetGuildChatMsgS2C = other.GetGuildChatMsgS2C.Clone();
			break;
		case MsgOneofCase.GuildMemberC2S:
			GuildMemberC2S = other.GuildMemberC2S.Clone();
			break;
		case MsgOneofCase.GuildMemberS2C:
			GuildMemberS2C = other.GuildMemberS2C.Clone();
			break;
		case MsgOneofCase.GetGuildsInfoC2S:
			GetGuildsInfoC2S = other.GetGuildsInfoC2S.Clone();
			break;
		case MsgOneofCase.GetGuildsInfoS2C:
			GetGuildsInfoS2C = other.GetGuildsInfoS2C.Clone();
			break;
		case MsgOneofCase.TestRpcEchoC2S:
			TestRpcEchoC2S = other.TestRpcEchoC2S.Clone();
			break;
		case MsgOneofCase.TestRpcEchoS2C:
			TestRpcEchoS2C = other.TestRpcEchoS2C.Clone();
			break;
		}
		_unknownFields = UnknownFieldSet.Clone(other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public protocol Clone()
	{
		return new protocol(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void ClearMsg()
	{
		msgCase_ = MsgOneofCase.None;
		msg_ = null;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override bool Equals(object other)
	{
		return Equals(other as protocol);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public bool Equals(protocol other)
	{
		if (other == null)
		{
			return false;
		}
		if (other == this)
		{
			return true;
		}
		if (!object.Equals(SysSendMailC2S, other.SysSendMailC2S))
		{
			return false;
		}
		if (!object.Equals(SysPlayerOnlineC2S, other.SysPlayerOnlineC2S))
		{
			return false;
		}
		if (!object.Equals(PlayerOnlineRoomC2S, other.PlayerOnlineRoomC2S))
		{
			return false;
		}
		if (!object.Equals(SysCanPraiseInfoC2S, other.SysCanPraiseInfoC2S))
		{
			return false;
		}
		if (!object.Equals(SysPraiseC2S, other.SysPraiseC2S))
		{
			return false;
		}
		if (!object.Equals(SysRoomFinishC2S, other.SysRoomFinishC2S))
		{
			return false;
		}
		if (!object.Equals(SysRoomAddExpC2S, other.SysRoomAddExpC2S))
		{
			return false;
		}
		if (!object.Equals(SysGetShowFriendC2S, other.SysGetShowFriendC2S))
		{
			return false;
		}
		if (!object.Equals(SysFriendInviteC2S, other.SysFriendInviteC2S))
		{
			return false;
		}
		if (!object.Equals(SysFriendDelC2S, other.SysFriendDelC2S))
		{
			return false;
		}
		if (!object.Equals(SysFriendInfoC2S, other.SysFriendInfoC2S))
		{
			return false;
		}
		if (!object.Equals(SysFriendApplyC2S, other.SysFriendApplyC2S))
		{
			return false;
		}
		if (!object.Equals(SysFriendAddC2S, other.SysFriendAddC2S))
		{
			return false;
		}
		if (!object.Equals(SysFriendSendMsgC2S, other.SysFriendSendMsgC2S))
		{
			return false;
		}
		if (!object.Equals(SysCampaignFinishC2S, other.SysCampaignFinishC2S))
		{
			return false;
		}
		if (!object.Equals(SysAbroadPayMsgC2S, other.SysAbroadPayMsgC2S))
		{
			return false;
		}
		if (!object.Equals(AcquisitionMsgC2S, other.AcquisitionMsgC2S))
		{
			return false;
		}
		if (!object.Equals(SysFightRecord, other.SysFightRecord))
		{
			return false;
		}
		if (!object.Equals(SysCampaignAwardC2S, other.SysCampaignAwardC2S))
		{
			return false;
		}
		if (!object.Equals(SysRecoupItemC2S, other.SysRecoupItemC2S))
		{
			return false;
		}
		if (!object.Equals(SysPlayerOnlineRoomC2S, other.SysPlayerOnlineRoomC2S))
		{
			return false;
		}
		if (!object.Equals(SysPlayerCleanC2S, other.SysPlayerCleanC2S))
		{
			return false;
		}
		if (!object.Equals(SysPlayerPunishmentTimeC2S, other.SysPlayerPunishmentTimeC2S))
		{
			return false;
		}
		if (!object.Equals(SysMatchSuccess, other.SysMatchSuccess))
		{
			return false;
		}
		if (!object.Equals(SysChinaPayMsgC2S, other.SysChinaPayMsgC2S))
		{
			return false;
		}
		if (!object.Equals(SysChangeMatchTeamState, other.SysChangeMatchTeamState))
		{
			return false;
		}
		if (!object.Equals(SysSaveSimplePlayerInfoC2S, other.SysSaveSimplePlayerInfoC2S))
		{
			return false;
		}
		if (!object.Equals(SysGmChangeNameC2S, other.SysGmChangeNameC2S))
		{
			return false;
		}
		if (!object.Equals(SysSyncPlayerC2S, other.SysSyncPlayerC2S))
		{
			return false;
		}
		if (!object.Equals(SysPlayerCreditScoreChangeC2S, other.SysPlayerCreditScoreChangeC2S))
		{
			return false;
		}
		if (!object.Equals(SysSyncPlayerMatchPunishmentTimeC2S, other.SysSyncPlayerMatchPunishmentTimeC2S))
		{
			return false;
		}
		if (!object.Equals(GMChangeCreditScoreC2S, other.GMChangeCreditScoreC2S))
		{
			return false;
		}
		if (!object.Equals(SysPushReturnInfoC2S, other.SysPushReturnInfoC2S))
		{
			return false;
		}
		if (!object.Equals(SysMutePlayerC2S, other.SysMutePlayerC2S))
		{
			return false;
		}
		if (!object.Equals(SysQuestionC2S, other.SysQuestionC2S))
		{
			return false;
		}
		if (!object.Equals(KickS2C, other.KickS2C))
		{
			return false;
		}
		if (!object.Equals(PredictActionS2C, other.PredictActionS2C))
		{
			return false;
		}
		if (!object.Equals(RunningGameS2C, other.RunningGameS2C))
		{
			return false;
		}
		if (!object.Equals(BattleS2C, other.BattleS2C))
		{
			return false;
		}
		if (!object.Equals(LotteryDrawS2C, other.LotteryDrawS2C))
		{
			return false;
		}
		if (!object.Equals(LandBuffsS2C, other.LandBuffsS2C))
		{
			return false;
		}
		if (!object.Equals(RoundStartS2C, other.RoundStartS2C))
		{
			return false;
		}
		if (!object.Equals(GameFinishS2C, other.GameFinishS2C))
		{
			return false;
		}
		if (!object.Equals(MonsterRefreshS2C, other.MonsterRefreshS2C))
		{
			return false;
		}
		if (!object.Equals(MovePointBuffS2C, other.MovePointBuffS2C))
		{
			return false;
		}
		if (!object.Equals(ChangeDirS2C, other.ChangeDirS2C))
		{
			return false;
		}
		if (!object.Equals(GambleChangeS2C, other.GambleChangeS2C))
		{
			return false;
		}
		if (!object.Equals(HeroBarBoxChangeS2C, other.HeroBarBoxChangeS2C))
		{
			return false;
		}
		if (!object.Equals(RoomNotifyS2C, other.RoomNotifyS2C))
		{
			return false;
		}
		if (!object.Equals(ActionStartNotifyS2C, other.ActionStartNotifyS2C))
		{
			return false;
		}
		if (!object.Equals(NoGambleNotifyS2C, other.NoGambleNotifyS2C))
		{
			return false;
		}
		if (!object.Equals(GambleObServeS2C, other.GambleObServeS2C))
		{
			return false;
		}
		if (!object.Equals(UpdateHeroAttrS2C, other.UpdateHeroAttrS2C))
		{
			return false;
		}
		if (!object.Equals(ChangePlayerSlotS2C, other.ChangePlayerSlotS2C))
		{
			return false;
		}
		if (!object.Equals(BossSleepS2C, other.BossSleepS2C))
		{
			return false;
		}
		if (!object.Equals(RefMallS2C, other.RefMallS2C))
		{
			return false;
		}
		if (!object.Equals(BagItemChangeS2C, other.BagItemChangeS2C))
		{
			return false;
		}
		if (!object.Equals(RoleCardChangeS2C, other.RoleCardChangeS2C))
		{
			return false;
		}
		if (!object.Equals(TaskConditionS2C, other.TaskConditionS2C))
		{
			return false;
		}
		if (!object.Equals(TaskInfoS2C, other.TaskInfoS2C))
		{
			return false;
		}
		if (!object.Equals(PlayerOnlineS2C, other.PlayerOnlineS2C))
		{
			return false;
		}
		if (!object.Equals(ChangeExpS2C, other.ChangeExpS2C))
		{
			return false;
		}
		if (!object.Equals(MailAddS2C, other.MailAddS2C))
		{
			return false;
		}
		if (!object.Equals(OnlineSyncRoomIdS2C, other.OnlineSyncRoomIdS2C))
		{
			return false;
		}
		if (!object.Equals(NoticeS2C, other.NoticeS2C))
		{
			return false;
		}
		if (!object.Equals(ActivityTaskConditionS2C, other.ActivityTaskConditionS2C))
		{
			return false;
		}
		if (!object.Equals(MapEventS2C, other.MapEventS2C))
		{
			return false;
		}
		if (!object.Equals(Day7RewardS2C, other.Day7RewardS2C))
		{
			return false;
		}
		if (!object.Equals(MapEventTrainS2C, other.MapEventTrainS2C))
		{
			return false;
		}
		if (!object.Equals(ChangePraiseNumS2C, other.ChangePraiseNumS2C))
		{
			return false;
		}
		if (!object.Equals(MonthlyCardS2C, other.MonthlyCardS2C))
		{
			return false;
		}
		if (!object.Equals(MailDelS2C, other.MailDelS2C))
		{
			return false;
		}
		if (!object.Equals(FriendNotifyS2C, other.FriendNotifyS2C))
		{
			return false;
		}
		if (!object.Equals(FriendListChangeS2C, other.FriendListChangeS2C))
		{
			return false;
		}
		if (!object.Equals(FriendInviteNotifyS2C, other.FriendInviteNotifyS2C))
		{
			return false;
		}
		if (!object.Equals(LoopNoticeS2C, other.LoopNoticeS2C))
		{
			return false;
		}
		if (!object.Equals(BattlePassLvS2C, other.BattlePassLvS2C))
		{
			return false;
		}
		if (!object.Equals(BattlePassTaskInfoS2C, other.BattlePassTaskInfoS2C))
		{
			return false;
		}
		if (!object.Equals(BattlePassUpdateTaskS2C, other.BattlePassUpdateTaskS2C))
		{
			return false;
		}
		if (!object.Equals(BattlePassBuyS2C, other.BattlePassBuyS2C))
		{
			return false;
		}
		if (!object.Equals(BattlePassInfoS2C, other.BattlePassInfoS2C))
		{
			return false;
		}
		if (!object.Equals(FriendsChatMsgS2C, other.FriendsChatMsgS2C))
		{
			return false;
		}
		if (!object.Equals(GameProgressChangeS2C, other.GameProgressChangeS2C))
		{
			return false;
		}
		if (!object.Equals(MapMissionNotifyS2C, other.MapMissionNotifyS2C))
		{
			return false;
		}
		if (!object.Equals(ChangeItemLimitS2C, other.ChangeItemLimitS2C))
		{
			return false;
		}
		if (!object.Equals(CleanItemLimitS2C, other.CleanItemLimitS2C))
		{
			return false;
		}
		if (!object.Equals(CampaignPassS2C, other.CampaignPassS2C))
		{
			return false;
		}
		if (!object.Equals(CampaignNotifyS2C, other.CampaignNotifyS2C))
		{
			return false;
		}
		if (!object.Equals(KillMessageS2C, other.KillMessageS2C))
		{
			return false;
		}
		if (!object.Equals(GameScoreChangeS2C, other.GameScoreChangeS2C))
		{
			return false;
		}
		if (!object.Equals(SignInRewardS2C, other.SignInRewardS2C))
		{
			return false;
		}
		if (!object.Equals(PayResultS2C, other.PayResultS2C))
		{
			return false;
		}
		if (!object.Equals(PayInfoChangeS2C, other.PayInfoChangeS2C))
		{
			return false;
		}
		if (!object.Equals(InviteSuccessS2C, other.InviteSuccessS2C))
		{
			return false;
		}
		if (!object.Equals(InviteInfoNotifyS2C, other.InviteInfoNotifyS2C))
		{
			return false;
		}
		if (!object.Equals(MapStatusChangeS2C, other.MapStatusChangeS2C))
		{
			return false;
		}
		if (!object.Equals(GachaCountS2C, other.GachaCountS2C))
		{
			return false;
		}
		if (!object.Equals(MapIndexChangeS2C, other.MapIndexChangeS2C))
		{
			return false;
		}
		if (!object.Equals(SurrenderPunishS2C, other.SurrenderPunishS2C))
		{
			return false;
		}
		if (!object.Equals(GamePassMapSuccessS2C, other.GamePassMapSuccessS2C))
		{
			return false;
		}
		if (!object.Equals(FriendDelNotifyS2C, other.FriendDelNotifyS2C))
		{
			return false;
		}
		if (!object.Equals(BagExpiredTransformNotify, other.BagExpiredTransformNotify))
		{
			return false;
		}
		if (!object.Equals(MapEventCrabS2C, other.MapEventCrabS2C))
		{
			return false;
		}
		if (!object.Equals(PkAfterVoteS2C, other.PkAfterVoteS2C))
		{
			return false;
		}
		if (!object.Equals(PlayerTaskNotifyS2C, other.PlayerTaskNotifyS2C))
		{
			return false;
		}
		if (!object.Equals(UnLockDifficultyS2C, other.UnLockDifficultyS2C))
		{
			return false;
		}
		if (!object.Equals(HeroSkillMoveEffectS2C, other.HeroSkillMoveEffectS2C))
		{
			return false;
		}
		if (!object.Equals(TimeOutKickPlayerS2C, other.TimeOutKickPlayerS2C))
		{
			return false;
		}
		if (!object.Equals(SayPhraseNotifyS2C, other.SayPhraseNotifyS2C))
		{
			return false;
		}
		if (!object.Equals(MatchTeamInviteNotify, other.MatchTeamInviteNotify))
		{
			return false;
		}
		if (!object.Equals(RefreshMatchTeamStateNotify, other.RefreshMatchTeamStateNotify))
		{
			return false;
		}
		if (!object.Equals(ActivityPassGearChangeS2C, other.ActivityPassGearChangeS2C))
		{
			return false;
		}
		if (!object.Equals(SingleGameScoreChange, other.SingleGameScoreChange))
		{
			return false;
		}
		if (!object.Equals(DelayProgressMapEventS2C, other.DelayProgressMapEventS2C))
		{
			return false;
		}
		if (!object.Equals(LuckyStarMissionChangeS2C, other.LuckyStarMissionChangeS2C))
		{
			return false;
		}
		if (!object.Equals(MatchPunishmentS2C, other.MatchPunishmentS2C))
		{
			return false;
		}
		if (!object.Equals(ChallengeDataChangeS2C, other.ChallengeDataChangeS2C))
		{
			return false;
		}
		if (!object.Equals(GmUnlockRoleInfoS2C, other.GmUnlockRoleInfoS2C))
		{
			return false;
		}
		if (!object.Equals(SyncPlayerCreditInfoS2C, other.SyncPlayerCreditInfoS2C))
		{
			return false;
		}
		if (!object.Equals(RoomHeroCardChangeS2C, other.RoomHeroCardChangeS2C))
		{
			return false;
		}
		if (!object.Equals(RoomRoundAddTermS2C, other.RoomRoundAddTermS2C))
		{
			return false;
		}
		if (!object.Equals(ReturnInfoS2C, other.ReturnInfoS2C))
		{
			return false;
		}
		if (!object.Equals(SyncRelicsS2C, other.SyncRelicsS2C))
		{
			return false;
		}
		if (!object.Equals(ReplaySnapshotS2C, other.ReplaySnapshotS2C))
		{
			return false;
		}
		if (!object.Equals(ClueNotifyS2C, other.ClueNotifyS2C))
		{
			return false;
		}
		if (!object.Equals(ReplayDieS2C, other.ReplayDieS2C))
		{
			return false;
		}
		if (!object.Equals(GuildTaskNotifyS2C, other.GuildTaskNotifyS2C))
		{
			return false;
		}
		if (!object.Equals(GameRoundChangeS2C, other.GameRoundChangeS2C))
		{
			return false;
		}
		if (!object.Equals(NotifyQuestionS2C, other.NotifyQuestionS2C))
		{
			return false;
		}
		if (!object.Equals(ConnectHandle, other.ConnectHandle))
		{
			return false;
		}
		if (!object.Equals(Connect, other.Connect))
		{
			return false;
		}
		if (!object.Equals(HeartbeatHandle, other.HeartbeatHandle))
		{
			return false;
		}
		if (!object.Equals(Heartbeat, other.Heartbeat))
		{
			return false;
		}
		if (!object.Equals(CreateRoomHandle, other.CreateRoomHandle))
		{
			return false;
		}
		if (!object.Equals(CreateRoom, other.CreateRoom))
		{
			return false;
		}
		if (!object.Equals(SyncRoomHandle, other.SyncRoomHandle))
		{
			return false;
		}
		if (!object.Equals(SyncRoom, other.SyncRoom))
		{
			return false;
		}
		if (!object.Equals(JoinRoomHandle, other.JoinRoomHandle))
		{
			return false;
		}
		if (!object.Equals(JoinRoom, other.JoinRoom))
		{
			return false;
		}
		if (!object.Equals(ExitRoomHandle, other.ExitRoomHandle))
		{
			return false;
		}
		if (!object.Equals(ExitRoom, other.ExitRoom))
		{
			return false;
		}
		if (!object.Equals(QueryRoomHandle, other.QueryRoomHandle))
		{
			return false;
		}
		if (!object.Equals(QueryRoom, other.QueryRoom))
		{
			return false;
		}
		if (!object.Equals(RefreshRoomStateHandle, other.RefreshRoomStateHandle))
		{
			return false;
		}
		if (!object.Equals(RefreshRoomState, other.RefreshRoomState))
		{
			return false;
		}
		if (!object.Equals(StartGameHandle, other.StartGameHandle))
		{
			return false;
		}
		if (!object.Equals(StartGame, other.StartGame))
		{
			return false;
		}
		if (!object.Equals(ThrowDiceHandle, other.ThrowDiceHandle))
		{
			return false;
		}
		if (!object.Equals(ThrowDice, other.ThrowDice))
		{
			return false;
		}
		if (!object.Equals(ChangeRoomHandle, other.ChangeRoomHandle))
		{
			return false;
		}
		if (!object.Equals(ChangeRoom, other.ChangeRoom))
		{
			return false;
		}
		if (!object.Equals(MoveHandle, other.MoveHandle))
		{
			return false;
		}
		if (!object.Equals(Move, other.Move))
		{
			return false;
		}
		if (!object.Equals(ShopBuyHandle, other.ShopBuyHandle))
		{
			return false;
		}
		if (!object.Equals(ShopBuy, other.ShopBuy))
		{
			return false;
		}
		if (!object.Equals(PursuitHandle, other.PursuitHandle))
		{
			return false;
		}
		if (!object.Equals(Pursuit, other.Pursuit))
		{
			return false;
		}
		if (!object.Equals(BattleUseCardHandle, other.BattleUseCardHandle))
		{
			return false;
		}
		if (!object.Equals(BattleUseCard, other.BattleUseCard))
		{
			return false;
		}
		if (!object.Equals(BattleThrowDiceHandle, other.BattleThrowDiceHandle))
		{
			return false;
		}
		if (!object.Equals(BattleThrowDice, other.BattleThrowDice))
		{
			return false;
		}
		if (!object.Equals(BattleChoiceHandle, other.BattleChoiceHandle))
		{
			return false;
		}
		if (!object.Equals(BattleChoice, other.BattleChoice))
		{
			return false;
		}
		if (!object.Equals(LotteryChoiceHandle, other.LotteryChoiceHandle))
		{
			return false;
		}
		if (!object.Equals(LotteryChoice, other.LotteryChoice))
		{
			return false;
		}
		if (!object.Equals(MoveAgainHandle, other.MoveAgainHandle))
		{
			return false;
		}
		if (!object.Equals(MoveAgain, other.MoveAgain))
		{
			return false;
		}
		if (!object.Equals(AskBattleHandle, other.AskBattleHandle))
		{
			return false;
		}
		if (!object.Equals(AskBattle, other.AskBattle))
		{
			return false;
		}
		if (!object.Equals(RollGoldHandle, other.RollGoldHandle))
		{
			return false;
		}
		if (!object.Equals(RollGold, other.RollGold))
		{
			return false;
		}
		if (!object.Equals(EventThrowDiceHandle, other.EventThrowDiceHandle))
		{
			return false;
		}
		if (!object.Equals(EventThrowDice, other.EventThrowDice))
		{
			return false;
		}
		if (!object.Equals(TriggerEventHandle, other.TriggerEventHandle))
		{
			return false;
		}
		if (!object.Equals(TriggerEvent, other.TriggerEvent))
		{
			return false;
		}
		if (!object.Equals(UseEffectCardHandle, other.UseEffectCardHandle))
		{
			return false;
		}
		if (!object.Equals(UseEffectCard, other.UseEffectCard))
		{
			return false;
		}
		if (!object.Equals(BombThrowDiceHandle, other.BombThrowDiceHandle))
		{
			return false;
		}
		if (!object.Equals(BombThrowDice, other.BombThrowDice))
		{
			return false;
		}
		if (!object.Equals(ChoiceDirectionHandle, other.ChoiceDirectionHandle))
		{
			return false;
		}
		if (!object.Equals(ChoiceDirection, other.ChoiceDirection))
		{
			return false;
		}
		if (!object.Equals(LandChoiceTargetHandle, other.LandChoiceTargetHandle))
		{
			return false;
		}
		if (!object.Equals(LandChoiceTarget, other.LandChoiceTarget))
		{
			return false;
		}
		if (!object.Equals(ThrowDiceResultHandle, other.ThrowDiceResultHandle))
		{
			return false;
		}
		if (!object.Equals(ThrowDiceResult, other.ThrowDiceResult))
		{
			return false;
		}
		if (!object.Equals(TriggerDivinationHandle, other.TriggerDivinationHandle))
		{
			return false;
		}
		if (!object.Equals(TriggerDivination, other.TriggerDivination))
		{
			return false;
		}
		if (!object.Equals(TriggerDestinyHandle, other.TriggerDestinyHandle))
		{
			return false;
		}
		if (!object.Equals(TriggerDestiny, other.TriggerDestiny))
		{
			return false;
		}
		if (!object.Equals(UseQuickCardHandle, other.UseQuickCardHandle))
		{
			return false;
		}
		if (!object.Equals(UseQuickCard, other.UseQuickCard))
		{
			return false;
		}
		if (!object.Equals(AbandonCardHandle, other.AbandonCardHandle))
		{
			return false;
		}
		if (!object.Equals(AbandonCard, other.AbandonCard))
		{
			return false;
		}
		if (!object.Equals(StopOrContinueHandle, other.StopOrContinueHandle))
		{
			return false;
		}
		if (!object.Equals(StopOrContinue, other.StopOrContinue))
		{
			return false;
		}
		if (!object.Equals(StartGambleHandle, other.StartGambleHandle))
		{
			return false;
		}
		if (!object.Equals(StartGamble, other.StartGamble))
		{
			return false;
		}
		if (!object.Equals(GambleThrowDicHandle, other.GambleThrowDicHandle))
		{
			return false;
		}
		if (!object.Equals(GambleThrowDic, other.GambleThrowDic))
		{
			return false;
		}
		if (!object.Equals(ChoiceHeroC2S2Handle, other.ChoiceHeroC2S2Handle))
		{
			return false;
		}
		if (!object.Equals(ChoiceHero2, other.ChoiceHero2))
		{
			return false;
		}
		if (!object.Equals(AffirmHeroHandle, other.AffirmHeroHandle))
		{
			return false;
		}
		if (!object.Equals(AffirmHero, other.AffirmHero))
		{
			return false;
		}
		if (!object.Equals(SearchRoomHandle, other.SearchRoomHandle))
		{
			return false;
		}
		if (!object.Equals(SearchRoom, other.SearchRoom))
		{
			return false;
		}
		if (!object.Equals(GmHandle, other.GmHandle))
		{
			return false;
		}
		if (!object.Equals(Gm, other.Gm))
		{
			return false;
		}
		if (!object.Equals(TriggerHospitalHandle, other.TriggerHospitalHandle))
		{
			return false;
		}
		if (!object.Equals(TriggerHospital, other.TriggerHospital))
		{
			return false;
		}
		if (!object.Equals(SendChatHandle, other.SendChatHandle))
		{
			return false;
		}
		if (!object.Equals(SendChat, other.SendChat))
		{
			return false;
		}
		if (!object.Equals(PlayerShopBuyC2S, other.PlayerShopBuyC2S))
		{
			return false;
		}
		if (!object.Equals(PlayerShopBuyS2C, other.PlayerShopBuyS2C))
		{
			return false;
		}
		if (!object.Equals(PlayerUseItemHandle, other.PlayerUseItemHandle))
		{
			return false;
		}
		if (!object.Equals(PlayerUseItem, other.PlayerUseItem))
		{
			return false;
		}
		if (!object.Equals(UseTreasureC2S, other.UseTreasureC2S))
		{
			return false;
		}
		if (!object.Equals(UseTreasureS2C, other.UseTreasureS2C))
		{
			return false;
		}
		if (!object.Equals(UseTreasureAutoTransformC2S, other.UseTreasureAutoTransformC2S))
		{
			return false;
		}
		if (!object.Equals(UseTreasureAutoTransformS2C, other.UseTreasureAutoTransformS2C))
		{
			return false;
		}
		if (!object.Equals(SetFashionC2S, other.SetFashionC2S))
		{
			return false;
		}
		if (!object.Equals(SetFashionS2C, other.SetFashionS2C))
		{
			return false;
		}
		if (!object.Equals(SelectFashionPlanC2S, other.SelectFashionPlanC2S))
		{
			return false;
		}
		if (!object.Equals(SelectFashionPlanS2C, other.SelectFashionPlanS2C))
		{
			return false;
		}
		if (!object.Equals(GachaC2S, other.GachaC2S))
		{
			return false;
		}
		if (!object.Equals(GachaS2C, other.GachaS2C))
		{
			return false;
		}
		if (!object.Equals(SteamSearchRoomC2S, other.SteamSearchRoomC2S))
		{
			return false;
		}
		if (!object.Equals(SteamSearchRoomS2C, other.SteamSearchRoomS2C))
		{
			return false;
		}
		if (!object.Equals(CheatItemHandle, other.CheatItemHandle))
		{
			return false;
		}
		if (!object.Equals(CheatItem, other.CheatItem))
		{
			return false;
		}
		if (!object.Equals(RoleCardUpLvC2S, other.RoleCardUpLvC2S))
		{
			return false;
		}
		if (!object.Equals(RoleCardUpLvS2C, other.RoleCardUpLvS2C))
		{
			return false;
		}
		if (!object.Equals(RoleCardBreakThroughC2S, other.RoleCardBreakThroughC2S))
		{
			return false;
		}
		if (!object.Equals(RoleCardBreakThroughS2C, other.RoleCardBreakThroughS2C))
		{
			return false;
		}
		if (!object.Equals(RoleCardChoiceResC2S, other.RoleCardChoiceResC2S))
		{
			return false;
		}
		if (!object.Equals(RoleCardChoiceResS2C, other.RoleCardChoiceResS2C))
		{
			return false;
		}
		if (!object.Equals(TaskRewardC2S, other.TaskRewardC2S))
		{
			return false;
		}
		if (!object.Equals(TaskRewardS2C, other.TaskRewardS2C))
		{
			return false;
		}
		if (!object.Equals(TeachingC2S, other.TeachingC2S))
		{
			return false;
		}
		if (!object.Equals(TeachingS2C, other.TeachingS2C))
		{
			return false;
		}
		if (!object.Equals(QuickJoinRoomC2S, other.QuickJoinRoomC2S))
		{
			return false;
		}
		if (!object.Equals(QuickJoinRoomS2C, other.QuickJoinRoomS2C))
		{
			return false;
		}
		if (!object.Equals(RoomKickPlayerC2S, other.RoomKickPlayerC2S))
		{
			return false;
		}
		if (!object.Equals(RoomKickPlayerS2C, other.RoomKickPlayerS2C))
		{
			return false;
		}
		if (!object.Equals(RoomAbdicationC2S, other.RoomAbdicationC2S))
		{
			return false;
		}
		if (!object.Equals(RoomAbdicationS2C, other.RoomAbdicationS2C))
		{
			return false;
		}
		if (!object.Equals(RoomReadyC2S, other.RoomReadyC2S))
		{
			return false;
		}
		if (!object.Equals(RoomReadyS2C, other.RoomReadyS2C))
		{
			return false;
		}
		if (!object.Equals(ChargeCreateC2S, other.ChargeCreateC2S))
		{
			return false;
		}
		if (!object.Equals(ChargeCreateS2C, other.ChargeCreateS2C))
		{
			return false;
		}
		if (!object.Equals(ChargeC2S, other.ChargeC2S))
		{
			return false;
		}
		if (!object.Equals(ChargeS2C, other.ChargeS2C))
		{
			return false;
		}
		if (!object.Equals(GiftCdkC2S, other.GiftCdkC2S))
		{
			return false;
		}
		if (!object.Equals(GiftCdkS2C, other.GiftCdkS2C))
		{
			return false;
		}
		if (!object.Equals(MailReadC2S, other.MailReadC2S))
		{
			return false;
		}
		if (!object.Equals(MailReadS2C, other.MailReadS2C))
		{
			return false;
		}
		if (!object.Equals(MailGetRewardC2S, other.MailGetRewardC2S))
		{
			return false;
		}
		if (!object.Equals(MailGetRewardS2C, other.MailGetRewardS2C))
		{
			return false;
		}
		if (!object.Equals(MailDelReadC2S, other.MailDelReadC2S))
		{
			return false;
		}
		if (!object.Equals(MailDelReadS2C, other.MailDelReadS2C))
		{
			return false;
		}
		if (!object.Equals(GachaRecordC2S, other.GachaRecordC2S))
		{
			return false;
		}
		if (!object.Equals(GachaRecordS2C, other.GachaRecordS2C))
		{
			return false;
		}
		if (!object.Equals(ActivityTaskRewardC2S, other.ActivityTaskRewardC2S))
		{
			return false;
		}
		if (!object.Equals(ActivityTaskRewardS2C, other.ActivityTaskRewardS2C))
		{
			return false;
		}
		if (!object.Equals(RoomShortChatC2S, other.RoomShortChatC2S))
		{
			return false;
		}
		if (!object.Equals(RoomShortChatS2C, other.RoomShortChatS2C))
		{
			return false;
		}
		if (!object.Equals(SetShowPlayerC2S, other.SetShowPlayerC2S))
		{
			return false;
		}
		if (!object.Equals(SetShowPlayerS2C, other.SetShowPlayerS2C))
		{
			return false;
		}
		if (!object.Equals(GetShowPlayerC2S, other.GetShowPlayerC2S))
		{
			return false;
		}
		if (!object.Equals(GetShowPlayerS2C, other.GetShowPlayerS2C))
		{
			return false;
		}
		if (!object.Equals(GetPlayerFightRecordC2S, other.GetPlayerFightRecordC2S))
		{
			return false;
		}
		if (!object.Equals(GetPlayerFightRecordS2C, other.GetPlayerFightRecordS2C))
		{
			return false;
		}
		if (!object.Equals(GetDay7RewardC2S, other.GetDay7RewardC2S))
		{
			return false;
		}
		if (!object.Equals(GetDay7RewardS2C, other.GetDay7RewardS2C))
		{
			return false;
		}
		if (!object.Equals(PraisePlayerC2S, other.PraisePlayerC2S))
		{
			return false;
		}
		if (!object.Equals(PraisePlayerS2C, other.PraisePlayerS2C))
		{
			return false;
		}
		if (!object.Equals(ClientDataUploadC2S, other.ClientDataUploadC2S))
		{
			return false;
		}
		if (!object.Equals(ClientDataUploadS2C, other.ClientDataUploadS2C))
		{
			return false;
		}
		if (!object.Equals(FriendListC2S, other.FriendListC2S))
		{
			return false;
		}
		if (!object.Equals(FriendListS2C, other.FriendListS2C))
		{
			return false;
		}
		if (!object.Equals(FriendApplyC2S, other.FriendApplyC2S))
		{
			return false;
		}
		if (!object.Equals(FriendApplyS2C, other.FriendApplyS2C))
		{
			return false;
		}
		if (!object.Equals(FriendApplyListC2S, other.FriendApplyListC2S))
		{
			return false;
		}
		if (!object.Equals(FriendApplyListS2C, other.FriendApplyListS2C))
		{
			return false;
		}
		if (!object.Equals(FriendApplyOpC2S, other.FriendApplyOpC2S))
		{
			return false;
		}
		if (!object.Equals(FriendApplyOpS2C, other.FriendApplyOpS2C))
		{
			return false;
		}
		if (!object.Equals(FriendOpC2S, other.FriendOpC2S))
		{
			return false;
		}
		if (!object.Equals(FriendOpS2C, other.FriendOpS2C))
		{
			return false;
		}
		if (!object.Equals(FriendInviteC2S, other.FriendInviteC2S))
		{
			return false;
		}
		if (!object.Equals(FriendInviteS2C, other.FriendInviteS2C))
		{
			return false;
		}
		if (!object.Equals(FriendInviteListC2S, other.FriendInviteListC2S))
		{
			return false;
		}
		if (!object.Equals(FriendInviteListS2C, other.FriendInviteListS2C))
		{
			return false;
		}
		if (!object.Equals(FriendInviteCleanC2S, other.FriendInviteCleanC2S))
		{
			return false;
		}
		if (!object.Equals(FriendInviteCleanS2C, other.FriendInviteCleanS2C))
		{
			return false;
		}
		if (!object.Equals(FriendBlacksListC2S, other.FriendBlacksListC2S))
		{
			return false;
		}
		if (!object.Equals(FriendBlacksListS2C, other.FriendBlacksListS2C))
		{
			return false;
		}
		if (!object.Equals(NearFightPlayerC2S, other.NearFightPlayerC2S))
		{
			return false;
		}
		if (!object.Equals(NearFightPlayerS2C, other.NearFightPlayerS2C))
		{
			return false;
		}
		if (!object.Equals(SearchPlayerC2S, other.SearchPlayerC2S))
		{
			return false;
		}
		if (!object.Equals(SearchPlayerS2C, other.SearchPlayerS2C))
		{
			return false;
		}
		if (!object.Equals(ScratchCardC2S, other.ScratchCardC2S))
		{
			return false;
		}
		if (!object.Equals(ScratchCardS2C, other.ScratchCardS2C))
		{
			return false;
		}
		if (!object.Equals(NextScratchCardPoolC2S, other.NextScratchCardPoolC2S))
		{
			return false;
		}
		if (!object.Equals(NextScratchCardPoolS2C, other.NextScratchCardPoolS2C))
		{
			return false;
		}
		if (!object.Equals(WatchJoinRoomC2S, other.WatchJoinRoomC2S))
		{
			return false;
		}
		if (!object.Equals(WatchJoinRoomS2C, other.WatchJoinRoomS2C))
		{
			return false;
		}
		if (!object.Equals(WatchRefreshRoomStateC2S, other.WatchRefreshRoomStateC2S))
		{
			return false;
		}
		if (!object.Equals(WatchRefreshRoomStateS2C, other.WatchRefreshRoomStateS2C))
		{
			return false;
		}
		if (!object.Equals(WatchExitRoomC2S, other.WatchExitRoomC2S))
		{
			return false;
		}
		if (!object.Equals(WatchExitRoomS2C, other.WatchExitRoomS2C))
		{
			return false;
		}
		if (!object.Equals(BattlePassGetRewardC2S, other.BattlePassGetRewardC2S))
		{
			return false;
		}
		if (!object.Equals(BattlePassGetRewardS2C, other.BattlePassGetRewardS2C))
		{
			return false;
		}
		if (!object.Equals(BattlePassTaskRewardC2S, other.BattlePassTaskRewardC2S))
		{
			return false;
		}
		if (!object.Equals(BattlePassTaskRewardS2C, other.BattlePassTaskRewardS2C))
		{
			return false;
		}
		if (!object.Equals(BattlePassUpLvC2S, other.BattlePassUpLvC2S))
		{
			return false;
		}
		if (!object.Equals(BattlePassUpLvS2C, other.BattlePassUpLvS2C))
		{
			return false;
		}
		if (!object.Equals(FriendSendMsgC2S, other.FriendSendMsgC2S))
		{
			return false;
		}
		if (!object.Equals(FriendSendMsgS2C, other.FriendSendMsgS2C))
		{
			return false;
		}
		if (!object.Equals(GetChatMsgC2S, other.GetChatMsgC2S))
		{
			return false;
		}
		if (!object.Equals(GetChatMsgS2C, other.GetChatMsgS2C))
		{
			return false;
		}
		if (!object.Equals(ReadChatMsgC2S, other.ReadChatMsgC2S))
		{
			return false;
		}
		if (!object.Equals(ReadChatMsgS2C, other.ReadChatMsgS2C))
		{
			return false;
		}
		if (!object.Equals(DelChatMsgInfoC2S, other.DelChatMsgInfoC2S))
		{
			return false;
		}
		if (!object.Equals(DelChatMsgInfoS2C, other.DelChatMsgInfoS2C))
		{
			return false;
		}
		if (!object.Equals(SelectRelicC2S, other.SelectRelicC2S))
		{
			return false;
		}
		if (!object.Equals(SelectRelicS2C, other.SelectRelicS2C))
		{
			return false;
		}
		if (!object.Equals(MonsterPursuitC2S, other.MonsterPursuitC2S))
		{
			return false;
		}
		if (!object.Equals(MonsterPursuitS2C, other.MonsterPursuitS2C))
		{
			return false;
		}
		if (!object.Equals(PVEShopBuyC2S, other.PVEShopBuyC2S))
		{
			return false;
		}
		if (!object.Equals(PVEShopBuyS2C, other.PVEShopBuyS2C))
		{
			return false;
		}
		if (!object.Equals(ClientCheckTaskC2S, other.ClientCheckTaskC2S))
		{
			return false;
		}
		if (!object.Equals(ClientCheckTaskS2C, other.ClientCheckTaskS2C))
		{
			return false;
		}
		if (!object.Equals(PveHeroUpLvC2S, other.PveHeroUpLvC2S))
		{
			return false;
		}
		if (!object.Equals(PveHeroUpLvS2C, other.PveHeroUpLvS2C))
		{
			return false;
		}
		if (!object.Equals(StartMatchC2S, other.StartMatchC2S))
		{
			return false;
		}
		if (!object.Equals(StartMatchS2C, other.StartMatchS2C))
		{
			return false;
		}
		if (!object.Equals(CancelMatchC2S, other.CancelMatchC2S))
		{
			return false;
		}
		if (!object.Equals(CancelMatchS2C, other.CancelMatchS2C))
		{
			return false;
		}
		if (!object.Equals(MatchSuccessC2S, other.MatchSuccessC2S))
		{
			return false;
		}
		if (!object.Equals(MatchSuccessS2C, other.MatchSuccessS2C))
		{
			return false;
		}
		if (!object.Equals(AccuseC2S, other.AccuseC2S))
		{
			return false;
		}
		if (!object.Equals(AccuseS2C, other.AccuseS2C))
		{
			return false;
		}
		if (!object.Equals(SingleCampaignC2S, other.SingleCampaignC2S))
		{
			return false;
		}
		if (!object.Equals(SingleCampaignS2C, other.SingleCampaignS2C))
		{
			return false;
		}
		if (!object.Equals(DevChargeC2S, other.DevChargeC2S))
		{
			return false;
		}
		if (!object.Equals(DevChargeS2C, other.DevChargeS2C))
		{
			return false;
		}
		if (!object.Equals(AskReviveTeammateC2S, other.AskReviveTeammateC2S))
		{
			return false;
		}
		if (!object.Equals(AskReviveTeammateS2C, other.AskReviveTeammateS2C))
		{
			return false;
		}
		if (!object.Equals(GetSignInRewardC2S, other.GetSignInRewardC2S))
		{
			return false;
		}
		if (!object.Equals(GetSignInRewardS2C, other.GetSignInRewardS2C))
		{
			return false;
		}
		if (!object.Equals(ChatMapMarkersC2S, other.ChatMapMarkersC2S))
		{
			return false;
		}
		if (!object.Equals(ChatMapMarkersS2C, other.ChatMapMarkersS2C))
		{
			return false;
		}
		if (!object.Equals(AbroadCreateOrderC2S, other.AbroadCreateOrderC2S))
		{
			return false;
		}
		if (!object.Equals(AbroadCreateOrderS2C, other.AbroadCreateOrderS2C))
		{
			return false;
		}
		if (!object.Equals(AgeVerifyC2S, other.AgeVerifyC2S))
		{
			return false;
		}
		if (!object.Equals(AgeVerifyS2C, other.AgeVerifyS2C))
		{
			return false;
		}
		if (!object.Equals(ChangeNameC2S, other.ChangeNameC2S))
		{
			return false;
		}
		if (!object.Equals(ChangeNameS2C, other.ChangeNameS2C))
		{
			return false;
		}
		if (!object.Equals(ClientClickConfirmTaskC2S, other.ClientClickConfirmTaskC2S))
		{
			return false;
		}
		if (!object.Equals(ClientClickConfirmTaskS2C, other.ClientClickConfirmTaskS2C))
		{
			return false;
		}
		if (!object.Equals(BuyRelicC2S, other.BuyRelicC2S))
		{
			return false;
		}
		if (!object.Equals(BuyRelicS2C, other.BuyRelicS2C))
		{
			return false;
		}
		if (!object.Equals(BuyLightGiftC2S, other.BuyLightGiftC2S))
		{
			return false;
		}
		if (!object.Equals(BuyLightGiftS2C, other.BuyLightGiftS2C))
		{
			return false;
		}
		if (!object.Equals(LightGiftC2S, other.LightGiftC2S))
		{
			return false;
		}
		if (!object.Equals(LightGiftS2C, other.LightGiftS2C))
		{
			return false;
		}
		if (!object.Equals(AcquisitionC2S, other.AcquisitionC2S))
		{
			return false;
		}
		if (!object.Equals(AcquisitionS2C, other.AcquisitionS2C))
		{
			return false;
		}
		if (!object.Equals(AcquisitionRewardC2S, other.AcquisitionRewardC2S))
		{
			return false;
		}
		if (!object.Equals(AcquisitionRewardS2C, other.AcquisitionRewardS2C))
		{
			return false;
		}
		if (!object.Equals(SelectMechanismC2S, other.SelectMechanismC2S))
		{
			return false;
		}
		if (!object.Equals(SelectMechanismS2C, other.SelectMechanismS2C))
		{
			return false;
		}
		if (!object.Equals(GachaCountRewardC2S, other.GachaCountRewardC2S))
		{
			return false;
		}
		if (!object.Equals(GachaCountRewardS2C, other.GachaCountRewardS2C))
		{
			return false;
		}
		if (!object.Equals(GetPlayerSimpleC2S, other.GetPlayerSimpleC2S))
		{
			return false;
		}
		if (!object.Equals(GetPlayerSimpleS2C, other.GetPlayerSimpleS2C))
		{
			return false;
		}
		if (!object.Equals(RoleCardCollectC2S, other.RoleCardCollectC2S))
		{
			return false;
		}
		if (!object.Equals(RoleCardCollectS2C, other.RoleCardCollectS2C))
		{
			return false;
		}
		if (!object.Equals(GMPlayerSettingC2S, other.GMPlayerSettingC2S))
		{
			return false;
		}
		if (!object.Equals(GMPlayerSettingS2C, other.GMPlayerSettingS2C))
		{
			return false;
		}
		if (!object.Equals(SetFriendNoteC2S, other.SetFriendNoteC2S))
		{
			return false;
		}
		if (!object.Equals(SetFriendNoteS2C, other.SetFriendNoteS2C))
		{
			return false;
		}
		if (!object.Equals(SetOnlineStatusC2S, other.SetOnlineStatusC2S))
		{
			return false;
		}
		if (!object.Equals(SetOnlineStatusS2C, other.SetOnlineStatusS2C))
		{
			return false;
		}
		if (!object.Equals(ChooseSkinC2S, other.ChooseSkinC2S))
		{
			return false;
		}
		if (!object.Equals(ChooseSkinS2C, other.ChooseSkinS2C))
		{
			return false;
		}
		if (!object.Equals(TimeWastingC2S, other.TimeWastingC2S))
		{
			return false;
		}
		if (!object.Equals(TimeWastingS2C, other.TimeWastingS2C))
		{
			return false;
		}
		if (!object.Equals(VoteC2S, other.VoteC2S))
		{
			return false;
		}
		if (!object.Equals(VoteS2C, other.VoteS2C))
		{
			return false;
		}
		if (!object.Equals(VoteSelectC2S, other.VoteSelectC2S))
		{
			return false;
		}
		if (!object.Equals(VoteSelectS2C, other.VoteSelectS2C))
		{
			return false;
		}
		if (!object.Equals(NotifyStoryC2S, other.NotifyStoryC2S))
		{
			return false;
		}
		if (!object.Equals(NotifyStoryS2C, other.NotifyStoryS2C))
		{
			return false;
		}
		if (!object.Equals(PveHeroTalentUpC2S, other.PveHeroTalentUpC2S))
		{
			return false;
		}
		if (!object.Equals(PveHeroTalentUpS2C, other.PveHeroTalentUpS2C))
		{
			return false;
		}
		if (!object.Equals(SelectEventC2S, other.SelectEventC2S))
		{
			return false;
		}
		if (!object.Equals(SelectEventS2C, other.SelectEventS2C))
		{
			return false;
		}
		if (!object.Equals(CampScoreC2S, other.CampScoreC2S))
		{
			return false;
		}
		if (!object.Equals(CampScoreS2C, other.CampScoreS2C))
		{
			return false;
		}
		if (!object.Equals(ActivityMissionRewardC2S, other.ActivityMissionRewardC2S))
		{
			return false;
		}
		if (!object.Equals(ActivityMissionRewardS2C, other.ActivityMissionRewardS2C))
		{
			return false;
		}
		if (!object.Equals(VendorBuyCardC2S, other.VendorBuyCardC2S))
		{
			return false;
		}
		if (!object.Equals(VendorBuyCardS2C, other.VendorBuyCardS2C))
		{
			return false;
		}
		if (!object.Equals(TransferStarDiscC2S, other.TransferStarDiscC2S))
		{
			return false;
		}
		if (!object.Equals(TransferStarDiscS2C, other.TransferStarDiscS2C))
		{
			return false;
		}
		if (!object.Equals(GetHeroInfoC2S, other.GetHeroInfoC2S))
		{
			return false;
		}
		if (!object.Equals(GetHeroInfoS2C, other.GetHeroInfoS2C))
		{
			return false;
		}
		if (!object.Equals(CreateMatchTeamC2S, other.CreateMatchTeamC2S))
		{
			return false;
		}
		if (!object.Equals(CreateMatchTeamS2C, other.CreateMatchTeamS2C))
		{
			return false;
		}
		if (!object.Equals(ChangeMatchTeamC2S, other.ChangeMatchTeamC2S))
		{
			return false;
		}
		if (!object.Equals(ChangeMatchTeamS2C, other.ChangeMatchTeamS2C))
		{
			return false;
		}
		if (!object.Equals(JoinMatchTeamC2S, other.JoinMatchTeamC2S))
		{
			return false;
		}
		if (!object.Equals(JoinMatchTeamS2C, other.JoinMatchTeamS2C))
		{
			return false;
		}
		if (!object.Equals(ExitMatchTeamC2S, other.ExitMatchTeamC2S))
		{
			return false;
		}
		if (!object.Equals(ExitMatchTeamS2C, other.ExitMatchTeamS2C))
		{
			return false;
		}
		if (!object.Equals(RefreshMatchTeamInfoC2S, other.RefreshMatchTeamInfoC2S))
		{
			return false;
		}
		if (!object.Equals(RefreshMatchTeamInfoS2C, other.RefreshMatchTeamInfoS2C))
		{
			return false;
		}
		if (!object.Equals(ChinaCreateOrderC2S, other.ChinaCreateOrderC2S))
		{
			return false;
		}
		if (!object.Equals(ChinaCreateOrderS2C, other.ChinaCreateOrderS2C))
		{
			return false;
		}
		if (!object.Equals(MatchTeamInviteC2S, other.MatchTeamInviteC2S))
		{
			return false;
		}
		if (!object.Equals(MatchTeamInviteS2C, other.MatchTeamInviteS2C))
		{
			return false;
		}
		if (!object.Equals(MatchTeamChatC2S, other.MatchTeamChatC2S))
		{
			return false;
		}
		if (!object.Equals(MatchTeamChatS2C, other.MatchTeamChatS2C))
		{
			return false;
		}
		if (!object.Equals(MatchTeamReadyC2S, other.MatchTeamReadyC2S))
		{
			return false;
		}
		if (!object.Equals(MatchTeamReadyS2C, other.MatchTeamReadyS2C))
		{
			return false;
		}
		if (!object.Equals(PlayerChatC2S, other.PlayerChatC2S))
		{
			return false;
		}
		if (!object.Equals(PlayerChatS2C, other.PlayerChatS2C))
		{
			return false;
		}
		if (!object.Equals(SyncSingleGameDataC2S, other.SyncSingleGameDataC2S))
		{
			return false;
		}
		if (!object.Equals(SyncSingleGameDataS2C, other.SyncSingleGameDataS2C))
		{
			return false;
		}
		if (!object.Equals(SingleGameDataC2S, other.SingleGameDataC2S))
		{
			return false;
		}
		if (!object.Equals(SingleGameDataS2C, other.SingleGameDataS2C))
		{
			return false;
		}
		if (!object.Equals(GetActivityPassRewardC2S, other.GetActivityPassRewardC2S))
		{
			return false;
		}
		if (!object.Equals(GetActivityPassRewardS2C, other.GetActivityPassRewardS2C))
		{
			return false;
		}
		if (!object.Equals(ApplyChangeSlotC2S, other.ApplyChangeSlotC2S))
		{
			return false;
		}
		if (!object.Equals(ApplyChangeSlotS2C, other.ApplyChangeSlotS2C))
		{
			return false;
		}
		if (!object.Equals(OpsChangeSlotC2S, other.OpsChangeSlotC2S))
		{
			return false;
		}
		if (!object.Equals(OpsChangeSlotS2C, other.OpsChangeSlotS2C))
		{
			return false;
		}
		if (!object.Equals(RookieGachaRewardC2S, other.RookieGachaRewardC2S))
		{
			return false;
		}
		if (!object.Equals(RookieGachaRewardS2C, other.RookieGachaRewardS2C))
		{
			return false;
		}
		if (!object.Equals(LiveGiftPackageC2S, other.LiveGiftPackageC2S))
		{
			return false;
		}
		if (!object.Equals(LiveGiftPackageS2C, other.LiveGiftPackageS2C))
		{
			return false;
		}
		if (!object.Equals(LaborActDiceC2S, other.LaborActDiceC2S))
		{
			return false;
		}
		if (!object.Equals(LaborActDiceS2C, other.LaborActDiceS2C))
		{
			return false;
		}
		if (!object.Equals(ActionOverTimeLogC2S, other.ActionOverTimeLogC2S))
		{
			return false;
		}
		if (!object.Equals(ActionOverTimeLogS2C, other.ActionOverTimeLogS2C))
		{
			return false;
		}
		if (!object.Equals(ClientHarmonyC2S, other.ClientHarmonyC2S))
		{
			return false;
		}
		if (!object.Equals(ClientHarmonyS2C, other.ClientHarmonyS2C))
		{
			return false;
		}
		if (!object.Equals(MailStarC2S, other.MailStarC2S))
		{
			return false;
		}
		if (!object.Equals(MailStarS2C, other.MailStarS2C))
		{
			return false;
		}
		if (!object.Equals(SetCardAltArtC2S, other.SetCardAltArtC2S))
		{
			return false;
		}
		if (!object.Equals(SetCardAltArtS2C, other.SetCardAltArtS2C))
		{
			return false;
		}
		if (!object.Equals(SelectRewardCardC2S, other.SelectRewardCardC2S))
		{
			return false;
		}
		if (!object.Equals(SelectRewardCardS2C, other.SelectRewardCardS2C))
		{
			return false;
		}
		if (!object.Equals(GetQuestionUrlC2S, other.GetQuestionUrlC2S))
		{
			return false;
		}
		if (!object.Equals(GetQuestionUrlS2C, other.GetQuestionUrlS2C))
		{
			return false;
		}
		if (!object.Equals(GetReturnInfoC2S, other.GetReturnInfoC2S))
		{
			return false;
		}
		if (!object.Equals(GetReturnInfoS2C, other.GetReturnInfoS2C))
		{
			return false;
		}
		if (!object.Equals(ReturnGiftClaimC2S, other.ReturnGiftClaimC2S))
		{
			return false;
		}
		if (!object.Equals(ReturnGiftClaimS2C, other.ReturnGiftClaimS2C))
		{
			return false;
		}
		if (!object.Equals(ReturnSignInClaimC2S, other.ReturnSignInClaimC2S))
		{
			return false;
		}
		if (!object.Equals(ReturnSignInClaimS2C, other.ReturnSignInClaimS2C))
		{
			return false;
		}
		if (!object.Equals(ReturnSurveyFinishC2S, other.ReturnSurveyFinishC2S))
		{
			return false;
		}
		if (!object.Equals(ReturnSurveyFinishS2C, other.ReturnSurveyFinishS2C))
		{
			return false;
		}
		if (!object.Equals(FlipCardC2S, other.FlipCardC2S))
		{
			return false;
		}
		if (!object.Equals(FlipCardS2C, other.FlipCardS2C))
		{
			return false;
		}
		if (!object.Equals(FlipCardProgressRewardC2S, other.FlipCardProgressRewardC2S))
		{
			return false;
		}
		if (!object.Equals(FlipCardProgressRewardS2C, other.FlipCardProgressRewardS2C))
		{
			return false;
		}
		if (!object.Equals(SyncPlayerGuildS2C, other.SyncPlayerGuildS2C))
		{
			return false;
		}
		if (!object.Equals(SyncPlayerJoinGuildS2C, other.SyncPlayerJoinGuildS2C))
		{
			return false;
		}
		if (!object.Equals(GuildChatMsgS2C, other.GuildChatMsgS2C))
		{
			return false;
		}
		if (!object.Equals(SyncGuildS2C, other.SyncGuildS2C))
		{
			return false;
		}
		if (!object.Equals(SyncGuildMemberS2C, other.SyncGuildMemberS2C))
		{
			return false;
		}
		if (!object.Equals(SyncGuildMemberExitS2C, other.SyncGuildMemberExitS2C))
		{
			return false;
		}
		if (!object.Equals(CreateGuildC2S, other.CreateGuildC2S))
		{
			return false;
		}
		if (!object.Equals(CreateGuildS2C, other.CreateGuildS2C))
		{
			return false;
		}
		if (!object.Equals(SearchGuildC2S, other.SearchGuildC2S))
		{
			return false;
		}
		if (!object.Equals(SearchGuildS2C, other.SearchGuildS2C))
		{
			return false;
		}
		if (!object.Equals(ApplyToGuildC2S, other.ApplyToGuildC2S))
		{
			return false;
		}
		if (!object.Equals(ApplyToGuildS2C, other.ApplyToGuildS2C))
		{
			return false;
		}
		if (!object.Equals(ProcessGuildApplicationC2S, other.ProcessGuildApplicationC2S))
		{
			return false;
		}
		if (!object.Equals(ProcessGuildApplicationS2C, other.ProcessGuildApplicationS2C))
		{
			return false;
		}
		if (!object.Equals(SendGuildInvitationC2S, other.SendGuildInvitationC2S))
		{
			return false;
		}
		if (!object.Equals(SendGuildInvitationS2C, other.SendGuildInvitationS2C))
		{
			return false;
		}
		if (!object.Equals(ProcessGuildInvitationC2S, other.ProcessGuildInvitationC2S))
		{
			return false;
		}
		if (!object.Equals(ProcessGuildInvitationS2C, other.ProcessGuildInvitationS2C))
		{
			return false;
		}
		if (!object.Equals(GetGuildInfoC2S, other.GetGuildInfoC2S))
		{
			return false;
		}
		if (!object.Equals(GetGuildInfoS2C, other.GetGuildInfoS2C))
		{
			return false;
		}
		if (!object.Equals(UpdateGuildSettingsC2S, other.UpdateGuildSettingsC2S))
		{
			return false;
		}
		if (!object.Equals(UpdateGuildSettingsS2C, other.UpdateGuildSettingsS2C))
		{
			return false;
		}
		if (!object.Equals(UpdateGuildInAnnouncementC2S, other.UpdateGuildInAnnouncementC2S))
		{
			return false;
		}
		if (!object.Equals(UpdateGuildInAnnouncementS2C, other.UpdateGuildInAnnouncementS2C))
		{
			return false;
		}
		if (!object.Equals(TransferGuildMasterC2S, other.TransferGuildMasterC2S))
		{
			return false;
		}
		if (!object.Equals(TransferGuildMasterS2C, other.TransferGuildMasterS2C))
		{
			return false;
		}
		if (!object.Equals(ChangeGuildMemberTitleC2S, other.ChangeGuildMemberTitleC2S))
		{
			return false;
		}
		if (!object.Equals(ChangeGuildMemberTitleS2C, other.ChangeGuildMemberTitleS2C))
		{
			return false;
		}
		if (!object.Equals(KickGuildMemberC2S, other.KickGuildMemberC2S))
		{
			return false;
		}
		if (!object.Equals(KickGuildMemberS2C, other.KickGuildMemberS2C))
		{
			return false;
		}
		if (!object.Equals(ImpeachGuildMasterC2S, other.ImpeachGuildMasterC2S))
		{
			return false;
		}
		if (!object.Equals(ImpeachGuildMasterS2C, other.ImpeachGuildMasterS2C))
		{
			return false;
		}
		if (!object.Equals(ExitGuildC2S, other.ExitGuildC2S))
		{
			return false;
		}
		if (!object.Equals(ExitGuildS2C, other.ExitGuildS2C))
		{
			return false;
		}
		if (!object.Equals(DisbandGuildC2S, other.DisbandGuildC2S))
		{
			return false;
		}
		if (!object.Equals(DisbandGuildS2C, other.DisbandGuildS2C))
		{
			return false;
		}
		if (!object.Equals(GuildMissionRewardC2S, other.GuildMissionRewardC2S))
		{
			return false;
		}
		if (!object.Equals(GuildMissionRewardS2C, other.GuildMissionRewardS2C))
		{
			return false;
		}
		if (!object.Equals(GetGuildMemberChangeMsgC2S, other.GetGuildMemberChangeMsgC2S))
		{
			return false;
		}
		if (!object.Equals(GetGuildMemberChangeMsgS2C, other.GetGuildMemberChangeMsgS2C))
		{
			return false;
		}
		if (!object.Equals(SendGuildChatMsgC2S, other.SendGuildChatMsgC2S))
		{
			return false;
		}
		if (!object.Equals(SendGuildChatMsgS2C, other.SendGuildChatMsgS2C))
		{
			return false;
		}
		if (!object.Equals(GetGuildChatMsgC2S, other.GetGuildChatMsgC2S))
		{
			return false;
		}
		if (!object.Equals(GetGuildChatMsgS2C, other.GetGuildChatMsgS2C))
		{
			return false;
		}
		if (!object.Equals(GuildMemberC2S, other.GuildMemberC2S))
		{
			return false;
		}
		if (!object.Equals(GuildMemberS2C, other.GuildMemberS2C))
		{
			return false;
		}
		if (!object.Equals(GetGuildsInfoC2S, other.GetGuildsInfoC2S))
		{
			return false;
		}
		if (!object.Equals(GetGuildsInfoS2C, other.GetGuildsInfoS2C))
		{
			return false;
		}
		if (!object.Equals(TestRpcEchoC2S, other.TestRpcEchoC2S))
		{
			return false;
		}
		if (!object.Equals(TestRpcEchoS2C, other.TestRpcEchoS2C))
		{
			return false;
		}
		if (MsgCase != other.MsgCase)
		{
			return false;
		}
		return object.Equals(_unknownFields, other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override int GetHashCode()
	{
		int num = 1;
		if (msgCase_ == MsgOneofCase.SysSendMailC2S)
		{
			num ^= SysSendMailC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.SysPlayerOnlineC2S)
		{
			num ^= SysPlayerOnlineC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.PlayerOnlineRoomC2S)
		{
			num ^= PlayerOnlineRoomC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.SysCanPraiseInfoC2S)
		{
			num ^= SysCanPraiseInfoC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.SysPraiseC2S)
		{
			num ^= SysPraiseC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.SysRoomFinishC2S)
		{
			num ^= SysRoomFinishC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.SysRoomAddExpC2S)
		{
			num ^= SysRoomAddExpC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.SysGetShowFriendC2S)
		{
			num ^= SysGetShowFriendC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.SysFriendInviteC2S)
		{
			num ^= SysFriendInviteC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.SysFriendDelC2S)
		{
			num ^= SysFriendDelC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.SysFriendInfoC2S)
		{
			num ^= SysFriendInfoC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.SysFriendApplyC2S)
		{
			num ^= SysFriendApplyC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.SysFriendAddC2S)
		{
			num ^= SysFriendAddC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.SysFriendSendMsgC2S)
		{
			num ^= SysFriendSendMsgC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.SysCampaignFinishC2S)
		{
			num ^= SysCampaignFinishC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.SysAbroadPayMsgC2S)
		{
			num ^= SysAbroadPayMsgC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.AcquisitionMsgC2S)
		{
			num ^= AcquisitionMsgC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.SysFightRecord)
		{
			num ^= SysFightRecord.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.SysCampaignAwardC2S)
		{
			num ^= SysCampaignAwardC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.SysRecoupItemC2S)
		{
			num ^= SysRecoupItemC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.SysPlayerOnlineRoomC2S)
		{
			num ^= SysPlayerOnlineRoomC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.SysPlayerCleanC2S)
		{
			num ^= SysPlayerCleanC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.SysPlayerPunishmentTimeC2S)
		{
			num ^= SysPlayerPunishmentTimeC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.SysMatchSuccess)
		{
			num ^= SysMatchSuccess.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.SysChinaPayMsgC2S)
		{
			num ^= SysChinaPayMsgC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.SysChangeMatchTeamState)
		{
			num ^= SysChangeMatchTeamState.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.SysSaveSimplePlayerInfoC2S)
		{
			num ^= SysSaveSimplePlayerInfoC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.SysGmChangeNameC2S)
		{
			num ^= SysGmChangeNameC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.SysSyncPlayerC2S)
		{
			num ^= SysSyncPlayerC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.SysPlayerCreditScoreChangeC2S)
		{
			num ^= SysPlayerCreditScoreChangeC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.SysSyncPlayerMatchPunishmentTimeC2S)
		{
			num ^= SysSyncPlayerMatchPunishmentTimeC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.GMChangeCreditScoreC2S)
		{
			num ^= GMChangeCreditScoreC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.SysPushReturnInfoC2S)
		{
			num ^= SysPushReturnInfoC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.SysMutePlayerC2S)
		{
			num ^= SysMutePlayerC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.SysQuestionC2S)
		{
			num ^= SysQuestionC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.KickS2C)
		{
			num ^= KickS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.PredictActionS2C)
		{
			num ^= PredictActionS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.RunningGameS2C)
		{
			num ^= RunningGameS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.BattleS2C)
		{
			num ^= BattleS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.LotteryDrawS2C)
		{
			num ^= LotteryDrawS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.LandBuffsS2C)
		{
			num ^= LandBuffsS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.RoundStartS2C)
		{
			num ^= RoundStartS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.GameFinishS2C)
		{
			num ^= GameFinishS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.MonsterRefreshS2C)
		{
			num ^= MonsterRefreshS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.MovePointBuffS2C)
		{
			num ^= MovePointBuffS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.ChangeDirS2C)
		{
			num ^= ChangeDirS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.GambleChangeS2C)
		{
			num ^= GambleChangeS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.HeroBarBoxChangeS2C)
		{
			num ^= HeroBarBoxChangeS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.RoomNotifyS2C)
		{
			num ^= RoomNotifyS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.ActionStartNotifyS2C)
		{
			num ^= ActionStartNotifyS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.NoGambleNotifyS2C)
		{
			num ^= NoGambleNotifyS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.GambleObServeS2C)
		{
			num ^= GambleObServeS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.UpdateHeroAttrS2C)
		{
			num ^= UpdateHeroAttrS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.ChangePlayerSlotS2C)
		{
			num ^= ChangePlayerSlotS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.BossSleepS2C)
		{
			num ^= BossSleepS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.RefMallS2C)
		{
			num ^= RefMallS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.BagItemChangeS2C)
		{
			num ^= BagItemChangeS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.RoleCardChangeS2C)
		{
			num ^= RoleCardChangeS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.TaskConditionS2C)
		{
			num ^= TaskConditionS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.TaskInfoS2C)
		{
			num ^= TaskInfoS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.PlayerOnlineS2C)
		{
			num ^= PlayerOnlineS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.ChangeExpS2C)
		{
			num ^= ChangeExpS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.MailAddS2C)
		{
			num ^= MailAddS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.OnlineSyncRoomIdS2C)
		{
			num ^= OnlineSyncRoomIdS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.NoticeS2C)
		{
			num ^= NoticeS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.ActivityTaskConditionS2C)
		{
			num ^= ActivityTaskConditionS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.MapEventS2C)
		{
			num ^= MapEventS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.Day7RewardS2C)
		{
			num ^= Day7RewardS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.MapEventTrainS2C)
		{
			num ^= MapEventTrainS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.ChangePraiseNumS2C)
		{
			num ^= ChangePraiseNumS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.MonthlyCardS2C)
		{
			num ^= MonthlyCardS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.MailDelS2C)
		{
			num ^= MailDelS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.FriendNotifyS2C)
		{
			num ^= FriendNotifyS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.FriendListChangeS2C)
		{
			num ^= FriendListChangeS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.FriendInviteNotifyS2C)
		{
			num ^= FriendInviteNotifyS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.LoopNoticeS2C)
		{
			num ^= LoopNoticeS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.BattlePassLvS2C)
		{
			num ^= BattlePassLvS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.BattlePassTaskInfoS2C)
		{
			num ^= BattlePassTaskInfoS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.BattlePassUpdateTaskS2C)
		{
			num ^= BattlePassUpdateTaskS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.BattlePassBuyS2C)
		{
			num ^= BattlePassBuyS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.BattlePassInfoS2C)
		{
			num ^= BattlePassInfoS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.FriendsChatMsgS2C)
		{
			num ^= FriendsChatMsgS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.GameProgressChangeS2C)
		{
			num ^= GameProgressChangeS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.MapMissionNotifyS2C)
		{
			num ^= MapMissionNotifyS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.ChangeItemLimitS2C)
		{
			num ^= ChangeItemLimitS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.CleanItemLimitS2C)
		{
			num ^= CleanItemLimitS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.CampaignPassS2C)
		{
			num ^= CampaignPassS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.CampaignNotifyS2C)
		{
			num ^= CampaignNotifyS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.KillMessageS2C)
		{
			num ^= KillMessageS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.GameScoreChangeS2C)
		{
			num ^= GameScoreChangeS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.SignInRewardS2C)
		{
			num ^= SignInRewardS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.PayResultS2C)
		{
			num ^= PayResultS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.PayInfoChangeS2C)
		{
			num ^= PayInfoChangeS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.InviteSuccessS2C)
		{
			num ^= InviteSuccessS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.InviteInfoNotifyS2C)
		{
			num ^= InviteInfoNotifyS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.MapStatusChangeS2C)
		{
			num ^= MapStatusChangeS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.GachaCountS2C)
		{
			num ^= GachaCountS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.MapIndexChangeS2C)
		{
			num ^= MapIndexChangeS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.SurrenderPunishS2C)
		{
			num ^= SurrenderPunishS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.GamePassMapSuccessS2C)
		{
			num ^= GamePassMapSuccessS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.FriendDelNotifyS2C)
		{
			num ^= FriendDelNotifyS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.BagExpiredTransformNotify)
		{
			num ^= BagExpiredTransformNotify.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.MapEventCrabS2C)
		{
			num ^= MapEventCrabS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.PkAfterVoteS2C)
		{
			num ^= PkAfterVoteS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.PlayerTaskNotifyS2C)
		{
			num ^= PlayerTaskNotifyS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.UnLockDifficultyS2C)
		{
			num ^= UnLockDifficultyS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.HeroSkillMoveEffectS2C)
		{
			num ^= HeroSkillMoveEffectS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.TimeOutKickPlayerS2C)
		{
			num ^= TimeOutKickPlayerS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.SayPhraseNotifyS2C)
		{
			num ^= SayPhraseNotifyS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.MatchTeamInviteNotify)
		{
			num ^= MatchTeamInviteNotify.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.RefreshMatchTeamStateNotify)
		{
			num ^= RefreshMatchTeamStateNotify.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.ActivityPassGearChangeS2C)
		{
			num ^= ActivityPassGearChangeS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.SingleGameScoreChange)
		{
			num ^= SingleGameScoreChange.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.DelayProgressMapEventS2C)
		{
			num ^= DelayProgressMapEventS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.LuckyStarMissionChangeS2C)
		{
			num ^= LuckyStarMissionChangeS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.MatchPunishmentS2C)
		{
			num ^= MatchPunishmentS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.ChallengeDataChangeS2C)
		{
			num ^= ChallengeDataChangeS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.GmUnlockRoleInfoS2C)
		{
			num ^= GmUnlockRoleInfoS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.SyncPlayerCreditInfoS2C)
		{
			num ^= SyncPlayerCreditInfoS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.RoomHeroCardChangeS2C)
		{
			num ^= RoomHeroCardChangeS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.RoomRoundAddTermS2C)
		{
			num ^= RoomRoundAddTermS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.ReturnInfoS2C)
		{
			num ^= ReturnInfoS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.SyncRelicsS2C)
		{
			num ^= SyncRelicsS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.ReplaySnapshotS2C)
		{
			num ^= ReplaySnapshotS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.ClueNotifyS2C)
		{
			num ^= ClueNotifyS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.ReplayDieS2C)
		{
			num ^= ReplayDieS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.GuildTaskNotifyS2C)
		{
			num ^= GuildTaskNotifyS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.GameRoundChangeS2C)
		{
			num ^= GameRoundChangeS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.NotifyQuestionS2C)
		{
			num ^= NotifyQuestionS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.ConnectHandle)
		{
			num ^= ConnectHandle.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.Connect)
		{
			num ^= Connect.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.HeartbeatHandle)
		{
			num ^= HeartbeatHandle.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.Heartbeat)
		{
			num ^= Heartbeat.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.CreateRoomHandle)
		{
			num ^= CreateRoomHandle.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.CreateRoom)
		{
			num ^= CreateRoom.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.SyncRoomHandle)
		{
			num ^= SyncRoomHandle.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.SyncRoom)
		{
			num ^= SyncRoom.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.JoinRoomHandle)
		{
			num ^= JoinRoomHandle.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.JoinRoom)
		{
			num ^= JoinRoom.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.ExitRoomHandle)
		{
			num ^= ExitRoomHandle.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.ExitRoom)
		{
			num ^= ExitRoom.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.QueryRoomHandle)
		{
			num ^= QueryRoomHandle.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.QueryRoom)
		{
			num ^= QueryRoom.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.RefreshRoomStateHandle)
		{
			num ^= RefreshRoomStateHandle.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.RefreshRoomState)
		{
			num ^= RefreshRoomState.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.StartGameHandle)
		{
			num ^= StartGameHandle.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.StartGame)
		{
			num ^= StartGame.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.ThrowDiceHandle)
		{
			num ^= ThrowDiceHandle.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.ThrowDice)
		{
			num ^= ThrowDice.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.ChangeRoomHandle)
		{
			num ^= ChangeRoomHandle.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.ChangeRoom)
		{
			num ^= ChangeRoom.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.MoveHandle)
		{
			num ^= MoveHandle.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.Move)
		{
			num ^= Move.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.ShopBuyHandle)
		{
			num ^= ShopBuyHandle.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.ShopBuy)
		{
			num ^= ShopBuy.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.PursuitHandle)
		{
			num ^= PursuitHandle.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.Pursuit)
		{
			num ^= Pursuit.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.BattleUseCardHandle)
		{
			num ^= BattleUseCardHandle.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.BattleUseCard)
		{
			num ^= BattleUseCard.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.BattleThrowDiceHandle)
		{
			num ^= BattleThrowDiceHandle.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.BattleThrowDice)
		{
			num ^= BattleThrowDice.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.BattleChoiceHandle)
		{
			num ^= BattleChoiceHandle.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.BattleChoice)
		{
			num ^= BattleChoice.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.LotteryChoiceHandle)
		{
			num ^= LotteryChoiceHandle.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.LotteryChoice)
		{
			num ^= LotteryChoice.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.MoveAgainHandle)
		{
			num ^= MoveAgainHandle.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.MoveAgain)
		{
			num ^= MoveAgain.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.AskBattleHandle)
		{
			num ^= AskBattleHandle.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.AskBattle)
		{
			num ^= AskBattle.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.RollGoldHandle)
		{
			num ^= RollGoldHandle.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.RollGold)
		{
			num ^= RollGold.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.EventThrowDiceHandle)
		{
			num ^= EventThrowDiceHandle.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.EventThrowDice)
		{
			num ^= EventThrowDice.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.TriggerEventHandle)
		{
			num ^= TriggerEventHandle.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.TriggerEvent)
		{
			num ^= TriggerEvent.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.UseEffectCardHandle)
		{
			num ^= UseEffectCardHandle.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.UseEffectCard)
		{
			num ^= UseEffectCard.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.BombThrowDiceHandle)
		{
			num ^= BombThrowDiceHandle.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.BombThrowDice)
		{
			num ^= BombThrowDice.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.ChoiceDirectionHandle)
		{
			num ^= ChoiceDirectionHandle.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.ChoiceDirection)
		{
			num ^= ChoiceDirection.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.LandChoiceTargetHandle)
		{
			num ^= LandChoiceTargetHandle.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.LandChoiceTarget)
		{
			num ^= LandChoiceTarget.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.ThrowDiceResultHandle)
		{
			num ^= ThrowDiceResultHandle.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.ThrowDiceResult)
		{
			num ^= ThrowDiceResult.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.TriggerDivinationHandle)
		{
			num ^= TriggerDivinationHandle.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.TriggerDivination)
		{
			num ^= TriggerDivination.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.TriggerDestinyHandle)
		{
			num ^= TriggerDestinyHandle.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.TriggerDestiny)
		{
			num ^= TriggerDestiny.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.UseQuickCardHandle)
		{
			num ^= UseQuickCardHandle.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.UseQuickCard)
		{
			num ^= UseQuickCard.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.AbandonCardHandle)
		{
			num ^= AbandonCardHandle.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.AbandonCard)
		{
			num ^= AbandonCard.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.StopOrContinueHandle)
		{
			num ^= StopOrContinueHandle.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.StopOrContinue)
		{
			num ^= StopOrContinue.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.StartGambleHandle)
		{
			num ^= StartGambleHandle.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.StartGamble)
		{
			num ^= StartGamble.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.GambleThrowDicHandle)
		{
			num ^= GambleThrowDicHandle.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.GambleThrowDic)
		{
			num ^= GambleThrowDic.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.ChoiceHeroC2S2Handle)
		{
			num ^= ChoiceHeroC2S2Handle.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.ChoiceHero2)
		{
			num ^= ChoiceHero2.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.AffirmHeroHandle)
		{
			num ^= AffirmHeroHandle.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.AffirmHero)
		{
			num ^= AffirmHero.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.SearchRoomHandle)
		{
			num ^= SearchRoomHandle.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.SearchRoom)
		{
			num ^= SearchRoom.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.GmHandle)
		{
			num ^= GmHandle.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.Gm)
		{
			num ^= Gm.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.TriggerHospitalHandle)
		{
			num ^= TriggerHospitalHandle.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.TriggerHospital)
		{
			num ^= TriggerHospital.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.SendChatHandle)
		{
			num ^= SendChatHandle.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.SendChat)
		{
			num ^= SendChat.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.PlayerShopBuyC2S)
		{
			num ^= PlayerShopBuyC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.PlayerShopBuyS2C)
		{
			num ^= PlayerShopBuyS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.PlayerUseItemHandle)
		{
			num ^= PlayerUseItemHandle.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.PlayerUseItem)
		{
			num ^= PlayerUseItem.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.UseTreasureC2S)
		{
			num ^= UseTreasureC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.UseTreasureS2C)
		{
			num ^= UseTreasureS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.UseTreasureAutoTransformC2S)
		{
			num ^= UseTreasureAutoTransformC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.UseTreasureAutoTransformS2C)
		{
			num ^= UseTreasureAutoTransformS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.SetFashionC2S)
		{
			num ^= SetFashionC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.SetFashionS2C)
		{
			num ^= SetFashionS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.SelectFashionPlanC2S)
		{
			num ^= SelectFashionPlanC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.SelectFashionPlanS2C)
		{
			num ^= SelectFashionPlanS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.GachaC2S)
		{
			num ^= GachaC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.GachaS2C)
		{
			num ^= GachaS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.SteamSearchRoomC2S)
		{
			num ^= SteamSearchRoomC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.SteamSearchRoomS2C)
		{
			num ^= SteamSearchRoomS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.CheatItemHandle)
		{
			num ^= CheatItemHandle.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.CheatItem)
		{
			num ^= CheatItem.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.RoleCardUpLvC2S)
		{
			num ^= RoleCardUpLvC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.RoleCardUpLvS2C)
		{
			num ^= RoleCardUpLvS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.RoleCardBreakThroughC2S)
		{
			num ^= RoleCardBreakThroughC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.RoleCardBreakThroughS2C)
		{
			num ^= RoleCardBreakThroughS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.RoleCardChoiceResC2S)
		{
			num ^= RoleCardChoiceResC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.RoleCardChoiceResS2C)
		{
			num ^= RoleCardChoiceResS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.TaskRewardC2S)
		{
			num ^= TaskRewardC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.TaskRewardS2C)
		{
			num ^= TaskRewardS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.TeachingC2S)
		{
			num ^= TeachingC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.TeachingS2C)
		{
			num ^= TeachingS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.QuickJoinRoomC2S)
		{
			num ^= QuickJoinRoomC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.QuickJoinRoomS2C)
		{
			num ^= QuickJoinRoomS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.RoomKickPlayerC2S)
		{
			num ^= RoomKickPlayerC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.RoomKickPlayerS2C)
		{
			num ^= RoomKickPlayerS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.RoomAbdicationC2S)
		{
			num ^= RoomAbdicationC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.RoomAbdicationS2C)
		{
			num ^= RoomAbdicationS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.RoomReadyC2S)
		{
			num ^= RoomReadyC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.RoomReadyS2C)
		{
			num ^= RoomReadyS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.ChargeCreateC2S)
		{
			num ^= ChargeCreateC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.ChargeCreateS2C)
		{
			num ^= ChargeCreateS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.ChargeC2S)
		{
			num ^= ChargeC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.ChargeS2C)
		{
			num ^= ChargeS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.GiftCdkC2S)
		{
			num ^= GiftCdkC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.GiftCdkS2C)
		{
			num ^= GiftCdkS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.MailReadC2S)
		{
			num ^= MailReadC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.MailReadS2C)
		{
			num ^= MailReadS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.MailGetRewardC2S)
		{
			num ^= MailGetRewardC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.MailGetRewardS2C)
		{
			num ^= MailGetRewardS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.MailDelReadC2S)
		{
			num ^= MailDelReadC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.MailDelReadS2C)
		{
			num ^= MailDelReadS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.GachaRecordC2S)
		{
			num ^= GachaRecordC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.GachaRecordS2C)
		{
			num ^= GachaRecordS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.ActivityTaskRewardC2S)
		{
			num ^= ActivityTaskRewardC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.ActivityTaskRewardS2C)
		{
			num ^= ActivityTaskRewardS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.RoomShortChatC2S)
		{
			num ^= RoomShortChatC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.RoomShortChatS2C)
		{
			num ^= RoomShortChatS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.SetShowPlayerC2S)
		{
			num ^= SetShowPlayerC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.SetShowPlayerS2C)
		{
			num ^= SetShowPlayerS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.GetShowPlayerC2S)
		{
			num ^= GetShowPlayerC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.GetShowPlayerS2C)
		{
			num ^= GetShowPlayerS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.GetPlayerFightRecordC2S)
		{
			num ^= GetPlayerFightRecordC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.GetPlayerFightRecordS2C)
		{
			num ^= GetPlayerFightRecordS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.GetDay7RewardC2S)
		{
			num ^= GetDay7RewardC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.GetDay7RewardS2C)
		{
			num ^= GetDay7RewardS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.PraisePlayerC2S)
		{
			num ^= PraisePlayerC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.PraisePlayerS2C)
		{
			num ^= PraisePlayerS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.ClientDataUploadC2S)
		{
			num ^= ClientDataUploadC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.ClientDataUploadS2C)
		{
			num ^= ClientDataUploadS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.FriendListC2S)
		{
			num ^= FriendListC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.FriendListS2C)
		{
			num ^= FriendListS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.FriendApplyC2S)
		{
			num ^= FriendApplyC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.FriendApplyS2C)
		{
			num ^= FriendApplyS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.FriendApplyListC2S)
		{
			num ^= FriendApplyListC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.FriendApplyListS2C)
		{
			num ^= FriendApplyListS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.FriendApplyOpC2S)
		{
			num ^= FriendApplyOpC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.FriendApplyOpS2C)
		{
			num ^= FriendApplyOpS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.FriendOpC2S)
		{
			num ^= FriendOpC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.FriendOpS2C)
		{
			num ^= FriendOpS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.FriendInviteC2S)
		{
			num ^= FriendInviteC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.FriendInviteS2C)
		{
			num ^= FriendInviteS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.FriendInviteListC2S)
		{
			num ^= FriendInviteListC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.FriendInviteListS2C)
		{
			num ^= FriendInviteListS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.FriendInviteCleanC2S)
		{
			num ^= FriendInviteCleanC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.FriendInviteCleanS2C)
		{
			num ^= FriendInviteCleanS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.FriendBlacksListC2S)
		{
			num ^= FriendBlacksListC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.FriendBlacksListS2C)
		{
			num ^= FriendBlacksListS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.NearFightPlayerC2S)
		{
			num ^= NearFightPlayerC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.NearFightPlayerS2C)
		{
			num ^= NearFightPlayerS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.SearchPlayerC2S)
		{
			num ^= SearchPlayerC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.SearchPlayerS2C)
		{
			num ^= SearchPlayerS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.ScratchCardC2S)
		{
			num ^= ScratchCardC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.ScratchCardS2C)
		{
			num ^= ScratchCardS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.NextScratchCardPoolC2S)
		{
			num ^= NextScratchCardPoolC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.NextScratchCardPoolS2C)
		{
			num ^= NextScratchCardPoolS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.WatchJoinRoomC2S)
		{
			num ^= WatchJoinRoomC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.WatchJoinRoomS2C)
		{
			num ^= WatchJoinRoomS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.WatchRefreshRoomStateC2S)
		{
			num ^= WatchRefreshRoomStateC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.WatchRefreshRoomStateS2C)
		{
			num ^= WatchRefreshRoomStateS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.WatchExitRoomC2S)
		{
			num ^= WatchExitRoomC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.WatchExitRoomS2C)
		{
			num ^= WatchExitRoomS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.BattlePassGetRewardC2S)
		{
			num ^= BattlePassGetRewardC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.BattlePassGetRewardS2C)
		{
			num ^= BattlePassGetRewardS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.BattlePassTaskRewardC2S)
		{
			num ^= BattlePassTaskRewardC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.BattlePassTaskRewardS2C)
		{
			num ^= BattlePassTaskRewardS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.BattlePassUpLvC2S)
		{
			num ^= BattlePassUpLvC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.BattlePassUpLvS2C)
		{
			num ^= BattlePassUpLvS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.FriendSendMsgC2S)
		{
			num ^= FriendSendMsgC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.FriendSendMsgS2C)
		{
			num ^= FriendSendMsgS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.GetChatMsgC2S)
		{
			num ^= GetChatMsgC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.GetChatMsgS2C)
		{
			num ^= GetChatMsgS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.ReadChatMsgC2S)
		{
			num ^= ReadChatMsgC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.ReadChatMsgS2C)
		{
			num ^= ReadChatMsgS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.DelChatMsgInfoC2S)
		{
			num ^= DelChatMsgInfoC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.DelChatMsgInfoS2C)
		{
			num ^= DelChatMsgInfoS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.SelectRelicC2S)
		{
			num ^= SelectRelicC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.SelectRelicS2C)
		{
			num ^= SelectRelicS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.MonsterPursuitC2S)
		{
			num ^= MonsterPursuitC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.MonsterPursuitS2C)
		{
			num ^= MonsterPursuitS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.PVEShopBuyC2S)
		{
			num ^= PVEShopBuyC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.PVEShopBuyS2C)
		{
			num ^= PVEShopBuyS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.ClientCheckTaskC2S)
		{
			num ^= ClientCheckTaskC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.ClientCheckTaskS2C)
		{
			num ^= ClientCheckTaskS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.PveHeroUpLvC2S)
		{
			num ^= PveHeroUpLvC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.PveHeroUpLvS2C)
		{
			num ^= PveHeroUpLvS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.StartMatchC2S)
		{
			num ^= StartMatchC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.StartMatchS2C)
		{
			num ^= StartMatchS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.CancelMatchC2S)
		{
			num ^= CancelMatchC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.CancelMatchS2C)
		{
			num ^= CancelMatchS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.MatchSuccessC2S)
		{
			num ^= MatchSuccessC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.MatchSuccessS2C)
		{
			num ^= MatchSuccessS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.AccuseC2S)
		{
			num ^= AccuseC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.AccuseS2C)
		{
			num ^= AccuseS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.SingleCampaignC2S)
		{
			num ^= SingleCampaignC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.SingleCampaignS2C)
		{
			num ^= SingleCampaignS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.DevChargeC2S)
		{
			num ^= DevChargeC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.DevChargeS2C)
		{
			num ^= DevChargeS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.AskReviveTeammateC2S)
		{
			num ^= AskReviveTeammateC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.AskReviveTeammateS2C)
		{
			num ^= AskReviveTeammateS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.GetSignInRewardC2S)
		{
			num ^= GetSignInRewardC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.GetSignInRewardS2C)
		{
			num ^= GetSignInRewardS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.ChatMapMarkersC2S)
		{
			num ^= ChatMapMarkersC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.ChatMapMarkersS2C)
		{
			num ^= ChatMapMarkersS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.AbroadCreateOrderC2S)
		{
			num ^= AbroadCreateOrderC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.AbroadCreateOrderS2C)
		{
			num ^= AbroadCreateOrderS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.AgeVerifyC2S)
		{
			num ^= AgeVerifyC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.AgeVerifyS2C)
		{
			num ^= AgeVerifyS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.ChangeNameC2S)
		{
			num ^= ChangeNameC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.ChangeNameS2C)
		{
			num ^= ChangeNameS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.ClientClickConfirmTaskC2S)
		{
			num ^= ClientClickConfirmTaskC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.ClientClickConfirmTaskS2C)
		{
			num ^= ClientClickConfirmTaskS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.BuyRelicC2S)
		{
			num ^= BuyRelicC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.BuyRelicS2C)
		{
			num ^= BuyRelicS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.BuyLightGiftC2S)
		{
			num ^= BuyLightGiftC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.BuyLightGiftS2C)
		{
			num ^= BuyLightGiftS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.LightGiftC2S)
		{
			num ^= LightGiftC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.LightGiftS2C)
		{
			num ^= LightGiftS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.AcquisitionC2S)
		{
			num ^= AcquisitionC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.AcquisitionS2C)
		{
			num ^= AcquisitionS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.AcquisitionRewardC2S)
		{
			num ^= AcquisitionRewardC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.AcquisitionRewardS2C)
		{
			num ^= AcquisitionRewardS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.SelectMechanismC2S)
		{
			num ^= SelectMechanismC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.SelectMechanismS2C)
		{
			num ^= SelectMechanismS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.GachaCountRewardC2S)
		{
			num ^= GachaCountRewardC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.GachaCountRewardS2C)
		{
			num ^= GachaCountRewardS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.GetPlayerSimpleC2S)
		{
			num ^= GetPlayerSimpleC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.GetPlayerSimpleS2C)
		{
			num ^= GetPlayerSimpleS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.RoleCardCollectC2S)
		{
			num ^= RoleCardCollectC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.RoleCardCollectS2C)
		{
			num ^= RoleCardCollectS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.GMPlayerSettingC2S)
		{
			num ^= GMPlayerSettingC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.GMPlayerSettingS2C)
		{
			num ^= GMPlayerSettingS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.SetFriendNoteC2S)
		{
			num ^= SetFriendNoteC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.SetFriendNoteS2C)
		{
			num ^= SetFriendNoteS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.SetOnlineStatusC2S)
		{
			num ^= SetOnlineStatusC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.SetOnlineStatusS2C)
		{
			num ^= SetOnlineStatusS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.ChooseSkinC2S)
		{
			num ^= ChooseSkinC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.ChooseSkinS2C)
		{
			num ^= ChooseSkinS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.TimeWastingC2S)
		{
			num ^= TimeWastingC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.TimeWastingS2C)
		{
			num ^= TimeWastingS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.VoteC2S)
		{
			num ^= VoteC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.VoteS2C)
		{
			num ^= VoteS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.VoteSelectC2S)
		{
			num ^= VoteSelectC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.VoteSelectS2C)
		{
			num ^= VoteSelectS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.NotifyStoryC2S)
		{
			num ^= NotifyStoryC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.NotifyStoryS2C)
		{
			num ^= NotifyStoryS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.PveHeroTalentUpC2S)
		{
			num ^= PveHeroTalentUpC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.PveHeroTalentUpS2C)
		{
			num ^= PveHeroTalentUpS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.SelectEventC2S)
		{
			num ^= SelectEventC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.SelectEventS2C)
		{
			num ^= SelectEventS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.CampScoreC2S)
		{
			num ^= CampScoreC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.CampScoreS2C)
		{
			num ^= CampScoreS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.ActivityMissionRewardC2S)
		{
			num ^= ActivityMissionRewardC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.ActivityMissionRewardS2C)
		{
			num ^= ActivityMissionRewardS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.VendorBuyCardC2S)
		{
			num ^= VendorBuyCardC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.VendorBuyCardS2C)
		{
			num ^= VendorBuyCardS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.TransferStarDiscC2S)
		{
			num ^= TransferStarDiscC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.TransferStarDiscS2C)
		{
			num ^= TransferStarDiscS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.GetHeroInfoC2S)
		{
			num ^= GetHeroInfoC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.GetHeroInfoS2C)
		{
			num ^= GetHeroInfoS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.CreateMatchTeamC2S)
		{
			num ^= CreateMatchTeamC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.CreateMatchTeamS2C)
		{
			num ^= CreateMatchTeamS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.ChangeMatchTeamC2S)
		{
			num ^= ChangeMatchTeamC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.ChangeMatchTeamS2C)
		{
			num ^= ChangeMatchTeamS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.JoinMatchTeamC2S)
		{
			num ^= JoinMatchTeamC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.JoinMatchTeamS2C)
		{
			num ^= JoinMatchTeamS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.ExitMatchTeamC2S)
		{
			num ^= ExitMatchTeamC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.ExitMatchTeamS2C)
		{
			num ^= ExitMatchTeamS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.RefreshMatchTeamInfoC2S)
		{
			num ^= RefreshMatchTeamInfoC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.RefreshMatchTeamInfoS2C)
		{
			num ^= RefreshMatchTeamInfoS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.ChinaCreateOrderC2S)
		{
			num ^= ChinaCreateOrderC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.ChinaCreateOrderS2C)
		{
			num ^= ChinaCreateOrderS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.MatchTeamInviteC2S)
		{
			num ^= MatchTeamInviteC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.MatchTeamInviteS2C)
		{
			num ^= MatchTeamInviteS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.MatchTeamChatC2S)
		{
			num ^= MatchTeamChatC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.MatchTeamChatS2C)
		{
			num ^= MatchTeamChatS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.MatchTeamReadyC2S)
		{
			num ^= MatchTeamReadyC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.MatchTeamReadyS2C)
		{
			num ^= MatchTeamReadyS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.PlayerChatC2S)
		{
			num ^= PlayerChatC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.PlayerChatS2C)
		{
			num ^= PlayerChatS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.SyncSingleGameDataC2S)
		{
			num ^= SyncSingleGameDataC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.SyncSingleGameDataS2C)
		{
			num ^= SyncSingleGameDataS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.SingleGameDataC2S)
		{
			num ^= SingleGameDataC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.SingleGameDataS2C)
		{
			num ^= SingleGameDataS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.GetActivityPassRewardC2S)
		{
			num ^= GetActivityPassRewardC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.GetActivityPassRewardS2C)
		{
			num ^= GetActivityPassRewardS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.ApplyChangeSlotC2S)
		{
			num ^= ApplyChangeSlotC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.ApplyChangeSlotS2C)
		{
			num ^= ApplyChangeSlotS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.OpsChangeSlotC2S)
		{
			num ^= OpsChangeSlotC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.OpsChangeSlotS2C)
		{
			num ^= OpsChangeSlotS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.RookieGachaRewardC2S)
		{
			num ^= RookieGachaRewardC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.RookieGachaRewardS2C)
		{
			num ^= RookieGachaRewardS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.LiveGiftPackageC2S)
		{
			num ^= LiveGiftPackageC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.LiveGiftPackageS2C)
		{
			num ^= LiveGiftPackageS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.LaborActDiceC2S)
		{
			num ^= LaborActDiceC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.LaborActDiceS2C)
		{
			num ^= LaborActDiceS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.ActionOverTimeLogC2S)
		{
			num ^= ActionOverTimeLogC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.ActionOverTimeLogS2C)
		{
			num ^= ActionOverTimeLogS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.ClientHarmonyC2S)
		{
			num ^= ClientHarmonyC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.ClientHarmonyS2C)
		{
			num ^= ClientHarmonyS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.MailStarC2S)
		{
			num ^= MailStarC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.MailStarS2C)
		{
			num ^= MailStarS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.SetCardAltArtC2S)
		{
			num ^= SetCardAltArtC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.SetCardAltArtS2C)
		{
			num ^= SetCardAltArtS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.SelectRewardCardC2S)
		{
			num ^= SelectRewardCardC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.SelectRewardCardS2C)
		{
			num ^= SelectRewardCardS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.GetQuestionUrlC2S)
		{
			num ^= GetQuestionUrlC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.GetQuestionUrlS2C)
		{
			num ^= GetQuestionUrlS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.GetReturnInfoC2S)
		{
			num ^= GetReturnInfoC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.GetReturnInfoS2C)
		{
			num ^= GetReturnInfoS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.ReturnGiftClaimC2S)
		{
			num ^= ReturnGiftClaimC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.ReturnGiftClaimS2C)
		{
			num ^= ReturnGiftClaimS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.ReturnSignInClaimC2S)
		{
			num ^= ReturnSignInClaimC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.ReturnSignInClaimS2C)
		{
			num ^= ReturnSignInClaimS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.ReturnSurveyFinishC2S)
		{
			num ^= ReturnSurveyFinishC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.ReturnSurveyFinishS2C)
		{
			num ^= ReturnSurveyFinishS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.FlipCardC2S)
		{
			num ^= FlipCardC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.FlipCardS2C)
		{
			num ^= FlipCardS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.FlipCardProgressRewardC2S)
		{
			num ^= FlipCardProgressRewardC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.FlipCardProgressRewardS2C)
		{
			num ^= FlipCardProgressRewardS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.SyncPlayerGuildS2C)
		{
			num ^= SyncPlayerGuildS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.SyncPlayerJoinGuildS2C)
		{
			num ^= SyncPlayerJoinGuildS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.GuildChatMsgS2C)
		{
			num ^= GuildChatMsgS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.SyncGuildS2C)
		{
			num ^= SyncGuildS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.SyncGuildMemberS2C)
		{
			num ^= SyncGuildMemberS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.SyncGuildMemberExitS2C)
		{
			num ^= SyncGuildMemberExitS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.CreateGuildC2S)
		{
			num ^= CreateGuildC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.CreateGuildS2C)
		{
			num ^= CreateGuildS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.SearchGuildC2S)
		{
			num ^= SearchGuildC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.SearchGuildS2C)
		{
			num ^= SearchGuildS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.ApplyToGuildC2S)
		{
			num ^= ApplyToGuildC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.ApplyToGuildS2C)
		{
			num ^= ApplyToGuildS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.ProcessGuildApplicationC2S)
		{
			num ^= ProcessGuildApplicationC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.ProcessGuildApplicationS2C)
		{
			num ^= ProcessGuildApplicationS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.SendGuildInvitationC2S)
		{
			num ^= SendGuildInvitationC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.SendGuildInvitationS2C)
		{
			num ^= SendGuildInvitationS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.ProcessGuildInvitationC2S)
		{
			num ^= ProcessGuildInvitationC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.ProcessGuildInvitationS2C)
		{
			num ^= ProcessGuildInvitationS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.GetGuildInfoC2S)
		{
			num ^= GetGuildInfoC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.GetGuildInfoS2C)
		{
			num ^= GetGuildInfoS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.UpdateGuildSettingsC2S)
		{
			num ^= UpdateGuildSettingsC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.UpdateGuildSettingsS2C)
		{
			num ^= UpdateGuildSettingsS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.UpdateGuildInAnnouncementC2S)
		{
			num ^= UpdateGuildInAnnouncementC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.UpdateGuildInAnnouncementS2C)
		{
			num ^= UpdateGuildInAnnouncementS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.TransferGuildMasterC2S)
		{
			num ^= TransferGuildMasterC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.TransferGuildMasterS2C)
		{
			num ^= TransferGuildMasterS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.ChangeGuildMemberTitleC2S)
		{
			num ^= ChangeGuildMemberTitleC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.ChangeGuildMemberTitleS2C)
		{
			num ^= ChangeGuildMemberTitleS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.KickGuildMemberC2S)
		{
			num ^= KickGuildMemberC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.KickGuildMemberS2C)
		{
			num ^= KickGuildMemberS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.ImpeachGuildMasterC2S)
		{
			num ^= ImpeachGuildMasterC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.ImpeachGuildMasterS2C)
		{
			num ^= ImpeachGuildMasterS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.ExitGuildC2S)
		{
			num ^= ExitGuildC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.ExitGuildS2C)
		{
			num ^= ExitGuildS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.DisbandGuildC2S)
		{
			num ^= DisbandGuildC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.DisbandGuildS2C)
		{
			num ^= DisbandGuildS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.GuildMissionRewardC2S)
		{
			num ^= GuildMissionRewardC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.GuildMissionRewardS2C)
		{
			num ^= GuildMissionRewardS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.GetGuildMemberChangeMsgC2S)
		{
			num ^= GetGuildMemberChangeMsgC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.GetGuildMemberChangeMsgS2C)
		{
			num ^= GetGuildMemberChangeMsgS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.SendGuildChatMsgC2S)
		{
			num ^= SendGuildChatMsgC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.SendGuildChatMsgS2C)
		{
			num ^= SendGuildChatMsgS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.GetGuildChatMsgC2S)
		{
			num ^= GetGuildChatMsgC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.GetGuildChatMsgS2C)
		{
			num ^= GetGuildChatMsgS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.GuildMemberC2S)
		{
			num ^= GuildMemberC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.GuildMemberS2C)
		{
			num ^= GuildMemberS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.GetGuildsInfoC2S)
		{
			num ^= GetGuildsInfoC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.GetGuildsInfoS2C)
		{
			num ^= GetGuildsInfoS2C.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.TestRpcEchoC2S)
		{
			num ^= TestRpcEchoC2S.GetHashCode();
		}
		if (msgCase_ == MsgOneofCase.TestRpcEchoS2C)
		{
			num ^= TestRpcEchoS2C.GetHashCode();
		}
		num ^= (int)msgCase_;
		if (_unknownFields != null)
		{
			num ^= _unknownFields.GetHashCode();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public override string ToString()
	{
		return JsonFormatter.ToDiagnosticString(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void WriteTo(CodedOutputStream output)
	{
		output.WriteRawMessage(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	void IBufferMessage.InternalWriteTo(ref WriteContext output)
	{
		if (msgCase_ == MsgOneofCase.SysSendMailC2S)
		{
			output.WriteRawTag(170, 6);
			output.WriteMessage(SysSendMailC2S);
		}
		if (msgCase_ == MsgOneofCase.SysPlayerOnlineC2S)
		{
			output.WriteRawTag(178, 6);
			output.WriteMessage(SysPlayerOnlineC2S);
		}
		if (msgCase_ == MsgOneofCase.PlayerOnlineRoomC2S)
		{
			output.WriteRawTag(186, 6);
			output.WriteMessage(PlayerOnlineRoomC2S);
		}
		if (msgCase_ == MsgOneofCase.SysCanPraiseInfoC2S)
		{
			output.WriteRawTag(194, 6);
			output.WriteMessage(SysCanPraiseInfoC2S);
		}
		if (msgCase_ == MsgOneofCase.SysPraiseC2S)
		{
			output.WriteRawTag(202, 6);
			output.WriteMessage(SysPraiseC2S);
		}
		if (msgCase_ == MsgOneofCase.SysRoomFinishC2S)
		{
			output.WriteRawTag(210, 6);
			output.WriteMessage(SysRoomFinishC2S);
		}
		if (msgCase_ == MsgOneofCase.SysRoomAddExpC2S)
		{
			output.WriteRawTag(218, 6);
			output.WriteMessage(SysRoomAddExpC2S);
		}
		if (msgCase_ == MsgOneofCase.SysGetShowFriendC2S)
		{
			output.WriteRawTag(226, 6);
			output.WriteMessage(SysGetShowFriendC2S);
		}
		if (msgCase_ == MsgOneofCase.SysFriendInviteC2S)
		{
			output.WriteRawTag(234, 6);
			output.WriteMessage(SysFriendInviteC2S);
		}
		if (msgCase_ == MsgOneofCase.SysFriendDelC2S)
		{
			output.WriteRawTag(242, 6);
			output.WriteMessage(SysFriendDelC2S);
		}
		if (msgCase_ == MsgOneofCase.SysFriendInfoC2S)
		{
			output.WriteRawTag(250, 6);
			output.WriteMessage(SysFriendInfoC2S);
		}
		if (msgCase_ == MsgOneofCase.SysFriendApplyC2S)
		{
			output.WriteRawTag(130, 7);
			output.WriteMessage(SysFriendApplyC2S);
		}
		if (msgCase_ == MsgOneofCase.SysFriendAddC2S)
		{
			output.WriteRawTag(138, 7);
			output.WriteMessage(SysFriendAddC2S);
		}
		if (msgCase_ == MsgOneofCase.SysFriendSendMsgC2S)
		{
			output.WriteRawTag(146, 7);
			output.WriteMessage(SysFriendSendMsgC2S);
		}
		if (msgCase_ == MsgOneofCase.SysCampaignFinishC2S)
		{
			output.WriteRawTag(154, 7);
			output.WriteMessage(SysCampaignFinishC2S);
		}
		if (msgCase_ == MsgOneofCase.SysAbroadPayMsgC2S)
		{
			output.WriteRawTag(162, 7);
			output.WriteMessage(SysAbroadPayMsgC2S);
		}
		if (msgCase_ == MsgOneofCase.AcquisitionMsgC2S)
		{
			output.WriteRawTag(170, 7);
			output.WriteMessage(AcquisitionMsgC2S);
		}
		if (msgCase_ == MsgOneofCase.SysFightRecord)
		{
			output.WriteRawTag(178, 7);
			output.WriteMessage(SysFightRecord);
		}
		if (msgCase_ == MsgOneofCase.SysCampaignAwardC2S)
		{
			output.WriteRawTag(186, 7);
			output.WriteMessage(SysCampaignAwardC2S);
		}
		if (msgCase_ == MsgOneofCase.SysRecoupItemC2S)
		{
			output.WriteRawTag(194, 7);
			output.WriteMessage(SysRecoupItemC2S);
		}
		if (msgCase_ == MsgOneofCase.SysPlayerOnlineRoomC2S)
		{
			output.WriteRawTag(202, 7);
			output.WriteMessage(SysPlayerOnlineRoomC2S);
		}
		if (msgCase_ == MsgOneofCase.SysPlayerCleanC2S)
		{
			output.WriteRawTag(210, 7);
			output.WriteMessage(SysPlayerCleanC2S);
		}
		if (msgCase_ == MsgOneofCase.SysPlayerPunishmentTimeC2S)
		{
			output.WriteRawTag(218, 7);
			output.WriteMessage(SysPlayerPunishmentTimeC2S);
		}
		if (msgCase_ == MsgOneofCase.SysMatchSuccess)
		{
			output.WriteRawTag(226, 7);
			output.WriteMessage(SysMatchSuccess);
		}
		if (msgCase_ == MsgOneofCase.SysChinaPayMsgC2S)
		{
			output.WriteRawTag(234, 7);
			output.WriteMessage(SysChinaPayMsgC2S);
		}
		if (msgCase_ == MsgOneofCase.SysChangeMatchTeamState)
		{
			output.WriteRawTag(242, 7);
			output.WriteMessage(SysChangeMatchTeamState);
		}
		if (msgCase_ == MsgOneofCase.SysSaveSimplePlayerInfoC2S)
		{
			output.WriteRawTag(250, 7);
			output.WriteMessage(SysSaveSimplePlayerInfoC2S);
		}
		if (msgCase_ == MsgOneofCase.SysGmChangeNameC2S)
		{
			output.WriteRawTag(130, 8);
			output.WriteMessage(SysGmChangeNameC2S);
		}
		if (msgCase_ == MsgOneofCase.SysSyncPlayerC2S)
		{
			output.WriteRawTag(138, 8);
			output.WriteMessage(SysSyncPlayerC2S);
		}
		if (msgCase_ == MsgOneofCase.SysPlayerCreditScoreChangeC2S)
		{
			output.WriteRawTag(146, 8);
			output.WriteMessage(SysPlayerCreditScoreChangeC2S);
		}
		if (msgCase_ == MsgOneofCase.SysSyncPlayerMatchPunishmentTimeC2S)
		{
			output.WriteRawTag(154, 8);
			output.WriteMessage(SysSyncPlayerMatchPunishmentTimeC2S);
		}
		if (msgCase_ == MsgOneofCase.GMChangeCreditScoreC2S)
		{
			output.WriteRawTag(162, 8);
			output.WriteMessage(GMChangeCreditScoreC2S);
		}
		if (msgCase_ == MsgOneofCase.SysPushReturnInfoC2S)
		{
			output.WriteRawTag(170, 8);
			output.WriteMessage(SysPushReturnInfoC2S);
		}
		if (msgCase_ == MsgOneofCase.SysMutePlayerC2S)
		{
			output.WriteRawTag(178, 8);
			output.WriteMessage(SysMutePlayerC2S);
		}
		if (msgCase_ == MsgOneofCase.SysQuestionC2S)
		{
			output.WriteRawTag(186, 8);
			output.WriteMessage(SysQuestionC2S);
		}
		if (msgCase_ == MsgOneofCase.KickS2C)
		{
			output.WriteRawTag(202, 62);
			output.WriteMessage(KickS2C);
		}
		if (msgCase_ == MsgOneofCase.PredictActionS2C)
		{
			output.WriteRawTag(210, 62);
			output.WriteMessage(PredictActionS2C);
		}
		if (msgCase_ == MsgOneofCase.RunningGameS2C)
		{
			output.WriteRawTag(218, 62);
			output.WriteMessage(RunningGameS2C);
		}
		if (msgCase_ == MsgOneofCase.BattleS2C)
		{
			output.WriteRawTag(250, 62);
			output.WriteMessage(BattleS2C);
		}
		if (msgCase_ == MsgOneofCase.LotteryDrawS2C)
		{
			output.WriteRawTag(154, 63);
			output.WriteMessage(LotteryDrawS2C);
		}
		if (msgCase_ == MsgOneofCase.LandBuffsS2C)
		{
			output.WriteRawTag(170, 63);
			output.WriteMessage(LandBuffsS2C);
		}
		if (msgCase_ == MsgOneofCase.RoundStartS2C)
		{
			output.WriteRawTag(186, 63);
			output.WriteMessage(RoundStartS2C);
		}
		if (msgCase_ == MsgOneofCase.GameFinishS2C)
		{
			output.WriteRawTag(194, 63);
			output.WriteMessage(GameFinishS2C);
		}
		if (msgCase_ == MsgOneofCase.MonsterRefreshS2C)
		{
			output.WriteRawTag(210, 63);
			output.WriteMessage(MonsterRefreshS2C);
		}
		if (msgCase_ == MsgOneofCase.MovePointBuffS2C)
		{
			output.WriteRawTag(218, 63);
			output.WriteMessage(MovePointBuffS2C);
		}
		if (msgCase_ == MsgOneofCase.ChangeDirS2C)
		{
			output.WriteRawTag(226, 63);
			output.WriteMessage(ChangeDirS2C);
		}
		if (msgCase_ == MsgOneofCase.GambleChangeS2C)
		{
			output.WriteRawTag(242, 63);
			output.WriteMessage(GambleChangeS2C);
		}
		if (msgCase_ == MsgOneofCase.HeroBarBoxChangeS2C)
		{
			output.WriteRawTag(250, 63);
			output.WriteMessage(HeroBarBoxChangeS2C);
		}
		if (msgCase_ == MsgOneofCase.RoomNotifyS2C)
		{
			output.WriteRawTag(130, 64);
			output.WriteMessage(RoomNotifyS2C);
		}
		if (msgCase_ == MsgOneofCase.ActionStartNotifyS2C)
		{
			output.WriteRawTag(146, 64);
			output.WriteMessage(ActionStartNotifyS2C);
		}
		if (msgCase_ == MsgOneofCase.NoGambleNotifyS2C)
		{
			output.WriteRawTag(186, 64);
			output.WriteMessage(NoGambleNotifyS2C);
		}
		if (msgCase_ == MsgOneofCase.GambleObServeS2C)
		{
			output.WriteRawTag(210, 64);
			output.WriteMessage(GambleObServeS2C);
		}
		if (msgCase_ == MsgOneofCase.UpdateHeroAttrS2C)
		{
			output.WriteRawTag(130, 65);
			output.WriteMessage(UpdateHeroAttrS2C);
		}
		if (msgCase_ == MsgOneofCase.ChangePlayerSlotS2C)
		{
			output.WriteRawTag(146, 65);
			output.WriteMessage(ChangePlayerSlotS2C);
		}
		if (msgCase_ == MsgOneofCase.BossSleepS2C)
		{
			output.WriteRawTag(154, 65);
			output.WriteMessage(BossSleepS2C);
		}
		if (msgCase_ == MsgOneofCase.RefMallS2C)
		{
			output.WriteRawTag(162, 65);
			output.WriteMessage(RefMallS2C);
		}
		if (msgCase_ == MsgOneofCase.BagItemChangeS2C)
		{
			output.WriteRawTag(170, 65);
			output.WriteMessage(BagItemChangeS2C);
		}
		if (msgCase_ == MsgOneofCase.RoleCardChangeS2C)
		{
			output.WriteRawTag(178, 65);
			output.WriteMessage(RoleCardChangeS2C);
		}
		if (msgCase_ == MsgOneofCase.TaskConditionS2C)
		{
			output.WriteRawTag(186, 65);
			output.WriteMessage(TaskConditionS2C);
		}
		if (msgCase_ == MsgOneofCase.TaskInfoS2C)
		{
			output.WriteRawTag(194, 65);
			output.WriteMessage(TaskInfoS2C);
		}
		if (msgCase_ == MsgOneofCase.PlayerOnlineS2C)
		{
			output.WriteRawTag(202, 65);
			output.WriteMessage(PlayerOnlineS2C);
		}
		if (msgCase_ == MsgOneofCase.ChangeExpS2C)
		{
			output.WriteRawTag(210, 65);
			output.WriteMessage(ChangeExpS2C);
		}
		if (msgCase_ == MsgOneofCase.MailAddS2C)
		{
			output.WriteRawTag(218, 65);
			output.WriteMessage(MailAddS2C);
		}
		if (msgCase_ == MsgOneofCase.OnlineSyncRoomIdS2C)
		{
			output.WriteRawTag(226, 65);
			output.WriteMessage(OnlineSyncRoomIdS2C);
		}
		if (msgCase_ == MsgOneofCase.NoticeS2C)
		{
			output.WriteRawTag(234, 65);
			output.WriteMessage(NoticeS2C);
		}
		if (msgCase_ == MsgOneofCase.ActivityTaskConditionS2C)
		{
			output.WriteRawTag(242, 65);
			output.WriteMessage(ActivityTaskConditionS2C);
		}
		if (msgCase_ == MsgOneofCase.MapEventS2C)
		{
			output.WriteRawTag(250, 65);
			output.WriteMessage(MapEventS2C);
		}
		if (msgCase_ == MsgOneofCase.Day7RewardS2C)
		{
			output.WriteRawTag(130, 66);
			output.WriteMessage(Day7RewardS2C);
		}
		if (msgCase_ == MsgOneofCase.MapEventTrainS2C)
		{
			output.WriteRawTag(138, 66);
			output.WriteMessage(MapEventTrainS2C);
		}
		if (msgCase_ == MsgOneofCase.ChangePraiseNumS2C)
		{
			output.WriteRawTag(146, 66);
			output.WriteMessage(ChangePraiseNumS2C);
		}
		if (msgCase_ == MsgOneofCase.MonthlyCardS2C)
		{
			output.WriteRawTag(154, 66);
			output.WriteMessage(MonthlyCardS2C);
		}
		if (msgCase_ == MsgOneofCase.MailDelS2C)
		{
			output.WriteRawTag(162, 66);
			output.WriteMessage(MailDelS2C);
		}
		if (msgCase_ == MsgOneofCase.FriendNotifyS2C)
		{
			output.WriteRawTag(170, 66);
			output.WriteMessage(FriendNotifyS2C);
		}
		if (msgCase_ == MsgOneofCase.FriendListChangeS2C)
		{
			output.WriteRawTag(178, 66);
			output.WriteMessage(FriendListChangeS2C);
		}
		if (msgCase_ == MsgOneofCase.FriendInviteNotifyS2C)
		{
			output.WriteRawTag(186, 66);
			output.WriteMessage(FriendInviteNotifyS2C);
		}
		if (msgCase_ == MsgOneofCase.LoopNoticeS2C)
		{
			output.WriteRawTag(194, 66);
			output.WriteMessage(LoopNoticeS2C);
		}
		if (msgCase_ == MsgOneofCase.BattlePassLvS2C)
		{
			output.WriteRawTag(202, 66);
			output.WriteMessage(BattlePassLvS2C);
		}
		if (msgCase_ == MsgOneofCase.BattlePassTaskInfoS2C)
		{
			output.WriteRawTag(210, 66);
			output.WriteMessage(BattlePassTaskInfoS2C);
		}
		if (msgCase_ == MsgOneofCase.BattlePassUpdateTaskS2C)
		{
			output.WriteRawTag(218, 66);
			output.WriteMessage(BattlePassUpdateTaskS2C);
		}
		if (msgCase_ == MsgOneofCase.BattlePassBuyS2C)
		{
			output.WriteRawTag(226, 66);
			output.WriteMessage(BattlePassBuyS2C);
		}
		if (msgCase_ == MsgOneofCase.BattlePassInfoS2C)
		{
			output.WriteRawTag(234, 66);
			output.WriteMessage(BattlePassInfoS2C);
		}
		if (msgCase_ == MsgOneofCase.FriendsChatMsgS2C)
		{
			output.WriteRawTag(242, 66);
			output.WriteMessage(FriendsChatMsgS2C);
		}
		if (msgCase_ == MsgOneofCase.GameProgressChangeS2C)
		{
			output.WriteRawTag(250, 66);
			output.WriteMessage(GameProgressChangeS2C);
		}
		if (msgCase_ == MsgOneofCase.MapMissionNotifyS2C)
		{
			output.WriteRawTag(130, 67);
			output.WriteMessage(MapMissionNotifyS2C);
		}
		if (msgCase_ == MsgOneofCase.ChangeItemLimitS2C)
		{
			output.WriteRawTag(138, 67);
			output.WriteMessage(ChangeItemLimitS2C);
		}
		if (msgCase_ == MsgOneofCase.CleanItemLimitS2C)
		{
			output.WriteRawTag(146, 67);
			output.WriteMessage(CleanItemLimitS2C);
		}
		if (msgCase_ == MsgOneofCase.CampaignPassS2C)
		{
			output.WriteRawTag(154, 67);
			output.WriteMessage(CampaignPassS2C);
		}
		if (msgCase_ == MsgOneofCase.CampaignNotifyS2C)
		{
			output.WriteRawTag(162, 67);
			output.WriteMessage(CampaignNotifyS2C);
		}
		if (msgCase_ == MsgOneofCase.KillMessageS2C)
		{
			output.WriteRawTag(170, 67);
			output.WriteMessage(KillMessageS2C);
		}
		if (msgCase_ == MsgOneofCase.GameScoreChangeS2C)
		{
			output.WriteRawTag(178, 67);
			output.WriteMessage(GameScoreChangeS2C);
		}
		if (msgCase_ == MsgOneofCase.SignInRewardS2C)
		{
			output.WriteRawTag(186, 67);
			output.WriteMessage(SignInRewardS2C);
		}
		if (msgCase_ == MsgOneofCase.PayResultS2C)
		{
			output.WriteRawTag(194, 67);
			output.WriteMessage(PayResultS2C);
		}
		if (msgCase_ == MsgOneofCase.PayInfoChangeS2C)
		{
			output.WriteRawTag(202, 67);
			output.WriteMessage(PayInfoChangeS2C);
		}
		if (msgCase_ == MsgOneofCase.InviteSuccessS2C)
		{
			output.WriteRawTag(210, 67);
			output.WriteMessage(InviteSuccessS2C);
		}
		if (msgCase_ == MsgOneofCase.InviteInfoNotifyS2C)
		{
			output.WriteRawTag(218, 67);
			output.WriteMessage(InviteInfoNotifyS2C);
		}
		if (msgCase_ == MsgOneofCase.MapStatusChangeS2C)
		{
			output.WriteRawTag(226, 67);
			output.WriteMessage(MapStatusChangeS2C);
		}
		if (msgCase_ == MsgOneofCase.GachaCountS2C)
		{
			output.WriteRawTag(234, 67);
			output.WriteMessage(GachaCountS2C);
		}
		if (msgCase_ == MsgOneofCase.MapIndexChangeS2C)
		{
			output.WriteRawTag(242, 67);
			output.WriteMessage(MapIndexChangeS2C);
		}
		if (msgCase_ == MsgOneofCase.SurrenderPunishS2C)
		{
			output.WriteRawTag(250, 67);
			output.WriteMessage(SurrenderPunishS2C);
		}
		if (msgCase_ == MsgOneofCase.GamePassMapSuccessS2C)
		{
			output.WriteRawTag(130, 68);
			output.WriteMessage(GamePassMapSuccessS2C);
		}
		if (msgCase_ == MsgOneofCase.FriendDelNotifyS2C)
		{
			output.WriteRawTag(146, 68);
			output.WriteMessage(FriendDelNotifyS2C);
		}
		if (msgCase_ == MsgOneofCase.BagExpiredTransformNotify)
		{
			output.WriteRawTag(154, 68);
			output.WriteMessage(BagExpiredTransformNotify);
		}
		if (msgCase_ == MsgOneofCase.MapEventCrabS2C)
		{
			output.WriteRawTag(162, 68);
			output.WriteMessage(MapEventCrabS2C);
		}
		if (msgCase_ == MsgOneofCase.PkAfterVoteS2C)
		{
			output.WriteRawTag(170, 68);
			output.WriteMessage(PkAfterVoteS2C);
		}
		if (msgCase_ == MsgOneofCase.PlayerTaskNotifyS2C)
		{
			output.WriteRawTag(178, 68);
			output.WriteMessage(PlayerTaskNotifyS2C);
		}
		if (msgCase_ == MsgOneofCase.UnLockDifficultyS2C)
		{
			output.WriteRawTag(186, 68);
			output.WriteMessage(UnLockDifficultyS2C);
		}
		if (msgCase_ == MsgOneofCase.HeroSkillMoveEffectS2C)
		{
			output.WriteRawTag(194, 68);
			output.WriteMessage(HeroSkillMoveEffectS2C);
		}
		if (msgCase_ == MsgOneofCase.TimeOutKickPlayerS2C)
		{
			output.WriteRawTag(202, 68);
			output.WriteMessage(TimeOutKickPlayerS2C);
		}
		if (msgCase_ == MsgOneofCase.SayPhraseNotifyS2C)
		{
			output.WriteRawTag(210, 68);
			output.WriteMessage(SayPhraseNotifyS2C);
		}
		if (msgCase_ == MsgOneofCase.MatchTeamInviteNotify)
		{
			output.WriteRawTag(218, 68);
			output.WriteMessage(MatchTeamInviteNotify);
		}
		if (msgCase_ == MsgOneofCase.RefreshMatchTeamStateNotify)
		{
			output.WriteRawTag(226, 68);
			output.WriteMessage(RefreshMatchTeamStateNotify);
		}
		if (msgCase_ == MsgOneofCase.ActivityPassGearChangeS2C)
		{
			output.WriteRawTag(234, 68);
			output.WriteMessage(ActivityPassGearChangeS2C);
		}
		if (msgCase_ == MsgOneofCase.SingleGameScoreChange)
		{
			output.WriteRawTag(242, 68);
			output.WriteMessage(SingleGameScoreChange);
		}
		if (msgCase_ == MsgOneofCase.DelayProgressMapEventS2C)
		{
			output.WriteRawTag(250, 68);
			output.WriteMessage(DelayProgressMapEventS2C);
		}
		if (msgCase_ == MsgOneofCase.LuckyStarMissionChangeS2C)
		{
			output.WriteRawTag(130, 69);
			output.WriteMessage(LuckyStarMissionChangeS2C);
		}
		if (msgCase_ == MsgOneofCase.MatchPunishmentS2C)
		{
			output.WriteRawTag(138, 69);
			output.WriteMessage(MatchPunishmentS2C);
		}
		if (msgCase_ == MsgOneofCase.ChallengeDataChangeS2C)
		{
			output.WriteRawTag(146, 69);
			output.WriteMessage(ChallengeDataChangeS2C);
		}
		if (msgCase_ == MsgOneofCase.GmUnlockRoleInfoS2C)
		{
			output.WriteRawTag(154, 69);
			output.WriteMessage(GmUnlockRoleInfoS2C);
		}
		if (msgCase_ == MsgOneofCase.SyncPlayerCreditInfoS2C)
		{
			output.WriteRawTag(162, 69);
			output.WriteMessage(SyncPlayerCreditInfoS2C);
		}
		if (msgCase_ == MsgOneofCase.RoomHeroCardChangeS2C)
		{
			output.WriteRawTag(170, 69);
			output.WriteMessage(RoomHeroCardChangeS2C);
		}
		if (msgCase_ == MsgOneofCase.RoomRoundAddTermS2C)
		{
			output.WriteRawTag(178, 69);
			output.WriteMessage(RoomRoundAddTermS2C);
		}
		if (msgCase_ == MsgOneofCase.ReturnInfoS2C)
		{
			output.WriteRawTag(186, 69);
			output.WriteMessage(ReturnInfoS2C);
		}
		if (msgCase_ == MsgOneofCase.SyncRelicsS2C)
		{
			output.WriteRawTag(194, 69);
			output.WriteMessage(SyncRelicsS2C);
		}
		if (msgCase_ == MsgOneofCase.ReplaySnapshotS2C)
		{
			output.WriteRawTag(202, 69);
			output.WriteMessage(ReplaySnapshotS2C);
		}
		if (msgCase_ == MsgOneofCase.ClueNotifyS2C)
		{
			output.WriteRawTag(210, 69);
			output.WriteMessage(ClueNotifyS2C);
		}
		if (msgCase_ == MsgOneofCase.ReplayDieS2C)
		{
			output.WriteRawTag(218, 69);
			output.WriteMessage(ReplayDieS2C);
		}
		if (msgCase_ == MsgOneofCase.GuildTaskNotifyS2C)
		{
			output.WriteRawTag(226, 69);
			output.WriteMessage(GuildTaskNotifyS2C);
		}
		if (msgCase_ == MsgOneofCase.GameRoundChangeS2C)
		{
			output.WriteRawTag(234, 69);
			output.WriteMessage(GameRoundChangeS2C);
		}
		if (msgCase_ == MsgOneofCase.NotifyQuestionS2C)
		{
			output.WriteRawTag(242, 69);
			output.WriteMessage(NotifyQuestionS2C);
		}
		if (msgCase_ == MsgOneofCase.SyncPlayerGuildS2C)
		{
			output.WriteRawTag(138, 125);
			output.WriteMessage(SyncPlayerGuildS2C);
		}
		if (msgCase_ == MsgOneofCase.SyncPlayerJoinGuildS2C)
		{
			output.WriteRawTag(146, 125);
			output.WriteMessage(SyncPlayerJoinGuildS2C);
		}
		if (msgCase_ == MsgOneofCase.GuildChatMsgS2C)
		{
			output.WriteRawTag(154, 125);
			output.WriteMessage(GuildChatMsgS2C);
		}
		if (msgCase_ == MsgOneofCase.SyncGuildS2C)
		{
			output.WriteRawTag(162, 125);
			output.WriteMessage(SyncGuildS2C);
		}
		if (msgCase_ == MsgOneofCase.SyncGuildMemberS2C)
		{
			output.WriteRawTag(170, 125);
			output.WriteMessage(SyncGuildMemberS2C);
		}
		if (msgCase_ == MsgOneofCase.SyncGuildMemberExitS2C)
		{
			output.WriteRawTag(178, 125);
			output.WriteMessage(SyncGuildMemberExitS2C);
		}
		if (msgCase_ == MsgOneofCase.ConnectHandle)
		{
			output.WriteRawTag(202, 184, 2);
			output.WriteMessage(ConnectHandle);
		}
		if (msgCase_ == MsgOneofCase.Connect)
		{
			output.WriteRawTag(210, 184, 2);
			output.WriteMessage(Connect);
		}
		if (msgCase_ == MsgOneofCase.HeartbeatHandle)
		{
			output.WriteRawTag(218, 184, 2);
			output.WriteMessage(HeartbeatHandle);
		}
		if (msgCase_ == MsgOneofCase.Heartbeat)
		{
			output.WriteRawTag(226, 184, 2);
			output.WriteMessage(Heartbeat);
		}
		if (msgCase_ == MsgOneofCase.CreateRoomHandle)
		{
			output.WriteRawTag(234, 184, 2);
			output.WriteMessage(CreateRoomHandle);
		}
		if (msgCase_ == MsgOneofCase.CreateRoom)
		{
			output.WriteRawTag(242, 184, 2);
			output.WriteMessage(CreateRoom);
		}
		if (msgCase_ == MsgOneofCase.SyncRoomHandle)
		{
			output.WriteRawTag(250, 184, 2);
			output.WriteMessage(SyncRoomHandle);
		}
		if (msgCase_ == MsgOneofCase.SyncRoom)
		{
			output.WriteRawTag(130, 185, 2);
			output.WriteMessage(SyncRoom);
		}
		if (msgCase_ == MsgOneofCase.JoinRoomHandle)
		{
			output.WriteRawTag(138, 185, 2);
			output.WriteMessage(JoinRoomHandle);
		}
		if (msgCase_ == MsgOneofCase.JoinRoom)
		{
			output.WriteRawTag(146, 185, 2);
			output.WriteMessage(JoinRoom);
		}
		if (msgCase_ == MsgOneofCase.ExitRoomHandle)
		{
			output.WriteRawTag(154, 185, 2);
			output.WriteMessage(ExitRoomHandle);
		}
		if (msgCase_ == MsgOneofCase.ExitRoom)
		{
			output.WriteRawTag(162, 185, 2);
			output.WriteMessage(ExitRoom);
		}
		if (msgCase_ == MsgOneofCase.QueryRoomHandle)
		{
			output.WriteRawTag(170, 185, 2);
			output.WriteMessage(QueryRoomHandle);
		}
		if (msgCase_ == MsgOneofCase.QueryRoom)
		{
			output.WriteRawTag(178, 185, 2);
			output.WriteMessage(QueryRoom);
		}
		if (msgCase_ == MsgOneofCase.RefreshRoomStateHandle)
		{
			output.WriteRawTag(186, 185, 2);
			output.WriteMessage(RefreshRoomStateHandle);
		}
		if (msgCase_ == MsgOneofCase.RefreshRoomState)
		{
			output.WriteRawTag(194, 185, 2);
			output.WriteMessage(RefreshRoomState);
		}
		if (msgCase_ == MsgOneofCase.StartGameHandle)
		{
			output.WriteRawTag(218, 185, 2);
			output.WriteMessage(StartGameHandle);
		}
		if (msgCase_ == MsgOneofCase.StartGame)
		{
			output.WriteRawTag(226, 185, 2);
			output.WriteMessage(StartGame);
		}
		if (msgCase_ == MsgOneofCase.ThrowDiceHandle)
		{
			output.WriteRawTag(234, 185, 2);
			output.WriteMessage(ThrowDiceHandle);
		}
		if (msgCase_ == MsgOneofCase.ThrowDice)
		{
			output.WriteRawTag(242, 185, 2);
			output.WriteMessage(ThrowDice);
		}
		if (msgCase_ == MsgOneofCase.ChangeRoomHandle)
		{
			output.WriteRawTag(250, 185, 2);
			output.WriteMessage(ChangeRoomHandle);
		}
		if (msgCase_ == MsgOneofCase.ChangeRoom)
		{
			output.WriteRawTag(130, 186, 2);
			output.WriteMessage(ChangeRoom);
		}
		if (msgCase_ == MsgOneofCase.MoveHandle)
		{
			output.WriteRawTag(154, 186, 2);
			output.WriteMessage(MoveHandle);
		}
		if (msgCase_ == MsgOneofCase.Move)
		{
			output.WriteRawTag(162, 186, 2);
			output.WriteMessage(Move);
		}
		if (msgCase_ == MsgOneofCase.ShopBuyHandle)
		{
			output.WriteRawTag(170, 186, 2);
			output.WriteMessage(ShopBuyHandle);
		}
		if (msgCase_ == MsgOneofCase.ShopBuy)
		{
			output.WriteRawTag(178, 186, 2);
			output.WriteMessage(ShopBuy);
		}
		if (msgCase_ == MsgOneofCase.PursuitHandle)
		{
			output.WriteRawTag(202, 186, 2);
			output.WriteMessage(PursuitHandle);
		}
		if (msgCase_ == MsgOneofCase.Pursuit)
		{
			output.WriteRawTag(210, 186, 2);
			output.WriteMessage(Pursuit);
		}
		if (msgCase_ == MsgOneofCase.BattleUseCardHandle)
		{
			output.WriteRawTag(218, 186, 2);
			output.WriteMessage(BattleUseCardHandle);
		}
		if (msgCase_ == MsgOneofCase.BattleUseCard)
		{
			output.WriteRawTag(226, 186, 2);
			output.WriteMessage(BattleUseCard);
		}
		if (msgCase_ == MsgOneofCase.BattleThrowDiceHandle)
		{
			output.WriteRawTag(234, 186, 2);
			output.WriteMessage(BattleThrowDiceHandle);
		}
		if (msgCase_ == MsgOneofCase.BattleThrowDice)
		{
			output.WriteRawTag(242, 186, 2);
			output.WriteMessage(BattleThrowDice);
		}
		if (msgCase_ == MsgOneofCase.BattleChoiceHandle)
		{
			output.WriteRawTag(250, 186, 2);
			output.WriteMessage(BattleChoiceHandle);
		}
		if (msgCase_ == MsgOneofCase.BattleChoice)
		{
			output.WriteRawTag(130, 187, 2);
			output.WriteMessage(BattleChoice);
		}
		if (msgCase_ == MsgOneofCase.LotteryChoiceHandle)
		{
			output.WriteRawTag(138, 187, 2);
			output.WriteMessage(LotteryChoiceHandle);
		}
		if (msgCase_ == MsgOneofCase.LotteryChoice)
		{
			output.WriteRawTag(146, 187, 2);
			output.WriteMessage(LotteryChoice);
		}
		if (msgCase_ == MsgOneofCase.MoveAgainHandle)
		{
			output.WriteRawTag(154, 187, 2);
			output.WriteMessage(MoveAgainHandle);
		}
		if (msgCase_ == MsgOneofCase.MoveAgain)
		{
			output.WriteRawTag(162, 187, 2);
			output.WriteMessage(MoveAgain);
		}
		if (msgCase_ == MsgOneofCase.AskBattleHandle)
		{
			output.WriteRawTag(186, 187, 2);
			output.WriteMessage(AskBattleHandle);
		}
		if (msgCase_ == MsgOneofCase.AskBattle)
		{
			output.WriteRawTag(194, 187, 2);
			output.WriteMessage(AskBattle);
		}
		if (msgCase_ == MsgOneofCase.RollGoldHandle)
		{
			output.WriteRawTag(202, 187, 2);
			output.WriteMessage(RollGoldHandle);
		}
		if (msgCase_ == MsgOneofCase.RollGold)
		{
			output.WriteRawTag(210, 187, 2);
			output.WriteMessage(RollGold);
		}
		if (msgCase_ == MsgOneofCase.EventThrowDiceHandle)
		{
			output.WriteRawTag(218, 187, 2);
			output.WriteMessage(EventThrowDiceHandle);
		}
		if (msgCase_ == MsgOneofCase.EventThrowDice)
		{
			output.WriteRawTag(226, 187, 2);
			output.WriteMessage(EventThrowDice);
		}
		if (msgCase_ == MsgOneofCase.TriggerEventHandle)
		{
			output.WriteRawTag(234, 187, 2);
			output.WriteMessage(TriggerEventHandle);
		}
		if (msgCase_ == MsgOneofCase.TriggerEvent)
		{
			output.WriteRawTag(242, 187, 2);
			output.WriteMessage(TriggerEvent);
		}
		if (msgCase_ == MsgOneofCase.UseEffectCardHandle)
		{
			output.WriteRawTag(250, 187, 2);
			output.WriteMessage(UseEffectCardHandle);
		}
		if (msgCase_ == MsgOneofCase.UseEffectCard)
		{
			output.WriteRawTag(130, 188, 2);
			output.WriteMessage(UseEffectCard);
		}
		if (msgCase_ == MsgOneofCase.BombThrowDiceHandle)
		{
			output.WriteRawTag(154, 188, 2);
			output.WriteMessage(BombThrowDiceHandle);
		}
		if (msgCase_ == MsgOneofCase.BombThrowDice)
		{
			output.WriteRawTag(162, 188, 2);
			output.WriteMessage(BombThrowDice);
		}
		if (msgCase_ == MsgOneofCase.ChoiceDirectionHandle)
		{
			output.WriteRawTag(170, 188, 2);
			output.WriteMessage(ChoiceDirectionHandle);
		}
		if (msgCase_ == MsgOneofCase.ChoiceDirection)
		{
			output.WriteRawTag(178, 188, 2);
			output.WriteMessage(ChoiceDirection);
		}
		if (msgCase_ == MsgOneofCase.LandChoiceTargetHandle)
		{
			output.WriteRawTag(186, 188, 2);
			output.WriteMessage(LandChoiceTargetHandle);
		}
		if (msgCase_ == MsgOneofCase.LandChoiceTarget)
		{
			output.WriteRawTag(194, 188, 2);
			output.WriteMessage(LandChoiceTarget);
		}
		if (msgCase_ == MsgOneofCase.ThrowDiceResultHandle)
		{
			output.WriteRawTag(218, 188, 2);
			output.WriteMessage(ThrowDiceResultHandle);
		}
		if (msgCase_ == MsgOneofCase.ThrowDiceResult)
		{
			output.WriteRawTag(226, 188, 2);
			output.WriteMessage(ThrowDiceResult);
		}
		if (msgCase_ == MsgOneofCase.TriggerDivinationHandle)
		{
			output.WriteRawTag(234, 188, 2);
			output.WriteMessage(TriggerDivinationHandle);
		}
		if (msgCase_ == MsgOneofCase.TriggerDivination)
		{
			output.WriteRawTag(242, 188, 2);
			output.WriteMessage(TriggerDivination);
		}
		if (msgCase_ == MsgOneofCase.TriggerDestinyHandle)
		{
			output.WriteRawTag(250, 188, 2);
			output.WriteMessage(TriggerDestinyHandle);
		}
		if (msgCase_ == MsgOneofCase.TriggerDestiny)
		{
			output.WriteRawTag(130, 189, 2);
			output.WriteMessage(TriggerDestiny);
		}
		if (msgCase_ == MsgOneofCase.UseQuickCardHandle)
		{
			output.WriteRawTag(138, 189, 2);
			output.WriteMessage(UseQuickCardHandle);
		}
		if (msgCase_ == MsgOneofCase.UseQuickCard)
		{
			output.WriteRawTag(146, 189, 2);
			output.WriteMessage(UseQuickCard);
		}
		if (msgCase_ == MsgOneofCase.AbandonCardHandle)
		{
			output.WriteRawTag(154, 189, 2);
			output.WriteMessage(AbandonCardHandle);
		}
		if (msgCase_ == MsgOneofCase.AbandonCard)
		{
			output.WriteRawTag(162, 189, 2);
			output.WriteMessage(AbandonCard);
		}
		if (msgCase_ == MsgOneofCase.StopOrContinueHandle)
		{
			output.WriteRawTag(170, 189, 2);
			output.WriteMessage(StopOrContinueHandle);
		}
		if (msgCase_ == MsgOneofCase.StopOrContinue)
		{
			output.WriteRawTag(178, 189, 2);
			output.WriteMessage(StopOrContinue);
		}
		if (msgCase_ == MsgOneofCase.StartGambleHandle)
		{
			output.WriteRawTag(202, 189, 2);
			output.WriteMessage(StartGambleHandle);
		}
		if (msgCase_ == MsgOneofCase.StartGamble)
		{
			output.WriteRawTag(210, 189, 2);
			output.WriteMessage(StartGamble);
		}
		if (msgCase_ == MsgOneofCase.GambleThrowDicHandle)
		{
			output.WriteRawTag(218, 189, 2);
			output.WriteMessage(GambleThrowDicHandle);
		}
		if (msgCase_ == MsgOneofCase.GambleThrowDic)
		{
			output.WriteRawTag(226, 189, 2);
			output.WriteMessage(GambleThrowDic);
		}
		if (msgCase_ == MsgOneofCase.ChoiceHeroC2S2Handle)
		{
			output.WriteRawTag(234, 189, 2);
			output.WriteMessage(ChoiceHeroC2S2Handle);
		}
		if (msgCase_ == MsgOneofCase.ChoiceHero2)
		{
			output.WriteRawTag(242, 189, 2);
			output.WriteMessage(ChoiceHero2);
		}
		if (msgCase_ == MsgOneofCase.AffirmHeroHandle)
		{
			output.WriteRawTag(250, 189, 2);
			output.WriteMessage(AffirmHeroHandle);
		}
		if (msgCase_ == MsgOneofCase.AffirmHero)
		{
			output.WriteRawTag(130, 190, 2);
			output.WriteMessage(AffirmHero);
		}
		if (msgCase_ == MsgOneofCase.SearchRoomHandle)
		{
			output.WriteRawTag(138, 190, 2);
			output.WriteMessage(SearchRoomHandle);
		}
		if (msgCase_ == MsgOneofCase.SearchRoom)
		{
			output.WriteRawTag(146, 190, 2);
			output.WriteMessage(SearchRoom);
		}
		if (msgCase_ == MsgOneofCase.GmHandle)
		{
			output.WriteRawTag(154, 190, 2);
			output.WriteMessage(GmHandle);
		}
		if (msgCase_ == MsgOneofCase.Gm)
		{
			output.WriteRawTag(162, 190, 2);
			output.WriteMessage(Gm);
		}
		if (msgCase_ == MsgOneofCase.TriggerHospitalHandle)
		{
			output.WriteRawTag(170, 190, 2);
			output.WriteMessage(TriggerHospitalHandle);
		}
		if (msgCase_ == MsgOneofCase.TriggerHospital)
		{
			output.WriteRawTag(178, 190, 2);
			output.WriteMessage(TriggerHospital);
		}
		if (msgCase_ == MsgOneofCase.SendChatHandle)
		{
			output.WriteRawTag(186, 190, 2);
			output.WriteMessage(SendChatHandle);
		}
		if (msgCase_ == MsgOneofCase.SendChat)
		{
			output.WriteRawTag(194, 190, 2);
			output.WriteMessage(SendChat);
		}
		if (msgCase_ == MsgOneofCase.PlayerShopBuyC2S)
		{
			output.WriteRawTag(202, 190, 2);
			output.WriteMessage(PlayerShopBuyC2S);
		}
		if (msgCase_ == MsgOneofCase.PlayerShopBuyS2C)
		{
			output.WriteRawTag(210, 190, 2);
			output.WriteMessage(PlayerShopBuyS2C);
		}
		if (msgCase_ == MsgOneofCase.PlayerUseItemHandle)
		{
			output.WriteRawTag(218, 190, 2);
			output.WriteMessage(PlayerUseItemHandle);
		}
		if (msgCase_ == MsgOneofCase.PlayerUseItem)
		{
			output.WriteRawTag(226, 190, 2);
			output.WriteMessage(PlayerUseItem);
		}
		if (msgCase_ == MsgOneofCase.UseTreasureC2S)
		{
			output.WriteRawTag(234, 190, 2);
			output.WriteMessage(UseTreasureC2S);
		}
		if (msgCase_ == MsgOneofCase.UseTreasureS2C)
		{
			output.WriteRawTag(242, 190, 2);
			output.WriteMessage(UseTreasureS2C);
		}
		if (msgCase_ == MsgOneofCase.SetFashionC2S)
		{
			output.WriteRawTag(250, 190, 2);
			output.WriteMessage(SetFashionC2S);
		}
		if (msgCase_ == MsgOneofCase.SetFashionS2C)
		{
			output.WriteRawTag(130, 191, 2);
			output.WriteMessage(SetFashionS2C);
		}
		if (msgCase_ == MsgOneofCase.SelectFashionPlanC2S)
		{
			output.WriteRawTag(138, 191, 2);
			output.WriteMessage(SelectFashionPlanC2S);
		}
		if (msgCase_ == MsgOneofCase.SelectFashionPlanS2C)
		{
			output.WriteRawTag(146, 191, 2);
			output.WriteMessage(SelectFashionPlanS2C);
		}
		if (msgCase_ == MsgOneofCase.GachaC2S)
		{
			output.WriteRawTag(154, 191, 2);
			output.WriteMessage(GachaC2S);
		}
		if (msgCase_ == MsgOneofCase.GachaS2C)
		{
			output.WriteRawTag(162, 191, 2);
			output.WriteMessage(GachaS2C);
		}
		if (msgCase_ == MsgOneofCase.SteamSearchRoomC2S)
		{
			output.WriteRawTag(170, 191, 2);
			output.WriteMessage(SteamSearchRoomC2S);
		}
		if (msgCase_ == MsgOneofCase.SteamSearchRoomS2C)
		{
			output.WriteRawTag(178, 191, 2);
			output.WriteMessage(SteamSearchRoomS2C);
		}
		if (msgCase_ == MsgOneofCase.CheatItemHandle)
		{
			output.WriteRawTag(186, 191, 2);
			output.WriteMessage(CheatItemHandle);
		}
		if (msgCase_ == MsgOneofCase.CheatItem)
		{
			output.WriteRawTag(194, 191, 2);
			output.WriteMessage(CheatItem);
		}
		if (msgCase_ == MsgOneofCase.RoleCardUpLvC2S)
		{
			output.WriteRawTag(202, 191, 2);
			output.WriteMessage(RoleCardUpLvC2S);
		}
		if (msgCase_ == MsgOneofCase.RoleCardUpLvS2C)
		{
			output.WriteRawTag(210, 191, 2);
			output.WriteMessage(RoleCardUpLvS2C);
		}
		if (msgCase_ == MsgOneofCase.RoleCardBreakThroughC2S)
		{
			output.WriteRawTag(218, 191, 2);
			output.WriteMessage(RoleCardBreakThroughC2S);
		}
		if (msgCase_ == MsgOneofCase.RoleCardBreakThroughS2C)
		{
			output.WriteRawTag(226, 191, 2);
			output.WriteMessage(RoleCardBreakThroughS2C);
		}
		if (msgCase_ == MsgOneofCase.RoleCardChoiceResC2S)
		{
			output.WriteRawTag(234, 191, 2);
			output.WriteMessage(RoleCardChoiceResC2S);
		}
		if (msgCase_ == MsgOneofCase.RoleCardChoiceResS2C)
		{
			output.WriteRawTag(242, 191, 2);
			output.WriteMessage(RoleCardChoiceResS2C);
		}
		if (msgCase_ == MsgOneofCase.TaskRewardC2S)
		{
			output.WriteRawTag(250, 191, 2);
			output.WriteMessage(TaskRewardC2S);
		}
		if (msgCase_ == MsgOneofCase.TaskRewardS2C)
		{
			output.WriteRawTag(130, 192, 2);
			output.WriteMessage(TaskRewardS2C);
		}
		if (msgCase_ == MsgOneofCase.TeachingC2S)
		{
			output.WriteRawTag(138, 192, 2);
			output.WriteMessage(TeachingC2S);
		}
		if (msgCase_ == MsgOneofCase.TeachingS2C)
		{
			output.WriteRawTag(146, 192, 2);
			output.WriteMessage(TeachingS2C);
		}
		if (msgCase_ == MsgOneofCase.UseTreasureAutoTransformC2S)
		{
			output.WriteRawTag(154, 192, 2);
			output.WriteMessage(UseTreasureAutoTransformC2S);
		}
		if (msgCase_ == MsgOneofCase.UseTreasureAutoTransformS2C)
		{
			output.WriteRawTag(162, 192, 2);
			output.WriteMessage(UseTreasureAutoTransformS2C);
		}
		if (msgCase_ == MsgOneofCase.QuickJoinRoomC2S)
		{
			output.WriteRawTag(170, 192, 2);
			output.WriteMessage(QuickJoinRoomC2S);
		}
		if (msgCase_ == MsgOneofCase.QuickJoinRoomS2C)
		{
			output.WriteRawTag(178, 192, 2);
			output.WriteMessage(QuickJoinRoomS2C);
		}
		if (msgCase_ == MsgOneofCase.RoomKickPlayerC2S)
		{
			output.WriteRawTag(186, 192, 2);
			output.WriteMessage(RoomKickPlayerC2S);
		}
		if (msgCase_ == MsgOneofCase.RoomKickPlayerS2C)
		{
			output.WriteRawTag(194, 192, 2);
			output.WriteMessage(RoomKickPlayerS2C);
		}
		if (msgCase_ == MsgOneofCase.RoomAbdicationC2S)
		{
			output.WriteRawTag(202, 192, 2);
			output.WriteMessage(RoomAbdicationC2S);
		}
		if (msgCase_ == MsgOneofCase.RoomAbdicationS2C)
		{
			output.WriteRawTag(210, 192, 2);
			output.WriteMessage(RoomAbdicationS2C);
		}
		if (msgCase_ == MsgOneofCase.RoomReadyC2S)
		{
			output.WriteRawTag(218, 192, 2);
			output.WriteMessage(RoomReadyC2S);
		}
		if (msgCase_ == MsgOneofCase.RoomReadyS2C)
		{
			output.WriteRawTag(226, 192, 2);
			output.WriteMessage(RoomReadyS2C);
		}
		if (msgCase_ == MsgOneofCase.ChargeCreateC2S)
		{
			output.WriteRawTag(234, 192, 2);
			output.WriteMessage(ChargeCreateC2S);
		}
		if (msgCase_ == MsgOneofCase.ChargeCreateS2C)
		{
			output.WriteRawTag(242, 192, 2);
			output.WriteMessage(ChargeCreateS2C);
		}
		if (msgCase_ == MsgOneofCase.ChargeC2S)
		{
			output.WriteRawTag(250, 192, 2);
			output.WriteMessage(ChargeC2S);
		}
		if (msgCase_ == MsgOneofCase.ChargeS2C)
		{
			output.WriteRawTag(130, 193, 2);
			output.WriteMessage(ChargeS2C);
		}
		if (msgCase_ == MsgOneofCase.GiftCdkC2S)
		{
			output.WriteRawTag(138, 193, 2);
			output.WriteMessage(GiftCdkC2S);
		}
		if (msgCase_ == MsgOneofCase.GiftCdkS2C)
		{
			output.WriteRawTag(146, 193, 2);
			output.WriteMessage(GiftCdkS2C);
		}
		if (msgCase_ == MsgOneofCase.MailReadC2S)
		{
			output.WriteRawTag(154, 193, 2);
			output.WriteMessage(MailReadC2S);
		}
		if (msgCase_ == MsgOneofCase.MailReadS2C)
		{
			output.WriteRawTag(162, 193, 2);
			output.WriteMessage(MailReadS2C);
		}
		if (msgCase_ == MsgOneofCase.MailGetRewardC2S)
		{
			output.WriteRawTag(170, 193, 2);
			output.WriteMessage(MailGetRewardC2S);
		}
		if (msgCase_ == MsgOneofCase.MailGetRewardS2C)
		{
			output.WriteRawTag(178, 193, 2);
			output.WriteMessage(MailGetRewardS2C);
		}
		if (msgCase_ == MsgOneofCase.MailDelReadC2S)
		{
			output.WriteRawTag(186, 193, 2);
			output.WriteMessage(MailDelReadC2S);
		}
		if (msgCase_ == MsgOneofCase.MailDelReadS2C)
		{
			output.WriteRawTag(194, 193, 2);
			output.WriteMessage(MailDelReadS2C);
		}
		if (msgCase_ == MsgOneofCase.GachaRecordC2S)
		{
			output.WriteRawTag(202, 193, 2);
			output.WriteMessage(GachaRecordC2S);
		}
		if (msgCase_ == MsgOneofCase.GachaRecordS2C)
		{
			output.WriteRawTag(210, 193, 2);
			output.WriteMessage(GachaRecordS2C);
		}
		if (msgCase_ == MsgOneofCase.ActivityTaskRewardC2S)
		{
			output.WriteRawTag(218, 193, 2);
			output.WriteMessage(ActivityTaskRewardC2S);
		}
		if (msgCase_ == MsgOneofCase.ActivityTaskRewardS2C)
		{
			output.WriteRawTag(226, 193, 2);
			output.WriteMessage(ActivityTaskRewardS2C);
		}
		if (msgCase_ == MsgOneofCase.RoomShortChatC2S)
		{
			output.WriteRawTag(234, 193, 2);
			output.WriteMessage(RoomShortChatC2S);
		}
		if (msgCase_ == MsgOneofCase.RoomShortChatS2C)
		{
			output.WriteRawTag(242, 193, 2);
			output.WriteMessage(RoomShortChatS2C);
		}
		if (msgCase_ == MsgOneofCase.SetShowPlayerC2S)
		{
			output.WriteRawTag(250, 193, 2);
			output.WriteMessage(SetShowPlayerC2S);
		}
		if (msgCase_ == MsgOneofCase.SetShowPlayerS2C)
		{
			output.WriteRawTag(130, 194, 2);
			output.WriteMessage(SetShowPlayerS2C);
		}
		if (msgCase_ == MsgOneofCase.GetShowPlayerC2S)
		{
			output.WriteRawTag(138, 194, 2);
			output.WriteMessage(GetShowPlayerC2S);
		}
		if (msgCase_ == MsgOneofCase.GetShowPlayerS2C)
		{
			output.WriteRawTag(146, 194, 2);
			output.WriteMessage(GetShowPlayerS2C);
		}
		if (msgCase_ == MsgOneofCase.GetPlayerFightRecordC2S)
		{
			output.WriteRawTag(154, 194, 2);
			output.WriteMessage(GetPlayerFightRecordC2S);
		}
		if (msgCase_ == MsgOneofCase.GetPlayerFightRecordS2C)
		{
			output.WriteRawTag(162, 194, 2);
			output.WriteMessage(GetPlayerFightRecordS2C);
		}
		if (msgCase_ == MsgOneofCase.GetDay7RewardC2S)
		{
			output.WriteRawTag(170, 194, 2);
			output.WriteMessage(GetDay7RewardC2S);
		}
		if (msgCase_ == MsgOneofCase.GetDay7RewardS2C)
		{
			output.WriteRawTag(178, 194, 2);
			output.WriteMessage(GetDay7RewardS2C);
		}
		if (msgCase_ == MsgOneofCase.PraisePlayerC2S)
		{
			output.WriteRawTag(186, 194, 2);
			output.WriteMessage(PraisePlayerC2S);
		}
		if (msgCase_ == MsgOneofCase.PraisePlayerS2C)
		{
			output.WriteRawTag(194, 194, 2);
			output.WriteMessage(PraisePlayerS2C);
		}
		if (msgCase_ == MsgOneofCase.ClientDataUploadC2S)
		{
			output.WriteRawTag(202, 194, 2);
			output.WriteMessage(ClientDataUploadC2S);
		}
		if (msgCase_ == MsgOneofCase.ClientDataUploadS2C)
		{
			output.WriteRawTag(210, 194, 2);
			output.WriteMessage(ClientDataUploadS2C);
		}
		if (msgCase_ == MsgOneofCase.FriendListC2S)
		{
			output.WriteRawTag(218, 194, 2);
			output.WriteMessage(FriendListC2S);
		}
		if (msgCase_ == MsgOneofCase.FriendListS2C)
		{
			output.WriteRawTag(226, 194, 2);
			output.WriteMessage(FriendListS2C);
		}
		if (msgCase_ == MsgOneofCase.FriendApplyC2S)
		{
			output.WriteRawTag(234, 194, 2);
			output.WriteMessage(FriendApplyC2S);
		}
		if (msgCase_ == MsgOneofCase.FriendApplyS2C)
		{
			output.WriteRawTag(242, 194, 2);
			output.WriteMessage(FriendApplyS2C);
		}
		if (msgCase_ == MsgOneofCase.FriendApplyListC2S)
		{
			output.WriteRawTag(250, 194, 2);
			output.WriteMessage(FriendApplyListC2S);
		}
		if (msgCase_ == MsgOneofCase.FriendApplyListS2C)
		{
			output.WriteRawTag(130, 195, 2);
			output.WriteMessage(FriendApplyListS2C);
		}
		if (msgCase_ == MsgOneofCase.FriendApplyOpC2S)
		{
			output.WriteRawTag(138, 195, 2);
			output.WriteMessage(FriendApplyOpC2S);
		}
		if (msgCase_ == MsgOneofCase.FriendApplyOpS2C)
		{
			output.WriteRawTag(146, 195, 2);
			output.WriteMessage(FriendApplyOpS2C);
		}
		if (msgCase_ == MsgOneofCase.FriendOpC2S)
		{
			output.WriteRawTag(154, 195, 2);
			output.WriteMessage(FriendOpC2S);
		}
		if (msgCase_ == MsgOneofCase.FriendOpS2C)
		{
			output.WriteRawTag(162, 195, 2);
			output.WriteMessage(FriendOpS2C);
		}
		if (msgCase_ == MsgOneofCase.FriendInviteC2S)
		{
			output.WriteRawTag(170, 195, 2);
			output.WriteMessage(FriendInviteC2S);
		}
		if (msgCase_ == MsgOneofCase.FriendInviteS2C)
		{
			output.WriteRawTag(178, 195, 2);
			output.WriteMessage(FriendInviteS2C);
		}
		if (msgCase_ == MsgOneofCase.FriendInviteListC2S)
		{
			output.WriteRawTag(186, 195, 2);
			output.WriteMessage(FriendInviteListC2S);
		}
		if (msgCase_ == MsgOneofCase.FriendInviteListS2C)
		{
			output.WriteRawTag(194, 195, 2);
			output.WriteMessage(FriendInviteListS2C);
		}
		if (msgCase_ == MsgOneofCase.FriendInviteCleanC2S)
		{
			output.WriteRawTag(218, 195, 2);
			output.WriteMessage(FriendInviteCleanC2S);
		}
		if (msgCase_ == MsgOneofCase.FriendInviteCleanS2C)
		{
			output.WriteRawTag(226, 195, 2);
			output.WriteMessage(FriendInviteCleanS2C);
		}
		if (msgCase_ == MsgOneofCase.FriendBlacksListC2S)
		{
			output.WriteRawTag(234, 195, 2);
			output.WriteMessage(FriendBlacksListC2S);
		}
		if (msgCase_ == MsgOneofCase.FriendBlacksListS2C)
		{
			output.WriteRawTag(242, 195, 2);
			output.WriteMessage(FriendBlacksListS2C);
		}
		if (msgCase_ == MsgOneofCase.NearFightPlayerC2S)
		{
			output.WriteRawTag(250, 195, 2);
			output.WriteMessage(NearFightPlayerC2S);
		}
		if (msgCase_ == MsgOneofCase.NearFightPlayerS2C)
		{
			output.WriteRawTag(130, 196, 2);
			output.WriteMessage(NearFightPlayerS2C);
		}
		if (msgCase_ == MsgOneofCase.SearchPlayerC2S)
		{
			output.WriteRawTag(138, 196, 2);
			output.WriteMessage(SearchPlayerC2S);
		}
		if (msgCase_ == MsgOneofCase.SearchPlayerS2C)
		{
			output.WriteRawTag(146, 196, 2);
			output.WriteMessage(SearchPlayerS2C);
		}
		if (msgCase_ == MsgOneofCase.ScratchCardC2S)
		{
			output.WriteRawTag(154, 196, 2);
			output.WriteMessage(ScratchCardC2S);
		}
		if (msgCase_ == MsgOneofCase.ScratchCardS2C)
		{
			output.WriteRawTag(162, 196, 2);
			output.WriteMessage(ScratchCardS2C);
		}
		if (msgCase_ == MsgOneofCase.NextScratchCardPoolC2S)
		{
			output.WriteRawTag(170, 196, 2);
			output.WriteMessage(NextScratchCardPoolC2S);
		}
		if (msgCase_ == MsgOneofCase.NextScratchCardPoolS2C)
		{
			output.WriteRawTag(178, 196, 2);
			output.WriteMessage(NextScratchCardPoolS2C);
		}
		if (msgCase_ == MsgOneofCase.WatchJoinRoomC2S)
		{
			output.WriteRawTag(186, 196, 2);
			output.WriteMessage(WatchJoinRoomC2S);
		}
		if (msgCase_ == MsgOneofCase.WatchJoinRoomS2C)
		{
			output.WriteRawTag(194, 196, 2);
			output.WriteMessage(WatchJoinRoomS2C);
		}
		if (msgCase_ == MsgOneofCase.WatchRefreshRoomStateC2S)
		{
			output.WriteRawTag(202, 196, 2);
			output.WriteMessage(WatchRefreshRoomStateC2S);
		}
		if (msgCase_ == MsgOneofCase.WatchRefreshRoomStateS2C)
		{
			output.WriteRawTag(210, 196, 2);
			output.WriteMessage(WatchRefreshRoomStateS2C);
		}
		if (msgCase_ == MsgOneofCase.WatchExitRoomC2S)
		{
			output.WriteRawTag(218, 196, 2);
			output.WriteMessage(WatchExitRoomC2S);
		}
		if (msgCase_ == MsgOneofCase.WatchExitRoomS2C)
		{
			output.WriteRawTag(226, 196, 2);
			output.WriteMessage(WatchExitRoomS2C);
		}
		if (msgCase_ == MsgOneofCase.BattlePassGetRewardC2S)
		{
			output.WriteRawTag(234, 196, 2);
			output.WriteMessage(BattlePassGetRewardC2S);
		}
		if (msgCase_ == MsgOneofCase.BattlePassGetRewardS2C)
		{
			output.WriteRawTag(242, 196, 2);
			output.WriteMessage(BattlePassGetRewardS2C);
		}
		if (msgCase_ == MsgOneofCase.BattlePassTaskRewardC2S)
		{
			output.WriteRawTag(250, 196, 2);
			output.WriteMessage(BattlePassTaskRewardC2S);
		}
		if (msgCase_ == MsgOneofCase.BattlePassTaskRewardS2C)
		{
			output.WriteRawTag(130, 197, 2);
			output.WriteMessage(BattlePassTaskRewardS2C);
		}
		if (msgCase_ == MsgOneofCase.BattlePassUpLvC2S)
		{
			output.WriteRawTag(138, 197, 2);
			output.WriteMessage(BattlePassUpLvC2S);
		}
		if (msgCase_ == MsgOneofCase.BattlePassUpLvS2C)
		{
			output.WriteRawTag(146, 197, 2);
			output.WriteMessage(BattlePassUpLvS2C);
		}
		if (msgCase_ == MsgOneofCase.FriendSendMsgC2S)
		{
			output.WriteRawTag(154, 197, 2);
			output.WriteMessage(FriendSendMsgC2S);
		}
		if (msgCase_ == MsgOneofCase.FriendSendMsgS2C)
		{
			output.WriteRawTag(162, 197, 2);
			output.WriteMessage(FriendSendMsgS2C);
		}
		if (msgCase_ == MsgOneofCase.GetChatMsgC2S)
		{
			output.WriteRawTag(170, 197, 2);
			output.WriteMessage(GetChatMsgC2S);
		}
		if (msgCase_ == MsgOneofCase.GetChatMsgS2C)
		{
			output.WriteRawTag(178, 197, 2);
			output.WriteMessage(GetChatMsgS2C);
		}
		if (msgCase_ == MsgOneofCase.ReadChatMsgC2S)
		{
			output.WriteRawTag(186, 197, 2);
			output.WriteMessage(ReadChatMsgC2S);
		}
		if (msgCase_ == MsgOneofCase.ReadChatMsgS2C)
		{
			output.WriteRawTag(194, 197, 2);
			output.WriteMessage(ReadChatMsgS2C);
		}
		if (msgCase_ == MsgOneofCase.DelChatMsgInfoC2S)
		{
			output.WriteRawTag(202, 197, 2);
			output.WriteMessage(DelChatMsgInfoC2S);
		}
		if (msgCase_ == MsgOneofCase.DelChatMsgInfoS2C)
		{
			output.WriteRawTag(210, 197, 2);
			output.WriteMessage(DelChatMsgInfoS2C);
		}
		if (msgCase_ == MsgOneofCase.SelectRelicC2S)
		{
			output.WriteRawTag(218, 197, 2);
			output.WriteMessage(SelectRelicC2S);
		}
		if (msgCase_ == MsgOneofCase.SelectRelicS2C)
		{
			output.WriteRawTag(226, 197, 2);
			output.WriteMessage(SelectRelicS2C);
		}
		if (msgCase_ == MsgOneofCase.MonsterPursuitC2S)
		{
			output.WriteRawTag(234, 197, 2);
			output.WriteMessage(MonsterPursuitC2S);
		}
		if (msgCase_ == MsgOneofCase.MonsterPursuitS2C)
		{
			output.WriteRawTag(242, 197, 2);
			output.WriteMessage(MonsterPursuitS2C);
		}
		if (msgCase_ == MsgOneofCase.PVEShopBuyC2S)
		{
			output.WriteRawTag(250, 197, 2);
			output.WriteMessage(PVEShopBuyC2S);
		}
		if (msgCase_ == MsgOneofCase.PVEShopBuyS2C)
		{
			output.WriteRawTag(130, 198, 2);
			output.WriteMessage(PVEShopBuyS2C);
		}
		if (msgCase_ == MsgOneofCase.ClientCheckTaskC2S)
		{
			output.WriteRawTag(138, 198, 2);
			output.WriteMessage(ClientCheckTaskC2S);
		}
		if (msgCase_ == MsgOneofCase.ClientCheckTaskS2C)
		{
			output.WriteRawTag(146, 198, 2);
			output.WriteMessage(ClientCheckTaskS2C);
		}
		if (msgCase_ == MsgOneofCase.PveHeroUpLvC2S)
		{
			output.WriteRawTag(154, 198, 2);
			output.WriteMessage(PveHeroUpLvC2S);
		}
		if (msgCase_ == MsgOneofCase.PveHeroUpLvS2C)
		{
			output.WriteRawTag(162, 198, 2);
			output.WriteMessage(PveHeroUpLvS2C);
		}
		if (msgCase_ == MsgOneofCase.StartMatchC2S)
		{
			output.WriteRawTag(170, 198, 2);
			output.WriteMessage(StartMatchC2S);
		}
		if (msgCase_ == MsgOneofCase.StartMatchS2C)
		{
			output.WriteRawTag(178, 198, 2);
			output.WriteMessage(StartMatchS2C);
		}
		if (msgCase_ == MsgOneofCase.CancelMatchC2S)
		{
			output.WriteRawTag(186, 198, 2);
			output.WriteMessage(CancelMatchC2S);
		}
		if (msgCase_ == MsgOneofCase.CancelMatchS2C)
		{
			output.WriteRawTag(194, 198, 2);
			output.WriteMessage(CancelMatchS2C);
		}
		if (msgCase_ == MsgOneofCase.MatchSuccessC2S)
		{
			output.WriteRawTag(202, 198, 2);
			output.WriteMessage(MatchSuccessC2S);
		}
		if (msgCase_ == MsgOneofCase.MatchSuccessS2C)
		{
			output.WriteRawTag(210, 198, 2);
			output.WriteMessage(MatchSuccessS2C);
		}
		if (msgCase_ == MsgOneofCase.AccuseC2S)
		{
			output.WriteRawTag(218, 198, 2);
			output.WriteMessage(AccuseC2S);
		}
		if (msgCase_ == MsgOneofCase.AccuseS2C)
		{
			output.WriteRawTag(226, 198, 2);
			output.WriteMessage(AccuseS2C);
		}
		if (msgCase_ == MsgOneofCase.SingleCampaignC2S)
		{
			output.WriteRawTag(234, 198, 2);
			output.WriteMessage(SingleCampaignC2S);
		}
		if (msgCase_ == MsgOneofCase.SingleCampaignS2C)
		{
			output.WriteRawTag(242, 198, 2);
			output.WriteMessage(SingleCampaignS2C);
		}
		if (msgCase_ == MsgOneofCase.DevChargeC2S)
		{
			output.WriteRawTag(250, 198, 2);
			output.WriteMessage(DevChargeC2S);
		}
		if (msgCase_ == MsgOneofCase.DevChargeS2C)
		{
			output.WriteRawTag(130, 199, 2);
			output.WriteMessage(DevChargeS2C);
		}
		if (msgCase_ == MsgOneofCase.AskReviveTeammateC2S)
		{
			output.WriteRawTag(138, 199, 2);
			output.WriteMessage(AskReviveTeammateC2S);
		}
		if (msgCase_ == MsgOneofCase.AskReviveTeammateS2C)
		{
			output.WriteRawTag(146, 199, 2);
			output.WriteMessage(AskReviveTeammateS2C);
		}
		if (msgCase_ == MsgOneofCase.GetSignInRewardC2S)
		{
			output.WriteRawTag(154, 199, 2);
			output.WriteMessage(GetSignInRewardC2S);
		}
		if (msgCase_ == MsgOneofCase.GetSignInRewardS2C)
		{
			output.WriteRawTag(162, 199, 2);
			output.WriteMessage(GetSignInRewardS2C);
		}
		if (msgCase_ == MsgOneofCase.ChatMapMarkersC2S)
		{
			output.WriteRawTag(170, 199, 2);
			output.WriteMessage(ChatMapMarkersC2S);
		}
		if (msgCase_ == MsgOneofCase.ChatMapMarkersS2C)
		{
			output.WriteRawTag(178, 199, 2);
			output.WriteMessage(ChatMapMarkersS2C);
		}
		if (msgCase_ == MsgOneofCase.AbroadCreateOrderC2S)
		{
			output.WriteRawTag(186, 199, 2);
			output.WriteMessage(AbroadCreateOrderC2S);
		}
		if (msgCase_ == MsgOneofCase.AbroadCreateOrderS2C)
		{
			output.WriteRawTag(194, 199, 2);
			output.WriteMessage(AbroadCreateOrderS2C);
		}
		if (msgCase_ == MsgOneofCase.AgeVerifyC2S)
		{
			output.WriteRawTag(218, 199, 2);
			output.WriteMessage(AgeVerifyC2S);
		}
		if (msgCase_ == MsgOneofCase.AgeVerifyS2C)
		{
			output.WriteRawTag(226, 199, 2);
			output.WriteMessage(AgeVerifyS2C);
		}
		if (msgCase_ == MsgOneofCase.ChangeNameC2S)
		{
			output.WriteRawTag(234, 199, 2);
			output.WriteMessage(ChangeNameC2S);
		}
		if (msgCase_ == MsgOneofCase.ChangeNameS2C)
		{
			output.WriteRawTag(242, 199, 2);
			output.WriteMessage(ChangeNameS2C);
		}
		if (msgCase_ == MsgOneofCase.ClientClickConfirmTaskC2S)
		{
			output.WriteRawTag(250, 199, 2);
			output.WriteMessage(ClientClickConfirmTaskC2S);
		}
		if (msgCase_ == MsgOneofCase.ClientClickConfirmTaskS2C)
		{
			output.WriteRawTag(130, 200, 2);
			output.WriteMessage(ClientClickConfirmTaskS2C);
		}
		if (msgCase_ == MsgOneofCase.BuyRelicC2S)
		{
			output.WriteRawTag(138, 200, 2);
			output.WriteMessage(BuyRelicC2S);
		}
		if (msgCase_ == MsgOneofCase.BuyRelicS2C)
		{
			output.WriteRawTag(146, 200, 2);
			output.WriteMessage(BuyRelicS2C);
		}
		if (msgCase_ == MsgOneofCase.BuyLightGiftC2S)
		{
			output.WriteRawTag(154, 200, 2);
			output.WriteMessage(BuyLightGiftC2S);
		}
		if (msgCase_ == MsgOneofCase.BuyLightGiftS2C)
		{
			output.WriteRawTag(162, 200, 2);
			output.WriteMessage(BuyLightGiftS2C);
		}
		if (msgCase_ == MsgOneofCase.LightGiftC2S)
		{
			output.WriteRawTag(170, 200, 2);
			output.WriteMessage(LightGiftC2S);
		}
		if (msgCase_ == MsgOneofCase.LightGiftS2C)
		{
			output.WriteRawTag(178, 200, 2);
			output.WriteMessage(LightGiftS2C);
		}
		if (msgCase_ == MsgOneofCase.AcquisitionC2S)
		{
			output.WriteRawTag(186, 200, 2);
			output.WriteMessage(AcquisitionC2S);
		}
		if (msgCase_ == MsgOneofCase.AcquisitionS2C)
		{
			output.WriteRawTag(194, 200, 2);
			output.WriteMessage(AcquisitionS2C);
		}
		if (msgCase_ == MsgOneofCase.AcquisitionRewardC2S)
		{
			output.WriteRawTag(202, 200, 2);
			output.WriteMessage(AcquisitionRewardC2S);
		}
		if (msgCase_ == MsgOneofCase.AcquisitionRewardS2C)
		{
			output.WriteRawTag(210, 200, 2);
			output.WriteMessage(AcquisitionRewardS2C);
		}
		if (msgCase_ == MsgOneofCase.SelectMechanismC2S)
		{
			output.WriteRawTag(218, 200, 2);
			output.WriteMessage(SelectMechanismC2S);
		}
		if (msgCase_ == MsgOneofCase.SelectMechanismS2C)
		{
			output.WriteRawTag(226, 200, 2);
			output.WriteMessage(SelectMechanismS2C);
		}
		if (msgCase_ == MsgOneofCase.GachaCountRewardC2S)
		{
			output.WriteRawTag(234, 200, 2);
			output.WriteMessage(GachaCountRewardC2S);
		}
		if (msgCase_ == MsgOneofCase.GachaCountRewardS2C)
		{
			output.WriteRawTag(242, 200, 2);
			output.WriteMessage(GachaCountRewardS2C);
		}
		if (msgCase_ == MsgOneofCase.GetPlayerSimpleC2S)
		{
			output.WriteRawTag(250, 200, 2);
			output.WriteMessage(GetPlayerSimpleC2S);
		}
		if (msgCase_ == MsgOneofCase.GetPlayerSimpleS2C)
		{
			output.WriteRawTag(130, 201, 2);
			output.WriteMessage(GetPlayerSimpleS2C);
		}
		if (msgCase_ == MsgOneofCase.RoleCardCollectC2S)
		{
			output.WriteRawTag(138, 201, 2);
			output.WriteMessage(RoleCardCollectC2S);
		}
		if (msgCase_ == MsgOneofCase.RoleCardCollectS2C)
		{
			output.WriteRawTag(146, 201, 2);
			output.WriteMessage(RoleCardCollectS2C);
		}
		if (msgCase_ == MsgOneofCase.GMPlayerSettingC2S)
		{
			output.WriteRawTag(154, 201, 2);
			output.WriteMessage(GMPlayerSettingC2S);
		}
		if (msgCase_ == MsgOneofCase.GMPlayerSettingS2C)
		{
			output.WriteRawTag(162, 201, 2);
			output.WriteMessage(GMPlayerSettingS2C);
		}
		if (msgCase_ == MsgOneofCase.SetFriendNoteC2S)
		{
			output.WriteRawTag(170, 201, 2);
			output.WriteMessage(SetFriendNoteC2S);
		}
		if (msgCase_ == MsgOneofCase.SetFriendNoteS2C)
		{
			output.WriteRawTag(178, 201, 2);
			output.WriteMessage(SetFriendNoteS2C);
		}
		if (msgCase_ == MsgOneofCase.SetOnlineStatusC2S)
		{
			output.WriteRawTag(186, 201, 2);
			output.WriteMessage(SetOnlineStatusC2S);
		}
		if (msgCase_ == MsgOneofCase.SetOnlineStatusS2C)
		{
			output.WriteRawTag(194, 201, 2);
			output.WriteMessage(SetOnlineStatusS2C);
		}
		if (msgCase_ == MsgOneofCase.ChooseSkinC2S)
		{
			output.WriteRawTag(202, 203, 2);
			output.WriteMessage(ChooseSkinC2S);
		}
		if (msgCase_ == MsgOneofCase.ChooseSkinS2C)
		{
			output.WriteRawTag(210, 203, 2);
			output.WriteMessage(ChooseSkinS2C);
		}
		if (msgCase_ == MsgOneofCase.TimeWastingC2S)
		{
			output.WriteRawTag(218, 203, 2);
			output.WriteMessage(TimeWastingC2S);
		}
		if (msgCase_ == MsgOneofCase.TimeWastingS2C)
		{
			output.WriteRawTag(226, 203, 2);
			output.WriteMessage(TimeWastingS2C);
		}
		if (msgCase_ == MsgOneofCase.VoteC2S)
		{
			output.WriteRawTag(234, 203, 2);
			output.WriteMessage(VoteC2S);
		}
		if (msgCase_ == MsgOneofCase.VoteS2C)
		{
			output.WriteRawTag(242, 203, 2);
			output.WriteMessage(VoteS2C);
		}
		if (msgCase_ == MsgOneofCase.VoteSelectC2S)
		{
			output.WriteRawTag(250, 203, 2);
			output.WriteMessage(VoteSelectC2S);
		}
		if (msgCase_ == MsgOneofCase.VoteSelectS2C)
		{
			output.WriteRawTag(130, 204, 2);
			output.WriteMessage(VoteSelectS2C);
		}
		if (msgCase_ == MsgOneofCase.NotifyStoryC2S)
		{
			output.WriteRawTag(138, 204, 2);
			output.WriteMessage(NotifyStoryC2S);
		}
		if (msgCase_ == MsgOneofCase.NotifyStoryS2C)
		{
			output.WriteRawTag(146, 204, 2);
			output.WriteMessage(NotifyStoryS2C);
		}
		if (msgCase_ == MsgOneofCase.PveHeroTalentUpC2S)
		{
			output.WriteRawTag(154, 204, 2);
			output.WriteMessage(PveHeroTalentUpC2S);
		}
		if (msgCase_ == MsgOneofCase.PveHeroTalentUpS2C)
		{
			output.WriteRawTag(162, 204, 2);
			output.WriteMessage(PveHeroTalentUpS2C);
		}
		if (msgCase_ == MsgOneofCase.SelectEventC2S)
		{
			output.WriteRawTag(170, 204, 2);
			output.WriteMessage(SelectEventC2S);
		}
		if (msgCase_ == MsgOneofCase.SelectEventS2C)
		{
			output.WriteRawTag(178, 204, 2);
			output.WriteMessage(SelectEventS2C);
		}
		if (msgCase_ == MsgOneofCase.CampScoreC2S)
		{
			output.WriteRawTag(186, 204, 2);
			output.WriteMessage(CampScoreC2S);
		}
		if (msgCase_ == MsgOneofCase.CampScoreS2C)
		{
			output.WriteRawTag(194, 204, 2);
			output.WriteMessage(CampScoreS2C);
		}
		if (msgCase_ == MsgOneofCase.ActivityMissionRewardC2S)
		{
			output.WriteRawTag(202, 204, 2);
			output.WriteMessage(ActivityMissionRewardC2S);
		}
		if (msgCase_ == MsgOneofCase.ActivityMissionRewardS2C)
		{
			output.WriteRawTag(210, 204, 2);
			output.WriteMessage(ActivityMissionRewardS2C);
		}
		if (msgCase_ == MsgOneofCase.VendorBuyCardC2S)
		{
			output.WriteRawTag(218, 204, 2);
			output.WriteMessage(VendorBuyCardC2S);
		}
		if (msgCase_ == MsgOneofCase.VendorBuyCardS2C)
		{
			output.WriteRawTag(226, 204, 2);
			output.WriteMessage(VendorBuyCardS2C);
		}
		if (msgCase_ == MsgOneofCase.TransferStarDiscC2S)
		{
			output.WriteRawTag(234, 204, 2);
			output.WriteMessage(TransferStarDiscC2S);
		}
		if (msgCase_ == MsgOneofCase.TransferStarDiscS2C)
		{
			output.WriteRawTag(242, 204, 2);
			output.WriteMessage(TransferStarDiscS2C);
		}
		if (msgCase_ == MsgOneofCase.GetHeroInfoC2S)
		{
			output.WriteRawTag(250, 204, 2);
			output.WriteMessage(GetHeroInfoC2S);
		}
		if (msgCase_ == MsgOneofCase.GetHeroInfoS2C)
		{
			output.WriteRawTag(130, 205, 2);
			output.WriteMessage(GetHeroInfoS2C);
		}
		if (msgCase_ == MsgOneofCase.CreateMatchTeamC2S)
		{
			output.WriteRawTag(138, 205, 2);
			output.WriteMessage(CreateMatchTeamC2S);
		}
		if (msgCase_ == MsgOneofCase.CreateMatchTeamS2C)
		{
			output.WriteRawTag(146, 205, 2);
			output.WriteMessage(CreateMatchTeamS2C);
		}
		if (msgCase_ == MsgOneofCase.ChangeMatchTeamC2S)
		{
			output.WriteRawTag(154, 205, 2);
			output.WriteMessage(ChangeMatchTeamC2S);
		}
		if (msgCase_ == MsgOneofCase.ChangeMatchTeamS2C)
		{
			output.WriteRawTag(162, 205, 2);
			output.WriteMessage(ChangeMatchTeamS2C);
		}
		if (msgCase_ == MsgOneofCase.JoinMatchTeamC2S)
		{
			output.WriteRawTag(170, 205, 2);
			output.WriteMessage(JoinMatchTeamC2S);
		}
		if (msgCase_ == MsgOneofCase.JoinMatchTeamS2C)
		{
			output.WriteRawTag(178, 205, 2);
			output.WriteMessage(JoinMatchTeamS2C);
		}
		if (msgCase_ == MsgOneofCase.ExitMatchTeamC2S)
		{
			output.WriteRawTag(186, 205, 2);
			output.WriteMessage(ExitMatchTeamC2S);
		}
		if (msgCase_ == MsgOneofCase.ExitMatchTeamS2C)
		{
			output.WriteRawTag(194, 205, 2);
			output.WriteMessage(ExitMatchTeamS2C);
		}
		if (msgCase_ == MsgOneofCase.RefreshMatchTeamInfoC2S)
		{
			output.WriteRawTag(202, 205, 2);
			output.WriteMessage(RefreshMatchTeamInfoC2S);
		}
		if (msgCase_ == MsgOneofCase.RefreshMatchTeamInfoS2C)
		{
			output.WriteRawTag(210, 205, 2);
			output.WriteMessage(RefreshMatchTeamInfoS2C);
		}
		if (msgCase_ == MsgOneofCase.ChinaCreateOrderC2S)
		{
			output.WriteRawTag(218, 205, 2);
			output.WriteMessage(ChinaCreateOrderC2S);
		}
		if (msgCase_ == MsgOneofCase.ChinaCreateOrderS2C)
		{
			output.WriteRawTag(226, 205, 2);
			output.WriteMessage(ChinaCreateOrderS2C);
		}
		if (msgCase_ == MsgOneofCase.MatchTeamInviteC2S)
		{
			output.WriteRawTag(234, 205, 2);
			output.WriteMessage(MatchTeamInviteC2S);
		}
		if (msgCase_ == MsgOneofCase.MatchTeamInviteS2C)
		{
			output.WriteRawTag(242, 205, 2);
			output.WriteMessage(MatchTeamInviteS2C);
		}
		if (msgCase_ == MsgOneofCase.MatchTeamChatC2S)
		{
			output.WriteRawTag(250, 205, 2);
			output.WriteMessage(MatchTeamChatC2S);
		}
		if (msgCase_ == MsgOneofCase.MatchTeamChatS2C)
		{
			output.WriteRawTag(130, 206, 2);
			output.WriteMessage(MatchTeamChatS2C);
		}
		if (msgCase_ == MsgOneofCase.MatchTeamReadyC2S)
		{
			output.WriteRawTag(138, 206, 2);
			output.WriteMessage(MatchTeamReadyC2S);
		}
		if (msgCase_ == MsgOneofCase.MatchTeamReadyS2C)
		{
			output.WriteRawTag(146, 206, 2);
			output.WriteMessage(MatchTeamReadyS2C);
		}
		if (msgCase_ == MsgOneofCase.PlayerChatC2S)
		{
			output.WriteRawTag(154, 206, 2);
			output.WriteMessage(PlayerChatC2S);
		}
		if (msgCase_ == MsgOneofCase.PlayerChatS2C)
		{
			output.WriteRawTag(162, 206, 2);
			output.WriteMessage(PlayerChatS2C);
		}
		if (msgCase_ == MsgOneofCase.SyncSingleGameDataC2S)
		{
			output.WriteRawTag(170, 206, 2);
			output.WriteMessage(SyncSingleGameDataC2S);
		}
		if (msgCase_ == MsgOneofCase.SyncSingleGameDataS2C)
		{
			output.WriteRawTag(178, 206, 2);
			output.WriteMessage(SyncSingleGameDataS2C);
		}
		if (msgCase_ == MsgOneofCase.SingleGameDataC2S)
		{
			output.WriteRawTag(186, 206, 2);
			output.WriteMessage(SingleGameDataC2S);
		}
		if (msgCase_ == MsgOneofCase.SingleGameDataS2C)
		{
			output.WriteRawTag(194, 206, 2);
			output.WriteMessage(SingleGameDataS2C);
		}
		if (msgCase_ == MsgOneofCase.GetActivityPassRewardC2S)
		{
			output.WriteRawTag(202, 206, 2);
			output.WriteMessage(GetActivityPassRewardC2S);
		}
		if (msgCase_ == MsgOneofCase.GetActivityPassRewardS2C)
		{
			output.WriteRawTag(210, 206, 2);
			output.WriteMessage(GetActivityPassRewardS2C);
		}
		if (msgCase_ == MsgOneofCase.ApplyChangeSlotC2S)
		{
			output.WriteRawTag(218, 206, 2);
			output.WriteMessage(ApplyChangeSlotC2S);
		}
		if (msgCase_ == MsgOneofCase.ApplyChangeSlotS2C)
		{
			output.WriteRawTag(226, 206, 2);
			output.WriteMessage(ApplyChangeSlotS2C);
		}
		if (msgCase_ == MsgOneofCase.OpsChangeSlotC2S)
		{
			output.WriteRawTag(234, 206, 2);
			output.WriteMessage(OpsChangeSlotC2S);
		}
		if (msgCase_ == MsgOneofCase.OpsChangeSlotS2C)
		{
			output.WriteRawTag(242, 206, 2);
			output.WriteMessage(OpsChangeSlotS2C);
		}
		if (msgCase_ == MsgOneofCase.RookieGachaRewardC2S)
		{
			output.WriteRawTag(250, 206, 2);
			output.WriteMessage(RookieGachaRewardC2S);
		}
		if (msgCase_ == MsgOneofCase.RookieGachaRewardS2C)
		{
			output.WriteRawTag(130, 207, 2);
			output.WriteMessage(RookieGachaRewardS2C);
		}
		if (msgCase_ == MsgOneofCase.LiveGiftPackageC2S)
		{
			output.WriteRawTag(138, 207, 2);
			output.WriteMessage(LiveGiftPackageC2S);
		}
		if (msgCase_ == MsgOneofCase.LiveGiftPackageS2C)
		{
			output.WriteRawTag(146, 207, 2);
			output.WriteMessage(LiveGiftPackageS2C);
		}
		if (msgCase_ == MsgOneofCase.LaborActDiceC2S)
		{
			output.WriteRawTag(154, 207, 2);
			output.WriteMessage(LaborActDiceC2S);
		}
		if (msgCase_ == MsgOneofCase.LaborActDiceS2C)
		{
			output.WriteRawTag(162, 207, 2);
			output.WriteMessage(LaborActDiceS2C);
		}
		if (msgCase_ == MsgOneofCase.ActionOverTimeLogC2S)
		{
			output.WriteRawTag(170, 207, 2);
			output.WriteMessage(ActionOverTimeLogC2S);
		}
		if (msgCase_ == MsgOneofCase.ActionOverTimeLogS2C)
		{
			output.WriteRawTag(178, 207, 2);
			output.WriteMessage(ActionOverTimeLogS2C);
		}
		if (msgCase_ == MsgOneofCase.ClientHarmonyC2S)
		{
			output.WriteRawTag(202, 207, 2);
			output.WriteMessage(ClientHarmonyC2S);
		}
		if (msgCase_ == MsgOneofCase.ClientHarmonyS2C)
		{
			output.WriteRawTag(210, 207, 2);
			output.WriteMessage(ClientHarmonyS2C);
		}
		if (msgCase_ == MsgOneofCase.MailStarC2S)
		{
			output.WriteRawTag(218, 207, 2);
			output.WriteMessage(MailStarC2S);
		}
		if (msgCase_ == MsgOneofCase.MailStarS2C)
		{
			output.WriteRawTag(226, 207, 2);
			output.WriteMessage(MailStarS2C);
		}
		if (msgCase_ == MsgOneofCase.SetCardAltArtC2S)
		{
			output.WriteRawTag(234, 207, 2);
			output.WriteMessage(SetCardAltArtC2S);
		}
		if (msgCase_ == MsgOneofCase.SetCardAltArtS2C)
		{
			output.WriteRawTag(242, 207, 2);
			output.WriteMessage(SetCardAltArtS2C);
		}
		if (msgCase_ == MsgOneofCase.SelectRewardCardC2S)
		{
			output.WriteRawTag(138, 208, 2);
			output.WriteMessage(SelectRewardCardC2S);
		}
		if (msgCase_ == MsgOneofCase.SelectRewardCardS2C)
		{
			output.WriteRawTag(146, 208, 2);
			output.WriteMessage(SelectRewardCardS2C);
		}
		if (msgCase_ == MsgOneofCase.GetReturnInfoC2S)
		{
			output.WriteRawTag(154, 208, 2);
			output.WriteMessage(GetReturnInfoC2S);
		}
		if (msgCase_ == MsgOneofCase.GetReturnInfoS2C)
		{
			output.WriteRawTag(162, 208, 2);
			output.WriteMessage(GetReturnInfoS2C);
		}
		if (msgCase_ == MsgOneofCase.ReturnGiftClaimC2S)
		{
			output.WriteRawTag(170, 208, 2);
			output.WriteMessage(ReturnGiftClaimC2S);
		}
		if (msgCase_ == MsgOneofCase.ReturnGiftClaimS2C)
		{
			output.WriteRawTag(178, 208, 2);
			output.WriteMessage(ReturnGiftClaimS2C);
		}
		if (msgCase_ == MsgOneofCase.ReturnSignInClaimC2S)
		{
			output.WriteRawTag(186, 208, 2);
			output.WriteMessage(ReturnSignInClaimC2S);
		}
		if (msgCase_ == MsgOneofCase.ReturnSignInClaimS2C)
		{
			output.WriteRawTag(194, 208, 2);
			output.WriteMessage(ReturnSignInClaimS2C);
		}
		if (msgCase_ == MsgOneofCase.ReturnSurveyFinishC2S)
		{
			output.WriteRawTag(202, 208, 2);
			output.WriteMessage(ReturnSurveyFinishC2S);
		}
		if (msgCase_ == MsgOneofCase.ReturnSurveyFinishS2C)
		{
			output.WriteRawTag(210, 208, 2);
			output.WriteMessage(ReturnSurveyFinishS2C);
		}
		if (msgCase_ == MsgOneofCase.FlipCardC2S)
		{
			output.WriteRawTag(218, 208, 2);
			output.WriteMessage(FlipCardC2S);
		}
		if (msgCase_ == MsgOneofCase.FlipCardS2C)
		{
			output.WriteRawTag(226, 208, 2);
			output.WriteMessage(FlipCardS2C);
		}
		if (msgCase_ == MsgOneofCase.FlipCardProgressRewardC2S)
		{
			output.WriteRawTag(234, 208, 2);
			output.WriteMessage(FlipCardProgressRewardC2S);
		}
		if (msgCase_ == MsgOneofCase.FlipCardProgressRewardS2C)
		{
			output.WriteRawTag(242, 208, 2);
			output.WriteMessage(FlipCardProgressRewardS2C);
		}
		if (msgCase_ == MsgOneofCase.GetQuestionUrlC2S)
		{
			output.WriteRawTag(154, 209, 2);
			output.WriteMessage(GetQuestionUrlC2S);
		}
		if (msgCase_ == MsgOneofCase.GetQuestionUrlS2C)
		{
			output.WriteRawTag(162, 209, 2);
			output.WriteMessage(GetQuestionUrlS2C);
		}
		if (msgCase_ == MsgOneofCase.CreateGuildC2S)
		{
			output.WriteRawTag(202, 178, 4);
			output.WriteMessage(CreateGuildC2S);
		}
		if (msgCase_ == MsgOneofCase.CreateGuildS2C)
		{
			output.WriteRawTag(210, 178, 4);
			output.WriteMessage(CreateGuildS2C);
		}
		if (msgCase_ == MsgOneofCase.SearchGuildC2S)
		{
			output.WriteRawTag(218, 178, 4);
			output.WriteMessage(SearchGuildC2S);
		}
		if (msgCase_ == MsgOneofCase.SearchGuildS2C)
		{
			output.WriteRawTag(226, 178, 4);
			output.WriteMessage(SearchGuildS2C);
		}
		if (msgCase_ == MsgOneofCase.ApplyToGuildC2S)
		{
			output.WriteRawTag(234, 178, 4);
			output.WriteMessage(ApplyToGuildC2S);
		}
		if (msgCase_ == MsgOneofCase.ApplyToGuildS2C)
		{
			output.WriteRawTag(242, 178, 4);
			output.WriteMessage(ApplyToGuildS2C);
		}
		if (msgCase_ == MsgOneofCase.ProcessGuildApplicationC2S)
		{
			output.WriteRawTag(250, 178, 4);
			output.WriteMessage(ProcessGuildApplicationC2S);
		}
		if (msgCase_ == MsgOneofCase.ProcessGuildApplicationS2C)
		{
			output.WriteRawTag(130, 179, 4);
			output.WriteMessage(ProcessGuildApplicationS2C);
		}
		if (msgCase_ == MsgOneofCase.SendGuildInvitationC2S)
		{
			output.WriteRawTag(138, 179, 4);
			output.WriteMessage(SendGuildInvitationC2S);
		}
		if (msgCase_ == MsgOneofCase.SendGuildInvitationS2C)
		{
			output.WriteRawTag(146, 179, 4);
			output.WriteMessage(SendGuildInvitationS2C);
		}
		if (msgCase_ == MsgOneofCase.ProcessGuildInvitationC2S)
		{
			output.WriteRawTag(154, 179, 4);
			output.WriteMessage(ProcessGuildInvitationC2S);
		}
		if (msgCase_ == MsgOneofCase.ProcessGuildInvitationS2C)
		{
			output.WriteRawTag(162, 179, 4);
			output.WriteMessage(ProcessGuildInvitationS2C);
		}
		if (msgCase_ == MsgOneofCase.GetGuildInfoC2S)
		{
			output.WriteRawTag(170, 179, 4);
			output.WriteMessage(GetGuildInfoC2S);
		}
		if (msgCase_ == MsgOneofCase.GetGuildInfoS2C)
		{
			output.WriteRawTag(178, 179, 4);
			output.WriteMessage(GetGuildInfoS2C);
		}
		if (msgCase_ == MsgOneofCase.UpdateGuildSettingsC2S)
		{
			output.WriteRawTag(186, 179, 4);
			output.WriteMessage(UpdateGuildSettingsC2S);
		}
		if (msgCase_ == MsgOneofCase.UpdateGuildSettingsS2C)
		{
			output.WriteRawTag(194, 179, 4);
			output.WriteMessage(UpdateGuildSettingsS2C);
		}
		if (msgCase_ == MsgOneofCase.UpdateGuildInAnnouncementC2S)
		{
			output.WriteRawTag(202, 179, 4);
			output.WriteMessage(UpdateGuildInAnnouncementC2S);
		}
		if (msgCase_ == MsgOneofCase.UpdateGuildInAnnouncementS2C)
		{
			output.WriteRawTag(210, 179, 4);
			output.WriteMessage(UpdateGuildInAnnouncementS2C);
		}
		if (msgCase_ == MsgOneofCase.TransferGuildMasterC2S)
		{
			output.WriteRawTag(218, 179, 4);
			output.WriteMessage(TransferGuildMasterC2S);
		}
		if (msgCase_ == MsgOneofCase.TransferGuildMasterS2C)
		{
			output.WriteRawTag(226, 179, 4);
			output.WriteMessage(TransferGuildMasterS2C);
		}
		if (msgCase_ == MsgOneofCase.ChangeGuildMemberTitleC2S)
		{
			output.WriteRawTag(234, 179, 4);
			output.WriteMessage(ChangeGuildMemberTitleC2S);
		}
		if (msgCase_ == MsgOneofCase.ChangeGuildMemberTitleS2C)
		{
			output.WriteRawTag(242, 179, 4);
			output.WriteMessage(ChangeGuildMemberTitleS2C);
		}
		if (msgCase_ == MsgOneofCase.KickGuildMemberC2S)
		{
			output.WriteRawTag(250, 179, 4);
			output.WriteMessage(KickGuildMemberC2S);
		}
		if (msgCase_ == MsgOneofCase.KickGuildMemberS2C)
		{
			output.WriteRawTag(130, 180, 4);
			output.WriteMessage(KickGuildMemberS2C);
		}
		if (msgCase_ == MsgOneofCase.ImpeachGuildMasterC2S)
		{
			output.WriteRawTag(138, 180, 4);
			output.WriteMessage(ImpeachGuildMasterC2S);
		}
		if (msgCase_ == MsgOneofCase.ImpeachGuildMasterS2C)
		{
			output.WriteRawTag(146, 180, 4);
			output.WriteMessage(ImpeachGuildMasterS2C);
		}
		if (msgCase_ == MsgOneofCase.ExitGuildC2S)
		{
			output.WriteRawTag(154, 180, 4);
			output.WriteMessage(ExitGuildC2S);
		}
		if (msgCase_ == MsgOneofCase.ExitGuildS2C)
		{
			output.WriteRawTag(162, 180, 4);
			output.WriteMessage(ExitGuildS2C);
		}
		if (msgCase_ == MsgOneofCase.DisbandGuildC2S)
		{
			output.WriteRawTag(170, 180, 4);
			output.WriteMessage(DisbandGuildC2S);
		}
		if (msgCase_ == MsgOneofCase.DisbandGuildS2C)
		{
			output.WriteRawTag(178, 180, 4);
			output.WriteMessage(DisbandGuildS2C);
		}
		if (msgCase_ == MsgOneofCase.GuildMissionRewardC2S)
		{
			output.WriteRawTag(186, 180, 4);
			output.WriteMessage(GuildMissionRewardC2S);
		}
		if (msgCase_ == MsgOneofCase.GuildMissionRewardS2C)
		{
			output.WriteRawTag(194, 180, 4);
			output.WriteMessage(GuildMissionRewardS2C);
		}
		if (msgCase_ == MsgOneofCase.GetGuildMemberChangeMsgC2S)
		{
			output.WriteRawTag(202, 180, 4);
			output.WriteMessage(GetGuildMemberChangeMsgC2S);
		}
		if (msgCase_ == MsgOneofCase.GetGuildMemberChangeMsgS2C)
		{
			output.WriteRawTag(210, 180, 4);
			output.WriteMessage(GetGuildMemberChangeMsgS2C);
		}
		if (msgCase_ == MsgOneofCase.SendGuildChatMsgC2S)
		{
			output.WriteRawTag(218, 180, 4);
			output.WriteMessage(SendGuildChatMsgC2S);
		}
		if (msgCase_ == MsgOneofCase.SendGuildChatMsgS2C)
		{
			output.WriteRawTag(226, 180, 4);
			output.WriteMessage(SendGuildChatMsgS2C);
		}
		if (msgCase_ == MsgOneofCase.GetGuildChatMsgC2S)
		{
			output.WriteRawTag(234, 180, 4);
			output.WriteMessage(GetGuildChatMsgC2S);
		}
		if (msgCase_ == MsgOneofCase.GetGuildChatMsgS2C)
		{
			output.WriteRawTag(242, 180, 4);
			output.WriteMessage(GetGuildChatMsgS2C);
		}
		if (msgCase_ == MsgOneofCase.GuildMemberC2S)
		{
			output.WriteRawTag(250, 180, 4);
			output.WriteMessage(GuildMemberC2S);
		}
		if (msgCase_ == MsgOneofCase.GuildMemberS2C)
		{
			output.WriteRawTag(130, 181, 4);
			output.WriteMessage(GuildMemberS2C);
		}
		if (msgCase_ == MsgOneofCase.GetGuildsInfoC2S)
		{
			output.WriteRawTag(138, 181, 4);
			output.WriteMessage(GetGuildsInfoC2S);
		}
		if (msgCase_ == MsgOneofCase.GetGuildsInfoS2C)
		{
			output.WriteRawTag(146, 181, 4);
			output.WriteMessage(GetGuildsInfoS2C);
		}
		if (msgCase_ == MsgOneofCase.TestRpcEchoC2S)
		{
			output.WriteRawTag(130, 181, 24);
			output.WriteMessage(TestRpcEchoC2S);
		}
		if (msgCase_ == MsgOneofCase.TestRpcEchoS2C)
		{
			output.WriteRawTag(138, 181, 24);
			output.WriteMessage(TestRpcEchoS2C);
		}
		if (_unknownFields != null)
		{
			_unknownFields.WriteTo(ref output);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public int CalculateSize()
	{
		int num = 0;
		if (msgCase_ == MsgOneofCase.SysSendMailC2S)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(SysSendMailC2S);
		}
		if (msgCase_ == MsgOneofCase.SysPlayerOnlineC2S)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(SysPlayerOnlineC2S);
		}
		if (msgCase_ == MsgOneofCase.PlayerOnlineRoomC2S)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(PlayerOnlineRoomC2S);
		}
		if (msgCase_ == MsgOneofCase.SysCanPraiseInfoC2S)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(SysCanPraiseInfoC2S);
		}
		if (msgCase_ == MsgOneofCase.SysPraiseC2S)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(SysPraiseC2S);
		}
		if (msgCase_ == MsgOneofCase.SysRoomFinishC2S)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(SysRoomFinishC2S);
		}
		if (msgCase_ == MsgOneofCase.SysRoomAddExpC2S)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(SysRoomAddExpC2S);
		}
		if (msgCase_ == MsgOneofCase.SysGetShowFriendC2S)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(SysGetShowFriendC2S);
		}
		if (msgCase_ == MsgOneofCase.SysFriendInviteC2S)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(SysFriendInviteC2S);
		}
		if (msgCase_ == MsgOneofCase.SysFriendDelC2S)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(SysFriendDelC2S);
		}
		if (msgCase_ == MsgOneofCase.SysFriendInfoC2S)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(SysFriendInfoC2S);
		}
		if (msgCase_ == MsgOneofCase.SysFriendApplyC2S)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(SysFriendApplyC2S);
		}
		if (msgCase_ == MsgOneofCase.SysFriendAddC2S)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(SysFriendAddC2S);
		}
		if (msgCase_ == MsgOneofCase.SysFriendSendMsgC2S)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(SysFriendSendMsgC2S);
		}
		if (msgCase_ == MsgOneofCase.SysCampaignFinishC2S)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(SysCampaignFinishC2S);
		}
		if (msgCase_ == MsgOneofCase.SysAbroadPayMsgC2S)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(SysAbroadPayMsgC2S);
		}
		if (msgCase_ == MsgOneofCase.AcquisitionMsgC2S)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(AcquisitionMsgC2S);
		}
		if (msgCase_ == MsgOneofCase.SysFightRecord)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(SysFightRecord);
		}
		if (msgCase_ == MsgOneofCase.SysCampaignAwardC2S)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(SysCampaignAwardC2S);
		}
		if (msgCase_ == MsgOneofCase.SysRecoupItemC2S)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(SysRecoupItemC2S);
		}
		if (msgCase_ == MsgOneofCase.SysPlayerOnlineRoomC2S)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(SysPlayerOnlineRoomC2S);
		}
		if (msgCase_ == MsgOneofCase.SysPlayerCleanC2S)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(SysPlayerCleanC2S);
		}
		if (msgCase_ == MsgOneofCase.SysPlayerPunishmentTimeC2S)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(SysPlayerPunishmentTimeC2S);
		}
		if (msgCase_ == MsgOneofCase.SysMatchSuccess)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(SysMatchSuccess);
		}
		if (msgCase_ == MsgOneofCase.SysChinaPayMsgC2S)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(SysChinaPayMsgC2S);
		}
		if (msgCase_ == MsgOneofCase.SysChangeMatchTeamState)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(SysChangeMatchTeamState);
		}
		if (msgCase_ == MsgOneofCase.SysSaveSimplePlayerInfoC2S)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(SysSaveSimplePlayerInfoC2S);
		}
		if (msgCase_ == MsgOneofCase.SysGmChangeNameC2S)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(SysGmChangeNameC2S);
		}
		if (msgCase_ == MsgOneofCase.SysSyncPlayerC2S)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(SysSyncPlayerC2S);
		}
		if (msgCase_ == MsgOneofCase.SysPlayerCreditScoreChangeC2S)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(SysPlayerCreditScoreChangeC2S);
		}
		if (msgCase_ == MsgOneofCase.SysSyncPlayerMatchPunishmentTimeC2S)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(SysSyncPlayerMatchPunishmentTimeC2S);
		}
		if (msgCase_ == MsgOneofCase.GMChangeCreditScoreC2S)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(GMChangeCreditScoreC2S);
		}
		if (msgCase_ == MsgOneofCase.SysPushReturnInfoC2S)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(SysPushReturnInfoC2S);
		}
		if (msgCase_ == MsgOneofCase.SysMutePlayerC2S)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(SysMutePlayerC2S);
		}
		if (msgCase_ == MsgOneofCase.SysQuestionC2S)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(SysQuestionC2S);
		}
		if (msgCase_ == MsgOneofCase.KickS2C)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(KickS2C);
		}
		if (msgCase_ == MsgOneofCase.PredictActionS2C)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(PredictActionS2C);
		}
		if (msgCase_ == MsgOneofCase.RunningGameS2C)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(RunningGameS2C);
		}
		if (msgCase_ == MsgOneofCase.BattleS2C)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(BattleS2C);
		}
		if (msgCase_ == MsgOneofCase.LotteryDrawS2C)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(LotteryDrawS2C);
		}
		if (msgCase_ == MsgOneofCase.LandBuffsS2C)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(LandBuffsS2C);
		}
		if (msgCase_ == MsgOneofCase.RoundStartS2C)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(RoundStartS2C);
		}
		if (msgCase_ == MsgOneofCase.GameFinishS2C)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(GameFinishS2C);
		}
		if (msgCase_ == MsgOneofCase.MonsterRefreshS2C)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(MonsterRefreshS2C);
		}
		if (msgCase_ == MsgOneofCase.MovePointBuffS2C)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(MovePointBuffS2C);
		}
		if (msgCase_ == MsgOneofCase.ChangeDirS2C)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(ChangeDirS2C);
		}
		if (msgCase_ == MsgOneofCase.GambleChangeS2C)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(GambleChangeS2C);
		}
		if (msgCase_ == MsgOneofCase.HeroBarBoxChangeS2C)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(HeroBarBoxChangeS2C);
		}
		if (msgCase_ == MsgOneofCase.RoomNotifyS2C)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(RoomNotifyS2C);
		}
		if (msgCase_ == MsgOneofCase.ActionStartNotifyS2C)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(ActionStartNotifyS2C);
		}
		if (msgCase_ == MsgOneofCase.NoGambleNotifyS2C)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(NoGambleNotifyS2C);
		}
		if (msgCase_ == MsgOneofCase.GambleObServeS2C)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(GambleObServeS2C);
		}
		if (msgCase_ == MsgOneofCase.UpdateHeroAttrS2C)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(UpdateHeroAttrS2C);
		}
		if (msgCase_ == MsgOneofCase.ChangePlayerSlotS2C)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(ChangePlayerSlotS2C);
		}
		if (msgCase_ == MsgOneofCase.BossSleepS2C)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(BossSleepS2C);
		}
		if (msgCase_ == MsgOneofCase.RefMallS2C)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(RefMallS2C);
		}
		if (msgCase_ == MsgOneofCase.BagItemChangeS2C)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(BagItemChangeS2C);
		}
		if (msgCase_ == MsgOneofCase.RoleCardChangeS2C)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(RoleCardChangeS2C);
		}
		if (msgCase_ == MsgOneofCase.TaskConditionS2C)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(TaskConditionS2C);
		}
		if (msgCase_ == MsgOneofCase.TaskInfoS2C)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(TaskInfoS2C);
		}
		if (msgCase_ == MsgOneofCase.PlayerOnlineS2C)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(PlayerOnlineS2C);
		}
		if (msgCase_ == MsgOneofCase.ChangeExpS2C)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(ChangeExpS2C);
		}
		if (msgCase_ == MsgOneofCase.MailAddS2C)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(MailAddS2C);
		}
		if (msgCase_ == MsgOneofCase.OnlineSyncRoomIdS2C)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(OnlineSyncRoomIdS2C);
		}
		if (msgCase_ == MsgOneofCase.NoticeS2C)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(NoticeS2C);
		}
		if (msgCase_ == MsgOneofCase.ActivityTaskConditionS2C)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(ActivityTaskConditionS2C);
		}
		if (msgCase_ == MsgOneofCase.MapEventS2C)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(MapEventS2C);
		}
		if (msgCase_ == MsgOneofCase.Day7RewardS2C)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(Day7RewardS2C);
		}
		if (msgCase_ == MsgOneofCase.MapEventTrainS2C)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(MapEventTrainS2C);
		}
		if (msgCase_ == MsgOneofCase.ChangePraiseNumS2C)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(ChangePraiseNumS2C);
		}
		if (msgCase_ == MsgOneofCase.MonthlyCardS2C)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(MonthlyCardS2C);
		}
		if (msgCase_ == MsgOneofCase.MailDelS2C)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(MailDelS2C);
		}
		if (msgCase_ == MsgOneofCase.FriendNotifyS2C)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(FriendNotifyS2C);
		}
		if (msgCase_ == MsgOneofCase.FriendListChangeS2C)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(FriendListChangeS2C);
		}
		if (msgCase_ == MsgOneofCase.FriendInviteNotifyS2C)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(FriendInviteNotifyS2C);
		}
		if (msgCase_ == MsgOneofCase.LoopNoticeS2C)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(LoopNoticeS2C);
		}
		if (msgCase_ == MsgOneofCase.BattlePassLvS2C)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(BattlePassLvS2C);
		}
		if (msgCase_ == MsgOneofCase.BattlePassTaskInfoS2C)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(BattlePassTaskInfoS2C);
		}
		if (msgCase_ == MsgOneofCase.BattlePassUpdateTaskS2C)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(BattlePassUpdateTaskS2C);
		}
		if (msgCase_ == MsgOneofCase.BattlePassBuyS2C)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(BattlePassBuyS2C);
		}
		if (msgCase_ == MsgOneofCase.BattlePassInfoS2C)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(BattlePassInfoS2C);
		}
		if (msgCase_ == MsgOneofCase.FriendsChatMsgS2C)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(FriendsChatMsgS2C);
		}
		if (msgCase_ == MsgOneofCase.GameProgressChangeS2C)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(GameProgressChangeS2C);
		}
		if (msgCase_ == MsgOneofCase.MapMissionNotifyS2C)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(MapMissionNotifyS2C);
		}
		if (msgCase_ == MsgOneofCase.ChangeItemLimitS2C)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(ChangeItemLimitS2C);
		}
		if (msgCase_ == MsgOneofCase.CleanItemLimitS2C)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(CleanItemLimitS2C);
		}
		if (msgCase_ == MsgOneofCase.CampaignPassS2C)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(CampaignPassS2C);
		}
		if (msgCase_ == MsgOneofCase.CampaignNotifyS2C)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(CampaignNotifyS2C);
		}
		if (msgCase_ == MsgOneofCase.KillMessageS2C)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(KillMessageS2C);
		}
		if (msgCase_ == MsgOneofCase.GameScoreChangeS2C)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(GameScoreChangeS2C);
		}
		if (msgCase_ == MsgOneofCase.SignInRewardS2C)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(SignInRewardS2C);
		}
		if (msgCase_ == MsgOneofCase.PayResultS2C)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(PayResultS2C);
		}
		if (msgCase_ == MsgOneofCase.PayInfoChangeS2C)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(PayInfoChangeS2C);
		}
		if (msgCase_ == MsgOneofCase.InviteSuccessS2C)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(InviteSuccessS2C);
		}
		if (msgCase_ == MsgOneofCase.InviteInfoNotifyS2C)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(InviteInfoNotifyS2C);
		}
		if (msgCase_ == MsgOneofCase.MapStatusChangeS2C)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(MapStatusChangeS2C);
		}
		if (msgCase_ == MsgOneofCase.GachaCountS2C)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(GachaCountS2C);
		}
		if (msgCase_ == MsgOneofCase.MapIndexChangeS2C)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(MapIndexChangeS2C);
		}
		if (msgCase_ == MsgOneofCase.SurrenderPunishS2C)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(SurrenderPunishS2C);
		}
		if (msgCase_ == MsgOneofCase.GamePassMapSuccessS2C)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(GamePassMapSuccessS2C);
		}
		if (msgCase_ == MsgOneofCase.FriendDelNotifyS2C)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(FriendDelNotifyS2C);
		}
		if (msgCase_ == MsgOneofCase.BagExpiredTransformNotify)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(BagExpiredTransformNotify);
		}
		if (msgCase_ == MsgOneofCase.MapEventCrabS2C)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(MapEventCrabS2C);
		}
		if (msgCase_ == MsgOneofCase.PkAfterVoteS2C)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(PkAfterVoteS2C);
		}
		if (msgCase_ == MsgOneofCase.PlayerTaskNotifyS2C)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(PlayerTaskNotifyS2C);
		}
		if (msgCase_ == MsgOneofCase.UnLockDifficultyS2C)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(UnLockDifficultyS2C);
		}
		if (msgCase_ == MsgOneofCase.HeroSkillMoveEffectS2C)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(HeroSkillMoveEffectS2C);
		}
		if (msgCase_ == MsgOneofCase.TimeOutKickPlayerS2C)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(TimeOutKickPlayerS2C);
		}
		if (msgCase_ == MsgOneofCase.SayPhraseNotifyS2C)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(SayPhraseNotifyS2C);
		}
		if (msgCase_ == MsgOneofCase.MatchTeamInviteNotify)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(MatchTeamInviteNotify);
		}
		if (msgCase_ == MsgOneofCase.RefreshMatchTeamStateNotify)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(RefreshMatchTeamStateNotify);
		}
		if (msgCase_ == MsgOneofCase.ActivityPassGearChangeS2C)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(ActivityPassGearChangeS2C);
		}
		if (msgCase_ == MsgOneofCase.SingleGameScoreChange)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(SingleGameScoreChange);
		}
		if (msgCase_ == MsgOneofCase.DelayProgressMapEventS2C)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(DelayProgressMapEventS2C);
		}
		if (msgCase_ == MsgOneofCase.LuckyStarMissionChangeS2C)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(LuckyStarMissionChangeS2C);
		}
		if (msgCase_ == MsgOneofCase.MatchPunishmentS2C)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(MatchPunishmentS2C);
		}
		if (msgCase_ == MsgOneofCase.ChallengeDataChangeS2C)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(ChallengeDataChangeS2C);
		}
		if (msgCase_ == MsgOneofCase.GmUnlockRoleInfoS2C)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(GmUnlockRoleInfoS2C);
		}
		if (msgCase_ == MsgOneofCase.SyncPlayerCreditInfoS2C)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(SyncPlayerCreditInfoS2C);
		}
		if (msgCase_ == MsgOneofCase.RoomHeroCardChangeS2C)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(RoomHeroCardChangeS2C);
		}
		if (msgCase_ == MsgOneofCase.RoomRoundAddTermS2C)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(RoomRoundAddTermS2C);
		}
		if (msgCase_ == MsgOneofCase.ReturnInfoS2C)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(ReturnInfoS2C);
		}
		if (msgCase_ == MsgOneofCase.SyncRelicsS2C)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(SyncRelicsS2C);
		}
		if (msgCase_ == MsgOneofCase.ReplaySnapshotS2C)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(ReplaySnapshotS2C);
		}
		if (msgCase_ == MsgOneofCase.ClueNotifyS2C)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(ClueNotifyS2C);
		}
		if (msgCase_ == MsgOneofCase.ReplayDieS2C)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(ReplayDieS2C);
		}
		if (msgCase_ == MsgOneofCase.GuildTaskNotifyS2C)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(GuildTaskNotifyS2C);
		}
		if (msgCase_ == MsgOneofCase.GameRoundChangeS2C)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(GameRoundChangeS2C);
		}
		if (msgCase_ == MsgOneofCase.NotifyQuestionS2C)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(NotifyQuestionS2C);
		}
		if (msgCase_ == MsgOneofCase.ConnectHandle)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(ConnectHandle);
		}
		if (msgCase_ == MsgOneofCase.Connect)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(Connect);
		}
		if (msgCase_ == MsgOneofCase.HeartbeatHandle)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(HeartbeatHandle);
		}
		if (msgCase_ == MsgOneofCase.Heartbeat)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(Heartbeat);
		}
		if (msgCase_ == MsgOneofCase.CreateRoomHandle)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(CreateRoomHandle);
		}
		if (msgCase_ == MsgOneofCase.CreateRoom)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(CreateRoom);
		}
		if (msgCase_ == MsgOneofCase.SyncRoomHandle)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(SyncRoomHandle);
		}
		if (msgCase_ == MsgOneofCase.SyncRoom)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(SyncRoom);
		}
		if (msgCase_ == MsgOneofCase.JoinRoomHandle)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(JoinRoomHandle);
		}
		if (msgCase_ == MsgOneofCase.JoinRoom)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(JoinRoom);
		}
		if (msgCase_ == MsgOneofCase.ExitRoomHandle)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(ExitRoomHandle);
		}
		if (msgCase_ == MsgOneofCase.ExitRoom)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(ExitRoom);
		}
		if (msgCase_ == MsgOneofCase.QueryRoomHandle)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(QueryRoomHandle);
		}
		if (msgCase_ == MsgOneofCase.QueryRoom)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(QueryRoom);
		}
		if (msgCase_ == MsgOneofCase.RefreshRoomStateHandle)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(RefreshRoomStateHandle);
		}
		if (msgCase_ == MsgOneofCase.RefreshRoomState)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(RefreshRoomState);
		}
		if (msgCase_ == MsgOneofCase.StartGameHandle)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(StartGameHandle);
		}
		if (msgCase_ == MsgOneofCase.StartGame)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(StartGame);
		}
		if (msgCase_ == MsgOneofCase.ThrowDiceHandle)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(ThrowDiceHandle);
		}
		if (msgCase_ == MsgOneofCase.ThrowDice)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(ThrowDice);
		}
		if (msgCase_ == MsgOneofCase.ChangeRoomHandle)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(ChangeRoomHandle);
		}
		if (msgCase_ == MsgOneofCase.ChangeRoom)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(ChangeRoom);
		}
		if (msgCase_ == MsgOneofCase.MoveHandle)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(MoveHandle);
		}
		if (msgCase_ == MsgOneofCase.Move)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(Move);
		}
		if (msgCase_ == MsgOneofCase.ShopBuyHandle)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(ShopBuyHandle);
		}
		if (msgCase_ == MsgOneofCase.ShopBuy)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(ShopBuy);
		}
		if (msgCase_ == MsgOneofCase.PursuitHandle)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(PursuitHandle);
		}
		if (msgCase_ == MsgOneofCase.Pursuit)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(Pursuit);
		}
		if (msgCase_ == MsgOneofCase.BattleUseCardHandle)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(BattleUseCardHandle);
		}
		if (msgCase_ == MsgOneofCase.BattleUseCard)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(BattleUseCard);
		}
		if (msgCase_ == MsgOneofCase.BattleThrowDiceHandle)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(BattleThrowDiceHandle);
		}
		if (msgCase_ == MsgOneofCase.BattleThrowDice)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(BattleThrowDice);
		}
		if (msgCase_ == MsgOneofCase.BattleChoiceHandle)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(BattleChoiceHandle);
		}
		if (msgCase_ == MsgOneofCase.BattleChoice)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(BattleChoice);
		}
		if (msgCase_ == MsgOneofCase.LotteryChoiceHandle)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(LotteryChoiceHandle);
		}
		if (msgCase_ == MsgOneofCase.LotteryChoice)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(LotteryChoice);
		}
		if (msgCase_ == MsgOneofCase.MoveAgainHandle)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(MoveAgainHandle);
		}
		if (msgCase_ == MsgOneofCase.MoveAgain)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(MoveAgain);
		}
		if (msgCase_ == MsgOneofCase.AskBattleHandle)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(AskBattleHandle);
		}
		if (msgCase_ == MsgOneofCase.AskBattle)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(AskBattle);
		}
		if (msgCase_ == MsgOneofCase.RollGoldHandle)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(RollGoldHandle);
		}
		if (msgCase_ == MsgOneofCase.RollGold)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(RollGold);
		}
		if (msgCase_ == MsgOneofCase.EventThrowDiceHandle)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(EventThrowDiceHandle);
		}
		if (msgCase_ == MsgOneofCase.EventThrowDice)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(EventThrowDice);
		}
		if (msgCase_ == MsgOneofCase.TriggerEventHandle)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(TriggerEventHandle);
		}
		if (msgCase_ == MsgOneofCase.TriggerEvent)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(TriggerEvent);
		}
		if (msgCase_ == MsgOneofCase.UseEffectCardHandle)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(UseEffectCardHandle);
		}
		if (msgCase_ == MsgOneofCase.UseEffectCard)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(UseEffectCard);
		}
		if (msgCase_ == MsgOneofCase.BombThrowDiceHandle)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(BombThrowDiceHandle);
		}
		if (msgCase_ == MsgOneofCase.BombThrowDice)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(BombThrowDice);
		}
		if (msgCase_ == MsgOneofCase.ChoiceDirectionHandle)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(ChoiceDirectionHandle);
		}
		if (msgCase_ == MsgOneofCase.ChoiceDirection)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(ChoiceDirection);
		}
		if (msgCase_ == MsgOneofCase.LandChoiceTargetHandle)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(LandChoiceTargetHandle);
		}
		if (msgCase_ == MsgOneofCase.LandChoiceTarget)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(LandChoiceTarget);
		}
		if (msgCase_ == MsgOneofCase.ThrowDiceResultHandle)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(ThrowDiceResultHandle);
		}
		if (msgCase_ == MsgOneofCase.ThrowDiceResult)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(ThrowDiceResult);
		}
		if (msgCase_ == MsgOneofCase.TriggerDivinationHandle)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(TriggerDivinationHandle);
		}
		if (msgCase_ == MsgOneofCase.TriggerDivination)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(TriggerDivination);
		}
		if (msgCase_ == MsgOneofCase.TriggerDestinyHandle)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(TriggerDestinyHandle);
		}
		if (msgCase_ == MsgOneofCase.TriggerDestiny)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(TriggerDestiny);
		}
		if (msgCase_ == MsgOneofCase.UseQuickCardHandle)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(UseQuickCardHandle);
		}
		if (msgCase_ == MsgOneofCase.UseQuickCard)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(UseQuickCard);
		}
		if (msgCase_ == MsgOneofCase.AbandonCardHandle)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(AbandonCardHandle);
		}
		if (msgCase_ == MsgOneofCase.AbandonCard)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(AbandonCard);
		}
		if (msgCase_ == MsgOneofCase.StopOrContinueHandle)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(StopOrContinueHandle);
		}
		if (msgCase_ == MsgOneofCase.StopOrContinue)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(StopOrContinue);
		}
		if (msgCase_ == MsgOneofCase.StartGambleHandle)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(StartGambleHandle);
		}
		if (msgCase_ == MsgOneofCase.StartGamble)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(StartGamble);
		}
		if (msgCase_ == MsgOneofCase.GambleThrowDicHandle)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(GambleThrowDicHandle);
		}
		if (msgCase_ == MsgOneofCase.GambleThrowDic)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(GambleThrowDic);
		}
		if (msgCase_ == MsgOneofCase.ChoiceHeroC2S2Handle)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(ChoiceHeroC2S2Handle);
		}
		if (msgCase_ == MsgOneofCase.ChoiceHero2)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(ChoiceHero2);
		}
		if (msgCase_ == MsgOneofCase.AffirmHeroHandle)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(AffirmHeroHandle);
		}
		if (msgCase_ == MsgOneofCase.AffirmHero)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(AffirmHero);
		}
		if (msgCase_ == MsgOneofCase.SearchRoomHandle)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(SearchRoomHandle);
		}
		if (msgCase_ == MsgOneofCase.SearchRoom)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(SearchRoom);
		}
		if (msgCase_ == MsgOneofCase.GmHandle)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(GmHandle);
		}
		if (msgCase_ == MsgOneofCase.Gm)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(Gm);
		}
		if (msgCase_ == MsgOneofCase.TriggerHospitalHandle)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(TriggerHospitalHandle);
		}
		if (msgCase_ == MsgOneofCase.TriggerHospital)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(TriggerHospital);
		}
		if (msgCase_ == MsgOneofCase.SendChatHandle)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(SendChatHandle);
		}
		if (msgCase_ == MsgOneofCase.SendChat)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(SendChat);
		}
		if (msgCase_ == MsgOneofCase.PlayerShopBuyC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(PlayerShopBuyC2S);
		}
		if (msgCase_ == MsgOneofCase.PlayerShopBuyS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(PlayerShopBuyS2C);
		}
		if (msgCase_ == MsgOneofCase.PlayerUseItemHandle)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(PlayerUseItemHandle);
		}
		if (msgCase_ == MsgOneofCase.PlayerUseItem)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(PlayerUseItem);
		}
		if (msgCase_ == MsgOneofCase.UseTreasureC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(UseTreasureC2S);
		}
		if (msgCase_ == MsgOneofCase.UseTreasureS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(UseTreasureS2C);
		}
		if (msgCase_ == MsgOneofCase.UseTreasureAutoTransformC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(UseTreasureAutoTransformC2S);
		}
		if (msgCase_ == MsgOneofCase.UseTreasureAutoTransformS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(UseTreasureAutoTransformS2C);
		}
		if (msgCase_ == MsgOneofCase.SetFashionC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(SetFashionC2S);
		}
		if (msgCase_ == MsgOneofCase.SetFashionS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(SetFashionS2C);
		}
		if (msgCase_ == MsgOneofCase.SelectFashionPlanC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(SelectFashionPlanC2S);
		}
		if (msgCase_ == MsgOneofCase.SelectFashionPlanS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(SelectFashionPlanS2C);
		}
		if (msgCase_ == MsgOneofCase.GachaC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(GachaC2S);
		}
		if (msgCase_ == MsgOneofCase.GachaS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(GachaS2C);
		}
		if (msgCase_ == MsgOneofCase.SteamSearchRoomC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(SteamSearchRoomC2S);
		}
		if (msgCase_ == MsgOneofCase.SteamSearchRoomS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(SteamSearchRoomS2C);
		}
		if (msgCase_ == MsgOneofCase.CheatItemHandle)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(CheatItemHandle);
		}
		if (msgCase_ == MsgOneofCase.CheatItem)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(CheatItem);
		}
		if (msgCase_ == MsgOneofCase.RoleCardUpLvC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(RoleCardUpLvC2S);
		}
		if (msgCase_ == MsgOneofCase.RoleCardUpLvS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(RoleCardUpLvS2C);
		}
		if (msgCase_ == MsgOneofCase.RoleCardBreakThroughC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(RoleCardBreakThroughC2S);
		}
		if (msgCase_ == MsgOneofCase.RoleCardBreakThroughS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(RoleCardBreakThroughS2C);
		}
		if (msgCase_ == MsgOneofCase.RoleCardChoiceResC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(RoleCardChoiceResC2S);
		}
		if (msgCase_ == MsgOneofCase.RoleCardChoiceResS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(RoleCardChoiceResS2C);
		}
		if (msgCase_ == MsgOneofCase.TaskRewardC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(TaskRewardC2S);
		}
		if (msgCase_ == MsgOneofCase.TaskRewardS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(TaskRewardS2C);
		}
		if (msgCase_ == MsgOneofCase.TeachingC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(TeachingC2S);
		}
		if (msgCase_ == MsgOneofCase.TeachingS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(TeachingS2C);
		}
		if (msgCase_ == MsgOneofCase.QuickJoinRoomC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(QuickJoinRoomC2S);
		}
		if (msgCase_ == MsgOneofCase.QuickJoinRoomS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(QuickJoinRoomS2C);
		}
		if (msgCase_ == MsgOneofCase.RoomKickPlayerC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(RoomKickPlayerC2S);
		}
		if (msgCase_ == MsgOneofCase.RoomKickPlayerS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(RoomKickPlayerS2C);
		}
		if (msgCase_ == MsgOneofCase.RoomAbdicationC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(RoomAbdicationC2S);
		}
		if (msgCase_ == MsgOneofCase.RoomAbdicationS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(RoomAbdicationS2C);
		}
		if (msgCase_ == MsgOneofCase.RoomReadyC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(RoomReadyC2S);
		}
		if (msgCase_ == MsgOneofCase.RoomReadyS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(RoomReadyS2C);
		}
		if (msgCase_ == MsgOneofCase.ChargeCreateC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(ChargeCreateC2S);
		}
		if (msgCase_ == MsgOneofCase.ChargeCreateS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(ChargeCreateS2C);
		}
		if (msgCase_ == MsgOneofCase.ChargeC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(ChargeC2S);
		}
		if (msgCase_ == MsgOneofCase.ChargeS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(ChargeS2C);
		}
		if (msgCase_ == MsgOneofCase.GiftCdkC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(GiftCdkC2S);
		}
		if (msgCase_ == MsgOneofCase.GiftCdkS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(GiftCdkS2C);
		}
		if (msgCase_ == MsgOneofCase.MailReadC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(MailReadC2S);
		}
		if (msgCase_ == MsgOneofCase.MailReadS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(MailReadS2C);
		}
		if (msgCase_ == MsgOneofCase.MailGetRewardC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(MailGetRewardC2S);
		}
		if (msgCase_ == MsgOneofCase.MailGetRewardS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(MailGetRewardS2C);
		}
		if (msgCase_ == MsgOneofCase.MailDelReadC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(MailDelReadC2S);
		}
		if (msgCase_ == MsgOneofCase.MailDelReadS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(MailDelReadS2C);
		}
		if (msgCase_ == MsgOneofCase.GachaRecordC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(GachaRecordC2S);
		}
		if (msgCase_ == MsgOneofCase.GachaRecordS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(GachaRecordS2C);
		}
		if (msgCase_ == MsgOneofCase.ActivityTaskRewardC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(ActivityTaskRewardC2S);
		}
		if (msgCase_ == MsgOneofCase.ActivityTaskRewardS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(ActivityTaskRewardS2C);
		}
		if (msgCase_ == MsgOneofCase.RoomShortChatC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(RoomShortChatC2S);
		}
		if (msgCase_ == MsgOneofCase.RoomShortChatS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(RoomShortChatS2C);
		}
		if (msgCase_ == MsgOneofCase.SetShowPlayerC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(SetShowPlayerC2S);
		}
		if (msgCase_ == MsgOneofCase.SetShowPlayerS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(SetShowPlayerS2C);
		}
		if (msgCase_ == MsgOneofCase.GetShowPlayerC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(GetShowPlayerC2S);
		}
		if (msgCase_ == MsgOneofCase.GetShowPlayerS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(GetShowPlayerS2C);
		}
		if (msgCase_ == MsgOneofCase.GetPlayerFightRecordC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(GetPlayerFightRecordC2S);
		}
		if (msgCase_ == MsgOneofCase.GetPlayerFightRecordS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(GetPlayerFightRecordS2C);
		}
		if (msgCase_ == MsgOneofCase.GetDay7RewardC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(GetDay7RewardC2S);
		}
		if (msgCase_ == MsgOneofCase.GetDay7RewardS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(GetDay7RewardS2C);
		}
		if (msgCase_ == MsgOneofCase.PraisePlayerC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(PraisePlayerC2S);
		}
		if (msgCase_ == MsgOneofCase.PraisePlayerS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(PraisePlayerS2C);
		}
		if (msgCase_ == MsgOneofCase.ClientDataUploadC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(ClientDataUploadC2S);
		}
		if (msgCase_ == MsgOneofCase.ClientDataUploadS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(ClientDataUploadS2C);
		}
		if (msgCase_ == MsgOneofCase.FriendListC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(FriendListC2S);
		}
		if (msgCase_ == MsgOneofCase.FriendListS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(FriendListS2C);
		}
		if (msgCase_ == MsgOneofCase.FriendApplyC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(FriendApplyC2S);
		}
		if (msgCase_ == MsgOneofCase.FriendApplyS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(FriendApplyS2C);
		}
		if (msgCase_ == MsgOneofCase.FriendApplyListC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(FriendApplyListC2S);
		}
		if (msgCase_ == MsgOneofCase.FriendApplyListS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(FriendApplyListS2C);
		}
		if (msgCase_ == MsgOneofCase.FriendApplyOpC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(FriendApplyOpC2S);
		}
		if (msgCase_ == MsgOneofCase.FriendApplyOpS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(FriendApplyOpS2C);
		}
		if (msgCase_ == MsgOneofCase.FriendOpC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(FriendOpC2S);
		}
		if (msgCase_ == MsgOneofCase.FriendOpS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(FriendOpS2C);
		}
		if (msgCase_ == MsgOneofCase.FriendInviteC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(FriendInviteC2S);
		}
		if (msgCase_ == MsgOneofCase.FriendInviteS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(FriendInviteS2C);
		}
		if (msgCase_ == MsgOneofCase.FriendInviteListC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(FriendInviteListC2S);
		}
		if (msgCase_ == MsgOneofCase.FriendInviteListS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(FriendInviteListS2C);
		}
		if (msgCase_ == MsgOneofCase.FriendInviteCleanC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(FriendInviteCleanC2S);
		}
		if (msgCase_ == MsgOneofCase.FriendInviteCleanS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(FriendInviteCleanS2C);
		}
		if (msgCase_ == MsgOneofCase.FriendBlacksListC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(FriendBlacksListC2S);
		}
		if (msgCase_ == MsgOneofCase.FriendBlacksListS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(FriendBlacksListS2C);
		}
		if (msgCase_ == MsgOneofCase.NearFightPlayerC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(NearFightPlayerC2S);
		}
		if (msgCase_ == MsgOneofCase.NearFightPlayerS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(NearFightPlayerS2C);
		}
		if (msgCase_ == MsgOneofCase.SearchPlayerC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(SearchPlayerC2S);
		}
		if (msgCase_ == MsgOneofCase.SearchPlayerS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(SearchPlayerS2C);
		}
		if (msgCase_ == MsgOneofCase.ScratchCardC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(ScratchCardC2S);
		}
		if (msgCase_ == MsgOneofCase.ScratchCardS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(ScratchCardS2C);
		}
		if (msgCase_ == MsgOneofCase.NextScratchCardPoolC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(NextScratchCardPoolC2S);
		}
		if (msgCase_ == MsgOneofCase.NextScratchCardPoolS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(NextScratchCardPoolS2C);
		}
		if (msgCase_ == MsgOneofCase.WatchJoinRoomC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(WatchJoinRoomC2S);
		}
		if (msgCase_ == MsgOneofCase.WatchJoinRoomS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(WatchJoinRoomS2C);
		}
		if (msgCase_ == MsgOneofCase.WatchRefreshRoomStateC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(WatchRefreshRoomStateC2S);
		}
		if (msgCase_ == MsgOneofCase.WatchRefreshRoomStateS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(WatchRefreshRoomStateS2C);
		}
		if (msgCase_ == MsgOneofCase.WatchExitRoomC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(WatchExitRoomC2S);
		}
		if (msgCase_ == MsgOneofCase.WatchExitRoomS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(WatchExitRoomS2C);
		}
		if (msgCase_ == MsgOneofCase.BattlePassGetRewardC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(BattlePassGetRewardC2S);
		}
		if (msgCase_ == MsgOneofCase.BattlePassGetRewardS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(BattlePassGetRewardS2C);
		}
		if (msgCase_ == MsgOneofCase.BattlePassTaskRewardC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(BattlePassTaskRewardC2S);
		}
		if (msgCase_ == MsgOneofCase.BattlePassTaskRewardS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(BattlePassTaskRewardS2C);
		}
		if (msgCase_ == MsgOneofCase.BattlePassUpLvC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(BattlePassUpLvC2S);
		}
		if (msgCase_ == MsgOneofCase.BattlePassUpLvS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(BattlePassUpLvS2C);
		}
		if (msgCase_ == MsgOneofCase.FriendSendMsgC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(FriendSendMsgC2S);
		}
		if (msgCase_ == MsgOneofCase.FriendSendMsgS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(FriendSendMsgS2C);
		}
		if (msgCase_ == MsgOneofCase.GetChatMsgC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(GetChatMsgC2S);
		}
		if (msgCase_ == MsgOneofCase.GetChatMsgS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(GetChatMsgS2C);
		}
		if (msgCase_ == MsgOneofCase.ReadChatMsgC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(ReadChatMsgC2S);
		}
		if (msgCase_ == MsgOneofCase.ReadChatMsgS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(ReadChatMsgS2C);
		}
		if (msgCase_ == MsgOneofCase.DelChatMsgInfoC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(DelChatMsgInfoC2S);
		}
		if (msgCase_ == MsgOneofCase.DelChatMsgInfoS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(DelChatMsgInfoS2C);
		}
		if (msgCase_ == MsgOneofCase.SelectRelicC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(SelectRelicC2S);
		}
		if (msgCase_ == MsgOneofCase.SelectRelicS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(SelectRelicS2C);
		}
		if (msgCase_ == MsgOneofCase.MonsterPursuitC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(MonsterPursuitC2S);
		}
		if (msgCase_ == MsgOneofCase.MonsterPursuitS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(MonsterPursuitS2C);
		}
		if (msgCase_ == MsgOneofCase.PVEShopBuyC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(PVEShopBuyC2S);
		}
		if (msgCase_ == MsgOneofCase.PVEShopBuyS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(PVEShopBuyS2C);
		}
		if (msgCase_ == MsgOneofCase.ClientCheckTaskC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(ClientCheckTaskC2S);
		}
		if (msgCase_ == MsgOneofCase.ClientCheckTaskS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(ClientCheckTaskS2C);
		}
		if (msgCase_ == MsgOneofCase.PveHeroUpLvC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(PveHeroUpLvC2S);
		}
		if (msgCase_ == MsgOneofCase.PveHeroUpLvS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(PveHeroUpLvS2C);
		}
		if (msgCase_ == MsgOneofCase.StartMatchC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(StartMatchC2S);
		}
		if (msgCase_ == MsgOneofCase.StartMatchS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(StartMatchS2C);
		}
		if (msgCase_ == MsgOneofCase.CancelMatchC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(CancelMatchC2S);
		}
		if (msgCase_ == MsgOneofCase.CancelMatchS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(CancelMatchS2C);
		}
		if (msgCase_ == MsgOneofCase.MatchSuccessC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(MatchSuccessC2S);
		}
		if (msgCase_ == MsgOneofCase.MatchSuccessS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(MatchSuccessS2C);
		}
		if (msgCase_ == MsgOneofCase.AccuseC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(AccuseC2S);
		}
		if (msgCase_ == MsgOneofCase.AccuseS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(AccuseS2C);
		}
		if (msgCase_ == MsgOneofCase.SingleCampaignC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(SingleCampaignC2S);
		}
		if (msgCase_ == MsgOneofCase.SingleCampaignS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(SingleCampaignS2C);
		}
		if (msgCase_ == MsgOneofCase.DevChargeC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(DevChargeC2S);
		}
		if (msgCase_ == MsgOneofCase.DevChargeS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(DevChargeS2C);
		}
		if (msgCase_ == MsgOneofCase.AskReviveTeammateC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(AskReviveTeammateC2S);
		}
		if (msgCase_ == MsgOneofCase.AskReviveTeammateS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(AskReviveTeammateS2C);
		}
		if (msgCase_ == MsgOneofCase.GetSignInRewardC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(GetSignInRewardC2S);
		}
		if (msgCase_ == MsgOneofCase.GetSignInRewardS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(GetSignInRewardS2C);
		}
		if (msgCase_ == MsgOneofCase.ChatMapMarkersC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(ChatMapMarkersC2S);
		}
		if (msgCase_ == MsgOneofCase.ChatMapMarkersS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(ChatMapMarkersS2C);
		}
		if (msgCase_ == MsgOneofCase.AbroadCreateOrderC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(AbroadCreateOrderC2S);
		}
		if (msgCase_ == MsgOneofCase.AbroadCreateOrderS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(AbroadCreateOrderS2C);
		}
		if (msgCase_ == MsgOneofCase.AgeVerifyC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(AgeVerifyC2S);
		}
		if (msgCase_ == MsgOneofCase.AgeVerifyS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(AgeVerifyS2C);
		}
		if (msgCase_ == MsgOneofCase.ChangeNameC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(ChangeNameC2S);
		}
		if (msgCase_ == MsgOneofCase.ChangeNameS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(ChangeNameS2C);
		}
		if (msgCase_ == MsgOneofCase.ClientClickConfirmTaskC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(ClientClickConfirmTaskC2S);
		}
		if (msgCase_ == MsgOneofCase.ClientClickConfirmTaskS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(ClientClickConfirmTaskS2C);
		}
		if (msgCase_ == MsgOneofCase.BuyRelicC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(BuyRelicC2S);
		}
		if (msgCase_ == MsgOneofCase.BuyRelicS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(BuyRelicS2C);
		}
		if (msgCase_ == MsgOneofCase.BuyLightGiftC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(BuyLightGiftC2S);
		}
		if (msgCase_ == MsgOneofCase.BuyLightGiftS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(BuyLightGiftS2C);
		}
		if (msgCase_ == MsgOneofCase.LightGiftC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(LightGiftC2S);
		}
		if (msgCase_ == MsgOneofCase.LightGiftS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(LightGiftS2C);
		}
		if (msgCase_ == MsgOneofCase.AcquisitionC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(AcquisitionC2S);
		}
		if (msgCase_ == MsgOneofCase.AcquisitionS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(AcquisitionS2C);
		}
		if (msgCase_ == MsgOneofCase.AcquisitionRewardC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(AcquisitionRewardC2S);
		}
		if (msgCase_ == MsgOneofCase.AcquisitionRewardS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(AcquisitionRewardS2C);
		}
		if (msgCase_ == MsgOneofCase.SelectMechanismC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(SelectMechanismC2S);
		}
		if (msgCase_ == MsgOneofCase.SelectMechanismS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(SelectMechanismS2C);
		}
		if (msgCase_ == MsgOneofCase.GachaCountRewardC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(GachaCountRewardC2S);
		}
		if (msgCase_ == MsgOneofCase.GachaCountRewardS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(GachaCountRewardS2C);
		}
		if (msgCase_ == MsgOneofCase.GetPlayerSimpleC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(GetPlayerSimpleC2S);
		}
		if (msgCase_ == MsgOneofCase.GetPlayerSimpleS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(GetPlayerSimpleS2C);
		}
		if (msgCase_ == MsgOneofCase.RoleCardCollectC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(RoleCardCollectC2S);
		}
		if (msgCase_ == MsgOneofCase.RoleCardCollectS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(RoleCardCollectS2C);
		}
		if (msgCase_ == MsgOneofCase.GMPlayerSettingC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(GMPlayerSettingC2S);
		}
		if (msgCase_ == MsgOneofCase.GMPlayerSettingS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(GMPlayerSettingS2C);
		}
		if (msgCase_ == MsgOneofCase.SetFriendNoteC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(SetFriendNoteC2S);
		}
		if (msgCase_ == MsgOneofCase.SetFriendNoteS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(SetFriendNoteS2C);
		}
		if (msgCase_ == MsgOneofCase.SetOnlineStatusC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(SetOnlineStatusC2S);
		}
		if (msgCase_ == MsgOneofCase.SetOnlineStatusS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(SetOnlineStatusS2C);
		}
		if (msgCase_ == MsgOneofCase.ChooseSkinC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(ChooseSkinC2S);
		}
		if (msgCase_ == MsgOneofCase.ChooseSkinS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(ChooseSkinS2C);
		}
		if (msgCase_ == MsgOneofCase.TimeWastingC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(TimeWastingC2S);
		}
		if (msgCase_ == MsgOneofCase.TimeWastingS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(TimeWastingS2C);
		}
		if (msgCase_ == MsgOneofCase.VoteC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(VoteC2S);
		}
		if (msgCase_ == MsgOneofCase.VoteS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(VoteS2C);
		}
		if (msgCase_ == MsgOneofCase.VoteSelectC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(VoteSelectC2S);
		}
		if (msgCase_ == MsgOneofCase.VoteSelectS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(VoteSelectS2C);
		}
		if (msgCase_ == MsgOneofCase.NotifyStoryC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(NotifyStoryC2S);
		}
		if (msgCase_ == MsgOneofCase.NotifyStoryS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(NotifyStoryS2C);
		}
		if (msgCase_ == MsgOneofCase.PveHeroTalentUpC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(PveHeroTalentUpC2S);
		}
		if (msgCase_ == MsgOneofCase.PveHeroTalentUpS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(PveHeroTalentUpS2C);
		}
		if (msgCase_ == MsgOneofCase.SelectEventC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(SelectEventC2S);
		}
		if (msgCase_ == MsgOneofCase.SelectEventS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(SelectEventS2C);
		}
		if (msgCase_ == MsgOneofCase.CampScoreC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(CampScoreC2S);
		}
		if (msgCase_ == MsgOneofCase.CampScoreS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(CampScoreS2C);
		}
		if (msgCase_ == MsgOneofCase.ActivityMissionRewardC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(ActivityMissionRewardC2S);
		}
		if (msgCase_ == MsgOneofCase.ActivityMissionRewardS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(ActivityMissionRewardS2C);
		}
		if (msgCase_ == MsgOneofCase.VendorBuyCardC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(VendorBuyCardC2S);
		}
		if (msgCase_ == MsgOneofCase.VendorBuyCardS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(VendorBuyCardS2C);
		}
		if (msgCase_ == MsgOneofCase.TransferStarDiscC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(TransferStarDiscC2S);
		}
		if (msgCase_ == MsgOneofCase.TransferStarDiscS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(TransferStarDiscS2C);
		}
		if (msgCase_ == MsgOneofCase.GetHeroInfoC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(GetHeroInfoC2S);
		}
		if (msgCase_ == MsgOneofCase.GetHeroInfoS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(GetHeroInfoS2C);
		}
		if (msgCase_ == MsgOneofCase.CreateMatchTeamC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(CreateMatchTeamC2S);
		}
		if (msgCase_ == MsgOneofCase.CreateMatchTeamS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(CreateMatchTeamS2C);
		}
		if (msgCase_ == MsgOneofCase.ChangeMatchTeamC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(ChangeMatchTeamC2S);
		}
		if (msgCase_ == MsgOneofCase.ChangeMatchTeamS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(ChangeMatchTeamS2C);
		}
		if (msgCase_ == MsgOneofCase.JoinMatchTeamC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(JoinMatchTeamC2S);
		}
		if (msgCase_ == MsgOneofCase.JoinMatchTeamS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(JoinMatchTeamS2C);
		}
		if (msgCase_ == MsgOneofCase.ExitMatchTeamC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(ExitMatchTeamC2S);
		}
		if (msgCase_ == MsgOneofCase.ExitMatchTeamS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(ExitMatchTeamS2C);
		}
		if (msgCase_ == MsgOneofCase.RefreshMatchTeamInfoC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(RefreshMatchTeamInfoC2S);
		}
		if (msgCase_ == MsgOneofCase.RefreshMatchTeamInfoS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(RefreshMatchTeamInfoS2C);
		}
		if (msgCase_ == MsgOneofCase.ChinaCreateOrderC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(ChinaCreateOrderC2S);
		}
		if (msgCase_ == MsgOneofCase.ChinaCreateOrderS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(ChinaCreateOrderS2C);
		}
		if (msgCase_ == MsgOneofCase.MatchTeamInviteC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(MatchTeamInviteC2S);
		}
		if (msgCase_ == MsgOneofCase.MatchTeamInviteS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(MatchTeamInviteS2C);
		}
		if (msgCase_ == MsgOneofCase.MatchTeamChatC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(MatchTeamChatC2S);
		}
		if (msgCase_ == MsgOneofCase.MatchTeamChatS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(MatchTeamChatS2C);
		}
		if (msgCase_ == MsgOneofCase.MatchTeamReadyC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(MatchTeamReadyC2S);
		}
		if (msgCase_ == MsgOneofCase.MatchTeamReadyS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(MatchTeamReadyS2C);
		}
		if (msgCase_ == MsgOneofCase.PlayerChatC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(PlayerChatC2S);
		}
		if (msgCase_ == MsgOneofCase.PlayerChatS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(PlayerChatS2C);
		}
		if (msgCase_ == MsgOneofCase.SyncSingleGameDataC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(SyncSingleGameDataC2S);
		}
		if (msgCase_ == MsgOneofCase.SyncSingleGameDataS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(SyncSingleGameDataS2C);
		}
		if (msgCase_ == MsgOneofCase.SingleGameDataC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(SingleGameDataC2S);
		}
		if (msgCase_ == MsgOneofCase.SingleGameDataS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(SingleGameDataS2C);
		}
		if (msgCase_ == MsgOneofCase.GetActivityPassRewardC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(GetActivityPassRewardC2S);
		}
		if (msgCase_ == MsgOneofCase.GetActivityPassRewardS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(GetActivityPassRewardS2C);
		}
		if (msgCase_ == MsgOneofCase.ApplyChangeSlotC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(ApplyChangeSlotC2S);
		}
		if (msgCase_ == MsgOneofCase.ApplyChangeSlotS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(ApplyChangeSlotS2C);
		}
		if (msgCase_ == MsgOneofCase.OpsChangeSlotC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(OpsChangeSlotC2S);
		}
		if (msgCase_ == MsgOneofCase.OpsChangeSlotS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(OpsChangeSlotS2C);
		}
		if (msgCase_ == MsgOneofCase.RookieGachaRewardC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(RookieGachaRewardC2S);
		}
		if (msgCase_ == MsgOneofCase.RookieGachaRewardS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(RookieGachaRewardS2C);
		}
		if (msgCase_ == MsgOneofCase.LiveGiftPackageC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(LiveGiftPackageC2S);
		}
		if (msgCase_ == MsgOneofCase.LiveGiftPackageS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(LiveGiftPackageS2C);
		}
		if (msgCase_ == MsgOneofCase.LaborActDiceC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(LaborActDiceC2S);
		}
		if (msgCase_ == MsgOneofCase.LaborActDiceS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(LaborActDiceS2C);
		}
		if (msgCase_ == MsgOneofCase.ActionOverTimeLogC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(ActionOverTimeLogC2S);
		}
		if (msgCase_ == MsgOneofCase.ActionOverTimeLogS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(ActionOverTimeLogS2C);
		}
		if (msgCase_ == MsgOneofCase.ClientHarmonyC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(ClientHarmonyC2S);
		}
		if (msgCase_ == MsgOneofCase.ClientHarmonyS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(ClientHarmonyS2C);
		}
		if (msgCase_ == MsgOneofCase.MailStarC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(MailStarC2S);
		}
		if (msgCase_ == MsgOneofCase.MailStarS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(MailStarS2C);
		}
		if (msgCase_ == MsgOneofCase.SetCardAltArtC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(SetCardAltArtC2S);
		}
		if (msgCase_ == MsgOneofCase.SetCardAltArtS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(SetCardAltArtS2C);
		}
		if (msgCase_ == MsgOneofCase.SelectRewardCardC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(SelectRewardCardC2S);
		}
		if (msgCase_ == MsgOneofCase.SelectRewardCardS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(SelectRewardCardS2C);
		}
		if (msgCase_ == MsgOneofCase.GetQuestionUrlC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(GetQuestionUrlC2S);
		}
		if (msgCase_ == MsgOneofCase.GetQuestionUrlS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(GetQuestionUrlS2C);
		}
		if (msgCase_ == MsgOneofCase.GetReturnInfoC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(GetReturnInfoC2S);
		}
		if (msgCase_ == MsgOneofCase.GetReturnInfoS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(GetReturnInfoS2C);
		}
		if (msgCase_ == MsgOneofCase.ReturnGiftClaimC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(ReturnGiftClaimC2S);
		}
		if (msgCase_ == MsgOneofCase.ReturnGiftClaimS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(ReturnGiftClaimS2C);
		}
		if (msgCase_ == MsgOneofCase.ReturnSignInClaimC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(ReturnSignInClaimC2S);
		}
		if (msgCase_ == MsgOneofCase.ReturnSignInClaimS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(ReturnSignInClaimS2C);
		}
		if (msgCase_ == MsgOneofCase.ReturnSurveyFinishC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(ReturnSurveyFinishC2S);
		}
		if (msgCase_ == MsgOneofCase.ReturnSurveyFinishS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(ReturnSurveyFinishS2C);
		}
		if (msgCase_ == MsgOneofCase.FlipCardC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(FlipCardC2S);
		}
		if (msgCase_ == MsgOneofCase.FlipCardS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(FlipCardS2C);
		}
		if (msgCase_ == MsgOneofCase.FlipCardProgressRewardC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(FlipCardProgressRewardC2S);
		}
		if (msgCase_ == MsgOneofCase.FlipCardProgressRewardS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(FlipCardProgressRewardS2C);
		}
		if (msgCase_ == MsgOneofCase.SyncPlayerGuildS2C)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(SyncPlayerGuildS2C);
		}
		if (msgCase_ == MsgOneofCase.SyncPlayerJoinGuildS2C)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(SyncPlayerJoinGuildS2C);
		}
		if (msgCase_ == MsgOneofCase.GuildChatMsgS2C)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(GuildChatMsgS2C);
		}
		if (msgCase_ == MsgOneofCase.SyncGuildS2C)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(SyncGuildS2C);
		}
		if (msgCase_ == MsgOneofCase.SyncGuildMemberS2C)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(SyncGuildMemberS2C);
		}
		if (msgCase_ == MsgOneofCase.SyncGuildMemberExitS2C)
		{
			num += 2 + CodedOutputStream.ComputeMessageSize(SyncGuildMemberExitS2C);
		}
		if (msgCase_ == MsgOneofCase.CreateGuildC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(CreateGuildC2S);
		}
		if (msgCase_ == MsgOneofCase.CreateGuildS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(CreateGuildS2C);
		}
		if (msgCase_ == MsgOneofCase.SearchGuildC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(SearchGuildC2S);
		}
		if (msgCase_ == MsgOneofCase.SearchGuildS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(SearchGuildS2C);
		}
		if (msgCase_ == MsgOneofCase.ApplyToGuildC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(ApplyToGuildC2S);
		}
		if (msgCase_ == MsgOneofCase.ApplyToGuildS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(ApplyToGuildS2C);
		}
		if (msgCase_ == MsgOneofCase.ProcessGuildApplicationC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(ProcessGuildApplicationC2S);
		}
		if (msgCase_ == MsgOneofCase.ProcessGuildApplicationS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(ProcessGuildApplicationS2C);
		}
		if (msgCase_ == MsgOneofCase.SendGuildInvitationC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(SendGuildInvitationC2S);
		}
		if (msgCase_ == MsgOneofCase.SendGuildInvitationS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(SendGuildInvitationS2C);
		}
		if (msgCase_ == MsgOneofCase.ProcessGuildInvitationC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(ProcessGuildInvitationC2S);
		}
		if (msgCase_ == MsgOneofCase.ProcessGuildInvitationS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(ProcessGuildInvitationS2C);
		}
		if (msgCase_ == MsgOneofCase.GetGuildInfoC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(GetGuildInfoC2S);
		}
		if (msgCase_ == MsgOneofCase.GetGuildInfoS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(GetGuildInfoS2C);
		}
		if (msgCase_ == MsgOneofCase.UpdateGuildSettingsC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(UpdateGuildSettingsC2S);
		}
		if (msgCase_ == MsgOneofCase.UpdateGuildSettingsS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(UpdateGuildSettingsS2C);
		}
		if (msgCase_ == MsgOneofCase.UpdateGuildInAnnouncementC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(UpdateGuildInAnnouncementC2S);
		}
		if (msgCase_ == MsgOneofCase.UpdateGuildInAnnouncementS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(UpdateGuildInAnnouncementS2C);
		}
		if (msgCase_ == MsgOneofCase.TransferGuildMasterC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(TransferGuildMasterC2S);
		}
		if (msgCase_ == MsgOneofCase.TransferGuildMasterS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(TransferGuildMasterS2C);
		}
		if (msgCase_ == MsgOneofCase.ChangeGuildMemberTitleC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(ChangeGuildMemberTitleC2S);
		}
		if (msgCase_ == MsgOneofCase.ChangeGuildMemberTitleS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(ChangeGuildMemberTitleS2C);
		}
		if (msgCase_ == MsgOneofCase.KickGuildMemberC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(KickGuildMemberC2S);
		}
		if (msgCase_ == MsgOneofCase.KickGuildMemberS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(KickGuildMemberS2C);
		}
		if (msgCase_ == MsgOneofCase.ImpeachGuildMasterC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(ImpeachGuildMasterC2S);
		}
		if (msgCase_ == MsgOneofCase.ImpeachGuildMasterS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(ImpeachGuildMasterS2C);
		}
		if (msgCase_ == MsgOneofCase.ExitGuildC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(ExitGuildC2S);
		}
		if (msgCase_ == MsgOneofCase.ExitGuildS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(ExitGuildS2C);
		}
		if (msgCase_ == MsgOneofCase.DisbandGuildC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(DisbandGuildC2S);
		}
		if (msgCase_ == MsgOneofCase.DisbandGuildS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(DisbandGuildS2C);
		}
		if (msgCase_ == MsgOneofCase.GuildMissionRewardC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(GuildMissionRewardC2S);
		}
		if (msgCase_ == MsgOneofCase.GuildMissionRewardS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(GuildMissionRewardS2C);
		}
		if (msgCase_ == MsgOneofCase.GetGuildMemberChangeMsgC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(GetGuildMemberChangeMsgC2S);
		}
		if (msgCase_ == MsgOneofCase.GetGuildMemberChangeMsgS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(GetGuildMemberChangeMsgS2C);
		}
		if (msgCase_ == MsgOneofCase.SendGuildChatMsgC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(SendGuildChatMsgC2S);
		}
		if (msgCase_ == MsgOneofCase.SendGuildChatMsgS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(SendGuildChatMsgS2C);
		}
		if (msgCase_ == MsgOneofCase.GetGuildChatMsgC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(GetGuildChatMsgC2S);
		}
		if (msgCase_ == MsgOneofCase.GetGuildChatMsgS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(GetGuildChatMsgS2C);
		}
		if (msgCase_ == MsgOneofCase.GuildMemberC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(GuildMemberC2S);
		}
		if (msgCase_ == MsgOneofCase.GuildMemberS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(GuildMemberS2C);
		}
		if (msgCase_ == MsgOneofCase.GetGuildsInfoC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(GetGuildsInfoC2S);
		}
		if (msgCase_ == MsgOneofCase.GetGuildsInfoS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(GetGuildsInfoS2C);
		}
		if (msgCase_ == MsgOneofCase.TestRpcEchoC2S)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(TestRpcEchoC2S);
		}
		if (msgCase_ == MsgOneofCase.TestRpcEchoS2C)
		{
			num += 3 + CodedOutputStream.ComputeMessageSize(TestRpcEchoS2C);
		}
		if (_unknownFields != null)
		{
			num += _unknownFields.CalculateSize();
		}
		return num;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(protocol other)
	{
		if (other == null)
		{
			return;
		}
		switch (other.MsgCase)
		{
		case MsgOneofCase.SysSendMailC2S:
			if (SysSendMailC2S == null)
			{
				SysSendMailC2S = new SysSendMailC2S();
			}
			SysSendMailC2S.MergeFrom(other.SysSendMailC2S);
			break;
		case MsgOneofCase.SysPlayerOnlineC2S:
			if (SysPlayerOnlineC2S == null)
			{
				SysPlayerOnlineC2S = new SysPlayerOnlineC2S();
			}
			SysPlayerOnlineC2S.MergeFrom(other.SysPlayerOnlineC2S);
			break;
		case MsgOneofCase.PlayerOnlineRoomC2S:
			if (PlayerOnlineRoomC2S == null)
			{
				PlayerOnlineRoomC2S = new PlayerOnlineRoomC2S();
			}
			PlayerOnlineRoomC2S.MergeFrom(other.PlayerOnlineRoomC2S);
			break;
		case MsgOneofCase.SysCanPraiseInfoC2S:
			if (SysCanPraiseInfoC2S == null)
			{
				SysCanPraiseInfoC2S = new SysCanPraiseInfoC2S();
			}
			SysCanPraiseInfoC2S.MergeFrom(other.SysCanPraiseInfoC2S);
			break;
		case MsgOneofCase.SysPraiseC2S:
			if (SysPraiseC2S == null)
			{
				SysPraiseC2S = new SysPraiseC2S();
			}
			SysPraiseC2S.MergeFrom(other.SysPraiseC2S);
			break;
		case MsgOneofCase.SysRoomFinishC2S:
			if (SysRoomFinishC2S == null)
			{
				SysRoomFinishC2S = new SysRoomFinishC2S();
			}
			SysRoomFinishC2S.MergeFrom(other.SysRoomFinishC2S);
			break;
		case MsgOneofCase.SysRoomAddExpC2S:
			if (SysRoomAddExpC2S == null)
			{
				SysRoomAddExpC2S = new SysRoomAddExpC2S();
			}
			SysRoomAddExpC2S.MergeFrom(other.SysRoomAddExpC2S);
			break;
		case MsgOneofCase.SysGetShowFriendC2S:
			if (SysGetShowFriendC2S == null)
			{
				SysGetShowFriendC2S = new SysGetShowFriendC2S();
			}
			SysGetShowFriendC2S.MergeFrom(other.SysGetShowFriendC2S);
			break;
		case MsgOneofCase.SysFriendInviteC2S:
			if (SysFriendInviteC2S == null)
			{
				SysFriendInviteC2S = new SysFriendInviteC2S();
			}
			SysFriendInviteC2S.MergeFrom(other.SysFriendInviteC2S);
			break;
		case MsgOneofCase.SysFriendDelC2S:
			if (SysFriendDelC2S == null)
			{
				SysFriendDelC2S = new SysFriendDelC2S();
			}
			SysFriendDelC2S.MergeFrom(other.SysFriendDelC2S);
			break;
		case MsgOneofCase.SysFriendInfoC2S:
			if (SysFriendInfoC2S == null)
			{
				SysFriendInfoC2S = new SysFriendInfoC2S();
			}
			SysFriendInfoC2S.MergeFrom(other.SysFriendInfoC2S);
			break;
		case MsgOneofCase.SysFriendApplyC2S:
			if (SysFriendApplyC2S == null)
			{
				SysFriendApplyC2S = new SysFriendApplyC2S();
			}
			SysFriendApplyC2S.MergeFrom(other.SysFriendApplyC2S);
			break;
		case MsgOneofCase.SysFriendAddC2S:
			if (SysFriendAddC2S == null)
			{
				SysFriendAddC2S = new SysFriendAddC2S();
			}
			SysFriendAddC2S.MergeFrom(other.SysFriendAddC2S);
			break;
		case MsgOneofCase.SysFriendSendMsgC2S:
			if (SysFriendSendMsgC2S == null)
			{
				SysFriendSendMsgC2S = new SysFriendSendMsgC2S();
			}
			SysFriendSendMsgC2S.MergeFrom(other.SysFriendSendMsgC2S);
			break;
		case MsgOneofCase.SysCampaignFinishC2S:
			if (SysCampaignFinishC2S == null)
			{
				SysCampaignFinishC2S = new SysCampaignFinishC2S();
			}
			SysCampaignFinishC2S.MergeFrom(other.SysCampaignFinishC2S);
			break;
		case MsgOneofCase.SysAbroadPayMsgC2S:
			if (SysAbroadPayMsgC2S == null)
			{
				SysAbroadPayMsgC2S = new SysAbroadPayMsgC2S();
			}
			SysAbroadPayMsgC2S.MergeFrom(other.SysAbroadPayMsgC2S);
			break;
		case MsgOneofCase.AcquisitionMsgC2S:
			if (AcquisitionMsgC2S == null)
			{
				AcquisitionMsgC2S = new AcquisitionMsgC2S();
			}
			AcquisitionMsgC2S.MergeFrom(other.AcquisitionMsgC2S);
			break;
		case MsgOneofCase.SysFightRecord:
			if (SysFightRecord == null)
			{
				SysFightRecord = new SysFightRecord();
			}
			SysFightRecord.MergeFrom(other.SysFightRecord);
			break;
		case MsgOneofCase.SysCampaignAwardC2S:
			if (SysCampaignAwardC2S == null)
			{
				SysCampaignAwardC2S = new SysCampaignAwardC2S();
			}
			SysCampaignAwardC2S.MergeFrom(other.SysCampaignAwardC2S);
			break;
		case MsgOneofCase.SysRecoupItemC2S:
			if (SysRecoupItemC2S == null)
			{
				SysRecoupItemC2S = new SysRecoupItemC2S();
			}
			SysRecoupItemC2S.MergeFrom(other.SysRecoupItemC2S);
			break;
		case MsgOneofCase.SysPlayerOnlineRoomC2S:
			if (SysPlayerOnlineRoomC2S == null)
			{
				SysPlayerOnlineRoomC2S = new SysPlayerOnlineRoomC2S();
			}
			SysPlayerOnlineRoomC2S.MergeFrom(other.SysPlayerOnlineRoomC2S);
			break;
		case MsgOneofCase.SysPlayerCleanC2S:
			if (SysPlayerCleanC2S == null)
			{
				SysPlayerCleanC2S = new SysPlayerCleanC2S();
			}
			SysPlayerCleanC2S.MergeFrom(other.SysPlayerCleanC2S);
			break;
		case MsgOneofCase.SysPlayerPunishmentTimeC2S:
			if (SysPlayerPunishmentTimeC2S == null)
			{
				SysPlayerPunishmentTimeC2S = new SysPlayerPunishmentTimeC2S();
			}
			SysPlayerPunishmentTimeC2S.MergeFrom(other.SysPlayerPunishmentTimeC2S);
			break;
		case MsgOneofCase.SysMatchSuccess:
			if (SysMatchSuccess == null)
			{
				SysMatchSuccess = new SysMatchSuccess();
			}
			SysMatchSuccess.MergeFrom(other.SysMatchSuccess);
			break;
		case MsgOneofCase.SysChinaPayMsgC2S:
			if (SysChinaPayMsgC2S == null)
			{
				SysChinaPayMsgC2S = new SysChinaPayMsgC2S();
			}
			SysChinaPayMsgC2S.MergeFrom(other.SysChinaPayMsgC2S);
			break;
		case MsgOneofCase.SysChangeMatchTeamState:
			if (SysChangeMatchTeamState == null)
			{
				SysChangeMatchTeamState = new SysChangeMatchTeamState();
			}
			SysChangeMatchTeamState.MergeFrom(other.SysChangeMatchTeamState);
			break;
		case MsgOneofCase.SysSaveSimplePlayerInfoC2S:
			if (SysSaveSimplePlayerInfoC2S == null)
			{
				SysSaveSimplePlayerInfoC2S = new SysSaveSimplePlayerInfoC2S();
			}
			SysSaveSimplePlayerInfoC2S.MergeFrom(other.SysSaveSimplePlayerInfoC2S);
			break;
		case MsgOneofCase.SysGmChangeNameC2S:
			if (SysGmChangeNameC2S == null)
			{
				SysGmChangeNameC2S = new SysGmChangeNameC2S();
			}
			SysGmChangeNameC2S.MergeFrom(other.SysGmChangeNameC2S);
			break;
		case MsgOneofCase.SysSyncPlayerC2S:
			if (SysSyncPlayerC2S == null)
			{
				SysSyncPlayerC2S = new SysSyncPlayerC2S();
			}
			SysSyncPlayerC2S.MergeFrom(other.SysSyncPlayerC2S);
			break;
		case MsgOneofCase.SysPlayerCreditScoreChangeC2S:
			if (SysPlayerCreditScoreChangeC2S == null)
			{
				SysPlayerCreditScoreChangeC2S = new SysPlayerCreditScoreChangeC2S();
			}
			SysPlayerCreditScoreChangeC2S.MergeFrom(other.SysPlayerCreditScoreChangeC2S);
			break;
		case MsgOneofCase.SysSyncPlayerMatchPunishmentTimeC2S:
			if (SysSyncPlayerMatchPunishmentTimeC2S == null)
			{
				SysSyncPlayerMatchPunishmentTimeC2S = new SysSyncPlayerMatchPunishmentTimeC2S();
			}
			SysSyncPlayerMatchPunishmentTimeC2S.MergeFrom(other.SysSyncPlayerMatchPunishmentTimeC2S);
			break;
		case MsgOneofCase.GMChangeCreditScoreC2S:
			if (GMChangeCreditScoreC2S == null)
			{
				GMChangeCreditScoreC2S = new GMChangeCreditScoreC2S();
			}
			GMChangeCreditScoreC2S.MergeFrom(other.GMChangeCreditScoreC2S);
			break;
		case MsgOneofCase.SysPushReturnInfoC2S:
			if (SysPushReturnInfoC2S == null)
			{
				SysPushReturnInfoC2S = new SysPushReturnInfoC2S();
			}
			SysPushReturnInfoC2S.MergeFrom(other.SysPushReturnInfoC2S);
			break;
		case MsgOneofCase.SysMutePlayerC2S:
			if (SysMutePlayerC2S == null)
			{
				SysMutePlayerC2S = new SysMutePlayerC2S();
			}
			SysMutePlayerC2S.MergeFrom(other.SysMutePlayerC2S);
			break;
		case MsgOneofCase.SysQuestionC2S:
			if (SysQuestionC2S == null)
			{
				SysQuestionC2S = new SysQuestionC2S();
			}
			SysQuestionC2S.MergeFrom(other.SysQuestionC2S);
			break;
		case MsgOneofCase.KickS2C:
			if (KickS2C == null)
			{
				KickS2C = new KickS2C();
			}
			KickS2C.MergeFrom(other.KickS2C);
			break;
		case MsgOneofCase.PredictActionS2C:
			if (PredictActionS2C == null)
			{
				PredictActionS2C = new PredictActionS2C();
			}
			PredictActionS2C.MergeFrom(other.PredictActionS2C);
			break;
		case MsgOneofCase.RunningGameS2C:
			if (RunningGameS2C == null)
			{
				RunningGameS2C = new RunningGameS2C();
			}
			RunningGameS2C.MergeFrom(other.RunningGameS2C);
			break;
		case MsgOneofCase.BattleS2C:
			if (BattleS2C == null)
			{
				BattleS2C = new BattleS2C();
			}
			BattleS2C.MergeFrom(other.BattleS2C);
			break;
		case MsgOneofCase.LotteryDrawS2C:
			if (LotteryDrawS2C == null)
			{
				LotteryDrawS2C = new LotteryDrawS2C();
			}
			LotteryDrawS2C.MergeFrom(other.LotteryDrawS2C);
			break;
		case MsgOneofCase.LandBuffsS2C:
			if (LandBuffsS2C == null)
			{
				LandBuffsS2C = new LandBuffsS2C();
			}
			LandBuffsS2C.MergeFrom(other.LandBuffsS2C);
			break;
		case MsgOneofCase.RoundStartS2C:
			if (RoundStartS2C == null)
			{
				RoundStartS2C = new RoundStartS2C();
			}
			RoundStartS2C.MergeFrom(other.RoundStartS2C);
			break;
		case MsgOneofCase.GameFinishS2C:
			if (GameFinishS2C == null)
			{
				GameFinishS2C = new GameFinishS2C();
			}
			GameFinishS2C.MergeFrom(other.GameFinishS2C);
			break;
		case MsgOneofCase.MonsterRefreshS2C:
			if (MonsterRefreshS2C == null)
			{
				MonsterRefreshS2C = new MonsterRefreshS2C();
			}
			MonsterRefreshS2C.MergeFrom(other.MonsterRefreshS2C);
			break;
		case MsgOneofCase.MovePointBuffS2C:
			if (MovePointBuffS2C == null)
			{
				MovePointBuffS2C = new MovePointBuffS2C();
			}
			MovePointBuffS2C.MergeFrom(other.MovePointBuffS2C);
			break;
		case MsgOneofCase.ChangeDirS2C:
			if (ChangeDirS2C == null)
			{
				ChangeDirS2C = new ChangeDirS2C();
			}
			ChangeDirS2C.MergeFrom(other.ChangeDirS2C);
			break;
		case MsgOneofCase.GambleChangeS2C:
			if (GambleChangeS2C == null)
			{
				GambleChangeS2C = new GambleChangeS2C();
			}
			GambleChangeS2C.MergeFrom(other.GambleChangeS2C);
			break;
		case MsgOneofCase.HeroBarBoxChangeS2C:
			if (HeroBarBoxChangeS2C == null)
			{
				HeroBarBoxChangeS2C = new HeroBarBoxChangeS2C();
			}
			HeroBarBoxChangeS2C.MergeFrom(other.HeroBarBoxChangeS2C);
			break;
		case MsgOneofCase.RoomNotifyS2C:
			if (RoomNotifyS2C == null)
			{
				RoomNotifyS2C = new RoomNotifyS2C();
			}
			RoomNotifyS2C.MergeFrom(other.RoomNotifyS2C);
			break;
		case MsgOneofCase.ActionStartNotifyS2C:
			if (ActionStartNotifyS2C == null)
			{
				ActionStartNotifyS2C = new ActionStartNotifyS2C();
			}
			ActionStartNotifyS2C.MergeFrom(other.ActionStartNotifyS2C);
			break;
		case MsgOneofCase.NoGambleNotifyS2C:
			if (NoGambleNotifyS2C == null)
			{
				NoGambleNotifyS2C = new NoGambleNotifyS2C();
			}
			NoGambleNotifyS2C.MergeFrom(other.NoGambleNotifyS2C);
			break;
		case MsgOneofCase.GambleObServeS2C:
			if (GambleObServeS2C == null)
			{
				GambleObServeS2C = new GambleObServeS2C();
			}
			GambleObServeS2C.MergeFrom(other.GambleObServeS2C);
			break;
		case MsgOneofCase.UpdateHeroAttrS2C:
			if (UpdateHeroAttrS2C == null)
			{
				UpdateHeroAttrS2C = new UpdateHeroAttrS2C();
			}
			UpdateHeroAttrS2C.MergeFrom(other.UpdateHeroAttrS2C);
			break;
		case MsgOneofCase.ChangePlayerSlotS2C:
			if (ChangePlayerSlotS2C == null)
			{
				ChangePlayerSlotS2C = new ChangePlayerSlotS2C();
			}
			ChangePlayerSlotS2C.MergeFrom(other.ChangePlayerSlotS2C);
			break;
		case MsgOneofCase.BossSleepS2C:
			if (BossSleepS2C == null)
			{
				BossSleepS2C = new BossSleepS2C();
			}
			BossSleepS2C.MergeFrom(other.BossSleepS2C);
			break;
		case MsgOneofCase.RefMallS2C:
			if (RefMallS2C == null)
			{
				RefMallS2C = new RefMallS2C();
			}
			RefMallS2C.MergeFrom(other.RefMallS2C);
			break;
		case MsgOneofCase.BagItemChangeS2C:
			if (BagItemChangeS2C == null)
			{
				BagItemChangeS2C = new BagItemChangeS2C();
			}
			BagItemChangeS2C.MergeFrom(other.BagItemChangeS2C);
			break;
		case MsgOneofCase.RoleCardChangeS2C:
			if (RoleCardChangeS2C == null)
			{
				RoleCardChangeS2C = new RoleCardChangeS2C();
			}
			RoleCardChangeS2C.MergeFrom(other.RoleCardChangeS2C);
			break;
		case MsgOneofCase.TaskConditionS2C:
			if (TaskConditionS2C == null)
			{
				TaskConditionS2C = new TaskConditionS2C();
			}
			TaskConditionS2C.MergeFrom(other.TaskConditionS2C);
			break;
		case MsgOneofCase.TaskInfoS2C:
			if (TaskInfoS2C == null)
			{
				TaskInfoS2C = new TaskInfoS2C();
			}
			TaskInfoS2C.MergeFrom(other.TaskInfoS2C);
			break;
		case MsgOneofCase.PlayerOnlineS2C:
			if (PlayerOnlineS2C == null)
			{
				PlayerOnlineS2C = new PlayerOnlineS2C();
			}
			PlayerOnlineS2C.MergeFrom(other.PlayerOnlineS2C);
			break;
		case MsgOneofCase.ChangeExpS2C:
			if (ChangeExpS2C == null)
			{
				ChangeExpS2C = new ChangeExpS2C();
			}
			ChangeExpS2C.MergeFrom(other.ChangeExpS2C);
			break;
		case MsgOneofCase.MailAddS2C:
			if (MailAddS2C == null)
			{
				MailAddS2C = new MailAddS2C();
			}
			MailAddS2C.MergeFrom(other.MailAddS2C);
			break;
		case MsgOneofCase.OnlineSyncRoomIdS2C:
			if (OnlineSyncRoomIdS2C == null)
			{
				OnlineSyncRoomIdS2C = new OnlineSyncRoomIdS2C();
			}
			OnlineSyncRoomIdS2C.MergeFrom(other.OnlineSyncRoomIdS2C);
			break;
		case MsgOneofCase.NoticeS2C:
			if (NoticeS2C == null)
			{
				NoticeS2C = new NoticeS2C();
			}
			NoticeS2C.MergeFrom(other.NoticeS2C);
			break;
		case MsgOneofCase.ActivityTaskConditionS2C:
			if (ActivityTaskConditionS2C == null)
			{
				ActivityTaskConditionS2C = new ActivityTaskConditionS2C();
			}
			ActivityTaskConditionS2C.MergeFrom(other.ActivityTaskConditionS2C);
			break;
		case MsgOneofCase.MapEventS2C:
			if (MapEventS2C == null)
			{
				MapEventS2C = new MapEventS2C();
			}
			MapEventS2C.MergeFrom(other.MapEventS2C);
			break;
		case MsgOneofCase.Day7RewardS2C:
			if (Day7RewardS2C == null)
			{
				Day7RewardS2C = new Day7RewardS2C();
			}
			Day7RewardS2C.MergeFrom(other.Day7RewardS2C);
			break;
		case MsgOneofCase.MapEventTrainS2C:
			if (MapEventTrainS2C == null)
			{
				MapEventTrainS2C = new MapEventTrainS2C();
			}
			MapEventTrainS2C.MergeFrom(other.MapEventTrainS2C);
			break;
		case MsgOneofCase.ChangePraiseNumS2C:
			if (ChangePraiseNumS2C == null)
			{
				ChangePraiseNumS2C = new ChangePraiseNumS2C();
			}
			ChangePraiseNumS2C.MergeFrom(other.ChangePraiseNumS2C);
			break;
		case MsgOneofCase.MonthlyCardS2C:
			if (MonthlyCardS2C == null)
			{
				MonthlyCardS2C = new MonthlyCardS2C();
			}
			MonthlyCardS2C.MergeFrom(other.MonthlyCardS2C);
			break;
		case MsgOneofCase.MailDelS2C:
			if (MailDelS2C == null)
			{
				MailDelS2C = new MailDelS2C();
			}
			MailDelS2C.MergeFrom(other.MailDelS2C);
			break;
		case MsgOneofCase.FriendNotifyS2C:
			if (FriendNotifyS2C == null)
			{
				FriendNotifyS2C = new FriendNotifyS2C();
			}
			FriendNotifyS2C.MergeFrom(other.FriendNotifyS2C);
			break;
		case MsgOneofCase.FriendListChangeS2C:
			if (FriendListChangeS2C == null)
			{
				FriendListChangeS2C = new FriendListChangeS2C();
			}
			FriendListChangeS2C.MergeFrom(other.FriendListChangeS2C);
			break;
		case MsgOneofCase.FriendInviteNotifyS2C:
			if (FriendInviteNotifyS2C == null)
			{
				FriendInviteNotifyS2C = new FriendInviteNotifyS2C();
			}
			FriendInviteNotifyS2C.MergeFrom(other.FriendInviteNotifyS2C);
			break;
		case MsgOneofCase.LoopNoticeS2C:
			if (LoopNoticeS2C == null)
			{
				LoopNoticeS2C = new LoopNoticeS2C();
			}
			LoopNoticeS2C.MergeFrom(other.LoopNoticeS2C);
			break;
		case MsgOneofCase.BattlePassLvS2C:
			if (BattlePassLvS2C == null)
			{
				BattlePassLvS2C = new BattlePassLvS2C();
			}
			BattlePassLvS2C.MergeFrom(other.BattlePassLvS2C);
			break;
		case MsgOneofCase.BattlePassTaskInfoS2C:
			if (BattlePassTaskInfoS2C == null)
			{
				BattlePassTaskInfoS2C = new BattlePassTaskInfoS2C();
			}
			BattlePassTaskInfoS2C.MergeFrom(other.BattlePassTaskInfoS2C);
			break;
		case MsgOneofCase.BattlePassUpdateTaskS2C:
			if (BattlePassUpdateTaskS2C == null)
			{
				BattlePassUpdateTaskS2C = new BattlePassUpdateTaskS2C();
			}
			BattlePassUpdateTaskS2C.MergeFrom(other.BattlePassUpdateTaskS2C);
			break;
		case MsgOneofCase.BattlePassBuyS2C:
			if (BattlePassBuyS2C == null)
			{
				BattlePassBuyS2C = new BattlePassBuyS2C();
			}
			BattlePassBuyS2C.MergeFrom(other.BattlePassBuyS2C);
			break;
		case MsgOneofCase.BattlePassInfoS2C:
			if (BattlePassInfoS2C == null)
			{
				BattlePassInfoS2C = new BattlePassInfoS2C();
			}
			BattlePassInfoS2C.MergeFrom(other.BattlePassInfoS2C);
			break;
		case MsgOneofCase.FriendsChatMsgS2C:
			if (FriendsChatMsgS2C == null)
			{
				FriendsChatMsgS2C = new FriendsChatMsgS2C();
			}
			FriendsChatMsgS2C.MergeFrom(other.FriendsChatMsgS2C);
			break;
		case MsgOneofCase.GameProgressChangeS2C:
			if (GameProgressChangeS2C == null)
			{
				GameProgressChangeS2C = new GameProgressChangeS2C();
			}
			GameProgressChangeS2C.MergeFrom(other.GameProgressChangeS2C);
			break;
		case MsgOneofCase.MapMissionNotifyS2C:
			if (MapMissionNotifyS2C == null)
			{
				MapMissionNotifyS2C = new MapMissionNotifyS2C();
			}
			MapMissionNotifyS2C.MergeFrom(other.MapMissionNotifyS2C);
			break;
		case MsgOneofCase.ChangeItemLimitS2C:
			if (ChangeItemLimitS2C == null)
			{
				ChangeItemLimitS2C = new ChangeItemLimitS2C();
			}
			ChangeItemLimitS2C.MergeFrom(other.ChangeItemLimitS2C);
			break;
		case MsgOneofCase.CleanItemLimitS2C:
			if (CleanItemLimitS2C == null)
			{
				CleanItemLimitS2C = new CleanItemLimitS2C();
			}
			CleanItemLimitS2C.MergeFrom(other.CleanItemLimitS2C);
			break;
		case MsgOneofCase.CampaignPassS2C:
			if (CampaignPassS2C == null)
			{
				CampaignPassS2C = new CampaignPassS2C();
			}
			CampaignPassS2C.MergeFrom(other.CampaignPassS2C);
			break;
		case MsgOneofCase.CampaignNotifyS2C:
			if (CampaignNotifyS2C == null)
			{
				CampaignNotifyS2C = new CampaignNotifyS2C();
			}
			CampaignNotifyS2C.MergeFrom(other.CampaignNotifyS2C);
			break;
		case MsgOneofCase.KillMessageS2C:
			if (KillMessageS2C == null)
			{
				KillMessageS2C = new KillMessageS2C();
			}
			KillMessageS2C.MergeFrom(other.KillMessageS2C);
			break;
		case MsgOneofCase.GameScoreChangeS2C:
			if (GameScoreChangeS2C == null)
			{
				GameScoreChangeS2C = new GameScoreChangeS2C();
			}
			GameScoreChangeS2C.MergeFrom(other.GameScoreChangeS2C);
			break;
		case MsgOneofCase.SignInRewardS2C:
			if (SignInRewardS2C == null)
			{
				SignInRewardS2C = new SignInRewardS2C();
			}
			SignInRewardS2C.MergeFrom(other.SignInRewardS2C);
			break;
		case MsgOneofCase.PayResultS2C:
			if (PayResultS2C == null)
			{
				PayResultS2C = new PayResultS2C();
			}
			PayResultS2C.MergeFrom(other.PayResultS2C);
			break;
		case MsgOneofCase.PayInfoChangeS2C:
			if (PayInfoChangeS2C == null)
			{
				PayInfoChangeS2C = new PayInfoChangeS2C();
			}
			PayInfoChangeS2C.MergeFrom(other.PayInfoChangeS2C);
			break;
		case MsgOneofCase.InviteSuccessS2C:
			if (InviteSuccessS2C == null)
			{
				InviteSuccessS2C = new InviteSuccessS2C();
			}
			InviteSuccessS2C.MergeFrom(other.InviteSuccessS2C);
			break;
		case MsgOneofCase.InviteInfoNotifyS2C:
			if (InviteInfoNotifyS2C == null)
			{
				InviteInfoNotifyS2C = new InviteInfoNotifyS2C();
			}
			InviteInfoNotifyS2C.MergeFrom(other.InviteInfoNotifyS2C);
			break;
		case MsgOneofCase.MapStatusChangeS2C:
			if (MapStatusChangeS2C == null)
			{
				MapStatusChangeS2C = new MapStatusChangeS2C();
			}
			MapStatusChangeS2C.MergeFrom(other.MapStatusChangeS2C);
			break;
		case MsgOneofCase.GachaCountS2C:
			if (GachaCountS2C == null)
			{
				GachaCountS2C = new GachaCountS2C();
			}
			GachaCountS2C.MergeFrom(other.GachaCountS2C);
			break;
		case MsgOneofCase.MapIndexChangeS2C:
			if (MapIndexChangeS2C == null)
			{
				MapIndexChangeS2C = new MapIndexChangeS2C();
			}
			MapIndexChangeS2C.MergeFrom(other.MapIndexChangeS2C);
			break;
		case MsgOneofCase.SurrenderPunishS2C:
			if (SurrenderPunishS2C == null)
			{
				SurrenderPunishS2C = new SurrenderPunishS2C();
			}
			SurrenderPunishS2C.MergeFrom(other.SurrenderPunishS2C);
			break;
		case MsgOneofCase.GamePassMapSuccessS2C:
			if (GamePassMapSuccessS2C == null)
			{
				GamePassMapSuccessS2C = new GamePassMapSuccessS2C();
			}
			GamePassMapSuccessS2C.MergeFrom(other.GamePassMapSuccessS2C);
			break;
		case MsgOneofCase.FriendDelNotifyS2C:
			if (FriendDelNotifyS2C == null)
			{
				FriendDelNotifyS2C = new FriendDelNotifyS2C();
			}
			FriendDelNotifyS2C.MergeFrom(other.FriendDelNotifyS2C);
			break;
		case MsgOneofCase.BagExpiredTransformNotify:
			if (BagExpiredTransformNotify == null)
			{
				BagExpiredTransformNotify = new BagExpiredTransformNotify();
			}
			BagExpiredTransformNotify.MergeFrom(other.BagExpiredTransformNotify);
			break;
		case MsgOneofCase.MapEventCrabS2C:
			if (MapEventCrabS2C == null)
			{
				MapEventCrabS2C = new MapEventCrabS2C();
			}
			MapEventCrabS2C.MergeFrom(other.MapEventCrabS2C);
			break;
		case MsgOneofCase.PkAfterVoteS2C:
			if (PkAfterVoteS2C == null)
			{
				PkAfterVoteS2C = new PkAfterVoteS2C();
			}
			PkAfterVoteS2C.MergeFrom(other.PkAfterVoteS2C);
			break;
		case MsgOneofCase.PlayerTaskNotifyS2C:
			if (PlayerTaskNotifyS2C == null)
			{
				PlayerTaskNotifyS2C = new PlayerTaskNotifyS2C();
			}
			PlayerTaskNotifyS2C.MergeFrom(other.PlayerTaskNotifyS2C);
			break;
		case MsgOneofCase.UnLockDifficultyS2C:
			if (UnLockDifficultyS2C == null)
			{
				UnLockDifficultyS2C = new UnLockDifficultyS2C();
			}
			UnLockDifficultyS2C.MergeFrom(other.UnLockDifficultyS2C);
			break;
		case MsgOneofCase.HeroSkillMoveEffectS2C:
			if (HeroSkillMoveEffectS2C == null)
			{
				HeroSkillMoveEffectS2C = new HeroSkillMoveEffectS2C();
			}
			HeroSkillMoveEffectS2C.MergeFrom(other.HeroSkillMoveEffectS2C);
			break;
		case MsgOneofCase.TimeOutKickPlayerS2C:
			if (TimeOutKickPlayerS2C == null)
			{
				TimeOutKickPlayerS2C = new TimeOutKickPlayerS2C();
			}
			TimeOutKickPlayerS2C.MergeFrom(other.TimeOutKickPlayerS2C);
			break;
		case MsgOneofCase.SayPhraseNotifyS2C:
			if (SayPhraseNotifyS2C == null)
			{
				SayPhraseNotifyS2C = new SayPhraseNotifyS2C();
			}
			SayPhraseNotifyS2C.MergeFrom(other.SayPhraseNotifyS2C);
			break;
		case MsgOneofCase.MatchTeamInviteNotify:
			if (MatchTeamInviteNotify == null)
			{
				MatchTeamInviteNotify = new MatchTeamInviteNotify();
			}
			MatchTeamInviteNotify.MergeFrom(other.MatchTeamInviteNotify);
			break;
		case MsgOneofCase.RefreshMatchTeamStateNotify:
			if (RefreshMatchTeamStateNotify == null)
			{
				RefreshMatchTeamStateNotify = new RefreshMatchTeamStateNotify();
			}
			RefreshMatchTeamStateNotify.MergeFrom(other.RefreshMatchTeamStateNotify);
			break;
		case MsgOneofCase.ActivityPassGearChangeS2C:
			if (ActivityPassGearChangeS2C == null)
			{
				ActivityPassGearChangeS2C = new ActivityPassGearChangeS2C();
			}
			ActivityPassGearChangeS2C.MergeFrom(other.ActivityPassGearChangeS2C);
			break;
		case MsgOneofCase.SingleGameScoreChange:
			if (SingleGameScoreChange == null)
			{
				SingleGameScoreChange = new SingleGameScoreChangeS2C();
			}
			SingleGameScoreChange.MergeFrom(other.SingleGameScoreChange);
			break;
		case MsgOneofCase.DelayProgressMapEventS2C:
			if (DelayProgressMapEventS2C == null)
			{
				DelayProgressMapEventS2C = new DelayProgressMapEventS2C();
			}
			DelayProgressMapEventS2C.MergeFrom(other.DelayProgressMapEventS2C);
			break;
		case MsgOneofCase.LuckyStarMissionChangeS2C:
			if (LuckyStarMissionChangeS2C == null)
			{
				LuckyStarMissionChangeS2C = new LuckyStarMissionChangeS2C();
			}
			LuckyStarMissionChangeS2C.MergeFrom(other.LuckyStarMissionChangeS2C);
			break;
		case MsgOneofCase.MatchPunishmentS2C:
			if (MatchPunishmentS2C == null)
			{
				MatchPunishmentS2C = new MatchPunishmentS2C();
			}
			MatchPunishmentS2C.MergeFrom(other.MatchPunishmentS2C);
			break;
		case MsgOneofCase.ChallengeDataChangeS2C:
			if (ChallengeDataChangeS2C == null)
			{
				ChallengeDataChangeS2C = new ChallengeDataChangeS2C();
			}
			ChallengeDataChangeS2C.MergeFrom(other.ChallengeDataChangeS2C);
			break;
		case MsgOneofCase.GmUnlockRoleInfoS2C:
			if (GmUnlockRoleInfoS2C == null)
			{
				GmUnlockRoleInfoS2C = new GmUnlockRoleInfoS2C();
			}
			GmUnlockRoleInfoS2C.MergeFrom(other.GmUnlockRoleInfoS2C);
			break;
		case MsgOneofCase.SyncPlayerCreditInfoS2C:
			if (SyncPlayerCreditInfoS2C == null)
			{
				SyncPlayerCreditInfoS2C = new SyncPlayerCreditInfoS2C();
			}
			SyncPlayerCreditInfoS2C.MergeFrom(other.SyncPlayerCreditInfoS2C);
			break;
		case MsgOneofCase.RoomHeroCardChangeS2C:
			if (RoomHeroCardChangeS2C == null)
			{
				RoomHeroCardChangeS2C = new RoomHeroCardChangeS2C();
			}
			RoomHeroCardChangeS2C.MergeFrom(other.RoomHeroCardChangeS2C);
			break;
		case MsgOneofCase.RoomRoundAddTermS2C:
			if (RoomRoundAddTermS2C == null)
			{
				RoomRoundAddTermS2C = new RoomRoundAddTermS2C();
			}
			RoomRoundAddTermS2C.MergeFrom(other.RoomRoundAddTermS2C);
			break;
		case MsgOneofCase.ReturnInfoS2C:
			if (ReturnInfoS2C == null)
			{
				ReturnInfoS2C = new ReturnInfoS2C();
			}
			ReturnInfoS2C.MergeFrom(other.ReturnInfoS2C);
			break;
		case MsgOneofCase.SyncRelicsS2C:
			if (SyncRelicsS2C == null)
			{
				SyncRelicsS2C = new SyncRelicsS2C();
			}
			SyncRelicsS2C.MergeFrom(other.SyncRelicsS2C);
			break;
		case MsgOneofCase.ReplaySnapshotS2C:
			if (ReplaySnapshotS2C == null)
			{
				ReplaySnapshotS2C = new ReplaySnapshotS2C();
			}
			ReplaySnapshotS2C.MergeFrom(other.ReplaySnapshotS2C);
			break;
		case MsgOneofCase.ClueNotifyS2C:
			if (ClueNotifyS2C == null)
			{
				ClueNotifyS2C = new ClueNotifyS2C();
			}
			ClueNotifyS2C.MergeFrom(other.ClueNotifyS2C);
			break;
		case MsgOneofCase.ReplayDieS2C:
			if (ReplayDieS2C == null)
			{
				ReplayDieS2C = new ReplayDieS2C();
			}
			ReplayDieS2C.MergeFrom(other.ReplayDieS2C);
			break;
		case MsgOneofCase.GuildTaskNotifyS2C:
			if (GuildTaskNotifyS2C == null)
			{
				GuildTaskNotifyS2C = new GuildTaskNotifyS2C();
			}
			GuildTaskNotifyS2C.MergeFrom(other.GuildTaskNotifyS2C);
			break;
		case MsgOneofCase.GameRoundChangeS2C:
			if (GameRoundChangeS2C == null)
			{
				GameRoundChangeS2C = new GameRoundChangeS2C();
			}
			GameRoundChangeS2C.MergeFrom(other.GameRoundChangeS2C);
			break;
		case MsgOneofCase.NotifyQuestionS2C:
			if (NotifyQuestionS2C == null)
			{
				NotifyQuestionS2C = new NotifyQuestionS2C();
			}
			NotifyQuestionS2C.MergeFrom(other.NotifyQuestionS2C);
			break;
		case MsgOneofCase.ConnectHandle:
			if (ConnectHandle == null)
			{
				ConnectHandle = new ConnectC2S();
			}
			ConnectHandle.MergeFrom(other.ConnectHandle);
			break;
		case MsgOneofCase.Connect:
			if (Connect == null)
			{
				Connect = new ConnectS2C();
			}
			Connect.MergeFrom(other.Connect);
			break;
		case MsgOneofCase.HeartbeatHandle:
			if (HeartbeatHandle == null)
			{
				HeartbeatHandle = new HeartbeatC2S();
			}
			HeartbeatHandle.MergeFrom(other.HeartbeatHandle);
			break;
		case MsgOneofCase.Heartbeat:
			if (Heartbeat == null)
			{
				Heartbeat = new HeartbeatS2C();
			}
			Heartbeat.MergeFrom(other.Heartbeat);
			break;
		case MsgOneofCase.CreateRoomHandle:
			if (CreateRoomHandle == null)
			{
				CreateRoomHandle = new CreateRoomC2S();
			}
			CreateRoomHandle.MergeFrom(other.CreateRoomHandle);
			break;
		case MsgOneofCase.CreateRoom:
			if (CreateRoom == null)
			{
				CreateRoom = new CreateRoomS2C();
			}
			CreateRoom.MergeFrom(other.CreateRoom);
			break;
		case MsgOneofCase.SyncRoomHandle:
			if (SyncRoomHandle == null)
			{
				SyncRoomHandle = new SyncRoomC2S();
			}
			SyncRoomHandle.MergeFrom(other.SyncRoomHandle);
			break;
		case MsgOneofCase.SyncRoom:
			if (SyncRoom == null)
			{
				SyncRoom = new SyncRoomS2C();
			}
			SyncRoom.MergeFrom(other.SyncRoom);
			break;
		case MsgOneofCase.JoinRoomHandle:
			if (JoinRoomHandle == null)
			{
				JoinRoomHandle = new JoinRoomC2S();
			}
			JoinRoomHandle.MergeFrom(other.JoinRoomHandle);
			break;
		case MsgOneofCase.JoinRoom:
			if (JoinRoom == null)
			{
				JoinRoom = new JoinRoomS2C();
			}
			JoinRoom.MergeFrom(other.JoinRoom);
			break;
		case MsgOneofCase.ExitRoomHandle:
			if (ExitRoomHandle == null)
			{
				ExitRoomHandle = new ExitRoomC2S();
			}
			ExitRoomHandle.MergeFrom(other.ExitRoomHandle);
			break;
		case MsgOneofCase.ExitRoom:
			if (ExitRoom == null)
			{
				ExitRoom = new ExitRoomS2C();
			}
			ExitRoom.MergeFrom(other.ExitRoom);
			break;
		case MsgOneofCase.QueryRoomHandle:
			if (QueryRoomHandle == null)
			{
				QueryRoomHandle = new QueryRoomC2S();
			}
			QueryRoomHandle.MergeFrom(other.QueryRoomHandle);
			break;
		case MsgOneofCase.QueryRoom:
			if (QueryRoom == null)
			{
				QueryRoom = new QueryRoomS2C();
			}
			QueryRoom.MergeFrom(other.QueryRoom);
			break;
		case MsgOneofCase.RefreshRoomStateHandle:
			if (RefreshRoomStateHandle == null)
			{
				RefreshRoomStateHandle = new RefreshRoomStateC2S();
			}
			RefreshRoomStateHandle.MergeFrom(other.RefreshRoomStateHandle);
			break;
		case MsgOneofCase.RefreshRoomState:
			if (RefreshRoomState == null)
			{
				RefreshRoomState = new RefreshRoomStateS2C();
			}
			RefreshRoomState.MergeFrom(other.RefreshRoomState);
			break;
		case MsgOneofCase.StartGameHandle:
			if (StartGameHandle == null)
			{
				StartGameHandle = new StartGameC2S();
			}
			StartGameHandle.MergeFrom(other.StartGameHandle);
			break;
		case MsgOneofCase.StartGame:
			if (StartGame == null)
			{
				StartGame = new StartGameS2C();
			}
			StartGame.MergeFrom(other.StartGame);
			break;
		case MsgOneofCase.ThrowDiceHandle:
			if (ThrowDiceHandle == null)
			{
				ThrowDiceHandle = new ThrowDiceC2S();
			}
			ThrowDiceHandle.MergeFrom(other.ThrowDiceHandle);
			break;
		case MsgOneofCase.ThrowDice:
			if (ThrowDice == null)
			{
				ThrowDice = new ThrowDiceS2C();
			}
			ThrowDice.MergeFrom(other.ThrowDice);
			break;
		case MsgOneofCase.ChangeRoomHandle:
			if (ChangeRoomHandle == null)
			{
				ChangeRoomHandle = new ChangeRoomC2S();
			}
			ChangeRoomHandle.MergeFrom(other.ChangeRoomHandle);
			break;
		case MsgOneofCase.ChangeRoom:
			if (ChangeRoom == null)
			{
				ChangeRoom = new ChangeRoomS2C();
			}
			ChangeRoom.MergeFrom(other.ChangeRoom);
			break;
		case MsgOneofCase.MoveHandle:
			if (MoveHandle == null)
			{
				MoveHandle = new MoveC2S();
			}
			MoveHandle.MergeFrom(other.MoveHandle);
			break;
		case MsgOneofCase.Move:
			if (Move == null)
			{
				Move = new MoveS2C();
			}
			Move.MergeFrom(other.Move);
			break;
		case MsgOneofCase.ShopBuyHandle:
			if (ShopBuyHandle == null)
			{
				ShopBuyHandle = new ShopBuyC2S();
			}
			ShopBuyHandle.MergeFrom(other.ShopBuyHandle);
			break;
		case MsgOneofCase.ShopBuy:
			if (ShopBuy == null)
			{
				ShopBuy = new ShopBuyS2C();
			}
			ShopBuy.MergeFrom(other.ShopBuy);
			break;
		case MsgOneofCase.PursuitHandle:
			if (PursuitHandle == null)
			{
				PursuitHandle = new PursuitC2S();
			}
			PursuitHandle.MergeFrom(other.PursuitHandle);
			break;
		case MsgOneofCase.Pursuit:
			if (Pursuit == null)
			{
				Pursuit = new PursuitS2C();
			}
			Pursuit.MergeFrom(other.Pursuit);
			break;
		case MsgOneofCase.BattleUseCardHandle:
			if (BattleUseCardHandle == null)
			{
				BattleUseCardHandle = new BattleUseCardC2S();
			}
			BattleUseCardHandle.MergeFrom(other.BattleUseCardHandle);
			break;
		case MsgOneofCase.BattleUseCard:
			if (BattleUseCard == null)
			{
				BattleUseCard = new BattleUseCardS2C();
			}
			BattleUseCard.MergeFrom(other.BattleUseCard);
			break;
		case MsgOneofCase.BattleThrowDiceHandle:
			if (BattleThrowDiceHandle == null)
			{
				BattleThrowDiceHandle = new BattleThrowDiceC2S();
			}
			BattleThrowDiceHandle.MergeFrom(other.BattleThrowDiceHandle);
			break;
		case MsgOneofCase.BattleThrowDice:
			if (BattleThrowDice == null)
			{
				BattleThrowDice = new BattleThrowDiceS2C();
			}
			BattleThrowDice.MergeFrom(other.BattleThrowDice);
			break;
		case MsgOneofCase.BattleChoiceHandle:
			if (BattleChoiceHandle == null)
			{
				BattleChoiceHandle = new BattleChoiceC2S();
			}
			BattleChoiceHandle.MergeFrom(other.BattleChoiceHandle);
			break;
		case MsgOneofCase.BattleChoice:
			if (BattleChoice == null)
			{
				BattleChoice = new BattleChoiceS2C();
			}
			BattleChoice.MergeFrom(other.BattleChoice);
			break;
		case MsgOneofCase.LotteryChoiceHandle:
			if (LotteryChoiceHandle == null)
			{
				LotteryChoiceHandle = new LotteryChoiceC2S();
			}
			LotteryChoiceHandle.MergeFrom(other.LotteryChoiceHandle);
			break;
		case MsgOneofCase.LotteryChoice:
			if (LotteryChoice == null)
			{
				LotteryChoice = new LotteryChoiceS2C();
			}
			LotteryChoice.MergeFrom(other.LotteryChoice);
			break;
		case MsgOneofCase.MoveAgainHandle:
			if (MoveAgainHandle == null)
			{
				MoveAgainHandle = new MoveAgainC2S();
			}
			MoveAgainHandle.MergeFrom(other.MoveAgainHandle);
			break;
		case MsgOneofCase.MoveAgain:
			if (MoveAgain == null)
			{
				MoveAgain = new MoveAgainS2C();
			}
			MoveAgain.MergeFrom(other.MoveAgain);
			break;
		case MsgOneofCase.AskBattleHandle:
			if (AskBattleHandle == null)
			{
				AskBattleHandle = new AskBattleC2S();
			}
			AskBattleHandle.MergeFrom(other.AskBattleHandle);
			break;
		case MsgOneofCase.AskBattle:
			if (AskBattle == null)
			{
				AskBattle = new AskBattleS2C();
			}
			AskBattle.MergeFrom(other.AskBattle);
			break;
		case MsgOneofCase.RollGoldHandle:
			if (RollGoldHandle == null)
			{
				RollGoldHandle = new RollGoldC2S();
			}
			RollGoldHandle.MergeFrom(other.RollGoldHandle);
			break;
		case MsgOneofCase.RollGold:
			if (RollGold == null)
			{
				RollGold = new RollGoldS2C();
			}
			RollGold.MergeFrom(other.RollGold);
			break;
		case MsgOneofCase.EventThrowDiceHandle:
			if (EventThrowDiceHandle == null)
			{
				EventThrowDiceHandle = new EventThrowDiceC2S();
			}
			EventThrowDiceHandle.MergeFrom(other.EventThrowDiceHandle);
			break;
		case MsgOneofCase.EventThrowDice:
			if (EventThrowDice == null)
			{
				EventThrowDice = new EventThrowDiceS2C();
			}
			EventThrowDice.MergeFrom(other.EventThrowDice);
			break;
		case MsgOneofCase.TriggerEventHandle:
			if (TriggerEventHandle == null)
			{
				TriggerEventHandle = new TriggerEventC2S();
			}
			TriggerEventHandle.MergeFrom(other.TriggerEventHandle);
			break;
		case MsgOneofCase.TriggerEvent:
			if (TriggerEvent == null)
			{
				TriggerEvent = new TriggerEventS2C();
			}
			TriggerEvent.MergeFrom(other.TriggerEvent);
			break;
		case MsgOneofCase.UseEffectCardHandle:
			if (UseEffectCardHandle == null)
			{
				UseEffectCardHandle = new UseEffectCardC2S();
			}
			UseEffectCardHandle.MergeFrom(other.UseEffectCardHandle);
			break;
		case MsgOneofCase.UseEffectCard:
			if (UseEffectCard == null)
			{
				UseEffectCard = new UseEffectCardS2C();
			}
			UseEffectCard.MergeFrom(other.UseEffectCard);
			break;
		case MsgOneofCase.BombThrowDiceHandle:
			if (BombThrowDiceHandle == null)
			{
				BombThrowDiceHandle = new BombThrowDiceC2S();
			}
			BombThrowDiceHandle.MergeFrom(other.BombThrowDiceHandle);
			break;
		case MsgOneofCase.BombThrowDice:
			if (BombThrowDice == null)
			{
				BombThrowDice = new BombThrowDiceS2C();
			}
			BombThrowDice.MergeFrom(other.BombThrowDice);
			break;
		case MsgOneofCase.ChoiceDirectionHandle:
			if (ChoiceDirectionHandle == null)
			{
				ChoiceDirectionHandle = new ChoiceDirectionC2S();
			}
			ChoiceDirectionHandle.MergeFrom(other.ChoiceDirectionHandle);
			break;
		case MsgOneofCase.ChoiceDirection:
			if (ChoiceDirection == null)
			{
				ChoiceDirection = new ChoiceDirectionS2C();
			}
			ChoiceDirection.MergeFrom(other.ChoiceDirection);
			break;
		case MsgOneofCase.LandChoiceTargetHandle:
			if (LandChoiceTargetHandle == null)
			{
				LandChoiceTargetHandle = new LandChoiceTargetC2S();
			}
			LandChoiceTargetHandle.MergeFrom(other.LandChoiceTargetHandle);
			break;
		case MsgOneofCase.LandChoiceTarget:
			if (LandChoiceTarget == null)
			{
				LandChoiceTarget = new LandChoiceTargetS2C();
			}
			LandChoiceTarget.MergeFrom(other.LandChoiceTarget);
			break;
		case MsgOneofCase.ThrowDiceResultHandle:
			if (ThrowDiceResultHandle == null)
			{
				ThrowDiceResultHandle = new ThrowDiceResultC2S();
			}
			ThrowDiceResultHandle.MergeFrom(other.ThrowDiceResultHandle);
			break;
		case MsgOneofCase.ThrowDiceResult:
			if (ThrowDiceResult == null)
			{
				ThrowDiceResult = new ThrowDiceResultS2C();
			}
			ThrowDiceResult.MergeFrom(other.ThrowDiceResult);
			break;
		case MsgOneofCase.TriggerDivinationHandle:
			if (TriggerDivinationHandle == null)
			{
				TriggerDivinationHandle = new TriggerDivinationC2S();
			}
			TriggerDivinationHandle.MergeFrom(other.TriggerDivinationHandle);
			break;
		case MsgOneofCase.TriggerDivination:
			if (TriggerDivination == null)
			{
				TriggerDivination = new TriggerDivinationS2C();
			}
			TriggerDivination.MergeFrom(other.TriggerDivination);
			break;
		case MsgOneofCase.TriggerDestinyHandle:
			if (TriggerDestinyHandle == null)
			{
				TriggerDestinyHandle = new TriggerDestinyC2S();
			}
			TriggerDestinyHandle.MergeFrom(other.TriggerDestinyHandle);
			break;
		case MsgOneofCase.TriggerDestiny:
			if (TriggerDestiny == null)
			{
				TriggerDestiny = new TriggerDestinyS2C();
			}
			TriggerDestiny.MergeFrom(other.TriggerDestiny);
			break;
		case MsgOneofCase.UseQuickCardHandle:
			if (UseQuickCardHandle == null)
			{
				UseQuickCardHandle = new UseQuickCardC2S();
			}
			UseQuickCardHandle.MergeFrom(other.UseQuickCardHandle);
			break;
		case MsgOneofCase.UseQuickCard:
			if (UseQuickCard == null)
			{
				UseQuickCard = new UseQuickCardS2C();
			}
			UseQuickCard.MergeFrom(other.UseQuickCard);
			break;
		case MsgOneofCase.AbandonCardHandle:
			if (AbandonCardHandle == null)
			{
				AbandonCardHandle = new AbandonCardC2S();
			}
			AbandonCardHandle.MergeFrom(other.AbandonCardHandle);
			break;
		case MsgOneofCase.AbandonCard:
			if (AbandonCard == null)
			{
				AbandonCard = new AbandonCardS2C();
			}
			AbandonCard.MergeFrom(other.AbandonCard);
			break;
		case MsgOneofCase.StopOrContinueHandle:
			if (StopOrContinueHandle == null)
			{
				StopOrContinueHandle = new StopOrContinueC2S();
			}
			StopOrContinueHandle.MergeFrom(other.StopOrContinueHandle);
			break;
		case MsgOneofCase.StopOrContinue:
			if (StopOrContinue == null)
			{
				StopOrContinue = new StopOrContinueS2C();
			}
			StopOrContinue.MergeFrom(other.StopOrContinue);
			break;
		case MsgOneofCase.StartGambleHandle:
			if (StartGambleHandle == null)
			{
				StartGambleHandle = new StartGambleC2S();
			}
			StartGambleHandle.MergeFrom(other.StartGambleHandle);
			break;
		case MsgOneofCase.StartGamble:
			if (StartGamble == null)
			{
				StartGamble = new StartGambleS2C();
			}
			StartGamble.MergeFrom(other.StartGamble);
			break;
		case MsgOneofCase.GambleThrowDicHandle:
			if (GambleThrowDicHandle == null)
			{
				GambleThrowDicHandle = new GambleThrowDicC2S();
			}
			GambleThrowDicHandle.MergeFrom(other.GambleThrowDicHandle);
			break;
		case MsgOneofCase.GambleThrowDic:
			if (GambleThrowDic == null)
			{
				GambleThrowDic = new GambleThrowDicS2C();
			}
			GambleThrowDic.MergeFrom(other.GambleThrowDic);
			break;
		case MsgOneofCase.ChoiceHeroC2S2Handle:
			if (ChoiceHeroC2S2Handle == null)
			{
				ChoiceHeroC2S2Handle = new ChoiceHeroC2S2();
			}
			ChoiceHeroC2S2Handle.MergeFrom(other.ChoiceHeroC2S2Handle);
			break;
		case MsgOneofCase.ChoiceHero2:
			if (ChoiceHero2 == null)
			{
				ChoiceHero2 = new ChoiceHeroS2C2();
			}
			ChoiceHero2.MergeFrom(other.ChoiceHero2);
			break;
		case MsgOneofCase.AffirmHeroHandle:
			if (AffirmHeroHandle == null)
			{
				AffirmHeroHandle = new AffirmHeroC2S();
			}
			AffirmHeroHandle.MergeFrom(other.AffirmHeroHandle);
			break;
		case MsgOneofCase.AffirmHero:
			if (AffirmHero == null)
			{
				AffirmHero = new AffirmHeroS2C();
			}
			AffirmHero.MergeFrom(other.AffirmHero);
			break;
		case MsgOneofCase.SearchRoomHandle:
			if (SearchRoomHandle == null)
			{
				SearchRoomHandle = new SearchRoomC2S();
			}
			SearchRoomHandle.MergeFrom(other.SearchRoomHandle);
			break;
		case MsgOneofCase.SearchRoom:
			if (SearchRoom == null)
			{
				SearchRoom = new SearchRoomS2C();
			}
			SearchRoom.MergeFrom(other.SearchRoom);
			break;
		case MsgOneofCase.GmHandle:
			if (GmHandle == null)
			{
				GmHandle = new GmC2S();
			}
			GmHandle.MergeFrom(other.GmHandle);
			break;
		case MsgOneofCase.Gm:
			if (Gm == null)
			{
				Gm = new GmS2C();
			}
			Gm.MergeFrom(other.Gm);
			break;
		case MsgOneofCase.TriggerHospitalHandle:
			if (TriggerHospitalHandle == null)
			{
				TriggerHospitalHandle = new TriggerHospitalC2S();
			}
			TriggerHospitalHandle.MergeFrom(other.TriggerHospitalHandle);
			break;
		case MsgOneofCase.TriggerHospital:
			if (TriggerHospital == null)
			{
				TriggerHospital = new TriggerHospitalS2C();
			}
			TriggerHospital.MergeFrom(other.TriggerHospital);
			break;
		case MsgOneofCase.SendChatHandle:
			if (SendChatHandle == null)
			{
				SendChatHandle = new SendChatC2S();
			}
			SendChatHandle.MergeFrom(other.SendChatHandle);
			break;
		case MsgOneofCase.SendChat:
			if (SendChat == null)
			{
				SendChat = new SendChatS2C();
			}
			SendChat.MergeFrom(other.SendChat);
			break;
		case MsgOneofCase.PlayerShopBuyC2S:
			if (PlayerShopBuyC2S == null)
			{
				PlayerShopBuyC2S = new PlayerShopBuyC2S();
			}
			PlayerShopBuyC2S.MergeFrom(other.PlayerShopBuyC2S);
			break;
		case MsgOneofCase.PlayerShopBuyS2C:
			if (PlayerShopBuyS2C == null)
			{
				PlayerShopBuyS2C = new PlayerShopBuyS2C();
			}
			PlayerShopBuyS2C.MergeFrom(other.PlayerShopBuyS2C);
			break;
		case MsgOneofCase.PlayerUseItemHandle:
			if (PlayerUseItemHandle == null)
			{
				PlayerUseItemHandle = new PlayerUseItemC2S();
			}
			PlayerUseItemHandle.MergeFrom(other.PlayerUseItemHandle);
			break;
		case MsgOneofCase.PlayerUseItem:
			if (PlayerUseItem == null)
			{
				PlayerUseItem = new PlayerUseItemS2C();
			}
			PlayerUseItem.MergeFrom(other.PlayerUseItem);
			break;
		case MsgOneofCase.UseTreasureC2S:
			if (UseTreasureC2S == null)
			{
				UseTreasureC2S = new UseTreasureC2S();
			}
			UseTreasureC2S.MergeFrom(other.UseTreasureC2S);
			break;
		case MsgOneofCase.UseTreasureS2C:
			if (UseTreasureS2C == null)
			{
				UseTreasureS2C = new UseTreasureS2C();
			}
			UseTreasureS2C.MergeFrom(other.UseTreasureS2C);
			break;
		case MsgOneofCase.UseTreasureAutoTransformC2S:
			if (UseTreasureAutoTransformC2S == null)
			{
				UseTreasureAutoTransformC2S = new UseTreasureAutoTransformC2S();
			}
			UseTreasureAutoTransformC2S.MergeFrom(other.UseTreasureAutoTransformC2S);
			break;
		case MsgOneofCase.UseTreasureAutoTransformS2C:
			if (UseTreasureAutoTransformS2C == null)
			{
				UseTreasureAutoTransformS2C = new UseTreasureAutoTransformS2C();
			}
			UseTreasureAutoTransformS2C.MergeFrom(other.UseTreasureAutoTransformS2C);
			break;
		case MsgOneofCase.SetFashionC2S:
			if (SetFashionC2S == null)
			{
				SetFashionC2S = new SetFashionC2S();
			}
			SetFashionC2S.MergeFrom(other.SetFashionC2S);
			break;
		case MsgOneofCase.SetFashionS2C:
			if (SetFashionS2C == null)
			{
				SetFashionS2C = new SetFashionS2C();
			}
			SetFashionS2C.MergeFrom(other.SetFashionS2C);
			break;
		case MsgOneofCase.SelectFashionPlanC2S:
			if (SelectFashionPlanC2S == null)
			{
				SelectFashionPlanC2S = new SelectFashionPlanC2S();
			}
			SelectFashionPlanC2S.MergeFrom(other.SelectFashionPlanC2S);
			break;
		case MsgOneofCase.SelectFashionPlanS2C:
			if (SelectFashionPlanS2C == null)
			{
				SelectFashionPlanS2C = new SelectFashionPlanS2C();
			}
			SelectFashionPlanS2C.MergeFrom(other.SelectFashionPlanS2C);
			break;
		case MsgOneofCase.GachaC2S:
			if (GachaC2S == null)
			{
				GachaC2S = new GachaC2S();
			}
			GachaC2S.MergeFrom(other.GachaC2S);
			break;
		case MsgOneofCase.GachaS2C:
			if (GachaS2C == null)
			{
				GachaS2C = new GachaS2C();
			}
			GachaS2C.MergeFrom(other.GachaS2C);
			break;
		case MsgOneofCase.SteamSearchRoomC2S:
			if (SteamSearchRoomC2S == null)
			{
				SteamSearchRoomC2S = new SteamSearchRoomC2S();
			}
			SteamSearchRoomC2S.MergeFrom(other.SteamSearchRoomC2S);
			break;
		case MsgOneofCase.SteamSearchRoomS2C:
			if (SteamSearchRoomS2C == null)
			{
				SteamSearchRoomS2C = new SteamSearchRoomS2C();
			}
			SteamSearchRoomS2C.MergeFrom(other.SteamSearchRoomS2C);
			break;
		case MsgOneofCase.CheatItemHandle:
			if (CheatItemHandle == null)
			{
				CheatItemHandle = new CheatItemC2S();
			}
			CheatItemHandle.MergeFrom(other.CheatItemHandle);
			break;
		case MsgOneofCase.CheatItem:
			if (CheatItem == null)
			{
				CheatItem = new CheatItemS2C();
			}
			CheatItem.MergeFrom(other.CheatItem);
			break;
		case MsgOneofCase.RoleCardUpLvC2S:
			if (RoleCardUpLvC2S == null)
			{
				RoleCardUpLvC2S = new RoleCardUpLvC2S();
			}
			RoleCardUpLvC2S.MergeFrom(other.RoleCardUpLvC2S);
			break;
		case MsgOneofCase.RoleCardUpLvS2C:
			if (RoleCardUpLvS2C == null)
			{
				RoleCardUpLvS2C = new RoleCardUpLvS2C();
			}
			RoleCardUpLvS2C.MergeFrom(other.RoleCardUpLvS2C);
			break;
		case MsgOneofCase.RoleCardBreakThroughC2S:
			if (RoleCardBreakThroughC2S == null)
			{
				RoleCardBreakThroughC2S = new RoleCardBreakThroughC2S();
			}
			RoleCardBreakThroughC2S.MergeFrom(other.RoleCardBreakThroughC2S);
			break;
		case MsgOneofCase.RoleCardBreakThroughS2C:
			if (RoleCardBreakThroughS2C == null)
			{
				RoleCardBreakThroughS2C = new RoleCardBreakThroughS2C();
			}
			RoleCardBreakThroughS2C.MergeFrom(other.RoleCardBreakThroughS2C);
			break;
		case MsgOneofCase.RoleCardChoiceResC2S:
			if (RoleCardChoiceResC2S == null)
			{
				RoleCardChoiceResC2S = new RoleCardChoiceResC2S();
			}
			RoleCardChoiceResC2S.MergeFrom(other.RoleCardChoiceResC2S);
			break;
		case MsgOneofCase.RoleCardChoiceResS2C:
			if (RoleCardChoiceResS2C == null)
			{
				RoleCardChoiceResS2C = new RoleCardChoiceResS2C();
			}
			RoleCardChoiceResS2C.MergeFrom(other.RoleCardChoiceResS2C);
			break;
		case MsgOneofCase.TaskRewardC2S:
			if (TaskRewardC2S == null)
			{
				TaskRewardC2S = new TaskRewardC2S();
			}
			TaskRewardC2S.MergeFrom(other.TaskRewardC2S);
			break;
		case MsgOneofCase.TaskRewardS2C:
			if (TaskRewardS2C == null)
			{
				TaskRewardS2C = new TaskRewardS2C();
			}
			TaskRewardS2C.MergeFrom(other.TaskRewardS2C);
			break;
		case MsgOneofCase.TeachingC2S:
			if (TeachingC2S == null)
			{
				TeachingC2S = new TeachingC2S();
			}
			TeachingC2S.MergeFrom(other.TeachingC2S);
			break;
		case MsgOneofCase.TeachingS2C:
			if (TeachingS2C == null)
			{
				TeachingS2C = new TeachingS2C();
			}
			TeachingS2C.MergeFrom(other.TeachingS2C);
			break;
		case MsgOneofCase.QuickJoinRoomC2S:
			if (QuickJoinRoomC2S == null)
			{
				QuickJoinRoomC2S = new QuickJoinRoomC2S();
			}
			QuickJoinRoomC2S.MergeFrom(other.QuickJoinRoomC2S);
			break;
		case MsgOneofCase.QuickJoinRoomS2C:
			if (QuickJoinRoomS2C == null)
			{
				QuickJoinRoomS2C = new QuickJoinRoomS2C();
			}
			QuickJoinRoomS2C.MergeFrom(other.QuickJoinRoomS2C);
			break;
		case MsgOneofCase.RoomKickPlayerC2S:
			if (RoomKickPlayerC2S == null)
			{
				RoomKickPlayerC2S = new RoomKickPlayerC2S();
			}
			RoomKickPlayerC2S.MergeFrom(other.RoomKickPlayerC2S);
			break;
		case MsgOneofCase.RoomKickPlayerS2C:
			if (RoomKickPlayerS2C == null)
			{
				RoomKickPlayerS2C = new RoomKickPlayerS2C();
			}
			RoomKickPlayerS2C.MergeFrom(other.RoomKickPlayerS2C);
			break;
		case MsgOneofCase.RoomAbdicationC2S:
			if (RoomAbdicationC2S == null)
			{
				RoomAbdicationC2S = new RoomAbdicationC2S();
			}
			RoomAbdicationC2S.MergeFrom(other.RoomAbdicationC2S);
			break;
		case MsgOneofCase.RoomAbdicationS2C:
			if (RoomAbdicationS2C == null)
			{
				RoomAbdicationS2C = new RoomAbdicationS2C();
			}
			RoomAbdicationS2C.MergeFrom(other.RoomAbdicationS2C);
			break;
		case MsgOneofCase.RoomReadyC2S:
			if (RoomReadyC2S == null)
			{
				RoomReadyC2S = new RoomReadyC2S();
			}
			RoomReadyC2S.MergeFrom(other.RoomReadyC2S);
			break;
		case MsgOneofCase.RoomReadyS2C:
			if (RoomReadyS2C == null)
			{
				RoomReadyS2C = new RoomReadyS2C();
			}
			RoomReadyS2C.MergeFrom(other.RoomReadyS2C);
			break;
		case MsgOneofCase.ChargeCreateC2S:
			if (ChargeCreateC2S == null)
			{
				ChargeCreateC2S = new ChargeCreateC2S();
			}
			ChargeCreateC2S.MergeFrom(other.ChargeCreateC2S);
			break;
		case MsgOneofCase.ChargeCreateS2C:
			if (ChargeCreateS2C == null)
			{
				ChargeCreateS2C = new ChargeCreateS2C();
			}
			ChargeCreateS2C.MergeFrom(other.ChargeCreateS2C);
			break;
		case MsgOneofCase.ChargeC2S:
			if (ChargeC2S == null)
			{
				ChargeC2S = new ChargeC2S();
			}
			ChargeC2S.MergeFrom(other.ChargeC2S);
			break;
		case MsgOneofCase.ChargeS2C:
			if (ChargeS2C == null)
			{
				ChargeS2C = new ChargeS2C();
			}
			ChargeS2C.MergeFrom(other.ChargeS2C);
			break;
		case MsgOneofCase.GiftCdkC2S:
			if (GiftCdkC2S == null)
			{
				GiftCdkC2S = new GiftCdkC2S();
			}
			GiftCdkC2S.MergeFrom(other.GiftCdkC2S);
			break;
		case MsgOneofCase.GiftCdkS2C:
			if (GiftCdkS2C == null)
			{
				GiftCdkS2C = new GiftCdkS2C();
			}
			GiftCdkS2C.MergeFrom(other.GiftCdkS2C);
			break;
		case MsgOneofCase.MailReadC2S:
			if (MailReadC2S == null)
			{
				MailReadC2S = new MailReadC2S();
			}
			MailReadC2S.MergeFrom(other.MailReadC2S);
			break;
		case MsgOneofCase.MailReadS2C:
			if (MailReadS2C == null)
			{
				MailReadS2C = new MailReadS2C();
			}
			MailReadS2C.MergeFrom(other.MailReadS2C);
			break;
		case MsgOneofCase.MailGetRewardC2S:
			if (MailGetRewardC2S == null)
			{
				MailGetRewardC2S = new MailGetRewardC2S();
			}
			MailGetRewardC2S.MergeFrom(other.MailGetRewardC2S);
			break;
		case MsgOneofCase.MailGetRewardS2C:
			if (MailGetRewardS2C == null)
			{
				MailGetRewardS2C = new MailGetRewardS2C();
			}
			MailGetRewardS2C.MergeFrom(other.MailGetRewardS2C);
			break;
		case MsgOneofCase.MailDelReadC2S:
			if (MailDelReadC2S == null)
			{
				MailDelReadC2S = new MailDelReadC2S();
			}
			MailDelReadC2S.MergeFrom(other.MailDelReadC2S);
			break;
		case MsgOneofCase.MailDelReadS2C:
			if (MailDelReadS2C == null)
			{
				MailDelReadS2C = new MailDelReadS2C();
			}
			MailDelReadS2C.MergeFrom(other.MailDelReadS2C);
			break;
		case MsgOneofCase.GachaRecordC2S:
			if (GachaRecordC2S == null)
			{
				GachaRecordC2S = new GachaRecordC2S();
			}
			GachaRecordC2S.MergeFrom(other.GachaRecordC2S);
			break;
		case MsgOneofCase.GachaRecordS2C:
			if (GachaRecordS2C == null)
			{
				GachaRecordS2C = new GachaRecordS2C();
			}
			GachaRecordS2C.MergeFrom(other.GachaRecordS2C);
			break;
		case MsgOneofCase.ActivityTaskRewardC2S:
			if (ActivityTaskRewardC2S == null)
			{
				ActivityTaskRewardC2S = new ActivityTaskRewardC2S();
			}
			ActivityTaskRewardC2S.MergeFrom(other.ActivityTaskRewardC2S);
			break;
		case MsgOneofCase.ActivityTaskRewardS2C:
			if (ActivityTaskRewardS2C == null)
			{
				ActivityTaskRewardS2C = new ActivityTaskRewardS2C();
			}
			ActivityTaskRewardS2C.MergeFrom(other.ActivityTaskRewardS2C);
			break;
		case MsgOneofCase.RoomShortChatC2S:
			if (RoomShortChatC2S == null)
			{
				RoomShortChatC2S = new RoomShortChatC2S();
			}
			RoomShortChatC2S.MergeFrom(other.RoomShortChatC2S);
			break;
		case MsgOneofCase.RoomShortChatS2C:
			if (RoomShortChatS2C == null)
			{
				RoomShortChatS2C = new RoomShortChatS2C();
			}
			RoomShortChatS2C.MergeFrom(other.RoomShortChatS2C);
			break;
		case MsgOneofCase.SetShowPlayerC2S:
			if (SetShowPlayerC2S == null)
			{
				SetShowPlayerC2S = new SetShowPlayerC2S();
			}
			SetShowPlayerC2S.MergeFrom(other.SetShowPlayerC2S);
			break;
		case MsgOneofCase.SetShowPlayerS2C:
			if (SetShowPlayerS2C == null)
			{
				SetShowPlayerS2C = new SetShowPlayerS2C();
			}
			SetShowPlayerS2C.MergeFrom(other.SetShowPlayerS2C);
			break;
		case MsgOneofCase.GetShowPlayerC2S:
			if (GetShowPlayerC2S == null)
			{
				GetShowPlayerC2S = new GetShowPlayerC2S();
			}
			GetShowPlayerC2S.MergeFrom(other.GetShowPlayerC2S);
			break;
		case MsgOneofCase.GetShowPlayerS2C:
			if (GetShowPlayerS2C == null)
			{
				GetShowPlayerS2C = new GetShowPlayerS2C();
			}
			GetShowPlayerS2C.MergeFrom(other.GetShowPlayerS2C);
			break;
		case MsgOneofCase.GetPlayerFightRecordC2S:
			if (GetPlayerFightRecordC2S == null)
			{
				GetPlayerFightRecordC2S = new GetPlayerFightRecordC2S();
			}
			GetPlayerFightRecordC2S.MergeFrom(other.GetPlayerFightRecordC2S);
			break;
		case MsgOneofCase.GetPlayerFightRecordS2C:
			if (GetPlayerFightRecordS2C == null)
			{
				GetPlayerFightRecordS2C = new GetPlayerFightRecordS2C();
			}
			GetPlayerFightRecordS2C.MergeFrom(other.GetPlayerFightRecordS2C);
			break;
		case MsgOneofCase.GetDay7RewardC2S:
			if (GetDay7RewardC2S == null)
			{
				GetDay7RewardC2S = new GetDay7RewardC2S();
			}
			GetDay7RewardC2S.MergeFrom(other.GetDay7RewardC2S);
			break;
		case MsgOneofCase.GetDay7RewardS2C:
			if (GetDay7RewardS2C == null)
			{
				GetDay7RewardS2C = new GetDay7RewardS2C();
			}
			GetDay7RewardS2C.MergeFrom(other.GetDay7RewardS2C);
			break;
		case MsgOneofCase.PraisePlayerC2S:
			if (PraisePlayerC2S == null)
			{
				PraisePlayerC2S = new PraisePlayerC2S();
			}
			PraisePlayerC2S.MergeFrom(other.PraisePlayerC2S);
			break;
		case MsgOneofCase.PraisePlayerS2C:
			if (PraisePlayerS2C == null)
			{
				PraisePlayerS2C = new PraisePlayerS2C();
			}
			PraisePlayerS2C.MergeFrom(other.PraisePlayerS2C);
			break;
		case MsgOneofCase.ClientDataUploadC2S:
			if (ClientDataUploadC2S == null)
			{
				ClientDataUploadC2S = new ClientDataUploadC2S();
			}
			ClientDataUploadC2S.MergeFrom(other.ClientDataUploadC2S);
			break;
		case MsgOneofCase.ClientDataUploadS2C:
			if (ClientDataUploadS2C == null)
			{
				ClientDataUploadS2C = new ClientDataUploadS2C();
			}
			ClientDataUploadS2C.MergeFrom(other.ClientDataUploadS2C);
			break;
		case MsgOneofCase.FriendListC2S:
			if (FriendListC2S == null)
			{
				FriendListC2S = new FriendListC2S();
			}
			FriendListC2S.MergeFrom(other.FriendListC2S);
			break;
		case MsgOneofCase.FriendListS2C:
			if (FriendListS2C == null)
			{
				FriendListS2C = new FriendListS2C();
			}
			FriendListS2C.MergeFrom(other.FriendListS2C);
			break;
		case MsgOneofCase.FriendApplyC2S:
			if (FriendApplyC2S == null)
			{
				FriendApplyC2S = new FriendApplyC2S();
			}
			FriendApplyC2S.MergeFrom(other.FriendApplyC2S);
			break;
		case MsgOneofCase.FriendApplyS2C:
			if (FriendApplyS2C == null)
			{
				FriendApplyS2C = new FriendApplyS2C();
			}
			FriendApplyS2C.MergeFrom(other.FriendApplyS2C);
			break;
		case MsgOneofCase.FriendApplyListC2S:
			if (FriendApplyListC2S == null)
			{
				FriendApplyListC2S = new FriendApplyListC2S();
			}
			FriendApplyListC2S.MergeFrom(other.FriendApplyListC2S);
			break;
		case MsgOneofCase.FriendApplyListS2C:
			if (FriendApplyListS2C == null)
			{
				FriendApplyListS2C = new FriendApplyListS2C();
			}
			FriendApplyListS2C.MergeFrom(other.FriendApplyListS2C);
			break;
		case MsgOneofCase.FriendApplyOpC2S:
			if (FriendApplyOpC2S == null)
			{
				FriendApplyOpC2S = new FriendApplyOpC2S();
			}
			FriendApplyOpC2S.MergeFrom(other.FriendApplyOpC2S);
			break;
		case MsgOneofCase.FriendApplyOpS2C:
			if (FriendApplyOpS2C == null)
			{
				FriendApplyOpS2C = new FriendApplyOpS2C();
			}
			FriendApplyOpS2C.MergeFrom(other.FriendApplyOpS2C);
			break;
		case MsgOneofCase.FriendOpC2S:
			if (FriendOpC2S == null)
			{
				FriendOpC2S = new FriendOpC2S();
			}
			FriendOpC2S.MergeFrom(other.FriendOpC2S);
			break;
		case MsgOneofCase.FriendOpS2C:
			if (FriendOpS2C == null)
			{
				FriendOpS2C = new FriendOpS2C();
			}
			FriendOpS2C.MergeFrom(other.FriendOpS2C);
			break;
		case MsgOneofCase.FriendInviteC2S:
			if (FriendInviteC2S == null)
			{
				FriendInviteC2S = new FriendInviteC2S();
			}
			FriendInviteC2S.MergeFrom(other.FriendInviteC2S);
			break;
		case MsgOneofCase.FriendInviteS2C:
			if (FriendInviteS2C == null)
			{
				FriendInviteS2C = new FriendInviteS2C();
			}
			FriendInviteS2C.MergeFrom(other.FriendInviteS2C);
			break;
		case MsgOneofCase.FriendInviteListC2S:
			if (FriendInviteListC2S == null)
			{
				FriendInviteListC2S = new FriendInviteListC2S();
			}
			FriendInviteListC2S.MergeFrom(other.FriendInviteListC2S);
			break;
		case MsgOneofCase.FriendInviteListS2C:
			if (FriendInviteListS2C == null)
			{
				FriendInviteListS2C = new FriendInviteListS2C();
			}
			FriendInviteListS2C.MergeFrom(other.FriendInviteListS2C);
			break;
		case MsgOneofCase.FriendInviteCleanC2S:
			if (FriendInviteCleanC2S == null)
			{
				FriendInviteCleanC2S = new FriendInviteCleanC2S();
			}
			FriendInviteCleanC2S.MergeFrom(other.FriendInviteCleanC2S);
			break;
		case MsgOneofCase.FriendInviteCleanS2C:
			if (FriendInviteCleanS2C == null)
			{
				FriendInviteCleanS2C = new FriendInviteCleanS2C();
			}
			FriendInviteCleanS2C.MergeFrom(other.FriendInviteCleanS2C);
			break;
		case MsgOneofCase.FriendBlacksListC2S:
			if (FriendBlacksListC2S == null)
			{
				FriendBlacksListC2S = new FriendBlacksListC2S();
			}
			FriendBlacksListC2S.MergeFrom(other.FriendBlacksListC2S);
			break;
		case MsgOneofCase.FriendBlacksListS2C:
			if (FriendBlacksListS2C == null)
			{
				FriendBlacksListS2C = new FriendBlacksListS2C();
			}
			FriendBlacksListS2C.MergeFrom(other.FriendBlacksListS2C);
			break;
		case MsgOneofCase.NearFightPlayerC2S:
			if (NearFightPlayerC2S == null)
			{
				NearFightPlayerC2S = new NearFightPlayerC2S();
			}
			NearFightPlayerC2S.MergeFrom(other.NearFightPlayerC2S);
			break;
		case MsgOneofCase.NearFightPlayerS2C:
			if (NearFightPlayerS2C == null)
			{
				NearFightPlayerS2C = new NearFightPlayerS2C();
			}
			NearFightPlayerS2C.MergeFrom(other.NearFightPlayerS2C);
			break;
		case MsgOneofCase.SearchPlayerC2S:
			if (SearchPlayerC2S == null)
			{
				SearchPlayerC2S = new SearchPlayerC2S();
			}
			SearchPlayerC2S.MergeFrom(other.SearchPlayerC2S);
			break;
		case MsgOneofCase.SearchPlayerS2C:
			if (SearchPlayerS2C == null)
			{
				SearchPlayerS2C = new SearchPlayerS2C();
			}
			SearchPlayerS2C.MergeFrom(other.SearchPlayerS2C);
			break;
		case MsgOneofCase.ScratchCardC2S:
			if (ScratchCardC2S == null)
			{
				ScratchCardC2S = new ScratchCardC2S();
			}
			ScratchCardC2S.MergeFrom(other.ScratchCardC2S);
			break;
		case MsgOneofCase.ScratchCardS2C:
			if (ScratchCardS2C == null)
			{
				ScratchCardS2C = new ScratchCardS2C();
			}
			ScratchCardS2C.MergeFrom(other.ScratchCardS2C);
			break;
		case MsgOneofCase.NextScratchCardPoolC2S:
			if (NextScratchCardPoolC2S == null)
			{
				NextScratchCardPoolC2S = new NextScratchCardPoolC2S();
			}
			NextScratchCardPoolC2S.MergeFrom(other.NextScratchCardPoolC2S);
			break;
		case MsgOneofCase.NextScratchCardPoolS2C:
			if (NextScratchCardPoolS2C == null)
			{
				NextScratchCardPoolS2C = new NextScratchCardPoolS2C();
			}
			NextScratchCardPoolS2C.MergeFrom(other.NextScratchCardPoolS2C);
			break;
		case MsgOneofCase.WatchJoinRoomC2S:
			if (WatchJoinRoomC2S == null)
			{
				WatchJoinRoomC2S = new WatchJoinRoomC2S();
			}
			WatchJoinRoomC2S.MergeFrom(other.WatchJoinRoomC2S);
			break;
		case MsgOneofCase.WatchJoinRoomS2C:
			if (WatchJoinRoomS2C == null)
			{
				WatchJoinRoomS2C = new WatchJoinRoomS2C();
			}
			WatchJoinRoomS2C.MergeFrom(other.WatchJoinRoomS2C);
			break;
		case MsgOneofCase.WatchRefreshRoomStateC2S:
			if (WatchRefreshRoomStateC2S == null)
			{
				WatchRefreshRoomStateC2S = new WatchRefreshRoomStateC2S();
			}
			WatchRefreshRoomStateC2S.MergeFrom(other.WatchRefreshRoomStateC2S);
			break;
		case MsgOneofCase.WatchRefreshRoomStateS2C:
			if (WatchRefreshRoomStateS2C == null)
			{
				WatchRefreshRoomStateS2C = new WatchRefreshRoomStateS2C();
			}
			WatchRefreshRoomStateS2C.MergeFrom(other.WatchRefreshRoomStateS2C);
			break;
		case MsgOneofCase.WatchExitRoomC2S:
			if (WatchExitRoomC2S == null)
			{
				WatchExitRoomC2S = new WatchExitRoomC2S();
			}
			WatchExitRoomC2S.MergeFrom(other.WatchExitRoomC2S);
			break;
		case MsgOneofCase.WatchExitRoomS2C:
			if (WatchExitRoomS2C == null)
			{
				WatchExitRoomS2C = new WatchExitRoomS2C();
			}
			WatchExitRoomS2C.MergeFrom(other.WatchExitRoomS2C);
			break;
		case MsgOneofCase.BattlePassGetRewardC2S:
			if (BattlePassGetRewardC2S == null)
			{
				BattlePassGetRewardC2S = new BattlePassGetRewardC2S();
			}
			BattlePassGetRewardC2S.MergeFrom(other.BattlePassGetRewardC2S);
			break;
		case MsgOneofCase.BattlePassGetRewardS2C:
			if (BattlePassGetRewardS2C == null)
			{
				BattlePassGetRewardS2C = new BattlePassGetRewardS2C();
			}
			BattlePassGetRewardS2C.MergeFrom(other.BattlePassGetRewardS2C);
			break;
		case MsgOneofCase.BattlePassTaskRewardC2S:
			if (BattlePassTaskRewardC2S == null)
			{
				BattlePassTaskRewardC2S = new BattlePassTaskRewardC2S();
			}
			BattlePassTaskRewardC2S.MergeFrom(other.BattlePassTaskRewardC2S);
			break;
		case MsgOneofCase.BattlePassTaskRewardS2C:
			if (BattlePassTaskRewardS2C == null)
			{
				BattlePassTaskRewardS2C = new BattlePassTaskRewardS2C();
			}
			BattlePassTaskRewardS2C.MergeFrom(other.BattlePassTaskRewardS2C);
			break;
		case MsgOneofCase.BattlePassUpLvC2S:
			if (BattlePassUpLvC2S == null)
			{
				BattlePassUpLvC2S = new BattlePassUpLvC2S();
			}
			BattlePassUpLvC2S.MergeFrom(other.BattlePassUpLvC2S);
			break;
		case MsgOneofCase.BattlePassUpLvS2C:
			if (BattlePassUpLvS2C == null)
			{
				BattlePassUpLvS2C = new BattlePassUpLvS2C();
			}
			BattlePassUpLvS2C.MergeFrom(other.BattlePassUpLvS2C);
			break;
		case MsgOneofCase.FriendSendMsgC2S:
			if (FriendSendMsgC2S == null)
			{
				FriendSendMsgC2S = new FriendSendMsgC2S();
			}
			FriendSendMsgC2S.MergeFrom(other.FriendSendMsgC2S);
			break;
		case MsgOneofCase.FriendSendMsgS2C:
			if (FriendSendMsgS2C == null)
			{
				FriendSendMsgS2C = new FriendSendMsgS2C();
			}
			FriendSendMsgS2C.MergeFrom(other.FriendSendMsgS2C);
			break;
		case MsgOneofCase.GetChatMsgC2S:
			if (GetChatMsgC2S == null)
			{
				GetChatMsgC2S = new GetChatMsgC2S();
			}
			GetChatMsgC2S.MergeFrom(other.GetChatMsgC2S);
			break;
		case MsgOneofCase.GetChatMsgS2C:
			if (GetChatMsgS2C == null)
			{
				GetChatMsgS2C = new GetChatMsgS2C();
			}
			GetChatMsgS2C.MergeFrom(other.GetChatMsgS2C);
			break;
		case MsgOneofCase.ReadChatMsgC2S:
			if (ReadChatMsgC2S == null)
			{
				ReadChatMsgC2S = new ReadChatMsgC2S();
			}
			ReadChatMsgC2S.MergeFrom(other.ReadChatMsgC2S);
			break;
		case MsgOneofCase.ReadChatMsgS2C:
			if (ReadChatMsgS2C == null)
			{
				ReadChatMsgS2C = new ReadChatMsgS2C();
			}
			ReadChatMsgS2C.MergeFrom(other.ReadChatMsgS2C);
			break;
		case MsgOneofCase.DelChatMsgInfoC2S:
			if (DelChatMsgInfoC2S == null)
			{
				DelChatMsgInfoC2S = new DelChatMsgInfoC2S();
			}
			DelChatMsgInfoC2S.MergeFrom(other.DelChatMsgInfoC2S);
			break;
		case MsgOneofCase.DelChatMsgInfoS2C:
			if (DelChatMsgInfoS2C == null)
			{
				DelChatMsgInfoS2C = new DelChatMsgInfoS2C();
			}
			DelChatMsgInfoS2C.MergeFrom(other.DelChatMsgInfoS2C);
			break;
		case MsgOneofCase.SelectRelicC2S:
			if (SelectRelicC2S == null)
			{
				SelectRelicC2S = new SelectRelicC2S();
			}
			SelectRelicC2S.MergeFrom(other.SelectRelicC2S);
			break;
		case MsgOneofCase.SelectRelicS2C:
			if (SelectRelicS2C == null)
			{
				SelectRelicS2C = new SelectRelicS2C();
			}
			SelectRelicS2C.MergeFrom(other.SelectRelicS2C);
			break;
		case MsgOneofCase.MonsterPursuitC2S:
			if (MonsterPursuitC2S == null)
			{
				MonsterPursuitC2S = new MonsterPursuitC2S();
			}
			MonsterPursuitC2S.MergeFrom(other.MonsterPursuitC2S);
			break;
		case MsgOneofCase.MonsterPursuitS2C:
			if (MonsterPursuitS2C == null)
			{
				MonsterPursuitS2C = new MonsterPursuitS2C();
			}
			MonsterPursuitS2C.MergeFrom(other.MonsterPursuitS2C);
			break;
		case MsgOneofCase.PVEShopBuyC2S:
			if (PVEShopBuyC2S == null)
			{
				PVEShopBuyC2S = new PVEShopBuyC2S();
			}
			PVEShopBuyC2S.MergeFrom(other.PVEShopBuyC2S);
			break;
		case MsgOneofCase.PVEShopBuyS2C:
			if (PVEShopBuyS2C == null)
			{
				PVEShopBuyS2C = new PVEShopBuyS2C();
			}
			PVEShopBuyS2C.MergeFrom(other.PVEShopBuyS2C);
			break;
		case MsgOneofCase.ClientCheckTaskC2S:
			if (ClientCheckTaskC2S == null)
			{
				ClientCheckTaskC2S = new ClientCheckTaskC2S();
			}
			ClientCheckTaskC2S.MergeFrom(other.ClientCheckTaskC2S);
			break;
		case MsgOneofCase.ClientCheckTaskS2C:
			if (ClientCheckTaskS2C == null)
			{
				ClientCheckTaskS2C = new ClientCheckTaskS2C();
			}
			ClientCheckTaskS2C.MergeFrom(other.ClientCheckTaskS2C);
			break;
		case MsgOneofCase.PveHeroUpLvC2S:
			if (PveHeroUpLvC2S == null)
			{
				PveHeroUpLvC2S = new PveHeroUpLvC2S();
			}
			PveHeroUpLvC2S.MergeFrom(other.PveHeroUpLvC2S);
			break;
		case MsgOneofCase.PveHeroUpLvS2C:
			if (PveHeroUpLvS2C == null)
			{
				PveHeroUpLvS2C = new PveHeroUpLvS2C();
			}
			PveHeroUpLvS2C.MergeFrom(other.PveHeroUpLvS2C);
			break;
		case MsgOneofCase.StartMatchC2S:
			if (StartMatchC2S == null)
			{
				StartMatchC2S = new StartMatchC2S();
			}
			StartMatchC2S.MergeFrom(other.StartMatchC2S);
			break;
		case MsgOneofCase.StartMatchS2C:
			if (StartMatchS2C == null)
			{
				StartMatchS2C = new StartMatchS2C();
			}
			StartMatchS2C.MergeFrom(other.StartMatchS2C);
			break;
		case MsgOneofCase.CancelMatchC2S:
			if (CancelMatchC2S == null)
			{
				CancelMatchC2S = new CancelMatchC2S();
			}
			CancelMatchC2S.MergeFrom(other.CancelMatchC2S);
			break;
		case MsgOneofCase.CancelMatchS2C:
			if (CancelMatchS2C == null)
			{
				CancelMatchS2C = new CancelMatchS2C();
			}
			CancelMatchS2C.MergeFrom(other.CancelMatchS2C);
			break;
		case MsgOneofCase.MatchSuccessC2S:
			if (MatchSuccessC2S == null)
			{
				MatchSuccessC2S = new MatchSuccessC2S();
			}
			MatchSuccessC2S.MergeFrom(other.MatchSuccessC2S);
			break;
		case MsgOneofCase.MatchSuccessS2C:
			if (MatchSuccessS2C == null)
			{
				MatchSuccessS2C = new MatchSuccessS2C();
			}
			MatchSuccessS2C.MergeFrom(other.MatchSuccessS2C);
			break;
		case MsgOneofCase.AccuseC2S:
			if (AccuseC2S == null)
			{
				AccuseC2S = new AccuseC2S();
			}
			AccuseC2S.MergeFrom(other.AccuseC2S);
			break;
		case MsgOneofCase.AccuseS2C:
			if (AccuseS2C == null)
			{
				AccuseS2C = new AccuseS2C();
			}
			AccuseS2C.MergeFrom(other.AccuseS2C);
			break;
		case MsgOneofCase.SingleCampaignC2S:
			if (SingleCampaignC2S == null)
			{
				SingleCampaignC2S = new SingleCampaignC2S();
			}
			SingleCampaignC2S.MergeFrom(other.SingleCampaignC2S);
			break;
		case MsgOneofCase.SingleCampaignS2C:
			if (SingleCampaignS2C == null)
			{
				SingleCampaignS2C = new SingleCampaignS2C();
			}
			SingleCampaignS2C.MergeFrom(other.SingleCampaignS2C);
			break;
		case MsgOneofCase.DevChargeC2S:
			if (DevChargeC2S == null)
			{
				DevChargeC2S = new DevChargeC2S();
			}
			DevChargeC2S.MergeFrom(other.DevChargeC2S);
			break;
		case MsgOneofCase.DevChargeS2C:
			if (DevChargeS2C == null)
			{
				DevChargeS2C = new DevChargeS2C();
			}
			DevChargeS2C.MergeFrom(other.DevChargeS2C);
			break;
		case MsgOneofCase.AskReviveTeammateC2S:
			if (AskReviveTeammateC2S == null)
			{
				AskReviveTeammateC2S = new AskReviveTeammateC2S();
			}
			AskReviveTeammateC2S.MergeFrom(other.AskReviveTeammateC2S);
			break;
		case MsgOneofCase.AskReviveTeammateS2C:
			if (AskReviveTeammateS2C == null)
			{
				AskReviveTeammateS2C = new AskReviveTeammateS2C();
			}
			AskReviveTeammateS2C.MergeFrom(other.AskReviveTeammateS2C);
			break;
		case MsgOneofCase.GetSignInRewardC2S:
			if (GetSignInRewardC2S == null)
			{
				GetSignInRewardC2S = new GetSignInRewardC2S();
			}
			GetSignInRewardC2S.MergeFrom(other.GetSignInRewardC2S);
			break;
		case MsgOneofCase.GetSignInRewardS2C:
			if (GetSignInRewardS2C == null)
			{
				GetSignInRewardS2C = new GetSignInRewardS2C();
			}
			GetSignInRewardS2C.MergeFrom(other.GetSignInRewardS2C);
			break;
		case MsgOneofCase.ChatMapMarkersC2S:
			if (ChatMapMarkersC2S == null)
			{
				ChatMapMarkersC2S = new ChatMapMarkersC2S();
			}
			ChatMapMarkersC2S.MergeFrom(other.ChatMapMarkersC2S);
			break;
		case MsgOneofCase.ChatMapMarkersS2C:
			if (ChatMapMarkersS2C == null)
			{
				ChatMapMarkersS2C = new ChatMapMarkersS2C();
			}
			ChatMapMarkersS2C.MergeFrom(other.ChatMapMarkersS2C);
			break;
		case MsgOneofCase.AbroadCreateOrderC2S:
			if (AbroadCreateOrderC2S == null)
			{
				AbroadCreateOrderC2S = new AbroadCreateOrderC2S();
			}
			AbroadCreateOrderC2S.MergeFrom(other.AbroadCreateOrderC2S);
			break;
		case MsgOneofCase.AbroadCreateOrderS2C:
			if (AbroadCreateOrderS2C == null)
			{
				AbroadCreateOrderS2C = new AbroadCreateOrderS2C();
			}
			AbroadCreateOrderS2C.MergeFrom(other.AbroadCreateOrderS2C);
			break;
		case MsgOneofCase.AgeVerifyC2S:
			if (AgeVerifyC2S == null)
			{
				AgeVerifyC2S = new AgeVerifyC2S();
			}
			AgeVerifyC2S.MergeFrom(other.AgeVerifyC2S);
			break;
		case MsgOneofCase.AgeVerifyS2C:
			if (AgeVerifyS2C == null)
			{
				AgeVerifyS2C = new AgeVerifyS2C();
			}
			AgeVerifyS2C.MergeFrom(other.AgeVerifyS2C);
			break;
		case MsgOneofCase.ChangeNameC2S:
			if (ChangeNameC2S == null)
			{
				ChangeNameC2S = new ChangeNameC2S();
			}
			ChangeNameC2S.MergeFrom(other.ChangeNameC2S);
			break;
		case MsgOneofCase.ChangeNameS2C:
			if (ChangeNameS2C == null)
			{
				ChangeNameS2C = new ChangeNameS2C();
			}
			ChangeNameS2C.MergeFrom(other.ChangeNameS2C);
			break;
		case MsgOneofCase.ClientClickConfirmTaskC2S:
			if (ClientClickConfirmTaskC2S == null)
			{
				ClientClickConfirmTaskC2S = new ClientClickConfirmTaskC2S();
			}
			ClientClickConfirmTaskC2S.MergeFrom(other.ClientClickConfirmTaskC2S);
			break;
		case MsgOneofCase.ClientClickConfirmTaskS2C:
			if (ClientClickConfirmTaskS2C == null)
			{
				ClientClickConfirmTaskS2C = new ClientClickConfirmTaskS2C();
			}
			ClientClickConfirmTaskS2C.MergeFrom(other.ClientClickConfirmTaskS2C);
			break;
		case MsgOneofCase.BuyRelicC2S:
			if (BuyRelicC2S == null)
			{
				BuyRelicC2S = new BuyRelicC2S();
			}
			BuyRelicC2S.MergeFrom(other.BuyRelicC2S);
			break;
		case MsgOneofCase.BuyRelicS2C:
			if (BuyRelicS2C == null)
			{
				BuyRelicS2C = new BuyRelicS2C();
			}
			BuyRelicS2C.MergeFrom(other.BuyRelicS2C);
			break;
		case MsgOneofCase.BuyLightGiftC2S:
			if (BuyLightGiftC2S == null)
			{
				BuyLightGiftC2S = new BuyLightGiftC2S();
			}
			BuyLightGiftC2S.MergeFrom(other.BuyLightGiftC2S);
			break;
		case MsgOneofCase.BuyLightGiftS2C:
			if (BuyLightGiftS2C == null)
			{
				BuyLightGiftS2C = new BuyLightGiftS2C();
			}
			BuyLightGiftS2C.MergeFrom(other.BuyLightGiftS2C);
			break;
		case MsgOneofCase.LightGiftC2S:
			if (LightGiftC2S == null)
			{
				LightGiftC2S = new LightGiftC2S();
			}
			LightGiftC2S.MergeFrom(other.LightGiftC2S);
			break;
		case MsgOneofCase.LightGiftS2C:
			if (LightGiftS2C == null)
			{
				LightGiftS2C = new LightGiftS2C();
			}
			LightGiftS2C.MergeFrom(other.LightGiftS2C);
			break;
		case MsgOneofCase.AcquisitionC2S:
			if (AcquisitionC2S == null)
			{
				AcquisitionC2S = new AcquisitionC2S();
			}
			AcquisitionC2S.MergeFrom(other.AcquisitionC2S);
			break;
		case MsgOneofCase.AcquisitionS2C:
			if (AcquisitionS2C == null)
			{
				AcquisitionS2C = new AcquisitionS2C();
			}
			AcquisitionS2C.MergeFrom(other.AcquisitionS2C);
			break;
		case MsgOneofCase.AcquisitionRewardC2S:
			if (AcquisitionRewardC2S == null)
			{
				AcquisitionRewardC2S = new AcquisitionRewardC2S();
			}
			AcquisitionRewardC2S.MergeFrom(other.AcquisitionRewardC2S);
			break;
		case MsgOneofCase.AcquisitionRewardS2C:
			if (AcquisitionRewardS2C == null)
			{
				AcquisitionRewardS2C = new AcquisitionRewardS2C();
			}
			AcquisitionRewardS2C.MergeFrom(other.AcquisitionRewardS2C);
			break;
		case MsgOneofCase.SelectMechanismC2S:
			if (SelectMechanismC2S == null)
			{
				SelectMechanismC2S = new SelectMechanismC2S();
			}
			SelectMechanismC2S.MergeFrom(other.SelectMechanismC2S);
			break;
		case MsgOneofCase.SelectMechanismS2C:
			if (SelectMechanismS2C == null)
			{
				SelectMechanismS2C = new SelectMechanismS2C();
			}
			SelectMechanismS2C.MergeFrom(other.SelectMechanismS2C);
			break;
		case MsgOneofCase.GachaCountRewardC2S:
			if (GachaCountRewardC2S == null)
			{
				GachaCountRewardC2S = new GachaCountRewardC2S();
			}
			GachaCountRewardC2S.MergeFrom(other.GachaCountRewardC2S);
			break;
		case MsgOneofCase.GachaCountRewardS2C:
			if (GachaCountRewardS2C == null)
			{
				GachaCountRewardS2C = new GachaCountRewardS2C();
			}
			GachaCountRewardS2C.MergeFrom(other.GachaCountRewardS2C);
			break;
		case MsgOneofCase.GetPlayerSimpleC2S:
			if (GetPlayerSimpleC2S == null)
			{
				GetPlayerSimpleC2S = new GetPlayerSimpleC2S();
			}
			GetPlayerSimpleC2S.MergeFrom(other.GetPlayerSimpleC2S);
			break;
		case MsgOneofCase.GetPlayerSimpleS2C:
			if (GetPlayerSimpleS2C == null)
			{
				GetPlayerSimpleS2C = new GetPlayerSimpleS2C();
			}
			GetPlayerSimpleS2C.MergeFrom(other.GetPlayerSimpleS2C);
			break;
		case MsgOneofCase.RoleCardCollectC2S:
			if (RoleCardCollectC2S == null)
			{
				RoleCardCollectC2S = new RoleCardCollectC2S();
			}
			RoleCardCollectC2S.MergeFrom(other.RoleCardCollectC2S);
			break;
		case MsgOneofCase.RoleCardCollectS2C:
			if (RoleCardCollectS2C == null)
			{
				RoleCardCollectS2C = new RoleCardCollectS2C();
			}
			RoleCardCollectS2C.MergeFrom(other.RoleCardCollectS2C);
			break;
		case MsgOneofCase.GMPlayerSettingC2S:
			if (GMPlayerSettingC2S == null)
			{
				GMPlayerSettingC2S = new GMPlayerSettingC2S();
			}
			GMPlayerSettingC2S.MergeFrom(other.GMPlayerSettingC2S);
			break;
		case MsgOneofCase.GMPlayerSettingS2C:
			if (GMPlayerSettingS2C == null)
			{
				GMPlayerSettingS2C = new GMPlayerSettingS2C();
			}
			GMPlayerSettingS2C.MergeFrom(other.GMPlayerSettingS2C);
			break;
		case MsgOneofCase.SetFriendNoteC2S:
			if (SetFriendNoteC2S == null)
			{
				SetFriendNoteC2S = new SetFriendNoteC2S();
			}
			SetFriendNoteC2S.MergeFrom(other.SetFriendNoteC2S);
			break;
		case MsgOneofCase.SetFriendNoteS2C:
			if (SetFriendNoteS2C == null)
			{
				SetFriendNoteS2C = new SetFriendNoteS2C();
			}
			SetFriendNoteS2C.MergeFrom(other.SetFriendNoteS2C);
			break;
		case MsgOneofCase.SetOnlineStatusC2S:
			if (SetOnlineStatusC2S == null)
			{
				SetOnlineStatusC2S = new SetOnlineStatusC2S();
			}
			SetOnlineStatusC2S.MergeFrom(other.SetOnlineStatusC2S);
			break;
		case MsgOneofCase.SetOnlineStatusS2C:
			if (SetOnlineStatusS2C == null)
			{
				SetOnlineStatusS2C = new SetOnlineStatusS2C();
			}
			SetOnlineStatusS2C.MergeFrom(other.SetOnlineStatusS2C);
			break;
		case MsgOneofCase.ChooseSkinC2S:
			if (ChooseSkinC2S == null)
			{
				ChooseSkinC2S = new ChooseSkinC2S();
			}
			ChooseSkinC2S.MergeFrom(other.ChooseSkinC2S);
			break;
		case MsgOneofCase.ChooseSkinS2C:
			if (ChooseSkinS2C == null)
			{
				ChooseSkinS2C = new ChooseSkinS2C();
			}
			ChooseSkinS2C.MergeFrom(other.ChooseSkinS2C);
			break;
		case MsgOneofCase.TimeWastingC2S:
			if (TimeWastingC2S == null)
			{
				TimeWastingC2S = new TimeWastingC2S();
			}
			TimeWastingC2S.MergeFrom(other.TimeWastingC2S);
			break;
		case MsgOneofCase.TimeWastingS2C:
			if (TimeWastingS2C == null)
			{
				TimeWastingS2C = new TimeWastingS2C();
			}
			TimeWastingS2C.MergeFrom(other.TimeWastingS2C);
			break;
		case MsgOneofCase.VoteC2S:
			if (VoteC2S == null)
			{
				VoteC2S = new VoteC2S();
			}
			VoteC2S.MergeFrom(other.VoteC2S);
			break;
		case MsgOneofCase.VoteS2C:
			if (VoteS2C == null)
			{
				VoteS2C = new VoteS2C();
			}
			VoteS2C.MergeFrom(other.VoteS2C);
			break;
		case MsgOneofCase.VoteSelectC2S:
			if (VoteSelectC2S == null)
			{
				VoteSelectC2S = new VoteSelectC2S();
			}
			VoteSelectC2S.MergeFrom(other.VoteSelectC2S);
			break;
		case MsgOneofCase.VoteSelectS2C:
			if (VoteSelectS2C == null)
			{
				VoteSelectS2C = new VoteSelectS2C();
			}
			VoteSelectS2C.MergeFrom(other.VoteSelectS2C);
			break;
		case MsgOneofCase.NotifyStoryC2S:
			if (NotifyStoryC2S == null)
			{
				NotifyStoryC2S = new NotifyStoryC2S();
			}
			NotifyStoryC2S.MergeFrom(other.NotifyStoryC2S);
			break;
		case MsgOneofCase.NotifyStoryS2C:
			if (NotifyStoryS2C == null)
			{
				NotifyStoryS2C = new NotifyStoryS2C();
			}
			NotifyStoryS2C.MergeFrom(other.NotifyStoryS2C);
			break;
		case MsgOneofCase.PveHeroTalentUpC2S:
			if (PveHeroTalentUpC2S == null)
			{
				PveHeroTalentUpC2S = new PveHeroTalentUpC2S();
			}
			PveHeroTalentUpC2S.MergeFrom(other.PveHeroTalentUpC2S);
			break;
		case MsgOneofCase.PveHeroTalentUpS2C:
			if (PveHeroTalentUpS2C == null)
			{
				PveHeroTalentUpS2C = new PveHeroTalentUpS2C();
			}
			PveHeroTalentUpS2C.MergeFrom(other.PveHeroTalentUpS2C);
			break;
		case MsgOneofCase.SelectEventC2S:
			if (SelectEventC2S == null)
			{
				SelectEventC2S = new SelectEventC2S();
			}
			SelectEventC2S.MergeFrom(other.SelectEventC2S);
			break;
		case MsgOneofCase.SelectEventS2C:
			if (SelectEventS2C == null)
			{
				SelectEventS2C = new SelectEventS2C();
			}
			SelectEventS2C.MergeFrom(other.SelectEventS2C);
			break;
		case MsgOneofCase.CampScoreC2S:
			if (CampScoreC2S == null)
			{
				CampScoreC2S = new CampScoreC2S();
			}
			CampScoreC2S.MergeFrom(other.CampScoreC2S);
			break;
		case MsgOneofCase.CampScoreS2C:
			if (CampScoreS2C == null)
			{
				CampScoreS2C = new CampScoreS2C();
			}
			CampScoreS2C.MergeFrom(other.CampScoreS2C);
			break;
		case MsgOneofCase.ActivityMissionRewardC2S:
			if (ActivityMissionRewardC2S == null)
			{
				ActivityMissionRewardC2S = new ActivityMissionRewardC2S();
			}
			ActivityMissionRewardC2S.MergeFrom(other.ActivityMissionRewardC2S);
			break;
		case MsgOneofCase.ActivityMissionRewardS2C:
			if (ActivityMissionRewardS2C == null)
			{
				ActivityMissionRewardS2C = new ActivityMissionRewardS2C();
			}
			ActivityMissionRewardS2C.MergeFrom(other.ActivityMissionRewardS2C);
			break;
		case MsgOneofCase.VendorBuyCardC2S:
			if (VendorBuyCardC2S == null)
			{
				VendorBuyCardC2S = new VendorBuyCardC2S();
			}
			VendorBuyCardC2S.MergeFrom(other.VendorBuyCardC2S);
			break;
		case MsgOneofCase.VendorBuyCardS2C:
			if (VendorBuyCardS2C == null)
			{
				VendorBuyCardS2C = new VendorBuyCardS2C();
			}
			VendorBuyCardS2C.MergeFrom(other.VendorBuyCardS2C);
			break;
		case MsgOneofCase.TransferStarDiscC2S:
			if (TransferStarDiscC2S == null)
			{
				TransferStarDiscC2S = new TransferStarDiscC2S();
			}
			TransferStarDiscC2S.MergeFrom(other.TransferStarDiscC2S);
			break;
		case MsgOneofCase.TransferStarDiscS2C:
			if (TransferStarDiscS2C == null)
			{
				TransferStarDiscS2C = new TransferStarDiscS2C();
			}
			TransferStarDiscS2C.MergeFrom(other.TransferStarDiscS2C);
			break;
		case MsgOneofCase.GetHeroInfoC2S:
			if (GetHeroInfoC2S == null)
			{
				GetHeroInfoC2S = new GetHeroInfoC2S();
			}
			GetHeroInfoC2S.MergeFrom(other.GetHeroInfoC2S);
			break;
		case MsgOneofCase.GetHeroInfoS2C:
			if (GetHeroInfoS2C == null)
			{
				GetHeroInfoS2C = new GetHeroInfoS2C();
			}
			GetHeroInfoS2C.MergeFrom(other.GetHeroInfoS2C);
			break;
		case MsgOneofCase.CreateMatchTeamC2S:
			if (CreateMatchTeamC2S == null)
			{
				CreateMatchTeamC2S = new CreateMatchTeamC2S();
			}
			CreateMatchTeamC2S.MergeFrom(other.CreateMatchTeamC2S);
			break;
		case MsgOneofCase.CreateMatchTeamS2C:
			if (CreateMatchTeamS2C == null)
			{
				CreateMatchTeamS2C = new CreateMatchTeamS2C();
			}
			CreateMatchTeamS2C.MergeFrom(other.CreateMatchTeamS2C);
			break;
		case MsgOneofCase.ChangeMatchTeamC2S:
			if (ChangeMatchTeamC2S == null)
			{
				ChangeMatchTeamC2S = new ChangeMatchTeamC2S();
			}
			ChangeMatchTeamC2S.MergeFrom(other.ChangeMatchTeamC2S);
			break;
		case MsgOneofCase.ChangeMatchTeamS2C:
			if (ChangeMatchTeamS2C == null)
			{
				ChangeMatchTeamS2C = new ChangeMatchTeamS2C();
			}
			ChangeMatchTeamS2C.MergeFrom(other.ChangeMatchTeamS2C);
			break;
		case MsgOneofCase.JoinMatchTeamC2S:
			if (JoinMatchTeamC2S == null)
			{
				JoinMatchTeamC2S = new JoinMatchTeamC2S();
			}
			JoinMatchTeamC2S.MergeFrom(other.JoinMatchTeamC2S);
			break;
		case MsgOneofCase.JoinMatchTeamS2C:
			if (JoinMatchTeamS2C == null)
			{
				JoinMatchTeamS2C = new JoinMatchTeamS2C();
			}
			JoinMatchTeamS2C.MergeFrom(other.JoinMatchTeamS2C);
			break;
		case MsgOneofCase.ExitMatchTeamC2S:
			if (ExitMatchTeamC2S == null)
			{
				ExitMatchTeamC2S = new ExitMatchTeamC2S();
			}
			ExitMatchTeamC2S.MergeFrom(other.ExitMatchTeamC2S);
			break;
		case MsgOneofCase.ExitMatchTeamS2C:
			if (ExitMatchTeamS2C == null)
			{
				ExitMatchTeamS2C = new ExitMatchTeamS2C();
			}
			ExitMatchTeamS2C.MergeFrom(other.ExitMatchTeamS2C);
			break;
		case MsgOneofCase.RefreshMatchTeamInfoC2S:
			if (RefreshMatchTeamInfoC2S == null)
			{
				RefreshMatchTeamInfoC2S = new RefreshMatchTeamInfoC2S();
			}
			RefreshMatchTeamInfoC2S.MergeFrom(other.RefreshMatchTeamInfoC2S);
			break;
		case MsgOneofCase.RefreshMatchTeamInfoS2C:
			if (RefreshMatchTeamInfoS2C == null)
			{
				RefreshMatchTeamInfoS2C = new RefreshMatchTeamInfoS2C();
			}
			RefreshMatchTeamInfoS2C.MergeFrom(other.RefreshMatchTeamInfoS2C);
			break;
		case MsgOneofCase.ChinaCreateOrderC2S:
			if (ChinaCreateOrderC2S == null)
			{
				ChinaCreateOrderC2S = new ChinaCreateOrderC2S();
			}
			ChinaCreateOrderC2S.MergeFrom(other.ChinaCreateOrderC2S);
			break;
		case MsgOneofCase.ChinaCreateOrderS2C:
			if (ChinaCreateOrderS2C == null)
			{
				ChinaCreateOrderS2C = new ChinaCreateOrderS2C();
			}
			ChinaCreateOrderS2C.MergeFrom(other.ChinaCreateOrderS2C);
			break;
		case MsgOneofCase.MatchTeamInviteC2S:
			if (MatchTeamInviteC2S == null)
			{
				MatchTeamInviteC2S = new MatchTeamInviteC2S();
			}
			MatchTeamInviteC2S.MergeFrom(other.MatchTeamInviteC2S);
			break;
		case MsgOneofCase.MatchTeamInviteS2C:
			if (MatchTeamInviteS2C == null)
			{
				MatchTeamInviteS2C = new MatchTeamInviteS2C();
			}
			MatchTeamInviteS2C.MergeFrom(other.MatchTeamInviteS2C);
			break;
		case MsgOneofCase.MatchTeamChatC2S:
			if (MatchTeamChatC2S == null)
			{
				MatchTeamChatC2S = new MatchTeamChatC2S();
			}
			MatchTeamChatC2S.MergeFrom(other.MatchTeamChatC2S);
			break;
		case MsgOneofCase.MatchTeamChatS2C:
			if (MatchTeamChatS2C == null)
			{
				MatchTeamChatS2C = new MatchTeamChatS2C();
			}
			MatchTeamChatS2C.MergeFrom(other.MatchTeamChatS2C);
			break;
		case MsgOneofCase.MatchTeamReadyC2S:
			if (MatchTeamReadyC2S == null)
			{
				MatchTeamReadyC2S = new MatchTeamReadyC2S();
			}
			MatchTeamReadyC2S.MergeFrom(other.MatchTeamReadyC2S);
			break;
		case MsgOneofCase.MatchTeamReadyS2C:
			if (MatchTeamReadyS2C == null)
			{
				MatchTeamReadyS2C = new MatchTeamReadyS2C();
			}
			MatchTeamReadyS2C.MergeFrom(other.MatchTeamReadyS2C);
			break;
		case MsgOneofCase.PlayerChatC2S:
			if (PlayerChatC2S == null)
			{
				PlayerChatC2S = new PlayerChatC2S();
			}
			PlayerChatC2S.MergeFrom(other.PlayerChatC2S);
			break;
		case MsgOneofCase.PlayerChatS2C:
			if (PlayerChatS2C == null)
			{
				PlayerChatS2C = new PlayerChatS2C();
			}
			PlayerChatS2C.MergeFrom(other.PlayerChatS2C);
			break;
		case MsgOneofCase.SyncSingleGameDataC2S:
			if (SyncSingleGameDataC2S == null)
			{
				SyncSingleGameDataC2S = new SyncSingleGameDataC2S();
			}
			SyncSingleGameDataC2S.MergeFrom(other.SyncSingleGameDataC2S);
			break;
		case MsgOneofCase.SyncSingleGameDataS2C:
			if (SyncSingleGameDataS2C == null)
			{
				SyncSingleGameDataS2C = new SyncSingleGameDataS2C();
			}
			SyncSingleGameDataS2C.MergeFrom(other.SyncSingleGameDataS2C);
			break;
		case MsgOneofCase.SingleGameDataC2S:
			if (SingleGameDataC2S == null)
			{
				SingleGameDataC2S = new SingleGameDataC2S();
			}
			SingleGameDataC2S.MergeFrom(other.SingleGameDataC2S);
			break;
		case MsgOneofCase.SingleGameDataS2C:
			if (SingleGameDataS2C == null)
			{
				SingleGameDataS2C = new SingleGameDataS2C();
			}
			SingleGameDataS2C.MergeFrom(other.SingleGameDataS2C);
			break;
		case MsgOneofCase.GetActivityPassRewardC2S:
			if (GetActivityPassRewardC2S == null)
			{
				GetActivityPassRewardC2S = new GetActivityPassRewardC2S();
			}
			GetActivityPassRewardC2S.MergeFrom(other.GetActivityPassRewardC2S);
			break;
		case MsgOneofCase.GetActivityPassRewardS2C:
			if (GetActivityPassRewardS2C == null)
			{
				GetActivityPassRewardS2C = new GetActivityPassRewardS2C();
			}
			GetActivityPassRewardS2C.MergeFrom(other.GetActivityPassRewardS2C);
			break;
		case MsgOneofCase.ApplyChangeSlotC2S:
			if (ApplyChangeSlotC2S == null)
			{
				ApplyChangeSlotC2S = new ApplyChangeSlotC2S();
			}
			ApplyChangeSlotC2S.MergeFrom(other.ApplyChangeSlotC2S);
			break;
		case MsgOneofCase.ApplyChangeSlotS2C:
			if (ApplyChangeSlotS2C == null)
			{
				ApplyChangeSlotS2C = new ApplyChangeSlotS2C();
			}
			ApplyChangeSlotS2C.MergeFrom(other.ApplyChangeSlotS2C);
			break;
		case MsgOneofCase.OpsChangeSlotC2S:
			if (OpsChangeSlotC2S == null)
			{
				OpsChangeSlotC2S = new OpsChangeSlotC2S();
			}
			OpsChangeSlotC2S.MergeFrom(other.OpsChangeSlotC2S);
			break;
		case MsgOneofCase.OpsChangeSlotS2C:
			if (OpsChangeSlotS2C == null)
			{
				OpsChangeSlotS2C = new OpsChangeSlotS2C();
			}
			OpsChangeSlotS2C.MergeFrom(other.OpsChangeSlotS2C);
			break;
		case MsgOneofCase.RookieGachaRewardC2S:
			if (RookieGachaRewardC2S == null)
			{
				RookieGachaRewardC2S = new RookieGachaRewardC2S();
			}
			RookieGachaRewardC2S.MergeFrom(other.RookieGachaRewardC2S);
			break;
		case MsgOneofCase.RookieGachaRewardS2C:
			if (RookieGachaRewardS2C == null)
			{
				RookieGachaRewardS2C = new RookieGachaRewardS2C();
			}
			RookieGachaRewardS2C.MergeFrom(other.RookieGachaRewardS2C);
			break;
		case MsgOneofCase.LiveGiftPackageC2S:
			if (LiveGiftPackageC2S == null)
			{
				LiveGiftPackageC2S = new LiveGiftPackageC2S();
			}
			LiveGiftPackageC2S.MergeFrom(other.LiveGiftPackageC2S);
			break;
		case MsgOneofCase.LiveGiftPackageS2C:
			if (LiveGiftPackageS2C == null)
			{
				LiveGiftPackageS2C = new LiveGiftPackageS2C();
			}
			LiveGiftPackageS2C.MergeFrom(other.LiveGiftPackageS2C);
			break;
		case MsgOneofCase.LaborActDiceC2S:
			if (LaborActDiceC2S == null)
			{
				LaborActDiceC2S = new LaborActDiceC2S();
			}
			LaborActDiceC2S.MergeFrom(other.LaborActDiceC2S);
			break;
		case MsgOneofCase.LaborActDiceS2C:
			if (LaborActDiceS2C == null)
			{
				LaborActDiceS2C = new LaborActDiceS2C();
			}
			LaborActDiceS2C.MergeFrom(other.LaborActDiceS2C);
			break;
		case MsgOneofCase.ActionOverTimeLogC2S:
			if (ActionOverTimeLogC2S == null)
			{
				ActionOverTimeLogC2S = new ActionOverTimeLogC2S();
			}
			ActionOverTimeLogC2S.MergeFrom(other.ActionOverTimeLogC2S);
			break;
		case MsgOneofCase.ActionOverTimeLogS2C:
			if (ActionOverTimeLogS2C == null)
			{
				ActionOverTimeLogS2C = new ActionOverTimeLogS2C();
			}
			ActionOverTimeLogS2C.MergeFrom(other.ActionOverTimeLogS2C);
			break;
		case MsgOneofCase.ClientHarmonyC2S:
			if (ClientHarmonyC2S == null)
			{
				ClientHarmonyC2S = new ClientHarmonyC2S();
			}
			ClientHarmonyC2S.MergeFrom(other.ClientHarmonyC2S);
			break;
		case MsgOneofCase.ClientHarmonyS2C:
			if (ClientHarmonyS2C == null)
			{
				ClientHarmonyS2C = new ClientHarmonyS2C();
			}
			ClientHarmonyS2C.MergeFrom(other.ClientHarmonyS2C);
			break;
		case MsgOneofCase.MailStarC2S:
			if (MailStarC2S == null)
			{
				MailStarC2S = new MailStarC2S();
			}
			MailStarC2S.MergeFrom(other.MailStarC2S);
			break;
		case MsgOneofCase.MailStarS2C:
			if (MailStarS2C == null)
			{
				MailStarS2C = new MailStarS2C();
			}
			MailStarS2C.MergeFrom(other.MailStarS2C);
			break;
		case MsgOneofCase.SetCardAltArtC2S:
			if (SetCardAltArtC2S == null)
			{
				SetCardAltArtC2S = new SetCardAltArtC2S();
			}
			SetCardAltArtC2S.MergeFrom(other.SetCardAltArtC2S);
			break;
		case MsgOneofCase.SetCardAltArtS2C:
			if (SetCardAltArtS2C == null)
			{
				SetCardAltArtS2C = new SetCardAltArtS2C();
			}
			SetCardAltArtS2C.MergeFrom(other.SetCardAltArtS2C);
			break;
		case MsgOneofCase.SelectRewardCardC2S:
			if (SelectRewardCardC2S == null)
			{
				SelectRewardCardC2S = new SelectRewardCardC2S();
			}
			SelectRewardCardC2S.MergeFrom(other.SelectRewardCardC2S);
			break;
		case MsgOneofCase.SelectRewardCardS2C:
			if (SelectRewardCardS2C == null)
			{
				SelectRewardCardS2C = new SelectRewardCardS2C();
			}
			SelectRewardCardS2C.MergeFrom(other.SelectRewardCardS2C);
			break;
		case MsgOneofCase.GetQuestionUrlC2S:
			if (GetQuestionUrlC2S == null)
			{
				GetQuestionUrlC2S = new GetQuestionUrlC2S();
			}
			GetQuestionUrlC2S.MergeFrom(other.GetQuestionUrlC2S);
			break;
		case MsgOneofCase.GetQuestionUrlS2C:
			if (GetQuestionUrlS2C == null)
			{
				GetQuestionUrlS2C = new GetQuestionUrlS2C();
			}
			GetQuestionUrlS2C.MergeFrom(other.GetQuestionUrlS2C);
			break;
		case MsgOneofCase.GetReturnInfoC2S:
			if (GetReturnInfoC2S == null)
			{
				GetReturnInfoC2S = new GetReturnInfoC2S();
			}
			GetReturnInfoC2S.MergeFrom(other.GetReturnInfoC2S);
			break;
		case MsgOneofCase.GetReturnInfoS2C:
			if (GetReturnInfoS2C == null)
			{
				GetReturnInfoS2C = new GetReturnInfoS2C();
			}
			GetReturnInfoS2C.MergeFrom(other.GetReturnInfoS2C);
			break;
		case MsgOneofCase.ReturnGiftClaimC2S:
			if (ReturnGiftClaimC2S == null)
			{
				ReturnGiftClaimC2S = new ReturnGiftClaimC2S();
			}
			ReturnGiftClaimC2S.MergeFrom(other.ReturnGiftClaimC2S);
			break;
		case MsgOneofCase.ReturnGiftClaimS2C:
			if (ReturnGiftClaimS2C == null)
			{
				ReturnGiftClaimS2C = new ReturnGiftClaimS2C();
			}
			ReturnGiftClaimS2C.MergeFrom(other.ReturnGiftClaimS2C);
			break;
		case MsgOneofCase.ReturnSignInClaimC2S:
			if (ReturnSignInClaimC2S == null)
			{
				ReturnSignInClaimC2S = new ReturnSignInClaimC2S();
			}
			ReturnSignInClaimC2S.MergeFrom(other.ReturnSignInClaimC2S);
			break;
		case MsgOneofCase.ReturnSignInClaimS2C:
			if (ReturnSignInClaimS2C == null)
			{
				ReturnSignInClaimS2C = new ReturnSignInClaimS2C();
			}
			ReturnSignInClaimS2C.MergeFrom(other.ReturnSignInClaimS2C);
			break;
		case MsgOneofCase.ReturnSurveyFinishC2S:
			if (ReturnSurveyFinishC2S == null)
			{
				ReturnSurveyFinishC2S = new ReturnSurveyFinishC2S();
			}
			ReturnSurveyFinishC2S.MergeFrom(other.ReturnSurveyFinishC2S);
			break;
		case MsgOneofCase.ReturnSurveyFinishS2C:
			if (ReturnSurveyFinishS2C == null)
			{
				ReturnSurveyFinishS2C = new ReturnSurveyFinishS2C();
			}
			ReturnSurveyFinishS2C.MergeFrom(other.ReturnSurveyFinishS2C);
			break;
		case MsgOneofCase.FlipCardC2S:
			if (FlipCardC2S == null)
			{
				FlipCardC2S = new FlipCardC2S();
			}
			FlipCardC2S.MergeFrom(other.FlipCardC2S);
			break;
		case MsgOneofCase.FlipCardS2C:
			if (FlipCardS2C == null)
			{
				FlipCardS2C = new FlipCardS2C();
			}
			FlipCardS2C.MergeFrom(other.FlipCardS2C);
			break;
		case MsgOneofCase.FlipCardProgressRewardC2S:
			if (FlipCardProgressRewardC2S == null)
			{
				FlipCardProgressRewardC2S = new FlipCardProgressRewardC2S();
			}
			FlipCardProgressRewardC2S.MergeFrom(other.FlipCardProgressRewardC2S);
			break;
		case MsgOneofCase.FlipCardProgressRewardS2C:
			if (FlipCardProgressRewardS2C == null)
			{
				FlipCardProgressRewardS2C = new FlipCardProgressRewardS2C();
			}
			FlipCardProgressRewardS2C.MergeFrom(other.FlipCardProgressRewardS2C);
			break;
		case MsgOneofCase.SyncPlayerGuildS2C:
			if (SyncPlayerGuildS2C == null)
			{
				SyncPlayerGuildS2C = new SyncPlayerGuildS2C();
			}
			SyncPlayerGuildS2C.MergeFrom(other.SyncPlayerGuildS2C);
			break;
		case MsgOneofCase.SyncPlayerJoinGuildS2C:
			if (SyncPlayerJoinGuildS2C == null)
			{
				SyncPlayerJoinGuildS2C = new SyncPlayerJoinGuildS2C();
			}
			SyncPlayerJoinGuildS2C.MergeFrom(other.SyncPlayerJoinGuildS2C);
			break;
		case MsgOneofCase.GuildChatMsgS2C:
			if (GuildChatMsgS2C == null)
			{
				GuildChatMsgS2C = new GuildChatMsgS2C();
			}
			GuildChatMsgS2C.MergeFrom(other.GuildChatMsgS2C);
			break;
		case MsgOneofCase.SyncGuildS2C:
			if (SyncGuildS2C == null)
			{
				SyncGuildS2C = new SyncGuildS2C();
			}
			SyncGuildS2C.MergeFrom(other.SyncGuildS2C);
			break;
		case MsgOneofCase.SyncGuildMemberS2C:
			if (SyncGuildMemberS2C == null)
			{
				SyncGuildMemberS2C = new SyncGuildMemberS2C();
			}
			SyncGuildMemberS2C.MergeFrom(other.SyncGuildMemberS2C);
			break;
		case MsgOneofCase.SyncGuildMemberExitS2C:
			if (SyncGuildMemberExitS2C == null)
			{
				SyncGuildMemberExitS2C = new SyncGuildMemberExitS2C();
			}
			SyncGuildMemberExitS2C.MergeFrom(other.SyncGuildMemberExitS2C);
			break;
		case MsgOneofCase.CreateGuildC2S:
			if (CreateGuildC2S == null)
			{
				CreateGuildC2S = new CreateGuildC2S();
			}
			CreateGuildC2S.MergeFrom(other.CreateGuildC2S);
			break;
		case MsgOneofCase.CreateGuildS2C:
			if (CreateGuildS2C == null)
			{
				CreateGuildS2C = new CreateGuildS2C();
			}
			CreateGuildS2C.MergeFrom(other.CreateGuildS2C);
			break;
		case MsgOneofCase.SearchGuildC2S:
			if (SearchGuildC2S == null)
			{
				SearchGuildC2S = new SearchGuildC2S();
			}
			SearchGuildC2S.MergeFrom(other.SearchGuildC2S);
			break;
		case MsgOneofCase.SearchGuildS2C:
			if (SearchGuildS2C == null)
			{
				SearchGuildS2C = new SearchGuildS2C();
			}
			SearchGuildS2C.MergeFrom(other.SearchGuildS2C);
			break;
		case MsgOneofCase.ApplyToGuildC2S:
			if (ApplyToGuildC2S == null)
			{
				ApplyToGuildC2S = new ApplyToGuildC2S();
			}
			ApplyToGuildC2S.MergeFrom(other.ApplyToGuildC2S);
			break;
		case MsgOneofCase.ApplyToGuildS2C:
			if (ApplyToGuildS2C == null)
			{
				ApplyToGuildS2C = new ApplyToGuildS2C();
			}
			ApplyToGuildS2C.MergeFrom(other.ApplyToGuildS2C);
			break;
		case MsgOneofCase.ProcessGuildApplicationC2S:
			if (ProcessGuildApplicationC2S == null)
			{
				ProcessGuildApplicationC2S = new ProcessGuildApplicationC2S();
			}
			ProcessGuildApplicationC2S.MergeFrom(other.ProcessGuildApplicationC2S);
			break;
		case MsgOneofCase.ProcessGuildApplicationS2C:
			if (ProcessGuildApplicationS2C == null)
			{
				ProcessGuildApplicationS2C = new ProcessGuildApplicationS2C();
			}
			ProcessGuildApplicationS2C.MergeFrom(other.ProcessGuildApplicationS2C);
			break;
		case MsgOneofCase.SendGuildInvitationC2S:
			if (SendGuildInvitationC2S == null)
			{
				SendGuildInvitationC2S = new SendGuildInvitationC2S();
			}
			SendGuildInvitationC2S.MergeFrom(other.SendGuildInvitationC2S);
			break;
		case MsgOneofCase.SendGuildInvitationS2C:
			if (SendGuildInvitationS2C == null)
			{
				SendGuildInvitationS2C = new SendGuildInvitationS2C();
			}
			SendGuildInvitationS2C.MergeFrom(other.SendGuildInvitationS2C);
			break;
		case MsgOneofCase.ProcessGuildInvitationC2S:
			if (ProcessGuildInvitationC2S == null)
			{
				ProcessGuildInvitationC2S = new ProcessGuildInvitationC2S();
			}
			ProcessGuildInvitationC2S.MergeFrom(other.ProcessGuildInvitationC2S);
			break;
		case MsgOneofCase.ProcessGuildInvitationS2C:
			if (ProcessGuildInvitationS2C == null)
			{
				ProcessGuildInvitationS2C = new ProcessGuildInvitationS2C();
			}
			ProcessGuildInvitationS2C.MergeFrom(other.ProcessGuildInvitationS2C);
			break;
		case MsgOneofCase.GetGuildInfoC2S:
			if (GetGuildInfoC2S == null)
			{
				GetGuildInfoC2S = new GetGuildInfoC2S();
			}
			GetGuildInfoC2S.MergeFrom(other.GetGuildInfoC2S);
			break;
		case MsgOneofCase.GetGuildInfoS2C:
			if (GetGuildInfoS2C == null)
			{
				GetGuildInfoS2C = new GetGuildInfoS2C();
			}
			GetGuildInfoS2C.MergeFrom(other.GetGuildInfoS2C);
			break;
		case MsgOneofCase.UpdateGuildSettingsC2S:
			if (UpdateGuildSettingsC2S == null)
			{
				UpdateGuildSettingsC2S = new UpdateGuildSettingsC2S();
			}
			UpdateGuildSettingsC2S.MergeFrom(other.UpdateGuildSettingsC2S);
			break;
		case MsgOneofCase.UpdateGuildSettingsS2C:
			if (UpdateGuildSettingsS2C == null)
			{
				UpdateGuildSettingsS2C = new UpdateGuildSettingsS2C();
			}
			UpdateGuildSettingsS2C.MergeFrom(other.UpdateGuildSettingsS2C);
			break;
		case MsgOneofCase.UpdateGuildInAnnouncementC2S:
			if (UpdateGuildInAnnouncementC2S == null)
			{
				UpdateGuildInAnnouncementC2S = new UpdateGuildInAnnouncementC2S();
			}
			UpdateGuildInAnnouncementC2S.MergeFrom(other.UpdateGuildInAnnouncementC2S);
			break;
		case MsgOneofCase.UpdateGuildInAnnouncementS2C:
			if (UpdateGuildInAnnouncementS2C == null)
			{
				UpdateGuildInAnnouncementS2C = new UpdateGuildInAnnouncementS2C();
			}
			UpdateGuildInAnnouncementS2C.MergeFrom(other.UpdateGuildInAnnouncementS2C);
			break;
		case MsgOneofCase.TransferGuildMasterC2S:
			if (TransferGuildMasterC2S == null)
			{
				TransferGuildMasterC2S = new TransferGuildMasterC2S();
			}
			TransferGuildMasterC2S.MergeFrom(other.TransferGuildMasterC2S);
			break;
		case MsgOneofCase.TransferGuildMasterS2C:
			if (TransferGuildMasterS2C == null)
			{
				TransferGuildMasterS2C = new TransferGuildMasterS2C();
			}
			TransferGuildMasterS2C.MergeFrom(other.TransferGuildMasterS2C);
			break;
		case MsgOneofCase.ChangeGuildMemberTitleC2S:
			if (ChangeGuildMemberTitleC2S == null)
			{
				ChangeGuildMemberTitleC2S = new ChangeGuildMemberTitleC2S();
			}
			ChangeGuildMemberTitleC2S.MergeFrom(other.ChangeGuildMemberTitleC2S);
			break;
		case MsgOneofCase.ChangeGuildMemberTitleS2C:
			if (ChangeGuildMemberTitleS2C == null)
			{
				ChangeGuildMemberTitleS2C = new ChangeGuildMemberTitleS2C();
			}
			ChangeGuildMemberTitleS2C.MergeFrom(other.ChangeGuildMemberTitleS2C);
			break;
		case MsgOneofCase.KickGuildMemberC2S:
			if (KickGuildMemberC2S == null)
			{
				KickGuildMemberC2S = new KickGuildMemberC2S();
			}
			KickGuildMemberC2S.MergeFrom(other.KickGuildMemberC2S);
			break;
		case MsgOneofCase.KickGuildMemberS2C:
			if (KickGuildMemberS2C == null)
			{
				KickGuildMemberS2C = new KickGuildMemberS2C();
			}
			KickGuildMemberS2C.MergeFrom(other.KickGuildMemberS2C);
			break;
		case MsgOneofCase.ImpeachGuildMasterC2S:
			if (ImpeachGuildMasterC2S == null)
			{
				ImpeachGuildMasterC2S = new ImpeachGuildMasterC2S();
			}
			ImpeachGuildMasterC2S.MergeFrom(other.ImpeachGuildMasterC2S);
			break;
		case MsgOneofCase.ImpeachGuildMasterS2C:
			if (ImpeachGuildMasterS2C == null)
			{
				ImpeachGuildMasterS2C = new ImpeachGuildMasterS2C();
			}
			ImpeachGuildMasterS2C.MergeFrom(other.ImpeachGuildMasterS2C);
			break;
		case MsgOneofCase.ExitGuildC2S:
			if (ExitGuildC2S == null)
			{
				ExitGuildC2S = new ExitGuildC2S();
			}
			ExitGuildC2S.MergeFrom(other.ExitGuildC2S);
			break;
		case MsgOneofCase.ExitGuildS2C:
			if (ExitGuildS2C == null)
			{
				ExitGuildS2C = new ExitGuildS2C();
			}
			ExitGuildS2C.MergeFrom(other.ExitGuildS2C);
			break;
		case MsgOneofCase.DisbandGuildC2S:
			if (DisbandGuildC2S == null)
			{
				DisbandGuildC2S = new DisbandGuildC2S();
			}
			DisbandGuildC2S.MergeFrom(other.DisbandGuildC2S);
			break;
		case MsgOneofCase.DisbandGuildS2C:
			if (DisbandGuildS2C == null)
			{
				DisbandGuildS2C = new DisbandGuildS2C();
			}
			DisbandGuildS2C.MergeFrom(other.DisbandGuildS2C);
			break;
		case MsgOneofCase.GuildMissionRewardC2S:
			if (GuildMissionRewardC2S == null)
			{
				GuildMissionRewardC2S = new GuildMissionRewardC2S();
			}
			GuildMissionRewardC2S.MergeFrom(other.GuildMissionRewardC2S);
			break;
		case MsgOneofCase.GuildMissionRewardS2C:
			if (GuildMissionRewardS2C == null)
			{
				GuildMissionRewardS2C = new GuildMissionRewardS2C();
			}
			GuildMissionRewardS2C.MergeFrom(other.GuildMissionRewardS2C);
			break;
		case MsgOneofCase.GetGuildMemberChangeMsgC2S:
			if (GetGuildMemberChangeMsgC2S == null)
			{
				GetGuildMemberChangeMsgC2S = new GetGuildMemberChangeMsgC2S();
			}
			GetGuildMemberChangeMsgC2S.MergeFrom(other.GetGuildMemberChangeMsgC2S);
			break;
		case MsgOneofCase.GetGuildMemberChangeMsgS2C:
			if (GetGuildMemberChangeMsgS2C == null)
			{
				GetGuildMemberChangeMsgS2C = new GetGuildMemberChangeMsgS2C();
			}
			GetGuildMemberChangeMsgS2C.MergeFrom(other.GetGuildMemberChangeMsgS2C);
			break;
		case MsgOneofCase.SendGuildChatMsgC2S:
			if (SendGuildChatMsgC2S == null)
			{
				SendGuildChatMsgC2S = new SendGuildChatMsgC2S();
			}
			SendGuildChatMsgC2S.MergeFrom(other.SendGuildChatMsgC2S);
			break;
		case MsgOneofCase.SendGuildChatMsgS2C:
			if (SendGuildChatMsgS2C == null)
			{
				SendGuildChatMsgS2C = new SendGuildChatMsgS2C();
			}
			SendGuildChatMsgS2C.MergeFrom(other.SendGuildChatMsgS2C);
			break;
		case MsgOneofCase.GetGuildChatMsgC2S:
			if (GetGuildChatMsgC2S == null)
			{
				GetGuildChatMsgC2S = new GetGuildChatMsgC2S();
			}
			GetGuildChatMsgC2S.MergeFrom(other.GetGuildChatMsgC2S);
			break;
		case MsgOneofCase.GetGuildChatMsgS2C:
			if (GetGuildChatMsgS2C == null)
			{
				GetGuildChatMsgS2C = new GetGuildChatMsgS2C();
			}
			GetGuildChatMsgS2C.MergeFrom(other.GetGuildChatMsgS2C);
			break;
		case MsgOneofCase.GuildMemberC2S:
			if (GuildMemberC2S == null)
			{
				GuildMemberC2S = new GuildMemberC2S();
			}
			GuildMemberC2S.MergeFrom(other.GuildMemberC2S);
			break;
		case MsgOneofCase.GuildMemberS2C:
			if (GuildMemberS2C == null)
			{
				GuildMemberS2C = new GuildMemberS2C();
			}
			GuildMemberS2C.MergeFrom(other.GuildMemberS2C);
			break;
		case MsgOneofCase.GetGuildsInfoC2S:
			if (GetGuildsInfoC2S == null)
			{
				GetGuildsInfoC2S = new GetGuildsInfoC2S();
			}
			GetGuildsInfoC2S.MergeFrom(other.GetGuildsInfoC2S);
			break;
		case MsgOneofCase.GetGuildsInfoS2C:
			if (GetGuildsInfoS2C == null)
			{
				GetGuildsInfoS2C = new GetGuildsInfoS2C();
			}
			GetGuildsInfoS2C.MergeFrom(other.GetGuildsInfoS2C);
			break;
		case MsgOneofCase.TestRpcEchoC2S:
			if (TestRpcEchoC2S == null)
			{
				TestRpcEchoC2S = new TestRpcEchoC2S();
			}
			TestRpcEchoC2S.MergeFrom(other.TestRpcEchoC2S);
			break;
		case MsgOneofCase.TestRpcEchoS2C:
			if (TestRpcEchoS2C == null)
			{
				TestRpcEchoS2C = new TestRpcEchoS2C();
			}
			TestRpcEchoS2C.MergeFrom(other.TestRpcEchoS2C);
			break;
		}
		_unknownFields = UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	public void MergeFrom(CodedInputStream input)
	{
		input.ReadRawMessage(this);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("protoc", null)]
	void IBufferMessage.InternalMergeFrom(ref ParseContext input)
	{
		uint num;
		while ((num = input.ReadTag()) != 0)
		{
			switch (num)
			{
			default:
				_unknownFields = UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
				break;
			case 810u:
			{
				SysSendMailC2S sysSendMailC2S = new SysSendMailC2S();
				if (msgCase_ == MsgOneofCase.SysSendMailC2S)
				{
					sysSendMailC2S.MergeFrom(SysSendMailC2S);
				}
				input.ReadMessage(sysSendMailC2S);
				SysSendMailC2S = sysSendMailC2S;
				break;
			}
			case 818u:
			{
				SysPlayerOnlineC2S sysPlayerOnlineC2S = new SysPlayerOnlineC2S();
				if (msgCase_ == MsgOneofCase.SysPlayerOnlineC2S)
				{
					sysPlayerOnlineC2S.MergeFrom(SysPlayerOnlineC2S);
				}
				input.ReadMessage(sysPlayerOnlineC2S);
				SysPlayerOnlineC2S = sysPlayerOnlineC2S;
				break;
			}
			case 826u:
			{
				PlayerOnlineRoomC2S playerOnlineRoomC2S = new PlayerOnlineRoomC2S();
				if (msgCase_ == MsgOneofCase.PlayerOnlineRoomC2S)
				{
					playerOnlineRoomC2S.MergeFrom(PlayerOnlineRoomC2S);
				}
				input.ReadMessage(playerOnlineRoomC2S);
				PlayerOnlineRoomC2S = playerOnlineRoomC2S;
				break;
			}
			case 834u:
			{
				SysCanPraiseInfoC2S sysCanPraiseInfoC2S = new SysCanPraiseInfoC2S();
				if (msgCase_ == MsgOneofCase.SysCanPraiseInfoC2S)
				{
					sysCanPraiseInfoC2S.MergeFrom(SysCanPraiseInfoC2S);
				}
				input.ReadMessage(sysCanPraiseInfoC2S);
				SysCanPraiseInfoC2S = sysCanPraiseInfoC2S;
				break;
			}
			case 842u:
			{
				SysPraiseC2S sysPraiseC2S = new SysPraiseC2S();
				if (msgCase_ == MsgOneofCase.SysPraiseC2S)
				{
					sysPraiseC2S.MergeFrom(SysPraiseC2S);
				}
				input.ReadMessage(sysPraiseC2S);
				SysPraiseC2S = sysPraiseC2S;
				break;
			}
			case 850u:
			{
				SysRoomFinishC2S sysRoomFinishC2S = new SysRoomFinishC2S();
				if (msgCase_ == MsgOneofCase.SysRoomFinishC2S)
				{
					sysRoomFinishC2S.MergeFrom(SysRoomFinishC2S);
				}
				input.ReadMessage(sysRoomFinishC2S);
				SysRoomFinishC2S = sysRoomFinishC2S;
				break;
			}
			case 858u:
			{
				SysRoomAddExpC2S sysRoomAddExpC2S = new SysRoomAddExpC2S();
				if (msgCase_ == MsgOneofCase.SysRoomAddExpC2S)
				{
					sysRoomAddExpC2S.MergeFrom(SysRoomAddExpC2S);
				}
				input.ReadMessage(sysRoomAddExpC2S);
				SysRoomAddExpC2S = sysRoomAddExpC2S;
				break;
			}
			case 866u:
			{
				SysGetShowFriendC2S sysGetShowFriendC2S = new SysGetShowFriendC2S();
				if (msgCase_ == MsgOneofCase.SysGetShowFriendC2S)
				{
					sysGetShowFriendC2S.MergeFrom(SysGetShowFriendC2S);
				}
				input.ReadMessage(sysGetShowFriendC2S);
				SysGetShowFriendC2S = sysGetShowFriendC2S;
				break;
			}
			case 874u:
			{
				SysFriendInviteC2S sysFriendInviteC2S = new SysFriendInviteC2S();
				if (msgCase_ == MsgOneofCase.SysFriendInviteC2S)
				{
					sysFriendInviteC2S.MergeFrom(SysFriendInviteC2S);
				}
				input.ReadMessage(sysFriendInviteC2S);
				SysFriendInviteC2S = sysFriendInviteC2S;
				break;
			}
			case 882u:
			{
				SysFriendDelC2S sysFriendDelC2S = new SysFriendDelC2S();
				if (msgCase_ == MsgOneofCase.SysFriendDelC2S)
				{
					sysFriendDelC2S.MergeFrom(SysFriendDelC2S);
				}
				input.ReadMessage(sysFriendDelC2S);
				SysFriendDelC2S = sysFriendDelC2S;
				break;
			}
			case 890u:
			{
				SysFriendInfoC2S sysFriendInfoC2S = new SysFriendInfoC2S();
				if (msgCase_ == MsgOneofCase.SysFriendInfoC2S)
				{
					sysFriendInfoC2S.MergeFrom(SysFriendInfoC2S);
				}
				input.ReadMessage(sysFriendInfoC2S);
				SysFriendInfoC2S = sysFriendInfoC2S;
				break;
			}
			case 898u:
			{
				SysFriendApplyC2S sysFriendApplyC2S = new SysFriendApplyC2S();
				if (msgCase_ == MsgOneofCase.SysFriendApplyC2S)
				{
					sysFriendApplyC2S.MergeFrom(SysFriendApplyC2S);
				}
				input.ReadMessage(sysFriendApplyC2S);
				SysFriendApplyC2S = sysFriendApplyC2S;
				break;
			}
			case 906u:
			{
				SysFriendAddC2S sysFriendAddC2S = new SysFriendAddC2S();
				if (msgCase_ == MsgOneofCase.SysFriendAddC2S)
				{
					sysFriendAddC2S.MergeFrom(SysFriendAddC2S);
				}
				input.ReadMessage(sysFriendAddC2S);
				SysFriendAddC2S = sysFriendAddC2S;
				break;
			}
			case 914u:
			{
				SysFriendSendMsgC2S sysFriendSendMsgC2S = new SysFriendSendMsgC2S();
				if (msgCase_ == MsgOneofCase.SysFriendSendMsgC2S)
				{
					sysFriendSendMsgC2S.MergeFrom(SysFriendSendMsgC2S);
				}
				input.ReadMessage(sysFriendSendMsgC2S);
				SysFriendSendMsgC2S = sysFriendSendMsgC2S;
				break;
			}
			case 922u:
			{
				SysCampaignFinishC2S sysCampaignFinishC2S = new SysCampaignFinishC2S();
				if (msgCase_ == MsgOneofCase.SysCampaignFinishC2S)
				{
					sysCampaignFinishC2S.MergeFrom(SysCampaignFinishC2S);
				}
				input.ReadMessage(sysCampaignFinishC2S);
				SysCampaignFinishC2S = sysCampaignFinishC2S;
				break;
			}
			case 930u:
			{
				SysAbroadPayMsgC2S sysAbroadPayMsgC2S = new SysAbroadPayMsgC2S();
				if (msgCase_ == MsgOneofCase.SysAbroadPayMsgC2S)
				{
					sysAbroadPayMsgC2S.MergeFrom(SysAbroadPayMsgC2S);
				}
				input.ReadMessage(sysAbroadPayMsgC2S);
				SysAbroadPayMsgC2S = sysAbroadPayMsgC2S;
				break;
			}
			case 938u:
			{
				AcquisitionMsgC2S acquisitionMsgC2S = new AcquisitionMsgC2S();
				if (msgCase_ == MsgOneofCase.AcquisitionMsgC2S)
				{
					acquisitionMsgC2S.MergeFrom(AcquisitionMsgC2S);
				}
				input.ReadMessage(acquisitionMsgC2S);
				AcquisitionMsgC2S = acquisitionMsgC2S;
				break;
			}
			case 946u:
			{
				SysFightRecord sysFightRecord = new SysFightRecord();
				if (msgCase_ == MsgOneofCase.SysFightRecord)
				{
					sysFightRecord.MergeFrom(SysFightRecord);
				}
				input.ReadMessage(sysFightRecord);
				SysFightRecord = sysFightRecord;
				break;
			}
			case 954u:
			{
				SysCampaignAwardC2S sysCampaignAwardC2S = new SysCampaignAwardC2S();
				if (msgCase_ == MsgOneofCase.SysCampaignAwardC2S)
				{
					sysCampaignAwardC2S.MergeFrom(SysCampaignAwardC2S);
				}
				input.ReadMessage(sysCampaignAwardC2S);
				SysCampaignAwardC2S = sysCampaignAwardC2S;
				break;
			}
			case 962u:
			{
				SysRecoupItemC2S sysRecoupItemC2S = new SysRecoupItemC2S();
				if (msgCase_ == MsgOneofCase.SysRecoupItemC2S)
				{
					sysRecoupItemC2S.MergeFrom(SysRecoupItemC2S);
				}
				input.ReadMessage(sysRecoupItemC2S);
				SysRecoupItemC2S = sysRecoupItemC2S;
				break;
			}
			case 970u:
			{
				SysPlayerOnlineRoomC2S sysPlayerOnlineRoomC2S = new SysPlayerOnlineRoomC2S();
				if (msgCase_ == MsgOneofCase.SysPlayerOnlineRoomC2S)
				{
					sysPlayerOnlineRoomC2S.MergeFrom(SysPlayerOnlineRoomC2S);
				}
				input.ReadMessage(sysPlayerOnlineRoomC2S);
				SysPlayerOnlineRoomC2S = sysPlayerOnlineRoomC2S;
				break;
			}
			case 978u:
			{
				SysPlayerCleanC2S sysPlayerCleanC2S = new SysPlayerCleanC2S();
				if (msgCase_ == MsgOneofCase.SysPlayerCleanC2S)
				{
					sysPlayerCleanC2S.MergeFrom(SysPlayerCleanC2S);
				}
				input.ReadMessage(sysPlayerCleanC2S);
				SysPlayerCleanC2S = sysPlayerCleanC2S;
				break;
			}
			case 986u:
			{
				SysPlayerPunishmentTimeC2S sysPlayerPunishmentTimeC2S = new SysPlayerPunishmentTimeC2S();
				if (msgCase_ == MsgOneofCase.SysPlayerPunishmentTimeC2S)
				{
					sysPlayerPunishmentTimeC2S.MergeFrom(SysPlayerPunishmentTimeC2S);
				}
				input.ReadMessage(sysPlayerPunishmentTimeC2S);
				SysPlayerPunishmentTimeC2S = sysPlayerPunishmentTimeC2S;
				break;
			}
			case 994u:
			{
				SysMatchSuccess sysMatchSuccess = new SysMatchSuccess();
				if (msgCase_ == MsgOneofCase.SysMatchSuccess)
				{
					sysMatchSuccess.MergeFrom(SysMatchSuccess);
				}
				input.ReadMessage(sysMatchSuccess);
				SysMatchSuccess = sysMatchSuccess;
				break;
			}
			case 1002u:
			{
				SysChinaPayMsgC2S sysChinaPayMsgC2S = new SysChinaPayMsgC2S();
				if (msgCase_ == MsgOneofCase.SysChinaPayMsgC2S)
				{
					sysChinaPayMsgC2S.MergeFrom(SysChinaPayMsgC2S);
				}
				input.ReadMessage(sysChinaPayMsgC2S);
				SysChinaPayMsgC2S = sysChinaPayMsgC2S;
				break;
			}
			case 1010u:
			{
				SysChangeMatchTeamState sysChangeMatchTeamState = new SysChangeMatchTeamState();
				if (msgCase_ == MsgOneofCase.SysChangeMatchTeamState)
				{
					sysChangeMatchTeamState.MergeFrom(SysChangeMatchTeamState);
				}
				input.ReadMessage(sysChangeMatchTeamState);
				SysChangeMatchTeamState = sysChangeMatchTeamState;
				break;
			}
			case 1018u:
			{
				SysSaveSimplePlayerInfoC2S sysSaveSimplePlayerInfoC2S = new SysSaveSimplePlayerInfoC2S();
				if (msgCase_ == MsgOneofCase.SysSaveSimplePlayerInfoC2S)
				{
					sysSaveSimplePlayerInfoC2S.MergeFrom(SysSaveSimplePlayerInfoC2S);
				}
				input.ReadMessage(sysSaveSimplePlayerInfoC2S);
				SysSaveSimplePlayerInfoC2S = sysSaveSimplePlayerInfoC2S;
				break;
			}
			case 1026u:
			{
				SysGmChangeNameC2S sysGmChangeNameC2S = new SysGmChangeNameC2S();
				if (msgCase_ == MsgOneofCase.SysGmChangeNameC2S)
				{
					sysGmChangeNameC2S.MergeFrom(SysGmChangeNameC2S);
				}
				input.ReadMessage(sysGmChangeNameC2S);
				SysGmChangeNameC2S = sysGmChangeNameC2S;
				break;
			}
			case 1034u:
			{
				SysSyncPlayerC2S sysSyncPlayerC2S = new SysSyncPlayerC2S();
				if (msgCase_ == MsgOneofCase.SysSyncPlayerC2S)
				{
					sysSyncPlayerC2S.MergeFrom(SysSyncPlayerC2S);
				}
				input.ReadMessage(sysSyncPlayerC2S);
				SysSyncPlayerC2S = sysSyncPlayerC2S;
				break;
			}
			case 1042u:
			{
				SysPlayerCreditScoreChangeC2S sysPlayerCreditScoreChangeC2S = new SysPlayerCreditScoreChangeC2S();
				if (msgCase_ == MsgOneofCase.SysPlayerCreditScoreChangeC2S)
				{
					sysPlayerCreditScoreChangeC2S.MergeFrom(SysPlayerCreditScoreChangeC2S);
				}
				input.ReadMessage(sysPlayerCreditScoreChangeC2S);
				SysPlayerCreditScoreChangeC2S = sysPlayerCreditScoreChangeC2S;
				break;
			}
			case 1050u:
			{
				SysSyncPlayerMatchPunishmentTimeC2S sysSyncPlayerMatchPunishmentTimeC2S = new SysSyncPlayerMatchPunishmentTimeC2S();
				if (msgCase_ == MsgOneofCase.SysSyncPlayerMatchPunishmentTimeC2S)
				{
					sysSyncPlayerMatchPunishmentTimeC2S.MergeFrom(SysSyncPlayerMatchPunishmentTimeC2S);
				}
				input.ReadMessage(sysSyncPlayerMatchPunishmentTimeC2S);
				SysSyncPlayerMatchPunishmentTimeC2S = sysSyncPlayerMatchPunishmentTimeC2S;
				break;
			}
			case 1058u:
			{
				GMChangeCreditScoreC2S gMChangeCreditScoreC2S = new GMChangeCreditScoreC2S();
				if (msgCase_ == MsgOneofCase.GMChangeCreditScoreC2S)
				{
					gMChangeCreditScoreC2S.MergeFrom(GMChangeCreditScoreC2S);
				}
				input.ReadMessage(gMChangeCreditScoreC2S);
				GMChangeCreditScoreC2S = gMChangeCreditScoreC2S;
				break;
			}
			case 1066u:
			{
				SysPushReturnInfoC2S sysPushReturnInfoC2S = new SysPushReturnInfoC2S();
				if (msgCase_ == MsgOneofCase.SysPushReturnInfoC2S)
				{
					sysPushReturnInfoC2S.MergeFrom(SysPushReturnInfoC2S);
				}
				input.ReadMessage(sysPushReturnInfoC2S);
				SysPushReturnInfoC2S = sysPushReturnInfoC2S;
				break;
			}
			case 1074u:
			{
				SysMutePlayerC2S sysMutePlayerC2S = new SysMutePlayerC2S();
				if (msgCase_ == MsgOneofCase.SysMutePlayerC2S)
				{
					sysMutePlayerC2S.MergeFrom(SysMutePlayerC2S);
				}
				input.ReadMessage(sysMutePlayerC2S);
				SysMutePlayerC2S = sysMutePlayerC2S;
				break;
			}
			case 1082u:
			{
				SysQuestionC2S sysQuestionC2S = new SysQuestionC2S();
				if (msgCase_ == MsgOneofCase.SysQuestionC2S)
				{
					sysQuestionC2S.MergeFrom(SysQuestionC2S);
				}
				input.ReadMessage(sysQuestionC2S);
				SysQuestionC2S = sysQuestionC2S;
				break;
			}
			case 8010u:
			{
				KickS2C kickS2C = new KickS2C();
				if (msgCase_ == MsgOneofCase.KickS2C)
				{
					kickS2C.MergeFrom(KickS2C);
				}
				input.ReadMessage(kickS2C);
				KickS2C = kickS2C;
				break;
			}
			case 8018u:
			{
				PredictActionS2C predictActionS2C = new PredictActionS2C();
				if (msgCase_ == MsgOneofCase.PredictActionS2C)
				{
					predictActionS2C.MergeFrom(PredictActionS2C);
				}
				input.ReadMessage(predictActionS2C);
				PredictActionS2C = predictActionS2C;
				break;
			}
			case 8026u:
			{
				RunningGameS2C runningGameS2C = new RunningGameS2C();
				if (msgCase_ == MsgOneofCase.RunningGameS2C)
				{
					runningGameS2C.MergeFrom(RunningGameS2C);
				}
				input.ReadMessage(runningGameS2C);
				RunningGameS2C = runningGameS2C;
				break;
			}
			case 8058u:
			{
				BattleS2C battleS2C = new BattleS2C();
				if (msgCase_ == MsgOneofCase.BattleS2C)
				{
					battleS2C.MergeFrom(BattleS2C);
				}
				input.ReadMessage(battleS2C);
				BattleS2C = battleS2C;
				break;
			}
			case 8090u:
			{
				LotteryDrawS2C lotteryDrawS2C = new LotteryDrawS2C();
				if (msgCase_ == MsgOneofCase.LotteryDrawS2C)
				{
					lotteryDrawS2C.MergeFrom(LotteryDrawS2C);
				}
				input.ReadMessage(lotteryDrawS2C);
				LotteryDrawS2C = lotteryDrawS2C;
				break;
			}
			case 8106u:
			{
				LandBuffsS2C landBuffsS2C = new LandBuffsS2C();
				if (msgCase_ == MsgOneofCase.LandBuffsS2C)
				{
					landBuffsS2C.MergeFrom(LandBuffsS2C);
				}
				input.ReadMessage(landBuffsS2C);
				LandBuffsS2C = landBuffsS2C;
				break;
			}
			case 8122u:
			{
				RoundStartS2C roundStartS2C = new RoundStartS2C();
				if (msgCase_ == MsgOneofCase.RoundStartS2C)
				{
					roundStartS2C.MergeFrom(RoundStartS2C);
				}
				input.ReadMessage(roundStartS2C);
				RoundStartS2C = roundStartS2C;
				break;
			}
			case 8130u:
			{
				GameFinishS2C gameFinishS2C = new GameFinishS2C();
				if (msgCase_ == MsgOneofCase.GameFinishS2C)
				{
					gameFinishS2C.MergeFrom(GameFinishS2C);
				}
				input.ReadMessage(gameFinishS2C);
				GameFinishS2C = gameFinishS2C;
				break;
			}
			case 8146u:
			{
				MonsterRefreshS2C monsterRefreshS2C = new MonsterRefreshS2C();
				if (msgCase_ == MsgOneofCase.MonsterRefreshS2C)
				{
					monsterRefreshS2C.MergeFrom(MonsterRefreshS2C);
				}
				input.ReadMessage(monsterRefreshS2C);
				MonsterRefreshS2C = monsterRefreshS2C;
				break;
			}
			case 8154u:
			{
				MovePointBuffS2C movePointBuffS2C = new MovePointBuffS2C();
				if (msgCase_ == MsgOneofCase.MovePointBuffS2C)
				{
					movePointBuffS2C.MergeFrom(MovePointBuffS2C);
				}
				input.ReadMessage(movePointBuffS2C);
				MovePointBuffS2C = movePointBuffS2C;
				break;
			}
			case 8162u:
			{
				ChangeDirS2C changeDirS2C = new ChangeDirS2C();
				if (msgCase_ == MsgOneofCase.ChangeDirS2C)
				{
					changeDirS2C.MergeFrom(ChangeDirS2C);
				}
				input.ReadMessage(changeDirS2C);
				ChangeDirS2C = changeDirS2C;
				break;
			}
			case 8178u:
			{
				GambleChangeS2C gambleChangeS2C = new GambleChangeS2C();
				if (msgCase_ == MsgOneofCase.GambleChangeS2C)
				{
					gambleChangeS2C.MergeFrom(GambleChangeS2C);
				}
				input.ReadMessage(gambleChangeS2C);
				GambleChangeS2C = gambleChangeS2C;
				break;
			}
			case 8186u:
			{
				HeroBarBoxChangeS2C heroBarBoxChangeS2C = new HeroBarBoxChangeS2C();
				if (msgCase_ == MsgOneofCase.HeroBarBoxChangeS2C)
				{
					heroBarBoxChangeS2C.MergeFrom(HeroBarBoxChangeS2C);
				}
				input.ReadMessage(heroBarBoxChangeS2C);
				HeroBarBoxChangeS2C = heroBarBoxChangeS2C;
				break;
			}
			case 8194u:
			{
				RoomNotifyS2C roomNotifyS2C = new RoomNotifyS2C();
				if (msgCase_ == MsgOneofCase.RoomNotifyS2C)
				{
					roomNotifyS2C.MergeFrom(RoomNotifyS2C);
				}
				input.ReadMessage(roomNotifyS2C);
				RoomNotifyS2C = roomNotifyS2C;
				break;
			}
			case 8210u:
			{
				ActionStartNotifyS2C actionStartNotifyS2C = new ActionStartNotifyS2C();
				if (msgCase_ == MsgOneofCase.ActionStartNotifyS2C)
				{
					actionStartNotifyS2C.MergeFrom(ActionStartNotifyS2C);
				}
				input.ReadMessage(actionStartNotifyS2C);
				ActionStartNotifyS2C = actionStartNotifyS2C;
				break;
			}
			case 8250u:
			{
				NoGambleNotifyS2C noGambleNotifyS2C = new NoGambleNotifyS2C();
				if (msgCase_ == MsgOneofCase.NoGambleNotifyS2C)
				{
					noGambleNotifyS2C.MergeFrom(NoGambleNotifyS2C);
				}
				input.ReadMessage(noGambleNotifyS2C);
				NoGambleNotifyS2C = noGambleNotifyS2C;
				break;
			}
			case 8274u:
			{
				GambleObServeS2C gambleObServeS2C = new GambleObServeS2C();
				if (msgCase_ == MsgOneofCase.GambleObServeS2C)
				{
					gambleObServeS2C.MergeFrom(GambleObServeS2C);
				}
				input.ReadMessage(gambleObServeS2C);
				GambleObServeS2C = gambleObServeS2C;
				break;
			}
			case 8322u:
			{
				UpdateHeroAttrS2C updateHeroAttrS2C = new UpdateHeroAttrS2C();
				if (msgCase_ == MsgOneofCase.UpdateHeroAttrS2C)
				{
					updateHeroAttrS2C.MergeFrom(UpdateHeroAttrS2C);
				}
				input.ReadMessage(updateHeroAttrS2C);
				UpdateHeroAttrS2C = updateHeroAttrS2C;
				break;
			}
			case 8338u:
			{
				ChangePlayerSlotS2C changePlayerSlotS2C = new ChangePlayerSlotS2C();
				if (msgCase_ == MsgOneofCase.ChangePlayerSlotS2C)
				{
					changePlayerSlotS2C.MergeFrom(ChangePlayerSlotS2C);
				}
				input.ReadMessage(changePlayerSlotS2C);
				ChangePlayerSlotS2C = changePlayerSlotS2C;
				break;
			}
			case 8346u:
			{
				BossSleepS2C bossSleepS2C = new BossSleepS2C();
				if (msgCase_ == MsgOneofCase.BossSleepS2C)
				{
					bossSleepS2C.MergeFrom(BossSleepS2C);
				}
				input.ReadMessage(bossSleepS2C);
				BossSleepS2C = bossSleepS2C;
				break;
			}
			case 8354u:
			{
				RefMallS2C refMallS2C = new RefMallS2C();
				if (msgCase_ == MsgOneofCase.RefMallS2C)
				{
					refMallS2C.MergeFrom(RefMallS2C);
				}
				input.ReadMessage(refMallS2C);
				RefMallS2C = refMallS2C;
				break;
			}
			case 8362u:
			{
				BagItemChangeS2C bagItemChangeS2C = new BagItemChangeS2C();
				if (msgCase_ == MsgOneofCase.BagItemChangeS2C)
				{
					bagItemChangeS2C.MergeFrom(BagItemChangeS2C);
				}
				input.ReadMessage(bagItemChangeS2C);
				BagItemChangeS2C = bagItemChangeS2C;
				break;
			}
			case 8370u:
			{
				RoleCardChangeS2C roleCardChangeS2C = new RoleCardChangeS2C();
				if (msgCase_ == MsgOneofCase.RoleCardChangeS2C)
				{
					roleCardChangeS2C.MergeFrom(RoleCardChangeS2C);
				}
				input.ReadMessage(roleCardChangeS2C);
				RoleCardChangeS2C = roleCardChangeS2C;
				break;
			}
			case 8378u:
			{
				TaskConditionS2C taskConditionS2C = new TaskConditionS2C();
				if (msgCase_ == MsgOneofCase.TaskConditionS2C)
				{
					taskConditionS2C.MergeFrom(TaskConditionS2C);
				}
				input.ReadMessage(taskConditionS2C);
				TaskConditionS2C = taskConditionS2C;
				break;
			}
			case 8386u:
			{
				TaskInfoS2C taskInfoS2C = new TaskInfoS2C();
				if (msgCase_ == MsgOneofCase.TaskInfoS2C)
				{
					taskInfoS2C.MergeFrom(TaskInfoS2C);
				}
				input.ReadMessage(taskInfoS2C);
				TaskInfoS2C = taskInfoS2C;
				break;
			}
			case 8394u:
			{
				PlayerOnlineS2C playerOnlineS2C = new PlayerOnlineS2C();
				if (msgCase_ == MsgOneofCase.PlayerOnlineS2C)
				{
					playerOnlineS2C.MergeFrom(PlayerOnlineS2C);
				}
				input.ReadMessage(playerOnlineS2C);
				PlayerOnlineS2C = playerOnlineS2C;
				break;
			}
			case 8402u:
			{
				ChangeExpS2C changeExpS2C = new ChangeExpS2C();
				if (msgCase_ == MsgOneofCase.ChangeExpS2C)
				{
					changeExpS2C.MergeFrom(ChangeExpS2C);
				}
				input.ReadMessage(changeExpS2C);
				ChangeExpS2C = changeExpS2C;
				break;
			}
			case 8410u:
			{
				MailAddS2C mailAddS2C = new MailAddS2C();
				if (msgCase_ == MsgOneofCase.MailAddS2C)
				{
					mailAddS2C.MergeFrom(MailAddS2C);
				}
				input.ReadMessage(mailAddS2C);
				MailAddS2C = mailAddS2C;
				break;
			}
			case 8418u:
			{
				OnlineSyncRoomIdS2C onlineSyncRoomIdS2C = new OnlineSyncRoomIdS2C();
				if (msgCase_ == MsgOneofCase.OnlineSyncRoomIdS2C)
				{
					onlineSyncRoomIdS2C.MergeFrom(OnlineSyncRoomIdS2C);
				}
				input.ReadMessage(onlineSyncRoomIdS2C);
				OnlineSyncRoomIdS2C = onlineSyncRoomIdS2C;
				break;
			}
			case 8426u:
			{
				NoticeS2C noticeS2C = new NoticeS2C();
				if (msgCase_ == MsgOneofCase.NoticeS2C)
				{
					noticeS2C.MergeFrom(NoticeS2C);
				}
				input.ReadMessage(noticeS2C);
				NoticeS2C = noticeS2C;
				break;
			}
			case 8434u:
			{
				ActivityTaskConditionS2C activityTaskConditionS2C = new ActivityTaskConditionS2C();
				if (msgCase_ == MsgOneofCase.ActivityTaskConditionS2C)
				{
					activityTaskConditionS2C.MergeFrom(ActivityTaskConditionS2C);
				}
				input.ReadMessage(activityTaskConditionS2C);
				ActivityTaskConditionS2C = activityTaskConditionS2C;
				break;
			}
			case 8442u:
			{
				MapEventS2C mapEventS2C = new MapEventS2C();
				if (msgCase_ == MsgOneofCase.MapEventS2C)
				{
					mapEventS2C.MergeFrom(MapEventS2C);
				}
				input.ReadMessage(mapEventS2C);
				MapEventS2C = mapEventS2C;
				break;
			}
			case 8450u:
			{
				Day7RewardS2C day7RewardS2C = new Day7RewardS2C();
				if (msgCase_ == MsgOneofCase.Day7RewardS2C)
				{
					day7RewardS2C.MergeFrom(Day7RewardS2C);
				}
				input.ReadMessage(day7RewardS2C);
				Day7RewardS2C = day7RewardS2C;
				break;
			}
			case 8458u:
			{
				MapEventTrainS2C mapEventTrainS2C = new MapEventTrainS2C();
				if (msgCase_ == MsgOneofCase.MapEventTrainS2C)
				{
					mapEventTrainS2C.MergeFrom(MapEventTrainS2C);
				}
				input.ReadMessage(mapEventTrainS2C);
				MapEventTrainS2C = mapEventTrainS2C;
				break;
			}
			case 8466u:
			{
				ChangePraiseNumS2C changePraiseNumS2C = new ChangePraiseNumS2C();
				if (msgCase_ == MsgOneofCase.ChangePraiseNumS2C)
				{
					changePraiseNumS2C.MergeFrom(ChangePraiseNumS2C);
				}
				input.ReadMessage(changePraiseNumS2C);
				ChangePraiseNumS2C = changePraiseNumS2C;
				break;
			}
			case 8474u:
			{
				MonthlyCardS2C monthlyCardS2C = new MonthlyCardS2C();
				if (msgCase_ == MsgOneofCase.MonthlyCardS2C)
				{
					monthlyCardS2C.MergeFrom(MonthlyCardS2C);
				}
				input.ReadMessage(monthlyCardS2C);
				MonthlyCardS2C = monthlyCardS2C;
				break;
			}
			case 8482u:
			{
				MailDelS2C mailDelS2C = new MailDelS2C();
				if (msgCase_ == MsgOneofCase.MailDelS2C)
				{
					mailDelS2C.MergeFrom(MailDelS2C);
				}
				input.ReadMessage(mailDelS2C);
				MailDelS2C = mailDelS2C;
				break;
			}
			case 8490u:
			{
				FriendNotifyS2C friendNotifyS2C = new FriendNotifyS2C();
				if (msgCase_ == MsgOneofCase.FriendNotifyS2C)
				{
					friendNotifyS2C.MergeFrom(FriendNotifyS2C);
				}
				input.ReadMessage(friendNotifyS2C);
				FriendNotifyS2C = friendNotifyS2C;
				break;
			}
			case 8498u:
			{
				FriendListChangeS2C friendListChangeS2C = new FriendListChangeS2C();
				if (msgCase_ == MsgOneofCase.FriendListChangeS2C)
				{
					friendListChangeS2C.MergeFrom(FriendListChangeS2C);
				}
				input.ReadMessage(friendListChangeS2C);
				FriendListChangeS2C = friendListChangeS2C;
				break;
			}
			case 8506u:
			{
				FriendInviteNotifyS2C friendInviteNotifyS2C = new FriendInviteNotifyS2C();
				if (msgCase_ == MsgOneofCase.FriendInviteNotifyS2C)
				{
					friendInviteNotifyS2C.MergeFrom(FriendInviteNotifyS2C);
				}
				input.ReadMessage(friendInviteNotifyS2C);
				FriendInviteNotifyS2C = friendInviteNotifyS2C;
				break;
			}
			case 8514u:
			{
				LoopNoticeS2C loopNoticeS2C = new LoopNoticeS2C();
				if (msgCase_ == MsgOneofCase.LoopNoticeS2C)
				{
					loopNoticeS2C.MergeFrom(LoopNoticeS2C);
				}
				input.ReadMessage(loopNoticeS2C);
				LoopNoticeS2C = loopNoticeS2C;
				break;
			}
			case 8522u:
			{
				BattlePassLvS2C battlePassLvS2C = new BattlePassLvS2C();
				if (msgCase_ == MsgOneofCase.BattlePassLvS2C)
				{
					battlePassLvS2C.MergeFrom(BattlePassLvS2C);
				}
				input.ReadMessage(battlePassLvS2C);
				BattlePassLvS2C = battlePassLvS2C;
				break;
			}
			case 8530u:
			{
				BattlePassTaskInfoS2C battlePassTaskInfoS2C = new BattlePassTaskInfoS2C();
				if (msgCase_ == MsgOneofCase.BattlePassTaskInfoS2C)
				{
					battlePassTaskInfoS2C.MergeFrom(BattlePassTaskInfoS2C);
				}
				input.ReadMessage(battlePassTaskInfoS2C);
				BattlePassTaskInfoS2C = battlePassTaskInfoS2C;
				break;
			}
			case 8538u:
			{
				BattlePassUpdateTaskS2C battlePassUpdateTaskS2C = new BattlePassUpdateTaskS2C();
				if (msgCase_ == MsgOneofCase.BattlePassUpdateTaskS2C)
				{
					battlePassUpdateTaskS2C.MergeFrom(BattlePassUpdateTaskS2C);
				}
				input.ReadMessage(battlePassUpdateTaskS2C);
				BattlePassUpdateTaskS2C = battlePassUpdateTaskS2C;
				break;
			}
			case 8546u:
			{
				BattlePassBuyS2C battlePassBuyS2C = new BattlePassBuyS2C();
				if (msgCase_ == MsgOneofCase.BattlePassBuyS2C)
				{
					battlePassBuyS2C.MergeFrom(BattlePassBuyS2C);
				}
				input.ReadMessage(battlePassBuyS2C);
				BattlePassBuyS2C = battlePassBuyS2C;
				break;
			}
			case 8554u:
			{
				BattlePassInfoS2C battlePassInfoS2C = new BattlePassInfoS2C();
				if (msgCase_ == MsgOneofCase.BattlePassInfoS2C)
				{
					battlePassInfoS2C.MergeFrom(BattlePassInfoS2C);
				}
				input.ReadMessage(battlePassInfoS2C);
				BattlePassInfoS2C = battlePassInfoS2C;
				break;
			}
			case 8562u:
			{
				FriendsChatMsgS2C friendsChatMsgS2C = new FriendsChatMsgS2C();
				if (msgCase_ == MsgOneofCase.FriendsChatMsgS2C)
				{
					friendsChatMsgS2C.MergeFrom(FriendsChatMsgS2C);
				}
				input.ReadMessage(friendsChatMsgS2C);
				FriendsChatMsgS2C = friendsChatMsgS2C;
				break;
			}
			case 8570u:
			{
				GameProgressChangeS2C gameProgressChangeS2C = new GameProgressChangeS2C();
				if (msgCase_ == MsgOneofCase.GameProgressChangeS2C)
				{
					gameProgressChangeS2C.MergeFrom(GameProgressChangeS2C);
				}
				input.ReadMessage(gameProgressChangeS2C);
				GameProgressChangeS2C = gameProgressChangeS2C;
				break;
			}
			case 8578u:
			{
				MapMissionNotifyS2C mapMissionNotifyS2C = new MapMissionNotifyS2C();
				if (msgCase_ == MsgOneofCase.MapMissionNotifyS2C)
				{
					mapMissionNotifyS2C.MergeFrom(MapMissionNotifyS2C);
				}
				input.ReadMessage(mapMissionNotifyS2C);
				MapMissionNotifyS2C = mapMissionNotifyS2C;
				break;
			}
			case 8586u:
			{
				ChangeItemLimitS2C changeItemLimitS2C = new ChangeItemLimitS2C();
				if (msgCase_ == MsgOneofCase.ChangeItemLimitS2C)
				{
					changeItemLimitS2C.MergeFrom(ChangeItemLimitS2C);
				}
				input.ReadMessage(changeItemLimitS2C);
				ChangeItemLimitS2C = changeItemLimitS2C;
				break;
			}
			case 8594u:
			{
				CleanItemLimitS2C cleanItemLimitS2C = new CleanItemLimitS2C();
				if (msgCase_ == MsgOneofCase.CleanItemLimitS2C)
				{
					cleanItemLimitS2C.MergeFrom(CleanItemLimitS2C);
				}
				input.ReadMessage(cleanItemLimitS2C);
				CleanItemLimitS2C = cleanItemLimitS2C;
				break;
			}
			case 8602u:
			{
				CampaignPassS2C campaignPassS2C = new CampaignPassS2C();
				if (msgCase_ == MsgOneofCase.CampaignPassS2C)
				{
					campaignPassS2C.MergeFrom(CampaignPassS2C);
				}
				input.ReadMessage(campaignPassS2C);
				CampaignPassS2C = campaignPassS2C;
				break;
			}
			case 8610u:
			{
				CampaignNotifyS2C campaignNotifyS2C = new CampaignNotifyS2C();
				if (msgCase_ == MsgOneofCase.CampaignNotifyS2C)
				{
					campaignNotifyS2C.MergeFrom(CampaignNotifyS2C);
				}
				input.ReadMessage(campaignNotifyS2C);
				CampaignNotifyS2C = campaignNotifyS2C;
				break;
			}
			case 8618u:
			{
				KillMessageS2C killMessageS2C = new KillMessageS2C();
				if (msgCase_ == MsgOneofCase.KillMessageS2C)
				{
					killMessageS2C.MergeFrom(KillMessageS2C);
				}
				input.ReadMessage(killMessageS2C);
				KillMessageS2C = killMessageS2C;
				break;
			}
			case 8626u:
			{
				GameScoreChangeS2C gameScoreChangeS2C = new GameScoreChangeS2C();
				if (msgCase_ == MsgOneofCase.GameScoreChangeS2C)
				{
					gameScoreChangeS2C.MergeFrom(GameScoreChangeS2C);
				}
				input.ReadMessage(gameScoreChangeS2C);
				GameScoreChangeS2C = gameScoreChangeS2C;
				break;
			}
			case 8634u:
			{
				SignInRewardS2C signInRewardS2C = new SignInRewardS2C();
				if (msgCase_ == MsgOneofCase.SignInRewardS2C)
				{
					signInRewardS2C.MergeFrom(SignInRewardS2C);
				}
				input.ReadMessage(signInRewardS2C);
				SignInRewardS2C = signInRewardS2C;
				break;
			}
			case 8642u:
			{
				PayResultS2C payResultS2C = new PayResultS2C();
				if (msgCase_ == MsgOneofCase.PayResultS2C)
				{
					payResultS2C.MergeFrom(PayResultS2C);
				}
				input.ReadMessage(payResultS2C);
				PayResultS2C = payResultS2C;
				break;
			}
			case 8650u:
			{
				PayInfoChangeS2C payInfoChangeS2C = new PayInfoChangeS2C();
				if (msgCase_ == MsgOneofCase.PayInfoChangeS2C)
				{
					payInfoChangeS2C.MergeFrom(PayInfoChangeS2C);
				}
				input.ReadMessage(payInfoChangeS2C);
				PayInfoChangeS2C = payInfoChangeS2C;
				break;
			}
			case 8658u:
			{
				InviteSuccessS2C inviteSuccessS2C = new InviteSuccessS2C();
				if (msgCase_ == MsgOneofCase.InviteSuccessS2C)
				{
					inviteSuccessS2C.MergeFrom(InviteSuccessS2C);
				}
				input.ReadMessage(inviteSuccessS2C);
				InviteSuccessS2C = inviteSuccessS2C;
				break;
			}
			case 8666u:
			{
				InviteInfoNotifyS2C inviteInfoNotifyS2C = new InviteInfoNotifyS2C();
				if (msgCase_ == MsgOneofCase.InviteInfoNotifyS2C)
				{
					inviteInfoNotifyS2C.MergeFrom(InviteInfoNotifyS2C);
				}
				input.ReadMessage(inviteInfoNotifyS2C);
				InviteInfoNotifyS2C = inviteInfoNotifyS2C;
				break;
			}
			case 8674u:
			{
				MapStatusChangeS2C mapStatusChangeS2C = new MapStatusChangeS2C();
				if (msgCase_ == MsgOneofCase.MapStatusChangeS2C)
				{
					mapStatusChangeS2C.MergeFrom(MapStatusChangeS2C);
				}
				input.ReadMessage(mapStatusChangeS2C);
				MapStatusChangeS2C = mapStatusChangeS2C;
				break;
			}
			case 8682u:
			{
				GachaCountS2C gachaCountS2C = new GachaCountS2C();
				if (msgCase_ == MsgOneofCase.GachaCountS2C)
				{
					gachaCountS2C.MergeFrom(GachaCountS2C);
				}
				input.ReadMessage(gachaCountS2C);
				GachaCountS2C = gachaCountS2C;
				break;
			}
			case 8690u:
			{
				MapIndexChangeS2C mapIndexChangeS2C = new MapIndexChangeS2C();
				if (msgCase_ == MsgOneofCase.MapIndexChangeS2C)
				{
					mapIndexChangeS2C.MergeFrom(MapIndexChangeS2C);
				}
				input.ReadMessage(mapIndexChangeS2C);
				MapIndexChangeS2C = mapIndexChangeS2C;
				break;
			}
			case 8698u:
			{
				SurrenderPunishS2C surrenderPunishS2C = new SurrenderPunishS2C();
				if (msgCase_ == MsgOneofCase.SurrenderPunishS2C)
				{
					surrenderPunishS2C.MergeFrom(SurrenderPunishS2C);
				}
				input.ReadMessage(surrenderPunishS2C);
				SurrenderPunishS2C = surrenderPunishS2C;
				break;
			}
			case 8706u:
			{
				GamePassMapSuccessS2C gamePassMapSuccessS2C = new GamePassMapSuccessS2C();
				if (msgCase_ == MsgOneofCase.GamePassMapSuccessS2C)
				{
					gamePassMapSuccessS2C.MergeFrom(GamePassMapSuccessS2C);
				}
				input.ReadMessage(gamePassMapSuccessS2C);
				GamePassMapSuccessS2C = gamePassMapSuccessS2C;
				break;
			}
			case 8722u:
			{
				FriendDelNotifyS2C friendDelNotifyS2C = new FriendDelNotifyS2C();
				if (msgCase_ == MsgOneofCase.FriendDelNotifyS2C)
				{
					friendDelNotifyS2C.MergeFrom(FriendDelNotifyS2C);
				}
				input.ReadMessage(friendDelNotifyS2C);
				FriendDelNotifyS2C = friendDelNotifyS2C;
				break;
			}
			case 8730u:
			{
				BagExpiredTransformNotify bagExpiredTransformNotify = new BagExpiredTransformNotify();
				if (msgCase_ == MsgOneofCase.BagExpiredTransformNotify)
				{
					bagExpiredTransformNotify.MergeFrom(BagExpiredTransformNotify);
				}
				input.ReadMessage(bagExpiredTransformNotify);
				BagExpiredTransformNotify = bagExpiredTransformNotify;
				break;
			}
			case 8738u:
			{
				MapEventCrabS2C mapEventCrabS2C = new MapEventCrabS2C();
				if (msgCase_ == MsgOneofCase.MapEventCrabS2C)
				{
					mapEventCrabS2C.MergeFrom(MapEventCrabS2C);
				}
				input.ReadMessage(mapEventCrabS2C);
				MapEventCrabS2C = mapEventCrabS2C;
				break;
			}
			case 8746u:
			{
				PkAfterVoteS2C pkAfterVoteS2C = new PkAfterVoteS2C();
				if (msgCase_ == MsgOneofCase.PkAfterVoteS2C)
				{
					pkAfterVoteS2C.MergeFrom(PkAfterVoteS2C);
				}
				input.ReadMessage(pkAfterVoteS2C);
				PkAfterVoteS2C = pkAfterVoteS2C;
				break;
			}
			case 8754u:
			{
				PlayerTaskNotifyS2C playerTaskNotifyS2C = new PlayerTaskNotifyS2C();
				if (msgCase_ == MsgOneofCase.PlayerTaskNotifyS2C)
				{
					playerTaskNotifyS2C.MergeFrom(PlayerTaskNotifyS2C);
				}
				input.ReadMessage(playerTaskNotifyS2C);
				PlayerTaskNotifyS2C = playerTaskNotifyS2C;
				break;
			}
			case 8762u:
			{
				UnLockDifficultyS2C unLockDifficultyS2C = new UnLockDifficultyS2C();
				if (msgCase_ == MsgOneofCase.UnLockDifficultyS2C)
				{
					unLockDifficultyS2C.MergeFrom(UnLockDifficultyS2C);
				}
				input.ReadMessage(unLockDifficultyS2C);
				UnLockDifficultyS2C = unLockDifficultyS2C;
				break;
			}
			case 8770u:
			{
				HeroSkillMoveEffectS2C heroSkillMoveEffectS2C = new HeroSkillMoveEffectS2C();
				if (msgCase_ == MsgOneofCase.HeroSkillMoveEffectS2C)
				{
					heroSkillMoveEffectS2C.MergeFrom(HeroSkillMoveEffectS2C);
				}
				input.ReadMessage(heroSkillMoveEffectS2C);
				HeroSkillMoveEffectS2C = heroSkillMoveEffectS2C;
				break;
			}
			case 8778u:
			{
				TimeOutKickPlayerS2C timeOutKickPlayerS2C = new TimeOutKickPlayerS2C();
				if (msgCase_ == MsgOneofCase.TimeOutKickPlayerS2C)
				{
					timeOutKickPlayerS2C.MergeFrom(TimeOutKickPlayerS2C);
				}
				input.ReadMessage(timeOutKickPlayerS2C);
				TimeOutKickPlayerS2C = timeOutKickPlayerS2C;
				break;
			}
			case 8786u:
			{
				SayPhraseNotifyS2C sayPhraseNotifyS2C = new SayPhraseNotifyS2C();
				if (msgCase_ == MsgOneofCase.SayPhraseNotifyS2C)
				{
					sayPhraseNotifyS2C.MergeFrom(SayPhraseNotifyS2C);
				}
				input.ReadMessage(sayPhraseNotifyS2C);
				SayPhraseNotifyS2C = sayPhraseNotifyS2C;
				break;
			}
			case 8794u:
			{
				MatchTeamInviteNotify matchTeamInviteNotify = new MatchTeamInviteNotify();
				if (msgCase_ == MsgOneofCase.MatchTeamInviteNotify)
				{
					matchTeamInviteNotify.MergeFrom(MatchTeamInviteNotify);
				}
				input.ReadMessage(matchTeamInviteNotify);
				MatchTeamInviteNotify = matchTeamInviteNotify;
				break;
			}
			case 8802u:
			{
				RefreshMatchTeamStateNotify refreshMatchTeamStateNotify = new RefreshMatchTeamStateNotify();
				if (msgCase_ == MsgOneofCase.RefreshMatchTeamStateNotify)
				{
					refreshMatchTeamStateNotify.MergeFrom(RefreshMatchTeamStateNotify);
				}
				input.ReadMessage(refreshMatchTeamStateNotify);
				RefreshMatchTeamStateNotify = refreshMatchTeamStateNotify;
				break;
			}
			case 8810u:
			{
				ActivityPassGearChangeS2C activityPassGearChangeS2C = new ActivityPassGearChangeS2C();
				if (msgCase_ == MsgOneofCase.ActivityPassGearChangeS2C)
				{
					activityPassGearChangeS2C.MergeFrom(ActivityPassGearChangeS2C);
				}
				input.ReadMessage(activityPassGearChangeS2C);
				ActivityPassGearChangeS2C = activityPassGearChangeS2C;
				break;
			}
			case 8818u:
			{
				SingleGameScoreChangeS2C singleGameScoreChangeS2C = new SingleGameScoreChangeS2C();
				if (msgCase_ == MsgOneofCase.SingleGameScoreChange)
				{
					singleGameScoreChangeS2C.MergeFrom(SingleGameScoreChange);
				}
				input.ReadMessage(singleGameScoreChangeS2C);
				SingleGameScoreChange = singleGameScoreChangeS2C;
				break;
			}
			case 8826u:
			{
				DelayProgressMapEventS2C delayProgressMapEventS2C = new DelayProgressMapEventS2C();
				if (msgCase_ == MsgOneofCase.DelayProgressMapEventS2C)
				{
					delayProgressMapEventS2C.MergeFrom(DelayProgressMapEventS2C);
				}
				input.ReadMessage(delayProgressMapEventS2C);
				DelayProgressMapEventS2C = delayProgressMapEventS2C;
				break;
			}
			case 8834u:
			{
				LuckyStarMissionChangeS2C luckyStarMissionChangeS2C = new LuckyStarMissionChangeS2C();
				if (msgCase_ == MsgOneofCase.LuckyStarMissionChangeS2C)
				{
					luckyStarMissionChangeS2C.MergeFrom(LuckyStarMissionChangeS2C);
				}
				input.ReadMessage(luckyStarMissionChangeS2C);
				LuckyStarMissionChangeS2C = luckyStarMissionChangeS2C;
				break;
			}
			case 8842u:
			{
				MatchPunishmentS2C matchPunishmentS2C = new MatchPunishmentS2C();
				if (msgCase_ == MsgOneofCase.MatchPunishmentS2C)
				{
					matchPunishmentS2C.MergeFrom(MatchPunishmentS2C);
				}
				input.ReadMessage(matchPunishmentS2C);
				MatchPunishmentS2C = matchPunishmentS2C;
				break;
			}
			case 8850u:
			{
				ChallengeDataChangeS2C challengeDataChangeS2C = new ChallengeDataChangeS2C();
				if (msgCase_ == MsgOneofCase.ChallengeDataChangeS2C)
				{
					challengeDataChangeS2C.MergeFrom(ChallengeDataChangeS2C);
				}
				input.ReadMessage(challengeDataChangeS2C);
				ChallengeDataChangeS2C = challengeDataChangeS2C;
				break;
			}
			case 8858u:
			{
				GmUnlockRoleInfoS2C gmUnlockRoleInfoS2C = new GmUnlockRoleInfoS2C();
				if (msgCase_ == MsgOneofCase.GmUnlockRoleInfoS2C)
				{
					gmUnlockRoleInfoS2C.MergeFrom(GmUnlockRoleInfoS2C);
				}
				input.ReadMessage(gmUnlockRoleInfoS2C);
				GmUnlockRoleInfoS2C = gmUnlockRoleInfoS2C;
				break;
			}
			case 8866u:
			{
				SyncPlayerCreditInfoS2C syncPlayerCreditInfoS2C = new SyncPlayerCreditInfoS2C();
				if (msgCase_ == MsgOneofCase.SyncPlayerCreditInfoS2C)
				{
					syncPlayerCreditInfoS2C.MergeFrom(SyncPlayerCreditInfoS2C);
				}
				input.ReadMessage(syncPlayerCreditInfoS2C);
				SyncPlayerCreditInfoS2C = syncPlayerCreditInfoS2C;
				break;
			}
			case 8874u:
			{
				RoomHeroCardChangeS2C roomHeroCardChangeS2C = new RoomHeroCardChangeS2C();
				if (msgCase_ == MsgOneofCase.RoomHeroCardChangeS2C)
				{
					roomHeroCardChangeS2C.MergeFrom(RoomHeroCardChangeS2C);
				}
				input.ReadMessage(roomHeroCardChangeS2C);
				RoomHeroCardChangeS2C = roomHeroCardChangeS2C;
				break;
			}
			case 8882u:
			{
				RoomRoundAddTermS2C roomRoundAddTermS2C = new RoomRoundAddTermS2C();
				if (msgCase_ == MsgOneofCase.RoomRoundAddTermS2C)
				{
					roomRoundAddTermS2C.MergeFrom(RoomRoundAddTermS2C);
				}
				input.ReadMessage(roomRoundAddTermS2C);
				RoomRoundAddTermS2C = roomRoundAddTermS2C;
				break;
			}
			case 8890u:
			{
				ReturnInfoS2C returnInfoS2C = new ReturnInfoS2C();
				if (msgCase_ == MsgOneofCase.ReturnInfoS2C)
				{
					returnInfoS2C.MergeFrom(ReturnInfoS2C);
				}
				input.ReadMessage(returnInfoS2C);
				ReturnInfoS2C = returnInfoS2C;
				break;
			}
			case 8898u:
			{
				SyncRelicsS2C syncRelicsS2C = new SyncRelicsS2C();
				if (msgCase_ == MsgOneofCase.SyncRelicsS2C)
				{
					syncRelicsS2C.MergeFrom(SyncRelicsS2C);
				}
				input.ReadMessage(syncRelicsS2C);
				SyncRelicsS2C = syncRelicsS2C;
				break;
			}
			case 8906u:
			{
				ReplaySnapshotS2C replaySnapshotS2C = new ReplaySnapshotS2C();
				if (msgCase_ == MsgOneofCase.ReplaySnapshotS2C)
				{
					replaySnapshotS2C.MergeFrom(ReplaySnapshotS2C);
				}
				input.ReadMessage(replaySnapshotS2C);
				ReplaySnapshotS2C = replaySnapshotS2C;
				break;
			}
			case 8914u:
			{
				ClueNotifyS2C clueNotifyS2C = new ClueNotifyS2C();
				if (msgCase_ == MsgOneofCase.ClueNotifyS2C)
				{
					clueNotifyS2C.MergeFrom(ClueNotifyS2C);
				}
				input.ReadMessage(clueNotifyS2C);
				ClueNotifyS2C = clueNotifyS2C;
				break;
			}
			case 8922u:
			{
				ReplayDieS2C replayDieS2C = new ReplayDieS2C();
				if (msgCase_ == MsgOneofCase.ReplayDieS2C)
				{
					replayDieS2C.MergeFrom(ReplayDieS2C);
				}
				input.ReadMessage(replayDieS2C);
				ReplayDieS2C = replayDieS2C;
				break;
			}
			case 8930u:
			{
				GuildTaskNotifyS2C guildTaskNotifyS2C = new GuildTaskNotifyS2C();
				if (msgCase_ == MsgOneofCase.GuildTaskNotifyS2C)
				{
					guildTaskNotifyS2C.MergeFrom(GuildTaskNotifyS2C);
				}
				input.ReadMessage(guildTaskNotifyS2C);
				GuildTaskNotifyS2C = guildTaskNotifyS2C;
				break;
			}
			case 8938u:
			{
				GameRoundChangeS2C gameRoundChangeS2C = new GameRoundChangeS2C();
				if (msgCase_ == MsgOneofCase.GameRoundChangeS2C)
				{
					gameRoundChangeS2C.MergeFrom(GameRoundChangeS2C);
				}
				input.ReadMessage(gameRoundChangeS2C);
				GameRoundChangeS2C = gameRoundChangeS2C;
				break;
			}
			case 8946u:
			{
				NotifyQuestionS2C notifyQuestionS2C = new NotifyQuestionS2C();
				if (msgCase_ == MsgOneofCase.NotifyQuestionS2C)
				{
					notifyQuestionS2C.MergeFrom(NotifyQuestionS2C);
				}
				input.ReadMessage(notifyQuestionS2C);
				NotifyQuestionS2C = notifyQuestionS2C;
				break;
			}
			case 16010u:
			{
				SyncPlayerGuildS2C syncPlayerGuildS2C = new SyncPlayerGuildS2C();
				if (msgCase_ == MsgOneofCase.SyncPlayerGuildS2C)
				{
					syncPlayerGuildS2C.MergeFrom(SyncPlayerGuildS2C);
				}
				input.ReadMessage(syncPlayerGuildS2C);
				SyncPlayerGuildS2C = syncPlayerGuildS2C;
				break;
			}
			case 16018u:
			{
				SyncPlayerJoinGuildS2C syncPlayerJoinGuildS2C = new SyncPlayerJoinGuildS2C();
				if (msgCase_ == MsgOneofCase.SyncPlayerJoinGuildS2C)
				{
					syncPlayerJoinGuildS2C.MergeFrom(SyncPlayerJoinGuildS2C);
				}
				input.ReadMessage(syncPlayerJoinGuildS2C);
				SyncPlayerJoinGuildS2C = syncPlayerJoinGuildS2C;
				break;
			}
			case 16026u:
			{
				GuildChatMsgS2C guildChatMsgS2C = new GuildChatMsgS2C();
				if (msgCase_ == MsgOneofCase.GuildChatMsgS2C)
				{
					guildChatMsgS2C.MergeFrom(GuildChatMsgS2C);
				}
				input.ReadMessage(guildChatMsgS2C);
				GuildChatMsgS2C = guildChatMsgS2C;
				break;
			}
			case 16034u:
			{
				SyncGuildS2C syncGuildS2C = new SyncGuildS2C();
				if (msgCase_ == MsgOneofCase.SyncGuildS2C)
				{
					syncGuildS2C.MergeFrom(SyncGuildS2C);
				}
				input.ReadMessage(syncGuildS2C);
				SyncGuildS2C = syncGuildS2C;
				break;
			}
			case 16042u:
			{
				SyncGuildMemberS2C syncGuildMemberS2C = new SyncGuildMemberS2C();
				if (msgCase_ == MsgOneofCase.SyncGuildMemberS2C)
				{
					syncGuildMemberS2C.MergeFrom(SyncGuildMemberS2C);
				}
				input.ReadMessage(syncGuildMemberS2C);
				SyncGuildMemberS2C = syncGuildMemberS2C;
				break;
			}
			case 16050u:
			{
				SyncGuildMemberExitS2C syncGuildMemberExitS2C = new SyncGuildMemberExitS2C();
				if (msgCase_ == MsgOneofCase.SyncGuildMemberExitS2C)
				{
					syncGuildMemberExitS2C.MergeFrom(SyncGuildMemberExitS2C);
				}
				input.ReadMessage(syncGuildMemberExitS2C);
				SyncGuildMemberExitS2C = syncGuildMemberExitS2C;
				break;
			}
			case 40010u:
			{
				ConnectC2S connectC2S = new ConnectC2S();
				if (msgCase_ == MsgOneofCase.ConnectHandle)
				{
					connectC2S.MergeFrom(ConnectHandle);
				}
				input.ReadMessage(connectC2S);
				ConnectHandle = connectC2S;
				break;
			}
			case 40018u:
			{
				ConnectS2C connectS2C = new ConnectS2C();
				if (msgCase_ == MsgOneofCase.Connect)
				{
					connectS2C.MergeFrom(Connect);
				}
				input.ReadMessage(connectS2C);
				Connect = connectS2C;
				break;
			}
			case 40026u:
			{
				HeartbeatC2S heartbeatC2S = new HeartbeatC2S();
				if (msgCase_ == MsgOneofCase.HeartbeatHandle)
				{
					heartbeatC2S.MergeFrom(HeartbeatHandle);
				}
				input.ReadMessage(heartbeatC2S);
				HeartbeatHandle = heartbeatC2S;
				break;
			}
			case 40034u:
			{
				HeartbeatS2C heartbeatS2C = new HeartbeatS2C();
				if (msgCase_ == MsgOneofCase.Heartbeat)
				{
					heartbeatS2C.MergeFrom(Heartbeat);
				}
				input.ReadMessage(heartbeatS2C);
				Heartbeat = heartbeatS2C;
				break;
			}
			case 40042u:
			{
				CreateRoomC2S createRoomC2S = new CreateRoomC2S();
				if (msgCase_ == MsgOneofCase.CreateRoomHandle)
				{
					createRoomC2S.MergeFrom(CreateRoomHandle);
				}
				input.ReadMessage(createRoomC2S);
				CreateRoomHandle = createRoomC2S;
				break;
			}
			case 40050u:
			{
				CreateRoomS2C createRoomS2C = new CreateRoomS2C();
				if (msgCase_ == MsgOneofCase.CreateRoom)
				{
					createRoomS2C.MergeFrom(CreateRoom);
				}
				input.ReadMessage(createRoomS2C);
				CreateRoom = createRoomS2C;
				break;
			}
			case 40058u:
			{
				SyncRoomC2S syncRoomC2S = new SyncRoomC2S();
				if (msgCase_ == MsgOneofCase.SyncRoomHandle)
				{
					syncRoomC2S.MergeFrom(SyncRoomHandle);
				}
				input.ReadMessage(syncRoomC2S);
				SyncRoomHandle = syncRoomC2S;
				break;
			}
			case 40066u:
			{
				SyncRoomS2C syncRoomS2C = new SyncRoomS2C();
				if (msgCase_ == MsgOneofCase.SyncRoom)
				{
					syncRoomS2C.MergeFrom(SyncRoom);
				}
				input.ReadMessage(syncRoomS2C);
				SyncRoom = syncRoomS2C;
				break;
			}
			case 40074u:
			{
				JoinRoomC2S joinRoomC2S = new JoinRoomC2S();
				if (msgCase_ == MsgOneofCase.JoinRoomHandle)
				{
					joinRoomC2S.MergeFrom(JoinRoomHandle);
				}
				input.ReadMessage(joinRoomC2S);
				JoinRoomHandle = joinRoomC2S;
				break;
			}
			case 40082u:
			{
				JoinRoomS2C joinRoomS2C = new JoinRoomS2C();
				if (msgCase_ == MsgOneofCase.JoinRoom)
				{
					joinRoomS2C.MergeFrom(JoinRoom);
				}
				input.ReadMessage(joinRoomS2C);
				JoinRoom = joinRoomS2C;
				break;
			}
			case 40090u:
			{
				ExitRoomC2S exitRoomC2S = new ExitRoomC2S();
				if (msgCase_ == MsgOneofCase.ExitRoomHandle)
				{
					exitRoomC2S.MergeFrom(ExitRoomHandle);
				}
				input.ReadMessage(exitRoomC2S);
				ExitRoomHandle = exitRoomC2S;
				break;
			}
			case 40098u:
			{
				ExitRoomS2C exitRoomS2C = new ExitRoomS2C();
				if (msgCase_ == MsgOneofCase.ExitRoom)
				{
					exitRoomS2C.MergeFrom(ExitRoom);
				}
				input.ReadMessage(exitRoomS2C);
				ExitRoom = exitRoomS2C;
				break;
			}
			case 40106u:
			{
				QueryRoomC2S queryRoomC2S = new QueryRoomC2S();
				if (msgCase_ == MsgOneofCase.QueryRoomHandle)
				{
					queryRoomC2S.MergeFrom(QueryRoomHandle);
				}
				input.ReadMessage(queryRoomC2S);
				QueryRoomHandle = queryRoomC2S;
				break;
			}
			case 40114u:
			{
				QueryRoomS2C queryRoomS2C = new QueryRoomS2C();
				if (msgCase_ == MsgOneofCase.QueryRoom)
				{
					queryRoomS2C.MergeFrom(QueryRoom);
				}
				input.ReadMessage(queryRoomS2C);
				QueryRoom = queryRoomS2C;
				break;
			}
			case 40122u:
			{
				RefreshRoomStateC2S refreshRoomStateC2S = new RefreshRoomStateC2S();
				if (msgCase_ == MsgOneofCase.RefreshRoomStateHandle)
				{
					refreshRoomStateC2S.MergeFrom(RefreshRoomStateHandle);
				}
				input.ReadMessage(refreshRoomStateC2S);
				RefreshRoomStateHandle = refreshRoomStateC2S;
				break;
			}
			case 40130u:
			{
				RefreshRoomStateS2C refreshRoomStateS2C = new RefreshRoomStateS2C();
				if (msgCase_ == MsgOneofCase.RefreshRoomState)
				{
					refreshRoomStateS2C.MergeFrom(RefreshRoomState);
				}
				input.ReadMessage(refreshRoomStateS2C);
				RefreshRoomState = refreshRoomStateS2C;
				break;
			}
			case 40154u:
			{
				StartGameC2S startGameC2S = new StartGameC2S();
				if (msgCase_ == MsgOneofCase.StartGameHandle)
				{
					startGameC2S.MergeFrom(StartGameHandle);
				}
				input.ReadMessage(startGameC2S);
				StartGameHandle = startGameC2S;
				break;
			}
			case 40162u:
			{
				StartGameS2C startGameS2C = new StartGameS2C();
				if (msgCase_ == MsgOneofCase.StartGame)
				{
					startGameS2C.MergeFrom(StartGame);
				}
				input.ReadMessage(startGameS2C);
				StartGame = startGameS2C;
				break;
			}
			case 40170u:
			{
				ThrowDiceC2S throwDiceC2S = new ThrowDiceC2S();
				if (msgCase_ == MsgOneofCase.ThrowDiceHandle)
				{
					throwDiceC2S.MergeFrom(ThrowDiceHandle);
				}
				input.ReadMessage(throwDiceC2S);
				ThrowDiceHandle = throwDiceC2S;
				break;
			}
			case 40178u:
			{
				ThrowDiceS2C throwDiceS2C = new ThrowDiceS2C();
				if (msgCase_ == MsgOneofCase.ThrowDice)
				{
					throwDiceS2C.MergeFrom(ThrowDice);
				}
				input.ReadMessage(throwDiceS2C);
				ThrowDice = throwDiceS2C;
				break;
			}
			case 40186u:
			{
				ChangeRoomC2S changeRoomC2S = new ChangeRoomC2S();
				if (msgCase_ == MsgOneofCase.ChangeRoomHandle)
				{
					changeRoomC2S.MergeFrom(ChangeRoomHandle);
				}
				input.ReadMessage(changeRoomC2S);
				ChangeRoomHandle = changeRoomC2S;
				break;
			}
			case 40194u:
			{
				ChangeRoomS2C changeRoomS2C = new ChangeRoomS2C();
				if (msgCase_ == MsgOneofCase.ChangeRoom)
				{
					changeRoomS2C.MergeFrom(ChangeRoom);
				}
				input.ReadMessage(changeRoomS2C);
				ChangeRoom = changeRoomS2C;
				break;
			}
			case 40218u:
			{
				MoveC2S moveC2S = new MoveC2S();
				if (msgCase_ == MsgOneofCase.MoveHandle)
				{
					moveC2S.MergeFrom(MoveHandle);
				}
				input.ReadMessage(moveC2S);
				MoveHandle = moveC2S;
				break;
			}
			case 40226u:
			{
				MoveS2C moveS2C = new MoveS2C();
				if (msgCase_ == MsgOneofCase.Move)
				{
					moveS2C.MergeFrom(Move);
				}
				input.ReadMessage(moveS2C);
				Move = moveS2C;
				break;
			}
			case 40234u:
			{
				ShopBuyC2S shopBuyC2S = new ShopBuyC2S();
				if (msgCase_ == MsgOneofCase.ShopBuyHandle)
				{
					shopBuyC2S.MergeFrom(ShopBuyHandle);
				}
				input.ReadMessage(shopBuyC2S);
				ShopBuyHandle = shopBuyC2S;
				break;
			}
			case 40242u:
			{
				ShopBuyS2C shopBuyS2C = new ShopBuyS2C();
				if (msgCase_ == MsgOneofCase.ShopBuy)
				{
					shopBuyS2C.MergeFrom(ShopBuy);
				}
				input.ReadMessage(shopBuyS2C);
				ShopBuy = shopBuyS2C;
				break;
			}
			case 40266u:
			{
				PursuitC2S pursuitC2S = new PursuitC2S();
				if (msgCase_ == MsgOneofCase.PursuitHandle)
				{
					pursuitC2S.MergeFrom(PursuitHandle);
				}
				input.ReadMessage(pursuitC2S);
				PursuitHandle = pursuitC2S;
				break;
			}
			case 40274u:
			{
				PursuitS2C pursuitS2C = new PursuitS2C();
				if (msgCase_ == MsgOneofCase.Pursuit)
				{
					pursuitS2C.MergeFrom(Pursuit);
				}
				input.ReadMessage(pursuitS2C);
				Pursuit = pursuitS2C;
				break;
			}
			case 40282u:
			{
				BattleUseCardC2S battleUseCardC2S = new BattleUseCardC2S();
				if (msgCase_ == MsgOneofCase.BattleUseCardHandle)
				{
					battleUseCardC2S.MergeFrom(BattleUseCardHandle);
				}
				input.ReadMessage(battleUseCardC2S);
				BattleUseCardHandle = battleUseCardC2S;
				break;
			}
			case 40290u:
			{
				BattleUseCardS2C battleUseCardS2C = new BattleUseCardS2C();
				if (msgCase_ == MsgOneofCase.BattleUseCard)
				{
					battleUseCardS2C.MergeFrom(BattleUseCard);
				}
				input.ReadMessage(battleUseCardS2C);
				BattleUseCard = battleUseCardS2C;
				break;
			}
			case 40298u:
			{
				BattleThrowDiceC2S battleThrowDiceC2S = new BattleThrowDiceC2S();
				if (msgCase_ == MsgOneofCase.BattleThrowDiceHandle)
				{
					battleThrowDiceC2S.MergeFrom(BattleThrowDiceHandle);
				}
				input.ReadMessage(battleThrowDiceC2S);
				BattleThrowDiceHandle = battleThrowDiceC2S;
				break;
			}
			case 40306u:
			{
				BattleThrowDiceS2C battleThrowDiceS2C = new BattleThrowDiceS2C();
				if (msgCase_ == MsgOneofCase.BattleThrowDice)
				{
					battleThrowDiceS2C.MergeFrom(BattleThrowDice);
				}
				input.ReadMessage(battleThrowDiceS2C);
				BattleThrowDice = battleThrowDiceS2C;
				break;
			}
			case 40314u:
			{
				BattleChoiceC2S battleChoiceC2S = new BattleChoiceC2S();
				if (msgCase_ == MsgOneofCase.BattleChoiceHandle)
				{
					battleChoiceC2S.MergeFrom(BattleChoiceHandle);
				}
				input.ReadMessage(battleChoiceC2S);
				BattleChoiceHandle = battleChoiceC2S;
				break;
			}
			case 40322u:
			{
				BattleChoiceS2C battleChoiceS2C = new BattleChoiceS2C();
				if (msgCase_ == MsgOneofCase.BattleChoice)
				{
					battleChoiceS2C.MergeFrom(BattleChoice);
				}
				input.ReadMessage(battleChoiceS2C);
				BattleChoice = battleChoiceS2C;
				break;
			}
			case 40330u:
			{
				LotteryChoiceC2S lotteryChoiceC2S = new LotteryChoiceC2S();
				if (msgCase_ == MsgOneofCase.LotteryChoiceHandle)
				{
					lotteryChoiceC2S.MergeFrom(LotteryChoiceHandle);
				}
				input.ReadMessage(lotteryChoiceC2S);
				LotteryChoiceHandle = lotteryChoiceC2S;
				break;
			}
			case 40338u:
			{
				LotteryChoiceS2C lotteryChoiceS2C = new LotteryChoiceS2C();
				if (msgCase_ == MsgOneofCase.LotteryChoice)
				{
					lotteryChoiceS2C.MergeFrom(LotteryChoice);
				}
				input.ReadMessage(lotteryChoiceS2C);
				LotteryChoice = lotteryChoiceS2C;
				break;
			}
			case 40346u:
			{
				MoveAgainC2S moveAgainC2S = new MoveAgainC2S();
				if (msgCase_ == MsgOneofCase.MoveAgainHandle)
				{
					moveAgainC2S.MergeFrom(MoveAgainHandle);
				}
				input.ReadMessage(moveAgainC2S);
				MoveAgainHandle = moveAgainC2S;
				break;
			}
			case 40354u:
			{
				MoveAgainS2C moveAgainS2C = new MoveAgainS2C();
				if (msgCase_ == MsgOneofCase.MoveAgain)
				{
					moveAgainS2C.MergeFrom(MoveAgain);
				}
				input.ReadMessage(moveAgainS2C);
				MoveAgain = moveAgainS2C;
				break;
			}
			case 40378u:
			{
				AskBattleC2S askBattleC2S = new AskBattleC2S();
				if (msgCase_ == MsgOneofCase.AskBattleHandle)
				{
					askBattleC2S.MergeFrom(AskBattleHandle);
				}
				input.ReadMessage(askBattleC2S);
				AskBattleHandle = askBattleC2S;
				break;
			}
			case 40386u:
			{
				AskBattleS2C askBattleS2C = new AskBattleS2C();
				if (msgCase_ == MsgOneofCase.AskBattle)
				{
					askBattleS2C.MergeFrom(AskBattle);
				}
				input.ReadMessage(askBattleS2C);
				AskBattle = askBattleS2C;
				break;
			}
			case 40394u:
			{
				RollGoldC2S rollGoldC2S = new RollGoldC2S();
				if (msgCase_ == MsgOneofCase.RollGoldHandle)
				{
					rollGoldC2S.MergeFrom(RollGoldHandle);
				}
				input.ReadMessage(rollGoldC2S);
				RollGoldHandle = rollGoldC2S;
				break;
			}
			case 40402u:
			{
				RollGoldS2C rollGoldS2C = new RollGoldS2C();
				if (msgCase_ == MsgOneofCase.RollGold)
				{
					rollGoldS2C.MergeFrom(RollGold);
				}
				input.ReadMessage(rollGoldS2C);
				RollGold = rollGoldS2C;
				break;
			}
			case 40410u:
			{
				EventThrowDiceC2S eventThrowDiceC2S = new EventThrowDiceC2S();
				if (msgCase_ == MsgOneofCase.EventThrowDiceHandle)
				{
					eventThrowDiceC2S.MergeFrom(EventThrowDiceHandle);
				}
				input.ReadMessage(eventThrowDiceC2S);
				EventThrowDiceHandle = eventThrowDiceC2S;
				break;
			}
			case 40418u:
			{
				EventThrowDiceS2C eventThrowDiceS2C = new EventThrowDiceS2C();
				if (msgCase_ == MsgOneofCase.EventThrowDice)
				{
					eventThrowDiceS2C.MergeFrom(EventThrowDice);
				}
				input.ReadMessage(eventThrowDiceS2C);
				EventThrowDice = eventThrowDiceS2C;
				break;
			}
			case 40426u:
			{
				TriggerEventC2S triggerEventC2S = new TriggerEventC2S();
				if (msgCase_ == MsgOneofCase.TriggerEventHandle)
				{
					triggerEventC2S.MergeFrom(TriggerEventHandle);
				}
				input.ReadMessage(triggerEventC2S);
				TriggerEventHandle = triggerEventC2S;
				break;
			}
			case 40434u:
			{
				TriggerEventS2C triggerEventS2C = new TriggerEventS2C();
				if (msgCase_ == MsgOneofCase.TriggerEvent)
				{
					triggerEventS2C.MergeFrom(TriggerEvent);
				}
				input.ReadMessage(triggerEventS2C);
				TriggerEvent = triggerEventS2C;
				break;
			}
			case 40442u:
			{
				UseEffectCardC2S useEffectCardC2S = new UseEffectCardC2S();
				if (msgCase_ == MsgOneofCase.UseEffectCardHandle)
				{
					useEffectCardC2S.MergeFrom(UseEffectCardHandle);
				}
				input.ReadMessage(useEffectCardC2S);
				UseEffectCardHandle = useEffectCardC2S;
				break;
			}
			case 40450u:
			{
				UseEffectCardS2C useEffectCardS2C = new UseEffectCardS2C();
				if (msgCase_ == MsgOneofCase.UseEffectCard)
				{
					useEffectCardS2C.MergeFrom(UseEffectCard);
				}
				input.ReadMessage(useEffectCardS2C);
				UseEffectCard = useEffectCardS2C;
				break;
			}
			case 40474u:
			{
				BombThrowDiceC2S bombThrowDiceC2S = new BombThrowDiceC2S();
				if (msgCase_ == MsgOneofCase.BombThrowDiceHandle)
				{
					bombThrowDiceC2S.MergeFrom(BombThrowDiceHandle);
				}
				input.ReadMessage(bombThrowDiceC2S);
				BombThrowDiceHandle = bombThrowDiceC2S;
				break;
			}
			case 40482u:
			{
				BombThrowDiceS2C bombThrowDiceS2C = new BombThrowDiceS2C();
				if (msgCase_ == MsgOneofCase.BombThrowDice)
				{
					bombThrowDiceS2C.MergeFrom(BombThrowDice);
				}
				input.ReadMessage(bombThrowDiceS2C);
				BombThrowDice = bombThrowDiceS2C;
				break;
			}
			case 40490u:
			{
				ChoiceDirectionC2S choiceDirectionC2S = new ChoiceDirectionC2S();
				if (msgCase_ == MsgOneofCase.ChoiceDirectionHandle)
				{
					choiceDirectionC2S.MergeFrom(ChoiceDirectionHandle);
				}
				input.ReadMessage(choiceDirectionC2S);
				ChoiceDirectionHandle = choiceDirectionC2S;
				break;
			}
			case 40498u:
			{
				ChoiceDirectionS2C choiceDirectionS2C = new ChoiceDirectionS2C();
				if (msgCase_ == MsgOneofCase.ChoiceDirection)
				{
					choiceDirectionS2C.MergeFrom(ChoiceDirection);
				}
				input.ReadMessage(choiceDirectionS2C);
				ChoiceDirection = choiceDirectionS2C;
				break;
			}
			case 40506u:
			{
				LandChoiceTargetC2S landChoiceTargetC2S = new LandChoiceTargetC2S();
				if (msgCase_ == MsgOneofCase.LandChoiceTargetHandle)
				{
					landChoiceTargetC2S.MergeFrom(LandChoiceTargetHandle);
				}
				input.ReadMessage(landChoiceTargetC2S);
				LandChoiceTargetHandle = landChoiceTargetC2S;
				break;
			}
			case 40514u:
			{
				LandChoiceTargetS2C landChoiceTargetS2C = new LandChoiceTargetS2C();
				if (msgCase_ == MsgOneofCase.LandChoiceTarget)
				{
					landChoiceTargetS2C.MergeFrom(LandChoiceTarget);
				}
				input.ReadMessage(landChoiceTargetS2C);
				LandChoiceTarget = landChoiceTargetS2C;
				break;
			}
			case 40538u:
			{
				ThrowDiceResultC2S throwDiceResultC2S = new ThrowDiceResultC2S();
				if (msgCase_ == MsgOneofCase.ThrowDiceResultHandle)
				{
					throwDiceResultC2S.MergeFrom(ThrowDiceResultHandle);
				}
				input.ReadMessage(throwDiceResultC2S);
				ThrowDiceResultHandle = throwDiceResultC2S;
				break;
			}
			case 40546u:
			{
				ThrowDiceResultS2C throwDiceResultS2C = new ThrowDiceResultS2C();
				if (msgCase_ == MsgOneofCase.ThrowDiceResult)
				{
					throwDiceResultS2C.MergeFrom(ThrowDiceResult);
				}
				input.ReadMessage(throwDiceResultS2C);
				ThrowDiceResult = throwDiceResultS2C;
				break;
			}
			case 40554u:
			{
				TriggerDivinationC2S triggerDivinationC2S = new TriggerDivinationC2S();
				if (msgCase_ == MsgOneofCase.TriggerDivinationHandle)
				{
					triggerDivinationC2S.MergeFrom(TriggerDivinationHandle);
				}
				input.ReadMessage(triggerDivinationC2S);
				TriggerDivinationHandle = triggerDivinationC2S;
				break;
			}
			case 40562u:
			{
				TriggerDivinationS2C triggerDivinationS2C = new TriggerDivinationS2C();
				if (msgCase_ == MsgOneofCase.TriggerDivination)
				{
					triggerDivinationS2C.MergeFrom(TriggerDivination);
				}
				input.ReadMessage(triggerDivinationS2C);
				TriggerDivination = triggerDivinationS2C;
				break;
			}
			case 40570u:
			{
				TriggerDestinyC2S triggerDestinyC2S = new TriggerDestinyC2S();
				if (msgCase_ == MsgOneofCase.TriggerDestinyHandle)
				{
					triggerDestinyC2S.MergeFrom(TriggerDestinyHandle);
				}
				input.ReadMessage(triggerDestinyC2S);
				TriggerDestinyHandle = triggerDestinyC2S;
				break;
			}
			case 40578u:
			{
				TriggerDestinyS2C triggerDestinyS2C = new TriggerDestinyS2C();
				if (msgCase_ == MsgOneofCase.TriggerDestiny)
				{
					triggerDestinyS2C.MergeFrom(TriggerDestiny);
				}
				input.ReadMessage(triggerDestinyS2C);
				TriggerDestiny = triggerDestinyS2C;
				break;
			}
			case 40586u:
			{
				UseQuickCardC2S useQuickCardC2S = new UseQuickCardC2S();
				if (msgCase_ == MsgOneofCase.UseQuickCardHandle)
				{
					useQuickCardC2S.MergeFrom(UseQuickCardHandle);
				}
				input.ReadMessage(useQuickCardC2S);
				UseQuickCardHandle = useQuickCardC2S;
				break;
			}
			case 40594u:
			{
				UseQuickCardS2C useQuickCardS2C = new UseQuickCardS2C();
				if (msgCase_ == MsgOneofCase.UseQuickCard)
				{
					useQuickCardS2C.MergeFrom(UseQuickCard);
				}
				input.ReadMessage(useQuickCardS2C);
				UseQuickCard = useQuickCardS2C;
				break;
			}
			case 40602u:
			{
				AbandonCardC2S abandonCardC2S = new AbandonCardC2S();
				if (msgCase_ == MsgOneofCase.AbandonCardHandle)
				{
					abandonCardC2S.MergeFrom(AbandonCardHandle);
				}
				input.ReadMessage(abandonCardC2S);
				AbandonCardHandle = abandonCardC2S;
				break;
			}
			case 40610u:
			{
				AbandonCardS2C abandonCardS2C = new AbandonCardS2C();
				if (msgCase_ == MsgOneofCase.AbandonCard)
				{
					abandonCardS2C.MergeFrom(AbandonCard);
				}
				input.ReadMessage(abandonCardS2C);
				AbandonCard = abandonCardS2C;
				break;
			}
			case 40618u:
			{
				StopOrContinueC2S stopOrContinueC2S = new StopOrContinueC2S();
				if (msgCase_ == MsgOneofCase.StopOrContinueHandle)
				{
					stopOrContinueC2S.MergeFrom(StopOrContinueHandle);
				}
				input.ReadMessage(stopOrContinueC2S);
				StopOrContinueHandle = stopOrContinueC2S;
				break;
			}
			case 40626u:
			{
				StopOrContinueS2C stopOrContinueS2C = new StopOrContinueS2C();
				if (msgCase_ == MsgOneofCase.StopOrContinue)
				{
					stopOrContinueS2C.MergeFrom(StopOrContinue);
				}
				input.ReadMessage(stopOrContinueS2C);
				StopOrContinue = stopOrContinueS2C;
				break;
			}
			case 40650u:
			{
				StartGambleC2S startGambleC2S = new StartGambleC2S();
				if (msgCase_ == MsgOneofCase.StartGambleHandle)
				{
					startGambleC2S.MergeFrom(StartGambleHandle);
				}
				input.ReadMessage(startGambleC2S);
				StartGambleHandle = startGambleC2S;
				break;
			}
			case 40658u:
			{
				StartGambleS2C startGambleS2C = new StartGambleS2C();
				if (msgCase_ == MsgOneofCase.StartGamble)
				{
					startGambleS2C.MergeFrom(StartGamble);
				}
				input.ReadMessage(startGambleS2C);
				StartGamble = startGambleS2C;
				break;
			}
			case 40666u:
			{
				GambleThrowDicC2S gambleThrowDicC2S = new GambleThrowDicC2S();
				if (msgCase_ == MsgOneofCase.GambleThrowDicHandle)
				{
					gambleThrowDicC2S.MergeFrom(GambleThrowDicHandle);
				}
				input.ReadMessage(gambleThrowDicC2S);
				GambleThrowDicHandle = gambleThrowDicC2S;
				break;
			}
			case 40674u:
			{
				GambleThrowDicS2C gambleThrowDicS2C = new GambleThrowDicS2C();
				if (msgCase_ == MsgOneofCase.GambleThrowDic)
				{
					gambleThrowDicS2C.MergeFrom(GambleThrowDic);
				}
				input.ReadMessage(gambleThrowDicS2C);
				GambleThrowDic = gambleThrowDicS2C;
				break;
			}
			case 40682u:
			{
				ChoiceHeroC2S2 choiceHeroC2S = new ChoiceHeroC2S2();
				if (msgCase_ == MsgOneofCase.ChoiceHeroC2S2Handle)
				{
					choiceHeroC2S.MergeFrom(ChoiceHeroC2S2Handle);
				}
				input.ReadMessage(choiceHeroC2S);
				ChoiceHeroC2S2Handle = choiceHeroC2S;
				break;
			}
			case 40690u:
			{
				ChoiceHeroS2C2 choiceHeroS2C = new ChoiceHeroS2C2();
				if (msgCase_ == MsgOneofCase.ChoiceHero2)
				{
					choiceHeroS2C.MergeFrom(ChoiceHero2);
				}
				input.ReadMessage(choiceHeroS2C);
				ChoiceHero2 = choiceHeroS2C;
				break;
			}
			case 40698u:
			{
				AffirmHeroC2S affirmHeroC2S = new AffirmHeroC2S();
				if (msgCase_ == MsgOneofCase.AffirmHeroHandle)
				{
					affirmHeroC2S.MergeFrom(AffirmHeroHandle);
				}
				input.ReadMessage(affirmHeroC2S);
				AffirmHeroHandle = affirmHeroC2S;
				break;
			}
			case 40706u:
			{
				AffirmHeroS2C affirmHeroS2C = new AffirmHeroS2C();
				if (msgCase_ == MsgOneofCase.AffirmHero)
				{
					affirmHeroS2C.MergeFrom(AffirmHero);
				}
				input.ReadMessage(affirmHeroS2C);
				AffirmHero = affirmHeroS2C;
				break;
			}
			case 40714u:
			{
				SearchRoomC2S searchRoomC2S = new SearchRoomC2S();
				if (msgCase_ == MsgOneofCase.SearchRoomHandle)
				{
					searchRoomC2S.MergeFrom(SearchRoomHandle);
				}
				input.ReadMessage(searchRoomC2S);
				SearchRoomHandle = searchRoomC2S;
				break;
			}
			case 40722u:
			{
				SearchRoomS2C searchRoomS2C = new SearchRoomS2C();
				if (msgCase_ == MsgOneofCase.SearchRoom)
				{
					searchRoomS2C.MergeFrom(SearchRoom);
				}
				input.ReadMessage(searchRoomS2C);
				SearchRoom = searchRoomS2C;
				break;
			}
			case 40730u:
			{
				GmC2S gmC2S = new GmC2S();
				if (msgCase_ == MsgOneofCase.GmHandle)
				{
					gmC2S.MergeFrom(GmHandle);
				}
				input.ReadMessage(gmC2S);
				GmHandle = gmC2S;
				break;
			}
			case 40738u:
			{
				GmS2C gmS2C = new GmS2C();
				if (msgCase_ == MsgOneofCase.Gm)
				{
					gmS2C.MergeFrom(Gm);
				}
				input.ReadMessage(gmS2C);
				Gm = gmS2C;
				break;
			}
			case 40746u:
			{
				TriggerHospitalC2S triggerHospitalC2S = new TriggerHospitalC2S();
				if (msgCase_ == MsgOneofCase.TriggerHospitalHandle)
				{
					triggerHospitalC2S.MergeFrom(TriggerHospitalHandle);
				}
				input.ReadMessage(triggerHospitalC2S);
				TriggerHospitalHandle = triggerHospitalC2S;
				break;
			}
			case 40754u:
			{
				TriggerHospitalS2C triggerHospitalS2C = new TriggerHospitalS2C();
				if (msgCase_ == MsgOneofCase.TriggerHospital)
				{
					triggerHospitalS2C.MergeFrom(TriggerHospital);
				}
				input.ReadMessage(triggerHospitalS2C);
				TriggerHospital = triggerHospitalS2C;
				break;
			}
			case 40762u:
			{
				SendChatC2S sendChatC2S = new SendChatC2S();
				if (msgCase_ == MsgOneofCase.SendChatHandle)
				{
					sendChatC2S.MergeFrom(SendChatHandle);
				}
				input.ReadMessage(sendChatC2S);
				SendChatHandle = sendChatC2S;
				break;
			}
			case 40770u:
			{
				SendChatS2C sendChatS2C = new SendChatS2C();
				if (msgCase_ == MsgOneofCase.SendChat)
				{
					sendChatS2C.MergeFrom(SendChat);
				}
				input.ReadMessage(sendChatS2C);
				SendChat = sendChatS2C;
				break;
			}
			case 40778u:
			{
				PlayerShopBuyC2S playerShopBuyC2S = new PlayerShopBuyC2S();
				if (msgCase_ == MsgOneofCase.PlayerShopBuyC2S)
				{
					playerShopBuyC2S.MergeFrom(PlayerShopBuyC2S);
				}
				input.ReadMessage(playerShopBuyC2S);
				PlayerShopBuyC2S = playerShopBuyC2S;
				break;
			}
			case 40786u:
			{
				PlayerShopBuyS2C playerShopBuyS2C = new PlayerShopBuyS2C();
				if (msgCase_ == MsgOneofCase.PlayerShopBuyS2C)
				{
					playerShopBuyS2C.MergeFrom(PlayerShopBuyS2C);
				}
				input.ReadMessage(playerShopBuyS2C);
				PlayerShopBuyS2C = playerShopBuyS2C;
				break;
			}
			case 40794u:
			{
				PlayerUseItemC2S playerUseItemC2S = new PlayerUseItemC2S();
				if (msgCase_ == MsgOneofCase.PlayerUseItemHandle)
				{
					playerUseItemC2S.MergeFrom(PlayerUseItemHandle);
				}
				input.ReadMessage(playerUseItemC2S);
				PlayerUseItemHandle = playerUseItemC2S;
				break;
			}
			case 40802u:
			{
				PlayerUseItemS2C playerUseItemS2C = new PlayerUseItemS2C();
				if (msgCase_ == MsgOneofCase.PlayerUseItem)
				{
					playerUseItemS2C.MergeFrom(PlayerUseItem);
				}
				input.ReadMessage(playerUseItemS2C);
				PlayerUseItem = playerUseItemS2C;
				break;
			}
			case 40810u:
			{
				UseTreasureC2S useTreasureC2S = new UseTreasureC2S();
				if (msgCase_ == MsgOneofCase.UseTreasureC2S)
				{
					useTreasureC2S.MergeFrom(UseTreasureC2S);
				}
				input.ReadMessage(useTreasureC2S);
				UseTreasureC2S = useTreasureC2S;
				break;
			}
			case 40818u:
			{
				UseTreasureS2C useTreasureS2C = new UseTreasureS2C();
				if (msgCase_ == MsgOneofCase.UseTreasureS2C)
				{
					useTreasureS2C.MergeFrom(UseTreasureS2C);
				}
				input.ReadMessage(useTreasureS2C);
				UseTreasureS2C = useTreasureS2C;
				break;
			}
			case 40826u:
			{
				SetFashionC2S setFashionC2S = new SetFashionC2S();
				if (msgCase_ == MsgOneofCase.SetFashionC2S)
				{
					setFashionC2S.MergeFrom(SetFashionC2S);
				}
				input.ReadMessage(setFashionC2S);
				SetFashionC2S = setFashionC2S;
				break;
			}
			case 40834u:
			{
				SetFashionS2C setFashionS2C = new SetFashionS2C();
				if (msgCase_ == MsgOneofCase.SetFashionS2C)
				{
					setFashionS2C.MergeFrom(SetFashionS2C);
				}
				input.ReadMessage(setFashionS2C);
				SetFashionS2C = setFashionS2C;
				break;
			}
			case 40842u:
			{
				SelectFashionPlanC2S selectFashionPlanC2S = new SelectFashionPlanC2S();
				if (msgCase_ == MsgOneofCase.SelectFashionPlanC2S)
				{
					selectFashionPlanC2S.MergeFrom(SelectFashionPlanC2S);
				}
				input.ReadMessage(selectFashionPlanC2S);
				SelectFashionPlanC2S = selectFashionPlanC2S;
				break;
			}
			case 40850u:
			{
				SelectFashionPlanS2C selectFashionPlanS2C = new SelectFashionPlanS2C();
				if (msgCase_ == MsgOneofCase.SelectFashionPlanS2C)
				{
					selectFashionPlanS2C.MergeFrom(SelectFashionPlanS2C);
				}
				input.ReadMessage(selectFashionPlanS2C);
				SelectFashionPlanS2C = selectFashionPlanS2C;
				break;
			}
			case 40858u:
			{
				GachaC2S gachaC2S = new GachaC2S();
				if (msgCase_ == MsgOneofCase.GachaC2S)
				{
					gachaC2S.MergeFrom(GachaC2S);
				}
				input.ReadMessage(gachaC2S);
				GachaC2S = gachaC2S;
				break;
			}
			case 40866u:
			{
				GachaS2C gachaS2C = new GachaS2C();
				if (msgCase_ == MsgOneofCase.GachaS2C)
				{
					gachaS2C.MergeFrom(GachaS2C);
				}
				input.ReadMessage(gachaS2C);
				GachaS2C = gachaS2C;
				break;
			}
			case 40874u:
			{
				SteamSearchRoomC2S steamSearchRoomC2S = new SteamSearchRoomC2S();
				if (msgCase_ == MsgOneofCase.SteamSearchRoomC2S)
				{
					steamSearchRoomC2S.MergeFrom(SteamSearchRoomC2S);
				}
				input.ReadMessage(steamSearchRoomC2S);
				SteamSearchRoomC2S = steamSearchRoomC2S;
				break;
			}
			case 40882u:
			{
				SteamSearchRoomS2C steamSearchRoomS2C = new SteamSearchRoomS2C();
				if (msgCase_ == MsgOneofCase.SteamSearchRoomS2C)
				{
					steamSearchRoomS2C.MergeFrom(SteamSearchRoomS2C);
				}
				input.ReadMessage(steamSearchRoomS2C);
				SteamSearchRoomS2C = steamSearchRoomS2C;
				break;
			}
			case 40890u:
			{
				CheatItemC2S cheatItemC2S = new CheatItemC2S();
				if (msgCase_ == MsgOneofCase.CheatItemHandle)
				{
					cheatItemC2S.MergeFrom(CheatItemHandle);
				}
				input.ReadMessage(cheatItemC2S);
				CheatItemHandle = cheatItemC2S;
				break;
			}
			case 40898u:
			{
				CheatItemS2C cheatItemS2C = new CheatItemS2C();
				if (msgCase_ == MsgOneofCase.CheatItem)
				{
					cheatItemS2C.MergeFrom(CheatItem);
				}
				input.ReadMessage(cheatItemS2C);
				CheatItem = cheatItemS2C;
				break;
			}
			case 40906u:
			{
				RoleCardUpLvC2S roleCardUpLvC2S = new RoleCardUpLvC2S();
				if (msgCase_ == MsgOneofCase.RoleCardUpLvC2S)
				{
					roleCardUpLvC2S.MergeFrom(RoleCardUpLvC2S);
				}
				input.ReadMessage(roleCardUpLvC2S);
				RoleCardUpLvC2S = roleCardUpLvC2S;
				break;
			}
			case 40914u:
			{
				RoleCardUpLvS2C roleCardUpLvS2C = new RoleCardUpLvS2C();
				if (msgCase_ == MsgOneofCase.RoleCardUpLvS2C)
				{
					roleCardUpLvS2C.MergeFrom(RoleCardUpLvS2C);
				}
				input.ReadMessage(roleCardUpLvS2C);
				RoleCardUpLvS2C = roleCardUpLvS2C;
				break;
			}
			case 40922u:
			{
				RoleCardBreakThroughC2S roleCardBreakThroughC2S = new RoleCardBreakThroughC2S();
				if (msgCase_ == MsgOneofCase.RoleCardBreakThroughC2S)
				{
					roleCardBreakThroughC2S.MergeFrom(RoleCardBreakThroughC2S);
				}
				input.ReadMessage(roleCardBreakThroughC2S);
				RoleCardBreakThroughC2S = roleCardBreakThroughC2S;
				break;
			}
			case 40930u:
			{
				RoleCardBreakThroughS2C roleCardBreakThroughS2C = new RoleCardBreakThroughS2C();
				if (msgCase_ == MsgOneofCase.RoleCardBreakThroughS2C)
				{
					roleCardBreakThroughS2C.MergeFrom(RoleCardBreakThroughS2C);
				}
				input.ReadMessage(roleCardBreakThroughS2C);
				RoleCardBreakThroughS2C = roleCardBreakThroughS2C;
				break;
			}
			case 40938u:
			{
				RoleCardChoiceResC2S roleCardChoiceResC2S = new RoleCardChoiceResC2S();
				if (msgCase_ == MsgOneofCase.RoleCardChoiceResC2S)
				{
					roleCardChoiceResC2S.MergeFrom(RoleCardChoiceResC2S);
				}
				input.ReadMessage(roleCardChoiceResC2S);
				RoleCardChoiceResC2S = roleCardChoiceResC2S;
				break;
			}
			case 40946u:
			{
				RoleCardChoiceResS2C roleCardChoiceResS2C = new RoleCardChoiceResS2C();
				if (msgCase_ == MsgOneofCase.RoleCardChoiceResS2C)
				{
					roleCardChoiceResS2C.MergeFrom(RoleCardChoiceResS2C);
				}
				input.ReadMessage(roleCardChoiceResS2C);
				RoleCardChoiceResS2C = roleCardChoiceResS2C;
				break;
			}
			case 40954u:
			{
				TaskRewardC2S taskRewardC2S = new TaskRewardC2S();
				if (msgCase_ == MsgOneofCase.TaskRewardC2S)
				{
					taskRewardC2S.MergeFrom(TaskRewardC2S);
				}
				input.ReadMessage(taskRewardC2S);
				TaskRewardC2S = taskRewardC2S;
				break;
			}
			case 40962u:
			{
				TaskRewardS2C taskRewardS2C = new TaskRewardS2C();
				if (msgCase_ == MsgOneofCase.TaskRewardS2C)
				{
					taskRewardS2C.MergeFrom(TaskRewardS2C);
				}
				input.ReadMessage(taskRewardS2C);
				TaskRewardS2C = taskRewardS2C;
				break;
			}
			case 40970u:
			{
				TeachingC2S teachingC2S = new TeachingC2S();
				if (msgCase_ == MsgOneofCase.TeachingC2S)
				{
					teachingC2S.MergeFrom(TeachingC2S);
				}
				input.ReadMessage(teachingC2S);
				TeachingC2S = teachingC2S;
				break;
			}
			case 40978u:
			{
				TeachingS2C teachingS2C = new TeachingS2C();
				if (msgCase_ == MsgOneofCase.TeachingS2C)
				{
					teachingS2C.MergeFrom(TeachingS2C);
				}
				input.ReadMessage(teachingS2C);
				TeachingS2C = teachingS2C;
				break;
			}
			case 40986u:
			{
				UseTreasureAutoTransformC2S useTreasureAutoTransformC2S = new UseTreasureAutoTransformC2S();
				if (msgCase_ == MsgOneofCase.UseTreasureAutoTransformC2S)
				{
					useTreasureAutoTransformC2S.MergeFrom(UseTreasureAutoTransformC2S);
				}
				input.ReadMessage(useTreasureAutoTransformC2S);
				UseTreasureAutoTransformC2S = useTreasureAutoTransformC2S;
				break;
			}
			case 40994u:
			{
				UseTreasureAutoTransformS2C useTreasureAutoTransformS2C = new UseTreasureAutoTransformS2C();
				if (msgCase_ == MsgOneofCase.UseTreasureAutoTransformS2C)
				{
					useTreasureAutoTransformS2C.MergeFrom(UseTreasureAutoTransformS2C);
				}
				input.ReadMessage(useTreasureAutoTransformS2C);
				UseTreasureAutoTransformS2C = useTreasureAutoTransformS2C;
				break;
			}
			case 41002u:
			{
				QuickJoinRoomC2S quickJoinRoomC2S = new QuickJoinRoomC2S();
				if (msgCase_ == MsgOneofCase.QuickJoinRoomC2S)
				{
					quickJoinRoomC2S.MergeFrom(QuickJoinRoomC2S);
				}
				input.ReadMessage(quickJoinRoomC2S);
				QuickJoinRoomC2S = quickJoinRoomC2S;
				break;
			}
			case 41010u:
			{
				QuickJoinRoomS2C quickJoinRoomS2C = new QuickJoinRoomS2C();
				if (msgCase_ == MsgOneofCase.QuickJoinRoomS2C)
				{
					quickJoinRoomS2C.MergeFrom(QuickJoinRoomS2C);
				}
				input.ReadMessage(quickJoinRoomS2C);
				QuickJoinRoomS2C = quickJoinRoomS2C;
				break;
			}
			case 41018u:
			{
				RoomKickPlayerC2S roomKickPlayerC2S = new RoomKickPlayerC2S();
				if (msgCase_ == MsgOneofCase.RoomKickPlayerC2S)
				{
					roomKickPlayerC2S.MergeFrom(RoomKickPlayerC2S);
				}
				input.ReadMessage(roomKickPlayerC2S);
				RoomKickPlayerC2S = roomKickPlayerC2S;
				break;
			}
			case 41026u:
			{
				RoomKickPlayerS2C roomKickPlayerS2C = new RoomKickPlayerS2C();
				if (msgCase_ == MsgOneofCase.RoomKickPlayerS2C)
				{
					roomKickPlayerS2C.MergeFrom(RoomKickPlayerS2C);
				}
				input.ReadMessage(roomKickPlayerS2C);
				RoomKickPlayerS2C = roomKickPlayerS2C;
				break;
			}
			case 41034u:
			{
				RoomAbdicationC2S roomAbdicationC2S = new RoomAbdicationC2S();
				if (msgCase_ == MsgOneofCase.RoomAbdicationC2S)
				{
					roomAbdicationC2S.MergeFrom(RoomAbdicationC2S);
				}
				input.ReadMessage(roomAbdicationC2S);
				RoomAbdicationC2S = roomAbdicationC2S;
				break;
			}
			case 41042u:
			{
				RoomAbdicationS2C roomAbdicationS2C = new RoomAbdicationS2C();
				if (msgCase_ == MsgOneofCase.RoomAbdicationS2C)
				{
					roomAbdicationS2C.MergeFrom(RoomAbdicationS2C);
				}
				input.ReadMessage(roomAbdicationS2C);
				RoomAbdicationS2C = roomAbdicationS2C;
				break;
			}
			case 41050u:
			{
				RoomReadyC2S roomReadyC2S = new RoomReadyC2S();
				if (msgCase_ == MsgOneofCase.RoomReadyC2S)
				{
					roomReadyC2S.MergeFrom(RoomReadyC2S);
				}
				input.ReadMessage(roomReadyC2S);
				RoomReadyC2S = roomReadyC2S;
				break;
			}
			case 41058u:
			{
				RoomReadyS2C roomReadyS2C = new RoomReadyS2C();
				if (msgCase_ == MsgOneofCase.RoomReadyS2C)
				{
					roomReadyS2C.MergeFrom(RoomReadyS2C);
				}
				input.ReadMessage(roomReadyS2C);
				RoomReadyS2C = roomReadyS2C;
				break;
			}
			case 41066u:
			{
				ChargeCreateC2S chargeCreateC2S = new ChargeCreateC2S();
				if (msgCase_ == MsgOneofCase.ChargeCreateC2S)
				{
					chargeCreateC2S.MergeFrom(ChargeCreateC2S);
				}
				input.ReadMessage(chargeCreateC2S);
				ChargeCreateC2S = chargeCreateC2S;
				break;
			}
			case 41074u:
			{
				ChargeCreateS2C chargeCreateS2C = new ChargeCreateS2C();
				if (msgCase_ == MsgOneofCase.ChargeCreateS2C)
				{
					chargeCreateS2C.MergeFrom(ChargeCreateS2C);
				}
				input.ReadMessage(chargeCreateS2C);
				ChargeCreateS2C = chargeCreateS2C;
				break;
			}
			case 41082u:
			{
				ChargeC2S chargeC2S = new ChargeC2S();
				if (msgCase_ == MsgOneofCase.ChargeC2S)
				{
					chargeC2S.MergeFrom(ChargeC2S);
				}
				input.ReadMessage(chargeC2S);
				ChargeC2S = chargeC2S;
				break;
			}
			case 41090u:
			{
				ChargeS2C chargeS2C = new ChargeS2C();
				if (msgCase_ == MsgOneofCase.ChargeS2C)
				{
					chargeS2C.MergeFrom(ChargeS2C);
				}
				input.ReadMessage(chargeS2C);
				ChargeS2C = chargeS2C;
				break;
			}
			case 41098u:
			{
				GiftCdkC2S giftCdkC2S = new GiftCdkC2S();
				if (msgCase_ == MsgOneofCase.GiftCdkC2S)
				{
					giftCdkC2S.MergeFrom(GiftCdkC2S);
				}
				input.ReadMessage(giftCdkC2S);
				GiftCdkC2S = giftCdkC2S;
				break;
			}
			case 41106u:
			{
				GiftCdkS2C giftCdkS2C = new GiftCdkS2C();
				if (msgCase_ == MsgOneofCase.GiftCdkS2C)
				{
					giftCdkS2C.MergeFrom(GiftCdkS2C);
				}
				input.ReadMessage(giftCdkS2C);
				GiftCdkS2C = giftCdkS2C;
				break;
			}
			case 41114u:
			{
				MailReadC2S mailReadC2S = new MailReadC2S();
				if (msgCase_ == MsgOneofCase.MailReadC2S)
				{
					mailReadC2S.MergeFrom(MailReadC2S);
				}
				input.ReadMessage(mailReadC2S);
				MailReadC2S = mailReadC2S;
				break;
			}
			case 41122u:
			{
				MailReadS2C mailReadS2C = new MailReadS2C();
				if (msgCase_ == MsgOneofCase.MailReadS2C)
				{
					mailReadS2C.MergeFrom(MailReadS2C);
				}
				input.ReadMessage(mailReadS2C);
				MailReadS2C = mailReadS2C;
				break;
			}
			case 41130u:
			{
				MailGetRewardC2S mailGetRewardC2S = new MailGetRewardC2S();
				if (msgCase_ == MsgOneofCase.MailGetRewardC2S)
				{
					mailGetRewardC2S.MergeFrom(MailGetRewardC2S);
				}
				input.ReadMessage(mailGetRewardC2S);
				MailGetRewardC2S = mailGetRewardC2S;
				break;
			}
			case 41138u:
			{
				MailGetRewardS2C mailGetRewardS2C = new MailGetRewardS2C();
				if (msgCase_ == MsgOneofCase.MailGetRewardS2C)
				{
					mailGetRewardS2C.MergeFrom(MailGetRewardS2C);
				}
				input.ReadMessage(mailGetRewardS2C);
				MailGetRewardS2C = mailGetRewardS2C;
				break;
			}
			case 41146u:
			{
				MailDelReadC2S mailDelReadC2S = new MailDelReadC2S();
				if (msgCase_ == MsgOneofCase.MailDelReadC2S)
				{
					mailDelReadC2S.MergeFrom(MailDelReadC2S);
				}
				input.ReadMessage(mailDelReadC2S);
				MailDelReadC2S = mailDelReadC2S;
				break;
			}
			case 41154u:
			{
				MailDelReadS2C mailDelReadS2C = new MailDelReadS2C();
				if (msgCase_ == MsgOneofCase.MailDelReadS2C)
				{
					mailDelReadS2C.MergeFrom(MailDelReadS2C);
				}
				input.ReadMessage(mailDelReadS2C);
				MailDelReadS2C = mailDelReadS2C;
				break;
			}
			case 41162u:
			{
				GachaRecordC2S gachaRecordC2S = new GachaRecordC2S();
				if (msgCase_ == MsgOneofCase.GachaRecordC2S)
				{
					gachaRecordC2S.MergeFrom(GachaRecordC2S);
				}
				input.ReadMessage(gachaRecordC2S);
				GachaRecordC2S = gachaRecordC2S;
				break;
			}
			case 41170u:
			{
				GachaRecordS2C gachaRecordS2C = new GachaRecordS2C();
				if (msgCase_ == MsgOneofCase.GachaRecordS2C)
				{
					gachaRecordS2C.MergeFrom(GachaRecordS2C);
				}
				input.ReadMessage(gachaRecordS2C);
				GachaRecordS2C = gachaRecordS2C;
				break;
			}
			case 41178u:
			{
				ActivityTaskRewardC2S activityTaskRewardC2S = new ActivityTaskRewardC2S();
				if (msgCase_ == MsgOneofCase.ActivityTaskRewardC2S)
				{
					activityTaskRewardC2S.MergeFrom(ActivityTaskRewardC2S);
				}
				input.ReadMessage(activityTaskRewardC2S);
				ActivityTaskRewardC2S = activityTaskRewardC2S;
				break;
			}
			case 41186u:
			{
				ActivityTaskRewardS2C activityTaskRewardS2C = new ActivityTaskRewardS2C();
				if (msgCase_ == MsgOneofCase.ActivityTaskRewardS2C)
				{
					activityTaskRewardS2C.MergeFrom(ActivityTaskRewardS2C);
				}
				input.ReadMessage(activityTaskRewardS2C);
				ActivityTaskRewardS2C = activityTaskRewardS2C;
				break;
			}
			case 41194u:
			{
				RoomShortChatC2S roomShortChatC2S = new RoomShortChatC2S();
				if (msgCase_ == MsgOneofCase.RoomShortChatC2S)
				{
					roomShortChatC2S.MergeFrom(RoomShortChatC2S);
				}
				input.ReadMessage(roomShortChatC2S);
				RoomShortChatC2S = roomShortChatC2S;
				break;
			}
			case 41202u:
			{
				RoomShortChatS2C roomShortChatS2C = new RoomShortChatS2C();
				if (msgCase_ == MsgOneofCase.RoomShortChatS2C)
				{
					roomShortChatS2C.MergeFrom(RoomShortChatS2C);
				}
				input.ReadMessage(roomShortChatS2C);
				RoomShortChatS2C = roomShortChatS2C;
				break;
			}
			case 41210u:
			{
				SetShowPlayerC2S setShowPlayerC2S = new SetShowPlayerC2S();
				if (msgCase_ == MsgOneofCase.SetShowPlayerC2S)
				{
					setShowPlayerC2S.MergeFrom(SetShowPlayerC2S);
				}
				input.ReadMessage(setShowPlayerC2S);
				SetShowPlayerC2S = setShowPlayerC2S;
				break;
			}
			case 41218u:
			{
				SetShowPlayerS2C setShowPlayerS2C = new SetShowPlayerS2C();
				if (msgCase_ == MsgOneofCase.SetShowPlayerS2C)
				{
					setShowPlayerS2C.MergeFrom(SetShowPlayerS2C);
				}
				input.ReadMessage(setShowPlayerS2C);
				SetShowPlayerS2C = setShowPlayerS2C;
				break;
			}
			case 41226u:
			{
				GetShowPlayerC2S getShowPlayerC2S = new GetShowPlayerC2S();
				if (msgCase_ == MsgOneofCase.GetShowPlayerC2S)
				{
					getShowPlayerC2S.MergeFrom(GetShowPlayerC2S);
				}
				input.ReadMessage(getShowPlayerC2S);
				GetShowPlayerC2S = getShowPlayerC2S;
				break;
			}
			case 41234u:
			{
				GetShowPlayerS2C getShowPlayerS2C = new GetShowPlayerS2C();
				if (msgCase_ == MsgOneofCase.GetShowPlayerS2C)
				{
					getShowPlayerS2C.MergeFrom(GetShowPlayerS2C);
				}
				input.ReadMessage(getShowPlayerS2C);
				GetShowPlayerS2C = getShowPlayerS2C;
				break;
			}
			case 41242u:
			{
				GetPlayerFightRecordC2S getPlayerFightRecordC2S = new GetPlayerFightRecordC2S();
				if (msgCase_ == MsgOneofCase.GetPlayerFightRecordC2S)
				{
					getPlayerFightRecordC2S.MergeFrom(GetPlayerFightRecordC2S);
				}
				input.ReadMessage(getPlayerFightRecordC2S);
				GetPlayerFightRecordC2S = getPlayerFightRecordC2S;
				break;
			}
			case 41250u:
			{
				GetPlayerFightRecordS2C getPlayerFightRecordS2C = new GetPlayerFightRecordS2C();
				if (msgCase_ == MsgOneofCase.GetPlayerFightRecordS2C)
				{
					getPlayerFightRecordS2C.MergeFrom(GetPlayerFightRecordS2C);
				}
				input.ReadMessage(getPlayerFightRecordS2C);
				GetPlayerFightRecordS2C = getPlayerFightRecordS2C;
				break;
			}
			case 41258u:
			{
				GetDay7RewardC2S getDay7RewardC2S = new GetDay7RewardC2S();
				if (msgCase_ == MsgOneofCase.GetDay7RewardC2S)
				{
					getDay7RewardC2S.MergeFrom(GetDay7RewardC2S);
				}
				input.ReadMessage(getDay7RewardC2S);
				GetDay7RewardC2S = getDay7RewardC2S;
				break;
			}
			case 41266u:
			{
				GetDay7RewardS2C getDay7RewardS2C = new GetDay7RewardS2C();
				if (msgCase_ == MsgOneofCase.GetDay7RewardS2C)
				{
					getDay7RewardS2C.MergeFrom(GetDay7RewardS2C);
				}
				input.ReadMessage(getDay7RewardS2C);
				GetDay7RewardS2C = getDay7RewardS2C;
				break;
			}
			case 41274u:
			{
				PraisePlayerC2S praisePlayerC2S = new PraisePlayerC2S();
				if (msgCase_ == MsgOneofCase.PraisePlayerC2S)
				{
					praisePlayerC2S.MergeFrom(PraisePlayerC2S);
				}
				input.ReadMessage(praisePlayerC2S);
				PraisePlayerC2S = praisePlayerC2S;
				break;
			}
			case 41282u:
			{
				PraisePlayerS2C praisePlayerS2C = new PraisePlayerS2C();
				if (msgCase_ == MsgOneofCase.PraisePlayerS2C)
				{
					praisePlayerS2C.MergeFrom(PraisePlayerS2C);
				}
				input.ReadMessage(praisePlayerS2C);
				PraisePlayerS2C = praisePlayerS2C;
				break;
			}
			case 41290u:
			{
				ClientDataUploadC2S clientDataUploadC2S = new ClientDataUploadC2S();
				if (msgCase_ == MsgOneofCase.ClientDataUploadC2S)
				{
					clientDataUploadC2S.MergeFrom(ClientDataUploadC2S);
				}
				input.ReadMessage(clientDataUploadC2S);
				ClientDataUploadC2S = clientDataUploadC2S;
				break;
			}
			case 41298u:
			{
				ClientDataUploadS2C clientDataUploadS2C = new ClientDataUploadS2C();
				if (msgCase_ == MsgOneofCase.ClientDataUploadS2C)
				{
					clientDataUploadS2C.MergeFrom(ClientDataUploadS2C);
				}
				input.ReadMessage(clientDataUploadS2C);
				ClientDataUploadS2C = clientDataUploadS2C;
				break;
			}
			case 41306u:
			{
				FriendListC2S friendListC2S = new FriendListC2S();
				if (msgCase_ == MsgOneofCase.FriendListC2S)
				{
					friendListC2S.MergeFrom(FriendListC2S);
				}
				input.ReadMessage(friendListC2S);
				FriendListC2S = friendListC2S;
				break;
			}
			case 41314u:
			{
				FriendListS2C friendListS2C = new FriendListS2C();
				if (msgCase_ == MsgOneofCase.FriendListS2C)
				{
					friendListS2C.MergeFrom(FriendListS2C);
				}
				input.ReadMessage(friendListS2C);
				FriendListS2C = friendListS2C;
				break;
			}
			case 41322u:
			{
				FriendApplyC2S friendApplyC2S = new FriendApplyC2S();
				if (msgCase_ == MsgOneofCase.FriendApplyC2S)
				{
					friendApplyC2S.MergeFrom(FriendApplyC2S);
				}
				input.ReadMessage(friendApplyC2S);
				FriendApplyC2S = friendApplyC2S;
				break;
			}
			case 41330u:
			{
				FriendApplyS2C friendApplyS2C = new FriendApplyS2C();
				if (msgCase_ == MsgOneofCase.FriendApplyS2C)
				{
					friendApplyS2C.MergeFrom(FriendApplyS2C);
				}
				input.ReadMessage(friendApplyS2C);
				FriendApplyS2C = friendApplyS2C;
				break;
			}
			case 41338u:
			{
				FriendApplyListC2S friendApplyListC2S = new FriendApplyListC2S();
				if (msgCase_ == MsgOneofCase.FriendApplyListC2S)
				{
					friendApplyListC2S.MergeFrom(FriendApplyListC2S);
				}
				input.ReadMessage(friendApplyListC2S);
				FriendApplyListC2S = friendApplyListC2S;
				break;
			}
			case 41346u:
			{
				FriendApplyListS2C friendApplyListS2C = new FriendApplyListS2C();
				if (msgCase_ == MsgOneofCase.FriendApplyListS2C)
				{
					friendApplyListS2C.MergeFrom(FriendApplyListS2C);
				}
				input.ReadMessage(friendApplyListS2C);
				FriendApplyListS2C = friendApplyListS2C;
				break;
			}
			case 41354u:
			{
				FriendApplyOpC2S friendApplyOpC2S = new FriendApplyOpC2S();
				if (msgCase_ == MsgOneofCase.FriendApplyOpC2S)
				{
					friendApplyOpC2S.MergeFrom(FriendApplyOpC2S);
				}
				input.ReadMessage(friendApplyOpC2S);
				FriendApplyOpC2S = friendApplyOpC2S;
				break;
			}
			case 41362u:
			{
				FriendApplyOpS2C friendApplyOpS2C = new FriendApplyOpS2C();
				if (msgCase_ == MsgOneofCase.FriendApplyOpS2C)
				{
					friendApplyOpS2C.MergeFrom(FriendApplyOpS2C);
				}
				input.ReadMessage(friendApplyOpS2C);
				FriendApplyOpS2C = friendApplyOpS2C;
				break;
			}
			case 41370u:
			{
				FriendOpC2S friendOpC2S = new FriendOpC2S();
				if (msgCase_ == MsgOneofCase.FriendOpC2S)
				{
					friendOpC2S.MergeFrom(FriendOpC2S);
				}
				input.ReadMessage(friendOpC2S);
				FriendOpC2S = friendOpC2S;
				break;
			}
			case 41378u:
			{
				FriendOpS2C friendOpS2C = new FriendOpS2C();
				if (msgCase_ == MsgOneofCase.FriendOpS2C)
				{
					friendOpS2C.MergeFrom(FriendOpS2C);
				}
				input.ReadMessage(friendOpS2C);
				FriendOpS2C = friendOpS2C;
				break;
			}
			case 41386u:
			{
				FriendInviteC2S friendInviteC2S = new FriendInviteC2S();
				if (msgCase_ == MsgOneofCase.FriendInviteC2S)
				{
					friendInviteC2S.MergeFrom(FriendInviteC2S);
				}
				input.ReadMessage(friendInviteC2S);
				FriendInviteC2S = friendInviteC2S;
				break;
			}
			case 41394u:
			{
				FriendInviteS2C friendInviteS2C = new FriendInviteS2C();
				if (msgCase_ == MsgOneofCase.FriendInviteS2C)
				{
					friendInviteS2C.MergeFrom(FriendInviteS2C);
				}
				input.ReadMessage(friendInviteS2C);
				FriendInviteS2C = friendInviteS2C;
				break;
			}
			case 41402u:
			{
				FriendInviteListC2S friendInviteListC2S = new FriendInviteListC2S();
				if (msgCase_ == MsgOneofCase.FriendInviteListC2S)
				{
					friendInviteListC2S.MergeFrom(FriendInviteListC2S);
				}
				input.ReadMessage(friendInviteListC2S);
				FriendInviteListC2S = friendInviteListC2S;
				break;
			}
			case 41410u:
			{
				FriendInviteListS2C friendInviteListS2C = new FriendInviteListS2C();
				if (msgCase_ == MsgOneofCase.FriendInviteListS2C)
				{
					friendInviteListS2C.MergeFrom(FriendInviteListS2C);
				}
				input.ReadMessage(friendInviteListS2C);
				FriendInviteListS2C = friendInviteListS2C;
				break;
			}
			case 41434u:
			{
				FriendInviteCleanC2S friendInviteCleanC2S = new FriendInviteCleanC2S();
				if (msgCase_ == MsgOneofCase.FriendInviteCleanC2S)
				{
					friendInviteCleanC2S.MergeFrom(FriendInviteCleanC2S);
				}
				input.ReadMessage(friendInviteCleanC2S);
				FriendInviteCleanC2S = friendInviteCleanC2S;
				break;
			}
			case 41442u:
			{
				FriendInviteCleanS2C friendInviteCleanS2C = new FriendInviteCleanS2C();
				if (msgCase_ == MsgOneofCase.FriendInviteCleanS2C)
				{
					friendInviteCleanS2C.MergeFrom(FriendInviteCleanS2C);
				}
				input.ReadMessage(friendInviteCleanS2C);
				FriendInviteCleanS2C = friendInviteCleanS2C;
				break;
			}
			case 41450u:
			{
				FriendBlacksListC2S friendBlacksListC2S = new FriendBlacksListC2S();
				if (msgCase_ == MsgOneofCase.FriendBlacksListC2S)
				{
					friendBlacksListC2S.MergeFrom(FriendBlacksListC2S);
				}
				input.ReadMessage(friendBlacksListC2S);
				FriendBlacksListC2S = friendBlacksListC2S;
				break;
			}
			case 41458u:
			{
				FriendBlacksListS2C friendBlacksListS2C = new FriendBlacksListS2C();
				if (msgCase_ == MsgOneofCase.FriendBlacksListS2C)
				{
					friendBlacksListS2C.MergeFrom(FriendBlacksListS2C);
				}
				input.ReadMessage(friendBlacksListS2C);
				FriendBlacksListS2C = friendBlacksListS2C;
				break;
			}
			case 41466u:
			{
				NearFightPlayerC2S nearFightPlayerC2S = new NearFightPlayerC2S();
				if (msgCase_ == MsgOneofCase.NearFightPlayerC2S)
				{
					nearFightPlayerC2S.MergeFrom(NearFightPlayerC2S);
				}
				input.ReadMessage(nearFightPlayerC2S);
				NearFightPlayerC2S = nearFightPlayerC2S;
				break;
			}
			case 41474u:
			{
				NearFightPlayerS2C nearFightPlayerS2C = new NearFightPlayerS2C();
				if (msgCase_ == MsgOneofCase.NearFightPlayerS2C)
				{
					nearFightPlayerS2C.MergeFrom(NearFightPlayerS2C);
				}
				input.ReadMessage(nearFightPlayerS2C);
				NearFightPlayerS2C = nearFightPlayerS2C;
				break;
			}
			case 41482u:
			{
				SearchPlayerC2S searchPlayerC2S = new SearchPlayerC2S();
				if (msgCase_ == MsgOneofCase.SearchPlayerC2S)
				{
					searchPlayerC2S.MergeFrom(SearchPlayerC2S);
				}
				input.ReadMessage(searchPlayerC2S);
				SearchPlayerC2S = searchPlayerC2S;
				break;
			}
			case 41490u:
			{
				SearchPlayerS2C searchPlayerS2C = new SearchPlayerS2C();
				if (msgCase_ == MsgOneofCase.SearchPlayerS2C)
				{
					searchPlayerS2C.MergeFrom(SearchPlayerS2C);
				}
				input.ReadMessage(searchPlayerS2C);
				SearchPlayerS2C = searchPlayerS2C;
				break;
			}
			case 41498u:
			{
				ScratchCardC2S scratchCardC2S = new ScratchCardC2S();
				if (msgCase_ == MsgOneofCase.ScratchCardC2S)
				{
					scratchCardC2S.MergeFrom(ScratchCardC2S);
				}
				input.ReadMessage(scratchCardC2S);
				ScratchCardC2S = scratchCardC2S;
				break;
			}
			case 41506u:
			{
				ScratchCardS2C scratchCardS2C = new ScratchCardS2C();
				if (msgCase_ == MsgOneofCase.ScratchCardS2C)
				{
					scratchCardS2C.MergeFrom(ScratchCardS2C);
				}
				input.ReadMessage(scratchCardS2C);
				ScratchCardS2C = scratchCardS2C;
				break;
			}
			case 41514u:
			{
				NextScratchCardPoolC2S nextScratchCardPoolC2S = new NextScratchCardPoolC2S();
				if (msgCase_ == MsgOneofCase.NextScratchCardPoolC2S)
				{
					nextScratchCardPoolC2S.MergeFrom(NextScratchCardPoolC2S);
				}
				input.ReadMessage(nextScratchCardPoolC2S);
				NextScratchCardPoolC2S = nextScratchCardPoolC2S;
				break;
			}
			case 41522u:
			{
				NextScratchCardPoolS2C nextScratchCardPoolS2C = new NextScratchCardPoolS2C();
				if (msgCase_ == MsgOneofCase.NextScratchCardPoolS2C)
				{
					nextScratchCardPoolS2C.MergeFrom(NextScratchCardPoolS2C);
				}
				input.ReadMessage(nextScratchCardPoolS2C);
				NextScratchCardPoolS2C = nextScratchCardPoolS2C;
				break;
			}
			case 41530u:
			{
				WatchJoinRoomC2S watchJoinRoomC2S = new WatchJoinRoomC2S();
				if (msgCase_ == MsgOneofCase.WatchJoinRoomC2S)
				{
					watchJoinRoomC2S.MergeFrom(WatchJoinRoomC2S);
				}
				input.ReadMessage(watchJoinRoomC2S);
				WatchJoinRoomC2S = watchJoinRoomC2S;
				break;
			}
			case 41538u:
			{
				WatchJoinRoomS2C watchJoinRoomS2C = new WatchJoinRoomS2C();
				if (msgCase_ == MsgOneofCase.WatchJoinRoomS2C)
				{
					watchJoinRoomS2C.MergeFrom(WatchJoinRoomS2C);
				}
				input.ReadMessage(watchJoinRoomS2C);
				WatchJoinRoomS2C = watchJoinRoomS2C;
				break;
			}
			case 41546u:
			{
				WatchRefreshRoomStateC2S watchRefreshRoomStateC2S = new WatchRefreshRoomStateC2S();
				if (msgCase_ == MsgOneofCase.WatchRefreshRoomStateC2S)
				{
					watchRefreshRoomStateC2S.MergeFrom(WatchRefreshRoomStateC2S);
				}
				input.ReadMessage(watchRefreshRoomStateC2S);
				WatchRefreshRoomStateC2S = watchRefreshRoomStateC2S;
				break;
			}
			case 41554u:
			{
				WatchRefreshRoomStateS2C watchRefreshRoomStateS2C = new WatchRefreshRoomStateS2C();
				if (msgCase_ == MsgOneofCase.WatchRefreshRoomStateS2C)
				{
					watchRefreshRoomStateS2C.MergeFrom(WatchRefreshRoomStateS2C);
				}
				input.ReadMessage(watchRefreshRoomStateS2C);
				WatchRefreshRoomStateS2C = watchRefreshRoomStateS2C;
				break;
			}
			case 41562u:
			{
				WatchExitRoomC2S watchExitRoomC2S = new WatchExitRoomC2S();
				if (msgCase_ == MsgOneofCase.WatchExitRoomC2S)
				{
					watchExitRoomC2S.MergeFrom(WatchExitRoomC2S);
				}
				input.ReadMessage(watchExitRoomC2S);
				WatchExitRoomC2S = watchExitRoomC2S;
				break;
			}
			case 41570u:
			{
				WatchExitRoomS2C watchExitRoomS2C = new WatchExitRoomS2C();
				if (msgCase_ == MsgOneofCase.WatchExitRoomS2C)
				{
					watchExitRoomS2C.MergeFrom(WatchExitRoomS2C);
				}
				input.ReadMessage(watchExitRoomS2C);
				WatchExitRoomS2C = watchExitRoomS2C;
				break;
			}
			case 41578u:
			{
				BattlePassGetRewardC2S battlePassGetRewardC2S = new BattlePassGetRewardC2S();
				if (msgCase_ == MsgOneofCase.BattlePassGetRewardC2S)
				{
					battlePassGetRewardC2S.MergeFrom(BattlePassGetRewardC2S);
				}
				input.ReadMessage(battlePassGetRewardC2S);
				BattlePassGetRewardC2S = battlePassGetRewardC2S;
				break;
			}
			case 41586u:
			{
				BattlePassGetRewardS2C battlePassGetRewardS2C = new BattlePassGetRewardS2C();
				if (msgCase_ == MsgOneofCase.BattlePassGetRewardS2C)
				{
					battlePassGetRewardS2C.MergeFrom(BattlePassGetRewardS2C);
				}
				input.ReadMessage(battlePassGetRewardS2C);
				BattlePassGetRewardS2C = battlePassGetRewardS2C;
				break;
			}
			case 41594u:
			{
				BattlePassTaskRewardC2S battlePassTaskRewardC2S = new BattlePassTaskRewardC2S();
				if (msgCase_ == MsgOneofCase.BattlePassTaskRewardC2S)
				{
					battlePassTaskRewardC2S.MergeFrom(BattlePassTaskRewardC2S);
				}
				input.ReadMessage(battlePassTaskRewardC2S);
				BattlePassTaskRewardC2S = battlePassTaskRewardC2S;
				break;
			}
			case 41602u:
			{
				BattlePassTaskRewardS2C battlePassTaskRewardS2C = new BattlePassTaskRewardS2C();
				if (msgCase_ == MsgOneofCase.BattlePassTaskRewardS2C)
				{
					battlePassTaskRewardS2C.MergeFrom(BattlePassTaskRewardS2C);
				}
				input.ReadMessage(battlePassTaskRewardS2C);
				BattlePassTaskRewardS2C = battlePassTaskRewardS2C;
				break;
			}
			case 41610u:
			{
				BattlePassUpLvC2S battlePassUpLvC2S = new BattlePassUpLvC2S();
				if (msgCase_ == MsgOneofCase.BattlePassUpLvC2S)
				{
					battlePassUpLvC2S.MergeFrom(BattlePassUpLvC2S);
				}
				input.ReadMessage(battlePassUpLvC2S);
				BattlePassUpLvC2S = battlePassUpLvC2S;
				break;
			}
			case 41618u:
			{
				BattlePassUpLvS2C battlePassUpLvS2C = new BattlePassUpLvS2C();
				if (msgCase_ == MsgOneofCase.BattlePassUpLvS2C)
				{
					battlePassUpLvS2C.MergeFrom(BattlePassUpLvS2C);
				}
				input.ReadMessage(battlePassUpLvS2C);
				BattlePassUpLvS2C = battlePassUpLvS2C;
				break;
			}
			case 41626u:
			{
				FriendSendMsgC2S friendSendMsgC2S = new FriendSendMsgC2S();
				if (msgCase_ == MsgOneofCase.FriendSendMsgC2S)
				{
					friendSendMsgC2S.MergeFrom(FriendSendMsgC2S);
				}
				input.ReadMessage(friendSendMsgC2S);
				FriendSendMsgC2S = friendSendMsgC2S;
				break;
			}
			case 41634u:
			{
				FriendSendMsgS2C friendSendMsgS2C = new FriendSendMsgS2C();
				if (msgCase_ == MsgOneofCase.FriendSendMsgS2C)
				{
					friendSendMsgS2C.MergeFrom(FriendSendMsgS2C);
				}
				input.ReadMessage(friendSendMsgS2C);
				FriendSendMsgS2C = friendSendMsgS2C;
				break;
			}
			case 41642u:
			{
				GetChatMsgC2S getChatMsgC2S = new GetChatMsgC2S();
				if (msgCase_ == MsgOneofCase.GetChatMsgC2S)
				{
					getChatMsgC2S.MergeFrom(GetChatMsgC2S);
				}
				input.ReadMessage(getChatMsgC2S);
				GetChatMsgC2S = getChatMsgC2S;
				break;
			}
			case 41650u:
			{
				GetChatMsgS2C getChatMsgS2C = new GetChatMsgS2C();
				if (msgCase_ == MsgOneofCase.GetChatMsgS2C)
				{
					getChatMsgS2C.MergeFrom(GetChatMsgS2C);
				}
				input.ReadMessage(getChatMsgS2C);
				GetChatMsgS2C = getChatMsgS2C;
				break;
			}
			case 41658u:
			{
				ReadChatMsgC2S readChatMsgC2S = new ReadChatMsgC2S();
				if (msgCase_ == MsgOneofCase.ReadChatMsgC2S)
				{
					readChatMsgC2S.MergeFrom(ReadChatMsgC2S);
				}
				input.ReadMessage(readChatMsgC2S);
				ReadChatMsgC2S = readChatMsgC2S;
				break;
			}
			case 41666u:
			{
				ReadChatMsgS2C readChatMsgS2C = new ReadChatMsgS2C();
				if (msgCase_ == MsgOneofCase.ReadChatMsgS2C)
				{
					readChatMsgS2C.MergeFrom(ReadChatMsgS2C);
				}
				input.ReadMessage(readChatMsgS2C);
				ReadChatMsgS2C = readChatMsgS2C;
				break;
			}
			case 41674u:
			{
				DelChatMsgInfoC2S delChatMsgInfoC2S = new DelChatMsgInfoC2S();
				if (msgCase_ == MsgOneofCase.DelChatMsgInfoC2S)
				{
					delChatMsgInfoC2S.MergeFrom(DelChatMsgInfoC2S);
				}
				input.ReadMessage(delChatMsgInfoC2S);
				DelChatMsgInfoC2S = delChatMsgInfoC2S;
				break;
			}
			case 41682u:
			{
				DelChatMsgInfoS2C delChatMsgInfoS2C = new DelChatMsgInfoS2C();
				if (msgCase_ == MsgOneofCase.DelChatMsgInfoS2C)
				{
					delChatMsgInfoS2C.MergeFrom(DelChatMsgInfoS2C);
				}
				input.ReadMessage(delChatMsgInfoS2C);
				DelChatMsgInfoS2C = delChatMsgInfoS2C;
				break;
			}
			case 41690u:
			{
				SelectRelicC2S selectRelicC2S = new SelectRelicC2S();
				if (msgCase_ == MsgOneofCase.SelectRelicC2S)
				{
					selectRelicC2S.MergeFrom(SelectRelicC2S);
				}
				input.ReadMessage(selectRelicC2S);
				SelectRelicC2S = selectRelicC2S;
				break;
			}
			case 41698u:
			{
				SelectRelicS2C selectRelicS2C = new SelectRelicS2C();
				if (msgCase_ == MsgOneofCase.SelectRelicS2C)
				{
					selectRelicS2C.MergeFrom(SelectRelicS2C);
				}
				input.ReadMessage(selectRelicS2C);
				SelectRelicS2C = selectRelicS2C;
				break;
			}
			case 41706u:
			{
				MonsterPursuitC2S monsterPursuitC2S = new MonsterPursuitC2S();
				if (msgCase_ == MsgOneofCase.MonsterPursuitC2S)
				{
					monsterPursuitC2S.MergeFrom(MonsterPursuitC2S);
				}
				input.ReadMessage(monsterPursuitC2S);
				MonsterPursuitC2S = monsterPursuitC2S;
				break;
			}
			case 41714u:
			{
				MonsterPursuitS2C monsterPursuitS2C = new MonsterPursuitS2C();
				if (msgCase_ == MsgOneofCase.MonsterPursuitS2C)
				{
					monsterPursuitS2C.MergeFrom(MonsterPursuitS2C);
				}
				input.ReadMessage(monsterPursuitS2C);
				MonsterPursuitS2C = monsterPursuitS2C;
				break;
			}
			case 41722u:
			{
				PVEShopBuyC2S pVEShopBuyC2S = new PVEShopBuyC2S();
				if (msgCase_ == MsgOneofCase.PVEShopBuyC2S)
				{
					pVEShopBuyC2S.MergeFrom(PVEShopBuyC2S);
				}
				input.ReadMessage(pVEShopBuyC2S);
				PVEShopBuyC2S = pVEShopBuyC2S;
				break;
			}
			case 41730u:
			{
				PVEShopBuyS2C pVEShopBuyS2C = new PVEShopBuyS2C();
				if (msgCase_ == MsgOneofCase.PVEShopBuyS2C)
				{
					pVEShopBuyS2C.MergeFrom(PVEShopBuyS2C);
				}
				input.ReadMessage(pVEShopBuyS2C);
				PVEShopBuyS2C = pVEShopBuyS2C;
				break;
			}
			case 41738u:
			{
				ClientCheckTaskC2S clientCheckTaskC2S = new ClientCheckTaskC2S();
				if (msgCase_ == MsgOneofCase.ClientCheckTaskC2S)
				{
					clientCheckTaskC2S.MergeFrom(ClientCheckTaskC2S);
				}
				input.ReadMessage(clientCheckTaskC2S);
				ClientCheckTaskC2S = clientCheckTaskC2S;
				break;
			}
			case 41746u:
			{
				ClientCheckTaskS2C clientCheckTaskS2C = new ClientCheckTaskS2C();
				if (msgCase_ == MsgOneofCase.ClientCheckTaskS2C)
				{
					clientCheckTaskS2C.MergeFrom(ClientCheckTaskS2C);
				}
				input.ReadMessage(clientCheckTaskS2C);
				ClientCheckTaskS2C = clientCheckTaskS2C;
				break;
			}
			case 41754u:
			{
				PveHeroUpLvC2S pveHeroUpLvC2S = new PveHeroUpLvC2S();
				if (msgCase_ == MsgOneofCase.PveHeroUpLvC2S)
				{
					pveHeroUpLvC2S.MergeFrom(PveHeroUpLvC2S);
				}
				input.ReadMessage(pveHeroUpLvC2S);
				PveHeroUpLvC2S = pveHeroUpLvC2S;
				break;
			}
			case 41762u:
			{
				PveHeroUpLvS2C pveHeroUpLvS2C = new PveHeroUpLvS2C();
				if (msgCase_ == MsgOneofCase.PveHeroUpLvS2C)
				{
					pveHeroUpLvS2C.MergeFrom(PveHeroUpLvS2C);
				}
				input.ReadMessage(pveHeroUpLvS2C);
				PveHeroUpLvS2C = pveHeroUpLvS2C;
				break;
			}
			case 41770u:
			{
				StartMatchC2S startMatchC2S = new StartMatchC2S();
				if (msgCase_ == MsgOneofCase.StartMatchC2S)
				{
					startMatchC2S.MergeFrom(StartMatchC2S);
				}
				input.ReadMessage(startMatchC2S);
				StartMatchC2S = startMatchC2S;
				break;
			}
			case 41778u:
			{
				StartMatchS2C startMatchS2C = new StartMatchS2C();
				if (msgCase_ == MsgOneofCase.StartMatchS2C)
				{
					startMatchS2C.MergeFrom(StartMatchS2C);
				}
				input.ReadMessage(startMatchS2C);
				StartMatchS2C = startMatchS2C;
				break;
			}
			case 41786u:
			{
				CancelMatchC2S cancelMatchC2S = new CancelMatchC2S();
				if (msgCase_ == MsgOneofCase.CancelMatchC2S)
				{
					cancelMatchC2S.MergeFrom(CancelMatchC2S);
				}
				input.ReadMessage(cancelMatchC2S);
				CancelMatchC2S = cancelMatchC2S;
				break;
			}
			case 41794u:
			{
				CancelMatchS2C cancelMatchS2C = new CancelMatchS2C();
				if (msgCase_ == MsgOneofCase.CancelMatchS2C)
				{
					cancelMatchS2C.MergeFrom(CancelMatchS2C);
				}
				input.ReadMessage(cancelMatchS2C);
				CancelMatchS2C = cancelMatchS2C;
				break;
			}
			case 41802u:
			{
				MatchSuccessC2S matchSuccessC2S = new MatchSuccessC2S();
				if (msgCase_ == MsgOneofCase.MatchSuccessC2S)
				{
					matchSuccessC2S.MergeFrom(MatchSuccessC2S);
				}
				input.ReadMessage(matchSuccessC2S);
				MatchSuccessC2S = matchSuccessC2S;
				break;
			}
			case 41810u:
			{
				MatchSuccessS2C matchSuccessS2C = new MatchSuccessS2C();
				if (msgCase_ == MsgOneofCase.MatchSuccessS2C)
				{
					matchSuccessS2C.MergeFrom(MatchSuccessS2C);
				}
				input.ReadMessage(matchSuccessS2C);
				MatchSuccessS2C = matchSuccessS2C;
				break;
			}
			case 41818u:
			{
				AccuseC2S accuseC2S = new AccuseC2S();
				if (msgCase_ == MsgOneofCase.AccuseC2S)
				{
					accuseC2S.MergeFrom(AccuseC2S);
				}
				input.ReadMessage(accuseC2S);
				AccuseC2S = accuseC2S;
				break;
			}
			case 41826u:
			{
				AccuseS2C accuseS2C = new AccuseS2C();
				if (msgCase_ == MsgOneofCase.AccuseS2C)
				{
					accuseS2C.MergeFrom(AccuseS2C);
				}
				input.ReadMessage(accuseS2C);
				AccuseS2C = accuseS2C;
				break;
			}
			case 41834u:
			{
				SingleCampaignC2S singleCampaignC2S = new SingleCampaignC2S();
				if (msgCase_ == MsgOneofCase.SingleCampaignC2S)
				{
					singleCampaignC2S.MergeFrom(SingleCampaignC2S);
				}
				input.ReadMessage(singleCampaignC2S);
				SingleCampaignC2S = singleCampaignC2S;
				break;
			}
			case 41842u:
			{
				SingleCampaignS2C singleCampaignS2C = new SingleCampaignS2C();
				if (msgCase_ == MsgOneofCase.SingleCampaignS2C)
				{
					singleCampaignS2C.MergeFrom(SingleCampaignS2C);
				}
				input.ReadMessage(singleCampaignS2C);
				SingleCampaignS2C = singleCampaignS2C;
				break;
			}
			case 41850u:
			{
				DevChargeC2S devChargeC2S = new DevChargeC2S();
				if (msgCase_ == MsgOneofCase.DevChargeC2S)
				{
					devChargeC2S.MergeFrom(DevChargeC2S);
				}
				input.ReadMessage(devChargeC2S);
				DevChargeC2S = devChargeC2S;
				break;
			}
			case 41858u:
			{
				DevChargeS2C devChargeS2C = new DevChargeS2C();
				if (msgCase_ == MsgOneofCase.DevChargeS2C)
				{
					devChargeS2C.MergeFrom(DevChargeS2C);
				}
				input.ReadMessage(devChargeS2C);
				DevChargeS2C = devChargeS2C;
				break;
			}
			case 41866u:
			{
				AskReviveTeammateC2S askReviveTeammateC2S = new AskReviveTeammateC2S();
				if (msgCase_ == MsgOneofCase.AskReviveTeammateC2S)
				{
					askReviveTeammateC2S.MergeFrom(AskReviveTeammateC2S);
				}
				input.ReadMessage(askReviveTeammateC2S);
				AskReviveTeammateC2S = askReviveTeammateC2S;
				break;
			}
			case 41874u:
			{
				AskReviveTeammateS2C askReviveTeammateS2C = new AskReviveTeammateS2C();
				if (msgCase_ == MsgOneofCase.AskReviveTeammateS2C)
				{
					askReviveTeammateS2C.MergeFrom(AskReviveTeammateS2C);
				}
				input.ReadMessage(askReviveTeammateS2C);
				AskReviveTeammateS2C = askReviveTeammateS2C;
				break;
			}
			case 41882u:
			{
				GetSignInRewardC2S getSignInRewardC2S = new GetSignInRewardC2S();
				if (msgCase_ == MsgOneofCase.GetSignInRewardC2S)
				{
					getSignInRewardC2S.MergeFrom(GetSignInRewardC2S);
				}
				input.ReadMessage(getSignInRewardC2S);
				GetSignInRewardC2S = getSignInRewardC2S;
				break;
			}
			case 41890u:
			{
				GetSignInRewardS2C getSignInRewardS2C = new GetSignInRewardS2C();
				if (msgCase_ == MsgOneofCase.GetSignInRewardS2C)
				{
					getSignInRewardS2C.MergeFrom(GetSignInRewardS2C);
				}
				input.ReadMessage(getSignInRewardS2C);
				GetSignInRewardS2C = getSignInRewardS2C;
				break;
			}
			case 41898u:
			{
				ChatMapMarkersC2S chatMapMarkersC2S = new ChatMapMarkersC2S();
				if (msgCase_ == MsgOneofCase.ChatMapMarkersC2S)
				{
					chatMapMarkersC2S.MergeFrom(ChatMapMarkersC2S);
				}
				input.ReadMessage(chatMapMarkersC2S);
				ChatMapMarkersC2S = chatMapMarkersC2S;
				break;
			}
			case 41906u:
			{
				ChatMapMarkersS2C chatMapMarkersS2C = new ChatMapMarkersS2C();
				if (msgCase_ == MsgOneofCase.ChatMapMarkersS2C)
				{
					chatMapMarkersS2C.MergeFrom(ChatMapMarkersS2C);
				}
				input.ReadMessage(chatMapMarkersS2C);
				ChatMapMarkersS2C = chatMapMarkersS2C;
				break;
			}
			case 41914u:
			{
				AbroadCreateOrderC2S abroadCreateOrderC2S = new AbroadCreateOrderC2S();
				if (msgCase_ == MsgOneofCase.AbroadCreateOrderC2S)
				{
					abroadCreateOrderC2S.MergeFrom(AbroadCreateOrderC2S);
				}
				input.ReadMessage(abroadCreateOrderC2S);
				AbroadCreateOrderC2S = abroadCreateOrderC2S;
				break;
			}
			case 41922u:
			{
				AbroadCreateOrderS2C abroadCreateOrderS2C = new AbroadCreateOrderS2C();
				if (msgCase_ == MsgOneofCase.AbroadCreateOrderS2C)
				{
					abroadCreateOrderS2C.MergeFrom(AbroadCreateOrderS2C);
				}
				input.ReadMessage(abroadCreateOrderS2C);
				AbroadCreateOrderS2C = abroadCreateOrderS2C;
				break;
			}
			case 41946u:
			{
				AgeVerifyC2S ageVerifyC2S = new AgeVerifyC2S();
				if (msgCase_ == MsgOneofCase.AgeVerifyC2S)
				{
					ageVerifyC2S.MergeFrom(AgeVerifyC2S);
				}
				input.ReadMessage(ageVerifyC2S);
				AgeVerifyC2S = ageVerifyC2S;
				break;
			}
			case 41954u:
			{
				AgeVerifyS2C ageVerifyS2C = new AgeVerifyS2C();
				if (msgCase_ == MsgOneofCase.AgeVerifyS2C)
				{
					ageVerifyS2C.MergeFrom(AgeVerifyS2C);
				}
				input.ReadMessage(ageVerifyS2C);
				AgeVerifyS2C = ageVerifyS2C;
				break;
			}
			case 41962u:
			{
				ChangeNameC2S changeNameC2S = new ChangeNameC2S();
				if (msgCase_ == MsgOneofCase.ChangeNameC2S)
				{
					changeNameC2S.MergeFrom(ChangeNameC2S);
				}
				input.ReadMessage(changeNameC2S);
				ChangeNameC2S = changeNameC2S;
				break;
			}
			case 41970u:
			{
				ChangeNameS2C changeNameS2C = new ChangeNameS2C();
				if (msgCase_ == MsgOneofCase.ChangeNameS2C)
				{
					changeNameS2C.MergeFrom(ChangeNameS2C);
				}
				input.ReadMessage(changeNameS2C);
				ChangeNameS2C = changeNameS2C;
				break;
			}
			case 41978u:
			{
				ClientClickConfirmTaskC2S clientClickConfirmTaskC2S = new ClientClickConfirmTaskC2S();
				if (msgCase_ == MsgOneofCase.ClientClickConfirmTaskC2S)
				{
					clientClickConfirmTaskC2S.MergeFrom(ClientClickConfirmTaskC2S);
				}
				input.ReadMessage(clientClickConfirmTaskC2S);
				ClientClickConfirmTaskC2S = clientClickConfirmTaskC2S;
				break;
			}
			case 41986u:
			{
				ClientClickConfirmTaskS2C clientClickConfirmTaskS2C = new ClientClickConfirmTaskS2C();
				if (msgCase_ == MsgOneofCase.ClientClickConfirmTaskS2C)
				{
					clientClickConfirmTaskS2C.MergeFrom(ClientClickConfirmTaskS2C);
				}
				input.ReadMessage(clientClickConfirmTaskS2C);
				ClientClickConfirmTaskS2C = clientClickConfirmTaskS2C;
				break;
			}
			case 41994u:
			{
				BuyRelicC2S buyRelicC2S = new BuyRelicC2S();
				if (msgCase_ == MsgOneofCase.BuyRelicC2S)
				{
					buyRelicC2S.MergeFrom(BuyRelicC2S);
				}
				input.ReadMessage(buyRelicC2S);
				BuyRelicC2S = buyRelicC2S;
				break;
			}
			case 42002u:
			{
				BuyRelicS2C buyRelicS2C = new BuyRelicS2C();
				if (msgCase_ == MsgOneofCase.BuyRelicS2C)
				{
					buyRelicS2C.MergeFrom(BuyRelicS2C);
				}
				input.ReadMessage(buyRelicS2C);
				BuyRelicS2C = buyRelicS2C;
				break;
			}
			case 42010u:
			{
				BuyLightGiftC2S buyLightGiftC2S = new BuyLightGiftC2S();
				if (msgCase_ == MsgOneofCase.BuyLightGiftC2S)
				{
					buyLightGiftC2S.MergeFrom(BuyLightGiftC2S);
				}
				input.ReadMessage(buyLightGiftC2S);
				BuyLightGiftC2S = buyLightGiftC2S;
				break;
			}
			case 42018u:
			{
				BuyLightGiftS2C buyLightGiftS2C = new BuyLightGiftS2C();
				if (msgCase_ == MsgOneofCase.BuyLightGiftS2C)
				{
					buyLightGiftS2C.MergeFrom(BuyLightGiftS2C);
				}
				input.ReadMessage(buyLightGiftS2C);
				BuyLightGiftS2C = buyLightGiftS2C;
				break;
			}
			case 42026u:
			{
				LightGiftC2S lightGiftC2S = new LightGiftC2S();
				if (msgCase_ == MsgOneofCase.LightGiftC2S)
				{
					lightGiftC2S.MergeFrom(LightGiftC2S);
				}
				input.ReadMessage(lightGiftC2S);
				LightGiftC2S = lightGiftC2S;
				break;
			}
			case 42034u:
			{
				LightGiftS2C lightGiftS2C = new LightGiftS2C();
				if (msgCase_ == MsgOneofCase.LightGiftS2C)
				{
					lightGiftS2C.MergeFrom(LightGiftS2C);
				}
				input.ReadMessage(lightGiftS2C);
				LightGiftS2C = lightGiftS2C;
				break;
			}
			case 42042u:
			{
				AcquisitionC2S acquisitionC2S = new AcquisitionC2S();
				if (msgCase_ == MsgOneofCase.AcquisitionC2S)
				{
					acquisitionC2S.MergeFrom(AcquisitionC2S);
				}
				input.ReadMessage(acquisitionC2S);
				AcquisitionC2S = acquisitionC2S;
				break;
			}
			case 42050u:
			{
				AcquisitionS2C acquisitionS2C = new AcquisitionS2C();
				if (msgCase_ == MsgOneofCase.AcquisitionS2C)
				{
					acquisitionS2C.MergeFrom(AcquisitionS2C);
				}
				input.ReadMessage(acquisitionS2C);
				AcquisitionS2C = acquisitionS2C;
				break;
			}
			case 42058u:
			{
				AcquisitionRewardC2S acquisitionRewardC2S = new AcquisitionRewardC2S();
				if (msgCase_ == MsgOneofCase.AcquisitionRewardC2S)
				{
					acquisitionRewardC2S.MergeFrom(AcquisitionRewardC2S);
				}
				input.ReadMessage(acquisitionRewardC2S);
				AcquisitionRewardC2S = acquisitionRewardC2S;
				break;
			}
			case 42066u:
			{
				AcquisitionRewardS2C acquisitionRewardS2C = new AcquisitionRewardS2C();
				if (msgCase_ == MsgOneofCase.AcquisitionRewardS2C)
				{
					acquisitionRewardS2C.MergeFrom(AcquisitionRewardS2C);
				}
				input.ReadMessage(acquisitionRewardS2C);
				AcquisitionRewardS2C = acquisitionRewardS2C;
				break;
			}
			case 42074u:
			{
				SelectMechanismC2S selectMechanismC2S = new SelectMechanismC2S();
				if (msgCase_ == MsgOneofCase.SelectMechanismC2S)
				{
					selectMechanismC2S.MergeFrom(SelectMechanismC2S);
				}
				input.ReadMessage(selectMechanismC2S);
				SelectMechanismC2S = selectMechanismC2S;
				break;
			}
			case 42082u:
			{
				SelectMechanismS2C selectMechanismS2C = new SelectMechanismS2C();
				if (msgCase_ == MsgOneofCase.SelectMechanismS2C)
				{
					selectMechanismS2C.MergeFrom(SelectMechanismS2C);
				}
				input.ReadMessage(selectMechanismS2C);
				SelectMechanismS2C = selectMechanismS2C;
				break;
			}
			case 42090u:
			{
				GachaCountRewardC2S gachaCountRewardC2S = new GachaCountRewardC2S();
				if (msgCase_ == MsgOneofCase.GachaCountRewardC2S)
				{
					gachaCountRewardC2S.MergeFrom(GachaCountRewardC2S);
				}
				input.ReadMessage(gachaCountRewardC2S);
				GachaCountRewardC2S = gachaCountRewardC2S;
				break;
			}
			case 42098u:
			{
				GachaCountRewardS2C gachaCountRewardS2C = new GachaCountRewardS2C();
				if (msgCase_ == MsgOneofCase.GachaCountRewardS2C)
				{
					gachaCountRewardS2C.MergeFrom(GachaCountRewardS2C);
				}
				input.ReadMessage(gachaCountRewardS2C);
				GachaCountRewardS2C = gachaCountRewardS2C;
				break;
			}
			case 42106u:
			{
				GetPlayerSimpleC2S getPlayerSimpleC2S = new GetPlayerSimpleC2S();
				if (msgCase_ == MsgOneofCase.GetPlayerSimpleC2S)
				{
					getPlayerSimpleC2S.MergeFrom(GetPlayerSimpleC2S);
				}
				input.ReadMessage(getPlayerSimpleC2S);
				GetPlayerSimpleC2S = getPlayerSimpleC2S;
				break;
			}
			case 42114u:
			{
				GetPlayerSimpleS2C getPlayerSimpleS2C = new GetPlayerSimpleS2C();
				if (msgCase_ == MsgOneofCase.GetPlayerSimpleS2C)
				{
					getPlayerSimpleS2C.MergeFrom(GetPlayerSimpleS2C);
				}
				input.ReadMessage(getPlayerSimpleS2C);
				GetPlayerSimpleS2C = getPlayerSimpleS2C;
				break;
			}
			case 42122u:
			{
				RoleCardCollectC2S roleCardCollectC2S = new RoleCardCollectC2S();
				if (msgCase_ == MsgOneofCase.RoleCardCollectC2S)
				{
					roleCardCollectC2S.MergeFrom(RoleCardCollectC2S);
				}
				input.ReadMessage(roleCardCollectC2S);
				RoleCardCollectC2S = roleCardCollectC2S;
				break;
			}
			case 42130u:
			{
				RoleCardCollectS2C roleCardCollectS2C = new RoleCardCollectS2C();
				if (msgCase_ == MsgOneofCase.RoleCardCollectS2C)
				{
					roleCardCollectS2C.MergeFrom(RoleCardCollectS2C);
				}
				input.ReadMessage(roleCardCollectS2C);
				RoleCardCollectS2C = roleCardCollectS2C;
				break;
			}
			case 42138u:
			{
				GMPlayerSettingC2S gMPlayerSettingC2S = new GMPlayerSettingC2S();
				if (msgCase_ == MsgOneofCase.GMPlayerSettingC2S)
				{
					gMPlayerSettingC2S.MergeFrom(GMPlayerSettingC2S);
				}
				input.ReadMessage(gMPlayerSettingC2S);
				GMPlayerSettingC2S = gMPlayerSettingC2S;
				break;
			}
			case 42146u:
			{
				GMPlayerSettingS2C gMPlayerSettingS2C = new GMPlayerSettingS2C();
				if (msgCase_ == MsgOneofCase.GMPlayerSettingS2C)
				{
					gMPlayerSettingS2C.MergeFrom(GMPlayerSettingS2C);
				}
				input.ReadMessage(gMPlayerSettingS2C);
				GMPlayerSettingS2C = gMPlayerSettingS2C;
				break;
			}
			case 42154u:
			{
				SetFriendNoteC2S setFriendNoteC2S = new SetFriendNoteC2S();
				if (msgCase_ == MsgOneofCase.SetFriendNoteC2S)
				{
					setFriendNoteC2S.MergeFrom(SetFriendNoteC2S);
				}
				input.ReadMessage(setFriendNoteC2S);
				SetFriendNoteC2S = setFriendNoteC2S;
				break;
			}
			case 42162u:
			{
				SetFriendNoteS2C setFriendNoteS2C = new SetFriendNoteS2C();
				if (msgCase_ == MsgOneofCase.SetFriendNoteS2C)
				{
					setFriendNoteS2C.MergeFrom(SetFriendNoteS2C);
				}
				input.ReadMessage(setFriendNoteS2C);
				SetFriendNoteS2C = setFriendNoteS2C;
				break;
			}
			case 42170u:
			{
				SetOnlineStatusC2S setOnlineStatusC2S = new SetOnlineStatusC2S();
				if (msgCase_ == MsgOneofCase.SetOnlineStatusC2S)
				{
					setOnlineStatusC2S.MergeFrom(SetOnlineStatusC2S);
				}
				input.ReadMessage(setOnlineStatusC2S);
				SetOnlineStatusC2S = setOnlineStatusC2S;
				break;
			}
			case 42178u:
			{
				SetOnlineStatusS2C setOnlineStatusS2C = new SetOnlineStatusS2C();
				if (msgCase_ == MsgOneofCase.SetOnlineStatusS2C)
				{
					setOnlineStatusS2C.MergeFrom(SetOnlineStatusS2C);
				}
				input.ReadMessage(setOnlineStatusS2C);
				SetOnlineStatusS2C = setOnlineStatusS2C;
				break;
			}
			case 42442u:
			{
				ChooseSkinC2S chooseSkinC2S = new ChooseSkinC2S();
				if (msgCase_ == MsgOneofCase.ChooseSkinC2S)
				{
					chooseSkinC2S.MergeFrom(ChooseSkinC2S);
				}
				input.ReadMessage(chooseSkinC2S);
				ChooseSkinC2S = chooseSkinC2S;
				break;
			}
			case 42450u:
			{
				ChooseSkinS2C chooseSkinS2C = new ChooseSkinS2C();
				if (msgCase_ == MsgOneofCase.ChooseSkinS2C)
				{
					chooseSkinS2C.MergeFrom(ChooseSkinS2C);
				}
				input.ReadMessage(chooseSkinS2C);
				ChooseSkinS2C = chooseSkinS2C;
				break;
			}
			case 42458u:
			{
				TimeWastingC2S timeWastingC2S = new TimeWastingC2S();
				if (msgCase_ == MsgOneofCase.TimeWastingC2S)
				{
					timeWastingC2S.MergeFrom(TimeWastingC2S);
				}
				input.ReadMessage(timeWastingC2S);
				TimeWastingC2S = timeWastingC2S;
				break;
			}
			case 42466u:
			{
				TimeWastingS2C timeWastingS2C = new TimeWastingS2C();
				if (msgCase_ == MsgOneofCase.TimeWastingS2C)
				{
					timeWastingS2C.MergeFrom(TimeWastingS2C);
				}
				input.ReadMessage(timeWastingS2C);
				TimeWastingS2C = timeWastingS2C;
				break;
			}
			case 42474u:
			{
				VoteC2S voteC2S = new VoteC2S();
				if (msgCase_ == MsgOneofCase.VoteC2S)
				{
					voteC2S.MergeFrom(VoteC2S);
				}
				input.ReadMessage(voteC2S);
				VoteC2S = voteC2S;
				break;
			}
			case 42482u:
			{
				VoteS2C voteS2C = new VoteS2C();
				if (msgCase_ == MsgOneofCase.VoteS2C)
				{
					voteS2C.MergeFrom(VoteS2C);
				}
				input.ReadMessage(voteS2C);
				VoteS2C = voteS2C;
				break;
			}
			case 42490u:
			{
				VoteSelectC2S voteSelectC2S = new VoteSelectC2S();
				if (msgCase_ == MsgOneofCase.VoteSelectC2S)
				{
					voteSelectC2S.MergeFrom(VoteSelectC2S);
				}
				input.ReadMessage(voteSelectC2S);
				VoteSelectC2S = voteSelectC2S;
				break;
			}
			case 42498u:
			{
				VoteSelectS2C voteSelectS2C = new VoteSelectS2C();
				if (msgCase_ == MsgOneofCase.VoteSelectS2C)
				{
					voteSelectS2C.MergeFrom(VoteSelectS2C);
				}
				input.ReadMessage(voteSelectS2C);
				VoteSelectS2C = voteSelectS2C;
				break;
			}
			case 42506u:
			{
				NotifyStoryC2S notifyStoryC2S = new NotifyStoryC2S();
				if (msgCase_ == MsgOneofCase.NotifyStoryC2S)
				{
					notifyStoryC2S.MergeFrom(NotifyStoryC2S);
				}
				input.ReadMessage(notifyStoryC2S);
				NotifyStoryC2S = notifyStoryC2S;
				break;
			}
			case 42514u:
			{
				NotifyStoryS2C notifyStoryS2C = new NotifyStoryS2C();
				if (msgCase_ == MsgOneofCase.NotifyStoryS2C)
				{
					notifyStoryS2C.MergeFrom(NotifyStoryS2C);
				}
				input.ReadMessage(notifyStoryS2C);
				NotifyStoryS2C = notifyStoryS2C;
				break;
			}
			case 42522u:
			{
				PveHeroTalentUpC2S pveHeroTalentUpC2S = new PveHeroTalentUpC2S();
				if (msgCase_ == MsgOneofCase.PveHeroTalentUpC2S)
				{
					pveHeroTalentUpC2S.MergeFrom(PveHeroTalentUpC2S);
				}
				input.ReadMessage(pveHeroTalentUpC2S);
				PveHeroTalentUpC2S = pveHeroTalentUpC2S;
				break;
			}
			case 42530u:
			{
				PveHeroTalentUpS2C pveHeroTalentUpS2C = new PveHeroTalentUpS2C();
				if (msgCase_ == MsgOneofCase.PveHeroTalentUpS2C)
				{
					pveHeroTalentUpS2C.MergeFrom(PveHeroTalentUpS2C);
				}
				input.ReadMessage(pveHeroTalentUpS2C);
				PveHeroTalentUpS2C = pveHeroTalentUpS2C;
				break;
			}
			case 42538u:
			{
				SelectEventC2S selectEventC2S = new SelectEventC2S();
				if (msgCase_ == MsgOneofCase.SelectEventC2S)
				{
					selectEventC2S.MergeFrom(SelectEventC2S);
				}
				input.ReadMessage(selectEventC2S);
				SelectEventC2S = selectEventC2S;
				break;
			}
			case 42546u:
			{
				SelectEventS2C selectEventS2C = new SelectEventS2C();
				if (msgCase_ == MsgOneofCase.SelectEventS2C)
				{
					selectEventS2C.MergeFrom(SelectEventS2C);
				}
				input.ReadMessage(selectEventS2C);
				SelectEventS2C = selectEventS2C;
				break;
			}
			case 42554u:
			{
				CampScoreC2S campScoreC2S = new CampScoreC2S();
				if (msgCase_ == MsgOneofCase.CampScoreC2S)
				{
					campScoreC2S.MergeFrom(CampScoreC2S);
				}
				input.ReadMessage(campScoreC2S);
				CampScoreC2S = campScoreC2S;
				break;
			}
			case 42562u:
			{
				CampScoreS2C campScoreS2C = new CampScoreS2C();
				if (msgCase_ == MsgOneofCase.CampScoreS2C)
				{
					campScoreS2C.MergeFrom(CampScoreS2C);
				}
				input.ReadMessage(campScoreS2C);
				CampScoreS2C = campScoreS2C;
				break;
			}
			case 42570u:
			{
				ActivityMissionRewardC2S activityMissionRewardC2S = new ActivityMissionRewardC2S();
				if (msgCase_ == MsgOneofCase.ActivityMissionRewardC2S)
				{
					activityMissionRewardC2S.MergeFrom(ActivityMissionRewardC2S);
				}
				input.ReadMessage(activityMissionRewardC2S);
				ActivityMissionRewardC2S = activityMissionRewardC2S;
				break;
			}
			case 42578u:
			{
				ActivityMissionRewardS2C activityMissionRewardS2C = new ActivityMissionRewardS2C();
				if (msgCase_ == MsgOneofCase.ActivityMissionRewardS2C)
				{
					activityMissionRewardS2C.MergeFrom(ActivityMissionRewardS2C);
				}
				input.ReadMessage(activityMissionRewardS2C);
				ActivityMissionRewardS2C = activityMissionRewardS2C;
				break;
			}
			case 42586u:
			{
				VendorBuyCardC2S vendorBuyCardC2S = new VendorBuyCardC2S();
				if (msgCase_ == MsgOneofCase.VendorBuyCardC2S)
				{
					vendorBuyCardC2S.MergeFrom(VendorBuyCardC2S);
				}
				input.ReadMessage(vendorBuyCardC2S);
				VendorBuyCardC2S = vendorBuyCardC2S;
				break;
			}
			case 42594u:
			{
				VendorBuyCardS2C vendorBuyCardS2C = new VendorBuyCardS2C();
				if (msgCase_ == MsgOneofCase.VendorBuyCardS2C)
				{
					vendorBuyCardS2C.MergeFrom(VendorBuyCardS2C);
				}
				input.ReadMessage(vendorBuyCardS2C);
				VendorBuyCardS2C = vendorBuyCardS2C;
				break;
			}
			case 42602u:
			{
				TransferStarDiscC2S transferStarDiscC2S = new TransferStarDiscC2S();
				if (msgCase_ == MsgOneofCase.TransferStarDiscC2S)
				{
					transferStarDiscC2S.MergeFrom(TransferStarDiscC2S);
				}
				input.ReadMessage(transferStarDiscC2S);
				TransferStarDiscC2S = transferStarDiscC2S;
				break;
			}
			case 42610u:
			{
				TransferStarDiscS2C transferStarDiscS2C = new TransferStarDiscS2C();
				if (msgCase_ == MsgOneofCase.TransferStarDiscS2C)
				{
					transferStarDiscS2C.MergeFrom(TransferStarDiscS2C);
				}
				input.ReadMessage(transferStarDiscS2C);
				TransferStarDiscS2C = transferStarDiscS2C;
				break;
			}
			case 42618u:
			{
				GetHeroInfoC2S getHeroInfoC2S = new GetHeroInfoC2S();
				if (msgCase_ == MsgOneofCase.GetHeroInfoC2S)
				{
					getHeroInfoC2S.MergeFrom(GetHeroInfoC2S);
				}
				input.ReadMessage(getHeroInfoC2S);
				GetHeroInfoC2S = getHeroInfoC2S;
				break;
			}
			case 42626u:
			{
				GetHeroInfoS2C getHeroInfoS2C = new GetHeroInfoS2C();
				if (msgCase_ == MsgOneofCase.GetHeroInfoS2C)
				{
					getHeroInfoS2C.MergeFrom(GetHeroInfoS2C);
				}
				input.ReadMessage(getHeroInfoS2C);
				GetHeroInfoS2C = getHeroInfoS2C;
				break;
			}
			case 42634u:
			{
				CreateMatchTeamC2S createMatchTeamC2S = new CreateMatchTeamC2S();
				if (msgCase_ == MsgOneofCase.CreateMatchTeamC2S)
				{
					createMatchTeamC2S.MergeFrom(CreateMatchTeamC2S);
				}
				input.ReadMessage(createMatchTeamC2S);
				CreateMatchTeamC2S = createMatchTeamC2S;
				break;
			}
			case 42642u:
			{
				CreateMatchTeamS2C createMatchTeamS2C = new CreateMatchTeamS2C();
				if (msgCase_ == MsgOneofCase.CreateMatchTeamS2C)
				{
					createMatchTeamS2C.MergeFrom(CreateMatchTeamS2C);
				}
				input.ReadMessage(createMatchTeamS2C);
				CreateMatchTeamS2C = createMatchTeamS2C;
				break;
			}
			case 42650u:
			{
				ChangeMatchTeamC2S changeMatchTeamC2S = new ChangeMatchTeamC2S();
				if (msgCase_ == MsgOneofCase.ChangeMatchTeamC2S)
				{
					changeMatchTeamC2S.MergeFrom(ChangeMatchTeamC2S);
				}
				input.ReadMessage(changeMatchTeamC2S);
				ChangeMatchTeamC2S = changeMatchTeamC2S;
				break;
			}
			case 42658u:
			{
				ChangeMatchTeamS2C changeMatchTeamS2C = new ChangeMatchTeamS2C();
				if (msgCase_ == MsgOneofCase.ChangeMatchTeamS2C)
				{
					changeMatchTeamS2C.MergeFrom(ChangeMatchTeamS2C);
				}
				input.ReadMessage(changeMatchTeamS2C);
				ChangeMatchTeamS2C = changeMatchTeamS2C;
				break;
			}
			case 42666u:
			{
				JoinMatchTeamC2S joinMatchTeamC2S = new JoinMatchTeamC2S();
				if (msgCase_ == MsgOneofCase.JoinMatchTeamC2S)
				{
					joinMatchTeamC2S.MergeFrom(JoinMatchTeamC2S);
				}
				input.ReadMessage(joinMatchTeamC2S);
				JoinMatchTeamC2S = joinMatchTeamC2S;
				break;
			}
			case 42674u:
			{
				JoinMatchTeamS2C joinMatchTeamS2C = new JoinMatchTeamS2C();
				if (msgCase_ == MsgOneofCase.JoinMatchTeamS2C)
				{
					joinMatchTeamS2C.MergeFrom(JoinMatchTeamS2C);
				}
				input.ReadMessage(joinMatchTeamS2C);
				JoinMatchTeamS2C = joinMatchTeamS2C;
				break;
			}
			case 42682u:
			{
				ExitMatchTeamC2S exitMatchTeamC2S = new ExitMatchTeamC2S();
				if (msgCase_ == MsgOneofCase.ExitMatchTeamC2S)
				{
					exitMatchTeamC2S.MergeFrom(ExitMatchTeamC2S);
				}
				input.ReadMessage(exitMatchTeamC2S);
				ExitMatchTeamC2S = exitMatchTeamC2S;
				break;
			}
			case 42690u:
			{
				ExitMatchTeamS2C exitMatchTeamS2C = new ExitMatchTeamS2C();
				if (msgCase_ == MsgOneofCase.ExitMatchTeamS2C)
				{
					exitMatchTeamS2C.MergeFrom(ExitMatchTeamS2C);
				}
				input.ReadMessage(exitMatchTeamS2C);
				ExitMatchTeamS2C = exitMatchTeamS2C;
				break;
			}
			case 42698u:
			{
				RefreshMatchTeamInfoC2S refreshMatchTeamInfoC2S = new RefreshMatchTeamInfoC2S();
				if (msgCase_ == MsgOneofCase.RefreshMatchTeamInfoC2S)
				{
					refreshMatchTeamInfoC2S.MergeFrom(RefreshMatchTeamInfoC2S);
				}
				input.ReadMessage(refreshMatchTeamInfoC2S);
				RefreshMatchTeamInfoC2S = refreshMatchTeamInfoC2S;
				break;
			}
			case 42706u:
			{
				RefreshMatchTeamInfoS2C refreshMatchTeamInfoS2C = new RefreshMatchTeamInfoS2C();
				if (msgCase_ == MsgOneofCase.RefreshMatchTeamInfoS2C)
				{
					refreshMatchTeamInfoS2C.MergeFrom(RefreshMatchTeamInfoS2C);
				}
				input.ReadMessage(refreshMatchTeamInfoS2C);
				RefreshMatchTeamInfoS2C = refreshMatchTeamInfoS2C;
				break;
			}
			case 42714u:
			{
				ChinaCreateOrderC2S chinaCreateOrderC2S = new ChinaCreateOrderC2S();
				if (msgCase_ == MsgOneofCase.ChinaCreateOrderC2S)
				{
					chinaCreateOrderC2S.MergeFrom(ChinaCreateOrderC2S);
				}
				input.ReadMessage(chinaCreateOrderC2S);
				ChinaCreateOrderC2S = chinaCreateOrderC2S;
				break;
			}
			case 42722u:
			{
				ChinaCreateOrderS2C chinaCreateOrderS2C = new ChinaCreateOrderS2C();
				if (msgCase_ == MsgOneofCase.ChinaCreateOrderS2C)
				{
					chinaCreateOrderS2C.MergeFrom(ChinaCreateOrderS2C);
				}
				input.ReadMessage(chinaCreateOrderS2C);
				ChinaCreateOrderS2C = chinaCreateOrderS2C;
				break;
			}
			case 42730u:
			{
				MatchTeamInviteC2S matchTeamInviteC2S = new MatchTeamInviteC2S();
				if (msgCase_ == MsgOneofCase.MatchTeamInviteC2S)
				{
					matchTeamInviteC2S.MergeFrom(MatchTeamInviteC2S);
				}
				input.ReadMessage(matchTeamInviteC2S);
				MatchTeamInviteC2S = matchTeamInviteC2S;
				break;
			}
			case 42738u:
			{
				MatchTeamInviteS2C matchTeamInviteS2C = new MatchTeamInviteS2C();
				if (msgCase_ == MsgOneofCase.MatchTeamInviteS2C)
				{
					matchTeamInviteS2C.MergeFrom(MatchTeamInviteS2C);
				}
				input.ReadMessage(matchTeamInviteS2C);
				MatchTeamInviteS2C = matchTeamInviteS2C;
				break;
			}
			case 42746u:
			{
				MatchTeamChatC2S matchTeamChatC2S = new MatchTeamChatC2S();
				if (msgCase_ == MsgOneofCase.MatchTeamChatC2S)
				{
					matchTeamChatC2S.MergeFrom(MatchTeamChatC2S);
				}
				input.ReadMessage(matchTeamChatC2S);
				MatchTeamChatC2S = matchTeamChatC2S;
				break;
			}
			case 42754u:
			{
				MatchTeamChatS2C matchTeamChatS2C = new MatchTeamChatS2C();
				if (msgCase_ == MsgOneofCase.MatchTeamChatS2C)
				{
					matchTeamChatS2C.MergeFrom(MatchTeamChatS2C);
				}
				input.ReadMessage(matchTeamChatS2C);
				MatchTeamChatS2C = matchTeamChatS2C;
				break;
			}
			case 42762u:
			{
				MatchTeamReadyC2S matchTeamReadyC2S = new MatchTeamReadyC2S();
				if (msgCase_ == MsgOneofCase.MatchTeamReadyC2S)
				{
					matchTeamReadyC2S.MergeFrom(MatchTeamReadyC2S);
				}
				input.ReadMessage(matchTeamReadyC2S);
				MatchTeamReadyC2S = matchTeamReadyC2S;
				break;
			}
			case 42770u:
			{
				MatchTeamReadyS2C matchTeamReadyS2C = new MatchTeamReadyS2C();
				if (msgCase_ == MsgOneofCase.MatchTeamReadyS2C)
				{
					matchTeamReadyS2C.MergeFrom(MatchTeamReadyS2C);
				}
				input.ReadMessage(matchTeamReadyS2C);
				MatchTeamReadyS2C = matchTeamReadyS2C;
				break;
			}
			case 42778u:
			{
				PlayerChatC2S playerChatC2S = new PlayerChatC2S();
				if (msgCase_ == MsgOneofCase.PlayerChatC2S)
				{
					playerChatC2S.MergeFrom(PlayerChatC2S);
				}
				input.ReadMessage(playerChatC2S);
				PlayerChatC2S = playerChatC2S;
				break;
			}
			case 42786u:
			{
				PlayerChatS2C playerChatS2C = new PlayerChatS2C();
				if (msgCase_ == MsgOneofCase.PlayerChatS2C)
				{
					playerChatS2C.MergeFrom(PlayerChatS2C);
				}
				input.ReadMessage(playerChatS2C);
				PlayerChatS2C = playerChatS2C;
				break;
			}
			case 42794u:
			{
				SyncSingleGameDataC2S syncSingleGameDataC2S = new SyncSingleGameDataC2S();
				if (msgCase_ == MsgOneofCase.SyncSingleGameDataC2S)
				{
					syncSingleGameDataC2S.MergeFrom(SyncSingleGameDataC2S);
				}
				input.ReadMessage(syncSingleGameDataC2S);
				SyncSingleGameDataC2S = syncSingleGameDataC2S;
				break;
			}
			case 42802u:
			{
				SyncSingleGameDataS2C syncSingleGameDataS2C = new SyncSingleGameDataS2C();
				if (msgCase_ == MsgOneofCase.SyncSingleGameDataS2C)
				{
					syncSingleGameDataS2C.MergeFrom(SyncSingleGameDataS2C);
				}
				input.ReadMessage(syncSingleGameDataS2C);
				SyncSingleGameDataS2C = syncSingleGameDataS2C;
				break;
			}
			case 42810u:
			{
				SingleGameDataC2S singleGameDataC2S = new SingleGameDataC2S();
				if (msgCase_ == MsgOneofCase.SingleGameDataC2S)
				{
					singleGameDataC2S.MergeFrom(SingleGameDataC2S);
				}
				input.ReadMessage(singleGameDataC2S);
				SingleGameDataC2S = singleGameDataC2S;
				break;
			}
			case 42818u:
			{
				SingleGameDataS2C singleGameDataS2C = new SingleGameDataS2C();
				if (msgCase_ == MsgOneofCase.SingleGameDataS2C)
				{
					singleGameDataS2C.MergeFrom(SingleGameDataS2C);
				}
				input.ReadMessage(singleGameDataS2C);
				SingleGameDataS2C = singleGameDataS2C;
				break;
			}
			case 42826u:
			{
				GetActivityPassRewardC2S getActivityPassRewardC2S = new GetActivityPassRewardC2S();
				if (msgCase_ == MsgOneofCase.GetActivityPassRewardC2S)
				{
					getActivityPassRewardC2S.MergeFrom(GetActivityPassRewardC2S);
				}
				input.ReadMessage(getActivityPassRewardC2S);
				GetActivityPassRewardC2S = getActivityPassRewardC2S;
				break;
			}
			case 42834u:
			{
				GetActivityPassRewardS2C getActivityPassRewardS2C = new GetActivityPassRewardS2C();
				if (msgCase_ == MsgOneofCase.GetActivityPassRewardS2C)
				{
					getActivityPassRewardS2C.MergeFrom(GetActivityPassRewardS2C);
				}
				input.ReadMessage(getActivityPassRewardS2C);
				GetActivityPassRewardS2C = getActivityPassRewardS2C;
				break;
			}
			case 42842u:
			{
				ApplyChangeSlotC2S applyChangeSlotC2S = new ApplyChangeSlotC2S();
				if (msgCase_ == MsgOneofCase.ApplyChangeSlotC2S)
				{
					applyChangeSlotC2S.MergeFrom(ApplyChangeSlotC2S);
				}
				input.ReadMessage(applyChangeSlotC2S);
				ApplyChangeSlotC2S = applyChangeSlotC2S;
				break;
			}
			case 42850u:
			{
				ApplyChangeSlotS2C applyChangeSlotS2C = new ApplyChangeSlotS2C();
				if (msgCase_ == MsgOneofCase.ApplyChangeSlotS2C)
				{
					applyChangeSlotS2C.MergeFrom(ApplyChangeSlotS2C);
				}
				input.ReadMessage(applyChangeSlotS2C);
				ApplyChangeSlotS2C = applyChangeSlotS2C;
				break;
			}
			case 42858u:
			{
				OpsChangeSlotC2S opsChangeSlotC2S = new OpsChangeSlotC2S();
				if (msgCase_ == MsgOneofCase.OpsChangeSlotC2S)
				{
					opsChangeSlotC2S.MergeFrom(OpsChangeSlotC2S);
				}
				input.ReadMessage(opsChangeSlotC2S);
				OpsChangeSlotC2S = opsChangeSlotC2S;
				break;
			}
			case 42866u:
			{
				OpsChangeSlotS2C opsChangeSlotS2C = new OpsChangeSlotS2C();
				if (msgCase_ == MsgOneofCase.OpsChangeSlotS2C)
				{
					opsChangeSlotS2C.MergeFrom(OpsChangeSlotS2C);
				}
				input.ReadMessage(opsChangeSlotS2C);
				OpsChangeSlotS2C = opsChangeSlotS2C;
				break;
			}
			case 42874u:
			{
				RookieGachaRewardC2S rookieGachaRewardC2S = new RookieGachaRewardC2S();
				if (msgCase_ == MsgOneofCase.RookieGachaRewardC2S)
				{
					rookieGachaRewardC2S.MergeFrom(RookieGachaRewardC2S);
				}
				input.ReadMessage(rookieGachaRewardC2S);
				RookieGachaRewardC2S = rookieGachaRewardC2S;
				break;
			}
			case 42882u:
			{
				RookieGachaRewardS2C rookieGachaRewardS2C = new RookieGachaRewardS2C();
				if (msgCase_ == MsgOneofCase.RookieGachaRewardS2C)
				{
					rookieGachaRewardS2C.MergeFrom(RookieGachaRewardS2C);
				}
				input.ReadMessage(rookieGachaRewardS2C);
				RookieGachaRewardS2C = rookieGachaRewardS2C;
				break;
			}
			case 42890u:
			{
				LiveGiftPackageC2S liveGiftPackageC2S = new LiveGiftPackageC2S();
				if (msgCase_ == MsgOneofCase.LiveGiftPackageC2S)
				{
					liveGiftPackageC2S.MergeFrom(LiveGiftPackageC2S);
				}
				input.ReadMessage(liveGiftPackageC2S);
				LiveGiftPackageC2S = liveGiftPackageC2S;
				break;
			}
			case 42898u:
			{
				LiveGiftPackageS2C liveGiftPackageS2C = new LiveGiftPackageS2C();
				if (msgCase_ == MsgOneofCase.LiveGiftPackageS2C)
				{
					liveGiftPackageS2C.MergeFrom(LiveGiftPackageS2C);
				}
				input.ReadMessage(liveGiftPackageS2C);
				LiveGiftPackageS2C = liveGiftPackageS2C;
				break;
			}
			case 42906u:
			{
				LaborActDiceC2S laborActDiceC2S = new LaborActDiceC2S();
				if (msgCase_ == MsgOneofCase.LaborActDiceC2S)
				{
					laborActDiceC2S.MergeFrom(LaborActDiceC2S);
				}
				input.ReadMessage(laborActDiceC2S);
				LaborActDiceC2S = laborActDiceC2S;
				break;
			}
			case 42914u:
			{
				LaborActDiceS2C laborActDiceS2C = new LaborActDiceS2C();
				if (msgCase_ == MsgOneofCase.LaborActDiceS2C)
				{
					laborActDiceS2C.MergeFrom(LaborActDiceS2C);
				}
				input.ReadMessage(laborActDiceS2C);
				LaborActDiceS2C = laborActDiceS2C;
				break;
			}
			case 42922u:
			{
				ActionOverTimeLogC2S actionOverTimeLogC2S = new ActionOverTimeLogC2S();
				if (msgCase_ == MsgOneofCase.ActionOverTimeLogC2S)
				{
					actionOverTimeLogC2S.MergeFrom(ActionOverTimeLogC2S);
				}
				input.ReadMessage(actionOverTimeLogC2S);
				ActionOverTimeLogC2S = actionOverTimeLogC2S;
				break;
			}
			case 42930u:
			{
				ActionOverTimeLogS2C actionOverTimeLogS2C = new ActionOverTimeLogS2C();
				if (msgCase_ == MsgOneofCase.ActionOverTimeLogS2C)
				{
					actionOverTimeLogS2C.MergeFrom(ActionOverTimeLogS2C);
				}
				input.ReadMessage(actionOverTimeLogS2C);
				ActionOverTimeLogS2C = actionOverTimeLogS2C;
				break;
			}
			case 42954u:
			{
				ClientHarmonyC2S clientHarmonyC2S = new ClientHarmonyC2S();
				if (msgCase_ == MsgOneofCase.ClientHarmonyC2S)
				{
					clientHarmonyC2S.MergeFrom(ClientHarmonyC2S);
				}
				input.ReadMessage(clientHarmonyC2S);
				ClientHarmonyC2S = clientHarmonyC2S;
				break;
			}
			case 42962u:
			{
				ClientHarmonyS2C clientHarmonyS2C = new ClientHarmonyS2C();
				if (msgCase_ == MsgOneofCase.ClientHarmonyS2C)
				{
					clientHarmonyS2C.MergeFrom(ClientHarmonyS2C);
				}
				input.ReadMessage(clientHarmonyS2C);
				ClientHarmonyS2C = clientHarmonyS2C;
				break;
			}
			case 42970u:
			{
				MailStarC2S mailStarC2S = new MailStarC2S();
				if (msgCase_ == MsgOneofCase.MailStarC2S)
				{
					mailStarC2S.MergeFrom(MailStarC2S);
				}
				input.ReadMessage(mailStarC2S);
				MailStarC2S = mailStarC2S;
				break;
			}
			case 42978u:
			{
				MailStarS2C mailStarS2C = new MailStarS2C();
				if (msgCase_ == MsgOneofCase.MailStarS2C)
				{
					mailStarS2C.MergeFrom(MailStarS2C);
				}
				input.ReadMessage(mailStarS2C);
				MailStarS2C = mailStarS2C;
				break;
			}
			case 42986u:
			{
				SetCardAltArtC2S setCardAltArtC2S = new SetCardAltArtC2S();
				if (msgCase_ == MsgOneofCase.SetCardAltArtC2S)
				{
					setCardAltArtC2S.MergeFrom(SetCardAltArtC2S);
				}
				input.ReadMessage(setCardAltArtC2S);
				SetCardAltArtC2S = setCardAltArtC2S;
				break;
			}
			case 42994u:
			{
				SetCardAltArtS2C setCardAltArtS2C = new SetCardAltArtS2C();
				if (msgCase_ == MsgOneofCase.SetCardAltArtS2C)
				{
					setCardAltArtS2C.MergeFrom(SetCardAltArtS2C);
				}
				input.ReadMessage(setCardAltArtS2C);
				SetCardAltArtS2C = setCardAltArtS2C;
				break;
			}
			case 43018u:
			{
				SelectRewardCardC2S selectRewardCardC2S = new SelectRewardCardC2S();
				if (msgCase_ == MsgOneofCase.SelectRewardCardC2S)
				{
					selectRewardCardC2S.MergeFrom(SelectRewardCardC2S);
				}
				input.ReadMessage(selectRewardCardC2S);
				SelectRewardCardC2S = selectRewardCardC2S;
				break;
			}
			case 43026u:
			{
				SelectRewardCardS2C selectRewardCardS2C = new SelectRewardCardS2C();
				if (msgCase_ == MsgOneofCase.SelectRewardCardS2C)
				{
					selectRewardCardS2C.MergeFrom(SelectRewardCardS2C);
				}
				input.ReadMessage(selectRewardCardS2C);
				SelectRewardCardS2C = selectRewardCardS2C;
				break;
			}
			case 43034u:
			{
				GetReturnInfoC2S getReturnInfoC2S = new GetReturnInfoC2S();
				if (msgCase_ == MsgOneofCase.GetReturnInfoC2S)
				{
					getReturnInfoC2S.MergeFrom(GetReturnInfoC2S);
				}
				input.ReadMessage(getReturnInfoC2S);
				GetReturnInfoC2S = getReturnInfoC2S;
				break;
			}
			case 43042u:
			{
				GetReturnInfoS2C getReturnInfoS2C = new GetReturnInfoS2C();
				if (msgCase_ == MsgOneofCase.GetReturnInfoS2C)
				{
					getReturnInfoS2C.MergeFrom(GetReturnInfoS2C);
				}
				input.ReadMessage(getReturnInfoS2C);
				GetReturnInfoS2C = getReturnInfoS2C;
				break;
			}
			case 43050u:
			{
				ReturnGiftClaimC2S returnGiftClaimC2S = new ReturnGiftClaimC2S();
				if (msgCase_ == MsgOneofCase.ReturnGiftClaimC2S)
				{
					returnGiftClaimC2S.MergeFrom(ReturnGiftClaimC2S);
				}
				input.ReadMessage(returnGiftClaimC2S);
				ReturnGiftClaimC2S = returnGiftClaimC2S;
				break;
			}
			case 43058u:
			{
				ReturnGiftClaimS2C returnGiftClaimS2C = new ReturnGiftClaimS2C();
				if (msgCase_ == MsgOneofCase.ReturnGiftClaimS2C)
				{
					returnGiftClaimS2C.MergeFrom(ReturnGiftClaimS2C);
				}
				input.ReadMessage(returnGiftClaimS2C);
				ReturnGiftClaimS2C = returnGiftClaimS2C;
				break;
			}
			case 43066u:
			{
				ReturnSignInClaimC2S returnSignInClaimC2S = new ReturnSignInClaimC2S();
				if (msgCase_ == MsgOneofCase.ReturnSignInClaimC2S)
				{
					returnSignInClaimC2S.MergeFrom(ReturnSignInClaimC2S);
				}
				input.ReadMessage(returnSignInClaimC2S);
				ReturnSignInClaimC2S = returnSignInClaimC2S;
				break;
			}
			case 43074u:
			{
				ReturnSignInClaimS2C returnSignInClaimS2C = new ReturnSignInClaimS2C();
				if (msgCase_ == MsgOneofCase.ReturnSignInClaimS2C)
				{
					returnSignInClaimS2C.MergeFrom(ReturnSignInClaimS2C);
				}
				input.ReadMessage(returnSignInClaimS2C);
				ReturnSignInClaimS2C = returnSignInClaimS2C;
				break;
			}
			case 43082u:
			{
				ReturnSurveyFinishC2S returnSurveyFinishC2S = new ReturnSurveyFinishC2S();
				if (msgCase_ == MsgOneofCase.ReturnSurveyFinishC2S)
				{
					returnSurveyFinishC2S.MergeFrom(ReturnSurveyFinishC2S);
				}
				input.ReadMessage(returnSurveyFinishC2S);
				ReturnSurveyFinishC2S = returnSurveyFinishC2S;
				break;
			}
			case 43090u:
			{
				ReturnSurveyFinishS2C returnSurveyFinishS2C = new ReturnSurveyFinishS2C();
				if (msgCase_ == MsgOneofCase.ReturnSurveyFinishS2C)
				{
					returnSurveyFinishS2C.MergeFrom(ReturnSurveyFinishS2C);
				}
				input.ReadMessage(returnSurveyFinishS2C);
				ReturnSurveyFinishS2C = returnSurveyFinishS2C;
				break;
			}
			case 43098u:
			{
				FlipCardC2S flipCardC2S = new FlipCardC2S();
				if (msgCase_ == MsgOneofCase.FlipCardC2S)
				{
					flipCardC2S.MergeFrom(FlipCardC2S);
				}
				input.ReadMessage(flipCardC2S);
				FlipCardC2S = flipCardC2S;
				break;
			}
			case 43106u:
			{
				FlipCardS2C flipCardS2C = new FlipCardS2C();
				if (msgCase_ == MsgOneofCase.FlipCardS2C)
				{
					flipCardS2C.MergeFrom(FlipCardS2C);
				}
				input.ReadMessage(flipCardS2C);
				FlipCardS2C = flipCardS2C;
				break;
			}
			case 43114u:
			{
				FlipCardProgressRewardC2S flipCardProgressRewardC2S = new FlipCardProgressRewardC2S();
				if (msgCase_ == MsgOneofCase.FlipCardProgressRewardC2S)
				{
					flipCardProgressRewardC2S.MergeFrom(FlipCardProgressRewardC2S);
				}
				input.ReadMessage(flipCardProgressRewardC2S);
				FlipCardProgressRewardC2S = flipCardProgressRewardC2S;
				break;
			}
			case 43122u:
			{
				FlipCardProgressRewardS2C flipCardProgressRewardS2C = new FlipCardProgressRewardS2C();
				if (msgCase_ == MsgOneofCase.FlipCardProgressRewardS2C)
				{
					flipCardProgressRewardS2C.MergeFrom(FlipCardProgressRewardS2C);
				}
				input.ReadMessage(flipCardProgressRewardS2C);
				FlipCardProgressRewardS2C = flipCardProgressRewardS2C;
				break;
			}
			case 43162u:
			{
				GetQuestionUrlC2S getQuestionUrlC2S = new GetQuestionUrlC2S();
				if (msgCase_ == MsgOneofCase.GetQuestionUrlC2S)
				{
					getQuestionUrlC2S.MergeFrom(GetQuestionUrlC2S);
				}
				input.ReadMessage(getQuestionUrlC2S);
				GetQuestionUrlC2S = getQuestionUrlC2S;
				break;
			}
			case 43170u:
			{
				GetQuestionUrlS2C getQuestionUrlS2C = new GetQuestionUrlS2C();
				if (msgCase_ == MsgOneofCase.GetQuestionUrlS2C)
				{
					getQuestionUrlS2C.MergeFrom(GetQuestionUrlS2C);
				}
				input.ReadMessage(getQuestionUrlS2C);
				GetQuestionUrlS2C = getQuestionUrlS2C;
				break;
			}
			case 72010u:
			{
				CreateGuildC2S createGuildC2S = new CreateGuildC2S();
				if (msgCase_ == MsgOneofCase.CreateGuildC2S)
				{
					createGuildC2S.MergeFrom(CreateGuildC2S);
				}
				input.ReadMessage(createGuildC2S);
				CreateGuildC2S = createGuildC2S;
				break;
			}
			case 72018u:
			{
				CreateGuildS2C createGuildS2C = new CreateGuildS2C();
				if (msgCase_ == MsgOneofCase.CreateGuildS2C)
				{
					createGuildS2C.MergeFrom(CreateGuildS2C);
				}
				input.ReadMessage(createGuildS2C);
				CreateGuildS2C = createGuildS2C;
				break;
			}
			case 72026u:
			{
				SearchGuildC2S searchGuildC2S = new SearchGuildC2S();
				if (msgCase_ == MsgOneofCase.SearchGuildC2S)
				{
					searchGuildC2S.MergeFrom(SearchGuildC2S);
				}
				input.ReadMessage(searchGuildC2S);
				SearchGuildC2S = searchGuildC2S;
				break;
			}
			case 72034u:
			{
				SearchGuildS2C searchGuildS2C = new SearchGuildS2C();
				if (msgCase_ == MsgOneofCase.SearchGuildS2C)
				{
					searchGuildS2C.MergeFrom(SearchGuildS2C);
				}
				input.ReadMessage(searchGuildS2C);
				SearchGuildS2C = searchGuildS2C;
				break;
			}
			case 72042u:
			{
				ApplyToGuildC2S applyToGuildC2S = new ApplyToGuildC2S();
				if (msgCase_ == MsgOneofCase.ApplyToGuildC2S)
				{
					applyToGuildC2S.MergeFrom(ApplyToGuildC2S);
				}
				input.ReadMessage(applyToGuildC2S);
				ApplyToGuildC2S = applyToGuildC2S;
				break;
			}
			case 72050u:
			{
				ApplyToGuildS2C applyToGuildS2C = new ApplyToGuildS2C();
				if (msgCase_ == MsgOneofCase.ApplyToGuildS2C)
				{
					applyToGuildS2C.MergeFrom(ApplyToGuildS2C);
				}
				input.ReadMessage(applyToGuildS2C);
				ApplyToGuildS2C = applyToGuildS2C;
				break;
			}
			case 72058u:
			{
				ProcessGuildApplicationC2S processGuildApplicationC2S = new ProcessGuildApplicationC2S();
				if (msgCase_ == MsgOneofCase.ProcessGuildApplicationC2S)
				{
					processGuildApplicationC2S.MergeFrom(ProcessGuildApplicationC2S);
				}
				input.ReadMessage(processGuildApplicationC2S);
				ProcessGuildApplicationC2S = processGuildApplicationC2S;
				break;
			}
			case 72066u:
			{
				ProcessGuildApplicationS2C processGuildApplicationS2C = new ProcessGuildApplicationS2C();
				if (msgCase_ == MsgOneofCase.ProcessGuildApplicationS2C)
				{
					processGuildApplicationS2C.MergeFrom(ProcessGuildApplicationS2C);
				}
				input.ReadMessage(processGuildApplicationS2C);
				ProcessGuildApplicationS2C = processGuildApplicationS2C;
				break;
			}
			case 72074u:
			{
				SendGuildInvitationC2S sendGuildInvitationC2S = new SendGuildInvitationC2S();
				if (msgCase_ == MsgOneofCase.SendGuildInvitationC2S)
				{
					sendGuildInvitationC2S.MergeFrom(SendGuildInvitationC2S);
				}
				input.ReadMessage(sendGuildInvitationC2S);
				SendGuildInvitationC2S = sendGuildInvitationC2S;
				break;
			}
			case 72082u:
			{
				SendGuildInvitationS2C sendGuildInvitationS2C = new SendGuildInvitationS2C();
				if (msgCase_ == MsgOneofCase.SendGuildInvitationS2C)
				{
					sendGuildInvitationS2C.MergeFrom(SendGuildInvitationS2C);
				}
				input.ReadMessage(sendGuildInvitationS2C);
				SendGuildInvitationS2C = sendGuildInvitationS2C;
				break;
			}
			case 72090u:
			{
				ProcessGuildInvitationC2S processGuildInvitationC2S = new ProcessGuildInvitationC2S();
				if (msgCase_ == MsgOneofCase.ProcessGuildInvitationC2S)
				{
					processGuildInvitationC2S.MergeFrom(ProcessGuildInvitationC2S);
				}
				input.ReadMessage(processGuildInvitationC2S);
				ProcessGuildInvitationC2S = processGuildInvitationC2S;
				break;
			}
			case 72098u:
			{
				ProcessGuildInvitationS2C processGuildInvitationS2C = new ProcessGuildInvitationS2C();
				if (msgCase_ == MsgOneofCase.ProcessGuildInvitationS2C)
				{
					processGuildInvitationS2C.MergeFrom(ProcessGuildInvitationS2C);
				}
				input.ReadMessage(processGuildInvitationS2C);
				ProcessGuildInvitationS2C = processGuildInvitationS2C;
				break;
			}
			case 72106u:
			{
				GetGuildInfoC2S getGuildInfoC2S = new GetGuildInfoC2S();
				if (msgCase_ == MsgOneofCase.GetGuildInfoC2S)
				{
					getGuildInfoC2S.MergeFrom(GetGuildInfoC2S);
				}
				input.ReadMessage(getGuildInfoC2S);
				GetGuildInfoC2S = getGuildInfoC2S;
				break;
			}
			case 72114u:
			{
				GetGuildInfoS2C getGuildInfoS2C = new GetGuildInfoS2C();
				if (msgCase_ == MsgOneofCase.GetGuildInfoS2C)
				{
					getGuildInfoS2C.MergeFrom(GetGuildInfoS2C);
				}
				input.ReadMessage(getGuildInfoS2C);
				GetGuildInfoS2C = getGuildInfoS2C;
				break;
			}
			case 72122u:
			{
				UpdateGuildSettingsC2S updateGuildSettingsC2S = new UpdateGuildSettingsC2S();
				if (msgCase_ == MsgOneofCase.UpdateGuildSettingsC2S)
				{
					updateGuildSettingsC2S.MergeFrom(UpdateGuildSettingsC2S);
				}
				input.ReadMessage(updateGuildSettingsC2S);
				UpdateGuildSettingsC2S = updateGuildSettingsC2S;
				break;
			}
			case 72130u:
			{
				UpdateGuildSettingsS2C updateGuildSettingsS2C = new UpdateGuildSettingsS2C();
				if (msgCase_ == MsgOneofCase.UpdateGuildSettingsS2C)
				{
					updateGuildSettingsS2C.MergeFrom(UpdateGuildSettingsS2C);
				}
				input.ReadMessage(updateGuildSettingsS2C);
				UpdateGuildSettingsS2C = updateGuildSettingsS2C;
				break;
			}
			case 72138u:
			{
				UpdateGuildInAnnouncementC2S updateGuildInAnnouncementC2S = new UpdateGuildInAnnouncementC2S();
				if (msgCase_ == MsgOneofCase.UpdateGuildInAnnouncementC2S)
				{
					updateGuildInAnnouncementC2S.MergeFrom(UpdateGuildInAnnouncementC2S);
				}
				input.ReadMessage(updateGuildInAnnouncementC2S);
				UpdateGuildInAnnouncementC2S = updateGuildInAnnouncementC2S;
				break;
			}
			case 72146u:
			{
				UpdateGuildInAnnouncementS2C updateGuildInAnnouncementS2C = new UpdateGuildInAnnouncementS2C();
				if (msgCase_ == MsgOneofCase.UpdateGuildInAnnouncementS2C)
				{
					updateGuildInAnnouncementS2C.MergeFrom(UpdateGuildInAnnouncementS2C);
				}
				input.ReadMessage(updateGuildInAnnouncementS2C);
				UpdateGuildInAnnouncementS2C = updateGuildInAnnouncementS2C;
				break;
			}
			case 72154u:
			{
				TransferGuildMasterC2S transferGuildMasterC2S = new TransferGuildMasterC2S();
				if (msgCase_ == MsgOneofCase.TransferGuildMasterC2S)
				{
					transferGuildMasterC2S.MergeFrom(TransferGuildMasterC2S);
				}
				input.ReadMessage(transferGuildMasterC2S);
				TransferGuildMasterC2S = transferGuildMasterC2S;
				break;
			}
			case 72162u:
			{
				TransferGuildMasterS2C transferGuildMasterS2C = new TransferGuildMasterS2C();
				if (msgCase_ == MsgOneofCase.TransferGuildMasterS2C)
				{
					transferGuildMasterS2C.MergeFrom(TransferGuildMasterS2C);
				}
				input.ReadMessage(transferGuildMasterS2C);
				TransferGuildMasterS2C = transferGuildMasterS2C;
				break;
			}
			case 72170u:
			{
				ChangeGuildMemberTitleC2S changeGuildMemberTitleC2S = new ChangeGuildMemberTitleC2S();
				if (msgCase_ == MsgOneofCase.ChangeGuildMemberTitleC2S)
				{
					changeGuildMemberTitleC2S.MergeFrom(ChangeGuildMemberTitleC2S);
				}
				input.ReadMessage(changeGuildMemberTitleC2S);
				ChangeGuildMemberTitleC2S = changeGuildMemberTitleC2S;
				break;
			}
			case 72178u:
			{
				ChangeGuildMemberTitleS2C changeGuildMemberTitleS2C = new ChangeGuildMemberTitleS2C();
				if (msgCase_ == MsgOneofCase.ChangeGuildMemberTitleS2C)
				{
					changeGuildMemberTitleS2C.MergeFrom(ChangeGuildMemberTitleS2C);
				}
				input.ReadMessage(changeGuildMemberTitleS2C);
				ChangeGuildMemberTitleS2C = changeGuildMemberTitleS2C;
				break;
			}
			case 72186u:
			{
				KickGuildMemberC2S kickGuildMemberC2S = new KickGuildMemberC2S();
				if (msgCase_ == MsgOneofCase.KickGuildMemberC2S)
				{
					kickGuildMemberC2S.MergeFrom(KickGuildMemberC2S);
				}
				input.ReadMessage(kickGuildMemberC2S);
				KickGuildMemberC2S = kickGuildMemberC2S;
				break;
			}
			case 72194u:
			{
				KickGuildMemberS2C kickGuildMemberS2C = new KickGuildMemberS2C();
				if (msgCase_ == MsgOneofCase.KickGuildMemberS2C)
				{
					kickGuildMemberS2C.MergeFrom(KickGuildMemberS2C);
				}
				input.ReadMessage(kickGuildMemberS2C);
				KickGuildMemberS2C = kickGuildMemberS2C;
				break;
			}
			case 72202u:
			{
				ImpeachGuildMasterC2S impeachGuildMasterC2S = new ImpeachGuildMasterC2S();
				if (msgCase_ == MsgOneofCase.ImpeachGuildMasterC2S)
				{
					impeachGuildMasterC2S.MergeFrom(ImpeachGuildMasterC2S);
				}
				input.ReadMessage(impeachGuildMasterC2S);
				ImpeachGuildMasterC2S = impeachGuildMasterC2S;
				break;
			}
			case 72210u:
			{
				ImpeachGuildMasterS2C impeachGuildMasterS2C = new ImpeachGuildMasterS2C();
				if (msgCase_ == MsgOneofCase.ImpeachGuildMasterS2C)
				{
					impeachGuildMasterS2C.MergeFrom(ImpeachGuildMasterS2C);
				}
				input.ReadMessage(impeachGuildMasterS2C);
				ImpeachGuildMasterS2C = impeachGuildMasterS2C;
				break;
			}
			case 72218u:
			{
				ExitGuildC2S exitGuildC2S = new ExitGuildC2S();
				if (msgCase_ == MsgOneofCase.ExitGuildC2S)
				{
					exitGuildC2S.MergeFrom(ExitGuildC2S);
				}
				input.ReadMessage(exitGuildC2S);
				ExitGuildC2S = exitGuildC2S;
				break;
			}
			case 72226u:
			{
				ExitGuildS2C exitGuildS2C = new ExitGuildS2C();
				if (msgCase_ == MsgOneofCase.ExitGuildS2C)
				{
					exitGuildS2C.MergeFrom(ExitGuildS2C);
				}
				input.ReadMessage(exitGuildS2C);
				ExitGuildS2C = exitGuildS2C;
				break;
			}
			case 72234u:
			{
				DisbandGuildC2S disbandGuildC2S = new DisbandGuildC2S();
				if (msgCase_ == MsgOneofCase.DisbandGuildC2S)
				{
					disbandGuildC2S.MergeFrom(DisbandGuildC2S);
				}
				input.ReadMessage(disbandGuildC2S);
				DisbandGuildC2S = disbandGuildC2S;
				break;
			}
			case 72242u:
			{
				DisbandGuildS2C disbandGuildS2C = new DisbandGuildS2C();
				if (msgCase_ == MsgOneofCase.DisbandGuildS2C)
				{
					disbandGuildS2C.MergeFrom(DisbandGuildS2C);
				}
				input.ReadMessage(disbandGuildS2C);
				DisbandGuildS2C = disbandGuildS2C;
				break;
			}
			case 72250u:
			{
				GuildMissionRewardC2S guildMissionRewardC2S = new GuildMissionRewardC2S();
				if (msgCase_ == MsgOneofCase.GuildMissionRewardC2S)
				{
					guildMissionRewardC2S.MergeFrom(GuildMissionRewardC2S);
				}
				input.ReadMessage(guildMissionRewardC2S);
				GuildMissionRewardC2S = guildMissionRewardC2S;
				break;
			}
			case 72258u:
			{
				GuildMissionRewardS2C guildMissionRewardS2C = new GuildMissionRewardS2C();
				if (msgCase_ == MsgOneofCase.GuildMissionRewardS2C)
				{
					guildMissionRewardS2C.MergeFrom(GuildMissionRewardS2C);
				}
				input.ReadMessage(guildMissionRewardS2C);
				GuildMissionRewardS2C = guildMissionRewardS2C;
				break;
			}
			case 72266u:
			{
				GetGuildMemberChangeMsgC2S getGuildMemberChangeMsgC2S = new GetGuildMemberChangeMsgC2S();
				if (msgCase_ == MsgOneofCase.GetGuildMemberChangeMsgC2S)
				{
					getGuildMemberChangeMsgC2S.MergeFrom(GetGuildMemberChangeMsgC2S);
				}
				input.ReadMessage(getGuildMemberChangeMsgC2S);
				GetGuildMemberChangeMsgC2S = getGuildMemberChangeMsgC2S;
				break;
			}
			case 72274u:
			{
				GetGuildMemberChangeMsgS2C getGuildMemberChangeMsgS2C = new GetGuildMemberChangeMsgS2C();
				if (msgCase_ == MsgOneofCase.GetGuildMemberChangeMsgS2C)
				{
					getGuildMemberChangeMsgS2C.MergeFrom(GetGuildMemberChangeMsgS2C);
				}
				input.ReadMessage(getGuildMemberChangeMsgS2C);
				GetGuildMemberChangeMsgS2C = getGuildMemberChangeMsgS2C;
				break;
			}
			case 72282u:
			{
				SendGuildChatMsgC2S sendGuildChatMsgC2S = new SendGuildChatMsgC2S();
				if (msgCase_ == MsgOneofCase.SendGuildChatMsgC2S)
				{
					sendGuildChatMsgC2S.MergeFrom(SendGuildChatMsgC2S);
				}
				input.ReadMessage(sendGuildChatMsgC2S);
				SendGuildChatMsgC2S = sendGuildChatMsgC2S;
				break;
			}
			case 72290u:
			{
				SendGuildChatMsgS2C sendGuildChatMsgS2C = new SendGuildChatMsgS2C();
				if (msgCase_ == MsgOneofCase.SendGuildChatMsgS2C)
				{
					sendGuildChatMsgS2C.MergeFrom(SendGuildChatMsgS2C);
				}
				input.ReadMessage(sendGuildChatMsgS2C);
				SendGuildChatMsgS2C = sendGuildChatMsgS2C;
				break;
			}
			case 72298u:
			{
				GetGuildChatMsgC2S getGuildChatMsgC2S = new GetGuildChatMsgC2S();
				if (msgCase_ == MsgOneofCase.GetGuildChatMsgC2S)
				{
					getGuildChatMsgC2S.MergeFrom(GetGuildChatMsgC2S);
				}
				input.ReadMessage(getGuildChatMsgC2S);
				GetGuildChatMsgC2S = getGuildChatMsgC2S;
				break;
			}
			case 72306u:
			{
				GetGuildChatMsgS2C getGuildChatMsgS2C = new GetGuildChatMsgS2C();
				if (msgCase_ == MsgOneofCase.GetGuildChatMsgS2C)
				{
					getGuildChatMsgS2C.MergeFrom(GetGuildChatMsgS2C);
				}
				input.ReadMessage(getGuildChatMsgS2C);
				GetGuildChatMsgS2C = getGuildChatMsgS2C;
				break;
			}
			case 72314u:
			{
				GuildMemberC2S guildMemberC2S = new GuildMemberC2S();
				if (msgCase_ == MsgOneofCase.GuildMemberC2S)
				{
					guildMemberC2S.MergeFrom(GuildMemberC2S);
				}
				input.ReadMessage(guildMemberC2S);
				GuildMemberC2S = guildMemberC2S;
				break;
			}
			case 72322u:
			{
				GuildMemberS2C guildMemberS2C = new GuildMemberS2C();
				if (msgCase_ == MsgOneofCase.GuildMemberS2C)
				{
					guildMemberS2C.MergeFrom(GuildMemberS2C);
				}
				input.ReadMessage(guildMemberS2C);
				GuildMemberS2C = guildMemberS2C;
				break;
			}
			case 72330u:
			{
				GetGuildsInfoC2S getGuildsInfoC2S = new GetGuildsInfoC2S();
				if (msgCase_ == MsgOneofCase.GetGuildsInfoC2S)
				{
					getGuildsInfoC2S.MergeFrom(GetGuildsInfoC2S);
				}
				input.ReadMessage(getGuildsInfoC2S);
				GetGuildsInfoC2S = getGuildsInfoC2S;
				break;
			}
			case 72338u:
			{
				GetGuildsInfoS2C getGuildsInfoS2C = new GetGuildsInfoS2C();
				if (msgCase_ == MsgOneofCase.GetGuildsInfoS2C)
				{
					getGuildsInfoS2C.MergeFrom(GetGuildsInfoS2C);
				}
				input.ReadMessage(getGuildsInfoS2C);
				GetGuildsInfoS2C = getGuildsInfoS2C;
				break;
			}
			case 400002u:
			{
				TestRpcEchoC2S testRpcEchoC2S = new TestRpcEchoC2S();
				if (msgCase_ == MsgOneofCase.TestRpcEchoC2S)
				{
					testRpcEchoC2S.MergeFrom(TestRpcEchoC2S);
				}
				input.ReadMessage(testRpcEchoC2S);
				TestRpcEchoC2S = testRpcEchoC2S;
				break;
			}
			case 400010u:
			{
				TestRpcEchoS2C testRpcEchoS2C = new TestRpcEchoS2C();
				if (msgCase_ == MsgOneofCase.TestRpcEchoS2C)
				{
					testRpcEchoS2C.MergeFrom(TestRpcEchoS2C);
				}
				input.ReadMessage(testRpcEchoS2C);
				TestRpcEchoS2C = testRpcEchoS2C;
				break;
			}
			}
		}
	}
}
