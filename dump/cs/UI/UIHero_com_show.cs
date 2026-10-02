using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIHero_com_show : GComponent
{
	public Controller Status;

	public Controller releationPowerUp;

	public GTextField LV_txt;

	public const string URL = "ui://7qkd4lqxjhclq4c";

	public static UIHero_com_show CreateInstance()
	{
		return (UIHero_com_show)UIPackage.CreateObject("Hero", "Hero_com_show");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		Status = GetControllerAt(0);
		releationPowerUp = GetControllerAt(1);
		LV_txt = (GTextField)GetChildAt(3);
	}
}
