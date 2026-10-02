using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace FairyGUI.Utils;

public class XML
{
	public string name;

	public string text;

	private Dictionary<string, string> _attributes;

	private XMLList _children;

	private static Stack<XML> sNodeStack = new Stack<XML>();

	public Dictionary<string, string> attributes
	{
		get
		{
			if (_attributes == null)
			{
				_attributes = new Dictionary<string, string>();
			}
			return _attributes;
		}
	}

	public XMLList elements
	{
		get
		{
			if (_children == null)
			{
				_children = new XMLList();
			}
			return _children;
		}
	}

	public static XML Create(string tag)
	{
		return new XML
		{
			name = tag
		};
	}

	public XML(string XmlString)
	{
		Parse(XmlString);
	}

	private XML()
	{
	}

	public bool HasAttribute(string attrName)
	{
		if (_attributes == null)
		{
			return false;
		}
		return _attributes.ContainsKey(attrName);
	}

	public string GetAttribute(string attrName)
	{
		return GetAttribute(attrName, null);
	}

	public string GetAttribute(string attrName, string defValue)
	{
		if (_attributes == null)
		{
			return defValue;
		}
		if (_attributes.TryGetValue(attrName, out var value))
		{
			return value;
		}
		return defValue;
	}

	public int GetAttributeInt(string attrName)
	{
		return GetAttributeInt(attrName, 0);
	}

	public int GetAttributeInt(string attrName, int defValue)
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

	public float GetAttributeFloat(string attrName)
	{
		return GetAttributeFloat(attrName, 0f);
	}

	public float GetAttributeFloat(string attrName, float defValue)
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

	public bool GetAttributeBool(string attrName)
	{
		return GetAttributeBool(attrName, defValue: false);
	}

	public bool GetAttributeBool(string attrName, bool defValue)
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

	public string[] GetAttributeArray(string attrName)
	{
		string attribute = GetAttribute(attrName);
		if (attribute != null)
		{
			if (attribute.Length == 0)
			{
				return new string[0];
			}
			return attribute.Split(',');
		}
		return null;
	}

	public string[] GetAttributeArray(string attrName, char seperator)
	{
		string attribute = GetAttribute(attrName);
		if (attribute != null)
		{
			if (attribute.Length == 0)
			{
				return new string[0];
			}
			return attribute.Split(seperator);
		}
		return null;
	}

	public Color GetAttributeColor(string attrName, Color defValue)
	{
		string attribute = GetAttribute(attrName);
		if (attribute == null || attribute.Length == 0)
		{
			return defValue;
		}
		return ToolSet.ConvertFromHtmlColor(attribute);
	}

	public Vector2 GetAttributeVector(string attrName)
	{
		string attribute = GetAttribute(attrName);
		if (attribute != null)
		{
			string[] array = attribute.Split(',');
			return new Vector2(float.Parse(array[0]), float.Parse(array[1]));
		}
		return Vector2.zero;
	}

	public void SetAttribute(string attrName, string attrValue)
	{
		if (_attributes == null)
		{
			_attributes = new Dictionary<string, string>();
		}
		_attributes[attrName] = attrValue;
	}

	public void SetAttribute(string attrName, bool attrValue)
	{
		if (_attributes == null)
		{
			_attributes = new Dictionary<string, string>();
		}
		_attributes[attrName] = (attrValue ? "true" : "false");
	}

	public void SetAttribute(string attrName, int attrValue)
	{
		if (_attributes == null)
		{
			_attributes = new Dictionary<string, string>();
		}
		_attributes[attrName] = attrValue.ToString();
	}

	public void SetAttribute(string attrName, float attrValue)
	{
		if (_attributes == null)
		{
			_attributes = new Dictionary<string, string>();
		}
		_attributes[attrName] = $"{attrValue:#.####}";
	}

	public void RemoveAttribute(string attrName)
	{
		if (_attributes != null)
		{
			_attributes.Remove(attrName);
		}
	}

	public XML GetNode(string selector)
	{
		if (_children == null)
		{
			return null;
		}
		return _children.Find(selector);
	}

	public XMLList Elements()
	{
		if (_children == null)
		{
			_children = new XMLList();
		}
		return _children;
	}

	public XMLList Elements(string selector)
	{
		if (_children == null)
		{
			_children = new XMLList();
		}
		return _children.Filter(selector);
	}

