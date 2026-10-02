using System;
using Google.Protobuf.Reflection;

public static class VersionReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static VersionReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("Cg1WZXJzaW9uLnByb3RvIkoKFFZlcnNpb25EYXRhQ29uZmlndXJlEgoKAmlk" + "GAEgASgPEhIKCmFwcFZlcnNpb24YAiABKAkSEgoKcmVzVmVyc2lvbhgDIAEo" + "CSKzAQoQVmVyc2lvbkNvbmZpZ3VyZRIkCgVEYXRhcxgBIAMoCzIVLlZlcnNp" + "b25EYXRhQ29uZmlndXJlEjEKCERhdGFEaWN0GAIgAygLMh8uVmVyc2lvbkNv" + "bmZpZ3VyZS5EYXRhRGljdEVudHJ5GkYKDURhdGFEaWN0RW50cnkSCwoDa2V5" + "GAEgASgPEiQKBXZhbHVlGAIgASgLMhUuVmVyc2lvbkRhdGFDb25maWd1cmU6" + "AjgBYgZwcm90bzM="), new FileDescriptor[0], new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[2]
		{
			new GeneratedClrTypeInfo(typeof(VersionDataConfigure), VersionDataConfigure.Parser, new string[3] { "Id", "AppVersion", "ResVersion" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(VersionConfigure), VersionConfigure.Parser, new string[2] { "Datas", "DataDict" }, null, null, null, new GeneratedClrTypeInfo[1])
		}));
	}
}
