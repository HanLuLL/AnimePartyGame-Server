using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UICampaign_Button_Chapter : GButton
{
	public Controller status;

	public GTextField txt_Title;

	public GTextField txt_Desc;

	public GTextField txt_Progress;

	public const string URL = "ui://0j78s7jyes7gy";

	public static UICampaign_Button_Chapter CreateInstance()
	{
		return (UICampaign_Button_Chapter)UIPackage.CreateObject("Campaign", "Campaign_Button_Chapter");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		status = GetControllerAt(1);
		txt_Title = (GTextField)GetChildAt(2);
		txt_Desc = (GTextField)GetChildAt(3);
		txt_Progress = (GTextField)GetChildAt(5);
	}
}
