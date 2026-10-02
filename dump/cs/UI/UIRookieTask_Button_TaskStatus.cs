using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIRookieTask_Button_TaskStatus : GButton
{
	public Controller Status;

	public const string URL = "ui://rx1j3readzvf4";

	public static UIRookieTask_Button_TaskStatus CreateInstance()
	{
		return (UIRookieTask_Button_TaskStatus)UIPackage.CreateObject("ActivityRookieTask", "RookieTask_Button_TaskStatus");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		Status = GetControllerAt(1);
	}
}
