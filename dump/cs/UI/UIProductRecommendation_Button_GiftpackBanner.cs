using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIProductRecommendation_Button_GiftpackBanner : GButton
{
	public Controller style;

	public GLoader loader_Image;

	public GTextField txt_Title_1;

	public GTextField txt_Title_2;

	public GTextField txt_Title_3;

	public const string URL = "ui://z8uldgkyqdq55";

	public static UIProductRecommendation_Button_GiftpackBanner CreateInstance()
	{
		return (UIProductRecommendation_Button_GiftpackBanner)UIPackage.CreateObject("ProductRecommendation", "ProductRecommendation_Button_GiftpackBanner");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		style = GetControllerAt(1);
		loader_Image = (GLoader)GetChildAt(1);
		txt_Title_1 = (GTextField)GetChildAt(2);
		txt_Title_2 = (GTextField)GetChildAt(3);
		txt_Title_3 = (GTextField)GetChildAt(4);
	}
}
