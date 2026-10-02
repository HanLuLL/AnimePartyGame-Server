using System;
using Google.Protobuf.Reflection;
using Google.Protobuf.WellKnownTypes;

public static class FixProductRecommendationReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static FixProductRecommendationReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("Ch5GaXhQcm9kdWN0UmVjb21tZW5kYXRpb24ucHJvdG8aH2dvb2dsZS9wcm90" + "b2J1Zi90aW1lc3RhbXAucHJvdG8ilwEKJ0ZpeFByb2R1Y3RSZWNvbW1lbmRh" + "dGlvbkJhbm5lckNvbmZpZ3VyZRIQCghiYW5uZXJJRBgBIAEoDxItCgliZWdp" + "blRpbWUYAiABKAsyGi5nb29nbGUucHJvdG9idWYuVGltZXN0YW1wEisKB2Vu" + "ZFRpbWUYAyABKAsyGi5nb29nbGUucHJvdG9idWYuVGltZXN0YW1wIoMCCiFG" + "aXhQcm9kdWN0UmVjb21tZW5kYXRpb25Db25maWd1cmUSOQoHQmFubmVycxgB" + "IAMoCzIoLkZpeFByb2R1Y3RSZWNvbW1lbmRhdGlvbkJhbm5lckNvbmZpZ3Vy" + "ZRJGCgpCYW5uZXJEaWN0GAIgAygLMjIuRml4UHJvZHVjdFJlY29tbWVuZGF0" + "aW9uQ29uZmlndXJlLkJhbm5lckRpY3RFbnRyeRpbCg9CYW5uZXJEaWN0RW50" + "cnkSCwoDa2V5GAEgASgPEjcKBXZhbHVlGAIgASgLMiguRml4UHJvZHVjdFJl" + "Y29tbWVuZGF0aW9uQmFubmVyQ29uZmlndXJlOgI4AWIGcHJvdG8z"), new FileDescriptor[1] { TimestampReflection.Descriptor }, new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[2]
		{
			new GeneratedClrTypeInfo(typeof(FixProductRecommendationBannerConfigure), FixProductRecommendationBannerConfigure.Parser, new string[3] { "BannerID", "BeginTime", "EndTime" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(FixProductRecommendationConfigure), FixProductRecommendationConfigure.Parser, new string[2] { "Banners", "BannerDict" }, null, null, null, new GeneratedClrTypeInfo[1])
		}));
	}
}
