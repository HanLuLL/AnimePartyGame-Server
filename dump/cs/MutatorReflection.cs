using System;
using Google.Protobuf.Reflection;

public static class MutatorReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static MutatorReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("Cg1NdXRhdG9yLnByb3RvGgpFbnVtLnByb3RvIsIBChRNdXRhdG9ySW5mb0Nv" + "bmZpZ3VyZRIKCgJpZBgBIAEoDxIOCgZuYW1lSUQYAiABKA8SDgoGZGVzY0lk" + "GAMgASgPEiEKC211dGF0b3JUeXBlGAQgAygOMgwuTXV0YXRvclR5cGUSDAoE" + "aWNvbhgFIAEoCRIOCgZwYXJhbXMYBiADKBESDgoGYnVmZklkGAcgAygPEhsK" + "E3ByZWxvYWRDaGFyYWN0ZXJJZHMYCCADKA8SEAoIcGVyZm9ybXMYCSADKA8i" + "YAoUTXV0YXRvclBvb2xDb25maWd1cmUSCgoCaWQYASABKA8SPAoZbXV0YXRv" + "clBvb2xDb25maWd1cmVJdGVtcxgCIAMoCzIZLk11dGF0b3JQb29sQ29uZmln" + "dXJlSXRlbSItChhNdXRhdG9yUG9vbENvbmZpZ3VyZUl0ZW0SEQoJbXV0YXRv" + "cklkGAEgASgPInUKG011dGF0b3JQb29sQ29tcG9zZUNvbmZpZ3VyZRIKCgJp" + "ZBgBIAEoDxJKCiBtdXRhdG9yUG9vbENvbXBvc2VDb25maWd1cmVJdGVtcxgC" + "IAMoCzIgLk11dGF0b3JQb29sQ29tcG9zZUNvbmZpZ3VyZUl0ZW0iVAofTXV0" + "YXRvclBvb2xDb21wb3NlQ29uZmlndXJlSXRlbRINCgVpbmRleBgBIAEoDxIi" + "Cgxjb21wb3NlVHlwZXMYAiADKA4yDC5NdXRhdG9yVHlwZSKfBAoQTXV0YXRv" + "ckNvbmZpZ3VyZRIkCgVJbmZvcxgBIAMoCzIVLk11dGF0b3JJbmZvQ29uZmln" + "dXJlEjEKCEluZm9EaWN0GAIgAygLMh8uTXV0YXRvckNvbmZpZ3VyZS5JbmZv" + "RGljdEVudHJ5EiQKBVBvb2xzGAMgAygLMhUuTXV0YXRvclBvb2xDb25maWd1" + "cmUSMQoIUG9vbERpY3QYBCADKAsyHy5NdXRhdG9yQ29uZmlndXJlLlBvb2xE" + "aWN0RW50cnkSMgoMUG9vbENvbXBvc2VzGAUgAygLMhwuTXV0YXRvclBvb2xD" + "b21wb3NlQ29uZmlndXJlEj8KD1Bvb2xDb21wb3NlRGljdBgGIAMoCzImLk11" + "dGF0b3JDb25maWd1cmUuUG9vbENvbXBvc2VEaWN0RW50cnkaRgoNSW5mb0Rp" + "Y3RFbnRyeRILCgNrZXkYASABKA8SJAoFdmFsdWUYAiABKAsyFS5NdXRhdG9y" + "SW5mb0NvbmZpZ3VyZToCOAEaRgoNUG9vbERpY3RFbnRyeRILCgNrZXkYASAB" + "KA8SJAoFdmFsdWUYAiABKAsyFS5NdXRhdG9yUG9vbENvbmZpZ3VyZToCOAEa" + "VAoUUG9vbENvbXBvc2VEaWN0RW50cnkSCwoDa2V5GAEgASgPEisKBXZhbHVl" + "GAIgASgLMhwuTXV0YXRvclBvb2xDb21wb3NlQ29uZmlndXJlOgI4AWIGcHJv" + "dG8z"), new FileDescriptor[1] { EnumReflection.Descriptor }, new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[6]
		{
			new GeneratedClrTypeInfo(typeof(MutatorInfoConfigure), MutatorInfoConfigure.Parser, new string[9] { "Id", "NameID", "DescId", "MutatorType", "Icon", "Params", "BuffId", "PreloadCharacterIds", "Performs" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(MutatorPoolConfigure), MutatorPoolConfigure.Parser, new string[2] { "Id", "MutatorPoolConfigureItems" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(MutatorPoolConfigureItem), MutatorPoolConfigureItem.Parser, new string[1] { "MutatorId" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(MutatorPoolComposeConfigure), MutatorPoolComposeConfigure.Parser, new string[2] { "Id", "MutatorPoolComposeConfigureItems" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(MutatorPoolComposeConfigureItem), MutatorPoolComposeConfigureItem.Parser, new string[2] { "Index", "ComposeTypes" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(MutatorConfigure), MutatorConfigure.Parser, new string[6] { "Infos", "InfoDict", "Pools", "PoolDict", "PoolComposes", "PoolComposeDict" }, null, null, null, new GeneratedClrTypeInfo[3])
		}));
	}
}
