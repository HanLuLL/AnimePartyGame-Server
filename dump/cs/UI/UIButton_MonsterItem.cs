using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIButton_MonsterItem : GButton
{
	public Controller status;

	public Controller showCounter;

	public Controller monsterType;

	public Controller omit;

	public GLoader loader_Icon;

	public GTextField txt_Name;

	public GTextField txt_Ordin;

	public GComponent com_AtkIcon;

	public GTextField txt_ATK;

	public GComponent com_DefIcon;

	public GTextField txt_DEF;

	public GComponent com_Counter;

	public GGroup attr;

	public GTextField txt_curLife;

	public GTextField txt_maxLife;

	public GList list_buff;

	public GLoader btn_showBuff;

	public const string URL = "ui://1ov1i0v9kqgjbm";

	public static UIButton_MonsterItem CreateInstance()
	{
		return (UIButton_MonsterItem)UIPackage.CreateObject("Common_Internal", "Button_MonsterItem");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		status = GetControllerAt(1);
		showCounter = GetControllerAt(2);
		monsterType = GetControllerAt(3);
		omit = GetControllerAt(4);
		loader_Icon = (GLoader)GetChildAt(0);
		txt_Name = (GTextField)GetChildAt(3);
		txt_Ordin = (GTextField)GetChildAt(4);
		com_AtkIcon = (GComponent)GetChildAt(5);
		txt_ATK = (GTextField)GetChildAt(6);
		com_DefIcon = (GComponent)GetChildAt(7);
		txt_DEF = (GTextField)GetChildAt(8);
		com_Counter = (GComponent)GetChildAt(9);
		attr = (GGroup)GetChildAt(10);
		txt_curLife = (GTextField)GetChildAt(11);
		txt_maxLife = (GTextField)GetChildAt(12);
		list_buff = (GList)GetChildAt(14);
		btn_showBuff = (GLoader)GetChildAt(16);
	}
}
