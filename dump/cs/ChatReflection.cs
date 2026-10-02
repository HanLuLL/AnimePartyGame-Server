using System;
using Google.Protobuf.Reflection;

public static class ChatReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static ChatReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("CgpDaGF0LnByb3RvGgpFbnVtLnByb3RvIoEBChFDaGF0SW5mb0NvbmZpZ3Vy" + "ZRIKCgJJRBgBIAEoDxIOCgZDaGF0SUQYAiABKA8SGwoIY2hhdFR5cGUYAyAD" + "KA4yCS5DaGF0VHlwZRIhCgttYXBNb2RlVHlwZRgEIAMoDjIMLk1hcE1vZGVU" + "eXBlEhAKCG1hcExpbWl0GAUgAygPIk4KEUNoYXRNYXJrQ29uZmlndXJlEgoK" + "AklEGAEgASgPEg4KBkNoYXRJRBgCIAEoDxIPCgdlZmZjdElEGAMgASgPEgwK" + "BGljb24YBCABKAkivwIKDUNoYXRDb25maWd1cmUSIQoFSW5mb3MYASADKAsy" + "Ei5DaGF0SW5mb0NvbmZpZ3VyZRIuCghJbmZvRGljdBgCIAMoCzIcLkNoYXRD" + "b25maWd1cmUuSW5mb0RpY3RFbnRyeRIhCgVNYXJrcxgDIAMoCzISLkNoYXRN" + "YXJrQ29uZmlndXJlEi4KCE1hcmtEaWN0GAQgAygLMhwuQ2hhdENvbmZpZ3Vy" + "ZS5NYXJrRGljdEVudHJ5GkMKDUluZm9EaWN0RW50cnkSCwoDa2V5GAEgASgP" + "EiEKBXZhbHVlGAIgASgLMhIuQ2hhdEluZm9Db25maWd1cmU6AjgBGkMKDU1h" + "cmtEaWN0RW50cnkSCwoDa2V5GAEgASgPEiEKBXZhbHVlGAIgASgLMhIuQ2hh" + "dE1hcmtDb25maWd1cmU6AjgBYgZwcm90bzM="), new FileDescriptor[1] { EnumReflection.Descriptor }, new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[3]
		{
			new GeneratedClrTypeInfo(typeof(ChatInfoConfigure), ChatInfoConfigure.Parser, new string[5] { "ID", "ChatID", "ChatType", "MapModeType", "MapLimit" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(ChatMarkConfigure), ChatMarkConfigure.Parser, new string[4] { "ID", "ChatID", "EffctID", "Icon" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(ChatConfigure), ChatConfigure.Parser, new string[4] { "Infos", "InfoDict", "Marks", "MarkDict" }, null, null, null, new GeneratedClrTypeInfo[2])
		}));
	}
}
