using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIBattlePass_Button_SurpassReward : GButton
{
	public UIBattlePass_Button_SurpassItem btn_SurpassReward;

	public const string URL = "ui://ssf8xg9nb93d2a";

	public static UIBattlePass_Button_SurpassReward CreateInstance()
	{
		return (UIBattlePass_Button_SurpassReward)UIPackage.CreateObject("BattlePass", "BattlePass_Button_SurpassReward");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		btn_SurpassReward = (UIBattlePass_Button_SurpassItem)GetChildAt(0);
	}
}
