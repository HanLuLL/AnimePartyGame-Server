using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIInfo_Button_Grip : GButton
{
	public GGraph grip;

	public const string URL = "ui://zhzrfum9dtry4";

	public static UIInfo_Button_Grip CreateInstance()
	{
		return (UIInfo_Button_Grip)UIPackage.CreateObject("Info", "Info_Button_Grip");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		grip = (GGraph)GetChildAt(0);
	}
}
