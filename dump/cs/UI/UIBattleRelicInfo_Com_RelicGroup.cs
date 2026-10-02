using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIBattleRelicInfo_Com_RelicGroup : GComponent
{
	public UIBattleRelicInfo_Button_Relic btn_Relic_0;

	public UIBattleRelicInfo_Button_Relic btn_Relic_1;

	public UIBattleRelicInfo_Button_Relic btn_Relic_2;

	public UIBattleRelicInfo_Button_Relic btn_Relic_3;

	public UIBattleRelicInfo_Button_Relic btn_Relic_4;

	public UIBattleRelicInfo_Button_Relic btn_Relic_5;

	public UIBattleRelicInfo_Button_Relic btn_Relic_6;

	public UIBattleRelicInfo_Button_Relic btn_Relic_7;

	public UIBattleRelicInfo_Button_Relic btn_Relic_8;

	public UIBattleRelicInfo_Button_Relic btn_Relic_9;

	public const string URL = "ui://ethkhr1hfcnum";

	public static UIBattleRelicInfo_Com_RelicGroup CreateInstance()
	{
		return (UIBattleRelicInfo_Com_RelicGroup)UIPackage.CreateObject("BattleRelicInfo", "BattleRelicInfo_Com_RelicGroup");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		btn_Relic_0 = (UIBattleRelicInfo_Button_Relic)GetChildAt(0);
		btn_Relic_1 = (UIBattleRelicInfo_Button_Relic)GetChildAt(1);
		btn_Relic_2 = (UIBattleRelicInfo_Button_Relic)GetChildAt(2);
		btn_Relic_3 = (UIBattleRelicInfo_Button_Relic)GetChildAt(3);
		btn_Relic_4 = (UIBattleRelicInfo_Button_Relic)GetChildAt(4);
		btn_Relic_5 = (UIBattleRelicInfo_Button_Relic)GetChildAt(5);
		btn_Relic_6 = (UIBattleRelicInfo_Button_Relic)GetChildAt(6);
		btn_Relic_7 = (UIBattleRelicInfo_Button_Relic)GetChildAt(7);
		btn_Relic_8 = (UIBattleRelicInfo_Button_Relic)GetChildAt(8);
		btn_Relic_9 = (UIBattleRelicInfo_Button_Relic)GetChildAt(9);
	}
}
