using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIHome_Button_PlayerInfo : GButton
{
	public Controller redPoint;

	public Transition Loop;

	public Transition Stay;

	public const string URL = "ui://u7xbdcgusjg4b";

	public static UIHome_Button_PlayerInfo CreateInstance()
	{
		return (UIHome_Button_PlayerInfo)UIPackage.CreateObject("Home", "Home_Button_PlayerInfo");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		redPoint = GetControllerAt(1);
		Loop = GetTransitionAt(0);
		Stay = GetTransitionAt(1);
	}
}
