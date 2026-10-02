using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIFashionPanel : GComponent
{
	public Controller planConfig;

	public Controller slot;

	public GGraph graph_2;

	public GButton btn_Return;

	public GComponent com_Label;

	public UIFashion_Button_Video btn_Video;

	public GComponent com_CardBack;

	public UIFashion_Com_video com_MainBack;

	public GTextField txt_Name;

	public GTextField txt_Desc;

	public GButton btn_link;

	public GGraph graph_1;

	public GList list_Tab;

	public GList list_Items;

	public GButton com_Item_Head;

	public GButton com_Item_Label;

	public GButton com_Item_Card;

	public GButton com_Item_Dice;

	public GButton com_Item_Effect;

	public GButton com_Item_Main;

	public UIFashion_Button_Change btn_Save;

	public UIFashion_Button_Change btn_reset;

	public const string URL = "ui://dl889m5qlsb445";

	public static UIFashionPanel CreateInstance()
	{
		BindAll();
		return (UIFashionPanel)UIPackage.CreateObject("Fashion", "FashionPanel");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://dl889m5qdx3g4w", typeof(UIFashion_Button_Type));
		UIObjectFactory.SetPackageItemExtension("ui://dl889m5qdx3g4y", typeof(UIFashion_Button_Video));
		UIObjectFactory.SetPackageItemExtension("ui://dl889m5qlsb445", typeof(UIFashionPanel));
		UIObjectFactory.SetPackageItemExtension("ui://dl889m5qlsb44e", typeof(UIFashion_Button_PropItem));
		UIObjectFactory.SetPackageItemExtension("ui://dl889m5qlsb44g", typeof(UIFashion_Button_Change));
		UIObjectFactory.SetPackageItemExtension("ui://dl889m5qu6bm50", typeof(UIFashion_Com_video));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		planConfig = GetControllerAt(0);
		slot = GetControllerAt(1);
		graph_2 = (GGraph)GetChildAt(0);
		btn_Return = (GButton)GetChildAt(1);
		com_Label = (GComponent)GetChildAt(2);
		btn_Video = (UIFashion_Button_Video)GetChildAt(3);
		com_CardBack = (GComponent)GetChildAt(4);
		com_MainBack = (UIFashion_Com_video)GetChildAt(5);
		txt_Name = (GTextField)GetChildAt(6);
		txt_Desc = (GTextField)GetChildAt(7);
		btn_link = (GButton)GetChildAt(8);
		graph_1 = (GGraph)GetChildAt(9);
		list_Tab = (GList)GetChildAt(10);
		list_Items = (GList)GetChildAt(11);
		com_Item_Head = (GButton)GetChildAt(12);
		com_Item_Label = (GButton)GetChildAt(13);
		com_Item_Card = (GButton)GetChildAt(14);
		com_Item_Dice = (GButton)GetChildAt(15);
		com_Item_Effect = (GButton)GetChildAt(16);
		com_Item_Main = (GButton)GetChildAt(17);
		btn_Save = (UIFashion_Button_Change)GetChildAt(20);
		btn_reset = (UIFashion_Button_Change)GetChildAt(21);
	}
}
