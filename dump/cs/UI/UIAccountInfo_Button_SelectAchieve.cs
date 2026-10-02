using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIAccountInfo_Button_SelectAchieve : GButton
{
	public Controller isShow;

	public Controller hasSelected;

	public GTextField txt_Achieve;

	public UIAccountInfo_Com_AchieveLoader com_AchieveLoader;

	public const string URL = "ui://iepldke7zhztc";

	public static UIAccountInfo_Button_SelectAchieve CreateInstance()
	{
		return (UIAccountInfo_Button_SelectAchieve)UIPackage.CreateObject("AccountInfo", "AccountInfo_Button_SelectAchieve");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		isShow = GetControllerAt(1);
		hasSelected = GetControllerAt(2);
		txt_Achieve = (GTextField)GetChildAt(4);
		com_AchieveLoader = (UIAccountInfo_Com_AchieveLoader)GetChildAt(5);
	}
}
