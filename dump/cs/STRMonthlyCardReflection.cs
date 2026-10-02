using System;
using Google.Protobuf.Reflection;

public static class STRMonthlyCardReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static STRMonthlyCardReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("ChRTVFJNb250aGx5Q2FyZC5wcm90byKGAQocU1RSTW9udGhseUNhcmRMb2Nh" + "bENvbmZpZ3VyZRIKCgJpZBgBIAEoDxISCgpzaW1wbGlmaWVkGAIgASgJEg8K" + "B2VuZ2xpc2gYAyABKAkSEAoIamFwYW5lc2UYBCABKAkSEwoLdHJhZGl0aW9u" + "YWwYBSABKAkSDgoGa29yZWFuGAYgASgJItUBChdTVFJNb250aGx5Q2FyZENv" + "bmZpZ3VyZRItCgZMb2NhbHMYASADKAsyHS5TVFJNb250aGx5Q2FyZExvY2Fs" + "Q29uZmlndXJlEjoKCUxvY2FsRGljdBgCIAMoCzInLlNUUk1vbnRobHlDYXJk" + "Q29uZmlndXJlLkxvY2FsRGljdEVudHJ5Gk8KDkxvY2FsRGljdEVudHJ5EgsK" + "A2tleRgBIAEoDxIsCgV2YWx1ZRgCIAEoCzIdLlNUUk1vbnRobHlDYXJkTG9j" + "YWxDb25maWd1cmU6AjgBYgZwcm90bzM="), new FileDescriptor[0], new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[2]
		{
			new GeneratedClrTypeInfo(typeof(STRMonthlyCardLocalConfigure), STRMonthlyCardLocalConfigure.Parser, new string[6] { "Id", "Simplified", "English", "Japanese", "Traditional", "Korean" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(STRMonthlyCardConfigure), STRMonthlyCardConfigure.Parser, new string[2] { "Locals", "LocalDict" }, null, null, null, new GeneratedClrTypeInfo[1])
		}));
	}
}
