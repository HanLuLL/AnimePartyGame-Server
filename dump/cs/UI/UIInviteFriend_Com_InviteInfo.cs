using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIInviteFriend_Com_InviteInfo : GComponent
{
	public Controller button;

	public Controller available;

	public GComponent com_PlayerLabel;

	public GComponent com_FriendStatus;

	public GButton btn_Join;

	public GTextField txt_Time;

	public const string URL = "ui://24i5r2i07h6s8";

	public static UIInviteFriend_Com_InviteInfo CreateInstance()
	{
		return (UIInviteFriend_Com_InviteInfo)UIPackage.CreateObject("Invite", "InviteFriend_Com_InviteInfo");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		button = GetControllerAt(0);
		available = GetControllerAt(1);
		com_PlayerLabel = (GComponent)GetChildAt(1);
		com_FriendStatus = (GComponent)GetChildAt(2);
		btn_Join = (GButton)GetChildAt(3);
		txt_Time = (GTextField)GetChildAt(4);
	}
}
