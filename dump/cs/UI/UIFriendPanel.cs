using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIFriendPanel : GComponent
{
	public Controller type;

	public UIFriend_Com_FriendList com_Friends;

	public UIFriend_Com_AddFriend com_AddFriend;

	public UIFriend_Com_ApplyFriend com_ApplyFriend;

	public UIFriend_Com_BlackList com_BlackList;

	public UIFriend_Button_Tab btn_TabFriends;

	public UIFriend_Button_Tab btn_TabAdd;

	public UIFriend_Button_Tab btn_TabApply;

	public UIFriend_Button_Tab btn_TabBlacklist;

	public GButton btn_Return;

	public Transition Cut_in;

	public const string URL = "ui://2wec2q8z7h6s0";

	public static UIFriendPanel CreateInstance()
	{
		BindAll();
		return (UIFriendPanel)UIPackage.CreateObject("Friend", "FriendPanel");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://2wec2q8z7h6s0", typeof(UIFriendPanel));
		UIObjectFactory.SetPackageItemExtension("ui://2wec2q8z7h6s6", typeof(UIFriend_Com_FriendItem));
		UIObjectFactory.SetPackageItemExtension("ui://2wec2q8z7h6s9", typeof(UIFriend_Com_AddFriend));
		UIObjectFactory.SetPackageItemExtension("ui://2wec2q8z7h6sf", typeof(UIFriend_Com_FriendList));
		UIObjectFactory.SetPackageItemExtension("ui://2wec2q8z7h6sj", typeof(UIFriend_Button_Tab));
		UIObjectFactory.SetPackageItemExtension("ui://2wec2q8z7h6sw", typeof(UIFriend_Com_BlackList));
		UIObjectFactory.SetPackageItemExtension("ui://2wec2q8z7h6sx", typeof(UIFriend_Com_ApplyItem));
		UIObjectFactory.SetPackageItemExtension("ui://2wec2q8z7h6sy", typeof(UIFriend_Com_ApplyFriend));
		UIObjectFactory.SetPackageItemExtension("ui://2wec2q8znzp81v", typeof(UIFriend_Com_ComboBox));
		UIObjectFactory.SetPackageItemExtension("ui://2wec2q8znzp81y", typeof(UIFriend_Com_ComboBox_popup));
		UIObjectFactory.SetPackageItemExtension("ui://2wec2q8ztiv31p", typeof(UIFriend_Button_FriendLabel));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		type = GetControllerAt(0);
		com_Friends = (UIFriend_Com_FriendList)GetChildAt(0);
		com_AddFriend = (UIFriend_Com_AddFriend)GetChildAt(1);
		com_ApplyFriend = (UIFriend_Com_ApplyFriend)GetChildAt(2);
		com_BlackList = (UIFriend_Com_BlackList)GetChildAt(3);
		btn_TabFriends = (UIFriend_Button_Tab)GetChildAt(4);
		btn_TabAdd = (UIFriend_Button_Tab)GetChildAt(5);
		btn_TabApply = (UIFriend_Button_Tab)GetChildAt(6);
		btn_TabBlacklist = (UIFriend_Button_Tab)GetChildAt(7);
		btn_Return = (GButton)GetChildAt(9);
		Cut_in = GetTransitionAt(0);
	}
}
