using System;
using Google.Protobuf.Reflection;

public static class DeveloperReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static DeveloperReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("Cg9EZXZlbG9wZXIucHJvdG8iOgobRGV2ZWxvcGVyRGV2ZWxvcGVyQ29uZmln" + "dXJlEgoKAmlkGAEgASgPEg8KB2NvbnRlbnQYAiABKAki2QEKEkRldmVsb3Bl" + "ckNvbmZpZ3VyZRIwCgpEZXZlbG9wZXJzGAEgAygLMhwuRGV2ZWxvcGVyRGV2" + "ZWxvcGVyQ29uZmlndXJlEj0KDURldmVsb3BlckRpY3QYAiADKAsyJi5EZXZl" + "bG9wZXJDb25maWd1cmUuRGV2ZWxvcGVyRGljdEVudHJ5GlIKEkRldmVsb3Bl" + "ckRpY3RFbnRyeRILCgNrZXkYASABKA8SKwoFdmFsdWUYAiABKAsyHC5EZXZl" + "bG9wZXJEZXZlbG9wZXJDb25maWd1cmU6AjgBYgZwcm90bzM="), new FileDescriptor[0], new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[2]
		{
			new GeneratedClrTypeInfo(typeof(DeveloperDeveloperConfigure), DeveloperDeveloperConfigure.Parser, new string[2] { "Id", "Content" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(DeveloperConfigure), DeveloperConfigure.Parser, new string[2] { "Developers", "DeveloperDict" }, null, null, null, new GeneratedClrTypeInfo[1])
		}));
	}
}
