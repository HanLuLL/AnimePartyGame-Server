using System;
using Google.Protobuf.Reflection;

public static class STRChoosingTimeLimitReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static STRChoosingTimeLimitReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("ChpTVFJDaG9vc2luZ1RpbWVMaW1pdC5wcm90byKMAQoiU1RSQ2hvb3NpbmdU" + "aW1lTGltaXRMb2NhbENvbmZpZ3VyZRIKCgJpZBgBIAEoDxISCgpzaW1wbGlm" + "aWVkGAIgASgJEg8KB2VuZ2xpc2gYAyABKAkSEAoIamFwYW5lc2UYBCABKAkS" + "EwoLdHJhZGl0aW9uYWwYBSABKAkSDgoGa29yZWFuGAYgASgJIu0BCh1TVFJD" + "aG9vc2luZ1RpbWVMaW1pdENvbmZpZ3VyZRIzCgZMb2NhbHMYASADKAsyIy5T" + "VFJDaG9vc2luZ1RpbWVMaW1pdExvY2FsQ29uZmlndXJlEkAKCUxvY2FsRGlj" + "dBgCIAMoCzItLlNUUkNob29zaW5nVGltZUxpbWl0Q29uZmlndXJlLkxvY2Fs" + "RGljdEVudHJ5GlUKDkxvY2FsRGljdEVudHJ5EgsKA2tleRgBIAEoDxIyCgV2" + "YWx1ZRgCIAEoCzIjLlNUUkNob29zaW5nVGltZUxpbWl0TG9jYWxDb25maWd1" + "cmU6AjgBYgZwcm90bzM="), new FileDescriptor[0], new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[2]
		{
			new GeneratedClrTypeInfo(typeof(STRChoosingTimeLimitLocalConfigure), STRChoosingTimeLimitLocalConfigure.Parser, new string[6] { "Id", "Simplified", "English", "Japanese", "Traditional", "Korean" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(STRChoosingTimeLimitConfigure), STRChoosingTimeLimitConfigure.Parser, new string[2] { "Locals", "LocalDict" }, null, null, null, new GeneratedClrTypeInfo[1])
		}));
	}
}
