using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIStore_Button_PurchaseMonthCard : GButton
{
	public Controller type;

	public GRichTextField txt_Price;

	public const string URL = "ui://zyd0rl00m1i7qq24";

	public static UIStore_Button_PurchaseMonthCard CreateInstance()
	{
		return (UIStore_Button_PurchaseMonthCard)UIPackage.CreateObject("Store", "Store_Button_PurchaseMonthCard");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		type = GetControllerAt(1);
		txt_Price = (GRichTextField)GetChildAt(3);
	}
}
