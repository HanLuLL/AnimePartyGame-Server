using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIFriendLeaderboardWindow : GComponent
{
	public GGraph mohu;

	public UIFirendLeaderBoard_Leaderboard_List list_FriendLeaderboard;

	public UIFirendLeaderBoard_label Me_Data;

	public GButton FriendClose_btn;

	public Transition Cut_in;

	public const string URL = "ui://fmvvkhb4u13x18";

	public static UIFriendLeaderboardWindow CreateInstance()
	{
		BindAll();
		return (UIFriendLeaderboardWindow)UIPackage.CreateObject("FriendLeaderboard", "FriendLeaderboardWindow");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://fmvvkhb4ms3m1s", typeof(UIFirendLeaderBoard_Com_BG));
		UIObjectFactory.SetPackageItemExtension("ui://fmvvkhb4psca1u", typeof(UIFirendLeaderBoard_ScrollBarButton));
		UIObjectFactory.SetPackageItemExtension("ui://fmvvkhb4u13x18", typeof(UIFriendLeaderboardWindow));
		UIObjectFactory.SetPackageItemExtension("ui://fmvvkhb4u13x1b", typeof(UIFirendLeaderBoard_Leaderboard_List));
		UIObjectFactory.SetPackageItemExtension("ui://fmvvkhb4u13x1c", typeof(UIFirendLeaderBoard_label));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		mohu = (GGraph)GetChildAt(0);
		list_FriendLeaderboard = (UIFirendLeaderBoard_Leaderboard_List)GetChildAt(3);
		Me_Data = (UIFirendLeaderBoard_label)GetChildAt(4);
		FriendClose_btn = (GButton)GetChildAt(5);
		Cut_in = GetTransitionAt(0);
	}
}
