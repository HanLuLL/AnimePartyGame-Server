using Google.Protobuf.Reflection;
using UnityEngine;

public enum PVEMissionTargetType
{
	[InspectorName("无")]
	[OriginalName("PVEMissionTargetType_None")]
	None,
	[InspectorName("击杀怪物")]
	[OriginalName("PVEMissionTargetType_KillMonster")]
	KillMonster,
	[InspectorName("目标生命值区间")]
	[OriginalName("PVEMissionTargetType_TargetHPRange")]
	TargetHprange,
	[InspectorName("角色星级")]
	[OriginalName("PVEMissionTargetType_RoleStar")]
	RoleStar,
	[InspectorName("判断角色Buff_唯一")]
	[OriginalName("PVEMissionTargetType_HeroBuffUniqueCondition")]
	HeroBuffUniqueCondition,
	[InspectorName("累计完成地图任务")]
	[OriginalName("PVEMissionTargetType_AccPVEMission")]
	AccPvemission,
	[InspectorName("累计角色星级")]
	[OriginalName("PVEMissionTargetType_AccRoleStar")]
	AccRoleStar,
	[InspectorName("判断角色Buff_罪证")]
	[OriginalName("PVEMissionTargetType_HeroBuffCrimeCondition")]
	HeroBuffCrimeCondition
}
