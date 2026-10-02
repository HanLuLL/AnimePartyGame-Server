using Google.Protobuf.Reflection;
using UnityEngine;

public enum UIPanelType
{
	[InspectorName("无")]
	[OriginalName("UIPanelType_None")]
	None = 0,
	[InspectorName("启动")]
	[OriginalName("UIPanelType_Launcher")]
	Launcher = 10,
	[InspectorName("登录")]
	[OriginalName("UIPanelType_Login")]
	Login = 11,
	[InspectorName("主界面")]
	[OriginalName("UIPanelType_Home")]
	Home = 14,
	[InspectorName("房间列表")]
	[OriginalName("UIPanelType_RoomList")]
	RoomList = 15,
	[InspectorName("房间等待")]
	[OriginalName("UIPanelType_RoomWait")]
	RoomWait = 16,
	[InspectorName("房间角色")]
	[OriginalName("UIPanelType_RoomHero")]
	RoomHero = 17,
	[InspectorName("背包")]
	[OriginalName("UIPanelType_Bag")]
	Bag = 18,
	[InspectorName("邮箱")]
	[OriginalName("UIPanelType_Mail")]
	Mail = 19,
	[InspectorName("抽卡")]
	[OriginalName("UIPanelType_Gacha")]
	Gacha = 20,
	[InspectorName("装扮")]
	[OriginalName("UIPanelType_Fashion")]
	Fashion = 21,
	[InspectorName("任务")]
	[OriginalName("UIPanelType_Task")]
	Task = 22,
	[InspectorName("角色")]
	[OriginalName("UIPanelType_Hero")]
	Hero = 23,
	[InspectorName("活动")]
	[OriginalName("UIPanelType_Activity")]
	Activity = 24,
	[InspectorName("商品推荐")]
	[OriginalName("UIPanelType_ProductRecommendation")]
	ProductRecommendation = 25,
	[InspectorName("商店")]
	[OriginalName("UIPanelType_Store")]
	Store = 26,
	[InspectorName("好友")]
	[OriginalName("UIPanelType_Friend")]
	Friend = 27,
	[InspectorName("观战")]
	[OriginalName("UIPanelType_Watch")]
	Watch = 28,
	[InspectorName("通行证")]
	[OriginalName("UIPanelType_BattlePass")]
	BattlePass = 29,
	[InspectorName("比赛入口")]
	[OriginalName("UIPanelType_MatchEntrance")]
	MatchEntrance = 30,
	[InspectorName("匹配")]
	[OriginalName("UIPanelType_Match")]
	Match = 31,
	[InspectorName("单人战役")]
	[OriginalName("UIPanelType_Campaign")]
	Campaign = 32,
	[InspectorName("单人模式")]
	[OriginalName("UIPanelType_SoloLevel")]
	SoloLevel = 33,
	[InspectorName("练习关卡")]
	[OriginalName("UIPanelType_TrainingLevel")]
	TrainingLevel = 34,
	[InspectorName("主播女孩联动活动")]
	[OriginalName("UIPanelType_ActivityNGO")]
	ActivityNgo = 35,
	[InspectorName("赛博酒保联动活动")]
	[OriginalName("UIPanelType_ActivityVA11HallA")]
	ActivityVa11HallA = 36,
	[InspectorName("国服充值返利活动")]
	[OriginalName("UIPanelType_ActivityRebate")]
	ActivityRebate = 37,
	[InspectorName("国服充值送月票活动")]
	[OriginalName("UIPanelType_ActivityMonthGift")]
	ActivityMonthGift = 38,
	[InspectorName("国服七日签到活动")]
	[OriginalName("UIPanelType_ActivitySevenDaySignIn")]
	ActivitySevenDaySignIn = 39,
	[InspectorName("地图通行证")]
	[OriginalName("UIPanelType_ActivityMapPass")]
	ActivityMapPass = 40,
	[InspectorName("等级通行证")]
	[OriginalName("UIPanelType_ActivityLevelPass")]
	ActivityLevelPass = 41,
	[InspectorName("新手任务")]
	[OriginalName("UIPanelType_ActivityRookieTask")]
	ActivityRookieTask = 42,
	[InspectorName("活动商店")]
	[OriginalName("UIPanelType_ActivityStore")]
	ActivityStore = 43,
	[InspectorName("送角色活动")]
	[OriginalName("UIPanelType_ActivityReceive")]
	ActivityReceive = 44,
	[InspectorName("资源周")]
	[OriginalName("UIPanelType_ActivityResourceWeek")]
	ActivityResourceWeek = 45,
	[InspectorName("签到模板1")]
	[OriginalName("UIPanelType_SignInModuleOne")]
	SignInModuleOne = 46,
	[InspectorName("领取皮肤优惠券")]
	[OriginalName("UIPanelType_ActivityFree")]
	ActivityFree = 47,
	[InspectorName("活动商店2")]
	[OriginalName("UIPanelType_ActivityStoreTwo")]
	ActivityStoreTwo = 48,
	[InspectorName("活动商店赛季")]
	[OriginalName("UIPanelType_ActivityStoreSeason")]
	ActivityStoreSeason = 49,
	[InspectorName("活动宣传页点灯")]
	[OriginalName("UIPanelType_ActivityBrochureLight")]
	ActivityBrochureLight = 50,
	[InspectorName("回归活动")]
	[OriginalName("UIPanelType_ActivityComeback")]
	ActivityComeback = 51,
	[InspectorName("宾果翻牌活动")]
	[OriginalName("UIPanelType_ActivityBingo")]
	ActivityBingo = 52,
	[InspectorName("魔裁联动活动")]
	[OriginalName("UIPanelType_ActivityMGWT")]
	ActivityMgwt = 53,
	[InspectorName("公会前置")]
	[OriginalName("UIPanelType_GuildDiscovery")]
	GuildDiscovery = 54,
	[InspectorName("公会")]
	[OriginalName("UIPanelType_Guild")]
	Guild = 55,
	[InspectorName("签到模板2")]
	[OriginalName("UIPanelType_SignInModuleTwo")]
	SignInModuleTwo = 56,
	[InspectorName("活动宣传页模板")]
	[OriginalName("UIPanelType_ActivityAdvert")]
	ActivityAdvert = 57,
	[InspectorName("底部菜单")]
	[OriginalName("UIPanelType_BottomMenu")]
	BottomMenu = 151,
	[InspectorName("背景")]
	[OriginalName("UIPanelType_Background")]
	Background = 152,
	[InspectorName("活动中心")]
	[OriginalName("UIPanelType_ActivityHub")]
	ActivityHub = 153,
	[InspectorName("战场信息")]
	[OriginalName("UIPanelType_BattleInfo")]
	BattleInfo = 201,
	[InspectorName("战斗玩家")]
	[OriginalName("UIPanelType_BattlePlayer")]
	BattlePlayer = 202,
	[InspectorName("手持卡牌")]
	[OriginalName("UIPanelType_HandCard")]
	HandCard = 203,
	[InspectorName("对战结算")]
	[OriginalName("UIPanelType_BattleSettlement")]
	BattleSettlement = 204,
	[InspectorName("音游活动")]
	[OriginalName("UIPanelType_ActivityRhythm")]
	ActivityRhythm = 205,
	[InspectorName("单人玩法开始")]
	[OriginalName("UIPanelType_SinglePlayerStart")]
	SinglePlayerStart = 206,
	[InspectorName("单人玩法")]
	[OriginalName("UIPanelType_SinglePlayer")]
	SinglePlayer = 207,
	[InspectorName("单人玩法对战结算")]
	[OriginalName("UIPanelType_SinglePlayerSettlement")]
	SinglePlayerSettlement = 208,
	[InspectorName("骰骰乐活动")]
	[OriginalName("UIPanelType_ActivityDice")]
	ActivityDice = 209
}
