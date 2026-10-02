using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIBattleSettlement_Button_OperatePlayer : GButton
{
	public Controller operateType;

	public Controller isFinish;

	public GGraph Vfx;

	public Transition Cut_in;

	public const string URL = "ui://avgradidqees2d";

	public static UIBattleSettlement_Button_OperatePlayer CreateInstance()
	{
		return (UIBattleSettlement_Button_OperatePlayer)UIPackage.CreateObject("BattleSettlement", "BattleSettlement_Button_OperatePlayer");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		operateType = GetControllerAt(0);
		isFinish = GetControllerAt(2);
		Vfx = (GGraph)GetChildAt(4);
		Cut_in = GetTransitionAt(0);
	}
}
