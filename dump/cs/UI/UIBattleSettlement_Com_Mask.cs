using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIBattleSettlement_Com_Mask : GComponent
{
	public Controller slot;

	public Transition Cut_in;

	public const string URL = "ui://avgradidi6dg3m";

	public static UIBattleSettlement_Com_Mask CreateInstance()
	{
		return (UIBattleSettlement_Com_Mask)UIPackage.CreateObject("BattleSettlement", "BattleSettlement_Com_Mask");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		slot = GetControllerAt(0);
		Cut_in = GetTransitionAt(0);
	}
}
