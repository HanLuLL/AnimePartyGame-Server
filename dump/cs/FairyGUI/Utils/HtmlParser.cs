using System.Collections.Generic;
using UnityEngine;

namespace FairyGUI.Utils;

public class HtmlParser
{
	protected class TextFormat2 : TextFormat
	{
		public bool colorChanged;
	}

	public static HtmlParser inst = new HtmlParser();

	protected List<TextFormat2> _textFormatStack;

	protected int _textFormatStackTop;

	protected TextFormat2 _format;

	protected List<HtmlElement> _elements;

	protected HtmlParseOptions _defaultOptions;

	private static List<string> sHelperList1 = new List<string>();

	private static List<string> sHelperList2 = new List<string>();

	public HtmlParser()
	{
		_textFormatStack = new List<TextFormat2>();
		_format = new TextFormat2();
		_defaultOptions = new HtmlParseOptions();
	}

	public virtual void Parse(string aSource, TextFormat defaultFormat, List<HtmlElement> elements, HtmlParseOptions parseOptions)
	{
		if (parseOptions == null)
		{
			parseOptions = _defaultOptions;
		}
		_elements = elements;
		_textFormatStackTop = 0;
		_format.CopyFrom(defaultFormat);
		_format.colorChanged = false;
		int num = 0;
		bool trim = parseOptions.ignoreWhiteSpace;
		bool flag = false;
		XMLIterator.Begin(aSource, lowerCaseName: true);
		while (XMLIterator.NextTag())
		{
			if (num == 0)
			{
				string text = XMLIterator.GetText(trim);
				if (text.Length > 0)
				{
					if (flag && text[0] == '\n')
					{
						text = text.Substring(1);
					}
					AppendText(text);
				}
			}
			flag = false;
			switch (XMLIterator.tagName)
			{
			case "b":
				if (XMLIterator.tagType == XMLTagType.Start)
				{
					PushTextFormat();
					_format.bold = true;
				}
				else
				{
					PopTextFormat();
				}
				break;
			case "i":
				if (XMLIterator.tagType == XMLTagType.Start)
				{
					PushTextFormat();
					_format.italic = true;
				}
				else
				{
					PopTextFormat();
				}
				break;
			case "u":
				if (XMLIterator.tagType == XMLTagType.Start)
				{
					PushTextFormat();
					_format.underline = true;
				}
				else
				{
					PopTextFormat();
				}
				break;
			case "strike":
				if (XMLIterator.tagType == XMLTagType.Start)
				{
					PushTextFormat();
					_format.strikethrough = true;
				}
				else
				{
					PopTextFormat();
				}
				break;
			case "sub":
				if (XMLIterator.tagType == XMLTagType.Start)
				{
					PushTextFormat();
					_format.specialStyle = TextFormat.SpecialStyle.Subscript;
				}
				else
				{
					PopTextFormat();
				}
				break;
			case "sup":
				if (XMLIterator.tagType == XMLTagType.Start)
				{
					PushTextFormat();
					_format.specialStyle = TextFormat.SpecialStyle.Superscript;
				}
				else
				{
					PopTextFormat();
				}
				break;
			case "font":
				if (XMLIterator.tagType == XMLTagType.Start)
				{
					PushTextFormat();
					_format.size = XMLIterator.GetAttributeInt("size", _format.size);
					string attribute = XMLIterator.GetAttribute("color");
					if (attribute == null)
					{
						break;
					}
					string[] array = attribute.Split(',');
					if (array.Length == 1)
					{
						_format.color = ToolSet.ConvertFromHtmlColor(attribute);
						_format.gradientColor = null;
						_format.colorChanged = true;
						break;
					}
					if (_format.gradientColor == null)
					{
						_format.gradientColor = new Color32[4];
					}
					_format.gradientColor[0] = ToolSet.ConvertFromHtmlColor(array[0]);
					_format.gradientColor[1] = ToolSet.ConvertFromHtmlColor(array[1]);
					if (array.Length > 2)
					{
						_format.gradientColor[2] = ToolSet.ConvertFromHtmlColor(array[2]);
						if (array.Length > 3)
						{
							_format.gradientColor[3] = ToolSet.ConvertFromHtmlColor(array[3]);
						}
						else
						{
							_format.gradientColor[3] = _format.gradientColor[2];
						}
					}
					else
					{
						_format.gradientColor[2] = _format.gradientColor[0];
						_format.gradientColor[3] = _format.gradientColor[1];
					}
				}
				else if (XMLIterator.tagType == XMLTagType.End)
				{
					PopTextFormat();
				}
				break;
			case "br":
				AppendText("\n");
				break;
			case "img":
				if (XMLIterator.tagType == XMLTagType.Start || XMLIterator.tagType == XMLTagType.Void)
				{
					HtmlElement element3 = HtmlElement.GetElement(HtmlElementType.Image);
					element3.FetchAttributes();
					element3.name = element3.GetString("name");
					element3.format.align = _format.align;
					_elements.Add(element3);
				}
				break;
			case "a":
				if (XMLIterator.tagType == XMLTagType.Start)
				{
					PushTextFormat();
					_format.underline = _format.underline || parseOptions.linkUnderline;
					if (!_format.colorChanged && parseOptions.linkColor.a != 0f)
					{
						_format.color = parseOptions.linkColor;
					}
					HtmlElement element4 = HtmlElement.GetElement(HtmlElementType.Link);
					element4.FetchAttributes();
					element4.name = element4.GetString("name");
					element4.format.align = _format.align;
					_elements.Add(element4);
				}
				else if (XMLIterator.tagType == XMLTagType.End)
				{
					PopTextFormat();
					HtmlElement element5 = HtmlElement.GetElement(HtmlElementType.LinkEnd);
					_elements.Add(element5);
				}
				break;
			case "input":
			{
				HtmlElement element2 = HtmlElement.GetElement(HtmlElementType.Input);
				element2.FetchAttributes();
				element2.name = element2.GetString("name");
				element2.format.CopyFrom(_format);
				_elements.Add(element2);
				break;
			}
			case "select":
			{
				if (XMLIterator.tagType != XMLTagType.Start && XMLIterator.tagType != XMLTagType.Void)
				{
					break;
				}
				HtmlElement element = HtmlElement.GetElement(HtmlElementType.Select);
				element.FetchAttributes();
				if (XMLIterator.tagType == XMLTagType.Start)
				{
					sHelperList1.Clear();
					sHelperList2.Clear();
					while (XMLIterator.NextTag() && !(XMLIterator.tagName == "select"))
					{
						if (XMLIterator.tagName == "option")
						{
							if (XMLIterator.tagType == XMLTagType.Start || XMLIterator.tagType == XMLTagType.Void)
							{
								sHelperList2.Add(XMLIterator.GetAttribute("value", string.Empty));
							}
							else
							{
								sHelperList1.Add(XMLIterator.GetText());
							}
						}
					}
					element.Set("items", sHelperList1.ToArray());
					element.Set("values", sHelperList2.ToArray());
				}
				element.name = element.GetString("name");
				element.format.CopyFrom(_format);
				_elements.Add(element);
				break;
			}
			case "p":
				if (XMLIterator.tagType == XMLTagType.Start)
				{
					PushTextFormat();
					string attribute2 = XMLIterator.GetAttribute("align");
					if (!(attribute2 == "center"))
					{
						if (attribute2 == "right")
						{
							_format.align = AlignType.Right;
						}
					}
					else
					{
						_format.align = AlignType.Center;
					}
					if (!IsNewLine())
					{
						AppendText("\n");
					}
				}
				else if (XMLIterator.tagType == XMLTagType.End)
				{
					AppendText("\n");
					flag = true;
					PopTextFormat();
				}
				break;
			case "ui":
			case "div":
			case "li":
				if (XMLIterator.tagType == XMLTagType.Start)
				{
					if (!IsNewLine())
					{
						AppendText("\n");
					}
				}
				else
				{
					AppendText("\n");
					flag = true;
				}
				break;
			case "html":
			case "body":
				trim = true;
				break;
			case "head":
			case "style":
			case "script":
			case "form":
				if (XMLIterator.tagType == XMLTagType.Start)
				{
					num++;
				}
				else if (XMLIterator.tagType == XMLTagType.End)
				{
					num--;
				}
				break;
			}
		}
		if (num == 0)
		{
			string text = XMLIterator.GetText(trim);
			if (text.Length > 0)
			{
				if (flag && text[0] == '\n')
				{
					text = text.Substring(1);
				}
				AppendText(text);
			}
		}
		_elements = null;
	}

