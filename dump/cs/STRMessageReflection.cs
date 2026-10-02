using System;
using Google.Protobuf.Reflection;

public static class STRMessageReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static STRMessageReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("ChBTVFJNZXNzYWdlLnByb3RvIoIBChhTVFJNZXNzYWdlTG9jYWxDb25maWd1" + "cmUSCgoCaWQYASABKA8SEgoKc2ltcGxpZmllZBgCIAEoCRIPCgdlbmdsaXNo" + "GAMgASgJEhAKCGphcGFuZXNlGAQgASgJEhMKC3RyYWRpdGlvbmFsGAUgASgJ" + "Eg4KBmtvcmVhbhgGIAEoCSLFAQoTU1RSTWVzc2FnZUNvbmZpZ3VyZRIpCgZM" + "b2NhbHMYASADKAsyGS5TVFJNZXNzYWdlTG9jYWxDb25maWd1cmUSNgoJTG9j" + "YWxEaWN0GAIgAygLMiMuU1RSTWVzc2FnZUNvbmZpZ3VyZS5Mb2NhbERpY3RF" + "bnRyeRpLCg5Mb2NhbERpY3RFbnRyeRILCgNrZXkYASABKA8SKAoFdmFsdWUY" + "AiABKAsyGS5TVFJNZXNzYWdlTG9jYWxDb25maWd1cmU6AjgBYgZwcm90bzM="), new FileDescriptor[0], new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[2]
		{
			new GeneratedClrTypeInfo(typeof(STRMessageLocalConfigure), STRMessageLocalConfigure.Parser, new string[6] { "Id", "Simplified", "English", "Japanese", "Traditional", "Korean" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(STRMessageConfigure), STRMessageConfigure.Parser, new string[2] { "Locals", "LocalDict" }, null, null, null, new GeneratedClrTypeInfo[1])
		}));
	}
}
