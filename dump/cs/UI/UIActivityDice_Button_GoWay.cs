using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivityDice_Button_GoWay : GButton
{
	public Controller Status;

	public const string URL = "ui://lypih982lbjz1b";

	public static UIActivityDice_Button_GoWay CreateInstance()
	{
		return (UIActivityDice_Button_GoWay)UIPackage.CreateObject("ActivityDice", "ActivityDice_Button_GoWay");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		Status = GetControllerAt(1);
	}
}
