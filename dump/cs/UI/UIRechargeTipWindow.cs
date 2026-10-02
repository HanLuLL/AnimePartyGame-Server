using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIRechargeTipWindow : GComponent
{
	public Controller type;

	public GComponent mohu;

	public GLabel bottom;

	public UIRechargeTip_Com_ExchangeItem com_ExchangeItem;

	public UIRechargeTip_Com_ExchangeCurrency com_ExchangeCurrency;

	public const string URL = "ui://c20i191cz7f7i";

	public static UIRechargeTipWindow CreateInstance()
	{
		BindAll();
		return (UIRechargeTipWindow)UIPackage.CreateObject("RechargeTip", "RechargeTipWindow");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://c20i191crle7o", typeof(UIRechargeTip_Com_ExchangeCurrency));
		UIObjectFactory.SetPackageItemExtension("ui://c20i191crle7q", typeof(UIRechargeTip_Slider_Currency));
		UIObjectFactory.SetPackageItemExtension("ui://c20i191crle7r", typeof(UIRechargeTip_Button_Bar));
		UIObjectFactory.SetPackageItemExtension("ui://c20i191crle7u", typeof(UIRechargeTip_Com_ExchangeItem));
		UIObjectFactory.SetPackageItemExtension("ui://c20i191cz7f7i", typeof(UIRechargeTipWindow));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		type = GetControllerAt(0);
		mohu = (GComponent)GetChildAt(0);
		bottom = (GLabel)GetChildAt(1);
		com_ExchangeItem = (UIRechargeTip_Com_ExchangeItem)GetChildAt(2);
		com_ExchangeCurrency = (UIRechargeTip_Com_ExchangeCurrency)GetChildAt(3);
	}
}
