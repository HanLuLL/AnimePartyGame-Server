using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIBattleInfo_Com_PlayerContainer : GComponent
{
	public UIBattleInfo_Button_PlayerInfo com_Player_4;

	public UIBattleInfo_Button_PlayerInfo com_Player_3;

	public UIBattleInfo_Button_PlayerInfo com_Player_2;

	public UIBattleInfo_Button_PlayerInfo com_Player_1;

	public const string URL = "ui://fxejlqlfsxcpbg";

	public static UIBattleInfo_Com_PlayerContainer CreateInstance()
	{
		return (UIBattleInfo_Com_PlayerContainer)UIPackage.CreateObject("BattleInfo", "BattleInfo_Com_PlayerContainer");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		com_Player_4 = (UIBattleInfo_Button_PlayerInfo)GetChildAt(0);
		com_Player_3 = (UIBattleInfo_Button_PlayerInfo)GetChildAt(1);
		com_Player_2 = (UIBattleInfo_Button_PlayerInfo)GetChildAt(2);
		com_Player_1 = (UIBattleInfo_Button_PlayerInfo)GetChildAt(3);
	}
}
