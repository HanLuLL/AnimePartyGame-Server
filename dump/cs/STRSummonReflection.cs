using System;
using Google.Protobuf.Reflection;

public static class STRSummonReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static STRSummonReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("Cg9TVFJTdW1tb24ucHJvdG8igQEKF1NUUlN1bW1vbkxvY2FsQ29uZmlndXJl" + "EgoKAmlkGAEgASgPEhIKCnNpbXBsaWZpZWQYAiABKAkSDwoHZW5nbGlzaBgD" + "IAEoCRIQCghqYXBhbmVzZRgEIAEoCRITCgt0cmFkaXRpb25hbBgFIAEoCRIO" + "CgZrb3JlYW4YBiABKAkiwQEKElNUUlN1bW1vbkNvbmZpZ3VyZRIoCgZMb2Nh" + "bHMYASADKAsyGC5TVFJTdW1tb25Mb2NhbENvbmZpZ3VyZRI1CglMb2NhbERp" + "Y3QYAiADKAsyIi5TVFJTdW1tb25Db25maWd1cmUuTG9jYWxEaWN0RW50cnka" + "SgoOTG9jYWxEaWN0RW50cnkSCwoDa2V5GAEgASgPEicKBXZhbHVlGAIgASgL" + "MhguU1RSU3VtbW9uTG9jYWxDb25maWd1cmU6AjgBYgZwcm90bzM="), new FileDescriptor[0], new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[2]
		{
			new GeneratedClrTypeInfo(typeof(STRSummonLocalConfigure), STRSummonLocalConfigure.Parser, new string[6] { "Id", "Simplified", "English", "Japanese", "Traditional", "Korean" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(STRSummonConfigure), STRSummonConfigure.Parser, new string[2] { "Locals", "LocalDict" }, null, null, null, new GeneratedClrTypeInfo[1])
		}));
	}
}
