using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UICom_BuffInfoItem : GComponent
{
	public GLoader loader_Buff;

	public GTextField txt_Title;

	public GRichTextField txt_Desc;

	public const string URL = "ui://xuaw6o8jbczmq3t";

	public static UICom_BuffInfoItem CreateInstance()
	{
		return (UICom_BuffInfoItem)UIPackage.CreateObject("Common", "Com_BuffInfoItem");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		loader_Buff = (GLoader)GetChildAt(0);
		txt_Title = (GTextField)GetChildAt(1);
		txt_Desc = (GRichTextField)GetChildAt(2);
	}
}
