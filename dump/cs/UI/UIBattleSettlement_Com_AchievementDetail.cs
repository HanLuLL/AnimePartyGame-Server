using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIBattleSettlement_Com_AchievementDetail : GComponent
{
	public GLoader loader_Achieve;

	public GTextField txt_Name;

	public GTextField txt_Desc;

	public GTextField txt_Value;

	public Transition Cut_in;

	public Transition Cut_out;

	public Transition CJ_Cut_in;

	public Transition CJ_Cut_out;

	public const string URL = "ui://avgradidw0q93j";

	public static UIBattleSettlement_Com_AchievementDetail CreateInstance()
	{
		return (UIBattleSettlement_Com_AchievementDetail)UIPackage.CreateObject("BattleSettlement", "BattleSettlement_Com_AchievementDetail");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		loader_Achieve = (GLoader)GetChildAt(2);
		txt_Name = (GTextField)GetChildAt(3);
		txt_Desc = (GTextField)GetChildAt(4);
		txt_Value = (GTextField)GetChildAt(5);
		Cut_in = GetTransitionAt(0);
		Cut_out = GetTransitionAt(1);
		CJ_Cut_in = GetTransitionAt(2);
		CJ_Cut_out = GetTransitionAt(3);
	}
}
