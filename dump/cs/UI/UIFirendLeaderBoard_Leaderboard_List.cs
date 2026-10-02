using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIFirendLeaderBoard_Leaderboard_List : GComponent
{
	public Controller Status;

	public GList list_friend;

	public const string URL = "ui://fmvvkhb4u13x1b";

	public static UIFirendLeaderBoard_Leaderboard_List CreateInstance()
	{
		return (UIFirendLeaderBoard_Leaderboard_List)UIPackage.CreateObject("FriendLeaderboard", "FirendLeaderBoard_Leaderboard_List");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		Status = GetControllerAt(0);
		list_friend = (GList)GetChildAt(0);
	}
}
