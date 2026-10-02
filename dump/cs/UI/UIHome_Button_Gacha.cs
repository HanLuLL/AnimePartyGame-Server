using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIHome_Button_Gacha : GButton
{
	public Controller redPoint;

	public Controller tips;

	public Transition Loop;

	public Transition Stay;

	public const string URL = "ui://u7xbdcgu11biq2e";

	public static UIHome_Button_Gacha CreateInstance()
	{
		return (UIHome_Button_Gacha)UIPackage.CreateObject("Home", "Home_Button_Gacha");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		redPoint = GetControllerAt(1);
		tips = GetControllerAt(2);
		Loop = GetTransitionAt(0);
		Stay = GetTransitionAt(1);
	}
}
