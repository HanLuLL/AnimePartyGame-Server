using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UITutorial_Com_Arrow : GComponent
{
	public Transition LoopArrow;

	public const string URL = "ui://b96qpoz68vxw1";

	public static UITutorial_Com_Arrow CreateInstance()
	{
		return (UITutorial_Com_Arrow)UIPackage.CreateObject("Tutorial", "Tutorial_Com_Arrow");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		LoopArrow = GetTransitionAt(0);
	}
}
