using Google.Protobuf.Reflection;
using UnityEngine;

public enum MapEventType
{
	[InspectorName("无")]
	[OriginalName("MapEventType_None")]
	None,
	[InspectorName("通常")]
	[OriginalName("MapEventType_Normal")]
	Normal,
	[InspectorName("延迟")]
	[OriginalName("MapEventType_Delay")]
	Delay
}
