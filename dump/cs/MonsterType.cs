using Google.Protobuf.Reflection;
using UnityEngine;

public enum MonsterType
{
	[InspectorName("无")]
	[OriginalName("MonsterType_None")]
	None,
	[InspectorName("普通")]
	[OriginalName("MonsterType_Normal")]
	Normal,
	[InspectorName("首领")]
	[OriginalName("MonsterType_Boss")]
	Boss,
	[InspectorName("精英")]
	[OriginalName("MonsterType_Elite")]
	Elite
}
