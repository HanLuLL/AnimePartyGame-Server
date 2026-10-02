using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIHero_Com_SkinSelector : GComponent
{
	public GList list_StandingPainting;

	public const string URL = "ui://7qkd4lqxat01q34";

	public static UIHero_Com_SkinSelector CreateInstance()
	{
		return (UIHero_Com_SkinSelector)UIPackage.CreateObject("Hero", "Hero_Com_SkinSelector");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		list_StandingPainting = (GList)GetChildAt(1);
	}
}
