using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIBottomMenu_Button_Function : GButton
{
	public Controller functionController;

	public Controller redPoint;

	public Controller type;

	public const string URL = "ui://ybwxnbf5qzg81d";

	public static UIBottomMenu_Button_Function CreateInstance()
	{
		return (UIBottomMenu_Button_Function)UIPackage.CreateObject("BottomMenu", "BottomMenu_Button_Function");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		functionController = GetControllerAt(1);
		redPoint = GetControllerAt(2);
		type = GetControllerAt(3);
	}
}
