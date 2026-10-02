using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIHome_Com_BGSet : GComponent
{
	public GGraph loader_Video;

	public const string URL = "ui://u7xbdcgupgrvq4h";

	public static UIHome_Com_BGSet CreateInstance()
	{
		return (UIHome_Com_BGSet)UIPackage.CreateObject("Home", "Home_Com_BGSet");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		loader_Video = (GGraph)GetChildAt(0);
	}
}
