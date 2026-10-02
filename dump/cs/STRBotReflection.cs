using System;
using Google.Protobuf.Reflection;

public static class STRBotReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static STRBotReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("CgxTVFJCb3QucHJvdG8ifgoUU1RSQm90TG9jYWxDb25maWd1cmUSCgoCaWQY" + "ASABKA8SEgoKc2ltcGxpZmllZBgCIAEoCRIPCgdlbmdsaXNoGAMgASgJEhAK" + "CGphcGFuZXNlGAQgASgJEhMKC3RyYWRpdGlvbmFsGAUgASgJEg4KBmtvcmVh" + "bhgGIAEoCSK1AQoPU1RSQm90Q29uZmlndXJlEiUKBkxvY2FscxgBIAMoCzIV" + "LlNUUkJvdExvY2FsQ29uZmlndXJlEjIKCUxvY2FsRGljdBgCIAMoCzIfLlNU" + "UkJvdENvbmZpZ3VyZS5Mb2NhbERpY3RFbnRyeRpHCg5Mb2NhbERpY3RFbnRy" + "eRILCgNrZXkYASABKA8SJAoFdmFsdWUYAiABKAsyFS5TVFJCb3RMb2NhbENv" + "bmZpZ3VyZToCOAFiBnByb3RvMw=="), new FileDescriptor[0], new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[2]
		{
			new GeneratedClrTypeInfo(typeof(STRBotLocalConfigure), STRBotLocalConfigure.Parser, new string[6] { "Id", "Simplified", "English", "Japanese", "Traditional", "Korean" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(STRBotConfigure), STRBotConfigure.Parser, new string[2] { "Locals", "LocalDict" }, null, null, null, new GeneratedClrTypeInfo[1])
		}));
	}
}
