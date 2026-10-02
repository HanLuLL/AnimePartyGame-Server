using System;
using Google.Protobuf.Reflection;

public static class ImageLocalizationReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static ImageLocalizationReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("ChdJbWFnZUxvY2FsaXphdGlvbi5wcm90byJ5Ch9JbWFnZUxvY2FsaXphdGlv" + "bkxvY2FsQ29uZmlndXJlEgoKAmlkGAEgASgPEhIKCnNpbXBsaWZpZWQYAiAB" + "KAkSDwoHZW5nbGlzaBgDIAEoCRIQCghqYXBhbmVzZRgEIAEoCRITCgt0cmFk" + "aXRpb25hbBgFIAEoCSLhAQoaSW1hZ2VMb2NhbGl6YXRpb25Db25maWd1cmUS" + "MAoGTG9jYWxzGAEgAygLMiAuSW1hZ2VMb2NhbGl6YXRpb25Mb2NhbENvbmZp" + "Z3VyZRI9CglMb2NhbERpY3QYAiADKAsyKi5JbWFnZUxvY2FsaXphdGlvbkNv" + "bmZpZ3VyZS5Mb2NhbERpY3RFbnRyeRpSCg5Mb2NhbERpY3RFbnRyeRILCgNr" + "ZXkYASABKA8SLwoFdmFsdWUYAiABKAsyIC5JbWFnZUxvY2FsaXphdGlvbkxv" + "Y2FsQ29uZmlndXJlOgI4AWIGcHJvdG8z"), new FileDescriptor[0], new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[2]
		{
			new GeneratedClrTypeInfo(typeof(ImageLocalizationLocalConfigure), ImageLocalizationLocalConfigure.Parser, new string[5] { "Id", "Simplified", "English", "Japanese", "Traditional" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(ImageLocalizationConfigure), ImageLocalizationConfigure.Parser, new string[2] { "Locals", "LocalDict" }, null, null, null, new GeneratedClrTypeInfo[1])
		}));
	}
}
