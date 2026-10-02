using System;
using Google.Protobuf.Reflection;

public static class STRMapEventReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static STRMapEventReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("ChFTVFJNYXBFdmVudC5wcm90byKDAQoZU1RSTWFwRXZlbnRMb2NhbENvbmZp" + "Z3VyZRIKCgJpZBgBIAEoDxISCgpzaW1wbGlmaWVkGAIgASgJEg8KB2VuZ2xp" + "c2gYAyABKAkSEAoIamFwYW5lc2UYBCABKAkSEwoLdHJhZGl0aW9uYWwYBSAB" + "KAkSDgoGa29yZWFuGAYgASgJIskBChRTVFJNYXBFdmVudENvbmZpZ3VyZRIq" + "CgZMb2NhbHMYASADKAsyGi5TVFJNYXBFdmVudExvY2FsQ29uZmlndXJlEjcK" + "CUxvY2FsRGljdBgCIAMoCzIkLlNUUk1hcEV2ZW50Q29uZmlndXJlLkxvY2Fs" + "RGljdEVudHJ5GkwKDkxvY2FsRGljdEVudHJ5EgsKA2tleRgBIAEoDxIpCgV2" + "YWx1ZRgCIAEoCzIaLlNUUk1hcEV2ZW50TG9jYWxDb25maWd1cmU6AjgBYgZw" + "cm90bzM="), new FileDescriptor[0], new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[2]
		{
			new GeneratedClrTypeInfo(typeof(STRMapEventLocalConfigure), STRMapEventLocalConfigure.Parser, new string[6] { "Id", "Simplified", "English", "Japanese", "Traditional", "Korean" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(STRMapEventConfigure), STRMapEventConfigure.Parser, new string[2] { "Locals", "LocalDict" }, null, null, null, new GeneratedClrTypeInfo[1])
		}));
	}
}
