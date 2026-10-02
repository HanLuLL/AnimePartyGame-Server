using System;
using Google.Protobuf.Reflection;

public static class SkinReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static SkinReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("CgpTa2luLnByb3RvGgpFbnVtLnByb3RvInsKHVNraW5TdGFuZGluZ1BhaW50" + "aW5nQ29uZmlndXJlEgoKAmlkGAEgASgPEk4KInNraW5TdGFuZGluZ1BhaW50" + "aW5nQ29uZmlndXJlSXRlbXMYAiADKAsyIi5Ta2luU3RhbmRpbmdQYWludGlu" + "Z0NvbmZpZ3VyZUl0ZW0ivwYKIVNraW5TdGFuZGluZ1BhaW50aW5nQ29uZmln" + "dXJlSXRlbRINCgVpbmRleBgBIAEoDxIRCglpc0RlZmF1bHQYAiABKAgSDgoG" + "aXRlbUlEGAMgASgPEi8KEnNraW5BcHBlYXJhbmNlVHlwZRgEIAEoDjITLlNr" + "aW5BcHBlYXJhbmNlVHlwZRIRCglza2luU2NhbGUYBSADKAISEAoIbGFuZElj" + "b24YBiABKAkSEQoJY2hhcmFjdGVyGAcgASgJEhQKDHNmd0NoYXJhY3RlchgI" + "IAEoCRIXCg9pbkdhbWVDaGFyYWN0ZXIYCSABKAkSGgoSaW5HYW1lQ2hhcmFj" + "dGVyU0ZXGAogASgJEhgKEGNoYXJhY3RlckxldmVsVXAYCyABKAkSGwoTY2hh" + "cmFjdGVyQmF0dGxsZVJlcxgMIAEoDxIWCg5jaGFyYWN0ZXJMYWJlbBgNIAEo" + "CRIUCgxwcm9maWxlUGhvdG8YDiABKAkSDAoEYnVzdBgPIAMoCRIPCgdzZndC" + "dXN0GBAgAygJEhYKDmNoYXJhY3RlclJlYWR5GBEgASgJEhkKEXNmd0NoYXJh" + "Y3RlclJlYWR5GBIgASgJEhUKDWNoYXJhY3RlclRoaW4YEyABKAkSGAoQc2Z3" + "Q2hhcmFjdGVyVGhpbhgUIAEoCRISCgpza2lsbFZpZGVvGBUgASgJEhUKDXNm" + "d1NraWxsVmlkZW8YFiABKAkSEQoJcm9sZVBob3RvGBcgASgJEhQKDHNmd1Jv" + "bGVQaG90bxgYIAEoCRIWCg5mYW5mYXJlUGVyZm9ybRgZIAEoDxIUCgxwcmV2" + "aWV3VmlkZW8YGiABKAkSEwoLZmlnaHRWaWRlb3MYGyADKA8SFwoPZmlnaHRC" + "YWNrR3JvdW5kGBwgASgJEhkKEWZpZ2h0QmFja0dyb3VuZFBQGB0gASgPEhMK" + "C3NvdW5kYmFua2lkGB4gAygPEhAKCGZpZ2h0QkdNGB8gASgPEg0KBXZvaWNl" + "GCAgASgPEg8KB2FjdGlvbnMYISADKAkSEQoJbW92ZVZmeElkGCIgASgPEhIK" + "CmxvbmdUZXh0SUQYIyABKA8SEwoLc2hvcnRUZXh0SUQYJCABKA8i4AEKE1Nr" + "aW5PZmZzZXRDb25maWd1cmUSCgoCaWQYASABKA8SEQoJaW1hZ2VOYW1lGAIg" + "ASgJEg8KB29mZnNldFgYAyABKAISDwoHb2Zmc2V0WRgEIAEoAhIhChljaGFy" + "YWN0ZXJTdGF0ZVNraW5PZmZzZXRYGAUgASgCEiEKGWNoYXJhY3RlclN0YXRl" + "U2tpbk9mZnNldFkYBiABKAISHwoXY2hhcmFjdGVyU3RhdGVTa2luU2NhbGUY" + "ByABKAISIQoZaXNDaGFyYWN0ZXJTdGF0ZVNraW5GbGlwWBgIIAEoCCJkChhT" + "a2luU2tpblBlbmRhbnRDb25maWd1cmUSCgoCaWQYASABKA8SEQoJcGVuZGFu" + "dElkGAIgASgPEhYKDnJlcGxhY2VQZXJmb3JtGAMgASgPEhEKCXNob3dWaWRl" + "bxgEIAEoCSLVBAoNU2tpbkNvbmZpZ3VyZRI5ChFTdGFuZGluZ1BhaW50aW5n" + "cxgBIAMoCzIeLlNraW5TdGFuZGluZ1BhaW50aW5nQ29uZmlndXJlEkYKFFN0" + "YW5kaW5nUGFpbnRpbmdEaWN0GAIgAygLMiguU2tpbkNvbmZpZ3VyZS5TdGFu" + "ZGluZ1BhaW50aW5nRGljdEVudHJ5EiUKB09mZnNldHMYAyADKAsyFC5Ta2lu" + "T2Zmc2V0Q29uZmlndXJlEjIKCk9mZnNldERpY3QYBCADKAsyHi5Ta2luQ29u" + "ZmlndXJlLk9mZnNldERpY3RFbnRyeRIvCgxTa2luUGVuZGFudHMYBSADKAsy" + "GS5Ta2luU2tpblBlbmRhbnRDb25maWd1cmUSPAoPU2tpblBlbmRhbnREaWN0" + "GAYgAygLMiMuU2tpbkNvbmZpZ3VyZS5Ta2luUGVuZGFudERpY3RFbnRyeRpb" + "ChlTdGFuZGluZ1BhaW50aW5nRGljdEVudHJ5EgsKA2tleRgBIAEoDxItCgV2" + "YWx1ZRgCIAEoCzIeLlNraW5TdGFuZGluZ1BhaW50aW5nQ29uZmlndXJlOgI4" + "ARpHCg9PZmZzZXREaWN0RW50cnkSCwoDa2V5GAEgASgPEiMKBXZhbHVlGAIg" + "ASgLMhQuU2tpbk9mZnNldENvbmZpZ3VyZToCOAEaUQoUU2tpblBlbmRhbnRE" + "aWN0RW50cnkSCwoDa2V5GAEgASgPEigKBXZhbHVlGAIgASgLMhkuU2tpblNr" + "aW5QZW5kYW50Q29uZmlndXJlOgI4AWIGcHJvdG8z"), new FileDescriptor[1] { EnumReflection.Descriptor }, new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[5]
		{
			new GeneratedClrTypeInfo(typeof(SkinStandingPaintingConfigure), SkinStandingPaintingConfigure.Parser, new string[2] { "Id", "SkinStandingPaintingConfigureItems" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(SkinStandingPaintingConfigureItem), SkinStandingPaintingConfigureItem.Parser, new string[36]
			{
				"Index", "IsDefault", "ItemID", "SkinAppearanceType", "SkinScale", "LandIcon", "Character", "SfwCharacter", "InGameCharacter", "InGameCharacterSFW",
				"CharacterLevelUp", "CharacterBattlleRes", "CharacterLabel", "ProfilePhoto", "Bust", "SfwBust", "CharacterReady", "SfwCharacterReady", "CharacterThin", "SfwCharacterThin",
				"SkillVideo", "SfwSkillVideo", "RolePhoto", "SfwRolePhoto", "FanfarePerform", "PreviewVideo", "FightVideos", "FightBackGround", "FightBackGroundPP", "Soundbankid",
				"FightBGM", "Voice", "Actions", "MoveVfxId", "LongTextID", "ShortTextID"
			}, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(SkinOffsetConfigure), SkinOffsetConfigure.Parser, new string[8] { "Id", "ImageName", "OffsetX", "OffsetY", "CharacterStateSkinOffsetX", "CharacterStateSkinOffsetY", "CharacterStateSkinScale", "IsCharacterStateSkinFlipX" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(SkinSkinPendantConfigure), SkinSkinPendantConfigure.Parser, new string[4] { "Id", "PendantId", "ReplacePerform", "ShowVideo" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(SkinConfigure), SkinConfigure.Parser, new string[6] { "StandingPaintings", "StandingPaintingDict", "Offsets", "OffsetDict", "SkinPendants", "SkinPendantDict" }, null, null, null, new GeneratedClrTypeInfo[3])
		}));
	}
}
