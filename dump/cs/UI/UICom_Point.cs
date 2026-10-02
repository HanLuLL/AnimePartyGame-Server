using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UICom_Point : GComponent
{
	public Controller player;

	public GMovieClip aMovie_Dice;

	public GGraph graph_Effect;

	public GTextField txt_Point;

	public GTextField txt_max;

	public GGroup group_Point;

	public Transition PointChange;

	public Transition MaxPoint;

	public const string URL = "ui://1ov1i0v9bczmcc";

	public static UICom_Point CreateInstance()
	{
		return (UICom_Point)UIPackage.CreateObject("Common_Internal", "Com_Point");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		player = GetControllerAt(0);
		aMovie_Dice = (GMovieClip)GetChildAt(2);
		graph_Effect = (GGraph)GetChildAt(5);
		txt_Point = (GTextField)GetChildAt(6);
		txt_max = (GTextField)GetChildAt(7);
		group_Point = (GGroup)GetChildAt(8);
		PointChange = GetTransitionAt(0);
		MaxPoint = GetTransitionAt(1);
	}
}
