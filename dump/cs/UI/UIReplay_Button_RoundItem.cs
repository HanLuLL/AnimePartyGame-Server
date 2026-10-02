using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIReplay_Button_RoundItem : GButton
{
	public Controller IsExpanded;

	public GTextField txt_Round;

	public Transition Cut_in;

	public const string URL = "ui://dw3tmgbem0hx7";

	public static UIReplay_Button_RoundItem CreateInstance()
	{
		return (UIReplay_Button_RoundItem)UIPackage.CreateObject("Replay", "Replay_Button_RoundItem");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		IsExpanded = GetControllerAt(1);
		txt_Round = (GTextField)GetChildAt(1);
		Cut_in = GetTransitionAt(0);
	}
}
