using System.Collections;
using System.Text;

namespace Tools;

public static class DFAAlgorithm
{
	private static Hashtable hashtable;

	public static void Init(int length)
	{
		hashtable = new Hashtable(length);
	}

	public static bool IsValid()
	{
		return hashtable != null;
	}

	public static void Clear()
	{
		hashtable = null;
	}

	public static void AddFilterWord(string word)
	{
		if (DFAAlgorithm.hashtable == null)
		{
			return;
		}
		Hashtable hashtable = DFAAlgorithm.hashtable;
		for (int i = 0; i < word.Length; i++)
		{
			char c = word[i];
			if (hashtable.ContainsKey(c))
			{
				hashtable = (Hashtable)hashtable[c];
			}
			else
			{
				Hashtable hashtable2 = new Hashtable();
				hashtable2.Add("IsEnd", 0);
				hashtable.Add(c, hashtable2);
				hashtable = hashtable2;
			}
			if (i == word.Length - 1)
			{
				if (hashtable.ContainsKey("IsEnd"))
				{
					hashtable["IsEnd"] = 1;
				}
				else
				{
					hashtable.Add("IsEnd", 1);
				}
			}
		}
	}

	public static string StringCheckAndReplace(string targetStr)
	{
		if (hashtable == null)
		{
			return targetStr;
		}
		StringBuilder stringBuilder = new StringBuilder(targetStr);
		int num = 0;
		int num2 = 0;
		while (num2 < targetStr.Length)
		{
			num = SensitiveWordsLength(targetStr, num2);
			if (num == 0)
			{
				num2++;
				continue;
			}
			for (int i = 0; i < num; i++)
			{
				stringBuilder[num2 + i] = '*';
			}
			num2 += num;
		}
		return stringBuilder.ToString();
	}

	public static bool Valid(string targetStr)
	{
		for (int i = 0; i < targetStr.Length; i++)
		{
			if (SensitiveWordsLength(targetStr, i) != 0)
			{
				return false;
			}
		}
		return true;
	}

	public static int SensitiveWordsLength(string targetStr, int beginIndex)
	{
		Hashtable hashtable = DFAAlgorithm.hashtable;
		int result = 0;
		for (int i = beginIndex; i < targetStr.Length; i++)
		{
			char c = targetStr[i];
			if ((c <= ' ' || c >= '0') && (c <= '9' || c >= 'A') && (c <= 'z' || c >= '~'))
			{
				Hashtable hashtable2 = (Hashtable)hashtable[c];
				if (hashtable2 == null)
				{
					break;
				}
				if ((int)hashtable2["IsEnd"] == 1)
				{
					result = i + 1 - beginIndex;
				}
				else
				{
					hashtable = hashtable2;
				}
			}
		}
		return result;
	}
}
