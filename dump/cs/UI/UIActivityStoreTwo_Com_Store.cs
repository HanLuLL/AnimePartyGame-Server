using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivityStoreTwo_Com_Store : GComponent
{
	public UIActivityStoreTwo_Com_BottomBg buttom;

	public UIActivityStoreTwo_Com_ItemBg bg;

	public GList list_Store_Goods;

	public GTextField txt_timeTip;

	public const string URL = "ui://6dt5s4htqw8h11";

	public static UIActivityStoreTwo_Com_Store CreateInstance()
	{
		return (UIActivityStoreTwo_Com_Store)UIPackage.CreateObject("ActivityStoreTwo", "ActivityStoreTwo_Com_Store");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		buttom = (UIActivityStoreTwo_Com_BottomBg)GetChildAt(0);
		bg = (UIActivityStoreTwo_Com_ItemBg)GetChildAt(2);
		list_Store_Goods = (GList)GetChildAt(3);
		txt_timeTip = (GTextField)GetChildAt(4);
	}
}
