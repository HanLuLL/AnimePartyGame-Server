using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIExplain_Com_PVP : GComponent
{
	public GLabel bottom;

	public GTextField txt_GoldLV1;

	public GTextField txt_GoldLV2;

	public GTextField txt_GoldLV3;

	public const string URL = "ui://5wai1nhyl6o66";

	public static UIExplain_Com_PVP CreateInstance()
	{
		return (UIExplain_Com_PVP)UIPackage.CreateObject("Explain", "Explain_Com_PVP");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		bottom = (GLabel)GetChildAt(0);
		txt_GoldLV1 = (GTextField)GetChildAt(15);
		txt_GoldLV2 = (GTextField)GetChildAt(16);
		txt_GoldLV3 = (GTextField)GetChildAt(17);
	}
}
