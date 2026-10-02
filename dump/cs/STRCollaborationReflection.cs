using System;
using Google.Protobuf.Reflection;

public static class STRCollaborationReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static STRCollaborationReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("ChZTVFJDb2xsYWJvcmF0aW9uLnByb3RvIogBCh5TVFJDb2xsYWJvcmF0aW9u" + "TG9jYWxDb25maWd1cmUSCgoCaWQYASABKA8SEgoKc2ltcGxpZmllZBgCIAEo" + "CRIPCgdlbmdsaXNoGAMgASgJEhAKCGphcGFuZXNlGAQgASgJEhMKC3RyYWRp" + "dGlvbmFsGAUgASgJEg4KBmtvcmVhbhgGIAEoCSLdAQoZU1RSQ29sbGFib3Jh" + "dGlvbkNvbmZpZ3VyZRIvCgZMb2NhbHMYASADKAsyHy5TVFJDb2xsYWJvcmF0" + "aW9uTG9jYWxDb25maWd1cmUSPAoJTG9jYWxEaWN0GAIgAygLMikuU1RSQ29s" + "bGFib3JhdGlvbkNvbmZpZ3VyZS5Mb2NhbERpY3RFbnRyeRpRCg5Mb2NhbERp" + "Y3RFbnRyeRILCgNrZXkYASABKA8SLgoFdmFsdWUYAiABKAsyHy5TVFJDb2xs" + "YWJvcmF0aW9uTG9jYWxDb25maWd1cmU6AjgBYgZwcm90bzM="), new FileDescriptor[0], new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[2]
		{
			new GeneratedClrTypeInfo(typeof(STRCollaborationLocalConfigure), STRCollaborationLocalConfigure.Parser, new string[6] { "Id", "Simplified", "English", "Japanese", "Traditional", "Korean" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(STRCollaborationConfigure), STRCollaborationConfigure.Parser, new string[2] { "Locals", "LocalDict" }, null, null, null, new GeneratedClrTypeInfo[1])
		}));
	}
}
