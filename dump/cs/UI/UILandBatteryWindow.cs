using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UILandBatteryWindow : GComponent
{
	public UILandBattery_Com_BG com_Battery;

	public GList list_Players;

	public Transition Cut_in;

	public const string URL = "ui://dc8zac77rum70";

	public static UILandBatteryWindow CreateInstance()
	{
		BindAll();
		return (UILandBatteryWindow)UIPackage.CreateObject("LandBattery", "LandBatteryWindow");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://dc8zac77hmjfi", typeof(UILandBattery_Com_BG));
		UIObjectFactory.SetPackageItemExtension("ui://dc8zac77rum70", typeof(UILandBatteryWindow));
		UIObjectFactory.SetPackageItemExtension("ui://dc8zac77rum73", typeof(UILandBattery_Button_RoleHeadshot));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		com_Battery = (UILandBattery_Com_BG)GetChildAt(2);
		list_Players = (GList)GetChildAt(3);
		Cut_in = GetTransitionAt(0);
	}
}
