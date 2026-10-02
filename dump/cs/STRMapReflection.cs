using System;
using Google.Protobuf.Reflection;

public static class STRMapReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static STRMapReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("CgxTVFJNYXAucHJvdG8ifgoUU1RSTWFwTG9jYWxDb25maWd1cmUSCgoCaWQY" + "ASABKA8SEgoKc2ltcGxpZmllZBgCIAEoCRIPCgdlbmdsaXNoGAMgASgJEhAK" + "CGphcGFuZXNlGAQgASgJEhMKC3RyYWRpdGlvbmFsGAUgASgJEg4KBmtvcmVh" + "bhgGIAEoCSK1AQoPU1RSTWFwQ29uZmlndXJlEiUKBkxvY2FscxgBIAMoCzIV" + "LlNUUk1hcExvY2FsQ29uZmlndXJlEjIKCUxvY2FsRGljdBgCIAMoCzIfLlNU" + "Uk1hcENvbmZpZ3VyZS5Mb2NhbERpY3RFbnRyeRpHCg5Mb2NhbERpY3RFbnRy" + "eRILCgNrZXkYASABKA8SJAoFdmFsdWUYAiABKAsyFS5TVFJNYXBMb2NhbENv" + "bmZpZ3VyZToCOAFiBnByb3RvMw=="), new FileDescriptor[0], new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[2]
		{
			new GeneratedClrTypeInfo(typeof(STRMapLocalConfigure), STRMapLocalConfigure.Parser, new string[6] { "Id", "Simplified", "English", "Japanese", "Traditional", "Korean" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(STRMapConfigure), STRMapConfigure.Parser, new string[2] { "Locals", "LocalDict" }, null, null, null, new GeneratedClrTypeInfo[1])
		}));
	}
}
