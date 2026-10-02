using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UILandShopWindow : GComponent
{
	public Controller IsShopEvent;

	public Controller Hide;

	public Controller type;

	public GList list_Card;

	public GGraph effect;

	public UILandShop_Button_Buy btn_Pay;

	public GButton btn_Leave;

	public GTextField myGold;

	public GButton btn_Transfer;

	public GTextField txt_HideTip;

	public Transition Cut_in;

	public const string URL = "ui://d5ngzgeurum70";

	public static UILandShopWindow CreateInstance()
	{
		BindAll();
		return (UILandShopWindow)UIPackage.CreateObject("LandShop", "LandShopWindow");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://d5ngzgeuhmjfd", typeof(UILandShop_BG));
		UIObjectFactory.SetPackageItemExtension("ui://d5ngzgeurum70", typeof(UILandShopWindow));
		UIObjectFactory.SetPackageItemExtension("ui://d5ngzgeurum72", typeof(UILandShop_Button_Card));
		UIObjectFactory.SetPackageItemExtension("ui://d5ngzgeurum76", typeof(UILandShop_Button_Buy));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		IsShopEvent = GetControllerAt(0);
		Hide = GetControllerAt(1);
		type = GetControllerAt(2);
		list_Card = (GList)GetChildAt(2);
		effect = (GGraph)GetChildAt(4);
		btn_Pay = (UILandShop_Button_Buy)GetChildAt(5);
		btn_Leave = (GButton)GetChildAt(6);
		myGold = (GTextField)GetChildAt(10);
		btn_Transfer = (GButton)GetChildAt(12);
		txt_HideTip = (GTextField)GetChildAt(16);
		Cut_in = GetTransitionAt(0);
	}
}
