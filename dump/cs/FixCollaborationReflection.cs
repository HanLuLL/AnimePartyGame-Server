using System;
using Google.Protobuf.Reflection;
using Google.Protobuf.WellKnownTypes;

public static class FixCollaborationReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static FixCollaborationReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("ChZGaXhDb2xsYWJvcmF0aW9uLnByb3RvGh9nb29nbGUvcHJvdG9idWYvdGlt" + "ZXN0YW1wLnByb3RvIocBCh1GaXhDb2xsYWJvcmF0aW9uSW5mb0NvbmZpZ3Vy" + "ZRIKCgJpZBgBIAEoDxItCgliZWdpblRpbWUYAiABKAsyGi5nb29nbGUucHJv" + "dG9idWYuVGltZXN0YW1wEisKB2VuZFRpbWUYAyABKAsyGi5nb29nbGUucHJv" + "dG9idWYuVGltZXN0YW1wItcBChlGaXhDb2xsYWJvcmF0aW9uQ29uZmlndXJl" + "Ei0KBUluZm9zGAEgAygLMh4uRml4Q29sbGFib3JhdGlvbkluZm9Db25maWd1" + "cmUSOgoISW5mb0RpY3QYAiADKAsyKC5GaXhDb2xsYWJvcmF0aW9uQ29uZmln" + "dXJlLkluZm9EaWN0RW50cnkaTwoNSW5mb0RpY3RFbnRyeRILCgNrZXkYASAB" + "KA8SLQoFdmFsdWUYAiABKAsyHi5GaXhDb2xsYWJvcmF0aW9uSW5mb0NvbmZp" + "Z3VyZToCOAFiBnByb3RvMw=="), new FileDescriptor[1] { TimestampReflection.Descriptor }, new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[2]
		{
			new GeneratedClrTypeInfo(typeof(FixCollaborationInfoConfigure), FixCollaborationInfoConfigure.Parser, new string[3] { "Id", "BeginTime", "EndTime" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(FixCollaborationConfigure), FixCollaborationConfigure.Parser, new string[2] { "Infos", "InfoDict" }, null, null, null, new GeneratedClrTypeInfo[1])
		}));
	}
}
