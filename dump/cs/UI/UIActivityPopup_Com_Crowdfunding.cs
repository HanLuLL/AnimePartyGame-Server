using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivityPopup_Com_Crowdfunding : GComponent
{
	public Controller status;

	public GLoader loader_BG;

	public GButton btn_GoCrowdfunding;

	public GTextField txt_Countdown;

	public UIActivityPopup_Com_Adv com_Adv;

	public const string URL = "ui://3tvdl51qnhqm35";

	public static UIActivityPopup_Com_Crowdfunding CreateInstance()
	{
		return (UIActivityPopup_Com_Crowdfunding)UIPackage.CreateObject("ActivityPopup", "ActivityPopup_Com_Crowdfunding");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		status = GetControllerAt(0);
		loader_BG = (GLoader)GetChildAt(0);
		btn_GoCrowdfunding = (GButton)GetChildAt(2);
		txt_Countdown = (GTextField)GetChildAt(5);
		com_Adv = (UIActivityPopup_Com_Adv)GetChildAt(6);
	}
}
