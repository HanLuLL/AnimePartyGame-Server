using Google.Protobuf.Reflection;
using UnityEngine;

public enum OnlineStatusType
{
	[InspectorName("在线")]
	[OriginalName("OnlineStatusType_Online")]
	Online,
	[InspectorName("隐身")]
	[OriginalName("OnlineStatusType_Invisible")]
	Invisible
}
