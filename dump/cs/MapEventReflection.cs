using System;
using Google.Protobuf.Reflection;

public static class MapEventReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static MapEventReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("Cg5NYXBFdmVudC5wcm90bxoKRW51bS5wcm90byKaAwoVTWFwRXZlbnRJbmZv" + "Q29uZmlndXJlEgoKAmlkGAEgASgPEg4KBmNhcmRJRBgCIAEoDxIOCgZpc0hp" + "ZGUYAyABKAgSFQoNdHJpZ2dlcnBhcmFtcxgEIAMoERIYChB0cmlnZ2VyTGFu" + "ZFVuaXRzGAUgAygRElAKFW1hcEV2ZW50RWZmZWN0Q29uZmlncxgGIAMoCzIx" + "Lk1hcEV2ZW50SW5mb0NvbmZpZ3VyZS5NYXBFdmVudEVmZmVjdENvbmZpZ3NF" + "bnRyeRIPCgdwYXJhbXMxGAcgAygREg8KB3BhcmFtczIYCCADKBESDwoHcGFy" + "YW1zMxgJIAEoCRIPCgdidWZmSWRzGAogAygPEhAKCHBlcmZvcm0xGAsgASgP" + "EhAKCHBlcmZvcm0yGAwgASgPGmoKGk1hcEV2ZW50RWZmZWN0Q29uZmlnc0Vu" + "dHJ5EgsKA2tleRgBIAEoDxI7CgV2YWx1ZRgCIAEoCzIsLk1hcEV2ZW50SW5m" + "b0NvbmZpZ3VyZU1hcEV2ZW50RWZmZWN0Q29uZmlnc3M6AjgBIj0KK01hcEV2" + "ZW50SW5mb0NvbmZpZ3VyZU1hcEV2ZW50RWZmZWN0Q29uZmlnc3MSDgoGdmFs" + "dWVzGAEgAygPIq8BCh1NYXBFdmVudE1hcEV2ZW50Q2FyZENvbmZpZ3VyZRIK" + "CgJpZBgBIAEoDxIOCgZuYW1lSUQYAiABKA8SGwoIY2FyZFR5cGUYAyABKA4y" + "CS5DYXJkVHlwZRIQCghjYXJkTnVtYhgEIAEoDxIOCgZkZXNjSWQYBSABKA8S" + "EQoJY29tbWVudElkGAYgASgPEg0KBWltYWdlGAcgASgJEhEKCWltYWdlX3Nm" + "dxgIIAEoCSKLAwoRTWFwRXZlbnRDb25maWd1cmUSJQoFSW5mb3MYASADKAsy" + "Fi5NYXBFdmVudEluZm9Db25maWd1cmUSMgoISW5mb0RpY3QYAiADKAsyIC5N" + "YXBFdmVudENvbmZpZ3VyZS5JbmZvRGljdEVudHJ5EjUKDU1hcEV2ZW50Q2Fy" + "ZHMYAyADKAsyHi5NYXBFdmVudE1hcEV2ZW50Q2FyZENvbmZpZ3VyZRJCChBN" + "YXBFdmVudENhcmREaWN0GAQgAygLMiguTWFwRXZlbnRDb25maWd1cmUuTWFw" + "RXZlbnRDYXJkRGljdEVudHJ5GkcKDUluZm9EaWN0RW50cnkSCwoDa2V5GAEg" + "ASgPEiUKBXZhbHVlGAIgASgLMhYuTWFwRXZlbnRJbmZvQ29uZmlndXJlOgI4" + "ARpXChVNYXBFdmVudENhcmREaWN0RW50cnkSCwoDa2V5GAEgASgPEi0KBXZh" + "bHVlGAIgASgLMh4uTWFwRXZlbnRNYXBFdmVudENhcmRDb25maWd1cmU6AjgB" + "YgZwcm90bzM="), new FileDescriptor[1] { EnumReflection.Descriptor }, new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[4]
		{
			new GeneratedClrTypeInfo(typeof(MapEventInfoConfigure), MapEventInfoConfigure.Parser, new string[12]
			{
				"Id", "CardID", "IsHide", "Triggerparams", "TriggerLandUnits", "MapEventEffectConfigs", "Params1", "Params2", "Params3", "BuffIds",
				"Perform1", "Perform2"
			}, null, null, null, new GeneratedClrTypeInfo[1]),
			new GeneratedClrTypeInfo(typeof(MapEventInfoConfigureMapEventEffectConfigss), MapEventInfoConfigureMapEventEffectConfigss.Parser, new string[1] { "Values" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(MapEventMapEventCardConfigure), MapEventMapEventCardConfigure.Parser, new string[8] { "Id", "NameID", "CardType", "CardNumb", "DescId", "CommentId", "Image", "ImageSfw" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(MapEventConfigure), MapEventConfigure.Parser, new string[4] { "Infos", "InfoDict", "MapEventCards", "MapEventCardDict" }, null, null, null, new GeneratedClrTypeInfo[2])
		}));
	}
}
