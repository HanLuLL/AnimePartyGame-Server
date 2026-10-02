using Google.Protobuf.Reflection;
using UnityEngine;

public enum DamageType
{
	[InspectorName("无")]
	[OriginalName("DamageType_None")]
	None,
	[InspectorName("技能")]
	[OriginalName("DamageType_Skill")]
	Skill,
	[InspectorName("卡牌")]
	[OriginalName("DamageType_Card")]
	Card,
	[InspectorName("命运")]
	[OriginalName("DamageType_Destiny")]
	Destiny,
	[InspectorName("占卜")]
	[OriginalName("DamageType_Divination")]
	Divination,
	[InspectorName("事件")]
	[OriginalName("DamageType_Event")]
	Event,
	[InspectorName("路块")]
	[OriginalName("DamageType_LAND")]
	Land,
	[InspectorName("战斗")]
	[OriginalName("DamageType_BATTLE")]
	Battle,
	[InspectorName("Buff")]
	[OriginalName("DamageType_Buff")]
	Buff,
	[InspectorName("召唤")]
	[OriginalName("DamageType_Summon")]
	Summon,
	[InspectorName("遗物")]
	[OriginalName("DamageType_Relic")]
	Relic,
	[InspectorName("特殊地图事件（此类型造成的伤害不受伤害加成减免影响）")]
	[OriginalName("DamageType_MapEvent")]
	MapEvent
}
