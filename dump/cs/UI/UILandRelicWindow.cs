using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UILandRelicWindow : GComponent
{
	public Controller Hide;

	public Controller boardRole;

	public GTextField txt_HideTip;

	public GButton btn_Cancel;

	public UILandRelic_Button_Sure btn_Sure;

	public Transition cut_in;

	public const string URL = "ui://avtf6i27y7ym9";

	public static UILandRelicWindow CreateInstance()
	{
		BindAll();
		return (UILandRelicWindow)UIPackage.CreateObject("LandRelic", "LandRelicWindow");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://avtf6i27y7ym4", typeof(UILandRelic_Button_Sure));
		UIObjectFactory.SetPackageItemExtension("ui://avtf6i27y7ym9", typeof(UILandRelicWindow));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		Hide = GetControllerAt(0);
		boardRole = GetControllerAt(1);
		txt_HideTip = (GTextField)GetChildAt(0);
		btn_Cancel = (GButton)GetChildAt(6);
		btn_Sure = (UILandRelic_Button_Sure)GetChildAt(7);
		cut_in = GetTransitionAt(0);
	}
}
