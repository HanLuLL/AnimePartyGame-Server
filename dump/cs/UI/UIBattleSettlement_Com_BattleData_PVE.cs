using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIBattleSettlement_Com_BattleData_PVE : GComponent
{
	public GTextField txt_GoldCount;

	public GTextField txt_TranGoldCount;

	public UIBattleSettlement_Com_RelicData com_Relic;

	public Transition Expand;

	public Transition Folder;

	public Transition Cut_in;

	public const string URL = "ui://avgradidw0q932";

	public static UIBattleSettlement_Com_BattleData_PVE CreateInstance()
	{
		return (UIBattleSettlement_Com_BattleData_PVE)UIPackage.CreateObject("BattleSettlement", "BattleSettlement_Com_BattleData_PVE");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		txt_GoldCount = (GTextField)GetChildAt(6);
		txt_TranGoldCount = (GTextField)GetChildAt(8);
		com_Relic = (UIBattleSettlement_Com_RelicData)GetChildAt(9);
		Expand = GetTransitionAt(0);
		Folder = GetTransitionAt(1);
		Cut_in = GetTransitionAt(2);
	}
}
