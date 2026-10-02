using Google.Protobuf.Reflection;
using UnityEngine;

public enum PVEMissionFailedType
{
	[InspectorName("无")]
	[OriginalName("PVEMissionFailedType_None")]
	None,
	[InspectorName("游戏轮次结束")]
	[OriginalName("PVEMissionFailedType_GameRoundEnd")]
	GameRoundEnd
}
