using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIGM_Com_Target : GComponent
{
	public GList list_Players;

	public GTextField txt_TargetPlayer;

	public UIGM_Com_SetAttr com_Move;

	public UIGM_Com_SetAttr com_FightAtkPoint;

	public UIGM_Com_SetAttr com_FightDefPoint;

	public UIGM_Com_SetAttr com_Portal;

	public UIGM_Com_SetAttr com_HP;

	public UIGM_Com_SetAttr com_ATK;

	public UIGM_Com_SetAttr com_DEF;

	public UIGM_Com_SetAttr com_Gold;

	public UIGM_Com_SetAttr com_LV;

	public UIGM_Com_SetAttr com_CardCount;

	public UIGM_Com_SetAttr_1 com_CreateMonster;

	public UIGM_Com_Set_Attr_Buff com_Buff;

	public UIGM_Button_Operate btn_ResetSkill;

	public const string URL = "ui://725vhs9yulwvs";

	public static UIGM_Com_Target CreateInstance()
	{
		return (UIGM_Com_Target)UIPackage.CreateObject("GM", "GM_Com_Target");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		list_Players = (GList)GetChildAt(2);
		txt_TargetPlayer = (GTextField)GetChildAt(4);
		com_Move = (UIGM_Com_SetAttr)GetChildAt(6);
		com_FightAtkPoint = (UIGM_Com_SetAttr)GetChildAt(7);
		com_FightDefPoint = (UIGM_Com_SetAttr)GetChildAt(8);
		com_Portal = (UIGM_Com_SetAttr)GetChildAt(9);
		com_HP = (UIGM_Com_SetAttr)GetChildAt(12);
		com_ATK = (UIGM_Com_SetAttr)GetChildAt(13);
		com_DEF = (UIGM_Com_SetAttr)GetChildAt(14);
		com_Gold = (UIGM_Com_SetAttr)GetChildAt(15);
		com_LV = (UIGM_Com_SetAttr)GetChildAt(16);
		com_CardCount = (UIGM_Com_SetAttr)GetChildAt(17);
		com_CreateMonster = (UIGM_Com_SetAttr_1)GetChildAt(19);
		com_Buff = (UIGM_Com_Set_Attr_Buff)GetChildAt(21);
		btn_ResetSkill = (UIGM_Button_Operate)GetChildAt(22);
	}
}
