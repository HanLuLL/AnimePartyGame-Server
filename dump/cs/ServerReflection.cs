using System;
using Google.Protobuf.Reflection;

public static class ServerReflection
{
	private static FileDescriptor descriptor;

	public static FileDescriptor Descriptor => descriptor;

	static ServerReflection()
	{
		descriptor = FileDescriptor.FromGeneratedCode(Convert.FromBase64String("CgxTZXJ2ZXIucHJvdG8aCkVudW0ucHJvdG8imAEKFFNlcnZlckVycm9yQ29u" + "ZmlndXJlEgoKAmlkGAEgASgPEjEKE3NlcnZlckVycm9yRGVhbFR5cGUYAiAB" + "KA4yFC5TZXJ2ZXJFcnJvckRlYWxUeXBlEjEKE3NlcnZlckVycm9yU2hvd1R5" + "cGUYAyABKA4yFC5TZXJ2ZXJFcnJvclNob3dUeXBlEg4KBmRlc2NJZBgEIAEo" + "DyK1AQoPU2VydmVyQ29uZmlndXJlEiUKBkVycm9ycxgBIAMoCzIVLlNlcnZl" + "ckVycm9yQ29uZmlndXJlEjIKCUVycm9yRGljdBgCIAMoCzIfLlNlcnZlckNv" + "bmZpZ3VyZS5FcnJvckRpY3RFbnRyeRpHCg5FcnJvckRpY3RFbnRyeRILCgNr" + "ZXkYASABKA8SJAoFdmFsdWUYAiABKAsyFS5TZXJ2ZXJFcnJvckNvbmZpZ3Vy" + "ZToCOAFiBnByb3RvMw=="), new FileDescriptor[1] { EnumReflection.Descriptor }, new GeneratedClrTypeInfo(null, null, new GeneratedClrTypeInfo[2]
		{
			new GeneratedClrTypeInfo(typeof(ServerErrorConfigure), ServerErrorConfigure.Parser, new string[4] { "Id", "ServerErrorDealType", "ServerErrorShowType", "DescId" }, null, null, null, null),
			new GeneratedClrTypeInfo(typeof(ServerConfigure), ServerConfigure.Parser, new string[2] { "Errors", "ErrorDict" }, null, null, null, new GeneratedClrTypeInfo[1])
		}));
	}
}
