using Google.Protobuf.Reflection;
using UnityEngine;

public enum ExitGuildType
{
	[InspectorName("无")]
	[OriginalName("ExitGuildType_None")]
	None,
	[InspectorName("被踢出公会")]
	[OriginalName("ExitGuildType_KickedFromTheGuild")]
	KickedFromTheGuild,
	[InspectorName("会长解散公会")]
	[OriginalName("ExitGuildType_GuildMasterDisbandsTheGuild")]
	GuildMasterDisbandsTheGuild,
	[InspectorName("长时间不活跃自动解散公会")]
	[OriginalName("ExitGuildType_GuildAutomaticallyDisbandedDueToProlongedInactivity")]
	GuildAutomaticallyDisbandedDueToProlongedInactivity,
	[InspectorName("GM强制解散公会")]
	[OriginalName("ExitGuildType_GuildForciblyDisbandedByGM")]
	GuildForciblyDisbandedByGm,
	[InspectorName("主动退出公会")]
	[OriginalName("ExitGuildType_ActivelyQuitTheGuild")]
	ActivelyQuitTheGuild
}
