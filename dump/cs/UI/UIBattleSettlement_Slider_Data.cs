using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIBattleSettlement_Slider_Data : GProgressBar
{
	public Controller type;

	public const string URL = "ui://avgradidqees2j";

	public static UIBattleSettlement_Slider_Data CreateInstance()
	{
		return (UIBattleSettlement_Slider_Data)UIPackage.CreateObject("BattleSettlement", "BattleSettlement_Slider_Data");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		type = GetControllerAt(0);
	}
}
