using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIRelic_button_Select : GButton
{
	public Controller status;

	public const string URL = "ui://kjm6oxy2iorl3";

	public static UIRelic_button_Select CreateInstance()
	{
		return (UIRelic_button_Select)UIPackage.CreateObject("Relic", "Relic_button_Select");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		status = GetControllerAt(1);
	}
}
