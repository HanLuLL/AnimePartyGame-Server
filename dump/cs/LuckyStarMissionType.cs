using Google.Protobuf.Reflection;
using UnityEngine;

public enum LuckyStarMissionType
{
	[InspectorName("无")]
	[OriginalName("LuckyStarMissionType_None")]
	None,
	[InspectorName("吉")]
	[OriginalName("LuckyStarMissionType_Luck")]
	Luck,
	[InspectorName("凶")]
	[OriginalName("LuckyStarMissionType_Jinx")]
	Jinx
}
