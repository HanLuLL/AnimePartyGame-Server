using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIAccountInfo_Button_Toggle : GButton
{
	public GImage di_0;

	public GImage di_1;

	public const string URL = "ui://iepldke7zhztg";

	public static UIAccountInfo_Button_Toggle CreateInstance()
	{
		return (UIAccountInfo_Button_Toggle)UIPackage.CreateObject("AccountInfo", "AccountInfo_Button_Toggle");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		di_0 = (GImage)GetChildAt(0);
		di_1 = (GImage)GetChildAt(1);
	}
}