	protected void PushTextFormat()
	{
		TextFormat2 textFormat;
		if (_textFormatStack.Count <= _textFormatStackTop)
		{
			textFormat = new TextFormat2();
			_textFormatStack.Add(textFormat);
		}
		else
		{
			textFormat = _textFormatStack[_textFormatStackTop];
		}
		textFormat.CopyFrom(_format);
		textFormat.colorChanged = _format.colorChanged;
		_textFormatStackTop++;
	}

	protected void PopTextFormat()
	{
		if (_textFormatStackTop > 0)
		{
			TextFormat2 textFormat = _textFormatStack[_textFormatStackTop - 1];
			_format.CopyFrom(textFormat);
			_format.colorChanged = textFormat.colorChanged;
			_textFormatStackTop--;
		}
	}

	protected bool IsNewLine()
	{
		if (_elements.Count > 0)
		{
			HtmlElement htmlElement = _elements[_elements.Count - 1];
			if (htmlElement != null && htmlElement.type == HtmlElementType.Text)
			{
				return htmlElement.text.EndsWith("\n");
			}
			return false;
		}
		return true;
	}

	protected void AppendText(string text)
	{
		HtmlElement htmlElement;
		if (_elements.Count > 0)
		{
			htmlElement = _elements[_elements.Count - 1];
			if (htmlElement.type == HtmlElementType.Text && htmlElement.format.EqualStyle(_format))
			{
				htmlElement.text += text;
				return;
			}
		}
		htmlElement = HtmlElement.GetElement(HtmlElementType.Text);
		htmlElement.text = text;
		htmlElement.format.CopyFrom(_format);
		_elements.Add(htmlElement);
	}
}
