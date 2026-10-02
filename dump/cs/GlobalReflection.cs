using System;
using Google.Protobuf.Reflection;

public static class GlobalReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static GlobalReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("CgxHbG9iYWwucHJvdG8iSwoTR2xvYmFsRGF0YUNvbmZpZ3VyZRIKCgJpZBgB" + "IAEoDxILCgNrZXkYAiABKAkSDQoFdmFsdWUYAyABKAkSDAoEVHlwZRgEIAEo" + "CSKvAQoPR2xvYmFsQ29uZmlndXJlEiMKBURhdGFzGAEgAygLMhQuR2xvYmFs" + "RGF0YUNvbmZpZ3VyZRIwCghEYXRhRGljdBgCIAMoCzIeLkdsb2JhbENvbmZp" + "Z3VyZS5EYXRhRGljdEVudHJ5GkUKDURhdGFEaWN0RW50cnkSCwoDa2V5GAEg" + "ASgPEiMKBXZhbHVlGAIgASgLMhQuR2xvYmFsRGF0YUNvbmZpZ3VyZToCOAFi" + "BnByb3RvMw=="), new FileDescriptor[0], new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[2]
		{
			new GeneratedClrTypeInfo(typeof(GlobalDataConfigure), GlobalDataConfigure.Parser, new string[4] { "Id", "Key", "Value", "Type" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(GlobalConfigure), GlobalConfigure.Parser, new string[2] { "Datas", "DataDict" }, null, null, null, new GeneratedClrTypeInfo[1])
		}));
	}
}
