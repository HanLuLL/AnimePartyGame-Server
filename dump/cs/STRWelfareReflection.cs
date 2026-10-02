using System;
using Google.Protobuf.Reflection;

public static class STRWelfareReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static STRWelfareReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("ChBTVFJXZWxmYXJlLnByb3RvIoIBChhTVFJXZWxmYXJlTG9jYWxDb25maWd1" + "cmUSCgoCaWQYASABKA8SEgoKc2ltcGxpZmllZBgCIAEoCRIPCgdlbmdsaXNo" + "GAMgASgJEhAKCGphcGFuZXNlGAQgASgJEhMKC3RyYWRpdGlvbmFsGAUgASgJ" + "Eg4KBmtvcmVhbhgGIAEoCSLFAQoTU1RSV2VsZmFyZUNvbmZpZ3VyZRIpCgZM" + "b2NhbHMYASADKAsyGS5TVFJXZWxmYXJlTG9jYWxDb25maWd1cmUSNgoJTG9j" + "YWxEaWN0GAIgAygLMiMuU1RSV2VsZmFyZUNvbmZpZ3VyZS5Mb2NhbERpY3RF" + "bnRyeRpLCg5Mb2NhbERpY3RFbnRyeRILCgNrZXkYASABKA8SKAoFdmFsdWUY" + "AiABKAsyGS5TVFJXZWxmYXJlTG9jYWxDb25maWd1cmU6AjgBYgZwcm90bzM="), new FileDescriptor[0], new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[2]
		{
			new GeneratedClrTypeInfo(typeof(STRWelfareLocalConfigure), STRWelfareLocalConfigure.Parser, new string[6] { "Id", "Simplified", "English", "Japanese", "Traditional", "Korean" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(STRWelfareConfigure), STRWelfareConfigure.Parser, new string[2] { "Locals", "LocalDict" }, null, null, null, new GeneratedClrTypeInfo[1])
		}));
	}
}
