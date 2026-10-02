using System;
using Google.Protobuf.Reflection;

public static class STRCharacterReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static STRCharacterReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("ChJTVFJDaGFyYWN0ZXIucHJvdG8ihAEKGlNUUkNoYXJhY3RlckxvY2FsQ29u" + "ZmlndXJlEgoKAmlkGAEgASgPEhIKCnNpbXBsaWZpZWQYAiABKAkSDwoHZW5n" + "bGlzaBgDIAEoCRIQCghqYXBhbmVzZRgEIAEoCRITCgt0cmFkaXRpb25hbBgF" + "IAEoCRIOCgZrb3JlYW4YBiABKAkizQEKFVNUUkNoYXJhY3RlckNvbmZpZ3Vy" + "ZRIrCgZMb2NhbHMYASADKAsyGy5TVFJDaGFyYWN0ZXJMb2NhbENvbmZpZ3Vy" + "ZRI4CglMb2NhbERpY3QYAiADKAsyJS5TVFJDaGFyYWN0ZXJDb25maWd1cmUu" + "TG9jYWxEaWN0RW50cnkaTQoOTG9jYWxEaWN0RW50cnkSCwoDa2V5GAEgASgP" + "EioKBXZhbHVlGAIgASgLMhsuU1RSQ2hhcmFjdGVyTG9jYWxDb25maWd1cmU6" + "AjgBYgZwcm90bzM="), new FileDescriptor[0], new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[2]
		{
			new GeneratedClrTypeInfo(typeof(STRCharacterLocalConfigure), STRCharacterLocalConfigure.Parser, new string[6] { "Id", "Simplified", "English", "Japanese", "Traditional", "Korean" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(STRCharacterConfigure), STRCharacterConfigure.Parser, new string[2] { "Locals", "LocalDict" }, null, null, null, new GeneratedClrTypeInfo[1])
		}));
	}
}
