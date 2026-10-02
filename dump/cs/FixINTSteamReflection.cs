using System;
using Google.Protobuf.Reflection;

public static class FixINTSteamReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static FixINTSteamReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("ChFGaXhJTlRTdGVhbS5wcm90byI4ChxGaXhJTlRTdGVhbUl0ZW1JbmZvQ29u" + "ZmlndXJlEgoKAmlkGAEgASgPEgwKBGljb24YAiABKAki2wEKFEZpeElOVFN0" + "ZWFtQ29uZmlndXJlEjAKCUl0ZW1JbmZvcxgBIAMoCzIdLkZpeElOVFN0ZWFt" + "SXRlbUluZm9Db25maWd1cmUSPQoMSXRlbUluZm9EaWN0GAIgAygLMicuRml4" + "SU5UU3RlYW1Db25maWd1cmUuSXRlbUluZm9EaWN0RW50cnkaUgoRSXRlbUlu" + "Zm9EaWN0RW50cnkSCwoDa2V5GAEgASgPEiwKBXZhbHVlGAIgASgLMh0uRml4" + "SU5UU3RlYW1JdGVtSW5mb0NvbmZpZ3VyZToCOAFiBnByb3RvMw=="), new FileDescriptor[0], new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[2]
		{
			new GeneratedClrTypeInfo(typeof(FixINTSteamItemInfoConfigure), FixINTSteamItemInfoConfigure.Parser, new string[2] { "Id", "Icon" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(FixINTSteamConfigure), FixINTSteamConfigure.Parser, new string[2] { "ItemInfos", "ItemInfoDict" }, null, null, null, new GeneratedClrTypeInfo[1])
		}));
	}
}
