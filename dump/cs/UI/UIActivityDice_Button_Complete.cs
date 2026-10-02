using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivityDice_Button_Complete : GButton
{
	public Controller Status;

	public const string URL = "ui://lypih982hdqo20";

	public static UIActivityDice_Button_Complete CreateInstance()
	{
		return (UIActivityDice_Button_Complete)UIPackage.CreateObject("ActivityDice", "ActivityDice_Button_Complete");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		Status = GetControllerAt(0);
	}
}
