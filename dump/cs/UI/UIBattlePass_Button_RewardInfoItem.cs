using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIBattlePass_Button_RewardInfoItem : GButton
{
	public Controller normalStatus;

	public Controller premiumStatus;

	public Controller showNormal;

	public Controller showPremium;

	public GLoader loader_Normal;

	public GTextField txt_NormalContent;

	public GGroup left;

	public GLoader loader_Premium;

	public GTextField txt_PremiumContent;

	public GGroup right;

	public const string URL = "ui://ssf8xg9njz241u";

	public static UIBattlePass_Button_RewardInfoItem CreateInstance()
	{
		return (UIBattlePass_Button_RewardInfoItem)UIPackage.CreateObject("BattlePass", "BattlePass_Button_RewardInfoItem");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		normalStatus = GetControllerAt(1);
		premiumStatus = GetControllerAt(2);
		showNormal = GetControllerAt(3);
		showPremium = GetControllerAt(4);
		loader_Normal = (GLoader)GetChildAt(1);
		txt_NormalContent = (GTextField)GetChildAt(2);
		left = (GGroup)GetChildAt(4);
		loader_Premium = (GLoader)GetChildAt(6);
		txt_PremiumContent = (GTextField)GetChildAt(7);
		right = (GGroup)GetChildAt(9);
	}
}
