using System;
using Google.Protobuf.Reflection;

public static class STRTutorialReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static STRTutorialReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("ChFTVFJUdXRvcmlhbC5wcm90byKDAQoZU1RSVHV0b3JpYWxMb2NhbENvbmZp" + "Z3VyZRIKCgJpZBgBIAEoDxISCgpzaW1wbGlmaWVkGAIgASgJEg8KB2VuZ2xp" + "c2gYAyABKAkSEAoIamFwYW5lc2UYBCABKAkSEwoLdHJhZGl0aW9uYWwYBSAB" + "KAkSDgoGa29yZWFuGAYgASgJIskBChRTVFJUdXRvcmlhbENvbmZpZ3VyZRIq" + "CgZMb2NhbHMYASADKAsyGi5TVFJUdXRvcmlhbExvY2FsQ29uZmlndXJlEjcK" + "CUxvY2FsRGljdBgCIAMoCzIkLlNUUlR1dG9yaWFsQ29uZmlndXJlLkxvY2Fs" + "RGljdEVudHJ5GkwKDkxvY2FsRGljdEVudHJ5EgsKA2tleRgBIAEoDxIpCgV2" + "YWx1ZRgCIAEoCzIaLlNUUlR1dG9yaWFsTG9jYWxDb25maWd1cmU6AjgBYgZw" + "cm90bzM="), new FileDescriptor[0], new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[2]
		{
			new GeneratedClrTypeInfo(typeof(STRTutorialLocalConfigure), STRTutorialLocalConfigure.Parser, new string[6] { "Id", "Simplified", "English", "Japanese", "Traditional", "Korean" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(STRTutorialConfigure), STRTutorialConfigure.Parser, new string[2] { "Locals", "LocalDict" }, null, null, null, new GeneratedClrTypeInfo[1])
		}));
	}
}
