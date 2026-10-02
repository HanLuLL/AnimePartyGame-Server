using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace FairyGUI;

public class FontManager
{
	public static Dictionary<string, BaseFont> sFontFactory = new Dictionary<string, BaseFont>();

	public static void RegisterFont(BaseFont font, string alias = null)
	{
		sFontFactory[font.name] = font;
		if (alias != null)
		{
			sFontFactory[alias] = font;
		}
	}

	public static void UnregisterFont(BaseFont font)
	{
		List<string> list = new List<string>();
		foreach (KeyValuePair<string, BaseFont> item in sFontFactory)
		{
			if (item.Value == font)
			{
				list.Add(item.Key);
			}
		}
		foreach (string item2 in list)
		{
			sFontFactory.Remove(item2);
		}
	}

	public static BaseFont GetFont(string name)
	{
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Expected O, but got Unknown
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Expected O, but got Unknown
		BaseFont value;
		if (name.StartsWith("ui://"))
		{
			value = UIPackage.GetItemAssetByURL(name) as BaseFont;
			if (value != null)
			{
				return value;
			}
		}
		if (sFontFactory.TryGetValue(name, out value))
		{
			return value;
		}
		object obj = Resources.Load(name);
		if (obj == null)
		{
			obj = Resources.Load("Fonts/" + name);
		}
		if (obj == null)
		{
			if (name.IndexOf(",") != -1)
			{
				string[] array = name.Split(',');
				int num = array.Length;
				for (int i = 0; i < num; i++)
				{
					array[i] = array[i].Trim();
				}
				obj = Font.CreateDynamicFontFromOSFont(array, 16);
			}
			else
			{
				obj = Font.CreateDynamicFontFromOSFont(name, 16);
			}
		}
		if (obj == null)
		{
			return Fallback(name);
		}
		if (obj is Font)
		{
			value = new DynamicFont();
			value.name = name;
			sFontFactory.Add(name, value);
			((DynamicFont)value).nativeFont = (Font)obj;
		}
		else
		{
			if (!(obj is TMP_FontAsset))
			{
				if (obj.GetType().Name.Contains("TMP_FontAsset"))
				{
					Debug.LogWarning("To enable TextMeshPro support, add script define symbol: FAIRYGUI_TMPRO");
				}
				return Fallback(name);
			}
			value = new TMPFont();
			value.name = name;
			sFontFactory.Add(name, value);
			((TMPFont)value).fontAsset = (TMP_FontAsset)obj;
		}
		return value;
	}

	private static BaseFont Fallback(string name)
	{
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Expected O, but got Unknown
		if (name != UIConfig.defaultFont && sFontFactory.TryGetValue(UIConfig.defaultFont, out var value))
		{
			sFontFactory[name] = value;
			return value;
		}
		Font val = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
		if ((UnityEngine.Object)(object)val == null)
		{
			throw new Exception("Failed to load font '" + name + "'");
		}
		BaseFont baseFont = new DynamicFont();
		baseFont.name = name;
		((DynamicFont)baseFont).nativeFont = val;
		sFontFactory.Add(name, baseFont);
		return baseFont;
	}

	public static void Clear()
	{
		foreach (KeyValuePair<string, BaseFont> item in sFontFactory)
		{
			item.Value.Dispose();
		}
		sFontFactory.Clear();
	}
}
