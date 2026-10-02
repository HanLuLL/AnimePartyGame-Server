using System;
using Google.Protobuf.Reflection;

public static class STRProductRecommendationReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static STRProductRecommendationReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("Ch5TVFJQcm9kdWN0UmVjb21tZW5kYXRpb24ucHJvdG8ikAEKJlNUUlByb2R1" + "Y3RSZWNvbW1lbmRhdGlvbkxvY2FsQ29uZmlndXJlEgoKAmlkGAEgASgPEhIK" + "CnNpbXBsaWZpZWQYAiABKAkSDwoHZW5nbGlzaBgDIAEoCRIQCghqYXBhbmVz" + "ZRgEIAEoCRITCgt0cmFkaXRpb25hbBgFIAEoCRIOCgZrb3JlYW4YBiABKAki" + "/QEKIVNUUlByb2R1Y3RSZWNvbW1lbmRhdGlvbkNvbmZpZ3VyZRI3CgZMb2Nh" + "bHMYASADKAsyJy5TVFJQcm9kdWN0UmVjb21tZW5kYXRpb25Mb2NhbENvbmZp" + "Z3VyZRJECglMb2NhbERpY3QYAiADKAsyMS5TVFJQcm9kdWN0UmVjb21tZW5k" + "YXRpb25Db25maWd1cmUuTG9jYWxEaWN0RW50cnkaWQoOTG9jYWxEaWN0RW50" + "cnkSCwoDa2V5GAEgASgPEjYKBXZhbHVlGAIgASgLMicuU1RSUHJvZHVjdFJl" + "Y29tbWVuZGF0aW9uTG9jYWxDb25maWd1cmU6AjgBYgZwcm90bzM="), new FileDescriptor[0], new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[2]
		{
			new GeneratedClrTypeInfo(typeof(STRProductRecommendationLocalConfigure), STRProductRecommendationLocalConfigure.Parser, new string[6] { "Id", "Simplified", "English", "Japanese", "Traditional", "Korean" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(STRProductRecommendationConfigure), STRProductRecommendationConfigure.Parser, new string[2] { "Locals", "LocalDict" }, null, null, null, new GeneratedClrTypeInfo[1])
		}));
	}
}
