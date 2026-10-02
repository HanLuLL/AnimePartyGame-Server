using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UISoloLevel_Button_Training : GButton
{
	public Controller angleMode;

	public GLoader icon_sfw;

	public GTextField txt_Tip;

	public const string URL = "ui://xuxzg1y1mgsn2";

	public static UISoloLevel_Button_Training CreateInstance()
	{
		return (UISoloLevel_Button_Training)UIPackage.CreateObject("SoloLevel", "SoloLevel_Button_Training");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		angleMode = GetControllerAt(0);
		icon_sfw = (GLoader)GetChildAt(1);
		txt_Tip = (GTextField)GetChildAt(3);
	}
}
