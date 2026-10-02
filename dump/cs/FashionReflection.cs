using System;
using Google.Protobuf.Reflection;

public static class FashionReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static FashionReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("Cg1GYXNoaW9uLnByb3RvImEKH0Zhc2hpb25BY2NvdW50SGVhZFNob3RDb25m" + "aWd1cmUSCgoCaWQYASABKA8SFgoOUHJvZmlsZVBpY3R1cmUYAiABKAkSGgoS" + "UHJvZmlsZVBpY3R1cmVfc2Z3GAMgASgJIq0BCiFGYXNoaW9uQWNjb3VudEJh" + "Y2tncm91bmRDb25maWd1cmUSCgoCaWQYASABKA8SGQoRQWNjb3VudEJhY2tn" + "cm91bmQYAiABKAkSHQoVQWNjb3VudEJhY2tncm91bmRfc2Z3GAMgASgJEh4K" + "FkFjY291bnRCYWNrZ3JvdW5kVmlkZW8YBCABKAkSIgoaQWNjb3VudEJhY2tn" + "cm91bmRWaWRlb19zZncYBSABKAkihAEKFkZhc2hpb25FZmZlY3RDb25maWd1" + "cmUSCgoCaWQYASABKA8SDwoHS2lsbFZmeBgCIAEoDxIOCgZpc1Jvb3QYAyAB" + "KAgSDgoGRmx5VmZ4GAQgASgPEhcKD0tpbGxDYW1lcmFTaGFrZRgFIAEoCRIU" + "CgxwcmV2aWV3VmlkZW8YBiABKAkikgEKGEZhc2hpb25DYXJkQmFja0NvbmZp" + "Z3VyZRIKCgJpZBgBIAEoDxIRCglDYXJkRnJvbnQYAiABKAkSFgoOQ2FyZEZy" + "b250Q29sb3IYAyABKAkSEAoIQ2FyZEJhY2sYBCABKAkSGAoQQ2FyZEJhY2tN" + "YXRlcmlhbBgFIAEoCRITCgtDYXJkUGVydmlldxgGIAEoCSJ/ChRGYXNoaW9u" + "RGljZUNvbmZpZ3VyZRIKCgJpZBgBIAEoDxIRCglEaWNlTW9kZWwYAiABKAkS" + "EAoIRGljZUFuaW0YAyABKAkSDwoHRGljZVZmeBgEIAEoCRIPCgdEaWNlU0ZY" + "GAUgASgPEhQKDHByZXZpZXdWaWRlbxgGIAEoCSJaChJGYXNoaW9uS1ZDb25m" + "aWd1cmUSCgoCaWQYASABKA8SEQoJbG9hZGVkS2V5GAIgASgJEhQKDGxvYWRl" + "ZEtleVNGVxgDIAEoCRIPCgdjdXJyZW50GAQgASgIIoAJChBGYXNoaW9uQ29u" + "ZmlndXJlEjoKEEFjY291bnRIZWFkU2hvdHMYASADKAsyIC5GYXNoaW9uQWNj" + "b3VudEhlYWRTaG90Q29uZmlndXJlEkcKE0FjY291bnRIZWFkU2hvdERpY3QY" + "AiADKAsyKi5GYXNoaW9uQ29uZmlndXJlLkFjY291bnRIZWFkU2hvdERpY3RF" + "bnRyeRI+ChJBY2NvdW50QmFja2dyb3VuZHMYAyADKAsyIi5GYXNoaW9uQWNj" + "b3VudEJhY2tncm91bmRDb25maWd1cmUSSwoVQWNjb3VudEJhY2tncm91bmRE" + "aWN0GAQgAygLMiwuRmFzaGlvbkNvbmZpZ3VyZS5BY2NvdW50QmFja2dyb3Vu" + "ZERpY3RFbnRyeRIoCgdFZmZlY3RzGAUgAygLMhcuRmFzaGlvbkVmZmVjdENv" + "bmZpZ3VyZRI1CgpFZmZlY3REaWN0GAYgAygLMiEuRmFzaGlvbkNvbmZpZ3Vy" + "ZS5FZmZlY3REaWN0RW50cnkSLAoJQ2FyZEJhY2tzGAcgAygLMhkuRmFzaGlv" + "bkNhcmRCYWNrQ29uZmlndXJlEjkKDENhcmRCYWNrRGljdBgIIAMoCzIjLkZh" + "c2hpb25Db25maWd1cmUuQ2FyZEJhY2tEaWN0RW50cnkSJAoFRGljZXMYCSAD" + "KAsyFS5GYXNoaW9uRGljZUNvbmZpZ3VyZRIxCghEaWNlRGljdBgKIAMoCzIf" + "LkZhc2hpb25Db25maWd1cmUuRGljZURpY3RFbnRyeRIgCgNLVnMYCyADKAsy" + "Ey5GYXNoaW9uS1ZDb25maWd1cmUSLQoGS1ZEaWN0GAwgAygLMh0uRmFzaGlv" + "bkNvbmZpZ3VyZS5LVkRpY3RFbnRyeRpcChhBY2NvdW50SGVhZFNob3REaWN0" + "RW50cnkSCwoDa2V5GAEgASgPEi8KBXZhbHVlGAIgASgLMiAuRmFzaGlvbkFj" + "Y291bnRIZWFkU2hvdENvbmZpZ3VyZToCOAEaYAoaQWNjb3VudEJhY2tncm91" + "bmREaWN0RW50cnkSCwoDa2V5GAEgASgPEjEKBXZhbHVlGAIgASgLMiIuRmFz" + "aGlvbkFjY291bnRCYWNrZ3JvdW5kQ29uZmlndXJlOgI4ARpKCg9FZmZlY3RE" + "aWN0RW50cnkSCwoDa2V5GAEgASgPEiYKBXZhbHVlGAIgASgLMhcuRmFzaGlv" + "bkVmZmVjdENvbmZpZ3VyZToCOAEaTgoRQ2FyZEJhY2tEaWN0RW50cnkSCwoD" + "a2V5GAEgASgPEigKBXZhbHVlGAIgASgLMhkuRmFzaGlvbkNhcmRCYWNrQ29u" + "ZmlndXJlOgI4ARpGCg1EaWNlRGljdEVudHJ5EgsKA2tleRgBIAEoDxIkCgV2" + "YWx1ZRgCIAEoCzIVLkZhc2hpb25EaWNlQ29uZmlndXJlOgI4ARpCCgtLVkRp" + "Y3RFbnRyeRILCgNrZXkYASABKA8SIgoFdmFsdWUYAiABKAsyEy5GYXNoaW9u" + "S1ZDb25maWd1cmU6AjgBYgZwcm90bzM="), new FileDescriptor[0], new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[7]
		{
			new GeneratedClrTypeInfo(typeof(FashionAccountHeadShotConfigure), FashionAccountHeadShotConfigure.Parser, new string[3] { "Id", "ProfilePicture", "ProfilePictureSfw" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(FashionAccountBackgroundConfigure), FashionAccountBackgroundConfigure.Parser, new string[5] { "Id", "AccountBackground", "AccountBackgroundSfw", "AccountBackgroundVideo", "AccountBackgroundVideoSfw" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(FashionEffectConfigure), FashionEffectConfigure.Parser, new string[6] { "Id", "KillVfx", "IsRoot", "FlyVfx", "KillCameraShake", "PreviewVideo" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(FashionCardBackConfigure), FashionCardBackConfigure.Parser, new string[6] { "Id", "CardFront", "CardFrontColor", "CardBack", "CardBackMaterial", "CardPerview" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(FashionDiceConfigure), FashionDiceConfigure.Parser, new string[6] { "Id", "DiceModel", "DiceAnim", "DiceVfx", "DiceSFX", "PreviewVideo" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(FashionKVConfigure), FashionKVConfigure.Parser, new string[4] { "Id", "LoadedKey", "LoadedKeySFW", "Current" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(FashionConfigure), FashionConfigure.Parser, new string[12]
			{
				"AccountHeadShots", "AccountHeadShotDict", "AccountBackgrounds", "AccountBackgroundDict", "Effects", "EffectDict", "CardBacks", "CardBackDict", "Dices", "DiceDict",
				"KVs", "KVDict"
			}, null, null, null, new GeneratedClrTypeInfo[6])
		}));
	}
}
