using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIShoppingPanel : GComponent
{
	public UIShopping_Com_Main com_Main;

	public GButton btn_Return;

	public const string URL = "ui://jyj1qox9tb9e0";

	public static UIShoppingPanel CreateInstance()
	{
		BindAll();
		return (UIShoppingPanel)UIPackage.CreateObject("Shopping", "ShoppingPanel");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://jyj1qox9tb9e0", typeof(UIShoppingPanel));
		UIObjectFactory.SetPackageItemExtension("ui://jyj1qox9tb9e1", typeof(UIShopping_Com_Main));
		UIObjectFactory.SetPackageItemExtension("ui://jyj1qox9tb9e3", typeof(UIShopping_Button_Mall));
		UIObjectFactory.SetPackageItemExtension("ui://jyj1qox9tb9e4", typeof(UIShopping_Button_Gacha));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		com_Main = (UIShopping_Com_Main)GetChildAt(0);
		btn_Return = (GButton)GetChildAt(1);
	}
}
