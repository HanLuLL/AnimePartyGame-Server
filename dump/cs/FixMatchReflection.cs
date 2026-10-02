using System;
using Google.Protobuf.Reflection;

public static class FixMatchReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static FixMatchReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("Cg5GaXhNYXRjaC5wcm90byIxChVGaXhNYXRjaEluZm9Db25maWd1cmUSCgoC" + "aWQYASABKA8SDAoEbWFwcxgCIAMoESK3AQoRRml4TWF0Y2hDb25maWd1cmUS" + "JQoFSW5mb3MYASADKAsyFi5GaXhNYXRjaEluZm9Db25maWd1cmUSMgoISW5m" + "b0RpY3QYAiADKAsyIC5GaXhNYXRjaENvbmZpZ3VyZS5JbmZvRGljdEVudHJ5" + "GkcKDUluZm9EaWN0RW50cnkSCwoDa2V5GAEgASgPEiUKBXZhbHVlGAIgASgL" + "MhYuRml4TWF0Y2hJbmZvQ29uZmlndXJlOgI4AWIGcHJvdG8z"), new FileDescriptor[0], new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[2]
		{
			new GeneratedClrTypeInfo(typeof(FixMatchInfoConfigure), FixMatchInfoConfigure.Parser, new string[2] { "Id", "Maps" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(FixMatchConfigure), FixMatchConfigure.Parser, new string[2] { "Infos", "InfoDict" }, null, null, null, new GeneratedClrTypeInfo[1])
		}));
	}
}
