using Google.Protobuf.Reflection;
using UnityEngine;

public enum VictoryType
{
	[InspectorName("无")]
	[OriginalName("VictoryType_None")]
	None,
	[InspectorName("升星")]
	[OriginalName("VictoryType_Star")]
	Star,
	[InspectorName("击倒怪物")]
	[OriginalName("VictoryType_KillMonster")]
	KillMonster
}
