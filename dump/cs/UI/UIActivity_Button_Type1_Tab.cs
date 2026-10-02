using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivity_Button_Type1_Tab : GButton
{
	public Controller redStatus;

	public const string URL = "ui://vckl96ksrsc71";

	public static UIActivity_Button_Type1_Tab CreateInstance()
	{
		return (UIActivity_Button_Type1_Tab)UIPackage.CreateObject("Activity", "Activity_Button_Type1_Tab");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		redStatus = GetControllerAt(1);
	}
}
