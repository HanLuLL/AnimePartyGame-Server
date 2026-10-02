using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIGacha_ProgressTab : GComponent
{
	public Controller getType;

	public GTextField txt_title;

	public GTextField txt_canget;

	public GButton btn_seeall;

	public GButton btn_get;

	public UIGacha_Button_Item btn_item;

	public const string URL = "ui://j90wpcmni82zqq39";

	public static UIGacha_ProgressTab CreateInstance()
	{
		return (UIGacha_ProgressTab)UIPackage.CreateObject("Gacha", "Gacha_ProgressTab");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		getType = GetControllerAt(0);
		txt_title = (GTextField)GetChildAt(1);
		txt_canget = (GTextField)GetChildAt(2);
		btn_seeall = (GButton)GetChildAt(3);
		btn_get = (GButton)GetChildAt(4);
		btn_item = (UIGacha_Button_Item)GetChildAt(5);
	}
}
