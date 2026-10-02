using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UICom_CardBack : GComponent
{
	public GLoader loader_CardBack;

	public GGraph graph_Effect;

	public const string URL = "ui://xuaw6o8jh334q3o";

	public static UICom_CardBack CreateInstance()
	{
		return (UICom_CardBack)UIPackage.CreateObject("Common", "Com_CardBack");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		loader_CardBack = (GLoader)GetChildAt(0);
		graph_Effect = (GGraph)GetChildAt(1);
	}
}
