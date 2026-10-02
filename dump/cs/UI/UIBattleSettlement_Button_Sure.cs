using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIBattleSettlement_Button_Sure : GButton
{
	public Transition Loop;

	public const string URL = "ui://avgradidqees2q";

	public static UIBattleSettlement_Button_Sure CreateInstance()
	{
		return (UIBattleSettlement_Button_Sure)UIPackage.CreateObject("BattleSettlement", "BattleSettlement_Button_Sure");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		Loop = GetTransitionAt(0);
	}
}
