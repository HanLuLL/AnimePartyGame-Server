using Google.Protobuf.Reflection;
using UnityEngine;

public enum DivinationTargetType
{
	[InspectorName("无")]
	[OriginalName("DivinationTargetType_None")]
	None,
	[InspectorName("等级最高的玩家")]
	[OriginalName("DivinationTargetType_MaxLevel")]
	MaxLevel,
	[InspectorName("等级最低的玩家")]
	[OriginalName("DivinationTargetType_MinLevel")]
	MinLevel,
	[InspectorName("生命最高的玩家")]
	[OriginalName("DivinationTargetType_MaxBlood")]
	MaxBlood,
	[InspectorName("生命最低的玩家")]
	[OriginalName("DivinationTargetType_MinBlood")]
	MinBlood,
	[InspectorName("金币最多的玩家")]
	[OriginalName("DivinationTargetType_MaxGold")]
	MaxGold,
	[InspectorName("金币最少的玩家")]
	[OriginalName("DivinationTargetType_MinGold")]
	MinGold,
	[InspectorName("手牌最多的玩家")]
	[OriginalName("DivinationTargetType_MaxCard")]
	MaxCard,
	[InspectorName("手牌最少的玩家")]
	[OriginalName("DivinationTargetType_MinCard")]
	MinCard
}
