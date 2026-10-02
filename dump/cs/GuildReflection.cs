using System;
using Google.Protobuf.Reflection;

public static class GuildReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static GuildReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("CgtHdWlsZC5wcm90bxoKRW51bS5wcm90byK+AgoXR3VpbGRBdXRob3JpdHlD" + "b25maWd1cmUSJwoOZ3VpbGRUaXRsZVR5cGUYASABKA4yDy5HdWlsZFRpdGxl" + "VHlwZRIOCgZuYW1lSUQYAiABKA8SFwoPY2FuR3VpbGRTZXR0aW5nGAMgASgI" + "Eh8KF2NhbkludGVybmFsQW5ub3VuY2VtZW50GAQgASgIEh4KFmNhbkFwcHJv" + "dmVBcHBsaWNhdGlvbnMYBSABKAgSGAoQY2FuSW52aXRlVG9HdWlsZBgGIAEo" + "CBITCgtjYW5UcmFuc2ZlchgHIAEoCBIaChJjYW5Qcm9tb3RlT3JEZW1vdGUY" + "CCABKAgSEgoKY2FuSW1wZWFjaBgJIAEoCBIYChBjYW5LaWNrRnJvbUd1aWxk" + "GAogASgIEhcKD2NhbkRpc2JhbmRHdWlsZBgLIAEoCCKHAQoUR3VpbGRDcmVh" + "dGVDb25maWd1cmUSCgoCaWQYASABKA8SMwoHY29uc3VtZRgCIAMoCzIiLkd1" + "aWxkQ3JlYXRlQ29uZmlndXJlLkNvbnN1bWVFbnRyeRouCgxDb25zdW1lRW50" + "cnkSCwoDa2V5GAEgASgPEg0KBXZhbHVlGAIgASgPOgI4ASKlAwoVR3VpbGRN" + "aXNzaW9uQ29uZmlndXJlEgoKAmlkGAEgASgPEhMKC29yZGVyV2VpZ2h0GAIg" + "ASgPEikKD3Rhc2tSZWZyZXNoVHlwZRgDIAEoDjIQLlRhc2tSZWZyZXNoVHlw" + "ZRIrChBndWlsZE1pc3Npb25UeXBlGAQgASgOMhEuR3VpbGRNaXNzaW9uVHlw" + "ZRIzChRtaXNzaW9uQ29uZGl0aW9uVHlwZRgFIAEoDjIVLk1pc3Npb25Db25k" + "aXRpb25UeXBlEhUKDXBhcmFtUHJvZ3Jlc3MYBiABKA8SIwoIcGFyYW1LZXkY" + "ByADKA4yES5NaXNzaW9uUGFyYW1UeXBlEhIKCnBhcmFtVmFsdWUYCCADKA8S" + "DgoGbmFtZUlEGAkgASgPEg4KBmRlc2NJZBgKIAEoDxIyCgZyZXdhcmQYCyAD" + "KAsyIi5HdWlsZE1pc3Npb25Db25maWd1cmUuUmV3YXJkRW50cnkSCwoDd2F5" + "GAwgASgPGi0KC1Jld2FyZEVudHJ5EgsKA2tleRgBIAEoDxINCgV2YWx1ZRgC" + "IAEoDzoCOAEikQEKJkd1aWxkTWVtYmVyQ2hhbmdlTm90aWZpY2F0aW9uQ29u" + "ZmlndXJlEjUKFWd1aWxkTWVtYmVyQ2hhbmdlVHlwZRgBIAEoDjIWLkd1aWxk" + "TWVtYmVyQ2hhbmdlVHlwZRIWCg5ub3RpZmljYWl0b25JRBgCIAEoDxIYChBu" + "b3RpZmljYWl0b25Gb3JtGAMgAygPIkgKEUd1aWxkVGFnQ29uZmlndXJlEiMK" + "DGd1aWxkVGFnVHlwZRgBIAEoDjINLkd1aWxkVGFnVHlwZRIOCgZuYW1lSUQY" + "AiABKA8ixQcKDkd1aWxkQ29uZmlndXJlEiwKCkF1dGhvcml0eXMYASADKAsy" + "GC5HdWlsZEF1dGhvcml0eUNvbmZpZ3VyZRI5Cg1BdXRob3JpdHlEaWN0GAIg" + "AygLMiIuR3VpbGRDb25maWd1cmUuQXV0aG9yaXR5RGljdEVudHJ5EiYKB0Ny" + "ZWF0ZXMYAyADKAsyFS5HdWlsZENyZWF0ZUNvbmZpZ3VyZRIzCgpDcmVhdGVE" + "aWN0GAQgAygLMh8uR3VpbGRDb25maWd1cmUuQ3JlYXRlRGljdEVudHJ5EigK" + "CE1pc3Npb25zGAUgAygLMhYuR3VpbGRNaXNzaW9uQ29uZmlndXJlEjUKC01p" + "c3Npb25EaWN0GAYgAygLMiAuR3VpbGRDb25maWd1cmUuTWlzc2lvbkRpY3RF" + "bnRyeRJKChlNZW1iZXJDaGFuZ2VOb3RpZmljYXRpb25zGAcgAygLMicuR3Vp" + "bGRNZW1iZXJDaGFuZ2VOb3RpZmljYXRpb25Db25maWd1cmUSVwocTWVtYmVy" + "Q2hhbmdlTm90aWZpY2F0aW9uRGljdBgIIAMoCzIxLkd1aWxkQ29uZmlndXJl" + "Lk1lbWJlckNoYW5nZU5vdGlmaWNhdGlvbkRpY3RFbnRyeRIgCgRUYWdzGAkg" + "AygLMhIuR3VpbGRUYWdDb25maWd1cmUSLQoHVGFnRGljdBgKIAMoCzIcLkd1" + "aWxkQ29uZmlndXJlLlRhZ0RpY3RFbnRyeRpOChJBdXRob3JpdHlEaWN0RW50" + "cnkSCwoDa2V5GAEgASgPEicKBXZhbHVlGAIgASgLMhguR3VpbGRBdXRob3Jp" + "dHlDb25maWd1cmU6AjgBGkgKD0NyZWF0ZURpY3RFbnRyeRILCgNrZXkYASAB" + "KA8SJAoFdmFsdWUYAiABKAsyFS5HdWlsZENyZWF0ZUNvbmZpZ3VyZToCOAEa" + "SgoQTWlzc2lvbkRpY3RFbnRyeRILCgNrZXkYASABKA8SJQoFdmFsdWUYAiAB" + "KAsyFi5HdWlsZE1pc3Npb25Db25maWd1cmU6AjgBGmwKIU1lbWJlckNoYW5n" + "ZU5vdGlmaWNhdGlvbkRpY3RFbnRyeRILCgNrZXkYASABKA8SNgoFdmFsdWUY" + "AiABKAsyJy5HdWlsZE1lbWJlckNoYW5nZU5vdGlmaWNhdGlvbkNvbmZpZ3Vy" + "ZToCOAEaQgoMVGFnRGljdEVudHJ5EgsKA2tleRgBIAEoDxIhCgV2YWx1ZRgC" + "IAEoCzISLkd1aWxkVGFnQ29uZmlndXJlOgI4AWIGcHJvdG8z"), new FileDescriptor[1] { EnumReflection.Descriptor }, new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[6]
		{
			new GeneratedClrTypeInfo(typeof(GuildAuthorityConfigure), GuildAuthorityConfigure.Parser, new string[11]
			{
				"GuildTitleType", "NameID", "CanGuildSetting", "CanInternalAnnouncement", "CanApproveApplications", "CanInviteToGuild", "CanTransfer", "CanPromoteOrDemote", "CanImpeach", "CanKickFromGuild",
				"CanDisbandGuild"
			}, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(GuildCreateConfigure), GuildCreateConfigure.Parser, new string[2] { "Id", "Consume" }, null, null, null, new GeneratedClrTypeInfo[1]),
			new GeneratedClrTypeInfo(typeof(GuildMissionConfigure), GuildMissionConfigure.Parser, new string[12]
			{
				"Id", "OrderWeight", "TaskRefreshType", "GuildMissionType", "MissionConditionType", "ParamProgress", "ParamKey", "ParamValue", "NameID", "DescId",
				"Reward", "Way"
			}, null, null, null, new GeneratedClrTypeInfo[1]),
			new GeneratedClrTypeInfo(typeof(GuildMemberChangeNotificationConfigure), GuildMemberChangeNotificationConfigure.Parser, new string[3] { "GuildMemberChangeType", "NotificaitonID", "NotificaitonForm" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(GuildTagConfigure), GuildTagConfigure.Parser, new string[2] { "GuildTagType", "NameID" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(GuildConfigure), GuildConfigure.Parser, new string[10] { "Authoritys", "AuthorityDict", "Creates", "CreateDict", "Missions", "MissionDict", "MemberChangeNotifications", "MemberChangeNotificationDict", "Tags", "TagDict" }, null, null, null, new GeneratedClrTypeInfo[5])
		}));
	}
}
