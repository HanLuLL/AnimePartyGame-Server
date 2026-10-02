using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIProductRecommendation_Button_RechargeStore : GButton
{
	public Controller redPoint;

	public const string URL = "ui://z8uldgkyqdq5a";

	public static UIProductRecommendation_Button_RechargeStore CreateInstance()
	{
		return (UIProductRecommendation_Button_RechargeStore)UIPackage.CreateObject("ProductRecommendation", "ProductRecommendation_Button_RechargeStore");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		redPoint = GetControllerAt(1);
	}
}
