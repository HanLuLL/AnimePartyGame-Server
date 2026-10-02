using System;
using Google.Protobuf.Reflection;

public static class STRDay7GiftPackageReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static STRDay7GiftPackageReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("ChhTVFJEYXk3R2lmdFBhY2thZ2UucHJvdG8iigEKIFNUUkRheTdHaWZ0UGFj" + "a2FnZUxvY2FsQ29uZmlndXJlEgoKAmlkGAEgASgPEhIKCnNpbXBsaWZpZWQY" + "AiABKAkSDwoHZW5nbGlzaBgDIAEoCRIQCghqYXBhbmVzZRgEIAEoCRITCgt0" + "cmFkaXRpb25hbBgFIAEoCRIOCgZrb3JlYW4YBiABKAki5QEKG1NUUkRheTdH" + "aWZ0UGFja2FnZUNvbmZpZ3VyZRIxCgZMb2NhbHMYASADKAsyIS5TVFJEYXk3" + "R2lmdFBhY2thZ2VMb2NhbENvbmZpZ3VyZRI+CglMb2NhbERpY3QYAiADKAsy" + "Ky5TVFJEYXk3R2lmdFBhY2thZ2VDb25maWd1cmUuTG9jYWxEaWN0RW50cnka" + "UwoOTG9jYWxEaWN0RW50cnkSCwoDa2V5GAEgASgPEjAKBXZhbHVlGAIgASgL" + "MiEuU1RSRGF5N0dpZnRQYWNrYWdlTG9jYWxDb25maWd1cmU6AjgBYgZwcm90" + "bzM="), new FileDescriptor[0], new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[2]
		{
			new GeneratedClrTypeInfo(typeof(STRDay7GiftPackageLocalConfigure), STRDay7GiftPackageLocalConfigure.Parser, new string[6] { "Id", "Simplified", "English", "Japanese", "Traditional", "Korean" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(STRDay7GiftPackageConfigure), STRDay7GiftPackageConfigure.Parser, new string[2] { "Locals", "LocalDict" }, null, null, null, new GeneratedClrTypeInfo[1])
		}));
	}
}
