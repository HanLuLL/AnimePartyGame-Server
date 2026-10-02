using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIBattlePlayerInfo_Com_Skill : GComponent
{
	public Controller showCD;

	public GTextField txt_Title;

	public GRichTextField txt_Desc;

	public GTextField txt_CD;

	public const string URL = "ui://qzmgh1v9nbg68t";

	public static UIBattlePlayerInfo_Com_Skill CreateInstance()
	{
		return (UIBattlePlayerInfo_Com_Skill)UIPackage.CreateObject("BattlePlayerInfo", "BattlePlayerInfo_Com_Skill");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		showCD = GetControllerAt(0);
		txt_Title = (GTextField)GetChildAt(0);
		txt_Desc = (GRichTextField)GetChildAt(1);
		txt_CD = (GTextField)GetChildAt(3);
	}
}
