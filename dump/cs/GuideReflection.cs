using System;
using Google.Protobuf.Reflection;

public static class GuideReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static GuideReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("CgtHdWlkZS5wcm90byJfChJHdWlkZUluZm9Db25maWd1cmUSDwoHZ3VpZGVJ" + "ZBgBIAEoDxI4ChdndWlkZUluZm9Db25maWd1cmVJdGVtcxgCIAMoCzIXLkd1" + "aWRlSW5mb0NvbmZpZ3VyZUl0ZW0iXQoWR3VpZGVJbmZvQ29uZmlndXJlSXRl" + "bRIOCgZzdGVwSWQYASABKA8SEAoIZGlhbG9nSWQYAiABKA8SEgoKdHV0b3Jp" + "YWxJZBgDIAEoDxINCgVpc0VuZBgEIAEoCCKAAQoUR3VpZGVEaWFsb2dDb25m" + "aWd1cmUSCgoCaWQYASABKA8SEgoKcGNEaWFsb2dJZBgCIAEoDxITCgttb2JE" + "aWFsb2dJZBgDIAEoDxILCgNkaXIYBCABKAkSEgoKcm9sZVNwcml0ZRgFIAEo" + "CRISCgpzaG93QnV0dG9uGAYgASgIIlQKFkd1aWRlVHV0b3JpYWxDb25maWd1" + "cmUSCgoCaWQYASABKA8SFAoMUENUdXRvcmlhbElkGAIgASgPEhgKEG1vYmls" + "ZVR1dG9yaWFsSWQYAyABKA8ihQQKDkd1aWRlQ29uZmlndXJlEiIKBUluZm9z" + "GAEgAygLMhMuR3VpZGVJbmZvQ29uZmlndXJlEi8KCEluZm9EaWN0GAIgAygL" + "Mh0uR3VpZGVDb25maWd1cmUuSW5mb0RpY3RFbnRyeRImCgdEaWFsb2dzGAMg" + "AygLMhUuR3VpZGVEaWFsb2dDb25maWd1cmUSMwoKRGlhbG9nRGljdBgEIAMo" + "CzIfLkd1aWRlQ29uZmlndXJlLkRpYWxvZ0RpY3RFbnRyeRIqCglUdXRvcmlh" + "bHMYBSADKAsyFy5HdWlkZVR1dG9yaWFsQ29uZmlndXJlEjcKDFR1dG9yaWFs" + "RGljdBgGIAMoCzIhLkd1aWRlQ29uZmlndXJlLlR1dG9yaWFsRGljdEVudHJ5" + "GkQKDUluZm9EaWN0RW50cnkSCwoDa2V5GAEgASgPEiIKBXZhbHVlGAIgASgL" + "MhMuR3VpZGVJbmZvQ29uZmlndXJlOgI4ARpICg9EaWFsb2dEaWN0RW50cnkS" + "CwoDa2V5GAEgASgPEiQKBXZhbHVlGAIgASgLMhUuR3VpZGVEaWFsb2dDb25m" + "aWd1cmU6AjgBGkwKEVR1dG9yaWFsRGljdEVudHJ5EgsKA2tleRgBIAEoDxIm" + "CgV2YWx1ZRgCIAEoCzIXLkd1aWRlVHV0b3JpYWxDb25maWd1cmU6AjgBYgZw" + "cm90bzM="), new FileDescriptor[0], new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[5]
		{
			new GeneratedClrTypeInfo(typeof(GuideInfoConfigure), GuideInfoConfigure.Parser, new string[2] { "GuideId", "GuideInfoConfigureItems" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(GuideInfoConfigureItem), GuideInfoConfigureItem.Parser, new string[4] { "StepId", "DialogId", "TutorialId", "IsEnd" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(GuideDialogConfigure), GuideDialogConfigure.Parser, new string[6] { "Id", "PcDialogId", "MobDialogId", "Dir", "RoleSprite", "ShowButton" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(GuideTutorialConfigure), GuideTutorialConfigure.Parser, new string[3] { "Id", "PCTutorialId", "MobileTutorialId" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(GuideConfigure), GuideConfigure.Parser, new string[6] { "Infos", "InfoDict", "Dialogs", "DialogDict", "Tutorials", "TutorialDict" }, null, null, null, new GeneratedClrTypeInfo[3])
		}));
	}
}
