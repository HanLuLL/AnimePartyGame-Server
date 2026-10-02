using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIExpressionListWindow : GComponent
{
	public GList list_Plaform;

	public const string URL = "ui://bdqipkfg5o9m0";

	public static UIExpressionListWindow CreateInstance()
	{
		BindAll();
		return (UIExpressionListWindow)UIPackage.CreateObject("ExpressionList", "ExpressionListWindow");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://bdqipkfg5o9m0", typeof(UIExpressionListWindow));
		UIObjectFactory.SetPackageItemExtension("ui://bdqipkfggbr1l", typeof(UIExpression_Com_ChatItem));
		UIObjectFactory.SetPackageItemExtension("ui://bdqipkfgnriba", typeof(UIExpression_Com_ExpressionItem));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		list_Plaform = (GList)GetChildAt(0);
	}
}
