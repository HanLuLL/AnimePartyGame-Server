using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIButton_Grip : GButton
{
	public GGraph grip;

	public const string URL = "ui://m6sn3r22g6uw6";

	public static UIButton_Grip CreateInstance()
	{
		return (UIButton_Grip)UIPackage.CreateObject("Common_External", "Button_Grip");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		grip = (GGraph)GetChildAt(0);
	}
}
