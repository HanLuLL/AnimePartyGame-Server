using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIGuide_Com_Arrow : GComponent
{
	public Transition LoopArrow;

	public const string URL = "ui://kogqu0l2ln3k3";

	public static UIGuide_Com_Arrow CreateInstance()
	{
		return (UIGuide_Com_Arrow)UIPackage.CreateObject("Guide", "Guide_Com_Arrow");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		LoopArrow = GetTransitionAt(0);
	}
}
