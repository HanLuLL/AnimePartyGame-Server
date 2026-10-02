using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivity_Com_Type4_Scratchoff : GComponent
{
	public GLoader loader_BG;

	public GList list_Preview;

	public UIActivity_Button_Type4_Scratchoff_Item btn_Reward_1;

	public UIActivity_Button_Type4_Scratchoff_Item btn_Reward_2;

	public UIActivity_Button_Type4_Scratchoff_Item btn_Reward_3;

	public UIActivity_Button_Type4_Scratchoff_Item btn_Reward_4;

	public UIActivity_Button_Type4_Scratchoff_Item btn_Reward_5;

	public UIActivity_Button_Type4_Scratchoff_Item btn_Reward_6;

	public UIActivity_Button_Type4_Scratchoff_Item btn_Reward_7;

	public UIActivity_Button_Type4_Scratchoff_Item btn_Reward_8;

	public UIActivity_Button_Type4_Scratchoff_Item btn_Reward_9;

	public UIActivity_Button_Type4_Scratchoff_Item btn_Reward_10;

	public UIActivity_Button_Type4_Scratchoff_Item btn_Reward_11;

	public UIActivity_Button_Type4_Scratchoff_Item btn_Reward_12;

	public UIActivity_Button_Type4_Scratchoff_Item btn_Reward_13;

	public UIActivity_Button_Type4_Scratchoff_Item btn_Reward_14;

	public GButton btn_PropItem;

	public GTextField txt_ItemCount;

	public GTextField txt_Time;

	public const string URL = "ui://c1v285vtpu2i11";

	public static UIActivity_Com_Type4_Scratchoff CreateInstance()
	{
		return (UIActivity_Com_Type4_Scratchoff)UIPackage.CreateObject("ActivityNgo", "Activity_Com_Type4_Scratchoff");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		loader_BG = (GLoader)GetChildAt(0);
		list_Preview = (GList)GetChildAt(20);
		btn_Reward_1 = (UIActivity_Button_Type4_Scratchoff_Item)GetChildAt(22);
		btn_Reward_2 = (UIActivity_Button_Type4_Scratchoff_Item)GetChildAt(23);
		btn_Reward_3 = (UIActivity_Button_Type4_Scratchoff_Item)GetChildAt(24);
		btn_Reward_4 = (UIActivity_Button_Type4_Scratchoff_Item)GetChildAt(25);
		btn_Reward_5 = (UIActivity_Button_Type4_Scratchoff_Item)GetChildAt(26);
		btn_Reward_6 = (UIActivity_Button_Type4_Scratchoff_Item)GetChildAt(27);
		btn_Reward_7 = (UIActivity_Button_Type4_Scratchoff_Item)GetChildAt(28);
		btn_Reward_8 = (UIActivity_Button_Type4_Scratchoff_Item)GetChildAt(29);
		btn_Reward_9 = (UIActivity_Button_Type4_Scratchoff_Item)GetChildAt(30);
		btn_Reward_10 = (UIActivity_Button_Type4_Scratchoff_Item)GetChildAt(31);
		btn_Reward_11 = (UIActivity_Button_Type4_Scratchoff_Item)GetChildAt(32);
		btn_Reward_12 = (UIActivity_Button_Type4_Scratchoff_Item)GetChildAt(33);
		btn_Reward_13 = (UIActivity_Button_Type4_Scratchoff_Item)GetChildAt(34);
		btn_Reward_14 = (UIActivity_Button_Type4_Scratchoff_Item)GetChildAt(35);
		btn_PropItem = (GButton)GetChildAt(43);
		txt_ItemCount = (GTextField)GetChildAt(44);
		txt_Time = (GTextField)GetChildAt(46);
	}
}
