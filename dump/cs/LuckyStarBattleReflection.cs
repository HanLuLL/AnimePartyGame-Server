using System;
using Google.Protobuf.Reflection;

public static class LuckyStarBattleReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static LuckyStarBattleReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("ChVMdWNreVN0YXJCYXR0bGUucHJvdG8aCkVudW0ucHJvdG8i9AEKHEx1Y2t5" + "U3RhckJhdHRsZUluZm9Db25maWd1cmUSCgoCaWQYASABKA8SDgoGbmFtZUlE" + "GAIgASgPEg4KBmRlc2NJZBgDIAEoDxIUCgxyZXdhcmREZXNjSWQYBCABKA8S" + "DQoFaW1hZ2UYBSABKAkSMwoUTHVja3lTdGFyTWlzc2lvblR5cGUYBiABKA4y" + "FS5MdWNreVN0YXJNaXNzaW9uVHlwZRIUCgxNaXNzaW9uUGFyYW0YByADKA8S" + "DgoGUmV3YXJkGAggAygPEhcKD0x1Y2t5U3RhclJld2FyZBgJIAEoDxIPCgdi" + "dWZmSWRzGAogAygPIlgKHUx1Y2t5U3RhckJhdHRsZVBhcmFtQ29uZmlndXJl" + "EgoKAmlkGAEgASgPEhUKDW5lZWRMdWNreVN0YXIYAiABKA8SFAoMbWlzc2lv" + "bkNvdW50GAMgAygPIpIDChhMdWNreVN0YXJCYXR0bGVDb25maWd1cmUSLAoF" + "SW5mb3MYASADKAsyHS5MdWNreVN0YXJCYXR0bGVJbmZvQ29uZmlndXJlEjkK" + "CEluZm9EaWN0GAIgAygLMicuTHVja3lTdGFyQmF0dGxlQ29uZmlndXJlLklu" + "Zm9EaWN0RW50cnkSLgoGUGFyYW1zGAMgAygLMh4uTHVja3lTdGFyQmF0dGxl" + "UGFyYW1Db25maWd1cmUSOwoJUGFyYW1EaWN0GAQgAygLMiguTHVja3lTdGFy" + "QmF0dGxlQ29uZmlndXJlLlBhcmFtRGljdEVudHJ5Gk4KDUluZm9EaWN0RW50" + "cnkSCwoDa2V5GAEgASgPEiwKBXZhbHVlGAIgASgLMh0uTHVja3lTdGFyQmF0" + "dGxlSW5mb0NvbmZpZ3VyZToCOAEaUAoOUGFyYW1EaWN0RW50cnkSCwoDa2V5" + "GAEgASgPEi0KBXZhbHVlGAIgASgLMh4uTHVja3lTdGFyQmF0dGxlUGFyYW1D" + "b25maWd1cmU6AjgBYgZwcm90bzM="), new FileDescriptor[1] { EnumReflection.Descriptor }, new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[3]
		{
			new GeneratedClrTypeInfo(typeof(LuckyStarBattleInfoConfigure), LuckyStarBattleInfoConfigure.Parser, new string[10] { "Id", "NameID", "DescId", "RewardDescId", "Image", "LuckyStarMissionType", "MissionParam", "Reward", "LuckyStarReward", "BuffIds" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(LuckyStarBattleParamConfigure), LuckyStarBattleParamConfigure.Parser, new string[3] { "Id", "NeedLuckyStar", "MissionCount" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(LuckyStarBattleConfigure), LuckyStarBattleConfigure.Parser, new string[4] { "Infos", "InfoDict", "Params", "ParamDict" }, null, null, null, new GeneratedClrTypeInfo[2])
		}));
	}
}
