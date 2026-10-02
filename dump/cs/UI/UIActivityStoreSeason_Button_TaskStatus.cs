using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivityStoreSeason_Button_TaskStatus : GButton
{
	public Controller Status;

	public const string URL = "ui://begz6gfvok4t1m";

	public static UIActivityStoreSeason_Button_TaskStatus CreateInstance()
	{
		return (UIActivityStoreSeason_Button_TaskStatus)UIPackage.CreateObject("ActivityStoreSeason", "ActivityStoreSeason_Button_TaskStatus");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		Status = GetControllerAt(1);
	}
}
