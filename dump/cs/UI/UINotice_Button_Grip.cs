using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UINotice_Button_Grip : GButton
{
	public GGraph grip;

	public const string URL = "ui://bwvbo0x0gd4da";

	public static UINotice_Button_Grip CreateInstance()
	{
		return (UINotice_Button_Grip)UIPackage.CreateObject("Notice", "Notice_Button_Grip");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		grip = (GGraph)GetChildAt(0);
	}
}
