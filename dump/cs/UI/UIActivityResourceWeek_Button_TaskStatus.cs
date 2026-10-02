using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivityResourceWeek_Button_TaskStatus : GButton
{
	public Controller Status;

	public const string URL = "ui://rel5h9izuytmd";

	public static UIActivityResourceWeek_Button_TaskStatus CreateInstance()
	{
		return (UIActivityResourceWeek_Button_TaskStatus)UIPackage.CreateObject("ActivityResourceWeek", "ActivityResourceWeek_Button_TaskStatus");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		Status = GetControllerAt(1);
	}
}
