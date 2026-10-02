using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIBattlePass_Com_Reward : GComponent
{
	public UIBattlePass_Button_FreeItem btn_FreeItem;

	public UIBattlePass_Progress_Item progress_Exp;

	public UIBattlePass_Button_SaleItem btn_NormalReward;

	public const string URL = "ui://ssf8xg9njz2418";

	public static UIBattlePass_Com_Reward CreateInstance()
	{
		return (UIBattlePass_Com_Reward)UIPackage.CreateObject("BattlePass", "BattlePass_Com_Reward");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		btn_FreeItem = (UIBattlePass_Button_FreeItem)GetChildAt(0);
		progress_Exp = (UIBattlePass_Progress_Item)GetChildAt(1);
		btn_NormalReward = (UIBattlePass_Button_SaleItem)GetChildAt(2);
	}
}
