using System;
using Google.Protobuf.Reflection;
using Google.Protobuf.WellKnownTypes;

public static class CollaborationReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static CollaborationReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("ChNDb2xsYWJvcmF0aW9uLnByb3RvGh9nb29nbGUvcHJvdG9idWYvdGltZXN0" + "YW1wLnByb3RvGgpFbnVtLnByb3RvIpUCChpDb2xsYWJvcmF0aW9uSW5mb0Nv" + "bmZpZ3VyZRIKCgJpZBgBIAEoDxIjCgx1SVdpbmRvd1R5cGUYAiABKA4yDS5V" + "SVdpbmRvd1R5cGUSLQoJYmVnaW5UaW1lGAMgASgLMhouZ29vZ2xlLnByb3Rv" + "YnVmLlRpbWVzdGFtcBIrCgdlbmRUaW1lGAQgASgLMhouZ29vZ2xlLnByb3Rv" + "YnVmLlRpbWVzdGFtcBILCgN3YXkYBSABKA8SFAoMUHJldmlld0luZGV4GAYg" + "AygPEhAKCGVudHJhbmNlGAcgASgJEhUKDWVudHJhbmNlVGl0bGUYCCABKA8S" + "DgoGaGVyb0lEGAkgAygPEg4KBmJnTGlzdBgKIAMoCSKNBQobQ29sbGFib3Jh" + "dGlvbkdvb2RzQ29uZmlndXJlEg0KBUluZGV4GAEgASgPEg8KB0dvb2RzSWQY" + "AiABKA8SDgoGaGVyb0lEGAMgAygPEhAKCGhlcm9OYW1lGAQgASgPEhQKDG11" + "dGlIZXJvTmFtZRgFIAMoDxIVCg1wbGF5ZXJQaG90b0lEGAYgAygPEhsKE2Fj" + "Y291bnRCYWNrZ3JvdW5kSUQYByADKA8SHgoWcGhvdG9BbmRCYWNrZ3JvdW5k" + "TmFtZRgIIAEoDxJECgxvdGhlclJld2FyZHMYCSADKAsyLi5Db2xsYWJvcmF0" + "aW9uR29vZHNDb25maWd1cmUuT3RoZXJSZXdhcmRzRW50cnkSDwoHZW1vamlJ" + "RBgKIAMoDxJYChZleHRyYUJhY2tncm91bmRSZXdhcmRzGAsgAygLMjguQ29s" + "bGFib3JhdGlvbkdvb2RzQ29uZmlndXJlLkV4dHJhQmFja2dyb3VuZFJld2Fy" + "ZHNFbnRyeRJMChBleHRyYUl0ZW1SZXdhcmRzGAwgAygLMjIuQ29sbGFib3Jh" + "dGlvbkdvb2RzQ29uZmlndXJlLkV4dHJhSXRlbVJld2FyZHNFbnRyeRIWCg5y" + "ZWxhdGVkR29vZHNJZBgNIAEoDxozChFPdGhlclJld2FyZHNFbnRyeRILCgNr" + "ZXkYASABKA8SDQoFdmFsdWUYAiABKA86AjgBGj0KG0V4dHJhQmFja2dyb3Vu" + "ZFJld2FyZHNFbnRyeRILCgNrZXkYASABKA8SDQoFdmFsdWUYAiABKA86AjgB" + "GjcKFUV4dHJhSXRlbVJld2FyZHNFbnRyeRILCgNrZXkYASABKA8SDQoFdmFs" + "dWUYAiABKA86AjgBIoQDChZDb2xsYWJvcmF0aW9uQ29uZmlndXJlEioKBUlu" + "Zm9zGAEgAygLMhsuQ29sbGFib3JhdGlvbkluZm9Db25maWd1cmUSNwoISW5m" + "b0RpY3QYAiADKAsyJS5Db2xsYWJvcmF0aW9uQ29uZmlndXJlLkluZm9EaWN0" + "RW50cnkSLAoGR29vZHNzGAMgAygLMhwuQ29sbGFib3JhdGlvbkdvb2RzQ29u" + "ZmlndXJlEjkKCUdvb2RzRGljdBgEIAMoCzImLkNvbGxhYm9yYXRpb25Db25m" + "aWd1cmUuR29vZHNEaWN0RW50cnkaTAoNSW5mb0RpY3RFbnRyeRILCgNrZXkY" + "ASABKA8SKgoFdmFsdWUYAiABKAsyGy5Db2xsYWJvcmF0aW9uSW5mb0NvbmZp" + "Z3VyZToCOAEaTgoOR29vZHNEaWN0RW50cnkSCwoDa2V5GAEgASgPEisKBXZh" + "bHVlGAIgASgLMhwuQ29sbGFib3JhdGlvbkdvb2RzQ29uZmlndXJlOgI4AWIG" + "cHJvdG8z"), new FileDescriptor[2]
		{
			TimestampReflection.Descriptor,
			EnumReflection.Descriptor
		}, new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[3]
		{
			new GeneratedClrTypeInfo(typeof(CollaborationInfoConfigure), CollaborationInfoConfigure.Parser, new string[10] { "Id", "UIWindowType", "BeginTime", "EndTime", "Way", "PreviewIndex", "Entrance", "EntranceTitle", "HeroID", "BgList" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(CollaborationGoodsConfigure), CollaborationGoodsConfigure.Parser, new string[13]
			{
				"Index", "GoodsId", "HeroID", "HeroName", "MutiHeroName", "PlayerPhotoID", "AccountBackgroundID", "PhotoAndBackgroundName", "OtherRewards", "EmojiID",
				"ExtraBackgroundRewards", "ExtraItemRewards", "RelatedGoodsId"
			}, null, null, null, new GeneratedClrTypeInfo[3]),
			new GeneratedClrTypeInfo(typeof(CollaborationConfigure), CollaborationConfigure.Parser, new string[4] { "Infos", "InfoDict", "Goodss", "GoodsDict" }, null, null, null, new GeneratedClrTypeInfo[2])
		}));
	}
}
