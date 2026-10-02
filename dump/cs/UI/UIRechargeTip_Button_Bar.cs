using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIRechargeTip_Button_Bar : GButton
{
	public Controller status;

	public const string URL = "ui://c20i191crle7r";

	public static UIRechargeTip_Button_Bar CreateInstance()
	{
		return (UIRechargeTip_Button_Bar)UIPackage.CreateObject("RechargeTip", "RechargeTip_Button_Bar");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		status = GetControllerAt(1);
	}
}
