using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIBattlePassPanel : GComponent
{
	public Controller tab;

	public Controller showRewardInfo;

	public GLoader loader_Main;

	public GLoader loader_Task;

	public GLoader loader_Effect;

	public GLoader loader_Skin;

	public GTextField txt_SkinName;

	public GButton btn_PreviewSkin;

	public UIBattlePass_Com_ExpProgress com_Progress;

	public UIBattlePass_Button_Purchase btn_GoPurchase;

	public GList list_BattlePass;

	public GTextField txt_DailyTime;

	public GTextField txt_WeeklyTime;

	public GList list_DailyTask;

	public GList list_WeeklyTask;

	public UIBattle_Com_Goods com_Goods;

	public UIBattlePass_Button_Tab btn_battlePass;

	public UIBattlePass_Button_Tab btn_Task;

	public GButton btn_Return;

	public UIBattlePass_Com_RewardInfo com_RewardInfo;

	public Transition Cutin;

	public const string URL = "ui://ssf8xg9nf7iw0";

	public static UIBattlePassPanel CreateInstance()
	{
		BindAll();
		return (UIBattlePassPanel)UIPackage.CreateObject("BattlePass", "BattlePassPanel");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://ssf8xg9nb93d29", typeof(UIBattlePass_Button_SurpassItem));
		UIObjectFactory.SetPackageItemExtension("ui://ssf8xg9nb93d2a", typeof(UIBattlePass_Button_SurpassReward));
		UIObjectFactory.SetPackageItemExtension("ui://ssf8xg9nf7iw0", typeof(UIBattlePassPanel));
		UIObjectFactory.SetPackageItemExtension("ui://ssf8xg9njz2411", typeof(UIBattlePass_Button_Purchase));
		UIObjectFactory.SetPackageItemExtension("ui://ssf8xg9njz2412", typeof(UIBattlePass_Com_ExpProgress));
		UIObjectFactory.SetPackageItemExtension("ui://ssf8xg9njz2414", typeof(UIBattlePass_Progress_Item));
		UIObjectFactory.SetPackageItemExtension("ui://ssf8xg9njz2415", typeof(UIBattlePass_Button_GoPurchaseLv));
		UIObjectFactory.SetPackageItemExtension("ui://ssf8xg9njz2416", typeof(UIBattlePass_Button_FreeItem));
		UIObjectFactory.SetPackageItemExtension("ui://ssf8xg9njz2418", typeof(UIBattlePass_Com_Reward));
		UIObjectFactory.SetPackageItemExtension("ui://ssf8xg9njz241a", typeof(UIBattlePass_Com_TaskLabel));
		UIObjectFactory.SetPackageItemExtension("ui://ssf8xg9njz241d", typeof(UIBattlePass_Button_TaskStatus));
		UIObjectFactory.SetPackageItemExtension("ui://ssf8xg9njz241j", typeof(UIBattle_Com_Goods));
		UIObjectFactory.SetPackageItemExtension("ui://ssf8xg9njz241k", typeof(UIBattlePass_Button_SaleItem));
		UIObjectFactory.SetPackageItemExtension("ui://ssf8xg9njz241m", typeof(UIBattlePass_Com_DoubleReward));
		UIObjectFactory.SetPackageItemExtension("ui://ssf8xg9njz241n", typeof(UIBattlePass_Com_DoubleRewardItem));
		UIObjectFactory.SetPackageItemExtension("ui://ssf8xg9njz241s", typeof(UIBattlePass_Com_RewardInfo));
		UIObjectFactory.SetPackageItemExtension("ui://ssf8xg9njz241u", typeof(UIBattlePass_Button_RewardInfoItem));
		UIObjectFactory.SetPackageItemExtension("ui://ssf8xg9njz2420", typeof(UIBattlePass_Com_PlayerLabelReward));
		UIObjectFactory.SetPackageItemExtension("ui://ssf8xg9njz2422", typeof(UIBattlePass_Com_SkinItem));
		UIObjectFactory.SetPackageItemExtension("ui://ssf8xg9njz2423", typeof(UIBattlePass_Com_PlayerLabelItem));
		UIObjectFactory.SetPackageItemExtension("ui://ssf8xg9njz2424", typeof(UIBattlePass_Com_CommonItem));
		UIObjectFactory.SetPackageItemExtension("ui://ssf8xg9njz2425", typeof(UIBattlePass_Com_MediumItem));
		UIObjectFactory.SetPackageItemExtension("ui://ssf8xg9njz2426", typeof(UIBattlePass_Com_TopicItem));
		UIObjectFactory.SetPackageItemExtension("ui://ssf8xg9njz2427", typeof(UIBattlePass_Com_LargeItem));
		UIObjectFactory.SetPackageItemExtension("ui://ssf8xg9njz24z", typeof(UIBattlePass_Button_Tab));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		tab = GetControllerAt(0);
		showRewardInfo = GetControllerAt(1);
		loader_Main = (GLoader)GetChildAt(0);
		loader_Task = (GLoader)GetChildAt(1);
		loader_Effect = (GLoader)GetChildAt(2);
		loader_Skin = (GLoader)GetChildAt(3);
		txt_SkinName = (GTextField)GetChildAt(5);
		btn_PreviewSkin = (GButton)GetChildAt(6);
		com_Progress = (UIBattlePass_Com_ExpProgress)GetChildAt(8);
		btn_GoPurchase = (UIBattlePass_Button_Purchase)GetChildAt(9);
		list_BattlePass = (GList)GetChildAt(10);
		txt_DailyTime = (GTextField)GetChildAt(15);
		txt_WeeklyTime = (GTextField)GetChildAt(16);
		list_DailyTask = (GList)GetChildAt(17);
		list_WeeklyTask = (GList)GetChildAt(18);
		com_Goods = (UIBattle_Com_Goods)GetChildAt(20);
		btn_battlePass = (UIBattlePass_Button_Tab)GetChildAt(21);
		btn_Task = (UIBattlePass_Button_Tab)GetChildAt(22);
		btn_Return = (GButton)GetChildAt(24);
		com_RewardInfo = (UIBattlePass_Com_RewardInfo)GetChildAt(25);
		Cutin = GetTransitionAt(0);
	}
}
