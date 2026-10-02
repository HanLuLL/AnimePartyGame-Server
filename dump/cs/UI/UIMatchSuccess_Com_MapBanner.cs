using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIMatchSuccess_Com_MapBanner : GComponent
{
	public GLoader loader_Map;

	public const string URL = "ui://aepd0gr2r26s2";

	public static UIMatchSuccess_Com_MapBanner CreateInstance()
	{
		return (UIMatchSuccess_Com_MapBanner)UIPackage.CreateObject("MatchSuccess", "MatchSuccess_Com_MapBanner");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		loader_Map = (GLoader)GetChildAt(1);
	}
}
