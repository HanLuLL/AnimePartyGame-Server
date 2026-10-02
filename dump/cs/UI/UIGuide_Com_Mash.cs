using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIGuide_Com_Mash : GComponent
{
	public GGraph graph_RectMask;

	public GGraph graph_CircularMask;

	public const string URL = "ui://kogqu0l2hj7w1";

	public static UIGuide_Com_Mash CreateInstance()
	{
		return (UIGuide_Com_Mash)UIPackage.CreateObject("Guide", "Guide_Com_Mash");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		graph_RectMask = (GGraph)GetChildAt(1);
		graph_CircularMask = (GGraph)GetChildAt(2);
	}
}
