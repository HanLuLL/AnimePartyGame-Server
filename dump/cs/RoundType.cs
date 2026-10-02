using Google.Protobuf.Reflection;
using UnityEngine;

public enum RoundType
{
	[InspectorName("无")]
	[OriginalName("RoundType_None")]
	None,
	[InspectorName("逐轮次")]
	[OriginalName("RoundType_PerRound")]
	PerRound,
	[InspectorName("发资源轮次")]
	[OriginalName("RoundType_DistributeResourcesRound")]
	DistributeResourcesRound,
	[InspectorName("彩票开奖轮次")]
	[OriginalName("RoundType_LotteryRound")]
	LotteryRound
}
