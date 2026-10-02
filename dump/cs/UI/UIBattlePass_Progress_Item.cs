using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIBattlePass_Progress_Item : GProgressBar
{
	public Controller status;

	public GTextField txt_Level;

	public UIBattlePass_Button_GoPurchaseLv btn_PurchaseLv;

	public const string URL = "ui://ssf8xg9njz2414";

	public static UIBattlePass_Progress_Item CreateInstance()
	{
		return (UIBattlePass_Progress_Item)UIPackage.CreateObject("BattlePass", "BattlePass_Progress_Item");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		status = GetControllerAt(0);
		txt_Level = (GTextField)GetChildAt(4);
		btn_PurchaseLv = (UIBattlePass_Button_GoPurchaseLv)GetChildAt(5);
	}
}
