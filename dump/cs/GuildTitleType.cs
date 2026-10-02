using Google.Protobuf.Reflection;
using UnityEngine;

public enum GuildTitleType
{
	[InspectorName("无")]
	[OriginalName("GuildTitleType_None")]
	None,
	[InspectorName("会长")]
	[OriginalName("GuildTitleType_GuildMaster")]
	GuildMaster,
	[InspectorName("副会长")]
	[OriginalName("GuildTitleType_ViceGuildMaster")]
	ViceGuildMaster,
	[InspectorName("精英")]
	[OriginalName("GuildTitleType_EliteMember")]
	EliteMember,
	[InspectorName("新手")]
	[OriginalName("GuildTitleType_NewMember")]
	NewMember
}
