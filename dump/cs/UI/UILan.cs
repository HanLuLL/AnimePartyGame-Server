using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UILan : GComponent
{
	public Transition Cut_in;

	public Transition SwapBlue;

	public Transition SwapRed;

	public Transition BackRed;

	public Transition BackBlue;

	public Transition PickRed;

	public Transition PickBlue;

	public Transition PK;

	public Transition SwapMid;

	public Transition PickMid;

	public const string URL = "ui://50xzye56r1gk1l";

	public static UILan CreateInstance()
	{
		return (UILan)UIPackage.CreateObject("AssistVoteS7", "蓝");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		Cut_in = GetTransitionAt(0);
		SwapBlue = GetTransitionAt(1);
		SwapRed = GetTransitionAt(2);
		BackRed = GetTransitionAt(3);
		BackBlue = GetTransitionAt(4);
		PickRed = GetTransitionAt(5);
		PickBlue = GetTransitionAt(6);
		PK = GetTransitionAt(7);
		SwapMid = GetTransitionAt(8);
		PickMid = GetTransitionAt(9);
	}
}
