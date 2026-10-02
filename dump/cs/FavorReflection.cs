using System;
using Google.Protobuf.Reflection;

public static class FavorReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static FavorReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("CgtGYXZvci5wcm90bxoKRW51bS5wcm90byJEChNGYXZvckxldmVsQ29uZmln" + "dXJlEgoKAmlkGAEgASgPEg8KB25lZWRFeHAYAiABKA8SEAoIdG90YWxFeHAY" + "AyABKA8ibwoZRmF2b3JMZXZlbFJld2FyZENvbmZpZ3VyZRIKCgJpZBgBIAEo" + "DxJGCh5mYXZvckxldmVsUmV3YXJkQ29uZmlndXJlSXRlbXMYAiADKAsyHi5G" + "YXZvckxldmVsUmV3YXJkQ29uZmlndXJlSXRlbSKhAQodRmF2b3JMZXZlbFJl" + "d2FyZENvbmZpZ3VyZUl0ZW0SEgoKZmF2b3JMZXZlbBgBIAEoDxI8CgdyZXdh" + "cmRzGAIgAygLMisuRmF2b3JMZXZlbFJld2FyZENvbmZpZ3VyZUl0ZW0uUmV3" + "YXJkc0VudHJ5Gi4KDFJld2FyZHNFbnRyeRILCgNrZXkYASABKA8SDQoFdmFs" + "dWUYAiABKA86AjgBIpACChpGYXZvckJyZWFrdGhyb3VnaENvbmZpZ3VyZRIK" + "CgJpZBgBIAEoDxJFCg1uZWVkTWF0ZXJpYWxzGAIgAygLMi4uRmF2b3JCcmVh" + "a3Rocm91Z2hDb25maWd1cmUuTmVlZE1hdGVyaWFsc0VudHJ5EjkKB3Jld2Fy" + "ZHMYAyADKAsyKC5GYXZvckJyZWFrdGhyb3VnaENvbmZpZ3VyZS5SZXdhcmRz" + "RW50cnkaNAoSTmVlZE1hdGVyaWFsc0VudHJ5EgsKA2tleRgBIAEoDxINCgV2" + "YWx1ZRgCIAEoDzoCOAEaLgoMUmV3YXJkc0VudHJ5EgsKA2tleRgBIAEoDxIN" + "CgV2YWx1ZRgCIAEoDzoCOAEiSAoSRmF2b3JHaWZ0Q29uZmlndXJlEgoKAmlk" + "GAEgASgHEhMKC25vcm1hbEZhdm9yGAIgASgHEhEKCWxpa2VGYXZvchgDIAEo" + "ByJdChFGYXZvcldheUNvbmZpZ3VyZRIKCgJpZBgBIAEoBxIPCgd3YXlMaXN0" + "GAIgAygPEhkKB3dheVR5cGUYAyABKA4yCC5XYXlUeXBlEhAKCHdheVBhcmFt" + "GAQgAygPIvEGCg5GYXZvckNvbmZpZ3VyZRIkCgZMZXZlbHMYASADKAsyFC5G" + "YXZvckxldmVsQ29uZmlndXJlEjEKCUxldmVsRGljdBgCIAMoCzIeLkZhdm9y" + "Q29uZmlndXJlLkxldmVsRGljdEVudHJ5EjAKDExldmVsUmV3YXJkcxgDIAMo" + "CzIaLkZhdm9yTGV2ZWxSZXdhcmRDb25maWd1cmUSPQoPTGV2ZWxSZXdhcmRE" + "aWN0GAQgAygLMiQuRmF2b3JDb25maWd1cmUuTGV2ZWxSZXdhcmREaWN0RW50" + "cnkSMgoNQnJlYWt0aHJvdWdocxgFIAMoCzIbLkZhdm9yQnJlYWt0aHJvdWdo" + "Q29uZmlndXJlEj8KEEJyZWFrdGhyb3VnaERpY3QYBiADKAsyJS5GYXZvckNv" + "bmZpZ3VyZS5CcmVha3Rocm91Z2hEaWN0RW50cnkSIgoFR2lmdHMYByADKAsy" + "Ey5GYXZvckdpZnRDb25maWd1cmUSLwoIR2lmdERpY3QYCCADKAsyHS5GYXZv" + "ckNvbmZpZ3VyZS5HaWZ0RGljdEVudHJ5EiAKBFdheXMYCSADKAsyEi5GYXZv" + "cldheUNvbmZpZ3VyZRItCgdXYXlEaWN0GAogAygLMhwuRmF2b3JDb25maWd1" + "cmUuV2F5RGljdEVudHJ5GkYKDkxldmVsRGljdEVudHJ5EgsKA2tleRgBIAEo" + "DxIjCgV2YWx1ZRgCIAEoCzIULkZhdm9yTGV2ZWxDb25maWd1cmU6AjgBGlIK" + "FExldmVsUmV3YXJkRGljdEVudHJ5EgsKA2tleRgBIAEoDxIpCgV2YWx1ZRgC" + "IAEoCzIaLkZhdm9yTGV2ZWxSZXdhcmRDb25maWd1cmU6AjgBGlQKFUJyZWFr" + "dGhyb3VnaERpY3RFbnRyeRILCgNrZXkYASABKA8SKgoFdmFsdWUYAiABKAsy" + "Gy5GYXZvckJyZWFrdGhyb3VnaENvbmZpZ3VyZToCOAEaRAoNR2lmdERpY3RF" + "bnRyeRILCgNrZXkYASABKA8SIgoFdmFsdWUYAiABKAsyEy5GYXZvckdpZnRD" + "b25maWd1cmU6AjgBGkIKDFdheURpY3RFbnRyeRILCgNrZXkYASABKA8SIQoF" + "dmFsdWUYAiABKAsyEi5GYXZvcldheUNvbmZpZ3VyZToCOAFiBnByb3RvMw=="), new FileDescriptor[1] { EnumReflection.Descriptor }, new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[7]
		{
			new GeneratedClrTypeInfo(typeof(FavorLevelConfigure), FavorLevelConfigure.Parser, new string[3] { "Id", "NeedExp", "TotalExp" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(FavorLevelRewardConfigure), FavorLevelRewardConfigure.Parser, new string[2] { "Id", "FavorLevelRewardConfigureItems" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(FavorLevelRewardConfigureItem), FavorLevelRewardConfigureItem.Parser, new string[2] { "FavorLevel", "Rewards" }, null, null, null, new GeneratedClrTypeInfo[1]),
			new GeneratedClrTypeInfo(typeof(FavorBreakthroughConfigure), FavorBreakthroughConfigure.Parser, new string[3] { "Id", "NeedMaterials", "Rewards" }, null, null, null, new GeneratedClrTypeInfo[2]),
			new GeneratedClrTypeInfo(typeof(FavorGiftConfigure), FavorGiftConfigure.Parser, new string[3] { "Id", "NormalFavor", "LikeFavor" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(FavorWayConfigure), FavorWayConfigure.Parser, new string[4] { "Id", "WayList", "WayType", "WayParam" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(FavorConfigure), FavorConfigure.Parser, new string[10] { "Levels", "LevelDict", "LevelRewards", "LevelRewardDict", "Breakthroughs", "BreakthroughDict", "Gifts", "GiftDict", "Ways", "WayDict" }, null, null, null, new GeneratedClrTypeInfo[5])
		}));
	}
}
