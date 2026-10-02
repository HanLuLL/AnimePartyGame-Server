using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIMGWT_Store_Buy_Button : GButton
{
	public Controller Type;

	public GRichTextField txt_Price;

	public GRichTextField txt_OriginalPrice;

	public const string URL = "ui://2p754tqkilj517";

	public static UIMGWT_Store_Buy_Button CreateInstance()
	{
		return (UIMGWT_Store_Buy_Button)UIPackage.CreateObject("MGWTStore", "MGWT_Store_Buy_Button");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		Type = GetControllerAt(1);
		txt_Price = (GRichTextField)GetChildAt(2);
		txt_OriginalPrice = (GRichTextField)GetChildAt(3);
	}
}
