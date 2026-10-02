using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIHome_Button_SpecialActivity : GButton
{
	public Controller redPoint;

	public GLoader loader_Icon;

	public GTextField txt_Time;

	public Transition Loop;

	public Transition Stay;

	public const string URL = "ui://u7xbdcgune5m1y";

	public static UIHome_Button_SpecialActivity CreateInstance()
	{
		return (UIHome_Button_SpecialActivity)UIPackage.CreateObject("Home", "Home_Button_SpecialActivity");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		redPoint = GetControllerAt(1);
		loader_Icon = (GLoader)GetChildAt(0);
		txt_Time = (GTextField)GetChildAt(3);
		Loop = GetTransitionAt(0);
		Stay = GetTransitionAt(1);
	}
}
