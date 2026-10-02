using System;
using Google.Protobuf.Reflection;

public static class STRChatReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static STRChatReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("Cg1TVFJDaGF0LnByb3RvIn8KFVNUUkNoYXRMb2NhbENvbmZpZ3VyZRIKCgJp" + "ZBgBIAEoDxISCgpzaW1wbGlmaWVkGAIgASgJEg8KB2VuZ2xpc2gYAyABKAkS" + "EAoIamFwYW5lc2UYBCABKAkSEwoLdHJhZGl0aW9uYWwYBSABKAkSDgoGa29y" + "ZWFuGAYgASgJIrkBChBTVFJDaGF0Q29uZmlndXJlEiYKBkxvY2FscxgBIAMo" + "CzIWLlNUUkNoYXRMb2NhbENvbmZpZ3VyZRIzCglMb2NhbERpY3QYAiADKAsy" + "IC5TVFJDaGF0Q29uZmlndXJlLkxvY2FsRGljdEVudHJ5GkgKDkxvY2FsRGlj" + "dEVudHJ5EgsKA2tleRgBIAEoDxIlCgV2YWx1ZRgCIAEoCzIWLlNUUkNoYXRM" + "b2NhbENvbmZpZ3VyZToCOAFiBnByb3RvMw=="), new FileDescriptor[0], new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[2]
		{
			new GeneratedClrTypeInfo(typeof(STRChatLocalConfigure), STRChatLocalConfigure.Parser, new string[6] { "Id", "Simplified", "English", "Japanese", "Traditional", "Korean" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(STRChatConfigure), STRChatConfigure.Parser, new string[2] { "Locals", "LocalDict" }, null, null, null, new GeneratedClrTypeInfo[1])
		}));
	}
}
