using System;
using Google.Protobuf.Reflection;
using Google.Protobuf.WellKnownTypes;

public static class GachaReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static GachaReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("CgtHYWNoYS5wcm90bxofZ29vZ2xlL3Byb3RvYnVmL3RpbWVzdGFtcC5wcm90" + "bxoKRW51bS5wcm90byKXAwoXR2FjaGFCYWNrc3RhZ2VDb25maWd1cmUSHQoJ" + "Z2FjaGFUeXBlGAEgASgOMgouR2FjaGFUeXBlEicKDmdhY2hhVGFibGVUeXBl" + "GAIgASgOMg8uR2FjaGFUYWJsZVR5cGUSIQoLdWlQYW5lbFR5cGUYAyABKA4y" + "DC5VSVBhbmVsVHlwZRIOCgZwb29sSUQYBCABKA8SEwoLY3VycmVuY3lCYXIY" + "BSABKA8SEAoIY29zdEl0ZW0YBiABKA8SEQoJdGltZUxpbWl0GAcgASgPEhAK" + "CGNvc3RPbmNlGAggASgPEhIKCkxlYXN0VGltZXMYCSABKA8SDgoGblRpbWVz" + "GAogASgPEg4KBmdpZnRJRBgLIAEoDxILCgN3YXkYDCABKA8SMQoNYmVnaW5E" + "YXRlVGltZRgNIAEoCzIaLmdvb2dsZS5wcm90b2J1Zi5UaW1lc3RhbXASLwoL" + "ZW5kRGF0ZVRpbWUYDiABKAsyGi5nb29nbGUucHJvdG9idWYuVGltZXN0YW1w" + "EhAKCHRhYk9yZGVyGA8gASgPIocGChJHYWNoYVBvb2xDb25maWd1cmUSDgoG" + "cG9vbElEGAEgASgPEhMKC2NvbWJEZWZhdWx0GAIgASgPEhUKDWNvbWJHdWFy" + "YW50ZWUYAyABKA8SEAoIY29tYlJvbGUYBCABKA8SDgoGY29tYlVQGAUgASgP" + "Eg4KBm5hbWVJRBgGIAEoDxITCgtwdWJsaWNpdHlJRBgHIAEoDxIYChBydWxl" + "RGVjcmlwdGlvbklEGAggASgPEhgKEHJvbGVEZWNyaXB0aW9uSUQYCSABKA8S" + "GwoTZmFzaGlvbkRlY3JpcHRpb25JRBgKIAEoDxIYChBnaWZ0RGVjcmlwdGlv" + "bklEGAsgASgPEhkKEW90aGVyRGVjcmlwdGlvbklEGAwgASgPEg4KBnVwSXRl" + "bRgNIAMoDxIVCg1za2luQ2hhcmFjdGVyGA4gASgPEhAKCHNraW5OYW1lGA8g" + "ASgPEiAKGHNlYXNvblNraW5SZXJ1bk5vdFVwSXRlbRgQIAMoDxIgChhzZWFz" + "b25Ta2luUmVydW5DaGFyYWN0ZXIYESADKA8SGwoTc2Vhc29uU2tpblJlcnVu" + "TmFtZRgSIAMoDxIUCgxiYWNrZ3JvdW5kQ04YEyABKAkSFwoPYmFja2dyb3Vu" + "ZENOU0ZXGBQgASgJEhQKDGJhY2tncm91bmRFThgVIAEoCRIXCg9iYWNrZ3Jv" + "dW5kRU5TRlcYFiABKAkSFAoMYmFja2dyb3VuZEpQGBcgASgJEhcKD2JhY2tn" + "cm91bmRKUFNGVxgYIAEoCRIUCgxiYWNrZ3JvdW5kVEMYGSABKAkSFwoPYmFj" + "a2dyb3VuZFRDU0ZXGBogASgJEhYKDnByb2dyZXNzUmV3YXJkGBsgASgPEjkK" + "C2V4dHJhUmV3YXJkGBwgAygLMiQuR2FjaGFQb29sQ29uZmlndXJlLkV4dHJh" + "UmV3YXJkRW50cnkSCwoDd2F5GB0gASgPGjIKEEV4dHJhUmV3YXJkRW50cnkS" + "CwoDa2V5GAEgASgPEg0KBXZhbHVlGAIgASgPOgI4ASJeChJHYWNoYUNvbWJD" + "b25maWd1cmUSDgoGY29tYklEGAEgASgPEjgKF2dhY2hhQ29tYkNvbmZpZ3Vy" + "ZUl0ZW1zGAIgAygLMhcuR2FjaGFDb21iQ29uZmlndXJlSXRlbSJNChZHYWNo" + "YUNvbWJDb25maWd1cmVJdGVtEg0KBWluZGV4GAEgASgPEg8KB2dyb3VwSUQY" + "AiABKA8SEwoLZ3JvdXBXZWlnaHQYAyABKA8ikQEKE0dhY2hhR3JvdXBDb25m" + "aWd1cmUSDwoHZ3JvdXBJRBgBIAEoDxItChFnYWNoYUl0ZW1Tb3J0VHlwZRgC" + "IAEoDjISLkdhY2hhSXRlbVNvcnRUeXBlEjoKGGdhY2hhR3JvdXBDb25maWd1" + "cmVJdGVtcxgDIAMoCzIYLkdhY2hhR3JvdXBDb25maWd1cmVJdGVtIl8KF0dh" + "Y2hhR3JvdXBDb25maWd1cmVJdGVtEg0KBWluZGV4GAEgASgPEg4KBml0ZW1J" + "ZBgCIAEoDxIRCgludW1iZXJNaW4YAyABKA8SEgoKaXRlbVdlaWdodBgEIAEo" + "DyJqChZHYWNoYVByb2dyZXNzQ29uZmlndXJlEg4KBnBvb2xJRBgBIAEoDxJA" + "ChtnYWNoYVByb2dyZXNzQ29uZmlndXJlSXRlbXMYAiADKAsyGy5HYWNoYVBy" + "b2dyZXNzQ29uZmlndXJlSXRlbSKTAQoaR2FjaGFQcm9ncmVzc0NvbmZpZ3Vy" + "ZUl0ZW0SDQoFY291bnQYASABKA8SNwoGcmV3YXJkGAIgAygLMicuR2FjaGFQ" + "cm9ncmVzc0NvbmZpZ3VyZUl0ZW0uUmV3YXJkRW50cnkaLQoLUmV3YXJkRW50" + "cnkSCwoDa2V5GAEgASgPEg0KBXZhbHVlGAIgASgPOgI4ASLTBgoOR2FjaGFD" + "b25maWd1cmUSLAoKQmFja3N0YWdlcxgBIAMoCzIYLkdhY2hhQmFja3N0YWdl" + "Q29uZmlndXJlEjkKDUJhY2tzdGFnZURpY3QYAiADKAsyIi5HYWNoYUNvbmZp" + "Z3VyZS5CYWNrc3RhZ2VEaWN0RW50cnkSIgoFUG9vbHMYAyADKAsyEy5HYWNo" + "YVBvb2xDb25maWd1cmUSLwoIUG9vbERpY3QYBCADKAsyHS5HYWNoYUNvbmZp" + "Z3VyZS5Qb29sRGljdEVudHJ5EiIKBUNvbWJzGAUgAygLMhMuR2FjaGFDb21i" + "Q29uZmlndXJlEi8KCENvbWJEaWN0GAYgAygLMh0uR2FjaGFDb25maWd1cmUu" + "Q29tYkRpY3RFbnRyeRIkCgZHcm91cHMYByADKAsyFC5HYWNoYUdyb3VwQ29u" + "ZmlndXJlEjEKCUdyb3VwRGljdBgIIAMoCzIeLkdhY2hhQ29uZmlndXJlLkdy" + "b3VwRGljdEVudHJ5EioKCVByb2dyZXNzcxgJIAMoCzIXLkdhY2hhUHJvZ3Jl" + "c3NDb25maWd1cmUSNwoMUHJvZ3Jlc3NEaWN0GAogAygLMiEuR2FjaGFDb25m" + "aWd1cmUuUHJvZ3Jlc3NEaWN0RW50cnkaTgoSQmFja3N0YWdlRGljdEVudHJ5" + "EgsKA2tleRgBIAEoDxInCgV2YWx1ZRgCIAEoCzIYLkdhY2hhQmFja3N0YWdl" + "Q29uZmlndXJlOgI4ARpECg1Qb29sRGljdEVudHJ5EgsKA2tleRgBIAEoDxIi" + "CgV2YWx1ZRgCIAEoCzITLkdhY2hhUG9vbENvbmZpZ3VyZToCOAEaRAoNQ29t" + "YkRpY3RFbnRyeRILCgNrZXkYASABKA8SIgoFdmFsdWUYAiABKAsyEy5HYWNo" + "YUNvbWJDb25maWd1cmU6AjgBGkYKDkdyb3VwRGljdEVudHJ5EgsKA2tleRgB" + "IAEoDxIjCgV2YWx1ZRgCIAEoCzIULkdhY2hhR3JvdXBDb25maWd1cmU6AjgB" + "GkwKEVByb2dyZXNzRGljdEVudHJ5EgsKA2tleRgBIAEoDxImCgV2YWx1ZRgC" + "IAEoCzIXLkdhY2hhUHJvZ3Jlc3NDb25maWd1cmU6AjgBYgZwcm90bzM="), new FileDescriptor[2]
		{
			TimestampReflection.Descriptor,
			EnumReflection.Descriptor
		}, new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[9]
		{
			new GeneratedClrTypeInfo(typeof(GachaBackstageConfigure), GachaBackstageConfigure.Parser, new string[15]
			{
				"GachaType", "GachaTableType", "UiPanelType", "PoolID", "CurrencyBar", "CostItem", "TimeLimit", "CostOnce", "LeastTimes", "NTimes",
				"GiftID", "Way", "BeginDateTime", "EndDateTime", "TabOrder"
			}, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(GachaPoolConfigure), GachaPoolConfigure.Parser, new string[29]
			{
				"PoolID", "CombDefault", "CombGuarantee", "CombRole", "CombUP", "NameID", "PublicityID", "RuleDecriptionID", "RoleDecriptionID", "FashionDecriptionID",
				"GiftDecriptionID", "OtherDecriptionID", "UpItem", "SkinCharacter", "SkinName", "SeasonSkinRerunNotUpItem", "SeasonSkinRerunCharacter", "SeasonSkinRerunName", "BackgroundCN", "BackgroundCNSFW",
				"BackgroundEN", "BackgroundENSFW", "BackgroundJP", "BackgroundJPSFW", "BackgroundTC", "BackgroundTCSFW", "ProgressReward", "ExtraReward", "Way"
			}, null, null, null, new GeneratedClrTypeInfo[1]),
			new GeneratedClrTypeInfo(typeof(GachaCombConfigure), GachaCombConfigure.Parser, new string[2] { "CombID", "GachaCombConfigureItems" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(GachaCombConfigureItem), GachaCombConfigureItem.Parser, new string[3] { "Index", "GroupID", "GroupWeight" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(GachaGroupConfigure), GachaGroupConfigure.Parser, new string[3] { "GroupID", "GachaItemSortType", "GachaGroupConfigureItems" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(GachaGroupConfigureItem), GachaGroupConfigureItem.Parser, new string[4] { "Index", "ItemId", "NumberMin", "ItemWeight" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(GachaProgressConfigure), GachaProgressConfigure.Parser, new string[2] { "PoolID", "GachaProgressConfigureItems" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(GachaProgressConfigureItem), GachaProgressConfigureItem.Parser, new string[2] { "Count", "Reward" }, null, null, null, new GeneratedClrTypeInfo[1]),
			new GeneratedClrTypeInfo(typeof(GachaConfigure), GachaConfigure.Parser, new string[10] { "Backstages", "BackstageDict", "Pools", "PoolDict", "Combs", "CombDict", "Groups", "GroupDict", "Progresss", "ProgressDict" }, null, null, null, new GeneratedClrTypeInfo[5])
		}));
	}
}
