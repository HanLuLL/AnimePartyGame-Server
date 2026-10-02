using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UITask_Button_SevenDay : GButton
{
	public Controller Status;

	public Controller redStatus;

	public const string URL = "ui://hhpzjcmzl6o63u";

	public static UITask_Button_SevenDay CreateInstance()
	{
		return (UITask_Button_SevenDay)UIPackage.CreateObject("Task", "Task_Button_SevenDay");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		Status = GetControllerAt(1);
		redStatus = GetControllerAt(2);
	}
}
