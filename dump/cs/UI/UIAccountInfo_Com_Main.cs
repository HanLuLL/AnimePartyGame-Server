using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIAccountInfo_Com_Main : GComponent
{
	public Controller dataType;

	public Controller showFight;

	public GButton btn_RefreshSkin;

	public UIAccountInfo_Com_Skin com_Skin;

	public GButton btn_changeName;

	public UIAccountInfo_Button_ShowAchieve btn_Achieve_0;

	public UIAccountInfo_Button_ShowAchieve btn_Achieve_1;

	public UIAccountInfo_Button_ShowAchieve btn_Achieve_2;

	public UIAccountInfo_Button_ShowAchieve btn_Achieve_3;

	public UIAccountInfo_Button_ShowAchieve btn_Achieve_4;

	public UIAccountInfo_Button_ShowAchieve btn_Achieve_5;

	public GButton btn_Data;

	public GButton btn_Battle;

	public GButton btn_Replay;

	public UIAccountInfo_Button_Toggle btn_ShowDataToggle;

	public UIAccountInfo_Button_Toggle btn_ShowFightToggle;

	public GTextField txt_ReplayTip;

	public GTextField txt_StatisticalTip;

	public GTextField txt_FightCount;

	public GTextField txt_WinCount;

	public GTextField txt_HeroCardCount;

	public GTextField txt_Hero;

	public GTextField txt_AdornCount;

	public GTextField txt_SkinCount;

	public GGroup group_Statistical;

	public GTextField txt_FightDataTip;

	public GList list_FightData;

	public GComponent com_Label;

	public UIAccountInfo_Button_UID btn_UID;

	public GProgressBar progress_Exp;

	public GTextField txt_PraiseNum;

	public UIAccountInfo_Com_FightInfo com_Fight;

	public Transition Cut_in;

	public const string URL = "ui://iepldke7x20l20";

	public static UIAccountInfo_Com_Main CreateInstance()
	{
		return (UIAccountInfo_Com_Main)UIPackage.CreateObject("AccountInfo", "AccountInfo_Com_Main");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		dataType = GetControllerAt(0);
		showFight = GetControllerAt(1);
		btn_RefreshSkin = (GButton)GetChildAt(1);
		com_Skin = (UIAccountInfo_Com_Skin)GetChildAt(3);
		btn_changeName = (GButton)GetChildAt(4);
		btn_Achieve_0 = (UIAccountInfo_Button_ShowAchieve)GetChildAt(6);
		btn_Achieve_1 = (UIAccountInfo_Button_ShowAchieve)GetChildAt(7);
		btn_Achieve_2 = (UIAccountInfo_Button_ShowAchieve)GetChildAt(8);
		btn_Achieve_3 = (UIAccountInfo_Button_ShowAchieve)GetChildAt(9);
		btn_Achieve_4 = (UIAccountInfo_Button_ShowAchieve)GetChildAt(10);
		btn_Achieve_5 = (UIAccountInfo_Button_ShowAchieve)GetChildAt(11);
		btn_Data = (GButton)GetChildAt(14);
		btn_Battle = (GButton)GetChildAt(15);
		btn_Replay = (GButton)GetChildAt(16);
		btn_ShowDataToggle = (UIAccountInfo_Button_Toggle)GetChildAt(17);
		btn_ShowFightToggle = (UIAccountInfo_Button_Toggle)GetChildAt(18);
		txt_ReplayTip = (GTextField)GetChildAt(19);
		txt_StatisticalTip = (GTextField)GetChildAt(20);
		txt_FightCount = (GTextField)GetChildAt(22);
		txt_WinCount = (GTextField)GetChildAt(23);
		txt_HeroCardCount = (GTextField)GetChildAt(24);
		txt_Hero = (GTextField)GetChildAt(25);
		txt_AdornCount = (GTextField)GetChildAt(26);
		txt_SkinCount = (GTextField)GetChildAt(27);
		group_Statistical = (GGroup)GetChildAt(28);
		txt_FightDataTip = (GTextField)GetChildAt(29);
		list_FightData = (GList)GetChildAt(30);
		com_Label = (GComponent)GetChildAt(33);
		btn_UID = (UIAccountInfo_Button_UID)GetChildAt(34);
		progress_Exp = (GProgressBar)GetChildAt(35);
		txt_PraiseNum = (GTextField)GetChildAt(39);
		com_Fight = (UIAccountInfo_Com_FightInfo)GetChildAt(41);
		Cut_in = GetTransitionAt(0);
	}
}
