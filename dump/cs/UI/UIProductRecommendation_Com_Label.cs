using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIProductRecommendation_Com_Label : GComponent
{
	public Controller labelController;

	public const string URL = "ui://z8uldgkyqdq5i";

	public static UIProductRecommendation_Com_Label CreateInstance()
	{
		return (UIProductRecommendation_Com_Label)UIPackage.CreateObject("ProductRecommendation", "ProductRecommendation_Com_Label");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		labelController = GetControllerAt(0);
	}
}
