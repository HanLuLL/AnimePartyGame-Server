using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIBattlePass_Com_DoubleRewardItem : GComponent
{
	public UIBattlePass_Button_SaleItem btn_PremiumReward;

	public UIBattlePass_Button_SaleItem btn_NormalReward;

	public Transition switchTrans;

	public const string URL = "ui://ssf8xg9njz241n";

	public static UIBattlePass_Com_DoubleRewardItem CreateInstance()
	{
		return (UIBattlePass_Com_DoubleRewardItem)UIPackage.CreateObject("BattlePass", "BattlePass_Com_DoubleRewardItem");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		btn_PremiumReward = (UIBattlePass_Button_SaleItem)GetChildAt(0);
		btn_NormalReward = (UIBattlePass_Button_SaleItem)GetChildAt(1);
		switchTrans = GetTransitionAt(0);
	}
}
