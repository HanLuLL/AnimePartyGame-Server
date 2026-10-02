using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIReplay_Button_Grip : GButton
{
	public GGraph grip;

	public const string URL = "ui://dw3tmgbepf0p14";

	public static UIReplay_Button_Grip CreateInstance()
	{
		return (UIReplay_Button_Grip)UIPackage.CreateObject("Replay", "Replay_Button_Grip");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		grip = (GGraph)GetChildAt(0);
	}
}
