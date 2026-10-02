using System;
using Google.Protobuf.Reflection;
using Google.Protobuf.WellKnownTypes;

public static class FixGameModeReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static FixGameModeReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("ChFGaXhHYW1lTW9kZS5wcm90bxofZ29vZ2xlL3Byb3RvYnVmL3RpbWVzdGFt" + "cC5wcm90bxoKRW51bS5wcm90byKoAQoYRml4R2FtZU1vZGVJbmZvQ29uZmln" + "dXJlEiEKC21hcE1vZGVUeXBlGAEgASgOMgwuTWFwTW9kZVR5cGUSLQoJYmVn" + "aW5UaW1lGAIgASgLMhouZ29vZ2xlLnByb3RvYnVmLlRpbWVzdGFtcBIrCgdl" + "bmRUaW1lGAMgASgLMhouZ29vZ2xlLnByb3RvYnVmLlRpbWVzdGFtcBINCgVt" + "YXBJRBgEIAMoDyLDAQoURml4R2FtZU1vZGVDb25maWd1cmUSKAoFSW5mb3MY" + "ASADKAsyGS5GaXhHYW1lTW9kZUluZm9Db25maWd1cmUSNQoISW5mb0RpY3QY" + "AiADKAsyIy5GaXhHYW1lTW9kZUNvbmZpZ3VyZS5JbmZvRGljdEVudHJ5GkoK" + "DUluZm9EaWN0RW50cnkSCwoDa2V5GAEgASgPEigKBXZhbHVlGAIgASgLMhku" + "Rml4R2FtZU1vZGVJbmZvQ29uZmlndXJlOgI4AWIGcHJvdG8z"), new FileDescriptor[2]
		{
			TimestampReflection.Descriptor,
			EnumReflection.Descriptor
		}, new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[2]
		{
			new GeneratedClrTypeInfo(typeof(FixGameModeInfoConfigure), FixGameModeInfoConfigure.Parser, new string[4] { "MapModeType", "BeginTime", "EndTime", "MapID" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(FixGameModeConfigure), FixGameModeConfigure.Parser, new string[2] { "Infos", "InfoDict" }, null, null, null, new GeneratedClrTypeInfo[1])
		}));
	}
}
