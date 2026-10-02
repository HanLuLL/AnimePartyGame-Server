using System.Collections.Generic;
using System.Text;

namespace FairyGUI;

public class RTLSupport
{
	internal enum CharState
	{
		isolated,
		final,
		lead,
		middle
	}

	public enum DirectionType
	{
		UNKNOW,
		LTR,
		RTL,
		NEUTRAL
	}

	public static DirectionType BaseDirection = DirectionType.UNKNOW;

	private static bool isCharsInitialized = false;

	private static Dictionary<int, char[]> mapping = new Dictionary<int, char[]>();

	private static List<char> listFinal = new List<char>();

	private static List<string> listRep = new List<string>();

	private static StringBuilder sbRep = new StringBuilder();

	private static StringBuilder sbN = new StringBuilder();

	private static StringBuilder sbFinal = new StringBuilder();

	private static StringBuilder sbReverse = new StringBuilder();

	public static bool IsArabicLetter(char ch)
	{
		if (ch >= '\u0600' && ch <= 'ۿ')
		{
			if ((ch >= '٠' && ch <= '٭') || (ch >= '۰' && ch <= '۹'))
			{
				return false;
			}
			return true;
		}
		if (ch >= 'ݐ' && ch <= 'ݿ')
		{
			return true;
		}
		if (ch >= 'ﭐ' && ch <= 'ﰿ')
		{
			return true;
		}
		if (ch >= 'ﹰ' && ch <= 'ﻼ')
		{
			return true;
		}
		return false;
	}

	public static string ConvertNumber(string strNumber)
	{
		sbRep.Length = 0;
		foreach (char c in strNumber)
		{
			int num = c;
			if (num != 1644 && c != ',')
			{
				switch (num)
				{
				case 1632:
				case 1776:
					sbRep.Append('0');
					break;
				case 1633:
				case 1777:
					sbRep.Append('1');
					break;
				case 1634:
				case 1778:
					sbRep.Append('2');
					break;
				case 1635:
				case 1779:
					sbRep.Append('3');
					break;
				case 1636:
				case 1780:
					sbRep.Append('4');
					break;
				case 1637:
				case 1781:
					sbRep.Append('5');
					break;
				case 1638:
				case 1782:
					sbRep.Append('6');
					break;
				case 1639:
				case 1783:
					sbRep.Append('7');
					break;
				case 1640:
				case 1784:
					sbRep.Append('8');
					break;
				case 1641:
				case 1785:
					sbRep.Append('9');
					break;
				default:
					sbRep.Append(c);
					break;
				}
			}
		}
		return sbRep.ToString();
	}

	public static bool ContainsArabicLetters(string text)
	{
		foreach (char c in text)
		{
			if (c >= '\u0600' && c <= 'ۿ')
			{
				return true;
			}
			if (c >= 'ݐ' && c <= 'ݿ')
			{
				return true;
			}
			if (c >= 'ﭐ' && c <= 'ﰿ')
			{
				return true;
			}
			if (c >= 'ﹰ' && c <= 'ﻼ')
			{
				return true;
			}
		}
		return false;
	}

	public static DirectionType DetectTextDirection(string text)
	{
		bool flag = false;
		bool flag2 = false;
		foreach (char c in text)
		{
			if (IsArabicLetter(c))
			{
				flag = true;
				if (flag2)
				{
					break;
				}
			}
			else if (char.IsLetter(c))
			{
				flag2 = true;
				if (flag)
				{
					break;
				}
			}
		}
		if (!flag)
		{
			return DirectionType.UNKNOW;
		}
		if (!flag2)
		{
			return DirectionType.RTL;
		}
		return BaseDirection;
	}

	private static bool CheckSeparator(char input)
	{
		if (!IsArabicLetter(input))
		{
			return true;
		}
		if (input != '،' && input != '?')
		{
			return input == '؟';
		}
		return true;
	}

	private static bool CheckSpecific(char input)
	{
		if (input != 'آ' && input != 'أ' && input != 'ا' && input != 'د' && input != 'إ' && input != 'ذ' && input != 'ر' && input != 'ز' && input != 'ژ' && input != 'و' && !_CheckSoundmark(input))
		{
			return false;
		}
		return true;
	}

