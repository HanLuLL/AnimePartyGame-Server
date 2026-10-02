using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIRechargeTip_Com_ExchangeItem : GComponent
{
	public GButton btn_Purchase;

	public GLabel com_LeftCurrency;

	public GLabel com_RightCurrency;

	public GRichTextField txt_TotalInfo;

	public const string URL = "ui://c20i191crle7u";

	public static UIRechargeTip_Com_ExchangeItem CreateInstance()
	{
		return (UIRechargeTip_Com_ExchangeItem)UIPackage.CreateObject("RechargeTip", "RechargeTip_Com_ExchangeItem");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		btn_Purchase = (GButton)GetChildAt(0);
		com_LeftCurrency = (GLabel)GetChildAt(1);
		com_RightCurrency = (GLabel)GetChildAt(2);
		txt_TotalInfo = (GRichTextField)GetChildAt(5);
	}
}
