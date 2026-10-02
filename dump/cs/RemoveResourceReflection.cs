using System;
using Google.Protobuf.Reflection;

public static class RemoveResourceReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static RemoveResourceReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("ChRSZW1vdmVSZXNvdXJjZS5wcm90byI5ChxSZW1vdmVSZXNvdXJjZUltYWdl" + "Q29uZmlndXJlEgoKAmlkGAEgASgPEg0KBWltYWdlGAIgASgJIjkKHFJlbW92" + "ZVJlc291cmNlVmlkZW9Db25maWd1cmUSCgoCaWQYASABKA8SDQoFdmlkZW8Y" + "AiABKAkiOwocUmVtb3ZlUmVzb3VyY2VBdWRpb0NvbmZpZ3VyZRIKCgJpZBgB" + "IAEoDxIPCgdsb2FkS2V5GAIgASgJIs0EChdSZW1vdmVSZXNvdXJjZUNvbmZp" + "Z3VyZRItCgZJbWFnZXMYASADKAsyHS5SZW1vdmVSZXNvdXJjZUltYWdlQ29u" + "ZmlndXJlEjoKCUltYWdlRGljdBgCIAMoCzInLlJlbW92ZVJlc291cmNlQ29u" + "ZmlndXJlLkltYWdlRGljdEVudHJ5Ei0KBlZpZGVvcxgDIAMoCzIdLlJlbW92" + "ZVJlc291cmNlVmlkZW9Db25maWd1cmUSOgoJVmlkZW9EaWN0GAQgAygLMicu" + "UmVtb3ZlUmVzb3VyY2VDb25maWd1cmUuVmlkZW9EaWN0RW50cnkSLQoGQXVk" + "aW9zGAUgAygLMh0uUmVtb3ZlUmVzb3VyY2VBdWRpb0NvbmZpZ3VyZRI6CglB" + "dWRpb0RpY3QYBiADKAsyJy5SZW1vdmVSZXNvdXJjZUNvbmZpZ3VyZS5BdWRp" + "b0RpY3RFbnRyeRpPCg5JbWFnZURpY3RFbnRyeRILCgNrZXkYASABKA8SLAoF" + "dmFsdWUYAiABKAsyHS5SZW1vdmVSZXNvdXJjZUltYWdlQ29uZmlndXJlOgI4" + "ARpPCg5WaWRlb0RpY3RFbnRyeRILCgNrZXkYASABKA8SLAoFdmFsdWUYAiAB" + "KAsyHS5SZW1vdmVSZXNvdXJjZVZpZGVvQ29uZmlndXJlOgI4ARpPCg5BdWRp" + "b0RpY3RFbnRyeRILCgNrZXkYASABKA8SLAoFdmFsdWUYAiABKAsyHS5SZW1v" + "dmVSZXNvdXJjZUF1ZGlvQ29uZmlndXJlOgI4AWIGcHJvdG8z"), new FileDescriptor[0], new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[4]
		{
			new GeneratedClrTypeInfo(typeof(RemoveResourceImageConfigure), RemoveResourceImageConfigure.Parser, new string[2] { "Id", "Image" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(RemoveResourceVideoConfigure), RemoveResourceVideoConfigure.Parser, new string[2] { "Id", "Video" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(RemoveResourceAudioConfigure), RemoveResourceAudioConfigure.Parser, new string[2] { "Id", "LoadKey" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(RemoveResourceConfigure), RemoveResourceConfigure.Parser, new string[6] { "Images", "ImageDict", "Videos", "VideoDict", "Audios", "AudioDict" }, null, null, null, new GeneratedClrTypeInfo[3])
		}));
	}
}
