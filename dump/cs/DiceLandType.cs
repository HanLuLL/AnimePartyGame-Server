using Google.Protobuf.Reflection;
using UnityEngine;

public enum DiceLandType
{
	[InspectorName("无")]
	[OriginalName("DiceLandType_None")]
	None,
	[InspectorName("起点格")]
	[OriginalName("DiceLandType_StartLand")]
	StartLand,
	[InspectorName("疾走格")]
	[OriginalName("DiceLandType_DashLand")]
	DashLand,
	[InspectorName("奖励格")]
	[OriginalName("DiceLandType_RewardLand")]
	RewardLand
}
