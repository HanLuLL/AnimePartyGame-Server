using Google.Protobuf.Reflection;
using UnityEngine;

public enum RelicQualityType
{
	[InspectorName("无")]
	[OriginalName("RelicQualityType_None")]
	None,
	[InspectorName("蓝")]
	[OriginalName("RelicQualityType_Blue")]
	Blue,
	[InspectorName("紫")]
	[OriginalName("RelicQualityType_Purple")]
	Purple,
	[InspectorName("橙")]
	[OriginalName("RelicQualityType_Orange")]
	Orange
}
