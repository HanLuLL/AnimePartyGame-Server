using Google.Protobuf.Reflection;
using UnityEngine;

public enum CharacterType
{
	[InspectorName("无")]
	[OriginalName("CharacterType_None")]
	None,
	[InspectorName("主角")]
	[OriginalName("CharacterType_Hero")]
	Hero,
	[InspectorName("怪物")]
	[OriginalName("CharacterType_Monster")]
	Monster
}
