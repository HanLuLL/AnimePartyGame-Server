using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIUpgradeWindow : GComponent
{
	public Controller type;

	public UIUpgrade_Com_PVPLabel com_PVPLabel;

	public UIUpgrade_Com_PVELabel com_PVELabel;

	public const string URL = "ui://6vzgbzmwpstt27";

	public static UIUpgradeWindow CreateInstance()
	{
		BindAll();
		return (UIUpgradeWindow)UIPackage.CreateObject("Upgrade", "UpgradeWindow");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://6vzgbzmwesjwx", typeof(UIUpgrade_Com_Star));
		UIObjectFactory.SetPackageItemExtension("ui://6vzgbzmwmsij1v", typeof(UIUpgrade_Com_Character));
		UIObjectFactory.SetPackageItemExtension("ui://6vzgbzmwot0w28", typeof(UIUpgrade_Com_PVPLabel));
		UIObjectFactory.SetPackageItemExtension("ui://6vzgbzmwot0w29", typeof(UIUpgrade_Com_PVELabel));
		UIObjectFactory.SetPackageItemExtension("ui://6vzgbzmwpstt27", typeof(UIUpgradeWindow));
		UIObjectFactory.SetPackageItemExtension("ui://6vzgbzmwqoeg2h", typeof(UIBG));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		type = GetControllerAt(0);
		com_PVPLabel = (UIUpgrade_Com_PVPLabel)GetChildAt(0);
		com_PVELabel = (UIUpgrade_Com_PVELabel)GetChildAt(1);
	}
}
