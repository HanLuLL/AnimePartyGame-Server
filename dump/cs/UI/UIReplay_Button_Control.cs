using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIReplay_Button_Control : GButton
{
	public Controller status;

	public const string URL = "ui://dw3tmgbem0hx1";

	public static UIReplay_Button_Control CreateInstance()
	{
		return (UIReplay_Button_Control)UIPackage.CreateObject("Replay", "Replay_Button_Control");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		status = GetControllerAt(0);
	}
}
