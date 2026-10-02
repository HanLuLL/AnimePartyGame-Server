using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivityResourceWeek_Button_Toggle : GButton
{
	public GGraph di_0;

	public GImage di_1;

	public const string URL = "ui://rel5h9izuytm9";

	public static UIActivityResourceWeek_Button_Toggle CreateInstance()
	{
		return (UIActivityResourceWeek_Button_Toggle)UIPackage.CreateObject("ActivityResourceWeek", "ActivityResourceWeek_Button_Toggle");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		di_0 = (GGraph)GetChildAt(0);
		di_1 = (GImage)GetChildAt(1);
	}
}
