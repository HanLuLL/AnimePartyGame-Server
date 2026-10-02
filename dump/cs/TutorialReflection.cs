using System;
using Google.Protobuf.Reflection;

public static class TutorialReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static TutorialReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("Cg5UdXRvcmlhbC5wcm90byJjChVUdXRvcmlhbGluZm9Db25maWd1cmUSCgoC" + "aWQYASABKA8SPgoadHV0b3JpYWxpbmZvQ29uZmlndXJlSXRlbXMYAiADKAsy" + "Gi5UdXRvcmlhbGluZm9Db25maWd1cmVJdGVtImEKGVR1dG9yaWFsaW5mb0Nv" + "bmZpZ3VyZUl0ZW0SDQoFaW5kZXgYASABKA8SDQoFbXNnSUQYAiABKA8SEwoL" + "bXNnSURNb2JpbGUYAyABKA8SEQoJaW1hZ2VOYW1lGAQgASgPIrYBChdUdXRv" + "cmlhbGRpYWxvZ0NvbmZpZ3VyZRIKCgJpZBgBIAEoDxIYChBzdGFuZGluZ1Bh" + "aW50aW5nGAIgASgJEhsKE3Nmd1N0YW5kaW5nUGFpbnRpbmcYAyABKAkSFAoM" + "cHJvZmlsZVBob3RvGAQgASgJEkIKHHR1dG9yaWFsZGlhbG9nQ29uZmlndXJl" + "SXRlbXMYBSADKAsyHC5UdXRvcmlhbGRpYWxvZ0NvbmZpZ3VyZUl0ZW0iZAob" + "VHV0b3JpYWxkaWFsb2dDb25maWd1cmVJdGVtEg0KBWluZGV4GAEgASgPEhIK" + "CmV4cHJlc3Npb24YAiABKAkSEQoJY29udGVudElkGAMgASgPEg8KB2F1ZGlv" + "SWQYBCABKA8iSAoWVHV0b3JpYWxwb3B1cENvbmZpZ3VyZRIKCgJpZBgBIAEo" + "DxIPCgd1aUluZGV4GAIgASgPEhEKCWNvbnRlbnRJZBgDIAEoDyKRBAoRVHV0" + "b3JpYWxDb25maWd1cmUSJQoFaW5mb3MYASADKAsyFi5UdXRvcmlhbGluZm9D" + "b25maWd1cmUSMgoIaW5mb0RpY3QYAiADKAsyIC5UdXRvcmlhbENvbmZpZ3Vy" + "ZS5JbmZvRGljdEVudHJ5EikKB2RpYWxvZ3MYAyADKAsyGC5UdXRvcmlhbGRp" + "YWxvZ0NvbmZpZ3VyZRI2CgpkaWFsb2dEaWN0GAQgAygLMiIuVHV0b3JpYWxD" + "b25maWd1cmUuRGlhbG9nRGljdEVudHJ5EicKBnBvcHVwcxgFIAMoCzIXLlR1" + "dG9yaWFscG9wdXBDb25maWd1cmUSNAoJcG9wdXBEaWN0GAYgAygLMiEuVHV0" + "b3JpYWxDb25maWd1cmUuUG9wdXBEaWN0RW50cnkaRwoNSW5mb0RpY3RFbnRy" + "eRILCgNrZXkYASABKA8SJQoFdmFsdWUYAiABKAsyFi5UdXRvcmlhbGluZm9D" + "b25maWd1cmU6AjgBGksKD0RpYWxvZ0RpY3RFbnRyeRILCgNrZXkYASABKA8S" + "JwoFdmFsdWUYAiABKAsyGC5UdXRvcmlhbGRpYWxvZ0NvbmZpZ3VyZToCOAEa" + "SQoOUG9wdXBEaWN0RW50cnkSCwoDa2V5GAEgASgPEiYKBXZhbHVlGAIgASgL" + "MhcuVHV0b3JpYWxwb3B1cENvbmZpZ3VyZToCOAFiBnByb3RvMw=="), new FileDescriptor[0], new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[6]
		{
			new GeneratedClrTypeInfo(typeof(TutorialinfoConfigure), TutorialinfoConfigure.Parser, new string[2] { "Id", "TutorialinfoConfigureItems" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(TutorialinfoConfigureItem), TutorialinfoConfigureItem.Parser, new string[4] { "Index", "MsgID", "MsgIDMobile", "ImageName" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(TutorialdialogConfigure), TutorialdialogConfigure.Parser, new string[5] { "Id", "StandingPainting", "SfwStandingPainting", "ProfilePhoto", "TutorialdialogConfigureItems" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(TutorialdialogConfigureItem), TutorialdialogConfigureItem.Parser, new string[4] { "Index", "Expression", "ContentId", "AudioId" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(TutorialpopupConfigure), TutorialpopupConfigure.Parser, new string[3] { "Id", "UiIndex", "ContentId" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(TutorialConfigure), TutorialConfigure.Parser, new string[6] { "Infos", "InfoDict", "Dialogs", "DialogDict", "Popups", "PopupDict" }, null, null, null, new GeneratedClrTypeInfo[3])
		}));
	}
}
