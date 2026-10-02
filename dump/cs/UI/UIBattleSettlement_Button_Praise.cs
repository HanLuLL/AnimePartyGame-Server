using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIBattleSettlement_Button_Praise : GButton
{
	public GGraph Vfx;

	public Transition Cut_in;

	public const string URL = "ui://avgradidw0q93l";

	public static UIBattleSettlement_Button_Praise CreateInstance()
	{
		return (UIBattleSettlement_Button_Praise)UIPackage.CreateObject("BattleSettlement", "BattleSettlement_Button_Praise");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		Vfx = (GGraph)GetChildAt(1);
		Cut_in = GetTransitionAt(0);
	}
}
