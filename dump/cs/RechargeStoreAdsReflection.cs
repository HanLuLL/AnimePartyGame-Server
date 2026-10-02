using System;
using Google.Protobuf.Reflection;

public static class RechargeStoreAdsReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static RechargeStoreAdsReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("ChZSZWNoYXJnZVN0b3JlQWRzLnByb3RvGgpFbnVtLnByb3RvIo8BChxSZWNo" + "YXJnZVN0b3JlQWRzQWRzQ29uZmlndXJlEiEKC3Nob3BUYWJUeXBlGAEgASgO" + "MgwuU2hvcFRhYlR5cGUSTAohcmVjaGFyZ2VTdG9yZUFkc0Fkc0NvbmZpZ3Vy" + "ZUl0ZW1zGAIgAygLMiEuUmVjaGFyZ2VTdG9yZUFkc0Fkc0NvbmZpZ3VyZUl0" + "ZW0ivgEKIFJlY2hhcmdlU3RvcmVBZHNBZHNDb25maWd1cmVJdGVtEg0KBWlu" + "ZGV4GAEgASgPEg8KB3RpdGxlSUQYAiABKA8SFQoNZGVzY3JpcHRpb25JRBgD" + "IAEoDxIUCgxiYWNrZ3JvdW5kQ04YBCABKAkSFAoMYmFja2dyb3VuZEVOGAUg" + "ASgJEhQKDGJhY2tncm91bmRKUBgGIAEoCRIUCgxiYWNrZ3JvdW5kVEMYByAB" + "KAkSCwoDd2F5GAggASgPItEBChlSZWNoYXJnZVN0b3JlQWRzQ29uZmlndXJl" + "EisKBEFkc3MYASADKAsyHS5SZWNoYXJnZVN0b3JlQWRzQWRzQ29uZmlndXJl" + "EjgKB0Fkc0RpY3QYAiADKAsyJy5SZWNoYXJnZVN0b3JlQWRzQ29uZmlndXJl" + "LkFkc0RpY3RFbnRyeRpNCgxBZHNEaWN0RW50cnkSCwoDa2V5GAEgASgPEiwK" + "BXZhbHVlGAIgASgLMh0uUmVjaGFyZ2VTdG9yZUFkc0Fkc0NvbmZpZ3VyZToC" + "OAFiBnByb3RvMw=="), new FileDescriptor[1] { EnumReflection.Descriptor }, new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[3]
		{
			new GeneratedClrTypeInfo(typeof(RechargeStoreAdsAdsConfigure), RechargeStoreAdsAdsConfigure.Parser, new string[2] { "ShopTabType", "RechargeStoreAdsAdsConfigureItems" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(RechargeStoreAdsAdsConfigureItem), RechargeStoreAdsAdsConfigureItem.Parser, new string[8] { "Index", "TitleID", "DescriptionID", "BackgroundCN", "BackgroundEN", "BackgroundJP", "BackgroundTC", "Way" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(RechargeStoreAdsConfigure), RechargeStoreAdsConfigure.Parser, new string[2] { "Adss", "AdsDict" }, null, null, null, new GeneratedClrTypeInfo[1])
		}));
	}
}
