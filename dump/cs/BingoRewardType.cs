using Google.Protobuf.Reflection;
using UnityEngine;

public enum BingoRewardType
{
	[InspectorName("无")]
	[OriginalName("BingoRewardType_None")]
	None,
	[InspectorName("格子")]
	[OriginalName("BingoRewardType_GridReward")]
	GridReward,
	[InspectorName("连线")]
	[OriginalName("BingoRewardType_LineReward")]
	LineReward,
	[InspectorName("进度")]
	[OriginalName("BingoRewardType_RoundReward")]
	RoundReward
}
