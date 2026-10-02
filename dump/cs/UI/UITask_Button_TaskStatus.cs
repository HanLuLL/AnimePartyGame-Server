using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UITask_Button_TaskStatus : GButton
{
	public Controller Status;

	public const string URL = "ui://hhpzjcmzthbm2v";

	public static UITask_Button_TaskStatus CreateInstance()
	{
		return (UITask_Button_TaskStatus)UIPackage.CreateObject("Task", "Task_Button_TaskStatus");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		Status = GetControllerAt(1);
	}
}
