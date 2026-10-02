using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UILandRollGoldWindow : GComponent
{
	public GList list_Gold;

	public const string URL = "ui://dfabevk6rum70";

	public static UILandRollGoldWindow CreateInstance()
	{
		BindAll();
		return (UILandRollGoldWindow)UIPackage.CreateObject("LandRollGold", "LandRollGoldWindow");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://dfabevk6p4oi6", typeof(UILandRollGold_Button_Gold));
		UIObjectFactory.SetPackageItemExtension("ui://dfabevk6rum70", typeof(UILandRollGoldWindow));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		list_Gold = (GList)GetChildAt(0);
	}
}
