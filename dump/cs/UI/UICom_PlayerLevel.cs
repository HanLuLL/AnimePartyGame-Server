using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UICom_PlayerLevel : GComponent
{
	public Controller avtiveLevel;

	public Controller slot;

	public GGraph graph_Effect;

	public Transition Cut_in;

	public const string URL = "ui://xuaw6o8jimo37v";

	public static UICom_PlayerLevel CreateInstance()
	{
		return (UICom_PlayerLevel)UIPackage.CreateObject("Common", "Com_PlayerLevel");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		avtiveLevel = GetControllerAt(0);
		slot = GetControllerAt(1);
		graph_Effect = (GGraph)GetChildAt(5);
		Cut_in = GetTransitionAt(0);
	}
}
