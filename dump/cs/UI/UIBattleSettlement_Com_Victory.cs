using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIBattleSettlement_Com_Victory : GComponent
{
	public Transition Victory_Cut_in;

	public const string URL = "ui://avgradida19i3s";

	public static UIBattleSettlement_Com_Victory CreateInstance()
	{
		return (UIBattleSettlement_Com_Victory)UIPackage.CreateObject("BattleSettlement", "BattleSettlement_Com_Victory");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		Victory_Cut_in = GetTransitionAt(0);
	}
}
