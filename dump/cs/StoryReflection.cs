using System;
using Google.Protobuf.Reflection;

public static class StoryReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static StoryReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("CgtTdG9yeS5wcm90byI0ChNTdG9yeVN0b3J5Q29uZmlndXJlEgoKAklEGAEg" + "ASgPEhEKCXN0b3J5RGF0YRgCIAEoCSJ4ChVTdG9yeVNwZWFrZXJDb25maWd1" + "cmUSCgoCSUQYASABKA8SEwoLQ2hhcmFjdGVySUQYAiABKA8SFAoMcHJvZmls" + "ZVBob3RvGAMgASgJEhQKDHByb2ZpbGVDb2xvchgEIAEoCRISCgpmcmFtZUlu" + "ZGV4GAUgASgPIsoBChJTdG9yeVNraW5Db25maWd1cmUSCgoCSUQYASABKA8S" + "FAoMYmFzZVBhaW50aW5nGAIgASgJEhMKC3Nmd1BhaW50aW5nGAMgASgJEg0K" + "BXBpdm90GAQgAygREg4KBmVtb2ppMRgFIAEoCRIOCgZlbW9qaTIYBiABKAkS" + "DgoGZW1vamkzGAcgASgJEg4KBmVtb2ppNBgIIAEoCRIOCgZlbW9qaTUYCSAB" + "KAkSDgoGZW1vamk2GAogASgJEg4KBmVtb2ppNxgLIAEoCSL5AwoOU3RvcnlD" + "b25maWd1cmUSJAoGU3RvcnlzGAEgAygLMhQuU3RvcnlTdG9yeUNvbmZpZ3Vy" + "ZRIxCglTdG9yeURpY3QYAiADKAsyHi5TdG9yeUNvbmZpZ3VyZS5TdG9yeURp" + "Y3RFbnRyeRIoCghTcGVha2VycxgDIAMoCzIWLlN0b3J5U3BlYWtlckNvbmZp" + "Z3VyZRI1CgtTcGVha2VyRGljdBgEIAMoCzIgLlN0b3J5Q29uZmlndXJlLlNw" + "ZWFrZXJEaWN0RW50cnkSIgoFU2tpbnMYBSADKAsyEy5TdG9yeVNraW5Db25m" + "aWd1cmUSLwoIU2tpbkRpY3QYBiADKAsyHS5TdG9yeUNvbmZpZ3VyZS5Ta2lu" + "RGljdEVudHJ5GkYKDlN0b3J5RGljdEVudHJ5EgsKA2tleRgBIAEoDxIjCgV2" + "YWx1ZRgCIAEoCzIULlN0b3J5U3RvcnlDb25maWd1cmU6AjgBGkoKEFNwZWFr" + "ZXJEaWN0RW50cnkSCwoDa2V5GAEgASgPEiUKBXZhbHVlGAIgASgLMhYuU3Rv" + "cnlTcGVha2VyQ29uZmlndXJlOgI4ARpECg1Ta2luRGljdEVudHJ5EgsKA2tl" + "eRgBIAEoDxIiCgV2YWx1ZRgCIAEoCzITLlN0b3J5U2tpbkNvbmZpZ3VyZToC" + "OAFiBnByb3RvMw=="), new FileDescriptor[0], new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[4]
		{
			new GeneratedClrTypeInfo(typeof(StoryStoryConfigure), StoryStoryConfigure.Parser, new string[2] { "ID", "StoryData" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(StorySpeakerConfigure), StorySpeakerConfigure.Parser, new string[5] { "ID", "CharacterID", "ProfilePhoto", "ProfileColor", "FrameIndex" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(StorySkinConfigure), StorySkinConfigure.Parser, new string[11]
			{
				"ID", "BasePainting", "SfwPainting", "Pivot", "Emoji1", "Emoji2", "Emoji3", "Emoji4", "Emoji5", "Emoji6",
				"Emoji7"
			}, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(StoryConfigure), StoryConfigure.Parser, new string[6] { "Storys", "StoryDict", "Speakers", "SpeakerDict", "Skins", "SkinDict" }, null, null, null, new GeneratedClrTypeInfo[3])
		}));
	}
}
