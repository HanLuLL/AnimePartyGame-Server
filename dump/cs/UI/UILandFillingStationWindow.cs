using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UILandFillingStationWindow : GComponent
{
	public GButton btn_Continue;

	public UILandFillingStation_Button_Stop btn_Stop;

	public Transition t0;

	public const string URL = "ui://dhoi6vx2rum70";

	public static UILandFillingStationWindow CreateInstance()
	{
		BindAll();
		return (UILandFillingStationWindow)UIPackage.CreateObject("LandFillingStation", "LandFillingStationWindow");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://dhoi6vx2rum70", typeof(UILandFillingStationWindow));
		UIObjectFactory.SetPackageItemExtension("ui://dhoi6vx2rum74", typeof(UILandFillingStation_Button_Stop));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		btn_Continue = (GButton)GetChildAt(0);
		btn_Stop = (UILandFillingStation_Button_Stop)GetChildAt(1);
		t0 = GetTransitionAt(0);
	}
}
