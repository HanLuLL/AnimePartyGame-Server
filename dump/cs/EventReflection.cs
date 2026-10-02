using System;
using Google.Protobuf.Reflection;

public static class EventReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static EventReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("CgtFdmVudC5wcm90bxoKRW51bS5wcm90byKWAgoSRXZlbnRJbmZvQ29uZmln" + "dXJlEgoKAmlkGAEgASgPEhQKDGZ1bmN0aW9uTmFtZRgCIAEoCRIbCghjYXJk" + "VHlwZRgDIAEoDjIJLkNhcmRUeXBlEhUKDWlzR2FsbGVyeVNob3cYBCABKAgS" + "DgoGbmFtZUlEGAUgASgPEhAKCGNhcmROdW1iGAYgASgPEg4KBmRlc2NJZBgH" + "IAEoDxIRCgljb21tZW50SWQYCCABKA8SDQoFaW1hZ2UYCSABKAkSEQoJaW1h" + "Z2Vfc2Z3GAogASgJEg4KBnBhcmFtcxgLIAMoERIPCgdidWZmSWRzGAwgAygP" + "EhAKCHBlcmZvcm0xGA0gASgPEhAKCHBlcmZvcm0yGA4gASgPImAKFEV2ZW50" + "UGVyaW9kQ29uZmlndXJlEgoKAmlkGAEgASgPEjwKGWV2ZW50UGVyaW9kQ29u" + "ZmlndXJlSXRlbXMYAiADKAsyGS5FdmVudFBlcmlvZENvbmZpZ3VyZUl0ZW0i" + "KwoYRXZlbnRQZXJpb2RDb25maWd1cmVJdGVtEg8KB2V2ZW50SWQYASABKA8i" + "0gIKDkV2ZW50Q29uZmlndXJlEiIKBUluZm9zGAEgAygLMhMuRXZlbnRJbmZv" + "Q29uZmlndXJlEi8KCEluZm9EaWN0GAIgAygLMh0uRXZlbnRDb25maWd1cmUu" + "SW5mb0RpY3RFbnRyeRImCgdQZXJpb2RzGAMgAygLMhUuRXZlbnRQZXJpb2RD" + "b25maWd1cmUSMwoKUGVyaW9kRGljdBgEIAMoCzIfLkV2ZW50Q29uZmlndXJl" + "LlBlcmlvZERpY3RFbnRyeRpECg1JbmZvRGljdEVudHJ5EgsKA2tleRgBIAEo" + "DxIiCgV2YWx1ZRgCIAEoCzITLkV2ZW50SW5mb0NvbmZpZ3VyZToCOAEaSAoP" + "UGVyaW9kRGljdEVudHJ5EgsKA2tleRgBIAEoDxIkCgV2YWx1ZRgCIAEoCzIV" + "LkV2ZW50UGVyaW9kQ29uZmlndXJlOgI4AWIGcHJvdG8z"), new FileDescriptor[1] { EnumReflection.Descriptor }, new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[4]
		{
			new GeneratedClrTypeInfo(typeof(EventInfoConfigure), EventInfoConfigure.Parser, new string[14]
			{
				"Id", "FunctionName", "CardType", "IsGalleryShow", "NameID", "CardNumb", "DescId", "CommentId", "Image", "ImageSfw",
				"Params", "BuffIds", "Perform1", "Perform2"
			}, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(EventPeriodConfigure), EventPeriodConfigure.Parser, new string[2] { "Id", "EventPeriodConfigureItems" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(EventPeriodConfigureItem), EventPeriodConfigureItem.Parser, new string[1] { "EventId" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(EventConfigure), EventConfigure.Parser, new string[4] { "Infos", "InfoDict", "Periods", "PeriodDict" }, null, null, null, new GeneratedClrTypeInfo[2])
		}));
	}
}
