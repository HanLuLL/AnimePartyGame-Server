using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIProductRecommendation_Button_ExchangeStore : GButton
{
	public Controller redPoint;

	public const string URL = "ui://z8uldgkyqdq5b";

	public static UIProductRecommendation_Button_ExchangeStore CreateInstance()
	{
		return (UIProductRecommendation_Button_ExchangeStore)UIPackage.CreateObject("ProductRecommendation", "ProductRecommendation_Button_ExchangeStore");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		redPoint = GetControllerAt(1);
	}
}
