using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIHero_Com_TalentItem : GComponent
{
	public GButton com_Item;

	public GTextField txt_Num;

	public const string URL = "ui://7qkd4lqxbfwlq3t";

	public static UIHero_Com_TalentItem CreateInstance()
	{
		return (UIHero_Com_TalentItem)UIPackage.CreateObject("Hero", "Hero_Com_TalentItem");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		com_Item = (GButton)GetChildAt(0);
		txt_Num = (GTextField)GetChildAt(1);
	}
}
