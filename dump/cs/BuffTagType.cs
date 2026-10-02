using Google.Protobuf.Reflection;
using UnityEngine;

public enum BuffTagType
{
	[InspectorName("无")]
	[OriginalName("BuffTagType_None")]
	None,
	[InspectorName("中毒")]
	[OriginalName("BuffTagType_Poison")]
	Poison,
	[InspectorName("诅咒")]
	[OriginalName("BuffTagType_Curse")]
	Curse
}
