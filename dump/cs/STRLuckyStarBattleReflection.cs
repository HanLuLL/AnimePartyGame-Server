using System;
using Google.Protobuf.Reflection;

public static class STRLuckyStarBattleReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static STRLuckyStarBattleReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("ChhTVFJMdWNreVN0YXJCYXR0bGUucHJvdG8iigEKIFNUUkx1Y2t5U3RhckJh" + "dHRsZUxvY2FsQ29uZmlndXJlEgoKAmlkGAEgASgPEhIKCnNpbXBsaWZpZWQY" + "AiABKAkSDwoHZW5nbGlzaBgDIAEoCRIQCghqYXBhbmVzZRgEIAEoCRITCgt0" + "cmFkaXRpb25hbBgFIAEoCRIOCgZrb3JlYW4YBiABKAki5QEKG1NUUkx1Y2t5" + "U3RhckJhdHRsZUNvbmZpZ3VyZRIxCgZMb2NhbHMYASADKAsyIS5TVFJMdWNr" + "eVN0YXJCYXR0bGVMb2NhbENvbmZpZ3VyZRI+CglMb2NhbERpY3QYAiADKAsy" + "Ky5TVFJMdWNreVN0YXJCYXR0bGVDb25maWd1cmUuTG9jYWxEaWN0RW50cnka" + "UwoOTG9jYWxEaWN0RW50cnkSCwoDa2V5GAEgASgPEjAKBXZhbHVlGAIgASgL" + "MiEuU1RSTHVja3lTdGFyQmF0dGxlTG9jYWxDb25maWd1cmU6AjgBYgZwcm90" + "bzM="), new FileDescriptor[0], new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[2]
		{
			new GeneratedClrTypeInfo(typeof(STRLuckyStarBattleLocalConfigure), STRLuckyStarBattleLocalConfigure.Parser, new string[6] { "Id", "Simplified", "English", "Japanese", "Traditional", "Korean" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(STRLuckyStarBattleConfigure), STRLuckyStarBattleConfigure.Parser, new string[2] { "Locals", "LocalDict" }, null, null, null, new GeneratedClrTypeInfo[1])
		}));
	}
}
