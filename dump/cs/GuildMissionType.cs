using Google.Protobuf.Reflection;
using UnityEngine;

public enum GuildMissionType
{
	[InspectorName("无")]
	[OriginalName("GuildMissionType_None")]
	None,
	[InspectorName("个人")]
	[OriginalName("GuildMissionType_Personal")]
	Personal,
	[InspectorName("公会")]
	[OriginalName("GuildMissionType_Guild")]
	Guild
}
