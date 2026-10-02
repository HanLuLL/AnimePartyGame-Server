using System;
using Google.Protobuf.Reflection;

public static class WayReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static WayReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("CglXYXkucHJvdG8aCkVudW0ucHJvdG8imQEKEFdheUluZm9Db25maWd1cmUS" + "GQoHd2F5VHlwZRgBIAEoDjIILldheVR5cGUSDQoFd2F5SUQYAiABKA8SFwoG" + "dWlUeXBlGAMgASgOMgcuVUlUeXBlEh8KCXBhbmVsVHlwZRgEIAEoDjIMLlVJ" + "UGFuZWxUeXBlEiEKCndpbmRvd1R5cGUYBSABKA4yDS5VSVdpbmRvd1R5cGUi" + "SwoQV2F5RGF0YUNvbmZpZ3VyZRIKCgJpZBgBIAEoBxIZCgd3YXlUeXBlGAIg" + "ASgOMgguV2F5VHlwZRIQCgh3YXlQYXJhbRgDIAMoDyK4AgoMV2F5Q29uZmln" + "dXJlEiAKBUluZm9zGAEgAygLMhEuV2F5SW5mb0NvbmZpZ3VyZRItCghJbmZv" + "RGljdBgCIAMoCzIbLldheUNvbmZpZ3VyZS5JbmZvRGljdEVudHJ5EiAKBURh" + "dGFzGAMgAygLMhEuV2F5RGF0YUNvbmZpZ3VyZRItCghEYXRhRGljdBgEIAMo" + "CzIbLldheUNvbmZpZ3VyZS5EYXRhRGljdEVudHJ5GkIKDUluZm9EaWN0RW50" + "cnkSCwoDa2V5GAEgASgPEiAKBXZhbHVlGAIgASgLMhEuV2F5SW5mb0NvbmZp" + "Z3VyZToCOAEaQgoNRGF0YURpY3RFbnRyeRILCgNrZXkYASABKA8SIAoFdmFs" + "dWUYAiABKAsyES5XYXlEYXRhQ29uZmlndXJlOgI4AWIGcHJvdG8z"), new FileDescriptor[1] { EnumReflection.Descriptor }, new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[3]
		{
			new GeneratedClrTypeInfo(typeof(WayInfoConfigure), WayInfoConfigure.Parser, new string[5] { "WayType", "WayID", "UiType", "PanelType", "WindowType" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(WayDataConfigure), WayDataConfigure.Parser, new string[3] { "Id", "WayType", "WayParam" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(WayConfigure), WayConfigure.Parser, new string[4] { "Infos", "InfoDict", "Datas", "DataDict" }, null, null, null, new GeneratedClrTypeInfo[2])
		}));
	}
}
