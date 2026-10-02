using Google.Protobuf.Reflection;
using UnityEngine;

public enum GachaType
{
	[InspectorName("无")]
	[OriginalName("GachaType_None")]
	None,
	[InspectorName("常驻")]
	[OriginalName("GachaType_Permanent")]
	Permanent,
	[InspectorName("扩充")]
	[OriginalName("GachaType_Expansion")]
	Expansion,
	[InspectorName("限时UP")]
	[OriginalName("GachaType_LimitedUp")]
	LimitedUp,
	[InspectorName("活动皮肤")]
	[OriginalName("GachaType_ActivitySkin")]
	ActivitySkin,
	[InspectorName("限时UP2")]
	[OriginalName("GachaType_LimitedUp2")]
	LimitedUp2,
	[InspectorName("往期皮肤")]
	[OriginalName("GachaType_PreviousSkin")]
	PreviousSkin,
	[InspectorName("往期皮肤2")]
	[OriginalName("GachaType_PreviousSkin2")]
	PreviousSkin2,
	[InspectorName("往期皮肤3")]
	[OriginalName("GachaType_PreviousSkin3")]
	PreviousSkin3,
	[InspectorName("新手卡池")]
	[OriginalName("GachaType_Rookie")]
	Rookie,
	[InspectorName("扩充2")]
	[OriginalName("GachaType_Expansion2")]
	Expansion2,
	[InspectorName("扩充3")]
	[OriginalName("GachaType_Expansion3")]
	Expansion3,
	[InspectorName("赛季皮肤")]
	[OriginalName("GachaType_SeasonSkin")]
	SeasonSkin
}
