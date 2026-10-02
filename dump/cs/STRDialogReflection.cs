using System;
using Google.Protobuf.Reflection;

public static class STRDialogReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static STRDialogReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("Cg9TVFJEaWFsb2cucHJvdG8igQEKF1NUUkRpYWxvZ0xvY2FsQ29uZmlndXJl" + "EgoKAmlkGAEgASgPEhIKCnNpbXBsaWZpZWQYAiABKAkSDwoHZW5nbGlzaBgD" + "IAEoCRIQCghqYXBhbmVzZRgEIAEoCRITCgt0cmFkaXRpb25hbBgFIAEoCRIO" + "CgZrb3JlYW4YBiABKAkiwQEKElNUUkRpYWxvZ0NvbmZpZ3VyZRIoCgZMb2Nh" + "bHMYASADKAsyGC5TVFJEaWFsb2dMb2NhbENvbmZpZ3VyZRI1CglMb2NhbERp" + "Y3QYAiADKAsyIi5TVFJEaWFsb2dDb25maWd1cmUuTG9jYWxEaWN0RW50cnka" + "SgoOTG9jYWxEaWN0RW50cnkSCwoDa2V5GAEgASgPEicKBXZhbHVlGAIgASgL" + "MhguU1RSRGlhbG9nTG9jYWxDb25maWd1cmU6AjgBYgZwcm90bzM="), new FileDescriptor[0], new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[2]
		{
			new GeneratedClrTypeInfo(typeof(STRDialogLocalConfigure), STRDialogLocalConfigure.Parser, new string[6] { "Id", "Simplified", "English", "Japanese", "Traditional", "Korean" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(STRDialogConfigure), STRDialogConfigure.Parser, new string[2] { "Locals", "LocalDict" }, null, null, null, new GeneratedClrTypeInfo[1])
		}));
	}
}
