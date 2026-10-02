using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivityComebackPanel : GComponent
{
	public Controller tabType;

	public GLoader loader_BG;

	public UIActivityComeback_Gift_Com gift_Com;

	public UIActivityComeback_Seven_Com seven_Com;

	public UIActivityComeback_Task_Com task_Com;

	public UIActivityComeback_Shop_Com shop_Com;

	public GButton btn_Back;

	public GList tabList;

	public GButton btn_Rule;

	public Transition Cut_in;

	public const string URL = "ui://hconmwfcy9qn1";

	public static UIActivityComebackPanel CreateInstance()
	{
		BindAll();
		return (UIActivityComebackPanel)UIPackage.CreateObject("ActivityComeback", "ActivityComebackPanel");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://hconmwfcvjqj11", typeof(UIActivityComeback_Buy_Button));
		UIObjectFactory.SetPackageItemExtension("ui://hconmwfcvjqj1a", typeof(UIActivityComeback_ListItem));
		UIObjectFactory.SetPackageItemExtension("ui://hconmwfcvjqj1f", typeof(UIActivityComeback_Button_RewardItem));
		UIObjectFactory.SetPackageItemExtension("ui://hconmwfcy9qn1", typeof(UIActivityComebackPanel));
		UIObjectFactory.SetPackageItemExtension("ui://hconmwfcy9qn4", typeof(UIActivityComeback_Tab_Button));
		UIObjectFactory.SetPackageItemExtension("ui://hconmwfcy9qn5", typeof(UIActivityComeback_Questionnaire_Button));
		UIObjectFactory.SetPackageItemExtension("ui://hconmwfcy9qn7", typeof(UIActivityComeback_Gift_Com));
		UIObjectFactory.SetPackageItemExtension("ui://hconmwfcy9qn8", typeof(UIActivityComeback_Seven_Com));
		UIObjectFactory.SetPackageItemExtension("ui://hconmwfcy9qn9", typeof(UIActivityComeback_Task_Com));
		UIObjectFactory.SetPackageItemExtension("ui://hconmwfcy9qna", typeof(UIActivityComeback_Shop_Com));
		UIObjectFactory.SetPackageItemExtension("ui://hconmwfcy9qnc", typeof(UIActivityComeback_Task_Com_Label));
		UIObjectFactory.SetPackageItemExtension("ui://hconmwfcy9qnf", typeof(UIActivityComeback_Task_Button_TaskStatus));
		UIObjectFactory.SetPackageItemExtension("ui://hconmwfcy9qnm", typeof(UIActivityComeback_Shop_Com_BottomBg));
		UIObjectFactory.SetPackageItemExtension("ui://hconmwfcy9qnp", typeof(UIActivityComeback_Shop_Com_BottomMask));
		UIObjectFactory.SetPackageItemExtension("ui://hconmwfcy9qns", typeof(UIActivityComeback_Shop_Com_ItemBg));
		UIObjectFactory.SetPackageItemExtension("ui://hconmwfcy9qnu", typeof(UIActivityComeback_Shop_Com_ItemMask));
		UIObjectFactory.SetPackageItemExtension("ui://hconmwfcy9qnw", typeof(UIActivityComeback_GetGift_Button));
		UIObjectFactory.SetPackageItemExtension("ui://hconmwfcy9qnx", typeof(UIActivityComeback_Hero_Button));
		UIObjectFactory.SetPackageItemExtension("ui://hconmwfcy9qny", typeof(UIActivityComeback_Gift_Item));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		tabType = GetControllerAt(0);
		loader_BG = (GLoader)GetChildAt(0);
		gift_Com = (UIActivityComeback_Gift_Com)GetChildAt(2);
		seven_Com = (UIActivityComeback_Seven_Com)GetChildAt(3);
		task_Com = (UIActivityComeback_Task_Com)GetChildAt(4);
		shop_Com = (UIActivityComeback_Shop_Com)GetChildAt(5);
		btn_Back = (GButton)GetChildAt(7);
		tabList = (GList)GetChildAt(8);
		btn_Rule = (GButton)GetChildAt(9);
		Cut_in = GetTransitionAt(0);
	}
}
