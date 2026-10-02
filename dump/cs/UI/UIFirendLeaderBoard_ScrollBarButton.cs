using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIFirendLeaderBoard_ScrollBarButton : GButton
{
	public GGraph grip;

	public const string URL = "ui://fmvvkhb4psca1u";

	public static UIFirendLeaderBoard_ScrollBarButton CreateInstance()
	{
		return (UIFirendLeaderBoard_ScrollBarButton)UIPackage.CreateObject("FriendLeaderboard", "FirendLeaderBoard_ScrollBarButton");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		grip = (GGraph)GetChildAt(0);
	}
}
