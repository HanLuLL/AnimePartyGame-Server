using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIHero_Com_Level : GComponent
{
	public GTextField txt_level;

	public const string URL = "ui://7qkd4lqxbfwlq3i";

	public static UIHero_Com_Level CreateInstance()
	{
		return (UIHero_Com_Level)UIPackage.CreateObject("Hero", "Hero_Com_Level");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		txt_level = (GTextField)GetChildAt(2);
	}
}
