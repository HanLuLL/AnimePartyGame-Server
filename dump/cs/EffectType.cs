using Google.Protobuf.Reflection;
using UnityEngine;

public enum EffectType
{
	[InspectorName("无")]
	[OriginalName("EffectType_None")]
	None,
	[InspectorName("攻击")]
	[OriginalName("EffectType_Attack")]
	Attack,
	[InspectorName("防御")]
	[OriginalName("EffectType_Defense")]
	Defense,
	[InspectorName("指向")]
	[OriginalName("EffectType_Direct")]
	Direct,
	[InspectorName("回复")]
	[OriginalName("EffectType_Reply")]
	Reply,
	[InspectorName("速效")]
	[OriginalName("EffectType_Quick")]
	Quick,
	[InspectorName("陷阱")]
	[OriginalName("EffectType_Trap")]
	Trap,
	[InspectorName("普通")]
	[OriginalName("EffectType_Normal")]
	Normal,
	[InspectorName("抉择")]
	[OriginalName("EffectType_Choose")]
	Choose
}
