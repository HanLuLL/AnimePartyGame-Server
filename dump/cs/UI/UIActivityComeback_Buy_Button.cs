using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivityComeback_Buy_Button : GButton
{
	public GRichTextField txt_price;

	public GTextField txt_tip;

	public const string URL = "ui://hconmwfcvjqj11";

	public static UIActivityComeback_Buy_Button CreateInstance()
	{
		return (UIActivityComeback_Buy_Button)UIPackage.CreateObject("ActivityComeback", "ActivityComeback_Buy_Button");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		txt_price = (GRichTextField)GetChildAt(1);
		txt_tip = (GTextField)GetChildAt(2);
	}
}
