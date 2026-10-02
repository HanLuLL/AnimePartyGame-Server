using System;
using Google.Protobuf.Reflection;

public static class PerformReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static PerformReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("Cg1QZXJmb3JtLnByb3RvGgpFbnVtLnByb3RvIpwFChRQZXJmb3JtSW5mb0Nv" + "bmZpZ3VyZRIKCgJpZBgBIAEoDxIRCgl0b3RhbFRpbWUYAiABKA8SEAoIaXNG" + "b2xsb3cYAyABKAgSFQoNaXNBbHRlcm5hdGVseRgEIAEoCBISCgpzd2l0Y2hU" + "aW1lGAUgASgPEjsKC0N1c3RvbVNob3dzGAYgAygLMiYuUGVyZm9ybUluZm9D" + "b25maWd1cmUuQ3VzdG9tU2hvd3NFbnRyeRI5CgphbmltYXRpb25zGAcgAygL" + "MiUuUGVyZm9ybUluZm9Db25maWd1cmUuQW5pbWF0aW9uc0VudHJ5EhcKD3Vw" + "ZGF0ZUF0dHJpYnV0ZRgIIAEoDxIzCgdlZmZlY3RzGAkgAygLMiIuUGVyZm9y" + "bUluZm9Db25maWd1cmUuRWZmZWN0c0VudHJ5EjkKCnNjcmVlblB1bXAYCiAD" + "KAsyJS5QZXJmb3JtSW5mb0NvbmZpZ3VyZS5TY3JlZW5QdW1wRW50cnkSLwoF" + "YXVkaW8YCyADKAsyIC5QZXJmb3JtSW5mb0NvbmZpZ3VyZS5BdWRpb0VudHJ5" + "GjIKEEN1c3RvbVNob3dzRW50cnkSCwoDa2V5GAEgASgJEg0KBXZhbHVlGAIg" + "ASgPOgI4ARoxCg9BbmltYXRpb25zRW50cnkSCwoDa2V5GAEgASgJEg0KBXZh" + "bHVlGAIgASgPOgI4ARouCgxFZmZlY3RzRW50cnkSCwoDa2V5GAEgASgPEg0K" + "BXZhbHVlGAIgASgPOgI4ARoxCg9TY3JlZW5QdW1wRW50cnkSCwoDa2V5GAEg" + "ASgJEg0KBXZhbHVlGAIgASgPOgI4ARosCgpBdWRpb0VudHJ5EgsKA2tleRgB" + "IAEoDxINCgV2YWx1ZRgCIAEoDzoCOAEi9gIKF1BlcmZvcm1UcmlnZ2VyQ29u" + "ZmlndXJlEgoKAmlkGAEgASgPEi8KEnBlcmZvcm1UcmlnZ2VyVHlwZRgCIAEo" + "DjITLlBlcmZvcm1UcmlnZ2VyVHlwZRIcChRwZXJmb3JtVHJpZ2dlclBhcmFt" + "cxgDIAMoDxIYChBhcHBseVByb2JhYmlsaXR5GAQgASgPEhAKCHByaW9yaXR5" + "GAUgASgPEjsKGHBlcmZvcm1UcmlnZ2VyQWN0aW9uVHlwZRgGIAEoDjIZLlBl" + "cmZvcm1UcmlnZ2VyQWN0aW9uVHlwZRIiChpwZXJmb3JtVHJpZ2dlckFjdGlv" + "blBhcmFtcxgHIAMoDxIRCgl0aW1lRGVsYXkYCCABKA8SMgoFYXVkaW8YCSAD" + "KAsyIy5QZXJmb3JtVHJpZ2dlckNvbmZpZ3VyZS5BdWRpb0VudHJ5GiwKCkF1" + "ZGlvRW50cnkSCwoDa2V5GAEgASgPEg0KBXZhbHVlGAIgASgPOgI4ASJyChpQ" + "ZXJmb3JtVHJpZ2dlclNldENvbmZpZ3VyZRIKCgJpZBgBIAEoDxJICh9wZXJm" + "b3JtVHJpZ2dlclNldENvbmZpZ3VyZUl0ZW1zGAIgAygLMh8uUGVyZm9ybVRy" + "aWdnZXJTZXRDb25maWd1cmVJdGVtIjMKHlBlcmZvcm1UcmlnZ2VyU2V0Q29u" + "ZmlndXJlSXRlbRIRCgl0cmlnZ2VySWQYASABKA8iqwQKEFBlcmZvcm1Db25m" + "aWd1cmUSJAoFSW5mb3MYASADKAsyFS5QZXJmb3JtSW5mb0NvbmZpZ3VyZRIx" + "CghJbmZvRGljdBgCIAMoCzIfLlBlcmZvcm1Db25maWd1cmUuSW5mb0RpY3RF" + "bnRyeRIqCghUcmlnZ2VycxgDIAMoCzIYLlBlcmZvcm1UcmlnZ2VyQ29uZmln" + "dXJlEjcKC1RyaWdnZXJEaWN0GAQgAygLMiIuUGVyZm9ybUNvbmZpZ3VyZS5U" + "cmlnZ2VyRGljdEVudHJ5EjAKC1RyaWdnZXJTZXRzGAUgAygLMhsuUGVyZm9y" + "bVRyaWdnZXJTZXRDb25maWd1cmUSPQoOVHJpZ2dlclNldERpY3QYBiADKAsy" + "JS5QZXJmb3JtQ29uZmlndXJlLlRyaWdnZXJTZXREaWN0RW50cnkaRgoNSW5m" + "b0RpY3RFbnRyeRILCgNrZXkYASABKA8SJAoFdmFsdWUYAiABKAsyFS5QZXJm" + "b3JtSW5mb0NvbmZpZ3VyZToCOAEaTAoQVHJpZ2dlckRpY3RFbnRyeRILCgNr" + "ZXkYASABKA8SJwoFdmFsdWUYAiABKAsyGC5QZXJmb3JtVHJpZ2dlckNvbmZp" + "Z3VyZToCOAEaUgoTVHJpZ2dlclNldERpY3RFbnRyeRILCgNrZXkYASABKA8S" + "KgoFdmFsdWUYAiABKAsyGy5QZXJmb3JtVHJpZ2dlclNldENvbmZpZ3VyZToC" + "OAFiBnByb3RvMw=="), new FileDescriptor[1] { EnumReflection.Descriptor }, new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[5]
		{
			new GeneratedClrTypeInfo(typeof(PerformInfoConfigure), PerformInfoConfigure.Parser, new string[11]
			{
				"Id", "TotalTime", "IsFollow", "IsAlternately", "SwitchTime", "CustomShows", "Animations", "UpdateAttribute", "Effects", "ScreenPump",
				"Audio"
			}, null, null, null, new GeneratedClrTypeInfo[5]),
			new GeneratedClrTypeInfo(typeof(PerformTriggerConfigure), PerformTriggerConfigure.Parser, new string[9] { "Id", "PerformTriggerType", "PerformTriggerParams", "ApplyProbability", "Priority", "PerformTriggerActionType", "PerformTriggerActionParams", "TimeDelay", "Audio" }, null, null, null, new GeneratedClrTypeInfo[1]),
			new GeneratedClrTypeInfo(typeof(PerformTriggerSetConfigure), PerformTriggerSetConfigure.Parser, new string[2] { "Id", "PerformTriggerSetConfigureItems" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(PerformTriggerSetConfigureItem), PerformTriggerSetConfigureItem.Parser, new string[1] { "TriggerId" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(PerformConfigure), PerformConfigure.Parser, new string[6] { "Infos", "InfoDict", "Triggers", "TriggerDict", "TriggerSets", "TriggerSetDict" }, null, null, null, new GeneratedClrTypeInfo[3])
		}));
	}
}
