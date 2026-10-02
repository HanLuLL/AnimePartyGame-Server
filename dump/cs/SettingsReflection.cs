using System;
using Google.Protobuf.Reflection;

public static class SettingsReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static SettingsReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("Cg5TZXR0aW5ncy5wcm90bxoKRW51bS5wcm90byJfChtTZXR0aW5nc1Jlc29s" + "dXRpb25Db25maWd1cmUSCgoCaWQYASABKA8SDQoFd2lkdGgYAiABKA8SDgoG" + "aGVpZ2h0GAMgASgPEhUKDWRlc2NyaXB0aW9uSUQYBCABKA8iUAocU2V0dGlu" + "Z3NSZWZyZXNoUmF0ZUNvbmZpZ3VyZRIKCgJpZBgBIAEoDxINCgV2YWx1ZRgC" + "IAEoDxIVCg1kZXNjcmlwdGlvbklEGAMgASgPIlUKHFNldHRpbmdzRGlzcGxh" + "eU1vZGVDb25maWd1cmUSCgoCaWQYASABKA8SEgoKc2NyZWVuTW9kZRgCIAEo" + "DxIVCg1kZXNjcmlwdGlvbklEGAMgASgPIoIBChlTZXR0aW5nc0xhbmd1YWdl" + "Q29uZmlndXJlEiMKDGxhbmd1YWdlVHlwZRgBIAEoDjINLkxhbmd1YWdlVHlw" + "ZRIRCglkYXRhSW5kZXgYAiABKA8SFQoNZGVzY3JpcHRpb25JRBgDIAEoDxIW" + "Cg5maWxlQ29udGVudEtleRgEIAEoCSJOChdTZXR0aW5nc0FuY2hvckNvbmZp" + "Z3VyZRIKCgJpZBgBIAEoDxIQCghpc0FuY2hvchgCIAEoCBIVCg1kZXNjcmlw" + "dGlvbklEGAMgASgPIj0KGFNldHRpbmdzUXVhbGl0eUNvbmZpZ3VyZRIKCgJp" + "ZBgBIAEoDxIVCg1kZXNjcmlwdGlvbklEGAIgASgPIm8KHlNldHRpbmdzVm9p" + "Y2VMYW5ndWFnZUNvbmZpZ3VyZRIjCgxsYW5ndWFnZVR5cGUYASABKA4yDS5M" + "YW5ndWFnZVR5cGUSEQoJZGF0YUluZGV4GAIgASgPEhUKDWRlc2NyaXB0aW9u" + "SUQYAyABKA8i8woKEVNldHRpbmdzQ29uZmlndXJlEjEKC1Jlc29sdXRpb25z" + "GAEgAygLMhwuU2V0dGluZ3NSZXNvbHV0aW9uQ29uZmlndXJlEj4KDlJlc29s" + "dXRpb25EaWN0GAIgAygLMiYuU2V0dGluZ3NDb25maWd1cmUuUmVzb2x1dGlv" + "bkRpY3RFbnRyeRIzCgxSZWZyZXNoUmF0ZXMYAyADKAsyHS5TZXR0aW5nc1Jl" + "ZnJlc2hSYXRlQ29uZmlndXJlEkAKD1JlZnJlc2hSYXRlRGljdBgEIAMoCzIn" + "LlNldHRpbmdzQ29uZmlndXJlLlJlZnJlc2hSYXRlRGljdEVudHJ5EjMKDERp" + "c3BsYXlNb2RlcxgFIAMoCzIdLlNldHRpbmdzRGlzcGxheU1vZGVDb25maWd1" + "cmUSQAoPRGlzcGxheU1vZGVEaWN0GAYgAygLMicuU2V0dGluZ3NDb25maWd1" + "cmUuRGlzcGxheU1vZGVEaWN0RW50cnkSLQoJTGFuZ3VhZ2VzGAcgAygLMhou" + "U2V0dGluZ3NMYW5ndWFnZUNvbmZpZ3VyZRI6CgxMYW5ndWFnZURpY3QYCCAD" + "KAsyJC5TZXR0aW5nc0NvbmZpZ3VyZS5MYW5ndWFnZURpY3RFbnRyeRIpCgdB" + "bmNob3JzGAkgAygLMhguU2V0dGluZ3NBbmNob3JDb25maWd1cmUSNgoKQW5j" + "aG9yRGljdBgKIAMoCzIiLlNldHRpbmdzQ29uZmlndXJlLkFuY2hvckRpY3RF" + "bnRyeRIrCghRdWFsaXR5cxgLIAMoCzIZLlNldHRpbmdzUXVhbGl0eUNvbmZp" + "Z3VyZRI4CgtRdWFsaXR5RGljdBgMIAMoCzIjLlNldHRpbmdzQ29uZmlndXJl" + "LlF1YWxpdHlEaWN0RW50cnkSNwoOVm9pY2VMYW5ndWFnZXMYDSADKAsyHy5T" + "ZXR0aW5nc1ZvaWNlTGFuZ3VhZ2VDb25maWd1cmUSRAoRVm9pY2VMYW5ndWFn" + "ZURpY3QYDiADKAsyKS5TZXR0aW5nc0NvbmZpZ3VyZS5Wb2ljZUxhbmd1YWdl" + "RGljdEVudHJ5GlMKE1Jlc29sdXRpb25EaWN0RW50cnkSCwoDa2V5GAEgASgP" + "EisKBXZhbHVlGAIgASgLMhwuU2V0dGluZ3NSZXNvbHV0aW9uQ29uZmlndXJl" + "OgI4ARpVChRSZWZyZXNoUmF0ZURpY3RFbnRyeRILCgNrZXkYASABKA8SLAoF" + "dmFsdWUYAiABKAsyHS5TZXR0aW5nc1JlZnJlc2hSYXRlQ29uZmlndXJlOgI4" + "ARpVChREaXNwbGF5TW9kZURpY3RFbnRyeRILCgNrZXkYASABKA8SLAoFdmFs" + "dWUYAiABKAsyHS5TZXR0aW5nc0Rpc3BsYXlNb2RlQ29uZmlndXJlOgI4ARpP" + "ChFMYW5ndWFnZURpY3RFbnRyeRILCgNrZXkYASABKA8SKQoFdmFsdWUYAiAB" + "KAsyGi5TZXR0aW5nc0xhbmd1YWdlQ29uZmlndXJlOgI4ARpLCg9BbmNob3JE" + "aWN0RW50cnkSCwoDa2V5GAEgASgPEicKBXZhbHVlGAIgASgLMhguU2V0dGlu" + "Z3NBbmNob3JDb25maWd1cmU6AjgBGk0KEFF1YWxpdHlEaWN0RW50cnkSCwoD" + "a2V5GAEgASgPEigKBXZhbHVlGAIgASgLMhkuU2V0dGluZ3NRdWFsaXR5Q29u" + "ZmlndXJlOgI4ARpZChZWb2ljZUxhbmd1YWdlRGljdEVudHJ5EgsKA2tleRgB" + "IAEoDxIuCgV2YWx1ZRgCIAEoCzIfLlNldHRpbmdzVm9pY2VMYW5ndWFnZUNv" + "bmZpZ3VyZToCOAFiBnByb3RvMw=="), new FileDescriptor[1] { EnumReflection.Descriptor }, new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[8]
		{
			new GeneratedClrTypeInfo(typeof(SettingsResolutionConfigure), SettingsResolutionConfigure.Parser, new string[4] { "Id", "Width", "Height", "DescriptionID" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(SettingsRefreshRateConfigure), SettingsRefreshRateConfigure.Parser, new string[3] { "Id", "Value", "DescriptionID" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(SettingsDisplayModeConfigure), SettingsDisplayModeConfigure.Parser, new string[3] { "Id", "ScreenMode", "DescriptionID" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(SettingsLanguageConfigure), SettingsLanguageConfigure.Parser, new string[4] { "LanguageType", "DataIndex", "DescriptionID", "FileContentKey" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(SettingsAnchorConfigure), SettingsAnchorConfigure.Parser, new string[3] { "Id", "IsAnchor", "DescriptionID" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(SettingsQualityConfigure), SettingsQualityConfigure.Parser, new string[2] { "Id", "DescriptionID" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(SettingsVoiceLanguageConfigure), SettingsVoiceLanguageConfigure.Parser, new string[3] { "LanguageType", "DataIndex", "DescriptionID" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(SettingsConfigure), SettingsConfigure.Parser, new string[14]
			{
				"Resolutions", "ResolutionDict", "RefreshRates", "RefreshRateDict", "DisplayModes", "DisplayModeDict", "Languages", "LanguageDict", "Anchors", "AnchorDict",
				"Qualitys", "QualityDict", "VoiceLanguages", "VoiceLanguageDict"
			}, null, null, null, new GeneratedClrTypeInfo[7])
		}));
	}
}
