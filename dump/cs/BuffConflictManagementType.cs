using Google.Protobuf.Reflection;
using UnityEngine;

public enum BuffConflictManagementType
{
	[InspectorName("无")]
	[OriginalName("BuffConflictManagementType_None")]
	None,
	[InspectorName("互斥")]
	[OriginalName("BuffConflictManagementType_Reject")]
	Reject,
	[InspectorName("替换")]
	[OriginalName("BuffConflictManagementType_Replace")]
	Replace
}
