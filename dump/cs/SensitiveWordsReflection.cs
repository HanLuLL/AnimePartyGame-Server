using System;
using Google.Protobuf.Reflection;

public static class SensitiveWordsReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static SensitiveWordsReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("ChRTZW5zaXRpdmVXb3Jkcy5wcm90byI3ChtTZW5zaXRpdmVXb3Jkc0luZm9D" + "b25maWd1cmUSCgoCaWQYASABKA8SDAoEd29yZBgCIAEoCSLPAQoXU2Vuc2l0" + "aXZlV29yZHNDb25maWd1cmUSKwoFSW5mb3MYASADKAsyHC5TZW5zaXRpdmVX" + "b3Jkc0luZm9Db25maWd1cmUSOAoISW5mb0RpY3QYAiADKAsyJi5TZW5zaXRp" + "dmVXb3Jkc0NvbmZpZ3VyZS5JbmZvRGljdEVudHJ5Gk0KDUluZm9EaWN0RW50" + "cnkSCwoDa2V5GAEgASgPEisKBXZhbHVlGAIgASgLMhwuU2Vuc2l0aXZlV29y" + "ZHNJbmZvQ29uZmlndXJlOgI4AWIGcHJvdG8z"), new FileDescriptor[0], new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[2]
		{
			new GeneratedClrTypeInfo(typeof(SensitiveWordsInfoConfigure), SensitiveWordsInfoConfigure.Parser, new string[2] { "Id", "Word" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(SensitiveWordsConfigure), SensitiveWordsConfigure.Parser, new string[2] { "Infos", "InfoDict" }, null, null, null, new GeneratedClrTypeInfo[1])
		}));
	}
}
