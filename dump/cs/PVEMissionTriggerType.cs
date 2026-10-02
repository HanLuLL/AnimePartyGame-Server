using Google.Protobuf.Reflection;
using UnityEngine;

public enum PVEMissionTriggerType
{
	[InspectorName("无")]
	[OriginalName("PVEMissionTriggerType_None")]
	None,
	[InspectorName("游戏进度触发")]
	[OriginalName("PVEMissionTriggerType_GameProgress")]
	GameProgress,
	[InspectorName("投票结果")]
	[OriginalName("PVEMissionTriggerType_VoteResult")]
	VoteResult,
	[InspectorName("游戏轮次区间")]
	[OriginalName("PVEMissionTriggerType_GameRoundRange")]
	GameRoundRange
}
