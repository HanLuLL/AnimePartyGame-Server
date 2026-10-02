using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIWitchWeaponWindow : GComponent
{
	public Controller page;

	public GGraph loader_BG;

	public UIWitchWeapon_Com_Advert com_Advert;

	public UIWitchWeapon_Com_Goods com_Goods;

	public GButton btn_Close;

	public const string URL = "ui://hn2q98k6ollu1g";

	public static UIWitchWeaponWindow CreateInstance()
	{
		BindAll();
		return (UIWitchWeaponWindow)UIPackage.CreateObject("WitchWeapon", "WitchWeaponWindow");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://hn2q98k6kqgj14", typeof(UIWitchWeapon_Com_Goods));
		UIObjectFactory.SetPackageItemExtension("ui://hn2q98k6kqgj15", typeof(UIWitchWeapon_Com_GoodsItem));
		UIObjectFactory.SetPackageItemExtension("ui://hn2q98k6kqgj1a", typeof(UIWitchWeapon_Button_Purchase));
		UIObjectFactory.SetPackageItemExtension("ui://hn2q98k6kqgji", typeof(UIWitchWeapon_Com_Advert));
		UIObjectFactory.SetPackageItemExtension("ui://hn2q98k6kqgjj", typeof(UIWitchWeapon_Button_GoStore));
		UIObjectFactory.SetPackageItemExtension("ui://hn2q98k6kqgjm", typeof(UIWitchWeapon_Com_CharacterInfo));
		UIObjectFactory.SetPackageItemExtension("ui://hn2q98k6ollu1g", typeof(UIWitchWeaponWindow));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		page = GetControllerAt(0);
		loader_BG = (GGraph)GetChildAt(0);
		com_Advert = (UIWitchWeapon_Com_Advert)GetChildAt(2);
		com_Goods = (UIWitchWeapon_Com_Goods)GetChildAt(3);
		btn_Close = (GButton)GetChildAt(4);
	}
}
