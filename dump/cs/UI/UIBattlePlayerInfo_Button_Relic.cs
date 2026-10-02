using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIBattlePlayerInfo_Button_Relic : GButton
{
	public GComponent com_Quality;

	public GLoader loader_Relic;

	public const string URL = "ui://qzmgh1v9m7gv8g";

	public static UIBattlePlayerInfo_Button_Relic CreateInstance()
	{
		return (UIBattlePlayerInfo_Button_Relic)UIPackage.CreateObject("BattlePlayerInfo", "BattlePlayerInfo_Button_Relic");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		com_Quality = (GComponent)GetChildAt(0);
		loader_Relic = (GLoader)GetChildAt(2);
	}
}
