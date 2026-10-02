using Google.Protobuf.Reflection;
using UnityEngine;

public enum FashionType
{
	[InspectorName("无")]
	[OriginalName("FashionType_None")]
	None,
	[InspectorName("头像")]
	[OriginalName("FashionType_AccountHeadShot")]
	AccountHeadShot,
	[InspectorName("名片")]
	[OriginalName("FashionType_AccountBackground")]
	AccountBackground,
	[InspectorName("卡牌")]
	[OriginalName("FashionType_CardBack")]
	CardBack,
	[InspectorName("骰子")]
	[OriginalName("FashionType_Dice")]
	Dice,
	[InspectorName("击杀特效")]
	[OriginalName("FashionType_Effect")]
	Effect,
	[InspectorName("主页背景")]
	[OriginalName("FashionType_KV")]
	Kv
}
