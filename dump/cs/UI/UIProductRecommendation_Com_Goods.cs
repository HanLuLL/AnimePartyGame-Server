using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIProductRecommendation_Com_Goods : GComponent
{
	public GTextField txt_Title;

	public GTextField txt_Time;

	public GList list_Goods;

	public const string URL = "ui://z8uldgkyqdq5o";

	public static UIProductRecommendation_Com_Goods CreateInstance()
	{
		return (UIProductRecommendation_Com_Goods)UIPackage.CreateObject("ProductRecommendation", "ProductRecommendation_Com_Goods");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		txt_Title = (GTextField)GetChildAt(0);
		txt_Time = (GTextField)GetChildAt(1);
		list_Goods = (GList)GetChildAt(2);
	}
}
