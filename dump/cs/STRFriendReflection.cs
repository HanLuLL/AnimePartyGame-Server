using System;
using Google.Protobuf.Reflection;

public static class STRFriendReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static STRFriendReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("Cg9TVFJGcmllbmQucHJvdG8igQEKF1NUUkZyaWVuZExvY2FsQ29uZmlndXJl" + "EgoKAmlkGAEgASgPEhIKCnNpbXBsaWZpZWQYAiABKAkSDwoHZW5nbGlzaBgD" + "IAEoCRIQCghqYXBhbmVzZRgEIAEoCRITCgt0cmFkaXRpb25hbBgFIAEoCRIO" + "CgZrb3JlYW4YBiABKAkiwQEKElNUUkZyaWVuZENvbmZpZ3VyZRIoCgZMb2Nh" + "bHMYASADKAsyGC5TVFJGcmllbmRMb2NhbENvbmZpZ3VyZRI1CglMb2NhbERp" + "Y3QYAiADKAsyIi5TVFJGcmllbmRDb25maWd1cmUuTG9jYWxEaWN0RW50cnka" + "SgoOTG9jYWxEaWN0RW50cnkSCwoDa2V5GAEgASgPEicKBXZhbHVlGAIgASgL" + "MhguU1RSRnJpZW5kTG9jYWxDb25maWd1cmU6AjgBYgZwcm90bzM="), new FileDescriptor[0], new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[2]
		{
			new GeneratedClrTypeInfo(typeof(STRFriendLocalConfigure), STRFriendLocalConfigure.Parser, new string[6] { "Id", "Simplified", "English", "Japanese", "Traditional", "Korean" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(STRFriendConfigure), STRFriendConfigure.Parser, new string[2] { "Locals", "LocalDict" }, null, null, null, new GeneratedClrTypeInfo[1])
		}));
	}
}
