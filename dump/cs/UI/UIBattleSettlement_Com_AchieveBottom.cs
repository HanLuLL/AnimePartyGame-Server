using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIBattleSettlement_Com_AchieveBottom : GComponent
{
	public Controller slot;

	public Transition Cut_in;

	public const string URL = "ui://avgradidqees2n";

	public static UIBattleSettlement_Com_AchieveBottom CreateInstance()
	{
		return (UIBattleSettlement_Com_AchieveBottom)UIPackage.CreateObject("BattleSettlement", "BattleSettlement_Com_AchieveBottom");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		slot = GetControllerAt(0);
		Cut_in = GetTransitionAt(0);
	}
}
