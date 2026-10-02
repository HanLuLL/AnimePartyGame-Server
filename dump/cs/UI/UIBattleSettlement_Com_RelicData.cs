using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIBattleSettlement_Com_RelicData : GComponent
{
	public GList list_RelicGroup;

	public GGraph btn_ChangeWin;

	public const string URL = "ui://avgradidw0q933";

	public static UIBattleSettlement_Com_RelicData CreateInstance()
	{
		return (UIBattleSettlement_Com_RelicData)UIPackage.CreateObject("BattleSettlement", "BattleSettlement_Com_RelicData");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		list_RelicGroup = (GList)GetChildAt(2);
		btn_ChangeWin = (GGraph)GetChildAt(3);
	}
}
