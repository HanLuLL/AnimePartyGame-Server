using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UICom_CostPoint : GComponent
{
	public Controller CpTyper;

	public GGraph graph_Effect;

	public const string URL = "ui://xuaw6o8j9wtj8h";

	public static UICom_CostPoint CreateInstance()
	{
		return (UICom_CostPoint)UIPackage.CreateObject("Common", "Com_CostPoint");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		CpTyper = GetControllerAt(0);
		graph_Effect = (GGraph)GetChildAt(3);
	}
}
