using System;
using Google.Protobuf.Reflection;

public static class STRActivityReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static STRActivityReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("ChFTVFJBY3Rpdml0eS5wcm90byKDAQoZU1RSQWN0aXZpdHlMb2NhbENvbmZp" + "Z3VyZRIKCgJpZBgBIAEoDxISCgpzaW1wbGlmaWVkGAIgASgJEg8KB2VuZ2xp" + "c2gYAyABKAkSEAoIamFwYW5lc2UYBCABKAkSEwoLdHJhZGl0aW9uYWwYBSAB" + "KAkSDgoGa29yZWFuGAYgASgJIskBChRTVFJBY3Rpdml0eUNvbmZpZ3VyZRIq" + "CgZMb2NhbHMYASADKAsyGi5TVFJBY3Rpdml0eUxvY2FsQ29uZmlndXJlEjcK" + "CUxvY2FsRGljdBgCIAMoCzIkLlNUUkFjdGl2aXR5Q29uZmlndXJlLkxvY2Fs" + "RGljdEVudHJ5GkwKDkxvY2FsRGljdEVudHJ5EgsKA2tleRgBIAEoDxIpCgV2" + "YWx1ZRgCIAEoCzIaLlNUUkFjdGl2aXR5TG9jYWxDb25maWd1cmU6AjgBYgZw" + "cm90bzM="), new FileDescriptor[0], new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[2]
		{
			new GeneratedClrTypeInfo(typeof(STRActivityLocalConfigure), STRActivityLocalConfigure.Parser, new string[6] { "Id", "Simplified", "English", "Japanese", "Traditional", "Korean" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(STRActivityConfigure), STRActivityConfigure.Parser, new string[2] { "Locals", "LocalDict" }, null, null, null, new GeneratedClrTypeInfo[1])
		}));
	}
}
