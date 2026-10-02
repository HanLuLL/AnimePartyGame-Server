using System;
using Google.Protobuf.Reflection;

public static class AchieveReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static AchieveReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("Cg1BY2hpZXZlLnByb3RvGgpFbnVtLnByb3RvIq4BChRBY2hpZXZlSW5mb0Nv" + "bmZpZ3VyZRItChFmaW5pc2hBY2hpZXZlVHlwZRgBIAEoDjISLkZpbmlzaEFj" + "aGlldmVUeXBlEhIKCmlzcG9zaXRpdmUYAiABKAgSHAoUQWNoaWV2ZVBpY1Nl" + "dHRsZW1lbnQYAyABKAkSFQoNQWNoaWV2ZWRlc2NJZBgEIAEoDxIOCgZkZXNj" + "SWQYBSABKA8SDgoGd2VpZ2h0GAYgASgPIvkBChZBY2hpZXZlR2xvYmFsQ29u" + "ZmlndXJlEgoKAmlkGAEgASgPEiUKDWNvbmRpdGlvblR5cGUYAiABKA4yDi5D" + "b25kaXRpb25UeXBlEg0KBXBhcmFtGAMgASgPEgsKA3JlZhgEIAEoDxIMCgRp" + "Y29uGAUgASgPEg4KBm5hbWVJRBgGIAEoDxIOCgZkZXNjSWQYByABKA8SMwoG" + "cmV3YXJkGAggAygLMiMuQWNoaWV2ZUdsb2JhbENvbmZpZ3VyZS5SZXdhcmRF" + "bnRyeRotCgtSZXdhcmRFbnRyeRILCgNrZXkYASABKA8SDQoFdmFsdWUYAiAB" + "KA86AjgBIjMKFkFjaGlldmVTaGllbGRDb25maWd1cmUSGQoRc2hpZWxkQ2hh" + "cmFjdGVySWQYASABKA8ijQQKEEFjaGlldmVDb25maWd1cmUSJAoFSW5mb3MY" + "ASADKAsyFS5BY2hpZXZlSW5mb0NvbmZpZ3VyZRIxCghJbmZvRGljdBgCIAMo" + "CzIfLkFjaGlldmVDb25maWd1cmUuSW5mb0RpY3RFbnRyeRIoCgdHbG9iYWxz" + "GAMgAygLMhcuQWNoaWV2ZUdsb2JhbENvbmZpZ3VyZRI1CgpHbG9iYWxEaWN0" + "GAQgAygLMiEuQWNoaWV2ZUNvbmZpZ3VyZS5HbG9iYWxEaWN0RW50cnkSKAoH" + "U2hpZWxkcxgFIAMoCzIXLkFjaGlldmVTaGllbGRDb25maWd1cmUSNQoKU2hp" + "ZWxkRGljdBgGIAMoCzIhLkFjaGlldmVDb25maWd1cmUuU2hpZWxkRGljdEVu" + "dHJ5GkYKDUluZm9EaWN0RW50cnkSCwoDa2V5GAEgASgPEiQKBXZhbHVlGAIg" + "ASgLMhUuQWNoaWV2ZUluZm9Db25maWd1cmU6AjgBGkoKD0dsb2JhbERpY3RF" + "bnRyeRILCgNrZXkYASABKA8SJgoFdmFsdWUYAiABKAsyFy5BY2hpZXZlR2xv" + "YmFsQ29uZmlndXJlOgI4ARpKCg9TaGllbGREaWN0RW50cnkSCwoDa2V5GAEg" + "ASgPEiYKBXZhbHVlGAIgASgLMhcuQWNoaWV2ZVNoaWVsZENvbmZpZ3VyZToC" + "OAFiBnByb3RvMw=="), new FileDescriptor[1] { EnumReflection.Descriptor }, new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[4]
		{
			new GeneratedClrTypeInfo(typeof(AchieveInfoConfigure), AchieveInfoConfigure.Parser, new string[6] { "FinishAchieveType", "Ispositive", "AchievePicSettlement", "AchievedescId", "DescId", "Weight" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(AchieveGlobalConfigure), AchieveGlobalConfigure.Parser, new string[8] { "Id", "ConditionType", "Param", "Ref", "Icon", "NameID", "DescId", "Reward" }, null, null, null, new GeneratedClrTypeInfo[1]),
			new GeneratedClrTypeInfo(typeof(AchieveShieldConfigure), AchieveShieldConfigure.Parser, new string[1] { "ShieldCharacterId" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(AchieveConfigure), AchieveConfigure.Parser, new string[6] { "Infos", "InfoDict", "Globals", "GlobalDict", "Shields", "ShieldDict" }, null, null, null, new GeneratedClrTypeInfo[3])
		}));
	}
}
