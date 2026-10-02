using System;
using Google.Protobuf.Reflection;

public static class DivinationReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static DivinationReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("ChBEaXZpbmF0aW9uLnByb3RvGgpFbnVtLnByb3RvIs0BChdEaXZpbmF0aW9u" + "SW5mb0NvbmZpZ3VyZRIKCgJpZBgBIAEoDxIbCghjYXJkVHlwZRgCIAEoDjIJ" + "LkNhcmRUeXBlEhQKDGZ1bmN0aW9uTmFtZRgDIAEoCRIOCgZuYW1lSUQYBCAB" + "KA8SEAoIY2FyZE51bWIYBSABKA8SDgoGZGVzY0lkGAYgASgPEhEKCWNvbW1l" + "bnRJZBgHIAEoDxINCgVpbWFnZRgIIAEoCRIOCgZwYXJhbXMYCSADKBESDwoH" + "cGVyZm9ybRgKIAEoDyK6AQoZRGl2aW5hdGlvblRhcmdldENvbmZpZ3VyZRIz" + "ChRkaXZpbmF0aW9uVGFyZ2V0VHlwZRgBIAEoDjIVLkRpdmluYXRpb25UYXJn" + "ZXRUeXBlEg4KBm5hbWVJRBgCIAEoDxIUCgxmdW5jdGlvbk5hbWUYAyABKAkS" + "EAoIY2FyZE51bWIYBCABKA8SDgoGZGVzY0lkGAUgASgPEhEKCWNvbW1lbnRJ" + "ZBgGIAEoDxINCgVpbWFnZRgHIAEoCSL1AgoTRGl2aW5hdGlvbkNvbmZpZ3Vy" + "ZRInCgVJbmZvcxgBIAMoCzIYLkRpdmluYXRpb25JbmZvQ29uZmlndXJlEjQK" + "CEluZm9EaWN0GAIgAygLMiIuRGl2aW5hdGlvbkNvbmZpZ3VyZS5JbmZvRGlj" + "dEVudHJ5EisKB1RhcmdldHMYAyADKAsyGi5EaXZpbmF0aW9uVGFyZ2V0Q29u" + "ZmlndXJlEjgKClRhcmdldERpY3QYBCADKAsyJC5EaXZpbmF0aW9uQ29uZmln" + "dXJlLlRhcmdldERpY3RFbnRyeRpJCg1JbmZvRGljdEVudHJ5EgsKA2tleRgB" + "IAEoDxInCgV2YWx1ZRgCIAEoCzIYLkRpdmluYXRpb25JbmZvQ29uZmlndXJl" + "OgI4ARpNCg9UYXJnZXREaWN0RW50cnkSCwoDa2V5GAEgASgPEikKBXZhbHVl" + "GAIgASgLMhouRGl2aW5hdGlvblRhcmdldENvbmZpZ3VyZToCOAFiBnByb3Rv" + "Mw=="), new FileDescriptor[1] { EnumReflection.Descriptor }, new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[3]
		{
			new GeneratedClrTypeInfo(typeof(DivinationInfoConfigure), DivinationInfoConfigure.Parser, new string[10] { "Id", "CardType", "FunctionName", "NameID", "CardNumb", "DescId", "CommentId", "Image", "Params", "Perform" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(DivinationTargetConfigure), DivinationTargetConfigure.Parser, new string[7] { "DivinationTargetType", "NameID", "FunctionName", "CardNumb", "DescId", "CommentId", "Image" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(DivinationConfigure), DivinationConfigure.Parser, new string[4] { "Infos", "InfoDict", "Targets", "TargetDict" }, null, null, null, new GeneratedClrTypeInfo[2])
		}));
	}
}
