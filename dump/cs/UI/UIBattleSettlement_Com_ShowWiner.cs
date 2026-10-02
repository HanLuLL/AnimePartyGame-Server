using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIBattleSettlement_Com_ShowWiner : GComponent
{
	public Controller isWinner;

	public UIBattleSettlement_Com_ShowRole com_ShowRole;

	public GTextField txt_WinnerCount;

	public GTextField txt_Achieve;

	public GComponent com_PlayerLabel;

	public GButton btn_Next;

	public UIBattleSettlement_Button_Praise btn_Praise;

	public Transition Victory_Cut_in;

	public const string URL = "ui://avgradidqees25";

	public static UIBattleSettlement_Com_ShowWiner CreateInstance()
	{
		return (UIBattleSettlement_Com_ShowWiner)UIPackage.CreateObject("BattleSettlement", "BattleSettlement_Com_ShowWiner");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		isWinner = GetControllerAt(0);
		com_ShowRole = (UIBattleSettlement_Com_ShowRole)GetChildAt(2);
		txt_WinnerCount = (GTextField)GetChildAt(4);
		txt_Achieve = (GTextField)GetChildAt(5);
		com_PlayerLabel = (GComponent)GetChildAt(6);
		btn_Next = (GButton)GetChildAt(7);
		btn_Praise = (UIBattleSettlement_Button_Praise)GetChildAt(8);
		Victory_Cut_in = GetTransitionAt(0);
	}
}