	private static bool _CheckSoundmark(char ch)
	{
		if (ch < '\u0610' || ch > '؞')
		{
			if (ch >= '\u064b')
			{
				return ch <= '\u065f';
			}
			return false;
		}
		return true;
	}

	public static string DoMapping(string input)
	{
		if (!isCharsInitialized)
		{
			isCharsInitialized = true;
			InitChars();
		}
		if (input == "الله")
		{
			input = "ﷲ";
		}
		sbFinal.Length = 0;
		sbFinal.Append(input);
		char input2 = '\0';
		for (int i = 0; i < sbFinal.Length; i++)
		{
			if (!mapping.ContainsKey(sbFinal[i]))
			{
				input2 = sbFinal[i];
			}
			else if (i + 1 == sbFinal.Length)
			{
				if (sbFinal.Length == 1)
				{
					input2 = sbFinal[i];
					sbFinal[i] = mapping[sbFinal[i]][0];
				}
				else if (CheckSeparator(input2) || CheckSpecific(input2))
				{
					input2 = sbFinal[i];
					sbFinal[i] = mapping[sbFinal[i]][0];
				}
				else
				{
					input2 = sbFinal[i];
					sbFinal[i] = mapping[sbFinal[i]][1];
				}
			}
			else if (i == 0)
			{
				if (!CheckSeparator(sbFinal[i + 1]))
				{
					input2 = sbFinal[i];
					sbFinal[i] = mapping[sbFinal[i]][2];
				}
				else
				{
					input2 = sbFinal[i];
					sbFinal[i] = mapping[sbFinal[i]][0];
				}
			}
			else if (CheckSeparator(sbFinal[i + 1]))
			{
				if (CheckSeparator(input2) || CheckSpecific(input2))
				{
					input2 = sbFinal[i];
					sbFinal[i] = mapping[sbFinal[i]][0];
				}
				else
				{
					input2 = sbFinal[i];
					sbFinal[i] = mapping[sbFinal[i]][1];
				}
			}
			else if (CheckSeparator(input2))
			{
				if (CheckSeparator(sbFinal[i + 1]))
				{
					input2 = sbFinal[i];
					sbFinal[i] = mapping[sbFinal[i]][0];
				}
				else
				{
					input2 = sbFinal[i];
					sbFinal[i] = mapping[sbFinal[i]][2];
				}
			}
			else if (CheckSpecific(sbFinal[i + 1]))
			{
				if (CheckSeparator(input2) || CheckSpecific(input2))
				{
					input2 = sbFinal[i];
					sbFinal[i] = mapping[sbFinal[i]][2];
				}
				else
				{
					input2 = sbFinal[i];
					sbFinal[i] = mapping[sbFinal[i]][3];
				}
			}
			else if (CheckSpecific(input2))
			{
				if (CheckSeparator(sbFinal[i + 1]))
				{
					input2 = sbFinal[i];
					sbFinal[i] = mapping[sbFinal[i]][0];
				}
				else
				{
					input2 = sbFinal[i];
					sbFinal[i] = mapping[sbFinal[i]][2];
				}
			}
			else
			{
				input2 = sbFinal[i];
				sbFinal[i] = mapping[sbFinal[i]][3];
			}
		}
		return sbFinal.ToString();
	}

