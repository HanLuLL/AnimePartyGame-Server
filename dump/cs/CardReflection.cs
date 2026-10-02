using System;
using Google.Protobuf.Reflection;

public static class CardReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static CardReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("CgpDYXJkLnByb3RvGgpFbnVtLnByb3RvIu4EChFDYXJkSW5mb0NvbmZpZ3Vy" + "ZRIKCgJpZBgBIAEoDxIOCgZuYW1lSUQYAiABKA8SEAoIY2FyZE51bWIYAyAB" + "KA8SDgoGZGVzY0lkGAQgASgPEhEKCWNvbW1lbnRJZBgFIAEoDxINCgVpbWFn" + "ZRgGIAEoCRIRCglpbWFnZV9zZncYByABKAkSHwoKZWZmZWN0VHlwZRgIIAEo" + "DjILLkVmZmVjdFR5cGUSGwoIY2FyZFR5cGUYCSABKA4yCS5DYXJkVHlwZRIn" + "Cg5jYXJkVGFyZ2V0VHlwZRgKIAEoDjIPLkNhcmRUYXJnZXRUeXBlEhUKDWlz" + "Q29udGFpblNlbGYYCyABKAgSHwoXaXNDb250YWluSG9zcGl0YWxQbGF5ZXIY" + "DCABKAgSFwoPaXNDb250YWluUGxheWVyGA0gASgIEhEKCWlzSG9zdGlsZRgO" + "IAEoCBISCgppc0ZyaWVuZGx5GA8gASgIEhgKEGlzQ29udGFpbk1vbnN0ZXIY" + "ECABKAgSGwoTaXNDb250YWluRGVhZFRhcmdldBgRIAEoCBISCgppc1BsYXlh" + "YmxlGBIgASgIEgwKBGNvc3QYEyABKA8SDgoGcGFyYW1zGBQgAygREg8KB2J1" + "ZmZJZHMYFSADKA8SDwoHY2FyZElkcxgWIAMoDxITCgtwZXJmb3JtQ2FzdBgX" + "IAMoDxIVCg1wZXJmb3JtVGFyZ2V0GBggAygPEhUKDWlzR2FsbGVyeVNob3cY" + "GSABKAgSEwoLaXNEZWRpY2F0ZWQYGiABKAgSEQoJUmVjb21CYXNlGBsgASgP" + "EhEKCVJlY29tUGx1cxgcIAMoDyJpChdDYXJkRWZmZWN0UG9vbENvbmZpZ3Vy" + "ZRIKCgJpZBgBIAEoDxJCChxjYXJkRWZmZWN0UG9vbENvbmZpZ3VyZUl0ZW1z" + "GAIgAygLMhwuQ2FyZEVmZmVjdFBvb2xDb25maWd1cmVJdGVtIi0KG0NhcmRF" + "ZmZlY3RQb29sQ29uZmlndXJlSXRlbRIOCgZjYXJkSWQYASABKA8iaQoXQ2Fy" + "ZEJhdHRsZVBvb2xDb25maWd1cmUSCgoCaWQYASABKA8SQgocY2FyZEJhdHRs" + "ZVBvb2xDb25maWd1cmVJdGVtcxgCIAMoCzIcLkNhcmRCYXR0bGVQb29sQ29u" + "ZmlndXJlSXRlbSItChtDYXJkQmF0dGxlUG9vbENvbmZpZ3VyZUl0ZW0SDgoG" + "Y2FyZElkGAEgASgPImEKE0NhcmRBbHRBcnRDb25maWd1cmUSDgoGY2FyZElk" + "GAEgASgPEjoKGGNhcmRBbHRBcnRDb25maWd1cmVJdGVtcxgCIAMoCzIYLkNh" + "cmRBbHRBcnRDb25maWd1cmVJdGVtIuUBChdDYXJkQWx0QXJ0Q29uZmlndXJl" + "SXRlbRIMCgR0eXBlGAEgASgPEhAKCGFsdEFydElkGAIgASgPEhkKEUFjY291" + "bnRCYWNrZ3JvdW5kGAMgASgJEh0KFUFjY291bnRCYWNrZ3JvdW5kX3NmdxgE" + "IAEoCRIeChZBY2NvdW50QmFja2dyb3VuZFZpZGVvGAUgASgJEiIKGkFjY291" + "bnRCYWNrZ3JvdW5kVmlkZW9fc2Z3GAYgASgJEhEKCUNhcmRGcm9udBgHIAEo" + "CRIZChFDYXJkRnJvbnRNYXRlcmlhbBgIIAEoCSLDBQoNQ2FyZENvbmZpZ3Vy" + "ZRIhCgVJbmZvcxgBIAMoCzISLkNhcmRJbmZvQ29uZmlndXJlEi4KCEluZm9E" + "aWN0GAIgAygLMhwuQ2FyZENvbmZpZ3VyZS5JbmZvRGljdEVudHJ5Ei0KC0Vm" + "ZmVjdFBvb2xzGAMgAygLMhguQ2FyZEVmZmVjdFBvb2xDb25maWd1cmUSOgoO" + "RWZmZWN0UG9vbERpY3QYBCADKAsyIi5DYXJkQ29uZmlndXJlLkVmZmVjdFBv" + "b2xEaWN0RW50cnkSLQoLQmF0dGxlUG9vbHMYBSADKAsyGC5DYXJkQmF0dGxl" + "UG9vbENvbmZpZ3VyZRI6Cg5CYXR0bGVQb29sRGljdBgGIAMoCzIiLkNhcmRD" + "b25maWd1cmUuQmF0dGxlUG9vbERpY3RFbnRyeRIlCgdBbHRBcnRzGAcgAygL" + "MhQuQ2FyZEFsdEFydENvbmZpZ3VyZRIyCgpBbHRBcnREaWN0GAggAygLMh4u" + "Q2FyZENvbmZpZ3VyZS5BbHRBcnREaWN0RW50cnkaQwoNSW5mb0RpY3RFbnRy" + "eRILCgNrZXkYASABKA8SIQoFdmFsdWUYAiABKAsyEi5DYXJkSW5mb0NvbmZp" + "Z3VyZToCOAEaTwoTRWZmZWN0UG9vbERpY3RFbnRyeRILCgNrZXkYASABKA8S" + "JwoFdmFsdWUYAiABKAsyGC5DYXJkRWZmZWN0UG9vbENvbmZpZ3VyZToCOAEa" + "TwoTQmF0dGxlUG9vbERpY3RFbnRyeRILCgNrZXkYASABKA8SJwoFdmFsdWUY" + "AiABKAsyGC5DYXJkQmF0dGxlUG9vbENvbmZpZ3VyZToCOAEaRwoPQWx0QXJ0" + "RGljdEVudHJ5EgsKA2tleRgBIAEoDxIjCgV2YWx1ZRgCIAEoCzIULkNhcmRB" + "bHRBcnRDb25maWd1cmU6AjgBYgZwcm90bzM="), new FileDescriptor[1] { EnumReflection.Descriptor }, new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[8]
		{
			new GeneratedClrTypeInfo(typeof(CardInfoConfigure), CardInfoConfigure.Parser, new string[28]
			{
				"Id", "NameID", "CardNumb", "DescId", "CommentId", "Image", "ImageSfw", "EffectType", "CardType", "CardTargetType",
				"IsContainSelf", "IsContainHospitalPlayer", "IsContainPlayer", "IsHostile", "IsFriendly", "IsContainMonster", "IsContainDeadTarget", "IsPlayable", "Cost", "Params",
				"BuffIds", "CardIds", "PerformCast", "PerformTarget", "IsGalleryShow", "IsDedicated", "RecomBase", "RecomPlus"
			}, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(CardEffectPoolConfigure), CardEffectPoolConfigure.Parser, new string[2] { "Id", "CardEffectPoolConfigureItems" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(CardEffectPoolConfigureItem), CardEffectPoolConfigureItem.Parser, new string[1] { "CardId" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(CardBattlePoolConfigure), CardBattlePoolConfigure.Parser, new string[2] { "Id", "CardBattlePoolConfigureItems" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(CardBattlePoolConfigureItem), CardBattlePoolConfigureItem.Parser, new string[1] { "CardId" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(CardAltArtConfigure), CardAltArtConfigure.Parser, new string[2] { "CardId", "CardAltArtConfigureItems" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(CardAltArtConfigureItem), CardAltArtConfigureItem.Parser, new string[8] { "Type", "AltArtId", "AccountBackground", "AccountBackgroundSfw", "AccountBackgroundVideo", "AccountBackgroundVideoSfw", "CardFront", "CardFrontMaterial" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(CardConfigure), CardConfigure.Parser, new string[8] { "Infos", "InfoDict", "EffectPools", "EffectPoolDict", "BattlePools", "BattlePoolDict", "AltArts", "AltArtDict" }, null, null, null, new GeneratedClrTypeInfo[4])
		}));
	}
}
