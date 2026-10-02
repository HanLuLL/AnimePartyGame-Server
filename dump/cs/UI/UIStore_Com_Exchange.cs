using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIStore_Com_Exchange : GComponent
{
	public GLoader Character;

	public UIStore_Com_BottomBg buttom;

	public GList list_SubTab;

	public UIStore_Com_ItemBg bg;

	public GList list_ExchangeGoods;

	public GTextField txt_timeTip;

	public GTextField txt_LimitPropDesc;

	public GLoader loader_LimitProp;

	public GTextField txt_LimitPropCount;

	public GGroup group_LimitProp;

	public Transition Cut_in;

	public const string URL = "ui://zyd0rl00r3jjqq4h";

	public static UIStore_Com_Exchange CreateInstance()
	{
		return (UIStore_Com_Exchange)UIPackage.CreateObject("Store", "Store_Com_Exchange");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		Character = (GLoader)GetChildAt(0);
		buttom = (UIStore_Com_BottomBg)GetChildAt(1);
		list_SubTab = (GList)GetChildAt(3);
		bg = (UIStore_Com_ItemBg)GetChildAt(4);
		list_ExchangeGoods = (GList)GetChildAt(5);
		txt_timeTip = (GTextField)GetChildAt(6);
		txt_LimitPropDesc = (GTextField)GetChildAt(7);
		loader_LimitProp = (GLoader)GetChildAt(8);
		txt_LimitPropCount = (GTextField)GetChildAt(9);
		group_LimitProp = (GGroup)GetChildAt(10);
		Cut_in = GetTransitionAt(0);
	}
}
