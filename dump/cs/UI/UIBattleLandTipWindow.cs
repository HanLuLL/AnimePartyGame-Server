using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIBattleLandTipWindow : GComponent
{
	public GComponent com_land;

	public const string URL = "ui://0xrcutybine4s7u";

	public static UIBattleLandTipWindow CreateInstance()
	{
		BindAll();
		return (UIBattleLandTipWindow)UIPackage.CreateObject("BattleLandTip", "BattleLandTipWindow");
	}

	private static void BindAll()
	{
		UIObjectFactory.SetPackageItemExtension("ui://0xrcutybine4s7u", typeof(UIBattleLandTipWindow));
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		com_land = (GComponent)GetChildAt(0);
	}
}
