using System;
using Google.Protobuf.Reflection;

public static class RoundReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static RoundReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("CgtSb3VuZC5wcm90bxoKRW51bS5wcm90byJDChJSb3VuZEluZm9Db25maWd1" + "cmUSHQoJcm91bmRUeXBlGAEgASgOMgouUm91bmRUeXBlEg4KBnBhcmFtcxgC" + "IAMoESKrAQoOUm91bmRDb25maWd1cmUSIgoFSW5mb3MYASADKAsyEy5Sb3Vu" + "ZEluZm9Db25maWd1cmUSLwoISW5mb0RpY3QYAiADKAsyHS5Sb3VuZENvbmZp" + "Z3VyZS5JbmZvRGljdEVudHJ5GkQKDUluZm9EaWN0RW50cnkSCwoDa2V5GAEg" + "ASgPEiIKBXZhbHVlGAIgASgLMhMuUm91bmRJbmZvQ29uZmlndXJlOgI4AWIG" + "cHJvdG8z"), new FileDescriptor[1] { EnumReflection.Descriptor }, new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[2]
		{
			new GeneratedClrTypeInfo(typeof(RoundInfoConfigure), RoundInfoConfigure.Parser, new string[2] { "RoundType", "Params" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(RoundConfigure), RoundConfigure.Parser, new string[2] { "Infos", "InfoDict" }, null, null, null, new GeneratedClrTypeInfo[1])
		}));
	}
}
