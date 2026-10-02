using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIFirendLeaderBoard_label : GComponent
{
	public Controller Status;

	public GComponent com_PlayerLabel;

	public GTextField Ranking_text;

	public GTextField Score_text;

	public Transition AnXia;

	public Transition Cut_in;

	public const string URL = "ui://fmvvkhb4u13x1c";

	public static UIFirendLeaderBoard_label CreateInstance()
	{
		return (UIFirendLeaderBoard_label)UIPackage.CreateObject("FriendLeaderboard", "FirendLeaderBoard_label");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		Status = GetControllerAt(0);
		com_PlayerLabel = (GComponent)GetChildAt(1);
		Ranking_text = (GTextField)GetChildAt(2);
		Score_text = (GTextField)GetChildAt(3);
		AnXia = GetTransitionAt(0);
		Cut_in = GetTransitionAt(1);
	}
}
