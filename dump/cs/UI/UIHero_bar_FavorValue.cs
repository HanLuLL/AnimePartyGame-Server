using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIHero_bar_FavorValue : GProgressBar
{
	public Controller barStatus;

	public GTextField txt_MAX;

	public const string URL = "ui://7qkd4lqxg1lk7";

	public static UIHero_bar_FavorValue CreateInstance()
	{
		return (UIHero_bar_FavorValue)UIPackage.CreateObject("Hero", "Hero_bar_FavorValue");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		barStatus = GetControllerAt(0);
		txt_MAX = (GTextField)GetChildAt(3);
	}
}
