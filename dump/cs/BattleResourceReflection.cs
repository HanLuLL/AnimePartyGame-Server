using System;
using Google.Protobuf.Reflection;

public static class BattleResourceReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static BattleResourceReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("ChRCYXR0bGVSZXNvdXJjZS5wcm90byLYBAobQmF0dGxlUmVzb3VyY2VJbmZv" + "Q29uZmlndXJlEgoKAmlkGAEgASgPEhEKCWluZm9MYXllchgCIAEoDxIQCghh" + "dGtMYXllchgDIAEoDxIQCghkZWZMYXllchgEIAEoDxIRCgloaXREaXJlY3QY" + "BSABKAkSFgoOYXBwcm9hY2hEaXJlY3QYBiABKAkSFAoMZGV0YWNoRGlyZWN0" + "GAcgASgJEhkKEWNoYWluQXR0YWNrRGlyZWN0GAggASgJEhgKEGFjdG9yUG9z" + "aXRpb25BdGsYCSABKA8SGAoQYWN0b3JQb3NpdGlvbkRlZhgKIAEoDxIaChJh" + "Y3RvclBvc2l0aW9uQ2hhaW4YCyABKA8SGwoTYmF0dGxlRmFuZmFyZUF0dGFj" + "axgMIAEoCRIYChBiYXR0bGVJZGxlQXR0YWNrGA0gASgJEhsKE2JhdHRsZUlk" + "bGVhdHRhY2tFbmQYDiABKAkSGQoRYmF0dGxlQ2hhaW5BdHRhY2sYDyABKAkS" + "GQoRYmF0dGxlSWRsZURlZmVuc2UYECABKAkSEQoJbW92ZXN0YXJ0GBEgASgJ" + "EgwKBG1vdmUYEiABKAkSOAoGYXR0YWNrGBMgAygLMiguQmF0dGxlUmVzb3Vy" + "Y2VJbmZvQ29uZmlndXJlLkF0dGFja0VudHJ5EgwKBGh1cnQYFCABKAkSDQoF" + "ZG9kZ2UYFSABKAkSCwoDd2luGBYgASgJEgwKBGRlYWQYFyABKAkaLQoLQXR0" + "YWNrRW50cnkSCwoDa2V5GAEgASgPEg0KBXZhbHVlGAIgASgJOgI4ASJ1CiJC" + "YXR0bGVSZXNvdXJjZVBvc3RQcm9jZXNzQ29uZmlndXJlEgoKAmlkGAEgASgP" + "EhEKCVRocmVzaG9sZBgCIAEoDxIRCglJbnRlbnNpdHkYAyABKA8SDwoHU2Nh" + "dHRlchgEIAEoDxIMCgRUaW50GAUgASgJIq8DChdCYXR0bGVSZXNvdXJjZUNv" + "bmZpZ3VyZRIrCgVJbmZvcxgBIAMoCzIcLkJhdHRsZVJlc291cmNlSW5mb0Nv" + "bmZpZ3VyZRI4CghJbmZvRGljdBgCIAMoCzImLkJhdHRsZVJlc291cmNlQ29u" + "ZmlndXJlLkluZm9EaWN0RW50cnkSOQoMUG9zdFByb2Nlc3NzGAMgAygLMiMu" + "QmF0dGxlUmVzb3VyY2VQb3N0UHJvY2Vzc0NvbmZpZ3VyZRJGCg9Qb3N0UHJv" + "Y2Vzc0RpY3QYBCADKAsyLS5CYXR0bGVSZXNvdXJjZUNvbmZpZ3VyZS5Qb3N0" + "UHJvY2Vzc0RpY3RFbnRyeRpNCg1JbmZvRGljdEVudHJ5EgsKA2tleRgBIAEo" + "DxIrCgV2YWx1ZRgCIAEoCzIcLkJhdHRsZVJlc291cmNlSW5mb0NvbmZpZ3Vy" + "ZToCOAEaWwoUUG9zdFByb2Nlc3NEaWN0RW50cnkSCwoDa2V5GAEgASgPEjIK" + "BXZhbHVlGAIgASgLMiMuQmF0dGxlUmVzb3VyY2VQb3N0UHJvY2Vzc0NvbmZp" + "Z3VyZToCOAFiBnByb3RvMw=="), new FileDescriptor[0], new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[3]
		{
			new GeneratedClrTypeInfo(typeof(BattleResourceInfoConfigure), BattleResourceInfoConfigure.Parser, new string[23]
			{
				"Id", "InfoLayer", "AtkLayer", "DefLayer", "HitDirect", "ApproachDirect", "DetachDirect", "ChainAttackDirect", "ActorPositionAtk", "ActorPositionDef",
				"ActorPositionChain", "BattleFanfareAttack", "BattleIdleAttack", "BattleIdleattackEnd", "BattleChainAttack", "BattleIdleDefense", "Movestart", "Move", "Attack", "Hurt",
				"Dodge", "Win", "Dead"
			}, null, null, null, new GeneratedClrTypeInfo[1]),
			new GeneratedClrTypeInfo(typeof(BattleResourcePostProcessConfigure), BattleResourcePostProcessConfigure.Parser, new string[5] { "Id", "Threshold", "Intensity", "Scatter", "Tint" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(BattleResourceConfigure), BattleResourceConfigure.Parser, new string[4] { "Infos", "InfoDict", "PostProcesss", "PostProcessDict" }, null, null, null, new GeneratedClrTypeInfo[2])
		}));
	}
}
