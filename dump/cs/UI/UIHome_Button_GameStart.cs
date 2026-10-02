using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIHome_Button_GameStart : GButton
{
	public Controller logoVersion;

	public Controller angelMode;

	public Controller country;

	public Transition down;

	public Transition Autoplay;

	public Transition Cut_in;

	public Transition Loop;

	public Transition Stay;

	public const string URL = "ui://u7xbdcgusjg4a";

	public static UIHome_Button_GameStart CreateInstance()
	{
		return (UIHome_Button_GameStart)UIPackage.CreateObject("Home", "Home_Button_GameStart");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		logoVersion = GetControllerAt(1);
		angelMode = GetControllerAt(2);
		country = GetControllerAt(3);
		down = GetTransitionAt(0);
		Autoplay = GetTransitionAt(1);
		Cut_in = GetTransitionAt(2);
		Loop = GetTransitionAt(3);
		Stay = GetTransitionAt(4);
	}
}
