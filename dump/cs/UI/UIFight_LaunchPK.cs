using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIFight_LaunchPK : GComponent
{
	public Controller playerState;

	public Controller country;

	public Controller Hide;

	public GTextField txt_HideTip;

	public GImage img_Defender;

	public GLoader loader_Icon;

	public GButton btn_PK;

	public GButton btn_Leave;

	public GTextField txt_RoleName;

	public GButton com_P1;

	public GButton com_P2;

	public GButton com_P3;

	public GButton com_P4;

	public GTextField txt_HP;

	public GTextField txt_ATK;

	public GTextField txt_DEF;

	public GComponent com_Counter;

	public GTextField txt_Counter;

	public GGroup group_Attr;

	public GButton btn_ShowWin;

	public GButton btn_HideWin;

	public const string URL = "ui://8irq146hmjrud";

	public static UIFight_LaunchPK CreateInstance()
	{
		return (UIFight_LaunchPK)UIPackage.CreateObject("Fight", "Fight_LaunchPK");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		playerState = GetControllerAt(0);
		country = GetControllerAt(1);
		Hide = GetControllerAt(2);
		txt_HideTip = (GTextField)GetChildAt(0);
		img_Defender = (GImage)GetChildAt(1);
		loader_Icon = (GLoader)GetChildAt(2);
		btn_PK = (GButton)GetChildAt(6);
		btn_Leave = (GButton)GetChildAt(7);
		txt_RoleName = (GTextField)GetChildAt(9);
		com_P1 = (GButton)GetChildAt(20);
		com_P2 = (GButton)GetChildAt(21);
		com_P3 = (GButton)GetChildAt(22);
		com_P4 = (GButton)GetChildAt(23);
		txt_HP = (GTextField)GetChildAt(26);
		txt_ATK = (GTextField)GetChildAt(28);
		txt_DEF = (GTextField)GetChildAt(30);
		com_Counter = (GComponent)GetChildAt(31);
		txt_Counter = (GTextField)GetChildAt(32);
		group_Attr = (GGroup)GetChildAt(33);
		btn_ShowWin = (GButton)GetChildAt(34);
		btn_HideWin = (GButton)GetChildAt(35);
	}
}
