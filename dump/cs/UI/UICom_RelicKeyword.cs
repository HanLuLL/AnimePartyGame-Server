using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UICom_RelicKeyword : GComponent
{
	public struct RelicKeywordData
	{
		public string icon;

		public string title;

		public string desc;
	}

	public GLoader loader_Icon;

	public GTextField txt_Title;

	public GRichTextField txt_Desc;

	public const string URL = "ui://xuaw6o8jo812a";

	public static UICom_RelicKeyword CreateInstance()
	{
		return (UICom_RelicKeyword)UIPackage.CreateObject("Common", "Com_RelicKeyword");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		loader_Icon = (GLoader)GetChildAt(1);
		txt_Title = (GTextField)GetChildAt(2);
		txt_Desc = (GRichTextField)GetChildAt(3);
	}

	public void Refresh(RelicKeywordsConfigure KeywordConfig)
	{
		if (KeywordConfig == null)
		{
			base.visible = false;
			return;
		}
		loader_Icon.url = KeywordConfig.Icon;
		txt_Title.text = KeywordConfig.KeywordNameID.GetLocal(UIStringType.Relic);
		txt_Desc.text = KeywordConfig.KeywordDesID.GetLocal(UIStringType.Relic);
		base.visible = true;
	}

	public void RefreshInfo(RelicKeywordData info)
	{
		if (string.IsNullOrEmpty(info.icon) || string.IsNullOrEmpty(info.desc))
		{
			base.visible = false;
			return;
		}
		loader_Icon.url = info.icon;
		txt_Title.text = info.title;
		txt_Desc.text = info.desc;
		base.visible = true;
	}
}
