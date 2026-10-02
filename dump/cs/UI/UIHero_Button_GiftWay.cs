using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIHero_Button_GiftWay : GButton
{
	public GButton com_Item;

	public const string URL = "ui://7qkd4lqxl6o6q1s";

	public static UIHero_Button_GiftWay CreateInstance()
	{
		return (UIHero_Button_GiftWay)UIPackage.CreateObject("Hero", "Hero_Button_GiftWay");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		com_Item = (GButton)GetChildAt(0);
	}
}
