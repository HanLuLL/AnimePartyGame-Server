using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIBattleSettlement_Com_Balance : GComponent
{
	public UIBattleSettlement_Com_ShowRole com_ShowRole;

	public UIBattleSettlement_Com_Camp com_Camp;

	public GList list_Reward;

	public GComponent com_PlayerLabel;

	public GProgressBar slider_Exp;

	public GButton btn_Next;

	public Transition Cut_in;

	public const string URL = "ui://avgradidqees29";

	public static UIBattleSettlement_Com_Balance CreateInstance()
	{
		return (UIBattleSettlement_Com_Balance)UIPackage.CreateObject("BattleSettlement", "BattleSettlement_Com_Balance");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		com_ShowRole = (UIBattleSettlement_Com_ShowRole)GetChildAt(0);
		com_Camp = (UIBattleSettlement_Com_Camp)GetChildAt(1);
		list_Reward = (GList)GetChildAt(4);
		com_PlayerLabel = (GComponent)GetChildAt(5);
		slider_Exp = (GProgressBar)GetChildAt(6);
		btn_Next = (GButton)GetChildAt(9);
		Cut_in = GetTransitionAt(0);
	}
}
