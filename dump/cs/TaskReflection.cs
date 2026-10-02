using System;
using Google.Protobuf.Reflection;

public static class TaskReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static TaskReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("CgpUYXNrLnByb3RvGgpFbnVtLnByb3RvIvYBChVUYXNrQmVnaW5uZXJDb25m" + "aWd1cmUSCgoCaWQYASABKA8SJQoNY29uZGl0aW9uVHlwZRgCIAEoDjIOLkNv" + "bmRpdGlvblR5cGUSDQoFcGFyYW0YAyABKA8SCwoDcmVmGAQgASgPEg4KBm5h" + "bWVJRBgFIAEoDxIOCgZkZXNjSWQYBiABKA8SMgoGcmV3YXJkGAcgAygLMiIu" + "VGFza0JlZ2lubmVyQ29uZmlndXJlLlJld2FyZEVudHJ5EgsKA3dheRgIIAEo" + "DxotCgtSZXdhcmRFbnRyeRILCgNrZXkYASABKA8SDQoFdmFsdWUYAiABKA86" + "AjgBItgBChNUYXNrV2Vla2x5Q29uZmlndXJlEgoKAmlkGAEgASgPEiUKDWNv" + "bmRpdGlvblR5cGUYAiABKA4yDi5Db25kaXRpb25UeXBlEg0KBXBhcmFtGAMg" + "ASgPEg4KBm5hbWVJRBgEIAEoDxIOCgZkZXNjSWQYBSABKA8SMAoGcmV3YXJk" + "GAYgAygLMiAuVGFza1dlZWtseUNvbmZpZ3VyZS5SZXdhcmRFbnRyeRotCgtS" + "ZXdhcmRFbnRyeRILCgNrZXkYASABKA8SDQoFdmFsdWUYAiABKA86AjgBIqQB" + "ChtUYXNrV2Vla2x5UHJvZ3Jlc3NDb25maWd1cmUSCgoCaWQYASABKA8SEAoI" + "cHJvZ3Jlc3MYAiABKA8SOAoGcmV3YXJkGAMgAygLMiguVGFza1dlZWtseVBy" + "b2dyZXNzQ29uZmlndXJlLlJld2FyZEVudHJ5Gi0KC1Jld2FyZEVudHJ5EgsK" + "A2tleRgBIAEoDxINCgV2YWx1ZRgCIAEoDzoCOAEi+wEKEVRhc2tEYXk3Q29u" + "ZmlndXJlEgoKAmlkGAEgASgPEgsKA2RheRgCIAEoDxIlCg1jb25kaXRpb25U" + "eXBlGAMgASgOMg4uQ29uZGl0aW9uVHlwZRINCgVwYXJhbRgEIAEoDxILCgNy" + "ZWYYBSABKA8SDgoGbmFtZUlEGAYgASgPEg4KBmRlc2NJZBgHIAEoDxIuCgZy" + "ZXdhcmQYCCADKAsyHi5UYXNrRGF5N0NvbmZpZ3VyZS5SZXdhcmRFbnRyeRIL" + "CgN3YXkYCSABKA8aLQoLUmV3YXJkRW50cnkSCwoDa2V5GAEgASgPEg0KBXZh" + "bHVlGAIgASgPOgI4ASLPBQoNVGFza0NvbmZpZ3VyZRIpCglCZWdpbm5lcnMY" + "ASADKAsyFi5UYXNrQmVnaW5uZXJDb25maWd1cmUSNgoMQmVnaW5uZXJEaWN0" + "GAIgAygLMiAuVGFza0NvbmZpZ3VyZS5CZWdpbm5lckRpY3RFbnRyeRIlCgdX" + "ZWVrbHlzGAMgAygLMhQuVGFza1dlZWtseUNvbmZpZ3VyZRIyCgpXZWVrbHlE" + "aWN0GAQgAygLMh4uVGFza0NvbmZpZ3VyZS5XZWVrbHlEaWN0RW50cnkSNQoP" + "V2Vla2x5UHJvZ3Jlc3NzGAUgAygLMhwuVGFza1dlZWtseVByb2dyZXNzQ29u" + "ZmlndXJlEkIKEldlZWtseVByb2dyZXNzRGljdBgGIAMoCzImLlRhc2tDb25m" + "aWd1cmUuV2Vla2x5UHJvZ3Jlc3NEaWN0RW50cnkSIQoFRGF5N3MYByADKAsy" + "Ei5UYXNrRGF5N0NvbmZpZ3VyZRIuCghEYXk3RGljdBgIIAMoCzIcLlRhc2tD" + "b25maWd1cmUuRGF5N0RpY3RFbnRyeRpLChFCZWdpbm5lckRpY3RFbnRyeRIL" + "CgNrZXkYASABKA8SJQoFdmFsdWUYAiABKAsyFi5UYXNrQmVnaW5uZXJDb25m" + "aWd1cmU6AjgBGkcKD1dlZWtseURpY3RFbnRyeRILCgNrZXkYASABKA8SIwoF" + "dmFsdWUYAiABKAsyFC5UYXNrV2Vla2x5Q29uZmlndXJlOgI4ARpXChdXZWVr" + "bHlQcm9ncmVzc0RpY3RFbnRyeRILCgNrZXkYASABKA8SKwoFdmFsdWUYAiAB" + "KAsyHC5UYXNrV2Vla2x5UHJvZ3Jlc3NDb25maWd1cmU6AjgBGkMKDURheTdE" + "aWN0RW50cnkSCwoDa2V5GAEgASgPEiEKBXZhbHVlGAIgASgLMhIuVGFza0Rh" + "eTdDb25maWd1cmU6AjgBYgZwcm90bzM="), new FileDescriptor[1] { EnumReflection.Descriptor }, new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[5]
		{
			new GeneratedClrTypeInfo(typeof(TaskBeginnerConfigure), TaskBeginnerConfigure.Parser, new string[8] { "Id", "ConditionType", "Param", "Ref", "NameID", "DescId", "Reward", "Way" }, null, null, null, new GeneratedClrTypeInfo[1]),
			new GeneratedClrTypeInfo(typeof(TaskWeeklyConfigure), TaskWeeklyConfigure.Parser, new string[6] { "Id", "ConditionType", "Param", "NameID", "DescId", "Reward" }, null, null, null, new GeneratedClrTypeInfo[1]),
			new GeneratedClrTypeInfo(typeof(TaskWeeklyProgressConfigure), TaskWeeklyProgressConfigure.Parser, new string[3] { "Id", "Progress", "Reward" }, null, null, null, new GeneratedClrTypeInfo[1]),
			new GeneratedClrTypeInfo(typeof(TaskDay7Configure), TaskDay7Configure.Parser, new string[9] { "Id", "Day", "ConditionType", "Param", "Ref", "NameID", "DescId", "Reward", "Way" }, null, null, null, new GeneratedClrTypeInfo[1]),
			new GeneratedClrTypeInfo(typeof(TaskConfigure), TaskConfigure.Parser, new string[8] { "Beginners", "BeginnerDict", "Weeklys", "WeeklyDict", "WeeklyProgresss", "WeeklyProgressDict", "Day7S", "Day7Dict" }, null, null, null, new GeneratedClrTypeInfo[4])
		}));
	}
}
