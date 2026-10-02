using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIInviteWindow : GComponent
{
	public Controller type;

	public Controller tab;

	public GGraph mohu;

	public GButton btn_Friend;

	public GButton btn_LastTeam;

	public GTextField txt_NotFriends;

	public GTextField txt_NotLastFriends;

	public GList list_PlayerLabel;

	public GTextField txt_TypeExplain;

	public GButton btn_Invite;

	public GList list_InviteInfo;

	public GTextField txt_inviteText;

	public GButton btn_ClearInfo;

	public const string URL = "ui://24i5r2i07h6s0";

	public static UIInviteWindow CreateInstance()
	{
		BindAll();
		return (UIInviteWindow)UIPackage.CreateObject("Invite", "InviteWindow");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://24i5r2i07h6s0", typeof(UIInviteWindow));
		UIObjectFactory.SetPackageItemExtension("ui://24i5r2i07h6s5", typeof(UIInviteFriend_Button_PlayerItem));
		UIObjectFactory.SetPackageItemExtension("ui://24i5r2i07h6s8", typeof(UIInviteFriend_Com_InviteInfo));
		UIObjectFactory.SetPackageItemExtension("ui://24i5r2i07h6sk", typeof(UIInviteFriend_Button_Grip));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		type = GetControllerAt(0);
		tab = GetControllerAt(1);
		mohu = (GGraph)GetChildAt(0);
		btn_Friend = (GButton)GetChildAt(2);
		btn_LastTeam = (GButton)GetChildAt(3);
		txt_NotFriends = (GTextField)GetChildAt(5);
		txt_NotLastFriends = (GTextField)GetChildAt(6);
		list_PlayerLabel = (GList)GetChildAt(7);
		txt_TypeExplain = (GTextField)GetChildAt(8);
		btn_Invite = (GButton)GetChildAt(9);
		list_InviteInfo = (GList)GetChildAt(11);
		txt_inviteText = (GTextField)GetChildAt(12);
		btn_ClearInfo = (GButton)GetChildAt(13);
	}
}
