using System;
using Google.Protobuf.Reflection;

public static class FixSteamReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static FixSteamReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("Cg5GaXhTdGVhbS5wcm90byI2ChpGaXhTdGVhbVRoYW5rMjU4OENvbmZpZ3Vy" + "ZRIKCgJpZBgBIAEoDxIMCgRuYW1lGAIgASgJIjUKGUZpeFN0ZWFtVGhhbms2" + "ODhDb25maWd1cmUSCgoCaWQYASABKA8SDAoEbmFtZRgCIAEoCSI1ChlGaXhT" + "dGVhbVRoYW5rMzg4Q29uZmlndXJlEgoKAmlkGAEgASgPEgwKBG5hbWUYAiAB" + "KAkiNQoZRml4U3RlYW1UaGFuazEzOENvbmZpZ3VyZRIKCgJpZBgBIAEoDxIM" + "CgRuYW1lGAIgASgJIjQKGEZpeFN0ZWFtVGhhbms2OENvbmZpZ3VyZRIKCgJp" + "ZBgBIAEoDxIMCgRuYW1lGAIgASgJIjQKGEZpeFN0ZWFtVGhhbmsyOENvbmZp" + "Z3VyZRIKCgJpZBgBIAEoDxIMCgRuYW1lGAIgASgJIjMKF0ZpeFN0ZWFtVGhh" + "bmswQ29uZmlndXJlEgoKAmlkGAEgASgPEgwKBG5hbWUYAiABKAkipQoKEUZp" + "eFN0ZWFtQ29uZmlndXJlEi8KClRoYW5rMjU4OHMYASADKAsyGy5GaXhTdGVh" + "bVRoYW5rMjU4OENvbmZpZ3VyZRI8Cg1UaGFuazI1ODhEaWN0GAIgAygLMiUu" + "Rml4U3RlYW1Db25maWd1cmUuVGhhbmsyNTg4RGljdEVudHJ5Ei0KCVRoYW5r" + "Njg4cxgDIAMoCzIaLkZpeFN0ZWFtVGhhbms2ODhDb25maWd1cmUSOgoMVGhh" + "bms2ODhEaWN0GAQgAygLMiQuRml4U3RlYW1Db25maWd1cmUuVGhhbms2ODhE" + "aWN0RW50cnkSLQoJVGhhbmszODhzGAUgAygLMhouRml4U3RlYW1UaGFuazM4" + "OENvbmZpZ3VyZRI6CgxUaGFuazM4OERpY3QYBiADKAsyJC5GaXhTdGVhbUNv" + "bmZpZ3VyZS5UaGFuazM4OERpY3RFbnRyeRItCglUaGFuazEzOHMYByADKAsy" + "Gi5GaXhTdGVhbVRoYW5rMTM4Q29uZmlndXJlEjoKDFRoYW5rMTM4RGljdBgI" + "IAMoCzIkLkZpeFN0ZWFtQ29uZmlndXJlLlRoYW5rMTM4RGljdEVudHJ5EisK" + "CFRoYW5rNjhzGAkgAygLMhkuRml4U3RlYW1UaGFuazY4Q29uZmlndXJlEjgK" + "C1RoYW5rNjhEaWN0GAogAygLMiMuRml4U3RlYW1Db25maWd1cmUuVGhhbms2" + "OERpY3RFbnRyeRIrCghUaGFuazI4cxgLIAMoCzIZLkZpeFN0ZWFtVGhhbmsy" + "OENvbmZpZ3VyZRI4CgtUaGFuazI4RGljdBgMIAMoCzIjLkZpeFN0ZWFtQ29u" + "ZmlndXJlLlRoYW5rMjhEaWN0RW50cnkSKQoHVGhhbmswcxgNIAMoCzIYLkZp" + "eFN0ZWFtVGhhbmswQ29uZmlndXJlEjYKClRoYW5rMERpY3QYDiADKAsyIi5G" + "aXhTdGVhbUNvbmZpZ3VyZS5UaGFuazBEaWN0RW50cnkaUQoSVGhhbmsyNTg4" + "RGljdEVudHJ5EgsKA2tleRgBIAEoDxIqCgV2YWx1ZRgCIAEoCzIbLkZpeFN0" + "ZWFtVGhhbmsyNTg4Q29uZmlndXJlOgI4ARpPChFUaGFuazY4OERpY3RFbnRy" + "eRILCgNrZXkYASABKA8SKQoFdmFsdWUYAiABKAsyGi5GaXhTdGVhbVRoYW5r" + "Njg4Q29uZmlndXJlOgI4ARpPChFUaGFuazM4OERpY3RFbnRyeRILCgNrZXkY" + "ASABKA8SKQoFdmFsdWUYAiABKAsyGi5GaXhTdGVhbVRoYW5rMzg4Q29uZmln" + "dXJlOgI4ARpPChFUaGFuazEzOERpY3RFbnRyeRILCgNrZXkYASABKA8SKQoF" + "dmFsdWUYAiABKAsyGi5GaXhTdGVhbVRoYW5rMTM4Q29uZmlndXJlOgI4ARpN" + "ChBUaGFuazY4RGljdEVudHJ5EgsKA2tleRgBIAEoDxIoCgV2YWx1ZRgCIAEo" + "CzIZLkZpeFN0ZWFtVGhhbms2OENvbmZpZ3VyZToCOAEaTQoQVGhhbmsyOERp" + "Y3RFbnRyeRILCgNrZXkYASABKA8SKAoFdmFsdWUYAiABKAsyGS5GaXhTdGVh" + "bVRoYW5rMjhDb25maWd1cmU6AjgBGksKD1RoYW5rMERpY3RFbnRyeRILCgNr" + "ZXkYASABKA8SJwoFdmFsdWUYAiABKAsyGC5GaXhTdGVhbVRoYW5rMENvbmZp" + "Z3VyZToCOAFiBnByb3RvMw=="), new FileDescriptor[0], new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[8]
		{
			new GeneratedClrTypeInfo(typeof(FixSteamThank2588Configure), FixSteamThank2588Configure.Parser, new string[2] { "Id", "Name" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(FixSteamThank688Configure), FixSteamThank688Configure.Parser, new string[2] { "Id", "Name" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(FixSteamThank388Configure), FixSteamThank388Configure.Parser, new string[2] { "Id", "Name" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(FixSteamThank138Configure), FixSteamThank138Configure.Parser, new string[2] { "Id", "Name" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(FixSteamThank68Configure), FixSteamThank68Configure.Parser, new string[2] { "Id", "Name" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(FixSteamThank28Configure), FixSteamThank28Configure.Parser, new string[2] { "Id", "Name" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(FixSteamThank0Configure), FixSteamThank0Configure.Parser, new string[2] { "Id", "Name" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(FixSteamConfigure), FixSteamConfigure.Parser, new string[14]
			{
				"Thank2588S", "Thank2588Dict", "Thank688S", "Thank688Dict", "Thank388S", "Thank388Dict", "Thank138S", "Thank138Dict", "Thank68S", "Thank68Dict",
				"Thank28S", "Thank28Dict", "Thank0S", "Thank0Dict"
			}, null, null, null, new GeneratedClrTypeInfo[7])
		}));
	}
}
