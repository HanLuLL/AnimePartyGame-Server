using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivityStoretype1_Button_TaskStatus : GButton
{
	public Controller Status;

	public Controller type;

	public const string URL = "ui://88m1yfwgeusd2u";

	public static UIActivityStoretype1_Button_TaskStatus CreateInstance()
	{
		return (UIActivityStoretype1_Button_TaskStatus)UIPackage.CreateObject("ActivityStore", "ActivityStoretype1_Button_TaskStatus");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		Status = GetControllerAt(1);
		type = GetControllerAt(2);
	}
}
