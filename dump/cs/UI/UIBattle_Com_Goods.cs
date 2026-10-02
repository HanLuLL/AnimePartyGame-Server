using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIBattle_Com_Goods : GComponent
{
	public Controller status;

	public UIBattlePass_Button_Purchase btn_PurchaseNormalGoods;

	public GList list_NormalGoods;

	public GGroup group_NormalGoods;

	public UIBattlePass_Button_Purchase btn_PurchasePremiumGoods;

	public GList list_PremiumGoods;

	public GGroup group_PremiumGoods;

	public GButton btn_RewardInfo;

	public const string URL = "ui://ssf8xg9njz241j";

	public static UIBattle_Com_Goods CreateInstance()
	{
		return (UIBattle_Com_Goods)UIPackage.CreateObject("BattlePass", "Battle_Com_Goods");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		status = GetControllerAt(0);
		btn_PurchaseNormalGoods = (UIBattlePass_Button_Purchase)GetChildAt(1);
		list_NormalGoods = (GList)GetChildAt(2);
		group_NormalGoods = (GGroup)GetChildAt(3);
		btn_PurchasePremiumGoods = (UIBattlePass_Button_Purchase)GetChildAt(5);
		list_PremiumGoods = (GList)GetChildAt(6);
		group_PremiumGoods = (GGroup)GetChildAt(7);
		btn_RewardInfo = (GButton)GetChildAt(8);
	}
}
