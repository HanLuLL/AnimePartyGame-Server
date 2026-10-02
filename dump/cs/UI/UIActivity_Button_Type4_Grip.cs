using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivity_Button_Type4_Grip : GButton
{
	public GGraph grip;

	public const string URL = "ui://c1v285vtpu2iu";

	public static UIActivity_Button_Type4_Grip CreateInstance()
	{
		return (UIActivity_Button_Type4_Grip)UIPackage.CreateObject("ActivityNgo", "Activity_Button_Type4_Grip");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		grip = (GGraph)GetChildAt(0);
	}
}
