using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIBattlePlayerInfo_Button_BuffItem : GButton
{
	public GButton btn_Buff;

	public const string URL = "ui://qzmgh1v9nbg68q";

	public static UIBattlePlayerInfo_Button_BuffItem CreateInstance()
	{
		return (UIBattlePlayerInfo_Button_BuffItem)UIPackage.CreateObject("BattlePlayerInfo", "BattlePlayerInfo_Button_BuffItem");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		btn_Buff = (GButton)GetChildAt(1);
	}
}
