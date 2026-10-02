using System;
using Google.Protobuf.Reflection;

public static class BattlePassNewReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static BattlePassNewReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("ChNCYXR0bGVQYXNzTmV3LnByb3RvGgpFbnVtLnByb3RvIpUBChpCYXR0bGVQ" + "YXNzTmV3SW5mb0NvbmZpZ3VyZRIKCgJpZBgBIAEoDxIhCgtzaG9wVGFiVHlw" + "ZRgCIAEoDjIMLlNob3BUYWJUeXBlEkgKH2JhdHRsZVBhc3NOZXdJbmZvQ29u" + "ZmlndXJlSXRlbXMYAyADKAsyHy5CYXR0bGVQYXNzTmV3SW5mb0NvbmZpZ3Vy" + "ZUl0ZW0i0gIKHkJhdHRsZVBhc3NOZXdJbmZvQ29uZmlndXJlSXRlbRIOCgZ0" + "YXNrSWQYASABKA8SRQoLZnJlZVJld2FyZHMYAiADKAsyMC5CYXR0bGVQYXNz" + "TmV3SW5mb0NvbmZpZ3VyZUl0ZW0uRnJlZVJld2FyZHNFbnRyeRJLCg5wcmVt" + "aXVtUmV3YXJkcxgDIAMoCzIzLkJhdHRsZVBhc3NOZXdJbmZvQ29uZmlndXJl" + "SXRlbS5QcmVtaXVtUmV3YXJkc0VudHJ5EhIKCmFjdGl2aXR5SWQYBCABKA8S" + "DQoFcGFyYW0YBSABKA8aMgoQRnJlZVJld2FyZHNFbnRyeRILCgNrZXkYASAB" + "KA8SDQoFdmFsdWUYAiABKA86AjgBGjUKE1ByZW1pdW1SZXdhcmRzRW50cnkS" + "CwoDa2V5GAEgASgPEg0KBXZhbHVlGAIgASgPOgI4ASLLAQoWQmF0dGxlUGFz" + "c05ld0NvbmZpZ3VyZRIqCgVJbmZvcxgBIAMoCzIbLkJhdHRsZVBhc3NOZXdJ" + "bmZvQ29uZmlndXJlEjcKCEluZm9EaWN0GAIgAygLMiUuQmF0dGxlUGFzc05l" + "d0NvbmZpZ3VyZS5JbmZvRGljdEVudHJ5GkwKDUluZm9EaWN0RW50cnkSCwoD" + "a2V5GAEgASgPEioKBXZhbHVlGAIgASgLMhsuQmF0dGxlUGFzc05ld0luZm9D" + "b25maWd1cmU6AjgBYgZwcm90bzM="), new FileDescriptor[1] { EnumReflection.Descriptor }, new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[3]
		{
			new GeneratedClrTypeInfo(typeof(BattlePassNewInfoConfigure), BattlePassNewInfoConfigure.Parser, new string[3] { "Id", "ShopTabType", "BattlePassNewInfoConfigureItems" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(BattlePassNewInfoConfigureItem), BattlePassNewInfoConfigureItem.Parser, new string[5] { "TaskId", "FreeRewards", "PremiumRewards", "ActivityId", "Param" }, null, null, null, new GeneratedClrTypeInfo[2]),
			new GeneratedClrTypeInfo(typeof(BattlePassNewConfigure), BattlePassNewConfigure.Parser, new string[2] { "Infos", "InfoDict" }, null, null, null, new GeneratedClrTypeInfo[1])
		}));
	}
}
