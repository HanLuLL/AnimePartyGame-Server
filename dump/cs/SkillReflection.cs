using System;
using Google.Protobuf.Reflection;

public static class SkillReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static SkillReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("CgtTa2lsbC5wcm90bxoKRW51bS5wcm90byLMAQoSU2tpbGxJbmZvQ29uZmln" + "dXJlEgoKAmlkGAEgASgPEh0KCXNraWxsVHlwZRgCIAEoDjIKLlNraWxsVHlw" + "ZRIOCgZpc1Nob3cYAyABKAgSDgoGbmFtZUlEGAQgASgPEg4KBmRlc2NJRBgF" + "IAEoDxINCgVyb3VuZBgGIAEoDxIOCgZwYXJhbXMYByADKBESDgoGYnVmZklk" + "GAggAygPEhYKDnBlcmZvcm1UYXJnZXRzGAkgAygPEhQKDHBlcmZvcm1TZWxm" + "cxgKIAMoDyKrAQoOU2tpbGxDb25maWd1cmUSIgoFSW5mb3MYASADKAsyEy5T" + "a2lsbEluZm9Db25maWd1cmUSLwoISW5mb0RpY3QYAiADKAsyHS5Ta2lsbENv" + "bmZpZ3VyZS5JbmZvRGljdEVudHJ5GkQKDUluZm9EaWN0RW50cnkSCwoDa2V5" + "GAEgASgPEiIKBXZhbHVlGAIgASgLMhMuU2tpbGxJbmZvQ29uZmlndXJlOgI4" + "AWIGcHJvdG8z"), new FileDescriptor[1] { EnumReflection.Descriptor }, new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[2]
		{
			new GeneratedClrTypeInfo(typeof(SkillInfoConfigure), SkillInfoConfigure.Parser, new string[10] { "Id", "SkillType", "IsShow", "NameID", "DescID", "Round", "Params", "BuffId", "PerformTargets", "PerformSelfs" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(SkillConfigure), SkillConfigure.Parser, new string[2] { "Infos", "InfoDict" }, null, null, null, new GeneratedClrTypeInfo[1])
		}));
	}
}
