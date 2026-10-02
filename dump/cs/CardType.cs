using Google.Protobuf.Reflection;
using UnityEngine;

public enum CardType
{
	[InspectorName("无")]
	[OriginalName("CardType_None")]
	None,
	[InspectorName("攻")]
	[OriginalName("CardType_Attack")]
	Attack,
	[InspectorName("守")]
	[OriginalName("CardType_Defend")]
	Defend,
	[InspectorName("效")]
	[OriginalName("CardType_Effect")]
	Effect,
	[InspectorName("反")]
	[OriginalName("CardType_Counter")]
	Counter,
	[InspectorName("聞")]
	[OriginalName("CardType_Event")]
	Event,
	[InspectorName("吉")]
	[OriginalName("CardType_Luck")]
	Luck,
	[InspectorName("凶")]
	[OriginalName("CardType_Jinx")]
	Jinx,
	[InspectorName("咒")]
	[OriginalName("CardType_Curse")]
	Curse
}
