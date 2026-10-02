using System;
using Google.Protobuf.Reflection;

public static class STRChestReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static STRChestReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("Cg5TVFJDaGVzdC5wcm90byKAAQoWU1RSQ2hlc3RMb2NhbENvbmZpZ3VyZRIK" + "CgJpZBgBIAEoDxISCgpzaW1wbGlmaWVkGAIgASgJEg8KB2VuZ2xpc2gYAyAB" + "KAkSEAoIamFwYW5lc2UYBCABKAkSEwoLdHJhZGl0aW9uYWwYBSABKAkSDgoG" + "a29yZWFuGAYgASgJIr0BChFTVFJDaGVzdENvbmZpZ3VyZRInCgZMb2NhbHMY" + "ASADKAsyFy5TVFJDaGVzdExvY2FsQ29uZmlndXJlEjQKCUxvY2FsRGljdBgC" + "IAMoCzIhLlNUUkNoZXN0Q29uZmlndXJlLkxvY2FsRGljdEVudHJ5GkkKDkxv" + "Y2FsRGljdEVudHJ5EgsKA2tleRgBIAEoDxImCgV2YWx1ZRgCIAEoCzIXLlNU" + "UkNoZXN0TG9jYWxDb25maWd1cmU6AjgBYgZwcm90bzM="), new FileDescriptor[0], new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[2]
		{
			new GeneratedClrTypeInfo(typeof(STRChestLocalConfigure), STRChestLocalConfigure.Parser, new string[6] { "Id", "Simplified", "English", "Japanese", "Traditional", "Korean" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(STRChestConfigure), STRChestConfigure.Parser, new string[2] { "Locals", "LocalDict" }, null, null, null, new GeneratedClrTypeInfo[1])
		}));
	}
}
