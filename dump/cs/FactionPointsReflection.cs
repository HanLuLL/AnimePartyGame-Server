using System;
using Google.Protobuf.Reflection;

public static class FactionPointsReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static FactionPointsReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("ChNGYWN0aW9uUG9pbnRzLnByb3RvIlEKGkZhY3Rpb25Qb2ludHNJbmZvQ29u" + "ZmlndXJlEg0KBW1hcElEGAEgASgPEhIKCmFjdGl2aXR5SUQYAiADKA8SEAoI" + "dm90ZUxpc3QYAyADKA8iywEKFkZhY3Rpb25Qb2ludHNDb25maWd1cmUSKgoF" + "SW5mb3MYASADKAsyGy5GYWN0aW9uUG9pbnRzSW5mb0NvbmZpZ3VyZRI3CghJ" + "bmZvRGljdBgCIAMoCzIlLkZhY3Rpb25Qb2ludHNDb25maWd1cmUuSW5mb0Rp" + "Y3RFbnRyeRpMCg1JbmZvRGljdEVudHJ5EgsKA2tleRgBIAEoDxIqCgV2YWx1" + "ZRgCIAEoCzIbLkZhY3Rpb25Qb2ludHNJbmZvQ29uZmlndXJlOgI4AWIGcHJv" + "dG8z"), new FileDescriptor[0], new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[2]
		{
			new GeneratedClrTypeInfo(typeof(FactionPointsInfoConfigure), FactionPointsInfoConfigure.Parser, new string[3] { "MapID", "ActivityID", "VoteList" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(FactionPointsConfigure), FactionPointsConfigure.Parser, new string[2] { "Infos", "InfoDict" }, null, null, null, new GeneratedClrTypeInfo[1])
		}));
	}
}
