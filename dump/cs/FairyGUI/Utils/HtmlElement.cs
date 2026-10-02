using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FairyGUI.Utils;

public class HtmlElement
{
	public HtmlElementType type;

	public string name;

	public string text;

	public TextFormat format;

	public int charIndex;

	public IHtmlObject htmlObject;

	public int status;

	public int space;

	public Vector2 position;

	private Hashtable attributes;

	private static Stack<HtmlElement> elementPool = new Stack<HtmlElement>();

	public bool isEntity
	{
		get
		{
			if (type != HtmlElementType.Image && type != HtmlElementType.Select && type != HtmlElementType.Input)
			{
				return type == HtmlElementType.Object;
			}
			return true;
		}
	}

	public HtmlElement()
	{
		format = new TextFormat();
	}

	public object Get(string attrName)
	{
		if (attributes == null)
		{
			return null;
		}
		return attributes[attrName];
	}

	public void Set(string attrName, object attrValue)
	{
		if (attributes == null)
		{
			attributes = new Hashtable();
		}
		attributes[attrName] = attrValue;
	}

	public string GetString(string attrName)
	{
		return GetString(attrName, null);
	}

	public string GetString(string attrName, string defValue)
	{
		if (attributes == null)
		{
			return defValue;
		}
		object obj = attributes[attrName];
		if (obj != null)
		{
			return obj.ToString();
		}
		return defValue;
	}

	public int GetInt(string attrName)
	{
		return GetInt(attrName, 0);
	}

	public int GetInt(string attrName, int defValue)
	{
		string text = GetString(attrName);
		if (text == null || text.Length == 0)
		{
			return defValue;
		}
		if (text[text.Length - 1] == '%')
		{
			if (int.TryParse(text.Substring(0, text.Length - 1), out var result))
			{
				return Mathf.CeilToInt((float)result / 100f * (float)defValue);
			}
			return defValue;
		}
		if (int.TryParse(text, out var result2))
		{
			return result2;
		}
		return defValue;
	}

	public float GetFloat(string attrName)
	{
		return GetFloat(attrName, 0f);
	}

	public float GetFloat(string attrName, float defValue)
	{
		string text = GetString(attrName);
		if (text == null || text.Length == 0)
		{
			return defValue;
		}
		if (float.TryParse(text, out var result))
		{
			return result;
		}
		return defValue;
	}

	public bool GetBool(string attrName)
	{
		return GetBool(attrName, defValue: false);
	}

	public bool GetBool(string attrName, bool defValue)
	{
		string text = GetString(attrName);
		if (text == null || text.Length == 0)
		{
			return defValue;
		}
		if (bool.TryParse(text, out var result))
		{
			return result;
		}
		return defValue;
	}

	public Color GetColor(string attrName, Color defValue)
	{
		string text = GetString(attrName);
		if (text == null || text.Length == 0)
		{
			return defValue;
		}
		return ToolSet.ConvertFromHtmlColor(text);
	}

	public void FetchAttributes()
	{
		attributes = XMLIterator.GetAttributes(attributes);
	}

	public static HtmlElement GetElement(HtmlElementType type)
	{
		HtmlElement htmlElement = ((elementPool.Count <= 0) ? new HtmlElement() : elementPool.Pop());
		htmlElement.type = type;
		if (type != HtmlElementType.Text && htmlElement.attributes == null)
		{
			htmlElement.attributes = new Hashtable();
		}
		return htmlElement;
	}

	public static void ReturnElement(HtmlElement element)
	{
		element.name = null;
		element.text = null;
		element.htmlObject = null;
		element.status = 0;
		if (element.attributes != null)
		{
			element.attributes.Clear();
		}
		elementPool.Push(element);
	}

	public static void ReturnElements(List<HtmlElement> elements)
	{
		int count = elements.Count;
		for (int i = 0; i < count; i++)
		{
			ReturnElement(elements[i]);
		}
		elements.Clear();
	}
}
