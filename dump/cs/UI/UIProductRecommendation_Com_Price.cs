using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIProductRecommendation_Com_Price : GComponent
{
	public Controller isDiscount;

	public Controller isToken;

	public Controller free;

	public GLoader loader_Icon;

	public GTextField txt_TokenSymbol;

	public GTextField txt_DiscountPrice;

	public GTextField txt_OriginalPrice;

	public GTextField txt_DiscountPercent;

	public const string URL = "ui://z8uldgkyqdq5k";

	public static UIProductRecommendation_Com_Price CreateInstance()
	{
		return (UIProductRecommendation_Com_Price)UIPackage.CreateObject("ProductRecommendation", "ProductRecommendation_Com_Price");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		isDiscount = GetControllerAt(0);
		isToken = GetControllerAt(1);
		free = GetControllerAt(2);
		loader_Icon = (GLoader)GetChildAt(0);
		txt_TokenSymbol = (GTextField)GetChildAt(1);
		txt_DiscountPrice = (GTextField)GetChildAt(2);
		txt_OriginalPrice = (GTextField)GetChildAt(4);
		txt_DiscountPercent = (GTextField)GetChildAt(7);
	}
}
