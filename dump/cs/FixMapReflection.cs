using System;
using Google.Protobuf.Reflection;
using Google.Protobuf.WellKnownTypes;

public static class FixMapReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static FixMapReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("CgxGaXhNYXAucHJvdG8aH2dvb2dsZS9wcm90b2J1Zi90aW1lc3RhbXAucHJv" + "dG8iaQoXRml4TWFwTWFwTGV2ZWxDb25maWd1cmUSCgoCaWQYASABKA8SQgoc" + "Zml4TWFwTWFwTGV2ZWxDb25maWd1cmVJdGVtcxgCIAMoCzIcLkZpeE1hcE1h" + "cExldmVsQ29uZmlndXJlSXRlbSLyAQobRml4TWFwTWFwTGV2ZWxDb25maWd1" + "cmVJdGVtEg0KBWluZGV4GAEgASgPEi0KCWJlZ2luVGltZRgCIAEoCzIaLmdv" + "b2dsZS5wcm90b2J1Zi5UaW1lc3RhbXASKwoHZW5kVGltZRgDIAEoCzIaLmdv" + "b2dsZS5wcm90b2J1Zi5UaW1lc3RhbXASNAoQYmVnaW5UaW1lTXV0YXRvchgE" + "IAEoCzIaLmdvb2dsZS5wcm90b2J1Zi5UaW1lc3RhbXASMgoOZW5kVGltZU11" + "dGF0b3IYBSABKAsyGi5nb29nbGUucHJvdG9idWYuVGltZXN0YW1wIscBCg9G" + "aXhNYXBDb25maWd1cmUSKwoJTWFwTGV2ZWxzGAEgAygLMhguRml4TWFwTWFw" + "TGV2ZWxDb25maWd1cmUSOAoMTWFwTGV2ZWxEaWN0GAIgAygLMiIuRml4TWFw" + "Q29uZmlndXJlLk1hcExldmVsRGljdEVudHJ5Gk0KEU1hcExldmVsRGljdEVu" + "dHJ5EgsKA2tleRgBIAEoDxInCgV2YWx1ZRgCIAEoCzIYLkZpeE1hcE1hcExl" + "dmVsQ29uZmlndXJlOgI4AWIGcHJvdG8z"), new FileDescriptor[1] { TimestampReflection.Descriptor }, new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[3]
		{
			new GeneratedClrTypeInfo(typeof(FixMapMapLevelConfigure), FixMapMapLevelConfigure.Parser, new string[2] { "Id", "FixMapMapLevelConfigureItems" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(FixMapMapLevelConfigureItem), FixMapMapLevelConfigureItem.Parser, new string[5] { "Index", "BeginTime", "EndTime", "BeginTimeMutator", "EndTimeMutator" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(FixMapConfigure), FixMapConfigure.Parser, new string[2] { "MapLevels", "MapLevelDict" }, null, null, null, new GeneratedClrTypeInfo[1])
		}));
	}
}
