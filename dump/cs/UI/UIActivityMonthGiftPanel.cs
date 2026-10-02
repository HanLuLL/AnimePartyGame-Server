using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivityMonthGiftPanel : GComponent
{
	public GLoader ActivityMonthGift_bg;

	public GImage Can_Condition;

	public GImage Cannot_Condition;

	public GGroup ActivityMonthGift_Condition;

	public GTextField ActivityMonthGift_Message;

	public GTextField ActivityMonthGift_Time;

	public GButton ActivityMonthGift_Btn_Message;

	public GGraph mohu;

	public GGroup ActivityMonthGift_Window;

	public Transition Cut_in;

	public const string URL = "ui://pietpp6yrr350";

	public static UIActivityMonthGiftPanel CreateInstance()
	{
		BindAll();
		return (UIActivityMonthGiftPanel)UIPackage.CreateObject("ActivityMonthGift", "ActivityMonthGiftPanel");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://pietpp6yrr350", typeof(UIActivityMonthGiftPanel));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		ActivityMonthGift_bg = (GLoader)GetChildAt(0);
		Can_Condition = (GImage)GetChildAt(11);
		Cannot_Condition = (GImage)GetChildAt(12);
		ActivityMonthGift_Condition = (GGroup)GetChildAt(13);
		ActivityMonthGift_Message = (GTextField)GetChildAt(16);
		ActivityMonthGift_Time = (GTextField)GetChildAt(17);
		ActivityMonthGift_Btn_Message = (GButton)GetChildAt(19);
		mohu = (GGraph)GetChildAt(27);
		ActivityMonthGift_Window = (GGroup)GetChildAt(30);
		Cut_in = GetTransitionAt(0);
	}
}
