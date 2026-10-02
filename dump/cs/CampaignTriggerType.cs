using Google.Protobuf.Reflection;
using UnityEngine;

public enum CampaignTriggerType
{
	[InspectorName("无")]
	[OriginalName("CampaignTriggerType_None")]
	None,
	[InspectorName("轮次")]
	[OriginalName("CampaignTriggerType_Round")]
	Round,
	[InspectorName("进度")]
	[OriginalName("CampaignTriggerType_GameProgress")]
	GameProgress,
	[InspectorName("首次进入战斗")]
	[OriginalName("CampaignTriggerType_FirstEnterPK")]
	FirstEnterPk,
	[InspectorName("首次击倒")]
	[OriginalName("CampaignTriggerType_FirstKill")]
	FirstKill,
	[InspectorName("首次被击倒")]
	[OriginalName("CampaignTriggerType_FirstKilled")]
	FirstKilled,
	[InspectorName("首次升星")]
	[OriginalName("CampaignTriggerType_FirstStarLevelUp")]
	FirstStarLevelUp,
	[InspectorName("地图事件")]
	[OriginalName("CampaignTriggerType_MapEvent")]
	MapEvent
}