	public XMLList.Enumerator GetEnumerator()
	{
		if (_children == null)
		{
			return new XMLList.Enumerator(null, null);
		}
		return new XMLList.Enumerator(_children.rawList, null);
	}

	public XMLList.Enumerator GetEnumerator(string selector)
	{
		if (_children == null)
		{
			return new XMLList.Enumerator(null, selector);
		}
		return new XMLList.Enumerator(_children.rawList, selector);
	}

	public void AppendChild(XML child)
	{
		elements.Add(child);
	}

	public void RemoveChild(XML child)
	{
		if (_children != null)
		{
			_children.rawList.Remove(child);
		}
	}

	public void RemoveChildren(string selector)
	{
		if (_children != null)
		{
			if (string.IsNullOrEmpty(selector))
			{
				_children.Clear();
			}
			else
			{
				_children.RemoveAll(selector);
			}
		}
	}

	public void Parse(string aSource)
	{
		Reset();
		XML xML = null;
		sNodeStack.Clear();
		XMLIterator.Begin(aSource);
		while (XMLIterator.NextTag())
		{
			if (XMLIterator.tagType == XMLTagType.Start || XMLIterator.tagType == XMLTagType.Void)
			{
				XML xML2;
				if (xML != null)
				{
					xML2 = new XML();
				}
				else
				{
					if (name != null)
					{
						Reset();
						throw new Exception("Invalid xml format - no root node.");
					}
					xML2 = this;
				}
				xML2.name = XMLIterator.tagName;
				xML2._attributes = XMLIterator.GetAttributes(xML2._attributes);
				if (xML != null)
				{
					if (XMLIterator.tagType != XMLTagType.Void)
					{
						sNodeStack.Push(xML);
					}
					if (xML._children == null)
					{
						xML._children = new XMLList();
					}
					xML._children.Add(xML2);
				}
				if (XMLIterator.tagType != XMLTagType.Void)
				{
					xML = xML2;
				}
			}
			else if (XMLIterator.tagType == XMLTagType.End)
			{
				if (xML == null || xML.name != XMLIterator.tagName)
				{
					Reset();
					throw new Exception("Invalid xml format - <" + XMLIterator.tagName + "> dismatched.");
				}
				if (xML._children == null || xML._children.Count == 0)
				{
					xML.text = XMLIterator.GetText();
				}
				xML = ((sNodeStack.Count <= 0) ? null : sNodeStack.Pop());
			}
		}
	}

	public void Reset()
	{
		if (_attributes != null)
		{
			_attributes.Clear();
		}
		if (_children != null)
		{
			_children.Clear();
		}
		text = null;
	}

	public string ToXMLString(bool includeHeader)
	{
		StringBuilder stringBuilder = new StringBuilder();
		if (includeHeader)
		{
			stringBuilder.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>\n");
		}
		ToXMLString(stringBuilder, 0);
		return stringBuilder.ToString();
	}

	private void ToXMLString(StringBuilder sb, int tabs)
	{
		if (tabs > 0)
		{
			sb.Append(' ', tabs * 2);
		}
		if (name == "!")
		{
			sb.Append("<!--");
			if (text != null)
			{
				int length = sb.Length;
				sb.Append(text);
				XMLUtils.EncodeString(sb, length);
			}
			sb.Append("-->");
			return;
		}
		sb.Append('<').Append(name);
		if (_attributes != null)
		{
			foreach (KeyValuePair<string, string> attribute in _attributes)
			{
				sb.Append(' ');
				sb.Append(attribute.Key).Append('=').Append('"');
				int length2 = sb.Length;
				sb.Append(attribute.Value);
				XMLUtils.EncodeString(sb, length2, encodeQuotes: true);
				sb.Append("\"");
			}
		}
		int num = ((_children != null) ? _children.Count : 0);
		if (string.IsNullOrEmpty(text) && num == 0)
		{
			sb.Append("/>");
			return;
		}
		sb.Append('>');
		if (!string.IsNullOrEmpty(text))
		{
			int length3 = sb.Length;
			sb.Append(text);
			XMLUtils.EncodeString(sb, length3);
		}
		if (num > 0)
		{
			sb.Append('\n');
			int tabs2 = tabs + 1;
			for (int i = 0; i < num; i++)
			{
				_children[i].ToXMLString(sb, tabs2);
				sb.Append('\n');
			}
			if (tabs > 0)
			{
				sb.Append(' ', tabs * 2);
			}
		}
		sb.Append("</").Append(name).Append(">");
	}
}
