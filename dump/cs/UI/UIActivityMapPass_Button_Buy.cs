using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivityMapPass_Button_Buy : GButton
{
	public GRichTextField txt_price;

	public GTextField txt_tip;

	public const string URL = "ui://vvv9zaj1u0xak";

	public static UIActivityMapPass_Button_Buy CreateInstance()
	{
		return (UIActivityMapPass_Button_Buy)UIPackage.CreateObject("ActivityMapPass", "ActivityMapPass_Button_Buy");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		txt_price = (GRichTextField)GetChildAt(2);
		txt_tip = (GTextField)GetChildAt(3);
	}
}
