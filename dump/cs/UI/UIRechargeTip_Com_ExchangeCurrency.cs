using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIRechargeTip_Com_ExchangeCurrency : GComponent
{
	public GLabel com_LeftCurrency;

	public GLabel com_RightCurrency;

	public UIRechargeTip_Slider_Currency slider_Count;

	public GButton btn_DelCount;

	public GButton btn_AddCount;

	public GButton btn_Min;

	public GButton btn_Max;

	public GTextField txt_Tip;

	public GButton btn_Purchase;

	public const string URL = "ui://c20i191crle7o";

	public static UIRechargeTip_Com_ExchangeCurrency CreateInstance()
	{
		return (UIRechargeTip_Com_ExchangeCurrency)UIPackage.CreateObject("RechargeTip", "RechargeTip_Com_ExchangeCurrency");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		com_LeftCurrency = (GLabel)GetChildAt(0);
		com_RightCurrency = (GLabel)GetChildAt(1);
		slider_Count = (UIRechargeTip_Slider_Currency)GetChildAt(4);
		btn_DelCount = (GButton)GetChildAt(5);
		btn_AddCount = (GButton)GetChildAt(6);
		btn_Min = (GButton)GetChildAt(7);
		btn_Max = (GButton)GetChildAt(8);
		txt_Tip = (GTextField)GetChildAt(9);
		btn_Purchase = (GButton)GetChildAt(10);
	}
}
