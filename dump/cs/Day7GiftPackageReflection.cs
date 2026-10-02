using System;
using Google.Protobuf.Reflection;

public static class Day7GiftPackageReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static Day7GiftPackageReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("ChVEYXk3R2lmdFBhY2thZ2UucHJvdG8iwAEKHURheTdHaWZ0UGFja2FnZUdv" + "b2RzQ29uZmlndXJlEg8KB2dvb2RzSUQYASABKA8SDwoHdGl0bGVJRBgCIAEo" + "DxIVCg1kZXNjcmlwdGlvbklEGAMgASgPEhYKDnZhbGlkaXR5UGVyaW9kGAQg" + "ASgPEk4KImRheTdHaWZ0UGFja2FnZUdvb2RzQ29uZmlndXJlSXRlbXMYBSAD" + "KAsyIi5EYXk3R2lmdFBhY2thZ2VHb29kc0NvbmZpZ3VyZUl0ZW0iowEKIURh" + "eTdHaWZ0UGFja2FnZUdvb2RzQ29uZmlndXJlSXRlbRIPCgdkYXlOdW1iGAEg" + "ASgPEj4KBnJld2FyZBgCIAMoCzIuLkRheTdHaWZ0UGFja2FnZUdvb2RzQ29u" + "ZmlndXJlSXRlbS5SZXdhcmRFbnRyeRotCgtSZXdhcmRFbnRyeRILCgNrZXkY" + "ASABKA8SDQoFdmFsdWUYAiABKA86AjgBItkBChhEYXk3R2lmdFBhY2thZ2VD" + "b25maWd1cmUSLgoGR29vZHNzGAEgAygLMh4uRGF5N0dpZnRQYWNrYWdlR29v" + "ZHNDb25maWd1cmUSOwoJR29vZHNEaWN0GAIgAygLMiguRGF5N0dpZnRQYWNr" + "YWdlQ29uZmlndXJlLkdvb2RzRGljdEVudHJ5GlAKDkdvb2RzRGljdEVudHJ5" + "EgsKA2tleRgBIAEoDxItCgV2YWx1ZRgCIAEoCzIeLkRheTdHaWZ0UGFja2Fn" + "ZUdvb2RzQ29uZmlndXJlOgI4AWIGcHJvdG8z"), new FileDescriptor[0], new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[3]
		{
			new GeneratedClrTypeInfo(typeof(Day7GiftPackageGoodsConfigure), Day7GiftPackageGoodsConfigure.Parser, new string[5] { "GoodsID", "TitleID", "DescriptionID", "ValidityPeriod", "Day7GiftPackageGoodsConfigureItems" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(Day7GiftPackageGoodsConfigureItem), Day7GiftPackageGoodsConfigureItem.Parser, new string[2] { "DayNumb", "Reward" }, null, null, null, new GeneratedClrTypeInfo[1]),
			new GeneratedClrTypeInfo(typeof(Day7GiftPackageConfigure), Day7GiftPackageConfigure.Parser, new string[2] { "Goodss", "GoodsDict" }, null, null, null, new GeneratedClrTypeInfo[1])
		}));
	}
}