	public static string ConvertLineL(string source)
	{
		listFinal.Clear();
		listRep.Clear();
		sbRep.Length = 0;
		sbN.Length = 0;
		int num = 0;
		DirectionType ePre = DirectionType.LTR;
		char c = '\0';
		for (int i = 0; i < source.Length; i++)
		{
			c = ((i < source.Length - 1) ? source[i + 1] : '\0');
			char c2 = source[i];
			DirectionType directionType = _GetDirection(c2, c, ePre, DirectionType.LTR);
			switch (directionType)
			{
			case DirectionType.RTL:
				if (sbRep.Length == 0)
				{
					listFinal.Add('¿');
					num++;
				}
				if (sbN.Length > 0)
				{
					sbRep.Append(sbN.ToString());
				}
				sbN.Length = 0;
				sbRep.Append(c2);
				break;
			case DirectionType.LTR:
				if (sbRep.Length > 0)
				{
					listRep.Add(sbRep.ToString());
				}
				sbRep.Length = 0;
				if (sbN.Length > 0)
				{
					for (int j = 0; j < sbN.Length; j++)
					{
						listFinal.Add(sbN[j]);
					}
				}
				sbN.Length = 0;
				listFinal.Add(c2);
				break;
			default:
				sbN.Append(c2);
				break;
			}
			ePre = directionType;
		}
		if (sbRep.Length > 0)
		{
			listRep.Add(sbRep.ToString());
		}
		if (sbN.Length > 0)
		{
			for (int k = 0; k < sbN.Length; k++)
			{
				listFinal.Add(sbN[k]);
			}
		}
		sbRep.Length = 0;
		sbN.Length = 0;
		sbFinal.Length = 0;
		sbFinal.Append(listFinal.ToArray());
		for (int l = 0; l < num; l++)
		{
			for (int m = 0; m < sbFinal.Length; m++)
			{
				if (sbFinal[m] != '¿')
				{
					continue;
				}
				sbRep.Length = 0;
				sbRep.Append(_Reverse(listRep[0]));
				listRep.RemoveAt(0);
				sbN.Length = 0;
				for (int n = 0; n < sbRep.Length && _IsNeutrality(sbRep[n]); n++)
				{
					sbN.Append(sbRep[n]);
				}
				if (sbN.Length > 0)
				{
					sbRep.Remove(0, sbN.Length);
					for (int num2 = sbN.Length - 1; num2 >= 0; num2--)
					{
						sbRep.Append(sbN[num2]);
					}
				}
				sbFinal.Replace(sbFinal[m].ToString(), sbRep.ToString(), m, 1);
				break;
			}
		}
		return sbFinal.ToString();
	}

	public static string ConvertLineR(string source)
	{
		listFinal.Clear();
		listRep.Clear();
		sbRep.Length = 0;
		sbN.Length = 0;
		int num = 0;
		DirectionType ePre = DirectionType.RTL;
		char c = '\0';
		for (int i = 0; i < source.Length; i++)
		{
			c = ((i < source.Length - 1) ? source[i + 1] : '\0');
			char c2 = source[i];
			DirectionType directionType = _GetDirection(c2, c, ePre, DirectionType.RTL);
			switch (directionType)
			{
			case DirectionType.LTR:
				if (sbRep.Length == 0)
				{
					listFinal.Add('¿');
					num++;
				}
				if (sbN.Length > 0)
				{
					sbRep.Append(sbN.ToString());
				}
				sbN.Length = 0;
				sbRep.Append(c2);
				break;
			case DirectionType.RTL:
				if (sbRep.Length > 0)
				{
					listRep.Add(sbRep.ToString());
				}
				sbRep.Length = 0;
				if (sbN.Length > 0)
				{
					for (int j = 0; j < sbN.Length; j++)
					{
						listFinal.Add(sbN[j]);
					}
				}
				sbN.Length = 0;
				c2 = _ProcessBracket(c2);
				listFinal.Add(c2);
				break;
			default:
				sbN.Append(c2);
				break;
			}
			ePre = directionType;
		}
		if (sbRep.Length > 0)
		{
			listRep.Add(sbRep.ToString());
		}
		if (sbN.Length > 0)
		{
			for (int k = 0; k < sbN.Length; k++)
			{
				listFinal.Add(sbN[k]);
			}
		}
		sbFinal.Length = 0;
		sbFinal.Append(listFinal.ToArray());
		for (int l = 0; l < num; l++)
		{
			for (int m = 0; m < sbFinal.Length; m++)
			{
				if (sbFinal[m] != '¿')
				{
					continue;
				}
				sbRep.Length = 0;
				sbRep.Append(_Reverse(listRep[0]));
				listRep.RemoveAt(0);
				sbN.Length = 0;
				for (int n = 0; n < sbRep.Length && _IsNeutrality(sbRep[n]); n++)
				{
					sbN.Append(sbRep[n]);
				}
				if (sbN.Length > 0)
				{
					sbRep.Remove(0, sbN.Length);
					for (int num2 = sbN.Length - 1; num2 >= 0; num2--)
					{
						sbRep.Append(sbN[num2]);
					}
				}
				sbFinal.Replace(sbFinal[m].ToString(), sbRep.ToString(), m, 1);
				break;
			}
		}
		return sbFinal.ToString();
	}

