using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIStore_Button_Recommend : GButton
{
	public Controller isDiscount;

	public Controller country;

	public GTextField txt_Time;

	public GImage txt_TokenSymbol;

	public GTextField txt_DiscountPrice;

	public GTextField txt_OriginalPrice;

	public GTextField txt_DiscountPercent;

	public const string URL = "ui://zyd0rl007h6sqq36";

	public static UIStore_Button_Recommend CreateInstance()
	{
		return (UIStore_Button_Recommend)UIPackage.CreateObject("Store", "Store_Button_Recommend");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		isDiscount = GetControllerAt(1);
		country = GetControllerAt(2);
		txt_Time = (GTextField)GetChildAt(2);
		txt_TokenSymbol = (GImage)GetChildAt(4);
		txt_DiscountPrice = (GTextField)GetChildAt(5);
		txt_OriginalPrice = (GTextField)GetChildAt(6);
		txt_DiscountPercent = (GTextField)GetChildAt(9);
	}
}
