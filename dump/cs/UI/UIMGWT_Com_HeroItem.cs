using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIMGWT_Com_HeroItem : GComponent
{
	public GGraph graph_Skin;

	public GTextField txt_title;

	public const string URL = "ui://2p754tqkilj51b";

	public static UIMGWT_Com_HeroItem CreateInstance()
	{
		return (UIMGWT_Com_HeroItem)UIPackage.CreateObject("MGWTStore", "MGWT_Com_HeroItem");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		graph_Skin = (GGraph)GetChildAt(0);
		txt_title = (GTextField)GetChildAt(1);
	}
}
