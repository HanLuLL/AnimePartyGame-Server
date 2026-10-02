using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIAssistVote_Com_VoteItem : GComponent
{
	public Controller status;

	public Controller Player;

	public GComponent com_Point;

	public GLoader loader_Icon;

	public Transition Cut_in;

	public Transition OK;

	public Transition Roll;

	public const string URL = "ui://andqlvspec7w4";

	public static UIAssistVote_Com_VoteItem CreateInstance()
	{
		return (UIAssistVote_Com_VoteItem)UIPackage.CreateObject("AssistVote", "AssistVote_Com_VoteItem");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		status = GetControllerAt(0);
		Player = GetControllerAt(1);
		com_Point = (GComponent)GetChildAt(0);
		loader_Icon = (GLoader)GetChildAt(1);
		Cut_in = GetTransitionAt(0);
		OK = GetTransitionAt(1);
		Roll = GetTransitionAt(2);
	}
}
