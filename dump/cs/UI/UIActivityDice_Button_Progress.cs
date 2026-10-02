using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivityDice_Button_Progress : GComponent
{
	public Controller Status;

	public Controller button;

	public GButton rewardItem;

	public GTextField title;

	public UIActivityDice_Button_Complete Btn_Compelete;

	public const string URL = "ui://lypih982lbjz1n";

	public static UIActivityDice_Button_Progress CreateInstance()
	{
		return (UIActivityDice_Button_Progress)UIPackage.CreateObject("ActivityDice", "ActivityDice_Button_Progress");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		Status = GetControllerAt(0);
		button = GetControllerAt(1);
		rewardItem = (GButton)GetChildAt(0);
		title = (GTextField)GetChildAt(2);
		Btn_Compelete = (UIActivityDice_Button_Complete)GetChildAt(3);
	}
}
