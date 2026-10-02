using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIPropDetailWindow : GComponent
{
	public Controller itemType;

	public Controller chestType;

	public Controller wayAvaliable;

	public Controller recycle;

	public Controller type;

	public GComponent mohu;

	public GLabel bottom;

	public GButton btn_item;

	public GTextField txt_itemName;

	public GTextField txt_itemDes;

	public GList list_PropWay;

	public GList list_ChestItems;

	public GButton btn_addNum;

	public GButton btn_delNum;

	public GTextField txt_itemSelectNum;

	public GButton btn_Min;

	public GButton btn_Max;

	public UIPropDetail_Com_ContentType com_Explain;

	public GTextField txt_Recycle;

	public GList list_Way;

	public const string URL = "ui://307oxfbquins15";

	public static UIPropDetailWindow CreateInstance()
	{
		BindAll();
		return (UIPropDetailWindow)UIPackage.CreateObject("PropDetail", "PropDetailWindow");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://307oxfbqhg7x1p", typeof(UIPropDetail_Com_ContentType));
		UIObjectFactory.SetPackageItemExtension("ui://307oxfbquins15", typeof(UIPropDetailWindow));
		UIObjectFactory.SetPackageItemExtension("ui://307oxfbquins17", typeof(UIPropDetail_Com_ChestItem));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		itemType = GetControllerAt(0);
		chestType = GetControllerAt(1);
		wayAvaliable = GetControllerAt(2);
		recycle = GetControllerAt(3);
		type = GetControllerAt(4);
		mohu = (GComponent)GetChildAt(0);
		bottom = (GLabel)GetChildAt(1);
		btn_item = (GButton)GetChildAt(2);
		txt_itemName = (GTextField)GetChildAt(3);
		txt_itemDes = (GTextField)GetChildAt(5);
		list_PropWay = (GList)GetChildAt(6);
		list_ChestItems = (GList)GetChildAt(7);
		btn_addNum = (GButton)GetChildAt(8);
		btn_delNum = (GButton)GetChildAt(9);
		txt_itemSelectNum = (GTextField)GetChildAt(11);
		btn_Min = (GButton)GetChildAt(12);
		btn_Max = (GButton)GetChildAt(13);
		com_Explain = (UIPropDetail_Com_ContentType)GetChildAt(15);
		txt_Recycle = (GTextField)GetChildAt(17);
		list_Way = (GList)GetChildAt(20);
	}
}
