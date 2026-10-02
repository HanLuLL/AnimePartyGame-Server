using Google.Protobuf.Reflection;
using UnityEngine;

public enum BattlePassUIType
{
	[InspectorName("无")]
	[OriginalName("BattlePassUIType_None")]
	None,
	[InspectorName("主题")]
	[OriginalName("BattlePassUIType_Topic")]
	Topic,
	[InspectorName("皮肤")]
	[OriginalName("BattlePassUIType_Skin")]
	Skin,
	[InspectorName("名片")]
	[OriginalName("BattlePassUIType_AccountBackground")]
	AccountBackground,
	[InspectorName("通用")]
	[OriginalName("BattlePassUIType_Common")]
	Common,
	[InspectorName("中")]
	[OriginalName("BattlePassUIType_Medium")]
	Medium,
	[InspectorName("大")]
	[OriginalName("BattlePassUIType_Large")]
	Large
}
