using System;
using Google.Protobuf.Reflection;

public static class STRSkillReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static STRSkillReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("Cg5TVFJTa2lsbC5wcm90byKAAQoWU1RSU2tpbGxMb2NhbENvbmZpZ3VyZRIK" + "CgJpZBgBIAEoDxISCgpzaW1wbGlmaWVkGAIgASgJEg8KB2VuZ2xpc2gYAyAB" + "KAkSEAoIamFwYW5lc2UYBCABKAkSEwoLdHJhZGl0aW9uYWwYBSABKAkSDgoG" + "a29yZWFuGAYgASgJIr0BChFTVFJTa2lsbENvbmZpZ3VyZRInCgZMb2NhbHMY" + "ASADKAsyFy5TVFJTa2lsbExvY2FsQ29uZmlndXJlEjQKCUxvY2FsRGljdBgC" + "IAMoCzIhLlNUUlNraWxsQ29uZmlndXJlLkxvY2FsRGljdEVudHJ5GkkKDkxv" + "Y2FsRGljdEVudHJ5EgsKA2tleRgBIAEoDxImCgV2YWx1ZRgCIAEoCzIXLlNU" + "UlNraWxsTG9jYWxDb25maWd1cmU6AjgBYgZwcm90bzM="), new FileDescriptor[0], new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[2]
		{
			new GeneratedClrTypeInfo(typeof(STRSkillLocalConfigure), STRSkillLocalConfigure.Parser, new string[6] { "Id", "Simplified", "English", "Japanese", "Traditional", "Korean" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(STRSkillConfigure), STRSkillConfigure.Parser, new string[2] { "Locals", "LocalDict" }, null, null, null, new GeneratedClrTypeInfo[1])
		}));
	}
}
