using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivityStoreSeason_Com_BottomBg : GComponent
{
	public Controller SetColor;

	public const string URL = "ui://begz6gfv7vwmt";

	public static UIActivityStoreSeason_Com_BottomBg CreateInstance()
	{
		return (UIActivityStoreSeason_Com_BottomBg)UIPackage.CreateObject("ActivityStoreSeason", "ActivityStoreSeason_Com_BottomBg");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		SetColor = GetControllerAt(0);
	}
}
