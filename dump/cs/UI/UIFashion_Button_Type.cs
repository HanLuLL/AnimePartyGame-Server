using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIFashion_Button_Type : GButton
{
	public Controller redpoint;

	public const string URL = "ui://dl889m5qdx3g4w";

	public static UIFashion_Button_Type CreateInstance()
	{
		return (UIFashion_Button_Type)UIPackage.CreateObject("Fashion", "Fashion_Button_Type");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		redpoint = GetControllerAt(1);
	}
}
