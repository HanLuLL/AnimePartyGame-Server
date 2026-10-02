using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Tools;

public static class ContactService
{
	private const string Key = "cmdrcodvppwqpsz34pe9qnnnkgfbs52g";

	public static string CreateCustomerServiceSign(Dictionary<string, string> parameters)
	{
		return CreateSign(new Dictionary<string, string>(parameters) { ["sign"] = CreateSign(parameters) });
	}

	private static string CreateSign(Dictionary<string, string> parameters)
	{
		IEnumerable<string> enumerable = parameters.Keys.OrderBy((string k) => k, StringComparer.Ordinal).Reverse();
		StringBuilder stringBuilder = new StringBuilder();
		foreach (string item in enumerable)
		{
			stringBuilder.Append(item).Append('=').Append(parameters[item])
				.Append('&');
		}
		stringBuilder.Append(GetMd5("cmdrcodvppwqpsz34pe9qnnnkgfbs52g"));
		return GetMd5(stringBuilder.ToString());
	}

	private static string GetMd5(string input)
	{
		using MD5 mD = MD5.Create();
		byte[] bytes = Encoding.UTF8.GetBytes(input);
		byte[] array = mD.ComputeHash(bytes);
		StringBuilder stringBuilder = new StringBuilder();
		byte[] array2 = array;
		foreach (byte b in array2)
		{
			stringBuilder.Append(b.ToString("x2"));
		}
		return stringBuilder.ToString();
	}
}
