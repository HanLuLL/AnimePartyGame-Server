using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivityMGWT_Task_Button_TaskStatus : GButton
{
	public Controller Status;

	public const string URL = "ui://wdl8l4hslwm1o";

	public static UIActivityMGWT_Task_Button_TaskStatus CreateInstance()
	{
		return (UIActivityMGWT_Task_Button_TaskStatus)UIPackage.CreateObject("ActivityMGWT", "ActivityMGWT_Task_Button_TaskStatus");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		Status = GetControllerAt(1);
	}
}
