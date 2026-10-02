using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIGachaInfo_Com_Flash : GComponent
{
	public Transition Cut_in;

	public const string URL = "ui://egrtucyhu2w5j";

	public static UIGachaInfo_Com_Flash CreateInstance()
	{
		return (UIGachaInfo_Com_Flash)UIPackage.CreateObject("GachaInfo", "GachaInfo_Com_Flash");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		Cut_in = GetTransitionAt(0);
	}
}
