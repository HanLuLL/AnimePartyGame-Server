using Google.Protobuf.Reflection;
using UnityEngine;

public enum VictoryConditionType
{
	[InspectorName("无")]
	[OriginalName("VictoryConditionType_None")]
	None,
	[InspectorName("击杀任一指定的目标单位")]
	[OriginalName("VictoryConditionType_KillAnyTarget")]
	KillAnyTarget,
	[InspectorName("击杀特殊条件下任一指定的目标单位")]
	[OriginalName("VictoryConditionType_KillAnyTargetSpecial")]
	KillAnyTargetSpecial
}
