using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UITask_Button_GoWay : GButton
{
	public Controller Status;

	public const string URL = "ui://rx1j3readzvfl";

	public static UITask_Button_GoWay CreateInstance()
	{
		return (UITask_Button_GoWay)UIPackage.CreateObject("ActivityRookieTask", "Task_Button_GoWay");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		Status = GetControllerAt(1);
	}
}
