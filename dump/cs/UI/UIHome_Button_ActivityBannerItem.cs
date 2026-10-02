using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIHome_Button_ActivityBannerItem : GButton
{
	public Controller redPoint;

	public GLoader loader_Icon;

	public GTextField txt_Time;

	public const string URL = "ui://u7xbdcguenysq3l";

	public static UIHome_Button_ActivityBannerItem CreateInstance()
	{
		return (UIHome_Button_ActivityBannerItem)UIPackage.CreateObject("Home", "Home_Button_ActivityBannerItem");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		redPoint = GetControllerAt(1);
		loader_Icon = (GLoader)GetChildAt(0);
		txt_Time = (GTextField)GetChildAt(2);
	}
}
