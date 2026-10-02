using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIProductRecommendationPanel : GComponent
{
	public Controller language;

	public UIProductRecommendation_Com_GiftpackBanner com_GiftpackBanner;

	public UIProductRecommendation_Button_RechargeStore btn_RechargeStore;

	public UIProductRecommendation_Button_ExchangeStore btn_ExchangeStore;

	public UIProductRecommendation_Com_Goods com_Goods;

	public GButton btn_Return;

	public GGraph com_MaskGoods;

	public Transition Cut_in;

	public const string URL = "ui://z8uldgkyqdq50";

	public static UIProductRecommendationPanel CreateInstance()
	{
		BindAll();
		return (UIProductRecommendationPanel)UIPackage.CreateObject("ProductRecommendation", "ProductRecommendationPanel");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://z8uldgkyqdq50", typeof(UIProductRecommendationPanel));
		UIObjectFactory.SetPackageItemExtension("ui://z8uldgkyqdq54", typeof(UIProductRecommendation_Com_GiftpackBanner));
		UIObjectFactory.SetPackageItemExtension("ui://z8uldgkyqdq55", typeof(UIProductRecommendation_Button_GiftpackBanner));
		UIObjectFactory.SetPackageItemExtension("ui://z8uldgkyqdq5a", typeof(UIProductRecommendation_Button_RechargeStore));
		UIObjectFactory.SetPackageItemExtension("ui://z8uldgkyqdq5b", typeof(UIProductRecommendation_Button_ExchangeStore));
		UIObjectFactory.SetPackageItemExtension("ui://z8uldgkyqdq5c", typeof(UIProductRecommendation_Button_GoodsItem));
		UIObjectFactory.SetPackageItemExtension("ui://z8uldgkyqdq5i", typeof(UIProductRecommendation_Com_Label));
		UIObjectFactory.SetPackageItemExtension("ui://z8uldgkyqdq5k", typeof(UIProductRecommendation_Com_Price));
		UIObjectFactory.SetPackageItemExtension("ui://z8uldgkyqdq5o", typeof(UIProductRecommendation_Com_Goods));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		language = GetControllerAt(0);
		com_GiftpackBanner = (UIProductRecommendation_Com_GiftpackBanner)GetChildAt(2);
		btn_RechargeStore = (UIProductRecommendation_Button_RechargeStore)GetChildAt(5);
		btn_ExchangeStore = (UIProductRecommendation_Button_ExchangeStore)GetChildAt(6);
		com_Goods = (UIProductRecommendation_Com_Goods)GetChildAt(7);
		btn_Return = (GButton)GetChildAt(8);
		com_MaskGoods = (GGraph)GetChildAt(9);
		Cut_in = GetTransitionAt(0);
	}
}
