using System;
using Google.Protobuf.Reflection;

public static class RechargeRebateReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static RechargeRebateReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("ChRSZWNoYXJnZVJlYmF0ZS5wcm90byJ7Ch1SZWNoYXJnZVJlYmF0ZVJlYmF0" + "ZUNvbmZpZ3VyZRIKCgJpZBgBIAEoDxJOCiJyZWNoYXJnZVJlYmF0ZVJlYmF0" + "ZUNvbmZpZ3VyZUl0ZW1zGAIgAygLMiIuUmVjaGFyZ2VSZWJhdGVSZWJhdGVD" + "b25maWd1cmVJdGVtIl0KIVJlY2hhcmdlUmViYXRlUmViYXRlQ29uZmlndXJl" + "SXRlbRITCgtBbW91bnRTdGFydBgBIAEoDxIRCglBbW91bnRFbmQYAiABKA8S" + "EAoITXVsdGlwbGUYAyABKAIi2wEKF1JlY2hhcmdlUmViYXRlQ29uZmlndXJl" + "Ei8KB1JlYmF0ZXMYASADKAsyHi5SZWNoYXJnZVJlYmF0ZVJlYmF0ZUNvbmZp" + "Z3VyZRI8CgpSZWJhdGVEaWN0GAIgAygLMiguUmVjaGFyZ2VSZWJhdGVDb25m" + "aWd1cmUuUmViYXRlRGljdEVudHJ5GlEKD1JlYmF0ZURpY3RFbnRyeRILCgNr" + "ZXkYASABKA8SLQoFdmFsdWUYAiABKAsyHi5SZWNoYXJnZVJlYmF0ZVJlYmF0" + "ZUNvbmZpZ3VyZToCOAFiBnByb3RvMw=="), new FileDescriptor[0], new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[3]
		{
			new GeneratedClrTypeInfo(typeof(RechargeRebateRebateConfigure), RechargeRebateRebateConfigure.Parser, new string[2] { "Id", "RechargeRebateRebateConfigureItems" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(RechargeRebateRebateConfigureItem), RechargeRebateRebateConfigureItem.Parser, new string[3] { "AmountStart", "AmountEnd", "Multiple" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(RechargeRebateConfigure), RechargeRebateConfigure.Parser, new string[2] { "Rebates", "RebateDict" }, null, null, null, new GeneratedClrTypeInfo[1])
		}));
	}
}
