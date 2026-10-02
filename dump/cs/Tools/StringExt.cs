using System.Text;
using System.Text.RegularExpressions;

namespace Tools;

public static class StringExt
{
	public static bool BetterStartsWith(this string a, string b)
	{
		int length = a.Length;
		int length2 = b.Length;
		int num = 0;
		int num2 = 0;
		while (num < length && num2 < length2 && a[num] == b[num2])
		{
			num++;
			num2++;
		}
		if (num2 == length2)
		{
			return length >= length2;
		}
		return false;
	}

	public static bool BetterEndsWith(this string a, string b)
	{
		int num = a.Length - 1;
		int num2 = b.Length - 1;
		while (num >= 0 && num2 >= 0 && a[num] == b[num2])
		{
			num--;
			num2--;
		}
		if (num2 < 0)
		{
			return a.Length >= b.Length;
		}
		return false;
	}

	public static int RealLength(this string str)
	{
		if (str == null)
		{
			return 0;
		}
		char[] array = str.ToCharArray();
		int num = 0;
		char[] array2 = array;
		foreach (char num2 in array2)
		{
			num++;
			if (num2 / 128 != 0)
			{
				num++;
			}
		}
		return num;
	}

	public static bool IsNumeric(this string str)
	{
		foreach (char c in str)
		{
			if (c < '0' || c > '9')
			{
				return false;
			}
		}
		return true;
	}

	public static bool HasEscapeChar(this string str)
	{
		if (!string.IsNullOrEmpty(str) && (str.Contains("\n") || str.Contains("\r") || str.Contains("\t") || str.Contains("\v")))
		{
			return true;
		}
		return false;
	}

	public static string CutText(this string srcText, int maxLength)
	{
		int num = srcText.RealLength();
		int num2 = srcText.Length - (num - maxLength);
		if (num2 <= 0)
		{
			return srcText;
		}
		return srcText.Substring(0, num2);
	}

	public static string TryCutSQLIllegalChars(this string text)
	{
		string pattern = "[^a-zA-Z0-9~!@#$%^&*()_+-={};:,./<>?！@#￥%……&*（）——+【】{}；：‘’“”，。《》？\\u4e00-\\u9fa5\\u3040-\\u309f\\u30a0-\\u30ff\\uac00-\\ud7af}]";
		return Regex.Replace(text, pattern, "*");
	}

	public static string ToHexString(this byte[] bytes)
	{
		StringBuilder stringBuilder = new StringBuilder();
		for (int i = 0; i < bytes.Length; i++)
		{
			stringBuilder.Append($"{bytes[i]:X2}");
		}
		return stringBuilder.ToString().Trim();
	}

	public static int GetPlaceholderCount(this string str)
	{
		return Regex.Matches(str, "\\{(\\d+)\\}").Count;
	}
}
