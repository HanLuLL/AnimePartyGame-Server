using System;
using Google.Protobuf.Reflection;
using Google.Protobuf.WellKnownTypes;

public static class RechargeStoreReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static RechargeStoreReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("ChNSZWNoYXJnZVN0b3JlLnByb3RvGh9nb29nbGUvcHJvdG9idWYvdGltZXN0" + "YW1wLnByb3RvGgpFbnVtLnByb3RvIoABChtSZWNoYXJnZVN0b3JlU2hlbGZD" + "b25maWd1cmUSCgoCaWQYASABKA8SDQoFb3JkZXIYAiABKA8SDgoGbmFtZUlE" + "GAMgASgPEhIKCmJhY2tncm91bmQYBCABKAkSIgoMc2hvcFRhYlR5cGVzGAUg" + "AygOMgwuU2hvcFRhYlR5cGUi7QEKGlJlY2hhcmdlU3RvcmVJbmZvQ29uZmln" + "dXJlEiEKC3Nob3BUYWJUeXBlGAEgASgOMgwuU2hvcFRhYlR5cGUSDgoGbmFt" + "ZUlEGAIgASgPEhMKC2N1cnJlbmN5QmFyGAMgASgPEi0KCWJlZ2luVGltZRgE" + "IAEoCzIaLmdvb2dsZS5wcm90b2J1Zi5UaW1lc3RhbXASKwoHZW5kVGltZRgF" + "IAEoCzIaLmdvb2dsZS5wcm90b2J1Zi5UaW1lc3RhbXASKwoQZ29vZHNSZWZy" + "ZXNoVHlwZRgGIAEoDjIRLkdvb2RzUmVmcmVzaFR5cGUijAEKG1JlY2hhcmdl" + "U3RvcmVHb29kc0NvbmZpZ3VyZRIhCgtzaG9wVGFiVHlwZRgBIAEoDjIMLlNo" + "b3BUYWJUeXBlEkoKIHJlY2hhcmdlU3RvcmVHb29kc0NvbmZpZ3VyZUl0ZW1z" + "GAIgAygLMiAuUmVjaGFyZ2VTdG9yZUdvb2RzQ29uZmlndXJlSXRlbSLfBgof" + "UmVjaGFyZ2VTdG9yZUdvb2RzQ29uZmlndXJlSXRlbRIPCgdnb29kc0lEGAEg" + "ASgPEhgKEG9wZXJhdGlvbkdvb2RzSUQYAiABKAkSEgoKZ29vZHNPcmRlchgD" + "IAEoDxIrChBtZXJjaGFuZGlzZWxUeXBlGAQgASgOMhEuTWVyY2hhbmRpc2Vs" + "VHlwZRIMCgRuYW1lGAUgASgPEhQKDHJlY2hhcmdlSWNvbhgGIAEoCRIQCghJ" + "dGVtVHlwZRgHIAEoCRIOCgZpdGVtSUQYCCABKA8SDwoHaXRlbU51bRgJIAEo" + "DxIUCgxib251c2VJdGVtSUQYCiABKA8SFAoMZ29vZHNCb251c2VzGAsgASgP" + "EhoKEmJvbnVzZXNEZXNjcmlwdGlvbhgMIAEoDxIXCg9vcmlnaW5hbFByaWNl" + "Q04YDSABKA8SFwoPZGlzY291bnRQcmljZUNOGA4gASgPEhcKD29yaWdpbmFs" + "UHJpY2VVUxgPIAEoDxIXCg9kaXNjb3VudFByaWNlVVMYECABKA8SFwoPb3Jp" + "Z2luYWxQcmljZUpQGBEgASgPEhcKD2Rpc2NvdW50UHJpY2VKUBgSIAEoDxI0" + "ChBiZWdpblRpbWVMaW1pdGVkGBMgASgLMhouZ29vZ2xlLnByb3RvYnVmLlRp" + "bWVzdGFtcBIyCg5lbmRUaW1lTGltaXRlZBgUIAEoCzIaLmdvb2dsZS5wcm90" + "b2J1Zi5UaW1lc3RhbXASHgoWZGlzY291bnRQcmljZUxpbWl0ZWRDThgVIAEo" + "DxIeChZkaXNjb3VudFByaWNlTGltaXRlZFVTGBYgASgPEh4KFmRpc2NvdW50" + "UHJpY2VMaW1pdGVkSlAYFyABKA8SKwoQZ29vZHNSZWZyZXNoVHlwZRgYIAEo" + "DjIRLkdvb2RzUmVmcmVzaFR5cGUSEAoIbnVtTGltaXQYGSABKA8SJwoOZ29v" + "ZHNMYWJlbFR5cGUYGiABKA4yDy5Hb29kc0xhYmVsVHlwZRItCgliZWdpblRp" + "bWUYGyABKAsyGi5nb29nbGUucHJvdG9idWYuVGltZXN0YW1wEisKB2VuZFRp" + "bWUYHCABKAsyGi5nb29nbGUucHJvdG9idWYuVGltZXN0YW1wEg0KBXBhcmFt" + "GB0gASgPIkcKIlJlY2hhcmdlU3RvcmVUcmFuc2ZlclR5cGVDb25maWd1cmUS" + "IQoLc2hvcFRhYlR5cGUYASABKA4yDC5TaG9wVGFiVHlwZSKgBgoWUmVjaGFy" + "Z2VTdG9yZUNvbmZpZ3VyZRIsCgZTaGVsZnMYASADKAsyHC5SZWNoYXJnZVN0" + "b3JlU2hlbGZDb25maWd1cmUSOQoJU2hlbGZEaWN0GAIgAygLMiYuUmVjaGFy" + "Z2VTdG9yZUNvbmZpZ3VyZS5TaGVsZkRpY3RFbnRyeRIqCgVJbmZvcxgDIAMo" + "CzIbLlJlY2hhcmdlU3RvcmVJbmZvQ29uZmlndXJlEjcKCEluZm9EaWN0GAQg" + "AygLMiUuUmVjaGFyZ2VTdG9yZUNvbmZpZ3VyZS5JbmZvRGljdEVudHJ5EiwK" + "Bkdvb2RzcxgFIAMoCzIcLlJlY2hhcmdlU3RvcmVHb29kc0NvbmZpZ3VyZRI5" + "CglHb29kc0RpY3QYBiADKAsyJi5SZWNoYXJnZVN0b3JlQ29uZmlndXJlLkdv" + "b2RzRGljdEVudHJ5EjoKDVRyYW5zZmVyVHlwZXMYByADKAsyIy5SZWNoYXJn" + "ZVN0b3JlVHJhbnNmZXJUeXBlQ29uZmlndXJlEkcKEFRyYW5zZmVyVHlwZURp" + "Y3QYCCADKAsyLS5SZWNoYXJnZVN0b3JlQ29uZmlndXJlLlRyYW5zZmVyVHlw" + "ZURpY3RFbnRyeRpOCg5TaGVsZkRpY3RFbnRyeRILCgNrZXkYASABKA8SKwoF" + "dmFsdWUYAiABKAsyHC5SZWNoYXJnZVN0b3JlU2hlbGZDb25maWd1cmU6AjgB" + "GkwKDUluZm9EaWN0RW50cnkSCwoDa2V5GAEgASgPEioKBXZhbHVlGAIgASgL" + "MhsuUmVjaGFyZ2VTdG9yZUluZm9Db25maWd1cmU6AjgBGk4KDkdvb2RzRGlj" + "dEVudHJ5EgsKA2tleRgBIAEoDxIrCgV2YWx1ZRgCIAEoCzIcLlJlY2hhcmdl" + "U3RvcmVHb29kc0NvbmZpZ3VyZToCOAEaXAoVVHJhbnNmZXJUeXBlRGljdEVu" + "dHJ5EgsKA2tleRgBIAEoDxIyCgV2YWx1ZRgCIAEoCzIjLlJlY2hhcmdlU3Rv" + "cmVUcmFuc2ZlclR5cGVDb25maWd1cmU6AjgBYgZwcm90bzM="), new FileDescriptor[2]
		{
			TimestampReflection.Descriptor,
			EnumReflection.Descriptor
		}, new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[6]
		{
			new GeneratedClrTypeInfo(typeof(RechargeStoreShelfConfigure), RechargeStoreShelfConfigure.Parser, new string[5] { "Id", "Order", "NameID", "Background", "ShopTabTypes" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(RechargeStoreInfoConfigure), RechargeStoreInfoConfigure.Parser, new string[6] { "ShopTabType", "NameID", "CurrencyBar", "BeginTime", "EndTime", "GoodsRefreshType" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(RechargeStoreGoodsConfigure), RechargeStoreGoodsConfigure.Parser, new string[2] { "ShopTabType", "RechargeStoreGoodsConfigureItems" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(RechargeStoreGoodsConfigureItem), RechargeStoreGoodsConfigureItem.Parser, new string[29]
			{
				"GoodsID", "OperationGoodsID", "GoodsOrder", "MerchandiselType", "Name", "RechargeIcon", "ItemType", "ItemID", "ItemNum", "BonuseItemID",
				"GoodsBonuses", "BonusesDescription", "OriginalPriceCN", "DiscountPriceCN", "OriginalPriceUS", "DiscountPriceUS", "OriginalPriceJP", "DiscountPriceJP", "BeginTimeLimited", "EndTimeLimited",
				"DiscountPriceLimitedCN", "DiscountPriceLimitedUS", "DiscountPriceLimitedJP", "GoodsRefreshType", "NumLimit", "GoodsLabelType", "BeginTime", "EndTime", "Param"
			}, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(RechargeStoreTransferTypeConfigure), RechargeStoreTransferTypeConfigure.Parser, new string[1] { "ShopTabType" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(RechargeStoreConfigure), RechargeStoreConfigure.Parser, new string[8] { "Shelfs", "ShelfDict", "Infos", "InfoDict", "Goodss", "GoodsDict", "TransferTypes", "TransferTypeDict" }, null, null, null, new GeneratedClrTypeInfo[4])
		}));
	}
}
