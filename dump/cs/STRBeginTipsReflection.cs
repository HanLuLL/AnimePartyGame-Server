using System;
using Google.Protobuf.Reflection;

public static class STRBeginTipsReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static STRBeginTipsReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("ChJTVFJCZWdpblRpcHMucHJvdG8ihAEKGlNUUkJlZ2luVGlwc0xvY2FsQ29u" + "ZmlndXJlEgoKAmlkGAEgASgPEhIKCnNpbXBsaWZpZWQYAiABKAkSDwoHZW5n" + "bGlzaBgDIAEoCRIQCghqYXBhbmVzZRgEIAEoCRITCgt0cmFkaXRpb25hbBgF" + "IAEoCRIOCgZrb3JlYW4YBiABKAkizQEKFVNUUkJlZ2luVGlwc0NvbmZpZ3Vy" + "ZRIrCgZMb2NhbHMYASADKAsyGy5TVFJCZWdpblRpcHNMb2NhbENvbmZpZ3Vy" + "ZRI4CglMb2NhbERpY3QYAiADKAsyJS5TVFJCZWdpblRpcHNDb25maWd1cmUu" + "TG9jYWxEaWN0RW50cnkaTQoOTG9jYWxEaWN0RW50cnkSCwoDa2V5GAEgASgP" + "EioKBXZhbHVlGAIgASgLMhsuU1RSQmVnaW5UaXBzTG9jYWxDb25maWd1cmU6" + "AjgBYgZwcm90bzM="), new FileDescriptor[0], new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[2]
		{
			new GeneratedClrTypeInfo(typeof(STRBeginTipsLocalConfigure), STRBeginTipsLocalConfigure.Parser, new string[6] { "Id", "Simplified", "English", "Japanese", "Traditional", "Korean" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(STRBeginTipsConfigure), STRBeginTipsConfigure.Parser, new string[2] { "Locals", "LocalDict" }, null, null, null, new GeneratedClrTypeInfo[1])
		}));
	}
}
