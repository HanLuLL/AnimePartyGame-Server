using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIBattleSettlement_Com_LimitReward : GComponent
{
	public UIBattleSettlement_Com_Reward com_Reward;

	public GTextField txt_LimitCount;

	public Transition Cut_in;

	public const string URL = "ui://avgradidw0q93a";

	public static UIBattleSettlement_Com_LimitReward CreateInstance()
	{
		return (UIBattleSettlement_Com_LimitReward)UIPackage.CreateObject("BattleSettlement", "BattleSettlement_Com_LimitReward");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		com_Reward = (UIBattleSettlement_Com_Reward)GetChildAt(0);
		txt_LimitCount = (GTextField)GetChildAt(2);
		Cut_in = GetTransitionAt(0);
	}
}
