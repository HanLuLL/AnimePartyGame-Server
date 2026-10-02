using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIBattleRelicInfoWindow : GComponent
{
	public Controller button;

	public GList list_Tab;

	public GButton btn_Close;

	public GList list_RelicGroup;

	public GTextField txt_Name;

	public GRichTextField txt_Desc;

	public UIBattleRelicInfo_Button_Arrow btn_Right;

	public UIBattleRelicInfo_Button_Arrow btn_Left;

	public UIBattleRelicInfo_Button_Chat btn_Chat;

	public Transition Cut_in;

	public Transition Cut_out;

	public const string URL = "ui://ethkhr1hot0wd";

	public static UIBattleRelicInfoWindow CreateInstance()
	{
		BindAll();
		return (UIBattleRelicInfoWindow)UIPackage.CreateObject("BattleRelicInfo", "BattleRelicInfoWindow");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://ethkhr1hfcnul", typeof(UIBattleRelicInfo_Button_PlayerTab_Mobile));
		UIObjectFactory.SetPackageItemExtension("ui://ethkhr1hfcnum", typeof(UIBattleRelicInfo_Com_RelicGroup));
		UIObjectFactory.SetPackageItemExtension("ui://ethkhr1hot0wd", typeof(UIBattleRelicInfoWindow));
		UIObjectFactory.SetPackageItemExtension("ui://ethkhr1hot0we", typeof(UIBattleRelicInfo_Button_PlayerTab_PC));
		UIObjectFactory.SetPackageItemExtension("ui://ethkhr1hot0wf", typeof(UIBattleRelicInfo_Button_Arrow));
		UIObjectFactory.SetPackageItemExtension("ui://ethkhr1hot0wg", typeof(UIBattleRelicInfo_Button_Relic));
		UIObjectFactory.SetPackageItemExtension("ui://ethkhr1hrmeyh", typeof(UIBattleRelicInfo_Button_Chat));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		button = GetControllerAt(0);
		list_Tab = (GList)GetChildAt(0);
		btn_Close = (GButton)GetChildAt(2);
		list_RelicGroup = (GList)GetChildAt(3);
		txt_Name = (GTextField)GetChildAt(4);
		txt_Desc = (GRichTextField)GetChildAt(5);
		btn_Right = (UIBattleRelicInfo_Button_Arrow)GetChildAt(7);
		btn_Left = (UIBattleRelicInfo_Button_Arrow)GetChildAt(8);
		btn_Chat = (UIBattleRelicInfo_Button_Chat)GetChildAt(9);
		Cut_in = GetTransitionAt(0);
		Cut_out = GetTransitionAt(1);
	}
}
