using System;
using Google.Protobuf.Reflection;

public static class STRSinglePlayerReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static STRSinglePlayerReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("ChVTVFJTaW5nbGVQbGF5ZXIucHJvdG8ihwEKHVNUUlNpbmdsZVBsYXllckxv" + "Y2FsQ29uZmlndXJlEgoKAmlkGAEgASgPEhIKCnNpbXBsaWZpZWQYAiABKAkS" + "DwoHZW5nbGlzaBgDIAEoCRIQCghqYXBhbmVzZRgEIAEoCRITCgt0cmFkaXRp" + "b25hbBgFIAEoCRIOCgZrb3JlYW4YBiABKAki2QEKGFNUUlNpbmdsZVBsYXll" + "ckNvbmZpZ3VyZRIuCgZMb2NhbHMYASADKAsyHi5TVFJTaW5nbGVQbGF5ZXJM" + "b2NhbENvbmZpZ3VyZRI7CglMb2NhbERpY3QYAiADKAsyKC5TVFJTaW5nbGVQ" + "bGF5ZXJDb25maWd1cmUuTG9jYWxEaWN0RW50cnkaUAoOTG9jYWxEaWN0RW50" + "cnkSCwoDa2V5GAEgASgPEi0KBXZhbHVlGAIgASgLMh4uU1RSU2luZ2xlUGxh" + "eWVyTG9jYWxDb25maWd1cmU6AjgBYgZwcm90bzM="), new FileDescriptor[0], new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[2]
		{
			new GeneratedClrTypeInfo(typeof(STRSinglePlayerLocalConfigure), STRSinglePlayerLocalConfigure.Parser, new string[6] { "Id", "Simplified", "English", "Japanese", "Traditional", "Korean" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(STRSinglePlayerConfigure), STRSinglePlayerConfigure.Parser, new string[2] { "Locals", "LocalDict" }, null, null, null, new GeneratedClrTypeInfo[1])
		}));
	}
}
