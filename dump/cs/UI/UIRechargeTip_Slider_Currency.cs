using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIRechargeTip_Slider_Currency : GSlider
{
	public GTextField txt_Max;

	public const string URL = "ui://c20i191crle7q";

	public static UIRechargeTip_Slider_Currency CreateInstance()
	{
		return (UIRechargeTip_Slider_Currency)UIPackage.CreateObject("RechargeTip", "RechargeTip_Slider_Currency");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		txt_Max = (GTextField)GetChildAt(3);
	}
}
