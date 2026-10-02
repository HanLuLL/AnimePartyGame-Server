using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIHome_Button_Mall : GButton
{
	public Controller angelMode;

	public Controller redPoint;

	public Transition Loop;

	public Transition Stay;

	public const string URL = "ui://u7xbdcgusjg4d";

	public static UIHome_Button_Mall CreateInstance()
	{
		return (UIHome_Button_Mall)UIPackage.CreateObject("Home", "Home_Button_Mall");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		angelMode = GetControllerAt(1);
		redPoint = GetControllerAt(2);
		Loop = GetTransitionAt(0);
		Stay = GetTransitionAt(1);
	}
}
