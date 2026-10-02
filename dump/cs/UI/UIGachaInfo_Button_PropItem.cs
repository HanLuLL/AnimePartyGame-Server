using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIGachaInfo_Button_PropItem : GButton
{
	public Controller isUp;

	public GButton com_LitItem;

	public GTextField txt_Percentage;

	public const string URL = "ui://egrtucyhot0w3";

	public static UIGachaInfo_Button_PropItem CreateInstance()
	{
		return (UIGachaInfo_Button_PropItem)UIPackage.CreateObject("GachaInfo", "GachaInfo_Button_PropItem");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		isUp = GetControllerAt(1);
		com_LitItem = (GButton)GetChildAt(0);
		txt_Percentage = (GTextField)GetChildAt(2);
	}
}
