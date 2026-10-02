using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UICheng : GComponent
{
	public Transition Cut_in;

	public const string URL = "ui://50xzye56r1gk1m";

	public static UICheng CreateInstance()
	{
		return (UICheng)UIPackage.CreateObject("AssistVoteS7", "橙");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		Cut_in = GetTransitionAt(0);
	}
}
