using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIBattleSettlement_Com_BattleData_PVP : GComponent
{
	public Controller slot;

	public Controller rand;

	public GList list_Level;

	public GTextField txt_GoldCount;

	public Transition Cut_in;

	public const string URL = "ui://avgradidw0q931";

	public static UIBattleSettlement_Com_BattleData_PVP CreateInstance()
	{
		return (UIBattleSettlement_Com_BattleData_PVP)UIPackage.CreateObject("BattleSettlement", "BattleSettlement_Com_BattleData_PVP");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		slot = GetControllerAt(0);
		rand = GetControllerAt(1);
		list_Level = (GList)GetChildAt(7);
		txt_GoldCount = (GTextField)GetChildAt(9);
		Cut_in = GetTransitionAt(0);
	}
}
