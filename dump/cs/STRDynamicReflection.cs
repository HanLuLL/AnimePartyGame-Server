using System;
using Google.Protobuf.Reflection;

public static class STRDynamicReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static STRDynamicReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("ChBTVFJEeW5hbWljLnByb3RvIoIBChhTVFJEeW5hbWljTG9jYWxDb25maWd1" + "cmUSCgoCaWQYASABKA8SEgoKc2ltcGxpZmllZBgCIAEoCRIPCgdlbmdsaXNo" + "GAMgASgJEhAKCGphcGFuZXNlGAQgASgJEhMKC3RyYWRpdGlvbmFsGAUgASgJ" + "Eg4KBmtvcmVhbhgGIAEoCSLFAQoTU1RSRHluYW1pY0NvbmZpZ3VyZRIpCgZM" + "b2NhbHMYASADKAsyGS5TVFJEeW5hbWljTG9jYWxDb25maWd1cmUSNgoJTG9j" + "YWxEaWN0GAIgAygLMiMuU1RSRHluYW1pY0NvbmZpZ3VyZS5Mb2NhbERpY3RF" + "bnRyeRpLCg5Mb2NhbERpY3RFbnRyeRILCgNrZXkYASABKA8SKAoFdmFsdWUY" + "AiABKAsyGS5TVFJEeW5hbWljTG9jYWxDb25maWd1cmU6AjgBYgZwcm90bzM="), new FileDescriptor[0], new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[2]
		{
			new GeneratedClrTypeInfo(typeof(STRDynamicLocalConfigure), STRDynamicLocalConfigure.Parser, new string[6] { "Id", "Simplified", "English", "Japanese", "Traditional", "Korean" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(STRDynamicConfigure), STRDynamicConfigure.Parser, new string[2] { "Locals", "LocalDict" }, null, null, null, new GeneratedClrTypeInfo[1])
		}));
	}
}
