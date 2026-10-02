using System;
using Google.Protobuf.Reflection;
using Google.Protobuf.WellKnownTypes;

public static class ItemReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static ItemReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("CgpJdGVtLnByb3RvGh9nb29nbGUvcHJvdG9idWYvdGltZXN0YW1wLnByb3Rv" + "GgpFbnVtLnByb3RvItQEChFJdGVtSW5mb0NvbmZpZ3VyZRIKCgJpZBgBIAEo" + "DxIbCghpdGVtVHlwZRgCIAEoDjIJLkl0ZW1UeXBlEhIKCnN1Yk1ldGVySUQY" + "AyABKA8SDAoEaWNvbhgEIAEoCRIQCghpY29uX3NmdxgFIAEoCRIOCgZpY29u" + "RU4YBiABKAkSDgoGaWNvbkpQGAcgASgJEg4KBmljb25UQxgIIAEoCRIOCgZu" + "YW1lSUQYCSABKA8SFQoNZGVzY3JpcHRpb25JRBgKIAEoDxIhCgtxdWFsaXR5" + "VHlwZRgLIAEoDjIMLlF1YWxpdHlUeXBlEhcKD2lzQXV0b1RyYW5zZm9ybRgM" + "IAEoCBI0Cgl0cmFuc2Zvcm0YDSADKAsyIS5JdGVtSW5mb0NvbmZpZ3VyZS5U" + "cmFuc2Zvcm1FbnRyeRIvCgtlbmREYXRlVGltZRgOIAEoCzIaLmdvb2dsZS5w" + "cm90b2J1Zi5UaW1lc3RhbXASQgoQb3V0dGltZVRyYW5zZm9ybRgPIAMoCzIo" + "Lkl0ZW1JbmZvQ29uZmlndXJlLk91dHRpbWVUcmFuc2Zvcm1FbnRyeRIUCgxp" + "c0NsaWVudFNob3cYECABKAgSDwoHd2F5TGlzdBgRIAMoDxISCgppc0F1dG9P" + "cGVuGBIgASgIGjAKDlRyYW5zZm9ybUVudHJ5EgsKA2tleRgBIAEoDxINCgV2" + "YWx1ZRgCIAEoDzoCOAEaNwoVT3V0dGltZVRyYW5zZm9ybUVudHJ5EgsKA2tl" + "eRgBIAEoDxINCgV2YWx1ZRgCIAEoDzoCOAEiewoQSXRlbVRhZ0NvbmZpZ3Vy" + "ZRIbCghpdGVtVHlwZRgBIAEoDjIJLkl0ZW1UeXBlEhAKCG1heENvdW50GAIg" + "ASgPEhEKCWVuYWJsZVVzZRgDIAEoCBIVCg1zZWxlY3RlZEluZGV4GAQgASgP" + "Eg4KBm5hbWVJRBgFIAEoDyJuCg9JdGVtVUlDb25maWd1cmUSJwoLbWVudVN1" + "YlR5cGUYASABKA4yEi5JdGVtVUlNZW51U3ViVHlwZRIUCgxmaWx0ZXJOYW1l" + "SUQYAiABKA8SHAoJaXRlbVR5cGVzGAMgAygOMgkuSXRlbVR5cGUixQMKDUl0" + "ZW1Db25maWd1cmUSIQoFSW5mb3MYASADKAsyEi5JdGVtSW5mb0NvbmZpZ3Vy" + "ZRIuCghJbmZvRGljdBgCIAMoCzIcLkl0ZW1Db25maWd1cmUuSW5mb0RpY3RF" + "bnRyeRIfCgRUYWdzGAMgAygLMhEuSXRlbVRhZ0NvbmZpZ3VyZRIsCgdUYWdE" + "aWN0GAQgAygLMhsuSXRlbUNvbmZpZ3VyZS5UYWdEaWN0RW50cnkSHQoDVUlz" + "GAUgAygLMhAuSXRlbVVJQ29uZmlndXJlEioKBlVJRGljdBgGIAMoCzIaLkl0" + "ZW1Db25maWd1cmUuVUlEaWN0RW50cnkaQwoNSW5mb0RpY3RFbnRyeRILCgNr" + "ZXkYASABKA8SIQoFdmFsdWUYAiABKAsyEi5JdGVtSW5mb0NvbmZpZ3VyZToC" + "OAEaQQoMVGFnRGljdEVudHJ5EgsKA2tleRgBIAEoDxIgCgV2YWx1ZRgCIAEo" + "CzIRLkl0ZW1UYWdDb25maWd1cmU6AjgBGj8KC1VJRGljdEVudHJ5EgsKA2tl" + "eRgBIAEoDxIfCgV2YWx1ZRgCIAEoCzIQLkl0ZW1VSUNvbmZpZ3VyZToCOAFi" + "BnByb3RvMw=="), new FileDescriptor[2]
		{
			TimestampReflection.Descriptor,
			EnumReflection.Descriptor
		}, new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[4]
		{
			new GeneratedClrTypeInfo(typeof(ItemInfoConfigure), ItemInfoConfigure.Parser, new string[18]
			{
				"Id", "ItemType", "SubMeterID", "Icon", "IconSfw", "IconEN", "IconJP", "IconTC", "NameID", "DescriptionID",
				"QualityType", "IsAutoTransform", "Transform", "EndDateTime", "OuttimeTransform", "IsClientShow", "WayList", "IsAutoOpen"
			}, null, null, null, new GeneratedClrTypeInfo[2]),
			new GeneratedClrTypeInfo(typeof(ItemTagConfigure), ItemTagConfigure.Parser, new string[5] { "ItemType", "MaxCount", "EnableUse", "SelectedIndex", "NameID" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(ItemUIConfigure), ItemUIConfigure.Parser, new string[3] { "MenuSubType", "FilterNameID", "ItemTypes" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(ItemConfigure), ItemConfigure.Parser, new string[6] { "Infos", "InfoDict", "Tags", "TagDict", "UIs", "UIDict" }, null, null, null, new GeneratedClrTypeInfo[3])
		}));
	}
}
