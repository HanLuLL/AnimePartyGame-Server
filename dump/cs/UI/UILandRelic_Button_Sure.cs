using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UILandRelic_Button_Sure : GButton
{
	public GRichTextField txt_Desc;

	public const string URL = "ui://avtf6i27y7ym4";

	public static UILandRelic_Button_Sure CreateInstance()
	{
		return (UILandRelic_Button_Sure)UIPackage.CreateObject("LandRelic", "LandRelic_Button_Sure");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		txt_Desc = (GRichTextField)GetChildAt(4);
	}
}
