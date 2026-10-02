using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIHero_Button_Set : GButton
{
	public Controller Type;

	public const string URL = "ui://u7xbdcgukrvoq4g";

	public static UIHero_Button_Set CreateInstance()
	{
		return (UIHero_Button_Set)UIPackage.CreateObject("Home", "Hero_Button_Set");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		Type = GetControllerAt(1);
	}
}
