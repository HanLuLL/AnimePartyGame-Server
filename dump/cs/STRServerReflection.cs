using System;
using Google.Protobuf.Reflection;

public static class STRServerReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static STRServerReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("Cg9TVFJTZXJ2ZXIucHJvdG8igQEKF1NUUlNlcnZlckxvY2FsQ29uZmlndXJl" + "EgoKAmlkGAEgASgPEhIKCnNpbXBsaWZpZWQYAiABKAkSDwoHZW5nbGlzaBgD" + "IAEoCRIQCghqYXBhbmVzZRgEIAEoCRITCgt0cmFkaXRpb25hbBgFIAEoCRIO" + "CgZrb3JlYW4YBiABKAkiwQEKElNUUlNlcnZlckNvbmZpZ3VyZRIoCgZMb2Nh" + "bHMYASADKAsyGC5TVFJTZXJ2ZXJMb2NhbENvbmZpZ3VyZRI1CglMb2NhbERp" + "Y3QYAiADKAsyIi5TVFJTZXJ2ZXJDb25maWd1cmUuTG9jYWxEaWN0RW50cnka" + "SgoOTG9jYWxEaWN0RW50cnkSCwoDa2V5GAEgASgPEicKBXZhbHVlGAIgASgL" + "MhguU1RSU2VydmVyTG9jYWxDb25maWd1cmU6AjgBYgZwcm90bzM="), new FileDescriptor[0], new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[2]
		{
			new GeneratedClrTypeInfo(typeof(STRServerLocalConfigure), STRServerLocalConfigure.Parser, new string[6] { "Id", "Simplified", "English", "Japanese", "Traditional", "Korean" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(STRServerConfigure), STRServerConfigure.Parser, new string[2] { "Locals", "LocalDict" }, null, null, null, new GeneratedClrTypeInfo[1])
		}));
	}
}
