using Google.Protobuf.Reflection;
using UnityEngine;

public enum GachaTagType
{
	[InspectorName("无")]
	[OriginalName("GachaTagType_None")]
	None,
	[InspectorName("Default")]
	[OriginalName("GachaTagType_Default")]
	Default,
	[InspectorName("Guarantee")]
	[OriginalName("GachaTagType_Guarantee")]
	Guarantee,
	[InspectorName("Role")]
	[OriginalName("GachaTagType_Role")]
	Role,
	[InspectorName("Up")]
	[OriginalName("GachaTagType_Up")]
	Up
}
