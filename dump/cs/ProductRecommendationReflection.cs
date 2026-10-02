using System;
using Google.Protobuf.Reflection;
using Google.Protobuf.WellKnownTypes;

public static class ProductRecommendationReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static ProductRecommendationReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("ChtQcm9kdWN0UmVjb21tZW5kYXRpb24ucHJvdG8aH2dvb2dsZS9wcm90b2J1" + "Zi90aW1lc3RhbXAucHJvdG8ilwIKJFByb2R1Y3RSZWNvbW1lbmRhdGlvbkJh" + "bm5lckNvbmZpZ3VyZRIQCghiYW5uZXJJRBgBIAEoDxITCgtvcmRlcldlaWdo" + "dBgCIAEoDxIPCgdkZXNjSWQxGAMgASgPEg8KB2Rlc2NJZDIYBCABKA8SLQoJ" + "YmVnaW5UaW1lGAUgASgLMhouZ29vZ2xlLnByb3RvYnVmLlRpbWVzdGFtcBIr" + "CgdlbmRUaW1lGAYgASgLMhouZ29vZ2xlLnByb3RvYnVmLlRpbWVzdGFtcBIN" + "CgVzdHlsZRgHIAEoDxIUCgxiYW5uZXJJbWFnZXMYCCADKAkSGAoQYmFubmVy" + "SW1hZ2VzX3NmdxgJIAMoCRILCgN3YXkYCiABKA8i9wEKHlByb2R1Y3RSZWNv" + "bW1lbmRhdGlvbkNvbmZpZ3VyZRI2CgdCYW5uZXJzGAEgAygLMiUuUHJvZHVj" + "dFJlY29tbWVuZGF0aW9uQmFubmVyQ29uZmlndXJlEkMKCkJhbm5lckRpY3QY" + "AiADKAsyLy5Qcm9kdWN0UmVjb21tZW5kYXRpb25Db25maWd1cmUuQmFubmVy" + "RGljdEVudHJ5GlgKD0Jhbm5lckRpY3RFbnRyeRILCgNrZXkYASABKA8SNAoF" + "dmFsdWUYAiABKAsyJS5Qcm9kdWN0UmVjb21tZW5kYXRpb25CYW5uZXJDb25m" + "aWd1cmU6AjgBYgZwcm90bzM="), new FileDescriptor[1] { TimestampReflection.Descriptor }, new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[2]
		{
			new GeneratedClrTypeInfo(typeof(ProductRecommendationBannerConfigure), ProductRecommendationBannerConfigure.Parser, new string[10] { "BannerID", "OrderWeight", "DescId1", "DescId2", "BeginTime", "EndTime", "Style", "BannerImages", "BannerImagesSfw", "Way" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(ProductRecommendationConfigure), ProductRecommendationConfigure.Parser, new string[2] { "Banners", "BannerDict" }, null, null, null, new GeneratedClrTypeInfo[1])
		}));
	}
}