	private static string _Reverse(string source)
	{
		sbReverse.Length = 0;
		int length = source.Length;
		int num = length - 1;
		while (num >= 0)
		{
			char c = source[num];
			if (c == '\r' && num != length - 1 && source[num + 1] == '\n')
			{
				num--;
				continue;
			}
			if (char.IsLowSurrogate(c))
			{
				sbReverse.Append(source[num - 1]);
				sbReverse.Append(c);
				num--;
			}
			else
			{
				sbReverse.Append(c);
			}
			num--;
		}
		return sbReverse.ToString();
	}

	private static void InitChars()
	{
		mapping.Add(1569, new char[4] { 'ﺀ', 'ﺊ', 'ﺋ', 'ﺌ' });
		mapping.Add(1575, new char[4] { 'ﺍ', 'ﺎ', 'ﺍ', 'ﺎ' });
		mapping.Add(1571, new char[4] { 'ﺃ', 'ﺄ', 'ﺃ', 'ﺄ' });
		mapping.Add(1572, new char[4] { 'ﺅ', 'ﺅ', 'ﺅ', 'ﺅ' });
		mapping.Add(1573, new char[4] { 'ﺇ', 'ﺇ', 'ﺇ', 'ﺇ' });
		mapping.Add(1609, new char[4] { 'ﯼ', 'ﯽ', 'ﯾ', 'ﯿ' });
		mapping.Add(1574, new char[4] { 'ﺉ', 'ﺊ', 'ﺋ', 'ﺌ' });
		mapping.Add(1576, new char[4] { 'ﺏ', 'ﺐ', 'ﺑ', 'ﺒ' });
		mapping.Add(1578, new char[4] { 'ﺕ', 'ﺖ', 'ﺗ', 'ﺘ' });
		mapping.Add(1579, new char[4] { 'ﺙ', 'ﺚ', 'ﺛ', 'ﺜ' });
		mapping.Add(1580, new char[4] { 'ﺝ', 'ﺞ', 'ﺟ', 'ﺠ' });
		mapping.Add(1581, new char[4] { 'ﺡ', 'ﺢ', 'ﺣ', 'ﺤ' });
		mapping.Add(1582, new char[4] { 'ﺥ', 'ﺦ', 'ﺧ', 'ﺨ' });
		mapping.Add(1583, new char[4] { 'ﺩ', 'ﺪ', 'ﺩ', 'ﺪ' });
		mapping.Add(1584, new char[4] { 'ﺫ', 'ﺬ', 'ﺫ', 'ﺬ' });
		mapping.Add(1585, new char[4] { 'ﺭ', 'ﺮ', 'ﺭ', 'ﺭ' });
		mapping.Add(1586, new char[4] { 'ﺯ', 'ﺰ', 'ﺯ', 'ﺰ' });
		mapping.Add(1587, new char[4] { 'ﺱ', 'ﺲ', 'ﺳ', 'ﺴ' });
		mapping.Add(1588, new char[4] { 'ﺵ', 'ﺶ', 'ﺷ', 'ﺸ' });
		mapping.Add(1589, new char[4] { 'ﺹ', 'ﺺ', 'ﺻ', 'ﺼ' });
		mapping.Add(1590, new char[4] { 'ﺽ', 'ﺾ', 'ﺿ', 'ﻀ' });
		mapping.Add(1591, new char[4] { 'ﻁ', 'ﻂ', 'ﻃ', 'ﻄ' });
		mapping.Add(1592, new char[4] { 'ﻅ', 'ﻆ', 'ﻇ', 'ﻈ' });
		mapping.Add(1593, new char[4] { 'ﻉ', 'ﻊ', 'ﻋ', 'ﻌ' });
		mapping.Add(1594, new char[4] { 'ﻍ', 'ﻎ', 'ﻏ', 'ﻐ' });
		mapping.Add(1601, new char[4] { 'ﻑ', 'ﻒ', 'ﻓ', 'ﻔ' });
		mapping.Add(1602, new char[4] { 'ﻕ', 'ﻖ', 'ﻗ', 'ﻘ' });
		mapping.Add(1603, new char[4] { 'ﻙ', 'ﻚ', 'ﻛ', 'ﻜ' });
		mapping.Add(1604, new char[4] { 'ﻝ', 'ﻞ', 'ﻟ', 'ﻠ' });
		mapping.Add(1605, new char[4] { 'ﻡ', 'ﻢ', 'ﻣ', 'ﻤ' });
		mapping.Add(1606, new char[4] { 'ﻥ', 'ﻦ', 'ﻧ', 'ﻨ' });
		mapping.Add(1607, new char[4] { 'ﻩ', 'ﻪ', 'ﻫ', 'ﻬ' });
		mapping.Add(1608, new char[4] { 'ﻭ', 'ﻮ', 'ﻭ', 'ﻮ' });
		mapping.Add(1610, new char[4] { 'ﻱ', 'ﻲ', 'ﻳ', 'ﻴ' });
		mapping.Add(1570, new char[4] { 'ﺁ', 'ﺁ', 'ﺁ', 'ﺁ' });
		mapping.Add(1577, new char[4] { 'ﺓ', 'ﺔ', 'ﺔ', 'ﺔ' });
		mapping.Add(1662, new char[4] { 'ﭖ', 'ﭗ', 'ﭘ', 'ﭙ' });
		mapping.Add(1670, new char[4] { 'ﭺ', 'ﭻ', 'ﭼ', 'ﭽ' });
		mapping.Add(1688, new char[4] { 'ﮊ', 'ﮋ', 'ﮊ', 'ﮋ' });
		mapping.Add(1711, new char[4] { 'ﮒ', 'ﮓ', 'ﮔ', 'ﮕ' });
		mapping.Add(1705, new char[4] { 'ﮎ', 'ﮏ', 'ﮐ', 'ﮑ' });
		mapping.Add(1726, new char[4] { 'ﻩ', 'ﻪ', 'ﻫ', 'ﻬ' });
		mapping.Add(1740, new char[4] { 'ﯼ', 'ﯽ', 'ﯾ', 'ﯿ' });
	}

