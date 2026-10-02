using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIReplay_Button_Round : GButton
{
	public Controller status;

	public const string URL = "ui://dw3tmgbem0hx3";

	public static UIReplay_Button_Round CreateInstance()
	{
		return (UIReplay_Button_Round)UIPackage.CreateObject("Replay", "Replay_Button_Round");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		status = GetControllerAt(1);
	}
}
