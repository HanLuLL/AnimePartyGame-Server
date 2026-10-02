using System;
using Google.Protobuf.Reflection;
using Google.Protobuf.WellKnownTypes;

public static class FixBannerReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static FixBannerReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("Cg9GaXhCYW5uZXIucHJvdG8aH2dvb2dsZS9wcm90b2J1Zi90aW1lc3RhbXAu" + "cHJvdG8ihgEKFkZpeEJhbm5lckluZm9Db25maWd1cmUSEAoIYmFubmVySUQY" + "ASABKA8SLQoJYmVnaW5UaW1lGAIgASgLMhouZ29vZ2xlLnByb3RvYnVmLlRp" + "bWVzdGFtcBIrCgdlbmRUaW1lGAMgASgLMhouZ29vZ2xlLnByb3RvYnVmLlRp" + "bWVzdGFtcCK7AQoSRml4QmFubmVyQ29uZmlndXJlEiYKBUluZm9zGAEgAygL" + "MhcuRml4QmFubmVySW5mb0NvbmZpZ3VyZRIzCghJbmZvRGljdBgCIAMoCzIh" + "LkZpeEJhbm5lckNvbmZpZ3VyZS5JbmZvRGljdEVudHJ5GkgKDUluZm9EaWN0" + "RW50cnkSCwoDa2V5GAEgASgPEiYKBXZhbHVlGAIgASgLMhcuRml4QmFubmVy" + "SW5mb0NvbmZpZ3VyZToCOAFiBnByb3RvMw=="), new FileDescriptor[1] { TimestampReflection.Descriptor }, new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[2]
		{
			new GeneratedClrTypeInfo(typeof(FixBannerInfoConfigure), FixBannerInfoConfigure.Parser, new string[3] { "BannerID", "BeginTime", "EndTime" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(FixBannerConfigure), FixBannerConfigure.Parser, new string[2] { "Infos", "InfoDict" }, null, null, null, new GeneratedClrTypeInfo[1])
		}));
	}
}
