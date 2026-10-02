using System;
using Google.Protobuf.Reflection;
using Google.Protobuf.WellKnownTypes;

public static class MissionReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static MissionReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("Cg1NaXNzaW9uLnByb3RvGh9nb29nbGUvcHJvdG9idWYvdGltZXN0YW1wLnBy" + "b3RvGgpFbnVtLnByb3RvIvMDChRNaXNzaW9uRGF0YUNvbmZpZ3VyZRIKCgJp" + "ZBgBIAEoDxITCgtvcmRlcldlaWdodBgCIAEoDxIOCgZuZXh0aWQYAyABKA8S" + "DwoHZnJvbnRJRBgEIAMoDxIpCg90YXNrUmVmcmVzaFR5cGUYBSABKA4yEC5U" + "YXNrUmVmcmVzaFR5cGUSMwoUbWlzc2lvbkNvbmRpdGlvblR5cGUYBiABKA4y" + "FS5NaXNzaW9uQ29uZGl0aW9uVHlwZRIVCg1wYXJhbVByb2dyZXNzGAcgASgP" + "EiMKCHBhcmFtS2V5GAggAygOMhEuTWlzc2lvblBhcmFtVHlwZRISCgpwYXJh" + "bVZhbHVlGAkgAygPEi0KCWJlZ2luVGltZRgKIAEoCzIaLmdvb2dsZS5wcm90" + "b2J1Zi5UaW1lc3RhbXASKwoHZW5kVGltZRgLIAEoCzIaLmdvb2dsZS5wcm90" + "b2J1Zi5UaW1lc3RhbXASDgoGbmFtZUlEGAwgASgPEg4KBmRlc2NJZBgNIAEo" + "DxIxCgZyZXdhcmQYDiADKAsyIS5NaXNzaW9uRGF0YUNvbmZpZ3VyZS5SZXdh" + "cmRFbnRyeRILCgN3YXkYDyABKA8aLQoLUmV3YXJkRW50cnkSCwoDa2V5GAEg" + "ASgPEg0KBXZhbHVlGAIgASgPOgI4ASKzAQoQTWlzc2lvbkNvbmZpZ3VyZRIk" + "CgVEYXRhcxgBIAMoCzIVLk1pc3Npb25EYXRhQ29uZmlndXJlEjEKCERhdGFE" + "aWN0GAIgAygLMh8uTWlzc2lvbkNvbmZpZ3VyZS5EYXRhRGljdEVudHJ5GkYK" + "DURhdGFEaWN0RW50cnkSCwoDa2V5GAEgASgPEiQKBXZhbHVlGAIgASgLMhUu" + "TWlzc2lvbkRhdGFDb25maWd1cmU6AjgBYgZwcm90bzM="), new FileDescriptor[2]
		{
			TimestampReflection.Descriptor,
			EnumReflection.Descriptor
		}, new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[2]
		{
			new GeneratedClrTypeInfo(typeof(MissionDataConfigure), MissionDataConfigure.Parser, new string[15]
			{
				"Id", "OrderWeight", "Nextid", "FrontID", "TaskRefreshType", "MissionConditionType", "ParamProgress", "ParamKey", "ParamValue", "BeginTime",
				"EndTime", "NameID", "DescId", "Reward", "Way"
			}, null, null, null, new GeneratedClrTypeInfo[1]),
			new GeneratedClrTypeInfo(typeof(MissionConfigure), MissionConfigure.Parser, new string[2] { "Datas", "DataDict" }, null, null, null, new GeneratedClrTypeInfo[1])
		}));
	}
}
