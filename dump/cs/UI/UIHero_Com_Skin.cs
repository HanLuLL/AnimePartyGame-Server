using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIHero_Com_Skin : GComponent
{
	public GTextField txt_SkinProgress;

	public UIHero_Com_SkinSelector com_StandingPaintingList;

	public const string URL = "ui://7qkd4lqxg1lk19";

	public static UIHero_Com_Skin CreateInstance()
	{
		return (UIHero_Com_Skin)UIPackage.CreateObject("Hero", "Hero_Com_Skin");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		txt_SkinProgress = (GTextField)GetChildAt(0);
		com_StandingPaintingList = (UIHero_Com_SkinSelector)GetChildAt(1);
	}
}
