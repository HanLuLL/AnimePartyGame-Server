using System;
using Google.Protobuf.Reflection;

public static class AcquisitionReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static AcquisitionReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("ChFBY3F1aXNpdGlvbi5wcm90bxoKRW51bS5wcm90byLrAQoYQWNxdWlzaXRp" + "b25JbmZvQ29uZmlndXJlEgoKAmlkGAEgASgPEhUKDWludml0ZVRhc2tJRHMY" + "AiADKA8SFgoOY2FwdGNoYVZhbGlkTHYYAyABKA8SNQoGcmV3YXJkGAQgAygL" + "MiUuQWNxdWlzaXRpb25JbmZvQ29uZmlndXJlLlJld2FyZEVudHJ5EhYKDmlu" + "dml0ZWRUYXNrSURzGAUgAygPEhYKDmNoYXJhY3RlckltYWdlGAYgASgJGi0K" + "C1Jld2FyZEVudHJ5EgsKA2tleRgBIAEoDxINCgV2YWx1ZRgCIAEoDzoCOAEi" + "9QEKGEFjcXVpc2l0aW9uVGFza0NvbmZpZ3VyZRIKCgJpZBgBIAEoDxITCgtv" + "cmRlcldlaWdodBgCIAEoDxIlCg1jb25kaXRpb25UeXBlGAMgASgOMg4uQ29u" + "ZGl0aW9uVHlwZRIOCgZwYXJhbXMYBCADKA8SDgoGZGVzY0lkGAUgASgPEjUK" + "BnJld2FyZBgGIAMoCzIlLkFjcXVpc2l0aW9uVGFza0NvbmZpZ3VyZS5SZXdh" + "cmRFbnRyeRILCgN3YXkYByABKA8aLQoLUmV3YXJkRW50cnkSCwoDa2V5GAEg" + "ASgPEg0KBXZhbHVlGAIgASgPOgI4ASLwAgoUQWNxdWlzaXRpb25Db25maWd1" + "cmUSKAoFSW5mb3MYASADKAsyGS5BY3F1aXNpdGlvbkluZm9Db25maWd1cmUS" + "NQoISW5mb0RpY3QYAiADKAsyIy5BY3F1aXNpdGlvbkNvbmZpZ3VyZS5JbmZv" + "RGljdEVudHJ5EigKBVRhc2tzGAMgAygLMhkuQWNxdWlzaXRpb25UYXNrQ29u" + "ZmlndXJlEjUKCFRhc2tEaWN0GAQgAygLMiMuQWNxdWlzaXRpb25Db25maWd1" + "cmUuVGFza0RpY3RFbnRyeRpKCg1JbmZvRGljdEVudHJ5EgsKA2tleRgBIAEo" + "DxIoCgV2YWx1ZRgCIAEoCzIZLkFjcXVpc2l0aW9uSW5mb0NvbmZpZ3VyZToC" + "OAEaSgoNVGFza0RpY3RFbnRyeRILCgNrZXkYASABKA8SKAoFdmFsdWUYAiAB" + "KAsyGS5BY3F1aXNpdGlvblRhc2tDb25maWd1cmU6AjgBYgZwcm90bzM="), new FileDescriptor[1] { EnumReflection.Descriptor }, new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[3]
		{
			new GeneratedClrTypeInfo(typeof(AcquisitionInfoConfigure), AcquisitionInfoConfigure.Parser, new string[6] { "Id", "InviteTaskIDs", "CaptchaValidLv", "Reward", "InvitedTaskIDs", "CharacterImage" }, null, null, null, new GeneratedClrTypeInfo[1]),
			new GeneratedClrTypeInfo(typeof(AcquisitionTaskConfigure), AcquisitionTaskConfigure.Parser, new string[7] { "Id", "OrderWeight", "ConditionType", "Params", "DescId", "Reward", "Way" }, null, null, null, new GeneratedClrTypeInfo[1]),
			new GeneratedClrTypeInfo(typeof(AcquisitionConfigure), AcquisitionConfigure.Parser, new string[4] { "Infos", "InfoDict", "Tasks", "TaskDict" }, null, null, null, new GeneratedClrTypeInfo[2])
		}));
	}
}
