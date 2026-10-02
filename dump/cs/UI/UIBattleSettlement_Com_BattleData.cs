using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIBattleSettlement_Com_BattleData : GComponent
{
	public Controller type;

	public UIBattleSettlement_Com_BattleDataItem com_killCount;

	public UIBattleSettlement_Com_BattleDataItem com_TotalDie;

	public UIBattleSettlement_Com_BattleDataItem com_TotalDamage;

	public UIBattleSettlement_Com_BattleDataItem com_TotalInjured;

	public UIBattleSettlement_Com_BattleDataItem com_TreatmentScore;

	public UIBattleSettlement_Com_BattleData_PVP com_PVP;

	public UIBattleSettlement_Com_BattleData_PVE com_PVE;

	public Transition Cut_in;

	public const string URL = "ui://avgradidqees2l";

	public static UIBattleSettlement_Com_BattleData CreateInstance()
	{
		return (UIBattleSettlement_Com_BattleData)UIPackage.CreateObject("BattleSettlement", "BattleSettlement_Com_BattleData");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		type = GetControllerAt(0);
		com_killCount = (UIBattleSettlement_Com_BattleDataItem)GetChildAt(2);
		com_TotalDie = (UIBattleSettlement_Com_BattleDataItem)GetChildAt(3);
		com_TotalDamage = (UIBattleSettlement_Com_BattleDataItem)GetChildAt(4);
		com_TotalInjured = (UIBattleSettlement_Com_BattleDataItem)GetChildAt(5);
		com_TreatmentScore = (UIBattleSettlement_Com_BattleDataItem)GetChildAt(6);
		com_PVP = (UIBattleSettlement_Com_BattleData_PVP)GetChildAt(7);
		com_PVE = (UIBattleSettlement_Com_BattleData_PVE)GetChildAt(8);
		Cut_in = GetTransitionAt(0);
	}
}
