using System;
using Google.Protobuf.Reflection;

public static class BotReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static BotReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("CglCb3QucHJvdG8iMQoTQm90QWNjb3VudENvbmZpZ3VyZRIKCgJpZBgBIAEo" + "DxIOCgZuYW1lSUQYAiABKAkitQEKDEJvdENvbmZpZ3VyZRImCghBY2NvdW50" + "cxgBIAMoCzIULkJvdEFjY291bnRDb25maWd1cmUSMwoLQWNjb3VudERpY3QY" + "AiADKAsyHi5Cb3RDb25maWd1cmUuQWNjb3VudERpY3RFbnRyeRpIChBBY2Nv" + "dW50RGljdEVudHJ5EgsKA2tleRgBIAEoDxIjCgV2YWx1ZRgCIAEoCzIULkJv" + "dEFjY291bnRDb25maWd1cmU6AjgBYgZwcm90bzM="), new FileDescriptor[0], new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[2]
		{
			new GeneratedClrTypeInfo(typeof(BotAccountConfigure), BotAccountConfigure.Parser, new string[2] { "Id", "NameID" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(BotConfigure), BotConfigure.Parser, new string[2] { "Accounts", "AccountDict" }, null, null, null, new GeneratedClrTypeInfo[1])
		}));
	}
}
