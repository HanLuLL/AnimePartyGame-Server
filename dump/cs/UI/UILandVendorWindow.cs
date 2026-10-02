using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UILandVendorWindow : GComponent
{
	public Controller Hide;

	public GTextField txt_HideTip;

	public GTextField txt_Gold;

	public GButton btn_Cancel;

	public UILandVendor_Button_Sure btn_Sure;

	public Transition Cut_in;

	public const string URL = "ui://fqs3emmdheyw0";

	public static UILandVendorWindow CreateInstance()
	{
		BindAll();
		return (UILandVendorWindow)UIPackage.CreateObject("LandVendor", "LandVendorWindow");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://fqs3emmdheyw0", typeof(UILandVendorWindow));
		UIObjectFactory.SetPackageItemExtension("ui://fqs3emmdheyw3", typeof(UILandVendor_Button_Sure));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		Hide = GetControllerAt(0);
		txt_HideTip = (GTextField)GetChildAt(0);
		txt_Gold = (GTextField)GetChildAt(12);
		btn_Cancel = (GButton)GetChildAt(14);
		btn_Sure = (UILandVendor_Button_Sure)GetChildAt(15);
		Cut_in = GetTransitionAt(0);
	}
}
