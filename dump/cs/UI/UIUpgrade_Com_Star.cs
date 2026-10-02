using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIUpgrade_Com_Star : GComponent
{
	public GGraph graph_Effect;

	public Transition Star;

	public Transition Star2;

	public const string URL = "ui://6vzgbzmwesjwx";

	public static UIUpgrade_Com_Star CreateInstance()
	{
		return (UIUpgrade_Com_Star)UIPackage.CreateObject("Upgrade", "Upgrade_Com_Star");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		graph_Effect = (GGraph)GetChildAt(21);
		Star = GetTransitionAt(0);
		Star2 = GetTransitionAt(1);
	}
}
