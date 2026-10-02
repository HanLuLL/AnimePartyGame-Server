using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIAccountInfo_Button_Grip : GButton
{
	public GGraph grip;

	public const string URL = "ui://iepldke7f3tl1n";

	public static UIAccountInfo_Button_Grip CreateInstance()
	{
		return (UIAccountInfo_Button_Grip)UIPackage.CreateObject("AccountInfo", "AccountInfo_Button_Grip");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		grip = (GGraph)GetChildAt(0);
	}
}
