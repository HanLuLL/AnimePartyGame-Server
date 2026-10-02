using System;
using Google.Protobuf.Reflection;

public static class STRLandReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static STRLandReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("Cg1TVFJMYW5kLnByb3RvIn8KFVNUUkxhbmRMb2NhbENvbmZpZ3VyZRIKCgJp" + "ZBgBIAEoDxISCgpzaW1wbGlmaWVkGAIgASgJEg8KB2VuZ2xpc2gYAyABKAkS" + "EAoIamFwYW5lc2UYBCABKAkSEwoLdHJhZGl0aW9uYWwYBSABKAkSDgoGa29y" + "ZWFuGAYgASgJIrkBChBTVFJMYW5kQ29uZmlndXJlEiYKBkxvY2FscxgBIAMo" + "CzIWLlNUUkxhbmRMb2NhbENvbmZpZ3VyZRIzCglMb2NhbERpY3QYAiADKAsy" + "IC5TVFJMYW5kQ29uZmlndXJlLkxvY2FsRGljdEVudHJ5GkgKDkxvY2FsRGlj" + "dEVudHJ5EgsKA2tleRgBIAEoDxIlCgV2YWx1ZRgCIAEoCzIWLlNUUkxhbmRM" + "b2NhbENvbmZpZ3VyZToCOAFiBnByb3RvMw=="), new FileDescriptor[0], new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[2]
		{
			new GeneratedClrTypeInfo(typeof(STRLandLocalConfigure), STRLandLocalConfigure.Parser, new string[6] { "Id", "Simplified", "English", "Japanese", "Traditional", "Korean" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(STRLandConfigure), STRLandConfigure.Parser, new string[2] { "Locals", "LocalDict" }, null, null, null, new GeneratedClrTypeInfo[1])
		}));
	}
}
