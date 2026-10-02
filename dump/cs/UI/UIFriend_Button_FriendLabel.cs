using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIFriend_Button_FriendLabel : GButton
{
	public Controller friendStatus;

	public GGraph btn_friendLable;

	public GButton btn_FriendStatus;

	public GTextField txt_OfflineTime;

	public GComponent com_PlayerLabel;

	public Transition Cut_in;

	public const string URL = "ui://2wec2q8ztiv31p";

	public static UIFriend_Button_FriendLabel CreateInstance()
	{
		return (UIFriend_Button_FriendLabel)UIPackage.CreateObject("Friend", "Friend_Button_FriendLabel");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		friendStatus = GetControllerAt(0);
		btn_friendLable = (GGraph)GetChildAt(0);
		btn_FriendStatus = (GButton)GetChildAt(2);
		txt_OfflineTime = (GTextField)GetChildAt(3);
		com_PlayerLabel = (GComponent)GetChildAt(5);
		Cut_in = GetTransitionAt(0);
	}
}
