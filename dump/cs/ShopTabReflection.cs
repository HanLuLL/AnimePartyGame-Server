using System;
using Google.Protobuf.Reflection;

public static class ShopTabReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static ShopTabReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("Cg1TaG9wVGFiLnByb3RvGgpFbnVtLnByb3RvImgKFFNob3BUYWJJbmZvQ29u" + "ZmlndXJlEiEKC3Nob3BUYWJUeXBlGAEgASgOMgwuU2hvcFRhYlR5cGUSLQoR" + "Z29vZHNQdXJjaGFzZVR5cGUYAiABKA4yEi5Hb29kc1B1cmNoYXNlVHlwZSKz" + "AQoQU2hvcFRhYkNvbmZpZ3VyZRIkCgVJbmZvcxgBIAMoCzIVLlNob3BUYWJJ" + "bmZvQ29uZmlndXJlEjEKCEluZm9EaWN0GAIgAygLMh8uU2hvcFRhYkNvbmZp" + "Z3VyZS5JbmZvRGljdEVudHJ5GkYKDUluZm9EaWN0RW50cnkSCwoDa2V5GAEg" + "ASgPEiQKBXZhbHVlGAIgASgLMhUuU2hvcFRhYkluZm9Db25maWd1cmU6AjgB" + "YgZwcm90bzM="), new FileDescriptor[1] { EnumReflection.Descriptor }, new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[2]
		{
			new GeneratedClrTypeInfo(typeof(ShopTabInfoConfigure), ShopTabInfoConfigure.Parser, new string[2] { "ShopTabType", "GoodsPurchaseType" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(ShopTabConfigure), ShopTabConfigure.Parser, new string[2] { "Infos", "InfoDict" }, null, null, null, new GeneratedClrTypeInfo[1])
		}));
	}
}
