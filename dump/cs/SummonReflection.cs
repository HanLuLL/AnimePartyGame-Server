using System;
using Google.Protobuf.Reflection;

public static class SummonReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static SummonReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("CgxTdW1tb24ucHJvdG8aCkVudW0ucHJvdG8i2AIKE1N1bW1vbkluZm9Db25m" + "aWd1cmUSCgoCaWQYASABKA8SHwoKc3VtbW9uVHlwZRgCIAEoDjILLlN1bW1v" + "blR5cGUSDgoGbmFtZUlEGAMgASgPEhoKEnN1bW1vblByb2ZpbGVQaG90bxgE" + "IAEoCRISCgpib3JuRWZmZWN0GAUgASgPEhIKCnByZWZhYk5hbWUYBiABKAkS" + "FgoOY29tbW9uUGVyZm9ybXMYByADKA8SEwoLcGVyZm9ybVNob3cYCCABKA8S" + "FgoOZGVidWZmUGVyZm9ybXMYCSADKA8SFAoMYnVmZlBlcmZvcm1zGAogAygP" + "EhEKCU1hcGNhcmRJRBgLIAEoDxIOCgZwYXJhbXMYDCADKBESDwoHYnVmZklk" + "cxgNIAMoDxIZChFpc0ZvbGxvd1N1bW1vbmluZxgOIAEoCBIWCg5pc1BlcmZv" + "cm1Hcm91cBgPIAEoCCKvAQoPU3VtbW9uQ29uZmlndXJlEiMKBUluZm9zGAEg" + "AygLMhQuU3VtbW9uSW5mb0NvbmZpZ3VyZRIwCghJbmZvRGljdBgCIAMoCzIe" + "LlN1bW1vbkNvbmZpZ3VyZS5JbmZvRGljdEVudHJ5GkUKDUluZm9EaWN0RW50" + "cnkSCwoDa2V5GAEgASgPEiMKBXZhbHVlGAIgASgLMhQuU3VtbW9uSW5mb0Nv" + "bmZpZ3VyZToCOAFiBnByb3RvMw=="), new FileDescriptor[1] { EnumReflection.Descriptor }, new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[2]
		{
			new GeneratedClrTypeInfo(typeof(SummonInfoConfigure), SummonInfoConfigure.Parser, new string[15]
			{
				"Id", "SummonType", "NameID", "SummonProfilePhoto", "BornEffect", "PrefabName", "CommonPerforms", "PerformShow", "DebuffPerforms", "BuffPerforms",
				"MapcardID", "Params", "BuffIds", "IsFollowSummoning", "IsPerformGroup"
			}, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(SummonConfigure), SummonConfigure.Parser, new string[2] { "Infos", "InfoDict" }, null, null, null, new GeneratedClrTypeInfo[1])
		}));
	}
}
