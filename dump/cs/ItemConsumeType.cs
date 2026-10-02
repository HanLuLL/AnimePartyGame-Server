using Google.Protobuf.Reflection;
using UnityEngine;

public enum ItemConsumeType
{
	[InspectorName("无")]
	[OriginalName("ItemConsumeType_None")]
	None = 0,
	[InspectorName("抽奖")]
	[OriginalName("ItemConsumeType_Gacha")]
	Gacha = 1,
	[InspectorName("GM")]
	[OriginalName("ItemConsumeType_GM")]
	Gm = 2,
	[InspectorName("使用宝箱")]
	[OriginalName("ItemConsumeType_UseTreasure")]
	UseTreasure = 3,
	[InspectorName("角色羁绊升级")]
	[OriginalName("ItemConsumeType_RoleCardUpLv")]
	RoleCardUpLv = 4,
	[InspectorName("角色缔结契约")]
	[OriginalName("ItemConsumeType_RoleCardBreakThrough")]
	RoleCardBreakThrough = 5,
	[InspectorName("道具过期兑换")]
	[OriginalName("ItemConsumeType_ExpireItem")]
	ExpireItem = 8,
	[InspectorName("刮刮乐")]
	[OriginalName("ItemConsumeType_ScratchCard")]
	ScratchCard = 14,
	[InspectorName("商店购买")]
	[OriginalName("ItemConsumeType_ShopBuy")]
	ShopBuy = 15,
	[InspectorName("通行证")]
	[OriginalName("ItemConsumeType_BattlePass")]
	BattlePass = 18
}
