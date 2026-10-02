using System;
using Google.Protobuf.Reflection;

public static class AudioReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static AudioReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("CgtBdWRpby5wcm90byJDChJBdWRpb0JhbmtDb25maWd1cmUSCgoCaWQYASAB" + "KA8SDwoHbG9hZEtleRgCIAEoCRIQCghiYW5rTmFtZRgDIAEoCSJWChNBdWRp" + "b0V2ZW50Q29uZmlndXJlEgoKAmlkGAEgASgPEhEKCWV2ZW50TmFtZRgCIAEo" + "CRIPCgdldmVudElEGAMgASgHEg8KB2lzQmxvY2sYBCABKAgizAIKDkF1ZGlv" + "Q29uZmlndXJlEiIKBUJhbmtzGAEgAygLMhMuQXVkaW9CYW5rQ29uZmlndXJl" + "Ei8KCEJhbmtEaWN0GAIgAygLMh0uQXVkaW9Db25maWd1cmUuQmFua0RpY3RF" + "bnRyeRIkCgZFdmVudHMYAyADKAsyFC5BdWRpb0V2ZW50Q29uZmlndXJlEjEK" + "CUV2ZW50RGljdBgEIAMoCzIeLkF1ZGlvQ29uZmlndXJlLkV2ZW50RGljdEVu" + "dHJ5GkQKDUJhbmtEaWN0RW50cnkSCwoDa2V5GAEgASgPEiIKBXZhbHVlGAIg" + "ASgLMhMuQXVkaW9CYW5rQ29uZmlndXJlOgI4ARpGCg5FdmVudERpY3RFbnRy" + "eRILCgNrZXkYASABKA8SIwoFdmFsdWUYAiABKAsyFC5BdWRpb0V2ZW50Q29u" + "ZmlndXJlOgI4AWIGcHJvdG8z"), new FileDescriptor[0], new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[3]
		{
			new GeneratedClrTypeInfo(typeof(AudioBankConfigure), AudioBankConfigure.Parser, new string[3] { "Id", "LoadKey", "BankName" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(AudioEventConfigure), AudioEventConfigure.Parser, new string[4] { "Id", "EventName", "EventID", "IsBlock" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(AudioConfigure), AudioConfigure.Parser, new string[4] { "Banks", "BankDict", "Events", "EventDict" }, null, null, null, new GeneratedClrTypeInfo[2])
		}));
	}
}
