using System;
using Google.Protobuf.Reflection;
using Google.Protobuf.WellKnownTypes;

public static class GameModeReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static GameModeReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("Cg5HYW1lTW9kZS5wcm90bxofZ29vZ2xlL3Byb3RvYnVmL3RpbWVzdGFtcC5w" + "cm90bxoKRW51bS5wcm90byLMBAoVR2FtZU1vZGVJbmZvQ29uZmlndXJlEiEK" + "C21hcE1vZGVUeXBlGAEgASgOMgwuTWFwTW9kZVR5cGUSDgoGbmFtZUlEGAIg" + "ASgPEg4KBmlzU2hvdxgDIAEoCBISCgphY2NvdW50RXhwGAQgASgPEhEKCWdv" + "bGRGaXJzdBgFIAEoDxIRCglnb2xkT3RoZXIYBiABKA8SDQoFbWFwSUQYByAD" + "KA8SDwoHdXBncmFkZRgIIAMoDxIUCgx2aWN0b3J5UGFyYW0YCSADKA8SFwoP" + "Y2FyZEluSGFuZExpbWl0GAogASgPEhIKCmJhdHRsZWNvc3QYCyABKA8SGwoT" + "ZGlzdHJpYnV0ZVJlc291cmNlcxgMIAMoERIUCgxib3VudHlQYXJhbXMYDSAD" + "KBESFwoPQ2hyaXN0bWFzUGFyYW1zGA4gAygREhIKCmNhcmROdW1GaXgYDyAB" + "KA8SEwoLY29vbERvd25GaXgYECABKA8SDQoFYnVmZnMYESADKBESFQoNYmFu" + "Q2hhcmFjdGVycxgSIAMoERITCgtvcmRlcldlaWdodBgTIAEoDxIPCgdydWxl" + "c0lEGBQgASgPEg8KB2lzTWF0Y2gYFSABKAgSEgoKbWF0Y2hEZXNJRBgWIAEo" + "DxIRCgltb2RlRGVzSUQYFyABKA8SLQoJYmVnaW5UaW1lGBggASgLMhouZ29v" + "Z2xlLnByb3RvYnVmLlRpbWVzdGFtcBIrCgdlbmRUaW1lGBkgASgLMhouZ29v" + "Z2xlLnByb3RvYnVmLlRpbWVzdGFtcCKYAQoaR2FtZU1vZGVOUENQbGF5ZXJD" + "b25maWd1cmUSIQoLbWFwTW9kZVR5cGUYASABKA4yDC5NYXBNb2RlVHlwZRIR" + "Cgltb25zdGVySWQYAiABKA8SFAoMcm9vbVBvc2l0aW9uGAMgASgPEhMKC3Bs" + "YXllclBob3RvGAQgASgJEhkKEWFjY291bnRCYWNrZ3JvdW5kGAUgASgJIpgB" + "Ch9HYW1lTW9kZURpZmZpY3VsdHlEYXRhQ29uZmlndXJlEiEKC21hcE1vZGVU" + "eXBlGAEgASgOMgwuTWFwTW9kZVR5cGUSUgokZ2FtZU1vZGVEaWZmaWN1bHR5" + "RGF0YUNvbmZpZ3VyZUl0ZW1zGAIgAygLMiQuR2FtZU1vZGVEaWZmaWN1bHR5" + "RGF0YUNvbmZpZ3VyZUl0ZW0ifgojR2FtZU1vZGVEaWZmaWN1bHR5RGF0YUNv" + "bmZpZ3VyZUl0ZW0SDQoFaW5kZXgYASABKA8SGwoTZGlzdHJpYnV0ZVJlc291" + "cmNlcxgCIAMoERIRCglVcGdyYWRlSWQYAyABKA8SGAoQRGlmZmljdWx0eURl" + "c2NJZBgEIAEoDyLgAQojR2FtZU1vZGVBc3ltbWV0cmljYWxCYXR0bGVDb25m" + "aWd1cmUSCgoCaWQYASABKA8SJAocYXN5bW1ldHJpY2FsRGVmZW5kZXJXaW5S" + "b3VuZBgCIAEoDxIkChxhc3ltbWV0cmljYWxBdHRhY2tlcldpblNjb3JlGAMg" + "ASgPEicKH2FzeW1tZXRyaWNhbEF0dGFja2VyUmV2aXZlUm91bmQYBCABKA8S" + "HgoWYXN5bW1ldHJpY2FsU3BlZWRSb3VuZBgFIAEoDxIYChBTcGVlZFJvdW5k" + "QXR0clVwGAYgAygPIkIKG0dhbWVNb2RlTXV0YXRvclBWRUNvbmZpZ3VyZRIK" + "CgJpZBgBIAEoDxIXCg9lbGlnaWJsZUhlcm9JZHMYAiADKA8iggEKG0dhbWVN" + "b2RlR2FsbGVyeU1hcENvbmZpZ3VyZRInCg5HYWxsZXJ5TWFwVHlwZRgBIAEo" + "DjIPLkdhbGxlcnlNYXBUeXBlEg0KBW1hcElEGAIgAygPEhUKDUdhbGxlcnlu" + "YW1lSUQYAyABKA8SFAoMR2FsbGVyeURlc0lEGAQgASgPIuEJChFHYW1lTW9k" + "ZUNvbmZpZ3VyZRIlCgVJbmZvcxgBIAMoCzIWLkdhbWVNb2RlSW5mb0NvbmZp" + "Z3VyZRIyCghJbmZvRGljdBgCIAMoCzIgLkdhbWVNb2RlQ29uZmlndXJlLklu" + "Zm9EaWN0RW50cnkSLwoKTlBDUGxheWVycxgDIAMoCzIbLkdhbWVNb2RlTlBD" + "UGxheWVyQ29uZmlndXJlEjwKDU5QQ1BsYXllckRpY3QYBCADKAsyJS5HYW1l" + "TW9kZUNvbmZpZ3VyZS5OUENQbGF5ZXJEaWN0RW50cnkSOQoPRGlmZmljdWx0" + "eURhdGFzGAUgAygLMiAuR2FtZU1vZGVEaWZmaWN1bHR5RGF0YUNvbmZpZ3Vy" + "ZRJGChJEaWZmaWN1bHR5RGF0YURpY3QYBiADKAsyKi5HYW1lTW9kZUNvbmZp" + "Z3VyZS5EaWZmaWN1bHR5RGF0YURpY3RFbnRyeRJBChNBc3ltbWV0cmljYWxC" + "YXR0bGVzGAcgAygLMiQuR2FtZU1vZGVBc3ltbWV0cmljYWxCYXR0bGVDb25m" + "aWd1cmUSTgoWQXN5bW1ldHJpY2FsQmF0dGxlRGljdBgIIAMoCzIuLkdhbWVN" + "b2RlQ29uZmlndXJlLkFzeW1tZXRyaWNhbEJhdHRsZURpY3RFbnRyeRIxCgtN" + "dXRhdG9yUFZFcxgJIAMoCzIcLkdhbWVNb2RlTXV0YXRvclBWRUNvbmZpZ3Vy" + "ZRI+Cg5NdXRhdG9yUFZFRGljdBgKIAMoCzImLkdhbWVNb2RlQ29uZmlndXJl" + "Lk11dGF0b3JQVkVEaWN0RW50cnkSMQoLR2FsbGVyeU1hcHMYCyADKAsyHC5H" + "YW1lTW9kZUdhbGxlcnlNYXBDb25maWd1cmUSPgoOR2FsbGVyeU1hcERpY3QY" + "DCADKAsyJi5HYW1lTW9kZUNvbmZpZ3VyZS5HYWxsZXJ5TWFwRGljdEVudHJ5" + "GkcKDUluZm9EaWN0RW50cnkSCwoDa2V5GAEgASgPEiUKBXZhbHVlGAIgASgL" + "MhYuR2FtZU1vZGVJbmZvQ29uZmlndXJlOgI4ARpRChJOUENQbGF5ZXJEaWN0" + "RW50cnkSCwoDa2V5GAEgASgPEioKBXZhbHVlGAIgASgLMhsuR2FtZU1vZGVO" + "UENQbGF5ZXJDb25maWd1cmU6AjgBGlsKF0RpZmZpY3VsdHlEYXRhRGljdEVu" + "dHJ5EgsKA2tleRgBIAEoDxIvCgV2YWx1ZRgCIAEoCzIgLkdhbWVNb2RlRGlm" + "ZmljdWx0eURhdGFDb25maWd1cmU6AjgBGmMKG0FzeW1tZXRyaWNhbEJhdHRs" + "ZURpY3RFbnRyeRILCgNrZXkYASABKA8SMwoFdmFsdWUYAiABKAsyJC5HYW1l" + "TW9kZUFzeW1tZXRyaWNhbEJhdHRsZUNvbmZpZ3VyZToCOAEaUwoTTXV0YXRv" + "clBWRURpY3RFbnRyeRILCgNrZXkYASABKA8SKwoFdmFsdWUYAiABKAsyHC5H" + "YW1lTW9kZU11dGF0b3JQVkVDb25maWd1cmU6AjgBGlMKE0dhbGxlcnlNYXBE" + "aWN0RW50cnkSCwoDa2V5GAEgASgPEisKBXZhbHVlGAIgASgLMhwuR2FtZU1v" + "ZGVHYWxsZXJ5TWFwQ29uZmlndXJlOgI4AWIGcHJvdG8z"), new FileDescriptor[2]
		{
			TimestampReflection.Descriptor,
			EnumReflection.Descriptor
		}, new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[8]
		{
			new GeneratedClrTypeInfo(typeof(GameModeInfoConfigure), GameModeInfoConfigure.Parser, new string[25]
			{
				"MapModeType", "NameID", "IsShow", "AccountExp", "GoldFirst", "GoldOther", "MapID", "Upgrade", "VictoryParam", "CardInHandLimit",
				"Battlecost", "DistributeResources", "BountyParams", "ChristmasParams", "CardNumFix", "CoolDownFix", "Buffs", "BanCharacters", "OrderWeight", "RulesID",
				"IsMatch", "MatchDesID", "ModeDesID", "BeginTime", "EndTime"
			}, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(GameModeNPCPlayerConfigure), GameModeNPCPlayerConfigure.Parser, new string[5] { "MapModeType", "MonsterId", "RoomPosition", "PlayerPhoto", "AccountBackground" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(GameModeDifficultyDataConfigure), GameModeDifficultyDataConfigure.Parser, new string[2] { "MapModeType", "GameModeDifficultyDataConfigureItems" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(GameModeDifficultyDataConfigureItem), GameModeDifficultyDataConfigureItem.Parser, new string[4] { "Index", "DistributeResources", "UpgradeId", "DifficultyDescId" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(GameModeAsymmetricalBattleConfigure), GameModeAsymmetricalBattleConfigure.Parser, new string[6] { "Id", "AsymmetricalDefenderWinRound", "AsymmetricalAttackerWinScore", "AsymmetricalAttackerReviveRound", "AsymmetricalSpeedRound", "SpeedRoundAttrUp" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(GameModeMutatorPVEConfigure), GameModeMutatorPVEConfigure.Parser, new string[2] { "Id", "EligibleHeroIds" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(GameModeGalleryMapConfigure), GameModeGalleryMapConfigure.Parser, new string[4] { "GalleryMapType", "MapID", "GallerynameID", "GalleryDesID" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(GameModeConfigure), GameModeConfigure.Parser, new string[12]
			{
				"Infos", "InfoDict", "NPCPlayers", "NPCPlayerDict", "DifficultyDatas", "DifficultyDataDict", "AsymmetricalBattles", "AsymmetricalBattleDict", "MutatorPVEs", "MutatorPVEDict",
				"GalleryMaps", "GalleryMapDict"
			}, null, null, null, new GeneratedClrTypeInfo[6])
		}));
	}
}
