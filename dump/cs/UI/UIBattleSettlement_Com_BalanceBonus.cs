using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIBattleSettlement_Com_BalanceBonus : GComponent
{
	public GTextField txt_Bonus;

	public const string URL = "ui://avgradidw0q939";

	public static UIBattleSettlement_Com_BalanceBonus CreateInstance()
	{
		return (UIBattleSettlement_Com_BalanceBonus)UIPackage.CreateObject("BattleSettlement", "BattleSettlement_Com_BalanceBonus");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		txt_Bonus = (GTextField)GetChildAt(1);
	}
}
