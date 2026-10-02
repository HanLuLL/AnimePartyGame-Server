using System;
using Google.Protobuf.Reflection;

public static class STRSkinReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static STRSkinReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("Cg1TVFJTa2luLnByb3RvIn8KFVNUUlNraW5Mb2NhbENvbmZpZ3VyZRIKCgJp" + "ZBgBIAEoDxISCgpzaW1wbGlmaWVkGAIgASgJEg8KB2VuZ2xpc2gYAyABKAkS" + "EAoIamFwYW5lc2UYBCABKAkSEwoLdHJhZGl0aW9uYWwYBSABKAkSDgoGa29y" + "ZWFuGAYgASgJIrkBChBTVFJTa2luQ29uZmlndXJlEiYKBkxvY2FscxgBIAMo" + "CzIWLlNUUlNraW5Mb2NhbENvbmZpZ3VyZRIzCglMb2NhbERpY3QYAiADKAsy" + "IC5TVFJTa2luQ29uZmlndXJlLkxvY2FsRGljdEVudHJ5GkgKDkxvY2FsRGlj" + "dEVudHJ5EgsKA2tleRgBIAEoDxIlCgV2YWx1ZRgCIAEoCzIWLlNUUlNraW5M" + "b2NhbENvbmZpZ3VyZToCOAFiBnByb3RvMw=="), new FileDescriptor[0], new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[2]
		{
			new GeneratedClrTypeInfo(typeof(STRSkinLocalConfigure), STRSkinLocalConfigure.Parser, new string[6] { "Id", "Simplified", "English", "Japanese", "Traditional", "Korean" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(STRSkinConfigure), STRSkinConfigure.Parser, new string[2] { "Locals", "LocalDict" }, null, null, null, new GeneratedClrTypeInfo[1])
		}));
	}
}
