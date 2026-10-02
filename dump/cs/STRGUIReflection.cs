using System;
using Google.Protobuf.Reflection;

public static class STRGUIReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static STRGUIReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("CgxTVFJHVUkucHJvdG8ifgoUU1RSR1VJTG9jYWxDb25maWd1cmUSCgoCaWQY" + "ASABKA8SEgoKc2ltcGxpZmllZBgCIAEoCRIPCgdlbmdsaXNoGAMgASgJEhAK" + "CGphcGFuZXNlGAQgASgJEhMKC3RyYWRpdGlvbmFsGAUgASgJEg4KBmtvcmVh" + "bhgGIAEoCSK1AQoPU1RSR1VJQ29uZmlndXJlEiUKBkxvY2FscxgBIAMoCzIV" + "LlNUUkdVSUxvY2FsQ29uZmlndXJlEjIKCUxvY2FsRGljdBgCIAMoCzIfLlNU" + "UkdVSUNvbmZpZ3VyZS5Mb2NhbERpY3RFbnRyeRpHCg5Mb2NhbERpY3RFbnRy" + "eRILCgNrZXkYASABKA8SJAoFdmFsdWUYAiABKAsyFS5TVFJHVUlMb2NhbENv" + "bmZpZ3VyZToCOAFiBnByb3RvMw=="), new FileDescriptor[0], new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[2]
		{
			new GeneratedClrTypeInfo(typeof(STRGUILocalConfigure), STRGUILocalConfigure.Parser, new string[6] { "Id", "Simplified", "English", "Japanese", "Traditional", "Korean" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(STRGUIConfigure), STRGUIConfigure.Parser, new string[2] { "Locals", "LocalDict" }, null, null, null, new GeneratedClrTypeInfo[1])
		}));
	}
}
