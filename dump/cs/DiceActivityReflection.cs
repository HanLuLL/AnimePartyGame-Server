using System;
using Google.Protobuf.Reflection;

public static class DiceActivityReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static DiceActivityReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("ChJEaWNlQWN0aXZpdHkucHJvdG8aCkVudW0ucHJvdG8iegoZRGljZUFjdGl2" + "aXR5SW5mb0NvbmZpZ3VyZRIKCgJpZBgBIAEoDxINCgVNYXBJRBgCIAEoDxIQ" + "CghEaWNlQ29zdBgDIAEoDxILCgNFWFAYBCABKA8SEQoJRVhQUmVwZWF0GAUg" + "ASgPEhAKCGhlYWRsaW5lGAYgASgPInIKGURpY2VBY3Rpdml0eURhdGFDb25m" + "aWd1cmUSDQoFTWFwSUQYASABKA8SRgoeZGljZUFjdGl2aXR5RGF0YUNvbmZp" + "Z3VyZUl0ZW1zGAIgAygLMh4uRGljZUFjdGl2aXR5RGF0YUNvbmZpZ3VyZUl0" + "ZW0izQEKHURpY2VBY3Rpdml0eURhdGFDb25maWd1cmVJdGVtEhIKCkRpY2VM" + "YW5kSUQYASABKA8SHwoITGFuZFR5cGUYAiABKA4yDS5EaWNlTGFuZFR5cGUS" + "DAoEaWNvbhgDIAEoCRI6CgZSZXdhcmQYBCADKAsyKi5EaWNlQWN0aXZpdHlE" + "YXRhQ29uZmlndXJlSXRlbS5SZXdhcmRFbnRyeRotCgtSZXdhcmRFbnRyeRIL" + "CgNrZXkYASABKA8SDQoFdmFsdWUYAiABKA86AjgBIvcCChVEaWNlQWN0aXZp" + "dHlDb25maWd1cmUSKQoFSW5mb3MYASADKAsyGi5EaWNlQWN0aXZpdHlJbmZv" + "Q29uZmlndXJlEjYKCEluZm9EaWN0GAIgAygLMiQuRGljZUFjdGl2aXR5Q29u" + "ZmlndXJlLkluZm9EaWN0RW50cnkSKQoFRGF0YXMYAyADKAsyGi5EaWNlQWN0" + "aXZpdHlEYXRhQ29uZmlndXJlEjYKCERhdGFEaWN0GAQgAygLMiQuRGljZUFj" + "dGl2aXR5Q29uZmlndXJlLkRhdGFEaWN0RW50cnkaSwoNSW5mb0RpY3RFbnRy" + "eRILCgNrZXkYASABKA8SKQoFdmFsdWUYAiABKAsyGi5EaWNlQWN0aXZpdHlJ" + "bmZvQ29uZmlndXJlOgI4ARpLCg1EYXRhRGljdEVudHJ5EgsKA2tleRgBIAEo" + "DxIpCgV2YWx1ZRgCIAEoCzIaLkRpY2VBY3Rpdml0eURhdGFDb25maWd1cmU6" + "AjgBYgZwcm90bzM="), new FileDescriptor[1] { EnumReflection.Descriptor }, new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[4]
		{
			new GeneratedClrTypeInfo(typeof(DiceActivityInfoConfigure), DiceActivityInfoConfigure.Parser, new string[6] { "Id", "MapID", "DiceCost", "EXP", "EXPRepeat", "Headline" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(DiceActivityDataConfigure), DiceActivityDataConfigure.Parser, new string[2] { "MapID", "DiceActivityDataConfigureItems" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(DiceActivityDataConfigureItem), DiceActivityDataConfigureItem.Parser, new string[4] { "DiceLandID", "LandType", "Icon", "Reward" }, null, null, null, new GeneratedClrTypeInfo[1]),
			new GeneratedClrTypeInfo(typeof(DiceActivityConfigure), DiceActivityConfigure.Parser, new string[4] { "Infos", "InfoDict", "Datas", "DataDict" }, null, null, null, new GeneratedClrTypeInfo[2])
		}));
	}
}
