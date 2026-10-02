using System;
using Google.Protobuf.Reflection;
using Google.Protobuf.WellKnownTypes;

public static class BannerReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static BannerReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("CgxCYW5uZXIucHJvdG8aH2dvb2dsZS9wcm90b2J1Zi90aW1lc3RhbXAucHJv" + "dG8aCkVudW0ucHJvdG8ivwIKE0Jhbm5lckluZm9Db25maWd1cmUSEAoIYmFu" + "bmVySUQYASABKA8SEwoLb3JkZXJXZWlnaHQYAiABKA8SIwoMbGFuZ3VhZ2VU" + "eXBlGAMgAygOMg0uTGFuZ3VhZ2VUeXBlEg8KB2Rlc2NJZDEYBCABKA8SDwoH" + "ZGVzY0lkMhgFIAEoDxItCgliZWdpblRpbWUYBiABKAsyGi5nb29nbGUucHJv" + "dG9idWYuVGltZXN0YW1wEisKB2VuZFRpbWUYByABKAsyGi5nb29nbGUucHJv" + "dG9idWYuVGltZXN0YW1wEg0KBXN0eWxlGAggASgPEhQKDGJhbm5lckltYWdl" + "cxgJIAMoCRIXCg9iYW5uZXJJbWFnZXNTRlcYCiADKAkSCwoDd2F5GAsgASgP" + "EhMKC21vbmV5U3dpdGNoGAwgASgIIq8BCg9CYW5uZXJDb25maWd1cmUSIwoF" + "SW5mb3MYASADKAsyFC5CYW5uZXJJbmZvQ29uZmlndXJlEjAKCEluZm9EaWN0" + "GAIgAygLMh4uQmFubmVyQ29uZmlndXJlLkluZm9EaWN0RW50cnkaRQoNSW5m" + "b0RpY3RFbnRyeRILCgNrZXkYASABKA8SIwoFdmFsdWUYAiABKAsyFC5CYW5u" + "ZXJJbmZvQ29uZmlndXJlOgI4AWIGcHJvdG8z"), new FileDescriptor[2]
		{
			TimestampReflection.Descriptor,
			EnumReflection.Descriptor
		}, new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[2]
		{
			new GeneratedClrTypeInfo(typeof(BannerInfoConfigure), BannerInfoConfigure.Parser, new string[12]
			{
				"BannerID", "OrderWeight", "LanguageType", "DescId1", "DescId2", "BeginTime", "EndTime", "Style", "BannerImages", "BannerImagesSFW",
				"Way", "MoneySwitch"
			}, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(BannerConfigure), BannerConfigure.Parser, new string[2] { "Infos", "InfoDict" }, null, null, null, new GeneratedClrTypeInfo[1])
		}));
	}
}
