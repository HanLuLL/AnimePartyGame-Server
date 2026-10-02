using Google.Protobuf.Reflection;
using UnityEngine;

public enum ParamType
{
	[InspectorName("金币")]
	[OriginalName("ParamType_Gold")]
	Gold = 0,
	[InspectorName("血量")]
	[OriginalName("ParamType_HP")]
	Hp = 1,
	[InspectorName("攻击")]
	[OriginalName("ParamType_ATK")]
	Atk = 2,
	[InspectorName("防御")]
	[OriginalName("ParamType_DEF")]
	Def = 3,
	[InspectorName("卡牌数量")]
	[OriginalName("ParamType_CardNum")]
	CardNum = 37,
	[InspectorName("金币倍率")]
	[OriginalName("ParamType_GoldRate")]
	GoldRate = 38,
	[InspectorName("伤害")]
	[OriginalName("ParamType_Harm")]
	Harm = 39
}
