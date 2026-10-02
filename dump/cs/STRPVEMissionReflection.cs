using System;
using Google.Protobuf.Reflection;

public static class STRPVEMissionReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static STRPVEMissionReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("ChNTVFJQVkVNaXNzaW9uLnByb3RvIoUBChtTVFJQVkVNaXNzaW9uTG9jYWxD" + "b25maWd1cmUSCgoCaWQYASABKA8SEgoKc2ltcGxpZmllZBgCIAEoCRIPCgdl" + "bmdsaXNoGAMgASgJEhAKCGphcGFuZXNlGAQgASgJEhMKC3RyYWRpdGlvbmFs" + "GAUgASgJEg4KBmtvcmVhbhgGIAEoCSLRAQoWU1RSUFZFTWlzc2lvbkNvbmZp" + "Z3VyZRIsCgZMb2NhbHMYASADKAsyHC5TVFJQVkVNaXNzaW9uTG9jYWxDb25m" + "aWd1cmUSOQoJTG9jYWxEaWN0GAIgAygLMiYuU1RSUFZFTWlzc2lvbkNvbmZp" + "Z3VyZS5Mb2NhbERpY3RFbnRyeRpOCg5Mb2NhbERpY3RFbnRyeRILCgNrZXkY" + "ASABKA8SKwoFdmFsdWUYAiABKAsyHC5TVFJQVkVNaXNzaW9uTG9jYWxDb25m" + "aWd1cmU6AjgBYgZwcm90bzM="), new FileDescriptor[0], new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[2]
		{
			new GeneratedClrTypeInfo(typeof(STRPVEMissionLocalConfigure), STRPVEMissionLocalConfigure.Parser, new string[6] { "Id", "Simplified", "English", "Japanese", "Traditional", "Korean" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(STRPVEMissionConfigure), STRPVEMissionConfigure.Parser, new string[2] { "Locals", "LocalDict" }, null, null, null, new GeneratedClrTypeInfo[1])
		}));
	}
}
