using System;
using Google.Protobuf.Reflection;
using Google.Protobuf.WellKnownTypes;

public static class SignInReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static SignInReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("CgxTaWduSW4ucHJvdG8aH2dvb2dsZS9wcm90b2J1Zi90aW1lc3RhbXAucHJv" + "dG8i5QEKE1NpZ25JbkluZm9Db25maWd1cmUSCgoCaWQYASABKA8SLQoJYmVn" + "aW5UaW1lGAIgASgLMhouZ29vZ2xlLnByb3RvYnVmLlRpbWVzdGFtcBIrCgdl" + "bmRUaW1lGAMgASgLMhouZ29vZ2xlLnByb3RvYnVmLlRpbWVzdGFtcBIWCg5z" + "aWduaW5SZXdhcmRJRBgEIAEoDxIMCgRpY29uGAUgASgJEgoKAmJnGAYgASgJ" + "Eg0KBXRpdGxlGAcgASgPEhAKCGhlYWRsaW5lGAggASgPEhMKC2Rlc2NyaXB0" + "aW9uGAkgASgPIl0KE1NpZ25JbkRhdGFDb25maWd1cmUSCgoCaWQYASABKA8S" + "OgoYc2lnbkluRGF0YUNvbmZpZ3VyZUl0ZW1zGAIgAygLMhguU2lnbkluRGF0" + "YUNvbmZpZ3VyZUl0ZW0iiAIKF1NpZ25JbkRhdGFDb25maWd1cmVJdGVtEgsK" + "A2RheRgBIAEoDxI0CgZyZXdhcmQYAiADKAsyJC5TaWduSW5EYXRhQ29uZmln" + "dXJlSXRlbS5SZXdhcmRFbnRyeRJECg5yZWNoYXJnZVJld2FyZBgDIAMoCzIs" + "LlNpZ25JbkRhdGFDb25maWd1cmVJdGVtLlJlY2hhcmdlUmV3YXJkRW50cnka" + "LQoLUmV3YXJkRW50cnkSCwoDa2V5GAEgASgPEg0KBXZhbHVlGAIgASgPOgI4" + "ARo1ChNSZWNoYXJnZVJld2FyZEVudHJ5EgsKA2tleRgBIAEoDxINCgV2YWx1" + "ZRgCIAEoDzoCOAEizQIKD1NpZ25JbkNvbmZpZ3VyZRIjCgVJbmZvcxgBIAMo" + "CzIULlNpZ25JbkluZm9Db25maWd1cmUSMAoISW5mb0RpY3QYAiADKAsyHi5T" + "aWduSW5Db25maWd1cmUuSW5mb0RpY3RFbnRyeRIjCgVEYXRhcxgDIAMoCzIU" + "LlNpZ25JbkRhdGFDb25maWd1cmUSMAoIRGF0YURpY3QYBCADKAsyHi5TaWdu" + "SW5Db25maWd1cmUuRGF0YURpY3RFbnRyeRpFCg1JbmZvRGljdEVudHJ5EgsK" + "A2tleRgBIAEoDxIjCgV2YWx1ZRgCIAEoCzIULlNpZ25JbkluZm9Db25maWd1" + "cmU6AjgBGkUKDURhdGFEaWN0RW50cnkSCwoDa2V5GAEgASgPEiMKBXZhbHVl" + "GAIgASgLMhQuU2lnbkluRGF0YUNvbmZpZ3VyZToCOAFiBnByb3RvMw=="), new FileDescriptor[1] { TimestampReflection.Descriptor }, new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[4]
		{
			new GeneratedClrTypeInfo(typeof(SignInInfoConfigure), SignInInfoConfigure.Parser, new string[9] { "Id", "BeginTime", "EndTime", "SigninRewardID", "Icon", "Bg", "Title", "Headline", "Description" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(SignInDataConfigure), SignInDataConfigure.Parser, new string[2] { "Id", "SignInDataConfigureItems" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(SignInDataConfigureItem), SignInDataConfigureItem.Parser, new string[3] { "Day", "Reward", "RechargeReward" }, null, null, null, new GeneratedClrTypeInfo[2]),
			new GeneratedClrTypeInfo(typeof(SignInConfigure), SignInConfigure.Parser, new string[4] { "Infos", "InfoDict", "Datas", "DataDict" }, null, null, null, new GeneratedClrTypeInfo[2])
		}));
	}
}
