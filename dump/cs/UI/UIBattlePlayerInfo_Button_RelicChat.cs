using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIBattlePlayerInfo_Button_RelicChat : GButton
{
	public GLoader btn_Chat;

	public const string URL = "ui://qzmgh1v9m7gv8l";

	public static UIBattlePlayerInfo_Button_RelicChat CreateInstance()
	{
		return (UIBattlePlayerInfo_Button_RelicChat)UIPackage.CreateObject("BattlePlayerInfo", "BattlePlayerInfo_Button_RelicChat");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		btn_Chat = (GLoader)GetChildAt(0);
	}
}
