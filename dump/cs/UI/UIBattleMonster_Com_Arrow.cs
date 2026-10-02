using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIBattleMonster_Com_Arrow : GComponent
{
	public Transition t0;

	public const string URL = "ui://nxg5t93dkqgj2";

	public static UIBattleMonster_Com_Arrow CreateInstance()
	{
		return (UIBattleMonster_Com_Arrow)UIPackage.CreateObject("BattleSelectMonster", "BattleMonster_Com_Arrow");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		t0 = GetTransitionAt(0);
	}
}
