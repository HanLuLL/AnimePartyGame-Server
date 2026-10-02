using Google.Protobuf.Reflection;
using UnityEngine;

public enum MerchandiselType
{
	[InspectorName("无")]
	[OriginalName("MerchandiselType_None")]
	None,
	[InspectorName("礼包")]
	[OriginalName("MerchandiselType_GiftPackage")]
	GiftPackage,
	[InspectorName("星盘")]
	[OriginalName("MerchandiselType_Currency")]
	Currency,
	[InspectorName("7日礼包")]
	[OriginalName("MerchandiselType_Day7GiftPackage")]
	Day7GiftPackage,
	[InspectorName("月卡")]
	[OriginalName("MerchandiselType_MonthlyCard")]
	MonthlyCard,
	[InspectorName("通行证")]
	[OriginalName("MerchandiselType_BattlePass")]
	BattlePass,
	[InspectorName("联动")]
	[OriginalName("MerchandiselType_Collaboration")]
	Collaboration,
	[InspectorName("皮肤组合包")]
	[OriginalName("MerchandiselType_SkinSell")]
	SkinSell,
	[InspectorName("地图通行证")]
	[OriginalName("MerchandiselType_BattlePassMap")]
	BattlePassMap,
	[InspectorName("等级通行证")]
	[OriginalName("MerchandiselType_BattlePassLevel")]
	BattlePassLevel,
	[InspectorName("回归签到")]
	[OriginalName("MerchandiselType_ComebackSignIn")]
	ComebackSignIn
}
