using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIBattlePass_Com_DoubleReward : GComponent
{
	public UIBattlePass_Button_FreeItem btn_FreeItem;

	public UIBattlePass_Com_DoubleRewardItem com_DoubleReward;

	public UIBattlePass_Progress_Item progress_Exp;

	public const string URL = "ui://ssf8xg9njz241m";

	public static UIBattlePass_Com_DoubleReward CreateInstance()
	{
		return (UIBattlePass_Com_DoubleReward)UIPackage.CreateObject("BattlePass", "BattlePass_Com_DoubleReward");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		btn_FreeItem = (UIBattlePass_Button_FreeItem)GetChildAt(0);
		com_DoubleReward = (UIBattlePass_Com_DoubleRewardItem)GetChildAt(1);
		progress_Exp = (UIBattlePass_Progress_Item)GetChildAt(2);
	}
}
