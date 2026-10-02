using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivity_Button_Type4_TaskStatus : GButton
{
	public Controller Status;

	public const string URL = "ui://c1v285vtpu2im";

	public static UIActivity_Button_Type4_TaskStatus CreateInstance()
	{
		return (UIActivity_Button_Type4_TaskStatus)UIPackage.CreateObject("ActivityNgo", "Activity_Button_Type4_TaskStatus");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		Status = GetControllerAt(1);
	}
}
