using Google.Protobuf.Reflection;
using UnityEngine;

public enum MissionType
{
	[InspectorName("无")]
	[OriginalName("MissionType_None")]
	None,
	[InspectorName("活动")]
	[OriginalName("MissionType_Activity")]
	Activity
}
