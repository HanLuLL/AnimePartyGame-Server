using System;
using Google.Protobuf.Reflection;
using Google.Protobuf.WellKnownTypes;

public static class FixSignInReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static FixSignInReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("Cg9GaXhTaWduSW4ucHJvdG8aH2dvb2dsZS9wcm90b2J1Zi90aW1lc3RhbXAu" + "cHJvdG8igAEKFkZpeFNpZ25JbkluZm9Db25maWd1cmUSCgoCaWQYASABKA8S" + "LQoJYmVnaW5UaW1lGAIgASgLMhouZ29vZ2xlLnByb3RvYnVmLlRpbWVzdGFt" + "cBIrCgdlbmRUaW1lGAMgASgLMhouZ29vZ2xlLnByb3RvYnVmLlRpbWVzdGFt" + "cCK7AQoSRml4U2lnbkluQ29uZmlndXJlEiYKBUluZm9zGAEgAygLMhcuRml4" + "U2lnbkluSW5mb0NvbmZpZ3VyZRIzCghJbmZvRGljdBgCIAMoCzIhLkZpeFNp" + "Z25JbkNvbmZpZ3VyZS5JbmZvRGljdEVudHJ5GkgKDUluZm9EaWN0RW50cnkS" + "CwoDa2V5GAEgASgPEiYKBXZhbHVlGAIgASgLMhcuRml4U2lnbkluSW5mb0Nv" + "bmZpZ3VyZToCOAFiBnByb3RvMw=="), new FileDescriptor[1] { TimestampReflection.Descriptor }, new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[2]
		{
			new GeneratedClrTypeInfo(typeof(FixSignInInfoConfigure), FixSignInInfoConfigure.Parser, new string[3] { "Id", "BeginTime", "EndTime" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(FixSignInConfigure), FixSignInConfigure.Parser, new string[2] { "Infos", "InfoDict" }, null, null, null, new GeneratedClrTypeInfo[1])
		}));
	}
}
