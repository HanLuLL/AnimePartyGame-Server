using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivityDice_Button_TaskStatus : GButton
{
	public Controller Status;

	public const string URL = "ui://lypih982lbjz15";

	public static UIActivityDice_Button_TaskStatus CreateInstance()
	{
		return (UIActivityDice_Button_TaskStatus)UIPackage.CreateObject("ActivityDice", "ActivityDice_Button_TaskStatus");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		Status = GetControllerAt(1);
	}
}
