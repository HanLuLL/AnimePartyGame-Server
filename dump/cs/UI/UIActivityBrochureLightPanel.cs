using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivityBrochureLightPanel : GComponent
{
	public Controller angelMode;

	public GLoader loader;

	public GButton btn_RightGoWay;

	public GButton btn_LeftGoWay;

	public const string URL = "ui://inpepupsk0vv0";

	public static UIActivityBrochureLightPanel CreateInstance()
	{
		BindAll();
		return (UIActivityBrochureLightPanel)UIPackage.CreateObject("ActivityBrochureLight", "ActivityBrochureLightPanel");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://inpepupsk0vv0", typeof(UIActivityBrochureLightPanel));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		angelMode = GetControllerAt(0);
		loader = (GLoader)GetChildAt(0);
		btn_RightGoWay = (GButton)GetChildAt(1);
		btn_LeftGoWay = (GButton)GetChildAt(2);
	}
}
