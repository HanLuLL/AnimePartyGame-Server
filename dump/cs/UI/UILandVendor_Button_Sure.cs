using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UILandVendor_Button_Sure : GButton
{
	public GRichTextField txt_Desc;

	public const string URL = "ui://fqs3emmdheyw3";

	public static UILandVendor_Button_Sure CreateInstance()
	{
		return (UILandVendor_Button_Sure)UIPackage.CreateObject("LandVendor", "LandVendor_Button_Sure");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		txt_Desc = (GRichTextField)GetChildAt(3);
	}
}
