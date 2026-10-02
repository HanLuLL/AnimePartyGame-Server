using Google.Protobuf.Reflection;
using UnityEngine;

public enum UIWindowType
{
	[InspectorName("无")]
	[OriginalName("UIWindowType_None")]
	None = 0,
	[InspectorName("加油站")]
	[OriginalName("UIWindowType_LandFillingStation")]
	LandFillingStation = 100,
	[InspectorName("飞门")]
	[OriginalName("UIWindowType_LandPursuit")]
	LandPursuit = 102,
	[InspectorName("占卜")]
	[OriginalName("UIWindowType_LandDivination")]
	LandDivination = 103,
	[InspectorName("商店")]
	[OriginalName("UIWindowType_LandShop")]
	LandShop = 104,
	[InspectorName("事件")]
	[OriginalName("UIWindowType_LandEvent")]
	LandEvent = 105,
	[InspectorName("彩票")]
	[OriginalName("UIWindowType_LandLottery")]
	LandLottery = 106,
	[InspectorName("赌博")]
	[OriginalName("UIWindowType_LandGamble")]
	LandGamble = 107,
	[InspectorName("炮台")]
	[OriginalName("UIWindowType_LandBattery")]
	LandBattery = 108,
	[InspectorName("Roll金")]
	[OriginalName("UIWindowType_LandRollGold")]
	LandRollGold = 109,
	[InspectorName("医院")]
	[OriginalName("UIWindowType_LandHospital")]
	LandHospital = 111,
	[InspectorName("筹码格")]
	[OriginalName("UIWindowType_LandRelic")]
	LandRelic = 112,
	[InspectorName("糖果格")]
	[OriginalName("UIWindowType_LandVendor")]
	LandVendor = 113,
	[InspectorName("对战")]
	[OriginalName("UIWindowType_Fight")]
	Fight = 201,
	[InspectorName("卡牌")]
	[OriginalName("UIWindowType_Card")]
	Card = 203,
	[InspectorName("技能")]
	[OriginalName("UIWindowType_Skill")]
	Skill = 204,
	[InspectorName("作战玩家详情")]
	[OriginalName("UIWindowType_BattlePlayerInfo")]
	BattlePlayerInfo = 205,
	[InspectorName("说明")]
	[OriginalName("UIWindowType_Explain")]
	Explain = 206,
	[InspectorName("丢弃卡牌")]
	[OriginalName("UIWindowType_LoseCard")]
	LoseCard = 207,
	[InspectorName("表情")]
	[OriginalName("UIWindowType_Expression")]
	Expression = 208,
	[InspectorName("升级")]
	[OriginalName("UIWindowType_Upgrade")]
	Upgrade = 209,
	[InspectorName("遗物")]
	[OriginalName("UIWindowType_Relic")]
	Relic = 210,
	[InspectorName("预览怪兽")]
	[OriginalName("UIWindowType_BattlePreMonster")]
	BattlePreMonster = 211,
	[InspectorName("选择怪兽")]
	[OriginalName("UIWindowType_BattleSelectMonster")]
	BattleSelectMonster = 212,
	[InspectorName("星币转账")]
	[OriginalName("UIWindowType_ATM")]
	Atm = 213,
	[InspectorName("祈愿")]
	[OriginalName("UIWindowType_LandPray")]
	LandPray = 214,
	[InspectorName("剧情")]
	[OriginalName("UIWindowType_Story")]
	Story = 215,
	[InspectorName("援助投票")]
	[OriginalName("UIWindowType_AssistVote")]
	AssistVote = 216,
	[InspectorName("援助投票S7")]
	[OriginalName("UIWindowType_AssistVoteS7")]
	AssistVoteS7 = 217,
	[InspectorName("对战提示")]
	[OriginalName("UIWindowType_BattleHint")]
	BattleHint = 218,
	[InspectorName("提示")]
	[OriginalName("UIWindowType_Tips")]
	Tips = 219,
	[InspectorName("幸运星任务")]
	[OriginalName("UIWindowType_LuckyStarMission")]
	LuckyStarMission = 220,
	[InspectorName("表情列表")]
	[OriginalName("UIWindowType_ExpressionList")]
	ExpressionList = 221,
	[InspectorName("账户信息")]
	[OriginalName("UIWindowType_AccountInfo")]
	AccountInfo = 300,
	[InspectorName("聊天")]
	[OriginalName("UIWindowType_Chat")]
	Chat = 301,
	[InspectorName("皮肤展示")]
	[OriginalName("UIWindowType_SkinShow")]
	SkinShow = 302,
	[InspectorName("规则说明")]
	[OriginalName("UIWindowType_Rule")]
	Rule = 303,
	[InspectorName("道具详情")]
	[OriginalName("UIWindowType_PropDetail")]
	PropDetail = 304,
	[InspectorName("商店购买")]
	[OriginalName("UIWindowType_Purchase")]
	Purchase = 305,
	[InspectorName("宝箱道具")]
	[OriginalName("UIWindowType_BoxProp")]
	BoxProp = 306,
	[InspectorName("奖励")]
	[OriginalName("UIWindowType_Reward")]
	Reward = 307,
	[InspectorName("充值提示")]
	[OriginalName("UIWindowType_RechargeTip")]
	RechargeTip = 308,
	[InspectorName("公告")]
	[OriginalName("UIWindowType_Notice")]
	Notice = 309,
	[InspectorName("信息盒")]
	[OriginalName("UIWindowType_MessageBox")]
	MessageBox = 310,
	[InspectorName("加载提示")]
	[OriginalName("UIWindowType_LoadingTip")]
	LoadingTip = 311,
	[InspectorName("游戏信息")]
	[OriginalName("UIWindowType_GameInfo")]
	GameInfo = 312,
	[InspectorName("系统Tips")]
	[OriginalName("UIWindowType_SystemTips")]
	SystemTips = 313,
	[InspectorName("输入")]
	[OriginalName("UIWindowType_Input")]
	Input = 314,
	[InspectorName("邀请")]
	[OriginalName("UIWindowType_Invite")]
	Invite = 315,
	[InspectorName("活动弹窗")]
	[OriginalName("UIWindowType_ActivityPopup")]
	ActivityPopup = 316,
	[InspectorName("抽卡信息")]
	[OriginalName("UIWindowType_GachaInfo")]
	GachaInfo = 317,
	[InspectorName("对战筹码详情")]
	[OriginalName("UIWindowType_BattleRelicInfo")]
	BattleRelicInfo = 318,
	[InspectorName("引导")]
	[OriginalName("UIWindowType_Guide")]
	Guide = 319,
	[InspectorName("教程选择")]
	[OriginalName("UIWindowType_SelectTutorial")]
	SelectTutorial = 320,
	[InspectorName("信息弹窗")]
	[OriginalName("UIWindowType_Info")]
	Info = 321,
	[InspectorName("房间筛选")]
	[OriginalName("UIWindowType_RoomFilter")]
	RoomFilter = 322,
	[InspectorName("主播女孩商店")]
	[OriginalName("UIWindowType_NGOStore")]
	Ngostore = 323,
	[InspectorName("注册年龄")]
	[OriginalName("UIWindowType_RegisterAge")]
	RegisterAge = 324,
	[InspectorName("签到")]
	[OriginalName("UIWindowType_SignIn")]
	SignIn = 325,
	[InspectorName("玩家改名")]
	[OriginalName("UIWindowType_PlayerRename")]
	PlayerRename = 326,
	[InspectorName("用户协议")]
	[OriginalName("UIWindowType_UserAgreement")]
	UserAgreement = 327,
	[InspectorName("折扣")]
	[OriginalName("UIWindowType_Discount")]
	Discount = 328,
	[InspectorName("计时")]
	[OriginalName("UIWindowType_OperateTime")]
	OperateTime = 329,
	[InspectorName("加载")]
	[OriginalName("UIWindowType_Loading")]
	Loading = 330,
	[InspectorName("展示卡牌")]
	[OriginalName("UIWindowType_DisplayCard")]
	DisplayCard = 331,
	[InspectorName("图鉴")]
	[OriginalName("UIWindowType_GameLibrary")]
	GameLibrary = 332,
	[InspectorName("魔女兵器")]
	[OriginalName("UIWindowType_WitchWeapon")]
	WitchWeapon = 333,
	[InspectorName("皮肤捆绑售卖")]
	[OriginalName("UIWindowType_SkinSell")]
	SkinSell = 334,
	[InspectorName("赛博酒保")]
	[OriginalName("UIWindowType_VA11HallA")]
	Va11HallA = 335,
	[InspectorName("匹配成功")]
	[OriginalName("UIWindowType_MatchSuccess")]
	MatchSuccess = 336,
	[InspectorName("设置")]
	[OriginalName("UIWindowType_Setting")]
	Setting = 337,
	[InspectorName("设置对战")]
	[OriginalName("UIWindowType_SettingInBattle")]
	SettingInBattle = 338,
	[InspectorName("设置名单")]
	[OriginalName("UIWindowType_SettingList")]
	SettingList = 339,
	[InspectorName("单人玩法对局设置")]
	[OriginalName("UIWindowType_SinglePlayerSettingInBattle")]
	SinglePlayerSettingInBattle = 340,
	[InspectorName("新手卡池奖励")]
	[OriginalName("UIWindowType_GachaRookieReward")]
	GachaRookieReward = 341,
	[InspectorName("匹配信息")]
	[OriginalName("UIWindowType_MatchInfo")]
	MatchInfo = 342,
	[InspectorName("教程")]
	[OriginalName("UIWindowType_Tutorial")]
	Tutorial = 343,
	[InspectorName("新手任务奖励")]
	[OriginalName("UIWindowType_RookieTaskInfo")]
	RookieTaskInfo = 344,
	[InspectorName("好友排行榜")]
	[OriginalName("UIWindowType_FriendLeaderboard")]
	FriendLeaderboard = 345,
	[InspectorName("抖音侧边栏奖励")]
	[OriginalName("UIWindowType_DouYinReward")]
	DouYinReward = 346,
	[InspectorName("周年庆礼包")]
	[OriginalName("UIWindowType_Anniversary_2Nd")]
	Anniversary2Nd = 347,
	[InspectorName("新图鉴")]
	[OriginalName("UIWindowType_NewGameLibrary")]
	NewGameLibrary = 348,
	[InspectorName("对战全屏视频")]
	[OriginalName("UIWindowType_BattleVideo")]
	BattleVideo = 349,
	[InspectorName("对战地图格提示")]
	[OriginalName("UIWindowType_BattleLandTip")]
	BattleLandTip = 350,
	[InspectorName("词条提示")]
	[OriginalName("UIWindowType_RoomTerms")]
	RoomTerms = 351,
	[InspectorName("轮次自选奖励")]
	[OriginalName("UIWindowType_ChooseRoundCard")]
	ChooseRoundCard = 352,
	[InspectorName("信誉分提示")]
	[OriginalName("UIWindowType_CreditWarning")]
	CreditWarning = 353,
	[InspectorName("魔裁联动商店")]
	[OriginalName("UIWindowType_MGWTStore")]
	Mgwtstore = 354,
	[InspectorName("回放窗口")]
	[OriginalName("UIWindowType_Replay")]
	Replay = 355,
	[InspectorName("调查问卷")]
	[OriginalName("UIWindowType_SurveyCenter")]
	SurveyCenter = 356,
	[InspectorName("GM")]
	[OriginalName("UIWindowType_GM")]
	Gm = 999
}
