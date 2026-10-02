using System;
using Google.Protobuf.Reflection;

public static class WelfareReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static WelfareReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("Cg1XZWxmYXJlLnByb3RvGgpFbnVtLnByb3RvIlcKFFdlbGZhcmVJbmZvQ29u" + "ZmlndXJlEiEKC3dlbGZhcmVUeXBlGAEgASgOMgwuV2VsZmFyZVR5cGUSDgoG" + "aXNTaG93GAIgASgIEgwKBG5hbWUYAyABKA8iswEKEFdlbGZhcmVDb25maWd1" + "cmUSJAoFSW5mb3MYASADKAsyFS5XZWxmYXJlSW5mb0NvbmZpZ3VyZRIxCghJ" + "bmZvRGljdBgCIAMoCzIfLldlbGZhcmVDb25maWd1cmUuSW5mb0RpY3RFbnRy" + "eRpGCg1JbmZvRGljdEVudHJ5EgsKA2tleRgBIAEoDxIkCgV2YWx1ZRgCIAEo" + "CzIVLldlbGZhcmVJbmZvQ29uZmlndXJlOgI4AWIGcHJvdG8z"), new FileDescriptor[1] { EnumReflection.Descriptor }, new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[2]
		{
			new GeneratedClrTypeInfo(typeof(WelfareInfoConfigure), WelfareInfoConfigure.Parser, new string[3] { "WelfareType", "IsShow", "Name" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(WelfareConfigure), WelfareConfigure.Parser, new string[2] { "Infos", "InfoDict" }, null, null, null, new GeneratedClrTypeInfo[1])
		}));
	}
}
