using System.Collections.Generic;
using System.Text;

namespace FairyGUI.Utils;

public class UBBParser
{
	public delegate string TagHandler(string tagName, bool end, string attr);

	public static UBBParser inst = new UBBParser();

	private string _text;

	private int _readPos;

	public TagHandler defaultTagHandler;

	public Dictionary<string, TagHandler> handlers;

	public int defaultImgWidth;

	public int defaultImgHeight;

	public UBBParser()
	{
		handlers = new Dictionary<string, TagHandler>();
		handlers["url"] = onTag_URL;
		handlers["img"] = onTag_IMG;
		handlers["b"] = onTag_Simple;
		handlers["i"] = onTag_Simple;
		handlers["u"] = onTag_Simple;
		handlers["sup"] = onTag_Simple;
		handlers["sub"] = onTag_Simple;
		handlers["color"] = onTag_COLOR;
		handlers["font"] = onTag_FONT;
		handlers["size"] = onTag_SIZE;
		handlers["align"] = onTag_ALIGN;
		handlers["strike"] = onTag_Simple;
	}

	protected string onTag_URL(string tagName, bool end, string attr)
	{
		if (!end)
		{
			if (attr != null)
			{
				return "<a href=\"" + attr + "\" target=\"_blank\">";
			}
			string tagText = GetTagText(remove: false);
			return "<a href=\"" + tagText + "\" target=\"_blank\">";
		}
		return "</a>";
	}

	protected string onTag_IMG(string tagName, bool end, string attr)
	{
		if (!end)
		{
			string tagText = GetTagText(remove: true);
			if (tagText == null || tagText.Length == 0)
			{
				return null;
			}
			if (defaultImgWidth != 0)
			{
				return "<img src=\"" + tagText + "\" width=\"" + defaultImgWidth + "\" height=\"" + defaultImgHeight + "\"/>";
			}
			return "<img src=\"" + tagText + "\"/>";
		}
		return null;
	}

	protected string onTag_Simple(string tagName, bool end, string attr)
	{
		if (!end)
		{
			return "<" + tagName + ">";
		}
		return "</" + tagName + ">";
	}

	protected string onTag_COLOR(string tagName, bool end, string attr)
	{
		if (!end)
		{
			return "<font color=\"" + attr + "\">";
		}
		return "</font>";
	}

	protected string onTag_FONT(string tagName, bool end, string attr)
	{
		if (!end)
		{
			return "<font face=\"" + attr + "\">";
		}
		return "</font>";
	}

	protected string onTag_SIZE(string tagName, bool end, string attr)
	{
		if (!end)
		{
			return "<font size=\"" + attr + "\">";
		}
		return "</font>";
	}

	protected string onTag_ALIGN(string tagName, bool end, string attr)
	{
		if (!end)
		{
			return "<p align=\"" + attr + "\">";
		}
		return "</p>";
	}

	public string GetTagText(bool remove)
	{
		int num = _readPos;
		StringBuilder stringBuilder = null;
		int num2;
		while ((num2 = _text.IndexOf('[', num)) != -1)
		{
			if (stringBuilder == null)
			{
				stringBuilder = new StringBuilder();
			}
			if (_text[num2 - 1] == '\\')
			{
				stringBuilder.Append(_text, num, num2 - num - 1);
				stringBuilder.Append('[');
				num = num2 + 1;
				continue;
			}
			stringBuilder.Append(_text, num, num2 - num);
			break;
		}
		if (num2 == -1)
		{
			return null;
		}
		if (remove)
		{
			_readPos = num2;
		}
		return stringBuilder.ToString();
	}

	public string Parse(string text)
	{
		_text = text;
		int num = 0;
		StringBuilder stringBuilder = null;
		int num2;
		while ((num2 = _text.IndexOf('[', num)) != -1)
		{
			if (stringBuilder == null)
			{
				stringBuilder = new StringBuilder();
			}
			if (num2 > 0 && _text[num2 - 1] == '\\')
			{
				stringBuilder.Append(_text, num, num2 - num - 1);
				stringBuilder.Append('[');
				num = num2 + 1;
				continue;
			}
			stringBuilder.Append(_text, num, num2 - num);
			num = num2;
			num2 = _text.IndexOf(']', num);
			if (num2 == -1)
			{
				break;
			}
			if (num2 == num + 1)
			{
				stringBuilder.Append(_text, num, 2);
				num = num2 + 1;
				continue;
			}
			bool flag = _text[num + 1] == '/';
			int num3 = (flag ? (num + 2) : (num + 1));
			string text2 = _text.Substring(num3, num2 - num3);
			_readPos = num2 + 1;
			string attr = null;
			string text3 = null;
			num3 = text2.IndexOf('=');
			if (num3 != -1)
			{
				attr = text2.Substring(num3 + 1);
				text2 = text2.Substring(0, num3);
			}
			text2 = text2.ToLower();
			if (handlers.TryGetValue(text2, out var value))
			{
				text3 = value(text2, flag, attr);
				if (text3 != null)
				{
					stringBuilder.Append(text3);
				}
			}
			else if (defaultTagHandler != null)
			{
				text3 = defaultTagHandler(text2, flag, attr);
				if (text3 != null)
				{
					stringBuilder.Append(text3);
				}
				else
				{
					stringBuilder.Append(_text, num, num2 - num + 1);
				}
			}
			else
			{
				stringBuilder.Append(_text, num, num2 - num + 1);
			}
			num = _readPos;
		}
		if (stringBuilder == null)
		{
			_text = null;
			return text;
		}
		if (num < _text.Length)
		{
			stringBuilder.Append(_text, num, _text.Length - num);
		}
		_text = null;
		return stringBuilder.ToString();
	}
}
