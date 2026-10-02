using Google.Protobuf.Reflection;
using UnityEngine;

public enum SkillType
{
	[InspectorName("无")]
	[OriginalName("SkillType_None")]
	None,
	[InspectorName("主动")]
	[OriginalName("SkillType_Active")]
	Active,
	[InspectorName("被动")]
	[OriginalName("SkillType_Passive")]
	Passive
}
