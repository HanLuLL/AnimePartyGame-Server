using System;
using Google.Protobuf.Reflection;
using Google.Protobuf.WellKnownTypes;

public static class FixMissionReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static FixMissionReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("ChBGaXhNaXNzaW9uLnByb3RvGh9nb29nbGUvcHJvdG9idWYvdGltZXN0YW1w" + "LnByb3RvIoEBChdGaXhNaXNzaW9uRGF0YUNvbmZpZ3VyZRIKCgJpZBgBIAEo" + "DxItCgliZWdpblRpbWUYAiABKAsyGi5nb29nbGUucHJvdG9idWYuVGltZXN0" + "YW1wEisKB2VuZFRpbWUYAyABKAsyGi5nb29nbGUucHJvdG9idWYuVGltZXN0" + "YW1wIr8BChNGaXhNaXNzaW9uQ29uZmlndXJlEicKBURhdGFzGAEgAygLMhgu" + "Rml4TWlzc2lvbkRhdGFDb25maWd1cmUSNAoIRGF0YURpY3QYAiADKAsyIi5G" + "aXhNaXNzaW9uQ29uZmlndXJlLkRhdGFEaWN0RW50cnkaSQoNRGF0YURpY3RF" + "bnRyeRILCgNrZXkYASABKA8SJwoFdmFsdWUYAiABKAsyGC5GaXhNaXNzaW9u" + "RGF0YUNvbmZpZ3VyZToCOAFiBnByb3RvMw=="), new FileDescriptor[1] { TimestampReflection.Descriptor }, new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[2]
		{
			new GeneratedClrTypeInfo(typeof(FixMissionDataConfigure), FixMissionDataConfigure.Parser, new string[3] { "Id", "BeginTime", "EndTime" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(FixMissionConfigure), FixMissionConfigure.Parser, new string[2] { "Datas", "DataDict" }, null, null, null, new GeneratedClrTypeInfo[1])
		}));
	}
}
