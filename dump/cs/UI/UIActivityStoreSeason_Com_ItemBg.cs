using FairyGUI;
using FairyGUI.Utils;

namespace UI;

public class UIActivityStoreSeason_Com_ItemBg : GComponent
{
	public Controller SetColor;

	public const string URL = "ui://begz6gfv7vwmz";

	public static UIActivityStoreSeason_Com_ItemBg CreateInstance()
	{
		return (UIActivityStoreSeason_Com_ItemBg)UIPackage.CreateObject("ActivityStoreSeason", "ActivityStoreSeason_Com_ItemBg");
	}

	public override void ConstructFromXML(XML xml)
	{
		base.ConstructFromXML(xml);
		SetColor = GetControllerAt(0);
	}
}
