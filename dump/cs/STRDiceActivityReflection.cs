using System;
using Google.Protobuf.Reflection;

public static class STRDiceActivityReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static STRDiceActivityReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("ChVTVFJEaWNlQWN0aXZpdHkucHJvdG8ihwEKHVNUUkRpY2VBY3Rpdml0eUxv" + "Y2FsQ29uZmlndXJlEgoKAmlkGAEgASgPEhIKCnNpbXBsaWZpZWQYAiABKAkS" + "DwoHZW5nbGlzaBgDIAEoCRIQCghqYXBhbmVzZRgEIAEoCRITCgt0cmFkaXRp" + "b25hbBgFIAEoCRIOCgZrb3JlYW4YBiABKAki2QEKGFNUUkRpY2VBY3Rpdml0" + "eUNvbmZpZ3VyZRIuCgZMb2NhbHMYASADKAsyHi5TVFJEaWNlQWN0aXZpdHlM" + "b2NhbENvbmZpZ3VyZRI7CglMb2NhbERpY3QYAiADKAsyKC5TVFJEaWNlQWN0" + "aXZpdHlDb25maWd1cmUuTG9jYWxEaWN0RW50cnkaUAoOTG9jYWxEaWN0RW50" + "cnkSCwoDa2V5GAEgASgPEi0KBXZhbHVlGAIgASgLMh4uU1RSRGljZUFjdGl2" + "aXR5TG9jYWxDb25maWd1cmU6AjgBYgZwcm90bzM="), new FileDescriptor[0], new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[2]
		{
			new GeneratedClrTypeInfo(typeof(STRDiceActivityLocalConfigure), STRDiceActivityLocalConfigure.Parser, new string[6] { "Id", "Simplified", "English", "Japanese", "Traditional", "Korean" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(STRDiceActivityConfigure), STRDiceActivityConfigure.Parser, new string[2] { "Locals", "LocalDict" }, null, null, null, new GeneratedClrTypeInfo[1])
		}));
	}
}
