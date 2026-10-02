using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIFriend_Com_ApplyFriend : GComponent
{
	public GTextField txt_ShowTime;

	public GList list_Friends;

	public GButton btn_AllRefuse;

	public const string URL = "ui://2wec2q8z7h6sy";

	public static UIFriend_Com_ApplyFriend CreateInstance()
	{
		return (UIFriend_Com_ApplyFriend)UIPackage.CreateObject("Friend", "Friend_Com_ApplyFriend");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		txt_ShowTime = (GTextField)GetChildAt(3);
		list_Friends = (GList)GetChildAt(7);
		btn_AllRefuse = (GButton)GetChildAt(8);
	}
}
