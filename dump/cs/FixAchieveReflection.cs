using System;
using Google.Protobuf.Reflection;

public static class FixAchieveReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static FixAchieveReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("ChBGaXhBY2hpZXZlLnByb3RvIicKGUZpeEFjaGlldmVHbG9iYWxDb25maWd1" + "cmUSCgoCaWQYASABKA8iywEKE0ZpeEFjaGlldmVDb25maWd1cmUSKwoHR2xv" + "YmFscxgBIAMoCzIaLkZpeEFjaGlldmVHbG9iYWxDb25maWd1cmUSOAoKR2xv" + "YmFsRGljdBgCIAMoCzIkLkZpeEFjaGlldmVDb25maWd1cmUuR2xvYmFsRGlj" + "dEVudHJ5Gk0KD0dsb2JhbERpY3RFbnRyeRILCgNrZXkYASABKA8SKQoFdmFs" + "dWUYAiABKAsyGi5GaXhBY2hpZXZlR2xvYmFsQ29uZmlndXJlOgI4AWIGcHJv" + "dG8z"), new FileDescriptor[0], new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[2]
		{
			new GeneratedClrTypeInfo(typeof(FixAchieveGlobalConfigure), FixAchieveGlobalConfigure.Parser, new string[1] { "Id" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(FixAchieveConfigure), FixAchieveConfigure.Parser, new string[2] { "Globals", "GlobalDict" }, null, null, null, new GeneratedClrTypeInfo[1])
		}));
	}
}
