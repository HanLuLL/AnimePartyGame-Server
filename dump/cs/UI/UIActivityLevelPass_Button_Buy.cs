using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivityLevelPass_Button_Buy : GButton
{
	public GRichTextField txt_price;

	public GTextField txt_tip;

	public const string URL = "ui://fajmeueem48ix";

	public static UIActivityLevelPass_Button_Buy CreateInstance()
	{
		return (UIActivityLevelPass_Button_Buy)UIPackage.CreateObject("ActivityLevelPass", "ActivityLevelPass_Button_Buy");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		txt_price = (GRichTextField)GetChildAt(2);
		txt_tip = (GTextField)GetChildAt(3);
	}
}
