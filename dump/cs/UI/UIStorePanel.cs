using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIStorePanel : GComponent
{
	public Controller goodsType;

	public Controller storeType;

	public UIStore_Com_CharacterGift com_CharacterGift;

	public UIStore_Com_Day7Gift_PVP com_7Day_PVP;

	public UIStore_Com_Day7Gift_PVE com_7Day_PVE;

	public UIStore_Com_Anniversary_2nd com_Anniversary_2nd;

	public UIStore_Com_MonthCard com_MonthCard;

	public UIStore_Com_Recommend com_Recommend;

	public UIStore_Com_GiftPackage com_GiftPackage;

	public UIStore_Com_Skin com_Skin;

	public UIStore_Com_Exchange com_Exchange;

	public UIStore_Com_Recharge com_Recharge;

	public GButton btn_SpecifiedCommercialTransactions;

	public GButton btn_PaymentServicesAct;

	public UIStore_SwitchTab com_ShelfTab;

	public GButton btn_Return;

	public Transition PTSP_Cut_in_MoRen;

	public Transition PFLB_Cut_in;

	public Transition PTLB_Cut_in;

	public const string URL = "ui://zyd0rl00qdq50";

	public static UIStorePanel CreateInstance()
	{
		BindAll();
		return (UIStorePanel)UIPackage.CreateObject("Store", "StorePanel");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://zyd0rl0011biq1p", typeof(UIStore_Button_GetStatusBottom));
		UIObjectFactory.SetPackageItemExtension("ui://zyd0rl0011biq1q", typeof(UIStore_Button_FinishStatus));
		UIObjectFactory.SetPackageItemExtension("ui://zyd0rl0011biq1r", typeof(UIStore_Button_Reward7DayGift));
		UIObjectFactory.SetPackageItemExtension("ui://zyd0rl0011biq1s", typeof(UIStore_Com_CharacterGift));
		UIObjectFactory.SetPackageItemExtension("ui://zyd0rl0011biq20", typeof(UIStore_Button_Way));
		UIObjectFactory.SetPackageItemExtension("ui://zyd0rl0011biqq21", typeof(UIStore_Button_GetStatus));
		UIObjectFactory.SetPackageItemExtension("ui://zyd0rl0011biqu", typeof(UIStore_Com_Day7Gift_PVP));
		UIObjectFactory.SetPackageItemExtension("ui://zyd0rl0011biqv", typeof(UIStore_Button_Purchase7DayGift));
		UIObjectFactory.SetPackageItemExtension("ui://zyd0rl0011biqy", typeof(UIStore_Com_7GiftRewardTab));
		UIObjectFactory.SetPackageItemExtension("ui://zyd0rl007h6sqq2z", typeof(UIStore_Button_RecommendItem));
		UIObjectFactory.SetPackageItemExtension("ui://zyd0rl007h6sqq34", typeof(UIStore_Com_Recommend));
		UIObjectFactory.SetPackageItemExtension("ui://zyd0rl007h6sqq35", typeof(UIStore_Com_RecommendEntity));
		UIObjectFactory.SetPackageItemExtension("ui://zyd0rl007h6sqq36", typeof(UIStore_Button_Recommend));
		UIObjectFactory.SetPackageItemExtension("ui://zyd0rl00c76fqq5v", typeof(UIStore_Com_Anniversary_2nd_Reward));
		UIObjectFactory.SetPackageItemExtension("ui://zyd0rl00ekc0qq3p", typeof(UIStore_Com_Day7Gift_PVE_Reward));
		UIObjectFactory.SetPackageItemExtension("ui://zyd0rl00fxqjqq5h", typeof(UIButtonsto_Grip));
		UIObjectFactory.SetPackageItemExtension("ui://zyd0rl00h0atqq5b", typeof(UIStore_Button_SkinTab));
		UIObjectFactory.SetPackageItemExtension("ui://zyd0rl00m1i7qq24", typeof(UIStore_Button_PurchaseMonthCard));
		UIObjectFactory.SetPackageItemExtension("ui://zyd0rl00m1i7qq25", typeof(UIStore_Com_MonthCard));
		UIObjectFactory.SetPackageItemExtension("ui://zyd0rl00px78qq3h", typeof(UIStore_Com_Day7Gift_PVE));
		UIObjectFactory.SetPackageItemExtension("ui://zyd0rl00qdq50", typeof(UIStorePanel));
		UIObjectFactory.SetPackageItemExtension("ui://zyd0rl00qdq5j", typeof(UIStore_Com_Label));
		UIObjectFactory.SetPackageItemExtension("ui://zyd0rl00qdq5r", typeof(UIStore_Button_SkinItem));
		UIObjectFactory.SetPackageItemExtension("ui://zyd0rl00qw8hqq5q", typeof(UIStore_Com_Anniversary_2nd));
		UIObjectFactory.SetPackageItemExtension("ui://zyd0rl00r3jjqq3s", typeof(UIStore_SwitchTab));
		UIObjectFactory.SetPackageItemExtension("ui://zyd0rl00r3jjqq4g", typeof(UIStore_SwitchTab_Button));
		UIObjectFactory.SetPackageItemExtension("ui://zyd0rl00r3jjqq4h", typeof(UIStore_Com_Exchange));
		UIObjectFactory.SetPackageItemExtension("ui://zyd0rl00r3jjqq4i", typeof(UIStore_Com_BottomBg));
		UIObjectFactory.SetPackageItemExtension("ui://zyd0rl00r3jjqq4l", typeof(UIStore_Com_BottomMask));
		UIObjectFactory.SetPackageItemExtension("ui://zyd0rl00r3jjqq4n", typeof(UIStore_Com_ItemMask));
		UIObjectFactory.SetPackageItemExtension("ui://zyd0rl00r3jjqq4s", typeof(UIStore_Com_ItemBg));
		UIObjectFactory.SetPackageItemExtension("ui://zyd0rl00r3jjqq4t", typeof(UIStore_GoodsList_SwitchTabButton));
		UIObjectFactory.SetPackageItemExtension("ui://zyd0rl00r3jjqq4v", typeof(UIStore_Com_Recharge));
		UIObjectFactory.SetPackageItemExtension("ui://zyd0rl00s9kvqq5p", typeof(UIBaiDi));
		UIObjectFactory.SetPackageItemExtension("ui://zyd0rl00vs31qq4w", typeof(UIStore_Com_GiftPackage));
		UIObjectFactory.SetPackageItemExtension("ui://zyd0rl00vs31qq50", typeof(UIStore_Item_GiftPackage));
		UIObjectFactory.SetPackageItemExtension("ui://zyd0rl00vs31qq51", typeof(UIStore_Com_Skin));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		goodsType = GetControllerAt(0);
		storeType = GetControllerAt(1);
		com_CharacterGift = (UIStore_Com_CharacterGift)GetChildAt(0);
		com_7Day_PVP = (UIStore_Com_Day7Gift_PVP)GetChildAt(1);
		com_7Day_PVE = (UIStore_Com_Day7Gift_PVE)GetChildAt(2);
		com_Anniversary_2nd = (UIStore_Com_Anniversary_2nd)GetChildAt(3);
		com_MonthCard = (UIStore_Com_MonthCard)GetChildAt(4);
		com_Recommend = (UIStore_Com_Recommend)GetChildAt(5);
		com_GiftPackage = (UIStore_Com_GiftPackage)GetChildAt(6);
		com_Skin = (UIStore_Com_Skin)GetChildAt(7);
		com_Exchange = (UIStore_Com_Exchange)GetChildAt(8);
		com_Recharge = (UIStore_Com_Recharge)GetChildAt(9);
		btn_SpecifiedCommercialTransactions = (GButton)GetChildAt(10);
		btn_PaymentServicesAct = (GButton)GetChildAt(11);
		com_ShelfTab = (UIStore_SwitchTab)GetChildAt(13);
		btn_Return = (GButton)GetChildAt(14);
		PTSP_Cut_in_MoRen = GetTransitionAt(0);
		PFLB_Cut_in = GetTransitionAt(1);
		PTLB_Cut_in = GetTransitionAt(2);
	}
}
