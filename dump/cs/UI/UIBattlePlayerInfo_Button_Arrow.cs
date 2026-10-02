using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIBattlePlayerInfo_Button_Arrow : GButton
{
	public Controller vailStatus;

	public const string URL = "ui://qzmgh1v9m7gv8i";

	public static UIBattlePlayerInfo_Button_Arrow CreateInstance()
	{
		return (UIBattlePlayerInfo_Button_Arrow)UIPackage.CreateObject("BattlePlayerInfo", "BattlePlayerInfo_Button_Arrow");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		vailStatus = GetControllerAt(1);
	}
}
