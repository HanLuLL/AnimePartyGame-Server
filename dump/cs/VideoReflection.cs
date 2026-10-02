using System;
using Google.Protobuf.Reflection;

public static class VideoReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static VideoReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("CgtWaWRlby5wcm90byJLChRWaWRlb0dsb2JhbENvbmZpZ3VyZRIKCgJpZBgB" + "IAEoDxIRCglsb2FkZWRLZXkYAiABKAkSFAoMbG9hZGVkS2V5U0ZXGAMgASgJ" + "ImsKGFZpZGVvVmlkZW9RdWV1ZUNvbmZpZ3VyZRIKCgJpZBgBIAEoDxIWCg5s" + "b2FkZWRLZXlTdGFydBgCIAEoCRIVCg1sb2FkZWRLZXlMb29wGAMgASgJEhQK" + "DGxvYWRlZEtleUVuZBgEIAEoCSL2AgoOVmlkZW9Db25maWd1cmUSJgoHR2xv" + "YmFscxgBIAMoCzIVLlZpZGVvR2xvYmFsQ29uZmlndXJlEjMKCkdsb2JhbERp" + "Y3QYAiADKAsyHy5WaWRlb0NvbmZpZ3VyZS5HbG9iYWxEaWN0RW50cnkSLgoL" + "VmlkZW9RdWV1ZXMYAyADKAsyGS5WaWRlb1ZpZGVvUXVldWVDb25maWd1cmUS" + "OwoOVmlkZW9RdWV1ZURpY3QYBCADKAsyIy5WaWRlb0NvbmZpZ3VyZS5WaWRl" + "b1F1ZXVlRGljdEVudHJ5GkgKD0dsb2JhbERpY3RFbnRyeRILCgNrZXkYASAB" + "KA8SJAoFdmFsdWUYAiABKAsyFS5WaWRlb0dsb2JhbENvbmZpZ3VyZToCOAEa" + "UAoTVmlkZW9RdWV1ZURpY3RFbnRyeRILCgNrZXkYASABKA8SKAoFdmFsdWUY" + "AiABKAsyGS5WaWRlb1ZpZGVvUXVldWVDb25maWd1cmU6AjgBYgZwcm90bzM="), new FileDescriptor[0], new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[3]
		{
			new GeneratedClrTypeInfo(typeof(VideoGlobalConfigure), VideoGlobalConfigure.Parser, new string[3] { "Id", "LoadedKey", "LoadedKeySFW" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(VideoVideoQueueConfigure), VideoVideoQueueConfigure.Parser, new string[4] { "Id", "LoadedKeyStart", "LoadedKeyLoop", "LoadedKeyEnd" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(VideoConfigure), VideoConfigure.Parser, new string[4] { "Globals", "GlobalDict", "VideoQueues", "VideoQueueDict" }, null, null, null, new GeneratedClrTypeInfo[2])
		}));
	}
}
