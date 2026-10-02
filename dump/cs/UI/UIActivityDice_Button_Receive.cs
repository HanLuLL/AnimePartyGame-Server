using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivityDice_Button_Receive : GButton
{
	public Controller Status;

	public const string URL = "ui://lypih982rae11t";

	public static UIActivityDice_Button_Receive CreateInstance()
	{
		return (UIActivityDice_Button_Receive)UIPackage.CreateObject("ActivityDice", "ActivityDice_Button_Receive");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		Status = GetControllerAt(0);
	}
}
