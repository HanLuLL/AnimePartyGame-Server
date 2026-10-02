using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivityRebatePanel : GComponent
{
	public GLoader RebateBackGround;

	public GImage Rebate_Title;

	public GImage Rebate_Title_Text;

	public UIActivityRebate_Btn_Message Rebate_Message_Btn;

	public GButton Go_Btn;

	public GImage Rebate_bg_Pink;

	public GTextField Rebate_num;

	public GTextField Rebate_num_Text;

	public GTextField Rebate_num_TextType;

	public GTextField Rebate_num1;

	public GTextField Rebate_num_Text1;

	public GGraph mohu;

	public GGroup RebateWindow;

	public GTextField Rebate_Time;

	public Transition Cutin;

	public const string URL = "ui://psydn4inrr350";

	public static UIActivityRebatePanel CreateInstance()
	{
		BindAll();
		return (UIActivityRebatePanel)UIPackage.CreateObject("ActivityRebate", "ActivityRebatePanel");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://psydn4inrr350", typeof(UIActivityRebatePanel));
		UIObjectFactory.SetPackageItemExtension("ui://psydn4inrr35k", typeof(UIActivityRebate_Btn_Message));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		RebateBackGround = (GLoader)GetChildAt(0);
		Rebate_Title = (GImage)GetChildAt(2);
		Rebate_Title_Text = (GImage)GetChildAt(3);
		Rebate_Message_Btn = (UIActivityRebate_Btn_Message)GetChildAt(4);
		Go_Btn = (GButton)GetChildAt(5);
		Rebate_bg_Pink = (GImage)GetChildAt(6);
		Rebate_num = (GTextField)GetChildAt(7);
		Rebate_num_Text = (GTextField)GetChildAt(8);
		Rebate_num_TextType = (GTextField)GetChildAt(9);
		Rebate_num1 = (GTextField)GetChildAt(12);
		Rebate_num_Text1 = (GTextField)GetChildAt(14);
		mohu = (GGraph)GetChildAt(16);
		RebateWindow = (GGroup)GetChildAt(19);
		Rebate_Time = (GTextField)GetChildAt(20);
		Cutin = GetTransitionAt(0);
	}
}
