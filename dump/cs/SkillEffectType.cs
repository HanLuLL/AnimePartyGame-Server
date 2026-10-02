using Google.Protobuf.Reflection;
using UnityEngine;

public enum SkillEffectType
{
	[InspectorName("无")]
	[OriginalName("SkillEffectType_None")]
	None,
	[InspectorName("客户端主动")]
	[OriginalName("SkillEffectType_Client")]
	Client,
	[InspectorName("服务器主动")]
	[OriginalName("SkillEffectType_Active")]
	Active,
	[InspectorName("服务器被动")]
	[OriginalName("SkillEffectType_Passive")]
	Passive
}
