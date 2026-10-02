using System;
using Google.Protobuf.Reflection;

public static class STRExchangeStoreReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static STRExchangeStoreReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("ChZTVFJFeGNoYW5nZVN0b3JlLnByb3RvIogBCh5TVFJFeGNoYW5nZVN0b3Jl" + "TG9jYWxDb25maWd1cmUSCgoCaWQYASABKA8SEgoKc2ltcGxpZmllZBgCIAEo" + "CRIPCgdlbmdsaXNoGAMgASgJEhAKCGphcGFuZXNlGAQgASgJEhMKC3RyYWRp" + "dGlvbmFsGAUgASgJEg4KBmtvcmVhbhgGIAEoCSLdAQoZU1RSRXhjaGFuZ2VT" + "dG9yZUNvbmZpZ3VyZRIvCgZMb2NhbHMYASADKAsyHy5TVFJFeGNoYW5nZVN0" + "b3JlTG9jYWxDb25maWd1cmUSPAoJTG9jYWxEaWN0GAIgAygLMikuU1RSRXhj" + "aGFuZ2VTdG9yZUNvbmZpZ3VyZS5Mb2NhbERpY3RFbnRyeRpRCg5Mb2NhbERp" + "Y3RFbnRyeRILCgNrZXkYASABKA8SLgoFdmFsdWUYAiABKAsyHy5TVFJFeGNo" + "YW5nZVN0b3JlTG9jYWxDb25maWd1cmU6AjgBYgZwcm90bzM="), new FileDescriptor[0], new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[2]
		{
			new GeneratedClrTypeInfo(typeof(STRExchangeStoreLocalConfigure), STRExchangeStoreLocalConfigure.Parser, new string[6] { "Id", "Simplified", "English", "Japanese", "Traditional", "Korean" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(STRExchangeStoreConfigure), STRExchangeStoreConfigure.Parser, new string[2] { "Locals", "LocalDict" }, null, null, null, new GeneratedClrTypeInfo[1])
		}));
	}
}
