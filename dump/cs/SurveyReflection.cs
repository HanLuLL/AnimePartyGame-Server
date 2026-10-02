using System;
using Google.Protobuf.Reflection;
using Google.Protobuf.WellKnownTypes;

public static class SurveyReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static SurveyReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("CgxTdXJ2ZXkucHJvdG8aH2dvb2dsZS9wcm90b2J1Zi90aW1lc3RhbXAucHJv" + "dG8aCkVudW0ucHJvdG8iuQMKE1N1cnZleUluZm9Db25maWd1cmUSCgoCaWQY" + "ASABKA8SHwoKc3VydmV5VHlwZRgCIAEoDjILLlN1cnZleVR5cGUSDgoGbmFt" + "ZUlEGAMgASgPEg8KB3RpdGxlSUQYBCABKA8SEwoLc3VydmV5SW1hZ2UYBSAB" + "KAkSDgoGdGV4dElEGAYgASgPEhMKC21haWxDb250ZW50GAcgASgPEhIKCnN1" + "cnZleUxpbmsYCCABKAkSMAoGcmV3YXJkGAkgAygLMiAuU3VydmV5SW5mb0Nv" + "bmZpZ3VyZS5SZXdhcmRFbnRyeRItCgliZWdpblRpbWUYCiABKAsyGi5nb29n" + "bGUucHJvdG9idWYuVGltZXN0YW1wEisKB2VuZFRpbWUYCyABKAsyGi5nb29n" + "bGUucHJvdG9idWYuVGltZXN0YW1wEhAKCGR1cmF0aW9uGAwgASgPEhEKCW1h" + "aWxUaXRsZRgNIAEoDxIQCghtYWlsVGV4dBgOIAEoDxISCgptYWlsU2VuZGVy" + "GA8gASgPGi0KC1Jld2FyZEVudHJ5EgsKA2tleRgBIAEoDxINCgV2YWx1ZRgC" + "IAEoDzoCOAEirwEKD1N1cnZleUNvbmZpZ3VyZRIjCgVJbmZvcxgBIAMoCzIU" + "LlN1cnZleUluZm9Db25maWd1cmUSMAoISW5mb0RpY3QYAiADKAsyHi5TdXJ2" + "ZXlDb25maWd1cmUuSW5mb0RpY3RFbnRyeRpFCg1JbmZvRGljdEVudHJ5EgsK" + "A2tleRgBIAEoDxIjCgV2YWx1ZRgCIAEoCzIULlN1cnZleUluZm9Db25maWd1" + "cmU6AjgBYgZwcm90bzM="), new FileDescriptor[2]
		{
			TimestampReflection.Descriptor,
			EnumReflection.Descriptor
		}, new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[2]
		{
			new GeneratedClrTypeInfo(typeof(SurveyInfoConfigure), SurveyInfoConfigure.Parser, new string[15]
			{
				"Id", "SurveyType", "NameID", "TitleID", "SurveyImage", "TextID", "MailContent", "SurveyLink", "Reward", "BeginTime",
				"EndTime", "Duration", "MailTitle", "MailText", "MailSender"
			}, null, null, null, new GeneratedClrTypeInfo[1]),
			new GeneratedClrTypeInfo(typeof(SurveyConfigure), SurveyConfigure.Parser, new string[2] { "Infos", "InfoDict" }, null, null, null, new GeneratedClrTypeInfo[1])
		}));
	}
}
