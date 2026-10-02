using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIStore_Com_Recharge : GComponent
{
	public UIStore_Com_BottomBg buttom;

	public GList list_SubTab;

	public UIStore_Com_ItemBg bg;

	public GList list_RechargeGoods;

	public GTextField txt_timeTip;

	public GTextField txt_LimitPropDesc;

	public GLoader loader_LimitProp;

	public GTextField txt_LimitPropCount;

	public GGroup group_LimitProp;

	public const string URL = "ui://zyd0rl00r3jjqq4v";

	public static UIStore_Com_Recharge CreateInstance()
	{
		return (UIStore_Com_Recharge)UIPackage.CreateObject("Store", "Store_Com_Recharge");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		buttom = (UIStore_Com_BottomBg)GetChildAt(0);
		list_SubTab = (GList)GetChildAt(2);
		bg = (UIStore_Com_ItemBg)GetChildAt(3);
		list_RechargeGoods = (GList)GetChildAt(4);
		txt_timeTip = (GTextField)GetChildAt(5);
		txt_LimitPropDesc = (GTextField)GetChildAt(6);
		loader_LimitProp = (GLoader)GetChildAt(7);
		txt_LimitPropCount = (GTextField)GetChildAt(8);
		group_LimitProp = (GGroup)GetChildAt(9);
	}
}
