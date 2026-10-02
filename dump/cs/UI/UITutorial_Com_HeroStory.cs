using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UITutorial_Com_HeroStory : GComponent
{
	public GTextField txt_Story;

	public const string URL = "ui://b96qpoz6iu43o";

	public static UITutorial_Com_HeroStory CreateInstance()
	{
		return (UITutorial_Com_HeroStory)UIPackage.CreateObject("Tutorial", "Tutorial_Com_HeroStory");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		txt_Story = (GTextField)GetChildAt(0);
	}
}
