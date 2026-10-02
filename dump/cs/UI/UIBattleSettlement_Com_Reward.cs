using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIBattleSettlement_Com_Reward : GComponent
{
	public GLoader loader_Icon;

	public GTextField txt_Name;

	public GTextField txt_Count;

	public UIBattleSettlement_Com_BalanceBonus com_Bonus;

	public GButton com_NoviceUP;

	public Transition Cut_in;

	public const string URL = "ui://avgradidw0q93c";

	public static UIBattleSettlement_Com_Reward CreateInstance()
	{
		return (UIBattleSettlement_Com_Reward)UIPackage.CreateObject("BattleSettlement", "BattleSettlement_Com_Reward");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		loader_Icon = (GLoader)GetChildAt(0);
		txt_Name = (GTextField)GetChildAt(1);
		txt_Count = (GTextField)GetChildAt(2);
		com_Bonus = (UIBattleSettlement_Com_BalanceBonus)GetChildAt(3);
		com_NoviceUP = (GButton)GetChildAt(4);
		Cut_in = GetTransitionAt(0);
	}
}
