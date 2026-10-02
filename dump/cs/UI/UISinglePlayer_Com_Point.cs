using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UISinglePlayer_Com_Point : GComponent
{
	public Controller player;

	public GMovieClip aMovie_Dice;

	public GTextField txt_Point;

	public Transition PointChange;

	public Transition Roll_6;

	public Transition Roll_1_5;

	public const string URL = "ui://mi9vm3w0kvexq4a";

	public static UISinglePlayer_Com_Point CreateInstance()
	{
		return (UISinglePlayer_Com_Point)UIPackage.CreateObject("SinglePlayer", "SinglePlayer_Com_Point");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		player = GetControllerAt(0);
		aMovie_Dice = (GMovieClip)GetChildAt(0);
		txt_Point = (GTextField)GetChildAt(2);
		PointChange = GetTransitionAt(0);
		Roll_6 = GetTransitionAt(1);
		Roll_1_5 = GetTransitionAt(2);
	}
}
