using System;
using Google.Protobuf.Reflection;
using Google.Protobuf.WellKnownTypes;

public static class FixItemReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static FixItemReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("Cg1GaXhJdGVtLnByb3RvGh9nb29nbGUvcHJvdG9idWYvdGltZXN0YW1wLnBy" + "b3RvIlMKFEZpeEl0ZW1JbmZvQ29uZmlndXJlEgoKAmlkGAEgASgPEi8KC2Vu" + "ZERhdGVUaW1lGAIgASgLMhouZ29vZ2xlLnByb3RvYnVmLlRpbWVzdGFtcCKB" + "AQoWRml4SXRlbVNoaWVsZENvbmZpZ3VyZRIKCgJpZBgBIAEoDxIuCgpsYXVu" + "Y2hUaW1lGAIgASgLMhouZ29vZ2xlLnByb3RvYnVmLlRpbWVzdGFtcBIrCgdl" + "bmRUaW1lGAMgASgLMhouZ29vZ2xlLnByb3RvYnVmLlRpbWVzdGFtcCLgAgoQ" + "Rml4SXRlbUNvbmZpZ3VyZRIkCgVJbmZvcxgBIAMoCzIVLkZpeEl0ZW1JbmZv" + "Q29uZmlndXJlEjEKCEluZm9EaWN0GAIgAygLMh8uRml4SXRlbUNvbmZpZ3Vy" + "ZS5JbmZvRGljdEVudHJ5EigKB1NoaWVsZHMYAyADKAsyFy5GaXhJdGVtU2hp" + "ZWxkQ29uZmlndXJlEjUKClNoaWVsZERpY3QYBCADKAsyIS5GaXhJdGVtQ29u" + "ZmlndXJlLlNoaWVsZERpY3RFbnRyeRpGCg1JbmZvRGljdEVudHJ5EgsKA2tl" + "eRgBIAEoDxIkCgV2YWx1ZRgCIAEoCzIVLkZpeEl0ZW1JbmZvQ29uZmlndXJl" + "OgI4ARpKCg9TaGllbGREaWN0RW50cnkSCwoDa2V5GAEgASgPEiYKBXZhbHVl" + "GAIgASgLMhcuRml4SXRlbVNoaWVsZENvbmZpZ3VyZToCOAFiBnByb3RvMw=="), new FileDescriptor[1] { TimestampReflection.Descriptor }, new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[3]
		{
			new GeneratedClrTypeInfo(typeof(FixItemInfoConfigure), FixItemInfoConfigure.Parser, new string[2] { "Id", "EndDateTime" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(FixItemShieldConfigure), FixItemShieldConfigure.Parser, new string[3] { "Id", "LaunchTime", "EndTime" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(FixItemConfigure), FixItemConfigure.Parser, new string[4] { "Infos", "InfoDict", "Shields", "ShieldDict" }, null, null, null, new GeneratedClrTypeInfo[2])
		}));
	}
}
