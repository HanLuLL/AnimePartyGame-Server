using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIFriend_Com_FriendItem : GComponent
{
	public Controller showWatch;

	public UIFriend_Button_FriendLabel btn_PlayerLabel;

	public GButton btn_Watch;

	public const string URL = "ui://2wec2q8z7h6s6";

	public static UIFriend_Com_FriendItem CreateInstance()
	{
		return (UIFriend_Com_FriendItem)UIPackage.CreateObject("Friend", "Friend_Com_FriendItem");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		showWatch = GetControllerAt(0);
		btn_PlayerLabel = (UIFriend_Button_FriendLabel)GetChildAt(0);
		btn_Watch = (GButton)GetChildAt(1);
	}
}
