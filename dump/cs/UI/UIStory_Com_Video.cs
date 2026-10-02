using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIStory_Com_Video : GComponent
{
	public GGraph graph_LoopVideo;

	public GGraph graph_CutInVideo;

	public const string URL = "ui://abmw5cforct9o";

	public static UIStory_Com_Video CreateInstance()
	{
		return (UIStory_Com_Video)UIPackage.CreateObject("Story", "Story_Com_Video");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		graph_LoopVideo = (GGraph)GetChildAt(0);
		graph_CutInVideo = (GGraph)GetChildAt(1);
	}
}
