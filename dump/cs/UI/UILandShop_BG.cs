using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UILandShop_BG : GComponent
{
	public Transition cut_in;

	public const string URL = "ui://d5ngzgeuhmjfd";

	public static UILandShop_BG CreateInstance()
	{
		return (UILandShop_BG)UIPackage.CreateObject("LandShop", "LandShop_BG");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		cut_in = GetTransitionAt(0);
	}
}
