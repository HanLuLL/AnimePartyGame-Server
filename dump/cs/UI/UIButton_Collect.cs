using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIButton_Collect : GButton
{
	public Controller collected;

	public const string URL = "ui://m6sn3r22w8cnq37";

	public static UIButton_Collect CreateInstance()
	{
		return (UIButton_Collect)UIPackage.CreateObject("Common_External", "Button_Collect");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		collected = GetControllerAt(1);
	}
}
