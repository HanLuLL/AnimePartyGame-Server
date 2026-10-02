using System;
using Google.Protobuf.Reflection;

public static class STRWayReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static STRWayReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("CgxTVFJXYXkucHJvdG8ifgoUU1RSV2F5TG9jYWxDb25maWd1cmUSCgoCaWQY" + "ASABKA8SEgoKc2ltcGxpZmllZBgCIAEoCRIPCgdlbmdsaXNoGAMgASgJEhAK" + "CGphcGFuZXNlGAQgASgJEhMKC3RyYWRpdGlvbmFsGAUgASgJEg4KBmtvcmVh" + "bhgGIAEoCSK1AQoPU1RSV2F5Q29uZmlndXJlEiUKBkxvY2FscxgBIAMoCzIV" + "LlNUUldheUxvY2FsQ29uZmlndXJlEjIKCUxvY2FsRGljdBgCIAMoCzIfLlNU" + "UldheUNvbmZpZ3VyZS5Mb2NhbERpY3RFbnRyeRpHCg5Mb2NhbERpY3RFbnRy" + "eRILCgNrZXkYASABKA8SJAoFdmFsdWUYAiABKAsyFS5TVFJXYXlMb2NhbENv" + "bmZpZ3VyZToCOAFiBnByb3RvMw=="), new FileDescriptor[0], new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[2]
		{
			new GeneratedClrTypeInfo(typeof(STRWayLocalConfigure), STRWayLocalConfigure.Parser, new string[6] { "Id", "Simplified", "English", "Japanese", "Traditional", "Korean" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(STRWayConfigure), STRWayConfigure.Parser, new string[2] { "Locals", "LocalDict" }, null, null, null, new GeneratedClrTypeInfo[1])
		}));
	}
}
