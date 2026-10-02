using Google.Protobuf.Reflection;
using UnityEngine;

public enum GuildTagType
{
	[InspectorName("无")]
	[OriginalName("GuildTagType_None")]
	None,
	[InspectorName("休闲")]
	[OriginalName("GuildTagType_Casual")]
	Casual,
	[InspectorName("社交")]
	[OriginalName("GuildTagType_Social")]
	Social,
	[InspectorName("PVE")]
	[OriginalName("GuildTagType_PVE")]
	Pve,
	[InspectorName("PVP")]
	[OriginalName("GuildTagType_PVP")]
	Pvp,
	[InspectorName("新手")]
	[OriginalName("GuildTagType_Novice")]
	Novice,
	[InspectorName("挑战")]
	[OriginalName("GuildTagType_Challenge")]
	Challenge
}
