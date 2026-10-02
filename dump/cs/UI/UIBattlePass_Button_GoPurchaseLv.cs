using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIBattlePass_Button_GoPurchaseLv : GButton
{
	public GTextField txt_CostPerLv;

	public GLoader loader_CostCurrency;

	public const string URL = "ui://ssf8xg9njz2415";

	public static UIBattlePass_Button_GoPurchaseLv CreateInstance()
	{
		return (UIBattlePass_Button_GoPurchaseLv)UIPackage.CreateObject("BattlePass", "BattlePass_Button_GoPurchaseLv");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		txt_CostPerLv = (GTextField)GetChildAt(1);
		loader_CostCurrency = (GLoader)GetChildAt(2);
	}
}
