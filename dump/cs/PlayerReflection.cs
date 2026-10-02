using System;
using Google.Protobuf.Reflection;

public static class PlayerReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static PlayerReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("CgxQbGF5ZXIucHJvdG8iNgoUUGxheWVyTGV2ZWxDb25maWd1cmUSDQoFbGV2" + "ZWwYASABKA8SDwoHbmVlZEV4cBgCIAEoDyKJAQoVUGxheWVyUmV3YXJkQ29u" + "ZmlndXJlEg0KBWxldmVsGAEgASgPEjIKBnJld2FyZBgCIAMoCzIiLlBsYXll" + "clJld2FyZENvbmZpZ3VyZS5SZXdhcmRFbnRyeRotCgtSZXdhcmRFbnRyeRIL" + "CgNrZXkYASABKA8SDQoFdmFsdWUYAiABKA86AjgBIjsKGFBsYXllckNvdmVy" + "TmFtZUNvbmZpZ3VyZRIKCgJpZBgBIAEoDxITCgtjb3Zlck5hbWVJRBgCIAEo" + "DyKbBAoPUGxheWVyQ29uZmlndXJlEiUKBkxldmVscxgBIAMoCzIVLlBsYXll" + "ckxldmVsQ29uZmlndXJlEjIKCUxldmVsRGljdBgCIAMoCzIfLlBsYXllckNv" + "bmZpZ3VyZS5MZXZlbERpY3RFbnRyeRInCgdSZXdhcmRzGAMgAygLMhYuUGxh" + "eWVyUmV3YXJkQ29uZmlndXJlEjQKClJld2FyZERpY3QYBCADKAsyIC5QbGF5" + "ZXJDb25maWd1cmUuUmV3YXJkRGljdEVudHJ5Ei0KCkNvdmVyTmFtZXMYBSAD" + "KAsyGS5QbGF5ZXJDb3Zlck5hbWVDb25maWd1cmUSOgoNQ292ZXJOYW1lRGlj" + "dBgGIAMoCzIjLlBsYXllckNvbmZpZ3VyZS5Db3Zlck5hbWVEaWN0RW50cnka" + "RwoOTGV2ZWxEaWN0RW50cnkSCwoDa2V5GAEgASgPEiQKBXZhbHVlGAIgASgL" + "MhUuUGxheWVyTGV2ZWxDb25maWd1cmU6AjgBGkkKD1Jld2FyZERpY3RFbnRy" + "eRILCgNrZXkYASABKA8SJQoFdmFsdWUYAiABKAsyFi5QbGF5ZXJSZXdhcmRD" + "b25maWd1cmU6AjgBGk8KEkNvdmVyTmFtZURpY3RFbnRyeRILCgNrZXkYASAB" + "KA8SKAoFdmFsdWUYAiABKAsyGS5QbGF5ZXJDb3Zlck5hbWVDb25maWd1cmU6" + "AjgBYgZwcm90bzM="), new FileDescriptor[0], new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[4]
		{
			new GeneratedClrTypeInfo(typeof(PlayerLevelConfigure), PlayerLevelConfigure.Parser, new string[2] { "Level", "NeedExp" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(PlayerRewardConfigure), PlayerRewardConfigure.Parser, new string[2] { "Level", "Reward" }, null, null, null, new GeneratedClrTypeInfo[1]),
			new GeneratedClrTypeInfo(typeof(PlayerCoverNameConfigure), PlayerCoverNameConfigure.Parser, new string[2] { "Id", "CoverNameID" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(PlayerConfigure), PlayerConfigure.Parser, new string[6] { "Levels", "LevelDict", "Rewards", "RewardDict", "CoverNames", "CoverNameDict" }, null, null, null, new GeneratedClrTypeInfo[3])
		}));
	}
}
