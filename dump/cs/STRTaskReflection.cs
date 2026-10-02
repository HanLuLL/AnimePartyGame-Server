using System;
using Google.Protobuf.Reflection;

public static class STRTaskReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static STRTaskReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("Cg1TVFJUYXNrLnByb3RvIn8KFVNUUlRhc2tMb2NhbENvbmZpZ3VyZRIKCgJp" + "ZBgBIAEoDxISCgpzaW1wbGlmaWVkGAIgASgJEg8KB2VuZ2xpc2gYAyABKAkS" + "EAoIamFwYW5lc2UYBCABKAkSEwoLdHJhZGl0aW9uYWwYBSABKAkSDgoGa29y" + "ZWFuGAYgASgJIrkBChBTVFJUYXNrQ29uZmlndXJlEiYKBkxvY2FscxgBIAMo" + "CzIWLlNUUlRhc2tMb2NhbENvbmZpZ3VyZRIzCglMb2NhbERpY3QYAiADKAsy" + "IC5TVFJUYXNrQ29uZmlndXJlLkxvY2FsRGljdEVudHJ5GkgKDkxvY2FsRGlj" + "dEVudHJ5EgsKA2tleRgBIAEoDxIlCgV2YWx1ZRgCIAEoCzIWLlNUUlRhc2tM" + "b2NhbENvbmZpZ3VyZToCOAFiBnByb3RvMw=="), new FileDescriptor[0], new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[2]
		{
			new GeneratedClrTypeInfo(typeof(STRTaskLocalConfigure), STRTaskLocalConfigure.Parser, new string[6] { "Id", "Simplified", "English", "Japanese", "Traditional", "Korean" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(STRTaskConfigure), STRTaskConfigure.Parser, new string[2] { "Locals", "LocalDict" }, null, null, null, new GeneratedClrTypeInfo[1])
		}));
	}
}
