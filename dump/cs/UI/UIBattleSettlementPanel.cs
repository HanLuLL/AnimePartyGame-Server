using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIBattleSettlementPanel : GComponent
{
	public Controller step;

	public GGraph graph_Effect;

	public UIBattleSettlement_Com_ShowWiner com_Winner;

	public UIBattleSettlement_Com_ShowTime com_ShowTime;

	public UIBattleSettlement_Com_Balance com_Balance;

	public UIBattleSettlement_Com_Victory com_Victory;

	public GButton btn_Return;

	public const string URL = "ui://avgradidqees0";

	public static UIBattleSettlementPanel CreateInstance()
	{
		BindAll();
		return (UIBattleSettlementPanel)UIPackage.CreateObject("BattleSettlement", "BattleSettlementPanel");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://avgradida19i3s", typeof(UIBattleSettlement_Com_Victory));
		UIObjectFactory.SetPackageItemExtension("ui://avgradidch0q141", typeof(UIBattleSettlement_Com_Camp));
		UIObjectFactory.SetPackageItemExtension("ui://avgradidi6dg3m", typeof(UIBattleSettlement_Com_Mask));
		UIObjectFactory.SetPackageItemExtension("ui://avgradidqees0", typeof(UIBattleSettlementPanel));
		UIObjectFactory.SetPackageItemExtension("ui://avgradidqees24", typeof(UIBattleSettlement_Com_ShowRole));
		UIObjectFactory.SetPackageItemExtension("ui://avgradidqees25", typeof(UIBattleSettlement_Com_ShowWiner));
		UIObjectFactory.SetPackageItemExtension("ui://avgradidqees29", typeof(UIBattleSettlement_Com_Balance));
		UIObjectFactory.SetPackageItemExtension("ui://avgradidqees2d", typeof(UIBattleSettlement_Button_OperatePlayer));
		UIObjectFactory.SetPackageItemExtension("ui://avgradidqees2j", typeof(UIBattleSettlement_Slider_Data));
		UIObjectFactory.SetPackageItemExtension("ui://avgradidqees2k", typeof(UIBattleSettlement_Com_BattleDataItem));
		UIObjectFactory.SetPackageItemExtension("ui://avgradidqees2l", typeof(UIBattleSettlement_Com_BattleData));
		UIObjectFactory.SetPackageItemExtension("ui://avgradidqees2n", typeof(UIBattleSettlement_Com_AchieveBottom));
		UIObjectFactory.SetPackageItemExtension("ui://avgradidqees2o", typeof(UIBattleSettlement_Com_Item));
		UIObjectFactory.SetPackageItemExtension("ui://avgradidqees2p", typeof(UIBattleSettlement_Com_ItemRole));
		UIObjectFactory.SetPackageItemExtension("ui://avgradidqees2q", typeof(UIBattleSettlement_Button_Sure));
		UIObjectFactory.SetPackageItemExtension("ui://avgradidqees2t", typeof(UIBattleSettlement_Com_Map));
		UIObjectFactory.SetPackageItemExtension("ui://avgradidqees2w", typeof(UIBattleSettlement_Com_ShowTime));
		UIObjectFactory.SetPackageItemExtension("ui://avgradidw0q930", typeof(UIBattleSettlement_Button_AchieveItem));
		UIObjectFactory.SetPackageItemExtension("ui://avgradidw0q931", typeof(UIBattleSettlement_Com_BattleData_PVP));
		UIObjectFactory.SetPackageItemExtension("ui://avgradidw0q932", typeof(UIBattleSettlement_Com_BattleData_PVE));
		UIObjectFactory.SetPackageItemExtension("ui://avgradidw0q933", typeof(UIBattleSettlement_Com_RelicData));
		UIObjectFactory.SetPackageItemExtension("ui://avgradidw0q934", typeof(UIBattleSettlement_Button_Relic));
		UIObjectFactory.SetPackageItemExtension("ui://avgradidw0q938", typeof(UIBattleSettlement_Com_RelicGroup));
		UIObjectFactory.SetPackageItemExtension("ui://avgradidw0q939", typeof(UIBattleSettlement_Com_BalanceBonus));
		UIObjectFactory.SetPackageItemExtension("ui://avgradidw0q93a", typeof(UIBattleSettlement_Com_LimitReward));
		UIObjectFactory.SetPackageItemExtension("ui://avgradidw0q93c", typeof(UIBattleSettlement_Com_Reward));
		UIObjectFactory.SetPackageItemExtension("ui://avgradidw0q93j", typeof(UIBattleSettlement_Com_AchievementDetail));
		UIObjectFactory.SetPackageItemExtension("ui://avgradidw0q93l", typeof(UIBattleSettlement_Button_Praise));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		step = GetControllerAt(0);
		graph_Effect = (GGraph)GetChildAt(0);
		com_Winner = (UIBattleSettlement_Com_ShowWiner)GetChildAt(1);
		com_ShowTime = (UIBattleSettlement_Com_ShowTime)GetChildAt(2);
		com_Balance = (UIBattleSettlement_Com_Balance)GetChildAt(3);
		com_Victory = (UIBattleSettlement_Com_Victory)GetChildAt(4);
		btn_Return = (GButton)GetChildAt(5);
	}
}
