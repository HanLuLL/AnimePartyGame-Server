using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UINGOStoreWindow : GComponent
{
	public Controller page;

	public GGraph loader_BG;

	public UINGOStore_Com_Advert com_Advert;

	public UINGOStore_Com_Goods com_Goods;

	public GButton btn_Close;

	public const string URL = "ui://na6sy4s6kqgjr";

	public static UINGOStoreWindow CreateInstance()
	{
		BindAll();
		return (UINGOStoreWindow)UIPackage.CreateObject("NGOStore", "NGOStoreWindow");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://na6sy4s6kqgj14", typeof(UINGOStore_Com_Goods));
		UIObjectFactory.SetPackageItemExtension("ui://na6sy4s6kqgj15", typeof(UINGOStore_Com_GoodsItem));
		UIObjectFactory.SetPackageItemExtension("ui://na6sy4s6kqgj1a", typeof(UINGOStore_Button_Purchase));
		UIObjectFactory.SetPackageItemExtension("ui://na6sy4s6kqgji", typeof(UINGOStore_Com_Advert));
		UIObjectFactory.SetPackageItemExtension("ui://na6sy4s6kqgjj", typeof(UINGOStore_Button_GoStore));
		UIObjectFactory.SetPackageItemExtension("ui://na6sy4s6kqgjk", typeof(UINGOStore_Button_PreviewSkin));
		UIObjectFactory.SetPackageItemExtension("ui://na6sy4s6kqgjm", typeof(UINGOStore_Com_CharacterInfo));
		UIObjectFactory.SetPackageItemExtension("ui://na6sy4s6kqgjr", typeof(UINGOStoreWindow));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		page = GetControllerAt(0);
		loader_BG = (GGraph)GetChildAt(0);
		com_Advert = (UINGOStore_Com_Advert)GetChildAt(1);
		com_Goods = (UINGOStore_Com_Goods)GetChildAt(2);
		btn_Close = (GButton)GetChildAt(3);
	}
}
