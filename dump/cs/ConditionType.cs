using Google.Protobuf.Reflection;
using UnityEngine;

public enum ConditionType
{
	[InspectorName("无")]
	[OriginalName("ConditionType_None")]
	None = 0,
	[InspectorName("完成新手教学")]
	[OriginalName("ConditionType_NoviceLevelDone")]
	NoviceLevelDone = 1,
	[InspectorName("累计击杀数量")]
	[OriginalName("ConditionType_Total_KillCount")]
	TotalKillCount = 2,
	[InspectorName("累计直接伤害")]
	[OriginalName("ConditionType_Total_DirectDamage")]
	TotalDirectDamage = 3,
	[InspectorName("累计死亡次数")]
	[OriginalName("ConditionType_Total_DieCount")]
	TotalDieCount = 4,
	[InspectorName("累计承受伤害")]
	[OriginalName("ConditionType_Total_Injured")]
	TotalInjured = 5,
	[InspectorName("累计陷阱次数")]
	[OriginalName("ConditionType_Total_TrappedCount")]
	TotalTrappedCount = 6,
	[InspectorName("累计放置陷阱")]
	[OriginalName("ConditionType_Total_SetTrapCount")]
	TotalSetTrapCount = 7,
	[InspectorName("累计放置路障")]
	[OriginalName("ConditionType_Total_SetBarricadeCount")]
	TotalSetBarricadeCount = 8,
	[InspectorName("累计被雷劈")]
	[OriginalName("ConditionType_Total_SufferThunderCount")]
	TotalSufferThunderCount = 9,
	[InspectorName("累计住院")]
	[OriginalName("ConditionType_Total_HospitalizedCount")]
	TotalHospitalizedCount = 10,
	[InspectorName("累计彩票中奖")]
	[OriginalName("ConditionType_Total_WinLotteryCount")]
	TotalWinLotteryCount = 11,
	[InspectorName("累计击杀小怪兽")]
	[OriginalName("ConditionType_Total_KillBossCount")]
	TotalKillBossCount = 12,
	[InspectorName("累计胜利")]
	[OriginalName("ConditionType_Total_WinGameCount")]
	TotalWinGameCount = 13,
	[InspectorName("累计参与")]
	[OriginalName("ConditionType_Total_DoneGameCount")]
	TotalDoneGameCount = 14,
	[InspectorName("累计登录天数")]
	[OriginalName("ConditionType_Total_LoginDays")]
	TotalLoginDays = 15,
	[InspectorName("累计参与天数")]
	[OriginalName("ConditionType_Total_PlayDays")]
	TotalPlayDays = 16,
	[InspectorName("累计使用角色参与")]
	[OriginalName("ConditionType_Total_Character_Done")]
	TotalCharacterDone = 17,
	[InspectorName("累计使用角色胜利")]
	[OriginalName("ConditionType_Total_Character_Win")]
	TotalCharacterWin = 18,
	[InspectorName("累计击杀小偷")]
	[OriginalName("ConditionType_Total_KillThiefCount")]
	TotalKillThiefCount = 19,
	[InspectorName("累计吃团子次数")]
	[OriginalName("ConditionType_Total_EatDumplingCount")]
	TotalEatDumplingCount = 20,
	[InspectorName("全程未被列车撞到的累计局数")]
	[OriginalName("ConditionType_Total_AvoidTrainGames")]
	TotalAvoidTrainGames = 21,
	[InspectorName("累计获得点赞")]
	[OriginalName("ConditionType_Total_GetThumbsUP")]
	TotalGetThumbsUp = 22,
	[InspectorName("累计参与地图")]
	[OriginalName("ConditionType_Total_MapPlayCount")]
	TotalMapPlayCount = 23,
	[InspectorName("累计参与模式")]
	[OriginalName("ConditionType_Total_MapModePlayCount")]
	TotalMapModePlayCount = 24,
	[InspectorName("累计提升羁绊等级次数")]
	[OriginalName("ConditionType_Total_BondLevelUPCount")]
	TotalBondLevelUpcount = 25,
	[InspectorName("累计参与PVE天数")]
	[OriginalName("ConditionType_Total_PlayDays_PVE")]
	TotalPlayDaysPve = 26,
	[InspectorName("累计PVE胜利")]
	[OriginalName("ConditionType_Total_WinGameCount_PVE")]
	TotalWinGameCountPve = 27,
	[InspectorName("累计PVE强化筹码")]
	[OriginalName("ConditionType_Total_RelicCount_PVE")]
	TotalRelicCountPve = 28,
	[InspectorName("累计任意难度PVE胜利")]
	[OriginalName("ConditionType_Total_WinGameCount_PVE_Any")]
	TotalWinGameCountPveAny = 29,
	[InspectorName("累计刮奖次数")]
	[OriginalName("ConditionType_Total_ScratchoffCount")]
	TotalScratchoffCount = 30,
	[InspectorName("疯狂难度达到3个金色筹码的次数")]
	[OriginalName("ConditionType_Total_Get3GoldRelic_InsaneTimes")]
	TotalGet3GoldRelicInsaneTimes = 31,
	[InspectorName("同时沉3个对手的次数")]
	[OriginalName("ConditionType_Total_SinkAllOpponents_Times")]
	TotalSinkAllOpponentsTimes = 32,
	[InspectorName("累计PVP胜利")]
	[OriginalName("ConditionType_Total_WinGameCount_Standard")]
	TotalWinGameCountStandard = 33,
	[InspectorName("累计激战胜利")]
	[OriginalName("ConditionType_Total_WinGameCount_Ultra")]
	TotalWinGameCountUltra = 34,
	[InspectorName("累计礼物大作战胜利")]
	[OriginalName("ConditionType_Total_WinGameCount_AsymmetricalBattle")]
	TotalWinGameCountAsymmetricalBattle = 35,
	[InspectorName("PVP结算排名")]
	[OriginalName("ConditionType_Total_PVPRank_Times")]
	TotalPvprankTimes = 36,
	[InspectorName("累计PVE胜利普通难度")]
	[OriginalName("ConditionType_Total_PVEWin_Difficulty1_Times")]
	TotalPvewinDifficulty1Times = 37,
	[InspectorName("累计PVE胜利困难难度或更高")]
	[OriginalName("ConditionType_Total_PVEWin_Difficulty2OrHarder_Times")]
	TotalPvewinDifficulty2OrHarderTimes = 38,
	[InspectorName("地图难度胜利次数")]
	[OriginalName("ConditionType_Total_PVEWin_Difficulty_Times")]
	TotalPvewinDifficultyTimes = 39,
	[InspectorName("地图难度起胜利次数")]
	[OriginalName("ConditionType_Total_PVEWin_DifficultyOrHarder_Times")]
	TotalPvewinDifficultyOrHarderTimes = 40,
	[InspectorName("地图胜利次数")]
	[OriginalName("ConditionType_Total_Map_Win_Times")]
	TotalMapWinTimes = 41,
	[InspectorName("累计击倒怪物")]
	[OriginalName("ConditionType_Total_Kill_Unit_Count")]
	TotalKillUnitCount = 42,
	[InspectorName("累计PVE胜利困难难度")]
	[OriginalName("ConditionType_Total_PVEWin_Difficulty2_Times")]
	TotalPvewinDifficulty2Times = 43,
	[InspectorName("累计PVE胜利噩梦难度")]
	[OriginalName("ConditionType_Total_PVEWin_Difficulty3_Times")]
	TotalPvewinDifficulty3Times = 44,
	[InspectorName("累计PVE胜利疯狂难度")]
	[OriginalName("ConditionType_Total_PVEWin_Difficulty4_Times")]
	TotalPvewinDifficulty4Times = 45,
	[InspectorName("累计PVE胜利极限难度")]
	[OriginalName("ConditionType_Total_PVEWin_Difficulty5_Times")]
	TotalPvewinDifficulty5Times = 46,
	[InspectorName("5轮次合成龙珠")]
	[OriginalName("ConditionType_DragonBallSynthesis")]
	DragonBallSynthesis = 47,
	[InspectorName("累计具体地图胜利")]
	[OriginalName("ConditionType_Specify_Map_WinGameCount")]
	SpecifyMapWinGameCount = 48,
	[InspectorName("单局击杀数量")]
	[OriginalName("ConditionType_SingleMax_KillCount")]
	SingleMaxKillCount = 100,
	[InspectorName("最高单次pk伤害")]
	[OriginalName("ConditionType_SingleMax_PkDamage")]
	SingleMaxPkDamage = 101,
	[InspectorName("单局获得星币")]
	[OriginalName("ConditionType_SingleMax_TotalGetGoldNum")]
	SingleMaxTotalGetGoldNum = 102,
	[InspectorName("单局吃住院团子的次数")]
	[OriginalName("ConditionType_SingleMax_EatDumplingToHospitalCount")]
	SingleMaxEatDumplingToHospitalCount = 103,
	[InspectorName("PVE进度15胜利")]
	[OriginalName("ConditionType_SingleMax_Progress15Win_PVE")]
	SingleMaxProgress15WinPve = 104,
	[InspectorName("PVE全员不死通关")]
	[OriginalName("ConditionType_SingleMax_NoDieWin_PVE")]
	SingleMaxNoDieWinPve = 105,
	[InspectorName("噩梦难度速通")]
	[OriginalName("ConditionType_SingleMax_QuickWin_Nightmare")]
	SingleMaxQuickWinNightmare = 106,
	[InspectorName("疯狂难度速通")]
	[OriginalName("ConditionType_SingleMax_QuickWin_Insane")]
	SingleMaxQuickWinInsane = 107,
	[InspectorName("疯狂难度打叛徒牌获胜")]
	[OriginalName("ConditionType_UseTraitorCard_Insane")]
	UseTraitorCardInsane = 108,
	[InspectorName("疯狂难度不吸收茶壶获胜")]
	[OriginalName("ConditionType_NotAbsorbTeapot_Insane")]
	NotAbsorbTeapotInsane = 109,
	[InspectorName("本周登录次数")]
	[OriginalName("ConditionType_LoginWeeklyCount")]
	LoginWeeklyCount = 200,
	[InspectorName("账号等级")]
	[OriginalName("ConditionType_Nurturance_PlayerLevel")]
	NurturancePlayerLevel = 300,
	[InspectorName("拥有角色数量")]
	[OriginalName("ConditionType_Nurturance_RoleCount")]
	NurturanceRoleCount = 301,
	[InspectorName("好感度突破角色数量")]
	[OriginalName("ConditionType_Nurturance_FavorBreakthroughCount")]
	NurturanceFavorBreakthroughCount = 302,
	[InspectorName("拥有骰子数量")]
	[OriginalName("ConditionType_Nurturance_DiceCount")]
	NurturanceDiceCount = 303,
	[InspectorName("好友数量")]
	[OriginalName("ConditionType_Total_FriendsCount")]
	TotalFriendsCount = 304,
	[InspectorName("拥有角色")]
	[OriginalName("ConditionType_Own_Role")]
	OwnRole = 305,
	[InspectorName("单人战役通关")]
	[OriginalName("ConditionType_Campaign_LevelPass")]
	CampaignLevelPass = 306,
	[InspectorName("受邀等级达标人数")]
	[OriginalName("ConditionType_Total_PlayerCount_InvitedLv")]
	TotalPlayerCountInvitedLv = 307,
	[InspectorName("羁绊5级")]
	[OriginalName("ConditionType_FavorLv5")]
	FavorLv5 = 308,
	[InspectorName("星币商店购买")]
	[OriginalName("ConditionType_StarCoinShopBuy")]
	StarCoinShopBuy = 309,
	[InspectorName("结算点赞")]
	[OriginalName("ConditionType_SettlementLike")]
	SettlementLike = 310,
	[InspectorName("任意商店任意商品购买次数")]
	[OriginalName("ConditionType_Total_ShopBuy_Count")]
	TotalShopBuyCount = 311,
	[InspectorName("参与并完成PVE匹配")]
	[OriginalName("ConditionType_PVE_Match_Complete")]
	PveMatchComplete = 312,
	[InspectorName("赠送礼物")]
	[OriginalName("ConditionType_GiftGive")]
	GiftGive = 313,
	[InspectorName("角色培养")]
	[OriginalName("ConditionType_PVE_LevelUp_To")]
	PveLevelUpTo = 314,
	[InspectorName("完成抽卡")]
	[OriginalName("ConditionType_Gacha_Draw")]
	GachaDraw = 315,
	[InspectorName("参与并完成PVP匹配")]
	[OriginalName("ConditionType_PVP_Match_Complete")]
	PvpMatchComplete = 316,
	[InspectorName("单人模式胜利次数")]
	[OriginalName("ConditionType_Total_SingleMode_Win_Times")]
	TotalSingleModeWinTimes = 317,
	[InspectorName("单人模式升级建筑")]
	[OriginalName("ConditionType_Total_SingleMode_BulidingUpgrade")]
	TotalSingleModeBulidingUpgrade = 318,
	[InspectorName("单人模式完成回合")]
	[OriginalName("ConditionType_Total_SingleMode_Round_Count")]
	TotalSingleModeRoundCount = 319,
	[InspectorName("加入公会")]
	[OriginalName("ConditionType_JoinGuild")]
	JoinGuild = 320,
	[InspectorName("客户端确认")]
	[OriginalName("ConditionType_ClickConfirm")]
	ClickConfirm = 400
}
