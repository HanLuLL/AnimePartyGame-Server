using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIBattleSettlement_Button_Relic : GButton
{
	public GComponent com_Quality;

	public GLoader loader_Relic;

	public const string URL = "ui://avgradidw0q934";

	public static UIBattleSettlement_Button_Relic CreateInstance()
	{
		return (UIBattleSettlement_Button_Relic)UIPackage.CreateObject("BattleSettlement", "BattleSettlement_Button_Relic");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		com_Quality = (GComponent)GetChildAt(0);
		loader_Relic = (GLoader)GetChildAt(2);
	}
}
