using Google.Protobuf.Reflection;
using UnityEngine;

public enum MapEventTriggerType
{
	[InspectorName("无")]
	[OriginalName("MapEventTriggerType_None")]
	None,
	[InspectorName("轮次触发")]
	[OriginalName("MapEventTriggerType_Round")]
	Round,
	[InspectorName("行动结束")]
	[OriginalName("MapEventTriggerType_OperationEnd")]
	OperationEnd,
	[InspectorName("游戏进度触发")]
	[OriginalName("MapEventTriggerType_GameProgress")]
	GameProgress,
	[InspectorName("任务完成")]
	[OriginalName("MapEventTriggerType_MissionComplete")]
	MissionComplete,
	[InspectorName("Boss血量检测")]
	[OriginalName("MapEventTriggerType_BossHpCheck")]
	BossHpCheck,
	[InspectorName("投票结果")]
	[OriginalName("MapEventTriggerType_VoteResult")]
	VoteResult,
	[InspectorName("地图事件触发后")]
	[OriginalName("MapEventTriggerType_AfterMapEvent")]
	AfterMapEvent
}
