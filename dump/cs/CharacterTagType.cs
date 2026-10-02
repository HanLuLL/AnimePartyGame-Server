using Google.Protobuf.Reflection;
using UnityEngine;

public enum CharacterTagType
{
	[InspectorName("无")]
	[OriginalName("CharacterTagType_None")]
	None,
	[InspectorName("通用")]
	[OriginalName("CharacterTagType_Generic")]
	Generic,
	[InspectorName("攻击")]
	[OriginalName("CharacterTagType_Attack")]
	Attack,
	[InspectorName("卡牌")]
	[OriginalName("CharacterTagType_Card")]
	Card,
	[InspectorName("辅助")]
	[OriginalName("CharacterTagType_Support")]
	Support,
	[InspectorName("生存")]
	[OriginalName("CharacterTagType_Tank")]
	Tank
}
