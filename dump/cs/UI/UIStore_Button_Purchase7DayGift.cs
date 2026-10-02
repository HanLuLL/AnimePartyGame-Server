using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIStore_Button_Purchase7DayGift : GButton
{
	public Controller isDiscount;

	public GLoader loader_Up;

	public GLoader loader_Down;

	public GTextField txt_Time;

	public GTextField txt_OriginalPrice;

	public GTextField txt_DiscountPercent;

	public GRichTextField txt_Price;

	public const string URL = "ui://zyd0rl0011biqv";

	public static UIStore_Button_Purchase7DayGift CreateInstance()
	{
		return (UIStore_Button_Purchase7DayGift)UIPackage.CreateObject("Store", "Store_Button_Purchase7DayGift");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		isDiscount = GetControllerAt(1);
		loader_Up = (GLoader)GetChildAt(0);
		loader_Down = (GLoader)GetChildAt(1);
		txt_Time = (GTextField)GetChildAt(2);
		txt_OriginalPrice = (GTextField)GetChildAt(3);
		txt_DiscountPercent = (GTextField)GetChildAt(6);
		txt_Price = (GRichTextField)GetChildAt(8);
	}
}
