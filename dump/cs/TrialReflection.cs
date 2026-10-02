using System;
using Google.Protobuf.Reflection;
using Google.Protobuf.WellKnownTypes;

public static class TrialReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static TrialReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("CgtUcmlhbC5wcm90bxofZ29vZ2xlL3Byb3RvYnVmL3RpbWVzdGFtcC5wcm90" + "byKRAQoWVHJpYWxBY3Rpdml0eUNvbmZpZ3VyZRIKCgJpZBgBIAEoDxItCgli" + "ZWdpblRpbWUYAiABKAsyGi5nb29nbGUucHJvdG9idWYuVGltZXN0YW1wEisK" + "B2VuZFRpbWUYAyABKAsyGi5nb29nbGUucHJvdG9idWYuVGltZXN0YW1wEg8K" + "B2hlcm9JRHMYBCADKA8iWgoUVHJpYWxQYXJhbXNDb25maWd1cmUSCgoCaWQY" + "ASABKA8SEAoIcHZlTGV2ZWwYAiABKA8SEwoLcGxheWVyTGV2ZWwYAyABKA8S" + "DwoHaGVyb0lEcxgEIAMoDyLqAgoOVHJpYWxDb25maWd1cmUSKgoJQWN0aXZp" + "dHlzGAEgAygLMhcuVHJpYWxBY3Rpdml0eUNvbmZpZ3VyZRI3CgxBY3Rpdml0" + "eURpY3QYAiADKAsyIS5UcmlhbENvbmZpZ3VyZS5BY3Rpdml0eURpY3RFbnRy" + "eRImCgdQYXJhbXNzGAMgAygLMhUuVHJpYWxQYXJhbXNDb25maWd1cmUSMwoK" + "UGFyYW1zRGljdBgEIAMoCzIfLlRyaWFsQ29uZmlndXJlLlBhcmFtc0RpY3RF" + "bnRyeRpMChFBY3Rpdml0eURpY3RFbnRyeRILCgNrZXkYASABKA8SJgoFdmFs" + "dWUYAiABKAsyFy5UcmlhbEFjdGl2aXR5Q29uZmlndXJlOgI4ARpICg9QYXJh" + "bXNEaWN0RW50cnkSCwoDa2V5GAEgASgPEiQKBXZhbHVlGAIgASgLMhUuVHJp" + "YWxQYXJhbXNDb25maWd1cmU6AjgBYgZwcm90bzM="), new FileDescriptor[1] { TimestampReflection.Descriptor }, new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[3]
		{
			new GeneratedClrTypeInfo(typeof(TrialActivityConfigure), TrialActivityConfigure.Parser, new string[4] { "Id", "BeginTime", "EndTime", "HeroIDs" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(TrialParamsConfigure), TrialParamsConfigure.Parser, new string[4] { "Id", "PveLevel", "PlayerLevel", "HeroIDs" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(TrialConfigure), TrialConfigure.Parser, new string[4] { "Activitys", "ActivityDict", "Paramss", "ParamsDict" }, null, null, null, new GeneratedClrTypeInfo[2])
		}));
	}
}
