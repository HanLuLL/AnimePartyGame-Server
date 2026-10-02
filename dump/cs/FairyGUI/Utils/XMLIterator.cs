using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace FairyGUI.Utils;

public class XMLIterator
{
	public static string tagName;

	public static XMLTagType tagType;

	public static string lastTagName;

	private static string source;

	private static int sourceLen;

	private static int parsePos;

	private static int tagPos;

	private static int tagLength;

	private static int lastTagEnd;

	private static bool attrParsed;

	private static bool lowerCaseName;

	private static StringBuilder buffer = new StringBuilder();

	private static Dictionary<string, string> attributes = new Dictionary<string, string>();

	private const string CDATA_START = "<![CDATA[";

	private const string CDATA_END = "]]>";

	private const string COMMENT_START = "<!--";

	private const string COMMENT_END = "-->";

	public static void Begin(string source, bool lowerCaseName = false)
	{
		XMLIterator.source = source;
		XMLIterator.lowerCaseName = lowerCaseName;
		sourceLen = source.Length;
		parsePos = 0;
		lastTagEnd = 0;
		tagPos = 0;
		tagLength = 0;
		tagName = null;
	}

	public static bool NextTag()
	{
		tagType = XMLTagType.Start;
		buffer.Length = 0;
		lastTagEnd = parsePos;
		attrParsed = false;
		lastTagName = tagName;
		int num;
		if ((num = source.IndexOf('<', parsePos)) != -1)
		{
			parsePos = num;
			num++;
			if (num != sourceLen)
			{
				switch (source[num])
				{
				case '!':
					if (sourceLen > num + 7 && source.Substring(num - 1, 9) == "<![CDATA[")
					{
						num = source.IndexOf("]]>", num);
						tagType = XMLTagType.CDATA;
						tagName = string.Empty;
						tagPos = parsePos;
						if (num == -1)
						{
							tagLength = sourceLen - parsePos;
						}
						else
						{
							tagLength = num + 3 - parsePos;
						}
						parsePos += tagLength;
						return true;
					}
					if (sourceLen > num + 2 && source.Substring(num - 1, 4) == "<!--")
					{
						num = source.IndexOf("-->", num);
						tagType = XMLTagType.Comment;
						tagName = string.Empty;
						tagPos = parsePos;
						if (num == -1)
						{
							tagLength = sourceLen - parsePos;
						}
						else
						{
							tagLength = num + 3 - parsePos;
						}
						parsePos += tagLength;
						return true;
					}
					num++;
					tagType = XMLTagType.Instruction;
					break;
				case '/':
					num++;
					tagType = XMLTagType.End;
					break;
				case '?':
					num++;
					tagType = XMLTagType.Instruction;
					break;
				}
				for (; num < sourceLen; num++)
				{
					char c = source[num];
					if (char.IsWhiteSpace(c) || c == '>' || c == '/')
					{
						break;
					}
				}
				if (num != sourceLen)
				{
					buffer.Append(source, parsePos + 1, num - parsePos - 1);
					if (buffer.Length > 0 && buffer[0] == '/')
					{
						buffer.Remove(0, 1);
					}
					bool flag = false;
					bool flag2 = false;
					int num2 = -1;
					for (; num < sourceLen; num++)
					{
						char c = source[num];
						switch (c)
						{
						case '"':
							if (!flag)
							{
								flag2 = !flag2;
							}
							break;
						case '\'':
							if (!flag2)
							{
								flag = !flag;
							}
							break;
						}
						switch (c)
						{
						case '>':
							if (!(flag || flag2))
							{
								num2 = -1;
								break;
							}
							num2 = num;
							continue;
						default:
							continue;
						case '<':
							break;
						}
						break;
					}
					if (num2 != -1)
					{
						num = num2;
					}
					if (num != sourceLen)
					{
						if (source[num - 1] == '/')
						{
							tagType = XMLTagType.Void;
						}
						tagName = buffer.ToString();
						if (lowerCaseName)
						{
							tagName = tagName.ToLower();
						}
						tagPos = parsePos;
						tagLength = num + 1 - parsePos;
						parsePos += tagLength;
						return true;
					}
				}
			}
		}
		tagPos = sourceLen;
		tagLength = 0;
		tagName = null;
		return false;
	}

	public static string GetTagSource()
	{
		return source.Substring(tagPos, tagLength);
	}

	public static string GetRawText(bool trim = false)
	{
		if (lastTagEnd == tagPos)
		{
			return string.Empty;
		}
		if (trim)
		{
			int i;
			for (i = lastTagEnd; i < tagPos && char.IsWhiteSpace(source[i]); i++)
			{
			}
			if (i == tagPos)
			{
				return string.Empty;
			}
			return source.Substring(i, tagPos - i).TrimEnd();
		}
		return source.Substring(lastTagEnd, tagPos - lastTagEnd);
	}

	public static string GetText(bool trim = false)
	{
		if (lastTagEnd == tagPos)
		{
			return string.Empty;
		}
		if (trim)
		{
			int i;
			for (i = lastTagEnd; i < tagPos && char.IsWhiteSpace(source[i]); i++)
			{
			}
			if (i == tagPos)
			{
				return string.Empty;
			}
			return XMLUtils.DecodeString(source.Substring(i, tagPos - i).TrimEnd());
		}
		return XMLUtils.DecodeString(source.Substring(lastTagEnd, tagPos - lastTagEnd));
	}

