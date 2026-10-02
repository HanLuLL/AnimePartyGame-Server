using System;
using Google.Protobuf.Reflection;
using party.code;
using party.model;

namespace party.protocol;

public static class InteriorReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static InteriorReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("Chdwcm90b2NvbC9pbnRlcmlvci5wcm90bxIIcHJvdG9jb2waEW1vZGVsL21v" + "ZGVsLnByb3RvGhJlcnJjb2RlL2NvZGUucHJvdG8iCQoHT2ZmbGluZSIICgZP" + "bmxpbmUiDgoMRGVsYXlPZmZMaW5lIkQKDEVudGVyR2FtZVJlcRIPCgdyYW5k" + "S2V5GAEgASgJEiMKB2FjY291bnQYAiABKAsyEi5tb2RlbC5BY2NvdW50SW5m" + "byJtCg1FbnRlckdhbWVSZXNwEhgKBGNvZGUYASABKA4yCi5jb2RlLkNvZGUS" + "IwoHYWNjb3VudBgCIAEoCzISLm1vZGVsLkFjY291bnRJbmZvEh0KBnBsYXll" + "chgDIAEoCzINLm1vZGVsLlBsYXllciILCglOZXRTdG9wZWQiDAoKR2FtZUNs" + "b3NlZCIKCghSb29tUGluZyKFAQoQU3lzU3RhcnRNYXRjaFJlcRIOCgZ0ZWFt" + "SWQYASABKAMSHgoEbW9kZRgCIAEoDjIQLm1vZGVsLk1hdGNoTW9kZRINCgVt" + "YXBJZBgDIAEoBRISCgpkaWZmaWN1bHR5GAQgASgFEh4KB3BsYXllcnMYBSAD" + "KAsyDS5tb2RlbC5QbGF5ZXIiPQoRU3lzU3RhcnRNYXRjaFJlc3ASGAoEY29k" + "ZRgBIAEoDjIKLmNvZGUuQ29kZRIOCgZ0ZWFtSWQYAiABKAMiMgoRU3lzQ2Fu" + "Y2VsTWF0Y2hSZXESDgoGdGVhbUlkGAEgASgDEg0KBWNvdW50GAIgASgFIj4K" + "ElN5c0NhbmNlbE1hdGNoUmVzcBIYCgRjb2RlGAEgASgOMgouY29kZS5Db2Rl" + "Eg4KBnRlYW1JZBgCIAEoAyInChhTeXNHZXRSYW5kb21NYXRjaFRlYW1SZXES" + "CwoDbnVtGAEgASgFIkUKGVN5c0dldFJhbmRvbU1hdGNoVGVhbVJlc3ASKAoF" + "dGVhbXMYASADKAsyGS5wcm90b2NvbC5SYW5kb21NYXRjaFRlYW0iPQoPUmFu" + "ZG9tTWF0Y2hUZWFtEgoKAmlkGAEgASgDEh4KB3BsYXllcnMYAiADKAsyDS5t" + "b2RlbC5QbGF5ZXIilwEKElN5c1N5bmNHdWlsZE1lbWJlchIQCghwbGF5ZXJJ" + "ZBgBIAEoEBIWCg53ZWVrbHlBY3Rpdml0eRgCIAEoDxIVCg1sYXN0TG9naW5U" + "aW1lGAMgASgQEhQKDHdlZWtseVNpZ25JbhgEIAEoCBIWCg5sYXN0TG9nb3V0" + "VGltZRgFIAEoEBISCgpwbGF5ZXJOYW1lGAYgASgJIkMKHVN5c1Byb2Nlc3NH" + "dWlsZEFwcGxpY2F0aW9uQzJTEg8KB2d1aWxkSWQYASABKBASEQoJZ3VpbGRO" + "YW1lGAIgASgJIjMKHVN5c1Byb2Nlc3NHdWlsZEFwcGxpY2F0aW9uUzJDEhIK" + "CnBsYXllck5hbWUYASABKAkiPwoZU3lzU2VuZEd1aWxkSW52aXRhdGlvbkMy" + "UxIPCgdndWlsZElkGAEgASgQEhEKCWludml0ZXJJZBgCIAEoECIbChlTeXNT" + "ZW5kR3VpbGRJbnZpdGF0aW9uUzJDIm0KD1N5c0V4aXRHdWlsZEMyUxIPCgdn" + "dWlsZElkGAEgASgQEhEKCWd1aWxkTmFtZRgCIAEoCRIQCghwbGF5ZXJJZBgD" + "IAEoEBISCgpwbGF5ZXJOYW1lGAQgASgJEhAKCGV4aXRUeXBlGAUgASgPIhEK" + "D1N5c0V4aXRHdWlsZFMyQyKYAQoYU3lzVXBkYXRlR3VpbGRNaXNzaW9uQzJT" + "EkgKC21pc3Npb25Db25kGAEgAygLMjMucHJvdG9jb2wuU3lzVXBkYXRlR3Vp" + "bGRNaXNzaW9uQzJTLk1pc3Npb25Db25kRW50cnkaMgoQTWlzc2lvbkNvbmRF" + "bnRyeRILCgNrZXkYASABKA8SDQoFdmFsdWUYAiABKA86AjgBIjoKFlN5c1Nl" + "bmRHdWlsZENoYXRNc2dDMlMSIAoDbXNnGAEgASgLMhMubW9kZWwuR3VpbGRD" + "aGF0TXNnIhIKEEF1dG9EaXNiYW5kR3VpbGQiJgoSU3lzRGlzYmFuZEd1aWxk" + "QzJTEhAKCGV4aXRUeXBlGAEgASgPIhUKE1N5c0dtR3VpbGRTZWFyY2hDMlMi" + "MgoTU3lzR21HdWlsZFNlYXJjaFMyQxIbCgVndWlsZBgBIAEoCzIMLm1vZGVs" + "Lkd1aWxkIicKF1N5c0dtR3VpbGRDaGFuZ2VOYW1lQzJTEgwKBG5hbWUYASAB" + "KAkiFgoUU3lzR21HdWlsZERpc2JhbmRDMlMiJQoTU3lzR21HdWlsZEZyZWV6" + "ZUMyUxIOCgZmcm96ZW4YASABKAgiKAoRU3lzR21HdWlsZE11dGVDMlMSEwoL" + "bXV0ZUVuZFRpbWUYASABKBAiKAoWU3lzR21HdWlsZENyZWF0ZUJhbkMyUxIO" + "CgZiYW5uZWQYASABKAgiLgoXU3lzR21HdWlsZE11dGVQbGF5ZXJDMlMSEwoL" + "bXV0ZUVuZFRpbWUYASABKBBCJFoRcGFydHkvcGIvcHJvdG9jb2yqAg5wYXJ0" + "eS5wcm90b2NvbGIGcHJvdG8z"), new FileDescriptor[2]
		{
			ModelReflection.Descriptor,
			CodeReflection.Descriptor
		}, new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[34]
		{
			new GeneratedClrTypeInfo(typeof(Offline), Offline.Parser, null, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(Online), Online.Parser, null, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(DelayOffLine), DelayOffLine.Parser, null, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(EnterGameReq), EnterGameReq.Parser, new string[2] { "RandKey", "Account" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(EnterGameResp), EnterGameResp.Parser, new string[3] { "Code", "Account", "Player" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(NetStoped), NetStoped.Parser, null, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(GameClosed), GameClosed.Parser, null, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(RoomPing), RoomPing.Parser, null, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(SysStartMatchReq), SysStartMatchReq.Parser, new string[5] { "TeamId", "Mode", "MapId", "Difficulty", "Players" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(SysStartMatchResp), SysStartMatchResp.Parser, new string[2] { "Code", "TeamId" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(SysCancelMatchReq), SysCancelMatchReq.Parser, new string[2] { "TeamId", "Count" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(SysCancelMatchResp), SysCancelMatchResp.Parser, new string[2] { "Code", "TeamId" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(SysGetRandomMatchTeamReq), SysGetRandomMatchTeamReq.Parser, new string[1] { "Num" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(SysGetRandomMatchTeamResp), SysGetRandomMatchTeamResp.Parser, new string[1] { "Teams" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(RandomMatchTeam), RandomMatchTeam.Parser, new string[2] { "Id", "Players" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(SysSyncGuildMember), SysSyncGuildMember.Parser, new string[6] { "PlayerId", "WeeklyActivity", "LastLoginTime", "WeeklySignIn", "LastLogoutTime", "PlayerName" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(SysProcessGuildApplicationC2S), SysProcessGuildApplicationC2S.Parser, new string[2] { "GuildId", "GuildName" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(SysProcessGuildApplicationS2C), SysProcessGuildApplicationS2C.Parser, new string[1] { "PlayerName" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(SysSendGuildInvitationC2S), SysSendGuildInvitationC2S.Parser, new string[2] { "GuildId", "InviterId" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(SysSendGuildInvitationS2C), SysSendGuildInvitationS2C.Parser, null, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(SysExitGuildC2S), SysExitGuildC2S.Parser, new string[5] { "GuildId", "GuildName", "PlayerId", "PlayerName", "ExitType" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(SysExitGuildS2C), SysExitGuildS2C.Parser, null, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(SysUpdateGuildMissionC2S), SysUpdateGuildMissionC2S.Parser, new string[1] { "MissionCond" }, null, null, null, new GeneratedClrTypeInfo[1]),
			new GeneratedClrTypeInfo(typeof(SysSendGuildChatMsgC2S), SysSendGuildChatMsgC2S.Parser, new string[1] { "Msg" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(AutoDisbandGuild), AutoDisbandGuild.Parser, null, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(SysDisbandGuildC2S), SysDisbandGuildC2S.Parser, new string[1] { "ExitType" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(SysGmGuildSearchC2S), SysGmGuildSearchC2S.Parser, null, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(SysGmGuildSearchS2C), SysGmGuildSearchS2C.Parser, new string[1] { "Guild" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(SysGmGuildChangeNameC2S), SysGmGuildChangeNameC2S.Parser, new string[1] { "Name" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(SysGmGuildDisbandC2S), SysGmGuildDisbandC2S.Parser, null, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(SysGmGuildFreezeC2S), SysGmGuildFreezeC2S.Parser, new string[1] { "Frozen" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(SysGmGuildMuteC2S), SysGmGuildMuteC2S.Parser, new string[1] { "MuteEndTime" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(SysGmGuildCreateBanC2S), SysGmGuildCreateBanC2S.Parser, new string[1] { "Banned" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(SysGmGuildMutePlayerC2S), SysGmGuildMutePlayerC2S.Parser, new string[1] { "MuteEndTime" }, null, null, null, null)
		}));
	}
}
