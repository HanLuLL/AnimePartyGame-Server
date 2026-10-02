using Google.Protobuf.Reflection;
using UnityEngine;

public enum MissionStatus
{
	[InspectorName("None")]
	[OriginalName("MissionStatus_MissionStatusInit")]
	MissionStatusInit,
	[InspectorName("None")]
	[OriginalName("MissionStatus_MissionStatusAccept")]
	MissionStatusAccept,
	[InspectorName("None")]
	[OriginalName("MissionStatus_MissionStatusProgress")]
	MissionStatusProgress,
	[InspectorName("None")]
	[OriginalName("MissionStatus_MissionStatusFinish")]
	MissionStatusFinish,
	[InspectorName("None")]
	[OriginalName("MissionStatus_MissionRewarded")]
	MissionRewarded,
	[InspectorName("None")]
	[OriginalName("MissionStatus_MissionRemoved")]
	MissionRemoved
}