	private static bool _IsNeutrality(char uc)
	{
		if (uc != ':' && uc != '：' && uc != ' ' && uc != '\n' && uc != '\r' && uc != '\t' && uc != '@')
		{
			if (uc >= '☀')
			{
				return uc <= '➿';
			}
			return false;
		}
		return true;
	}

	private static bool _IsEndPunctuation(char uc, char nextChar)
	{
		switch (uc)
		{
		case '.':
			return _IsNeutrality(nextChar);
		default:
			return uc == '؟';
		case '!':
		case '?':
		case '،':
		case '。':
		case '！':
			return true;
		}
	}

	private static DirectionType _GetDirection(char uc, char nextChar, DirectionType ePre, DirectionType eBase)
	{
		DirectionType directionType = ePre;
		if (_IsBracket(uc) || _IsEndPunctuation(uc, nextChar))
		{
			return eBase;
		}
		if (uc >= '٠' && uc <= '٭')
		{
			return DirectionType.LTR;
		}
		if (!IsArabicLetter(uc))
		{
			switch (uc)
			{
			case '%':
			case '+':
				break;
			case '-':
				if (char.IsNumber(nextChar))
				{
					return DirectionType.LTR;
				}
				return DirectionType.RTL;
			default:
				if (_IsNeutrality(uc))
				{
					if (ePre == DirectionType.UNKNOW || ePre == DirectionType.NEUTRAL)
					{
						if (char.IsNumber(nextChar))
						{
							return BaseDirection;
						}
						return DirectionType.NEUTRAL;
					}
					return ePre;
				}
				return DirectionType.LTR;
			}
		}
		return DirectionType.RTL;
	}

	private static bool _IsBracket(char uc)
	{
		if (uc != ')' && uc != '(' && uc != '）' && uc != '（' && uc != ']' && uc != '[' && uc != '】' && uc != '【' && uc != '}' && uc != '{' && uc != '》' && uc != '《' && uc != '“' && uc != '”')
		{
			return uc == '"';
		}
		return true;
	}

	private static char _ProcessBracket(char uc)
	{
		switch (uc)
		{
		case '[':
			return ']';
		case ']':
			return '[';
		case '【':
			return '】';
		case '】':
			return '【';
		case '{':
			return '}';
		case '}':
			return '{';
		case '(':
			return ')';
		case ')':
			return '(';
		case '（':
			return '）';
		case '）':
			return '（';
		case '<':
			return '>';
		case '>':
			return '<';
		case '《':
			return '》';
		case '》':
			return '《';
		case '≤':
			return '≥';
		case '≥':
			return '≤';
		case '”':
			return '“';
		default:
			if (uc == '”')
			{
				return '“';
			}
			return uc;
		}
	}
}
