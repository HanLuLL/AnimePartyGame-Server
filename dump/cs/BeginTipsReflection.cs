using System;
using Google.Protobuf.Reflection;

public static class BeginTipsReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static BeginTipsReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("Cg9CZWdpblRpcHMucHJvdG8aCkVudW0ucHJvdG8iVwoWQmVnaW5UaXBzSW5m" + "b0NvbmZpZ3VyZRIKCgJJRBgBIAEoDxIOCgZ0aXBzSUQYAiABKA8SIQoLbWFw" + "TW9kZVR5cGUYAyADKA4yDC5NYXBNb2RlVHlwZSK7AQoSQmVnaW5UaXBzQ29u" + "ZmlndXJlEiYKBUluZm9zGAEgAygLMhcuQmVnaW5UaXBzSW5mb0NvbmZpZ3Vy" + "ZRIzCghJbmZvRGljdBgCIAMoCzIhLkJlZ2luVGlwc0NvbmZpZ3VyZS5JbmZv" + "RGljdEVudHJ5GkgKDUluZm9EaWN0RW50cnkSCwoDa2V5GAEgASgPEiYKBXZh" + "bHVlGAIgASgLMhcuQmVnaW5UaXBzSW5mb0NvbmZpZ3VyZToCOAFiBnByb3Rv" + "Mw=="), new FileDescriptor[1] { EnumReflection.Descriptor }, new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[2]
		{
			new GeneratedClrTypeInfo(typeof(BeginTipsInfoConfigure), BeginTipsInfoConfigure.Parser, new string[3] { "ID", "TipsID", "MapModeType" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(BeginTipsConfigure), BeginTipsConfigure.Parser, new string[2] { "Infos", "InfoDict" }, null, null, null, new GeneratedClrTypeInfo[1])
		}));
	}
}
