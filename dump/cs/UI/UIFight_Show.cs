using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIFight_Show : GComponent
{
	public GGraph loader_Movie;

	public GGraph graph_Mask;

	public Transition fightOver;

	public const string URL = "ui://8irq146hmjrug";

	public static UIFight_Show CreateInstance()
	{
		return (UIFight_Show)UIPackage.CreateObject("Fight", "Fight_Show");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		loader_Movie = (GGraph)GetChildAt(0);
		graph_Mask = (GGraph)GetChildAt(1);
		fightOver = GetTransitionAt(0);
	}
}
