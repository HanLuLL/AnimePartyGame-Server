using System;
using Google.Protobuf.Reflection;

public static class ComebackReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static ComebackReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("Cg5Db21lYmFjay5wcm90byK+AgoXQ29tZWJhY2tQYXJhbXNDb25maWd1cmUS" + "CgoCaWQYASABKA8SEgoKYWN0aXZpdHlJZBgCIAEoDxIPCgdjaGVzdElkGAMg" + "ASgPEhAKCHB2ZUxldmVsGAQgASgPEg8KB2hlcm9JRHMYBSADKA8SEAoIc2ln" + "bkluSWQYBiABKA8SGAoQc2lnbkluUmVjaGFyZ2VJZBgHIAEoDxIXCg9xdWVz" + "dGlvbm5haXJlSWQYCCADKAkSTgoTcXVlc3Rpb25uYWlyZVJld2FyZBgJIAMo" + "CzIxLkNvbWViYWNrUGFyYW1zQ29uZmlndXJlLlF1ZXN0aW9ubmFpcmVSZXdh" + "cmRFbnRyeRo6ChhRdWVzdGlvbm5haXJlUmV3YXJkRW50cnkSCwoDa2V5GAEg" + "ASgPEg0KBXZhbHVlGAIgASgPOgI4ASLDAQoRQ29tZWJhY2tDb25maWd1cmUS" + "KQoHUGFyYW1zcxgBIAMoCzIYLkNvbWViYWNrUGFyYW1zQ29uZmlndXJlEjYK" + "ClBhcmFtc0RpY3QYAiADKAsyIi5Db21lYmFja0NvbmZpZ3VyZS5QYXJhbXNE" + "aWN0RW50cnkaSwoPUGFyYW1zRGljdEVudHJ5EgsKA2tleRgBIAEoDxInCgV2" + "YWx1ZRgCIAEoCzIYLkNvbWViYWNrUGFyYW1zQ29uZmlndXJlOgI4AWIGcHJv" + "dG8z"), new FileDescriptor[0], new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[2]
		{
			new GeneratedClrTypeInfo(typeof(ComebackParamsConfigure), ComebackParamsConfigure.Parser, new string[9] { "Id", "ActivityId", "ChestId", "PveLevel", "HeroIDs", "SignInId", "SignInRechargeId", "QuestionnaireId", "QuestionnaireReward" }, null, null, null, new GeneratedClrTypeInfo[1]),
			new GeneratedClrTypeInfo(typeof(ComebackConfigure), ComebackConfigure.Parser, new string[2] { "Paramss", "ParamsDict" }, null, null, null, new GeneratedClrTypeInfo[1])
		}));
	}
}
