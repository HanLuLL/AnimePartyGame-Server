using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIHome_Button_CardBook : GButton
{
	public Transition Loop;

	public Transition Stay;

	public const string URL = "ui://u7xbdcgusjg4c";

	public static UIHome_Button_CardBook CreateInstance()
	{
		return (UIHome_Button_CardBook)UIPackage.CreateObject("Home", "Home_Button_CardBook");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		Loop = GetTransitionAt(0);
		Stay = GetTransitionAt(1);
	}
}
