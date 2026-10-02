using Google.Protobuf.Reflection;
using UnityEngine;

public enum GachaTableType
{
	[InspectorName("无")]
	[OriginalName("GachaTableType_None")]
	None,
	[InspectorName("角色池")]
	[OriginalName("GachaTableType_Role")]
	Role,
	[InspectorName("皮肤池")]
	[OriginalName("GachaTableType_Skin")]
	Skin,
	[InspectorName("活动池")]
	[OriginalName("GachaTableType_Activity")]
	Activity,
	[InspectorName("复刻池")]
	[OriginalName("GachaTableType_Replica")]
	Replica
}
