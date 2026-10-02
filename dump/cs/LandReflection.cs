using System;
using Google.Protobuf.Reflection;

public static class LandReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static LandReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("CgpMYW5kLnByb3RvGgpFbnVtLnByb3RvIqACChFMYW5kSW5mb0NvbmZpZ3Vy" + "ZRIbCghsYW5kVHlwZRgBIAEoDjIJLkxhbmRUeXBlEh4KFnBsYXRmb3JtTWFp" + "blRleF9PZmZzZXQYAiADKA8SFQoNaXNHYWxsZXJ5U2hvdxgDIAEoCBIQCghp" + "c1NQTGFuZBgEIAEoCBIOCgZuYW1lSUQYBSABKA8SFQoNZGVzY3JpcHRpb25J" + "RBgGIAEoDxIRCglVSUVsZW1lbnQYByABKAkSFAoMc2Z3VUlFbGVtZW50GAgg" + "ASgJEhAKCGxhbmRJY29uGAkgASgJEg8KB0xhbmRTZngYCiABKA8SDgoGcGFy" + "YW1zGAsgAygREhAKCHBlcmZvcm0xGAwgASgPEhAKCHBlcmZvcm0yGA0gASgP" + "IjEKFUxhbmRSb2xsR29sZENvbmZpZ3VyZRIKCgJpZBgBIAEoDxIMCgRudW1i" + "GAIgASgPItcCCg1MYW5kQ29uZmlndXJlEiEKBUluZm9zGAEgAygLMhIuTGFu" + "ZEluZm9Db25maWd1cmUSLgoISW5mb0RpY3QYAiADKAsyHC5MYW5kQ29uZmln" + "dXJlLkluZm9EaWN0RW50cnkSKQoJUm9sbEdvbGRzGAMgAygLMhYuTGFuZFJv" + "bGxHb2xkQ29uZmlndXJlEjYKDFJvbGxHb2xkRGljdBgEIAMoCzIgLkxhbmRD" + "b25maWd1cmUuUm9sbEdvbGREaWN0RW50cnkaQwoNSW5mb0RpY3RFbnRyeRIL" + "CgNrZXkYASABKA8SIQoFdmFsdWUYAiABKAsyEi5MYW5kSW5mb0NvbmZpZ3Vy" + "ZToCOAEaSwoRUm9sbEdvbGREaWN0RW50cnkSCwoDa2V5GAEgASgPEiUKBXZh" + "bHVlGAIgASgLMhYuTGFuZFJvbGxHb2xkQ29uZmlndXJlOgI4AWIGcHJvdG8z"), new FileDescriptor[1] { EnumReflection.Descriptor }, new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[3]
		{
			new GeneratedClrTypeInfo(typeof(LandInfoConfigure), LandInfoConfigure.Parser, new string[13]
			{
				"LandType", "PlatformMainTexOffset", "IsGalleryShow", "IsSPLand", "NameID", "DescriptionID", "UIElement", "SfwUIElement", "LandIcon", "LandSfx",
				"Params", "Perform1", "Perform2"
			}, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(LandRollGoldConfigure), LandRollGoldConfigure.Parser, new string[2] { "Id", "Numb" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(LandConfigure), LandConfigure.Parser, new string[4] { "Infos", "InfoDict", "RollGolds", "RollGoldDict" }, null, null, null, new GeneratedClrTypeInfo[2])
		}));
	}
}
