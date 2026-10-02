using Google.Protobuf.Reflection;
using UnityEngine;

public enum WayType
{
	[InspectorName("无")]
	[OriginalName("WayType_None")]
	None = 0,
	[InspectorName("默认")]
	[OriginalName("WayType_Default")]
	Default = 10,
	[InspectorName("众筹")]
	[OriginalName("WayType_Crowdfunding")]
	Crowdfunding = 20,
	[InspectorName("游戏结算")]
	[OriginalName("WayType_BattleResult")]
	BattleResult = 30,
	[InspectorName("任务")]
	[OriginalName("WayType_Task")]
	Task = 40,
	[InspectorName("成就")]
	[OriginalName("WayType_Achieve")]
	Achieve = 50,
	[InspectorName("抽卡")]
	[OriginalName("WayType_Gacha")]
	Gacha = 60,
	[InspectorName("角色分解")]
	[OriginalName("WayType_DecomposeRole")]
	DecomposeRole = 70,
	[InspectorName("装扮分解")]
	[OriginalName("WayType_DecomposeFashion")]
	DecomposeFashion = 80,
	[InspectorName("羁绊等级")]
	[OriginalName("WayType_FriendshipLevel")]
	FriendshipLevel = 90,
	[InspectorName("缔结契约")]
	[OriginalName("WayType_BondForged")]
	BondForged = 100,
	[InspectorName("商店")]
	[OriginalName("WayType_Mall")]
	Mall = 110,
	[InspectorName("礼包商店")]
	[OriginalName("WayType_GiftPackageStore")]
	GiftPackageStore = 111,
	[InspectorName("星盘商店")]
	[OriginalName("WayType_StarDiscStore")]
	StarDiscStore = 112,
	[InspectorName("活动")]
	[OriginalName("WayType_Activity")]
	Activity = 120,
	[InspectorName("活动弹窗")]
	[OriginalName("WayType_ActivityPopup")]
	ActivityPopup = 121,
	[InspectorName("联动购买")]
	[OriginalName("WayType_Collaboration")]
	Collaboration = 122,
	[InspectorName("签到")]
	[OriginalName("WayType_SignIn")]
	SignIn = 123,
	[InspectorName("进行游戏")]
	[OriginalName("WayType_PlayParty")]
	PlayParty = 130,
	[InspectorName("匹配或自定义")]
	[OriginalName("WayType_PlayPartyWetherMatch")]
	PlayPartyWetherMatch = 131,
	[InspectorName("单人战役")]
	[OriginalName("WayType_Campaign")]
	Campaign = 132,
	[InspectorName("通行证")]
	[OriginalName("WayType_BattlePass")]
	BattlePass = 140,
	[InspectorName("皮肤捆绑售卖")]
	[OriginalName("WayType_SkinBundlingSales")]
	SkinBundlingSales = 150,
	[InspectorName("赛博酒保联动")]
	[OriginalName("WayType_ActivityVA11HallA")]
	ActivityVa11HallA = 160,
	[InspectorName("音游活动")]
	[OriginalName("WayType_ActivityRhythm")]
	ActivityRhythm = 170,
	[InspectorName("单人玩法活动")]
	[OriginalName("WayType_ActivitySinglePLayer")]
	ActivitySinglePlayer = 180,
	[InspectorName("活动商店")]
	[OriginalName("WayType_ActivityStore")]
	ActivityStore = 190,
	[InspectorName("新手任务")]
	[OriginalName("WayType_ActivityRookieTask")]
	ActivityRookieTask = 200,
	[InspectorName("角色档案")]
	[OriginalName("WayType_CharacterProfile")]
	CharacterProfile = 210,
	[InspectorName("等级通行证")]
	[OriginalName("WayType_ActivityLevelPass")]
	ActivityLevelPass = 220,
	[InspectorName("周年庆礼包")]
	[OriginalName("WayType_Anniversary_2Nd")]
	Anniversary2Nd = 230,
	[InspectorName("活动商店2")]
	[OriginalName("WayType_ActivityStore2")]
	ActivityStore2 = 240,
	[InspectorName("骰骰乐活动")]
	[OriginalName("WayType_ActivityDice")]
	ActivityDice = 250,
	[InspectorName("主播女孩联动活动")]
	[OriginalName("WayType_ActivityNGO")]
	ActivityNgo = 260,
	[InspectorName("活动商店赛季")]
	[OriginalName("WayType_ActivityStoreSeason")]
	ActivityStoreSeason = 270,
	[InspectorName("宾果翻牌活动")]
	[OriginalName("WayType_ActivityBingo")]
	ActivityBingo = 280,
	[InspectorName("魔裁联动活动")]
	[OriginalName("WayType_ActivityMGWT")]
	ActivityMgwt = 290
}
