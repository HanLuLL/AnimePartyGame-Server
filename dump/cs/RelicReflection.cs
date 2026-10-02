using System;
using Google.Protobuf.Reflection;

public static class RelicReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static RelicReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("CgtSZWxpYy5wcm90bxoKRW51bS5wcm90byLkAwoSUmVsaWNJbmZvQ29uZmln" + "dXJlEgoKAmlkGAEgASgPEisKEHJlbGljUXVhbGl0eVR5cGUYAiABKA4yES5S" + "ZWxpY1F1YWxpdHlUeXBlEiIKB3RhZ1R5cGUYAyADKA4yES5DaGFyYWN0ZXJU" + "YWdUeXBlEiYKC2tleVdvcmRUeXBlGAQgASgOMhEuUmVsaWNLZXlXb3JkVHlw" + "ZRIQCghtYXBMaW1pdBgFIAMoERIMCgRpY29uGAYgASgJEg4KBm5hbWVJRBgH" + "IAEoDxIOCgZkZXNjSUQYCCABKA8SDgoGcGFyYW1zGAkgAygREg4KBmJ1ZmZJ" + "ZBgKIAEoDxIVCg1wZXJmb3JtVGFyZ2V0GAsgASgPEhMKC3BlcmZvcm1TZWxm" + "GAwgASgPEhMKC29yZGVyV2VpZ2h0GA0gASgPEhAKCGF0dGFja1B0GA4gASgP" + "Eg4KBmNhcmRQdBgPIAEoDxIRCglTdXBwb3J0UHQYECABKA8SDgoGdGFua1B0" + "GBEgASgPEhkKEXJlbGljUmVjQmFzZVNjb3JlGBIgASgPEhYKDnJlbGljUmVj" + "R3Jvd3RoGBMgASgPEhoKEnJlbGljUmVjUm9sZU5vcm1hbBgUIAMoERIUCgxy" + "ZWxpY1JlY1JvbGUYFSADKBEi4wIKFFJlbGljUGFyYW1zQ29uZmlndXJlEgoK" + "AmlkGAEgASgPEh8KF3JlbGljUmVjUm9sZU5vcm1hbFNjb3JlGAIgASgPEhkK" + "EXJlbGljUmVjUm9sZVNjb3JlGAMgASgPEhsKE3JlbGljUmVjR3Jvd3RoU2Nv" + "cmUYBCABKA8SGwoTcmVsaWNSZWNUYWdUeXBlTmRQdBgFIAEoDxIcChRyZWxp" + "Y1JlY1RhZ1R5cGVTY29yZRgGIAEoDxIdChVyZWxpY1JlY1dvcmRUeXBlU2Nv" + "cmUYByABKA8SGQoRcmVsaWNSZWNCbHVlU2NvcmUYCCABKA8SGwoTcmVsaWNS" + "ZWNQdXJwbGVTY29yZRgJIAEoDxIbChNyZWxpY1JlY09yYW5nZVNjb3JlGAog" + "ASgPEhoKEnJlbGljUmVjTGltaXRTY29yZRgLIAEoDxIbChNyZWxpY1JlY1NM" + "aW1pdFNjb3JlGAwgASgPInsKFlJlbGljS2V5d29yZHNDb25maWd1cmUSJgoL" + "a2V5V29yZFR5cGUYASABKA4yES5SZWxpY0tleVdvcmRUeXBlEgwKBGljb24Y" + "AiABKAkSFQoNa2V5d29yZE5hbWVJRBgDIAEoDxIUCgxrZXl3b3JkRGVzSUQY" + "BCABKA8ibwocUmVsaWNDaGFyYWN0ZXJSZWxpY0NvbmZpZ3VyZRIKCgJpZBgB" + "IAEoDxIQCghhdHRhY2tQdBgCIAEoDxIOCgZjYXJkUHQYAyABKA8SEQoJU3Vw" + "cG9ydFB0GAQgASgPEg4KBnRhbmtQdBgFIAEoDyLcBQoOUmVsaWNDb25maWd1" + "cmUSIgoFSW5mb3MYASADKAsyEy5SZWxpY0luZm9Db25maWd1cmUSLwoISW5m" + "b0RpY3QYAiADKAsyHS5SZWxpY0NvbmZpZ3VyZS5JbmZvRGljdEVudHJ5EiYK" + "B1BhcmFtc3MYAyADKAsyFS5SZWxpY1BhcmFtc0NvbmZpZ3VyZRIzCgpQYXJh" + "bXNEaWN0GAQgAygLMh8uUmVsaWNDb25maWd1cmUuUGFyYW1zRGljdEVudHJ5" + "EioKCUtleXdvcmRzcxgFIAMoCzIXLlJlbGljS2V5d29yZHNDb25maWd1cmUS" + "NwoMS2V5d29yZHNEaWN0GAYgAygLMiEuUmVsaWNDb25maWd1cmUuS2V5d29y" + "ZHNEaWN0RW50cnkSNgoPQ2hhcmFjdGVyUmVsaWNzGAcgAygLMh0uUmVsaWND" + "aGFyYWN0ZXJSZWxpY0NvbmZpZ3VyZRJDChJDaGFyYWN0ZXJSZWxpY0RpY3QY" + "CCADKAsyJy5SZWxpY0NvbmZpZ3VyZS5DaGFyYWN0ZXJSZWxpY0RpY3RFbnRy" + "eRpECg1JbmZvRGljdEVudHJ5EgsKA2tleRgBIAEoDxIiCgV2YWx1ZRgCIAEo" + "CzITLlJlbGljSW5mb0NvbmZpZ3VyZToCOAEaSAoPUGFyYW1zRGljdEVudHJ5" + "EgsKA2tleRgBIAEoDxIkCgV2YWx1ZRgCIAEoCzIVLlJlbGljUGFyYW1zQ29u" + "ZmlndXJlOgI4ARpMChFLZXl3b3Jkc0RpY3RFbnRyeRILCgNrZXkYASABKA8S" + "JgoFdmFsdWUYAiABKAsyFy5SZWxpY0tleXdvcmRzQ29uZmlndXJlOgI4ARpY" + "ChdDaGFyYWN0ZXJSZWxpY0RpY3RFbnRyeRILCgNrZXkYASABKA8SLAoFdmFs" + "dWUYAiABKAsyHS5SZWxpY0NoYXJhY3RlclJlbGljQ29uZmlndXJlOgI4AWIG" + "cHJvdG8z"), new FileDescriptor[1] { EnumReflection.Descriptor }, new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[5]
		{
			new GeneratedClrTypeInfo(typeof(RelicInfoConfigure), RelicInfoConfigure.Parser, new string[21]
			{
				"Id", "RelicQualityType", "TagType", "KeyWordType", "MapLimit", "Icon", "NameID", "DescID", "Params", "BuffId",
				"PerformTarget", "PerformSelf", "OrderWeight", "AttackPt", "CardPt", "SupportPt", "TankPt", "RelicRecBaseScore", "RelicRecGrowth", "RelicRecRoleNormal",
				"RelicRecRole"
			}, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(RelicParamsConfigure), RelicParamsConfigure.Parser, new string[12]
			{
				"Id", "RelicRecRoleNormalScore", "RelicRecRoleScore", "RelicRecGrowthScore", "RelicRecTagTypeNdPt", "RelicRecTagTypeScore", "RelicRecWordTypeScore", "RelicRecBlueScore", "RelicRecPurpleScore", "RelicRecOrangeScore",
				"RelicRecLimitScore", "RelicRecSLimitScore"
			}, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(RelicKeywordsConfigure), RelicKeywordsConfigure.Parser, new string[4] { "KeyWordType", "Icon", "KeywordNameID", "KeywordDesID" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(RelicCharacterRelicConfigure), RelicCharacterRelicConfigure.Parser, new string[5] { "Id", "AttackPt", "CardPt", "SupportPt", "TankPt" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(RelicConfigure), RelicConfigure.Parser, new string[8] { "Infos", "InfoDict", "Paramss", "ParamsDict", "Keywordss", "KeywordsDict", "CharacterRelics", "CharacterRelicDict" }, null, null, null, new GeneratedClrTypeInfo[4])
		}));
	}
}
