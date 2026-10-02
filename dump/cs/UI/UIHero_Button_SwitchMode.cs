using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIHero_Button_SwitchMode : GButton
{
	public Controller GameMode;

	public Transition SwitchPVP;

	public Transition SwitchPVE;

	public const string URL = "ui://7qkd4lqxx2xxq1x";

	public static UIHero_Button_SwitchMode CreateInstance()
	{
		return (UIHero_Button_SwitchMode)UIPackage.CreateObject("Hero", "Hero_Button_SwitchMode");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		GameMode = GetControllerAt(1);
		SwitchPVP = GetTransitionAt(0);
		SwitchPVE = GetTransitionAt(1);
	}
}
