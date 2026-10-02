using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIDiscount_Button_Coupons : GButton
{
	public GLoader loader_Icon;

	public GTextField txt_Title;

	public GTextField txt_Time;

	public GTextField txt_Count;

	public GImage di_1;

	public const string URL = "ui://i6k190d4axwd2";

	public static UIDiscount_Button_Coupons CreateInstance()
	{
		return (UIDiscount_Button_Coupons)UIPackage.CreateObject("Discount", "Discount_Button_Coupons");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		loader_Icon = (GLoader)GetChildAt(1);
		txt_Title = (GTextField)GetChildAt(2);
		txt_Time = (GTextField)GetChildAt(3);
		txt_Count = (GTextField)GetChildAt(4);
		di_1 = (GImage)GetChildAt(6);
	}
}
