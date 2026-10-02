using System;
using Google.Protobuf.Reflection;

public static class STRDivinationReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static STRDivinationReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("ChNTVFJEaXZpbmF0aW9uLnByb3RvIoUBChtTVFJEaXZpbmF0aW9uTG9jYWxD" + "b25maWd1cmUSCgoCaWQYASABKA8SEgoKc2ltcGxpZmllZBgCIAEoCRIPCgdl" + "bmdsaXNoGAMgASgJEhAKCGphcGFuZXNlGAQgASgJEhMKC3RyYWRpdGlvbmFs" + "GAUgASgJEg4KBmtvcmVhbhgGIAEoCSLRAQoWU1RSRGl2aW5hdGlvbkNvbmZp" + "Z3VyZRIsCgZMb2NhbHMYASADKAsyHC5TVFJEaXZpbmF0aW9uTG9jYWxDb25m" + "aWd1cmUSOQoJTG9jYWxEaWN0GAIgAygLMiYuU1RSRGl2aW5hdGlvbkNvbmZp" + "Z3VyZS5Mb2NhbERpY3RFbnRyeRpOCg5Mb2NhbERpY3RFbnRyeRILCgNrZXkY" + "ASABKA8SKwoFdmFsdWUYAiABKAsyHC5TVFJEaXZpbmF0aW9uTG9jYWxDb25m" + "aWd1cmU6AjgBYgZwcm90bzM="), new FileDescriptor[0], new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[2]
		{
			new GeneratedClrTypeInfo(typeof(STRDivinationLocalConfigure), STRDivinationLocalConfigure.Parser, new string[6] { "Id", "Simplified", "English", "Japanese", "Traditional", "Korean" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(STRDivinationConfigure), STRDivinationConfigure.Parser, new string[2] { "Locals", "LocalDict" }, null, null, null, new GeneratedClrTypeInfo[1])
		}));
	}
}
