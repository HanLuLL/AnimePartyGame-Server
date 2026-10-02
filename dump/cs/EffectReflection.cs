using System;
using Google.Protobuf.Reflection;

public static class EffectReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static EffectReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("CgxFZmZlY3QucHJvdG8iegoTRWZmZWN0SW5mb0NvbmZpZ3VyZRIKCgJpZBgB" + "IAEoDxISCgplZmZlY3ROYW1lGAIgASgJEhUKDWlzSGVyb1N1cHBvcnQYAyAB" + "KAgSFgoOaXNDaGFyYWN0ZXJPYmoYBCABKAgSFAoMaXNGb2xsb3dGbGlwGAUg" + "ASgIIq8BCg9FZmZlY3RDb25maWd1cmUSIwoFSW5mb3MYASADKAsyFC5FZmZl" + "Y3RJbmZvQ29uZmlndXJlEjAKCEluZm9EaWN0GAIgAygLMh4uRWZmZWN0Q29u" + "ZmlndXJlLkluZm9EaWN0RW50cnkaRQoNSW5mb0RpY3RFbnRyeRILCgNrZXkY" + "ASABKA8SIwoFdmFsdWUYAiABKAsyFC5FZmZlY3RJbmZvQ29uZmlndXJlOgI4" + "AWIGcHJvdG8z"), new FileDescriptor[0], new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[2]
		{
			new GeneratedClrTypeInfo(typeof(EffectInfoConfigure), EffectInfoConfigure.Parser, new string[5] { "Id", "EffectName", "IsHeroSupport", "IsCharacterObj", "IsFollowFlip" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(EffectConfigure), EffectConfigure.Parser, new string[2] { "Infos", "InfoDict" }, null, null, null, new GeneratedClrTypeInfo[1])
		}));
	}
}
