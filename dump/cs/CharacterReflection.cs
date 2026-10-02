using System;
using Google.Protobuf.Reflection;

public static class CharacterReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static CharacterReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("Cg9DaGFyYWN0ZXIucHJvdG8aCkVudW0ucHJvdG8izQUKFkNoYXJhY3Rlcklu" + "Zm9Db25maWd1cmUSCgoCaWQYASABKA8SEwoLb3JkZXJXZWlnaHQYAiABKA8S" + "EQoJaXNMaW5rYWdlGAMgASgIEg4KBm5hbWVJRBgEIAEoDxIOCgZuaWNrSUQY" + "BSABKA8SEQoJY29tbWVudElkGAYgASgPEhMKC2Jpb2dyYXBoeUlEGAcgASgP" + "EhUKDWlzR2FsbGVyeVNob3cYCCABKAgSEwoLbWVjaGFuaXNtSUQYCSABKA8S" + "DwoHbGluZXNJRBgKIAEoDxITCgtuYW1lQkdDb2xvchgLIAEoCRIgCghoZXJv" + "VHlwZRgMIAEoDjIOLkNoYXJhY3RlclR5cGUSIgoHdGFnVHlwZRgNIAEoDjIR" + "LkNoYXJhY3RlclRhZ1R5cGUSEQoJaXNEZWZhdWx0GA4gASgIEg0KBWJsb29k" + "GA8gASgPEg4KBmF0dGFjaxgQIAEoDxIPCgdkZWZlbnNlGBEgASgPEhMKC2Fj" + "dGl2ZVNraWxsGBIgASgPEhYKDnB2ZUFjdGl2ZVNraWxsGBMgASgPEhUKDXBh" + "c3NpdmVTa2lsbHMYFCADKA8SGAoQcHZlUGFzc2l2ZVNraWxscxgVIAMoDxIQ" + "CghwdmVCcmVhaxgWIAMoDxIUCgxjaGFyYWN0ZXJNYXAYFyABKAkSEwoLb2Zm" + "c2V0SW5NYXAYGCADKA8SDwoHbGFuZFRleBgZIAEoCRIYChBleHByZXNzaW9u" + "UGFja0lEGBogASgPEhgKEHN0YW5kaW5nUGFpbnRpbmcYGyABKA8SEQoJaGFz" + "S2l6dW5hGBwgASgIEhUKDWhlcm9GYXZvckdpZnQYHSABKA8SGAoQZmF2b3JM" + "ZXZlbFJld2FyZBgeIAEoDxIfChdmYXZvckJyZWFrdGhyb3VnaFJld2FyZBgf" + "IAEoDxIXCg9pbnRlbnNlRml4U2tpbGwYICABKA8ihAEKIENoYXJhY3RlckV4" + "cHJlc3Npb25QYWNrQ29uZmlndXJlEgoKAmlkGAEgASgPElQKJWNoYXJhY3Rl" + "ckV4cHJlc3Npb25QYWNrQ29uZmlndXJlSXRlbXMYAiADKAsyJS5DaGFyYWN0" + "ZXJFeHByZXNzaW9uUGFja0NvbmZpZ3VyZUl0ZW0iWwokQ2hhcmFjdGVyRXhw" + "cmVzc2lvblBhY2tDb25maWd1cmVJdGVtEg4KBml0ZW1JRBgBIAEoDxIQCgh2" + "aWRlb0tleRgCIAEoCRIRCglpc0RlZmF1bHQYAyABKAgigQEKH0NoYXJhY3Rl" + "ckhlcm9GYXZvckdpZnRDb25maWd1cmUSCgoCaWQYASABKA8SUgokY2hhcmFj" + "dGVySGVyb0Zhdm9yR2lmdENvbmZpZ3VyZUl0ZW1zGAIgAygLMiQuQ2hhcmFj" + "dGVySGVyb0Zhdm9yR2lmdENvbmZpZ3VyZUl0ZW0iRAojQ2hhcmFjdGVySGVy" + "b0Zhdm9yR2lmdENvbmZpZ3VyZUl0ZW0SDQoFaW5kZXgYASABKA8SDgoGaXRl" + "bUlEGAIgASgPIrgDChdDaGFyYWN0ZXJWb2ljZUNvbmZpZ3VyZRIKCgJpZBgB" + "IAEoDxIRCglzb3VuZEJhbmsYAiABKA8SFAoMYWNoaWV2ZVZvaWNlGAMgASgP" + "EhAKCGF0a1ZvaWNlGAQgASgPEhEKCWNhcmRWb2ljZRgFIAEoDxIQCghkZWZW" + "b2ljZRgGIAEoDxISCgpldmVudFZvaWNlGAcgASgPEhEKCWtpbGxWb2ljZRgI" + "IAEoDxIRCglsdlVwVm9pY2UYCSABKA8SEQoJbW92ZVZvaWNlGAogASgPEhMK" + "C3NlbGVjdFZvaWNlGAsgASgPEhEKCXNob3BWb2ljZRgMIAEoDxISCgpza2ls" + "bFZvaWNlGA0gASgPEhAKCHdpblZvaWNlGA4gASgPEhIKCmF3YWtlVm9pY2UY" + "DyABKA8SFAoMZmFuZmFyZVZvaWNlGBAgASgPEjoKCXNob3dWb2ljZRgRIAMo" + "CzInLkNoYXJhY3RlclZvaWNlQ29uZmlndXJlLlNob3dWb2ljZUVudHJ5GjAK" + "DlNob3dWb2ljZUVudHJ5EgsKA2tleRgBIAEoDxINCgV2YWx1ZRgCIAEoDzoC" + "OAEiqAYKEkNoYXJhY3RlckNvbmZpZ3VyZRImCgVJbmZvcxgBIAMoCzIXLkNo" + "YXJhY3RlckluZm9Db25maWd1cmUSMwoISW5mb0RpY3QYAiADKAsyIS5DaGFy" + "YWN0ZXJDb25maWd1cmUuSW5mb0RpY3RFbnRyeRI6Cg9FeHByZXNzaW9uUGFj" + "a3MYAyADKAsyIS5DaGFyYWN0ZXJFeHByZXNzaW9uUGFja0NvbmZpZ3VyZRJH" + "ChJFeHByZXNzaW9uUGFja0RpY3QYBCADKAsyKy5DaGFyYWN0ZXJDb25maWd1" + "cmUuRXhwcmVzc2lvblBhY2tEaWN0RW50cnkSOAoOSGVyb0Zhdm9yR2lmdHMY" + "BSADKAsyIC5DaGFyYWN0ZXJIZXJvRmF2b3JHaWZ0Q29uZmlndXJlEkUKEUhl" + "cm9GYXZvckdpZnREaWN0GAYgAygLMiouQ2hhcmFjdGVyQ29uZmlndXJlLkhl" + "cm9GYXZvckdpZnREaWN0RW50cnkSKAoGVm9pY2VzGAcgAygLMhguQ2hhcmFj" + "dGVyVm9pY2VDb25maWd1cmUSNQoJVm9pY2VEaWN0GAggAygLMiIuQ2hhcmFj" + "dGVyQ29uZmlndXJlLlZvaWNlRGljdEVudHJ5GkgKDUluZm9EaWN0RW50cnkS" + "CwoDa2V5GAEgASgPEiYKBXZhbHVlGAIgASgLMhcuQ2hhcmFjdGVySW5mb0Nv" + "bmZpZ3VyZToCOAEaXAoXRXhwcmVzc2lvblBhY2tEaWN0RW50cnkSCwoDa2V5" + "GAEgASgPEjAKBXZhbHVlGAIgASgLMiEuQ2hhcmFjdGVyRXhwcmVzc2lvblBh" + "Y2tDb25maWd1cmU6AjgBGloKFkhlcm9GYXZvckdpZnREaWN0RW50cnkSCwoD" + "a2V5GAEgASgPEi8KBXZhbHVlGAIgASgLMiAuQ2hhcmFjdGVySGVyb0Zhdm9y" + "R2lmdENvbmZpZ3VyZToCOAEaSgoOVm9pY2VEaWN0RW50cnkSCwoDa2V5GAEg" + "ASgPEicKBXZhbHVlGAIgASgLMhguQ2hhcmFjdGVyVm9pY2VDb25maWd1cmU6" + "AjgBYgZwcm90bzM="), new FileDescriptor[1] { EnumReflection.Descriptor }, new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[7]
		{
			new GeneratedClrTypeInfo(typeof(CharacterInfoConfigure), CharacterInfoConfigure.Parser, new string[32]
			{
				"Id", "OrderWeight", "IsLinkage", "NameID", "NickID", "CommentId", "BiographyID", "IsGalleryShow", "MechanismID", "LinesID",
				"NameBGColor", "HeroType", "TagType", "IsDefault", "Blood", "Attack", "Defense", "ActiveSkill", "PveActiveSkill", "PassiveSkills",
				"PvePassiveSkills", "PveBreak", "CharacterMap", "OffsetInMap", "LandTex", "ExpressionPackID", "StandingPainting", "HasKizuna", "HeroFavorGift", "FavorLevelReward",
				"FavorBreakthroughReward", "IntenseFixSkill"
			}, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(CharacterExpressionPackConfigure), CharacterExpressionPackConfigure.Parser, new string[2] { "Id", "CharacterExpressionPackConfigureItems" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(CharacterExpressionPackConfigureItem), CharacterExpressionPackConfigureItem.Parser, new string[3] { "ItemID", "VideoKey", "IsDefault" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(CharacterHeroFavorGiftConfigure), CharacterHeroFavorGiftConfigure.Parser, new string[2] { "Id", "CharacterHeroFavorGiftConfigureItems" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(CharacterHeroFavorGiftConfigureItem), CharacterHeroFavorGiftConfigureItem.Parser, new string[2] { "Index", "ItemID" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(CharacterVoiceConfigure), CharacterVoiceConfigure.Parser, new string[17]
			{
				"Id", "SoundBank", "AchieveVoice", "AtkVoice", "CardVoice", "DefVoice", "EventVoice", "KillVoice", "LvUpVoice", "MoveVoice",
				"SelectVoice", "ShopVoice", "SkillVoice", "WinVoice", "AwakeVoice", "FanfareVoice", "ShowVoice"
			}, null, null, null, new GeneratedClrTypeInfo[1]),
			new GeneratedClrTypeInfo(typeof(CharacterConfigure), CharacterConfigure.Parser, new string[8] { "Infos", "InfoDict", "ExpressionPacks", "ExpressionPackDict", "HeroFavorGifts", "HeroFavorGiftDict", "Voices", "VoiceDict" }, null, null, null, new GeneratedClrTypeInfo[4])
		}));
	}
}
