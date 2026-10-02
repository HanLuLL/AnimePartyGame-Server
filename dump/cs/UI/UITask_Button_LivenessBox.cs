using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UITask_Button_LivenessBox : GButton
{
	public Controller Status;

	public const string URL = "ui://hhpzjcmzthbm37";

	public static UITask_Button_LivenessBox CreateInstance()
	{
		return (UITask_Button_LivenessBox)UIPackage.CreateObject("Task", "Task_Button_LivenessBox");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		Status = GetControllerAt(0);
	}
}
