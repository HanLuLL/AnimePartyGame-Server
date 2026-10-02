using System.Text;

public static class StringExtensions
{
	public static string GetSubString(this string originalString, int maxLength, out int originalLength)
	{
		originalLength = 0;
		if (string.IsNullOrEmpty(originalString))
		{
			return originalString;
		}
		int num = 0;
		StringBuilder stringBuilder = new StringBuilder();
		foreach (char c in originalString)
		{
			if (!char.IsSurrogate(c))
			{
				if (char.IsUpper(c) || char.IsLower(c) || char.IsDigit(c) || char.IsPunctuation(c))
				{
					originalLength++;
				}
				else
				{
					originalLength += 2;
				}
				if (originalLength > maxLength)
				{
					break;
				}
				stringBuilder.Append(c);
				num++;
			}
		}
		return stringBuilder.ToString();
	}

	public static bool IsEnglishOrPunctuation(this char c)
	{
		if (!char.IsUpper(c) && !char.IsLower(c) && !char.IsDigit(c))
		{
			return char.IsPunctuation(c);
		}
		return true;
	}

	public static int GetStringLength(this string originalString)
	{
		int num = 0;
		foreach (char c in originalString)
		{
			if (!char.IsSurrogate(c))
			{
				num = ((!c.IsEnglishOrPunctuation()) ? (num + 2) : (num + 1));
			}
		}
		return num;
	}
}
