using System;
using Google.Protobuf.Reflection;

public static class FixGlobalReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static FixGlobalReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("Cg9GaXhHbG9iYWwucHJvdG8iTgoWRml4R2xvYmFsRGF0YUNvbmZpZ3VyZRIK" + "CgJpZBgBIAEoDxILCgNrZXkYAiABKAkSDQoFdmFsdWUYAyABKAkSDAoEVHlw" + "ZRgEIAEoCSK7AQoSRml4R2xvYmFsQ29uZmlndXJlEiYKBURhdGFzGAEgAygL" + "MhcuRml4R2xvYmFsRGF0YUNvbmZpZ3VyZRIzCghEYXRhRGljdBgCIAMoCzIh" + "LkZpeEdsb2JhbENvbmZpZ3VyZS5EYXRhRGljdEVudHJ5GkgKDURhdGFEaWN0" + "RW50cnkSCwoDa2V5GAEgASgPEiYKBXZhbHVlGAIgASgLMhcuRml4R2xvYmFs" + "RGF0YUNvbmZpZ3VyZToCOAFiBnByb3RvMw=="), new FileDescriptor[0], new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[2]
		{
			new GeneratedClrTypeInfo(typeof(FixGlobalDataConfigure), FixGlobalDataConfigure.Parser, new string[4] { "Id", "Key", "Value", "Type" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(FixGlobalConfigure), FixGlobalConfigure.Parser, new string[2] { "Datas", "DataDict" }, null, null, null, new GeneratedClrTypeInfo[1])
		}));
	}
}
