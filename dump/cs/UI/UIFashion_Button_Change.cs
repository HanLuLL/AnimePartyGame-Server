using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIFashion_Button_Change : GButton
{
	public Controller Status;

	public const string URL = "ui://dl889m5qlsb44g";

	public static UIFashion_Button_Change CreateInstance()
	{
		return (UIFashion_Button_Change)UIPackage.CreateObject("Fashion", "Fashion_Button_Change");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		Status = GetControllerAt(1);
	}
}
