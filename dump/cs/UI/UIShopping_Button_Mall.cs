using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIShopping_Button_Mall : GButton
{
	public Controller angelMode;

	public const string URL = "ui://jyj1qox9tb9e3";

	public static UIShopping_Button_Mall CreateInstance()
	{
		return (UIShopping_Button_Mall)UIPackage.CreateObject("Shopping", "Shopping_Button_Mall");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		angelMode = GetControllerAt(0);
	}
}
