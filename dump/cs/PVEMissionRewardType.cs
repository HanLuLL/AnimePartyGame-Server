using Google.Protobuf.Reflection;
using UnityEngine;

public enum PVEMissionRewardType
{
	[InspectorName("无")]
	[OriginalName("PVEMissionRewardType_None")]
	None,
	[InspectorName("获得筹码")]
	[OriginalName("PVEMissionRewardType_GainRelic")]
	GainRelic,
	[InspectorName("获得指定筹码")]
	[OriginalName("PVEMissionRewardType_GainRelicTarget")]
	GainRelicTarget,
	[InspectorName("进度变更")]
	[OriginalName("PVEMissionRewardType_ProgressChange")]
	ProgressChange,
	[InspectorName("获得星币")]
	[OriginalName("PVEMissionRewardType_GainGold")]
	GainGold,
	[InspectorName("触发地图事件")]
	[OriginalName("PVEMissionRewardType_MapEventTrigger")]
	MapEventTrigger,
	[InspectorName("获得卡牌")]
	[OriginalName("PVEMissionRewardType_GainCard")]
	GainCard,
	[InspectorName("变更Buff层数_唯一")]
	[OriginalName("PVEMissionRewardType_ChangeHeroBuffUnique")]
	ChangeHeroBuffUnique
}
