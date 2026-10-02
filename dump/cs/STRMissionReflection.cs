using System;
using Google.Protobuf.Reflection;

public static class STRMissionReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static STRMissionReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("ChBTVFJNaXNzaW9uLnByb3RvIoIBChhTVFJNaXNzaW9uTG9jYWxDb25maWd1" + "cmUSCgoCaWQYASABKA8SEgoKc2ltcGxpZmllZBgCIAEoCRIPCgdlbmdsaXNo" + "GAMgASgJEhAKCGphcGFuZXNlGAQgASgJEhMKC3RyYWRpdGlvbmFsGAUgASgJ" + "Eg4KBmtvcmVhbhgGIAEoCSLFAQoTU1RSTWlzc2lvbkNvbmZpZ3VyZRIpCgZM" + "b2NhbHMYASADKAsyGS5TVFJNaXNzaW9uTG9jYWxDb25maWd1cmUSNgoJTG9j" + "YWxEaWN0GAIgAygLMiMuU1RSTWlzc2lvbkNvbmZpZ3VyZS5Mb2NhbERpY3RF" + "bnRyeRpLCg5Mb2NhbERpY3RFbnRyeRILCgNrZXkYASABKA8SKAoFdmFsdWUY" + "AiABKAsyGS5TVFJNaXNzaW9uTG9jYWxDb25maWd1cmU6AjgBYgZwcm90bzM="), new FileDescriptor[0], new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[2]
		{
			new GeneratedClrTypeInfo(typeof(STRMissionLocalConfigure), STRMissionLocalConfigure.Parser, new string[6] { "Id", "Simplified", "English", "Japanese", "Traditional", "Korean" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(STRMissionConfigure), STRMissionConfigure.Parser, new string[2] { "Locals", "LocalDict" }, null, null, null, new GeneratedClrTypeInfo[1])
		}));
	}
}
