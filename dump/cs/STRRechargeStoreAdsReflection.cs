using System;
using Google.Protobuf.Reflection;

public static class STRRechargeStoreAdsReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static STRRechargeStoreAdsReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("ChlTVFJSZWNoYXJnZVN0b3JlQWRzLnByb3RvIosBCiFTVFJSZWNoYXJnZVN0" + "b3JlQWRzTG9jYWxDb25maWd1cmUSCgoCaWQYASABKA8SEgoKc2ltcGxpZmll" + "ZBgCIAEoCRIPCgdlbmdsaXNoGAMgASgJEhAKCGphcGFuZXNlGAQgASgJEhMK" + "C3RyYWRpdGlvbmFsGAUgASgJEg4KBmtvcmVhbhgGIAEoCSLpAQocU1RSUmVj" + "aGFyZ2VTdG9yZUFkc0NvbmZpZ3VyZRIyCgZMb2NhbHMYASADKAsyIi5TVFJS" + "ZWNoYXJnZVN0b3JlQWRzTG9jYWxDb25maWd1cmUSPwoJTG9jYWxEaWN0GAIg" + "AygLMiwuU1RSUmVjaGFyZ2VTdG9yZUFkc0NvbmZpZ3VyZS5Mb2NhbERpY3RF" + "bnRyeRpUCg5Mb2NhbERpY3RFbnRyeRILCgNrZXkYASABKA8SMQoFdmFsdWUY" + "AiABKAsyIi5TVFJSZWNoYXJnZVN0b3JlQWRzTG9jYWxDb25maWd1cmU6AjgB" + "YgZwcm90bzM="), new FileDescriptor[0], new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[2]
		{
			new GeneratedClrTypeInfo(typeof(STRRechargeStoreAdsLocalConfigure), STRRechargeStoreAdsLocalConfigure.Parser, new string[6] { "Id", "Simplified", "English", "Japanese", "Traditional", "Korean" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(STRRechargeStoreAdsConfigure), STRRechargeStoreAdsConfigure.Parser, new string[2] { "Locals", "LocalDict" }, null, null, null, new GeneratedClrTypeInfo[1])
		}));
	}
}
