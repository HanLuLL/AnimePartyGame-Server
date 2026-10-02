using System;
using Google.Protobuf.Reflection;

public static class STRPVENurturanceReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static STRPVENurturanceReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("ChZTVFJQVkVOdXJ0dXJhbmNlLnByb3RvIogBCh5TVFJQVkVOdXJ0dXJhbmNl" + "TG9jYWxDb25maWd1cmUSCgoCaWQYASABKA8SEgoKc2ltcGxpZmllZBgCIAEo" + "CRIPCgdlbmdsaXNoGAMgASgJEhAKCGphcGFuZXNlGAQgASgJEhMKC3RyYWRp" + "dGlvbmFsGAUgASgJEg4KBmtvcmVhbhgGIAEoCSLdAQoZU1RSUFZFTnVydHVy" + "YW5jZUNvbmZpZ3VyZRIvCgZMb2NhbHMYASADKAsyHy5TVFJQVkVOdXJ0dXJh" + "bmNlTG9jYWxDb25maWd1cmUSPAoJTG9jYWxEaWN0GAIgAygLMikuU1RSUFZF" + "TnVydHVyYW5jZUNvbmZpZ3VyZS5Mb2NhbERpY3RFbnRyeRpRCg5Mb2NhbERp" + "Y3RFbnRyeRILCgNrZXkYASABKA8SLgoFdmFsdWUYAiABKAsyHy5TVFJQVkVO" + "dXJ0dXJhbmNlTG9jYWxDb25maWd1cmU6AjgBYgZwcm90bzM="), new FileDescriptor[0], new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[2]
		{
			new GeneratedClrTypeInfo(typeof(STRPVENurturanceLocalConfigure), STRPVENurturanceLocalConfigure.Parser, new string[6] { "Id", "Simplified", "English", "Japanese", "Traditional", "Korean" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(STRPVENurturanceConfigure), STRPVENurturanceConfigure.Parser, new string[2] { "Locals", "LocalDict" }, null, null, null, new GeneratedClrTypeInfo[1])
		}));
	}
}
