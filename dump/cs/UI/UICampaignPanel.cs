using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UICampaignPanel : GComponent
{
	public UICampaign_Button_Chapter btn_Novice;

	public UICampaign_Button_Chapter btn_PVE;

	public UICampaign_Button_Chapter btn_PVP;

	public GList list_Level;

	public GButton Back;

	public GButton btn_CreateRoom;

	public Transition Cut_in;

	public const string URL = "ui://0j78s7jyeyou0";

	public static UICampaignPanel CreateInstance()
	{
		BindAll();
		return (UICampaignPanel)UIPackage.CreateObject("Campaign", "CampaignPanel");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://0j78s7jyes7gy", typeof(UICampaign_Button_Chapter));
		UIObjectFactory.SetPackageItemExtension("ui://0j78s7jyeyou0", typeof(UICampaignPanel));
		UIObjectFactory.SetPackageItemExtension("ui://0j78s7jyh0j4p", typeof(UICampaign_Button_Level));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		btn_Novice = (UICampaign_Button_Chapter)GetChildAt(0);
		btn_PVE = (UICampaign_Button_Chapter)GetChildAt(1);
		btn_PVP = (UICampaign_Button_Chapter)GetChildAt(2);
		list_Level = (GList)GetChildAt(4);
		Back = (GButton)GetChildAt(5);
		btn_CreateRoom = (GButton)GetChildAt(6);
		Cut_in = GetTransitionAt(0);
	}
}
