using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIBattlePlayerInfo_Button_GameMode : GButton
{
	public GGraph loader_Icon_Up;

	public GGraph loader_Icon_Down;

	public GRichTextField txt_title;

	public const string URL = "ui://qzmgh1v9m7gv87";

	public static UIBattlePlayerInfo_Button_GameMode CreateInstance()
	{
		return (UIBattlePlayerInfo_Button_GameMode)UIPackage.CreateObject("BattlePlayerInfo", "BattlePlayerInfo_Button_GameMode");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		loader_Icon_Up = (GGraph)GetChildAt(0);
		loader_Icon_Down = (GGraph)GetChildAt(1);
		txt_title = (GRichTextField)GetChildAt(2);
	}
}
