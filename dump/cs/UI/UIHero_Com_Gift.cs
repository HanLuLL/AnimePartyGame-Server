using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIHero_Com_Gift : GComponent
{
	public Controller selectGift;

	public Controller operateSelect;

	public Controller breakThroughStatus;

	public Controller LV5;

	public Controller releationPowerUp;

	public GButton btn_GiftTab;

	public GButton btn_OperateBreak;

	public GList list_Relation;

	public GList list_Gifts;

	public GButton btn_giveGift;

	public GButton btn_addNum;

	public GButton btn_delNum;

	public GTextField txt_itemSelectNum;

	public GButton btn_Min;

	public GButton btn_Max;

	public UIHero_Com_Progress group_Progress;

	public GImage image_CN;

	public GImage image_EN;

	public GProgressBar progress_ReleasePowerUp;

	public GTextField txt_CurFavorValue;

	public UIHero_Button_GiftItem com_SelectedGift;

	public GList list_BreakThroughReward;

	public GButton btn_showBreakThrough;

	public GList list_BreakThrough;

	public GButton btn_breakThrough;

	public GButton btn_Switch;

	public GGraph com_MaskGift;

	public Transition Cut_in;

	public const string URL = "ui://7qkd4lqxg1lkn";

	public static UIHero_Com_Gift CreateInstance()
	{
		return (UIHero_Com_Gift)UIPackage.CreateObject("Hero", "Hero_Com_Gift");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		selectGift = GetControllerAt(0);
		operateSelect = GetControllerAt(1);
		breakThroughStatus = GetControllerAt(2);
		LV5 = GetControllerAt(3);
		releationPowerUp = GetControllerAt(4);
		btn_GiftTab = (GButton)GetChildAt(1);
		btn_OperateBreak = (GButton)GetChildAt(2);
		list_Relation = (GList)GetChildAt(4);
		list_Gifts = (GList)GetChildAt(5);
		btn_giveGift = (GButton)GetChildAt(7);
		btn_addNum = (GButton)GetChildAt(8);
		btn_delNum = (GButton)GetChildAt(9);
		txt_itemSelectNum = (GTextField)GetChildAt(11);
		btn_Min = (GButton)GetChildAt(12);
		btn_Max = (GButton)GetChildAt(13);
		group_Progress = (UIHero_Com_Progress)GetChildAt(15);
		image_CN = (GImage)GetChildAt(16);
		image_EN = (GImage)GetChildAt(17);
		progress_ReleasePowerUp = (GProgressBar)GetChildAt(18);
		txt_CurFavorValue = (GTextField)GetChildAt(19);
		com_SelectedGift = (UIHero_Button_GiftItem)GetChildAt(23);
		list_BreakThroughReward = (GList)GetChildAt(26);
		btn_showBreakThrough = (GButton)GetChildAt(30);
		list_BreakThrough = (GList)GetChildAt(31);
		btn_breakThrough = (GButton)GetChildAt(32);
		btn_Switch = (GButton)GetChildAt(34);
		com_MaskGift = (GGraph)GetChildAt(37);
		Cut_in = GetTransitionAt(0);
	}
}
