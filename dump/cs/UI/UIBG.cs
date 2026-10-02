using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIBG : GComponent
{
	public Controller colortype;

	public const string URL = "ui://6vzgbzmwqoeg2h";

	public static UIBG CreateInstance()
	{
		return (UIBG)UIPackage.CreateObject("Upgrade", "BG");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		colortype = GetControllerAt(0);
	}
}
