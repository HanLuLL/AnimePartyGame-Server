using System;
using Google.Protobuf.Reflection;

public static class MonsterReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static MonsterReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("Cg1Nb25zdGVyLnByb3RvGgpFbnVtLnByb3RvIoIEChRNb25zdGVySW5mb0Nv" + "bmZpZ3VyZRIKCgJpZBgBIAEoDxITCgtvcmRlcldlaWdodBgCIAEoDxIOCgZu" + "YW1lSUQYAyABKA8SDgoGbmlja0lEGAQgASgPEhEKCWNvbW1lbnRJZBgFIAEo" + "DxITCgtiaW9ncmFwaHlJRBgGIAEoDxIVCg1pc0dhbGxlcnlTaG93GAcgASgI" + "EhMKC21lY2hhbmlzbUlEGAggASgPEiAKCGhlcm9UeXBlGAkgASgOMg4uQ2hh" + "cmFjdGVyVHlwZRIhCgttb25zdGVyVHlwZRgKIAEoDjIMLk1vbnN0ZXJUeXBl" + "EikKD21vbnN0ZXJUeXBlUmFjZRgLIAMoDjIQLk1vbnN0ZXJSYWNlVHlwZRIi" + "Cgd0YWdUeXBlGAwgASgOMhEuQ2hhcmFjdGVyVGFnVHlwZRIMCgRnb2xkGA0g" + "ASgPEg0KBWJsb29kGA4gASgPEg4KBmF0dGFjaxgPIAEoDxIPCgdkZWZlbnNl" + "GBAgASgPEhMKC2FjdGl2ZVNraWxsGBEgASgPEhUKDXBhc3NpdmVTa2lsbHMY" + "EiADKA8SFAoMY2hhcmFjdGVyTWFwGBMgASgJEhMKC29mZnNldEluTWFwGBQg" + "AygPEhgKEHN0YW5kaW5nUGFpbnRpbmcYFSABKA8SEgoKY2FuQ291bnRlchgW" + "IAEoCCJvChlNb25zdGVyQXR0cmlidXRlQ29uZmlndXJlEgoKAmlkGAEgASgP" + "EkYKHm1vbnN0ZXJBdHRyaWJ1dGVDb25maWd1cmVJdGVtcxgCIAMoCzIeLk1v" + "bnN0ZXJBdHRyaWJ1dGVDb25maWd1cmVJdGVtIq0BCh1Nb25zdGVyQXR0cmli" + "dXRlQ29uZmlndXJlSXRlbRINCgVpbmRleBgBIAEoDxINCgVibG9vZBgCIAEo" + "DxIOCgZhdHRhY2sYAyABKA8SDwoHZGVmZW5zZRgEIAEoDxIWCg5wdmVBY3Rp" + "dmVTa2lsbBgFIAEoDxIbChNwdmVBY3RpdmVTa2lsbEV4dHJhGAYgAygPEhgK" + "EHB2ZVBhc3NpdmVTa2lsbHMYByADKA8i8gIKEE1vbnN0ZXJDb25maWd1cmUS" + "JAoFSW5mb3MYASADKAsyFS5Nb25zdGVySW5mb0NvbmZpZ3VyZRIxCghJbmZv" + "RGljdBgCIAMoCzIfLk1vbnN0ZXJDb25maWd1cmUuSW5mb0RpY3RFbnRyeRIu" + "CgpBdHRyaWJ1dGVzGAMgAygLMhouTW9uc3RlckF0dHJpYnV0ZUNvbmZpZ3Vy" + "ZRI7Cg1BdHRyaWJ1dGVEaWN0GAQgAygLMiQuTW9uc3RlckNvbmZpZ3VyZS5B" + "dHRyaWJ1dGVEaWN0RW50cnkaRgoNSW5mb0RpY3RFbnRyeRILCgNrZXkYASAB" + "KA8SJAoFdmFsdWUYAiABKAsyFS5Nb25zdGVySW5mb0NvbmZpZ3VyZToCOAEa" + "UAoSQXR0cmlidXRlRGljdEVudHJ5EgsKA2tleRgBIAEoDxIpCgV2YWx1ZRgC" + "IAEoCzIaLk1vbnN0ZXJBdHRyaWJ1dGVDb25maWd1cmU6AjgBYgZwcm90bzM="), new FileDescriptor[1] { EnumReflection.Descriptor }, new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[4]
		{
			new GeneratedClrTypeInfo(typeof(MonsterInfoConfigure), MonsterInfoConfigure.Parser, new string[22]
			{
				"Id", "OrderWeight", "NameID", "NickID", "CommentId", "BiographyID", "IsGalleryShow", "MechanismID", "HeroType", "MonsterType",
				"MonsterTypeRace", "TagType", "Gold", "Blood", "Attack", "Defense", "ActiveSkill", "PassiveSkills", "CharacterMap", "OffsetInMap",
				"StandingPainting", "CanCounter"
			}, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(MonsterAttributeConfigure), MonsterAttributeConfigure.Parser, new string[2] { "Id", "MonsterAttributeConfigureItems" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(MonsterAttributeConfigureItem), MonsterAttributeConfigureItem.Parser, new string[7] { "Index", "Blood", "Attack", "Defense", "PveActiveSkill", "PveActiveSkillExtra", "PvePassiveSkills" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(MonsterConfigure), MonsterConfigure.Parser, new string[4] { "Infos", "InfoDict", "Attributes", "AttributeDict" }, null, null, null, new GeneratedClrTypeInfo[2])
		}));
	}
}
