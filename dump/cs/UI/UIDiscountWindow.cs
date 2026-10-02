using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIDiscountWindow : GComponent
{
	public GGraph mohu;

	public GLabel bottom;

	public GList list_Coupons;

	public GRichTextField txt_Price;

	public GButton btn_Return;

	public GButton btn_Cash;

	public const string URL = "ui://i6k190d4axwd0";

	public static UIDiscountWindow CreateInstance()
	{
		BindAll();
		return (UIDiscountWindow)UIPackage.CreateObject("Discount", "DiscountWindow");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://i6k190d4axwd0", typeof(UIDiscountWindow));
		UIObjectFactory.SetPackageItemExtension("ui://i6k190d4axwd2", typeof(UIDiscount_Button_Coupons));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		mohu = (GGraph)GetChildAt(0);
		bottom = (GLabel)GetChildAt(1);
		list_Coupons = (GList)GetChildAt(2);
		txt_Price = (GRichTextField)GetChildAt(3);
		btn_Return = (GButton)GetChildAt(4);
		btn_Cash = (GButton)GetChildAt(5);
	}
}
