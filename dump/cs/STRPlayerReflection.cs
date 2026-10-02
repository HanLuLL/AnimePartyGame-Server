using System;
using Google.Protobuf.Reflection;

public static class STRPlayerReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static STRPlayerReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("Cg9TVFJQbGF5ZXIucHJvdG8igQEKF1NUUlBsYXllckxvY2FsQ29uZmlndXJl" + "EgoKAmlkGAEgASgPEhIKCnNpbXBsaWZpZWQYAiABKAkSDwoHZW5nbGlzaBgD" + "IAEoCRIQCghqYXBhbmVzZRgEIAEoCRITCgt0cmFkaXRpb25hbBgFIAEoCRIO" + "CgZrb3JlYW4YBiABKAkiwQEKElNUUlBsYXllckNvbmZpZ3VyZRIoCgZMb2Nh" + "bHMYASADKAsyGC5TVFJQbGF5ZXJMb2NhbENvbmZpZ3VyZRI1CglMb2NhbERp" + "Y3QYAiADKAsyIi5TVFJQbGF5ZXJDb25maWd1cmUuTG9jYWxEaWN0RW50cnka" + "SgoOTG9jYWxEaWN0RW50cnkSCwoDa2V5GAEgASgPEicKBXZhbHVlGAIgASgL" + "MhguU1RSUGxheWVyTG9jYWxDb25maWd1cmU6AjgBYgZwcm90bzM="), new FileDescriptor[0], new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[2]
		{
			new GeneratedClrTypeInfo(typeof(STRPlayerLocalConfigure), STRPlayerLocalConfigure.Parser, new string[6] { "Id", "Simplified", "English", "Japanese", "Traditional", "Korean" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(STRPlayerConfigure), STRPlayerConfigure.Parser, new string[2] { "Locals", "LocalDict" }, null, null, null, new GeneratedClrTypeInfo[1])
		}));
	}
}
