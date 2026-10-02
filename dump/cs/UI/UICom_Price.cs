using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UICom_Price : GComponent
{
	public Controller isDiscount;

	public GRichTextField txt_SalePrice;

	public GTextField txt_OriginalPrice;

	public GTextField txt_DiscountPercent;

	public GGroup group_Discount;

	public const string URL = "ui://m6sn3r22ot0w9";

	public static UICom_Price CreateInstance()
	{
		return (UICom_Price)UIPackage.CreateObject("Common_External", "Com_Price");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		isDiscount = GetControllerAt(0);
		txt_SalePrice = (GRichTextField)GetChildAt(0);
		txt_OriginalPrice = (GTextField)GetChildAt(1);
		txt_DiscountPercent = (GTextField)GetChildAt(4);
		group_Discount = (GGroup)GetChildAt(5);
	}
}
