using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIAnniversary_2nd_Button_Price : GButton
{
	public Controller status;

	public Controller hasDiscount;

	public GRichTextField txt_OriginalPrice;

	public GRichTextField txt_DisscountPrice;

	public GTextField txt_Discount;

	public const string URL = "ui://k49wk9ftnqmf18";

	public static UIAnniversary_2nd_Button_Price CreateInstance()
	{
		return (UIAnniversary_2nd_Button_Price)UIPackage.CreateObject("Anniversary_2Nd", "Anniversary_2nd_Button_Price");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		status = GetControllerAt(1);
		hasDiscount = GetControllerAt(2);
		txt_OriginalPrice = (GRichTextField)GetChildAt(3);
		txt_DisscountPrice = (GRichTextField)GetChildAt(4);
		txt_Discount = (GTextField)GetChildAt(6);
	}
}
