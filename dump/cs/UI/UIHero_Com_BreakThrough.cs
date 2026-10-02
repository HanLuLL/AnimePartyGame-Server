using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIHero_Com_BreakThrough : GComponent
{
	public GButton com_Item;

	public GTextField txt_Num;

	public const string URL = "ui://7qkd4lqxg1lk18";

	public static UIHero_Com_BreakThrough CreateInstance()
	{
		return (UIHero_Com_BreakThrough)UIPackage.CreateObject("Hero", "Hero_Com_BreakThrough");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		com_Item = (GButton)GetChildAt(0);
		txt_Num = (GTextField)GetChildAt(1);
	}
}
