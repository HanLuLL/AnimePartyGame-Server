using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivityPopupWindow : GComponent
{
	public Controller type;

	public GLoader di;

	public UIActivityPopup_Com_TapTap com_Type1;

	public UIActivityPopup_Com_Crowdfunding com_Type2;

	public UIActivityPopup_Com_GoCnMobile com_type3;

	public GButton btn_Close;

	public Transition Cut_in;

	public const string URL = "ui://3tvdl51qsywh0";

	public static UIActivityPopupWindow CreateInstance()
	{
		BindAll();
		return (UIActivityPopupWindow)UIPackage.CreateObject("ActivityPopup", "ActivityPopupWindow");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://3tvdl51q8f672n", typeof(UIActivityPopup_Com_TapTap));
		UIObjectFactory.SetPackageItemExtension("ui://3tvdl51qfmns3m", typeof(UIActivityPopup_Com_GoCnMobile));
		UIObjectFactory.SetPackageItemExtension("ui://3tvdl51qnhqm35", typeof(UIActivityPopup_Com_Crowdfunding));
		UIObjectFactory.SetPackageItemExtension("ui://3tvdl51qnhqm39", typeof(UIActivityPopup_Com_Adv));
		UIObjectFactory.SetPackageItemExtension("ui://3tvdl51qnhqm3a", typeof(UIActivityPopup_Button_Adv));
		UIObjectFactory.SetPackageItemExtension("ui://3tvdl51qsywh0", typeof(UIActivityPopupWindow));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		type = GetControllerAt(0);
		di = (GLoader)GetChildAt(0);
		com_Type1 = (UIActivityPopup_Com_TapTap)GetChildAt(2);
		com_Type2 = (UIActivityPopup_Com_Crowdfunding)GetChildAt(3);
		com_type3 = (UIActivityPopup_Com_GoCnMobile)GetChildAt(4);
		btn_Close = (GButton)GetChildAt(5);
		Cut_in = GetTransitionAt(0);
	}
}
