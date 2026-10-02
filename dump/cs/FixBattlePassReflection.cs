using System;
using Google.Protobuf.Reflection;
using Google.Protobuf.WellKnownTypes;

public static class FixBattlePassReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static FixBattlePassReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("ChNGaXhCYXR0bGVQYXNzLnByb3RvGh9nb29nbGUvcHJvdG9idWYvdGltZXN0" + "YW1wLnByb3RvIoQBChpGaXhCYXR0bGVQYXNzSW5mb0NvbmZpZ3VyZRIKCgJp" + "ZBgBIAEoDxItCgliZWdpblRpbWUYAiABKAsyGi5nb29nbGUucHJvdG9idWYu" + "VGltZXN0YW1wEisKB2VuZFRpbWUYAyABKAsyGi5nb29nbGUucHJvdG9idWYu" + "VGltZXN0YW1wIoQBChpGaXhCYXR0bGVQYXNzVGFza0NvbmZpZ3VyZRIKCgJp" + "ZBgBIAEoDxItCgliZWdpblRpbWUYAiABKAsyGi5nb29nbGUucHJvdG9idWYu" + "VGltZXN0YW1wEisKB2VuZFRpbWUYAyABKAsyGi5nb29nbGUucHJvdG9idWYu" + "VGltZXN0YW1wIv4CChZGaXhCYXR0bGVQYXNzQ29uZmlndXJlEioKBUluZm9z" + "GAEgAygLMhsuRml4QmF0dGxlUGFzc0luZm9Db25maWd1cmUSNwoISW5mb0Rp" + "Y3QYAiADKAsyJS5GaXhCYXR0bGVQYXNzQ29uZmlndXJlLkluZm9EaWN0RW50" + "cnkSKgoFVGFza3MYAyADKAsyGy5GaXhCYXR0bGVQYXNzVGFza0NvbmZpZ3Vy" + "ZRI3CghUYXNrRGljdBgEIAMoCzIlLkZpeEJhdHRsZVBhc3NDb25maWd1cmUu" + "VGFza0RpY3RFbnRyeRpMCg1JbmZvRGljdEVudHJ5EgsKA2tleRgBIAEoDxIq" + "CgV2YWx1ZRgCIAEoCzIbLkZpeEJhdHRsZVBhc3NJbmZvQ29uZmlndXJlOgI4" + "ARpMCg1UYXNrRGljdEVudHJ5EgsKA2tleRgBIAEoDxIqCgV2YWx1ZRgCIAEo" + "CzIbLkZpeEJhdHRsZVBhc3NUYXNrQ29uZmlndXJlOgI4AWIGcHJvdG8z"), new FileDescriptor[1] { TimestampReflection.Descriptor }, new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[3]
		{
			new GeneratedClrTypeInfo(typeof(FixBattlePassInfoConfigure), FixBattlePassInfoConfigure.Parser, new string[3] { "Id", "BeginTime", "EndTime" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(FixBattlePassTaskConfigure), FixBattlePassTaskConfigure.Parser, new string[3] { "Id", "BeginTime", "EndTime" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(FixBattlePassConfigure), FixBattlePassConfigure.Parser, new string[4] { "Infos", "InfoDict", "Tasks", "TaskDict" }, null, null, null, new GeneratedClrTypeInfo[2])
		}));
	}
}
