using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIPurchaseWindow : GComponent
{
	public Controller multiState;

	public Controller tokenType;

	public Controller isDiscount;

	public Controller itemType;

	public Controller WinType;

	public GComponent mohu;

	public GLabel bottom;

	public GButton btn_itemLoader;

	public GTextField txt_itemNum;

	public GTextField txt_itemName;

	public GTextField txt_itemDes;

	public GList list_ChestItems;

	public UIPurchase_Com_ContentType com_Explain;

	public GButton btn_addNum;

	public GButton btn_delNum;

	public GTextField txt_itemSelectNum;

	public GButton btn_Min;

	public GButton btn_Max;

	public GLoader loader_Currency;

	public GRichTextField txt_CashSymbol;

	public GTextField txt_UnitPrice;

	public GTextField txt_OriginalPrice;

	public GTextField txt_DiscountPercent;

	public GLoader loader_Currency_Total;

	public GRichTextField txt_CashSymbol_Total;

	public GTextField txt_TotalPrice;

	public GLoader loader_Token;

	public GTextField txt_TokenCount;

	public GButton btn_DelLv;

	public GButton btn_AddLv;

	public UIPurchase_Slider_BattlePassLevel slider_BattlePassLevel;

	public const string URL = "ui://cu17piy4hhk60";

	public static UIPurchaseWindow CreateInstance()
	{
		BindAll();
		return (UIPurchaseWindow)UIPackage.CreateObject("Purchase", "PurchaseWindow");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://cu17piy4hhk60", typeof(UIPurchaseWindow));
		UIObjectFactory.SetPackageItemExtension("ui://cu17piy4jz241g", typeof(UIPurchase_Button_BattlePassLevel));
		UIObjectFactory.SetPackageItemExtension("ui://cu17piy4jz241h", typeof(UIPurchase_Slider_BattlePassLevel));
		UIObjectFactory.SetPackageItemExtension("ui://cu17piy4tb9e17", typeof(UIPurchase_Com_ChestItem));
		UIObjectFactory.SetPackageItemExtension("ui://cu17piy4tb9e18", typeof(UIPurchase_Com_ContentType));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		multiState = GetControllerAt(0);
		tokenType = GetControllerAt(1);
		isDiscount = GetControllerAt(2);
		itemType = GetControllerAt(3);
		WinType = GetControllerAt(4);
		mohu = (GComponent)GetChildAt(0);
		bottom = (GLabel)GetChildAt(1);
		btn_itemLoader = (GButton)GetChildAt(2);
		txt_itemNum = (GTextField)GetChildAt(3);
		txt_itemName = (GTextField)GetChildAt(5);
		txt_itemDes = (GTextField)GetChildAt(7);
		list_ChestItems = (GList)GetChildAt(8);
		com_Explain = (UIPurchase_Com_ContentType)GetChildAt(9);
		btn_addNum = (GButton)GetChildAt(10);
		btn_delNum = (GButton)GetChildAt(11);
		txt_itemSelectNum = (GTextField)GetChildAt(13);
		btn_Min = (GButton)GetChildAt(14);
		btn_Max = (GButton)GetChildAt(15);
		loader_Currency = (GLoader)GetChildAt(19);
		txt_CashSymbol = (GRichTextField)GetChildAt(20);
		txt_UnitPrice = (GTextField)GetChildAt(21);
		txt_OriginalPrice = (GTextField)GetChildAt(23);
		txt_DiscountPercent = (GTextField)GetChildAt(24);
		loader_Currency_Total = (GLoader)GetChildAt(27);
		txt_CashSymbol_Total = (GRichTextField)GetChildAt(28);
		txt_TotalPrice = (GTextField)GetChildAt(30);
		loader_Token = (GLoader)GetChildAt(34);
		txt_TokenCount = (GTextField)GetChildAt(35);
		btn_DelLv = (GButton)GetChildAt(36);
		btn_AddLv = (GButton)GetChildAt(37);
		slider_BattlePassLevel = (UIPurchase_Slider_BattlePassLevel)GetChildAt(38);
	}
}
