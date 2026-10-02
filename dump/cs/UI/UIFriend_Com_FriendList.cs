using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIFriend_Com_FriendList : GComponent
{
	public GTextField txt_FriendsNum;

	public GList list_Friends;

	public UIFriend_Com_ComboBox com_State;

	public GTextField Txt_State;

	public const string URL = "ui://2wec2q8z7h6sf";

	public static UIFriend_Com_FriendList CreateInstance()
	{
		return (UIFriend_Com_FriendList)UIPackage.CreateObject("Friend", "Friend_Com_FriendList");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		txt_FriendsNum = (GTextField)GetChildAt(3);
		list_Friends = (GList)GetChildAt(7);
		com_State = (UIFriend_Com_ComboBox)GetChildAt(10);
		Txt_State = (GTextField)GetChildAt(11);
	}
}
