using System;
using Google.Protobuf.Reflection;

public static class CouponsReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static CouponsReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("Cg1Db3Vwb25zLnByb3RvIjQKFENvdXBvbnNJbmZvQ29uZmlndXJlEgoKAmlk" + "GAEgASgPEhAKCGRpc2NvdW50GAIgASgPIrMBChBDb3Vwb25zQ29uZmlndXJl" + "EiQKBUluZm9zGAEgAygLMhUuQ291cG9uc0luZm9Db25maWd1cmUSMQoISW5m" + "b0RpY3QYAiADKAsyHy5Db3Vwb25zQ29uZmlndXJlLkluZm9EaWN0RW50cnka" + "RgoNSW5mb0RpY3RFbnRyeRILCgNrZXkYASABKA8SJAoFdmFsdWUYAiABKAsy" + "FS5Db3Vwb25zSW5mb0NvbmZpZ3VyZToCOAFiBnByb3RvMw=="), new FileDescriptor[0], new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[2]
		{
			new GeneratedClrTypeInfo(typeof(CouponsInfoConfigure), CouponsInfoConfigure.Parser, new string[2] { "Id", "Discount" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(CouponsConfigure), CouponsConfigure.Parser, new string[2] { "Infos", "InfoDict" }, null, null, null, new GeneratedClrTypeInfo[1])
		}));
	}
}
