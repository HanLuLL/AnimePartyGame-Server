using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivitySevenDaySignIn_Btn : GButton
{
	public Controller get;

	public GTextField text_no;

	public GTextField text_yes;

	public GLoader icon1;

	public GTextField txt_number;

	public const string URL = "ui://p8fy3he4rr3513";

	public static UIActivitySevenDaySignIn_Btn CreateInstance()
	{
		return (UIActivitySevenDaySignIn_Btn)UIPackage.CreateObject("ActivitySevenDaySignIn", "ActivitySevenDaySignIn_Btn");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		get = GetControllerAt(1);
		text_no = (GTextField)GetChildAt(4);
		text_yes = (GTextField)GetChildAt(5);
		icon1 = (GLoader)GetChildAt(6);
		txt_number = (GTextField)GetChildAt(7);
	}
}
