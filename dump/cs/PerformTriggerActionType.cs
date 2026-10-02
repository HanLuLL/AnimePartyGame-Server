using Google.Protobuf.Reflection;
using UnityEngine;

public enum PerformTriggerActionType
{
	[InspectorName("无")]
	[OriginalName("PerformTriggerActionType_None")]
	None,
	[InspectorName("裁判解说")]
	[OriginalName("PerformTriggerActionType_RefereeCall")]
	RefereeCall
}
