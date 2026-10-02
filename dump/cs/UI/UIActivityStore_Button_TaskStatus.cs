using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivityStore_Button_TaskStatus : GButton
{
	public Controller Status;

	public Controller type;

	public const string URL = "ui://88m1yfwgv1vk1j";

	public static UIActivityStore_Button_TaskStatus CreateInstance()
	{
		return (UIActivityStore_Button_TaskStatus)UIPackage.CreateObject("ActivityStore", "ActivityStore_Button_TaskStatus");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		Status = GetControllerAt(1);
		type = GetControllerAt(2);
	}
}
