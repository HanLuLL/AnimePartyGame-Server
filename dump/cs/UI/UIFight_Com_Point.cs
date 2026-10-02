using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIFight_Com_Point : GComponent
{
	public Controller player;

	public GMovieClip aMovie_Dice;

	public GImage FightDiceBG;

	public GGraph graph_Effect;

	public GTextField txt_Point;

	public GTextField txt_max;

	public GGroup group_Point;

	public GImage Image_DiceEffect;

	public Transition PointChange;

	public Transition MaxPoint;

	public Transition ShowDiceEffect;

	public const string URL = "ui://8irq146ha90c4p";

	public static UIFight_Com_Point CreateInstance()
	{
		return (UIFight_Com_Point)UIPackage.CreateObject("Fight", "Fight_Com_Point");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		player = GetControllerAt(0);
		aMovie_Dice = (GMovieClip)GetChildAt(5);
		FightDiceBG = (GImage)GetChildAt(6);
		graph_Effect = (GGraph)GetChildAt(7);
		txt_Point = (GTextField)GetChildAt(8);
		txt_max = (GTextField)GetChildAt(9);
		group_Point = (GGroup)GetChildAt(10);
		Image_DiceEffect = (GImage)GetChildAt(17);
		PointChange = GetTransitionAt(0);
		MaxPoint = GetTransitionAt(1);
		ShowDiceEffect = GetTransitionAt(2);
	}
}
