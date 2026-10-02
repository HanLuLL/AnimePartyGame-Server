using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIShopping_Com_Main : GComponent
{
	public UIShopping_Button_Gacha btn_Gacha;

	public UIShopping_Button_Mall btn_Mall;

	public Transition display;

	public const string URL = "ui://jyj1qox9tb9e1";

	public static UIShopping_Com_Main CreateInstance()
	{
		return (UIShopping_Com_Main)UIPackage.CreateObject("Shopping", "Shopping_Com_Main");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		btn_Gacha = (UIShopping_Button_Gacha)GetChildAt(0);
		btn_Mall = (UIShopping_Button_Mall)GetChildAt(1);
		display = GetTransitionAt(0);
	}
}
