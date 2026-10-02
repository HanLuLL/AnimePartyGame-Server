using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIStore_Button_Way : GButton
{
	public GTextField txt_Time;

	public GRichTextField txt_DiscountPrice;

	public const string URL = "ui://zyd0rl0011biq20";

	public static UIStore_Button_Way CreateInstance()
	{
		return (UIStore_Button_Way)UIPackage.CreateObject("Store", "Store_Button_Way");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		txt_Time = (GTextField)GetChildAt(2);
		txt_DiscountPrice = (GRichTextField)GetChildAt(3);
	}
}
