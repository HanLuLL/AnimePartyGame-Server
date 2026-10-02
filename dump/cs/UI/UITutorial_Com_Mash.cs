using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UITutorial_Com_Mash : GComponent
{
	public GGraph graph_RectMask;

	public GGraph graph_CircularMask;

	public UITutorial_Com_Arrow com_Arrow;

	public const string URL = "ui://b96qpoz68vxw2";

	public static UITutorial_Com_Mash CreateInstance()
	{
		return (UITutorial_Com_Mash)UIPackage.CreateObject("Tutorial", "Tutorial_Com_Mash");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		graph_RectMask = (GGraph)GetChildAt(1);
		graph_CircularMask = (GGraph)GetChildAt(2);
		com_Arrow = (UITutorial_Com_Arrow)GetChildAt(3);
	}
}
