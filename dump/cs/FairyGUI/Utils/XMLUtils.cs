using System;
using System.Text;

namespace FairyGUI.Utils;

public class XMLUtils
{
	private static string[] ESCAPES = new string[10] { "&", "&amp;", "<", "&lt;", ">", "&gt;", "'", "&apos;", "\"", "&quot;" };

	public static string DecodeString(string aSource)
	{
		int length = aSource.Length;
		StringBuilder stringBuilder = new StringBuilder();
		int num = 0;
		int num2 = 0;
		while (true)
		{
			num2 = aSource.IndexOf('&', num);
			if (num2 == -1)
			{
				break;
			}
			stringBuilder.Append(aSource.Substring(num, num2 - num));
			num = num2 + 1;
			num2 = num;
			int num3;
			for (num3 = Math.Min(length, num2 + 10); num2 < num3 && aSource[num2] != ';'; num2++)
			{
			}
			if (num2 < num3 && num2 > num)
			{
				string text = aSource.Substring(num, num2 - num);
				int num4 = 0;
				if (text[0] == '#')
				{
					if (text.Length > 1)
					{
						num4 = ((text[1] != 'x') ? Convert.ToInt16(text.Substring(1)) : Convert.ToInt16(text.Substring(2), 16));
						stringBuilder.Append((char)num4);
						num = num2 + 1;
					}
					else
					{
						stringBuilder.Append('&');
					}
					continue;
				}
				switch (text)
				{
				case "amp":
					num4 = 38;
					break;
				case "apos":
					num4 = 39;
					break;
				case "gt":
					num4 = 62;
					break;
				case "lt":
					num4 = 60;
					break;
				case "nbsp":
					num4 = 32;
					break;
				case "quot":
					num4 = 34;
					break;
				}
				if (num4 > 0)
				{
					stringBuilder.Append((char)num4);
					num = num2 + 1;
				}
				else
				{
					stringBuilder.Append('&');
				}
			}
			else
			{
				stringBuilder.Append('&');
			}
		}
		stringBuilder.Append(aSource.Substring(num));
		return stringBuilder.ToString();
	}

	public static void EncodeString(StringBuilder sb, int start, bool encodeQuotes = false)
	{
		int num = (encodeQuotes ? ESCAPES.Length : (ESCAPES.Length - 4));
		for (int i = 0; i < num; i += 2)
		{
			int count = sb.Length - start;
			sb.Replace(ESCAPES[i], ESCAPES[i + 1], start, count);
		}
	}

	public static string EncodeString(string str, bool encodeQuotes = false)
	{
		if (string.IsNullOrEmpty(str))
		{
			return "";
		}
		StringBuilder stringBuilder = new StringBuilder(str);
		EncodeString(stringBuilder, 0);
		return stringBuilder.ToString();
	}
}
