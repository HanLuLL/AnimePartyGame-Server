using System;
using Google.Protobuf.Reflection;

public static class STRStoryReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static STRStoryReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("Cg5TVFJTdG9yeS5wcm90byKAAQoWU1RSU3RvcnlMb2NhbENvbmZpZ3VyZRIK" + "CgJpZBgBIAEoDxISCgpzaW1wbGlmaWVkGAIgASgJEg8KB2VuZ2xpc2gYAyAB" + "KAkSEAoIamFwYW5lc2UYBCABKAkSEwoLdHJhZGl0aW9uYWwYBSABKAkSDgoG" + "a29yZWFuGAYgASgJIr0BChFTVFJTdG9yeUNvbmZpZ3VyZRInCgZMb2NhbHMY" + "ASADKAsyFy5TVFJTdG9yeUxvY2FsQ29uZmlndXJlEjQKCUxvY2FsRGljdBgC" + "IAMoCzIhLlNUUlN0b3J5Q29uZmlndXJlLkxvY2FsRGljdEVudHJ5GkkKDkxv" + "Y2FsRGljdEVudHJ5EgsKA2tleRgBIAEoDxImCgV2YWx1ZRgCIAEoCzIXLlNU" + "UlN0b3J5TG9jYWxDb25maWd1cmU6AjgBYgZwcm90bzM="), new FileDescriptor[0], new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[2]
		{
			new GeneratedClrTypeInfo(typeof(STRStoryLocalConfigure), STRStoryLocalConfigure.Parser, new string[6] { "Id", "Simplified", "English", "Japanese", "Traditional", "Korean" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(STRStoryConfigure), STRStoryConfigure.Parser, new string[2] { "Locals", "LocalDict" }, null, null, null, new GeneratedClrTypeInfo[1])
		}));
	}
}
