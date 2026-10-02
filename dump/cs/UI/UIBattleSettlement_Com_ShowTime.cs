using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIBattleSettlement_Com_ShowTime : GComponent
{
	public UIBattleSettlement_Com_Item com_Player4;

	public UIBattleSettlement_Com_Item com_Player3;

	public UIBattleSettlement_Com_Item com_Player2;

	public UIBattleSettlement_Com_Item com_Player1;

	public UIBattleSettlement_Com_Map com_Map;

	public UIBattleSettlement_Button_Sure btn_Next;

	public GButton btn_ScreenShot;

	public Transition ShowTime;

	public const string URL = "ui://avgradidqees2w";

	public static UIBattleSettlement_Com_ShowTime CreateInstance()
	{
		return (UIBattleSettlement_Com_ShowTime)UIPackage.CreateObject("BattleSettlement", "BattleSettlement_Com_ShowTime");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		com_Player4 = (UIBattleSettlement_Com_Item)GetChildAt(1);
		com_Player3 = (UIBattleSettlement_Com_Item)GetChildAt(2);
		com_Player2 = (UIBattleSettlement_Com_Item)GetChildAt(3);
		com_Player1 = (UIBattleSettlement_Com_Item)GetChildAt(4);
		com_Map = (UIBattleSettlement_Com_Map)GetChildAt(5);
		btn_Next = (UIBattleSettlement_Button_Sure)GetChildAt(6);
		btn_ScreenShot = (GButton)GetChildAt(7);
		ShowTime = GetTransitionAt(0);
	}
}
