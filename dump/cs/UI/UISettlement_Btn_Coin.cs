using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UISettlement_Btn_Coin : GButton
{
	public GTextField txt_gold;

	public const string URL = "ui://wwhrkd30hh8o11";

	public static UISettlement_Btn_Coin CreateInstance()
	{
		return (UISettlement_Btn_Coin)UIPackage.CreateObject("SinglePlayerSettlement", "Settlement_Btn_Coin");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		txt_gold = (GTextField)GetChildAt(4);
	}
}
