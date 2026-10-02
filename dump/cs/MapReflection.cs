using System;
using Google.Protobuf.Reflection;
using Google.Protobuf.WellKnownTypes;

public static class MapReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static MapReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("CglNYXAucHJvdG8aH2dvb2dsZS9wcm90b2J1Zi90aW1lc3RhbXAucHJvdG8a" + "CkVudW0ucHJvdG8iyAQKEE1hcEluZm9Db25maWd1cmUSCgoCaWQYASABKA8S" + "DwoHbWFwTmFtZRgCIAEoDxIWCg5tYXBEZXNjcmlwdGlvbhgDIAEoDxISCgpt" + "YXBHYWxsZXJ5GAQgASgPEgwKBG1pZHMYBSADKBESFQoNZGlmZmljdWx0eUlk" + "cxgGIAMoDxITCgttYXBldmVudElEcxgHIAMoDxIQCghtYXBJbWFnZRgIIAEo" + "CRIVCg1tYXBTY2VuZUltYWdlGAkgASgJEhIKClR1dG9yaWFsSWQYCiABKA8S" + "FgoObWFwU3BlY2lhbENhcmQYCyADKA8SFwoPbWFwU3BlY2lhbEV2ZW50GAwg" + "AygPEhkKBnNwTGFuZBgNIAEoDjIJLkxhbmRUeXBlEhcKD21hcFNwZWNpYWxS" + "ZWxpYxgOIAMoDxIbChNwcmVsb2FkQ2hhcmFjdGVySWRzGA8gAygPEhAKCHN0" + "b3J5SWRzGBAgAygPEi0KCWJlZ2luVGltZRgRIAEoCzIaLmdvb2dsZS5wcm90" + "b2J1Zi5UaW1lc3RhbXASKwoHZW5kVGltZRgSIAEoCzIaLmdvb2dsZS5wcm90" + "b2J1Zi5UaW1lc3RhbXASMAoMYmVnaW5UaW1lTWFwGBMgASgLMhouZ29vZ2xl" + "LnByb3RvYnVmLlRpbWVzdGFtcBIuCgplbmRUaW1lTWFwGBQgASgLMhouZ29v" + "Z2xlLnByb3RvYnVmLlRpbWVzdGFtcBIRCglMYWJlbFR5cGUYFSABKA8SDwoH" + "TWFwTWFyaxgWIAEoDyKDAQoRTWFwU2NlbmVDb25maWd1cmUSCgoCaWQYASAB" + "KA8SEQoJYXNzZXROYW1lGAIgASgJEhMKC21pbmlNYXBOYW1lGAMgASgJEhcK" + "D2NoYXJhY3RlckhlaWdodBgEIAEoAhIUCgxzdW1tb25IZWlnaHQYBSABKAIS" + "CwoDYmdtGAYgASgPInIKGk1hcEdhbWVEaWZmaWN1bHR5Q29uZmlndXJlEgoK" + "AmlkGAEgASgPEkgKH21hcEdhbWVEaWZmaWN1bHR5Q29uZmlndXJlSXRlbXMY" + "AiADKAsyHy5NYXBHYW1lRGlmZmljdWx0eUNvbmZpZ3VyZUl0ZW0i4QIKHk1h" + "cEdhbWVEaWZmaWN1bHR5Q29uZmlndXJlSXRlbRINCgVpbmRleBgBIAEoDxIV" + "Cg1wcm9ncmVzc0xpbWl0GAIgASgPEhYKDnByb2dyZXNzRXZlbnRzGAMgAygP" + "EhMKC3B2ZU1pc3Npb25zGAQgAygPEkMKCm1vbnN0ZXJJZHMYBSADKAsyLy5N" + "YXBHYW1lRGlmZmljdWx0eUNvbmZpZ3VyZUl0ZW0uTW9uc3Rlcklkc0VudHJ5" + "EhsKE3ByZWxvYWRDaGFyYWN0ZXJJZHMYBiADKA8SJQoKZXh0cmFNb2RlcxgH" + "IAMoDjIRLlBWRUV4dHJhTW9kZVR5cGUSEwoLbXV0YXRvclBvb2wYCCABKA8S" + "GwoTcGVyZm9ybVRyaWdnZXJTZXRJZBgJIAEoDxoxCg9Nb25zdGVySWRzRW50" + "cnkSCwoDa2V5GAEgASgPEg0KBXZhbHVlGAIgASgPOgI4ASKtBAoVTWFwTWFw" + "UmV3YXJkQ29uZmlndXJlEgoKAmlkGAEgASgPEkAKDXZpY3RvcnlSZXdhcmQY" + "AiADKAsyKS5NYXBNYXBSZXdhcmRDb25maWd1cmUuVmljdG9yeVJld2FyZEVu" + "dHJ5EjoKCmxvc2VSZXdhcmQYAyADKAsyJi5NYXBNYXBSZXdhcmRDb25maWd1" + "cmUuTG9zZVJld2FyZEVudHJ5ElAKFXZpY3RvcnlSZXdhcmRBZGRpdGlvbhgE" + "IAMoCzIxLk1hcE1hcFJld2FyZENvbmZpZ3VyZS5WaWN0b3J5UmV3YXJkQWRk" + "aXRpb25FbnRyeRJSChZjb21lYmFja1Jld2FyZEFkZGl0aW9uGAUgAygLMjIu" + "TWFwTWFwUmV3YXJkQ29uZmlndXJlLkNvbWViYWNrUmV3YXJkQWRkaXRpb25F" + "bnRyeRo0ChJWaWN0b3J5UmV3YXJkRW50cnkSCwoDa2V5GAEgASgPEg0KBXZh" + "bHVlGAIgASgPOgI4ARoxCg9Mb3NlUmV3YXJkRW50cnkSCwoDa2V5GAEgASgP" + "Eg0KBXZhbHVlGAIgASgPOgI4ARo8ChpWaWN0b3J5UmV3YXJkQWRkaXRpb25F" + "bnRyeRILCgNrZXkYASABKA8SDQoFdmFsdWUYAiABKA86AjgBGj0KG0NvbWVi" + "YWNrUmV3YXJkQWRkaXRpb25FbnRyeRILCgNrZXkYASABKA8SDQoFdmFsdWUY" + "AiABKA86AjgBImAKFE1hcE1hcExldmVsQ29uZmlndXJlEgoKAmlkGAEgASgP" + "EjwKGW1hcE1hcExldmVsQ29uZmlndXJlSXRlbXMYAiADKAsyGS5NYXBNYXBM" + "ZXZlbENvbmZpZ3VyZUl0ZW0ihQEKGE1hcE1hcExldmVsQ29uZmlndXJlSXRl" + "bRINCgVpbmRleBgBIAEoDxItCgliZWdpblRpbWUYAiABKAsyGi5nb29nbGUu" + "cHJvdG9idWYuVGltZXN0YW1wEisKB2VuZFRpbWUYAyABKAsyGi5nb29nbGUu" + "cHJvdG9idWYuVGltZXN0YW1wImQKE01hcE1hcFBvb2xDb25maWd1cmUSCgoC" + "aWQYASABKA8SEQoJZXZlbnRQb29sGAIgASgPEhYKDmVmZmVjdENhcmRQb29s" + "GAMgASgPEhYKDmJhdHRsZUNhcmRQb29sGAQgASgPIpYICgxNYXBDb25maWd1" + "cmUSIAoFSW5mb3MYASADKAsyES5NYXBJbmZvQ29uZmlndXJlEi0KCEluZm9E" + "aWN0GAIgAygLMhsuTWFwQ29uZmlndXJlLkluZm9EaWN0RW50cnkSIgoGU2Nl" + "bmVzGAMgAygLMhIuTWFwU2NlbmVDb25maWd1cmUSLwoJU2NlbmVEaWN0GAQg" + "AygLMhwuTWFwQ29uZmlndXJlLlNjZW5lRGljdEVudHJ5EjQKD0dhbWVEaWZm" + "aWN1bHR5cxgFIAMoCzIbLk1hcEdhbWVEaWZmaWN1bHR5Q29uZmlndXJlEkEK" + "EkdhbWVEaWZmaWN1bHR5RGljdBgGIAMoCzIlLk1hcENvbmZpZ3VyZS5HYW1l" + "RGlmZmljdWx0eURpY3RFbnRyeRIqCgpNYXBSZXdhcmRzGAcgAygLMhYuTWFw" + "TWFwUmV3YXJkQ29uZmlndXJlEjcKDU1hcFJld2FyZERpY3QYCCADKAsyIC5N" + "YXBDb25maWd1cmUuTWFwUmV3YXJkRGljdEVudHJ5EigKCU1hcExldmVscxgJ" + "IAMoCzIVLk1hcE1hcExldmVsQ29uZmlndXJlEjUKDE1hcExldmVsRGljdBgK" + "IAMoCzIfLk1hcENvbmZpZ3VyZS5NYXBMZXZlbERpY3RFbnRyeRImCghNYXBQ" + "b29scxgLIAMoCzIULk1hcE1hcFBvb2xDb25maWd1cmUSMwoLTWFwUG9vbERp" + "Y3QYDCADKAsyHi5NYXBDb25maWd1cmUuTWFwUG9vbERpY3RFbnRyeRpCCg1J" + "bmZvRGljdEVudHJ5EgsKA2tleRgBIAEoDxIgCgV2YWx1ZRgCIAEoCzIRLk1h" + "cEluZm9Db25maWd1cmU6AjgBGkQKDlNjZW5lRGljdEVudHJ5EgsKA2tleRgB" + "IAEoDxIhCgV2YWx1ZRgCIAEoCzISLk1hcFNjZW5lQ29uZmlndXJlOgI4ARpW" + "ChdHYW1lRGlmZmljdWx0eURpY3RFbnRyeRILCgNrZXkYASABKA8SKgoFdmFs" + "dWUYAiABKAsyGy5NYXBHYW1lRGlmZmljdWx0eUNvbmZpZ3VyZToCOAEaTAoS" + "TWFwUmV3YXJkRGljdEVudHJ5EgsKA2tleRgBIAEoDxIlCgV2YWx1ZRgCIAEo" + "CzIWLk1hcE1hcFJld2FyZENvbmZpZ3VyZToCOAEaSgoRTWFwTGV2ZWxEaWN0" + "RW50cnkSCwoDa2V5GAEgASgPEiQKBXZhbHVlGAIgASgLMhUuTWFwTWFwTGV2" + "ZWxDb25maWd1cmU6AjgBGkgKEE1hcFBvb2xEaWN0RW50cnkSCwoDa2V5GAEg" + "ASgPEiMKBXZhbHVlGAIgASgLMhQuTWFwTWFwUG9vbENvbmZpZ3VyZToCOAFi" + "BnByb3RvMw=="), new FileDescriptor[2]
		{
			TimestampReflection.Descriptor,
			EnumReflection.Descriptor
		}, new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[9]
		{
			new GeneratedClrTypeInfo(typeof(MapInfoConfigure), MapInfoConfigure.Parser, new string[22]
			{
				"Id", "MapName", "MapDescription", "MapGallery", "Mids", "DifficultyIds", "MapeventIDs", "MapImage", "MapSceneImage", "TutorialId",
				"MapSpecialCard", "MapSpecialEvent", "SpLand", "MapSpecialRelic", "PreloadCharacterIds", "StoryIds", "BeginTime", "EndTime", "BeginTimeMap", "EndTimeMap",
				"LabelType", "MapMark"
			}, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(MapSceneConfigure), MapSceneConfigure.Parser, new string[6] { "Id", "AssetName", "MiniMapName", "CharacterHeight", "SummonHeight", "Bgm" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(MapGameDifficultyConfigure), MapGameDifficultyConfigure.Parser, new string[2] { "Id", "MapGameDifficultyConfigureItems" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(MapGameDifficultyConfigureItem), MapGameDifficultyConfigureItem.Parser, new string[9] { "Index", "ProgressLimit", "ProgressEvents", "PveMissions", "MonsterIds", "PreloadCharacterIds", "ExtraModes", "MutatorPool", "PerformTriggerSetId" }, null, null, null, new GeneratedClrTypeInfo[1]),
			new GeneratedClrTypeInfo(typeof(MapMapRewardConfigure), MapMapRewardConfigure.Parser, new string[5] { "Id", "VictoryReward", "LoseReward", "VictoryRewardAddition", "ComebackRewardAddition" }, null, null, null, new GeneratedClrTypeInfo[4]),
			new GeneratedClrTypeInfo(typeof(MapMapLevelConfigure), MapMapLevelConfigure.Parser, new string[2] { "Id", "MapMapLevelConfigureItems" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(MapMapLevelConfigureItem), MapMapLevelConfigureItem.Parser, new string[3] { "Index", "BeginTime", "EndTime" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(MapMapPoolConfigure), MapMapPoolConfigure.Parser, new string[4] { "Id", "EventPool", "EffectCardPool", "BattleCardPool" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(MapConfigure), MapConfigure.Parser, new string[12]
			{
				"Infos", "InfoDict", "Scenes", "SceneDict", "GameDifficultys", "GameDifficultyDict", "MapRewards", "MapRewardDict", "MapLevels", "MapLevelDict",
				"MapPools", "MapPoolDict"
			}, null, null, null, new GeneratedClrTypeInfo[6])
		}));
	}
}
