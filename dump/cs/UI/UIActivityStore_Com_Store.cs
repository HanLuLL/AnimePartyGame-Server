using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivityStore_Com_Store : GComponent
{
	public UIActivityStore_Com_BottomBg buttom;

	public UIActivityStore_Com_ItemBg bg;

	public GList list_Store_Goods;

	public GTextField txt_timeTip;

	public const string URL = "ui://88m1yfwgv1vk1e";

	public static UIActivityStore_Com_Store CreateInstance()
	{
		return (UIActivityStore_Com_Store)UIPackage.CreateObject("ActivityStore", "ActivityStore_Com_Store");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		buttom = (UIActivityStore_Com_BottomBg)GetChildAt(0);
		bg = (UIActivityStore_Com_ItemBg)GetChildAt(2);
		list_Store_Goods = (GList)GetChildAt(3);
		txt_timeTip = (GTextField)GetChildAt(4);
	}
}
