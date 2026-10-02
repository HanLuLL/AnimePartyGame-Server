using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIMatch_Com_MapBanner : GComponent
{
	public GLoader loader_Map;

	public const string URL = "ui://qxwapsemr26s1c";

	public static UIMatch_Com_MapBanner CreateInstance()
	{
		return (UIMatch_Com_MapBanner)UIPackage.CreateObject("Match", "Match_Com_MapBanner");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		loader_Map = (GLoader)GetChildAt(1);
	}
}
