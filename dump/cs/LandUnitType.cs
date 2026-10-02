using Google.Protobuf.Reflection;
using UnityEngine;

public enum LandUnitType
{
	[InspectorName("无")]
	[OriginalName("LandUnitType_None")]
	None,
	[InspectorName("怪物")]
	[OriginalName("LandUnitType_Monster")]
	Monster,
	[InspectorName("召唤物")]
	[OriginalName("LandUnitType_Summon")]
	Summon
}
