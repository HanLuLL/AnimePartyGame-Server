using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIFirendLeaderBoard_Com_BG : GComponent
{
	public GLoader loader_Bg;

	public const string URL = "ui://fmvvkhb4ms3m1s";

	public static UIFirendLeaderBoard_Com_BG CreateInstance()
	{
		return (UIFirendLeaderBoard_Com_BG)UIPackage.CreateObject("FriendLeaderboard", "FirendLeaderBoard_Com_BG");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		loader_Bg = (GLoader)GetChildAt(2);
	}
}
