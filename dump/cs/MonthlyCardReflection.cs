using System;
using Google.Protobuf.Reflection;

public static class MonthlyCardReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static MonthlyCardReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("ChFNb250aGx5Q2FyZC5wcm90byLXAwoZTW9udGhseUNhcmRHb29kc0NvbmZp" + "Z3VyZRIPCgdnb29kc0lEGAEgASgPEg8KB3RpdGxlSUQYAiABKA8SFQoNZGVz" + "Y3JpcHRpb25JRBgDIAEoDxIUCgxiYWNrZ3JvdW5kQ04YBCABKAkSFAoMYmFj" + "a2dyb3VuZEVOGAUgASgJEhQKDGJhY2tncm91bmRKUBgGIAEoCRIUCgxiYWNr" + "Z3JvdW5kVEMYByABKAkSTAoRaW1tZWRpYXRlbHlSZXdhcmQYCCADKAsyMS5N" + "b250aGx5Q2FyZEdvb2RzQ29uZmlndXJlLkltbWVkaWF0ZWx5UmV3YXJkRW50" + "cnkSQAoLZGFpbHlSZXdhcmQYCSADKAsyKy5Nb250aGx5Q2FyZEdvb2RzQ29u" + "ZmlndXJlLkRhaWx5UmV3YXJkRW50cnkSEwoLbW9udGhseURheXMYCiABKA8S" + "FgoOc3Vic2NyaWJlTGltaXQYCyABKA8aOAoWSW1tZWRpYXRlbHlSZXdhcmRF" + "bnRyeRILCgNrZXkYASABKA8SDQoFdmFsdWUYAiABKA86AjgBGjIKEERhaWx5" + "UmV3YXJkRW50cnkSCwoDa2V5GAEgASgPEg0KBXZhbHVlGAIgASgPOgI4ASLJ" + "AQoUTW9udGhseUNhcmRDb25maWd1cmUSKgoGR29vZHNzGAEgAygLMhouTW9u" + "dGhseUNhcmRHb29kc0NvbmZpZ3VyZRI3CglHb29kc0RpY3QYAiADKAsyJC5N" + "b250aGx5Q2FyZENvbmZpZ3VyZS5Hb29kc0RpY3RFbnRyeRpMCg5Hb29kc0Rp" + "Y3RFbnRyeRILCgNrZXkYASABKA8SKQoFdmFsdWUYAiABKAsyGi5Nb250aGx5" + "Q2FyZEdvb2RzQ29uZmlndXJlOgI4AWIGcHJvdG8z"), new FileDescriptor[0], new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[2]
		{
			new GeneratedClrTypeInfo(typeof(MonthlyCardGoodsConfigure), MonthlyCardGoodsConfigure.Parser, new string[11]
			{
				"GoodsID", "TitleID", "DescriptionID", "BackgroundCN", "BackgroundEN", "BackgroundJP", "BackgroundTC", "ImmediatelyReward", "DailyReward", "MonthlyDays",
				"SubscribeLimit"
			}, null, null, null, new GeneratedClrTypeInfo[2]),
			new GeneratedClrTypeInfo(typeof(MonthlyCardConfigure), MonthlyCardConfigure.Parser, new string[2] { "Goodss", "GoodsDict" }, null, null, null, new GeneratedClrTypeInfo[1])
		}));
	}
}
