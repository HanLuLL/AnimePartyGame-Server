using System;
using Google.Protobuf.Reflection;

public static class ChestReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static ChestReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("CgtDaGVzdC5wcm90byLnAQoSQ2hlc3RJbmZvQ29uZmlndXJlEgoKAmlkGAEg" + "ASgPEhIKCmlzT3B0aW9uYWwYAiABKAgSFAoMcmFuZG9tUmV3YXJkGAMgAygP" + "Ej8KDm9wdGlvbmFsUmV3YXJkGAQgAygLMicuQ2hlc3RJbmZvQ29uZmlndXJl" + "Lk9wdGlvbmFsUmV3YXJkRW50cnkSEwoLY29udGVudFR5cGUYBSABKA8SDgoG" + "dXNlTWF4GAYgASgPGjUKE09wdGlvbmFsUmV3YXJkRW50cnkSCwoDa2V5GAEg" + "ASgPEg0KBXZhbHVlGAIgASgPOgI4ASJyChpDaGVzdFJhbmRvbVJld2FyZENv" + "bmZpZ3VyZRIKCgJpZBgBIAEoDxJICh9jaGVzdFJhbmRvbVJld2FyZENvbmZp" + "Z3VyZUl0ZW1zGAIgAygLMh8uQ2hlc3RSYW5kb21SZXdhcmRDb25maWd1cmVJ" + "dGVtInMKHkNoZXN0UmFuZG9tUmV3YXJkQ29uZmlndXJlSXRlbRINCgVpbmRl" + "eBgBIAEoDxIOCgZ3ZWlnaHQYAiABKA8SDgoGaXRlbUlEGAMgASgPEhAKCG1p" + "bkNvdW50GAQgASgPEhAKCG1heENvdW50GAUgASgPIvYCCg5DaGVzdENvbmZp" + "Z3VyZRIiCgVJbmZvcxgBIAMoCzITLkNoZXN0SW5mb0NvbmZpZ3VyZRIvCghJ" + "bmZvRGljdBgCIAMoCzIdLkNoZXN0Q29uZmlndXJlLkluZm9EaWN0RW50cnkS" + "MgoNUmFuZG9tUmV3YXJkcxgDIAMoCzIbLkNoZXN0UmFuZG9tUmV3YXJkQ29u" + "ZmlndXJlEj8KEFJhbmRvbVJld2FyZERpY3QYBCADKAsyJS5DaGVzdENvbmZp" + "Z3VyZS5SYW5kb21SZXdhcmREaWN0RW50cnkaRAoNSW5mb0RpY3RFbnRyeRIL" + "CgNrZXkYASABKA8SIgoFdmFsdWUYAiABKAsyEy5DaGVzdEluZm9Db25maWd1" + "cmU6AjgBGlQKFVJhbmRvbVJld2FyZERpY3RFbnRyeRILCgNrZXkYASABKA8S" + "KgoFdmFsdWUYAiABKAsyGy5DaGVzdFJhbmRvbVJld2FyZENvbmZpZ3VyZToC" + "OAFiBnByb3RvMw=="), new FileDescriptor[0], new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[4]
		{
			new GeneratedClrTypeInfo(typeof(ChestInfoConfigure), ChestInfoConfigure.Parser, new string[6] { "Id", "IsOptional", "RandomReward", "OptionalReward", "ContentType", "UseMax" }, null, null, null, new GeneratedClrTypeInfo[1]),
			new GeneratedClrTypeInfo(typeof(ChestRandomRewardConfigure), ChestRandomRewardConfigure.Parser, new string[2] { "Id", "ChestRandomRewardConfigureItems" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(ChestRandomRewardConfigureItem), ChestRandomRewardConfigureItem.Parser, new string[5] { "Index", "Weight", "ItemID", "MinCount", "MaxCount" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(ChestConfigure), ChestConfigure.Parser, new string[4] { "Infos", "InfoDict", "RandomRewards", "RandomRewardDict" }, null, null, null, new GeneratedClrTypeInfo[2])
		}));
	}
}
