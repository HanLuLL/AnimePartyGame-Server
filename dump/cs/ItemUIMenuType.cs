using Google.Protobuf.Reflection;
using UnityEngine;

public enum ItemUIMenuType
{
	[InspectorName("无")]
	[OriginalName("ItemUIMenuType_None")]
	None,
	[InspectorName("道具")]
	[OriginalName("ItemUIMenuType_Prop")]
	Prop,
	[InspectorName("养成")]
	[OriginalName("ItemUIMenuType_Nurturance")]
	Nurturance,
	[InspectorName("时尚")]
	[OriginalName("ItemUIMenuType_Fashion")]
	Fashion
}
