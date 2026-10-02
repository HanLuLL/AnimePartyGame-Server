using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIShopping_Button_Gacha : GButton
{
	public Controller angelMode;

	public const string URL = "ui://jyj1qox9tb9e4";

	public static UIShopping_Button_Gacha CreateInstance()
	{
		return (UIShopping_Button_Gacha)UIPackage.CreateObject("Shopping", "Shopping_Button_Gacha");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		angelMode = GetControllerAt(0);
	}
}
