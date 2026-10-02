using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIHero_Com_FileBreakThroughDesc : GComponent
{
	public GRichTextField title;

	public const string URL = "ui://7qkd4lqxbfwlq3q";

	public static UIHero_Com_FileBreakThroughDesc CreateInstance()
	{
		return (UIHero_Com_FileBreakThroughDesc)UIPackage.CreateObject("Hero", "Hero_Com_FileBreakThroughDesc");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		title = (GRichTextField)GetChildAt(0);
	}
}
