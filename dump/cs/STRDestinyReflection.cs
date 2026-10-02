using System;
using Google.Protobuf.Reflection;

public static class STRDestinyReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static STRDestinyReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("ChBTVFJEZXN0aW55LnByb3RvIoIBChhTVFJEZXN0aW55TG9jYWxDb25maWd1" + "cmUSCgoCaWQYASABKA8SEgoKc2ltcGxpZmllZBgCIAEoCRIPCgdlbmdsaXNo" + "GAMgASgJEhAKCGphcGFuZXNlGAQgASgJEhMKC3RyYWRpdGlvbmFsGAUgASgJ" + "Eg4KBmtvcmVhbhgGIAEoCSLFAQoTU1RSRGVzdGlueUNvbmZpZ3VyZRIpCgZM" + "b2NhbHMYASADKAsyGS5TVFJEZXN0aW55TG9jYWxDb25maWd1cmUSNgoJTG9j" + "YWxEaWN0GAIgAygLMiMuU1RSRGVzdGlueUNvbmZpZ3VyZS5Mb2NhbERpY3RF" + "bnRyeRpLCg5Mb2NhbERpY3RFbnRyeRILCgNrZXkYASABKA8SKAoFdmFsdWUY" + "AiABKAsyGS5TVFJEZXN0aW55TG9jYWxDb25maWd1cmU6AjgBYgZwcm90bzM="), new FileDescriptor[0], new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[2]
		{
			new GeneratedClrTypeInfo(typeof(STRDestinyLocalConfigure), STRDestinyLocalConfigure.Parser, new string[6] { "Id", "Simplified", "English", "Japanese", "Traditional", "Korean" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(STRDestinyConfigure), STRDestinyConfigure.Parser, new string[2] { "Locals", "LocalDict" }, null, null, null, new GeneratedClrTypeInfo[1])
		}));
	}
}
