using System;
using Google.Protobuf.Reflection;

public static class SkinSellReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static SkinSellReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("Cg5Ta2luU2VsbC5wcm90byKzAQoVU2tpblNlbGxJbmZvQ29uZmlndXJlEgoK" + "AmlkGAEgASgPEhAKCGVudHJhbmNlGAIgASgJEhIKCmJhY2tncm91bmQYAyAB" + "KAkSFQoNZW50cmFuY2VUaXRsZRgEIAEoDxIRCglzcGxpdFNhbGUYBSADKA8S" + "Pgoac2tpblNlbGxJbmZvQ29uZmlndXJlSXRlbXMYBiADKAsyGi5Ta2luU2Vs" + "bEluZm9Db25maWd1cmVJdGVtImUKGVNraW5TZWxsSW5mb0NvbmZpZ3VyZUl0" + "ZW0SCgoCaWQYASABKA8SFQoNb3JpZ2luYWxQcmljZRgCIAEoDxIVCg1kaXNj" + "b3VudFByaWNlGAMgASgPEg4KBml0ZW1JRBgEIAEoDyK3AQoRU2tpblNlbGxD" + "b25maWd1cmUSJQoFSW5mb3MYASADKAsyFi5Ta2luU2VsbEluZm9Db25maWd1" + "cmUSMgoISW5mb0RpY3QYAiADKAsyIC5Ta2luU2VsbENvbmZpZ3VyZS5JbmZv" + "RGljdEVudHJ5GkcKDUluZm9EaWN0RW50cnkSCwoDa2V5GAEgASgPEiUKBXZh" + "bHVlGAIgASgLMhYuU2tpblNlbGxJbmZvQ29uZmlndXJlOgI4AWIGcHJvdG8z"), new FileDescriptor[0], new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[3]
		{
			new GeneratedClrTypeInfo(typeof(SkinSellInfoConfigure), SkinSellInfoConfigure.Parser, new string[6] { "Id", "Entrance", "Background", "EntranceTitle", "SplitSale", "SkinSellInfoConfigureItems" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(SkinSellInfoConfigureItem), SkinSellInfoConfigureItem.Parser, new string[4] { "Id", "OriginalPrice", "DiscountPrice", "ItemID" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(SkinSellConfigure), SkinSellConfigure.Parser, new string[2] { "Infos", "InfoDict" }, null, null, null, new GeneratedClrTypeInfo[1])
		}));
	}
}
