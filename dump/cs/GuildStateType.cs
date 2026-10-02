using Google.Protobuf.Reflection;
using UnityEngine;

public enum GuildStateType
{
	[InspectorName("无")]
	[OriginalName("GuildStateType_None")]
	None,
	[InspectorName("正常")]
	[OriginalName("GuildStateType_Normal")]
	Normal,
	[InspectorName("被封禁中")]
	[OriginalName("GuildStateType_Banned")]
	Banned,
	[InspectorName("已解散")]
	[OriginalName("GuildStateType_Disbanded")]
	Disbanded
}
