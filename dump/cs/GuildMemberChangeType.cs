using Google.Protobuf.Reflection;
using UnityEngine;

public enum GuildMemberChangeType
{
	[InspectorName("无")]
	[OriginalName("GuildMemberChangeType_None")]
	None,
	[InspectorName("加入")]
	[OriginalName("GuildMemberChangeType_Join")]
	Join,
	[InspectorName("退出")]
	[OriginalName("GuildMemberChangeType_Quit")]
	Quit,
	[InspectorName("踢出")]
	[OriginalName("GuildMemberChangeType_Kick")]
	Kick,
	[InspectorName("升职")]
	[OriginalName("GuildMemberChangeType_Promote")]
	Promote,
	[InspectorName("降职")]
	[OriginalName("GuildMemberChangeType_Demote")]
	Demote,
	[InspectorName("转让会长")]
	[OriginalName("GuildMemberChangeType_TransferGuildMaster")]
	TransferGuildMaster,
	[InspectorName("非弹劾流程成为会长")]
	[OriginalName("GuildMemberChangeType_BecameGuildMaster")]
	BecameGuildMaster,
	[InspectorName("弹劾流程成为会长")]
	[OriginalName("GuildMemberChangeType_BecameGuildMasterViaImpeachment")]
	BecameGuildMasterViaImpeachment,
	[InspectorName("弹劾流程成为新手")]
	[OriginalName("GuildMemberChangeType_DemotedToNewMemberViaImpeachment")]
	DemotedToNewMemberViaImpeachment
}
