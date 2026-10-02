using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UISkinSellWindow : GComponent
{
	public Controller tab;

	public GLoader bg;

	public UISkinSell_Com_S5 com_Main_1;

	public UISkinSell_Com_Goods com_Goods;

	public Transition Cut_in_CN;

	public Transition Cut_in_EN;

	public Transition Cut_in_JP;

	public const string URL = "ui://hmljxqy1ub0x0";

	public static UISkinSellWindow CreateInstance()
	{
		BindAll();
		return (UISkinSellWindow)UIPackage.CreateObject("SkinSell", "SkinSellWindow");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://hmljxqy1fu2t11", typeof(UISkinSell_Com_SkinItem));
		UIObjectFactory.SetPackageItemExtension("ui://hmljxqy1kvfs9q", typeof(UISkinSell_Com_S5));
		UIObjectFactory.SetPackageItemExtension("ui://hmljxqy1ub0x0", typeof(UISkinSellWindow));
		UIObjectFactory.SetPackageItemExtension("ui://hmljxqy1ub0x4", typeof(UISkinSell_Button_Price));
		UIObjectFactory.SetPackageItemExtension("ui://hmljxqy1ub0x6", typeof(UISkinSell_Com_Goods));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		tab = GetControllerAt(0);
		bg = (GLoader)GetChildAt(0);
		com_Main_1 = (UISkinSell_Com_S5)GetChildAt(3);
		com_Goods = (UISkinSell_Com_Goods)GetChildAt(4);
		Cut_in_CN = GetTransitionAt(0);
		Cut_in_EN = GetTransitionAt(1);
		Cut_in_JP = GetTransitionAt(2);
	}
}
