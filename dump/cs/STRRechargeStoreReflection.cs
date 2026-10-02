using System;
using Google.Protobuf.Reflection;

public static class STRRechargeStoreReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static STRRechargeStoreReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("ChZTVFJSZWNoYXJnZVN0b3JlLnByb3RvIogBCh5TVFJSZWNoYXJnZVN0b3Jl" + "TG9jYWxDb25maWd1cmUSCgoCaWQYASABKA8SEgoKc2ltcGxpZmllZBgCIAEo" + "CRIPCgdlbmdsaXNoGAMgASgJEhAKCGphcGFuZXNlGAQgASgJEhMKC3RyYWRp" + "dGlvbmFsGAUgASgJEg4KBmtvcmVhbhgGIAEoCSLdAQoZU1RSUmVjaGFyZ2VT" + "dG9yZUNvbmZpZ3VyZRIvCgZMb2NhbHMYASADKAsyHy5TVFJSZWNoYXJnZVN0" + "b3JlTG9jYWxDb25maWd1cmUSPAoJTG9jYWxEaWN0GAIgAygLMikuU1RSUmVj" + "aGFyZ2VTdG9yZUNvbmZpZ3VyZS5Mb2NhbERpY3RFbnRyeRpRCg5Mb2NhbERp" + "Y3RFbnRyeRILCgNrZXkYASABKA8SLgoFdmFsdWUYAiABKAsyHy5TVFJSZWNo" + "YXJnZVN0b3JlTG9jYWxDb25maWd1cmU6AjgBYgZwcm90bzM="), new FileDescriptor[0], new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[2]
		{
			new GeneratedClrTypeInfo(typeof(STRRechargeStoreLocalConfigure), STRRechargeStoreLocalConfigure.Parser, new string[6] { "Id", "Simplified", "English", "Japanese", "Traditional", "Korean" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(STRRechargeStoreConfigure), STRRechargeStoreConfigure.Parser, new string[2] { "Locals", "LocalDict" }, null, null, null, new GeneratedClrTypeInfo[1])
		}));
	}
}
