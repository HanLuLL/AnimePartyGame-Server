using System;
using Google.Protobuf.Reflection;

public static class STRAcquisitionReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static STRAcquisitionReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("ChRTVFJBY3F1aXNpdGlvbi5wcm90byKGAQocU1RSQWNxdWlzaXRpb25Mb2Nh" + "bENvbmZpZ3VyZRIKCgJpZBgBIAEoDxISCgpzaW1wbGlmaWVkGAIgASgJEg8K" + "B2VuZ2xpc2gYAyABKAkSEAoIamFwYW5lc2UYBCABKAkSEwoLdHJhZGl0aW9u" + "YWwYBSABKAkSDgoGa29yZWFuGAYgASgJItUBChdTVFJBY3F1aXNpdGlvbkNv" + "bmZpZ3VyZRItCgZMb2NhbHMYASADKAsyHS5TVFJBY3F1aXNpdGlvbkxvY2Fs" + "Q29uZmlndXJlEjoKCUxvY2FsRGljdBgCIAMoCzInLlNUUkFjcXVpc2l0aW9u" + "Q29uZmlndXJlLkxvY2FsRGljdEVudHJ5Gk8KDkxvY2FsRGljdEVudHJ5EgsK" + "A2tleRgBIAEoDxIsCgV2YWx1ZRgCIAEoCzIdLlNUUkFjcXVpc2l0aW9uTG9j" + "YWxDb25maWd1cmU6AjgBYgZwcm90bzM="), new FileDescriptor[0], new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[2]
		{
			new GeneratedClrTypeInfo(typeof(STRAcquisitionLocalConfigure), STRAcquisitionLocalConfigure.Parser, new string[6] { "Id", "Simplified", "English", "Japanese", "Traditional", "Korean" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(STRAcquisitionConfigure), STRAcquisitionConfigure.Parser, new string[2] { "Locals", "LocalDict" }, null, null, null, new GeneratedClrTypeInfo[1])
		}));
	}
}
