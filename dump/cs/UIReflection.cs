using System;
using Google.Protobuf.Reflection;

public static class UIReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static UIReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("CghVSS5wcm90bxoKRW51bS5wcm90byLYAwoQVUlQYW5lbENvbmZpZ3VyZRIf" + "CglwYW5lbFR5cGUYASABKA4yDC5VSVBhbmVsVHlwZRIKCgJpZBgCIAEoDxIT" + "CgtQYWNrYWdlTmFtZRgDIAEoCRIVCg1pc1Jvb3RJblNjZW5lGAQgASgIEhgK" + "EG5lZWRCYWNrZ3JvdW5kVUkYBSABKAgSFgoObmVlZEJvdHRvbU1lbnUYBiAB" + "KAgSGAoQY2xvc2VBY3Rpdml0eUh1YhgHIAEoCBIdChVuZWVkQm90dG9tUGxh" + "eWVyTGFiZWwYCCABKAgSFAoMc2hvd0Z1bmN0aW9uGAkgAygPEg0KBWxheWVy" + "GAogASgREhkKEW5lZWRDbG9zZVByZXZpb3VzGAsgASgIEhMKC0JHTUNvbmZp" + "Z0lEGAwgASgPEj0KDnNob3dDdXJyZW5jaWVzGA0gAygLMiUuVUlQYW5lbENv" + "bmZpZ3VyZS5TaG93Q3VycmVuY2llc0VudHJ5EhMKC01vbmV5TGluZVVwGA4g" + "ASgPGlcKE1Nob3dDdXJyZW5jaWVzRW50cnkSCwoDa2V5GAEgASgPEi8KBXZh" + "bHVlGAIgASgLMiAuVUlQYW5lbENvbmZpZ3VyZVNob3dDdXJyZW5jaWVzczoC" + "OAEiMQofVUlQYW5lbENvbmZpZ3VyZVNob3dDdXJyZW5jaWVzcxIOCgZ2YWx1" + "ZXMYASADKA8iWgoRVUlXaW5kb3dDb25maWd1cmUSIQoKd2luZG93VHlwZRgB" + "IAEoDjINLlVJV2luZG93VHlwZRITCgtQYWNrYWdlTmFtZRgCIAEoCRINCgVs" + "YXllchgDIAEoESLDAgoLVUlDb25maWd1cmUSIQoGUGFuZWxzGAEgAygLMhEu" + "VUlQYW5lbENvbmZpZ3VyZRIuCglQYW5lbERpY3QYAiADKAsyGy5VSUNvbmZp" + "Z3VyZS5QYW5lbERpY3RFbnRyeRIjCgdXaW5kb3dzGAMgAygLMhIuVUlXaW5k" + "b3dDb25maWd1cmUSMAoKV2luZG93RGljdBgEIAMoCzIcLlVJQ29uZmlndXJl" + "LldpbmRvd0RpY3RFbnRyeRpDCg5QYW5lbERpY3RFbnRyeRILCgNrZXkYASAB" + "KA8SIAoFdmFsdWUYAiABKAsyES5VSVBhbmVsQ29uZmlndXJlOgI4ARpFCg9X" + "aW5kb3dEaWN0RW50cnkSCwoDa2V5GAEgASgPEiEKBXZhbHVlGAIgASgLMhIu" + "VUlXaW5kb3dDb25maWd1cmU6AjgBYgZwcm90bzM="), new FileDescriptor[1] { EnumReflection.Descriptor }, new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[4]
		{
			new GeneratedClrTypeInfo(typeof(UIPanelConfigure), UIPanelConfigure.Parser, new string[14]
			{
				"PanelType", "Id", "PackageName", "IsRootInScene", "NeedBackgroundUI", "NeedBottomMenu", "CloseActivityHub", "NeedBottomPlayerLabel", "ShowFunction", "Layer",
				"NeedClosePrevious", "BGMConfigID", "ShowCurrencies", "MoneyLineUp"
			}, null, null, null, new GeneratedClrTypeInfo[1]),
			new GeneratedClrTypeInfo(typeof(UIPanelConfigureShowCurrenciess), UIPanelConfigureShowCurrenciess.Parser, new string[1] { "Values" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(UIWindowConfigure), UIWindowConfigure.Parser, new string[3] { "WindowType", "PackageName", "Layer" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(UIConfigure), UIConfigure.Parser, new string[4] { "Panels", "PanelDict", "Windows", "WindowDict" }, null, null, null, new GeneratedClrTypeInfo[2])
		}));
	}
}
