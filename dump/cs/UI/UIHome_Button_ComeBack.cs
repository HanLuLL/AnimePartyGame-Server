using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIHome_Button_ComeBack : GButton
{
	public Controller redPoint;

	public GTextField txt_Time;

	public const string URL = "ui://u7xbdcguy9qnq5j";

	public static UIHome_Button_ComeBack CreateInstance()
	{
		return (UIHome_Button_ComeBack)UIPackage.CreateObject("Home", "Home_Button_ComeBack");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		redPoint = GetControllerAt(1);
		txt_Time = (GTextField)GetChildAt(3);
	}
}
