using System;
using Google.Protobuf.Reflection;

public static class STRSpectateReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static STRSpectateReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("ChFTVFJTcGVjdGF0ZS5wcm90byKDAQoZU1RSU3BlY3RhdGVMb2NhbENvbmZp" + "Z3VyZRIKCgJpZBgBIAEoDxISCgpzaW1wbGlmaWVkGAIgASgJEg8KB2VuZ2xp" + "c2gYAyABKAkSEAoIamFwYW5lc2UYBCABKAkSEwoLdHJhZGl0aW9uYWwYBSAB" + "KAkSDgoGa29yZWFuGAYgASgJIskBChRTVFJTcGVjdGF0ZUNvbmZpZ3VyZRIq" + "CgZMb2NhbHMYASADKAsyGi5TVFJTcGVjdGF0ZUxvY2FsQ29uZmlndXJlEjcK" + "CUxvY2FsRGljdBgCIAMoCzIkLlNUUlNwZWN0YXRlQ29uZmlndXJlLkxvY2Fs" + "RGljdEVudHJ5GkwKDkxvY2FsRGljdEVudHJ5EgsKA2tleRgBIAEoDxIpCgV2" + "YWx1ZRgCIAEoCzIaLlNUUlNwZWN0YXRlTG9jYWxDb25maWd1cmU6AjgBYgZw" + "cm90bzM="), new FileDescriptor[0], new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[2]
		{
			new GeneratedClrTypeInfo(typeof(STRSpectateLocalConfigure), STRSpectateLocalConfigure.Parser, new string[6] { "Id", "Simplified", "English", "Japanese", "Traditional", "Korean" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(STRSpectateConfigure), STRSpectateConfigure.Parser, new string[2] { "Locals", "LocalDict" }, null, null, null, new GeneratedClrTypeInfo[1])
		}));
	}
}
