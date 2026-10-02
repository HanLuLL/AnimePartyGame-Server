using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIBattleSettlement_Com_BattleDataItem : GLabel
{
	public Controller type;

	public GTextField txt_Count;

	public UIBattleSettlement_Slider_Data slider_Data;

	public Transition Cut_in;

	public const string URL = "ui://avgradidqees2k";

	public static UIBattleSettlement_Com_BattleDataItem CreateInstance()
	{
		return (UIBattleSettlement_Com_BattleDataItem)UIPackage.CreateObject("BattleSettlement", "BattleSettlement_Com_BattleDataItem");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		type = GetControllerAt(0);
		txt_Count = (GTextField)GetChildAt(4);
		slider_Data = (UIBattleSettlement_Slider_Data)GetChildAt(6);
		Cut_in = GetTransitionAt(0);
	}
}
