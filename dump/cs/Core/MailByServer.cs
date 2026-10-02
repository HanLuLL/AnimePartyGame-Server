using UI;
using UnityEngine;

namespace Core;

public class MailByServer
{
	public string lo;

	public string Simplified;

	public string English;

	public string Japanese;

	public string Traditional;

	public string Item;

	public string _Simplified
	{
		get
		{
			if (!string.IsNullOrWhiteSpace(lo) && int.TryParse(lo, out var result))
			{
				return result.GetLocal(UIStringType.Message);
			}
			return Simplified;
		}
	}

	public string _English
	{
		get
		{
			if (!string.IsNullOrWhiteSpace(lo) && int.TryParse(lo, out var result))
			{
				return result.GetLocal(UIStringType.Message);
			}
			return English;
		}
	}

	public string _Japanese
	{
		get
		{
			if (!string.IsNullOrWhiteSpace(lo) && int.TryParse(lo, out var result))
			{
				return result.GetLocal(UIStringType.Message);
			}
			return Japanese;
		}
	}

	public string _Traditional
	{
		get
		{
			if (!string.IsNullOrWhiteSpace(lo) && int.TryParse(lo, out var result))
			{
				return result.GetLocal(UIStringType.Message);
			}
			return Traditional;
		}
	}

	public string _Item
	{
		get
		{
			if (!string.IsNullOrWhiteSpace(Item))
			{
				string text = "";
				string[] array = Item.Split(',');
				foreach (string text2 in array)
				{
					string[] array2 = text2.Split(':');
					if (array2.Length == 2 && int.TryParse(array2[0], out var result))
					{
						if (StaticConfigure.Item.InfoDict.TryGetValue(result, out var value))
						{
							string local = value.NameID.GetLocal(UIStringType.Item);
							text += $"\n<img src='{value.ShowIcon}' width='40' height='40'/>[url=Item_{result}_{array2[1]}]{local}[/url]x{array2[1]}";
						}
						else
						{
							Debug.LogError("附件配置无法通过道具Id：" + array2[0] + "获取配置");
						}
					}
					else
					{
						Debug.LogError("附件配置无法区分道具Id和道具数量：" + text2);
					}
				}
				return text;
			}
			return Item;
		}
	}
}
