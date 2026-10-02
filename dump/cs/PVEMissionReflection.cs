using System;
using Google.Protobuf.Reflection;

public static class PVEMissionReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static PVEMissionReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("ChBQVkVNaXNzaW9uLnByb3RvGgpFbnVtLnByb3RvItAHChdQVkVNaXNzaW9u" + "SW5mb0NvbmZpZ3VyZRIKCgJpZBgBIAEoDxI1ChVwdmVNaXNzaW9uVHJpZ2dl" + "clR5cGUYAiABKA4yFi5QVkVNaXNzaW9uVHJpZ2dlclR5cGUSFQoNdHJpZ2dl" + "cnBhcmFtcxgDIAMoDxIVCg1taXNzaW9uRGVzY0lEGAQgASgPElAKFG1pc3Np" + "b25UYXJnZXRDb25maWdzGAUgAygLMjIuUFZFTWlzc2lvbkluZm9Db25maWd1" + "cmUuTWlzc2lvblRhcmdldENvbmZpZ3NFbnRyeRJQChRtaXNzaW9uRmFpbGVk" + "Q29uZmlncxgGIAMoCzIyLlBWRU1pc3Npb25JbmZvQ29uZmlndXJlLk1pc3Np" + "b25GYWlsZWRDb25maWdzRW50cnkSFQoNbWlzc2lvblRhcmdldBgHIAMoDxIU" + "CgxtaXNzaW9uUGFyYW0YCCADKA8SFAoMcmV3YXJkRGVzY0lEGAkgASgPEhQK" + "DGZhaWxlZERlc2NJRBgKIAEoDxITCgtyZXdhcmRQYXJhbRgLIAMoDxJCCg1y" + "ZXdhcmRDb25maWdzGAwgAygLMisuUFZFTWlzc2lvbkluZm9Db25maWd1cmUu" + "UmV3YXJkQ29uZmlnc0VudHJ5Ek4KE3Jld2FyZEZhaWxlZENvbmZpZ3MYDSAD" + "KAsyMS5QVkVNaXNzaW9uSW5mb0NvbmZpZ3VyZS5SZXdhcmRGYWlsZWRDb25m" + "aWdzRW50cnkaagoZTWlzc2lvblRhcmdldENvbmZpZ3NFbnRyeRILCgNrZXkY" + "ASABKA8SPAoFdmFsdWUYAiABKAsyLS5QVkVNaXNzaW9uSW5mb0NvbmZpZ3Vy" + "ZU1pc3Npb25UYXJnZXRDb25maWdzczoCOAEaagoZTWlzc2lvbkZhaWxlZENv" + "bmZpZ3NFbnRyeRILCgNrZXkYASABKA8SPAoFdmFsdWUYAiABKAsyLS5QVkVN" + "aXNzaW9uSW5mb0NvbmZpZ3VyZU1pc3Npb25GYWlsZWRDb25maWdzczoCOAEa" + "XAoSUmV3YXJkQ29uZmlnc0VudHJ5EgsKA2tleRgBIAEoDxI1CgV2YWx1ZRgC" + "IAEoCzImLlBWRU1pc3Npb25JbmZvQ29uZmlndXJlUmV3YXJkQ29uZmlnc3M6" + "AjgBGmgKGFJld2FyZEZhaWxlZENvbmZpZ3NFbnRyeRILCgNrZXkYASABKA8S" + "OwoFdmFsdWUYAiABKAsyLC5QVkVNaXNzaW9uSW5mb0NvbmZpZ3VyZVJld2Fy" + "ZEZhaWxlZENvbmZpZ3NzOgI4ASI+CixQVkVNaXNzaW9uSW5mb0NvbmZpZ3Vy" + "ZU1pc3Npb25UYXJnZXRDb25maWdzcxIOCgZ2YWx1ZXMYASADKA8iPgosUFZF" + "TWlzc2lvbkluZm9Db25maWd1cmVNaXNzaW9uRmFpbGVkQ29uZmlnc3MSDgoG" + "dmFsdWVzGAEgAygPIjcKJVBWRU1pc3Npb25JbmZvQ29uZmlndXJlUmV3YXJk" + "Q29uZmlnc3MSDgoGdmFsdWVzGAEgAygPIj0KK1BWRU1pc3Npb25JbmZvQ29u" + "ZmlndXJlUmV3YXJkRmFpbGVkQ29uZmlnc3MSDgoGdmFsdWVzGAEgAygPIkUK" + "F1BWRU1pc3Npb25Wb3RlQ29uZmlndXJlEgoKAmlkGAEgASgPEg8KB3JlbGlj" + "SWQYAiABKA8SDQoFbWFwSWQYAyABKA8i3wIKF1BWRU1pc3Npb25DbHVlQ29u" + "ZmlndXJlEgoKAmlkGAEgASgPEjUKFXB2ZU1pc3Npb25UcmlnZ2VyVHlwZRgC" + "IAEoDjIWLlBWRU1pc3Npb25UcmlnZ2VyVHlwZRIVCg10cmlnZ2VycGFyYW1z" + "GAMgAygPEhUKDW1pc3Npb25EZXNjSUQYBCABKA8SUAoUbWlzc2lvblRhcmdl" + "dENvbmZpZ3MYBSADKAsyMi5QVkVNaXNzaW9uQ2x1ZUNvbmZpZ3VyZS5NaXNz" + "aW9uVGFyZ2V0Q29uZmlnc0VudHJ5EhUKDXJld2FyZENvbmZpZ3MYBiABKA8a" + "agoZTWlzc2lvblRhcmdldENvbmZpZ3NFbnRyeRILCgNrZXkYASABKA8SPAoF" + "dmFsdWUYAiABKAsyLS5QVkVNaXNzaW9uQ2x1ZUNvbmZpZ3VyZU1pc3Npb25U" + "YXJnZXRDb25maWdzczoCOAEiPgosUFZFTWlzc2lvbkNsdWVDb25maWd1cmVN" + "aXNzaW9uVGFyZ2V0Q29uZmlnc3MSDgoGdmFsdWVzGAEgAygPIpMEChNQVkVN" + "aXNzaW9uQ29uZmlndXJlEicKBUluZm9zGAEgAygLMhguUFZFTWlzc2lvbklu" + "Zm9Db25maWd1cmUSNAoISW5mb0RpY3QYAiADKAsyIi5QVkVNaXNzaW9uQ29u" + "ZmlndXJlLkluZm9EaWN0RW50cnkSJwoFVm90ZXMYAyADKAsyGC5QVkVNaXNz" + "aW9uVm90ZUNvbmZpZ3VyZRI0CghWb3RlRGljdBgEIAMoCzIiLlBWRU1pc3Np" + "b25Db25maWd1cmUuVm90ZURpY3RFbnRyeRInCgVDbHVlcxgFIAMoCzIYLlBW" + "RU1pc3Npb25DbHVlQ29uZmlndXJlEjQKCENsdWVEaWN0GAYgAygLMiIuUFZF" + "TWlzc2lvbkNvbmZpZ3VyZS5DbHVlRGljdEVudHJ5GkkKDUluZm9EaWN0RW50" + "cnkSCwoDa2V5GAEgASgPEicKBXZhbHVlGAIgASgLMhguUFZFTWlzc2lvbklu" + "Zm9Db25maWd1cmU6AjgBGkkKDVZvdGVEaWN0RW50cnkSCwoDa2V5GAEgASgP" + "EicKBXZhbHVlGAIgASgLMhguUFZFTWlzc2lvblZvdGVDb25maWd1cmU6AjgB" + "GkkKDUNsdWVEaWN0RW50cnkSCwoDa2V5GAEgASgPEicKBXZhbHVlGAIgASgL" + "MhguUFZFTWlzc2lvbkNsdWVDb25maWd1cmU6AjgBYgZwcm90bzM="), new FileDescriptor[1] { EnumReflection.Descriptor }, new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[9]
		{
			new GeneratedClrTypeInfo(typeof(PVEMissionInfoConfigure), PVEMissionInfoConfigure.Parser, new string[13]
			{
				"Id", "PveMissionTriggerType", "Triggerparams", "MissionDescID", "MissionTargetConfigs", "MissionFailedConfigs", "MissionTarget", "MissionParam", "RewardDescID", "FailedDescID",
				"RewardParam", "RewardConfigs", "RewardFailedConfigs"
			}, null, null, null, new GeneratedClrTypeInfo[4]),
			new GeneratedClrTypeInfo(typeof(PVEMissionInfoConfigureMissionTargetConfigss), PVEMissionInfoConfigureMissionTargetConfigss.Parser, new string[1] { "Values" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(PVEMissionInfoConfigureMissionFailedConfigss), PVEMissionInfoConfigureMissionFailedConfigss.Parser, new string[1] { "Values" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(PVEMissionInfoConfigureRewardConfigss), PVEMissionInfoConfigureRewardConfigss.Parser, new string[1] { "Values" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(PVEMissionInfoConfigureRewardFailedConfigss), PVEMissionInfoConfigureRewardFailedConfigss.Parser, new string[1] { "Values" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(PVEMissionVoteConfigure), PVEMissionVoteConfigure.Parser, new string[3] { "Id", "RelicId", "MapId" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(PVEMissionClueConfigure), PVEMissionClueConfigure.Parser, new string[6] { "Id", "PveMissionTriggerType", "Triggerparams", "MissionDescID", "MissionTargetConfigs", "RewardConfigs" }, null, null, null, new GeneratedClrTypeInfo[1]),
			new GeneratedClrTypeInfo(typeof(PVEMissionClueConfigureMissionTargetConfigss), PVEMissionClueConfigureMissionTargetConfigss.Parser, new string[1] { "Values" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(PVEMissionConfigure), PVEMissionConfigure.Parser, new string[6] { "Infos", "InfoDict", "Votes", "VoteDict", "Clues", "ClueDict" }, null, null, null, new GeneratedClrTypeInfo[3])
		}));
	}
}
