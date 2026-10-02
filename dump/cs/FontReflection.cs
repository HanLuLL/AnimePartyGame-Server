using System;
using Google.Protobuf.Reflection;

public static class FontReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static FontReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("CgpGb250LnByb3RvIoYBChFGb250SW5mb0NvbmZpZ3VyZRIKCgJpZBgBIAEo" + "DxIMCgRuYW1lGAIgASgJEhQKDGxvYWRlZEtleV9DThgDIAEoCRIUCgxsb2Fk" + "ZWRLZXlfRU4YBCABKAkSFAoMbG9hZGVkS2V5X0pQGAUgASgJEhUKDWxvYWRl" + "ZEtleV9DSFQYBiABKAkipwEKDUZvbnRDb25maWd1cmUSIQoFSW5mb3MYASAD" + "KAsyEi5Gb250SW5mb0NvbmZpZ3VyZRIuCghJbmZvRGljdBgCIAMoCzIcLkZv" + "bnRDb25maWd1cmUuSW5mb0RpY3RFbnRyeRpDCg1JbmZvRGljdEVudHJ5EgsK" + "A2tleRgBIAEoDxIhCgV2YWx1ZRgCIAEoCzISLkZvbnRJbmZvQ29uZmlndXJl" + "OgI4AWIGcHJvdG8z"), new FileDescriptor[0], new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[2]
		{
			new GeneratedClrTypeInfo(typeof(FontInfoConfigure), FontInfoConfigure.Parser, new string[6] { "Id", "Name", "LoadedKeyCN", "LoadedKeyEN", "LoadedKeyJP", "LoadedKeyCHT" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(FontConfigure), FontConfigure.Parser, new string[2] { "Infos", "InfoDict" }, null, null, null, new GeneratedClrTypeInfo[1])
		}));
	}
}
