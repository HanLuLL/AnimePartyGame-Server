using System;
using Google.Protobuf.Reflection;

public static class STRMatchReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static STRMatchReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("Cg5TVFJNYXRjaC5wcm90byKAAQoWU1RSTWF0Y2hMb2NhbENvbmZpZ3VyZRIK" + "CgJpZBgBIAEoDxISCgpzaW1wbGlmaWVkGAIgASgJEg8KB2VuZ2xpc2gYAyAB" + "KAkSEAoIamFwYW5lc2UYBCABKAkSEwoLdHJhZGl0aW9uYWwYBSABKAkSDgoG" + "a29yZWFuGAYgASgJIr0BChFTVFJNYXRjaENvbmZpZ3VyZRInCgZMb2NhbHMY" + "ASADKAsyFy5TVFJNYXRjaExvY2FsQ29uZmlndXJlEjQKCUxvY2FsRGljdBgC" + "IAMoCzIhLlNUUk1hdGNoQ29uZmlndXJlLkxvY2FsRGljdEVudHJ5GkkKDkxv" + "Y2FsRGljdEVudHJ5EgsKA2tleRgBIAEoDxImCgV2YWx1ZRgCIAEoCzIXLlNU" + "Uk1hdGNoTG9jYWxDb25maWd1cmU6AjgBYgZwcm90bzM="), new FileDescriptor[0], new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[2]
		{
			new GeneratedClrTypeInfo(typeof(STRMatchLocalConfigure), STRMatchLocalConfigure.Parser, new string[6] { "Id", "Simplified", "English", "Japanese", "Traditional", "Korean" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(STRMatchConfigure), STRMatchConfigure.Parser, new string[2] { "Locals", "LocalDict" }, null, null, null, new GeneratedClrTypeInfo[1])
		}));
	}
}
