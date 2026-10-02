using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIReplay_Button_Turn : GButton
{
	public Controller status;

	public const string URL = "ui://dw3tmgbem0hx2";

	public static UIReplay_Button_Turn CreateInstance()
	{
		return (UIReplay_Button_Turn)UIPackage.CreateObject("Replay", "Replay_Button_Turn");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		status = GetControllerAt(1);
	}
}
