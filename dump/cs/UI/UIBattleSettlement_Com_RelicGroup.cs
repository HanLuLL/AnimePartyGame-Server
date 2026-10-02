using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIBattleSettlement_Com_RelicGroup : GComponent
{
	public UIBattleSettlement_Button_Relic btn_RelicItem_1;

	public UIBattleSettlement_Button_Relic btn_RelicItem_2;

	public UIBattleSettlement_Button_Relic btn_RelicItem_3;

	public UIBattleSettlement_Button_Relic btn_RelicItem_4;

	public UIBattleSettlement_Button_Relic btn_RelicItem_5;

	public UIBattleSettlement_Button_Relic btn_RelicItem_6;

	public UIBattleSettlement_Button_Relic btn_RelicItem_7;

	public Transition Cut_in;

	public const string URL = "ui://avgradidw0q938";

	public static UIBattleSettlement_Com_RelicGroup CreateInstance()
	{
		return (UIBattleSettlement_Com_RelicGroup)UIPackage.CreateObject("BattleSettlement", "BattleSettlement_Com_RelicGroup");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		btn_RelicItem_1 = (UIBattleSettlement_Button_Relic)GetChildAt(0);
		btn_RelicItem_2 = (UIBattleSettlement_Button_Relic)GetChildAt(1);
		btn_RelicItem_3 = (UIBattleSettlement_Button_Relic)GetChildAt(2);
		btn_RelicItem_4 = (UIBattleSettlement_Button_Relic)GetChildAt(3);
		btn_RelicItem_5 = (UIBattleSettlement_Button_Relic)GetChildAt(4);
		btn_RelicItem_6 = (UIBattleSettlement_Button_Relic)GetChildAt(5);
		btn_RelicItem_7 = (UIBattleSettlement_Button_Relic)GetChildAt(6);
		Cut_in = GetTransitionAt(0);
	}
}
