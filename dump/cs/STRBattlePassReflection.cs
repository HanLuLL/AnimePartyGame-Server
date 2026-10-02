using System;
using Google.Protobuf.Reflection;

public static class STRBattlePassReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static STRBattlePassReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("ChNTVFJCYXR0bGVQYXNzLnByb3RvIoUBChtTVFJCYXR0bGVQYXNzTG9jYWxD" + "b25maWd1cmUSCgoCaWQYASABKA8SEgoKc2ltcGxpZmllZBgCIAEoCRIPCgdl" + "bmdsaXNoGAMgASgJEhAKCGphcGFuZXNlGAQgASgJEhMKC3RyYWRpdGlvbmFs" + "GAUgASgJEg4KBmtvcmVhbhgGIAEoCSLRAQoWU1RSQmF0dGxlUGFzc0NvbmZp" + "Z3VyZRIsCgZMb2NhbHMYASADKAsyHC5TVFJCYXR0bGVQYXNzTG9jYWxDb25m" + "aWd1cmUSOQoJTG9jYWxEaWN0GAIgAygLMiYuU1RSQmF0dGxlUGFzc0NvbmZp" + "Z3VyZS5Mb2NhbERpY3RFbnRyeRpOCg5Mb2NhbERpY3RFbnRyeRILCgNrZXkY" + "ASABKA8SKwoFdmFsdWUYAiABKAsyHC5TVFJCYXR0bGVQYXNzTG9jYWxDb25m" + "aWd1cmU6AjgBYgZwcm90bzM="), new FileDescriptor[0], new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[2]
		{
			new GeneratedClrTypeInfo(typeof(STRBattlePassLocalConfigure), STRBattlePassLocalConfigure.Parser, new string[6] { "Id", "Simplified", "English", "Japanese", "Traditional", "Korean" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(STRBattlePassConfigure), STRBattlePassConfigure.Parser, new string[2] { "Locals", "LocalDict" }, null, null, null, new GeneratedClrTypeInfo[1])
		}));
	}
}
