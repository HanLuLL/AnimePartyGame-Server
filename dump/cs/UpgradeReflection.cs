using System;
using Google.Protobuf.Reflection;

public static class UpgradeReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static UpgradeReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("Cg1VcGdyYWRlLnByb3RvIoYBChRVcGdyYWRlRGF0YUNvbmZpZ3VyZRIKCgJp" + "ZBgBIAEoDxIRCglnb2xkU1RSaWQYAiABKA8SEQoJaXNEZWZhdWx0GAMgASgI" + "EjwKGXVwZ3JhZGVEYXRhQ29uZmlndXJlSXRlbXMYBCADKAsyGS5VcGdyYWRl" + "RGF0YUNvbmZpZ3VyZUl0ZW0icwoYVXBncmFkZURhdGFDb25maWd1cmVJdGVt" + "EgwKBHN0YXIYASABKA8SDAoEZ29sZBgCIAEoDxIQCghnb2xkY29zdBgDIAEo" + "DxITCgtjb3N0RW5oYW5jZRgEIAEoDxIUCgxyZWxpY1dlaWdodHMYBSADKA8i" + "swEKEFVwZ3JhZGVDb25maWd1cmUSJAoFRGF0YXMYASADKAsyFS5VcGdyYWRl" + "RGF0YUNvbmZpZ3VyZRIxCghEYXRhRGljdBgCIAMoCzIfLlVwZ3JhZGVDb25m" + "aWd1cmUuRGF0YURpY3RFbnRyeRpGCg1EYXRhRGljdEVudHJ5EgsKA2tleRgB" + "IAEoDxIkCgV2YWx1ZRgCIAEoCzIVLlVwZ3JhZGVEYXRhQ29uZmlndXJlOgI4" + "AWIGcHJvdG8z"), new FileDescriptor[0], new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[3]
		{
			new GeneratedClrTypeInfo(typeof(UpgradeDataConfigure), UpgradeDataConfigure.Parser, new string[4] { "Id", "GoldSTRid", "IsDefault", "UpgradeDataConfigureItems" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(UpgradeDataConfigureItem), UpgradeDataConfigureItem.Parser, new string[5] { "Star", "Gold", "Goldcost", "CostEnhance", "RelicWeights" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(UpgradeConfigure), UpgradeConfigure.Parser, new string[2] { "Datas", "DataDict" }, null, null, null, new GeneratedClrTypeInfo[1])
		}));
	}
}