	public static bool HasAttribute(string attrName)
	{
		if (!attrParsed)
		{
			attributes.Clear();
			ParseAttributes(attributes);
			attrParsed = true;
		}
		return attributes.ContainsKey(attrName);
	}

	public static string GetAttribute(string attrName)
	{
		if (!attrParsed)
		{
			attributes.Clear();
			ParseAttributes(attributes);
			attrParsed = true;
		}
		if (attributes.TryGetValue(attrName, out var value))
		{
			return value;
		}
		return null;
	}

	public static string GetAttribute(string attrName, string defValue)
	{
		string attribute = GetAttribute(attrName);
		if (attribute != null)
		{
			return attribute;
		}
		return defValue;
	}

	public static int GetAttributeInt(string attrName)
	{
		return GetAttributeInt(attrName, 0);
	}

	public static int GetAttributeInt(string attrName, int defValue)
	{
		string attribute = GetAttribute(attrName);
		if (attribute == null || attribute.Length == 0)
		{
			return defValue;
		}
		if (int.TryParse(attribute, out var result))
		{
			return result;
		}
		return defValue;
	}

	public static float GetAttributeFloat(string attrName)
	{
		return GetAttributeFloat(attrName, 0f);
	}

	public static float GetAttributeFloat(string attrName, float defValue)
	{
		string attribute = GetAttribute(attrName);
		if (attribute == null || attribute.Length == 0)
		{
			return defValue;
		}
		if (float.TryParse(attribute, out var result))
		{
			return result;
		}
		return defValue;
	}

	public static bool GetAttributeBool(string attrName)
	{
		return GetAttributeBool(attrName, defValue: false);
	}

	public static bool GetAttributeBool(string attrName, bool defValue)
	{
		string attribute = GetAttribute(attrName);
		if (attribute == null || attribute.Length == 0)
		{
			return defValue;
		}
		if (bool.TryParse(attribute, out var result))
		{
			return result;
		}
		return defValue;
	}

	public static Dictionary<string, string> GetAttributes(Dictionary<string, string> result)
	{
		if (result == null)
		{
			result = new Dictionary<string, string>();
		}
		if (attrParsed)
		{
			foreach (KeyValuePair<string, string> attribute in attributes)
			{
				result[attribute.Key] = attribute.Value;
			}
		}
		else
		{
			ParseAttributes(result);
		}
		return result;
	}

	public static Hashtable GetAttributes(Hashtable result)
	{
		if (result == null)
		{
			result = new Hashtable();
		}
		if (attrParsed)
		{
			foreach (KeyValuePair<string, string> attribute in attributes)
			{
				result[attribute.Key] = attribute.Value;
			}
		}
		else
		{
			ParseAttributes(result);
		}
		return result;
	}

	private static void ParseAttributes(IDictionary attrs)
	{
		bool flag = false;
		buffer.Length = 0;
		int i = tagPos;
		int num = tagPos + tagLength;
		if (i < num && source[i] == '<')
		{
			for (; i < num; i++)
			{
				char c = source[i];
				if (char.IsWhiteSpace(c) || c == '>' || c == '/')
				{
					break;
				}
			}
		}
		for (; i < num; i++)
		{
			char c2 = source[i];
			if (c2 == '=')
			{
				int num2 = -1;
				int num3 = -1;
				int num4 = 0;
				for (int j = i + 1; j < num; j++)
				{
					char c3 = source[j];
					if (char.IsWhiteSpace(c3))
					{
						if (num2 != -1 && num4 == 0)
						{
							num3 = j - 1;
							break;
						}
						continue;
					}
					switch (c3)
					{
					case '>':
						if (num4 != 0)
						{
							continue;
						}
						num3 = j - 1;
						break;
					case '"':
						if (num2 != -1)
						{
							if (num4 == 1)
							{
								continue;
							}
							num3 = j - 1;
							break;
						}
						num4 = 2;
						num2 = j + 1;
						continue;
					case '\'':
						if (num2 != -1)
						{
							if (num4 == 2)
							{
								continue;
							}
							num3 = j - 1;
							break;
						}
						num4 = 1;
						num2 = j + 1;
						continue;
					default:
						if (num2 == -1)
						{
							num2 = j;
						}
						continue;
					}
					break;
				}
				if (num2 == -1 || num3 == -1)
				{
					break;
				}
				string text = buffer.ToString();
				if (lowerCaseName)
				{
					text = text.ToLower();
				}
				buffer.Length = 0;
				attrs[text] = XMLUtils.DecodeString(source.Substring(num2, num3 - num2 + 1));
				i = num3 + 1;
			}
			else if (!char.IsWhiteSpace(c2))
			{
				if (flag || c2 == '/' || c2 == '>')
				{
					if (buffer.Length > 0)
					{
						string text = buffer.ToString();
						if (lowerCaseName)
						{
							text = text.ToLower();
						}
						attrs[text] = string.Empty;
						buffer.Length = 0;
					}
					flag = false;
				}
				if (c2 != '/' && c2 != '>')
				{
					buffer.Append(c2);
				}
			}
			else if (buffer.Length > 0)
			{
				flag = true;
			}
		}
	}
}
