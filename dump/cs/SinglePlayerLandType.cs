using Google.Protobuf.Reflection;
using UnityEngine;

public enum SinglePlayerLandType
{
	[InspectorName("无")]
	[OriginalName("SinglePlayerLandType_None")]
	None,
	[InspectorName("出生点")]
	[OriginalName("SinglePlayerLandType_Start")]
	Start,
	[InspectorName("怪物")]
	[OriginalName("SinglePlayerLandType_Monster")]
	Monster,
	[InspectorName("金币")]
	[OriginalName("SinglePlayerLandType_Gold")]
	Gold,
	[InspectorName("卡牌")]
	[OriginalName("SinglePlayerLandType_Card")]
	Card
}
