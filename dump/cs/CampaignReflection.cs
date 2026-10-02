using System;
using Google.Protobuf.Reflection;

public static class CampaignReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static CampaignReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("Cg5DYW1wYWlnbi5wcm90bxoKRW51bS5wcm90byJEChhDYW1wYWlnbkNoYXB0" + "ZXJDb25maWd1cmUSCgoCaWQYASABKA8SDAoEbmFtZRgCIAEoDxIOCgZsZXZl" + "bHMYAyADKA8i5gcKFkNhbXBhaWduTGV2ZWxDb25maWd1cmUSCgoCaWQYASAB" + "KA8SEAoIZm9ybWVySUQYAiABKA8SDgoGYmFubmVyGAMgASgJEhQKDHNlcmlh" + "bE51bWJlchgEIAEoDxIMCgRuYW1lGAUgASgPEg0KBW1hcElEGAYgASgPEiEK" + "C21hcE1vZGVUeXBlGAcgASgOMgwuTWFwTW9kZVR5cGUSLwoSZ2FtZURpZmZp" + "Y3VsdHlUeXBlGAggASgOMhMuR2FtZURpZmZpY3VsdHlUeXBlEiEKC3ZpY3Rv" + "cnlUeXBlGAkgASgOMgwuVmljdG9yeVR5cGUSFQoNdmljdG9yeVBhcmFtcxgK" + "IAMoDxIaChJ2aWN0b3J5RGVzY3JpcHRpb24YCyABKA8SIAoYYmFubmVyVmlj" + "dG9yeURlc2NyaXB0aW9uGAwgASgPEkUKD2ZpcnN0UGFzc1Jld2FyZBgNIAMo" + "CzIsLkNhbXBhaWduTGV2ZWxDb25maWd1cmUuRmlyc3RQYXNzUmV3YXJkRW50" + "cnkSRwoQcmVwZWF0UGFzc1Jld2FyZBgOIAMoCzItLkNhbXBhaWduTGV2ZWxD" + "b25maWd1cmUuUmVwZWF0UGFzc1Jld2FyZEVudHJ5EhIKCmJvdEhlcm9JZHMY" + "DyADKA8SHgoWcHJvdGFnb25pc3RJbml0aWFsU3RhchgQIAEoDxIeChZwcm90" + "YWdvbmlzdEluaXRpYWxHb2xkGBEgASgPEhMKC2luaXRpYWxTdGFyGBIgAygP" + "EhMKC2luaXRpYWxHb2xkGBMgAygPEhMKC3RyaWdnZXJUaXBzGBQgAygPEisK" + "EGNob29zaW5nVGltZVR5cGUYFSABKA4yES5DaG9vc2luZ1RpbWVUeXBlEhUK" + "DWhlcm9lc0xpbWl0ZWQYFiADKA8SHgoWcHJvdGFnb25pc3RJbml0aWFsQ2Fy" + "ZBgXIAMoDxI9Cgtpbml0aWFsQ2FyZBgYIAMoCzIoLkNhbXBhaWduTGV2ZWxD" + "b25maWd1cmUuSW5pdGlhbENhcmRFbnRyeRIPCgdidWZmSWRzGBkgAygPGjYK" + "FEZpcnN0UGFzc1Jld2FyZEVudHJ5EgsKA2tleRgBIAEoDxINCgV2YWx1ZRgC" + "IAEoDzoCOAEaNwoVUmVwZWF0UGFzc1Jld2FyZEVudHJ5EgsKA2tleRgBIAEo" + "DxINCgV2YWx1ZRgCIAEoDzoCOAEaVwoQSW5pdGlhbENhcmRFbnRyeRILCgNr" + "ZXkYASABKA8SMgoFdmFsdWUYAiABKAsyIy5DYW1wYWlnbkxldmVsQ29uZmln" + "dXJlSW5pdGlhbENhcmRzOgI4ASI0CiJDYW1wYWlnbkxldmVsQ29uZmlndXJl" + "SW5pdGlhbENhcmRzEg4KBnZhbHVlcxgBIAMoDyKMAQoYQ2FtcGFpZ25Ucmln" + "Z2VyQ29uZmlndXJlEgoKAmlkGAEgASgPEjEKE2NhbXBhaWduVHJpZ2dlclR5" + "cGUYAiADKA4yFC5DYW1wYWlnblRyaWdnZXJUeXBlEh0KFWNhbXBhaWduVHJp" + "Z2dlclBhcmFtcxgDIAMoDxISCgp0dXRvcmlhbElEGAQgASgPIj0KF0NhbXBh" + "aWduVHJ5T3V0Q29uZmlndXJlEgoKAmlkGAEgASgPEhYKDmZvYmlkZGVuVHJ5" + "T3V0GAIgAygPItkFChFDYW1wYWlnbkNvbmZpZ3VyZRIrCghDaGFwdGVycxgB" + "IAMoCzIZLkNhbXBhaWduQ2hhcHRlckNvbmZpZ3VyZRI4CgtDaGFwdGVyRGlj" + "dBgCIAMoCzIjLkNhbXBhaWduQ29uZmlndXJlLkNoYXB0ZXJEaWN0RW50cnkS" + "JwoGTGV2ZWxzGAMgAygLMhcuQ2FtcGFpZ25MZXZlbENvbmZpZ3VyZRI0CglM" + "ZXZlbERpY3QYBCADKAsyIS5DYW1wYWlnbkNvbmZpZ3VyZS5MZXZlbERpY3RF" + "bnRyeRIrCghUcmlnZ2VycxgFIAMoCzIZLkNhbXBhaWduVHJpZ2dlckNvbmZp" + "Z3VyZRI4CgtUcmlnZ2VyRGljdBgGIAMoCzIjLkNhbXBhaWduQ29uZmlndXJl" + "LlRyaWdnZXJEaWN0RW50cnkSKQoHVHJ5T3V0cxgHIAMoCzIYLkNhbXBhaWdu" + "VHJ5T3V0Q29uZmlndXJlEjYKClRyeU91dERpY3QYCCADKAsyIi5DYW1wYWln" + "bkNvbmZpZ3VyZS5UcnlPdXREaWN0RW50cnkaTQoQQ2hhcHRlckRpY3RFbnRy" + "eRILCgNrZXkYASABKA8SKAoFdmFsdWUYAiABKAsyGS5DYW1wYWlnbkNoYXB0" + "ZXJDb25maWd1cmU6AjgBGkkKDkxldmVsRGljdEVudHJ5EgsKA2tleRgBIAEo" + "DxImCgV2YWx1ZRgCIAEoCzIXLkNhbXBhaWduTGV2ZWxDb25maWd1cmU6AjgB" + "Gk0KEFRyaWdnZXJEaWN0RW50cnkSCwoDa2V5GAEgASgPEigKBXZhbHVlGAIg" + "ASgLMhkuQ2FtcGFpZ25UcmlnZ2VyQ29uZmlndXJlOgI4ARpLCg9UcnlPdXRE" + "aWN0RW50cnkSCwoDa2V5GAEgASgPEicKBXZhbHVlGAIgASgLMhguQ2FtcGFp" + "Z25UcnlPdXRDb25maWd1cmU6AjgBYgZwcm90bzM="), new FileDescriptor[1] { EnumReflection.Descriptor }, new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[6]
		{
			new GeneratedClrTypeInfo(typeof(CampaignChapterConfigure), CampaignChapterConfigure.Parser, new string[3] { "Id", "Name", "Levels" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(CampaignLevelConfigure), CampaignLevelConfigure.Parser, new string[25]
			{
				"Id", "FormerID", "Banner", "SerialNumber", "Name", "MapID", "MapModeType", "GameDifficultyType", "VictoryType", "VictoryParams",
				"VictoryDescription", "BannerVictoryDescription", "FirstPassReward", "RepeatPassReward", "BotHeroIds", "ProtagonistInitialStar", "ProtagonistInitialGold", "InitialStar", "InitialGold", "TriggerTips",
				"ChoosingTimeType", "HeroesLimited", "ProtagonistInitialCard", "InitialCard", "BuffIds"
			}, null, null, null, new GeneratedClrTypeInfo[3]),
			new GeneratedClrTypeInfo(typeof(CampaignLevelConfigureInitialCards), CampaignLevelConfigureInitialCards.Parser, new string[1] { "Values" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(CampaignTriggerConfigure), CampaignTriggerConfigure.Parser, new string[4] { "Id", "CampaignTriggerType", "CampaignTriggerParams", "TutorialID" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(CampaignTryOutConfigure), CampaignTryOutConfigure.Parser, new string[2] { "Id", "FobiddenTryOut" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(CampaignConfigure), CampaignConfigure.Parser, new string[8] { "Chapters", "ChapterDict", "Levels", "LevelDict", "Triggers", "TriggerDict", "TryOuts", "TryOutDict" }, null, null, null, new GeneratedClrTypeInfo[4])
		}));
	}
}
