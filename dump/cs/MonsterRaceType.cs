using Google.Protobuf.Reflection;
using UnityEngine;

public enum MonsterRaceType
{
	[InspectorName("无")]
	[OriginalName("MonsterRaceType_None")]
	None,
	[InspectorName("智械")]
	[OriginalName("MonsterRaceType_Techform")]
	Techform,
	[InspectorName("物件")]
	[OriginalName("MonsterRaceType_Object")]
	Object
}
