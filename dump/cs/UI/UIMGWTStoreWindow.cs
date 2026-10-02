using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIMGWTStoreWindow : GComponent
{
	public Controller page;

	public GLoader loader_BG;

	public UIMGWT_Com_Advent com_Advent;

	public UIMGWT_Com_Goods com_Goods;

	public GButton btn_Close;

	public const string URL = "ui://2p754tqkilj50";

	public static UIMGWTStoreWindow CreateInstance()
	{
		BindAll();
		return (UIMGWTStoreWindow)UIPackage.CreateObject("MGWTStore", "MGWTStoreWindow");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://2p754tqkilj50", typeof(UIMGWTStoreWindow));
		UIObjectFactory.SetPackageItemExtension("ui://2p754tqkilj51", typeof(UIMGWT_Store_Com_GoodsItem));
		UIObjectFactory.SetPackageItemExtension("ui://2p754tqkilj515", typeof(UIMGWT_Com_Goods));
		UIObjectFactory.SetPackageItemExtension("ui://2p754tqkilj517", typeof(UIMGWT_Store_Buy_Button));
		UIObjectFactory.SetPackageItemExtension("ui://2p754tqkilj51a", typeof(UIMGWT_Com_CommonItem));
		UIObjectFactory.SetPackageItemExtension("ui://2p754tqkilj51b", typeof(UIMGWT_Com_HeroItem));
		UIObjectFactory.SetPackageItemExtension("ui://2p754tqkilj58", typeof(UIMGWT_Com_Advent));
		UIObjectFactory.SetPackageItemExtension("ui://2p754tqkilj5q", typeof(UIMGWT_Preview_Button));
		UIObjectFactory.SetPackageItemExtension("ui://2p754tqkrc482f", typeof(UIMGWT_Com_ExtraAward));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		page = GetControllerAt(0);
		loader_BG = (GLoader)GetChildAt(0);
		com_Advent = (UIMGWT_Com_Advent)GetChildAt(1);
		com_Goods = (UIMGWT_Com_Goods)GetChildAt(2);
		btn_Close = (GButton)GetChildAt(3);
	}
}
