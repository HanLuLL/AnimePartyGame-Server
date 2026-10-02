using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIAnniversary_2ndWindow : GComponent
{
	public Controller tab;

	public GLoader bg;

	public UIAnniversary_2nd_Com com_Main_1;

	public UIAnniversary_2nd_Com_Goods com_Goods;

	public Transition Cut_in_CN;

	public Transition Cut_in_EN;

	public Transition Cut_in_JP;

	public const string URL = "ui://k49wk9ftnqmf0";

	public static UIAnniversary_2ndWindow CreateInstance()
	{
		BindAll();
		return (UIAnniversary_2ndWindow)UIPackage.CreateObject("Anniversary_2Nd", "Anniversary_2ndWindow");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://k49wk9ftc76f51", typeof(UIAnniversary_2nd_Com_LabelItem));
		UIObjectFactory.SetPackageItemExtension("ui://k49wk9ftesyu4b", typeof(UIAnniversary_2nd_Com_CommonItem));
		UIObjectFactory.SetPackageItemExtension("ui://k49wk9ftnqmf0", typeof(UIAnniversary_2ndWindow));
		UIObjectFactory.SetPackageItemExtension("ui://k49wk9ftnqmf18", typeof(UIAnniversary_2nd_Button_Price));
		UIObjectFactory.SetPackageItemExtension("ui://k49wk9ftnqmf1e", typeof(UIAnniversary_2nd_Com));
		UIObjectFactory.SetPackageItemExtension("ui://k49wk9ftnqmf2a", typeof(UIAnniversary_2nd_Com_Goods));
		UIObjectFactory.SetPackageItemExtension("ui://k49wk9ftnqmf2b", typeof(UIAnniversary_2nd_Com_SkinItem));
		UIObjectFactory.SetPackageItemExtension("ui://k49wk9ftnqmf2c", typeof(UIAnniversary_2nd_Com_Item));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		tab = GetControllerAt(0);
		bg = (GLoader)GetChildAt(0);
		com_Main_1 = (UIAnniversary_2nd_Com)GetChildAt(1);
		com_Goods = (UIAnniversary_2nd_Com_Goods)GetChildAt(2);
		Cut_in_CN = GetTransitionAt(0);
		Cut_in_EN = GetTransitionAt(1);
		Cut_in_JP = GetTransitionAt(2);
	}
}
