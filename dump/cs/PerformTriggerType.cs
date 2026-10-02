using Google.Protobuf.Reflection;
using UnityEngine;

public enum PerformTriggerType
{
	[InspectorName("无")]
	[OriginalName("PerformTriggerType_None")]
	None,
	[InspectorName("受到伤害")]
	[OriginalName("PerformTriggerType_TakeDamage")]
	TakeDamage,
	[InspectorName("角色移动最终点数")]
	[OriginalName("PerformTriggerType_HeroMoveFinalPoints")]
	HeroMoveFinalPoints,
	[InspectorName("角色升星")]
	[OriginalName("PerformTriggerType_HeroStarUp")]
	HeroStarUp,
	[InspectorName("特定怪物刷新")]
	[OriginalName("PerformTriggerType_MonsterShow")]
	MonsterShow,
	[InspectorName("裁判加入战斗")]
	[OriginalName("PerformTriggerType_RefereeJoinBattle")]
	RefereeJoinBattle
}
