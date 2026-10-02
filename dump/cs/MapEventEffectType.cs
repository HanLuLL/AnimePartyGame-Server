using Google.Protobuf.Reflection;
using UnityEngine;

public enum MapEventEffectType
{
	[InspectorName("无")]
	[OriginalName("MapEventEffectType_None")]
	None,
	[InspectorName("刷怪随机地块")]
	[OriginalName("MapEventEffectType_CreateMonster")]
	CreateMonster,
	[InspectorName("刷一种怪随机地块")]
	[OriginalName("MapEventEffectType_CreateMonsterOneKind")]
	CreateMonsterOneKind,
	[InspectorName("怪物攻防变更")]
	[OriginalName("MapEventEffectType_MonsterAttrChange")]
	MonsterAttrChange,
	[InspectorName("获得卡牌")]
	[OriginalName("MapEventEffectType_GainCard")]
	GainCard,
	[InspectorName("触发地图单位组")]
	[OriginalName("MapEventEffectType_LandUnitsGroupIdTrigger")]
	LandUnitsGroupIdTrigger,
	[InspectorName("激活Boss")]
	[OriginalName("MapEventEffectType_ActiveBoss")]
	ActiveBoss
}
