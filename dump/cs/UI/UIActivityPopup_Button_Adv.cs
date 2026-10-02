using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivityPopup_Button_Adv : GButton
{
	public Controller page;

	public GLoader loader_1;

	public GLoader loader_2;

	public GGraph loader_Video;

	public const string URL = "ui://3tvdl51qnhqm3a";

	public static UIActivityPopup_Button_Adv CreateInstance()
	{
		return (UIActivityPopup_Button_Adv)UIPackage.CreateObject("ActivityPopup", "ActivityPopup_Button_Adv");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		page = GetControllerAt(1);
		loader_1 = (GLoader)GetChildAt(0);
		loader_2 = (GLoader)GetChildAt(1);
		loader_Video = (GGraph)GetChildAt(2);
	}
}
