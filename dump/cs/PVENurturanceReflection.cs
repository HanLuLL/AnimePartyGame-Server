using System;
using Google.Protobuf.Reflection;

public static class PVENurturanceReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static PVENurturanceReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("ChNQVkVOdXJ0dXJhbmNlLnByb3RvIjgKHVBWRU51cnR1cmFuY2VMZXZlbHVw" + "Q29uZmlndXJlEgoKAmlkGAEgASgPEgsKA2V4cBgCIAEoDyKHAQohUFZFTnVy" + "dHVyYW5jZUVuaGFuY2VtZW50Q29uZmlndXJlEgoKAmlkGAEgASgPElYKJnBW" + "RU51cnR1cmFuY2VFbmhhbmNlbWVudENvbmZpZ3VyZUl0ZW1zGAIgAygLMiYu" + "UFZFTnVydHVyYW5jZUVuaGFuY2VtZW50Q29uZmlndXJlSXRlbSJKCiVQVkVO" + "dXJ0dXJhbmNlRW5oYW5jZW1lbnRDb25maWd1cmVJdGVtEgoKAmx2GAEgASgP" + "EhUKDWRlc2NyaXB0aW9uSUQYAiABKA8iOAodUFZFTnVydHVyYW5jZUl0ZW1F" + "eHBDb25maWd1cmUSCgoCaWQYASABKA8SCwoDZXhwGAIgASgPIqoCChtQVkVO" + "dXJ0dXJhbmNlQnJlYWtDb25maWd1cmUSCgoCaWQYASABKA8SGgoScmVwbGFj" + "ZUFjdGl2ZVNraWxsGAIgASgPEhgKEGRlbFBhc3NpdmVTa2lsbHMYAyADKA8S" + "GAoQYWRkUGFzc2l2ZVNraWxscxgEIAMoDxIVCg1kZXNjcmlwdGlvbklEGAUg" + "ASgPEkYKDW5lZWRNYXRlcmlhbHMYBiADKAsyLy5QVkVOdXJ0dXJhbmNlQnJl" + "YWtDb25maWd1cmUuTmVlZE1hdGVyaWFsc0VudHJ5EhoKEnVubG9ja05lZWRQ" + "VkVMZXZlbBgHIAEoDxo0ChJOZWVkTWF0ZXJpYWxzRW50cnkSCwoDa2V5GAEg" + "ASgPEg0KBXZhbHVlGAIgASgPOgI4ASK4BgoWUFZFTnVydHVyYW5jZUNvbmZp" + "Z3VyZRIwCghMZXZlbHVwcxgBIAMoCzIeLlBWRU51cnR1cmFuY2VMZXZlbHVw" + "Q29uZmlndXJlEj0KC0xldmVsdXBEaWN0GAIgAygLMiguUFZFTnVydHVyYW5j" + "ZUNvbmZpZ3VyZS5MZXZlbHVwRGljdEVudHJ5EjgKDEVuaGFuY2VtZW50cxgD" + "IAMoCzIiLlBWRU51cnR1cmFuY2VFbmhhbmNlbWVudENvbmZpZ3VyZRJFCg9F" + "bmhhbmNlbWVudERpY3QYBCADKAsyLC5QVkVOdXJ0dXJhbmNlQ29uZmlndXJl" + "LkVuaGFuY2VtZW50RGljdEVudHJ5EjAKCEl0ZW1FeHBzGAUgAygLMh4uUFZF" + "TnVydHVyYW5jZUl0ZW1FeHBDb25maWd1cmUSPQoLSXRlbUV4cERpY3QYBiAD" + "KAsyKC5QVkVOdXJ0dXJhbmNlQ29uZmlndXJlLkl0ZW1FeHBEaWN0RW50cnkS" + "LAoGQnJlYWtzGAcgAygLMhwuUFZFTnVydHVyYW5jZUJyZWFrQ29uZmlndXJl" + "EjkKCUJyZWFrRGljdBgIIAMoCzImLlBWRU51cnR1cmFuY2VDb25maWd1cmUu" + "QnJlYWtEaWN0RW50cnkaUgoQTGV2ZWx1cERpY3RFbnRyeRILCgNrZXkYASAB" + "KA8SLQoFdmFsdWUYAiABKAsyHi5QVkVOdXJ0dXJhbmNlTGV2ZWx1cENvbmZp" + "Z3VyZToCOAEaWgoURW5oYW5jZW1lbnREaWN0RW50cnkSCwoDa2V5GAEgASgP" + "EjEKBXZhbHVlGAIgASgLMiIuUFZFTnVydHVyYW5jZUVuaGFuY2VtZW50Q29u" + "ZmlndXJlOgI4ARpSChBJdGVtRXhwRGljdEVudHJ5EgsKA2tleRgBIAEoDxIt" + "CgV2YWx1ZRgCIAEoCzIeLlBWRU51cnR1cmFuY2VJdGVtRXhwQ29uZmlndXJl" + "OgI4ARpOCg5CcmVha0RpY3RFbnRyeRILCgNrZXkYASABKA8SKwoFdmFsdWUY" + "AiABKAsyHC5QVkVOdXJ0dXJhbmNlQnJlYWtDb25maWd1cmU6AjgBYgZwcm90" + "bzM="), new FileDescriptor[0], new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[6]
		{
			new GeneratedClrTypeInfo(typeof(PVENurturanceLevelupConfigure), PVENurturanceLevelupConfigure.Parser, new string[2] { "Id", "Exp" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(PVENurturanceEnhancementConfigure), PVENurturanceEnhancementConfigure.Parser, new string[2] { "Id", "PVENurturanceEnhancementConfigureItems" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(PVENurturanceEnhancementConfigureItem), PVENurturanceEnhancementConfigureItem.Parser, new string[2] { "Lv", "DescriptionID" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(PVENurturanceItemExpConfigure), PVENurturanceItemExpConfigure.Parser, new string[2] { "Id", "Exp" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(PVENurturanceBreakConfigure), PVENurturanceBreakConfigure.Parser, new string[7] { "Id", "ReplaceActiveSkill", "DelPassiveSkills", "AddPassiveSkills", "DescriptionID", "NeedMaterials", "UnlockNeedPVELevel" }, null, null, null, new GeneratedClrTypeInfo[1]),
			new GeneratedClrTypeInfo(typeof(PVENurturanceConfigure), PVENurturanceConfigure.Parser, new string[8] { "Levelups", "LevelupDict", "Enhancements", "EnhancementDict", "ItemExps", "ItemExpDict", "Breaks", "BreakDict" }, null, null, null, new GeneratedClrTypeInfo[4])
		}));
	}
}
