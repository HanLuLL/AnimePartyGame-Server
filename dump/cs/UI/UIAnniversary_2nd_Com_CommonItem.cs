using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIAnniversary_2nd_Com_CommonItem : GButton
{
	public GLoader loader_Icon;

	public GTextField txt_title;

	public const string URL = "ui://k49wk9ftesyu4b";

	public static UIAnniversary_2nd_Com_CommonItem CreateInstance()
	{
		return (UIAnniversary_2nd_Com_CommonItem)UIPackage.CreateObject("Anniversary_2Nd", "Anniversary_2nd_Com_CommonItem");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		loader_Icon = (GLoader)GetChildAt(0);
		txt_title = (GTextField)GetChildAt(1);
	}
}
