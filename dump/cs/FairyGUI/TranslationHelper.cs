using System.Collections.Generic;
using FairyGUI.Utils;

namespace FairyGUI;

public class TranslationHelper
{
	public static Dictionary<string, Dictionary<string, string>> strings;

	public static void LoadFromXML(XML source)
	{
		strings = new Dictionary<string, Dictionary<string, string>>();
		XMLList.Enumerator enumerator = source.GetEnumerator("string");
		while (enumerator.MoveNext())
		{
			XML current = enumerator.Current;
			string attribute = current.GetAttribute("name");
			string text = current.text;
			int num = attribute.IndexOf("-");
			if (num != -1)
			{
				string key = attribute.Substring(0, num);
				string key2 = attribute.Substring(num + 1);
				if (!strings.TryGetValue(key, out var value))
				{
					value = new Dictionary<string, string>();
					strings[key] = value;
				}
				value[key2] = text;
			}
		}
	}

	public static void TranslateComponent(PackageItem item)
	{
		if (strings == null || !strings.TryGetValue(item.owner.id + item.id, out var value))
		{
			return;
		}
		ByteBuffer rawData = item.rawData;
		rawData.Seek(0, 2);
		int num = rawData.ReadShort();
		for (int i = 0; i < num; i++)
		{
			int num2 = rawData.ReadShort();
			int position = rawData.position;
			rawData.Seek(position, 0);
			ObjectType objectType = (ObjectType)rawData.ReadByte();
			ObjectType objectType2 = objectType;
			rawData.Skip(4);
			string text = rawData.ReadS();
			if (objectType2 == ObjectType.Component && rawData.Seek(position, 6))
			{
				objectType2 = (ObjectType)rawData.ReadByte();
			}
			rawData.Seek(position, 1);
			if (value.TryGetValue(text + "-tips", out var value2))
			{
				rawData.WriteS(value2);
			}
			rawData.Seek(position, 2);
			int num3 = rawData.ReadShort();
			for (int j = 0; j < num3; j++)
			{
				int num4 = rawData.ReadShort();
				num4 += rawData.position;
				if (rawData.ReadByte() == 6)
				{
					rawData.Skip(2);
					int num5 = rawData.ReadShort();
					for (int k = 0; k < num5; k++)
					{
						if (rawData.ReadS() != null)
						{
							if (value.TryGetValue(text + "-texts_" + k, out value2))
							{
								rawData.WriteS(value2);
							}
							else
							{
								rawData.Skip(2);
							}
						}
					}
					if (rawData.ReadBool() && value.TryGetValue(text + "-texts_def", out value2))
					{
						rawData.WriteS(value2);
					}
				}
				rawData.position = num4;
			}
			if (objectType == ObjectType.Component && rawData.version >= 2)
			{
				rawData.Seek(position, 4);
				rawData.Skip(2);
				rawData.Skip(4 * rawData.ReadShort());
				int num6 = rawData.ReadShort();
				for (int l = 0; l < num6; l++)
				{
					string text2 = rawData.ReadS();
					if (rawData.ReadShort() == 0 && value.TryGetValue(text + "-cp-" + text2, out value2))
					{
						rawData.WriteS(value2);
					}
					else
					{
						rawData.Skip(2);
					}
				}
			}
			switch (objectType2)
			{
			case ObjectType.Text:
			case ObjectType.RichText:
			case ObjectType.InputText:
				if (value.TryGetValue(text, out value2))
				{
					rawData.Seek(position, 6);
					rawData.WriteS(value2);
				}
				if (value.TryGetValue(text + "-prompt", out value2))
				{
					rawData.Seek(position, 4);
					rawData.WriteS(value2);
				}
				break;
			case ObjectType.List:
			case ObjectType.Tree:
			{
				rawData.Seek(position, 8);
				rawData.Skip(2);
				int num9 = rawData.ReadShort();
				for (int n = 0; n < num9; n++)
				{
					int num10 = rawData.ReadShort();
					num10 += rawData.position;
					rawData.Skip(2);
					if (objectType2 == ObjectType.Tree)
					{
						rawData.Skip(2);
					}
					if (value.TryGetValue(text + "-" + n, out value2))
					{
						rawData.WriteS(value2);
					}
					else
					{
						rawData.Skip(2);
					}
					if (value.TryGetValue(text + "-" + n + "-0", out value2))
					{
						rawData.WriteS(value2);
					}
					else
					{
						rawData.Skip(2);
					}
					if (rawData.version >= 2)
					{
						rawData.Skip(6);
						rawData.Skip(rawData.ReadShort() * 4);
						int num11 = rawData.ReadShort();
						for (int num12 = 0; num12 < num11; num12++)
						{
							string text3 = rawData.ReadS();
							if (rawData.ReadShort() == 0 && value.TryGetValue(text + "-" + n + "-" + text3, out value2))
							{
								rawData.WriteS(value2);
							}
							else
							{
								rawData.Skip(2);
							}
						}
					}
					rawData.position = num10;
				}
				break;
			}
			case ObjectType.Label:
				if (rawData.Seek(position, 6) && (ObjectType)rawData.ReadByte() == objectType2)
				{
					if (value.TryGetValue(text, out value2))
					{
						rawData.WriteS(value2);
					}
					else
					{
						rawData.Skip(2);
					}
					rawData.Skip(2);
					if (rawData.ReadBool())
					{
						rawData.Skip(4);
					}
					rawData.Skip(4);
					if (rawData.ReadBool() && value.TryGetValue(text + "-prompt", out value2))
					{
						rawData.WriteS(value2);
					}
				}
				break;
			case ObjectType.Button:
				if (rawData.Seek(position, 6) && (ObjectType)rawData.ReadByte() == objectType2)
				{
					if (value.TryGetValue(text, out value2))
					{
						rawData.WriteS(value2);
					}
					else
					{
						rawData.Skip(2);
					}
					if (value.TryGetValue(text + "-0", out value2))
					{
						rawData.WriteS(value2);
					}
				}
				break;
			case ObjectType.ComboBox:
			{
				if (!rawData.Seek(position, 6) || (ObjectType)rawData.ReadByte() != objectType2)
				{
					break;
				}
				int num7 = rawData.ReadShort();
				for (int m = 0; m < num7; m++)
				{
					int num8 = rawData.ReadShort();
					num8 += rawData.position;
					if (value.TryGetValue(text + "-" + m, out value2))
					{
						rawData.WriteS(value2);
					}
					rawData.position = num8;
				}
				if (value.TryGetValue(text, out value2))
				{
					rawData.WriteS(value2);
				}
				break;
			}
			}
			rawData.position = position + num2;
		}
	}
}
