using System;
using Google.Protobuf.Reflection;
using Google.Protobuf.WellKnownTypes;

public static class ExchangeStoreReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static ExchangeStoreReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("ChNFeGNoYW5nZVN0b3JlLnByb3RvGh9nb29nbGUvcHJvdG9idWYvdGltZXN0" + "YW1wLnByb3RvGgpFbnVtLnByb3RvIn8KG0V4Y2hhbmdlU3RvcmVTaGVsZkNv" + "bmZpZ3VyZRIKCgJpZBgBIAEoDxIQCgh0YWJPcmRlchgCIAEoDxIOCgZuYW1l" + "SUQYAyABKA8SIgoMc2hvcFRhYlR5cGVzGAQgAygOMgwuU2hvcFRhYlR5cGUS" + "DgoGaXNTaG93GAUgASgIIuEDChpFeGNoYW5nZVN0b3JlSW5mb0NvbmZpZ3Vy" + "ZRIhCgtzaG9wVGFiVHlwZRgBIAEoDjIMLlNob3BUYWJUeXBlEg4KBm5hbWVJ" + "RBgCIAEoDxITCgtjdXJyZW5jeUJhchgDIAEoDxItCgliZWdpblRpbWUYBCAB" + "KAsyGi5nb29nbGUucHJvdG9idWYuVGltZXN0YW1wEisKB2VuZFRpbWUYBSAB" + "KAsyGi5nb29nbGUucHJvdG9idWYuVGltZXN0YW1wEisKEGdvb2RzUmVmcmVz" + "aFR5cGUYBiABKA4yES5Hb29kc1JlZnJlc2hUeXBlEkMKDHdvcmtkYXlHb29k" + "cxgHIAMoCzItLkV4Y2hhbmdlU3RvcmVJbmZvQ29uZmlndXJlLldvcmtkYXlH" + "b29kc0VudHJ5EkMKDHdlZWtlbmRHb29kcxgIIAMoCzItLkV4Y2hhbmdlU3Rv" + "cmVJbmZvQ29uZmlndXJlLldlZWtlbmRHb29kc0VudHJ5GjMKEVdvcmtkYXlH" + "b29kc0VudHJ5EgsKA2tleRgBIAEoDxINCgV2YWx1ZRgCIAEoDzoCOAEaMwoR" + "V2Vla2VuZEdvb2RzRW50cnkSCwoDa2V5GAEgASgPEg0KBXZhbHVlGAIgASgP" + "OgI4ASKHAQohRXhjaGFuZ2VTdG9yZVJlZnJlc2hQb29sQ29uZmlndXJlEgoK" + "AmlkGAEgASgPElYKJmV4Y2hhbmdlU3RvcmVSZWZyZXNoUG9vbENvbmZpZ3Vy" + "ZUl0ZW1zGAIgAygLMiYuRXhjaGFuZ2VTdG9yZVJlZnJlc2hQb29sQ29uZmln" + "dXJlSXRlbSKtAQolRXhjaGFuZ2VTdG9yZVJlZnJlc2hQb29sQ29uZmlndXJl" + "SXRlbRIPCgdnb29kc0lEGAEgASgPEg4KBml0ZW1JRBgCIAEoDxIPCgdpdGVt" + "TnVtGAMgASgPEhIKCmN1cnJlbmN5SUQYBCABKA8SFQoNb3JpZ2luYWxQcmlj" + "ZRgFIAEoDxIVCg1kaXNjb3VudFByaWNlGAYgASgPEhAKCG51bUxpbWl0GAcg" + "ASgPIowBChtFeGNoYW5nZVN0b3JlR29vZHNDb25maWd1cmUSIQoLc2hvcFRh" + "YlR5cGUYASABKA4yDC5TaG9wVGFiVHlwZRJKCiBleGNoYW5nZVN0b3JlR29v" + "ZHNDb25maWd1cmVJdGVtcxgCIAMoCzIgLkV4Y2hhbmdlU3RvcmVHb29kc0Nv" + "bmZpZ3VyZUl0ZW0iuwQKH0V4Y2hhbmdlU3RvcmVHb29kc0NvbmZpZ3VyZUl0" + "ZW0SDwoHZ29vZHNJRBgBIAEoDxISCgpnb29kc09yZGVyGAIgASgPEhQKDHJl" + "Y2hhcmdlSWNvbhgDIAEoCRIQCghJdGVtVHlwZRgEIAEoCRIOCgZpdGVtSUQY" + "BSABKA8SDwoHaXRlbU51bRgGIAEoDxISCgpjdXJyZW5jeUlEGAcgASgPEhUK" + "DW9yaWdpbmFsUHJpY2UYCCABKA8SFQoNZGlzY291bnRQcmljZRgJIAEoDxI0" + "ChBiZWdpblRpbWVMaW1pdGVkGAogASgLMhouZ29vZ2xlLnByb3RvYnVmLlRp" + "bWVzdGFtcBIyCg5lbmRUaW1lTGltaXRlZBgLIAEoCzIaLmdvb2dsZS5wcm90" + "b2J1Zi5UaW1lc3RhbXASHAoUZGlzY291bnRQcmljZUxpbWl0ZWQYDCABKA8S" + "KwoQZ29vZHNSZWZyZXNoVHlwZRgNIAEoDjIRLkdvb2RzUmVmcmVzaFR5cGUS" + "EAoIbnVtTGltaXQYDiABKA8SJwoOZ29vZHNMYWJlbFR5cGUYDyABKA4yDy5H" + "b29kc0xhYmVsVHlwZRItCgliZWdpblRpbWUYECABKAsyGi5nb29nbGUucHJv" + "dG9idWYuVGltZXN0YW1wEisKB2VuZFRpbWUYESABKAsyGi5nb29nbGUucHJv" + "dG9idWYuVGltZXN0YW1wEg0KBXBhcmFtGBIgASgPEg0KBWdyZWF0GBMgASgP" + "IoEBCh9FeGNoYW5nZVN0b3JlR2lmdENoYWluQ29uZmlndXJlEgoKAmlkGAEg" + "ASgPElIKJGV4Y2hhbmdlU3RvcmVHaWZ0Q2hhaW5Db25maWd1cmVJdGVtcxgC" + "IAMoCzIkLkV4Y2hhbmdlU3RvcmVHaWZ0Q2hhaW5Db25maWd1cmVJdGVtIkUK" + "I0V4Y2hhbmdlU3RvcmVHaWZ0Q2hhaW5Db25maWd1cmVJdGVtEg8KB2dvb2Rz" + "SUQYASABKA8SDQoFcGFyYW0YAiABKA8iXQofRXhjaGFuZ2VTdG9yZVNraW5H" + "cm91cENvbmZpZ3VyZRIKCgJpZBgBIAEoDxIQCgh0YWJPcmRlchgCIAEoDxIM" + "CgRpY29uGAMgASgJEg4KBm5hbWVJRBgEIAEoDyK8CQoWRXhjaGFuZ2VTdG9y" + "ZUNvbmZpZ3VyZRIsCgZTaGVsZnMYASADKAsyHC5FeGNoYW5nZVN0b3JlU2hl" + "bGZDb25maWd1cmUSOQoJU2hlbGZEaWN0GAIgAygLMiYuRXhjaGFuZ2VTdG9y" + "ZUNvbmZpZ3VyZS5TaGVsZkRpY3RFbnRyeRIqCgVJbmZvcxgDIAMoCzIbLkV4" + "Y2hhbmdlU3RvcmVJbmZvQ29uZmlndXJlEjcKCEluZm9EaWN0GAQgAygLMiUu" + "RXhjaGFuZ2VTdG9yZUNvbmZpZ3VyZS5JbmZvRGljdEVudHJ5EjgKDFJlZnJl" + "c2hQb29scxgFIAMoCzIiLkV4Y2hhbmdlU3RvcmVSZWZyZXNoUG9vbENvbmZp" + "Z3VyZRJFCg9SZWZyZXNoUG9vbERpY3QYBiADKAsyLC5FeGNoYW5nZVN0b3Jl" + "Q29uZmlndXJlLlJlZnJlc2hQb29sRGljdEVudHJ5EiwKBkdvb2RzcxgHIAMo" + "CzIcLkV4Y2hhbmdlU3RvcmVHb29kc0NvbmZpZ3VyZRI5CglHb29kc0RpY3QY" + "CCADKAsyJi5FeGNoYW5nZVN0b3JlQ29uZmlndXJlLkdvb2RzRGljdEVudHJ5" + "EjQKCkdpZnRDaGFpbnMYCSADKAsyIC5FeGNoYW5nZVN0b3JlR2lmdENoYWlu" + "Q29uZmlndXJlEkEKDUdpZnRDaGFpbkRpY3QYCiADKAsyKi5FeGNoYW5nZVN0" + "b3JlQ29uZmlndXJlLkdpZnRDaGFpbkRpY3RFbnRyeRI0CgpTa2luR3JvdXBz" + "GAsgAygLMiAuRXhjaGFuZ2VTdG9yZVNraW5Hcm91cENvbmZpZ3VyZRJBCg1T" + "a2luR3JvdXBEaWN0GAwgAygLMiouRXhjaGFuZ2VTdG9yZUNvbmZpZ3VyZS5T" + "a2luR3JvdXBEaWN0RW50cnkaTgoOU2hlbGZEaWN0RW50cnkSCwoDa2V5GAEg" + "ASgPEisKBXZhbHVlGAIgASgLMhwuRXhjaGFuZ2VTdG9yZVNoZWxmQ29uZmln" + "dXJlOgI4ARpMCg1JbmZvRGljdEVudHJ5EgsKA2tleRgBIAEoDxIqCgV2YWx1" + "ZRgCIAEoCzIbLkV4Y2hhbmdlU3RvcmVJbmZvQ29uZmlndXJlOgI4ARpaChRS" + "ZWZyZXNoUG9vbERpY3RFbnRyeRILCgNrZXkYASABKA8SMQoFdmFsdWUYAiAB" + "KAsyIi5FeGNoYW5nZVN0b3JlUmVmcmVzaFBvb2xDb25maWd1cmU6AjgBGk4K" + "Dkdvb2RzRGljdEVudHJ5EgsKA2tleRgBIAEoDxIrCgV2YWx1ZRgCIAEoCzIc" + "LkV4Y2hhbmdlU3RvcmVHb29kc0NvbmZpZ3VyZToCOAEaVgoSR2lmdENoYWlu" + "RGljdEVudHJ5EgsKA2tleRgBIAEoDxIvCgV2YWx1ZRgCIAEoCzIgLkV4Y2hh" + "bmdlU3RvcmVHaWZ0Q2hhaW5Db25maWd1cmU6AjgBGlYKElNraW5Hcm91cERp" + "Y3RFbnRyeRILCgNrZXkYASABKA8SLwoFdmFsdWUYAiABKAsyIC5FeGNoYW5n" + "ZVN0b3JlU2tpbkdyb3VwQ29uZmlndXJlOgI4AWIGcHJvdG8z"), new FileDescriptor[2]
		{
			TimestampReflection.Descriptor,
			EnumReflection.Descriptor
		}, new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[10]
		{
			new GeneratedClrTypeInfo(typeof(ExchangeStoreShelfConfigure), ExchangeStoreShelfConfigure.Parser, new string[5] { "Id", "TabOrder", "NameID", "ShopTabTypes", "IsShow" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(ExchangeStoreInfoConfigure), ExchangeStoreInfoConfigure.Parser, new string[8] { "ShopTabType", "NameID", "CurrencyBar", "BeginTime", "EndTime", "GoodsRefreshType", "WorkdayGoods", "WeekendGoods" }, null, null, null, new GeneratedClrTypeInfo[2]),
			new GeneratedClrTypeInfo(typeof(ExchangeStoreRefreshPoolConfigure), ExchangeStoreRefreshPoolConfigure.Parser, new string[2] { "Id", "ExchangeStoreRefreshPoolConfigureItems" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(ExchangeStoreRefreshPoolConfigureItem), ExchangeStoreRefreshPoolConfigureItem.Parser, new string[7] { "GoodsID", "ItemID", "ItemNum", "CurrencyID", "OriginalPrice", "DiscountPrice", "NumLimit" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(ExchangeStoreGoodsConfigure), ExchangeStoreGoodsConfigure.Parser, new string[2] { "ShopTabType", "ExchangeStoreGoodsConfigureItems" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(ExchangeStoreGoodsConfigureItem), ExchangeStoreGoodsConfigureItem.Parser, new string[19]
			{
				"GoodsID", "GoodsOrder", "RechargeIcon", "ItemType", "ItemID", "ItemNum", "CurrencyID", "OriginalPrice", "DiscountPrice", "BeginTimeLimited",
				"EndTimeLimited", "DiscountPriceLimited", "GoodsRefreshType", "NumLimit", "GoodsLabelType", "BeginTime", "EndTime", "Param", "Great"
			}, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(ExchangeStoreGiftChainConfigure), ExchangeStoreGiftChainConfigure.Parser, new string[2] { "Id", "ExchangeStoreGiftChainConfigureItems" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(ExchangeStoreGiftChainConfigureItem), ExchangeStoreGiftChainConfigureItem.Parser, new string[2] { "GoodsID", "Param" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(ExchangeStoreSkinGroupConfigure), ExchangeStoreSkinGroupConfigure.Parser, new string[4] { "Id", "TabOrder", "Icon", "NameID" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(ExchangeStoreConfigure), ExchangeStoreConfigure.Parser, new string[12]
			{
				"Shelfs", "ShelfDict", "Infos", "InfoDict", "RefreshPools", "RefreshPoolDict", "Goodss", "GoodsDict", "GiftChains", "GiftChainDict",
				"SkinGroups", "SkinGroupDict"
			}, null, null, null, new GeneratedClrTypeInfo[6])
		}));
	}
}
