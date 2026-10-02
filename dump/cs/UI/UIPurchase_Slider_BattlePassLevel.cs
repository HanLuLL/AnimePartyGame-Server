using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIPurchase_Slider_BattlePassLevel : GSlider
{
	public GTextField txt_Max;

	public GTextField txt_Min;

	public const string URL = "ui://cu17piy4jz241h";

	public static UIPurchase_Slider_BattlePassLevel CreateInstance()
	{
		return (UIPurchase_Slider_BattlePassLevel)UIPackage.CreateObject("Purchase", "Purchase_Slider_BattlePassLevel");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		txt_Max = (GTextField)GetChildAt(3);
		txt_Min = (GTextField)GetChildAt(5);
	}
}
