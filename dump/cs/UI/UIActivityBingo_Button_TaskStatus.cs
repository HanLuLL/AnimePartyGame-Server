using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivityBingo_Button_TaskStatus : GButton
{
	public Controller Status;

	public const string URL = "ui://1pgen0em6brr19";

	public static UIActivityBingo_Button_TaskStatus CreateInstance()
	{
		return (UIActivityBingo_Button_TaskStatus)UIPackage.CreateObject("ActivityBingo", "ActivityBingo_Button_TaskStatus");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		Status = GetControllerAt(1);
	}
}
