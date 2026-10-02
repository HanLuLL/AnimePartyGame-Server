using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIUpgrade_Com_PVELabel : GComponent
{
	public Controller GameResult;

	public GGraph graph_FirstPlayer;

	public GGraph graph_SecondPlayer;

	public GGraph graph_ThirdPlayer;

	public GGraph graph_FourthPlayer;

	public Transition GG;

	public const string URL = "ui://6vzgbzmwot0w29";

	public static UIUpgrade_Com_PVELabel CreateInstance()
	{
		return (UIUpgrade_Com_PVELabel)UIPackage.CreateObject("Upgrade", "Upgrade_Com_PVELabel");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		GameResult = GetControllerAt(0);
		graph_FirstPlayer = (GGraph)GetChildAt(11);
		graph_SecondPlayer = (GGraph)GetChildAt(12);
		graph_ThirdPlayer = (GGraph)GetChildAt(13);
		graph_FourthPlayer = (GGraph)GetChildAt(14);
		GG = GetTransitionAt(0);
	}
}
