using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIAccountInfo_Button_ShowAchieve : GButton
{
	public Controller isLoaded;

	public UIAccountInfo_Com_AchieveLoader com_AchieveLoader;

	public const string URL = "ui://iepldke7zhzta";

	public static UIAccountInfo_Button_ShowAchieve CreateInstance()
	{
		return (UIAccountInfo_Button_ShowAchieve)UIPackage.CreateObject("AccountInfo", "AccountInfo_Button_ShowAchieve");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		isLoaded = GetControllerAt(1);
		com_AchieveLoader = (UIAccountInfo_Com_AchieveLoader)GetChildAt(4);
	}
}
