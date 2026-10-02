using System;
using Google.Protobuf.Reflection;

public static class STREventReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static STREventReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("Cg5TVFJFdmVudC5wcm90byKAAQoWU1RSRXZlbnRMb2NhbENvbmZpZ3VyZRIK" + "CgJpZBgBIAEoDxISCgpzaW1wbGlmaWVkGAIgASgJEg8KB2VuZ2xpc2gYAyAB" + "KAkSEAoIamFwYW5lc2UYBCABKAkSEwoLdHJhZGl0aW9uYWwYBSABKAkSDgoG" + "a29yZWFuGAYgASgJIr0BChFTVFJFdmVudENvbmZpZ3VyZRInCgZMb2NhbHMY" + "ASADKAsyFy5TVFJFdmVudExvY2FsQ29uZmlndXJlEjQKCUxvY2FsRGljdBgC" + "IAMoCzIhLlNUUkV2ZW50Q29uZmlndXJlLkxvY2FsRGljdEVudHJ5GkkKDkxv" + "Y2FsRGljdEVudHJ5EgsKA2tleRgBIAEoDxImCgV2YWx1ZRgCIAEoCzIXLlNU" + "UkV2ZW50TG9jYWxDb25maWd1cmU6AjgBYgZwcm90bzM="), new FileDescriptor[0], new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[2]
		{
			new GeneratedClrTypeInfo(typeof(STREventLocalConfigure), STREventLocalConfigure.Parser, new string[6] { "Id", "Simplified", "English", "Japanese", "Traditional", "Korean" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(STREventConfigure), STREventConfigure.Parser, new string[2] { "Locals", "LocalDict" }, null, null, null, new GeneratedClrTypeInfo[1])
		}));
	}
}
