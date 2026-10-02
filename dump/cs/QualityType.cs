using Google.Protobuf.Reflection;
using UnityEngine;

public enum QualityType
{
	[InspectorName("无")]
	[OriginalName("QualityType_None")]
	None,
	[InspectorName("白")]
	[OriginalName("QualityType_White")]
	White,
	[InspectorName("绿")]
	[OriginalName("QualityType_Green")]
	Green,
	[InspectorName("蓝")]
	[OriginalName("QualityType_Blue")]
	Blue,
	[InspectorName("紫")]
	[OriginalName("QualityType_Purple")]
	Purple,
	[InspectorName("橙")]
	[OriginalName("QualityType_Orange")]
	Orange
}
