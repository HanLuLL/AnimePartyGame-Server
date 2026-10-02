using System;
using Google.Protobuf.Reflection;

public static class DestinyReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static DestinyReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("Cg1EZXN0aW55LnByb3RvGgpFbnVtLnByb3RvIu0BChREZXN0aW55SW5mb0Nv" + "bmZpZ3VyZRIKCgJpZBgBIAEoDxIUCgxmdW5jdGlvbk5hbWUYAiABKAkSGwoI" + "Y2FyZFR5cGUYAyABKA4yCS5DYXJkVHlwZRIOCgZuYW1lSUQYBCABKA8SEAoI" + "Y2FyZE51bWIYBSABKA8SDgoGZGVzY0lkGAYgASgPEhEKCWNvbW1lbnRJZBgH" + "IAEoDxINCgVpbWFnZRgIIAEoCRIOCgZwYXJhbXMYCSADKBESDwoHYnVmZklk" + "cxgKIAMoDxIQCghzdW1tb25JZBgLIAEoDxIPCgdwZXJmb3JtGAwgASgPIiQK" + "FkRlc3RpbnlQZXJpb2RDb25maWd1cmUSCgoCaWQYASABKA8i4AIKEERlc3Rp" + "bnlDb25maWd1cmUSJAoFSW5mb3MYASADKAsyFS5EZXN0aW55SW5mb0NvbmZp" + "Z3VyZRIxCghJbmZvRGljdBgCIAMoCzIfLkRlc3RpbnlDb25maWd1cmUuSW5m" + "b0RpY3RFbnRyeRIoCgdQZXJpb2RzGAMgAygLMhcuRGVzdGlueVBlcmlvZENv" + "bmZpZ3VyZRI1CgpQZXJpb2REaWN0GAQgAygLMiEuRGVzdGlueUNvbmZpZ3Vy" + "ZS5QZXJpb2REaWN0RW50cnkaRgoNSW5mb0RpY3RFbnRyeRILCgNrZXkYASAB" + "KA8SJAoFdmFsdWUYAiABKAsyFS5EZXN0aW55SW5mb0NvbmZpZ3VyZToCOAEa" + "SgoPUGVyaW9kRGljdEVudHJ5EgsKA2tleRgBIAEoDxImCgV2YWx1ZRgCIAEo" + "CzIXLkRlc3RpbnlQZXJpb2RDb25maWd1cmU6AjgBYgZwcm90bzM="), new FileDescriptor[1] { EnumReflection.Descriptor }, new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[3]
		{
			new GeneratedClrTypeInfo(typeof(DestinyInfoConfigure), DestinyInfoConfigure.Parser, new string[12]
			{
				"Id", "FunctionName", "CardType", "NameID", "CardNumb", "DescId", "CommentId", "Image", "Params", "BuffIds",
				"SummonId", "Perform"
			}, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(DestinyPeriodConfigure), DestinyPeriodConfigure.Parser, new string[1] { "Id" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(DestinyConfigure), DestinyConfigure.Parser, new string[4] { "Infos", "InfoDict", "Periods", "PeriodDict" }, null, null, null, new GeneratedClrTypeInfo[2])
		}));
	}
}
