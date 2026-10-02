using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivity_Button_TaskStatus : GButton
{
	public Controller Status;

	public const string URL = "ui://vckl96ksrsc7a";

	public static UIActivity_Button_TaskStatus CreateInstance()
	{
		return (UIActivity_Button_TaskStatus)UIPackage.CreateObject("Activity", "Activity_Button_TaskStatus");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		Status = GetControllerAt(1);
	}
}
