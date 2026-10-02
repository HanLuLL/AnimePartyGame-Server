using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIBattlePass_Com_RewardInfo : GComponent
{
	public GGraph mohu;

	public GLoader loader_Normal;

	public GLoader loader_Premium;

	public GTextField txt_NormalTitle;

	public GTextField txt_PremiumTitle;

	public GList list_Rewards;

	public GButton btn_Close;

	public const string URL = "ui://ssf8xg9njz241s";

	public static UIBattlePass_Com_RewardInfo CreateInstance()
	{
		return (UIBattlePass_Com_RewardInfo)UIPackage.CreateObject("BattlePass", "BattlePass_Com_RewardInfo");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		mohu = (GGraph)GetChildAt(0);
		loader_Normal = (GLoader)GetChildAt(2);
		loader_Premium = (GLoader)GetChildAt(3);
		txt_NormalTitle = (GTextField)GetChildAt(5);
		txt_PremiumTitle = (GTextField)GetChildAt(6);
		list_Rewards = (GList)GetChildAt(7);
		btn_Close = (GButton)GetChildAt(8);
	}
}
