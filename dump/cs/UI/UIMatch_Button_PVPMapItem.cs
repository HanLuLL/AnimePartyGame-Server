using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIMatch_Button_PVPMapItem : GButton
{
	public Controller status;

	public Controller itemType;

	public UIMatch_Com_MapBanner com_Banner;

	public GTextField txt_Mode;

	public GTextField txt_MapTitle;

	public const string URL = "ui://qxwapsemzi0cm";

	public static UIMatch_Button_PVPMapItem CreateInstance()
	{
		return (UIMatch_Button_PVPMapItem)UIPackage.CreateObject("Match", "Match_Button_PVPMapItem");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		status = GetControllerAt(1);
		itemType = GetControllerAt(2);
		com_Banner = (UIMatch_Com_MapBanner)GetChildAt(0);
		txt_Mode = (GTextField)GetChildAt(4);
		txt_MapTitle = (GTextField)GetChildAt(5);
	}
}
