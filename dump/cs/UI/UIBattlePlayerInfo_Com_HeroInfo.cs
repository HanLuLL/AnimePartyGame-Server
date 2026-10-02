using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIBattlePlayerInfo_Com_HeroInfo : GComponent
{
	public Controller tab;

	public Controller type;

	public GGraph di;

	public GGraph middle_line;

	public UIBattlePlayerInfo_Button_GameMode btn_Attr;

	public UIBattlePlayerInfo_Button_GameMode btn_Skill;

	public GTextField txt_ATK;

	public GTextField txt_DEF;

	public GTextField txt_Card;

	public GLoader loader_Talent;

	public GTextField txt_PVELv;

	public GTextField txt_CharaterNick;

	public GTextField txt_CharaterName;

	public UIBattlePlayerInfo_Button_Relic btn_Relic_0;

	public UIBattlePlayerInfo_Button_Relic btn_Relic_1;

	public UIBattlePlayerInfo_Button_Relic btn_Relic_2;

	public UIBattlePlayerInfo_Button_Relic btn_Relic_3;

	public UIBattlePlayerInfo_Button_Relic btn_Relic_4;

	public UIBattlePlayerInfo_Button_Relic btn_Relic_5;

	public UIBattlePlayerInfo_Button_Relic btn_Relic_6;

	public UIBattlePlayerInfo_Button_Relic btn_Relic_7;

	public UIBattlePlayerInfo_Button_Relic btn_Relic_8;

	public UIBattlePlayerInfo_Button_Relic btn_Relic_9;

	public UIBattlePlayerInfo_Button_Arrow btn_Right;

	public UIBattlePlayerInfo_Button_Arrow btn_Left;

	public UIBattlePlayerInfo_Button_RelicChat btn_Chat;

	public GTextField txt_Name;

	public GRichTextField txt_Desc;

	public GList list_Buff;

	public GRichTextField txt_BuffDesc;

	public UIBattlePlayerInfo_Com_BattleData com_killCount;

	public UIBattlePlayerInfo_Com_BattleData com_TotalDie;

	public UIBattlePlayerInfo_Com_BattleData com_TotalDamage;

	public UIBattlePlayerInfo_Com_BattleData com_TotalInjured;

	public UIBattlePlayerInfo_Com_BattleData com_TreatmentScore;

	public GList list_Skill;

	public GButton btn_Quit;

	public const string URL = "ui://qzmgh1v9nbg68o";

	public static UIBattlePlayerInfo_Com_HeroInfo CreateInstance()
	{
		return (UIBattlePlayerInfo_Com_HeroInfo)UIPackage.CreateObject("BattlePlayerInfo", "BattlePlayerInfo_Com_HeroInfo");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		tab = GetControllerAt(0);
		type = GetControllerAt(1);
		di = (GGraph)GetChildAt(0);
		middle_line = (GGraph)GetChildAt(8);
		btn_Attr = (UIBattlePlayerInfo_Button_GameMode)GetChildAt(9);
		btn_Skill = (UIBattlePlayerInfo_Button_GameMode)GetChildAt(10);
		txt_ATK = (GTextField)GetChildAt(12);
		txt_DEF = (GTextField)GetChildAt(14);
		txt_Card = (GTextField)GetChildAt(16);
		loader_Talent = (GLoader)GetChildAt(17);
		txt_PVELv = (GTextField)GetChildAt(19);
		txt_CharaterNick = (GTextField)GetChildAt(21);
		txt_CharaterName = (GTextField)GetChildAt(22);
		btn_Relic_0 = (UIBattlePlayerInfo_Button_Relic)GetChildAt(28);
		btn_Relic_1 = (UIBattlePlayerInfo_Button_Relic)GetChildAt(29);
		btn_Relic_2 = (UIBattlePlayerInfo_Button_Relic)GetChildAt(30);
		btn_Relic_3 = (UIBattlePlayerInfo_Button_Relic)GetChildAt(31);
		btn_Relic_4 = (UIBattlePlayerInfo_Button_Relic)GetChildAt(32);
		btn_Relic_5 = (UIBattlePlayerInfo_Button_Relic)GetChildAt(33);
		btn_Relic_6 = (UIBattlePlayerInfo_Button_Relic)GetChildAt(34);
		btn_Relic_7 = (UIBattlePlayerInfo_Button_Relic)GetChildAt(35);
		btn_Relic_8 = (UIBattlePlayerInfo_Button_Relic)GetChildAt(36);
		btn_Relic_9 = (UIBattlePlayerInfo_Button_Relic)GetChildAt(37);
		btn_Right = (UIBattlePlayerInfo_Button_Arrow)GetChildAt(39);
		btn_Left = (UIBattlePlayerInfo_Button_Arrow)GetChildAt(40);
		btn_Chat = (UIBattlePlayerInfo_Button_RelicChat)GetChildAt(42);
		txt_Name = (GTextField)GetChildAt(43);
		txt_Desc = (GRichTextField)GetChildAt(44);
		list_Buff = (GList)GetChildAt(47);
		txt_BuffDesc = (GRichTextField)GetChildAt(48);
		com_killCount = (UIBattlePlayerInfo_Com_BattleData)GetChildAt(50);
		com_TotalDie = (UIBattlePlayerInfo_Com_BattleData)GetChildAt(51);
		com_TotalDamage = (UIBattlePlayerInfo_Com_BattleData)GetChildAt(52);
		com_TotalInjured = (UIBattlePlayerInfo_Com_BattleData)GetChildAt(53);
		com_TreatmentScore = (UIBattlePlayerInfo_Com_BattleData)GetChildAt(54);
		list_Skill = (GList)GetChildAt(56);
		btn_Quit = (GButton)GetChildAt(57);
	}
}
