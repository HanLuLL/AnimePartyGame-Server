using Google.Protobuf.Reflection;
using UnityEngine;

public enum MissionConditionType
{
	[InspectorName("无")]
	[OriginalName("MissionConditionType_None")]
	None = 0,
	[InspectorName("累计参与天数")]
	[OriginalName("MissionConditionType_Total_PlayDays")]
	TotalPlayDays = 1,
	[InspectorName("地图胜利次数")]
	[OriginalName("MissionConditionType_Total_Map_Win_Times")]
	TotalMapWinTimes = 2,
	[InspectorName("地图难度胜利次数")]
	[OriginalName("MissionConditionType_Total_PVEWin_Difficulty_Times")]
	TotalPvewinDifficultyTimes = 3,
	[InspectorName("地图难度起胜利次数")]
	[OriginalName("MissionConditionType_Total_PVEWin_DifficultyOrHarder_Times")]
	TotalPvewinDifficultyOrHarderTimes = 4,
	[InspectorName("累计击倒怪物")]
	[OriginalName("MissionConditionType_Total_Kill_Unit_Count")]
	TotalKillUnitCount = 5,
	[InspectorName("每日登录")]
	[OriginalName("MissionConditionType_DailyLogin")]
	DailyLogin = 6,
	[InspectorName("累计参与模式")]
	[OriginalName("MissionConditionType_Total_MapModePlayCount")]
	TotalMapModePlayCount = 7,
	[InspectorName("累计参与PVE天数")]
	[OriginalName("MissionConditionType_Total_PlayDays_PVE")]
	TotalPlayDaysPve = 8,
	[InspectorName("完成指定任务")]
	[OriginalName("MissionConditionType_SpecialMissionComplete")]
	SpecialMissionComplete = 9,
	[InspectorName("结算点赞")]
	[OriginalName("MissionConditionType_SettlementLike")]
	SettlementLike = 10,
	[InspectorName("星币商店购买")]
	[OriginalName("MissionConditionType_StarCoinShopBuy")]
	StarCoinShopBuy = 11,
	[InspectorName("累计胜利")]
	[OriginalName("MissionConditionType_Total_WinGameCount")]
	TotalWinGameCount = 12,
	[InspectorName("解锁道具")]
	[OriginalName("MissionConditionType_ItemUnlock")]
	ItemUnlock = 13,
	[InspectorName("累计使用角色胜利")]
	[OriginalName("MissionConditionType_Total_Character_Win")]
	TotalCharacterWin = 14,
	[InspectorName("累计糖果使用")]
	[OriginalName("MissionConditionType_Total_Candy_Use")]
	TotalCandyUse = 15,
	[InspectorName("达到PVE等级")]
	[OriginalName("MissionConditionType_PVE_Level")]
	PveLevel = 16,
	[InspectorName("累计参与有效模式天数")]
	[OriginalName("MissionConditionType_Total_ValidMode_PlayDays")]
	TotalValidModePlayDays = 17,
	[InspectorName("累计参与有效模式场次")]
	[OriginalName("MissionConditionType_Total_ValidMode_PlayCount")]
	TotalValidModePlayCount = 18,
	[InspectorName("累计有效模式胜利")]
	[OriginalName("MissionConditionType_Total_ValidMode_Win_Times")]
	TotalValidModeWinTimes = 19,
	[InspectorName("冲刺登录")]
	[OriginalName("MissionConditionType_Countdown_Login")]
	CountdownLogin = 20,
	[InspectorName("曲子完成总数")]
	[OriginalName("MissionConditionType_Total_Music_Conut")]
	TotalMusicConut = 21,
	[InspectorName("团队击杀总数")]
	[OriginalName("MissionConditionType_Total_Team_Kill_Count")]
	TotalTeamKillCount = 22,
	[InspectorName("单人模式胜利次数")]
	[OriginalName("MissionConditionType_Total_SingleMode_Win_Times")]
	TotalSingleModeWinTimes = 23,
	[InspectorName("单人模式击杀怪物")]
	[OriginalName("MissionConditionType_Total_SingleMode_MonsterKill")]
	TotalSingleModeMonsterKill = 24,
	[InspectorName("单人模式升级建筑")]
	[OriginalName("MissionConditionType_Total_SingleMode_BulidingUpgrade")]
	TotalSingleModeBulidingUpgrade = 25,
	[InspectorName("参与并完成PVE匹配")]
	[OriginalName("MissionConditionType_PVE_Match_Complete")]
	PveMatchComplete = 26,
	[InspectorName("角色培养")]
	[OriginalName("MissionConditionType_PVE_LevelUp_To")]
	PveLevelUpTo = 27,
	[InspectorName("赠送礼物")]
	[OriginalName("MissionConditionType_GiftGive")]
	GiftGive = 28,
	[InspectorName("完成抽卡")]
	[OriginalName("MissionConditionType_Gacha_Draw")]
	GachaDraw = 29,
	[InspectorName("完成新手教学")]
	[OriginalName("MissionConditionType_NoviceLevelDone")]
	NoviceLevelDone = 30,
	[InspectorName("参与并完成PVP匹配")]
	[OriginalName("MissionConditionType_PVP_Match_Complete")]
	PvpMatchComplete = 31,
	[InspectorName("账号等级")]
	[OriginalName("MissionConditionType_Nurturance_PlayerLevel")]
	NurturancePlayerLevel = 32,
	[InspectorName("参与并完成匹配")]
	[OriginalName("MissionConditionType_Match_Complete")]
	MatchComplete = 33,
	[InspectorName("角色培养次数")]
	[OriginalName("MissionConditionType_PVE_LevelUp_Times")]
	PveLevelUpTimes = 34,
	[InspectorName("单人战役通关")]
	[OriginalName("MissionConditionType_Campaign_LevelPass")]
	CampaignLevelPass = 35,
	[InspectorName("完成新手阶段任务")]
	[OriginalName("MissionConditionType_Rookie_Mission_Complete")]
	RookieMissionComplete = 36,
	[InspectorName("任意商店任意商品购买次数")]
	[OriginalName("MissionConditionType_Total_ShopBuy_Count")]
	TotalShopBuyCount = 37,
	[InspectorName("地图通行证地图难度起胜利次数")]
	[OriginalName("MissionConditionType_BattlePass_Map")]
	BattlePassMap = 38,
	[InspectorName("等级通行证账号等级")]
	[OriginalName("MissionConditionType_BattlePass_Level")]
	BattlePassLevel = 39,
	[InspectorName("单人模式完成回合")]
	[OriginalName("MissionConditionType_Total_SingleMode_Round_Count")]
	TotalSingleModeRoundCount = 40,
	[InspectorName("匹配获得角色")]
	[OriginalName("MissionConditionType_Match_Obtain_Character")]
	MatchObtainCharacter = 41,
	[InspectorName("无条件点击领取")]
	[OriginalName("MissionConditionType_ClickToClaim")]
	ClickToClaim = 42,
	[InspectorName("骰骰乐完成圈数")]
	[OriginalName("MissionConditionType_ActivityDice_Round_Count")]
	ActivityDiceRoundCount = 43,
	[InspectorName("骰骰乐累计进度值")]
	[OriginalName("MissionConditionType_ActivityDice_Total_EXP")]
	ActivityDiceTotalExp = 44,
	[InspectorName("和好友完成游戏")]
	[OriginalName("MissionConditionType_GameWithFriend")]
	GameWithFriend = 45,
	[InspectorName("单人模式指定关卡胜利")]
	[OriginalName("MissionConditionType_Total_SingleMode_Map_Win")]
	TotalSingleModeMapWin = 46,
	[InspectorName("单人模式建筑获得星币")]
	[OriginalName("MissionConditionType_Total_SingleMode_StarCoin_ByBuilding")]
	TotalSingleModeStarCoinByBuilding = 47,
	[InspectorName("累计参与幸运星争夺战天数")]
	[OriginalName("MissionConditionType_Total_PlayDays_LuckyStarBattle")]
	TotalPlayDaysLuckyStarBattle = 48,
	[InspectorName("幸运星争夺战胜利次数")]
	[OriginalName("MissionConditionType_LuckyStarBattle_Win_Times")]
	LuckyStarBattleWinTimes = 49,
	[InspectorName("团队获得幸运星数量")]
	[OriginalName("MissionConditionType_LuckyStar_Count")]
	LuckyStarCount = 50,
	[InspectorName("和好友完成幸运星争夺战")]
	[OriginalName("MissionConditionType_LuckyStarBattle_With_Friend")]
	LuckyStarBattleWithFriend = 51,
	[InspectorName("幸运星争夺战击倒人数")]
	[OriginalName("MissionConditionType_LuckyStarBattle_KillCount")]
	LuckyStarBattleKillCount = 52,
	[InspectorName("累计奖章获得数")]
	[OriginalName("MissionConditionType_Medal_Count")]
	MedalCount = 53,
	[InspectorName("任意角色获得全部奖章")]
	[OriginalName("MissionConditionType_Character_Medal_GetAll")]
	CharacterMedalGetAll = 54,
	[InspectorName("获得全部奖章的角色数量")]
	[OriginalName("MissionConditionType_Character_Medal_GetAll_Count")]
	CharacterMedalGetAllCount = 55,
	[InspectorName("运动会PVE参与次数")]
	[OriginalName("MissionConditionType_MutatorPVE_Play_Count")]
	MutatorPvePlayCount = 56,
	[InspectorName("运动会PVE团队击杀")]
	[OriginalName("MissionConditionType_MutatorPVE_Monster_KillCount")]
	MutatorPveMonsterKillCount = 57,
	[InspectorName("完成宾果翻牌轮次")]
	[OriginalName("MissionConditionType_BingoFlip_Round_Count")]
	BingoFlipRoundCount = 58,
	[InspectorName("魔裁联动玩法收集线索数量")]
	[OriginalName("MissionConditionType_MGWTPVE_Collected_Clues_Count")]
	MgwtpveCollectedCluesCount = 59,
	[InspectorName("与公会成员完成对局次数")]
	[OriginalName("MissionConditionType_Total_GuildMember_CompleteParty_Times")]
	TotalGuildMemberCompletePartyTimes = 60,
	[InspectorName("公会累计签到人数")]
	[OriginalName("MissionConditionType_Total_GuildMember_Sign_Count")]
	TotalGuildMemberSignCount = 61,
	[InspectorName("抖音每日领取")]
	[OriginalName("MissionConditionType_TikTok_Daily")]
	TikTokDaily = 98,
	[InspectorName("客户端确认")]
	[OriginalName("MissionConditionType_ClickConfirm")]
	ClickConfirm = 99
}
