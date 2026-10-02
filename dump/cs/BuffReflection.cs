using System;
using Google.Protobuf.Reflection;

public static class BuffReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static BuffReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("CgpCdWZmLnByb3RvGgpFbnVtLnByb3RvIqkFChFCdWZmSW5mb0NvbmZpZ3Vy" + "ZRIKCgJpZBgBIAEoDxIbCghidWZmVHlwZRgCIAEoDjIJLkJ1ZmZUeXBlEiEK" + "C2J1ZmZUYWdUeXBlGAMgAygOMgwuQnVmZlRhZ1R5cGUSLwoSYnVmZlJvdW5k" + "Q291bnRUeXBlGAQgASgOMhMuQnVmZlJvdW5kQ291bnRUeXBlEhIKCmRlbGF5" + "Um91bmQYBSABKA8SEQoJa2VlcFJvdW5kGAYgASgFEhYKDmlzVHJpZ2dlckNs" + "ZWFyGAcgASgIEhQKDGlzRGVhdGhDbGVhchgIIAEoCBIVCg1lZmZlY3RDYXJk" + "SWRzGAkgAygPEj8KGmJ1ZmZDb25mbGljdE1hbmFnZW1lbnRUeXBlGAogASgO" + "MhsuQnVmZkNvbmZsaWN0TWFuYWdlbWVudFR5cGUSDgoGaXNTaG93GAsgASgI" + "EgwKBGljb24YDCABKAkSHgoWaXNTaG93SWNvblByb2dyZXNzWmVybxgNIAEo" + "CBIOCgZuYW1lSWQYDiABKA8SDgoGZGVzY0lkGA8gASgPEhQKDHBlcmZvcm1T" + "dGFydBgQIAEoDxISCgpwZXJmb3JtRW5kGBEgASgPEhQKDHBlcmZvcm1Bd2Fr" + "ZRgSIAEoDxIWCg5wZXJmb3JtRGVzdHJveRgTIAEoDxIQCghlZmZlY3RJRBgU" + "IAEoDxJIChNza2luUmVwbGFjZUVmZmVjdElEGBUgAygLMisuQnVmZkluZm9D" + "b25maWd1cmUuU2tpblJlcGxhY2VFZmZlY3RJREVudHJ5EhwKFGlzU2hvd0Vm" + "ZmVjdER1cmluZ1BLGBYgASgIGjoKGFNraW5SZXBsYWNlRWZmZWN0SURFbnRy" + "eRILCgNrZXkYASABKA8SDQoFdmFsdWUYAiABKA86AjgBIqcBCg1CdWZmQ29u" + "ZmlndXJlEiEKBUluZm9zGAEgAygLMhIuQnVmZkluZm9Db25maWd1cmUSLgoI" + "SW5mb0RpY3QYAiADKAsyHC5CdWZmQ29uZmlndXJlLkluZm9EaWN0RW50cnka" + "QwoNSW5mb0RpY3RFbnRyeRILCgNrZXkYASABKA8SIQoFdmFsdWUYAiABKAsy" + "Ei5CdWZmSW5mb0NvbmZpZ3VyZToCOAFiBnByb3RvMw=="), new FileDescriptor[1] { EnumReflection.Descriptor }, new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[2]
		{
			new GeneratedClrTypeInfo(typeof(BuffInfoConfigure), BuffInfoConfigure.Parser, new string[22]
			{
				"Id", "BuffType", "BuffTagType", "BuffRoundCountType", "DelayRound", "KeepRound", "IsTriggerClear", "IsDeathClear", "EffectCardIds", "BuffConflictManagementType",
				"IsShow", "Icon", "IsShowIconProgressZero", "NameId", "DescId", "PerformStart", "PerformEnd", "PerformAwake", "PerformDestroy", "EffectID",
				"SkinReplaceEffectID", "IsShowEffectDuringPK"
			}, null, null, null, new GeneratedClrTypeInfo[1]),
			new GeneratedClrTypeInfo(typeof(BuffConfigure), BuffConfigure.Parser, new string[2] { "Infos", "InfoDict" }, null, null, null, new GeneratedClrTypeInfo[1])
		}));
	}
}
