using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIBattleSettlement_Button_AchieveItem : GButton
{
	public GTextField title_Down;

	public GTextField title_Up;

	public Transition Cut_in;

	public const string URL = "ui://avgradidw0q930";

	public static UIBattleSettlement_Button_AchieveItem CreateInstance()
	{
		return (UIBattleSettlement_Button_AchieveItem)UIPackage.CreateObject("BattleSettlement", "BattleSettlement_Button_AchieveItem");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		title_Down = (GTextField)GetChildAt(1);
		title_Up = (GTextField)GetChildAt(2);
		Cut_in = GetTransitionAt(0);
	}
}
