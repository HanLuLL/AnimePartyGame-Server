using Google.Protobuf.Reflection;
using UnityEngine;

public enum MapModeType
{
	[InspectorName("无")]
	[OriginalName("MapModeType_None")]
	None,
	[InspectorName("标准")]
	[OriginalName("MapModeType_Standard")]
	Standard,
	[InspectorName("新手")]
	[OriginalName("MapModeType_Novice")]
	Novice,
	[InspectorName("无限火力")]
	[OriginalName("MapModeType_Ultra")]
	Ultra,
	[InspectorName("PVE")]
	[OriginalName("MapModeType_Pve")]
	Pve,
	[InspectorName("战役PVP")]
	[OriginalName("MapModeType_CampaignPVP")]
	CampaignPvp,
	[InspectorName("战役PVE")]
	[OriginalName("MapModeType_CampaignPVE")]
	CampaignPve,
	[InspectorName("非对称对抗")]
	[OriginalName("MapModeType_AsymmetricalBattle")]
	AsymmetricalBattle,
	[InspectorName("练习PVP")]
	[OriginalName("MapModeType_PracticePVP")]
	PracticePvp,
	[InspectorName("练习PVE")]
	[OriginalName("MapModeType_PracticePVE")]
	PracticePve,
	[InspectorName("PVE新手")]
	[OriginalName("MapModeType_PVENovice")]
	Pvenovice,
	[InspectorName("幸运星争夺战")]
	[OriginalName("MapModeType_LuckyStarBattle")]
	LuckyStarBattle,
	[InspectorName("词条PVE")]
	[OriginalName("MapModeType_MutatorPVE")]
	MutatorPve
}
