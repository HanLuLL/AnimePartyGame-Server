using System;
using Google.Protobuf.Reflection;

public static class STRGameModeReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static STRGameModeReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("ChFTVFJHYW1lTW9kZS5wcm90byKDAQoZU1RSR2FtZU1vZGVMb2NhbENvbmZp" + "Z3VyZRIKCgJpZBgBIAEoDxISCgpzaW1wbGlmaWVkGAIgASgJEg8KB2VuZ2xp" + "c2gYAyABKAkSEAoIamFwYW5lc2UYBCABKAkSEwoLdHJhZGl0aW9uYWwYBSAB" + "KAkSDgoGa29yZWFuGAYgASgJIskBChRTVFJHYW1lTW9kZUNvbmZpZ3VyZRIq" + "CgZMb2NhbHMYASADKAsyGi5TVFJHYW1lTW9kZUxvY2FsQ29uZmlndXJlEjcK" + "CUxvY2FsRGljdBgCIAMoCzIkLlNUUkdhbWVNb2RlQ29uZmlndXJlLkxvY2Fs" + "RGljdEVudHJ5GkwKDkxvY2FsRGljdEVudHJ5EgsKA2tleRgBIAEoDxIpCgV2" + "YWx1ZRgCIAEoCzIaLlNUUkdhbWVNb2RlTG9jYWxDb25maWd1cmU6AjgBYgZw" + "cm90bzM="), new FileDescriptor[0], new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[2]
		{
			new GeneratedClrTypeInfo(typeof(STRGameModeLocalConfigure), STRGameModeLocalConfigure.Parser, new string[6] { "Id", "Simplified", "English", "Japanese", "Traditional", "Korean" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(STRGameModeConfigure), STRGameModeConfigure.Parser, new string[2] { "Locals", "LocalDict" }, null, null, null, new GeneratedClrTypeInfo[1])
		}));
	}
}
