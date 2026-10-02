using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivityComeback_Task_Button_TaskStatus : GButton
{
	public Controller Status;

	public const string URL = "ui://hconmwfcy9qnf";

	public static UIActivityComeback_Task_Button_TaskStatus CreateInstance()
	{
		return (UIActivityComeback_Task_Button_TaskStatus)UIPackage.CreateObject("ActivityComeback", "ActivityComeback_Task_Button_TaskStatus");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		Status = GetControllerAt(1);
	}
}
