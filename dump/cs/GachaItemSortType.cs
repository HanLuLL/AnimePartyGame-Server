using Google.Protobuf.Reflection;
using UnityEngine;

public enum GachaItemSortType
{
	[InspectorName("无")]
	[OriginalName("GachaItemSortType_None")]
	None,
	[InspectorName("角色")]
	[OriginalName("GachaItemSortType_Role")]
	Role,
	[InspectorName("时尚")]
	[OriginalName("GachaItemSortType_Fashion")]
	Fashion,
	[InspectorName("礼物")]
	[OriginalName("GachaItemSortType_Gift")]
	Gift,
	[InspectorName("其他")]
	[OriginalName("GachaItemSortType_Other")]
	Other
}
