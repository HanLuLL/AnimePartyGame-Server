using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIBottomMenu_Button_Token : GButton
{
	public Controller canRecharge;

	public GLoader loader_Token;

	public GTextField txt_Token;

	public GImage btn_Rechage;

	public const string URL = "ui://ybwxnbf5z52sy";

	public static UIBottomMenu_Button_Token CreateInstance()
	{
		return (UIBottomMenu_Button_Token)UIPackage.CreateObject("BottomMenu", "BottomMenu_Button_Token");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		canRecharge = GetControllerAt(0);
		loader_Token = (GLoader)GetChildAt(1);
		txt_Token = (GTextField)GetChildAt(2);
		btn_Rechage = (GImage)GetChildAt(3);
	}
}
