using Google.Protobuf.Reflection;
using UnityEngine;

public enum GoodsPurchaseType
{
	[InspectorName("无")]
	[OriginalName("GoodsPurchaseType_None")]
	None,
	[InspectorName("直购")]
	[OriginalName("GoodsPurchaseType_Recharge")]
	Recharge,
	[InspectorName("兑换")]
	[OriginalName("GoodsPurchaseType_Exchange")]
	Exchange,
	[InspectorName("每日星币")]
	[OriginalName("GoodsPurchaseType_DailyStarCoin")]
	DailyStarCoin
}
