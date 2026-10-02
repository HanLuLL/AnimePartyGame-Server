using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UISkinSell_Com_SkinItem : GComponent
{
	public GGraph graph_Skin;

	public GTextField txt_Get;

	public GTextField txt_Title;

	public const string URL = "ui://hmljxqy1fu2t11";

	public static UISkinSell_Com_SkinItem CreateInstance()
	{
		return (UISkinSell_Com_SkinItem)UIPackage.CreateObject("SkinSell", "SkinSell_Com_SkinItem");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		graph_Skin = (GGraph)GetChildAt(0);
		txt_Get = (GTextField)GetChildAt(1);
		txt_Title = (GTextField)GetChildAt(2);
	}
}
