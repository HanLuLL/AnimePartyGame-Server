using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIHero_Button_GiftItem : GButton
{
	public Controller like;

	public GButton com_Item;

	public const string URL = "ui://7qkd4lqxg1lkv";

	public static UIHero_Button_GiftItem CreateInstance()
	{
		return (UIHero_Button_GiftItem)UIPackage.CreateObject("Hero", "Hero_Button_GiftItem");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		like = GetControllerAt(1);
		com_Item = (GButton)GetChildAt(0);
	}
}
