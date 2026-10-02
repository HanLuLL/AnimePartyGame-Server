using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIHero_Com_InfoArchiveContentDesc : GComponent
{
	public GRichTextField title;

	public const string URL = "ui://7qkd4lqxbfwlq3w";

	public static UIHero_Com_InfoArchiveContentDesc CreateInstance()
	{
		return (UIHero_Com_InfoArchiveContentDesc)UIPackage.CreateObject("Hero", "Hero_Com_InfoArchiveContentDesc");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		title = (GRichTextField)GetChildAt(0);
	}
}
