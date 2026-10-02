using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIBattlePass_Button_SurpassItem : GButton
{
	public Controller LockStatus;

	public Controller vailReward;

	public Controller isFinish;

	public GLoader loader_Icon;

	public GTextField txt_Name;

	public GTextField txt_Count;

	public const string URL = "ui://ssf8xg9nb93d29";

	public static UIBattlePass_Button_SurpassItem CreateInstance()
	{
		return (UIBattlePass_Button_SurpassItem)UIPackage.CreateObject("BattlePass", "BattlePass_Button_SurpassItem");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		LockStatus = GetControllerAt(1);
		vailReward = GetControllerAt(2);
		isFinish = GetControllerAt(3);
		loader_Icon = (GLoader)GetChildAt(2);
		txt_Name = (GTextField)GetChildAt(3);
		txt_Count = (GTextField)GetChildAt(4);
	}
}
