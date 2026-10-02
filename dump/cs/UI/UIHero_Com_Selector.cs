using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIHero_Com_Selector : GComponent
{
	public GList list_Hero;

	public const string URL = "ui://7qkd4lqxat01q33";

	public static UIHero_Com_Selector CreateInstance()
	{
		return (UIHero_Com_Selector)UIPackage.CreateObject("Hero", "Hero_Com_Selector");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		list_Hero = (GList)GetChildAt(1);
	}
}
