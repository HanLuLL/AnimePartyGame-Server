using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UITask_Button_Tab : GButton
{
	public Controller redStatus;

	public const string URL = "ui://hhpzjcmzthbm2n";

	public static UITask_Button_Tab CreateInstance()
	{
		return (UITask_Button_Tab)UIPackage.CreateObject("Task", "Task_Button_Tab");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		redStatus = GetControllerAt(1);
	}
}
