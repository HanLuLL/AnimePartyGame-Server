using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIReplay_Button_Switch : GButton
{
	public Controller status;

	public const string URL = "ui://dw3tmgbepf0p15";

	public static UIReplay_Button_Switch CreateInstance()
	{
		return (UIReplay_Button_Switch)UIPackage.CreateObject("Replay", "Replay_Button_Switch");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		status = GetControllerAt(1);
	}
}
