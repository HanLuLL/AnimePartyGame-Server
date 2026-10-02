using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIBattlePass_Button_SaleItem : GButton
{
	public Controller infoType;

	public Controller rewardType;

	public Controller LockStatus;

	public Controller vailReward;

	public Controller showItemType;

	public Controller isFinish;

	public GLoader loader_Icon;

	public GGraph graph_Skin;

	public UIBattlePass_Com_PlayerLabelReward com_PlayerLabel;

	public GComponent com_expression;

	public GLoader loader_PropItem;

	public GComponent com_CardBack;

	public GTextField txt_Count;

	public GTextField txt_Name;

	public const string URL = "ui://ssf8xg9njz241k";

	public static UIBattlePass_Button_SaleItem CreateInstance()
	{
		return (UIBattlePass_Button_SaleItem)UIPackage.CreateObject("BattlePass", "BattlePass_Button_SaleItem");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		infoType = GetControllerAt(1);
		rewardType = GetControllerAt(2);
		LockStatus = GetControllerAt(3);
		vailReward = GetControllerAt(4);
		showItemType = GetControllerAt(5);
		isFinish = GetControllerAt(6);
		loader_Icon = (GLoader)GetChildAt(7);
		graph_Skin = (GGraph)GetChildAt(8);
		com_PlayerLabel = (UIBattlePass_Com_PlayerLabelReward)GetChildAt(9);
		com_expression = (GComponent)GetChildAt(10);
		loader_PropItem = (GLoader)GetChildAt(11);
		com_CardBack = (GComponent)GetChildAt(12);
		txt_Count = (GTextField)GetChildAt(13);
		txt_Name = (GTextField)GetChildAt(14);
	}
}
