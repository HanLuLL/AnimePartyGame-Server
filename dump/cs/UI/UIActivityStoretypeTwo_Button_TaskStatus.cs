using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivityStoretypeTwo_Button_TaskStatus : GButton
{
	public Controller Status;

	public const string URL = "ui://6dt5s4htqw8hv";

	public static UIActivityStoretypeTwo_Button_TaskStatus CreateInstance()
	{
		return (UIActivityStoretypeTwo_Button_TaskStatus)UIPackage.CreateObject("ActivityStoreTwo", "ActivityStoretypeTwo_Button_TaskStatus");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		Status = GetControllerAt(1);
	}
}
