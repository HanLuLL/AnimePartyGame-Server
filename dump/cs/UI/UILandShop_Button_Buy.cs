using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UILandShop_Button_Buy : GButton
{
	public GTextField txt_Cost;

	public const string URL = "ui://d5ngzgeurum76";

	public static UILandShop_Button_Buy CreateInstance()
	{
		return (UILandShop_Button_Buy)UIPackage.CreateObject("LandShop", "LandShop_Button_Buy");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		txt_Cost = (GTextField)GetChildAt(2);
	}
}
