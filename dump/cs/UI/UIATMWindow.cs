using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIATMWindow : GComponent
{
	public Controller Hide;

	public GTextField txt_HideTip;

	public GButton btn_Leave;

	public GButton btn_ATM;

	public GList list_Players;

	public GTextField txt_Token;

	public GButton btn_Transfer;

	public Transition Cut_in;

	public const string URL = "ui://lv54en7xbnngd";

	public static UIATMWindow CreateInstance()
	{
		BindAll();
		return (UIATMWindow)UIPackage.CreateObject("ATM", "ATMWindow");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://lv54en7xbnngd", typeof(UIATMWindow));
		UIObjectFactory.SetPackageItemExtension("ui://lv54en7xbnngh", typeof(UIATM_Button_RoleHeadshot));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		Hide = GetControllerAt(0);
		txt_HideTip = (GTextField)GetChildAt(0);
		btn_Leave = (GButton)GetChildAt(5);
		btn_ATM = (GButton)GetChildAt(6);
		list_Players = (GList)GetChildAt(7);
		txt_Token = (GTextField)GetChildAt(9);
		btn_Transfer = (GButton)GetChildAt(11);
		Cut_in = GetTransitionAt(0);
	}
}
