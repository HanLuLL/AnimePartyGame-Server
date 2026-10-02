using System;
using Google.Protobuf.Reflection;

public static class STRBuffReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static STRBuffReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("Cg1TVFJCdWZmLnByb3RvIn8KFVNUUkJ1ZmZMb2NhbENvbmZpZ3VyZRIKCgJp" + "ZBgBIAEoDxISCgpzaW1wbGlmaWVkGAIgASgJEg8KB2VuZ2xpc2gYAyABKAkS" + "EAoIamFwYW5lc2UYBCABKAkSEwoLdHJhZGl0aW9uYWwYBSABKAkSDgoGa29y" + "ZWFuGAYgASgJIrkBChBTVFJCdWZmQ29uZmlndXJlEiYKBkxvY2FscxgBIAMo" + "CzIWLlNUUkJ1ZmZMb2NhbENvbmZpZ3VyZRIzCglMb2NhbERpY3QYAiADKAsy" + "IC5TVFJCdWZmQ29uZmlndXJlLkxvY2FsRGljdEVudHJ5GkgKDkxvY2FsRGlj" + "dEVudHJ5EgsKA2tleRgBIAEoDxIlCgV2YWx1ZRgCIAEoCzIWLlNUUkJ1ZmZM" + "b2NhbENvbmZpZ3VyZToCOAFiBnByb3RvMw=="), new FileDescriptor[0], new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[2]
		{
			new GeneratedClrTypeInfo(typeof(STRBuffLocalConfigure), STRBuffLocalConfigure.Parser, new string[6] { "Id", "Simplified", "English", "Japanese", "Traditional", "Korean" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(STRBuffConfigure), STRBuffConfigure.Parser, new string[2] { "Locals", "LocalDict" }, null, null, null, new GeneratedClrTypeInfo[1])
		}));
	}
}
