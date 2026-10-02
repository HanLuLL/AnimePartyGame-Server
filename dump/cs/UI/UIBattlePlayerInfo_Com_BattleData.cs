using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIBattlePlayerInfo_Com_BattleData : GComponent
{
	public GTextField txt_Name;

	public GTextField txt_Count;

	public const string URL = "ui://qzmgh1v9nbg68r";

	public static UIBattlePlayerInfo_Com_BattleData CreateInstance()
	{
		return (UIBattlePlayerInfo_Com_BattleData)UIPackage.CreateObject("BattlePlayerInfo", "BattlePlayerInfo_Com_BattleData");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		txt_Name = (GTextField)GetChildAt(3);
		txt_Count = (GTextField)GetChildAt(4);
	}
}
