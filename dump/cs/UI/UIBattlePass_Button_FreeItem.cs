using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIBattlePass_Button_FreeItem : GButton
{
	public Controller isFinish;

	public Controller vailReward;

	public GLoader loader_Item;

	public GTextField txt_ItemFreeNum;

	public const string URL = "ui://ssf8xg9njz2416";

	public static UIBattlePass_Button_FreeItem CreateInstance()
	{
		return (UIBattlePass_Button_FreeItem)UIPackage.CreateObject("BattlePass", "BattlePass_Button_FreeItem");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		isFinish = GetControllerAt(1);
		vailReward = GetControllerAt(2);
		loader_Item = (GLoader)GetChildAt(2);
		txt_ItemFreeNum = (GTextField)GetChildAt(4);
	}
}
